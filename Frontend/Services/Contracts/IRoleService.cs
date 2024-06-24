using ESOF.WebApp.DBLayer.Entities;

namespace Frontend.Services.Contracts;

public interface IRoleService
{
    Task<List<Role>> GetRolesAsync();
    Task<Role> GetRoleByIdAsync(Guid id);
    Task<Role> GetRoleByNameAsync(string name);
    Task<Role?> CreateRoleAsync(Role role);
    Task UpdateRoleAsync(Guid id, Role role);
    Task DeleteRoleAsync(Guid id);
    Task<List<Permission>?> GetRolePermissionsAsync(Guid id);
}