/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using Microsoft.EntityFrameworkCore;
using Regata.Core.DB.MSSQL.Models;
using AdysTech.CredentialManager;

namespace Regata.Core.DB.MSSQL.Context
{
    public class RegataContext : DbContext
    {
        //public DbSet<Monitor>          Monitors        { get; set; }
        //public DbSet<MonitorsSet>      MonitorsSets    { get; set; }
        //public DbSet<SRM>              SRMs            { get; set; }
        //public DbSet<SRMsSet>          SRMsSets        { get; set; }
        //public DbSet<Sample>           Samples         { get; set; }
        //public DbSet<SamplesSet>       SamplesSets     { get; set; }
        //public DbSet<reweightInfo>     Reweights       { get; set; }

        public DbSet<Irradiation>        Irradiations    { get; set; }
        public DbSet<Measurement>        Measurements    { get; set; }
        public DbSet<SharedSpectrum>     SharedSpectra   { get; set; }
        public DbSet<SpectrumSLI>        SLISpectra      { get; set; }
        public DbSet<SpectrumLLI>        LLISpectra      { get; set; }
        public DbSet<UILabel>            UILabels        { get; set; }

        private const string DBTarget = "RegataDB";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CredentialManager.GetCredentials(DBTarget).Password, 
                                        options => 
                                            {
                                                options.EnableRetryOnFailure(3);
                                                options.CommandTimeout(60);
                                            }
                                       );
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Sample>()
            //                   .HasKey(s => new
            //                   {
            //                       s.Country_Code,
            //                       s.Client_Id,
            //                       s.Year,
            //                       s.Sample_Set_Id,
            //                       s.Sample_Set_Index,
            //                       s.A_Sample_ID
            //                   });

            //modelBuilder.Entity<SamplesSet>()
            //                   .HasKey(s => new
            //                   {
            //                       s.Country_Code,
            //                       s.Client_Id,
            //                       s.Year,
            //                       s.Sample_Set_Id,
            //                       s.Sample_Set_Index
            //                   });

            //modelBuilder.Entity<Monitor>()
            //                   .HasKey(s => new
            //                   {
            //                       s.Monitor_Set_Name,
            //                       s.Monitor_Set_Number,
            //                       s.Monitor_Number
            //                   });

            //modelBuilder.Entity<MonitorsSet>()
            //                   .HasKey(s => new
            //                   {
            //                       s.Monitor_Set_Name,
            //                       s.Monitor_Set_Number
            //                   });

            //modelBuilder.Entity<SRM>()
            //                   .HasKey(s => new
            //                   {
            //                       s.SRM_Set_Name,
            //                       s.SRM_Set_Number,
            //                       s.SRM_Number
            //                   });

            //modelBuilder.Entity<SRMsSet>()
            //                   .HasKey(s => new
            //                   {
            //                       s.SRM_Set_Name,
            //                       s.SRM_Set_Number
            //                   });

            //modelBuilder.Entity<reweightInfo>().HasKey(s => new
            //{
            //    s.loadNumber,
            //    s.Country_Code,
            //    s.Client_Id,
            //    s.Year,
            //    s.Sample_Set_Id,
            //    s.Sample_Set_Index,
            //    s.Sample_ID
            //});

            modelBuilder.Entity<Irradiation>()
                            .HasKey(i => i.Id);

            modelBuilder.Entity<Measurement>()
                            .HasKey(m => m.Id);

            modelBuilder.Entity<MeasurementsRegisterInfo>()
                           .HasKey(mr => mr.Id);

            modelBuilder.Entity<SpectrumLLI>()
                 .HasKey(slis => slis.SampleSpectra);

            modelBuilder.Entity<SpectrumSLI>()
                   .HasKey(llis => llis.SampleSpectra);

            modelBuilder.Entity<SharedSpectrum>()
                   .HasKey(ss => ss.fileS);

            modelBuilder.Entity<UILabel>()
                .HasKey(l => new { l.FormName, l.ComponentName });

        }


        public bool CanConnect()
        {
            if (Database.CanConnect())
            {
                Report.Notify(Codes.SUCC_DB_CONN);
                return true;
            }
            else
            {
                Report.Notify(Codes.ERR_DB_CON);
                return false;
            }    
        }

    } // public class WeightingContext : DbContext
} // namespace SamplesWeighting

