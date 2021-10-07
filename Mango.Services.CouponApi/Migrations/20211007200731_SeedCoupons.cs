using Microsoft.EntityFrameworkCore.Migrations;

namespace Mango.Services.CouponApi.Migrations
{
    public partial class SeedCoupons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DiscountAmount",
                table: "Coupon",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "CouponCode", "DiscountAmount" },
                values: new object[] { 1, "10OFF", 10 });

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "CouponCode", "DiscountAmount" },
                values: new object[] { 2, "20OFF", 20 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "DiscountAmount",
                table: "Coupon",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
