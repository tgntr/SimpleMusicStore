using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleMusicStore.Data.Migrations
{
    public partial class changerecordremoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Records",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Records");
        }
    }
}
