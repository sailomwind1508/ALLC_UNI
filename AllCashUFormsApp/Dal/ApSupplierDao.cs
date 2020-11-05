using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ApSupplierDao
    {
        /// <summary>
        /// select data by criteria
        /// </summary>
        /// <param name="tbl_ApSupplier"></param>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static tbl_ApSupplier Select(this tbl_ApSupplier tbl_ApSupplier, string supplierID)
        {
            tbl_ApSupplier ret = new tbl_ApSupplier();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    ret = db.tbl_ApSupplier.Where(x => x.FlagDel == false).FirstOrDefault(x => x.SupplierID == supplierID);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplier.GetType());
            }

            return ret;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ApSupplier"></param>
        /// <returns></returns>
        public static List<tbl_ApSupplier> SelectAll(this tbl_ApSupplier tbl_ApSupplier)
        {
            List<tbl_ApSupplier> list = new List<tbl_ApSupplier>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ApSupplier.Where(x => x.FlagDel == false).OrderBy(x => x.SupplierCode).ToList();
                }
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ApSupplier.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ApSupplier"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ApSupplier tbl_ApSupplier)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ApSupplier.Attach(tbl_ApSupplier);
                    db.tbl_ApSupplier.Add(tbl_ApSupplier);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplier.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ApSupplier"></param>
        /// <returns></returns>
        public static int Update(this tbl_ApSupplier tbl_ApSupplier)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ApSupplier.FirstOrDefault(x => x.SupplierID == tbl_ApSupplier.SupplierID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ApSupplierItem in tbl_ApSupplier.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ApSupplierItem.Name)
                                {
                                    var value = tbl_ApSupplierItem.GetValue(tbl_ApSupplier, null);

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
                ex.WriteLog(tbl_ApSupplier.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ApSupplier"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ApSupplier tbl_ApSupplier)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ApSupplier).State = EntityState.Deleted;
                    db.tbl_ApSupplier.Remove(tbl_ApSupplier);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplier.GetType());
            }

            return ret;
        }
    }
}
