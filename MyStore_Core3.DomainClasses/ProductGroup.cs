using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyStore_Core3.DomainClasses
{
   public class ProductGroup
    {
        [Key]
        public int ProductGroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(200)]
        public string ProductGroupTitle { get; set; }

        public List<Product> RelatedProducts { get; set; }



    }
}
