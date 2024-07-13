using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmsApi.Migrations
{
    /// <inheritdoc />
    public partial class user_Admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "CreatedBy", "HashPassword", "Password", "SaltPassword", "UpdatedAt", "UpdatedBy", "UserName", "userType" },
                values: new object[] { 1, new DateTime(2024, 7, 13, 11, 13, 44, 219, DateTimeKind.Local).AddTicks(8119), 1, "", "Cms@P@$$w0rd", "", null, null, "adminDanat", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
