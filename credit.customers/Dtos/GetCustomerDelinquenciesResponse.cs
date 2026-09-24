using credit.customers.Clients;

namespace credit.customers.Dtos;

public record GetCustomerDelinquenciesResponse(Guid CustomerId, List<LoanClient.DelinquenciesResponse> Delinquencies);