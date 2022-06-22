using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quacker.API.Infrastructure.Persistence.Migrations
{
    public partial class AddQuacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM public.\"UserProfile\"");
            
            migrationBuilder.CreateTable(
                name: "Flock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flock_UserProfile_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quack",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FlockId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quack_Flock_FlockId",
                        column: x => x.FlockId,
                        principalTable: "Flock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quack_UserProfile_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flock_OwnerId",
                table: "Flock",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Quack_FlockId",
                table: "Quack",
                column: "FlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Quack_OwnerId",
                table: "Quack",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quack");

            migrationBuilder.DropTable(
                name: "Flock");
        }
    }
}
