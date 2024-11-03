using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPay.Migrations
{
    /// <inheritdoc />
    public partial class initialdbb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Employees_EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "LeaveRequests",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests",
                newName: "IX_LeaveRequests_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Users_UserId",
                table: "LeaveRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Users_UserId",
                table: "LeaveRequests");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "LeaveRequests",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveRequests_UserId",
                table: "LeaveRequests",
                newName: "IX_LeaveRequests_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Employees_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
