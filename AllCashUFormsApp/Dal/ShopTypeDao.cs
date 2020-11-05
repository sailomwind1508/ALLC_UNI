using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ShopTypeDao
    {
        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ShopType"></param>
        /// <returns></returns>
        public static List<tbl_ShopType> SelectAll(this tbl_ShopType tbl_ShopType)
        {
            List<tbl_ShopType> list = new List<tbl_ShopType>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ShopType.Where(x => x.FlagDel == false).OrderBy(x => x.ShopTypeID).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ShopType.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ShopType"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ShopType tbl_ShopType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ShopType.Attach(tbl_ShopType);
                    db.tbl_ShopType.Add(tbl_ShopType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ShopType.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ShopType"></param>
        /// <returns></returns>
        public static int Update(this tbl_ShopType tbl_ShopType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ShopType.FirstOrDefault(x => x.ShopTypeID == tbl_ShopType.ShopTypeID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ShopTypeItem in tbl_ShopType.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ShopTypeItem.Name)
                                {
                                    var value = tbl_ShopTypeItem.GetValue(tbl_ShopType, null);

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
                ex.WriteLog(tbl_ShopType.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ShopType"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ShopType tbl_ShopType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ShopType).State = EntityState.Deleted;
                    db.tbl_ShopType.Remove(tbl_ShopType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ShopType.GetType());
            }

            return ret;
        }
    }
}
