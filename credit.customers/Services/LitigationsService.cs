using credit.customers.Data;
using credit.customers.Data.Entities;
using credit.customers.Dtos;
using Microsoft.EntityFrameworkCore;

namespace credit.customers.Services;

public class LitigationsService(CustomerDbContext db)
{
    public async Task<List<LitigationResponse>> GetLitigations(Guid customerId, CancellationToken cancellationToken)
    {
        return await db.Litigation
            .AsNoTracking()
            .Where(litigation => litigation.CustomerId == customerId)
            .Select(litigation => new LitigationResponse(
                litigation.LitigationId,
                litigation.CourtId,
                litigation.LoanId,
                litigation.InstitutionId,
                litigation.CustomerId,
                litigation.Status,
                litigation.DateOfVerdict))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<LitigationResponse>> GetLitigationsByLoanIds(Guid[] loanIds, CancellationToken cancellationToken)
    {
        return await db.Litigation
            .AsNoTracking()
            .Where(l => Enumerable.Contains(loanIds, l.LoanId))
            .Select(litigation => new LitigationResponse(
                litigation.LitigationId,
                litigation.CourtId,
                litigation.LoanId,
                litigation.InstitutionId,
                litigation.CustomerId,
                litigation.Status,
                litigation.DateOfVerdict))
            .ToListAsync(cancellationToken);
    }
    public async Task<LitigationResponse> CreateLitigation(CreateLitigationRequest createLitigationRequest)
    {
        var litigation = new Litigation
        {
            LitigationId = createLitigationRequest.LitigationId,
            CourtId = createLitigationRequest.CourtId,
            LoanId = createLitigationRequest.LoanId,
            InstitutionId = createLitigationRequest.InstitutionId,
            CustomerId = createLitigationRequest.CustomerId,
            Status = createLitigationRequest.Status,
            DateOfVerdict = createLitigationRequest.DateOfVerdict
        };
        
        db.Litigation.Add(litigation);
        await db.SaveChangesAsync();
        
        return new LitigationResponse(
            litigation.LitigationId,
            litigation.CourtId,
            litigation.LoanId,
            litigation.InstitutionId,
            litigation.CustomerId,
            litigation.Status,
            litigation.DateOfVerdict);
    }
}