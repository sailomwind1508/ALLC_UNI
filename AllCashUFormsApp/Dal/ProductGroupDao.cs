using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ProductGroupDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_ProductGroup"></param>
        /// <returns></returns>
        public static List<tbl_ProductGroup> Select(this tbl_ProductGroup tbl_ProductGroup, Func<tbl_ProductGroup, bool> predicate)
        {
            List<tbl_ProductGroup> list = new List<tbl_ProductGroup>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ProductGroup.Where(x => x.FlagDel == false).Where(predicate).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductGroup.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ProductGroup"></param>
        /// <returns></returns>
        public static List<tbl_ProductGroup> SelectAll(this tbl_ProductGroup tbl_ProductGroup)
        {
            List<tbl_ProductGroup> list = new List<tbl_ProductGroup>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ProductGroup.Where(x => x.FlagDel == false).OrderBy(x => x.ProductGroupID).ToList();
                }
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ProductGroup.GetType());
            }

            return list;
        }


        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ProductGroup"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ProductGroup tbl_ProductGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ProductGroup.Attach(tbl_ProductGroup);
                    db.tbl_ProductGroup.Add(tbl_ProductGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ProductGroup"></param>
        /// <returns></returns>
        public static int Update(this tbl_ProductGroup tbl_ProductGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ProductGroup.FirstOrDefault(x => x.ProductGroupID == tbl_ProductGroup.ProductGroupID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ProductGroupItem in tbl_ProductGroup.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ProductGroupItem.Name)
                                {
                                    var value = tbl_ProductGroupItem.GetValue(tbl_ProductGroup, null);

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
                ex.WriteLog(tbl_ProductGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ProductGroup"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ProductGroup tbl_ProductGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ProductGroup).State = EntityState.Deleted;
                    db.tbl_ProductGroup.Remove(tbl_ProductGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductGroup.GetType());
            }

            return ret;
        }
    }
}
