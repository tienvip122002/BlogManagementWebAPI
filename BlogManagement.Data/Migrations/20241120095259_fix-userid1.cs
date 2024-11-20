using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Data.Migrations
{
    public partial class fixuserid1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "88743d0f-120e-41ed-837d-ac2444e1ec18");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "407d4e8f-e3c8-4fad-8507-b8f19d8b0632", "81274339-6069-49be-bb9f-6ce761fa26f4" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "81274339-6069-49be-bb9f-6ce761fa26f4");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "407d4e8f-e3c8-4fad-8507-b8f19d8b0632");

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a1ec4114-8ca5-46dc-9353-088fc9f8cc7c", 0, null, "cba26e74-6cd0-4dd2-b49f-d019082a66c2", "admin@ymail.com", false, null, false, null, "ADMIN@YMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEMzeoHK+J/ba+BtX3akPTT2ZxZtadVYaYcGMmWGQqDVT0D024fcsMKo53gRDOsOJBQ==", null, false, "2f5bf15a-f3eb-4e71-bbaa-95c94d0ed5ca", false, "administrator" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "937ff76f-218c-4fd0-b08a-10b7cc0faf4c", "20b8f6d9-418c-4137-99ab-181d0a45e946", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dbb26b8d-6bbc-4181-9f8e-df8ed9cdf4fa", "bd1c67d5-3684-4f2f-9ccd-1e5fd9958ead", "User", "USER" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "937ff76f-218c-4fd0-b08a-10b7cc0faf4c", "a1ec4114-8ca5-46dc-9353-088fc9f8cc7c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "dbb26b8d-6bbc-4181-9f8e-df8ed9cdf4fa");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "937ff76f-218c-4fd0-b08a-10b7cc0faf4c", "a1ec4114-8ca5-46dc-9353-088fc9f8cc7c" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "a1ec4114-8ca5-46dc-9353-088fc9f8cc7c");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "937ff76f-218c-4fd0-b08a-10b7cc0faf4c");

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "81274339-6069-49be-bb9f-6ce761fa26f4", 0, null, "b1d91e83-1537-44b0-969b-c9e59944804d", "admin@ymail.com", false, null, false, null, "ADMIN@YMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEF3vLnF4Zm6gZULcfQ1Dntnzy/8KWutQ0cLOz3CcQFvrP1kFOhJJid0u4XDEoQIMPA==", null, false, "0ef13885-ccd2-4ec7-b09a-c45873bc71b6", false, "administrator" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "407d4e8f-e3c8-4fad-8507-b8f19d8b0632", "8e4fe81b-409e-439e-9aa6-cac6de934e12", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "88743d0f-120e-41ed-837d-ac2444e1ec18", "97d3a507-302b-4ff0-8143-95ed9b9413a1", "User", "USER" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "407d4e8f-e3c8-4fad-8507-b8f19d8b0632", "81274339-6069-49be-bb9f-6ce761fa26f4" });
        }
    }
}
