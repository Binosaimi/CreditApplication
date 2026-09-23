using System.ComponentModel.DataAnnotations;

namespace credit.identity.Data.Entities;

public class Roles
{
    public Guid Id { get; set; }
    [MaxLength(20)] public required string Name { get; set; }
}