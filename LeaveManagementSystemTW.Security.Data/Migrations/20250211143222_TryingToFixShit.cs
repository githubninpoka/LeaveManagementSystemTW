using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystemTW.Security.Data.Migrations
{
    /// <inheritdoc />
    public partial class TryingToFixShit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a81d8f9-f054-4acc-b024-d2663b8e64a7",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "SecurityStamp", "UserName" },
                values: new object[] { "94092982-d079-468a-81a5-e8c37d8181d5", "ADMIN@LOCALHOST.COM", "2abb6c6b-bf93-4948-9725-40fa7cdba9ab", "admin@localhost.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a81d8f9-f054-4acc-b024-d2663b8e64a7",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "SecurityStamp", "UserName" },
                values: new object[] { "1060a73b-95f1-46d8-b9b4-e64b6de6163e", "ADMIN", "59b8cc41-6a3c-428b-819a-a771d976b7ff", null });
        }
    }
}
