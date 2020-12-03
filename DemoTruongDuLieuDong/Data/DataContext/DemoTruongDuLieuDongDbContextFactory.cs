using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DemoTruongDuLieuDong.Data.DataContext
{
    public class DemoTruongDuLieuDongDbContextFactory : IDesignTimeDbContextFactory<DemoTruongDuLieuDongDbContext>
    {
        public DemoTruongDuLieuDongDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionStrings = configuration.GetConnectionString("DemoTruongDuLieuDongConnectionStrings");
            
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(connectionStrings);
            
            return new DemoTruongDuLieuDongDbContext(optionsBuilder.Options);
        }
    }
}