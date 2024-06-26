using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESOF.WebApp.DBLayer.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullInvoiceIdInTimesheets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Invoices_TimesheetId",
                table: "Timesheets");

            migrationBuilder.AlterColumn<Guid>(
                name: "InvoiceId",
                table: "Timesheets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TimesheetId",
                table: "Invoices",
                column: "TimesheetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Timesheets_TimesheetId",
                table: "Invoices",
                column: "TimesheetId",
                principalTable: "Timesheets",
                principalColumn: "TimesheetId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Timesheets_TimesheetId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_TimesheetId",
                table: "Invoices");

            migrationBuilder.AlterColumn<Guid>(
                name: "InvoiceId",
                table: "Timesheets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Invoices_TimesheetId",
                table: "Timesheets",
                column: "TimesheetId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
