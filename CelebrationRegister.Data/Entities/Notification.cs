using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CelebrationRegister.Data.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Display(Name = "عنوان اطلاعیه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string Title { get; set; }

        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        
        [Display(Name = "متن اطلاعیه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string Text { get; set; }

        [Display(Name = "تصویر اطلاعیه")]
        public string Image { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime CreateDate { get; set; }

    }
}
