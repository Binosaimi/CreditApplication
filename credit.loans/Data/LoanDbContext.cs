using credit.loans.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace credit.loans.Data;

public class LoanDbContext(DbContextOptions<LoanDbContext> options) : DbContext(options)
{
    public DbSet<Loans> Loans => Set<Loans>();
    public DbSet<Delinquencies> Delinquencies => Set<Delinquencies>();
    public DbSet<PaymentLedger> PaymentLedger => Set<PaymentLedger>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Loans>(entity =>
        {
            entity.ToTable("loans");
            entity.Property(e => e.LoanId).HasColumnName("loan_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.InstitutionId).HasColumnName("institution_id");
            entity.Property(e => e.LoanStartDate).HasColumnName("loan_start_date");
            entity.Property(e => e.Tenor).HasColumnName("tenor");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.Status).HasColumnName("status");
        });
        
        modelBuilder.Entity<PaymentLedger>(entity =>
        {
            entity.ToTable("PaymentLedger");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.LoanId).HasColumnName("loan_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.InstitutionId).HasColumnName("institution_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
        });
        
        modelBuilder.Entity<Delinquencies>(entity =>
        {
            entity.ToTable("Delinquencies");
            entity.Property(e => e.DelinquencyId).HasColumnName("id");
            entity.Property(e => e.LoanId).HasColumnName("loan_id");
            entity.Property(e => e.DelinquencyDate).HasColumnName("delinquency_date");
        });
    }
}