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
    public class EmployeesController : Controller
    {
        private IUserServices _userServices;

        public EmployeesController(IUserServices userServices)
        {
            _userServices = userServices;
        }


        // GET: Admin/Employees
        public async Task<IActionResult> Index(int pageId = 1, int take = 20, string filter = null, int cityId = 0, string personalCode = null)
        {
            var employees = await _userServices.GetEmployeesAsync(pageId, take, filter, cityId, personalCode);
            ViewData["pageId"] = pageId;
            ViewData["currentPage"] = pageId;
            ViewData["take"] = take;
            ViewData["UserCount"] = await _userServices.GetNumberOfEmployeeAsync();
            ViewData["filter"] = filter;
            ViewData["CityId"] = cityId;
            ViewData["personalCode"] = personalCode;
            return View(employees);
        }

        // GET: Admin/Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _userServices.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Admin/Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,NationalCode,ProsonnelCode,FullName,ChildCount")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (await _userServices.NationalCodeIsExist(employee.NationalCode) ||
                    await _userServices.PersonalCodeIsExist(employee.ProsonnelCode))
                {
                    ViewData["IsExist"] = "true";
                    return View(employee);
                }
                else
                {
                    await _userServices.AddEmployee(employee);

                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }

        // GET: Admin/Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _userServices.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Admin/Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,NationalCode,ProsonnelCode,FullName,ChildCount")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userServices.UpdateEmployeeAsync(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Admin/Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _userServices.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Admin/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userServices.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ImportData(IFormFile excelFile)
        {
            string fileName = ImageTools.SaveImportExcelFile(excelFile);

            ViewData["pageId"] = 1;
            ViewData["currentPage"] = 1;
            ViewData["take"] = 20;
            ViewData["UserCount"] = await _userServices.GetNumberOfEmployeeAsync();

            if (_userServices.ImportDataFromExcelFile(fileName))
            {
                ViewData["AddResult"] = true;
            }
            else
            {
                ViewData["AddResult"] = false;
            }

            return View("Index", await _userServices.GetEmployeesAsync(1, 20));

        }

    }
}
