using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ProductDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_Product"></param>
        /// <returns></returns>
        public static List<tbl_Product> Select(this tbl_Product tbl_Product, Func<tbl_Product, bool> predicate)
        {
            List<tbl_Product> list = new List<tbl_Product>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_Product.Where(x => x.FlagDel == false).Where(predicate).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Product"></param>
        /// <returns></returns>
        public static List<tbl_Product> SelectAll(this tbl_Product tbl_Product)
        {
            List<tbl_Product> list = new List<tbl_Product>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_Product.Where(x => x.FlagDel == false).ToList(); 
                }
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_Product.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_Product"></param>
        /// <returns></returns>
        public static int Insert(this tbl_Product tbl_Product)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_Product.Attach(tbl_Product);
                    db.tbl_Product.Add(tbl_Product);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Product"></param>
        /// <returns></returns>
        public static int Update(this tbl_Product tbl_Product)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_Product.FirstOrDefault(x => x.ProductID == tbl_Product.ProductID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ProductItem in tbl_Product.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ProductItem.Name)
                                {
                                    var value = tbl_ProductItem.GetValue(tbl_Product, null);

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
                ex.WriteLog(tbl_Product.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Product"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Product tbl_Product)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_Product).State = EntityState.Deleted;
                    db.tbl_Product.Remove(tbl_Product);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
            }

            return ret;
        }
    }
}
