using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoTruongDuLieuDong.Migrations
{
    public partial class Init_Project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    TenTruong = table.Column<string>(nullable: true),
                    NoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MaHangHoa = table.Column<string>(nullable: true),
                    TenHangHoa = table.Column<string>(nullable: true),
                    SoLuong = table.Column<string>(nullable: true),
                    GiaNhap = table.Column<double>(nullable: true),
                    GiaBan = table.Column<double>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFields");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
