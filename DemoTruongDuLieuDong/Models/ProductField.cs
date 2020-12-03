using System;
using System.ComponentModel.DataAnnotations;

namespace DemoTruongDuLieuDong.Models
{
    public class ProductField
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        [Display(Name = "Tên trường")]
        public string TenTruong { get; set; }
        [Display(Name = "Tên hiển thị")]
        public string TenHienThi { get; set; }
        [Required]
        [Display(Name = "Kiểu dữ liệu")]
        public FieldDataTypeEnum KieuDuLieu { get; set; }
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }
        [Display(Name = "Thứ tự")]
        public int Index { get; set; }

        public ProductField()
        {
            Id = new Guid();
        }
    }
}
