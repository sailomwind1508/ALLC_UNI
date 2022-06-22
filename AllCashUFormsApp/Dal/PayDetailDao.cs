using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace AllCashUFormsApp
{
    public static class PayDetailDao
    {
        public static List<tbl_PayDetail> GetPayDetailSingle(this tbl_PayDetail tbl_PayDetail, string _DocNo, int _FlagDel)
        {
            string msg = "start PayDetailDao=>GetPayDetailSingle";
            msg.WriteLog(null);

            var list = new List<tbl_PayDetail>();
            try
            {
                string sql = "SELECT *  FROM tbl_PayDetail";
                sql += " WHERE FlagDel = " + _FlagDel;
                if (!string.IsNullOrEmpty(_DocNo))
                {
                    sql += " AND DocNo = '" + _DocNo.Trim() + "'";
                }

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PayDetail), sql);
                list = dynamicListReturned.Cast<tbl_PayDetail>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail != null ? tbl_PayDetail.GetType() : null);
            }

            msg = "end PayDetailDao=>GetPayDetailSingle";
            msg.WriteLog(null);

            return list;
        }

        public static int SelectPayDetail_MaxID(this tbl_PayDetail tbl_PayDetail)
        {
            int MaxID = 0;
            try
            {
                string sql = "SELECT TOP 1 MAX(AutoID) FROM tbl_PayDetail";
                var dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    MaxID = dt.Rows[0].Field<int>(0) + 1;
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
            }
            return MaxID;
        }

        public static List<tbl_PayDetail> Select(this tbl_PayDetail tbl_PayDetail, Func<tbl_PayDetail, bool> predicate)
        {
            List<tbl_PayDetail> list = new List<tbl_PayDetail>();
            try
            {
                list = tbl_PayDetail.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PayDetail.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
            }

            return list;
        }

        public static List<tbl_PayDetail> SelectAll(this tbl_PayDetail tbl_PayDetail)
        {
            List<tbl_PayDetail> list = new List<tbl_PayDetail>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_PayDetail] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PayDetail), sql);
                list = dynamicListReturned.Cast<tbl_PayDetail>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_PayDetail>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PayDetail.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PayDetail"></param>
        /// <returns></returns>
        public static void Insert(this tbl_PayDetail tbl_PayDetail, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start IVDetailDao=>InsertWithDB";
            msg.WriteLog(null);

            try
            {
                db.tbl_PayDetail.Attach(tbl_PayDetail);
                db.tbl_PayDetail.Add(tbl_PayDetail);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
            }

            msg = "end IVDetailDao=>InsertWithDB";
            msg.WriteLog(null);
        }

        public static void Insert(this List<tbl_PayDetail> tbl_PayDetails, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start IVDetailDao=>InsertListWithDB";
            msg.WriteLog(null);

            try
            {
                foreach (var tbl_PayDetail in tbl_PayDetails)
                {

                    db.tbl_PayDetail.Attach(tbl_PayDetail);
                    db.tbl_PayDetail.Add(tbl_PayDetail);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }

            msg = "end IVDetailDao=>InsertListWithDB";
            msg.WriteLog(null);
        }

        public static int UpdateEntity(this List<tbl_PayDetail> tbl_PayDetails, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PayDetailDao=>UpdateEntity";
            msg.WriteLog(null);

            int ret = 0;

            try
            {

                var docNo = tbl_PayDetails.First().DocNo;
                var tbl_PayDetailList = db.tbl_PayDetail.Where(x => x.DocNo == docNo).ToList();

                if (tbl_PayDetailList.Count > 0)
                {
                    foreach (var tbl_PayDetail in tbl_PayDetails)
                    {
                        var updateData = tbl_PayDetailList.FirstOrDefault(x => x.DocNo == tbl_PayDetail.DocNo && x.WHID == tbl_PayDetail.WHID && x.AutoID == tbl_PayDetail.AutoID);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_PayDetailItem in tbl_PayDetail.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name == tbl_PayDetailItem.Name)
                                    {
                                        var value = tbl_PayDetailItem.GetValue(tbl_PayDetail, null);

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
                            //tbl_PayDetail.Delete(db);
                            tbl_PayDetail.Insert(db);
                        }

                    }
                }
                else
                    tbl_PayDetails.Insert(db);

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            msg = "end PayDetailDao=>UpdateEntity";
            msg.WriteLog(null);

            return ret;
        }

        public static int Update(this List<tbl_PayDetail> tbl_PayDetails)
        {
            string msg = "start PayDetailDao=>UpdateList";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_PayDetail in tbl_PayDetails)
                    {
                        var updateData = db.tbl_PayDetail.FirstOrDefault(x => x.DocNo == tbl_PayDetail.DocNo && x.WHID == tbl_PayDetail.WHID && x.AutoID == tbl_PayDetail.AutoID);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_PayDetailItem in tbl_PayDetail.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name == tbl_PayDetailItem.Name)
                                    {
                                        var value = tbl_PayDetailItem.GetValue(tbl_PayDetail, null);

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
                            tbl_PayDetail.Delete(db);
                            tbl_PayDetail.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //ex.WriteLog(tbl_PayDetail);
            }

            msg = "end PayDetailDao=>UpdateList";
            msg.WriteLog(null);

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PayDetail"></param>
        /// <returns></returns>
        public static void Delete(this tbl_PayDetail tbl_PayDetail, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PayDetailDao=>DeleteWithDB";
            msg.WriteLog(null);

            try
            {
                db.Entry(tbl_PayDetail).State = EntityState.Deleted;
                db.tbl_PayDetail.Remove(tbl_PayDetail);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
            }

            msg = "end PayDetailDao=>DeleteWithDB";
            msg.WriteLog(null);
        }

        public static int Insert(this tbl_PayDetail tbl_PayDetail)
        {
            string msg = "start PayDetailDao=>Insert";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_PayDetail.Attach(tbl_PayDetail);
                    db.tbl_PayDetail.Add(tbl_PayDetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
            }

            msg = "end PayDetailDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        public static int Update(this tbl_PayDetail tbl_PayDetail)
        {
            string msg = "start PayDetailDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_PayDetail.FirstOrDefault(x => x.DocNo == tbl_PayDetail.DocNo && x.WHID == tbl_PayDetail.WHID && x.AutoID == tbl_PayDetail.AutoID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PayDetailItem in tbl_PayDetail.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_PayDetailItem.Name)
                                {
                                    var value = tbl_PayDetailItem.GetValue(tbl_PayDetail, null);

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
                        ret = tbl_PayDetail.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
                ex.ToString();
            }

            msg = "end PayDetailDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        public static int Delete(this tbl_PayDetail tbl_PayDetail)
        {
            string msg = "start PayDetailDao=>Delete";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_PayDetail).State = EntityState.Deleted;
                    db.tbl_PayDetail.Remove(tbl_PayDetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
            }

            msg = "end PayDetailDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkInsert(this List<tbl_PayDetail> tbl_PayDetails)
        {
            string msg = "start PayDetailDao=>BulkInsert";
            msg.WriteLog(null);

            int ret = 1;

            var table = tbl_PayDetails.ToDataTable();
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
                                bcp.DestinationTableName = "tbl_PayDetail";
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

            msg = "end PayDetailDao=>BulkInsert";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkUpdate(this List<tbl_PayDetail> tbl_PayDetails)
        {
            string msg = "start PayDetailDao=>BulkUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                string whIds = "";
                int j = 0;
                foreach (var whid in tbl_PayDetails.Select(a => a.WHID).Distinct().ToList())
                {
                    if (j == tbl_PayDetails.Select(a => a.WHID).Distinct().ToList().Count - 1)
                        whIds += "'" + whid + "' ";
                    else
                        whIds += "'" + whid + "', ";

                    j++;
                }

                //x.DocNo == tbl_PayDetail.DocNo && x.WHID == tbl_PayDetail.WHID && x.AutoID == tbl_PayDetail.AutoID
                string sql = " DELETE FROM tbl_PayDetail WHERE WHID IN (" + whIds + ") AND DocNo = '" + tbl_PayDetails.FirstOrDefault().DocNo + "' AND AutoID = '" + tbl_PayDetails.FirstOrDefault().AutoID + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////

                ret = tbl_PayDetails.BulkInsert();
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }

            msg = "end PayDetailDao=>BulkUpdate";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkRemove(this List<tbl_PayDetail> tbl_PayDetails)
        {
            string msg = "start tbl_PayDetail=>BulkRemove";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                string sql = " DELETE FROM tbl_PayDetail WHERE DocNo = '" + tbl_PayDetails.FirstOrDefault().DocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////
                ret = 1;
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end tbl_PayDetail=>BulkRemove";
            msg.WriteLog(null);

            return ret;
        }

        public static int PerformUpdate(this List<tbl_PayDetail> tbl_PayDetails, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PayDetailDao=>PerformUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var updateData = new tbl_PayDetail();
                var docNo = tbl_PayDetails.First().DocNo;
                updateData = db.tbl_PayDetail.FirstOrDefault(x => x.DocNo == docNo);

                if (updateData != null)
                {
                    ret = tbl_PayDetails.BulkUpdate();
                }
                else
                {
                    ret = tbl_PayDetails.BulkInsert();
                }
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }

            msg = "end PayDetailDao=>PerformUpdate";
            msg.WriteLog(null);

            return ret;
        }
    }
}
