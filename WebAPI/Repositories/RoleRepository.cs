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
        
        public async Task<Permission?> AddPermissionToRoleAsync(Guid roleId, Permission? permission)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);
            
            if (role == null || permission == null) return null;
            
            await _dbContext.Permissions.AddAsync(permission);
            
            var entityEntry = await _dbContext.RolePermissions.AddAsync(new RolePermission
            {
                RoleId = role.RoleId,
                PermissionId = permission.PermissionId
            });
            
            
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity.Permission;

        }
        
        public async Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId)
        {
            var role = await _dbContext.Roles.Include(role => role.RolePermissions).FirstOrDefaultAsync(r => r.RoleId == roleId);
    
            if (role == null) return;
    
            var rolePermission = role.RolePermissions.FirstOrDefault(rp => rp.PermissionId == permissionId);
            var permission = await _dbContext.Permissions.FindAsync(permissionId);
    
            if (rolePermission != null && permission != null)
            {
                _dbContext.RolePermissions.Remove(rolePermission);
                _dbContext.Permissions.Remove(permission);  
                await _dbContext.SaveChangesAsync();
            }
        }

        
        public async Task<List<User>> GetUsersByRoleAsync(Guid roleId)
        {
            return await _dbContext.Users
                .Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId))
                .ToListAsync();
        }
        
        public async Task<Permission?> GetPermissionByIdAsync(Guid permissionId)
        {
            return await _dbContext.Permissions.FindAsync(permissionId);
        }
    }
}