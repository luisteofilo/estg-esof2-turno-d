using ESOF.WebApp.DBLayer.Entities;
using Frontend.Services.Contracts;
using System.Net.Http.Json;
using ESOF.WebApp.DBLayer.Context;

namespace Frontend.Services
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _httpClient;

        public RoleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Role>>("api/roles");
        }

        public async Task<Role> GetRoleByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Role>($"api/roles/{id}");
        }
        
        public async Task<Role> GetRoleByNameAsync(string name)
        {
            return await _httpClient.GetFromJsonAsync<Role>($"api/roles/{name}");
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            var response = await _httpClient.PostAsJsonAsync("api/roles", role);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Role>();
        }

        public async Task UpdateRoleAsync(Guid id, Role role)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/roles/{id}", role);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/roles/{id}");
            response.EnsureSuccessStatusCode();
        }
        
        public async Task<List<Permission>?> GetRolePermissionsAsync(Guid id)
        { 
            try
            {
                var response = await _httpClient.GetAsync($"api/roles/{id}/permissions");

                if (response.IsSuccessStatusCode)
                {
                    var permissions = await response.Content.ReadFromJsonAsync<List<Permission>>();
                    return permissions;
                }
                else
                {
                    // Tratar o caso em que a requisição não foi bem sucedida (como 404 Not Found)
                    Console.WriteLine($"Failed to retrieve permissions for role {id}. Status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching role permissions: {ex.Message}");
                return null;
            }
        }
    }
}