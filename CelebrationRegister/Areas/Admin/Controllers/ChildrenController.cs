using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CelebrationRegister.Core.Services.Interfaces;
using CelebrationRegister.Core.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CelebrationRegister.Data.Context;
using CelebrationRegister.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace CelebrationRegister.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChildrenController : Controller
    {

        #region Injection

        private readonly CelebrationRegister_Context _context;
        private IUserServices _userServices;

        public ChildrenController(CelebrationRegister_Context context, IUserServices userServices)
        {
            _context = context;
            _userServices = userServices;
        }

        #endregion

        // GET: Admin/Children
        public async Task<IActionResult> Index(int employeeId, int pageId = 1, int take = 10)
        {
            ViewData["pageId"] = pageId;
            ViewData["currentPage"] = pageId;
            ViewData["take"] = take;
            ViewData["UserCount"] = await _userServices.GetNumberOfChildrenAsync();

            return View(await _userServices.GetAllChildrenAsync(employeeId, pageId, take));
        }

        // GET: Admin/Children/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _userServices.GetChildInformationAsync(id.Value);

            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // GET: Admin/Children/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_userServices.GetEmployees(), "EmployeeId", "FullName");
            ViewData["GradeId"] = new SelectList(_userServices.GetAllGrade(), "GradeId", "GradeTitle");
            return View();
        }

        // POST: Admin/Children/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChildId,EmployeeId,FullName,ReportCardId,Image,GradeId,IsDelete")] Child child)
        {
            if (ModelState.IsValid)
            {
                await _userServices.AddChildAsync(child);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_userServices.GetEmployees(), "EmployeeId", "FullName", child.EmployeeId);
            ViewData["GradeId"] = new SelectList(_userServices.GetAllGrade(), "GradeId", "GradeTitle", child.GradeId);
            return View(child);
        }

        // GET: Admin/Children/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _userServices.GetChildInformationAsync(id.Value);

            if (child == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_userServices.GetEmployees(), "EmployeeId", "FullName", child.EmployeeId);
            ViewData["GradeId"] = new SelectList(_userServices.GetAllGrade(), "GradeId", "GradeTitle", child.GradeId);
            return View(child);
        }

        // POST: Admin/Children/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChildId,EmployeeId,FullName,ReportCardId,Image,GradeId,IsDelete")] Child child, IFormFile imgReportCard, IFormFile imgPersonal)
        {
            if (id != child.ChildId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //TODO : update images
                    await _userServices.EditChildAsync(child);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FullName", child.EmployeeId);
            ViewData["GradeId"] = new SelectList(_context.Grades, "GradeId", "GradeTitle", child.GradeId);
            return View(child);
        }

        // GET: Admin/Children/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _userServices.GetChildInformationAsync(id.Value);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // POST: Admin/Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var child = await _userServices.GetChildInformationAsync(id);
            await _userServices.EditChildAsync(child);
            return RedirectToAction(nameof(Index));
        }

    }
}
