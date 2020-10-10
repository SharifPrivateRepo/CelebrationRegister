using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CelebrationRegister.Core.Services.Interfaces;
using CelebrationRegister.Core.ViewModels.AdminViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CelebrationRegister.Data.Context;
using CelebrationRegister.Data.Entities.Role;

namespace CelebrationRegister.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {

        #region Injection

        private readonly CelebrationRegister_Context _context;
        private IPermissionServices _permissionServices;

        public RolesController(CelebrationRegister_Context context, IPermissionServices permissionServices)
        {
            _context = context;
            _permissionServices = permissionServices;
        }

        #endregion

        // GET: Admin/Roles
        public async Task<IActionResult> Index()
        {
            return View(await _permissionServices.GetAllRolesAsync());
        }

        // GET: Admin/Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _permissionServices.GetRoleByRoleId(id.Value);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Admin/Roles/Create
        public IActionResult Create()
        {
            ViewData["Permissions"] = _permissionServices.GetAllPermission();
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleViewModel role, List<int> selectedPermissions)
        {
            if (ModelState.IsValid)
            {
                var addRole = new Role()
                {
                    RoleTitle = role.RoleTitle
                };
                var roleId = await _permissionServices.AddRoles(addRole);
                await _permissionServices.AddPermissionToRole(roleId, selectedPermissions);


                return RedirectToAction(nameof(Index));
            }
            ViewData["Permissions"] = _permissionServices.GetAllPermission();
            return View(role);
        }

        // GET: Admin/Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Perimmsions"] = _permissionServices.GetAllPermission();
            ViewData["selectedPermissions"] = await _permissionServices.PermissionsRoleList(id.Value);
            var role = await _permissionServices.GetRoleByRoleId(id.Value);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,RoleTitle")] Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _permissionServices.UpdateRoles(role);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.RoleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Admin/Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _permissionServices.GetRoleByRoleId(id.Value);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _permissionServices.GetRoleByRoleId(id);
            await _permissionServices.DeleteRoles(role);
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.RoleId == id);
        }
    }
}
