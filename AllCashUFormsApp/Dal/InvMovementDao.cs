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
        public static List<tbl_InvMovement> ValidateStock(this tbl_InvMovement tbl_InvMovement)
        {
            List<tbl_InvMovement> list = new List<tbl_InvMovement>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT SUM(TrnQty) AS 'TrnQty', ProductID, WHID ";
                sql += " FROM dbo.tbl_InvMovement ";
                sql += " WHERE TrnType <> 'X'  ";
                sql += " GROUP BY ProductID, WHID ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_InvMovement), sql);

                list = dynamicListReturned.Cast<tbl_InvMovement>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return list;
        }

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

        public static List<tbl_InvMovement> SelectTotalStock(this tbl_InvMovement tbl_InvMovement, List<string> productIDs, string whid)
        {
            List<tbl_InvMovement> list = new List<tbl_InvMovement>();
            try
            {
                string prdIDs = "";
                int i = 0;
                foreach (var prd in productIDs)
                {
                    if (i == productIDs.Count - 1)
                        prdIDs += "'" + prd + "' ";
                    else
                        prdIDs += "'" + prd + "', ";

                    i++;
                }

                DataTable dt = new DataTable();
                //last edit by sailom .k 04/07/2022
                //string sql = "";
                //sql += " SELECT SUM(TrnQty) AS 'TrnQty', ProductID, WHID "; //edit by sailom 13/12/2021
                //sql += " FROM [dbo].[tbl_InvMovement] ";
                //sql += " WHERE TrnType <> 'X' AND ProductID IN (" + prdIDs + ") AND WHID = '" + whid.Trim() + "' GROUP BY ProductID, WHID ";

                string sql = " SELECT ISNULL(t2.TrnQty, 0) as 'TrnQty', t1.ProductID, '" + whid.Trim() + "' as WHID ";
                sql += " FROM dbo.tbl_Product t1 ";
                sql += " LEFT JOIN  ";
                sql += " (  ";
                sql += "    SELECT SUM(TrnQty)  AS 'TrnQty'  ";
                sql += "    , ProductID  ";
                sql += " , WHID    ";
                sql += " FROM [dbo].[tbl_InvMovement]   ";
                sql += " WHERE TrnType <> 'X'  ";
                //sql += " AND ProductID IN (" + prdIDs + ") AND WHID = '" + whid.Trim() + "' GROUP BY ProductID, WHID "; //last edit by sailom .k 18/07/2022
                sql += " AND WHID = '" + whid.Trim() + "' GROUP BY ProductID, WHID ";
                sql += " ) t2 ON t1.ProductID = t2.ProductID  ";

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

        public static DataTable SelectForMovement(this tbl_InvMovement tbl_InvMovement, string whid, List<string> prdIDList)
        {
            DataTable dt = new DataTable();
            try
            {
                string prdIds = "";
                int i = 0;
                foreach (var prdID in prdIDList)
                {
                    if (i == prdIDList.Count - 1)
                        prdIds += "'" + prdID + "' ";
                    else
                        prdIds += "'" + prdID + "', ";

                    i++;
                }

                string sql = "";
                sql += " SELECT TrnDate, TrnType, RefDocNo, WHID, ToWHID, ProductID, ProductName, TrnQtyIn, TrnQtyOut, TrnQty, CrDate  ";
                sql += " FROM [dbo].[tbl_InvMovement] ";
                sql += " WHERE TrnType <> 'X' ";

                if (prdIds != "")
                    sql += " AND ProductID IN (" + prdIds + ") ";
                if (whid != "-1")
                    sql += " AND WHID = '" + whid + "' ";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return dt;
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
            string msg = "start InvMovementDao=>InsertWithDB";
            msg.WriteLog(null);

            try
            {
                db.tbl_InvMovement.Attach(tbl_InvMovement);
                db.tbl_InvMovement.Add(tbl_InvMovement);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            msg = "end InvMovementDao=>InsertWithDB";
            msg.WriteLog(null);
        }

        public static void Insert(this List<tbl_InvMovement> tbl_InvMovements, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start InvMovementDao=>InsertListWithDB";
            msg.WriteLog(null);

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

            msg = "end InvMovementDao=>InsertListWithDB";
            msg.WriteLog(null);
        }

        public static int UpdateEntity(this List<tbl_InvMovement> tbl_InvMovements, DB_ALL_CASH_UNIEntities db, string docTypeCode = "")
        {
            string msg = "start InvMovementDao=>UpdateEntity";
            msg.WriteLog(null);

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

            msg = "end InvMovementDao=>UpdateEntity";
            msg.WriteLog(null);

            return ret;
        }

        public static int Update(this List<tbl_InvMovement> tbl_InvMovements)
        {
            string msg = "start InvMovementDao=>UpdateList";
            msg.WriteLog(null);

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
                ex.WriteLog(null);
                //ex.WriteLog(tbl_InvMovement);
            }

            msg = "end InvMovementDao=>UpdateList";
            msg.WriteLog(null);

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static void Delete(this tbl_InvMovement tbl_InvMovement, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start InvMovementDao=>DeleteWithDB";
            msg.WriteLog(null);

            try
            {
                db.Entry(tbl_InvMovement).State = EntityState.Deleted;
                db.tbl_InvMovement.Remove(tbl_InvMovement);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            msg = "end InvMovementDao=>DeleteWithDB";
            msg.WriteLog(null);
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static int Insert(this tbl_InvMovement tbl_InvMovement)
        {
            string msg = "start InvMovementDao=>Insert";
            msg.WriteLog(null);

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

            msg = "end InvMovementDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static int Update(this tbl_InvMovement tbl_InvMovement)
        {
            string msg = "start InvMovementDao=>Update";
            msg.WriteLog(null);

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

            msg = "end InvMovementDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static int Delete(this tbl_InvMovement tbl_InvMovement)
        {
            string msg = "start InvMovementDao=>Delete";
            msg.WriteLog(null);
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

            msg = "end InvMovementDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }


        public static int BulkInsert(this List<tbl_InvMovement> tbl_InvMovements)
        {
            string msg = "start tbl_InvMovement=>BulkInsert";
            msg.WriteLog(null);

            int ret = 1;

            var table = tbl_InvMovements.ToDataTable();
            if (table != null && table.Rows.Count > 0)
            {
                using (var conn = new SqlConnection(Connection.ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        using (SqlBulkCopy bcp = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, trans))
                        {
                            try
                            {
                                bcp.DestinationTableName = "tbl_InvMovement";
                                bcp.WriteToServer(table);
                                trans.Commit();
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                                conn.Close();
                                ret = 0;
                            }
                        }
                    }
                }
            }

            msg = "end tbl_InvMovement=>BulkInsert";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkUpdate(this List<tbl_InvMovement> tbl_InvMovements)
        {
            string msg = "start tbl_InvMovement=>BulkUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                //var table = tbl_InvMovements.ToDataTable();
                //if (table != null && table.Rows.Count > 0)
                //{
                //    using (var conn = new SqlConnection(Connection.ConnectionString))
                //    {
                //        if (conn.State == ConnectionState.Closed)
                //        {
                //            conn.Open();
                //        }

                //        string sql = " DELETE FROM tbl_InvMovement WHERE RefDocNo = '"+ tbl_InvMovements.FirstOrDefault().RefDocNo + "'; SELECT * FROM tbl_InvMovement ";
                //        var cmd = new SqlCommand(sql, conn);
                //        var ad = new SqlDataAdapter(cmd);
                //        SqlCommandBuilder cmdb = new SqlCommandBuilder(ad);
                //        ad.Update(table);
                //        table.AcceptChanges();

                //        ret = 1;
                //    }
                //}

                string sql = " DELETE FROM tbl_InvMovement WHERE RefDocNo = '" + tbl_InvMovements.FirstOrDefault().RefDocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////

                ret = tbl_InvMovements.BulkInsert();

            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end tbl_InvMovement=>UpdateList";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkRemove(this List<tbl_InvMovement> tbl_InvMovements)
        {
            string msg = "start tbl_InvMovement=>BulkRemove";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                string sql = " DELETE FROM tbl_InvMovement WHERE RefDocNo = '" + tbl_InvMovements.FirstOrDefault().RefDocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////
                ret = 1;
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end tbl_InvMovement=>BulkRemove";
            msg.WriteLog(null);

            return ret;
        }

        public static int PerformUpdate(this List<tbl_InvMovement> tbl_InvMovements, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start tbl_InvMovement=>PerformUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var updateData = new tbl_InvMovement();
                var docNo = tbl_InvMovements.First().RefDocNo;
                updateData = db.tbl_InvMovement.FirstOrDefault(x => x.RefDocNo == docNo);

                if (updateData != null)
                {
                    ret = tbl_InvMovements.BulkUpdate();
                }
                else
                {
                    ret = tbl_InvMovements.BulkInsert();
                }
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }

            msg = "end tbl_InvMovement=>PerformUpdate";
            msg.WriteLog(null);

            return ret;
        }

        public static int UpdateInvMovementData(this tbl_InvMovement tbl_InvMovement, Dictionary<string, object> _params)
        {
            int ret = 0;
            try
            {
                var dt = My_DataTable_Extensions.ExecuteStoreToDataTable("proc_Update_InvMovement_Data", _params);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ret = dt.Rows[0].Field<int>("Result");
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement != null ? tbl_InvMovement.GetType() : null);
            }
            return ret;
        }
    }
}
