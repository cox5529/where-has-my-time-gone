using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhereHasMyTimeGone.API.Infrastructure.Persistence.Migrations
{
    public partial class AddProfileImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "UserProfile",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "UserProfile");
        }
    }
}
