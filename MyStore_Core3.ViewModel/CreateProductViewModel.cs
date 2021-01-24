using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStore_Core3.DomainClasses;

namespace MyStore_Core3.ViewModel
{
   public class CreateProductViewModel
    {

        [Display(Name = "نام کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(300)]
        public string ProductName { get; set; }

        [Display(Name = "دسته بندی کالا")]
        public ProductGroupViewModel ProductGroup { get; set; }
        [Display(Name = "دسته بندی کالا")]
        public int ProductGroupId { get; set; }


        [Display(Name = "قیمت کالا ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public decimal ProductPrice { get; set; }


        [Display(Name = "تصویر کالا")]
        public string ProductImage { get; set; }

        [Display(Name = "موجودی کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public int ProductStock { get; set; }


        [Display(Name = "شرح کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(600)]
        [DataType(DataType.MultilineText)] 
        public string ProductDescription { get; set; }


        [Display(Name = "وضعیت کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public EnumProductStatusType ProductStatus { get; set; }



        public IEnumerable<SelectListItem> ProductGroupSelectListItems { get; set; }

    }
}
