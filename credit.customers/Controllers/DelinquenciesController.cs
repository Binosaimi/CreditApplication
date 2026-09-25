using credit.customers.Dtos;
using credit.customers.Services;
using Microsoft.AspNetCore.Mvc;

namespace credit.customers.Controllers;

// TODO: move this to loans service
[ApiController]
[Route("api/v1/delinquencies")]
public class DelinquenciesController(DelinquenciesService service) : ControllerBase
{
    [HttpGet("{customerId:guid}/delinquencies")]
    public async Task<GetCustomerDelinquenciesResponse> GetCustomerDelinquentLoans(Guid customerId, CancellationToken cancellationToken)
    {
        var loans = await service.GetCustomerDelinquentLoansAsync(customerId, cancellationToken);

        return loans;
    }
}