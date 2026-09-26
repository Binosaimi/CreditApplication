using credit.customers.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace credit.customers.Data;

public static class CustomerDbSeeder
{
    public static async Task SeedAsync(CustomerDbContext db)
    {
        if (await db.Customers.AnyAsync())
            return;

        var institutionId =
            Guid.Parse("10000000-0000-0000-0000-000000000001");

        var customerAId =
            Guid.Parse("20000000-0000-0000-0000-000000000001");

        var customerBId =
            Guid.Parse("20000000-0000-0000-0000-000000000002");

        var customerCId =
            Guid.Parse("20000000-0000-0000-0000-000000000003");

        var customerFId =
            Guid.Parse("20000000-0000-0000-0000-000000000004");

        var loanFId =
            Guid.Parse("30000000-0000-0000-0000-000000000004");

        db.Customers.AddRange(
            new Customers
            {
                CustomerId = customerAId,
                CivilId = "111111111111",
                Name = "Customer A",
                Dob = new DateOnly(1990, 1, 1),
                IsEligible = true
            },
            new Customers
            {
                CustomerId = customerBId,
                CivilId = "222222222222",
                Name = "Customer B",
                Dob = new DateOnly(1991, 2, 2),
                IsEligible = true
            },
            new Customers
            {
                CustomerId = customerCId,
                CivilId = "333333333333",
                Name = "Customer C",
                Dob = new DateOnly(1992, 3, 3),
                IsEligible = true
            },
            new Customers
            {
                CustomerId = customerFId,
                CivilId = "444444444444",
                Name = "Customer F",
                Dob = new DateOnly(1993, 4, 4),
                IsEligible = true
            }
        );

        db.Litigation.Add(
            new Litigation
            {
                LitigationId =
                    Guid.Parse("50000000-0000-0000-0000-000000000001"),

                CourtId =
                    Guid.Parse("60000000-0000-0000-0000-000000000001"),

                LoanId = loanFId,
                InstitutionId = institutionId,
                CustomerId = customerFId,
                Status = "Guilty",
                DateOfVerdict = DateTime.UtcNow.AddYears(-1)
            }
        );

        await db.SaveChangesAsync();
    }
}