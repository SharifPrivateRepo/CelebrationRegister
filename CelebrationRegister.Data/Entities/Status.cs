using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CelebrationRegister.Data.Entities
{
   public class Status
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        public string StatusTitle { get; set; }

        #region Relation

        public List<ReportCard> RportStatus { get; set; }

        #endregion
    }
}
