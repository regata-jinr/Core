using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareSpectra
{
    public class InfoContext : DbContext
    {
        public DbSet<SharedSpectra> Spectra { get; set; }

        private readonly string connectionString = AdysTech.CredentialManager.CredentialManager.GetCredentials("NAA_DB").Password;

        public InfoContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SharedSpectra>()
                   .HasKey(s => new { s.token });
        }
    }

    [Table("sharedspectra")]
    public class SharedSpectra
    {
        public string fileS { get; set; }
        public string token { get; set; }
    }
}
