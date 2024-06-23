using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBikeSupplier.EF.Migrations
{
    public partial class FixInWishListItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_Baskets_ProductId",
                table: "WishListItems");

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_Bikes_ProductId",
                table: "WishListItems",
                column: "ProductId",
                principalTable: "Bikes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_Bikes_ProductId",
                table: "WishListItems");

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_Baskets_ProductId",
                table: "WishListItems",
                column: "ProductId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
