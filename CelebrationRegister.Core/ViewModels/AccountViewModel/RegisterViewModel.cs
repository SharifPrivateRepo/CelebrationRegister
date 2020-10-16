using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CelebrationRegister.Data.Entities;
using CelebrationRegister.Data.Entities.AdditionalOptions;
using Microsoft.AspNetCore.Http;

namespace CelebrationRegister.Core.ViewModels.AccountViewModel
{
    public class RegisterViewModel
    {
        public int ChildId { get; set; }
        public int EmployeeId { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string FullName { get; set; }

        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        public string NationalCode { get; set; }

        [Display(Name = "معدل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public double AverageGrade { get; set; }

        [Display(Name = "پایه تحصیلی")]
        public int GradeId { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Birthday { get; set; }

        [Display(Name = "تصویر کارنامه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile ReportCardImage { get; set; }

        [Display(Name = "تصویر پرسنلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile PersonalImage { get; set; }

        public OptionalDetails OptionalSampadDetail { get; set; }

        public OptionalDetails OptionalUniversityDetails { get; set; }

        public List<AdditionalOptions> AdditionalOptions { get; set; }

        public IFormFile OptionalDetailsSampadImages { get; set; }

        public IFormFile OptionalDetailsUniversityImages { get; set; }
    }
}
