using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore_Core3.DataLayer.Context;
using MyStore_Core3.DomainClasses;
using MyStore_Core3.Services.Repositories;

namespace MyStore_Core3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private IProductRepository _productRepository;
        private IProductGroupRepository _productGroupRepository;
        public ProductsController(IProductRepository productRepository, IProductGroupRepository productGroupRepository)
        {
            _productRepository = productRepository;
            _productGroupRepository = productGroupRepository;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            return View(_productRepository.GetAllEntities());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetEntityById(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["ProductGroupId"] = new SelectList(_productGroupRepository.GetAllEntities(), "ProductGroupId", "ProductGroupTitle");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductGroupId,ProductName,ProductPrice,ProductImage,ProductStock,ProductDescription,ProductStatus")] Product product, IFormFile imageUp)
        {
            if (ModelState.IsValid)
            {

                if (imageUp != null)
                {


                    product.ProductImage = Guid.NewGuid().ToString() + Path.GetExtension(imageUp.FileName);



                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/ProductImages", product.ProductImage
                    );

                    await using var stream = new FileStream(savePath, FileMode.Create);
                    {
                        await imageUp.CopyToAsync(stream);
                    }



                }

                _productRepository.InsertEntity(product);
                _productRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductGroupId"] = new SelectList(_productGroupRepository.GetAllEntities(), "ProductGroupId", "ProductGroupTitle", product.ProductGroupId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetEntityById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductGroupId"] = new SelectList(_productGroupRepository.GetAllEntities(), "ProductGroupId", "ProductGroupTitle", product.ProductGroupId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductGroupId,ProductName,ProductPrice,ProductImage,ProductStock,ProductDescription,ProductStatus")] Product product, IFormFile ImageUp)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageUp != null)
                    {
                        if(product.ProductImage == null)
                        {
                            product.ProductImage = Guid.NewGuid().ToString() + Path.GetExtension(ImageUp.FileName);

                        }

                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/ProductImages", product.ProductImage
                        );

                        await using (var stream = new FileStream(savePath, FileMode.Create))//ذخیره ی عکس
                        {
                            await ImageUp.CopyToAsync(stream);
                        }


                    }
                    _productRepository.UpdateEntity(product);
                    _productRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["ProductGroupId"] = new SelectList(_productGroupRepository.GetAllEntities(), "ProductGroupId", "ProductGroupTitle", product.ProductGroupId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetEntityById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
        _productRepository.DeleteEntity(id);
        _productRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _productRepository.EntityExists(id);
        }
    }
}
