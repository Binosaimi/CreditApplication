namespace credit.loans.Dtos;

public record LoansResponse(
    Guid LoanId,
    Guid CustomerId,
    Guid InstitutionId,
    DateTime LoanStartDate,
    int Tenor,
    double Amount,
    double Rate,
    string Status
    );