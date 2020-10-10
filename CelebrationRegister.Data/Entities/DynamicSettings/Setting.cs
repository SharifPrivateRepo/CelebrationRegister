using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CelebrationRegister.Data.Entities.DynamicSettings
{
   public class Setting
    {
        [Key]
        public int SettingId { get; set; }

        public string SettingTitle { get; set; }

        public DateTime? BirthDayLimitation { get; set; }
    }
}
