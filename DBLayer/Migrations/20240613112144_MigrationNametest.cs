using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESOF.WebApp.DBLayer.Migrations
{
    /// <inheritdoc />
    public partial class MigrationNametest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vertical",
                columns: table => new
                {
                    VerticalId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    VerticalName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vertical", x => x.VerticalId);
                });

            migrationBuilder.CreateTable(
                name: "Role_verticals",
                columns: table => new
                {
                    Role_verticalsId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Role_verticalsName = table.Column<string>(type: "text", nullable: false),
                    VerticalId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_verticals", x => x.Role_verticalsId);
                    table.ForeignKey(
                        name: "FK_Role_verticals_Vertical_VerticalId",
                        column: x => x.VerticalId,
                        principalTable: "Vertical",
                        principalColumn: "VerticalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "verticalsUser",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    VerticalId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_verticalsUser", x => new { x.UserId, x.VerticalId });
                    table.ForeignKey(
                        name: "FK_verticalsUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_verticalsUser_Vertical_VerticalId",
                        column: x => x.VerticalId,
                        principalTable: "Vertical",
                        principalColumn: "VerticalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "skil_veticals",
                columns: table => new
                {
                    skil_veticalsId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    skil_veticalsName = table.Column<string>(type: "text", nullable: false),
                    skil_veticalsExperiencia = table.Column<int>(type: "integer", nullable: false),
                    Role_verticalsId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Role_verticals_VerticalId",
                table: "Role_verticals",
                column: "VerticalId");

            migrationBuilder.CreateIndex(
                name: "IX_skil_veticals_Role_verticalsId",
                table: "skil_veticals",
                column: "Role_verticalsId");

            migrationBuilder.CreateIndex(
                name: "IX_verticalsUser_VerticalId",
                table: "verticalsUser",
                column: "VerticalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "skil_veticals");

            migrationBuilder.DropTable(
                name: "verticalsUser");

            migrationBuilder.DropTable(
                name: "Role_verticals");

            migrationBuilder.DropTable(
                name: "Vertical");
        }
    }
}
