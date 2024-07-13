using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmsApi.Migrations
{
    /// <inheritdoc />
    public partial class userType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userType",
                table: "Users");
        }
    }
}
