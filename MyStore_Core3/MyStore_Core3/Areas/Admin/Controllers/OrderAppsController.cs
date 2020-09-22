using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    public class OrderAppsController : Controller
    {
        private IOrderAppRepository _orderAppRepository;
        private IProductRepository _productRepository;
        private UserManager<IdentityUser> _userManager;
        private IMapper _mapper;

        public OrderAppsController(IOrderAppRepository orderAppRepository, IProductRepository productRepository, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _orderAppRepository = orderAppRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        // GET: Admin/OrderApps
        public async Task<IActionResult> Index()
        {
            var orderApps = _orderAppRepository.GetAllEntities().ToList();
            var orderAppsModel = _mapper.Map<List<OrderApp>, List<OrderAppViewModel>>(orderApps);
            ViewData["ProductId"] = new SelectList(_productRepository.GetAllEntities(), "ProductId", "ProductName");
            return View(orderAppsModel);
        }

        // GET: Admin/OrderApps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderApp = _orderAppRepository.GetEntityById(id.Value);
            var orderAppsModel = _mapper.Map<OrderAppViewModel>(orderApp);

            return View(orderAppsModel);
        }
        //Todo:Do It
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
        public async Task<IActionResult> Create(OrderAppViewModel orderAppViewModel)
        {
            if (ModelState.IsValid)
            {
                var orderAppsModel = _mapper.Map<OrderApp>(orderAppViewModel);
                _orderAppRepository.InsertEntity(orderAppsModel);
                _orderAppRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_userManager.Users, "CustomerId", "UserName", orderAppViewModel.CustomerId);
            ViewData["ProductId"] = new SelectList(_productRepository.GetAllEntities(), "ProductId", "ProductName", orderAppViewModel.ProductId);
            return View(orderAppViewModel);
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
            var orderAppsModel = _mapper.Map<OrderAppViewModel>(orderApp);

            ViewData["CustomerId"] = new SelectList(_userManager.Users, "Id", "UserName", orderApp.CustomerId);
            ViewData["ProductId"] = new SelectList(_productRepository.GetAllEntities(), "ProductId", "ProductName", orderApp.ProductId);
            return View(orderAppsModel);
        }

        // POST: Admin/OrderApps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderAppViewModel orderAppViewModel)
        {
            if (id != orderAppViewModel.OrderAppId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var orderAppsModel = _mapper.Map<OrderApp>(orderAppViewModel);

                    _orderAppRepository.UpdateEntity(orderAppsModel);
                    _orderAppRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderAppExists(orderAppViewModel.OrderAppId))
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
            ViewData["CustomerId"] = new SelectList(_userManager.Users, "Id", "UserName", orderAppViewModel.CustomerId);
            ViewData["ProductId"] = new SelectList(_productRepository.GetAllEntities(), "ProductId", "ProductName", orderAppViewModel.ProductId);
            return View(orderAppViewModel);
        }

        // GET: Admin/OrderApps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderApp = _orderAppRepository.GetEntityById(id.Value);
            var orderAppsModel = _mapper.Map<OrderAppViewModel>(orderApp);

            if (orderApp == null)
            {
                return NotFound();
            }

            return View(orderAppsModel);
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
