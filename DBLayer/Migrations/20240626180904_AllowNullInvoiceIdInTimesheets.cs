using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESOF.WebApp.DBLayer.Migrations
{
    public partial class AllowNullInvoiceIdInTimesheets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='Timesheets' AND column_name='InvoiceId') THEN
                        ALTER TABLE ""Timesheets"" ADD COLUMN ""InvoiceId"" uuid;
                    END IF;
                END $$;
            ");

            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_Timesheets_Invoices_TimesheetId') THEN
                        ALTER TABLE ""Timesheets"" DROP CONSTRAINT ""FK_Timesheets_Invoices_TimesheetId"";
                    END IF;
                END $$;
            ");

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
