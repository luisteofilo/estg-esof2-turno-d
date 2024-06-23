
using Common.Dtos.Profile;

using Frontend.Services.Contracts;

namespace Frontend.Services;

public class SearchService(HttpClient httpClient) : ISearchService
{   
    public string firstName { get; set; }
    
    public async Task<IEnumerable<ProfileDto>> GetResults(string firstName)
    {
            var response = await httpClient.GetAsync($"api/Search/{firstName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ProfileDto>>();
            
    }
    
    public async Task<IEnumerable<ProfileDto>> GetResultsBySkill(string skill)
    {
        var response = await httpClient.GetAsync($"api/Search/skills/{skill}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ProfileDto>>();
    }

    public void SetName(string name)
    {
        firstName = name;
    }
}