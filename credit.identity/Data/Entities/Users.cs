namespace credit.identity.Data.Entities;

public class Users
{
    public Guid UserId { get; set; }
    public Guid Username { get; set; }
    public Guid InstituteId { get; set; }
    public required Institutes Institute { get; set; }
}