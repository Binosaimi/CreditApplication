using credit.identity.Data;
using credit.identity.Data.Entities;
using credit.identity.Dtos;

namespace credit.identity.Services;

public class InstituteService(IdentityDbContext db)
{
    public async Task<InstituteResponse> CreateInstitute(CreateInstituteRequest instituteRequest, CancellationToken cancellationToken)
    {
        var institute = new Institutes()
        {
            InstituteId = Guid.NewGuid(),
            InstituteName = instituteRequest.InstituteName
        };
        try
        {
            db.Institutes.Add(institute);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
            // throw CannotSignUpException;
        }

        await db.SaveChangesAsync(cancellationToken);

        return new InstituteResponse(
            institute.InstituteId,
            institute.InstituteName);
    }
}