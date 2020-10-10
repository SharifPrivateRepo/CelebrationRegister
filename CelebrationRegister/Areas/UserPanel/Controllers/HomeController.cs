using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CelebrationRegister.Core.Convertors;
using CelebrationRegister.Core.Services.Interfaces;
using CelebrationRegister.Core.Tools;
using CelebrationRegister.Core.ViewModels.AccountViewModel;
using CelebrationRegister.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace CelebrationRegister.Web.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class HomeController : Controller
    {
        #region Injection

        private IUserServices _userServices;
        private ISettingServices _settingServices;

        public HomeController(IUserServices userServices, ISettingServices settingServices)
        {
            _userServices = userServices;
            _settingServices = settingServices;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var list = await _userServices.GetAllGradesList();
            ViewData["GradeList"] = list;
            var model = await _userServices.GetEmployeeByIdAsync(User.Identity.Name);

            var children = await _userServices.GetChildByPersonalCodeAsync(User.Identity.Name);

            if (User.Identity.IsAuthenticated)
            {
                if (model.FirstLogin)
                {
                    ViewData["FirstLogin"] = true;
                    return View(model);
                }

            }

            ViewData["Children"] = children;
            return View(model);
        }

        public IActionResult GuideFileDownload()
        {
            if (User.Identity.IsAuthenticated)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/GuideFile", "Guide.pdf");

                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-download", "Guide.pdf");
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            await _userServices.SetEmployeeChildAsync(employee.EmployeeId, employee.ChildCount);

            var list = await _userServices.GetAllGradesList();
            ViewData["GradeList"] = list;
            var model = await _userServices.GetEmployeeByIdAsync(User.Identity.Name);
            var children = await _userServices.GetChildByPersonalCodeAsync(User.Identity.Name);

            ViewData["Children"] = children;

            return View(model);
        }

        public async Task<IActionResult> Register(int employeeId)
        {
            var items = await _userServices.GetAllGradesAsync();

            var employee = await _userServices.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            if (employee.Children != null)
            {
                if (employee.ChildCount == employee.Children.Count)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewData["GradeList"] = items;
            ViewData["EmployeeId"] = employee.EmployeeId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel child)
        {
            DateTime birthday = DateConvertor.ToMiladi(child.Birthday);

            var maxDate = await _settingServices.GetBirthDayLimitation();


            if (!ModelState.IsValid || (birthday < maxDate))
            {
                var items = await _userServices.GetAllGradesAsync();
                if (birthday < maxDate)
                {
                    ModelState.AddModelError("", "تاریخ تولد صحیح نمی باشد");
                }

                ViewData["GradeList"] = items;
                ViewData["Error"] = true;
                ViewData["EmployeeId"] = child.EmployeeId;

                return View(child);
            }


            child.ChildId = await _userServices.AddChildAsync(child);

            if (child.OptionalDetailsSampadImages != null)
            {
                child.OptionalSampadDetail = new OptionalDetails()
                {
                    DetailTitleId = 1,
                    ChildId = child.ChildId
                };

                await _userServices.AddOptionalDetails(child.ChildId, child.OptionalSampadDetail,
                    child.OptionalDetailsSampadImages);
            }

            if (child.OptionalDetailsUniversityImages != null)
            {
                child.OptionalUniversityDetails = new OptionalDetails()
                {
                    DetailTitleId = 2,
                    ChildId = child.ChildId
                };

                await _userServices.AddOptionalDetails(child.ChildId, child.OptionalUniversityDetails,
                    child.OptionalDetailsUniversityImages);
            }

            return Redirect("/Userpanel");
        }
    }
}
