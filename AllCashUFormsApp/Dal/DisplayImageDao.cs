using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class DisplayImageDao
    {
        public static List<tbl_DisplayImage> Select(this tbl_DisplayImage tbl_DisplayImage, Func<tbl_DisplayImage, bool> predicate)
        {
            List<tbl_DisplayImage> list = new List<tbl_DisplayImage>();
            try
            {
                list = tbl_DisplayImage.SelectAll().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DisplayImage.GetType());
            }

            return list;
        }
        public static List<tbl_DisplayImage> SelectAll(this tbl_DisplayImage tbl_DisplayImage)
        {
            List<tbl_DisplayImage> list = new List<tbl_DisplayImage>();
            try
            {
                string sql = "SELECT * FROM tbl_DisplayImage ";
                var dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_DisplayImage),sql);
                list = dynamicListReturned.Cast<tbl_DisplayImage>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DisplayImage.GetType());
            }

            return list;
        }
        public static int Insert(this tbl_DisplayImage tbl_DisplayImage)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_DisplayImage.Attach(tbl_DisplayImage);
                    db.tbl_DisplayImage.Add(tbl_DisplayImage);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DisplayImage.GetType());
            }

            return ret;
        }
        public static int Update(this tbl_DisplayImage tbl_DisplayImage)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_DisplayImage.FirstOrDefault(x => x.AutoID == tbl_DisplayImage.AutoID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_DisplayImageItem in tbl_DisplayImage.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_DisplayImageItem.Name)
                                {
                                    var value = tbl_DisplayImageItem.GetValue(tbl_DisplayImage, null);

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
                        ret = tbl_DisplayImage.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DisplayImage.GetType());
            }

            return ret;
        }
        public static DataTable GetDisplayImageData(this tbl_DisplayImage tbl_DisplayImage,int flagDel,string search)
        {
            string sql = "SELECT * FROM tbl_DisplayImage WHERE FlagDel = " + flagDel + "";
            if (!string.IsNullOrEmpty(search))
            {
                sql += " AND Name LIKE '%' +'" + search + "'+ '%' ";
            }
            DataTable dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            return dt;
        }
    }
}
