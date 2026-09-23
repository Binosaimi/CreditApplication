using credit.loans.Data;
using credit.loans.Dtos;
using Microsoft.EntityFrameworkCore;

namespace credit.loans.Services;

public class DelinquenciesService(LoanDbContext db)
{
    public async Task<List<DelinquenciesResponse>> GetDelinquenciesByCustomerAsync(Guid customerId, CancellationToken cancellationToken)
    {
        return await db.Delinquencies
            .AsNoTracking()
            .Where(d => d.Loans.CustomerId == customerId)
            .Select(delinquencies => new DelinquenciesResponse(
                delinquencies.Id,
                delinquencies.LoanId,
                delinquencies.DelinquencyDate))
            .ToListAsync(cancellationToken);
    }
}