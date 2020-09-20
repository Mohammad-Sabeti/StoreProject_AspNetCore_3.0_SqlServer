using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore_Core3.DataLayer.Context;
using MyStore_Core3.DomainClasses;
using MyStore_Core3.Services.Repositories;

namespace MyStore_Core3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderAppsController : Controller
    {
        private IOrderAppRepository _orderAppRepository;
        private IProductRepository _productRepository;

        private UserManager<IdentityUser> _userManager;

        public OrderAppsController(IOrderAppRepository orderAppRepository, IProductRepository productRepository, UserManager<IdentityUser> userManager)
        {
            _orderAppRepository = orderAppRepository;
            _productRepository = productRepository;
            _userManager = userManager;
        }
        // GET: Admin/OrderApps
        public async Task<IActionResult> Index()
        {
            // ViewData["CustomerId"] = new SelectList(_userManager.Users, "CustomerId", "UserName", orderApp.CustomerId);
            ViewData["ProductId"] = new SelectList(_productRepository.GetAllEntities(), "ProductId", "ProductName");
            return View(_orderAppRepository.GetAllEntities());
        }

        // GET: Admin/OrderApps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderApp = _orderAppRepository.GetEntityById(id.Value);

            return View(orderApp);
        }

        // GET: Admin/OrderApps/Create
        public IActionResult Create()
        {

            //TODO:DO IT SOON
            ViewData["CustomerId"] = new SelectList(_userManager.Users, "Id", "UserName");
            ViewData["ProductId"] = new SelectList(_productRepository.GetAllEntities(), "ProductId", "ProductName");
            return View();
        }

        // POST: Admin/OrderApps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderAppId,CustomerId,ProductId,SellCount,OrderTime")] OrderApp orderApp)
        {
            if (ModelState.IsValid)
            {
                _orderAppRepository.InsertEntity(orderApp);
                _orderAppRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_userManager.Users, "CustomerId", "UserName", orderApp.CustomerId);
            ViewData["ProductId"] = new SelectList(_productRepository.GetAllEntities(), "ProductId", "ProductName", orderApp.ProductId);
            return View(orderApp);
        }

        // GET: Admin/OrderApps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderApp = _orderAppRepository.GetEntityById(id.Value);
            if (orderApp == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_userManager.Users, "Id", "UserName", orderApp.CustomerId);
            ViewData["ProductId"] = new SelectList(_productRepository.GetAllEntities(), "ProductId", "ProductName", orderApp.ProductId);
            return View(orderApp);
        }

        // POST: Admin/OrderApps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderAppId,CustomerId,ProductId,SellCount,OrderTime")] OrderApp orderApp)
        {
            if (id != orderApp.OrderAppId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _orderAppRepository.UpdateEntity(orderApp);
                    _orderAppRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderAppExists(orderApp.OrderAppId))
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
            ViewData["CustomerId"] = new SelectList(_userManager.Users, "Id", "UserName", orderApp.CustomerId);
            ViewData["ProductId"] = new SelectList(_productRepository.GetAllEntities(), "ProductId", "ProductName", orderApp.ProductId);
            return View(orderApp);
        }

        // GET: Admin/OrderApps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderApp = _orderAppRepository.GetEntityById(id.Value);
            if (orderApp == null)
            {
                return NotFound();
            }

            return View(orderApp);
        }

        // POST: Admin/OrderApps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _orderAppRepository.DeleteEntity(id);
            _orderAppRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderAppExists(int id)
        {
            return _orderAppRepository.EntityExists(id);
        }
    }
}
