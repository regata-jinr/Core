/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2017-2020, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

using Microsoft.EntityFrameworkCore;

namespace SamplesWeighting
{
    public class WeightingContext : DbContext
    {
        public DbSet<Monitor>          Monitors        { get; set; }
        public DbSet<MonitorsSet>      MonitorsSets    { get; set; }
        public DbSet<SRM>              SRMs            { get; set; }
        public DbSet<SRMsSet>          SRMsSets        { get; set; }
        public DbSet<Sample>           Samples         { get; set; }
        public DbSet<SamplesSet>       SamplesSets     { get; set; }
        public DbSet<Irradiations>     Irradiations    { get; set; }
        public DbSet<reweightInfo>     Reweights       { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionString, 
                                        options => 
                                            {
                                                options.EnableRetryOnFailure(3);
                                                options.CommandTimeout(60); 
                                            }
                                       );
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sample>()
                               .HasKey(s => new
                               {
                                   s.Country_Code,
                                   s.Client_Id,
                                   s.Year,
                                   s.Sample_Set_Id,
                                   s.Sample_Set_Index,
                                   s.A_Sample_ID
                               });

            modelBuilder.Entity<SamplesSet>()
                               .HasKey(s => new
                               {
                                   s.Country_Code,
                                   s.Client_Id,
                                   s.Year,
                                   s.Sample_Set_Id,
                                   s.Sample_Set_Index
                               });

            modelBuilder.Entity<Monitor>()
                               .HasKey(s => new
                               {
                                   s.Monitor_Set_Name,
                                   s.Monitor_Set_Number,
                                   s.Monitor_Number
                               });

            modelBuilder.Entity<MonitorsSet>()
                               .HasKey(s => new
                               {
                                   s.Monitor_Set_Name,
                                   s.Monitor_Set_Number
                               });

            modelBuilder.Entity<SRM>()
                               .HasKey(s => new
                               {
                                   s.SRM_Set_Name,
                                   s.SRM_Set_Number,
                                   s.SRM_Number
                               });

            modelBuilder.Entity<SRMsSet>()
                               .HasKey(s => new
                               {
                                   s.SRM_Set_Name,
                                   s.SRM_Set_Number
                               });
            modelBuilder.Entity<Irradiations>()
                            .HasKey(s => new
                            {
                                s.Date_Start,
                                s.loadNumber,
                                s.Country_Code,
                                s.Client_Id,
                                s.Year,
                                s.Sample_Set_Id,
                                s.Sample_Set_Index,
                                s.Sample_ID
                            });
            modelBuilder.Entity<reweightInfo>().HasKey(s => new
            {
                s.loadNumber,
                s.Country_Code,
                s.Client_Id,
                s.Year,
                s.Sample_Set_Id,
                s.Sample_Set_Index,
                s.Sample_ID
            });
        }

    } // public class WeightingContext : DbContext
} // namespace SamplesWeighting

