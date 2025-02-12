using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystemTW.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class StaticPassHashAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a81d8f9-f054-4acc-b024-d2663b8e64a7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1060a73b-95f1-46d8-b9b4-e64b6de6163e", "AQAAAAIAAYagAAAAEHodb2tgnqTz/jgT6xtYgxSWDdz8uipG3FzohM9jydkKqhjpSpgym//5oF3tNq6BWQ==", "59b8cc41-6a3c-428b-819a-a771d976b7ff" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a81d8f9-f054-4acc-b024-d2663b8e64a7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9415146-92c1-4707-ba01-79fdff57815a", "AQAAAAIAAYagAAAAELDNFRbjhcno3BqA+OrK7zSaXRM4eH6NWjBipw3sl5Ctw/XrgLxN6yFptAAozW/NYw==", "1707c5a2-cd5f-4a2d-8344-5f23cc76efc0" });
        }
    }
}
