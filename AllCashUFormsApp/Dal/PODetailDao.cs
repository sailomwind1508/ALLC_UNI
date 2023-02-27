using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class PODetailDao
    {
        public static List<tbl_PODetail> Select(this tbl_PODetail tbl_PODetail, List<string> docNoList)
        {
            List<tbl_PODetail> list = new List<tbl_PODetail>();
            try
            {
                if (docNoList != null && docNoList.Count > 0)
                {
                    DataTable dt = new DataTable();
                    string docNos = "'" + string.Join("','", docNoList) + "'";

                    string sql = "";
                    sql += " SELECT t1.* ";
                    sql += " FROM [dbo].[tbl_PODetail] t1 ";
                    sql += " INNER JOIN dbo.tbl_POMaster t2 ON t1.DocNo = t2.DocNo ";
                    sql += " WHERE t1.FlagDel = 0 ";
                    sql += " AND t2.DocNo IN (" + docNos + ") ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PODetail), sql);
                    list = dynamicListReturned.Cast<tbl_PODetail>().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            return list;
        }

        public static List<tbl_PODetail> SelectPODTEndDay(this tbl_PODetail tbl_PODetail, DateTime docDate)
        {
            List<tbl_PODetail> list = new List<tbl_PODetail>();
            try
            {
                string _docDate = docDate.ToString("yyyyMMdd", new CultureInfo("en-US"));

                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT t1.* ";
                sql += " FROM [dbo].[tbl_PODetail] t1 ";
                sql += " INNER JOIN dbo.tbl_POMaster t2 ON t1.DocNo = t2.DocNo ";
                sql += " WHERE t1.FlagDel = 0 AND t2.DocStatus = '4' ";
                sql += " AND CAST(t2.DocDate AS DATE) = '" + _docDate + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PODetail), sql);
                list = dynamicListReturned.Cast<tbl_PODetail>().ToList();

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            return list;
        }

        public static List<tbl_PODetail> Select(this tbl_PODetail tbl_PODetail, DateTime docDate)
        {
            List<tbl_PODetail> list = new List<tbl_PODetail>();
            try
            {
                string _docDate = docDate.ToString("yyyyMMdd", new CultureInfo("en-US"));

                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT t1.* ";
                sql += " FROM [dbo].[tbl_PODetail] t1 ";
                sql += " INNER JOIN dbo.tbl_POMaster t2 ON t1.DocNo = t2.DocNo ";
                sql += " WHERE t1.FlagDel = 0 ";
                sql += " AND CAST(t2.DocDate AS DATE) = '" + _docDate + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PODetail), sql);
                list = dynamicListReturned.Cast<tbl_PODetail>().ToList();

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PODetail"></param>
        /// <returns></returns>
        public static List<tbl_PODetail> Select(this tbl_PODetail tbl_PODetail, string docNo)
        {
            List<tbl_PODetail> list = new List<tbl_PODetail>();
            try
            {
                if (!string.IsNullOrEmpty(docNo))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT t1.* ";
                    sql += " FROM [dbo].[tbl_PODetail] t1 ";
                    sql += " INNER JOIN dbo.tbl_POMaster t2 ON t1.DocNo = t2.DocNo ";
                    sql += " WHERE t1.FlagDel = 0 ";
                    sql += " AND t2.DocNo = '" + docNo.Trim() + "' ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PODetail), sql);
                    list = dynamicListReturned.Cast<tbl_PODetail>().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PODetail"></param>
        /// <returns></returns>
        public static List<tbl_PODetail> Select(this tbl_PODetail tbl_PODetail, Func<tbl_PODetail, bool> predicate, string docTypeCode = "", string docNo = "")
        {
            List<tbl_PODetail> list = new List<tbl_PODetail>();
            try
            {

                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT t1.* ";
                sql += " FROM [dbo].[tbl_PODetail] t1 ";
                sql += " INNER JOIN dbo.tbl_POMaster t2 ON t1.DocNo = t2.DocNo ";
                sql += " WHERE t1.FlagDel = 0 ";

                if (!string.IsNullOrEmpty(docTypeCode))
                    sql += " AND t2.DocTypeCode = '" + docTypeCode.Trim() + "' ";

                if (!string.IsNullOrEmpty(docNo))
                    sql += " AND t1.DocNo = '" + docNo.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PODetail), sql);
                list = dynamicListReturned.Cast<tbl_PODetail>().ToList();

                if (predicate != null)
                    list = list.Where(predicate).ToList();

                if (string.IsNullOrEmpty(docTypeCode) && string.IsNullOrEmpty(docNo))
                    list = tbl_PODetail.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PODetail.Where(predicate).AsQueryable().ToList();

                //    //list = db.tbl_PODetail.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_PODetail"></param>
        /// <returns></returns>
        public static List<tbl_PODetail> SelectAll(this tbl_PODetail tbl_PODetail, string docTypeCode = "")
        {
            List<tbl_PODetail> list = new List<tbl_PODetail>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM tbl_PODetail WHERE FlagDel = 0 ";

                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    sql = "";
                    sql += " SELECT t1.* ";
                    sql += " FROM [dbo].[tbl_PODetail] t1 ";
                    sql += " INNER JOIN dbo.tbl_POMaster t2 ON t1.DocNo = t2.DocNo ";
                    sql += " WHERE t1.FlagDel = 0 ";
                    sql += " AND t2.DocTypeCode = '" + docTypeCode.Trim() + "' ";
                }

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PODetail), sql);
                list = dynamicListReturned.Cast<tbl_PODetail>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_PODetail>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PODetail.OrderBy(x => x.DocNo).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PODetail"></param>
        /// <returns></returns>
        public static void Insert(this tbl_PODetail tbl_PODetail, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PODetailDao=>InsertWithDB";
            msg.WriteLog(null);

            try
            {
                db.tbl_PODetail.Attach(tbl_PODetail);
                db.tbl_PODetail.Add(tbl_PODetail);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            msg = "end PODetailDao=>InsertWithDB";
            msg.WriteLog(null);
        }

        public static void Insert(this List<tbl_PODetail> tbl_PODetails, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PODetailDao=>InsertListWithDB";
            msg.WriteLog(null);
            try
            {
                foreach (var tbl_PODetail in tbl_PODetails)
                {
                    db.tbl_PODetail.Attach(tbl_PODetail);
                    db.tbl_PODetail.Add(tbl_PODetail);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }

            msg = "end PODetailDao=>InsertListWithDB";
            msg.WriteLog(null);
        }

        public static int Insert(this tbl_PODetail tbl_PODetail)
        {
            string msg = "start PODetailDao=>Insert";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_PODetail.Attach(tbl_PODetail);
                    db.tbl_PODetail.Add(tbl_PODetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            msg = "end PODetailDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        public static int Update(this List<tbl_PODetail> tbl_PODetails)
        {
            string msg = "start PODetailDao=>UpdateList";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_PODetail in tbl_PODetails)
                    {
                        var updateData = db.tbl_PODetail.FirstOrDefault(x => x.DocNo == tbl_PODetail.DocNo && x.ProductID == tbl_PODetail.ProductID && tbl_PODetail.Line == x.Line);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_PODetailItem in tbl_PODetail.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_PODetailItem.Name)
                                    {
                                        var value = tbl_PODetailItem.GetValue(tbl_PODetail, null);

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
                            tbl_PODetail.Delete(db);
                            tbl_PODetail.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
                //ex.WriteLog(tbl_PODetail);
            }

            msg = "end PODetailDao=>UpdateList";
            msg.WriteLog(null);

            return ret != 0 ? 1 : 0;
        }

        public static int UpdateEntity(this List<tbl_PODetail> tbl_PODetails, DB_ALL_CASH_UNIEntities db, string docTypeCode = "")
        {
            string msg = "start PODetailDao=>UpdateEntity";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                var docNo = tbl_PODetails.First().DocNo;
                var tbl_PODetailList = db.tbl_PODetail.Where(x => x.DocNo == docNo).ToList();

                if (tbl_PODetailList.Count > 0)
                {
                    foreach (var tbl_PODetail in tbl_PODetails)
                    {
                        var updateData = tbl_PODetailList.FirstOrDefault(x => x.DocNo == tbl_PODetail.DocNo && x.ProductID == tbl_PODetail.ProductID && tbl_PODetail.Line == x.Line); // db.tbl_PODetail.FirstOrDefault(x => x.DocNo == tbl_PODetail.DocNo && x.ProductID == tbl_PODetail.ProductID && tbl_PODetail.Line == x.Line);

                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_PODetailItem in tbl_PODetail.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_PODetailItem.Name)
                                    {
                                        var value = tbl_PODetailItem.GetValue(tbl_PODetail, null);

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
                            //tbl_PODetail.Delete(db);
                            tbl_PODetail.Insert(db);
                        }
                    }
                }
                else
                    tbl_PODetails.Insert(db);

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            msg = "end PODetailDao=>UpdateEntity";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_PODetail"></param>
        /// <returns></returns>
        public static int Update(this tbl_PODetail tbl_PODetail)
        {
            string msg = "start PODetailDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_PODetail.FirstOrDefault(x => x.DocNo == tbl_PODetail.DocNo && x.ProductID == tbl_PODetail.ProductID && tbl_PODetail.Line == x.Line);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PODetailItem in tbl_PODetail.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_PODetailItem.Name)
                                {
                                    var value = tbl_PODetailItem.GetValue(tbl_PODetail, null);

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
                        ret = tbl_PODetail.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            msg = "end PODetailDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        public static int UpdateSQL(this tbl_PODetail tbl_PODetail, string sqlCmd)
        {
            string msg = "start PODetailDao=>UpdateSQL";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                ret = My_DataTable_Extensions.ExecuteSQLScalar(sqlCmd, CommandType.Text);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail != null ? tbl_PODetail.GetType() : null);
            }

            msg = "end PODetailDao=>UpdateSQL";
            msg.WriteLog(null);

            return ret;
        }


        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PODetail"></param>
        /// <returns></returns>
        public static void Delete(this tbl_PODetail tbl_PODetail, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PODetailDao=>DeleteWithDB";
            msg.WriteLog(null);

            try
            {
                db.Entry(tbl_PODetail).State = EntityState.Deleted;
                db.tbl_PODetail.Remove(tbl_PODetail);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            msg = "end PODetailDao=>DeleteWithDB";
            msg.WriteLog(null);
        }

        public static int Delete(this tbl_PODetail tbl_PODetail)
        {
            string msg = "start PODetailDao=>Delete";
            msg.WriteLog(null);
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_PODetail).State = EntityState.Deleted;
                    db.tbl_PODetail.Remove(tbl_PODetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            msg = "end PODetailDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }


        public static int BulkInsert(this List<tbl_PODetail> tbl_PODetails)
        {
            string msg = "start PODetailDao=>BulkInsert";
            msg.WriteLog(null);

            int ret = 1;

            var table = tbl_PODetails.ToDataTable();
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
                                bcp.DestinationTableName = "tbl_PODetail";
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

            msg = "end PODetailDao=>BulkInsert";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkUpdate(this List<tbl_PODetail> tbl_PODetails)
        {
            string msg = "start tbl_PODetail=>BulkUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                //var table = tbl_PODetails.ToDataTable();
                //if (table != null && table.Rows.Count > 0)
                //{
                //    using (var conn = new SqlConnection(Connection.ConnectionString))
                //    {
                //        if (conn.State == ConnectionState.Closed)
                //        {
                //            conn.Open();
                //        }

                //        string sql = " SELECT * FROM tbl_PODetail ";
                //        var cmd = new SqlCommand(sql, conn);
                //        var ad = new SqlDataAdapter(cmd);
                //        SqlCommandBuilder cmdb = new SqlCommandBuilder(ad);
                //        ad.Update(table);
                //        table.AcceptChanges();

                //        ret = 1;
                //    }
                //}

                string sql = " DELETE FROM tbl_PODetail WHERE DocNo = '" + tbl_PODetails.FirstOrDefault().DocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////

                ret = tbl_PODetails.BulkInsert();
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end PODetailDao=>UpdateList";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkRemove(this List<tbl_PODetail> tbl_PODetails)
        {
            string msg = "start tbl_PODetail=>BulkRemove";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                string sql = " DELETE FROM tbl_PODetail WHERE DocNo = '" + tbl_PODetails.FirstOrDefault().DocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////
                
                ret = 1;
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end tbl_PODetail=>BulkRemove";
            msg.WriteLog(null);

            return ret;
        }

        public static int PerformUpdate(this List<tbl_PODetail> tbl_PODetails, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PODetailDao=>PerformUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var updateData = new tbl_PODetail();
                var docNo = tbl_PODetails.First().DocNo;
                updateData = db.tbl_PODetail.FirstOrDefault(x => x.DocNo == docNo);

                if (updateData != null)
                {
                    ret = tbl_PODetails.BulkUpdate();
                }
                else
                {
                    ret = tbl_PODetails.BulkInsert();
                }
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }

            msg = "end PODetailDao=>PerformUpdate";
            msg.WriteLog(null);

            return ret;
        }

        public static List<tbl_PODetail> GetPODetail_AllBranch(this tbl_PODetail tbl_PODetail, Dictionary<string, object> Params)
        {
            List<tbl_PODetail> list = new List<tbl_PODetail>();
            try
            {
                string sql = "proc_PODetail_GetData_AllBranch";
                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteStoreToList(typeof(tbl_PODetail), sql, Params);
                list = dynamicListReturned.Cast<tbl_PODetail>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            return list;
        }

    }
}
