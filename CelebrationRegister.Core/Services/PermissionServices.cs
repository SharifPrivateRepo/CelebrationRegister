using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CelebrationRegister.Core.Services.Interfaces;
using CelebrationRegister.Data.Context;
using CelebrationRegister.Data.Entities.Role;
using Microsoft.EntityFrameworkCore;

namespace CelebrationRegister.Core.Services
{
    public class PermissionServices : IPermissionServices
    {

        #region Injection

        private CelebrationRegister_Context db;

        public PermissionServices(CelebrationRegister_Context db)
        {
            this.db = db;
        }

        #endregion

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await db.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleByRoleId(int roleId)
        {
            return await db.Roles
                 .FirstOrDefaultAsync(m => m.RoleId == roleId);
        }

        public async Task DeleteRoles(Role role)
        {
            db.Roles.Remove(role);
            await db.SaveChangesAsync();
        }

        public async Task UpdateRoles(Role role)
        {
            db.Update(role);
            await db.SaveChangesAsync();
        }

        public async Task<int> AddRoles(Role role)
        {
            await db.AddAsync(role);
            await db.SaveChangesAsync();
            return role.RoleId;
        }

        public List<Permission> GetAllPermission()
        {
            return db.Permissions.ToList();
        }

        public async Task AddPermissionToRole(int roleId, List<int> selectedPermissions)
        {
            foreach (var item in selectedPermissions)
            {
               await db.PermissionRoles.AddAsync(new PermissionRole()
                {
                    RoleId = roleId,
                    PermissionId = item
                });
            }

            await db.SaveChangesAsync();
        }

        public async Task<List<PermissionRole>> PermissionsRoleList(int roleId)
        {
            return await db.PermissionRoles.ToListAsync();
        }
    }
}
