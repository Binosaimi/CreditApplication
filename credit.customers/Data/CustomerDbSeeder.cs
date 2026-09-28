using credit.customers.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace credit.customers.Data;

public static class CustomerDbSeeder
{
    public static async Task SeedAsync(CustomerDbContext db)
    {
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

        var customerLitigationTestId =
            Guid.Parse("20000000-0000-0000-0000-000000000005");

        var loanFId =
            Guid.Parse("30000000-0000-0000-0000-000000000004");

        var innocentLoanId =
            Guid.Parse("30000000-0000-0000-0000-000000000005");

        if (!await db.Customers.AnyAsync())
        {
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
                },
                new Customers
                {
                    CustomerId = customerLitigationTestId,
                    CivilId = "555555555555",
                    Name = "Customer Innocent",
                    Dob = new DateOnly(1994, 5, 5),
                    IsEligible = true
                });

            await db.SaveChangesAsync();
        }

        if (!await db.Litigation.AnyAsync())
        {
            db.Litigation.AddRange(
                // F:
                // Loan > 10k and guilty verdict within 3 years.
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
                },

                // Used to prove Innocent litigation does NOT trigger F.
                new Litigation
                {
                    LitigationId =
                        Guid.Parse("50000000-0000-0000-0000-000000000002"),

                    CourtId =
                        Guid.Parse("60000000-0000-0000-0000-000000000002"),

                    LoanId = innocentLoanId,
                    InstitutionId = institutionId,
                    CustomerId = customerLitigationTestId,
                    Status = "Innocent",
                    DateOfVerdict = DateTime.UtcNow.AddMonths(-6)
                });

            await db.SaveChangesAsync();
        }
    }
}