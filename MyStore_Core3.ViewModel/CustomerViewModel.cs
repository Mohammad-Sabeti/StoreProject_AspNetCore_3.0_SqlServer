using System;
using System.Collections.Generic;
using System.Text;
using MyStore_Core3.DomainClasses;

namespace MyStore_Core3.ViewModel
{
   public class CustomerViewModel
   {
       public string CustomerId { get; set; }
       public string CustomerName { get; set; }
       public EnumGenderType CustomerGender { get; set; }
       public string CustomerMobile { get; set; }
       public string CustomerProfileImage { get; set; }
       public string CustomerAddress { get; set; }
    }
}
