using credit.loans.Data;
using credit.loans.Dtos;
using Microsoft.EntityFrameworkCore;

namespace credit.loans.Services;

public class PaymentsService(LoanDbContext db)
{
    public async Task<List<PaymentsResponse>> GetCustomerLastNPayments(Guid customerId, int numberOfPayments, CancellationToken cancellationToken)
    {
        try
        {
            return await db.PaymentLedger
                .AsNoTracking()
                .Where(l => l.CustomerId == customerId)
                .OrderByDescending(l => l.PaymentDate)
                .Take(numberOfPayments)
                .Select(l => new PaymentsResponse(
                    l.PaymentId,
                    l.PaymentDate,
                    l.LoanId,
                    l.CustomerId,
                    l.InstitutionId,
                    l.Amount
                ))
                .ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}