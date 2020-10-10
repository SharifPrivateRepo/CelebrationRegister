using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CelebrationRegister.Data.Entities
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Display(Name = "محل خدمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CityName { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
