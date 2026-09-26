using credit.customers.Clients;

namespace credit.customers.Dtos;

public record GetCustomerTotalLoansResponse(double TotalLoansAmount, List<LoanClient.LoansResponse> Loans);