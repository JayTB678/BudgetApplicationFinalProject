using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetWepApp.Migrations
{
    /// <inheritdoc />
    public partial class StartDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysTillNextPayment",
                table: "recurringPayments");

            migrationBuilder.DropColumn(
                name: "DaysTillNextPayment",
                table: "Incomes");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartingDate",
                table: "recurringPayments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartingDate",
                table: "Incomes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "IncomeID",
                keyValue: 1,
                column: "StartingDate",
                value: new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "IncomeID",
                keyValue: 2,
                column: "StartingDate",
                value: new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "recurringPayments",
                keyColumn: "RecurringPaymentId",
                keyValue: 1,
                column: "StartingDate",
                value: new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "recurringPayments",
                keyColumn: "RecurringPaymentId",
                keyValue: 2,
                column: "StartingDate",
                value: new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartingDate",
                table: "recurringPayments");

            migrationBuilder.DropColumn(
                name: "StartingDate",
                table: "Incomes");

            migrationBuilder.AddColumn<int>(
                name: "DaysTillNextPayment",
                table: "recurringPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DaysTillNextPayment",
                table: "Incomes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "IncomeID",
                keyValue: 1,
                column: "DaysTillNextPayment",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "IncomeID",
                keyValue: 2,
                column: "DaysTillNextPayment",
                value: 5);

            migrationBuilder.UpdateData(
                table: "recurringPayments",
                keyColumn: "RecurringPaymentId",
                keyValue: 1,
                column: "DaysTillNextPayment",
                value: 5);

            migrationBuilder.UpdateData(
                table: "recurringPayments",
                keyColumn: "RecurringPaymentId",
                keyValue: 2,
                column: "DaysTillNextPayment",
                value: 5);
        }
    }
}
