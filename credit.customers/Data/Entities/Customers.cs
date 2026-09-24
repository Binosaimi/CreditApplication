using System.ComponentModel.DataAnnotations;

namespace credit.customers.Data.Entities;

using Microsoft.EntityFrameworkCore;

[Index(nameof(CivilId), IsUnique = true)]
[PrimaryKey(nameof(CustomerId))]
public class Customers
{
    public Guid CustomerId { get; set; }
    [MaxLength(12)] public required string CivilId { get; set; }
    [MaxLength(100)] public required string Name { get; set; }
    public required DateOnly Dob { get; set; }
    public required bool IsEligible { get; set; }
}