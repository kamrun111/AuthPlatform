using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLineTotalToInvoiceDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LineTotal",
                table: "ExpenditureInvoiceDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineTotal",
                table: "ExpenditureInvoiceDetails");
        }
    }
}
