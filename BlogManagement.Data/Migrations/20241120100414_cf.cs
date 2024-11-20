using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Data.Migrations
{
    public partial class cf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
