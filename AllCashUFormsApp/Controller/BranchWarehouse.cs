using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class BranchWarehouse : BaseControl, IObject
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

        public BranchWarehouse() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }
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
            DataTable newTableTmp = new DataTable();

            if (predicate != null)
            {
                var bwh = new tbl_BranchWarehouse();
                var tbl_BranchWarehouses = bwh.Select(predicate).ToList();

                if (tbl_BranchWarehouses.All(x => x.SaleEmpID == "0"))
                {
                    var query1 = from bhw in tbl_BranchWarehouses
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

                    newTable = query1.ToList().ToDataTable();
                }
                else
                {
                    var nonVan = tbl_BranchWarehouses.Where(x => x.SaleEmpID == "0").ToList();
                    var van = tbl_BranchWarehouses.Where(x => x.SaleEmpID != "0").ToList();

                    var query1 = from bhw in nonVan
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

                    newTable = query1.ToList().ToDataTable();
                    newTable.Clear();

                    foreach (var item in query1.ToList())
                    {
                        newTable.Rows.Add(item.WHID, item.BranchID, item.WHCode, item.WHName, item.EmpID, item.EmpCode, item.EmpName);
                    }

                    var tbl_Employees = (new tbl_Employee()).SelectAll().ToList();

                    var query2 = from bhw in van
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


                    if (query1.ToList().Count == 0)
                    {
                        newTable = query2.ToList().ToDataTable();
                        newTable.Clear();
                    }

                    foreach (var item in query2.ToList())
                    {
                        newTable.Rows.Add(item.WHID, item.BranchID, item.WHCode, item.WHName, item.EmpID, item.EmpCode, item.EmpName);
                    }
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

        public DataTable GetBranchWareHouseTable(Func<tbl_BranchWarehouse, bool> func)
        {
            try
            {
                List<tbl_BranchWarehouse> tbl_BranchWarehouses2 = new List<tbl_BranchWarehouse>();
                tbl_BranchWarehouses2 = new tbl_BranchWarehouse().Select(func);
                DataTable dt = new DataTable();
                dt = tbl_BranchWarehouses2.ToDataTable();
                return dt;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }
    }
}
