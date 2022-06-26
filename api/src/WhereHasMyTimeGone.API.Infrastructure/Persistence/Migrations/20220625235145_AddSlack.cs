using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhereHasMyTimeGone.API.Infrastructure.Persistence.Migrations
{
    public partial class AddSlack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SlackId",
                table: "UserProfile",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Huddle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huddle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Huddle_UserProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HuddleGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuddleGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HuddleGroup_UserProfile_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HuddleLink",
                columns: table => new
                {
                    HuddleId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuddleLink", x => new { x.GroupId, x.HuddleId });
                    table.ForeignKey(
                        name: "FK_HuddleLink_Huddle_HuddleId",
                        column: x => x.HuddleId,
                        principalTable: "Huddle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HuddleLink_HuddleGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "HuddleGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_SlackId",
                table: "UserProfile",
                column: "SlackId");

            migrationBuilder.CreateIndex(
                name: "IX_Huddle_UserProfileId",
                table: "Huddle",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_HuddleGroup_OwnerId",
                table: "HuddleGroup",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_HuddleLink_HuddleId",
                table: "HuddleLink",
                column: "HuddleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HuddleLink");

            migrationBuilder.DropTable(
                name: "Huddle");

            migrationBuilder.DropTable(
                name: "HuddleGroup");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_SlackId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "SlackId",
                table: "UserProfile");
        }
    }
}
