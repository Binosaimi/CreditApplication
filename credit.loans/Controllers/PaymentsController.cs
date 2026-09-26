using credit.loans.Dtos;
using credit.loans.Services;
using Microsoft.AspNetCore.Mvc;

namespace credit.loans.Controllers;

[ApiController]
[Route("api/v1/payments")]
public class PaymentsController(PaymentsService service)
{
    [HttpGet("{customerId:guid}/{numberOfPayments:int}")]
    public async Task<List<PaymentsResponse>> GetCustomerLastNPayments(Guid customerId, int numberOfPayments, CancellationToken cancellationToken)
    {
        if (numberOfPayments <= 0)
        {
            throw new ArgumentException("Number of transactions must be greater than 0");
        }

        return await service.GetCustomerLastNPayments(customerId, numberOfPayments, cancellationToken);
    }
    
    [HttpGet("loan/{loanId:guid}")]
    public async Task<List<PaymentsResponse>> GetLoansPayments(Guid loanId, CancellationToken cancellationToken)
    {
        return await service.GetLoansPayments(loanId, cancellationToken);
    }
}