using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class BranchWarehouseDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_BranchWarehouse"></param>
        /// <returns></returns>
        public static List<tbl_BranchWarehouse> Select(this tbl_BranchWarehouse tbl_BranchWarehouse, Func<tbl_BranchWarehouse, bool> predicate)
        {
            List<tbl_BranchWarehouse> list = new List<tbl_BranchWarehouse>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_BranchWarehouse.Where(predicate).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_BranchWarehouse"></param>
        /// <returns></returns>
        public static List<tbl_BranchWarehouse> SelectAll(this tbl_BranchWarehouse tbl_BranchWarehouse)
        {
            List<tbl_BranchWarehouse> list = new List<tbl_BranchWarehouse>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_BranchWarehouse.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_BranchWarehouse"></param>
        /// <returns></returns>
        public static int Insert(this tbl_BranchWarehouse tbl_BranchWarehouse)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_BranchWarehouse.Attach(tbl_BranchWarehouse);
                    db.tbl_BranchWarehouse.Add(tbl_BranchWarehouse);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_BranchWarehouse"></param>
        /// <returns></returns>
        public static int Update(this tbl_BranchWarehouse tbl_BranchWarehouse)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_BranchWarehouse.FirstOrDefault(x => x.WHID == tbl_BranchWarehouse.WHID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_BranchWarehouseItem in tbl_BranchWarehouse.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_BranchWarehouseItem.Name)
                                {
                                    var value = tbl_BranchWarehouseItem.GetValue(tbl_BranchWarehouse, null);

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
                        ret = tbl_BranchWarehouse.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_BranchWarehouse"></param>
        /// <returns></returns>
        public static int Delete(this tbl_BranchWarehouse tbl_BranchWarehouse)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_BranchWarehouse).State = EntityState.Deleted;
                    db.tbl_BranchWarehouse.Remove(tbl_BranchWarehouse);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
            }

            return ret;
        }
    }
}
