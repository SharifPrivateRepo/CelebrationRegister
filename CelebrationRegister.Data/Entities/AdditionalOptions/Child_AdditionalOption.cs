using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CelebrationRegister.Data.Entities.AdditionalOptions
{
   public class Child_AdditionalOption
    {
        [Key]
        public int CH_AOId { get; set; }

        public int ChildId { get; set; }

        public int OptionId { get; set; }


        public Child Child { get; set; }
        public AdditionalOptions AdditionalOptions { get; set; }

    }
}
