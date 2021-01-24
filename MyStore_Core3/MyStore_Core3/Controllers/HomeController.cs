using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyStore_Core3.DomainClasses;
using MyStore_Core3.Models;
using MyStore_Core3.Services.Repositories;
using MyStore_Core3.ViewModel;
using Serilog;
using Serilog.Events;


namespace MyStore_Core3.Controllers
{
    public class HomeController : Controller
    {

        private IProductRepository _productRepository;
        private IMapper _mapper;

        public HomeController( IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {

            Log.Logger.Write(LogEventLevel.Warning, "Hello  Mr S-W-G-D :) ");
            var product = _productRepository.GetAllEntities().ToList();
            Log.Logger.Write(LogEventLevel.Error, "Wowww !!! This log is Error ... ");
            var model = _mapper.Map<List<Product>, List<DetailsProductViewModel>>(product);

            Log.Logger.Write(LogEventLevel.Information, "Wowww !!! This log is Information ... ");
            Log.Logger.Write(LogEventLevel.Warning, "Wowww !!! This is Warning ... ");

            return View("Index",model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
