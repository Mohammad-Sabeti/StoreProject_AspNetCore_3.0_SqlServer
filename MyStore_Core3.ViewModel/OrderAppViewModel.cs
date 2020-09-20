using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MyStore_Core3.ViewModel
{
   public class OrderAppViewModel
    {


        public int OrderAppId { get; set; }


        public CustomerViewModel Customer { get; set; }
        public string CustomerId { get; set; }


        public DetailsProductViewModel Product { get; set; }
        public int ProductId { get; set; }

        public IEnumerable<SelectListItem> CustomerSelectListItems { get; set; }
        public IEnumerable<SelectListItem> ProductSelectListItems { get; set; }

        public int SellCount { get; set; }



        public string OrderTime { get; set; }
    }
}
