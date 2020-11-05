using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class InvWarehouseDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_InvWarehouse"></param>
        /// <returns></returns>
        public static List<tbl_InvWarehouse> Select(this tbl_InvWarehouse tbl_InvWarehouse, Func<tbl_InvWarehouse, bool> predicate)
        {
            List<tbl_InvWarehouse> list = new List<tbl_InvWarehouse>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_InvWarehouse.Where(predicate).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvWarehouse.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_InvWarehouse"></param>
        /// <returns></returns>
        public static List<tbl_InvWarehouse> SelectAll(this tbl_InvWarehouse tbl_InvWarehouse)
        {
            List<tbl_InvWarehouse> list = new List<tbl_InvWarehouse>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_InvWarehouse.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvWarehouse.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_InvWarehouse"></param>
        /// <returns></returns>
        public static int Insert(this tbl_InvWarehouse tbl_InvWarehouse)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_InvWarehouse.Attach(tbl_InvWarehouse);
                    db.tbl_InvWarehouse.Add(tbl_InvWarehouse);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvWarehouse.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_InvWarehouse"></param>
        /// <returns></returns>
        public static int Update(this tbl_InvWarehouse tbl_InvWarehouse)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_InvWarehouse.FirstOrDefault(x => x.ProductID == tbl_InvWarehouse.ProductID && x.WHID == tbl_InvWarehouse.WHID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_InvWarehouseItem in tbl_InvWarehouse.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_InvWarehouseItem.Name)
                                {
                                    var value = tbl_InvWarehouseItem.GetValue(tbl_InvWarehouse, null);

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
                        ret = tbl_InvWarehouse.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvWarehouse.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_InvWarehouse"></param>
        /// <returns></returns>
        public static int Update(this tbl_InvWarehouse tbl_InvWarehouse, string WHID, string docTypeCode)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_InvWarehouse.FirstOrDefault(x => x.ProductID == tbl_InvWarehouse.ProductID && x.WHID == WHID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_InvWarehouseItem in tbl_InvWarehouse.GetType().GetProperties())
                            {
                                object value = null;
                                if (updateDataItem.Name == tbl_InvWarehouseItem.Name)
                                {
                                    if (docTypeCode == "RL")
                                    {
                                        if (updateDataItem.Name.ToLower() == "whid")
                                        {
                                            value = WHID;
                                        }
                                        else if (updateDataItem.Name.ToLower() == "qtyonhand")
                                        {
                                            var QtyOnHand = tbl_InvWarehouseItem.GetValue(tbl_InvWarehouse, null);

                                            if (WHID.Contains("1000"))
                                            {
                                                value = updateData.QtyOnHand - Convert.ToDecimal(QtyOnHand);
                                            }
                                            else if (WHID.Contains("V"))
                                                value = Convert.ToDecimal(QtyOnHand);
                                        }
                                        else
                                        {
                                            value = tbl_InvWarehouseItem.GetValue(tbl_InvWarehouse, null);
                                        }
                                    }
                                    else
                                    {
                                        value = tbl_InvWarehouseItem.GetValue(tbl_InvWarehouse, null);
                                    }

                                    Type t = Nullable.GetUnderlyingType(updateDataItem.PropertyType) ?? updateDataItem.PropertyType;
                                    object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                                    updateDataItem.SetValue(updateData, safeValue, null);
                                }

                                
                            }
                        }
                        ret = db.SaveChanges();
                    }
                    else
                    {
                        ret = tbl_InvWarehouse.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvWarehouse.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_InvWarehouse"></param>
        /// <returns></returns>
        public static int Delete(this tbl_InvWarehouse tbl_InvWarehouse)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_InvWarehouse).State = EntityState.Deleted;
                    db.tbl_InvWarehouse.Remove(tbl_InvWarehouse);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvWarehouse.GetType());
            }

            return ret;
        }
    }
}