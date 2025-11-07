using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roadmap.Beginner.ExpenseTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEndpoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Expense",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Expense",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "Currency" },
                values: new object[] { 5, 0 });

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Category", "Currency" },
                values: new object[] { 5, 0 });

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Category", "Currency" },
                values: new object[] { 5, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Expense");
        }
    }
}
