namespace credit.loans.Data.Entities;

public class PaymentLedger
{
    public Guid Id { get; set; }
    public DateTime PaymentDate { get; set; }
    public Guid LoanId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid InstitutionId { get; set; }
    public double Amount { get; set; }
}