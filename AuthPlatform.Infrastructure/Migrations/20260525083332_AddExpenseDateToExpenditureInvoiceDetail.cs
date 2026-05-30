using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExpenseDateToExpenditureInvoiceDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpenseDate",
                table: "ExpenditureInvoiceDetails",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseDate",
                table: "ExpenditureInvoiceDetails");
        }
    }
}
