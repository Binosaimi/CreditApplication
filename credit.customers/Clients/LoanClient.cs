namespace credit.customers.Clients;

public class LoanClient(HttpClient client)
{
    public async Task<List<DelinquenciesResponse>> GetDelinquencies(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        var response = await client.GetAsync($"/api/v1/delinquencies/{customerId}", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<DelinquenciesResponse>>(cancellationToken) ?? [];
    }

    public record DelinquenciesResponse(
        Guid DelinquencyId,
        Guid LoanId,
        DateTime DelinquencyDate);
}