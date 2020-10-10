using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CelebrationRegister.Data.Entities
{
    public class Grade
    {
        public Grade()
        {

        }

        [Key]
        public int GradeId { get; set; }

        [Display(Name = "پایه تحصیلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string GradeTitle { get; set; }


        public bool IsDelete { get; set; }


        #region Relation

        public virtual List<Child> ChildrenList { get; set; }

        #endregion

    }
}
