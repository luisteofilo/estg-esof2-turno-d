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

    public async Task<JobSkillDto> CreateJobSkill(Guid JobId, Guid SkillId, bool isRequired, JobSkillDto jobSkillDto)
    {
        jobSkillDto.JobId = JobId;
        jobSkillDto.SkillId = SkillId;
        jobSkillDto.IsRequired = isRequired;

        var response = await _httpClient.PostAsJsonAsync($"api/Job/CreateJobSkill", jobSkillDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<JobSkillDto>();
    }

    public async Task<IEnumerable<JobDto>> GetJobs()
    {
        var response = await _httpClient.GetAsync("api/Job");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<JobDto>>();
    }
}