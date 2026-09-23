using System.ComponentModel.DataAnnotations;

namespace credit.identity.Data.Entities;

public class Institutes
{
    public Guid Id { get; set; }
    [MaxLength(255)] public required string Name { get; set; }
}