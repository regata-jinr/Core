using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;


namespace Regata.Utilities
{

    [Table("SharedSpectra")]
    public class SharedSpectra
    {
        public string fileS { get; set; }
        public string token { get; set; }
    }

    public class SetSpectraSLI
    {
        public string SampleType { get; set; }
        public string SampleSpectra { get; set; }
        public string token { get; set; }
    }

    public class SetSpectraLLI
    {
        public int LoadNumber { get; set; }
        public string irtype { get; set; }
        public short Container { get; set; }
        public string SampleType { get; set; }
        public string SampleSpectra { get; set; }
        public string token { get; set; }
    }

    public class SpectraContext : DbContext
    {
        public DbSet<SharedSpectra> SharedSpectras { get; set; }
        public DbSet<SetSpectraSLI> SLISpectras { get; set; }
        public DbSet<SetSpectraLLI> LLISpectras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Settings.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SetSpectraLLI>()
                   .HasKey(slis => slis.SampleSpectra);
            modelBuilder.Entity<SetSpectraSLI>()
                   .HasKey(llis => llis.SampleSpectra);
            modelBuilder.Entity<SharedSpectra>()
                   .HasKey(ss => ss.fileS);
        }

    }

    public enum IrradiationType { SLI, LLI1, LLI2, BCKG }

    public static class SpectraTools
    {
        // TODO: implement GetBackgroundSpectra
        public static IReadOnlyDictionary<IrradiationType, string> IrradiationTypeMap = new Dictionary<IrradiationType, string>()
                                                                                {
                                                                                    {IrradiationType.SLI, "SLI"},
                                                                                    {IrradiationType.LLI1, "LLI-1"},
                                                                                    {IrradiationType.LLI2, "LLI-2"},
                                                                                    {IrradiationType.BCKG, "BCKG"}
                                                                                 };

        public static async Task GetBackgroundSpectra(string SetKey, CancellationToken? ct = null)
        {
            throw new NotImplementedException();

        }

        public static async IAsyncEnumerable<SetSpectraSLI> SLISetSpectrasAsync(string SetKey, CancellationToken ct)
        {
            var sks = SetKey.Split('-');
            using (var ssc = new SpectraContext())
            {
                var sliSpectras = ssc.SLISpectras.FromSqlRaw("exec FormSLIFilesList @countryCode, @clientid, @year, @setnum, @setind",
                    new SqlParameter("countrycode", sks[0]),
                    new SqlParameter("clientid", sks[1]),
                    new SqlParameter("year", sks[2]),
                    new SqlParameter("setnum", sks[3]),
                    new SqlParameter("setind", sks[4])).AsAsyncEnumerable().WithCancellation(ct);
                await foreach (var spectraInfo in sliSpectras)
                    yield return spectraInfo;
            }
        }

        public static async IAsyncEnumerable<SetSpectraLLI> LLISetSpectrasAsync(string SetKey, IrradiationType type, CancellationToken ct)
        {
            var sks = SetKey.Split('-');
            using (var ssc = new SpectraContext())
            {
                var lliSpectras = ssc.LLISpectras.FromSqlRaw("exec FormLLIFilesList @countryCode, @clientid, @year, @setnum, @setind, @type",
                    new SqlParameter("countrycode", sks[0]),
                    new SqlParameter("clientid", sks[1]),
                    new SqlParameter("year", sks[2]),
                    new SqlParameter("setnum", sks[3]),
                    new SqlParameter("setind", sks[4]),
                    new SqlParameter("type", IrradiationTypeMap[type])).AsAsyncEnumerable().WithCancellation(ct);

                await foreach (var spectraInfo in lliSpectras)
                    yield return spectraInfo;
            }
        }

        public static async Task DownloadSpectraAsync(string spectra, string path, CancellationToken ct)
        {
            using (var ssc = new SpectraContext())
            {
                var sharedSpectra = ssc.SharedSpectras.Where(ss => ss.fileS == spectra).FirstOrDefault();
                if (sharedSpectra == null) throw new FileNotFoundException($"Spectra '{spectra}' was not found");
                await WebDavClientApi.DownloadFile(sharedSpectra.token, $"{path}/{spectra}.cnf", ct);
            }
        }
    }
}
