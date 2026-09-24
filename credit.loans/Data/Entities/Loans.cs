namespace credit.loans.Data.Entities;

public class Loans
{
    public Guid LoanId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid InstitutionId { get; set; }
    public DateTime LoanStartDate { get; set; }
    public int Tenor { get; set; }
    public double Amount { get; set; }
    public double Rate { get; set; }
    public string? Status { get; set; }
}