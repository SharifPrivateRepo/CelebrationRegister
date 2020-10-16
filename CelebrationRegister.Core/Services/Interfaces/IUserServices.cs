using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CelebrationRegister.Core.ViewModels.AccountViewModel;
using CelebrationRegister.Core.ViewModels.AdminViewModel;
using CelebrationRegister.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace CelebrationRegister.Core.Services.Interfaces
{
    public interface IUserServices
    {
        #region Employee

        Task AddEmployee(Employee employee);
        Task<List<Employee>> GetEmployeesAsync(int pageId = 1, int take = 20, string filter = null, int cityId = 0, string personalCode = null);
        List<Employee> GetEmployees();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<Employee> GetEmployeeByIdAsync(string personalCode);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
        Task<int> GetNumberOfEmployeeAsync(List<Employee> employees = null);
        Task<bool> NationalCodeIsExist(string nationalCode);
        Task<bool> PersonalCodeIsExist(string personalCode);
        Task<Employee> LoginEmployeeAsync(LoginViewModel login);
        Task<bool> IsFirstLoginAsync(string personalCode);
        Task SetEmployeeChildAsync(int employeeId, int childCount);
        bool ImportDataFromExcelFile(string fileName);
        string GetEmployeeNameByPersonnelCode(string personnelCode);

        #endregion

        #region Grade

        Task<List<Grade>> GetAllGradesAsync();
        Task<SelectList> GetAllGradesList();
        Task<List<Grade>> GetAllDeletedGrades();
        Task AddGradeAsync(Grade grade);
        Task UpdateGradeAsync(int gradeId, string gradeTitle);
        Task DeleteGradeAsync(int gradeId);
        List<Grade> GetAllGrade();
        Grade GetGradeById(int gradeId);

        #endregion

        #region Child

        Task<List<Child>> GetAllChildrenAsync(int employeeId = 0, int pageId = 1, int take = 10, string filter = null, string parentPersonnelCode = null, int cityId = 0);
        Task<int> GetNumberOfChildrenAsync();
        Task<List<Child>> GetChildByEmployeeIdAsync(int employeeId);
        Task<List<Child>> GetChildByPersonalCodeAsync(string pesonalCode);
        Task<int> AddChildAsync(RegisterViewModel child);
        Task AddOptionForChild(int childId, List<int> options);
        Task<int> AddChildAsync(Child child);
        Task<EditChildViewModel> GetChildInformationAsync(int childId);
        Task EditChildAsync(Child child);
        Task DeleteChildAsync(Child child);
        Task AddOptionalDetails(int id, OptionalDetails details, IFormFile image);
        Task UpdateStatusId(int reportCardId, int statusId);

        #endregion

        #region Status

        List<Status> GetAllStatus();

        #endregion

        #region report Card

        ReportCard GetReportCardByChildId(int childId);

        #endregion

        #region Export Data

        List<ExportExcelFileViewModel> ExportChildData();
        List<string> GetAllCityName();

        #endregion
    }
}
