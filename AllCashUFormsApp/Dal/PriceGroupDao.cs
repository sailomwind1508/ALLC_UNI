using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;


namespace AllCashUFormsApp
{
    public static class PriceGroupDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PriceGroup"></param>
        /// <returns></returns>
        public static List<tbl_PriceGroup> Select(this tbl_PriceGroup tbl_PriceGroup, Func<tbl_PriceGroup, bool> predicate)
        {
            List<tbl_PriceGroup> list = new List<tbl_PriceGroup>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_PriceGroup.Where(x => x.FlagDel == false).Where(predicate).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PriceGroup.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_PriceGroup"></param>
        /// <returns></returns>
        public static List<tbl_PriceGroup> SelectAll(this tbl_PriceGroup tbl_PriceGroup)
        {
            List<tbl_PriceGroup> list = new List<tbl_PriceGroup>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_PriceGroup.Where(x => x.FlagDel == false).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PriceGroup.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PriceGroup"></param>
        /// <returns></returns>
        public static int Insert(this tbl_PriceGroup tbl_PriceGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_PriceGroup.Attach(tbl_PriceGroup);
                    db.tbl_PriceGroup.Add(tbl_PriceGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PriceGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_PriceGroup"></param>
        /// <returns></returns>
        public static int Update(this tbl_PriceGroup tbl_PriceGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_PriceGroup.FirstOrDefault(x => x.PriceGroupID == tbl_PriceGroup.PriceGroupID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PriceGroupItem in tbl_PriceGroup.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_PriceGroupItem.Name)
                                {
                                    var value = tbl_PriceGroupItem.GetValue(tbl_PriceGroup, null);

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
                ex.WriteLog(tbl_PriceGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PriceGroup"></param>
        /// <returns></returns>
        public static int Delete(this tbl_PriceGroup tbl_PriceGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_PriceGroup).State = EntityState.Deleted;
                    db.tbl_PriceGroup.Remove(tbl_PriceGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PriceGroup.GetType());
            }

            return ret;
        }
    }
}
