using ESOF.WebApp.DBLayer.Entities;

namespace Common.Dtos.RoleAndPerms
{
    public static class DtoConversion
    {
        public static RoleDto RoleConvertToDto(this Role role)
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

        public static Role DtoConvertToRole(this RoleDto roleDto)
        {
            return new Role
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
        
        public static UserRoleDto UserRoleConvertToDto(this UserRole userRole)
        {
            return new UserRoleDto
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId
            };
        }
        
        public static UserRole DtoConvertToUserRole(this UserRoleDto userRoleDto)
        {
            return new UserRole
            {
                UserId = userRoleDto.UserId,
                RoleId = userRoleDto.RoleId
            };
        }
        
    }
}