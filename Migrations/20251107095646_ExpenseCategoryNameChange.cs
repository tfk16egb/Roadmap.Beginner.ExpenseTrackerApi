using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roadmap.Beginner.ExpenseTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class ExpenseCategoryNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Expense",
                newName: "ExpenseCategory");

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpenseCategory",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpenseCategory",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpenseCategory",
                value: 9);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpenseCategory",
                table: "Expense",
                newName: "Category");

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 1,
                column: "Category",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 2,
                column: "Category",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 3,
                column: "Category",
                value: 5);
        }
    }
}
