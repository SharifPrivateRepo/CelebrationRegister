using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CelebrationRegister.Data.Entities.Role
{
   public class PermissionRole
    {
        [Key]
        public int PR_Id { get; set; }

        [Required]
        public int PermissionId { get; set; }
        
        [Required]
        public int RoleId { get; set; }

        #region Relation

        public Permission Permission { get; set; }
        public Role Role { get; set; }

        #endregion
    }
}
