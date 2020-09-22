using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore_Core3.DataLayer.Context;
using MyStore_Core3.DomainClasses;
using MyStore_Core3.Services.Repositories;
using MyStore_Core3.ViewModel;

namespace MyStore_Core3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private IProductRepository _productRepository;
        private IProductGroupRepository _productGroupRepository;
        private IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IProductGroupRepository productGroupRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productGroupRepository = productGroupRepository;
            _mapper = mapper;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var product = _productRepository.GetAllEntities().ToList();
            var model = _mapper.Map<List<Product>, List<DetailsProductViewModel>>(product);
            return View(model);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetEntityById(id.Value);
            var productModel = _mapper.Map<DetailsProductViewModel>(product);

            if (product == null)
            {
                return NotFound();
            }

            return View(productModel);
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
        public async Task<IActionResult> Create(CreateProductViewModel createProductViewModel, IFormFile imageUp)
        {
            if (ModelState.IsValid)
            {

                if (imageUp != null)
                {


                    createProductViewModel.ProductImage = Guid.NewGuid().ToString() + Path.GetExtension(imageUp.FileName);



                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/ProductImages", createProductViewModel.ProductImage
                    );

                    await using var stream = new FileStream(savePath, FileMode.Create);
                    {
                        await imageUp.CopyToAsync(stream);
                    }



                }

                var product = _mapper.Map<Product>(createProductViewModel);
                _productRepository.InsertEntity(product);
                _productRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductGroupId"] = new SelectList(_productGroupRepository.GetAllEntities(), "ProductGroupId", "ProductGroupTitle", createProductViewModel.ProductGroupId);
            return View(createProductViewModel);
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

            var productModel = _mapper.Map<DetailsProductViewModel>(product);

            ViewData["ProductGroupId"] = new SelectList(_productGroupRepository.GetAllEntities(), "ProductGroupId", "ProductGroupTitle", product.ProductGroupId);
            return View(productModel);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,DetailsProductViewModel detailsProductViewModel, IFormFile ImageUp)
        {
            if (id != detailsProductViewModel.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageUp != null)
                    {
                        if(detailsProductViewModel.ProductImage == null)
                        {
                            detailsProductViewModel.ProductImage = Guid.NewGuid().ToString() + Path.GetExtension(ImageUp.FileName);

                        }

                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/ProductImages", detailsProductViewModel.ProductImage
                        );

                        await using (var stream = new FileStream(savePath, FileMode.Create))//ذخیره ی عکس
                        {
                            await ImageUp.CopyToAsync(stream);
                        }


                    }
                    var productModel = _mapper.Map<Product>(detailsProductViewModel);

                    _productRepository.UpdateEntity(productModel);
                    _productRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(detailsProductViewModel.ProductId))
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
            ViewData["ProductGroupId"] = new SelectList(_productGroupRepository.GetAllEntities(), "ProductGroupId", "ProductGroupTitle", detailsProductViewModel.ProductGroupId);
            return View(detailsProductViewModel);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetEntityById(id.Value);
            var productModel = _mapper.Map<DetailsProductViewModel>(product);
            if (product == null)
            {
                return NotFound();
            }

            return View(productModel);
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
