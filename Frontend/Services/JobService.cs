using Common.Dtos.Job;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class JobService(HttpClient _httpClient) : IJobService
{
    public async Task<JobDto> CreateJob(Guid JobId, Guid ClientId, JobDto jobDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Job", jobDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<JobDto>();
    }
    
}