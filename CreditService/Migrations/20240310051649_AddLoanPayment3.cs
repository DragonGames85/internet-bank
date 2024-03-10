using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditService.Migrations
{
    /// <inheritdoc />
    public partial class AddLoanPayment3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Payments",
                newName: "PercentageForPeriod");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Payments",
                newName: "MainDebt");

            migrationBuilder.AddColumn<decimal>(
                name: "AmountOfPayment",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BalanceOwed",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfPayment",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BalanceOwed",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PercentageForPeriod",
                table: "Payments",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "MainDebt",
                table: "Payments",
                newName: "Balance");
        }
    }
}
