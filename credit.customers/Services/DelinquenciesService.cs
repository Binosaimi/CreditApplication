using credit.customers.Clients;
using credit.customers.Data;
using credit.customers.Dtos;

namespace credit.customers.Services;

public class DelinquenciesService(CustomerDbContext db, LoanClient client)
{
    public async Task<GetCustomerDelinquenciesResponse> GetCustomerDelinquentLoansAsync(Guid customerId, CancellationToken cancellationToken)
    {
        return new GetCustomerDelinquenciesResponse(customerId, await client.GetDelinquencies(customerId, cancellationToken));
    }
}