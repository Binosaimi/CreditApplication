namespace credit.customers.Dtos;

public record CustomerCreditScoreResponse(
    Guid CustomerId,
    string CivilId,
    char CreditScore);