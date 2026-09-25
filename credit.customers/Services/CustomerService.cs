using credit.customers.Data;
using credit.customers.Data.Entities;
using credit.customers.Dtos;
using Microsoft.EntityFrameworkCore;

namespace credit.customers.Services;

public class CustomerService(CustomerDbContext db)
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
}