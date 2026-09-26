using Microsoft.EntityFrameworkCore;

namespace credit.identity.Data.Entities;

[PrimaryKey(nameof(UserId))]
public class Users
{
    public Guid UserId { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public Guid InstituteId { get; set; }
    public required Institutes Institute { get; set; }
}