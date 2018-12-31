using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleMusicStore.Data.Migrations
{
    public partial class fixaddressremoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Addresses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Addresses");
        }
    }
}
