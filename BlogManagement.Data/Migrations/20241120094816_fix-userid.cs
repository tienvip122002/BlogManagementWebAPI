using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Data.Migrations
{
    public partial class fixuserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "fa5c5a86-ce64-4097-9c16-490be057c27a");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "846f4f3a-0241-4dc0-9e8d-6d0e92ae3f7f", "1545204e-ed3c-4872-baa5-ef1b53b7b7b1" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "1545204e-ed3c-4872-baa5-ef1b53b7b7b1");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "846f4f3a-0241-4dc0-9e8d-6d0e92ae3f7f");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "1545204e-ed3c-4872-baa5-ef1b53b7b7b1", 0, null, "376d0816-0ada-4544-a2de-01236d375941", "admin@ymail.com", false, null, false, null, "ADMIN@YMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEBXmq0Z2uiFHi3KLHRrBll2IJsAtcjbUpmWyHG2kKv2kLJHVBpZYE6W5ewwJZ6X+VA==", null, false, "420907a8-cde0-4bc2-aedc-e24f68b0c378", false, "administrator" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "846f4f3a-0241-4dc0-9e8d-6d0e92ae3f7f", "dd4b3072-e0ae-42c9-9dfa-94b9e823cff2", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fa5c5a86-ce64-4097-9c16-490be057c27a", "1fbdf15e-2ed2-4e51-8a8a-219a4670255b", "User", "USER" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "846f4f3a-0241-4dc0-9e8d-6d0e92ae3f7f", "1545204e-ed3c-4872-baa5-ef1b53b7b7b1" });
        }
    }
}
