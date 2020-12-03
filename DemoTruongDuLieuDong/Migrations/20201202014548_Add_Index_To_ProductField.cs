using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoTruongDuLieuDong.Migrations
{
    public partial class Add_Index_To_ProductField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "ProductFields",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "ProductFields");
        }
    }
}
