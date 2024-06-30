using ESOF.WebApp.DBLayer.Entities;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    
    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<User>?> GetUsersByRoleAsync(Guid roleId)
    {
        var response = await _httpClient.GetAsync($"api/roles/{roleId}/users");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<User>>();
    }
}