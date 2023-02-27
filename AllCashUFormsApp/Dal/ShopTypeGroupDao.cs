using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ShopTypeGroupDao
    {
        public static List<tbl_ShopTypeGroup> Select(this tbl_ShopTypeGroup tbl_ShopTypeGroup, Func<tbl_ShopTypeGroup, bool> condition)
        {
            List<tbl_ShopTypeGroup> list = new List<tbl_ShopTypeGroup>();
            try
            {
                list = tbl_ShopTypeGroup.SelectAll().Where(condition).ToList();

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ShopTypeGroup.GetType());
            }

            return list;
        }
        public static List<tbl_ShopTypeGroup> SelectAll(this tbl_ShopTypeGroup tbl_ShopTypeGroup)
        {
            List<tbl_ShopTypeGroup> list = new List<tbl_ShopTypeGroup>();
            try
            {
                string sql = "SELECT * FROM tbl_ShopTypeGroup";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ShopTypeGroup), sql);
                list = dynamicListReturned.Cast<tbl_ShopTypeGroup>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ShopTypeGroup.GetType());
            }

            return list;
        }
    }
}
