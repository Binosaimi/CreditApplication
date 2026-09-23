namespace credit.customers.Dtos;

public record CustomerResponse(
    Guid Id,
    string CivilId,
    string Name,
    DateOnly Dob,
    bool Eligible);