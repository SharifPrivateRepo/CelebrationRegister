using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CelebrationRegister.Data.Entities.Role;

namespace CelebrationRegister.Core.Services.Interfaces
{
   public interface IPermissionServices
   {
       Task<List<Role>> GetAllRolesAsync();
       Task<Role> GetRoleByRoleId(int roleId);
       Task DeleteRoles(Role role);
       Task UpdateRoles(Role role);
       Task<int> AddRoles(Role role);

        #region Permission

        List<Permission> GetAllPermission();
        Task AddPermissionToRole(int roleId, List<int> selectedPermissions);
        Task<List<PermissionRole>> PermissionsRoleList(int roleId);

        #endregion

   }
}
