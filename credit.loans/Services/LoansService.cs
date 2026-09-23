using credit.loans.Data;
using credit.loans.Data.Entities;
using credit.loans.Dtos;
using Microsoft.EntityFrameworkCore;

namespace credit.loans.Services;

/*
 * expected functionalities from loans service:
 * 
 * 1. get loans by customer id
 * 2. get loans by institution id
 * 3. create loan
 * 4. get customers last 5 payments
 * 5. 
*/
public class LoansService(LoanDbContext db)
{
    public async Task<LoansResponse> GetLoansByCustomerAsync(Guid customerId, CancellationToken cancellationToken)
    {
        return await db.Loans
            .AsNoTracking()
            .Where(loan => loan.CustomerId == customerId)
            .Select(loan => new LoansResponse(
                loan.Id,
                loan.CustomerId,
                loan.InstitutionId,
                loan.LoanStartDate,
                loan.Tenor,
                loan.Amount,
                loan.Rate,
                loan.Status
            )).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<LoansResponse> GetLoansByInstitutionAsync(Guid institutionId, CancellationToken cancellationToken)
    {
        return await db.Loans
            .AsNoTracking()
            .Where(loan => loan.InstitutionId == institutionId)
            .Select(loan => new LoansResponse(
                loan.Id,
                loan.CustomerId,
                loan.InstitutionId,
                loan.LoanStartDate,
                loan.Tenor,
                loan.Amount,
                loan.Rate,
                loan.Status
            )).FirstOrDefaultAsync(cancellationToken);
    }
}