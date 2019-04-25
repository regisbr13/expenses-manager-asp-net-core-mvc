using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpensesManager.Migrations
{
    public partial class correction2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseType_ExpenseTypeId",
                table: "Expenses");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseType_ExpenseTypeId",
                table: "Expenses",
                column: "ExpenseTypeId",
                principalTable: "ExpenseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseType_ExpenseTypeId",
                table: "Expenses");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseType_ExpenseTypeId",
                table: "Expenses",
                column: "ExpenseTypeId",
                principalTable: "ExpenseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
