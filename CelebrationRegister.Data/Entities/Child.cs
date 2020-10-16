using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CelebrationRegister.Data.Entities.AdditionalOptions;

namespace CelebrationRegister.Data.Entities
{
    public class Child
    {
        [Key]
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


        [Display(Name = "تصویر کارنامه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ReportCardId { get; set; }

        [Display(Name = "تصویر پرسنلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Image { get; set; }

        [Display(Name = "پایه تحصیلی")]
        public int GradeId { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime Birthday { get; set; }

        public bool IsDelete { get; set; }

        #region Relation

        [Display(Name = "پایه تحصیلی")]
        public Grade Grade { get; set; }

        [Display(Name = "پدر / مادر")]
        public Employee Employee { get; set; }

        public List<ReportCard> ReportCards { get; set; }

        public List<OptionalDetails> OptionalDetails { get; set; }

        public List<Child_AdditionalOption> AdditionalOptions { get; set; }

        #endregion
    }
}
