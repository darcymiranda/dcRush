using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheRush.WebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerRushingStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Player = table.Column<string>(nullable: true),
                    Team = table.Column<string>(nullable: true),
                    Pos = table.Column<string>(nullable: true),
                    Att = table.Column<int>(nullable: false),
                    AttG = table.Column<double>(nullable: false),
                    Yds = table.Column<int>(nullable: false),
                    Avg = table.Column<double>(nullable: false),
                    YdsG = table.Column<double>(nullable: false),
                    Td = table.Column<int>(nullable: false),
                    Lng = table.Column<int>(nullable: false),
                    First = table.Column<int>(nullable: false),
                    FirstPercentage = table.Column<double>(nullable: false),
                    TwentyPlus = table.Column<int>(nullable: false),
                    FortyPlus = table.Column<int>(nullable: false),
                    Fum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRushingStats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerRushingStats");
        }
    }
}
