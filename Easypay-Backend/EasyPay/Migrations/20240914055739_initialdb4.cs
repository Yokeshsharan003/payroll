using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPay.Migrations
{
    /// <inheritdoc />
    public partial class initialdb4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "NetAmount",
                table: "Payrolls",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalEarnings",
                table: "Payrolls",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetAmount",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "TotalEarnings",
                table: "Payrolls");
        }
    }
}
