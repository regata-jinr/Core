using System;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;

namespace GSI.Core.SpectraFileParser
{
    class Program
    {
        private const string conStr = @"Server=RUMLAB\REGATALOCAL;Database=NAA_DB_TEST;Trusted_Connection=True;";
        static void Main(string[] args)
        {
            try
            {
                var loadedFiles = new List<string>();
                using (var ic = new InfoContext(conStr))
                {
                    loadedFiles = ic.Measurements.Select(m => m.FileSpectra).ToList();
                }
                Console.WriteLine("Initialise spectra parsing");
                var files = Directory.GetFiles(@"D:\Spectra", "*.cnf", SearchOption.AllDirectories).ToList();

                //files = files.Take(1).ToList();
                //files[0] = @"D:\Spectra\2020\04\dji-1\1108126.cnf";
                Console.WriteLine($"Total files number - {files.Count}");


                foreach (var l in loadedFiles)
                {
                    var f = files.Where(f => f.Contains(l)).FirstOrDefault();
                    if (!string.IsNullOrEmpty(f))
                       files.Remove(f);
                }

                Console.WriteLine($"Files number for processing - {files.Count}");

                files.AsParallel().ForAll(f => ProcessFile(f));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Parsing complete");
        }
      
        private static void ProcessFile(string file)
        {
            try
            {
                Console.WriteLine($"Processing of file '{file}'");
                var spectra = new GSI.Core.Spectra(file);
                var sk = spectra.Sample.Id.Split('-');
                var pref = "";

                if (sk[0] == "s")
                    pref = "s";
                if (sk[0] == "m")
                    pref = "m";

                if (string.IsNullOrEmpty(pref))
                {
                    Console.WriteLine($"Wrong file - '{file}'");
                    return;
                }

                using (var ic = new InfoContext(conStr))
                {
                    var oper = spectra.Sample.Description.Split('_')[0].ToLower();
                    var assist = ic.Empls.Where(e => e.LastName.ToLower() == oper).FirstOrDefault();
                    int? pid = null;
                    if (assist != null)
                        pid = assist.PersonalId;
                    var mi = new MeasurementInfo
                    {
                        CountryCode = pref,
                        ClientNumber = pref,
                        Year = pref,
                        SetNumber = sk[1],
                        SetIndex = sk[2],
                        SampleNumber = sk[3],
                        Type = spectra.Sample.Type,
                        AcqMode = spectra.Sample.AcqMod,
                        Height = spectra.Sample.Geometry,
                        DateTimeStart = spectra.Sample.AcqStartDate,
                        Duration = (int)spectra.Sample.Duration,
                        DeadTime = spectra.DeadTime,
                        DateTimeFinish = null,
                        FileSpectra = Path.GetFileNameWithoutExtension(file),
                        Detector = $"D{Path.GetFileName(file).Substring(0, 1)}",
                        Token = null,
                        Assistant = pid,
                        Note = null
                    };
                    Console.WriteLine(mi);
                    ic.Add(mi);
                    ic.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

    static class ObjectHelper
    {
        public static void Dump<T>(this T x, string prem)
        {
            Console.WriteLine($"{prem} {x}");
        }
    }

    public class MeasurementInfo 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? IrradiationId { get; set; }
        [Required]
        public int? MRegId { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string ClientNumber { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string SetNumber { get; set; }
        [Required]
        public string SetIndex { get; set; }
        [Required]
        public string SampleNumber { get; set; }
        [Required]
        public string Type { get; set; }
        public string AcqMode { get; set; }
        public float? Height { get; set; }
        public DateTime? DateTimeStart { get; set; }
        public int? Duration { get; set; }
        public float? DeadTime { get; set; }
        public DateTime? DateTimeFinish { get; set; }
        public string FileSpectra { get; set; }
        public string Detector { get; set; }
        public string Token { get; set; }
        public int? Assistant { get; set; }
        public string Note { get; set; }


        [NotMapped]
        public string SetKey => $"{CountryCode}-{ClientNumber}-{Year}-{SetNumber}-{SetIndex}";

        [NotMapped]
        public string SampleKey => $"{SetIndex}-{SampleNumber}";
        public override string ToString() => $"{SetKey}-{SampleNumber}";
    }

    [Table("Staff")]
    public class Staff
    {
        public int PersonalId { get; set; }
        [Required]
        public string LastName { get; set; }
    }


    public class InfoContext : DbContext
    {
        public DbSet<MeasurementInfo> Measurements { get; set; }
        public DbSet<Staff> Empls { get; set; }

        private readonly string _conString;

        public InfoContext(string conStr) : base()
        {
            _conString = conStr;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeasurementInfo>()
                .HasIndex(c => c.FileSpectra)
                .IsUnique();

            modelBuilder.Entity<Staff>()
               .HasKey(c => c.PersonalId);

        }
    }
}

