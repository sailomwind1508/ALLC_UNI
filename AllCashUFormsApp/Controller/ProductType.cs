using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllCashUFormsApp.Model;

namespace AllCashUFormsApp.Controller
{
    public class ProductType
    {
        public DataTable GetProductTypeTable()
        {
            DataTable dt = new DataTable();
            List<tbl_ProductType> tbl_ProductTypes = new List<tbl_ProductType>();
            tbl_ProductTypes = new tbl_ProductType().SelectAll();
            dt = tbl_ProductTypes.ToDataTable();
            return dt;
        }
    }
}
