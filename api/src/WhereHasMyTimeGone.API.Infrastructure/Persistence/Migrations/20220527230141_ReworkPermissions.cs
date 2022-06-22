using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quacker.API.Infrastructure.Persistence.Migrations
{
    public partial class ReworkPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "UserProfileFlock");

            migrationBuilder.AddColumn<bool>(
                name: "Owner",
                table: "UserProfileFlock",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Post",
                table: "UserProfileFlock",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "UserProfileFlock",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "UserProfileFlock");

            migrationBuilder.DropColumn(
                name: "Post",
                table: "UserProfileFlock");

            migrationBuilder.DropColumn(
                name: "Read",
                table: "UserProfileFlock");

            migrationBuilder.AddColumn<long>(
                name: "Permissions",
                table: "UserProfileFlock",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
