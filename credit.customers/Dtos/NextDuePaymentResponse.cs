namespace credit.loans.Dtos;

public record NextDuePaymentResponse(
    Guid LoanId,
    DateTime DueDate);