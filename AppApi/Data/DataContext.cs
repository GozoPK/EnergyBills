using AppApi.Entities;
using AppApi.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<UserBill> Bills { get; set; }

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

            modelBuilder.Entity<UserEntity>()
                .HasMany(p => p.UserBills)
                .WithOne(p => p.UserEntity)
                .HasForeignKey(p => p.UserEntityId)
                .IsRequired();

            modelBuilder.Entity<UserBill>()
                .Property(p => p.Ammount)
                .HasColumnType("decimal(13,2)");
            
            modelBuilder.Entity<UserBill>()
                .Property(p => p.AmmountToReturn)
                .HasColumnType("decimal(13,2)");

            modelBuilder.Entity<UserBill>()
                .HasIndex(p => p.BillNumber)
                .IsUnique();

            modelBuilder.Entity<UserBill>()
                .Property(p => p.Status)
                .HasConversion(
                    p => p.ToString(),
                    p => (Status)Enum.Parse(typeof(Status), p)
                );

            modelBuilder.Entity<UserBill>()
                .Property(p => p.Type)
                .HasConversion(
                    p => p.ToString(),
                    p => (BillType)Enum.Parse(typeof(BillType), p)
                );

            modelBuilder.Entity<UserBill>()
                .Property(p => p.Year)
                .HasColumnType("year(4)");
        }
    }
}