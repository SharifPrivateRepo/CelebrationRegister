using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CelebrationRegister.Data.Entities.Role;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CelebrationRegister.Data.Entities
{
    public class Employee
    {
        public Employee()
        {
            
        }

        [Key]
        public int EmployeeId { get; set; }

        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string NationalCode { get; set; }

        [Display(Name = "کد پرسنلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string ProsonnelCode { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string FullName { get; set; }

        [Required]
        public int CityId { get; set; }

        [Display(Name = "تعداد فرزندان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ChildCount { get; set; }

        public bool FirstLogin { get; set; }

        [Display(Name = "حذف شده؟")]
        public bool IsDelete { get; set; }


        #region Relation

        public virtual List<Child> Children { get; set; }

        public List<EmployeeRole> EmployeeRoles { get; set; }

        public City City { get; set; }


        #endregion
    }
}
