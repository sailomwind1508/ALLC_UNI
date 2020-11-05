using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ProductUomDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_ProductUom"></param>
        /// <returns></returns>
        public static List<tbl_ProductUom> Select(this tbl_ProductUom tbl_ProductUom, Func<tbl_ProductUom, bool> predicate)
        {
            List<tbl_ProductUom> list = new List<tbl_ProductUom>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ProductUom.Where(x => x.FlagDel == false).Where(predicate).OrderBy(x => x.ProductUomID).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUom.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ProductUom"></param>
        /// <returns></returns>
        public static List<tbl_ProductUom> SelectAll(this tbl_ProductUom tbl_ProductUom)
        {
            List<tbl_ProductUom> list = new List<tbl_ProductUom>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ProductUom.Where(x => x.FlagDel == false).OrderBy(x => x.ProductUomID).ToList();
                }
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ProductUom.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ProductUom"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ProductUom tbl_ProductUom)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ProductUom.Attach(tbl_ProductUom);
                    db.tbl_ProductUom.Add(tbl_ProductUom);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUom.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ProductUom"></param>
        /// <returns></returns>
        public static int Update(this tbl_ProductUom tbl_ProductUom)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ProductUom.FirstOrDefault(x => x.ProductUomID == tbl_ProductUom.ProductUomID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ProductUomItem in tbl_ProductUom.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ProductUomItem.Name)
                                {
                                    var value = tbl_ProductUomItem.GetValue(tbl_ProductUom, null);

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
                ex.WriteLog(tbl_ProductUom.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ProductUom"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ProductUom tbl_ProductUom)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ProductUom).State = EntityState.Deleted;
                    db.tbl_ProductUom.Remove(tbl_ProductUom);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUom.GetType());
            }

            return ret;
        }
    }
}
