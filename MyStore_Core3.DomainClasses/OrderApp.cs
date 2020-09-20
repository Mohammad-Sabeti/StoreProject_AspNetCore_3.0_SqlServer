using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyStore_Core3.DomainClasses
{
   public class OrderApp
    {
        [Key]
        public int OrderAppId { get; set; }

        [Display(Name = "نام مشتری ")]
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }


        [Display(Name = "نام کالا ")]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int ProductId { get; set; }



        [Display(Name = "تعداد خرید ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public int SellCount { get; set; }



        [Display(Name = "تاریخ ثبت سفارش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [DataType(DataType.DateTime)]
        public string OrderTime { get; set; }
    }
}
