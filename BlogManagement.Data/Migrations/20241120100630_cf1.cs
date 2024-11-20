using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Data.Migrations
{
    public partial class cf1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "dec80ea8-84a4-4d08-ac6a-18ef1d99acdf");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e163c312-4147-4059-bc71-9708ccaf3f86", "6fadd302-3190-480b-917d-3e0182bbd248" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "6fadd302-3190-480b-917d-3e0182bbd248");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "e163c312-4147-4059-bc71-9708ccaf3f86");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "6fadd302-3190-480b-917d-3e0182bbd248", 0, null, "47e19d72-abee-4c98-b301-23e48d755fe3", "admin@ymail.com", false, null, false, null, "ADMIN@YMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEAOBMGOgiCOI0ZLW0g3rxeV2aCSrslVrkbgLC1zc5wkEIiXS3JCxohcs5pcOMzeUyg==", null, false, "15ef210c-24d2-4cc7-ae77-75b4c2efb628", false, "administrator" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e163c312-4147-4059-bc71-9708ccaf3f86", "0089d6d0-232f-4c8b-9671-6a9167c1b072", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dec80ea8-84a4-4d08-ac6a-18ef1d99acdf", "37f35969-6a6f-468c-bdf9-d8cca9600561", "User", "USER" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e163c312-4147-4059-bc71-9708ccaf3f86", "6fadd302-3190-480b-917d-3e0182bbd248" });
        }
    }
}
