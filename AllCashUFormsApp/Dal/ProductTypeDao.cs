using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ProductTypeDao
    {
        public static List<tbl_ProductType> Select(this tbl_ProductType tbl_ProductType, Func<tbl_ProductType, bool> predicate)
        {
            List<tbl_ProductType> list = new List<tbl_ProductType>();
            try
            {
                list = tbl_ProductType.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductType.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductType.GetType());
            }
            return list;
        }

        public static List<tbl_ProductType> SelectAll(this tbl_ProductType tbl_ProductType)
        {
            List<tbl_ProductType> list = new List<tbl_ProductType>();
            try
            {

                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_ProductType] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductType), sql);
                list = dynamicListReturned.Cast<tbl_ProductType>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductType.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductType.GetType());
            }
            return list;
        }
    }
}
