using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quacker.API.Infrastructure.Persistence.Migrations
{
    public partial class AddFollowerCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "UserProfile",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Followers",
                table: "UserProfile",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedDisplayName",
                table: "UserProfile",
                type: "text",
                nullable: false,
                defaultValueSql: "gen_random_uuid()");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_NormalizedDisplayName",
                table: "UserProfile",
                column: "NormalizedDisplayName");

            migrationBuilder.Sql("UPDATE public.\"UserProfile\" SET \"NormalizedDisplayName\" = UPPER(\"DisplayName\")");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfile_NormalizedDisplayName",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Followers",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "NormalizedDisplayName",
                table: "UserProfile");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "UserProfile",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
