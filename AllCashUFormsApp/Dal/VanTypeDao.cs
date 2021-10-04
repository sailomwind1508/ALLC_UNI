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
    public static class VanTypeDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_VanType"></param>
        /// <returns></returns>
        public static List<tbl_VanType> Select(this tbl_VanType tbl_VanType, Func<tbl_VanType, bool> predicate)
        {
            List<tbl_VanType> list = new List<tbl_VanType>();
            try
            {
                list = tbl_VanType.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_VanType.Where(x => x.FlagDel == false).Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_VanType.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_VanType"></param>
        /// <returns></returns>
        public static List<tbl_VanType> SelectAll(this tbl_VanType tbl_VanType)
        {
            List<tbl_VanType> list = new List<tbl_VanType>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_VanType] WHERE FlagDel = 0 ORDER BY Seq ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_VanType), sql);
                list = dynamicListReturned.Cast<tbl_VanType>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_VanType.Where(x => x.FlagDel == false).OrderBy(x => x.Seq).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_VanType.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_VanType"></param>
        /// <returns></returns>
        public static int Insert(this tbl_VanType tbl_VanType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_VanType.Attach(tbl_VanType);
                    db.tbl_VanType.Add(tbl_VanType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_VanType.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_VanType"></param>
        /// <returns></returns>
        public static int Update(this tbl_VanType tbl_VanType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_VanType.FirstOrDefault(x => x.AutoID == tbl_VanType.AutoID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_VanTypeItem in tbl_VanType.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_VanTypeItem.Name)
                                {
                                    var value = tbl_VanTypeItem.GetValue(tbl_VanType, null);

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
                ex.WriteLog(tbl_VanType.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_VanType"></param>
        /// <returns></returns>
        public static int Delete(this tbl_VanType tbl_VanType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_VanType).State = EntityState.Deleted;
                    db.tbl_VanType.Remove(tbl_VanType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_VanType.GetType());
            }

            return ret;
        }
    }
}
