using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class CauseDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_Cause"></param>
        /// <returns></returns>
        public static List<tbl_Cause> Select(this tbl_Cause tbl_Cause, Func<tbl_Cause, bool> predicate)
        {
            List<tbl_Cause> list = new List<tbl_Cause>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_Cause.Where(x => x.FlagDel == false).Where(predicate).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Cause.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Cause"></param>
        /// <returns></returns>
        public static List<tbl_Cause> SelectAll(this tbl_Cause tbl_Cause)
        {
            List<tbl_Cause> list = new List<tbl_Cause>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_Cause.Where(x => x.FlagDel == false).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Cause.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_Cause"></param>
        /// <returns></returns>
        public static int Insert(this tbl_Cause tbl_Cause)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_Cause.Attach(tbl_Cause);
                    db.tbl_Cause.Add(tbl_Cause);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Cause.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Cause"></param>
        /// <returns></returns>
        public static int Update(this tbl_Cause tbl_Cause)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_Cause.FirstOrDefault(x => x.CauseID == tbl_Cause.CauseID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_CauseItem in tbl_Cause.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_CauseItem.Name)
                                {
                                    var value = tbl_CauseItem.GetValue(tbl_Cause, null);

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
                ex.WriteLog(tbl_Cause.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Cause"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Cause tbl_Cause)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_Cause).State = EntityState.Deleted;
                    db.tbl_Cause.Remove(tbl_Cause);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Cause.GetType());
            }

            return ret;
        }

    }
}
