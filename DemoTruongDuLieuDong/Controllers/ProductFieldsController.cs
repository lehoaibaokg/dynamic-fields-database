using DemoTruongDuLieuDong.Data.DataContext;
using DemoTruongDuLieuDong.Models;
using DemoTruongDuLieuDong.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoTruongDuLieuDong.Controllers
{
    public class ProductFieldsController : Controller
    {
        private readonly DemoTruongDuLieuDongDbContext _context;

        public ProductFieldsController(DemoTruongDuLieuDongDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productFields = _context.ProductFields
                .OrderBy(x => x.Index)
                .DistinctBy(x => x.TenTruong)
                .ToList();

            return View(productFields);
        }

        public IActionResult Create()
        {
            var dataTypes = Enum.GetValues(typeof(FieldDataTypeEnum));
            var listKieuDuLieu = new Dictionary<int, string>();
            foreach(var item in dataTypes)
            {
                listKieuDuLieu.Add((int)item, item.ToString());
            }

            var productFields = _context.ProductFields
                .OrderBy(x => x.Index)
                .DistinctBy(x => x.TenTruong)
                .ToList();

            var maxIndex = (productFields.Count > 0) ? productFields.Max(f => f.Index) : 0;

            var listIndex = new Dictionary<float, string>();
            for (int i = 1; i <= maxIndex; i++)
            {
                listIndex.Add(i, "Thứ " + i);
            }

            var newIndex = maxIndex + 1;

            listIndex.Add(newIndex, "Thứ " + newIndex);

            ViewData["KieuDuLieu"] = new SelectList(listKieuDuLieu, "Key", "Value", listKieuDuLieu.First());
            ViewData["Index"] = new SelectList(listIndex, "Key", "Value", newIndex);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductField productField)
        {
            var dataTypes = Enum.GetValues(typeof(FieldDataTypeEnum));
            var listKieuDuLieu = new Dictionary<int, string>();
            foreach (var item in dataTypes)
            {
                listKieuDuLieu.Add((int)item, item.ToString());
            }

            var productFields = _context.ProductFields
                .OrderBy(x => x.Index)
                .DistinctBy(x => x.TenTruong)
                .ToList();

            var maxIndex = (productFields.Count > 0) ? productFields.Max(f => f.Index) : 0;

            var listIndex = new Dictionary<float, string>();

            for (int i = 1; i <= maxIndex; i++)
            {
                listIndex.Add(i, "Thứ " + i);
            }

            listIndex.Add(maxIndex + 1, "Thứ " + maxIndex + 1);

            ViewData["KieuDuLieu"] = new SelectList(listKieuDuLieu, "Key", "Value", listKieuDuLieu.First());
            ViewData["Index"] = new SelectList(listIndex, "Key", "Value", productField.Index);

            if (!string.IsNullOrEmpty(productField.TenTruong))
            {
                if (productField.TenTruong.Trim().Contains(" "))
                {
                    ModelState.AddModelError(string.Empty, "Tên trường có chứa khoảng trắng, vui lòng chọn tên khác!");
                    return View(productField);
                }

                if (productFields.Any(x => x.TenTruong.Trim().Equals(productField.TenTruong.Trim()))) {
                    ModelState.AddModelError(string.Empty, "Tên trường đã tồn tại rồi, vui lòng chọn tên khác!");
                    return View(productField);
                }

                foreach (var item in productFields)
                {
                    if (item.Index >= productField.Index)
                    {
                        var nearItems = _context.ProductFields
                            .Where(x => x.Index == item.Index && x.Id != item.Id)
                            .ToList();

                        foreach (var nearItem in nearItems)
                        {
                            nearItem.Index += 1;
                        }

                        _context.ProductFields.UpdateRange(nearItems);

                        item.Index += 1;
                    }
                }

                _context.ProductFields.UpdateRange(productFields);

                if (string.IsNullOrEmpty(productField.TenHienThi))
                    productField.TenHienThi = productField.TenTruong;

                var products = await _context.Products.ToListAsync();
                foreach (var item in products)
                {
                    var productFieldInsert = new ProductField
                    {
                        Id = Guid.NewGuid(),
                        ProductId = item.Id,
                        TenTruong = productField.TenTruong,
                        TenHienThi = productField.TenHienThi,
                        KieuDuLieu = productField.KieuDuLieu,
                        Index = productField.Index
                    };

                    _context.ProductFields.Add(productFieldInsert);
                }
                
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(productField);
        }

        public IActionResult Edit(string tenTruongCu)
        {
            var dataTypes = Enum.GetValues(typeof(FieldDataTypeEnum));
            var listKieuDuLieu = new Dictionary<int, string>();
            foreach (var item in dataTypes)
            {
                listKieuDuLieu.Add((int)item, item.ToString());
            }

            var productField = _context.ProductFields
                .DistinctBy(x => x.TenTruong)
                .FirstOrDefault(x => x.TenTruong.Trim().Equals(tenTruongCu.Trim()));

            var productFields = _context.ProductFields
                .OrderBy(x => x.Index)
                .DistinctBy(x => x.TenTruong)
                .ToList();

            var listIndex = new Dictionary<float, string>();

            foreach (var item in productFields)
            {
                var indexName = "Thứ " + item.Index;

                listIndex.Add(item.Index, indexName);
            }

            ViewData["TenTruongCu"] = tenTruongCu;
            ViewData["KieuDuLieu"] = new SelectList(listKieuDuLieu, "Key", "Value", listKieuDuLieu.First());
            ViewData["Index"] = new SelectList(listIndex, "Key", "Value", productField.Index);

            return View(productField);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string tenTruongCu, ProductField productField)
        {
            // load lại các select list ngoài view khi submit faild
            var dataTypes = Enum.GetValues(typeof(FieldDataTypeEnum));
            var listKieuDuLieu = new Dictionary<int, string>();
            foreach (var item in dataTypes)
            {
                listKieuDuLieu.Add((int)item, item.ToString());
            }

            var productFieldsUnique = _context.ProductFields
                .OrderBy(x => x.Index)
                .DistinctBy(x => x.TenTruong)
                .ToList();

            var listIndex = new Dictionary<float, string>();

            foreach (var item in productFieldsUnique)
            {
                var indexName = "Thứ " + item.Index;

                listIndex.Add(item.Index, indexName);
            }

            ViewData["TenTruongCu"] = tenTruongCu;
            ViewData["KieuDuLieu"] = new SelectList(listKieuDuLieu, "Key", "Value", listKieuDuLieu.First());
            ViewData["Index"] = new SelectList(listIndex, "Key", "Value", productField.Index);

            if (!string.IsNullOrEmpty(productField.TenTruong))
            {
                if (productField.TenTruong.Trim().Contains(" "))
                {
                    ModelState.AddModelError(string.Empty, "Tên trường có chứa khoảng trắng, vui lòng chọn tên khác!");
                    return View(productField);
                }

                if (!productField.TenTruong.Trim().Equals(tenTruongCu.Trim()) && productFieldsUnique.Any(x => x.TenTruong.Trim().Equals(productField.TenTruong.Trim())))
                {
                    ModelState.AddModelError(string.Empty, "Tên trường đã tồn tại rồi, vui lòng chọn tên khác!");
                    return View(productField);
                }

                // cập nhật index các records
                var sourceProductFields = await _context.ProductFields
                    .Where(x => x.TenTruong.Trim().Equals(tenTruongCu.Trim()))
                    .ToListAsync();

                var sourceProductField = sourceProductFields.FirstOrDefault();

                _context.ProductFields.RemoveRange(sourceProductFields);

                var productFieldsIndex = _context.ProductFields
                   .OrderBy(x => x.Index)
                   .DistinctBy(x => x.TenTruong)
                   .ToList();

                foreach (var item in productFieldsIndex)
                {
                    var nearItems = _context.ProductFields
                        .Where(x => x.Index == item.Index && x.Id != item.Id)
                        .ToList();

                    if (sourceProductField.Index < productField.Index)
                    {
                        if (item.Index > sourceProductField.Index && item.Index <= productField.Index)
                        {
                            foreach (var nearItem in nearItems)
                            {
                                nearItem.Index -= 1;
                            }

                            item.Index -= 1;
                        }
                    }
                    else if (sourceProductField.Index > productField.Index)
                    {
                        if (item.Index < sourceProductField.Index && item.Index >= productField.Index)
                        {
                            foreach (var nearItem in nearItems)
                            {
                                nearItem.Index += 1;
                            }

                            item.Index += 1;
                        }
                    }

                    _context.ProductFields.UpdateRange(nearItems);
                }

                _context.ProductFields.UpdateRange(productFieldsIndex);

                // cập nhật các records với tên trường hiện tại
                foreach (var item in sourceProductFields)
                {
                    item.TenTruong = productField.TenTruong;
                    item.TenHienThi = productField.TenHienThi;
                    item.KieuDuLieu = productField.KieuDuLieu;
                    item.Index = productField.Index;
                }

                _context.ProductFields.UpdateRange(sourceProductFields);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(productField);
        }

        public IActionResult Delete(string tenTruong)
        {
            var productField = _context.ProductFields
                .DistinctBy(x => x.TenTruong)
                .FirstOrDefault(x => x.TenTruong.Trim().Equals(tenTruong.Trim()));

            return View(productField);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string tenTruong)
        {
            var productFields = await _context.ProductFields
                .Where(x => x.TenTruong.Trim().Equals(tenTruong.Trim()))
                .ToListAsync();

            var productField = productFields.FirstOrDefault();

            _context.ProductFields.RemoveRange(productFields);
            await _context.SaveChangesAsync();

            var productFieldsIndex = _context.ProductFields
                .OrderBy(x => x.Index)
                .DistinctBy(x => x.TenTruong)
                .ToList();

            foreach (var item in productFieldsIndex)
            {
                if (item.Index > productField.Index)
                {
                    var nearItems = _context.ProductFields
                        .Where(x => x.Index == item.Index && x.Id != item.Id)
                        .ToList();

                    foreach (var nearItem in nearItems)
                    {
                        nearItem.Index -= 1;
                    }

                    item.Index -= 1;

                    _context.ProductFields.UpdateRange(nearItems);
                }
            }

            _context.ProductFields.UpdateRange(productFieldsIndex);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProductFieldExist(Guid id)
        {
            return _context.ProductFields.Any(e => e.Id == id);
        }
    }
}
