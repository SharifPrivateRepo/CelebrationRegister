using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CelebrationRegister.Data.Entities.Role
{
    public class EmployeeRole
    {
        [Key]
        public int ER_Id { get; set; }

        [Required] 
        public int EmployeeId { get; set; }
        
        [Required] 
        public int RoleId { get; set; }

        #region Realtion

        public Employee Employee { get; set; }
        public Role Role { get; set; }

        #endregion
    }
}
