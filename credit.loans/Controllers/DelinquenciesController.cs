using credit.loans.Dtos;
using credit.loans.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace credit.loans.Controllers;

[ApiController]
[Route("api/v1/delinquencies")]
public class DelinquenciesController(DelinquenciesService service) : ControllerBase
{
    
    [Authorize(Roles = "Reader")]
    [HttpGet("{customerId:guid}")]
    public async Task<List<DelinquenciesResponse>> GetDelinquenciesByCustomerAsync(Guid customerId, CancellationToken cancellationToken)
    {
        return await service.GetDelinquenciesByCustomerAsync(customerId, cancellationToken);
    }
    
}