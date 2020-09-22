using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MyStore_Core3.ViewModel
{
   public class OrderAppViewModel
    {


        public int OrderAppId { get; set; }




        [Display(Name = "نام مشتری ")]

        public CustomerViewModel Customer { get; set; }
        [Display(Name = "نام مشتری ")]
        public string CustomerId { get; set; }




        [Display(Name = "نام کالا ")]
        public DetailsProductViewModel Product { get; set; }
        [Display(Name = "نام کالا ")]
        public int ProductId { get; set; }




        public IEnumerable<SelectListItem> CustomerSelectListItems { get; set; }
        public IEnumerable<SelectListItem> ProductSelectListItems { get; set; }



        [Display(Name = "تعداد خرید ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public int SellCount { get; set; }



        [Display(Name = "تاریخ ثبت سفارش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [DataType(DataType.DateTime)]
        public string OrderTime { get; set; }
    }
}
