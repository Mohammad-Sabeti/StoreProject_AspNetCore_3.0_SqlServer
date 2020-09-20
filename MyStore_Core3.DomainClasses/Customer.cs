using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace MyStore_Core3.DomainClasses
{

    public class Customer: IdentityUser
    {


        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(300)]
        public string CustomerName { get; set; }


        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(50)]
        public EnumGenderType CustomerGender { get; set; }


        [Display(Name = "شماره موبایل ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [DataType(DataType.PhoneNumber)]
        public string CustomerMobile { get; set; }


        [Display(Name = "تاریخ ثبت نام")]
        [DataType(DataType.DateTime)]
        public string CustomerRegisterDate { get; set; }


        [Display(Name = "پروفایل کاربری")]
        [DataType(DataType.ImageUrl)]
        public string CustomerProfileImage { get; set; }



        [Display(Name = "آدرس")]
        [DataType(DataType.MultilineText)]
        [MaxLength(500)]
        public string CustomerAddress { get; set; }




    }
}
