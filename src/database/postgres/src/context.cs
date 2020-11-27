/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/


using Microsoft.EntityFrameworkCore;
using AdysTech.CredentialManager;
using Regata.Core.DataBase.Postgres.Models;

namespace Regata.Core.DataBase.Postgres.Context
{
    public class LogContext : DbContext
    {
        private readonly string _connectionStringTarget;

        public LogContext(string ConStringTarget)
        {
            _connectionStringTarget = ConStringTarget;
        }

        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(CredentialManager.GetCredentials(_connectionStringTarget).Password);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>()
                               .HasKey(l => l.id);
        }

    }
}
