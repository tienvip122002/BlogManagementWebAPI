using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Data.Migrations
{
    public partial class createaboutusandsysfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "208eb2ba-d937-447d-a9ef-096e0c3ed1be");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5d8202d3-ed0a-4041-bcf0-dfe068ebae85", "d2a3b0f0-7c57-42e0-93e5-575f612d8f71" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "d2a3b0f0-7c57-42e0-93e5-575f612d8f71");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "5d8202d3-ed0a-4041-bcf0-dfe068ebae85");

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3b784613-8a09-46ba-9943-756284b3e703", 0, null, "2ca2a671-0150-4f8b-8087-3bcbd7567ddc", "admin@ymail.com", false, null, false, null, "ADMIN@YMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEN6na8Xuc7m4x/Of/QoBCyuKUlOBkF5o1LQ9rv7gDw7KxFSi2TYffnwNOQkR7ooMGw==", null, false, "0a15744a-fad0-4727-b4d0-87329bcc8cfd", false, "administrator" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9462e7e7-7236-4e85-939d-ffa03ab4d74d", "8b1090ec-139a-4fa2-b47c-3b05cdcf74f6", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "50ac45eb-001b-42a5-9897-611663bbf036", "922c9d34-fbbd-46c6-80e4-cd336d0be92b", "User", "USER" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9462e7e7-7236-4e85-939d-ffa03ab4d74d", "3b784613-8a09-46ba-9943-756284b3e703" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "50ac45eb-001b-42a5-9897-611663bbf036");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9462e7e7-7236-4e85-939d-ffa03ab4d74d", "3b784613-8a09-46ba-9943-756284b3e703" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "3b784613-8a09-46ba-9943-756284b3e703");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "9462e7e7-7236-4e85-939d-ffa03ab4d74d");

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d2a3b0f0-7c57-42e0-93e5-575f612d8f71", 0, null, "be4a2ef8-88fe-4ffb-acbb-341841a3e299", "admin@ymail.com", false, null, false, null, "ADMIN@YMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEPseGA0XImyBGgJnYc2K5xMICkE51k6HNZhY0aEStbIEJ7ieadnLFW8FwUFV9zswHQ==", null, false, "27244dfa-073b-45e3-9671-43b2db563605", false, "administrator" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5d8202d3-ed0a-4041-bcf0-dfe068ebae85", "4844f7d3-e4b9-4d8e-93d0-20f9a7a2f2bf", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "208eb2ba-d937-447d-a9ef-096e0c3ed1be", "21763900-1016-45cc-801f-0def57163c31", "User", "USER" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5d8202d3-ed0a-4041-bcf0-dfe068ebae85", "d2a3b0f0-7c57-42e0-93e5-575f612d8f71" });
        }
    }
}
