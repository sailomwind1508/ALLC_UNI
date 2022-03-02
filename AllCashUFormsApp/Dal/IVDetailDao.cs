using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
//using System.Data.Entity.Infrastructure;
//using System.Data.SqlClient;
//using System.Text;
//using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class IVDetailDao
    {
        public static List<tbl_IVDetail> SelectWithDocNo(this tbl_IVDetail tbl_IVDetail, string docNo)
        {
            List<tbl_IVDetail> list = new List<tbl_IVDetail>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_IVDetail] ";
                sql += " WHERE DocNo = '" + docNo + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_IVDetail), sql);
                list = dynamicListReturned.Cast<tbl_IVDetail>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
            }

            return list;
        }


        public static List<tbl_IVDetail> Select(this tbl_IVDetail tbl_IVDetail, DateTime docDate)
        {
            List<tbl_IVDetail> list = new List<tbl_IVDetail>();
            try
            {
                string _docDate = docDate.ToString("yyyyMMdd", new CultureInfo("en-US"));

                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT t1.* ";
                sql += " FROM [dbo].[tbl_IVDetail] t1 ";
                sql += " INNER JOIN [dbo].[tbl_IVMaster] t2 ON t1.DocNo = t2.DocNo WHERE t1.FlagDel = 0 ";
                sql += " AND CAST(t2.DocDate AS DATE) = '" + _docDate + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_IVDetail), sql);
                list = dynamicListReturned.Cast<tbl_IVDetail>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_IVDetail"></param>
        /// <returns></returns>
        public static List<tbl_IVDetail> Select(this tbl_IVDetail tbl_IVDetail, string sqlFilter)
        {
            List<tbl_IVDetail> list = new List<tbl_IVDetail>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_IVDetail] WHERE FlagDel = 0 AND " + sqlFilter;

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_IVDetail), sql);
                list = dynamicListReturned.Cast<tbl_IVDetail>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_IVDetail"></param>
        /// <returns></returns>
        public static List<tbl_IVDetail> Select(this tbl_IVDetail tbl_IVDetail, Func<tbl_IVDetail, bool> predicate)
        {
            List<tbl_IVDetail> list = new List<tbl_IVDetail>();
            try
            {
                list = tbl_IVDetail.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_IVDetail.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_IVDetail"></param>
        /// <returns></returns>
        public static List<tbl_IVDetail> SelectAll(this tbl_IVDetail tbl_IVDetail)
        {
            List<tbl_IVDetail> list = new List<tbl_IVDetail>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_IVDetail] WHERE FlagDel = 0 ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_IVDetail), sql);
                list = dynamicListReturned.Cast<tbl_IVDetail>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_IVDetail>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_IVDetail.OrderBy(x => x.DocNo).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_IVDetail"></param>
        /// <returns></returns>
        public static void Insert(this tbl_IVDetail tbl_IVDetail, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start IVDetailDao=>InsertWithDB";
            msg.WriteLog(null);

            try
            {
                db.tbl_IVDetail.Attach(tbl_IVDetail);
                db.tbl_IVDetail.Add(tbl_IVDetail);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            msg = "end IVDetailDao=>InsertWithDB";
            msg.WriteLog(null);
        }

        public static void Insert(this List<tbl_IVDetail> tbl_IVDetails, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start IVDetailDao=>InsertListWithDB";
            msg.WriteLog(null);

            try
            {
                foreach (var tbl_IVDetail in tbl_IVDetails)
                {
                    db.tbl_IVDetail.Attach(tbl_IVDetail);
                    db.tbl_IVDetail.Add(tbl_IVDetail);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }

            msg = "end IVDetailDao=>InsertListWithDB";
            msg.WriteLog(null);
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_IVDetail"></param>
        /// <returns></returns>
        public static int Insert(this tbl_IVDetail tbl_IVDetail)
        {
            string msg = "start IVDetailDao=>Insert";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_IVDetail.Attach(tbl_IVDetail);
                    db.tbl_IVDetail.Add(tbl_IVDetail);
                    ret = db.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            msg = "end IVDetailDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        public static int UpdateEntity(this List<tbl_IVDetail> tbl_IVDetails, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start IVDetailDao=>UpdateEntityWithDB";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var docNo = tbl_IVDetails.First().DocNo;
                var tbl_IVDetailList = db.tbl_IVDetail.Where(x => x.DocNo == docNo).ToList();

                if (tbl_IVDetailList.Count > 0)
                {
                    foreach (var tbl_IVDetail in tbl_IVDetails)
                    {
                        var updateData = tbl_IVDetailList.FirstOrDefault(x => x.DocNo == tbl_IVDetail.DocNo && x.ProductID == tbl_IVDetail.ProductID);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_IVDetailItem in tbl_IVDetail.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_IVDetailItem.Name)
                                    {
                                        var value = tbl_IVDetailItem.GetValue(tbl_IVDetail, null);

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
                            //tbl_IVDetail.Delete(db);
                            tbl_IVDetail.Insert(db);
                        }
                    }
                }
                else
                    tbl_IVDetails.Insert(db);

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            msg = "end IVDetailDao=>UpdateEntityWithDB";
            msg.WriteLog(null);

            return ret;
        }

        public static int Update(this List<tbl_IVDetail> tbl_IVDetails)
        {
            string msg = "start IVDetailDao=>UpdateList";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_IVDetail in tbl_IVDetails)
                    {
                        var updateData = db.tbl_IVDetail.FirstOrDefault(x => x.DocNo == tbl_IVDetail.DocNo && x.ProductID == tbl_IVDetail.ProductID);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_IVDetailItem in tbl_IVDetail.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_IVDetailItem.Name)
                                    {
                                        var value = tbl_IVDetailItem.GetValue(tbl_IVDetail, null);

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
                            tbl_IVDetail.Delete(db);
                            tbl_IVDetail.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //ex.WriteLog(tbl_IVDetail);
            }

            msg = "end IVDetailDao=>UpdateList";
            msg.WriteLog(null);

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_IVDetail"></param>
        /// <returns></returns>
        public static int Update(this tbl_IVDetail tbl_IVDetail)
        {
            string msg = "start IVDetailDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_IVDetail.FirstOrDefault(x => x.DocNo == tbl_IVDetail.DocNo && x.ProductID == tbl_IVDetail.ProductID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_IVDetailItem in tbl_IVDetail.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_IVDetailItem.Name)
                                {
                                    var value = tbl_IVDetailItem.GetValue(tbl_IVDetail, null);

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
                        ret = tbl_IVDetail.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            msg = "end IVDetailDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_IVDetail"></param>
        /// <returns></returns>
        public static int Delete(this tbl_IVDetail tbl_IVDetail)
        {
            string msg = "start IVDetailDao=>Delete";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_IVDetail).State = EntityState.Deleted;
                    db.tbl_IVDetail.Remove(tbl_IVDetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            msg = "end IVDetailDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_IVDetail"></param>
        /// <returns></returns>
        public static void Delete(this tbl_IVDetail tbl_IVDetail, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start IVDetailDao=>DeleteWithDB";
            msg.WriteLog(null);

            try
            {
                db.Entry(tbl_IVDetail).State = EntityState.Deleted;
                db.tbl_IVDetail.Remove(tbl_IVDetail);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            msg = "end IVDetailDao=>DeleteWithDB";
            msg.WriteLog(null);
        }

        public static int BulkInsert(this List<tbl_IVDetail> tbl_IVDetails)
        {
            string msg = "start tbl_IVDetail=>BulkInsert";
            msg.WriteLog(null);

            int ret = 1;

            var table = tbl_IVDetails.ToDataTable();
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
                                bcp.DestinationTableName = "tbl_IVDetail";
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

            msg = "end tbl_IVDetail=>BulkInsert";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkUpdate(this List<tbl_IVDetail> tbl_IVDetails)
        {
            string msg = "start tbl_IVDetail=>BulkUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                //var table = tbl_IVDetails.ToDataTable();
                //if (table != null && table.Rows.Count > 0)
                //{
                //    using (var conn = new SqlConnection(Connection.ConnectionString))
                //    {
                //        if (conn.State == ConnectionState.Closed)
                //        {
                //            conn.Open();
                //        }

                //        string sql = " SELECT * FROM tbl_IVDetail ";
                //        var cmd = new SqlCommand(sql, conn);
                //        var ad = new SqlDataAdapter(cmd);
                //        SqlCommandBuilder cmdb = new SqlCommandBuilder(ad);
                //        ad.Update(table);
                //        table.AcceptChanges();

                //        ret = 1;
                //    }
                //}

                string sql = " DELETE FROM tbl_IVDetail WHERE DocNo = '" + tbl_IVDetails.FirstOrDefault().DocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////

                ret = tbl_IVDetails.BulkInsert();
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end tbl_IVDetail=>BulkUpdate";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkRemove(this List<tbl_IVDetail> tbl_IVDetails)
        {
            string msg = "start tbl_IVDetail=>BulkRemove";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                string sql = " DELETE FROM tbl_IVDetail WHERE DocNo = '" + tbl_IVDetails.FirstOrDefault().DocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////
                ret = 1;
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end tbl_IVDetail=>BulkRemove";
            msg.WriteLog(null);

            return ret;
        }

        public static int PerformUpdate(this List<tbl_IVDetail> tbl_IVDetails, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start tbl_IVDetail=>PerformUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var updateData = new tbl_IVDetail();
                var docNo = tbl_IVDetails.First().DocNo;
                updateData = db.tbl_IVDetail.FirstOrDefault(x => x.DocNo == docNo);

                if (updateData != null)
                {
                    ret = tbl_IVDetails.BulkUpdate();
                }
                else
                {
                    ret = tbl_IVDetails.BulkInsert();
                }
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }

            msg = "end tbl_IVDetail=>PerformUpdate";
            msg.WriteLog(null);

            return ret;
        }
    }
}
