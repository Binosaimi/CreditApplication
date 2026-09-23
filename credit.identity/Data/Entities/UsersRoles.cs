namespace credit.identity.Data.Entities;

public class UsersRoles
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    
    public required Users User { get; set; }
    public required Roles Role { get; set; }
}