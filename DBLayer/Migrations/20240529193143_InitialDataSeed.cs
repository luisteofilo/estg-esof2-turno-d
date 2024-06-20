using ESOF.WebApp.DBLayer.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESOF.WebApp.DBLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
       // Adding roles
        var adminRoleId = Guid.NewGuid();
        var normalRoleId = Guid.NewGuid();

        migrationBuilder.InsertData(
            table: "Roles",
            columns: new[] { "RoleId", "Name" },
            values: new object[,]
            {
                { adminRoleId, "Admin" },
                { normalRoleId, "Normal" }
            });

        // Adding permissions
        var sampleFeature1PermissionId = Guid.NewGuid();
        var sampleFeature2PermissionId = Guid.NewGuid();
        var sampleAdminFeaturePermissionId = Guid.NewGuid();

        migrationBuilder.InsertData(
            table: "Permissions",
            columns: new[] { "PermissionId", "Name" },
            values: new object[,]
            {
                { sampleFeature1PermissionId, "Sample Feature 1" },
                { sampleFeature2PermissionId, "Sample Feature 2" },
                { sampleAdminFeaturePermissionId, "Sample Admin Feature" }
            });

        // Adding role-permissions
        migrationBuilder.InsertData(
            table: "RolePermissions",
            columns: new[] { "RoleId", "PermissionId" },
            values: new object[,]
            {
                { adminRoleId, sampleFeature1PermissionId },
                { adminRoleId, sampleFeature2PermissionId },
                { adminRoleId, sampleAdminFeaturePermissionId },
                { normalRoleId, sampleFeature1PermissionId },
                { normalRoleId, sampleFeature2PermissionId }
            });

        // Adding users
        var adminUserId = Guid.NewGuid();
        var normalUserId = Guid.NewGuid();
        PasswordHelper.CreatePasswordHash("root", out byte[] adminPasswordHash, out byte[] adminPasswordSalt);
        PasswordHelper.CreatePasswordHash("normal", out byte[] normalPasswordHash, out byte[] normalPasswordSalt);

        migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "UserId", "Email", "PasswordHash", "PasswordSalt" },
            values: new object[,]
            {
                { adminUserId, "root@example.com", adminPasswordHash, adminPasswordSalt },
                { normalUserId, "normal@example.com", normalPasswordHash, normalPasswordSalt }
            });

        // Adding user-roles
        migrationBuilder.InsertData(
            table: "UserRoles",
            columns: new[] { "UserId", "RoleId" },
            values: new object[,]
            {
                { adminUserId, adminRoleId },
                { normalUserId, normalRoleId }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM UserRoles");
            migrationBuilder.Sql("DELETE FROM Users");
            migrationBuilder.Sql("DELETE FROM RolePermissions");
            migrationBuilder.Sql("DELETE FROM Permissions");
            migrationBuilder.Sql("DELETE FROM Roles");
            migrationBuilder.Sql("DELETE FROM Companies");
            migrationBuilder.Sql("DELETE FROM UserCompany");
        }
    }
}
