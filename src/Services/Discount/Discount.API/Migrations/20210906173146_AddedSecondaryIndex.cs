using Microsoft.EntityFrameworkCore.Migrations;

namespace Discount.API.Migrations
{
    public partial class AddedSecondaryIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Coupon",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_ProductName_From",
                table: "Coupon",
                columns: new[] { "ProductName", "From" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coupon_ProductName_From",
                table: "Coupon");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Coupon",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
