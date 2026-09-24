using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace credit.identity.Data.Entities;

[PrimaryKey(nameof(InstituteId))]
public class Institutes
{
    public Guid InstituteId { get; set; }
    [MaxLength(255)] public required string InstituteName { get; set; }
}