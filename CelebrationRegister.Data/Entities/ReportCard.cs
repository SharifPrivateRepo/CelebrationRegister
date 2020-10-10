using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CelebrationRegister.Data.Entities
{
    public class ReportCard
    {
        public ReportCard()
        {
            
        }

        [Key]
        public int ReportCardId { get; set; }

        [Display(Name = "نام محصل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ChildId { get; set; }

        [Display(Name = "تصویر کارنامه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ReportCardImage { get; set; }

        [Display(Name = "تاییدیه")]
        public bool IsConfirm { get; set; }
        
        [Display(Name = "معدل")]
        public double AverageGrade { get; set; }

        public int StatusId { get; set; }

        public string Description { get; set; }

        public int OptionalDetailId { get; set; }



        #region Relation

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        public virtual Child Child { get; set; }
        public List<OptionalDetails> OptionalDetails { get; set; }

        #endregion
    }
}
