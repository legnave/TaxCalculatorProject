using Microsoft.EntityFrameworkCore;
using TaxCalculator.Models;

public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaxCalculation> TaxCalculations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxCalculation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PostalCode).IsRequired();
            entity.Property(e => e.AnnualIncome).IsRequired();
            entity.Property(e => e.CalculatedTax).IsRequired();
            entity.Property(e => e.CalculationDate).IsRequired();

        });

        base.OnModelCreating(modelBuilder);
    }
}
