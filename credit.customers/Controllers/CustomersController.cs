using credit.customers.Dtos;
using credit.customers.Services;
using Microsoft.AspNetCore.Mvc;

namespace credit.customers.Controllers;

[ApiController]
[Route("api/v1/customers")]
public class CustomersController(CustomerService service) : ControllerBase
{
    [HttpGet("{customerId:guid}")]
    public async Task<ActionResult<CustomerResponse>> Get(Guid customerId, CancellationToken cancellationToken)
    {
        var customer = await service.GetAsync(customerId, cancellationToken);

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
            new { customerId = customer.CustomerId },
            customer);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<CustomerResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var customers = await service.GetAllAsync(cancellationToken);

        return Ok(customers);
    }
    
    [HttpPost("customer-block")]
    public async Task<ActionResult<CustomerResponse>> BlockCustomer(
        BlockCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var customer = await service.BlockCustomerAsync(request, cancellationToken);

        return Ok(customer);
    }
}