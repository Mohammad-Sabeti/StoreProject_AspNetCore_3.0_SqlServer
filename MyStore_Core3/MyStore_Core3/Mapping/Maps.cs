using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyStore_Core3.DomainClasses;
using MyStore_Core3.ViewModel;

namespace MyStore_Core3.Mapping
{
    public class Maps:Profile
    {
        public Maps()
        {

            CreateMap<Product, DetailsProductViewModel>().ReverseMap();
            CreateMap<Product, CreateProductViewModel>().ReverseMap();
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<OrderApp, OrderAppViewModel>().ReverseMap();
            CreateMap<ProductGroup, ProductGroupViewModel>().ReverseMap();

        }
    }
}
