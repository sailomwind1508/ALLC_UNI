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
    public static class InvMovementDao
    {
        public static List<tbl_InvMovement> SelectStock(this tbl_InvMovement tbl_InvMovement, string productID, string whid)
        {
            List<tbl_InvMovement> list = new List<tbl_InvMovement>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_InvMovement] ";
                sql += " WHERE TrnType <> 'X' AND ProductID = '" + productID.Trim() + "' AND WHID = '" + whid.Trim() + "'";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_InvMovement), sql);
                list = dynamicListReturned.Cast<tbl_InvMovement>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return list;
        }

        public static List<tbl_InvMovement> Select(this tbl_InvMovement tbl_InvMovement, string docNo)
        {
            List<tbl_InvMovement> list = new List<tbl_InvMovement>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_InvMovement] ";
                sql += " WHERE RefDocNo = '" + docNo.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_InvMovement), sql);
                list = dynamicListReturned.Cast<tbl_InvMovement>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static List<tbl_InvMovement> Select(this tbl_InvMovement tbl_InvMovement, Func<tbl_InvMovement, bool> predicate, string docTypeCode = "")
        {
            List<tbl_InvMovement> list = new List<tbl_InvMovement>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * ";
                    sql += " FROM [dbo].[tbl_InvMovement] ";
                    sql += " WHERE DocTypeCode = '" + docTypeCode.Trim() + "' ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_InvMovement), sql);
                    list = dynamicListReturned.Cast<tbl_InvMovement>().ToList();
                }
                else
                    list = tbl_InvMovement.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_InvMovement.Where(predicate).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return list;
        }


        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static DataTable SelectForMovement(this tbl_InvMovement tbl_InvMovement, string docTypeCode = "")
        {
            DataTable dt = new DataTable();
            try
            {
                ///////////////////////////////////////////////////////////////////
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    
                    string sql = "";
                    sql += " SELECT TrnDate, TrnType, RefDocNo, WHID, ToWHID, ProductID, ProductName, TrnQtyIn, TrnQtyOut, TrnQty, CrDate  ";
                    sql += " FROM [dbo].[tbl_InvMovement] ";
                    sql += " WHERE DocTypeCode = '" + docTypeCode.Trim() + "' AND TrnType <> 'X' ";

                    dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                }
                else
                    dt = tbl_InvMovement.SelectAllForMovement();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return dt;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static DataTable SelectAllForMovement(this tbl_InvMovement tbl_InvMovement)
        {
            DataTable dt = new DataTable();
            try
            {
                
                string sql = "";
                sql += " SELECT TrnDate, TrnType, RefDocNo, WHID, ToWHID, ProductID, ProductName, TrnQtyIn, TrnQtyOut, TrnQty, CrDate  ";
                sql += " FROM [dbo].[tbl_InvMovement] WHERE TrnType <> 'X' ";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return dt;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static List<tbl_InvMovement> SelectAll(this tbl_InvMovement tbl_InvMovement)
        {
            List<tbl_InvMovement> list = new List<tbl_InvMovement>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_InvMovement] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_InvMovement), sql);
                list = dynamicListReturned.Cast<tbl_InvMovement>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_InvMovement>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_InvMovement.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static void Insert(this tbl_InvMovement tbl_InvMovement, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.tbl_InvMovement.Attach(tbl_InvMovement);
                db.tbl_InvMovement.Add(tbl_InvMovement);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }
        }

        public static void Insert(this List<tbl_InvMovement> tbl_InvMovements, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                foreach (var tbl_InvMovement in tbl_InvMovements)
                {
                    db.tbl_InvMovement.Attach(tbl_InvMovement);
                    db.tbl_InvMovement.Add(tbl_InvMovement);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }
        }

        public static int UpdateEntity(this List<tbl_InvMovement> tbl_InvMovements, DB_ALL_CASH_UNIEntities db, string docTypeCode = "")
        {
            int ret = 0;

            try
            {
                //var tbl_InvMovementList = db.tbl_InvMovement.Where(x => tbl_InvMovements.Select(a => a.TransactionID).Contains(x.TransactionID)).ToList();
                var refDocNo = tbl_InvMovements.First().RefDocNo;
                var tbl_InvMovementList = db.tbl_InvMovement.Where(x => x.RefDocNo == refDocNo).ToList();

                if (tbl_InvMovementList.Count > 0)
                {
                    foreach (var tbl_InvMovement in tbl_InvMovements)
                    {
                        var updateData = tbl_InvMovementList.FirstOrDefault(x => x.TransactionID == tbl_InvMovement.TransactionID); // db.tbl_InvMovement.FirstOrDefault(x => x.TransactionID == tbl_InvMovement.TransactionID);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_InvMovementItem in tbl_InvMovement.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "transactonid" && updateDataItem.Name == tbl_InvMovementItem.Name)
                                    {
                                        var value = tbl_InvMovementItem.GetValue(tbl_InvMovement, null);

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
                            tbl_InvMovement.Insert(db);
                        }
                    }
                }
                else
                    tbl_InvMovements.Insert(db);

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            return ret;
        }

        public static int Update(this List<tbl_InvMovement> tbl_InvMovements)
        {
            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_InvMovement in tbl_InvMovements)
                    {
                        var updateData = db.tbl_InvMovement.FirstOrDefault(x => x.TransactionID == tbl_InvMovement.TransactionID);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_InvMovementItem in tbl_InvMovement.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "transactonid" && updateDataItem.Name == tbl_InvMovementItem.Name)
                                    {
                                        var value = tbl_InvMovementItem.GetValue(tbl_InvMovement, null);

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
                            tbl_InvMovement.Delete(db);
                            tbl_InvMovement.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //ex.WriteLog(tbl_InvMovement);
            }

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static void Delete(this tbl_InvMovement tbl_InvMovement, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.Entry(tbl_InvMovement).State = EntityState.Deleted;
                db.tbl_InvMovement.Remove(tbl_InvMovement);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static int Insert(this tbl_InvMovement tbl_InvMovement)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_InvMovement.Attach(tbl_InvMovement);
                    db.tbl_InvMovement.Add(tbl_InvMovement);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static int Update(this tbl_InvMovement tbl_InvMovement)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_InvMovement.FirstOrDefault(x => x.TransactionID == tbl_InvMovement.TransactionID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_InvMovementItem in tbl_InvMovement.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "transactonid" && updateDataItem.Name == tbl_InvMovementItem.Name)
                                {
                                    var value = tbl_InvMovementItem.GetValue(tbl_InvMovement, null);

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
                        ret = tbl_InvMovement.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return ret;
        }


        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static int Delete(this tbl_InvMovement tbl_InvMovement)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_InvMovement).State = EntityState.Deleted;
                    //db.tbl_InvMovement.Attach(tbl_InvMovement);
                    db.tbl_InvMovement.Remove(tbl_InvMovement);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return ret;
        }
    }
}
