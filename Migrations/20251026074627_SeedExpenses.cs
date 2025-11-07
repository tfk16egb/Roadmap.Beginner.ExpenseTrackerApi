using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Roadmap.Beginner.ExpenseTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_User_UserModelId",
                table: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_Expense_UserModelId",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Expense");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Expense",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Expense",
                columns: new[] { "Id", "Amaount", "DateOfTransaction", "Description", "UserId" },
                values: new object[,]
                {
                    { 1, 999.00m, new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Sample Expense", 3 },
                    { 2, 100.00m, new DateTimeOffset(new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Beer", 2 },
                    { 3, 200.00m, new DateTimeOffset(new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Coffee", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expense_UserId",
                table: "Expense",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_User_UserId",
                table: "Expense",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_User_UserId",
                table: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_User_Username",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Expense_UserId",
                table: "Expense");

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Expense");

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Expense",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expense_UserModelId",
                table: "Expense",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_User_UserModelId",
                table: "Expense",
                column: "UserModelId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
