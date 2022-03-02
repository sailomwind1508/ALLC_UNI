using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class Employee : BaseControl, IObject
    {
        private Func<tbl_PRMaster, bool> _docTypePredicate = null;

        public virtual Func<tbl_PRMaster, bool> docTypePredicate
        {
            get { return _docTypePredicate; }
            set
            {
                _docTypePredicate = value;
            }
        }

        public Employee() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        public List<tbl_Employee> GetAllData()
        {
            return (new tbl_Employee()).SelectAll();
        }

        public virtual int AddData(tbl_Employee tbl_Employee)
        {
            return tbl_Employee.Insert();
        }

        public int UpdateData(tbl_Employee tbl_Employee)
        {
            return tbl_Employee.Update();
        }

        public int UpdateBranchWareHouseData(tbl_BranchWarehouse tbl_BranchWarehouse)
        {
            return tbl_BranchWarehouse.Update();
        }

        public int UpdateSalAreaData(tbl_SalArea tbl_SalArea)
        {
            return tbl_SalArea.Update();
        }

        public int RemoveData(tbl_Employee tbl_Employee)
        {
            return tbl_Employee.Delete();
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                var query = (new tbl_Employee()).SelectAll();

                DataTable newTable = new DataTable();
                newTable.Columns.Add("EmpID", typeof(string));
                newTable.Columns.Add("EmpCode", typeof(string));
                newTable.Columns.Add("EmpName", typeof(string));

                foreach (var r in query)
                {
                    newTable.Rows.Add(r.EmpID, r.EmpCode, string.Join(" ", r.TitleName, r.FirstName));
                }

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetDataTable(Func<tbl_Employee, bool> predicate)
        {
            try
            {
                var query = (new tbl_Employee()).Select(predicate);

                DataTable newTable = new DataTable();
                newTable.Columns.Add("EmpID", typeof(string));
                newTable.Columns.Add("EmpCode", typeof(string));
                newTable.Columns.Add("EmpName", typeof(string));

                foreach (var r in query)
                {
                    newTable.Rows.Add(r.EmpID, r.EmpCode, string.Join(" ", r.TitleName, r.FirstName));
                }

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetDataTableByCondition(Func<tbl_Employee, bool> predicate)
        {
            DataTable dt = new DataTable();

            if (predicate != null)
            {
                dt = GetDataTable(predicate);
            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        public virtual DataTable GetDataTableByCondition(string[] filters)
        {
            DataTable dt = new DataTable();
            List<tbl_Employee> tbl_Employees = new List<tbl_Employee>();

            if (filters != null)
            {

            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        public DataTable GetSaleEmployee(int PositionID)
        {
            return (new tbl_Employee()).GetSaleEmployee(PositionID);
        }

        public DataTable GetAllSaleEmployee()
        {
            return (new tbl_Employee()).GetAllSaleEmployee();
        }

        public DataTable GetEmployeePopup()
        {
            return (new tbl_Employee()).GetEmployeePopup();
        }

        public DataTable GetBranchWarehouseMappingDT()
        {
            return (new tbl_BranchWarehouseMapping()).GetBranchWarehouseMappingDT();
        }

        public int SaveWithStore(List<tbl_BranchWarehouseMapping> tbl_BranchWarehouseMapping)
        {
            return tbl_BranchWarehouseMapping.SaveWithStore();
        }

        public int DeleteWithStore(tbl_BranchWarehouseMapping tbl_BranchWarehouseMapping)
        {
            return tbl_BranchWarehouseMapping.DeleteWithStore();
        }

    }
}
