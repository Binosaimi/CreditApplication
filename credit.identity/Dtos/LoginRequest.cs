namespace credit.identity.Dtos;

public record LoginRequest(
    string Username,
    string Password);