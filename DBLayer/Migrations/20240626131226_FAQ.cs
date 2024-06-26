using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESOF.WebApp.DBLayer.Migrations
{
    /// <inheritdoc />
    public partial class FAQ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FAQJobs",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    JobTitle = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQJobs", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "FAQQuestions",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    QuestionText = table.Column<string>(type: "text", nullable: false),
                    VerifierUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    JobId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQQuestions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_FAQQuestions_FAQJobs_JobId",
                        column: x => x.JobId,
                        principalTable: "FAQJobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FAQQuestions_Users_VerifierUserId",
                        column: x => x.VerifierUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "FAQAnswers",
                columns: table => new
                {
                    AnswerId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    AuthorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnswerText = table.Column<string>(type: "text", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    AuthorEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQAnswers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_FAQAnswers_FAQQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "FAQQuestions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FAQAnswers_Users_AuthorUserId",
                        column: x => x.AuthorUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FAQAnswers_AuthorUserId",
                table: "FAQAnswers",
                column: "AuthorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FAQAnswers_QuestionId",
                table: "FAQAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FAQQuestions_JobId",
                table: "FAQQuestions",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_FAQQuestions_VerifierUserId",
                table: "FAQQuestions",
                column: "VerifierUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FAQAnswers");

            migrationBuilder.DropTable(
                name: "FAQQuestions");

            migrationBuilder.DropTable(
                name: "FAQJobs");
        }
    }
}
