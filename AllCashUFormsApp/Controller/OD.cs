using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class OD : BaseControl, IObject
    {
        public OD() : base("OD")
        {

        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_POMaster> tbl_POMaster = new List<tbl_POMaster>();
                tbl_POMaster = (new tbl_POMaster()).Select(docTypepredicate);

                var docStatus = GetDocStatus();

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
                    tbl_Employee emp = GetEmployee(r.EmpID);
                    string crUser = string.Join(" ", emp.TitleName, emp.FirstName);

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
