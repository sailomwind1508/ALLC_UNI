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
    public static class PRDetailDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static List<tbl_PRDetail> Select(this tbl_PRDetail tbl_PRDetail, string docNo)
        {
            List<tbl_PRDetail> list = new List<tbl_PRDetail>();
            try
            {
                if (!string.IsNullOrEmpty(docNo))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT t1.* ";
                    sql += " FROM [dbo].[tbl_PRDetail] t1 ";
                    sql += " INNER JOIN dbo.tbl_PRMaster t2 ON t1.DocNo = t2.DocNo ";
                    sql += " WHERE t1.FlagDel = 0 ";
                    sql += " AND t2.DocNo = '" + docNo.Trim() + "' ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PRDetail), sql);
                    list = dynamicListReturned.Cast<tbl_PRDetail>().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static List<tbl_PRDetail> Select(this tbl_PRDetail tbl_PRDetail, string sqlFilter, string docTypeCode = "")
        {
            List<tbl_PRDetail> list = new List<tbl_PRDetail>();
            try
            {
                if (!string.IsNullOrEmpty(sqlFilter) && !string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT t1.* ";
                    sql += " FROM [dbo].[tbl_PRDetail] t1 ";
                    sql += " INNER JOIN dbo.tbl_PRMaster t2 ON t1.DocNo = t2.DocNo ";
                    sql += " LEFT JOIN dbo.tbl_Product t3 ON t1.ProductID = t3.ProductID ";
                    sql += " WHERE t1.FlagDel = 0 ";
                    sql += " AND " + sqlFilter;
                    sql += " AND t2.DocTypeCode = '" + docTypeCode.Trim() + "' ";
                    sql += " ORDER BY t3.ProductGroupID, t3.ProductSubGroupID, t3.Seq"; //last edit by sailom 25/08/2021

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PRDetail), sql);
                    list = dynamicListReturned.Cast<tbl_PRDetail>().ToList();
                }

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PRDetail.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static List<tbl_PRDetail> Select(this tbl_PRDetail tbl_PRDetail, Func<tbl_PRDetail, bool> predicate, string docTypeCode = "")
        {
            List<tbl_PRDetail> list = new List<tbl_PRDetail>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT t1.* ";
                    sql += " FROM [dbo].[tbl_PRDetail] t1 ";
                    sql += " INNER JOIN dbo.tbl_PRMaster t2 ON t1.DocNo = t2.DocNo ";
                    sql += " WHERE t1.FlagDel = 0 ";
                    sql += " AND t2.DocTypeCode = '" + docTypeCode.Trim() + "' ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PRDetail), sql);
                    list = dynamicListReturned.Cast<tbl_PRDetail>().ToList();
                    list = list.Where(predicate).ToList();
                }
                else
                    list = tbl_PRDetail.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PRDetail.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static List<tbl_PRDetail> SelectAll(this tbl_PRDetail tbl_PRDetail, string docTypeCode = "")
        {
            List<tbl_PRDetail> list = new List<tbl_PRDetail>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_PRDetail] WHERE FlagDel = 0 ";

                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    sql = "";
                    sql += " SELECT t1.* ";
                    sql += " FROM [dbo].[tbl_PRDetail] t1 ";
                    sql += " INNER JOIN dbo.tbl_PRMaster t2 ON t1.DocNo = t2.DocNo ";
                    sql += " WHERE t1.FlagDel = 0 ";
                    sql += " AND t2.DocTypeCode = '" + docTypeCode.Trim() + "' ";
                }

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PRDetail), sql);
                list = dynamicListReturned.Cast<tbl_PRDetail>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_PRDetail>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PRDetail.OrderBy(x => x.DocNo).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static void Insert(this tbl_PRDetail tbl_PRDetail, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PRDetailDao=>InsertWithDB";
            msg.WriteLog(null);

            try
            {
                db.tbl_PRDetail.Attach(tbl_PRDetail);
                db.tbl_PRDetail.Add(tbl_PRDetail);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            msg = "end PRDetailDao=>InsertWithDB";
            msg.WriteLog(null);
        }

        public static void Insert(this List<tbl_PRDetail> tbl_PRDetails, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PRDetailDao=>InsertListWithDB";
            msg.WriteLog(null);

            try
            {
                foreach (var tbl_PRDetail in tbl_PRDetails)
                {
                    db.tbl_PRDetail.Attach(tbl_PRDetail);
                    db.tbl_PRDetail.Add(tbl_PRDetail);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }

            msg = "end PRDetailDao=>InsertListWithDB";
            msg.WriteLog(null);
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static int Insert(this tbl_PRDetail tbl_PRDetail)
        {
            string msg = "start PRDetailDao=>Insert";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_PRDetail.Attach(tbl_PRDetail);
                    db.tbl_PRDetail.Add(tbl_PRDetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            msg = "end PRDetailDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        public static int UpdateEntity(this List<tbl_PRDetail> tbl_PRDetails, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PRDetailDao=>UpdateEntity";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var docNo = tbl_PRDetails.First().DocNo;
                var tbl_PRDetailList = db.tbl_PRDetail.Where(x => x.DocNo == docNo).ToList();

                if (tbl_PRDetailList.Count > 0)
                {
                    foreach (var tbl_PRDetail in tbl_PRDetails)
                    {
                        var updateData = tbl_PRDetailList.FirstOrDefault(x => x.DocNo == tbl_PRDetail.DocNo && x.ProductID == tbl_PRDetail.ProductID);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_PRDetailItem in tbl_PRDetail.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name == tbl_PRDetailItem.Name)
                                    {
                                        var value = tbl_PRDetailItem.GetValue(tbl_PRDetail, null);

                                        Type t = Nullable.GetUnderlyingType(updateDataItem.PropertyType) ?? updateDataItem.PropertyType;
                                        object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                                        updateDataItem.SetValue(updateData, safeValue, null);
                                    }
                                }
                            }

                            //db.up (tbl_PRDetails);
                            db.Entry(updateData).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            //tbl_PRDetail.Delete(db);
                            tbl_PRDetail.Insert(db);
                        }

                    }
                }
                else
                {
                    tbl_PRDetails.Insert(db);
                }

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            msg = "end PRDetailDao=>UpdateEntity";
            msg.WriteLog(null);

            return ret;
        }

        public static int Update(this List<tbl_PRDetail> tbl_PRDetails)
        {
            string msg = "start PRDetailDao=>UpdateList";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_PRDetail in tbl_PRDetails)
                    {
                        var updateData = db.tbl_PRDetail.FirstOrDefault(x => x.DocNo == tbl_PRDetail.DocNo && x.ProductID == tbl_PRDetail.ProductID);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_PRDetailItem in tbl_PRDetail.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name == tbl_PRDetailItem.Name)
                                    {
                                        var value = tbl_PRDetailItem.GetValue(tbl_PRDetail, null);

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
                            tbl_PRDetail.Delete(db);
                            tbl_PRDetail.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
                //ex.WriteLog(tbl_PRDetail);
            }

            msg = "end PRDetailDao=>UpdateList";
            msg.WriteLog(null);

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static int Update(this tbl_PRDetail tbl_PRDetail)
        {
            string msg = "start PRDetailDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_PRDetail.FirstOrDefault(x => x.DocNo == tbl_PRDetail.DocNo && x.ProductID == tbl_PRDetail.ProductID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PRDetailItem in tbl_PRDetail.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_PRDetailItem.Name)
                                {
                                    var value = tbl_PRDetailItem.GetValue(tbl_PRDetail, null);

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
                        ret = tbl_PRDetail.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            msg = "end PRDetailDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static void Delete(this tbl_PRDetail tbl_PRDetail, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PRDetailDao=>DeleteWithDB";
            msg.WriteLog(null);

            try
            {
                db.Entry(tbl_PRDetail).State = EntityState.Deleted;
                db.tbl_PRDetail.Remove(tbl_PRDetail);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            msg = "end PRDetailDao=>DeleteWithDB";
            msg.WriteLog(null);
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static int Delete(this tbl_PRDetail tbl_PRDetail)
        {
            string msg = "start PRDetailDao=>Delete";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_PRDetail).State = EntityState.Deleted;
                    db.tbl_PRDetail.Remove(tbl_PRDetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            msg = "end PRDetailDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }


        public static int BulkInsert(this List<tbl_PRDetail> tbl_PRDetails)
        {
            string msg = "start PRDetailDao=>BulkInsert";
            msg.WriteLog(null);

            int ret = 1;

            var table = tbl_PRDetails.ToDataTable();
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
                                bcp.DestinationTableName = "tbl_PRDetail";
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

            msg = "end PRDetailDao=>BulkInsert";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkUpdate(this List<tbl_PRDetail> tbl_PRDetails)
        {
            string msg = "start PRDetailDao=>BulkUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                //var table = tbl_PRDetails.ToDataTable();
                //if (table != null && table.Rows.Count > 0)
                //{
                //    using (var conn = new SqlConnection(Connection.ConnectionString))
                //    {
                //        if (conn.State == ConnectionState.Closed)
                //        {
                //            conn.Open();
                //        }

                //        string sql = " SELECT * FROM tbl_PRDetail ";
                //        var cmd = new SqlCommand(sql, conn);
                //        var ad = new SqlDataAdapter(cmd);
                //        SqlCommandBuilder cmdb = new SqlCommandBuilder(ad);
                //        ad.Update(table);
                //        table.AcceptChanges();

                //        ret = 1;
                //    }
                //}

                string sql = " DELETE FROM tbl_PRDetail WHERE DocNo = '" + tbl_PRDetails.FirstOrDefault().DocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////

                ret = tbl_PRDetails.BulkInsert();
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }

            msg = "end PRDetailDao=>BulkUpdate";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkRemove(this List<tbl_PRDetail> tbl_PRDetails)
        {
            string msg = "start tbl_PRDetail=>BulkRemove";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                string sql = " DELETE FROM tbl_PRDetail WHERE DocNo = '" + tbl_PRDetails.FirstOrDefault().DocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////
                ret = 1;
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end tbl_PRDetail=>BulkRemove";
            msg.WriteLog(null);

            return ret;
        }

        public static int PerformUpdate(this List<tbl_PRDetail> tbl_PRDetails, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PRDetailDao=>PerformUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var updateData = new tbl_PRDetail();
                var docNo = tbl_PRDetails.First().DocNo;
                updateData = db.tbl_PRDetail.FirstOrDefault(x => x.DocNo == docNo);

                if (updateData != null)
                {
                    ret = tbl_PRDetails.BulkUpdate();
                }
                else
                {
                    ret = tbl_PRDetails.BulkInsert();
                }
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }

            msg = "end PRDetailDao=>PerformUpdate";
            msg.WriteLog(null);

            return ret;
        }

        public static List<tbl_PRDetail> GetPRDetail_AllBranch(this tbl_PRDetail tbl_PRDetail, Dictionary<string, object> Params)
        {
            List<tbl_PRDetail> list = new List<tbl_PRDetail>();
            try
            {
                string sql = "proc_PRDetail_GetData_AllBranch";
                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteStoreToList(typeof(tbl_PRDetail), sql, Params);
                list = dynamicListReturned.Cast<tbl_PRDetail>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            return list;
        }
    }
}
