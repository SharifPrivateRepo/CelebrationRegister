using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CelebrationRegister.Data.Entities;
using CelebrationRegister.Data.Entities.AdditionalOptions;

namespace CelebrationRegister.Core.ViewModels.AdminViewModel
{
   public class EditChildViewModel
    {
        public int ChildId { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string FullName { get; set; }

        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(15)]
        public string NationalCode { get; set; }

        [Display(Name = "تصویر پرسنلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Image { get; set; }

        [Display(Name = "پایه تحصیلی")]
        public int GradeId { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime Birthday { get; set; }

        public List<int> OptionalDetailId { get; set; }

        public List<int> AdditionalOptionsId { get; set; }

        public int StatusId { get; set; }

    }
}
