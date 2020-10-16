using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CelebrationRegister.Data.Entities.Role;

namespace CelebrationRegister.Core.Services.Interfaces
{
    public interface IPermissionServices
    {
        bool CheckPermission(int roleId, string personnelCode);
        List<Role> GetAllRole();
        bool AddRoleForEmployee(int roleId, string employeeId);
    }
}
