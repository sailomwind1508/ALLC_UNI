using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ApSupplierTypeDao
    {
        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ApSupplierType"></param>
        /// <returns></returns>
        public static List<tbl_ApSupplierType> SelectAll(this tbl_ApSupplierType tbl_ApSupplierType)
        {
            List<tbl_ApSupplierType> list = new List<tbl_ApSupplierType>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ApSupplierType.Where(x => x.FlagDel == false).OrderBy(x => x.ApSupplierTypeCode).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplierType.GetType());
               
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ApSupplierType"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ApSupplierType tbl_ApSupplierType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ApSupplierType.Attach(tbl_ApSupplierType);
                    db.tbl_ApSupplierType.Add(tbl_ApSupplierType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplierType.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ApSupplierType"></param>
        /// <returns></returns>
        public static int Update(this tbl_ApSupplierType tbl_ApSupplierType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ApSupplierType.FirstOrDefault(x => x.APSupplierTypeID == tbl_ApSupplierType.APSupplierTypeID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ApSupplierTypeItem in tbl_ApSupplierType.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ApSupplierTypeItem.Name)
                                {
                                    var value = tbl_ApSupplierTypeItem.GetValue(tbl_ApSupplierType, null);

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
                ex.WriteLog(tbl_ApSupplierType.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ApSupplierType"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ApSupplierType tbl_ApSupplierType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ApSupplierType).State = EntityState.Deleted;
                    db.tbl_ApSupplierType.Remove(tbl_ApSupplierType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplierType.GetType());
            }

            return ret;
        }
    }
}
