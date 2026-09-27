using credit.customers.Dtos;
using credit.customers.Services;
using credit.loans.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace credit.customers.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/customers")]
public class CustomersController(CustomerService service) : ControllerBase
{
    [Authorize(Roles = "Reader")]
    [HttpGet("{customerId:guid}")]
    public async Task<ActionResult<CustomerResponse>> Get(Guid customerId, CancellationToken cancellationToken)
    {
        var customer = await service.GetCustomerByIdAsync(customerId, cancellationToken);

        if (customer is null)
            return NotFound();

        return Ok(customer);
    }

    [Authorize(Roles = "Writer")]
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

    [Authorize(Roles = "Reader")]
    [HttpGet]
    public async Task<ActionResult<List<CustomerResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var customers = await service.GetAllAsync(cancellationToken);

        return Ok(customers);
    }

    [Authorize(Roles = "Writer")]
    [HttpPost("customer-block")]
    public async Task<ActionResult<CustomerResponse>> BlockCustomer(
        BlockCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var customer = await service.BlockCustomerAsync(request, cancellationToken);

        return Ok(customer);
    }

    [Authorize(Roles = "Reader")]
    [HttpGet("customerId:guid/creditscore")]
    public async Task<ActionResult<CustomerCreditScoreResponse>> GetCreditScore(Guid customerId, CancellationToken cancellationToken)
    {
        return await service.CalculateCreditScore(customerId, cancellationToken);
    }

    [Authorize(Roles = "Reader")]
    [HttpGet("customerId:guid/nextduepayment")]
    public async Task<ActionResult<NextDuePaymentResponse>> GetNextDuePayment(Guid customerId, CancellationToken cancellationToken)
    {
        return await service.GetNextDuePayment(customerId, cancellationToken);
    }

    [Authorize(Roles = "Reader")]
    [HttpGet("customerId:guid/totalloans")]
    public async Task<ActionResult<CustomerTotalLoansResponse>> GetCustomerTotalLoans(Guid customerId, CancellationToken cancellationToken)
    {
        return await service.GetCustomerTotalLoans(customerId, cancellationToken);
    }
}