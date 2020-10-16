using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CelebrationRegister.Data.Entities.AdditionalOptions
{
    public class AdditionalOptions
    {
        [Key]
        public int OptionId { get; set; }

        [Display(Name = "مورد تشویقی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string OptionTitle { get; set; }

        public bool IsDelete { get; set; }

        public List<Child_AdditionalOption> Children { get; set; }
    }
}
