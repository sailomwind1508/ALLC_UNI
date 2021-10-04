using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ProductRemarkRejectDao 
    {
        public static List<tbl_ProductRemarkReject> Select(this tbl_ProductRemarkReject tbl_ProductRemarkReject, Func<tbl_ProductRemarkReject, bool> predicate)
        {
            List<tbl_ProductRemarkReject> list = new List<tbl_ProductRemarkReject>();
            try
            {
                list = tbl_ProductRemarkReject.SelectAll().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductRemarkReject.GetType());
            }

            return list;
        }
        public static List<tbl_ProductRemarkReject> SelectAll(this tbl_ProductRemarkReject tbl_ProductRemarkReject)
        {
            List<tbl_ProductRemarkReject> list = new List<tbl_ProductRemarkReject>();
            try
            {
                string sql = "SELECT * FROM tbl_ProductRemarkReject";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductRemarkReject), sql);
                list = dynamicListReturned.Cast<tbl_ProductRemarkReject>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductRemarkReject.GetType());
            }

            return list;
        }
        public static int Insert(this tbl_ProductRemarkReject tbl_ProductRemarkReject)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ProductRemarkReject.Attach(tbl_ProductRemarkReject);
                    db.tbl_ProductRemarkReject.Add(tbl_ProductRemarkReject);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductRemarkReject.GetType());
            }

            return ret;
        }
        public static int Update(this tbl_ProductRemarkReject tbl_ProductRemarkReject)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ProductRemarkReject.FirstOrDefault(x => x.RemarkRejectID == tbl_ProductRemarkReject.RemarkRejectID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ProductRemarkRejectItem in tbl_ProductRemarkReject.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ProductRemarkRejectItem.Name)
                                {
                                    var value = tbl_ProductRemarkRejectItem.GetValue(tbl_ProductRemarkReject, null);

                                    Type t = Nullable.GetUnderlyingType(updateDataItem.PropertyType) ?? updateDataItem.PropertyType;
                                    object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                                    updateDataItem.SetValue(updateData, safeValue, null);
                                }
                            }
                        }

                        db.Entry(updateData).State = System.Data.Entity.EntityState.Modified;
                        ret = db.SaveChanges();
                    }
                    else
                    {
                        ret = tbl_ProductRemarkReject.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductRemarkReject.GetType());
            }

            return ret;
        }
        public static DataTable GetProductRemarkRejectData(this tbl_ProductRemarkReject tbl_ProductRemarkReject, string search,int flagDel)
        {
            DataTable dt = new DataTable();

            string sql = "SELECT * FROM tbl_ProductRemarkReject WHERE FlagDel = " + flagDel + "";

            if (!string.IsNullOrEmpty(search))
            {
                sql += " AND RemarkRejectName like '%'+'" + search + "'+'%'";
            }

            dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            return dt;
        }
    }
}
