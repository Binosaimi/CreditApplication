namespace credit.identity.Dtos;

public record SignupRequest(
    string Username,
    string Password,
    Guid InstituteId);