using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class BranchDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_Branch"></param>
        /// <returns></returns>
        public static List<tbl_Branch> Select(this tbl_Branch tbl_Branch, Func<tbl_Branch, bool> predicate)
        {
            List<tbl_Branch> list = new List<tbl_Branch>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_Branch.Where(predicate).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Branch.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Branch"></param>
        /// <returns></returns>
        public static List<tbl_Branch> SelectAll(this tbl_Branch tbl_Branch)
        {
            List<tbl_Branch> list = new List<tbl_Branch>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_Branch.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Branch.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_Branch"></param>
        /// <returns></returns>
        public static int Insert(this tbl_Branch tbl_Branch)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_Branch.Attach(tbl_Branch);
                    db.tbl_Branch.Add(tbl_Branch);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Branch.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Branch"></param>
        /// <returns></returns>
        public static int Update(this tbl_Branch tbl_Branch)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_Branch.FirstOrDefault(x => x.BranchID == tbl_Branch.BranchID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_BranchItem in tbl_Branch.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_BranchItem.Name)
                                {
                                    var value = tbl_BranchItem.GetValue(tbl_Branch, null);

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
                        ret = tbl_Branch.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Branch.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Branch"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Branch tbl_Branch)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_Branch).State = EntityState.Deleted;
                    db.tbl_Branch.Remove(tbl_Branch);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Branch.GetType());
            }

            return ret;
        }
    }
}
