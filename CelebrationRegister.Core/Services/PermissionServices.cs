using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CelebrationRegister.Core.Services.Interfaces;
using CelebrationRegister.Data.Context;
using CelebrationRegister.Data.Entities.Role;
using Microsoft.EntityFrameworkCore;
using SelectListItem = System.Web.Mvc.SelectListItem;

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

        public bool CheckPermission(int roleId, string personnelCode)
        {
            int employeeId = db.Employees.SingleOrDefault(e => e.ProsonnelCode == personnelCode).EmployeeId;

            List<EmployeeRole> employeeRole = db.EmployeeRoles.Where(e => e.EmployeeId == employeeId).ToList();

            if (!employeeRole.Any())
            {
                return false;
            }

            if (employeeRole.Any(er => er.RoleId == 1))
            {
                return true;
            }

            return false;
        }

        public List<Role> GetAllRole()
        {
            return db.Roles.ToList();
        }

        public bool AddRoleForEmployee(int roleId, string personnelCode)
        {
            int? employeeId = db.Employees.Where(e => e.ProsonnelCode == personnelCode).Select(e => e.EmployeeId)
                .FirstOrDefault();

            if (employeeId == null)
            {
                return false;
            }

            try
            {
                db.EmployeeRoles.Add(new EmployeeRole()
                {
                    EmployeeId = employeeId.Value,
                    RoleId = roleId
                });
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
