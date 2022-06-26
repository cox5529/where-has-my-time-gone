using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhereHasMyTimeGone.API.Infrastructure.Persistence.Migrations
{
    public partial class AddEmailIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfile_SlackId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "SlackId",
                table: "UserProfile");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_Email",
                table: "UserProfile",
                column: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfile_Email",
                table: "UserProfile");

            migrationBuilder.AddColumn<string>(
                name: "SlackId",
                table: "UserProfile",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_SlackId",
                table: "UserProfile",
                column: "SlackId");
        }
    }
}
