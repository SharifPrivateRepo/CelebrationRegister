using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CelebrationRegister.Core.ViewModels.AccountViewModel
{
    public class ExportExcelFileViewModel
    {
        public int ChildId { get; set; }

        public string EmployeeName { get; set; }

        public string FullName { get; set; }

        public string NationalCode { get; set; }

        public double Average { get; set; }

        public string Grade { get; set; }

        public string Birthday { get; set; }

        public string City { get; set; }

    }
}
