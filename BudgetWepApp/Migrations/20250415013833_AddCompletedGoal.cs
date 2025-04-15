using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetWepApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCompletedGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Goals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Goals",
                keyColumn: "GoalID",
                keyValue: 1,
                column: "IsCompleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Goals",
                keyColumn: "GoalID",
                keyValue: 2,
                column: "IsCompleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Goals");
        }
    }
}
