using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystemTW.Security.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityAndRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4a1a2b2a-0ceb-4a00-ae7c-e57e3c637090", null, "Employee", "EMPLOYEE" },
                    { "b15eeb74-0e1a-4270-8358-790eb8b1f5e3", null, "Supervisor", "SUPERVISOR" },
                    { "dfe72870-e5ed-4235-a9b9-8103c3f0f0f3", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2a81d8f9-f054-4acc-b024-d2663b8e64a7", 0, "e9415146-92c1-4707-ba01-79fdff57815a", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN", "AQAAAAIAAYagAAAAELDNFRbjhcno3BqA+OrK7zSaXRM4eH6NWjBipw3sl5Ctw/XrgLxN6yFptAAozW/NYw==", null, false, "1707c5a2-cd5f-4a2d-8344-5f23cc76efc0", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dfe72870-e5ed-4235-a9b9-8103c3f0f0f3", "2a81d8f9-f054-4acc-b024-d2663b8e64a7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a1a2b2a-0ceb-4a00-ae7c-e57e3c637090");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b15eeb74-0e1a-4270-8358-790eb8b1f5e3");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dfe72870-e5ed-4235-a9b9-8103c3f0f0f3", "2a81d8f9-f054-4acc-b024-d2663b8e64a7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfe72870-e5ed-4235-a9b9-8103c3f0f0f3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a81d8f9-f054-4acc-b024-d2663b8e64a7");
        }
    }
}
