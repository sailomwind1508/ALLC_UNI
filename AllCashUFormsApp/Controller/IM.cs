﻿using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class IM : BaseControl, IObject
    {
        private Func<tbl_POMaster, bool> _ivDocTypePredicate = null;
        public virtual Func<tbl_POMaster, bool> ivDocTypePredicate
        {
            get { return _ivDocTypePredicate; }
            set
            {
                _ivDocTypePredicate = value;
            }
        }

        public IM() : base("IM")
        {
            _ivDocTypePredicate = (x => x.DocTypeCode.Trim() == "IV" && !string.IsNullOrEmpty(x.DocRef) && x.DocRef.Trim() == "IM");
        }


        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();

                string sql = "proc_IM_GetDataTable";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                //List<tbl_POMaster> tbl_POMaster = new List<tbl_POMaster>();
                //tbl_POMaster = (new tbl_POMaster()).Select(ivDocTypePredicate);

                //var docStatus = GetDocStatus();
                //List<tbl_ArCustomer> tbl_ArCustomers = new List<tbl_ArCustomer>();

                //var allEmp = GetEmployee();
                //var allCust = GetCustomer();

                //DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();

                //newTable.Columns.Add("DocNo", typeof(string));
                //newTable.Columns.Add("DocStatusImg", typeof(Bitmap));
                //newTable.Columns.Add("DocStatus", typeof(string));
                //newTable.Columns.Add("DocDate", typeof(DateTime));
                //newTable.Columns.Add("CustomerID", typeof(string));
                //newTable.Columns.Add("CustomerName", typeof(string));
                //newTable.Columns.Add("WHID", typeof(string));
                //newTable.Columns.Add("SaleEmpID", typeof(string));
                //newTable.Columns.Add("TotalDue", typeof(decimal));
                //newTable.Columns.Add("CrUser", typeof(string));
                //newTable.Columns.Add("Remark", typeof(string));

                //foreach (var r in tbl_POMaster)
                //{
                //    Bitmap closeImg = new Bitmap(Properties.Resources.power_off);
                //    Bitmap cancelmg = new Bitmap(Properties.Resources.closeBtn);
                //    Bitmap statusImg = r.DocStatus == "4" ? closeImg : cancelmg;

                //    string docStatusName = docStatus.First(x => x.DocStatusCode == r.DocStatus).DocStatusName;
                //    tbl_Employee emp = allEmp.FirstOrDefault(x => x.EmpID == r.EmpID);
                //    string crUser = "";
                //    if (emp != null)
                //        crUser = string.Join(" ", emp.TitleName, emp.FirstName);

                //    string customerName = "";

                //    var cust = new List<tbl_ArCustomer>();
                //    var tmpCust = allCust.FirstOrDefault(x => x.CustomerID.Trim() == r.CustomerID.Trim());

                //    if (tmpCust != null)
                //        cust.Add(tmpCust);
                //    else
                //        cust = new tbl_ArCustomer().SelectSingle(r.CustomerID.Trim());

                //    if (cust != null && cust.Count > 0)
                //        customerName = string.Join(" ", cust[0].CustTitle, cust[0].CustName);

                //    newTable.Rows.Add(r.DocNo, statusImg, docStatusName, r.DocDate,
                //        r.CustomerID, customerName, r.WHID, r.SaleEmpID, r.TotalDue, crUser, r.Remark);
                //}

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
