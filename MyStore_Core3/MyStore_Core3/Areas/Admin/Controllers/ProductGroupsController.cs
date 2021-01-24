using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Administrator")]

    public class ProductGroupsController : Controller
    {
        private IProductGroupRepository _productGroupRepository;
        private IMapper _mapper;

        public ProductGroupsController(IProductGroupRepository productGroupRepository, IMapper mapper)
        {
            _productGroupRepository = productGroupRepository;
            _mapper = mapper;
        }

        // GET: Admin/ProductGroups
        public async Task<IActionResult> Index()
        {
            var productGroups = _productGroupRepository.GetAllEntities().ToList();
            var productGroupsModel = _mapper.Map<List<ProductGroup>, List<ProductGroupViewModel>>(productGroups);
            return View(productGroupsModel);
        }

        // GET: Admin/ProductGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup =  _productGroupRepository.GetEntityById(id.Value);
            var productGroupsModel = _mapper.Map<ProductGroupViewModel>(productGroup);

            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroupsModel);
        }

        // GET: Admin/ProductGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductGroupViewModel productGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var productGroupsModel = _mapper.Map<ProductGroup>(productGroupViewModel);
                _productGroupRepository.InsertEntity(productGroupsModel);
                _productGroupRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(productGroupViewModel);
        }

        // GET: Admin/ProductGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = _productGroupRepository.GetEntityById(id.Value);

            if (productGroup == null)
            {
                return NotFound();
            }
            var productGroupsModel = _mapper.Map<ProductGroupViewModel>(productGroup);
            return View(productGroupsModel);
        }

        // POST: Admin/ProductGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductGroupViewModel productGroupViewModel)
        {
            if (id != productGroupViewModel.ProductGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var productGroupsModel = _mapper.Map<ProductGroup>(productGroupViewModel);
                    _productGroupRepository.UpdateEntity(productGroupsModel);
                    _productGroupRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductGroupExists(productGroupViewModel.ProductGroupId))
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
            return View(productGroupViewModel);
        }

        // GET: Admin/ProductGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = _productGroupRepository.GetEntityById(id.Value);
            var productGroupsModel = _mapper.Map<ProductGroupViewModel>(productGroup);

            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroupsModel);
        }

        // POST: Admin/ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _productGroupRepository.DeleteEntity(id);
            _productGroupRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductGroupExists(int id)
        {
            return _productGroupRepository.EntityExists(id);
        }
    }
}
