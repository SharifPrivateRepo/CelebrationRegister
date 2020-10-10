using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CelebrationRegister.Data.Entities.Role
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }
        [Required]
        public string PermissionTitle { get; set; }

        public int? ParentId { get; set; }


        #region Relation

        public List<PermissionRole> PermissionRoles { get; set; }
        
        [ForeignKey("ParentId")]
        public List<Permission> Permissions { get; set; }
        #endregion
    }
}
