using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace JobBoardAPI.Entities
{
    public class JobBoardDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=JobBoardDb;Trusted_Connection=True;";
        public DbSet<JobAdvertisement> JobAdvertisements { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // JobAdvertisement
            modelBuilder.Entity<JobAdvertisement>()
                .Property(ja => ja.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<JobAdvertisement>()
                .Property(ja => ja.CompanyName)
                .IsRequired()
                .HasMaxLength(70);
            modelBuilder.Entity<JobAdvertisement>()
                .Property(ja => ja.Responsibilities)
                .IsRequired();

            // JobApplication
            modelBuilder.Entity<JobApplication>()
                .Property(ja => ja.CoverLetter)
                .IsRequired();

            // Category
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(35);

            // Address
            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(40);
            modelBuilder.Entity<Address>()
                .Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(60);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
