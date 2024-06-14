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
                name: "FAQAnswers",
                columns: table => new
                {
                    AnswerId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    AuthorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnswerText = table.Column<string>(type: "text", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQAnswers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_FAQAnswers_Users_AuthorUserId",
                        column: x => x.AuthorUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FAQQuestions",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    QuestionText = table.Column<string>(type: "text", nullable: false),
                    VerifiedAnswerAnswerId = table.Column<Guid>(type: "uuid", nullable: false),
                    VerifierUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQQuestions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_FAQQuestions_FAQAnswers_VerifiedAnswerAnswerId",
                        column: x => x.VerifiedAnswerAnswerId,
                        principalTable: "FAQAnswers",
                        principalColumn: "AnswerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FAQQuestions_Users_VerifierUserId",
                        column: x => x.VerifierUserId,
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
                name: "IX_FAQQuestions_VerifiedAnswerAnswerId",
                table: "FAQQuestions",
                column: "VerifiedAnswerAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_FAQQuestions_VerifierUserId",
                table: "FAQQuestions",
                column: "VerifierUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQAnswers_FAQQuestions_QuestionId",
                table: "FAQAnswers",
                column: "QuestionId",
                principalTable: "FAQQuestions",
                principalColumn: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQAnswers_FAQQuestions_QuestionId",
                table: "FAQAnswers");

            migrationBuilder.DropTable(
                name: "FAQQuestions");

            migrationBuilder.DropTable(
                name: "FAQAnswers");
        }
    }
}
