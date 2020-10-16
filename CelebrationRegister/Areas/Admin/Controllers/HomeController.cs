using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CelebrationRegister.Core.Convertors;
using CelebrationRegister.Core.Security;
using CelebrationRegister.Core.Services.Interfaces;
using CelebrationRegister.Data.Entities;
using CelebrationRegister.Data.Entities.DynamicSettings;
using Microsoft.AspNetCore.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;

namespace CelebrationRegister.Web.Areas.Admin.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [PermissionChecker(1)]
    [Area("Admin")]
    public class HomeController : Controller
    {
        #region Injection

        private IUserServices _userServices;
        private ISettingServices _settingServices;
        private IPermissionServices _permissionServices;

        public HomeController(IUserServices userServices, ISettingServices settingServices, IPermissionServices permissionServices)
        {
            _userServices = userServices;
            _settingServices = settingServices;
            _permissionServices = permissionServices;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }


        #region Settings

        public IActionResult SetBirthday()
        {
            ViewData["Time"] = _settingServices.GetBirthDayLimitation().ToShamsi();
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult SetBirthday(string date)
        {
            DateTime newDate = DateConvertor.ToMiladi(date);

            _settingServices.SetBirthDayLimitation(newDate);
            ViewData["Time"] = _settingServices.GetBirthDayLimitation().ToShamsi();
            return View();
        }

        public IActionResult AdditionalOptions()
        {
            ViewData["PanelTitle"] = "افزودن مورد تشویقی";
            return View(_settingServices.GetAllAdditionalOptions());
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult AdditionalOptions(string optionTitle)
        {
            ViewData["PanelTitle"] = "افزودن مورد تشویقی";
            _settingServices.AddOption(optionTitle);
            return View(_settingServices.GetAllAdditionalOptions());
        }

        public IActionResult DeleteOption(int optionId)
        {
            _settingServices.DeleteOption(optionId);

            return RedirectToAction("AdditionalOptions");
        }

        public IActionResult SetNotification()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult SetNotification(string text)
        {
            _settingServices.SetAverageNotification(text);
            return View();
        }

        public IActionResult AddRole()
        {
            ViewData["EmployeeId"] = new SelectList(_permissionServices.GetAllRole(), "RoleId", "RoleTitle", 0);
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult AddRole(string employeeId, int roleId)
        {
            if (_permissionServices.AddRoleForEmployee(roleId, employeeId))
            {
                ViewData["Success"] = true;
            }
            ViewData["EmployeeId"] = new SelectList(_permissionServices.GetAllRole(), "RoleId", "RoleTitle", 0);

            return View();
        }
        #endregion

        #region Grade

        public async Task<IActionResult> Grade(int type = 1, int gradeId = -1)
        {
            ViewData["gradeTitle"] = "افزودن مقطع تحصیلی";
            if (type == 1)
            {
                ViewData["DeletedList"] = true;
                if (gradeId != -1)
                {
                    ViewData["Grade"] = _userServices.GetGradeById(gradeId);
                }
                return View(await _userServices.GetAllGradesAsync());
            }
            else
            {
                ViewData["DeletedList"] = false;
                return View(await _userServices.GetAllDeletedGrades());
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGrade(string gradeTitle)
        {
            ViewData["gradeTitle"] = "افزودن مقطع تحصیلی";

            await _userServices.AddGradeAsync(new Grade()
            {
                GradeTitle = gradeTitle
            });

            return RedirectToAction("Grade");
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateGrade(string gradeTitle, int gradeId)
        {
            ViewData["gradeTitle"] = "افزودن مقطع تحصیلی";


            await _userServices.UpdateGradeAsync(gradeId, gradeTitle);

            return RedirectToAction("Grade");
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IActionResult> DeleteGrade(int gradeId)
        {
            await _userServices.DeleteGradeAsync(gradeId);

            return RedirectToAction("Grade");
        }

        #endregion
    }
}
