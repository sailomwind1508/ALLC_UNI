using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Model
{
    public class BranchEmployeeModel
    {
        public string EmpCode { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentID { get; set; }
        public string PositionName { get; set; }
        public int PositionID { get; set; }
        public string MgrName { get; set; }
        public string Mobile { get; set; }
        public string CrDate { get; set; }
        public string CrUser { get; set; }
        public string EdDate { get; set; }
        public string EdUser { get; set; }
        public bool FlagDel { get; set; }

        public string Emp_ID_Card { get; set; }
        public string IDCard { get; set; }
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public int? RoleID { get; set; }
        public string EmpID { get; set; }

    }
}
