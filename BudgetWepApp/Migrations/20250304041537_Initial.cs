using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BudgetWepApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentBalance = table.Column<double>(type: "float", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAddded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.GoalID);
                    table.ForeignKey(
                        name: "FK_Goals_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    IncomeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    IncomeAmmount = table.Column<double>(type: "float", nullable: false),
                    PayPeriodDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.IncomeID);
                    table.ForeignKey(
                        name: "FK_Incomes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recurringPayments",
                columns: table => new
                {
                    RecurringPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PaymentAmount = table.Column<double>(type: "float", nullable: false),
                    PaymenFrequencyDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recurringPayments", x => x.RecurringPaymentId);
                    table.ForeignKey(
                        name: "FK_recurringPayments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ammount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "AccountCreationDate", "CurrentBalance", "Email", "IsAdmin", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000.0, "test1@email.com", true, "1234", "Name1" },
                    { 2, new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000.0, "test2@email.com", true, "1234", "Name2" }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "GoalID", "DateAddded", "Description", "Name", "UserID" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the first goal.", "Goal 1", 1 },
                    { 2, new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is the second goal.", "Goal 2", 2 }
                });

            migrationBuilder.InsertData(
                table: "Incomes",
                columns: new[] { "IncomeID", "IncomeAmmount", "PayPeriodDays", "UserID" },
                values: new object[,]
                {
                    { 1, 100.0, 14, 1 },
                    { 2, 200.0, 7, 2 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionID", "Ammount", "TimeStamp", "UserID" },
                values: new object[,]
                {
                    { 1, 100.0, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 200.0, new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "recurringPayments",
                columns: new[] { "RecurringPaymentId", "PaymenFrequencyDays", "PaymentAmount", "UserID" },
                values: new object[,]
                {
                    { 1, 14, 100.0, 1 },
                    { 2, 7, 200.0, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserID",
                table: "Goals",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UserID",
                table: "Incomes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_recurringPayments_UserID",
                table: "recurringPayments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserID",
                table: "Transactions",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "recurringPayments");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
