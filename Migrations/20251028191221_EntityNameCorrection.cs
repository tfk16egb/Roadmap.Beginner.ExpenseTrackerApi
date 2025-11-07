using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roadmap.Beginner.ExpenseTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class EntityNameCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amaount",
                table: "Expense",
                newName: "Amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Expense",
                newName: "Amaount");
        }
    }
}
