using credit.identity.Dtos;
using credit.identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace credit.identity.Controllers;

[ApiController]
[Route("api/v1/admin")]
public class InstituteController(InstituteService service) : ControllerBase
{
    
    [HttpPost]
    [Route("institute")]
    public async Task<InstituteResponse> CreateInstitute(CreateInstituteRequest instituteRequest, CancellationToken cancellationToken)
    {
        return await service.CreateInstitute(instituteRequest, cancellationToken);
    }
    
}