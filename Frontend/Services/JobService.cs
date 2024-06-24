using Common.Dtos.Job;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class JobService(HttpClient _httpClient) : IJobService
{
    public async Task<JobDto> CreateJob(Guid ClientId, JobDto jobDto)
    {
        jobDto.ClientId = ClientId;
        
        var response = await _httpClient.PostAsJsonAsync("api/Job", jobDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<JobDto>();
    }
    
    public async Task<ClientDto> GetClient(Guid ClientId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Job/Client/{ClientId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ClientDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}