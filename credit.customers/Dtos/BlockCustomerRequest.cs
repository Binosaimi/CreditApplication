namespace credit.customers.Dtos;

public record BlockCustomerRequest(Guid CustomerId, bool SetUserBlocked);