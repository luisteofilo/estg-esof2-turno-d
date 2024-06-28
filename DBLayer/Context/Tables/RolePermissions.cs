using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildRolePermissions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RolePermission>()
            .HasKey(e => new { e.RoleId, e.PermissionId });

    }
}
