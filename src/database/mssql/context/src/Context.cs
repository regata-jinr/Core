/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2020, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/


using Microsoft.EntityFrameworkCore;
using Regata.Core.DB.MSSQL.Models;
using AdysTech;

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
        public DbSet<SharedSpectra>      SharedSpectras  { get; set; }
        public DbSet<SetSpectraSLI>      SLISpectras     { get; set; }
        public DbSet<SetSpectraLLI>      LLISpectras     { get; set; }

        private readonly string _csTraget;

        public RegataContext(string ConnectionStringTarget)
        {
            _csTraget = ConnectionStringTarget;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AdysTech.CredentialManager.CredentialManager.GetCredentials(_csTraget).Password, 
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
                            .HasKey(i => new
                            {
                                i.Id
                            });

            modelBuilder.Entity<Measurement>()
                            .HasKey(m => new
                            {
                                m.Id
                            });

            modelBuilder.Entity<MeasurementsRegisterInfo>()
                           .HasKey(mr => new
                           {
                               mr.Id
                           });

            modelBuilder.Entity<SetSpectraLLI>()
                 .HasKey(slis => slis.SampleSpectra);
            modelBuilder.Entity<SetSpectraSLI>()
                   .HasKey(llis => llis.SampleSpectra);
            modelBuilder.Entity<SharedSpectra>()
                   .HasKey(ss => ss.fileS);

        }

    } // public class WeightingContext : DbContext
} // namespace SamplesWeighting

