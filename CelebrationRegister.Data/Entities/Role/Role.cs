using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CelebrationRegister.Data.Entities.Role
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string RoleTitle { get; set; }

        #region Realtion

        public List<EmployeeRole> EmployeeRoles { get; set; }
        public List<PermissionRole> PermissionRoles { get; set; }

        #endregion
    }
}
