using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditService.Migrations
{
    /// <inheritdoc />
    public partial class AddPennyInTariff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "CreditTariff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PennyPercent",
                table: "CreditTariff",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "Currency",
                table: "Credit",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Credit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PaymentPeriod",
                table: "Credit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RepaymentPeriod",
                table: "Credit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Credit",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "CreditTariff");

            migrationBuilder.DropColumn(
                name: "PennyPercent",
                table: "CreditTariff");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Credit");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Credit");

            migrationBuilder.DropColumn(
                name: "PaymentPeriod",
                table: "Credit");

            migrationBuilder.DropColumn(
                name: "RepaymentPeriod",
                table: "Credit");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Credit");
        }
    }
}
