﻿using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class TR : BaseControl, IObject
    {
        private Func<tbl_PRMaster, bool> _trDocTypePredicate = null;
        public virtual Func<tbl_PRMaster, bool> trDocTypePredicate
        {
            get { return _trDocTypePredicate; }
            set
            {
                _trDocTypePredicate = value;
            }
        }

        public TR() : base("TR")
        {
            _trDocTypePredicate = (x => x.DocTypeCode == "TR");
        }

        public List<tbl_SalArea> GetAllSaleArea()
        {
            return new tbl_SalArea().SelectAll();
        }

        public List<tbl_SalAreaDistrict> GetAllSaleAreaDistrict()
        {
            return new tbl_SalAreaDistrict().SelectAll();
        }

        public List<tbl_ArCustomer> GetCustomer(Func<tbl_ArCustomer, bool> predicate)
        {
            return new tbl_ArCustomer().Select(predicate);
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_PRMaster> tbl_PRMaster = new List<tbl_PRMaster>();
                tbl_PRMaster = (new tbl_PRMaster()).Select(trDocTypePredicate);

                var docStatus = GetDocStatus();

                DataTable newTable = new DataTable();
                newTable.Columns.Add("DocNo", typeof(string));
                newTable.Columns.Add("DocStatusImg", typeof(Bitmap));
                newTable.Columns.Add("DocStatus", typeof(string));
                newTable.Columns.Add("DocRef", typeof(string));
                newTable.Columns.Add("DocDate", typeof(DateTime));
                newTable.Columns.Add("SuppName", typeof(string));
                newTable.Columns.Add("CreditDay", typeof(short));
                newTable.Columns.Add("DueDate", typeof(DateTime));
                newTable.Columns.Add("TotalDue", typeof(decimal));
                newTable.Columns.Add("CrUser", typeof(string));
                newTable.Columns.Add("Remark", typeof(string));

                foreach (var r in tbl_PRMaster)
                {
                    Bitmap closeImg = new Bitmap(Properties.Resources.power_off);
                    Bitmap cancelmg = new Bitmap(Properties.Resources.closeBtn);
                    Bitmap statusImg = r.DocStatus == "4" ? closeImg : cancelmg;

                    string docStatusName = docStatus.First(x => x.DocStatusCode == r.DocStatus).DocStatusName;
                    tbl_Employee emp = GetEmployee(r.EmpID);
                    string crUser = string.Join(" ", emp.TitleName, emp.FirstName);

                    string SuppName = "";
                    short CreditDay = 0;
                    DateTime DueDate = DateTime.MinValue;
                    decimal TotalDue = 0;

                    var branch = GetBranch();
                    if (branch != null && branch.Count > 0)
                    {
                        SuppName = branch.FirstOrDefault(x => x.BranchCode == r.FromBranchID).BranchName;
                    }

                    newTable.Rows.Add(r.DocNo, statusImg, docStatusName, r.DocRef, r.DocDate,
                        SuppName, CreditDay, DueDate, TotalDue, crUser, r.Remark);
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
