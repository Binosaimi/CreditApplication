using credit.loans.Dtos;
using credit.loans.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace credit.loans.Controllers;

[ApiController]
[Route("api/v1/loans")]
public class LoansController(LoansService service) : ControllerBase
{
    
    [Authorize(Roles = "Reader")]
    [HttpGet("{customerId:guid}")]
    public async Task<List<LoansResponse>> Get(Guid customerId, CancellationToken cancellationToken)
    {
        return await service.GetLoansByCustomerAsync(customerId, cancellationToken);
    }

    [Authorize(Roles = "Writer")]
    [HttpPost]
    public async Task<LoansResponse> Create(CreateLoanRequest request, CancellationToken cancellationToken)
    {
        var loan = await service.CreateLoanAsync(request, cancellationToken);

        return loan;
    }
}