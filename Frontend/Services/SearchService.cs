
using Common.Dtos.Job;
using Common.Dtos.Profile;

using Frontend.Services.Contracts;

namespace Frontend.Services;

public class SearchService(HttpClient httpClient) : ISearchService
{   
    public bool searchPerformed { get; set; } = false;
    
    public async Task<IEnumerable<ProfileDto>> GetResults(string firstName, string skill, string location)
    {
        try
        {
            var response = await httpClient.GetAsync($"api/Search/profile?firstName={firstName}&skill={skill}&location={location}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ProfileDto>>();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);  
            return Enumerable.Empty<ProfileDto>();
        }
         
    }
    
    public async Task<IEnumerable<string>> GetLocations()
    {
        var response = await httpClient.GetAsync($"api/Search/locations");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
    }
    
  /*  public async Task<IEnumerable<ProfileDto>> GetResultsBySkill(string skill)
    {
        var response = await httpClient.GetAsync($"api/Search/skills/{skill}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ProfileDto>>();
    }
    
    public async Task<IEnumerable<ProfileDto>> GetResultsBySkill_Name(string skill, string firstName)
    {
        var response = await httpClient.GetAsync($"api/Search/{firstName}/skills/{skill}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ProfileDto>>();
    }
    
    public async Task<IEnumerable<ProfileDto>> GetResultsByLocation_Name(string location, string firstName)
    {
        var response = await httpClient.GetAsync($"api/Search/{firstName}/locations/{location}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ProfileDto>>();
    }
    
    public async Task<IEnumerable<ProfileDto>> GetResultsByLocation_Skill(string location, string skill)
    {
        var response = await httpClient.GetAsync($"api/Search/skills/{skill}/locations/{location}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ProfileDto>>();
    }
    
    public async Task<IEnumerable<ProfileDto>> GetResultsBySkill_Name_Location(string skill, string firstName, string location)
    {
        var response = await httpClient.GetAsync($"api/Search/{firstName}/skills/{skill}/locations/{location}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ProfileDto>>();
    }
    
    public async Task<IEnumerable<ProfileDto>> GetResultByLocation(string location)
    {
        var response = await httpClient.GetAsync($"api/Search/locations/{location}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ProfileDto>>();
    } 
    
    public async Task<IEnumerable<string>> GetLocations()
    {
        var response = await httpClient.GetAsync($"api/Search/locations");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
    }
    
    public async Task<IEnumerable<JobDto>> GetJobs()
    {
        var response = await httpClient.GetAsync($"api/Job");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<JobDto>>();
    }

    public async Task<IEnumerable<JobDto>> GetJobsBySkill(string skill)
    {
        var response = await httpClient.GetAsync($"api/Search/jobs/skills/{skill}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<JobDto>>();
    }
    
    public async Task<IEnumerable<JobDto>> GetJobsByLocation(string location)
    {
        var response = await httpClient.GetAsync($"api/Search/jobs/locations/{location}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<JobDto>>();
    }
    
    public async Task<IEnumerable<JobDto>> GetJobsByPosition(string position)
    {
        var response = await httpClient.GetAsync($"api/Search/jobs/positions/{position}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<JobDto>>();
    }*/
   
}