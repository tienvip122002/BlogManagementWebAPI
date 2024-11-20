using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Data.Migrations
{
    public partial class cf2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "dc333a87-7ff7-40c0-b37b-f341b34e5145");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d1b188ae-4af2-40a5-bb28-8ba099bfd6bf", "1e323582-9aac-4457-8bb7-eeedb75d4b64" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "1e323582-9aac-4457-8bb7-eeedb75d4b64");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "d1b188ae-4af2-40a5-bb28-8ba099bfd6bf");

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b4b13c50-b4eb-4e93-8344-556d8cd036f3", 0, null, "bd4443f8-be95-4139-88ec-fbc07a05402d", "admin@ymail.com", false, null, false, null, "ADMIN@YMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEPQRHgel+HzeUoqXAf4b1n5nliHdx9VKLwuvSsz61bVYy8ZKfWKhizfSNze6rPUsUA==", null, false, "6fefaa17-3b16-4944-8e8f-bf6749f3b542", false, "administrator" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cd917c93-3c4d-4614-bf38-fc1413190f74", "a493c4f8-d963-4cbe-a25d-14aba47fbba6", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6cd1c12b-cfd6-480a-aa71-9b379f2c042a", "e60a43ba-fe7b-4001-8f7d-445a3c70650b", "User", "USER" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cd917c93-3c4d-4614-bf38-fc1413190f74", "b4b13c50-b4eb-4e93-8344-556d8cd036f3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "6cd1c12b-cfd6-480a-aa71-9b379f2c042a");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cd917c93-3c4d-4614-bf38-fc1413190f74", "b4b13c50-b4eb-4e93-8344-556d8cd036f3" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "b4b13c50-b4eb-4e93-8344-556d8cd036f3");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "cd917c93-3c4d-4614-bf38-fc1413190f74");

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1e323582-9aac-4457-8bb7-eeedb75d4b64", 0, null, "a0e00e05-1362-4128-ba08-adf8dd61aefe", "admin@ymail.com", false, null, false, null, "ADMIN@YMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEDNBEBf7VEUHiIC2kXa/YPxK9N7l5aX+WgSo8Dmfu9/ZnVcpBjGZ5gGZFaknzf0VYw==", null, false, "e49a7065-ffa3-4cc6-9e54-dfc65640a2eb", false, "administrator" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1b188ae-4af2-40a5-bb28-8ba099bfd6bf", "b508a869-26dc-4983-9ecf-20fbc28fe9bf", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc333a87-7ff7-40c0-b37b-f341b34e5145", "dc925180-6023-4b79-8e9e-1681281796d0", "User", "USER" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d1b188ae-4af2-40a5-bb28-8ba099bfd6bf", "1e323582-9aac-4457-8bb7-eeedb75d4b64" });
        }
    }
}
