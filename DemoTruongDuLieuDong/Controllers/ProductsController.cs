using DemoTruongDuLieuDong.Data.DataContext;
using DemoTruongDuLieuDong.Models;
using DemoTruongDuLieuDong.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DemoTruongDuLieuDong.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DemoTruongDuLieuDongDbContext _context;

        public ProductsController(DemoTruongDuLieuDongDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(x => x.ProductFields)
                .ToListAsync();

            return View(products);
        }

        public IActionResult Create()
        {
            var productFields = _context.ProductFields
                .DistinctBy(x => x.TenTruong)
                .ToList();

            var product = new Product
            {
                ProductFields = productFields.Select(x => { x.NoiDung = string.Empty; return x; }).ToList()
            };

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product == null)
                {
                    return NotFound();
                }

                product.Id = Guid.NewGuid();

                foreach (var item in product.ProductFields)
                {
                    item.Id = Guid.NewGuid();
                    item.ProductId = product.Id;
                }

                _context.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(x => x.ProductFields)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product == null)
                    {
                        return NotFound();
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExist(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(x => x.ProductFields)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _context.Products
                .Include(x => x.ProductFields)
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExist(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
