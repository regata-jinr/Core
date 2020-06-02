using Microsoft.EntityFrameworkCore;

namespace UITemplateWinFormTest
{
   public class InfoContext : DbContext
    {
        public DbSet<SamplesSetModel> Sets{ get; set; }

        private readonly string connectionString = @"Data Source=RUMLAB\REGATALOCAL;Initial Catalog=NAA_DB_TEST;Integrated Security=True;";
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
            modelBuilder.Entity<SamplesSetModel>()
                   .HasKey(s => new { s.Country_Code, s.Client_ID, s.Year, s.Sample_Set_ID, s.Sample_Set_Index });
        }
    }
}
