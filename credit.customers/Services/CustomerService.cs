using credit.customers.Clients;
using credit.customers.Data;
using credit.customers.Data.Entities;
using credit.customers.Dtos;
using Microsoft.EntityFrameworkCore;

namespace credit.customers.Services;
/*
 * expected functionalities:
 * 1. get customer info
 * 2. get customer delinquent loans
 * 3. get customer's total loans
 * 4. get customer by institute id
 * 5. add block on customer
 */
public class CustomerService(CustomerDbContext db, LoanClient client)
{
    public async Task<List<CustomerResponse>> GetAsync(Guid customerId, CancellationToken cancellationToken)
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
            .ToListAsync(cancellationToken);
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
    
    public async Task<GetCustomerDelinquenciesResponse> GetCustomerDelinquentLoansAsync(Guid customerId, CancellationToken cancellationToken)
    {
        return new GetCustomerDelinquenciesResponse(customerId, await client.GetDelinquencies(customerId, cancellationToken));
    }
}