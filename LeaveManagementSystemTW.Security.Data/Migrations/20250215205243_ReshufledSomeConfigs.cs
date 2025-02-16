using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystemTW.Security.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReshufledSomeConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a81d8f9-f054-4acc-b024-d2663b8e64a7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "363b2570-a75e-4e1d-8050-9c2fe1ffa1dc", "AQAAAAIAAYagAAAAEDWqZdVy5RSDY+KQIZPz0WiSSRzN99AwuxhDaPRPmgWgToNDxKAsDQAgLpFm30H1pQ==", "aeff2401-2947-486c-b2f6-ded50916581e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a81d8f9-f054-4acc-b024-d2663b8e64a7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "157a4348-e399-4eb3-ba31-29b829a965e8", "AQAAAAIAAYagAAAAENNRLkOZRbDw/OW14ebIsCs/Yj7Q9/MV0RxeZQ8igvHxDddJ3dpW1jeSlNwg+pQk2g==", "03a1ffd8-7ee0-4f7b-a2e4-fd48475d86bf" });
        }
    }
}
