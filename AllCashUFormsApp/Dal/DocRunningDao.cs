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
    public static class DocRunningDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_DocRunning"></param>
        /// <returns></returns>
        public static List<tbl_DocRunning> Select(this tbl_DocRunning tbl_DocRunning, Func<tbl_DocRunning, bool> predicate)
        {
            List<tbl_DocRunning> list = new List<tbl_DocRunning>();
            try
            {
                list = tbl_DocRunning.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    var x = db.tbl_DocRunning.ToList();
                //    list = db.tbl_DocRunning.Where(predicate).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocRunning.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_DocRunning"></param>
        /// <returns></returns>
        public static List<tbl_DocRunning> SelectAll(this tbl_DocRunning tbl_DocRunning)
        {
            List<tbl_DocRunning> list = new List<tbl_DocRunning>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM tbl_DocRunning";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_DocRunning), sql);
                list = dynamicListReturned.Cast<tbl_DocRunning>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_DocRunning>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_DocRunning.ToList();
                //}
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_DocRunning.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_DocRunning"></param>
        /// <returns></returns>
        public static void Insert(this tbl_DocRunning tbl_DocRunning, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start DocRunningDao=>InsertWithDB";
            msg.WriteLog(null);

            try
            {
                db.tbl_DocRunning.Attach(tbl_DocRunning);
                db.tbl_DocRunning.Add(tbl_DocRunning);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocRunning.GetType());
            }

            msg = "end DocRunningDao=>InsertWithDB";
            msg.WriteLog(null);
        }

        public static int UpdateEntity(this List<tbl_DocRunning> tbl_DocRunnings, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start DocRunningDao=>UpdateEntity";
           msg.WriteLog(null);

            int ret = 0;

            try
            {
                foreach (var tbl_DocRunning in tbl_DocRunnings)
                {
                    var updateData = db.tbl_DocRunning.FirstOrDefault(x => x.DocNum == tbl_DocRunning.DocNum);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_DocRunningItem in tbl_DocRunning.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "docnum" && updateDataItem.Name == tbl_DocRunningItem.Name)
                                {
                                    var value = tbl_DocRunningItem.GetValue(tbl_DocRunning, null);

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
                        tbl_DocRunning.Delete(db);
                        tbl_DocRunning.Insert(db);
                    }
                }

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            msg = "end DocRunningDao=>UpdateEntity";
           msg.WriteLog(null);

            return ret;
        }

        public static int Update(this List<tbl_DocRunning> tbl_DocRunnings)
        {
            string msg = "start DocRunningDao=>UpdateList";
           msg.WriteLog(null);

            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_DocRunning in tbl_DocRunnings)
                    {
                        var updateData = db.tbl_DocRunning.FirstOrDefault(x => x.DocNum == tbl_DocRunning.DocNum);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_DocRunningItem in tbl_DocRunning.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "docnum" && updateDataItem.Name == tbl_DocRunningItem.Name)
                                    {
                                        var value = tbl_DocRunningItem.GetValue(tbl_DocRunning, null);

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
                            tbl_DocRunning.Delete(db);
                            tbl_DocRunning.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //ex.WriteLog(tbl_DocRunning);
                ex.WriteLog(null);
            }

            msg = "end DocRunningDao=>UpdateList";
           msg.WriteLog(null);

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_DocRunning"></param>
        /// <returns></returns>
        public static void Delete(this tbl_DocRunning tbl_DocRunning, DB_ALL_CASH_UNIEntities db)
        {
            string msg  = "start DocRunningDao=>DeleteWithDB";
            msg.WriteLog(null);

            try
            {
                db.Entry(tbl_DocRunning).State = EntityState.Deleted;
                db.tbl_DocRunning.Remove(tbl_DocRunning);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocRunning.GetType());
            }

            msg = "end DocRunningDao=>DeleteWithDB";
            msg.WriteLog(null);
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_DocRunning"></param>
        /// <returns></returns>
        public static int Insert(this tbl_DocRunning tbl_DocRunning)
        {
            string msg = "start DocRunningDao=>Insert";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_DocRunning.Attach(tbl_DocRunning);
                    db.tbl_DocRunning.Add(tbl_DocRunning);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocRunning.GetType());
            }

            msg = "end DocRunningDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_DocRunning"></param>
        /// <returns></returns>
        public static int Update(this tbl_DocRunning tbl_DocRunning)
        {
            string msg = "start DocRunningDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_DocRunning.FirstOrDefault(x => x.DocNum == tbl_DocRunning.DocNum);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_DocRunningItem in tbl_DocRunning.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "docnum" && updateDataItem.Name == tbl_DocRunningItem.Name)
                                {
                                    var value = tbl_DocRunningItem.GetValue(tbl_DocRunning, null);

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
                        ret = tbl_DocRunning.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocRunning.GetType());
            }

            msg = "end DocRunningDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_DocRunning"></param>
        /// <returns></returns>
        public static int Delete(this tbl_DocRunning tbl_DocRunning)
        {
            string msg = "start DocRunningDao=>Delete";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_DocRunning).State = EntityState.Deleted;
                    db.tbl_DocRunning.Remove(tbl_DocRunning);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocRunning.GetType());
            }

            msg = "end DocRunningDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }


        public static int BulkInsert(this List<tbl_DocRunning> tbl_DocRunnings)
        {
            string msg = "start tbl_DocRunning=>BulkInsert";
            msg.WriteLog(null);

            int ret = 1;

            var table = tbl_DocRunnings.ToDataTable();
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
                                bcp.DestinationTableName = "tbl_DocRunning";
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

            msg = "end tbl_DocRunning=>BulkInsert";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkUpdate(this List<tbl_DocRunning> tbl_DocRunnings)
        {
            string msg = "start tbl_DocRunning=>BulkUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {

                string sql = " DELETE FROM tbl_DocRunning WHERE DocNum = '" + tbl_DocRunnings.FirstOrDefault().DocNum + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////

                ret = tbl_DocRunnings.BulkInsert();

            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end tbl_DocRunning=>UpdateList";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkRemove(this List<tbl_DocRunning> tbl_DocRunnings)
        {
            string msg = "start tbl_DocRunning=>BulkRemove";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                string sql = " DELETE FROM tbl_DocRunning WHERE DocNum = '" + tbl_DocRunnings.FirstOrDefault().DocNum + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////
                ret = 1;
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end tbl_DocRunning=>BulkRemove";
            msg.WriteLog(null);

            return ret;
        }

        public static int PerformUpdate(this List<tbl_DocRunning> tbl_DocRunnings, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start tbl_DocRunning=>PerformUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var updateData = new tbl_DocRunning();
                var docNo = tbl_DocRunnings.First().DocNum;
                var docTypeCode = tbl_DocRunnings.First().DocTypeCode;
                updateData = db.tbl_DocRunning.FirstOrDefault(x => x.DocNum == docNo && x.DocTypeCode == docTypeCode);

                if (updateData != null)
                {
                    ret = tbl_DocRunnings.BulkUpdate();
                }
                else
                {
                    ret = tbl_DocRunnings.BulkInsert();
                }
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }

            msg = "end tbl_DocRunning=>PerformUpdate";
            msg.WriteLog(null);

            return ret;
        }
    }
}
