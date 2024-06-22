using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ESOF.WebApp.DBLayer.Context;

namespace ESOF.WebApp.WebAPI.Repositories
{
    public class RoleRepository
    {
        private readonly ApplicationDbContext _dbContext = new();

        public async Task<ICollection<Role>> GetRolesAsync()
        {
            return await _dbContext.Roles
                .Include(r => r.UserRoles)
                .Include(r => r.RolePermissions)
                .OrderBy(p => p.RoleId)
                .ToListAsync();
        }

        public async Task<bool> RoleExistsAsync(Guid roleId)
        {
            return await _dbContext.Roles.AnyAsync(p => p.RoleId == roleId);
        }

        public async Task<int> UpdateRoleAsync(Role role)
        {
            _dbContext.Roles.Update(role);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(Guid roleId)
        {
            var role = await _dbContext.Roles.FindAsync(roleId);

            if (role != null)
            {
                _dbContext.Roles.Remove(role);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            var entityEntry = await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<Role?> GetRoleByIdAsync(Guid roleId)
        {
            return await _dbContext.Roles
                .Include(r => r.UserRoles)
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.RoleId == roleId);
        }
        
        public async Task<Role?> GetRoleByNameAsync(string name)
        {
            return await _dbContext.Roles
                .Include(r => r.UserRoles)
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Name == name);
        }
        
        public async Task<List<Permission>?> GetRolePermissionsAsync(Guid roleId)
        {
            var role = await _dbContext.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(r => r.RoleId == roleId);
            
            var permissions = role?.RolePermissions.Select(rp => rp.Permission).ToList();
            return permissions;
        }
    }
}