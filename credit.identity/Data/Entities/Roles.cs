using System.ComponentModel.DataAnnotations;

namespace credit.identity.Data.Entities;

public class Roles
{
    public Guid RoleId { get; set; }
    [MaxLength(20)] public required string RoleName { get; set; }
}