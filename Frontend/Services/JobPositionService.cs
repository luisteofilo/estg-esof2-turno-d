using Common.Dtos.Job;
using Common.Dtos.Jobs;
using Common.Dtos.Position;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class JobPositionService(HttpClient _httpClient) : IJobPositionService
{
    public async Task ConvertJobToPosition(Guid jobId, JobToPositionConvertDTO dto)
    {
        var request = new CreateJobToPositionRequest
        {
            JobId = jobId,
            JobToPositionConvert = dto
        };
        
        var response = await _httpClient.PostAsJsonAsync("api/Jobs", request);
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Converting Job To Position");
        }
    }
}

public class CreateJobToPositionRequest
{
    public Guid JobId { get; set; }
    public JobToPositionConvertDTO JobToPositionConvert { get; set; }
}