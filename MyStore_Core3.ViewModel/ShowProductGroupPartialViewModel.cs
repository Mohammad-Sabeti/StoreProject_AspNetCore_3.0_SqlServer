using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyStore_Core3.ViewModel
{
   public class ShowProductGroupPartialViewModel
    {

        public int ProductGroupId { get; set; }
        [Display(Name = "گروه کالا")]
        public string ProductGroupTitle { get; set; }

        [Display(Name = "تعداد کالا های گروه مربوطه")]
        public int ProductCountInThisGroup { get; set; }



    }
}
