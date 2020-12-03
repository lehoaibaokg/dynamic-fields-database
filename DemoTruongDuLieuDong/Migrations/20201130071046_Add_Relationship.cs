using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoTruongDuLieuDong.Migrations
{
    public partial class Add_Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductFields_ProductId",
                table: "ProductFields",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFields_Products_ProductId",
                table: "ProductFields",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFields_Products_ProductId",
                table: "ProductFields");

            migrationBuilder.DropIndex(
                name: "IX_ProductFields_ProductId",
                table: "ProductFields");
        }
    }
}
