using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBikeSupplier.EF.Migrations
{
    public partial class addFieldTotalItemsInBasketTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalItems",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalItems",
                table: "Baskets");
        }
    }
}
