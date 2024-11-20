using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Data.Migrations
{
    public partial class fixuserid3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "a2e1840b-56e1-43ad-9d01-ebfbdaa76a37", 0, null, "cd933376-7bfe-4143-b6ea-8950e59306bb", "admin@ymail.com", false, null, false, null, "ADMIN@YMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEMMJu4QwNBu43heFftiaFAH4DLrFc+QVr5Pxv2+WgmSXm0KFx6WKKdO2HKvXS3RVSQ==", null, false, "cafadb23-4cdb-403a-977b-37a449d26cd2", false, "administrator" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7150e6c8-158a-4e74-a031-944299f2fdb9", "1ccd2674-bad8-4522-902f-66b7c561cd46", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8bbace8b-4230-4481-a83b-e6b8034d73f9", "8c73d528-55c0-419a-b2e7-3f7bcef1c92a", "User", "USER" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7150e6c8-158a-4e74-a031-944299f2fdb9", "a2e1840b-56e1-43ad-9d01-ebfbdaa76a37" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "8bbace8b-4230-4481-a83b-e6b8034d73f9");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7150e6c8-158a-4e74-a031-944299f2fdb9", "a2e1840b-56e1-43ad-9d01-ebfbdaa76a37" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "a2e1840b-56e1-43ad-9d01-ebfbdaa76a37");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "7150e6c8-158a-4e74-a031-944299f2fdb9");

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
    }
}
