namespace credit.identity.Dtos;

public record AssignRoleRequest(
    string Username,
    string RoleName
);