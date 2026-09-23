namespace credit.loans.Data.Entities;

public class Delinquencies
{
    public Guid Id { get; set; }
    public Guid LoanId { get; set; }
    public DateTime DelinquencyDate { get; set; }
    public Loans Loans { get; set; } = null!;
}