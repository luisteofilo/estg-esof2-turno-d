using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class RolePermission
{
    public Guid RoleId { get; set; }
    
    public Guid PermissionId { get; set; }
    
    public Role Role { get; set; }
    public Permission Permission { get; set; }

    
}