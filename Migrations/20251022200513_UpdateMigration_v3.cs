using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roadmap.Beginner.ExpenseTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "User",
                newName: "Name");
        }
    }
}
