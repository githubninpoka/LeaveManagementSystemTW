using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystemTW.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a81d8f9-f054-4acc-b024-d2663b8e64a7",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "157a4348-e399-4eb3-ba31-29b829a965e8", null, "Ad", "Min", "AQAAAAIAAYagAAAAENNRLkOZRbDw/OW14ebIsCs/Yj7Q9/MV0RxeZQ8igvHxDddJ3dpW1jeSlNwg+pQk2g==", "03a1ffd8-7ee0-4f7b-a2e4-fd48475d86bf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a81d8f9-f054-4acc-b024-d2663b8e64a7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94092982-d079-468a-81a5-e8c37d8181d5", "AQAAAAIAAYagAAAAEHodb2tgnqTz/jgT6xtYgxSWDdz8uipG3FzohM9jydkKqhjpSpgym//5oF3tNq6BWQ==", "2abb6c6b-bf93-4948-9725-40fa7cdba9ab" });
        }
    }
}
