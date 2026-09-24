using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace credit.customers.Data.Entities;

[PrimaryKey(nameof(LitigationId))]
public class Litigation
{
    public Guid LitigationId { get; set; }
    public Guid LoanId { get; set; }
    public Guid InstitutionId { get; set; }
    public Guid CustomerId { get; set; }
    [MaxLength(40)]
    public required string Status { get; set; }
    public DateTime DateOfVerdict { get; set; }
}