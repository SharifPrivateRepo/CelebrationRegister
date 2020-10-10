using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CelebrationRegister.Data.Entities
{
   public class DetailTitle
    {
        [Key]
        public int DetailTitleId { get; set; }

        [Display(Name = "عنوان مورد")]
        public string Title { get; set; }

        #region Relation

        public List<OptionalDetails> OptionalDetails { get; set; }


        #endregion
    }
}
