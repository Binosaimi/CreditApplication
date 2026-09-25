namespace credit.loans.Dtos;

public record PaymentsResponse(Guid PaymentId,
    DateTime PaymentDate,
    Guid LoanId,
    Guid CustomerId,
    Guid InstitutionId,
    double Amount);