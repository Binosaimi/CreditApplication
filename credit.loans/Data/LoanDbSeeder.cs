using credit.loans.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace credit.loans.Data;

public static class LoanDbSeeder
{
    public static async Task SeedAsync(LoanDbContext db)
    {
        if (await db.Loans.AnyAsync())
            return;

        var institutionId =
            Guid.Parse("10000000-0000-0000-0000-000000000001");

        var customerAId =
            Guid.Parse("20000000-0000-0000-0000-000000000001");

        var customerCId =
            Guid.Parse("20000000-0000-0000-0000-000000000003");

        var customerFId =
            Guid.Parse("20000000-0000-0000-0000-000000000004");

        var loanAId =
            Guid.Parse("30000000-0000-0000-0000-000000000001");

        var loanCId =
            Guid.Parse("30000000-0000-0000-0000-000000000003");

        var loanFId =
            Guid.Parse("30000000-0000-0000-0000-000000000004");

        db.Loans.AddRange(
            // Grade A: active loan, no delinquencies
            new Loans
            {
                LoanId = loanAId,
                CustomerId = customerAId,
                InstitutionId = institutionId,
                LoanStartDate = DateTime.UtcNow.AddYears(-1),
                Tenor = 60,
                Amount = 15_000,
                Rate = 5,
                Status = "Active"
            },

            // Grade C: 2 delinquencies
            new Loans
            {
                LoanId = loanCId,
                CustomerId = customerCId,
                InstitutionId = institutionId,
                LoanStartDate = DateTime.UtcNow.AddYears(-2),
                Tenor = 48,
                Amount = 8_000,
                Rate = 4.5,
                Status = "Active"
            },

            // Grade F: guilty litigation within 3 years, > 10k
            new Loans
            {
                LoanId = loanFId,
                CustomerId = customerFId,
                InstitutionId = institutionId,
                LoanStartDate = DateTime.UtcNow.AddYears(-2),
                Tenor = 60,
                Amount = 20_000,
                Rate = 6,
                Status = "Active"
            }
        );

        db.Delinquencies.AddRange(
            new Delinquencies
            {
                DelinquencyId =
                    Guid.Parse("40000000-0000-0000-0000-000000000001"),

                LoanId = loanCId,
                DelinquencyDate = DateTime.UtcNow.AddMonths(-4)
            },
            new Delinquencies
            {
                DelinquencyId =
                    Guid.Parse("40000000-0000-0000-0000-000000000002"),

                LoanId = loanCId,
                DelinquencyDate = DateTime.UtcNow.AddMonths(-2)
            }
        );

        // Six payments so "last 5 payments" can be tested.
        db.PaymentLedger.AddRange(
            CreatePayment(1, loanAId, customerAId, institutionId, -6),
            CreatePayment(2, loanAId, customerAId, institutionId, -5),
            CreatePayment(3, loanAId, customerAId, institutionId, -4),
            CreatePayment(4, loanAId, customerAId, institutionId, -3),
            CreatePayment(5, loanAId, customerAId, institutionId, -2),
            CreatePayment(6, loanAId, customerAId, institutionId, -1)
        );

        await db.SaveChangesAsync();
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
            PaymentId = Guid.Parse(
                $"80000000-0000-0000-0000-{number:D12}"),

            PaymentDate = DateTime.UtcNow.AddMonths(monthsAgo),
            LoanId = loanId,
            CustomerId = customerId,
            InstitutionId = institutionId,
            Amount = 250
        };
    }
}