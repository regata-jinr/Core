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
using Regata.Core.DataBase.Models;
using AdysTech.CredentialManager;
using Regata.Core.Settings;

namespace Regata.Core.DataBase
{
    public class RegataContext : DbContext
    {
        public static string ConString;

        public DbSet<Sample>               Samples               { get; set; }
        public DbSet<ReweightInfo>         ReweightInfoes        { get; set; }
        public DbSet<SamplesSet>           SamplesSets           { get; set; }
        public DbSet<Standard>             Standards             { get; set; }
        public DbSet<StandardSet>          StandardSets          { get; set; }
        public DbSet<Monitor>              Monitors              { get; set; }
        public DbSet<MonitorSet>           MonitorSets           { get; set; }
        public DbSet<Irradiation>          Irradiations          { get; set; }
        public DbSet<Measurement>          Measurements          { get; set; }
        public DbSet<MeasurementsRegister> MeasurementsRegisters { get; set; }
        public DbSet<SharedSpectrum>       SharedSpectra         { get; set; }
        public DbSet<SpectrumSLI>          SLISpectra            { get; set; }
        public DbSet<SpectrumLLI>          LLISpectra            { get; set; }
        public DbSet<UILabel>              UILabels              { get; set; }
        public DbSet<MessageBase>          MessageBases          { get; set; }
        public DbSet<MessageDefault>       MessageDefaults       { get; set; }
        public DbSet<User>                 Users                 { get; set; }
        public DbSet<Log>                  Logs                  { get; set; }
        public DbSet<Position>             Positions             { get; set; }

        public RegataContext()
        {
            if (string.IsNullOrEmpty(ConString))
                ConString = CredentialManager.GetCredentials(GlobalSettings.Targets.DB).Password;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)   
        {
            optionsBuilder.UseSqlServer(ConString, 
                                        options => 
                                            {
                                                options.EnableRetryOnFailure(3);
                                                options.CommandTimeout(60);
                                            }
                                       );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Irradiation>()
                            .HasKey(i => i.Id);

            modelBuilder.Entity<Measurement>()
                            .HasKey(m => m.Id);
            
            modelBuilder.Entity<MeasurementsRegister>()
                           .HasKey(mr => mr.Id);

            modelBuilder.Entity<SpectrumLLI>()
                           .HasKey(slis => slis.SampleSpectra);

            modelBuilder.Entity<SpectrumSLI>()
                           .HasKey(llis => llis.SampleSpectra);

            modelBuilder.Entity<SharedSpectrum>()
                           .HasKey(ss => ss.fileS);

            modelBuilder.Entity<UILabel>()
                           .HasKey(l => new { l.FormName, l.ComponentName });

            modelBuilder.Entity<MessageBase>()
                           .HasKey(m => (new { m.Code, m.Language}));

            modelBuilder.Entity<MessageDefault>()
                           .HasKey(m => new { m.Language, m.ExpandButtonText });

            modelBuilder.Entity<User>()
                           .HasKey(u => new { u.Id });

            modelBuilder.Entity<Log>()
                           .HasKey(l => new { l.Id });

            modelBuilder.Entity<Sample>()
                               .HasKey(s => new
                               {
                                   s.CountryCode,
                                   s.ClientNumber,
                                   s.Year,
                                   s.SetNumber,
                                   s.SetIndex,
                                   s.SampleNumber
                               });

            modelBuilder.Entity<ReweightInfo>()
                              .HasKey(ri => new
                              {
                                  ri.LoadNumber,
                                  ri.CountryCode,
                                  ri.ClientNumber,
                                  ri.Year,
                                  ri.SetNumber,
                                  ri.SetIndex,
                                  ri.SampleNumber
                              });

            modelBuilder.Entity<SamplesSet>()
                               .HasKey(s => new
                               {
                                   s.CountryCode,
                                   s.ClientNumber,
                                   s.Year,
                                   s.SetNumber,
                                   s.SetIndex
                               });

            modelBuilder.Entity<Standard>()
                               .HasKey(srm => new
                               {
                                   srm.SetName,
                                   srm.SetNumber,
                                   srm.Number
                               });

            modelBuilder.Entity<StandardSet>()
                    .HasKey(srm => new
                    {
                        srm.SetName,
                        srm.SetNumber
                    });

            modelBuilder.Entity<Monitor>()
                               .HasKey(m => new
                               {
                                   m.SetName,
                                   m.SetNumber,
                                   m.Number
                               });

            modelBuilder.Entity<MonitorSet>()
                              .HasKey(m => new
                              {
                                  m.SetName,
                                  m.SetNumber
                              });

            modelBuilder.Entity<Position>()
                              .HasKey(p => new
                              {
                                  p.Name, p.SerialNumber, p.Detector
                              });

            #region to be added



            //modelBuilder.Entity<MonitorsSet>()
            //                   .HasKey(s => new
            //                   {
            //                       s.Monitor_Set_Name,
            //                       s.Monitor_Set_Number
            //                   });


            //modelBuilder.Entity<SRMsSet>()
            //                   .HasKey(s => new
            //                   {
            //                       s.SRM_Set_Name,
            //                       s.SRM_Set_Number
            //                   });

            #endregion

        }

    } // public class WeightingContext : DbContext
} // namespace SamplesWeighting
