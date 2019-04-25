using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpensesManager.Migrations
{
    public partial class correction4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_IncomeType_IncomeTypeId",
                table: "Incomes");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_IncomeType_IncomeTypeId",
                table: "Incomes",
                column: "IncomeTypeId",
                principalTable: "IncomeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_IncomeType_IncomeTypeId",
                table: "Incomes");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_IncomeType_IncomeTypeId",
                table: "Incomes",
                column: "IncomeTypeId",
                principalTable: "IncomeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
