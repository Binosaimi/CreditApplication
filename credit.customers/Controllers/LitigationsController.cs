using credit.customers.Dtos;
using credit.customers.Services;
using Microsoft.AspNetCore.Mvc;

namespace credit.customers.Controllers;

[ApiController]
[Route("api/v1/litigations")]
public class LitigationsController(LitigationsService service) : ControllerBase
{
    [HttpGet("{customerId:guid}")]
    public async Task<List<LitigationResponse>> GetLitigations(Guid customerId, CancellationToken cancellationToken)
    {
        return await service.GetLitigations(customerId,cancellationToken);
    }
}