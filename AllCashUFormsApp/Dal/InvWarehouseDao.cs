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
        public static List<tbl_InvWarehouse> Select(this tbl_InvWarehouse tbl_InvWarehouse, string productID, string whID)
        {
            List<tbl_InvWarehouse> list = new List<tbl_InvWarehouse>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_InvWarehouse] ";
                sql += " WHERE ProductID = '"+ productID.Trim() + "' ";
                sql += " AND WHID = '" + whID.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_InvWarehouse), sql);
                list = dynamicListReturned.Cast<tbl_InvWarehouse>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvWarehouse.GetType());
            }

            return list;
        }

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
                list = tbl_InvWarehouse.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_InvWarehouse.Where(predicate).AsQueryable().ToList();
                //}
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
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_InvWarehouse] WHERE FlagDel = 0 ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_InvWarehouse), sql);
                list = dynamicListReturned.Cast<tbl_InvWarehouse>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_InvWarehouse>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_InvWarehouse.ToList();
                //}
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
        public static void Insert(this tbl_InvWarehouse tbl_InvWarehouse, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.tbl_InvWarehouse.Attach(tbl_InvWarehouse);
                db.tbl_InvWarehouse.Add(tbl_InvWarehouse);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvWarehouse.GetType());
            }
        }

        public static void Insert(this List<tbl_InvWarehouse> tbl_InvWarehouses, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                foreach (var tbl_InvWarehouse in tbl_InvWarehouses)
                {
                    db.tbl_InvWarehouse.Attach(tbl_InvWarehouse);
                    db.tbl_InvWarehouse.Add(tbl_InvWarehouse);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }
        }

        public static int UpdateEntity(this List<tbl_InvWarehouse> tbl_InvWarehouses, DB_ALL_CASH_UNIEntities db, string docTypeCode = "")
        {
            int ret = 0;

            try
            {
                List<tbl_InvWarehouse> tbl_InvWarehouseList = new List<tbl_InvWarehouse>();
                foreach (var whid in tbl_InvWarehouses.Select(x => x.WHID).Distinct().ToList())
                {
                    tbl_InvWarehouseList.AddRange(db.tbl_InvWarehouse.Where(x => x.WHID == whid).ToList());
                }

                if (tbl_InvWarehouseList.Count > 0)
                {
                    foreach (var tbl_InvWarehouse in tbl_InvWarehouses)
                    {
                        var updateData = tbl_InvWarehouseList.FirstOrDefault(x => x.ProductID == tbl_InvWarehouse.ProductID && x.WHID == tbl_InvWarehouse.WHID);// db.tbl_InvWarehouse.FirstOrDefault(x => x.ProductID == tbl_InvWarehouse.ProductID && x.WHID == tbl_InvWarehouse.WHID);
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
                        }
                        else
                        {
                            //tbl_InvWarehouse.Delete(db);
                            tbl_InvWarehouse.Insert(db);
                        }
                    }
                }
                else
                    tbl_InvWarehouses.Insert(db);

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            return ret;
        }


        public static int Update(this List<tbl_InvWarehouse> tbl_InvWarehouses)
        {
            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_InvWarehouse in tbl_InvWarehouses)
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
                        }
                        else
                        {
                            tbl_InvWarehouse.Delete(db);
                            tbl_InvWarehouse.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //ex.WriteLog(tbl_InvWarehouse);
            }

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_InvWarehouse"></param>
        /// <returns></returns>
        public static void Delete(this tbl_InvWarehouse tbl_InvWarehouse, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.Entry(tbl_InvWarehouse).State = EntityState.Deleted;
                db.tbl_InvWarehouse.Remove(tbl_InvWarehouse);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvWarehouse.GetType());
            }
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