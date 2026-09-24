namespace credit.customers.Dtos;

public record CustomerResponse(
    Guid CustomerId,
    string CivilId,
    string Name,
    DateOnly Dob,
    bool Eligible);