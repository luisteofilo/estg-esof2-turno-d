using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESOF.WebApp.DBLayer.Migrations
{
    /// <inheritdoc />
    public partial class FAQWithJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JobId",
                table: "FAQQuestions",
                type: "uuid",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    JobTitle = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FAQQuestions_JobId",
                table: "FAQQuestions",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQQuestions_Job_JobId",
                table: "FAQQuestions",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQQuestions_Job_JobId",
                table: "FAQQuestions");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropIndex(
                name: "IX_FAQQuestions_JobId",
                table: "FAQQuestions");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "FAQQuestions");
        }
    }
}
