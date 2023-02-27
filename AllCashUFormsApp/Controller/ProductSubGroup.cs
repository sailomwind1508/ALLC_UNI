using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AllCashUFormsApp.Model;

namespace AllCashUFormsApp.Controller
{
    public class ProductSubGroup
    {
        public DataTable GetProductSubGroupTable()
        {
            DataTable dt = new DataTable();
            List<tbl_ProductSubGroup> tbl_ProductSubGroups = new List<tbl_ProductSubGroup>();
            tbl_ProductSubGroups = new tbl_ProductSubGroup().SelectAll();
            dt = tbl_ProductSubGroups.ToDataTable();
            return dt;
        }

        public DataTable GetProductSubGroupData_Popup(string Search)
        {
            return new tbl_ProductSubGroup().GetProductSubGroupData_Popup(Search);
        }
    }
}
