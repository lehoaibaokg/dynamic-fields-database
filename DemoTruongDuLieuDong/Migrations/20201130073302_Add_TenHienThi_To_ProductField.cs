using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoTruongDuLieuDong.Migrations
{
    public partial class Add_TenHienThi_To_ProductField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TenTruong",
                table: "ProductFields",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenHienThi",
                table: "ProductFields",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenHienThi",
                table: "ProductFields");

            migrationBuilder.AlterColumn<string>(
                name: "TenTruong",
                table: "ProductFields",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
