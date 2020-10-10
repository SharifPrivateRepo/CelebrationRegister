using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CelebrationRegister.Data.Entities
{
    public class OptionalDetails
    {
        [Key]
        public int ODetailsId { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }

        public int? ChildId { get; set; }

        public int? EmployeeId { get; set; }

        public int DetailTitleId { get; set; }


        #region Relation

        public Child Child { get; set; }
        public Employee Employee { get; set; }
        public DetailTitle DetailTitle { get; set; }


        #endregion
    }
}
