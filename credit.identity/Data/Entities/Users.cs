namespace credit.identity.Data.Entities;

public class Users
{
    public Guid Id { get; set; }
    public Guid Username { get; set; }
    public Guid InstituteId { get; set; }
    public required Institutes Institute { get; set; }
}