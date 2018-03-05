using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MouseHeatmap.Collector.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScreenUnits",
                columns: table => new
                {
                    ScreenUnitId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MouseEnteredCount = table.Column<int>(nullable: false),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenUnits", x => x.ScreenUnitId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScreenUnits");
        }
    }
}
