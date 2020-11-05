using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class HQ_SKUGroupDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_SKUGroup"></param>
        /// <returns></returns>
        public static IEnumerable<tbl_HQ_SKUGroup> Select(this tbl_HQ_SKUGroup obj, object condition)
        {
            return new tbl_HQ_SKUGroup().Select(x => x.SKU_ID.Trim() == condition.ToString().Trim()).AsEnumerable();
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_SKUGroup"></param>
        /// <returns></returns>
        public static List<tbl_HQ_SKUGroup> Select(this tbl_HQ_SKUGroup tbl_HQ_SKUGroup, Func<tbl_HQ_SKUGroup, bool> predicate)
        {
            List<tbl_HQ_SKUGroup> list = new List<tbl_HQ_SKUGroup>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_HQ_SKUGroup.Where(predicate).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_SKUGroup.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_HQ_SKUGroup"></param>
        /// <returns></returns>
        public static List<tbl_HQ_SKUGroup> SelectAll(this tbl_HQ_SKUGroup tbl_HQ_SKUGroup)
        {
            List<tbl_HQ_SKUGroup> list = new List<tbl_HQ_SKUGroup>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_HQ_SKUGroup.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_SKUGroup.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_HQ_SKUGroup"></param>
        /// <returns></returns>
        public static int Insert(this tbl_HQ_SKUGroup tbl_HQ_SKUGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_HQ_SKUGroup.Attach(tbl_HQ_SKUGroup);
                    db.tbl_HQ_SKUGroup.Add(tbl_HQ_SKUGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_SKUGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_HQ_SKUGroup"></param>
        /// <returns></returns>
        public static int Update(this tbl_HQ_SKUGroup tbl_HQ_SKUGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_HQ_SKUGroup.FirstOrDefault(x => x.SKUGroupID == tbl_HQ_SKUGroup.SKUGroupID && x.SKU_ID == tbl_HQ_SKUGroup.SKU_ID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_HQ_SKUGroupItem in tbl_HQ_SKUGroup.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_HQ_SKUGroupItem.Name)
                                {
                                    var value = tbl_HQ_SKUGroupItem.GetValue(tbl_HQ_SKUGroup, null);

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
                        ret = tbl_HQ_SKUGroup.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_SKUGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_HQ_SKUGroup"></param>
        /// <returns></returns>
        public static int Delete(this tbl_HQ_SKUGroup tbl_HQ_SKUGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_HQ_SKUGroup).State = EntityState.Deleted;
                    db.tbl_HQ_SKUGroup.Remove(tbl_HQ_SKUGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_SKUGroup.GetType());
            }

            return ret;
        }


    }
}
