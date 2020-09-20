using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStore_Core3.DomainClasses;

namespace MyStore_Core3.ViewModel
{
   public class CreateProductViewModel
    {
        public string ProductName { get; set; }
        public ProductGroupViewModel ProductGroup { get; set; }
        public int ProductGroupId { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int ProductStock { get; set; }
        public string ProductDescription { get; set; }
        public EnumProductStatusType ProductStatus { get; set; }
        public IEnumerable<SelectListItem> ProductGroupSelectListItems { get; set; }

    }
}
