using credit.customers.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace credit.customers.Data;

public class CustomerDbContext(DbContextOptions<CustomerDbContext> options) : DbContext(options)
{
    public DbSet<Customers> Customers => Set<Customers>();
    public DbSet<Litigation> Litigation => Set<Litigation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customers>(entity =>
        {
            entity.ToTable("customers");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CivilId).HasColumnName("civil_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.IsEligible).HasColumnName("is_eligible");
        });

        modelBuilder.Entity<Litigation>(entity =>
        {
            entity.ToTable("litigation");
            entity.Property(e => e.LitigationId).HasColumnName("litigation_id");
            entity.Property(e => e.LoanId).HasColumnName("loan_id");
            entity.Property(e => e.InstitutionId).HasColumnName("institution_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.DateOfVerdict).HasColumnName("date_of_verdict");
        });
    }
}