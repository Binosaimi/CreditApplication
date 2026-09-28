using credit.loans.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace credit.loans.Data;

public static class LoanDbSeeder
{
    public static async Task SeedAsync(LoanDbContext db)
    {
        var institutionId =
            Guid.Parse("10000000-0000-0000-0000-000000000001");

        var customerAId =
            Guid.Parse("20000000-0000-0000-0000-000000000001");

        var customerCId =
            Guid.Parse("20000000-0000-0000-0000-000000000003");

        var customerFId =
            Guid.Parse("20000000-0000-0000-0000-000000000004");

        var customerInnocentId =
            Guid.Parse("20000000-0000-0000-0000-000000000005");

        var loanAId =
            Guid.Parse("30000000-0000-0000-0000-000000000001");

        var loanCId =
            Guid.Parse("30000000-0000-0000-0000-000000000003");

        var loanFId =
            Guid.Parse("30000000-0000-0000-0000-000000000004");

        var innocentLoanId =
            Guid.Parse("30000000-0000-0000-0000-000000000005");

        if (!await db.Loans.AnyAsync())
        {
            db.Loans.AddRange(
                // Expected A
                new Loans
                {
                    LoanId = loanAId,
                    CustomerId = customerAId,
                    InstitutionId = institutionId,
                    LoanStartDate = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                    Tenor = 60,
                    Amount = 15_000,
                    Rate = 5,
                    Status = "Active"
                },

                // Expected C: 2 delinquencies
                new Loans
                {
                    LoanId = loanCId,
                    CustomerId = customerCId,
                    InstitutionId = institutionId,
                    LoanStartDate = new DateTime(2024, 1, 10, 0, 0, 0, DateTimeKind.Utc),
                    Tenor = 48,
                    Amount = 8_000,
                    Rate = 4.5,
                    Status = "Active"
                },

                // Expected F:
                // > 10k + recent guilty litigation
                new Loans
                {
                    LoanId = loanFId,
                    CustomerId = customerFId,
                    InstitutionId = institutionId,
                    LoanStartDate = new DateTime(2024, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                    Tenor = 60,
                    Amount = 20_000,
                    Rate = 6,
                    Status = "Active"
                },

                // Active + innocent litigation.
                // Useful for total-loans/legal-state test.
                new Loans
                {
                    LoanId = innocentLoanId,
                    CustomerId = customerInnocentId,
                    InstitutionId = institutionId,
                    LoanStartDate = new DateTime(2025, 1, 25, 0, 0, 0, DateTimeKind.Utc),
                    Tenor = 36,
                    Amount = 9_000,
                    Rate = 4,
                    Status = "Active"
                });

            await db.SaveChangesAsync();
        }

        if (!await db.Delinquencies.AnyAsync())
        {
            db.Delinquencies.AddRange(
                // Customer C: exactly 2
                CreateDelinquency(1, loanCId, -4),
                CreateDelinquency(2, loanCId, -2),

                // Customer F: 4 delinquencies.
                // This independently triggers F.
                CreateDelinquency(3, loanFId, -8),
                CreateDelinquency(4, loanFId, -6),
                CreateDelinquency(5, loanFId, -4),
                CreateDelinquency(6, loanFId, -2));

            await db.SaveChangesAsync();
        }

        if (!await db.PaymentLedger.AnyAsync())
        {
            // 7 rows intentionally.
            // Last-5 must exclude payments 1 and 2.
            db.PaymentLedger.AddRange(
                CreatePayment(1, loanAId, customerAId, institutionId, -7),
                CreatePayment(2, loanAId, customerAId, institutionId, -6),
                CreatePayment(3, loanAId, customerAId, institutionId, -5),
                CreatePayment(4, loanAId, customerAId, institutionId, -4),
                CreatePayment(5, loanAId, customerAId, institutionId, -3),
                CreatePayment(6, loanAId, customerAId, institutionId, -2),
                CreatePayment(7, loanAId, customerAId, institutionId, -1));

            await db.SaveChangesAsync();
        }
    }

    private static Delinquencies CreateDelinquency(
        int number,
        Guid loanId,
        int monthsAgo)
    {
        return new Delinquencies
        {
            DelinquencyId = Guid.Parse(
                $"40000000-0000-0000-0000-{number:D12}"),

            LoanId = loanId,
            DelinquencyDate = DateTime.UtcNow.AddMonths(monthsAgo)
        };
    }

    private static PaymentLedger CreatePayment(
        int number,
        Guid loanId,
        Guid customerId,
        Guid institutionId,
        int monthsAgo)
    {
        return new PaymentLedger
        {
            PaymentId = Guid.Parse($"80000000-0000-0000-0000-{number:D12}"),

            PaymentDate = DateTime.UtcNow.AddMonths(monthsAgo),
            LoanId = loanId,
            CustomerId = customerId,
            InstitutionId = institutionId,
            Amount = 250
        };
    }
}