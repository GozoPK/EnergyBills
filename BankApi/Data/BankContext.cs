using System;
using BankApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(p => p.Balance)
                .HasPrecision(13, 2);

            modelBuilder.Entity<Transaction>()
                .Property(p => p.Amount)
                .HasPrecision(13,2);
        }
    }
}