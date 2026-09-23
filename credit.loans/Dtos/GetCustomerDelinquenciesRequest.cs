using System.ComponentModel.DataAnnotations;

namespace credit.loans.Dtos;

public record GetCustomerDelinquenciesRequest(
    [Required] Guid CustomerId
    );