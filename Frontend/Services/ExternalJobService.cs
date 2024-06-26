using Common.Contracts.Job;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class ExternalJobService(HttpClient _httpClient) : IExternalJobService
{
    public async Task<ExternalJobResponse> CreateExternalJob(string url)
    {
        var response = await _httpClient.PostAsJsonAsync("api/jobs/external", new ExternalJobRequest(url));
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ExternalJobResponse>();
    }
}