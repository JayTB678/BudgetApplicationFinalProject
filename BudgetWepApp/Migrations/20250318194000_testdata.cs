using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetWepApp.Migrations
{
    /// <inheritdoc />
    public partial class testdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "TransactionID",
                keyValue: 2,
                columns: new[] { "Ammount", "UserID" },
                values: new object[] { -50.0, 1 });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionID", "Ammount", "TimeStamp", "UserID" },
                values: new object[] { 3, 200.0, new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionID",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "TransactionID",
                keyValue: 2,
                columns: new[] { "Ammount", "UserID" },
                values: new object[] { 200.0, 2 });
        }
    }
}
