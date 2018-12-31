using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleMusicStore.Data.Migrations
{
    public partial class fixthecascades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_UserId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistUsers_Artists_ArtistId",
                table: "ArtistUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistUsers_AspNetUsers_UserId",
                table: "ArtistUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_LabelUsers_Labels_LabelId",
                table: "LabelUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_LabelUsers_AspNetUsers_UserId",
                table: "LabelUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordOrders_Orders_OrderId",
                table: "RecordOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordOrders_Records_RecordId",
                table: "RecordOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Artists_ArtistId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Labels_LabelId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordUsers_Records_RecordId",
                table: "RecordUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordUsers_AspNetUsers_UserId",
                table: "RecordUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Records_RecordId",
                table: "Tracks");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Records_RecordId",
                table: "Videos");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_UserId",
                table: "Addresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistUsers_Artists_ArtistId",
                table: "ArtistUsers",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistUsers_AspNetUsers_UserId",
                table: "ArtistUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LabelUsers_Labels_LabelId",
                table: "LabelUsers",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LabelUsers_AspNetUsers_UserId",
                table: "LabelUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordOrders_Orders_OrderId",
                table: "RecordOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordOrders_Records_RecordId",
                table: "RecordOrders",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Artists_ArtistId",
                table: "Records",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Labels_LabelId",
                table: "Records",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordUsers_Records_RecordId",
                table: "RecordUsers",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordUsers_AspNetUsers_UserId",
                table: "RecordUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Records_RecordId",
                table: "Tracks",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Records_RecordId",
                table: "Videos",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_UserId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistUsers_Artists_ArtistId",
                table: "ArtistUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistUsers_AspNetUsers_UserId",
                table: "ArtistUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_LabelUsers_Labels_LabelId",
                table: "LabelUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_LabelUsers_AspNetUsers_UserId",
                table: "LabelUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordOrders_Orders_OrderId",
                table: "RecordOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordOrders_Records_RecordId",
                table: "RecordOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Artists_ArtistId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Labels_LabelId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordUsers_Records_RecordId",
                table: "RecordUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordUsers_AspNetUsers_UserId",
                table: "RecordUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Records_RecordId",
                table: "Tracks");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Records_RecordId",
                table: "Videos");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_UserId",
                table: "Addresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistUsers_Artists_ArtistId",
                table: "ArtistUsers",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistUsers_AspNetUsers_UserId",
                table: "ArtistUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LabelUsers_Labels_LabelId",
                table: "LabelUsers",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LabelUsers_AspNetUsers_UserId",
                table: "LabelUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordOrders_Orders_OrderId",
                table: "RecordOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordOrders_Records_RecordId",
                table: "RecordOrders",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Artists_ArtistId",
                table: "Records",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Labels_LabelId",
                table: "Records",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordUsers_Records_RecordId",
                table: "RecordUsers",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordUsers_AspNetUsers_UserId",
                table: "RecordUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Records_RecordId",
                table: "Tracks",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Records_RecordId",
                table: "Videos",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
