using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetWepApp.Migrations
{
    /// <inheritdoc />
    public partial class addeddaystill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysTillNextPayment",
                table: "recurringPayments");

            migrationBuilder.DropColumn(
                name: "DaysTillNextPayment",
                table: "Incomes");
        }
    }
}
