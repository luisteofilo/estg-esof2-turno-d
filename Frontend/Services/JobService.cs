using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Common.Dtos.Job;
using Frontend.Services.Contracts;

namespace Frontend.Services
{
    public class JobService : IJobService
    {
        private readonly HttpClient _httpClient;

        public JobService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<JobDto> CreateJob(Guid ClientId, JobDto jobDto)
        {
            jobDto.ClientId = ClientId;

            var response = await _httpClient.PostAsJsonAsync("api/Job", jobDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JobDto>();
        }

        public async Task<IEnumerable<JobDto>> GetJobs()
        {
            var response = await _httpClient.GetAsync("api/Job"); // Adjust endpoint if necessary
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<JobDto>>();
        }

        public async Task<JobSkillDto> CreateJobSkill(Guid JobId, Guid SkillId, bool isRequired, JobSkillDto jobSkillDto)
        {
            jobSkillDto.JobId = JobId;
            jobSkillDto.SkillId = SkillId;
            jobSkillDto.IsRequired = isRequired;

            var response = await _httpClient.PostAsJsonAsync($"api/JobSkill", jobSkillDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JobSkillDto>();
        }
    }
}