using AppApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .Property(p => p.AnnualIncome)
                .HasPrecision(13, 2);

            modelBuilder.Entity<UserEntity>()
                .HasIndex(p => p.Afm)
                .IsUnique();
        }
    }
}