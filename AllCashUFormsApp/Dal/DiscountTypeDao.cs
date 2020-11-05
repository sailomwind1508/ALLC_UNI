using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class DiscountTypeDao
    {
        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_DiscountType"></param>
        /// <returns></returns>
        public static List<tbl_DiscountType> SelectAll(this tbl_DiscountType tbl_DiscountType)
        {
            List<tbl_DiscountType> list = new List<tbl_DiscountType>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_DiscountType.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DiscountType.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_DiscountType"></param>
        /// <returns></returns>
        public static int Insert(this tbl_DiscountType tbl_DiscountType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_DiscountType.Attach(tbl_DiscountType);
                    db.tbl_DiscountType.Add(tbl_DiscountType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DiscountType.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_DiscountType"></param>
        /// <returns></returns>
        public static int Update(this tbl_DiscountType tbl_DiscountType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_DiscountType.FirstOrDefault(x => x.DiscountTypeCode == tbl_DiscountType.DiscountTypeCode);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_DiscountTypeItem in tbl_DiscountType.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_DiscountTypeItem.Name)
                                {
                                    var value = tbl_DiscountTypeItem.GetValue(tbl_DiscountType, null);

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
                ex.WriteLog(tbl_DiscountType.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_DiscountType"></param>
        /// <returns></returns>
        public static int Delete(this tbl_DiscountType tbl_DiscountType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_DiscountType).State = EntityState.Deleted;
                    db.tbl_DiscountType.Remove(tbl_DiscountType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DiscountType.GetType());
            }

            return ret;
        }
    }
}
