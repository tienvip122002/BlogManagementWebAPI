using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Data.Migrations
{
    public partial class fixuserid2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "40f65e97-ef98-4f4f-95dc-eba2d7100e95", 0, null, "ca7314a7-77c6-4e1c-97a7-ee5a28d97f46", "admin@ymail.com", false, null, false, null, "ADMIN@YMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEIljv+n7dFzKOXQR1VXIk+U5bmMJ0SYGTmGKjbo+cRwAK3wpKU/O9krh7NIzr2A32w==", null, false, "cc320d06-a582-4883-8e35-4b384241c36e", false, "administrator" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "179d2482-5d1d-48be-848d-3931435e35ef", "e4015935-815b-4655-85a7-57ea78e3cb87", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eae06343-567d-4ff4-99fe-33adea63d262", "f79c3af3-3992-4212-a6ac-c26da2723571", "User", "USER" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "179d2482-5d1d-48be-848d-3931435e35ef", "40f65e97-ef98-4f4f-95dc-eba2d7100e95" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "eae06343-567d-4ff4-99fe-33adea63d262");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "179d2482-5d1d-48be-848d-3931435e35ef", "40f65e97-ef98-4f4f-95dc-eba2d7100e95" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "40f65e97-ef98-4f4f-95dc-eba2d7100e95");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "179d2482-5d1d-48be-848d-3931435e35ef");

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
    }
}
