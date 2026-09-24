using System.ComponentModel.DataAnnotations;

namespace credit.identity.Data.Entities;

public class Institutes
{
    public Guid InstituteId { get; set; }
    [MaxLength(255)] public required string InstituteName { get; set; }
}