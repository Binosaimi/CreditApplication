namespace credit.identity.Dtos;

public record SignupResponse(Guid UserId, Guid InstituteId, string username);