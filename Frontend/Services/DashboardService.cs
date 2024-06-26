using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Common.Dtos.Job;
using Common.Dtos.Profile;
using Frontend.Services.Contracts;

namespace Frontend.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProfileSkillDto>> GetProfileSkills()
        {
            var response = await _httpClient.GetAsync("api/Dashboard/ProfileSkills");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ProfileSkillDto>>();
        }

        public async Task<IEnumerable<DashboardJobDTo>> GetJobSkills()
        {
            var response = await _httpClient.GetAsync("api/Dashboard/JobSkills");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<DashboardJobDTo>>();
        }
        
        public async Task<IEnumerable<ExperienceDto>> GetExperiences()
        {
            var response = await _httpClient.GetAsync("api/Dashboard/Experiences");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ExperienceDto>>();
        }
    }
}