using Microsoft.EntityFrameworkCore.Migrations;

namespace Mango.Services.OrderApi.Migrations
{
    public partial class AddingProductIdToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderDetail");
        }
    }
}
