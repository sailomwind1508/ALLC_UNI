using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class BranchWarehouse : IObject
    {
        public List<tbl_BranchWarehouse> GetAllData()
        {
            return new tbl_BranchWarehouse().SelectAll();
        }

        public virtual int AddData(tbl_BranchWarehouse tbl_BranchWarehouse)
        {
            return tbl_BranchWarehouse.Insert();
        }

        public int UpdateData(tbl_BranchWarehouse tbl_BranchWarehouse)
        {
            return tbl_BranchWarehouse.Update();
        }

        public int RemoveData(tbl_BranchWarehouse tbl_BranchWarehouse)
        {
            return tbl_BranchWarehouse.Delete();
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                return GetWarehouseData();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public virtual DataTable GetDataTableByCondition(string[] filters)
        {
            DataTable dt = new DataTable();
            List<tbl_BranchWarehouse> tbl_BranchWarehouses = new List<tbl_BranchWarehouse>();

            if (filters != null)
            {
                tbl_BranchWarehouses = (new tbl_BranchWarehouse()).SelectAll().Where(x => filters.Contains(x.WHCode)).ToList();
                dt = tbl_BranchWarehouses.ToDataTable();
            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        public DataTable GetDataTableByCondition(Func<tbl_BranchWarehouse, bool> predicate)
        {
            DataTable dt = new DataTable();

            if (predicate != null)
            {
                dt = GetWarehouseData(predicate);
            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        private DataTable GetWarehouseData(Func<tbl_BranchWarehouse, bool> predicate = null)
        {
            DataTable newTable = new DataTable();

            if (predicate != null)
            {
                var bwh = new tbl_BranchWarehouse();
                var tbl_BranchWarehouses = bwh.Select(predicate).ToList();

                if (tbl_BranchWarehouses.All(x => x.SaleEmpID == "0"))
                {
                    var query = from bhw in tbl_BranchWarehouses
                                select new
                                {
                                    WHID = bhw.WHID,
                                    BranchID = bhw.BranchID,
                                    WHCode = bhw.WHCode,
                                    WHName = bhw.WHName,
                                    EmpID = "",
                                    EmpCode = "",
                                    EmpName = string.Join(" ", "", "")
                                };

                    newTable = query.ToList().ToDataTable();
                }
                else
                {
                    var tbl_Employees = (new tbl_Employee()).SelectAll().ToList();

                    var query = from bhw in tbl_BranchWarehouses
                                join emp in tbl_Employees on bhw.SaleEmpID equals emp.EmpID
                                select new
                                {
                                    WHID = bhw.WHID,
                                    BranchID = bhw.BranchID,
                                    WHCode = bhw.WHCode,
                                    WHName = bhw.WHName,
                                    EmpID = emp.EmpID,
                                    EmpCode = emp.EmpCode,
                                    EmpName = string.Join(" ", emp.TitleName, emp.FirstName)
                                };

                    newTable = query.ToList().ToDataTable();
                }
            }
            else
            {
                var bwh = new tbl_BranchWarehouse();
                var tbl_BranchWarehouses = bwh.SelectAll();

                var tbl_Employees = (new tbl_Employee()).SelectAll().ToList();

                var query = from bhw in tbl_BranchWarehouses
                            select new
                            {
                                WHID = bhw.WHID,
                                BranchID = bhw.BranchID,
                                WHCode = bhw.WHCode,
                                WHName = bhw.WHName,
                                EmpID = "",
                                EmpCode = "",
                                EmpName = string.Join(" ", "", "")
                            };

                newTable = query.ToList().ToDataTable();
            }

            return newTable;
        }
    }
}
