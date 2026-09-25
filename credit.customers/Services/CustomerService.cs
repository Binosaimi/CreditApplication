using credit.customers.Clients;
using credit.customers.Data;
using credit.customers.Data.Entities;
using credit.customers.Dtos;
using Microsoft.EntityFrameworkCore;

namespace credit.customers.Services;

public class CustomerService(CustomerDbContext db, LoanClient loanClient, LitigationsService litigationsService)
{
    public async Task<CustomerResponse> GetAsync(Guid customerId, CancellationToken cancellationToken)
    {
        return await db.Customers
            .AsNoTracking()
            .Where(customer => customer.CustomerId == customerId)
            .Select(customer => new CustomerResponse(
                customer.CustomerId,
                customer.CivilId,
                customer.Name,
                customer.Dob,
                customer.IsEligible))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<CustomerResponse> CreateAsync(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customers
        {
            CustomerId = Guid.NewGuid(),
            CivilId = request.CivilId,
            Dob = request.Dob,
            Name = request.Name,
            IsEligible = true
        };

        db.Customers.Add(customer);
        await db.SaveChangesAsync(cancellationToken);
        return new CustomerResponse(
            customer.CustomerId, customer.CivilId, customer.Name, customer.Dob, customer.IsEligible);
    }

    public async Task<CustomerResponse> BlockCustomerAsync(BlockCustomerRequest blockCustomerRequest, CancellationToken cancellationToken)
    {
        var customer = await db.Customers
            .SingleOrDefaultAsync(
                c => c.CustomerId == blockCustomerRequest.CustomerId,
                cancellationToken);
        if (customer == null)
        {
            throw new ArgumentException("Customer not found");
        }

        customer.IsEligible = !blockCustomerRequest.SetUserBlocked;
        await db.SaveChangesAsync(cancellationToken);
        return new CustomerResponse(
            customer.CustomerId, customer.CivilId, customer.Name, customer.Dob, customer.IsEligible);
    }

    public async Task<List<CustomerResponse>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await db.Customers
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(c => new CustomerResponse(
                c.CustomerId,
                c.CivilId,
                c.Name,
                c.Dob,
                c.IsEligible
            ))
            .ToListAsync(cancellationToken);
    }

    /*
     * Credit score calculation:
     * A: 1 or More active loans with no delinquencies
     * B: 0 active loans, with no delinquencies
     * C: Between 1-3 delinquencies, no loans in litigation
     * F: More than 3 delinquencies, or 1 or more loans with guilty litigation less than 3 years old if amount of loan > 10000,
     *      or guilty litigation less than 1 years old if amount < 10000
     *
     * Big decisions here:
     * Credit rating C says no loans in litigation. Technically, no loans in litigation means no loans in Pending state. I included Guilty
     * states that did not satisfy condition for rating F. if i didn't do that, it would be the same rating as A if both have no delinquencies.
     * This makes no sense to compare perfect record with guilty verdicts
     * 
     */
    public async Task<CustomerCreditScoreResponse> CalculateCreditScore(Guid customerId, CancellationToken cancellationToken)
    {
        var loans = await loanClient.GetLoansByCustomerIdAsync(customerId, cancellationToken);
        var activeLoans = loans.Where(l => l.Status == "Active").ToList();
        var delinquencies = await loanClient.GetDelinquencies(customerId, cancellationToken);
        var litigations = await litigationsService.GetLitigations(customerId, cancellationToken);
        var civilId = (await GetAsync(customerId, cancellationToken)).CivilId;


        var isFCreditScore = delinquencies.Count > 3 ||
                             litigations.Any(l =>
                             {
                                 if (l.Status != "Guilty")
                                     return false;

                                 var loan = loans.FirstOrDefault(x => x.LoanId == l.LoanId);

                                 if (loan == null)
                                     return false;

                                 return loan.Amount >= 10_000 ? 
                                     l.DateOfVerdict > DateTime.UtcNow.AddYears(-3) : 
                                     l.DateOfVerdict > DateTime.UtcNow.AddYears(-1);
                             });

        var creditScore = (isFCreditScore, delinquencies.Count, activeLoans.Count) switch
        {
            (true, _, _) => 'F',

            (_, >= 1 and <= 3, _)
                when litigations.Any(l => l.Status is "Pending" or "Guilty")
                => 'C',

            (_, 0, 0) => 'B',

            (_, 0, > 0) => 'A',

            _ => throw new InvalidOperationException(
                "Invalid credit score calculation")
        };

        return new CustomerCreditScoreResponse(customerId, civilId, creditScore);
    }
}