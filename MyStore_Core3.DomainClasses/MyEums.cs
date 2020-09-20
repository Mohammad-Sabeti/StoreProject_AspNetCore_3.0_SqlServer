using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyStore_Core3.DomainClasses
{
    public enum EnumGenderType
    {
        [Display(Name = "مرد")]
        MALE = 1,
        [Display(Name = "زن")]
        FEMALE = 2
    }
    public enum EnumProductStatusType
    {
        [Display(Name = "موجود در انبار")]
        Available = 1,
        [Display(Name = "موجودی تا چند روز آینده")]
        NotAvailable = 2
    }
    
}