using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyStore_Core3.ViewModel
{
    public class ShowCustomersViewModel
    {
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        // public EnumGenderType CustomerGender { get; set; }
        [Required]
        public string CustomerMobile { get; set; }

        [Required]
        public string CustomerAddress { get; set; }




    }
}
