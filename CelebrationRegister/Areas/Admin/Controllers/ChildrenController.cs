using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using CelebrationRegister.Core.Convertors;
using CelebrationRegister.Core.Security;
using CelebrationRegister.Core.Services.Interfaces;
using CelebrationRegister.Core.ViewModels.AdminViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CelebrationRegister.Data.Context;
using CelebrationRegister.Data.Entities;
using ClosedXML.Excel;

namespace CelebrationRegister.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker(1)]
    public class ChildrenController : Controller
    {
        #region Injection

        private IUserServices _userServices;

        public ChildrenController( IUserServices userServices)
        {
            _userServices = userServices;
        }

        #endregion

        // GET: Admin/Children
        public async Task<IActionResult> Index(int employeeId, int pageId = 1, int take = 10
            , string filter = null, string parentPersonnelCode = null, int cityId = 0)
        {
            ViewData["pageId"] = pageId;
            ViewData["currentPage"] = pageId;
            ViewData["take"] = take;
            ViewData["UserCount"] = await _userServices.GetNumberOfChildrenAsync();

            ViewData["filter"] = filter;
            ViewData["CityId"] = cityId;
            ViewData["personalCode"] = parentPersonnelCode;

            return View(await _userServices.GetAllChildrenAsync(employeeId, pageId, take, filter, parentPersonnelCode, cityId));
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

            var reportCard = _userServices.GetReportCardByChildId(child.ChildId);
            ViewData["ReportCard"] = reportCard;
            child.StatusId = reportCard.StatusId;

            var employee = await _userServices.GetEmployeeByIdAsync(child.EmployeeId);
            ViewData["Employee"] = employee;

            ViewData["EmployeeId"] = new SelectList(_userServices.GetEmployees(), "EmployeeId", "FullName", child.EmployeeId);
            ViewData["GradeId"] = new SelectList(_userServices.GetAllGrade(), "GradeId", "GradeTitle", child.GradeId);
            ViewData["StatusItem"] = new SelectList(_userServices.GetAllStatus(), "StatusId", "StatusTitle", reportCard.Status.StatusTitle);
            return View(child);
        }

        // POST: Admin/Children/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChildId,EmployeeId,FullName,ReportCardId,Image,GradeId,IsDelete,StatusId,NationalCode,Birthday")] EditChildViewModel child, int reportCardId)
        {
            if (id != child.ChildId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Child addChild = new Child()
                    {
                        FullName = child.FullName,
                        ChildId = child.ChildId,
                        Birthday = child.Birthday,
                        EmployeeId = child.EmployeeId,
                        GradeId = child.GradeId,
                        Image = child.Image,
                        NationalCode = child.NationalCode
                    };
                    await _userServices.EditChildAsync(addChild);
                    await _userServices.UpdateStatusId(reportCardId, child.StatusId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Forbid();
                }

                return RedirectToAction(nameof(Index));
            }

            var reportCard = _userServices.GetReportCardByChildId(child.ChildId);
            ViewData["ReportCard"] = reportCard;
            child.StatusId = reportCard.StatusId;

            ViewData["EmployeeId"] = new SelectList(_userServices.GetEmployees(), "EmployeeId", "FullName", child.EmployeeId);
            ViewData["GradeId"] = new SelectList(_userServices.GetAllGrade(), "GradeId", "GradeTitle", child.GradeId);
            ViewData["StatusItem"] = new SelectList(_userServices.GetAllStatus(), "StatusId", "StatusTitle", reportCard.Status.StatusTitle);
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
            //var child = await _userServices.getchild(id);
            //await _userServices.EditChildAsync(child);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ExportExcelData()
        {
            var data = _userServices.ExportChildData();
            using (var exportFile = new XLWorkbook())
            {
                var sheet = exportFile.Worksheets.Add("Child");
                var currentRow = 1;

                #region Header

                sheet.Cell(currentRow, 1).Value = "شماره ردیف";
                sheet.Cell(currentRow, 2).Value = "نام و نام خانوادگی";
                sheet.Cell(currentRow, 3).Value = "نام پدر / مادر";
                sheet.Cell(currentRow, 4).Value = "معدل";
                sheet.Cell(currentRow, 5).Value = "کد ملی";
                sheet.Cell(currentRow, 6).Value = "پایه تحصیلی";
                sheet.Cell(currentRow, 7).Value = "تاریخ تولد";
                sheet.Cell(currentRow, 8).Value = "شهرستان";

                #endregion

                #region Body

                foreach (var item in data)
                {
                    currentRow++;
                    sheet.Cell(currentRow, 1).Value = item.ChildId;
                    sheet.Cell(currentRow, 2).Value = item.FullName;
                    sheet.Cell(currentRow, 3).Value = item.EmployeeName;
                    sheet.Cell(currentRow, 4).Value = item.Average;
                    sheet.Cell(currentRow, 5).Value = item.NationalCode;
                    sheet.Cell(currentRow, 6).Value = item.Grade;
                    sheet.Cell(currentRow, 7).Value = item.Birthday;
                    sheet.Cell(currentRow, 8).Value = item.City;
                }

                #endregion

                using (var stream = new MemoryStream())
                {
                    exportFile.SaveAs(stream);

                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Export-Data-(" + DateTime.Now.ToShamsi().Replace("/", "-") + ").xlsx");
                }
            }
        }

        public IActionResult ExportPersonalImageData()
        {
            var cityList = _userServices.GetAllCityName();
            foreach (var cityName in cityList)
            {
                string fileName = cityName + DateTime.Now.ToShamsi().Replace("/", "-") + ".zip";
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Data\\PersonalImages", fileName);

                string inputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Information\\PersonalImage", cityName);
                if (System.IO.Directory.Exists(inputPath))
                {
                    if (System.IO.File.Exists(outputPath))
                    {
                        System.IO.File.Delete(outputPath);
                    }
                    System.IO.Compression.ZipFile.CreateFromDirectory(inputPath, outputPath, CompressionLevel.Optimal, false);

                    byte[] file = System.IO.File.ReadAllBytes(outputPath);
                    return File(file, "application/zip", fileName);
                }
                continue;
            }
            return NotFound();
        }
        
        public IActionResult ExportReportCardImageData()
        {
            var cityList = _userServices.GetAllCityName();

            foreach (var cityName in cityList)
            {
                string fileName = cityName + DateTime.Now.ToShamsi().Replace("/", "-") + ".zip";
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Data\\ReportCards", fileName);

                string inputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Information\\ReportCard", cityName);
                if (System.IO.Directory.Exists(inputPath))
                {
                    if (System.IO.File.Exists(outputPath))
                    {
                        System.IO.File.Delete(outputPath);
                    }
                    System.IO.Compression.ZipFile.CreateFromDirectory(inputPath, outputPath, CompressionLevel.Optimal, false);

                    byte[] file = System.IO.File.ReadAllBytes(outputPath);
                    return File(file, "application/zip", fileName);
                }
                continue;
            }
            return NotFound();
        }
    }
}
