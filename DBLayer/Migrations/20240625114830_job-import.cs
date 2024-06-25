using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESOF.WebApp.DBLayer.Migrations
{
    /// <inheritdoc />
    public partial class jobimport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImportId",
                table: "Jobs",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDetails",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Imports",
                columns: table => new
                {
                    ImportId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imports", x => x.ImportId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ImportId",
                table: "Jobs",
                column: "ImportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Imports_ImportId",
                table: "Jobs",
                column: "ImportId",
                principalTable: "Imports",
                principalColumn: "ImportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Imports_ImportId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "Imports");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_ImportId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ImportId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "OtherDetails",
                table: "Jobs");
        }
    }
}
