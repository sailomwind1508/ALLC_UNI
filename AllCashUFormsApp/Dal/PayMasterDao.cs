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
    public static class PayMasterDao
    {
        public static List<tbl_PayMaster> Select(this tbl_PayMaster tbl_PayMaster, Func<tbl_PayMaster, bool> predicate)
        {
            List<tbl_PayMaster> list = new List<tbl_PayMaster>();
            try
            {
                list = tbl_PayMaster.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PayMaster.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayMaster.GetType());
            }

            return list;
        }

        public static List<tbl_PayMaster> SelectAll(this tbl_PayMaster tbl_PayMaster)
        {
            List<tbl_PayMaster> list = new List<tbl_PayMaster>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_PayMaster] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PayMaster), sql);
                list = dynamicListReturned.Cast<tbl_PayMaster>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_PayMaster>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PayMaster.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayMaster.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PayMaster"></param>
        /// <returns></returns>
        public static void Insert(this tbl_PayMaster tbl_PayMaster, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.tbl_PayMaster.Attach(tbl_PayMaster);
                db.tbl_PayMaster.Add(tbl_PayMaster);
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }
        }

        public static int UpdateEntity(this List<tbl_PayMaster> tbl_PayMasters, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PayMasterDao=>UpdateEntity";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                foreach (var tbl_PayMaster in tbl_PayMasters)
                {
                    var updateData = db.tbl_PayMaster.FirstOrDefault(x => x.DocNo == tbl_PayMaster.DocNo && x.AutoID == tbl_PayMaster.AutoID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PayMasterItem in tbl_PayMaster.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_PayMasterItem.Name)
                                {
                                    var value = tbl_PayMasterItem.GetValue(tbl_PayMaster, null);

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
                        tbl_PayMaster.Insert(db);
                    }

                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            msg = "end PayMasterDao=>UpdateEntity";
            msg.WriteLog(null);

            return ret;
        }

        public static int Update(this List<tbl_PayMaster> tbl_PayMasters)
        {
            string msg = "start PayMasterDao=>UpdateList";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_PayMaster in tbl_PayMasters)
                    {
                        var updateData = db.tbl_PayMaster.FirstOrDefault(x => x.DocNo == tbl_PayMaster.DocNo && x.AutoID == tbl_PayMaster.AutoID);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_PayMasterItem in tbl_PayMaster.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name == tbl_PayMasterItem.Name)
                                    {
                                        var value = tbl_PayMasterItem.GetValue(tbl_PayMaster, null);

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
                            tbl_PayMaster.Delete(db);
                            tbl_PayMaster.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //ex.WriteLog(tbl_PayMaster);
            }

            msg = "end PayMasterDao=>UpdateList";
            msg.WriteLog(null);

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PayMaster"></param>
        /// <returns></returns>
        public static void Delete(this tbl_PayMaster tbl_PayMaster, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PayMasterDao=>DeleteWithDB";
            msg.WriteLog(null);

            try
            {
                db.Entry(tbl_PayMaster).State = EntityState.Deleted;
                db.tbl_PayMaster.Remove(tbl_PayMaster);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayMaster.GetType());
            }

            msg = "end PayMasterDao=>DeleteWithDB";
            msg.WriteLog(null);
        }

        public static int Insert(this tbl_PayMaster tbl_PayMaster)
        {
            string msg = "start PayMasterDao=>Insert";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_PayMaster.Attach(tbl_PayMaster);
                    db.tbl_PayMaster.Add(tbl_PayMaster);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayMaster.GetType());
            }

            msg = "end PayMasterDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        public static int Update(this tbl_PayMaster tbl_PayMaster)
        {
            string msg = "start PayMasterDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_PayMaster.FirstOrDefault(x => x.DocNo == tbl_PayMaster.DocNo && x.AutoID == tbl_PayMaster.AutoID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PayMasterItem in tbl_PayMaster.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_PayMasterItem.Name)
                                {
                                    var value = tbl_PayMasterItem.GetValue(tbl_PayMaster, null);

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
                        ret = tbl_PayMaster.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayMaster.GetType());
            }

            msg = "end PayMasterDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        public static int Delete(this tbl_PayMaster tbl_PayMaster)
        {
            string msg = "start PayMasterDao=>Delete";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_PayMaster).State = EntityState.Deleted;
                    db.tbl_PayMaster.Remove(tbl_PayMaster);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayMaster.GetType());
            }

            msg = "end PayMasterDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkInsert(this List<tbl_PayMaster> tbl_PayMasters)
        {
            string msg = "start PayMasterDao=>BulkInsert";
            msg.WriteLog(null);

            int ret = 1;

            var table = tbl_PayMasters.ToDataTable();
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
                                bcp.DestinationTableName = "tbl_PayMaster";
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

            msg = "end PayMasterDao=>BulkInsert";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkUpdate(this List<tbl_PayMaster> tbl_PayMasters)
        {
            string msg = "start PayMasterDao=>BulkUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                //var table = tbl_PayMasters.ToDataTable();
                //if (table != null && table.Rows.Count > 0)
                //{
                //    using (var conn = new SqlConnection(Connection.ConnectionString))
                //    {
                //        if (conn.State == ConnectionState.Closed)
                //        {
                //            conn.Open();
                //        }

                //        string sql = " SELECT * FROM tbl_PayMaster ";
                //        var cmd = new SqlCommand(sql, conn);
                //        var ad = new SqlDataAdapter(cmd);
                //        SqlCommandBuilder cmdb = new SqlCommandBuilder(ad);
                //        ad.Update(table);
                //        table.AcceptChanges();

                //        ret = 1;
                //    }
                //}

                string sql = " DELETE FROM tbl_PayMaster WHERE DocNo = '" + tbl_PayMasters.FirstOrDefault().DocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////

                ret = tbl_PayMasters.BulkInsert();
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }

            msg = "end PayMasterDao=>BulkUpdate";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkRemove(this List<tbl_PayMaster> tbl_PayMasters)
        {
            string msg = "start tbl_PayMaster=>BulkRemove";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                string sql = " DELETE FROM tbl_PayMaster WHERE DocNo = '" + tbl_PayMasters.FirstOrDefault().DocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////
                ret = 1;
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end tbl_PayMaster=>BulkRemove";
            msg.WriteLog(null);

            return ret;
        }

        public static int PerformUpdate(this List<tbl_PayMaster> tbl_PayMasters, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PayMasterDao=>PerformUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var updateData = new tbl_PayMaster();
                var docNo = tbl_PayMasters.First().DocNo;
                updateData = db.tbl_PayMaster.FirstOrDefault(x => x.DocNo == docNo);

                if (updateData != null)
                {
                    ret = tbl_PayMasters.BulkUpdate();
                }
                else
                {
                    ret = tbl_PayMasters.BulkInsert();
                }
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }

            msg = "end PayMasterDao=>PerformUpdate";
            msg.WriteLog(null);

            return ret;
        }

        public static List<tbl_PayMaster> GetPayMasterSingle(this tbl_PayMaster tbl_PayMaster, string _DocNo, int _FlagDel)
        {
            List<tbl_PayMaster> list = new List<tbl_PayMaster>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM tbl_PayMaster WHERE FlagDel = " + _FlagDel;
                if (!string.IsNullOrEmpty(_DocNo))
                {
                    sql += " AND DocNo = '" + _DocNo + "'";
                }
                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PayMaster), sql);
                list = dynamicListReturned.Cast<tbl_PayMaster>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayMaster.GetType());
            }

            return list;
        }

        public static int SelectPayMaster_MaxID(this tbl_PayMaster tbl_PayMaster)
        {
            int MaxID = 0;
            try
            {
                string sql = "SELECT TOP 1 MAX(AutoID) FROM tbl_PayMaster";
                var dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    MaxID = dt.Rows[0].Field<int>(0) + 1;
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayMaster.GetType());
            }
            return MaxID;
        }
    }
}
