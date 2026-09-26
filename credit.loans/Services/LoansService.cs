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
        try
        {
            return await db.Loans
                .AsNoTracking()
                .Where(loan => loan.CustomerId == customerId)
                .Select(loan => new LoansResponse(
                    loan.LoanId,
                    loan.CustomerId,
                    loan.InstitutionId,
                    loan.LoanStartDate,
                    loan.Tenor,
                    loan.Amount,
                    loan.Rate,
                    loan.Status
                )).FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<LoansResponse> GetLoansByInstitutionAsync(Guid institutionId, CancellationToken cancellationToken)
    {
        try
        {
            return await db.Loans
                .AsNoTracking()
                .Where(loan => loan.InstitutionId == institutionId)
                .Select(loan => new LoansResponse(
                    loan.LoanId,
                    loan.CustomerId,
                    loan.InstitutionId,
                    loan.LoanStartDate,
                    loan.Tenor,
                    loan.Amount,
                    loan.Rate,
                    loan.Status
                )).FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<LoansResponse> CreateLoanAsync(CreateLoanRequest createLoanRequest, CancellationToken cancellationToken)
    {
        var loan = new Loans
        {
            LoanId = Guid.NewGuid(),
            CustomerId = createLoanRequest.CustomerId,
            InstitutionId = createLoanRequest.InstitutionId,
            LoanStartDate = createLoanRequest.LoanStartDate,
            Tenor = createLoanRequest.Tenor,
            Amount = createLoanRequest.Amount,
            Rate = createLoanRequest.Rate,
            Status = "Active"
        };

        db.Loans.Add(loan);
        await db.SaveChangesAsync(cancellationToken);
        return new LoansResponse(
            loan.LoanId, loan.CustomerId, loan.InstitutionId, loan.LoanStartDate, loan.Tenor, loan.Amount, loan.Rate, loan.Status);
    }
    
}