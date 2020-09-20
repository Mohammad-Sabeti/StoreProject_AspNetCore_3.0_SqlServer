using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MyStore_Core3.DomainClasses;

namespace MyStore_Core3.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "رمز عبور")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "تایید رمز عبور")]
            [Compare("Password", ErrorMessage = "رمز عبور تطابق تدارد")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "نام کاربر")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
            [MaxLength(300)]
            public string CustomerName { get; set; }

            
            [Display(Name = "جنسیت")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
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

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl, IFormFile imageUp)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new Customer() { Email = Input.Email, UserName = Input.CustomerName, CustomerName = Input.CustomerName, CustomerGender = Input.CustomerGender, CustomerMobile = Input.CustomerMobile, PhoneNumber = Input.CustomerMobile, CustomerAddress = Input.CustomerAddress, CustomerProfileImage = Input.CustomerProfileImage, CustomerRegisterDate = DateTime.Now.ToString() };

                if (imageUp != null)
                {


                    user.CustomerProfileImage = Guid.NewGuid().ToString() + Path.GetExtension(imageUp.FileName);



                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/CustomerProfileImages", user.CustomerProfileImage
                    );

                    await using var stream = new FileStream(savePath, FileMode.Create);
                    {
                        await imageUp.CopyToAsync(stream);
                    }



                }
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
