using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "8f81d5ac-0db4-4d2f-a5a2-25aa3a70ac9d");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eea19bad-e2ee-46cd-88ef-911c49126da5", "89bb5390-5343-43f3-a713-c99b89025ac5" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "89bb5390-5343-43f3-a713-c99b89025ac5");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "eea19bad-e2ee-46cd-88ef-911c49126da5");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Category");

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Category",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ThumbnailFileId",
                table: "Category",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AttachmentFileId",
                table: "Article",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHighlight",
                table: "Article",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ThumbnailFileId",
                table: "Article",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Article",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ThumbnailFileId",
                table: "Category",
                column: "ThumbnailFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_AttachmentFileId",
                table: "Article",
                column: "AttachmentFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_ThumbnailFileId",
                table: "Article",
                column: "ThumbnailFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_UserId",
                table: "Article",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_ApplicationUser_UserId",
                table: "Article",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_SysFile_AttachmentFileId",
                table: "Article",
                column: "AttachmentFileId",
                principalTable: "SysFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_SysFile_ThumbnailFileId",
                table: "Article",
                column: "ThumbnailFileId",
                principalTable: "SysFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category",
                column: "ParentId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_SysFile_ThumbnailFileId",
                table: "Category",
                column: "ThumbnailFileId",
                principalTable: "SysFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_ApplicationUser_UserId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_SysFile_AttachmentFileId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_SysFile_ThumbnailFileId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_SysFile_ThumbnailFileId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_ParentId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_ThumbnailFileId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Article_AttachmentFileId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_ThumbnailFileId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_UserId",
                table: "Article");

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

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "AttachmentFileId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "IsHighlight",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Article");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "89bb5390-5343-43f3-a713-c99b89025ac5", 0, null, "d09592b7-c167-490f-8930-a79e297723ce", "admin@ymail.com", false, null, false, null, "ADMIN@YMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAECqZAwUWf9n8ljvMK5xms49p6cf23w2aQX8mTsiTu0fE+IaWI83hQbd1X8qUzuGYJw==", null, false, "8d0e5a24-f706-4717-a9f2-d8e9b2493468", false, "administrator" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eea19bad-e2ee-46cd-88ef-911c49126da5", "2c04726f-9386-4f7e-bfb3-50a80dcc6235", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8f81d5ac-0db4-4d2f-a5a2-25aa3a70ac9d", "ae585abb-fa77-4a9b-909d-9c970db8252d", "User", "USER" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "eea19bad-e2ee-46cd-88ef-911c49126da5", "89bb5390-5343-43f3-a713-c99b89025ac5" });
        }
    }
}
