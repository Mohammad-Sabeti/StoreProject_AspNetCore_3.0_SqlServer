using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyStore_Core3.DomainClasses;

namespace MyStore_Core3.ViewModel
{
   public class DetailsProductViewModel: CreateProductViewModel
    {
        [Display(Name = "کد کالا")]
        public int ProductId { get; set; }

    }
}
