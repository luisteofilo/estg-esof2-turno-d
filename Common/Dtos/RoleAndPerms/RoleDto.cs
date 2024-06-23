namespace Common.Dtos.Role
{
    public class RoleDto
    {
        public Guid RoleId { get; set; }
        public string? Name { get; set; }
        public List<UserRoleDto>? UserRoles { get; set; }
        public List<RolePermissionDto>? RolePermissions { get; set; }
    }

    public class UserRoleDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }

    public class RolePermissionDto
    {
        public Guid PermissionId { get; set; }
        public Guid RoleId { get; set; }
    }
    
    public class PermissionDto
    {
        public Guid PermissionId { get; set; }
        public string? Name { get; set; }
    }
}