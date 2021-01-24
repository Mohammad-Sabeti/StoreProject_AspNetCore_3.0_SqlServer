using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStore_Core3.DomainClasses;
using MyStore_Core3.Services.Repositories;
using MyStore_Core3.ViewModel;

namespace MyStore_Core3.Controllers
{
    public class ShowProductsController : Controller
    {
        private IProductRepository _productRepository;
        private IOrderAppRepository _orderAppRepository;
        private IProductGroupRepository _productGroupRepository;
        private UserManager<IdentityUser> _userManager;
        private IMapper _mapper;

        public ShowProductsController(IProductRepository productRepository, IOrderAppRepository orderAppRepository, IProductGroupRepository productGroupRepository, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _productRepository = productRepository;
            _orderAppRepository = orderAppRepository;
            _productGroupRepository = productGroupRepository;
            _userManager = userManager;
            _mapper = mapper;
        }




        [Route("Products/{ProductId}")]
        public IActionResult ShowProductDetails(int ProductId)
        {

            var product = _productRepository.GetEntityById(ProductId);
            var productModel = _mapper.Map<DetailsProductViewModel>(product);
            if (product != null)
            {
                _productRepository.UpdateEntity(product);
                _productRepository.Save();
            }

            return View(productModel);
        }

        [Authorize]
        [Route("Order/{productId}/{sell_count}")]
        public IActionResult OrderCreate(int productId,int sell_count)
        {
            var orderAppViewModel=new OrderAppViewModel()
            {
                 CustomerId = _userManager.GetUserId(this.User),
                 ProductId = productId,
                 OrderTime = DateTime.Now.ToString(),
                 SellCount = sell_count
            };
            var orderApp = _mapper.Map<OrderApp>(orderAppViewModel);
            if (ModelState.IsValid)
            {
                _orderAppRepository.InsertEntity(orderApp);
               _productRepository.UpdateStockProduct(productId,sell_count);
                _orderAppRepository.Save();
                return RedirectToAction(nameof(OrderList));
            }
            return RedirectToAction(nameof(ShowProductDetails)); 
        }

        [Authorize]
        public IActionResult OrderList()
        {

            var UserId = _userManager.GetUserId(this.User);
            var Orderslist = _orderAppRepository.GetAllOrdersOfOneUserByUserId(UserId).ToList();
            ViewData["OrdersCount"] = Orderslist.Count;
            var orderAppsModel = _mapper.Map<List<OrderApp>, List<OrderAppViewModel>>(Orderslist);


            return View(orderAppsModel);
        }

        [Route("Group/{groupId}")]
        public IActionResult ProductGroupItemList(int groupId)
        {
        
            var ItemList = _productRepository.GetProductsByGroupId(groupId).ToList();
            var ItemListModel = _mapper.Map<List<Product>, List<DetailsProductViewModel>>(ItemList);
        
            return View("ProductGroupItemList",ItemListModel);
        }





        [Route("Search")]
        [HttpGet("Search")]
        public IActionResult Search(string p)
        {
            try
            {
                var searchResult = _productRepository.Search(p).ToList();
                var searchResultModel = _mapper.Map<List<Product>,List<DetailsProductViewModel>>(searchResult);
                return View(searchResultModel);
            }
            catch 
            {
                return BadRequest();
            }
        }


    }
}
