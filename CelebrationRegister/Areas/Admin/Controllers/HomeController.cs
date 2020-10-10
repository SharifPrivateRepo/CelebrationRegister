using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CelebrationRegister.Core.Convertors;
using CelebrationRegister.Core.Services.Interfaces;
using CelebrationRegister.Data.Entities;
using CelebrationRegister.Data.Entities.DynamicSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelebrationRegister.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
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

        public IActionResult Index()
        {
            return View();
        }


        #region Settings

        public IActionResult SetBirthday()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetBirthday(string date)
        {
            DateTime newDate = DateConvertor.ToMiladi(date);
            
            _settingServices.SetBirthDayLimitation(newDate);
            return View();
        }

        #endregion

        #region Grade

        public async Task<IActionResult> Grade(int type = 1)
        {
            ViewData["gradeTitle"] = "افزودن مقطع تحصیلی";
            if (type == 1)
            {
                ViewData["DeletedList"] = true;
                return View(await _userServices.GetAllGradesAsync());
            }
            else
            {
                ViewData["DeletedList"] = false;
                return View(await _userServices.GetAllDeletedGrades());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGrade(string gradeTitle)
        {
            ViewData["gradeTitle"] = "افزودن مقطع تحصیلی";

            await _userServices.AddGradeAsync(new Grade()
            {
                GradeTitle = gradeTitle
            });

            return RedirectToAction("Grade");
        }



        [HttpGet]
        public async Task<IActionResult> DeleteGrade(int gradeId)
        {
            await _userServices.DeleteGradeAsync(gradeId);

            return RedirectToAction("Grade");
        }

        #endregion
    }
}
