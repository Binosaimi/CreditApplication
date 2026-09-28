namespace credit.customers.Clients;

public class LoanClient(HttpClient client, IHttpContextAccessor httpContextAccessor)
{
    private void ForwardAuthorizationHeader()
    {
        var authorization =
            httpContextAccessor.HttpContext?
                .Request.Headers.Authorization
                .ToString();

        if (!string.IsNullOrWhiteSpace(authorization))
        {
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.TryAddWithoutValidation(
                "Authorization",
                authorization);
        }
    }
    public async Task<List<DelinquenciesResponse>> GetDelinquencies(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        ForwardAuthorizationHeader();
        var response = await client.GetAsync($"/api/v1/delinquencies/{customerId}", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<DelinquenciesResponse>>(cancellationToken) ?? [];
    }

    public async Task<List<LoansResponse>> GetLoansByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        ForwardAuthorizationHeader();
        var response = await client.GetAsync($"/api/v1/loans/{customerId}", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<LoansResponse>>(cancellationToken) ?? [];
    }

    public async Task<List<LoansResponse>> GetActiveLoansByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        ForwardAuthorizationHeader();
        var response = await client.GetAsync($"/api/v1/loans/{customerId}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var loans = await response.Content.ReadFromJsonAsync<List<LoansResponse>>(cancellationToken) ?? [];
        return [.. loans.Where(l => l.Status == "Active")];
    }

    public record DelinquenciesResponse(
        Guid DelinquencyId,
        Guid LoanId,
        DateTime DelinquencyDate);

    public record LoansResponse(
        Guid LoanId,
        Guid CustomerId,
        Guid InstitutionId,
        DateTime LoanStartDate,
        int Tenor,
        double Amount,
        double Rate,
        string Status
    );
}