using Microsoft.EntityFrameworkCore;
using TaxisNetSimApi.Entities;

namespace TaxisNetSimApi.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TaxisNetUserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaxisNetUserEntity>()
                .Property(p => p.AnnualIncome)
                .HasColumnType("decimal(13,2)");
        }
    }
}