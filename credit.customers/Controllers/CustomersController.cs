using credit.customers.Dtos;
using credit.customers.Services;
using Microsoft.AspNetCore.Mvc;

namespace credit.customers.Controllers;

[ApiController]
[Route("api/v1/customers")]
public class CustomersController(CustomerService service) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerResponse>> Get(Guid id, CancellationToken cancellationToken)
    {
        var customer = await service.GetAsync(id, cancellationToken);

        if (customer is null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerResponse>> Create(
        CreateCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var customer = await service.CreateAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(Get),
            new { id = customer.Id },
            customer);
    }
    
    [HttpGet("{id:guid}/delinquencies")]
    public async Task<GetCustomerDelinquenciesResponse> GetCustomerDelinquentLoans(Guid customerId, CancellationToken cancellationToken)
    {
        var loans = await service.GetCustomerDelinquentLoansAsync(customerId, cancellationToken);

        return loans;
    }
}