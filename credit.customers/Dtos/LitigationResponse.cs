namespace credit.customers.Dtos;

public record LitigationResponse(
    Guid LitigationId,
    Guid CourtId,
    Guid LoanId,
    Guid InstitutionId,
    Guid CustomerId,
    string? Status,
    DateTime DateOfVerdict);