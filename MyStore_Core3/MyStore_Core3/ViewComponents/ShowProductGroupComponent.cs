using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyStore_Core3.Services.Repositories;
using MyStore_Core3.ViewModel;

namespace MyStore_Core3.ViewComponents
{
    public class ShowProductGroupComponent:ViewComponent
    {
        private IProductGroupRepository _productGroupRepository;
        private IMapper _mapper;

        public ShowProductGroupComponent(IProductGroupRepository productGroupRepository, IMapper mapper)
        {
            _productGroupRepository = productGroupRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var showProductGroup = _productGroupRepository.GetListGroupsPartialViewModel();
            var showProductGroupModel = _mapper.Map<List<ShowProductGroupPartialViewModel>>(showProductGroup);
            return await Task.FromResult((IViewComponentResult)View("ShowProductGroupComponent",
                showProductGroupModel));
        }

        //
        // public IActionResult ShowProductGroupPartial()
        // {
        //     var showProductGroup = _productGroupRepository.GetListGroupsPartialViewModel().ToList();
        //     var showProductGroupModel = _mapper.Map<List<ShowProductGroupPartialViewModel>>(showProductGroup);
        //     ViewData["showProductGroup"] = showProductGroup;
        //     return View("_ShowProductGroupPartial");
        // }


    }
}
