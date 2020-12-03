using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoTruongDuLieuDong.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Display(Name = "Mã hàng hóa")]
        public string MaHangHoa { get; set; }
        [Display(Name = "Tên hàng hóa")]
        public string TenHangHoa { get; set; }
        [Display(Name = "SL")]
        public string SoLuong { get; set; }
        [Display(Name = "Giá nhập")]
        public double? GiaNhap { get; set; }
        [Display(Name = "Giá bán")]
        public double? GiaBan { get; set; }
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }
        public List<ProductField> ProductFields { get; set; }

        public Product()
        {
            Id = new Guid();
        }
    }
}
