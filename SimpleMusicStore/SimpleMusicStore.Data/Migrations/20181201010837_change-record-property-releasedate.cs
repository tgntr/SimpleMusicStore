using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleMusicStore.Data.Migrations
{
    public partial class changerecordpropertyreleasedate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Records");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Records",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Records");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Records",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
