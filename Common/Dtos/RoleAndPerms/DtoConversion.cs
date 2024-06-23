using Common.Dtos.Role;
using ESOF.WebApp.DBLayer.Entities;

namespace Common.Dtos.Role
{
    public static class DtoConversion
    {
        public static RoleDto RoleConvertToDto(this ESOF.WebApp.DBLayer.Entities.Role role)
        {
            return new RoleDto
            {
                RoleId = role.RoleId,
                Name = role.Name,
                UserRoles = role.UserRoles?.Select(ur => new UserRoleDto
                {
                    UserId = ur.UserId,
                    RoleId = ur.RoleId
                }).ToList(),
                RolePermissions = role.RolePermissions?.Select(rp => new RolePermissionDto
                {
                    PermissionId = rp.PermissionId,
                    RoleId = rp.RoleId
                }).ToList()
            };
        }

        public static ESOF.WebApp.DBLayer.Entities.Role DtoConvertToRole(this RoleDto roleDto)
        {
            return new ESOF.WebApp.DBLayer.Entities.Role
            {
                RoleId = roleDto.RoleId,
                Name = roleDto.Name,
                UserRoles = roleDto.UserRoles?.Select(ur => new UserRole
                {
                    UserId = ur.UserId,
                    RoleId = ur.RoleId
                }).ToList(),
                RolePermissions = roleDto.RolePermissions?.Select(rp => new RolePermission
                {
                    PermissionId = rp.PermissionId,
                    RoleId = rp.RoleId
                }).ToList()
            };
        }
        
        public static PermissionDto? PermissionConvertToDto(this Permission? permission)
        {
            if (permission == null)
            {
                return null;
            }

            return new PermissionDto
            {
                PermissionId = permission.PermissionId,
                Name = permission.Name
            };
        }
        
        public static Permission DtoConvertToPermission(this PermissionDto permissionDto)
        {
            return new Permission
            {
                PermissionId = permissionDto.PermissionId,
                Name = permissionDto.Name
            };
        }
        
    }
}