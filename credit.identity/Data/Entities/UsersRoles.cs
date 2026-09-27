namespace credit.identity.Data.Entities;

public class UsersRoles
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public Users User { get; set; } = null!;
    public Roles Role { get; set; } = null!;
}