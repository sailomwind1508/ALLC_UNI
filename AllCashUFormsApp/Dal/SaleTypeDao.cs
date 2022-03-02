using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class SaleTypeDao
    {
        public static List<tbl_SaleType> SelectAll(this tbl_SaleType tbl_SaleType)
        {
            List<tbl_SaleType> list = new List<tbl_SaleType>();
            try
            {
                //string sql = "SELECT * FROM tbl_SaleType WHERE FlagDel = 0 AND SaleTypeID <> 1"; 
                string sql = "SELECT * FROM tbl_SaleType WHERE FlagDel = 0 "; //edti by sailom.k for support center 29/11/2021

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_SaleType), sql);
                list = dynamicListReturned.Cast<tbl_SaleType>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SaleType.GetType());
            }

            return list;
        }
    }
}
