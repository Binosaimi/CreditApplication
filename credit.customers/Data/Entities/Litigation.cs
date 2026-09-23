using System.ComponentModel.DataAnnotations;

namespace credit.customers.Data.Entities;

public class Litigation
{
    public Guid Id { get; set; }
    public Guid LoanId { get; set; }
    public Guid InstitutionId { get; set; }
    public Guid CustomerId { get; set; }
    [MaxLength(40)]
    public required string Status { get; set; }
    public DateTime DateOfVerdict { get; set; }
}