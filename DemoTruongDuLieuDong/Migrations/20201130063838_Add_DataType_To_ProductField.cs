using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoTruongDuLieuDong.Migrations
{
    public partial class Add_DataType_To_ProductField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KieuDuLieu",
                table: "ProductFields",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KieuDuLieu",
                table: "ProductFields");
        }
    }
}
