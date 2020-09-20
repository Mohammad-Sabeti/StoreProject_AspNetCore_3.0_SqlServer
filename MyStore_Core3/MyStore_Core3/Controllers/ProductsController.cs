using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStore_Core3.DomainClasses;
using MyStore_Core3.Services.Repositories;

namespace MyStore_Core3.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository _productRepository;
        private IOrderAppRepository _orderAppRepository;
        private UserManager<IdentityUser> _userManager;

        public ProductsController(IProductRepository productRepository, IOrderAppRepository orderAppRepository, UserManager<IdentityUser> userManager)
        {
            _productRepository = productRepository;
            _orderAppRepository = orderAppRepository;
            _userManager = userManager;
        }




        [Route("Products/{ProductId}")]
        public IActionResult ShowProducts(int ProductId)
        {

            var product = _productRepository.GetEntityById(ProductId);

            if (product != null)
            {
                _productRepository.UpdateEntity(product);
                _productRepository.Save();
            }

            return View(product);
        }

        [Authorize]
        [Route("Order/{productId}/{sell_count}")]
        public IActionResult OrderCreate(int productId,int sell_count)
        {
            var orderApp=new OrderApp()
            {
                 CustomerId = _userManager.GetUserId(this.User),
                 ProductId = productId,
                 OrderTime = DateTime.Now.ToString(),
                 SellCount = sell_count
            };
            if (ModelState.IsValid)
            {
                _orderAppRepository.InsertEntity(orderApp);
                _orderAppRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return Content("Tamam");
        }

        // public async Task<IActionResult> Create([Bind("OrderAppId,CustomerId,ProductId,SellCount,OrderTime")] OrderApp orderApp)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _orderAppRepository.InsertOrder(orderApp);
        //         _orderAppRepository.Save();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["CustomerId"] = new SelectList(_userManager.Users, "CustomerId", "UserName", orderApp.CustomerId);
        //     ViewData["ProductId"] = new SelectList(_productRepository.GetAllProducts(), "ProductId", "ProductName", orderApp.ProductId);
        //     return View(orderApp);
        // }


        // [Route("Group/{productGroupId}/{title}")]
        // public IActionResult ShowNewsByGroupId(int productGroupId, string title)
        // {
        //     ViewData["title"] = title;
        //     return View(_productRepository.GetProductsByGroupId(productGroupId));
        // }
        [Route("Search")]
        public IActionResult Search(string p)
        {
            return View(_productRepository.Search(p));
        }

    }
}
