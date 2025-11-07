using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roadmap.Beginner.ExpenseTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserModels",
                table: "UserModels");

            migrationBuilder.RenameTable(
                name: "UserModels",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "UserModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserModels",
                table: "UserModels",
                column: "Id");
        }
    }
}
