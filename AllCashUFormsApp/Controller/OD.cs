using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class OD : BaseControl, IObject
    {
        public OD() : base("OD")
        {

        }

        //public int UpdateOD(string docTypeCode)
        //{
        //    int result = 0;
        //    List<int> ret = new List<int>();
        //    try
        //    {
        //        DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString);

        //        //PO--------------------------------------------------------------------------------
        //        if (tbl_POMaster != null && !string.IsNullOrEmpty(tbl_POMaster.DocNo))
        //            ret.Add(tbl_POMaster.UpdateEntity(db, docTypeCode));

        //        if (tbl_PODetails != null && tbl_PODetails.Count > 0)
        //            ret.Add(tbl_PODetails.UpdateEntity(db, docTypeCode));
        //        //PO--------------------------------------------------------------------------------

        //        //InvMovements--------------------------------------------------------------------------------
        //        if (tbl_InvMovements != null && tbl_InvMovements.Count > 0)
        //            ret.Add(tbl_InvMovements.UpdateEntity(db, docTypeCode));
        //        //PO--------------------------------------------------------------------------------));
        //        //InvMovements--------------------------------------------------------------------------------

        //        //DocRunning--------------------------------------------------------------------------------
        //        if (tbl_DocRunning != null && tbl_DocRunning.Count > 0)
        //            ret.Add(tbl_DocRunning.UpdateEntity(db));
        //        //DocRunning--------------------------------------------------------------------------------

        //        //InvWarehouses--------------------------------------------------------------------------------
        //        if (tbl_InvWarehouses != null && tbl_InvWarehouses.Count > 0)
        //            ret.Add(tbl_InvWarehouses.UpdateEntity(db));
        //        //PO--------------------------------------------------------------------------------));

        //        if (ret.All(x => x == 1))
        //            result = db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.WriteLog(this.GetType());
        //        return 0;
        //    }

        //    return result != 0 ? 1 : 0;
        //}

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_POMaster> tbl_POMaster = new List<tbl_POMaster>();
                tbl_POMaster = (new tbl_POMaster()).Select(docTypepredicate);

                var docStatus = GetDocStatus();
                var allEmp = GetEmployee();

                DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();
                //newTable.Clear();

                newTable.Columns.Add("DocNo", typeof(string));
                newTable.Columns.Add("DocStatusImg", typeof(Bitmap));  //Type.GetType("System.Byte[]"));
                newTable.Columns.Add("DocStatus", typeof(string));
                newTable.Columns.Add("DocRef", typeof(string));
                newTable.Columns.Add("DocDate", typeof(DateTime));
                newTable.Columns.Add("SuppName", typeof(string));
                newTable.Columns.Add("CreditDay", typeof(short));
                newTable.Columns.Add("DueDate", typeof(DateTime));
                newTable.Columns.Add("TotalDue", typeof(decimal));
                newTable.Columns.Add("CrUser", typeof(string));
                newTable.Columns.Add("Remark", typeof(string));

                foreach (var r in tbl_POMaster)
                {
                    Bitmap closeImg = new Bitmap(Properties.Resources.power_off);
                    Bitmap cancelmg = new Bitmap(Properties.Resources.closeBtn);
                    Bitmap statusImg = r.DocStatus == "4" ? closeImg : cancelmg;

                    string docStatusName = docStatus.First(x => x.DocStatusCode == r.DocStatus).DocStatusName;
                    tbl_Employee emp = allEmp.FirstOrDefault(x => x.EmpID == r.EmpID);
                    string crUser = "";
                    if (emp != null)
                        crUser = string.Join(" ", emp.TitleName, emp.FirstName);

                    newTable.Rows.Add(r.DocNo, statusImg, docStatusName, r.DocRef, r.DocDate,
                        r.SuppName, r.CreditDay, r.DueDate, r.TotalDue, crUser, r.Remark);
                }

                return newTable;
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

            if (filters != null)
            {

            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

    }
}
