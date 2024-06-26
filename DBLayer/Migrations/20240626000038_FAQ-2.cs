using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESOF.WebApp.DBLayer.Migrations
{
    /// <inheritdoc />
    public partial class FAQ2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorEmail",
                table: "FAQAnswers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FAQAnswers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorEmail",
                table: "FAQAnswers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FAQAnswers");
        }
    }
}
