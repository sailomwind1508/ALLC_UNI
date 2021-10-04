using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class ZoneDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_Zone"></param>
        /// <returns></returns>
        public static List<tbl_Zone> Select(this tbl_Zone tbl_Zone, Func<tbl_Zone, bool> predicate)
        {
            List<tbl_Zone> list = new List<tbl_Zone>();
            try
            {
                list = tbl_Zone.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Zone.Where(x => x.FlagDel == false).Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Zone.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Zone"></param>
        /// <returns></returns>
        public static List<tbl_Zone> SelectAll(this tbl_Zone tbl_Zone)
        {
            List<tbl_Zone> list = new List<tbl_Zone>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_Zone] WHERE FlagDel = 0 ORDER BY ZoneID ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Zone), sql);
                list = dynamicListReturned.Cast<tbl_Zone>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Zone.Where(x => x.FlagDel == false).OrderBy(x => x.ZoneID).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Zone.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_Zone"></param>
        /// <returns></returns>
        public static int Insert(this tbl_Zone tbl_Zone)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_Zone.Attach(tbl_Zone);
                    db.tbl_Zone.Add(tbl_Zone);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Zone.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Zone"></param>
        /// <returns></returns>
        public static int Update(this tbl_Zone tbl_Zone)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_Zone.FirstOrDefault(x => x.ZoneID == tbl_Zone.ZoneID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ZoneItem in tbl_Zone.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ZoneItem.Name)
                                {
                                    var value = tbl_ZoneItem.GetValue(tbl_Zone, null);

                                    Type t = Nullable.GetUnderlyingType(updateDataItem.PropertyType) ?? updateDataItem.PropertyType;
                                    object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                                    updateDataItem.SetValue(updateData, safeValue, null);
                                }
                            }
                        }

                        db.Entry(updateData).State = System.Data.Entity.EntityState.Modified;
                        ret = db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Zone.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Zone"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Zone tbl_Zone)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_Zone).State = EntityState.Deleted;
                    db.tbl_Zone.Remove(tbl_Zone);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Zone.GetType());
            }

            return ret;
        }
    }
}
