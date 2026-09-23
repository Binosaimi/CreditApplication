namespace credit.loans.Dtos;

public record DelinquenciesResponse(
    Guid DelinquencyId,
    Guid LoanId, 
    DateTime DelinquencyDate);