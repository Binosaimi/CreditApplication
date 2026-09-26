using credit.customers.Clients;

namespace credit.customers.Dtos;

public record CustomerTotalLoansResponse(double TotalLoansAmount, List<LoanClient.LoansResponse> Loans);