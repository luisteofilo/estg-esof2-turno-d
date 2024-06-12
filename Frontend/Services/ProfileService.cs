
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class ProfileService(HttpClient httpClient) : IProfileService
{
    
    public async Task<ProfileDto> GetProfile()
    {
        try
        {
            var response = await httpClient.GetAsync($"api/Profile");
                
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