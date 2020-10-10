using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CelebrationRegister.Core.ViewModels.AccountViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کدملی")]
        public string NationalCode { get; set; }

        [Display(Name = "کدپرسنلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PersonalCode { get; set; }

        [Display(Name = "مرا بخاطر بسپار !")]
        public bool RememberMe { get; set; }
    }
}
