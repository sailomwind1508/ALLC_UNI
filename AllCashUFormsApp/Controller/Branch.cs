using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class Branch : IObject
    {
        public List<tbl_Branch> GetAllData()
        {
            return (new tbl_Branch()).SelectAll();
        }

        public virtual int AddData(tbl_Branch tbl_Branch)
        {
            return tbl_Branch.Insert();
        }

        public int UpdateData(tbl_Branch tbl_Branch)
        {
            return tbl_Branch.Update();
        }

        public int RemoveData(tbl_Branch tbl_Branch)
        {
            return tbl_Branch.Delete();
        }
        public DataTable GetDataTable(bool isPopup = true)
        {
            return new tbl_Branch().GetDataTable();
        }
        public virtual DataTable GetDataTableByCondition(string[] filters)
        {
            DataTable dt = new DataTable();
            List<tbl_Branch> tbl_Branchs = new List<tbl_Branch>();

            if (filters != null)
            {
                tbl_Branchs = (new tbl_Branch()).SelectAll().Where(x => filters.Contains(x.BranchCode)).ToList();
                dt = tbl_Branchs.ToDataTable();
            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }
    }
}
