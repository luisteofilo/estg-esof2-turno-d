using Common.Dtos.RoleAndPerms;
using ESOF.WebApp.DBLayer.Entities;
using Frontend.Services.Contracts;

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

        public async Task<Role?> CreateRoleAsync(Role role)
        {
            var roleDto = role.RoleConvertToDto();
            var response = await _httpClient.PostAsJsonAsync("api/roles", roleDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Role>();
        }

        public async Task UpdateRoleAsync(Guid id, Role role)
        {
            try
            {
                var roleDto = role.RoleConvertToDto();
                var response = await _httpClient.PutAsJsonAsync($"api/roles/{id}", roleDto);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Role updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating role: {ex.Message}");
                throw;
            }
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

                Console.WriteLine(
                    $"Failed to retrieve permissions for role {id}. Status code: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching role permissions: {ex.Message}");
                return null;
            }
        }
        
        public async Task AddPermissionToRoleAsync(Guid roleId, Permission permission)
        {
            
                var permissionDto = permission.PermissionConvertToDto();
                var response = await _httpClient.PostAsJsonAsync($"api/roles/{roleId}/permissions", permissionDto);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Permission added successfully.");
            
            
        }
        
        public async Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/roles/{roleId}/permissions/{permissionId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing permission: {ex.Message}");
                throw;
            }
        }
        
    }
}