using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
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

        public async Task<IEnumerable<string>> GetProfileListOfSkills()
        {
            var response = await _httpClient.GetAsync("api/Dashboard/Skills");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        }
    }
}