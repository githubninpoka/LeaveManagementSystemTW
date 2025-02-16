using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_LeaveRequestStatuses_StatusId",
                table: "LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_StatusId",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "LeaveRequests");

            migrationBuilder.RenameColumn(
                name: "LeaveRequestId",
                table: "LeaveRequests",
                newName: "LeaveRequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveRequestStatusId",
                table: "LeaveRequests",
                column: "LeaveRequestStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_LeaveRequestStatuses_LeaveRequestStatusId",
                table: "LeaveRequests",
                column: "LeaveRequestStatusId",
                principalTable: "LeaveRequestStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_LeaveRequestStatuses_LeaveRequestStatusId",
                table: "LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_LeaveRequestStatusId",
                table: "LeaveRequests");

            migrationBuilder.RenameColumn(
                name: "LeaveRequestStatusId",
                table: "LeaveRequests",
                newName: "LeaveRequestId");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "LeaveRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_StatusId",
                table: "LeaveRequests",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_LeaveRequestStatuses_StatusId",
                table: "LeaveRequests",
                column: "StatusId",
                principalTable: "LeaveRequestStatuses",
                principalColumn: "Id");
        }
    }
}
