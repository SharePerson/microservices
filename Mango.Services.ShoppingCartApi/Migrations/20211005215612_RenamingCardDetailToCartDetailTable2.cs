using Microsoft.EntityFrameworkCore.Migrations;

namespace Mango.Services.ShoppingCartApi.Migrations
{
    public partial class RenamingCardDetailToCartDetailTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardDetail_CartHeader_CartHeaderId",
                table: "CardDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_CardDetail_Product_ProductId",
                table: "CardDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardDetail",
                table: "CardDetail");

            migrationBuilder.RenameTable(
                name: "CardDetail",
                newName: "CartDetail");

            migrationBuilder.RenameIndex(
                name: "IX_CardDetail_ProductId",
                table: "CartDetail",
                newName: "IX_CartDetail_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CardDetail_CartHeaderId",
                table: "CartDetail",
                newName: "IX_CartDetail_CartHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartDetail",
                table: "CartDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_CartHeader_CartHeaderId",
                table: "CartDetail",
                column: "CartHeaderId",
                principalTable: "CartHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_Product_ProductId",
                table: "CartDetail",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_CartHeader_CartHeaderId",
                table: "CartDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_Product_ProductId",
                table: "CartDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartDetail",
                table: "CartDetail");

            migrationBuilder.RenameTable(
                name: "CartDetail",
                newName: "CardDetail");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetail_ProductId",
                table: "CardDetail",
                newName: "IX_CardDetail_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetail_CartHeaderId",
                table: "CardDetail",
                newName: "IX_CardDetail_CartHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardDetail",
                table: "CardDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CardDetail_CartHeader_CartHeaderId",
                table: "CardDetail",
                column: "CartHeaderId",
                principalTable: "CartHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardDetail_Product_ProductId",
                table: "CardDetail",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
