using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPay.Migrations
{
    /// <inheritdoc />
    public partial class Sat28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "EmployeeDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "EmployeeDetails",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
