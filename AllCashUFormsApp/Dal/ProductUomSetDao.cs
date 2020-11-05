using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ProductUomSetDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_ProductUomSet"></param>
        /// <returns></returns>
        public static List<tbl_ProductUomSet> Select(this tbl_ProductUomSet tbl_ProductUomSet, Func<tbl_ProductUomSet, bool> predicate)
        {
            List<tbl_ProductUomSet> list = new List<tbl_ProductUomSet>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ProductUomSet.Where(x => x.FlagDel == false).Where(predicate).OrderBy(x => x.UomSetID).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUomSet.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ProductUomSet"></param>
        /// <returns></returns>
        public static List<tbl_ProductUomSet> SelectAll(this tbl_ProductUomSet tbl_ProductUomSet)
        {
            List<tbl_ProductUomSet> list = new List<tbl_ProductUomSet>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ProductUomSet.Where(x => x.FlagDel == false).OrderBy(x => x.UomSetID).ToList();
                }
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ProductUomSet.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ProductUomSet"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ProductUomSet tbl_ProductUomSet)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ProductUomSet.Attach(tbl_ProductUomSet);
                    db.tbl_ProductUomSet.Add(tbl_ProductUomSet);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUomSet.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ProductUomSet"></param>
        /// <returns></returns>
        public static int Update(this tbl_ProductUomSet tbl_ProductUomSet)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ProductUomSet.FirstOrDefault(x => x.ProductID == tbl_ProductUomSet.ProductID && x.UomSetID == tbl_ProductUomSet.UomSetID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ProductUomSetItem in tbl_ProductUomSet.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ProductUomSetItem.Name)
                                {
                                    var value = tbl_ProductUomSetItem.GetValue(tbl_ProductUomSet, null);

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
                ex.WriteLog(tbl_ProductUomSet.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ProductUomSet"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ProductUomSet tbl_ProductUomSet)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ProductUomSet).State = EntityState.Deleted;
                    db.tbl_ProductUomSet.Remove(tbl_ProductUomSet);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUomSet.GetType());
            }

            return ret;
        }
    }
}
