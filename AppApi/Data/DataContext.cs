using AppApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .Property(p => p.AnnualIncome)
                .HasColumnType("decimal(13,2)");

            modelBuilder.Entity<UserEntity>()
                .HasIndex(p => p.Afm)
                .IsUnique();

            modelBuilder.Entity<UserEntity>()
                .HasIndex(p => p.Email)
                .IsUnique();
        }
    }
}