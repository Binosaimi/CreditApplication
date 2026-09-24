using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace credit.identity.Data.Entities;

[PrimaryKey(nameof(RoleId))]
public class Roles
{
    public Guid RoleId { get; set; }
    [MaxLength(20)] public required string RoleName { get; set; }
}