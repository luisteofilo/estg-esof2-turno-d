using Common.Dtos.Profile;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class SearchService(HttpClient httpClient) : ISearchService
{   
    
    public async Task<ProfileDto> GetResults()
    {
        try
        {
            var response = await httpClient.GetAsync("api/Search");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return new ProfileDto();
                }

                return await response.Content.ReadFromJsonAsync<ProfileDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while fetching profile: {ex.Message}", ex);
        }      
    }
}