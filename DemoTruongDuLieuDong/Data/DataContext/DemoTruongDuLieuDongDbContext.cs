using DemoTruongDuLieuDong.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoTruongDuLieuDong.Data.DataContext
{
    public class DemoTruongDuLieuDongDbContext : DbContext
    {
        public DemoTruongDuLieuDongDbContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductField>(entity =>
            {
                entity.HasOne(c => c.Product)
                    .WithMany(b => b.ProductFields)
                    .HasForeignKey(c => c.ProductId);
            });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductField> ProductFields { get; set; }
    }
}
