using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class SalAreaDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_SalArea"></param>
        /// <returns></returns>
        public static List<tbl_SalArea> Select(this tbl_SalArea tbl_SalArea, Func<tbl_SalArea, bool> predicate)
        {
            List<tbl_SalArea> list = new List<tbl_SalArea>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_SalArea.Where(predicate).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_SalArea"></param>
        /// <returns></returns>
        public static List<tbl_SalArea> SelectAll(this tbl_SalArea tbl_SalArea)
        {
            List<tbl_SalArea> list = new List<tbl_SalArea>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_SalArea.OrderBy(x => x.SalAreaCode).ThenBy(x => x.Seq).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_SalArea"></param>
        /// <returns></returns>
        public static int Insert(this tbl_SalArea tbl_SalArea)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_SalArea.Attach(tbl_SalArea);
                    db.tbl_SalArea.Add(tbl_SalArea);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_SalArea"></param>
        /// <returns></returns>
        public static int Update(this tbl_SalArea tbl_SalArea)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_SalArea.FirstOrDefault(x => x.SalAreaID == tbl_SalArea.SalAreaID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_SalAreaItem in tbl_SalArea.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_SalAreaItem.Name)
                                {
                                    var value = tbl_SalAreaItem.GetValue(tbl_SalArea, null);

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
                        ret = tbl_SalArea.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_SalArea"></param>
        /// <returns></returns>
        public static int Delete(this tbl_SalArea tbl_SalArea)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_SalArea).State = EntityState.Deleted;
                    db.tbl_SalArea.Remove(tbl_SalArea);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
            }

            return ret;
        }
    }
}
