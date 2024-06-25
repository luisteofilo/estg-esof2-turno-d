using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESOF.WebApp.DBLayer.Migrations
{
    /// <inheritdoc />
    public partial class FAQAlterQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQQuestions_FAQAnswers_VerifiedAnswerAnswerId",
                table: "FAQQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_FAQQuestions_Job_JobId",
                table: "FAQQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_FAQQuestions_Users_VerifierUserId",
                table: "FAQQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Job",
                table: "Job");

            migrationBuilder.RenameTable(
                name: "Job",
                newName: "Jobs");

            migrationBuilder.AlterColumn<Guid>(
                name: "VerifierUserId",
                table: "FAQQuestions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "VerifiedAnswerAnswerId",
                table: "FAQQuestions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQQuestions_FAQAnswers_VerifiedAnswerAnswerId",
                table: "FAQQuestions",
                column: "VerifiedAnswerAnswerId",
                principalTable: "FAQAnswers",
                principalColumn: "AnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQQuestions_Jobs_JobId",
                table: "FAQQuestions",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FAQQuestions_Users_VerifierUserId",
                table: "FAQQuestions",
                column: "VerifierUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQQuestions_FAQAnswers_VerifiedAnswerAnswerId",
                table: "FAQQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_FAQQuestions_Jobs_JobId",
                table: "FAQQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_FAQQuestions_Users_VerifierUserId",
                table: "FAQQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs");

            migrationBuilder.RenameTable(
                name: "Jobs",
                newName: "Job");

            migrationBuilder.AlterColumn<Guid>(
                name: "VerifierUserId",
                table: "FAQQuestions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "VerifiedAnswerAnswerId",
                table: "FAQQuestions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Job",
                table: "Job",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQQuestions_FAQAnswers_VerifiedAnswerAnswerId",
                table: "FAQQuestions",
                column: "VerifiedAnswerAnswerId",
                principalTable: "FAQAnswers",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FAQQuestions_Job_JobId",
                table: "FAQQuestions",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FAQQuestions_Users_VerifierUserId",
                table: "FAQQuestions",
                column: "VerifierUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
