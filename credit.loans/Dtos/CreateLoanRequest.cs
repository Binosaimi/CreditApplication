using System.ComponentModel.DataAnnotations;

namespace credit.loans.Dtos;

public record CreateLoanRequest(
    [Required] Guid CustomerId,
    [Required] Guid InstitutionId,
    [Required] DateTime LoanStartDate,
    [Range(0, int.MaxValue, MinimumIsExclusive = true, ErrorMessage = "Tenor must be greater than 0")] int Tenor,
    [Range(0, double.MaxValue, MinimumIsExclusive = true, ErrorMessage = "Amount must be greater than 0")] double Amount,
    [Range(0, double.MaxValue, MinimumIsExclusive = true, ErrorMessage = "Rate must be greater than 0")] double Rate
);