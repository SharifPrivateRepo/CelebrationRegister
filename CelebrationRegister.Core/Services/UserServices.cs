using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CelebrationRegister.Core.Convertors;
using CelebrationRegister.Core.Services.Interfaces;
using CelebrationRegister.Core.Tools;
using CelebrationRegister.Core.ViewModels.AccountViewModel;
using CelebrationRegister.Core.ViewModels.AdminViewModel;
using CelebrationRegister.Data.Context;
using CelebrationRegister.Data.Entities;
using CelebrationRegister.Data.Entities.AdditionalOptions;
using ClosedXML.Excel;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CelebrationRegister.Core.Services
{
    public class UserServices : IUserServices
    {
        #region Injection

        private CelebrationRegister_Context db;

        public UserServices(CelebrationRegister_Context db)
        {
            this.db = db;
        }

        #endregion

        public async Task AddEmployee(Employee employee)
        {
            employee.FirstLogin = true;
            await db.Employees.AddAsync(employee);
            await db.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetEmployeesAsync(int pageId = 1, int take = 20, string filter = null, int cityId = 0, string personalCode = null)
        {
            IQueryable<Employee> result = db.Employees;

            if (!string.IsNullOrEmpty(filter))
            {
                string filter2 = "";
                if (filter.Contains("ی"))
                {
                    filter2 = filter.Replace("ی", "ي");
                }
                result = result.Where(e => EF.Functions.Like(e.FullName, "%" + filter + "%") ||
                                           EF.Functions.Like(e.FullName, "%" + filter2 + "%")).Distinct();
            }
            if (cityId != 0)
            {
                result = result.Where(e => e.CityId == cityId);
            }

            if (!string.IsNullOrEmpty(personalCode))
            {
                result = result.Where(e => e.ProsonnelCode == personalCode);
            }

            int skip = (pageId - 1) * take;

            result = result.Include(e => e.City)
                .OrderByDescending(e => e.ProsonnelCode).Skip(skip).Take(take);

            return await result.ToListAsync();
        }

        public List<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await db.Employees.Include(e => e.City)
                .Where(e => e.EmployeeId == id).SingleOrDefaultAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(string personalCode)
        {
            return await db.Employees
                .Include(e=>e.City)
                .Include(e => e.Children)
                .ThenInclude(ec => ec.ReportCards)
                .Where(e => e.ProsonnelCode == personalCode).SingleOrDefaultAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            db.Employees.Update(employee);
            await db.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            if (!employee.IsDelete)
                employee.IsDelete = true;

            db.Employees.Update(employee);
            await db.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await GetEmployeeByIdAsync(employeeId);

            await DeleteEmployeeAsync(employee);
        }

        public async Task<int> GetNumberOfEmployeeAsync(List<Employee> employees = null)
        {
            if (employees != null)
            {
                return employees.Count;
            }
            return await db.Employees.CountAsync();
        }

        public async Task<bool> NationalCodeIsExist(string nationalCode)
        {
            return await Task.FromResult(db.Employees.Any(e => e.NationalCode == nationalCode));
        }

        public async Task<bool> PersonalCodeIsExist(string personalCode)
        {
            return await Task.FromResult(db.Employees.Any(e => e.ProsonnelCode == personalCode));
        }

        public async Task<Employee> LoginEmployeeAsync(LoginViewModel login)
        {
            login.NationalCode = login.NationalCode.Trim();
            login.PersonalCode = login.PersonalCode.Trim();

            return await db.Employees.SingleOrDefaultAsync(e =>
                e.ProsonnelCode == login.PersonalCode && e.NationalCode == login.NationalCode);
        }

        public async Task<bool> IsFirstLoginAsync(string personalCode)
        {
            var employee = await db.Employees.SingleOrDefaultAsync(e => e.ProsonnelCode == personalCode);
            return await Task.FromResult(employee.FirstLogin);
        }

        public async Task SetEmployeeChildAsync(int employeeId, int childCount)
        {
            var employee = await GetEmployeeByIdAsync(employeeId);

            employee.ChildCount = childCount;
            if (employee.FirstLogin)
            {
                employee.FirstLogin = false;
            }

            await UpdateEmployeeAsync(employee);
        }

        public bool ImportDataFromExcelFile(string fileName)
        {
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Data/", fileName);
                bool exists = Directory.Exists(filePath);

                List<ExcelFileViewModel> data = new List<ExcelFileViewModel>();
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        reader.Read();
                        if (reader.GetValue(0).ToString() != "Personal Code")
                        {
                            return false;
                        }
                        while (reader.Read())
                        {
                            data.Add(new ExcelFileViewModel()
                            {
                                PersonalCode = reader.GetValue(0).ToString(),
                                Name = reader.GetValue(1).ToString(),
                                Family = reader.GetValue(2).ToString(),
                                NationalCode = reader.GetValue(3).ToString(),
                                CityId = int.Parse(reader.GetValue(4).ToString())
                            });

                        }
                    }

                }

                foreach (var item in data)
                {
                    db.Add(new Employee()
                    {
                        FullName = item.Name + " " + item.Family,
                        CityId = item.CityId,
                        NationalCode = item.NationalCode,
                        ProsonnelCode = item.PersonalCode,
                        FirstLogin = true,
                        IsDelete = false,
                        ChildCount = 0
                    });
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public string GetEmployeeNameByPersonnelCode(string personnelCode)
        {
            return db.Employees.SingleOrDefault(e => e.ProsonnelCode == personnelCode)?.FullName;
        }

        public async Task<List<Grade>> GetAllGradesAsync()
        {
            return await db.Grades
                .Include(g => g.ChildrenList)
                .ToListAsync();
        }

        public async Task<SelectList> GetAllGradesList()
        {
            var items = db.Grades.Select(g => new SelectListItem()
            {
                Value = g.GradeId.ToString(),
                Text = g.GradeTitle
            });

            var list = new SelectList(items, "Value", "Text");

            return await Task.FromResult(list);
        }

        public async Task<List<Grade>> GetAllDeletedGrades()
        {
            return await db.Grades.IgnoreQueryFilters()
                .Include(g => g.ChildrenList)
                .Where(g => g.IsDelete == true).ToListAsync();
        }

        public async Task AddGradeAsync(Grade grade)
        {
            grade.IsDelete = false;
            await db.Grades.AddAsync(grade);
            await db.SaveChangesAsync();
        }

        public async Task UpdateGradeAsync(int gradeId, string gradeTitle)
        {
            var grade = GetGradeById(gradeId);
            grade.GradeTitle = gradeTitle;
            db.Grades.Update(grade);
            await db.SaveChangesAsync();
        }

        public async Task DeleteGradeAsync(int gradeId)
        {
            var grade = await db.Grades.FindAsync(gradeId);
            grade.IsDelete = true;
            db.Grades.Update(grade);
            await db.SaveChangesAsync();
        }

        public List<Grade> GetAllGrade()
        {
            return db.Grades.ToList();
        }

        public Grade GetGradeById(int gradeId)
        {
            return db.Grades.Find(gradeId);
        }

        public async Task<List<Child>> GetAllChildrenAsync(int employeeId = 0, int pageId = 1, int take = 10, string filter = null, string parentPersonnelCode = null, int cityId = 0)
        {
            IQueryable<Child> result = db.Children
                .Include(c => c.Employee)
                .ThenInclude(p => p.City)
                .Include(c => c.Grade)
                .Include(c => c.ReportCards)
                .ThenInclude(r => r.Status);

            if (employeeId != 0)
            {
                result = result.Where(c => c.EmployeeId == employeeId);
            }

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => EF.Functions.Like(c.FullName, "%" + filter + "%"));
            }

            if (!string.IsNullOrEmpty(parentPersonnelCode))
            {
                result = result.Where(c => EF.Functions.Like(c.Employee.ProsonnelCode, "%" + parentPersonnelCode + "%"));
            }

            if (cityId != 0)
            {
                result = result.Where(c => c.Employee.City.CityId == cityId);
            }

            int skip = (pageId - 1) * take;

            result = result.Skip(skip).Take(take);

            return await result.ToListAsync();

        }

        public async Task<int> GetNumberOfChildrenAsync()
        {
            return await db.Children.CountAsync();
        }

        public async Task<List<Child>> GetChildByEmployeeIdAsync(int employeeId)
        {
            return await db.Children
                .Include(c => c.Grade)
                .Include(c => c.ReportCards).ThenInclude(cr => cr.Status)
                .Where(c => c.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<List<Child>> GetChildByPersonalCodeAsync(string personalCode)
        {
            int employeeId = await db.Employees.Where(e => e.ProsonnelCode == personalCode).Select(e => e.EmployeeId)
                .SingleOrDefaultAsync();

            return await GetChildByEmployeeIdAsync(employeeId);
        }

        public async Task<int> AddChildAsync(RegisterViewModel child)
        {
            //TODO : Save images
            var employeeName = db.Employees.Include(e => e.City)
                .SingleOrDefault(e => e.EmployeeId == child.EmployeeId);
            string personalImageName = ImageTools.SavePersonalImage(child.PersonalImage, employeeName.FullName, child.FullName, employeeName.City.CityName);
            string reportCardImageName =
                ImageTools.SaveReportCardImage(child.ReportCardImage, employeeName.FullName, child.FullName, employeeName.City.CityName);

            var addChild = new Child()
            {
                FullName = child.FullName,
                NationalCode = child.NationalCode,
                GradeId = child.GradeId,
                EmployeeId = child.EmployeeId,
                Birthday = DateConvertor.ToMiladi(child.Birthday),
                Image = personalImageName,
                ReportCards = new List<ReportCard>()
                {
                    new ReportCard()
                    {
                        AverageGrade = child.AverageGrade,
                        ReportCardImage = reportCardImageName,
                        IsConfirm = false,
                        StatusId = 1,
                        OptionalDetails = null,
                        Description = ""
                    }
                }
            };

            await db.Children.AddAsync(addChild);
            await db.SaveChangesAsync();
            return addChild.ChildId;
        }

        public async Task AddOptionForChild(int childId, List<int> options)
        {
            options.ForEach(option =>
             {
                 db.ChildAdditionalOptions.AddAsync(new Child_AdditionalOption()
                 {
                     ChildId = childId,
                     OptionId = option
                 });
             });

            await db.SaveChangesAsync();
        }

        public async Task<int> AddChildAsync(Child child)
        {
            await db.Children.AddAsync(child);
            await db.SaveChangesAsync();
            return child.ChildId;
        }

        public async Task<EditChildViewModel> GetChildInformationAsync(int childId)
        {
            var result = await db.Children
                .Select(c => new EditChildViewModel()
                {
                    FullName = c.FullName,
                    ChildId = c.ChildId,
                    EmployeeId = c.EmployeeId,
                    GradeId = c.GradeId,
                    Birthday = c.Birthday,
                    Image = c.Image,
                    NationalCode = c.NationalCode
                })
                .FirstOrDefaultAsync(m => m.ChildId == childId);

            result.AdditionalOptionsId = db.ChildAdditionalOptions.Where(a => a.ChildId == childId)
                .Select(a => a.OptionId).ToList();

            result.OptionalDetailId = db.OptionalDetails.Where(od => od.ChildId == childId)
                .Select(od => od.ODetailsId).ToList();

            return result;
        }

        public async Task EditChildAsync(Child child)
        {
            db.Update(child);
            await db.SaveChangesAsync();
        }

        public async Task DeleteChildAsync(Child child)
        {
            child.IsDelete = true;
            await EditChildAsync(child);
        }

        public async Task AddOptionalDetails(int id, OptionalDetails details, IFormFile image)
        {
            var child = await GetChildInformationAsync(id);
            var employee = await GetEmployeeByIdAsync(id);

            details.DetailTitle = db.DetailTitles.Find(details.DetailTitleId);

            if (child != null)
            {
                var parent = await db.Employees.Include(e => e.City)
                    .SingleOrDefaultAsync(e => e.EmployeeId == child.EmployeeId);
                details.ChildId = child.ChildId;
                //TODO : save image
                details.ImageName = ImageTools.SaveOptionalDetailImage(image, parent.FullName, parent.City.CityName, child.FullName,
                    details.DetailTitle.Title);
            }

            if (employee != null)
            {
                details.EmployeeId = employee.EmployeeId;
                //TODO : save image
            }

            await db.OptionalDetails.AddAsync(details);
        }

        public async Task UpdateStatusId(int reportCardId, int statusId)
        {
            var reportCard = await db.ReportCards.Include(r => r.Status)
                .SingleOrDefaultAsync(r => r.ReportCardId == reportCardId);

            reportCard.StatusId = statusId;

            db.Update(reportCard);
            await db.SaveChangesAsync();
        }

        public List<Status> GetAllStatus()
        {
            return db.Statuses.ToList();
        }

        public ReportCard GetReportCardByChildId(int childId)
        {
            return db.ReportCards.Include(r => r.Status)
                .OrderByDescending(r => r.ReportCardId)
                .FirstOrDefault(r => r.ChildId == childId);
        }

        public List<ExportExcelFileViewModel> ExportChildData()
        {
            var data = db.Children.Include(c => c.Employee).ThenInclude(e => e.City)
                .Include(c => c.ReportCards)
                .Include(c => c.Grade)
                .Select(c => new ExportExcelFileViewModel()
                {
                    FullName = c.FullName,
                    NationalCode = c.NationalCode,
                    Average = c.ReportCards[0].AverageGrade,
                    Grade = c.Grade.GradeTitle,
                    Birthday = c.Birthday.ToShamsi(),
                    ChildId = c.ChildId,
                    EmployeeName = c.Employee.FullName,
                    City = c.Employee.City.CityName
                }).ToList();

            return data;
        }

        public List<string> GetAllCityName()
        {
            return db.City.Select(c => c.CityName).ToList();
        }
    }
}