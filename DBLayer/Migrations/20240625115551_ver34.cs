using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESOF.WebApp.DBLayer.Migrations
{
    /// <inheritdoc />
    public partial class ver34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Verticals",
                columns: table => new
                {
                    VerticalId = table.Column<Guid>(nullable: false),
                    VerticalName = table.Column<string>(nullable: false)
                    // Add other columns as needed
                },
                constraints: table => { table.PrimaryKey("PK_Verticals", x => x.VerticalId); });
            migrationBuilder.CreateTable(
                name: "Role_verticals",
                columns: table => new
                {
                    Role_verticalsId = table.Column<Guid>(nullable: false),
                    Role_verticalsName = table.Column<string>(nullable: false),
                    VerticalId = table.Column<Guid>(nullable: false)
                    // Add other columns as needed
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_verticals", x => x.Role_verticalsId);
                    table.ForeignKey(
                        name: "FK_Role_verticals_Verticals_VerticalId",
                        column: x => x.VerticalId,
                        principalTable: "Verticals",
                        principalColumn: "VerticalId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "skil_veticals",
                columns: table => new
                {
                    skil_veticalsId = table.Column<Guid>(nullable: false),
                    skil_veticalsName = table.Column<string>(nullable: false),
                    skil_veticalsExperiencia = table.Column<int>(nullable: false),
                    Role_verticalsId = table.Column<Guid>(nullable: false)
                    // Add other columns as needed
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skil_veticals", x => x.skil_veticalsId);
                    table.ForeignKey(
                        name: "FK_skil_veticals_Role_verticals_Role_verticalsId",
                        column: x => x.Role_verticalsId,
                        principalTable: "Role_verticals",
                        principalColumn: "Role_verticalsId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "verticalsUser",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    VerticalId = table.Column<Guid>(nullable: false)
                    // Add other columns as needed
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_verticalsUser", x => new { x.UserId, x.VerticalId });
                    table.ForeignKey(
                        name: "FK_verticalsUser_Verticals_VerticalId",
                        column: x => x.VerticalId,
                        principalTable: "Verticals",
                        principalColumn: "VerticalId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Verticals");
            migrationBuilder.DropTable(
                name: "Role_verticals");
            migrationBuilder.DropTable(
                name: "skil_veticals");
            migrationBuilder.DropTable(
                name: "verticalsUser");
        }
    }
}
