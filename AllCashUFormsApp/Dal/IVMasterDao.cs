using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class IVMasterDao
    {
        public static List<tbl_IVMaster> SelectWithDocNo(this tbl_IVMaster tbl_IVMaster, string docNo)
        {
            List<tbl_IVMaster> list = new List<tbl_IVMaster>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_IVMaster] ";
                sql += " WHERE DocNo = '" + docNo + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_IVMaster), sql);
                list = dynamicListReturned.Cast<tbl_IVMaster>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
            }

            return list;
        }

        public static List<tbl_IVMaster> Select(this tbl_IVMaster tbl_IVMaster, DateTime docDate)
        {
            List<tbl_IVMaster> list = new List<tbl_IVMaster>();
            try
            {
                string _docDate = docDate.ToString("yyyyMMdd", new CultureInfo("en-US"));

                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_IVMaster] ";
                sql += " WHERE FlagDel = 0 ";
                sql += " AND CAST(DocDate AS DATE) = '" + _docDate + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_IVMaster), sql);
                list = dynamicListReturned.Cast<tbl_IVMaster>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            return list;
        }

        public static List<tbl_IVMaster> SelectExists(this tbl_IVMaster tbl_IVMaster, string docNo, string docTypeCode = "")
        {
            List<tbl_IVMaster> list = new List<tbl_IVMaster>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_IVMaster] ";
                sql += " WHERE FlagDel = 0 ";
                sql += " AND DocNo = '" + docNo.Trim() + "' ";

                if (!string.IsNullOrEmpty(docTypeCode))
                    sql += " AND DocTypeCode = '" + docTypeCode.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_IVMaster), sql);
                list = dynamicListReturned.Cast<tbl_IVMaster>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            return list;
        }

        public static List<tbl_IVMaster> SelectMaxAutoID(this tbl_IVMaster tbl_IVMaster, string docTypeCode = "")
        {
            List<tbl_IVMaster> list = new List<tbl_IVMaster>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    if (docTypeCode.Trim() == "V")
                    {
                        //sql += " SELECT * FROM dbo.tbl_IVMaster WHERE CrDate = (SELECT MAX(CrDate) FROM dbo.tbl_IVMaster "; //last edit by sailom .k 25/11/2022
                        //sql += " WHERE DocTypeCode = '" + docTypeCode.Trim() + "') ";

                        //sql += @" SELECT * FROM dbo.tbl_IVMaster WHERE CrDate = (SELECT TOP 1 ti.CrDate FROM tbl_IVMaster ti
                        //    WHERE ti.DocTypeCode = 'V' ORDER BY ti.DocDate DESC, ti.CrDate DESC) "; //last edit by sailom.k 15/12/2022

                        sql += @" SELECT * FROM dbo.tbl_IVMaster WHERE CAST(SUBSTRING(DocNo, 3, LEN(DocNo)) AS INT) = 
                                (SELECT MAX(CAST(SUBSTRING(ti.DocNo, 3, LEN(ti.DocNo)) AS INT)) FROM tbl_IVMaster ti WHERE ti.DocTypeCode = 'V') ";  //last edit by sailom.k 17/12/2022                       
                    }
                    else
                    {
                        sql += " SELECT * FROM dbo.tbl_IVMaster WHERE AutoID = (SELECT MAX(AutoID) FROM dbo.tbl_IVMaster "; //last edit by sailom .k 10/09/2022
                        sql += " WHERE DocTypeCode = '" + docTypeCode.Trim() + "') ";
                    }
                    

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_IVMaster), sql);
                    list = dynamicListReturned.Cast<tbl_IVMaster>().ToList();

                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_IVMaster"></param>
        /// <returns></returns>
        public static List<tbl_IVMaster> Select(this tbl_IVMaster tbl_IVMaster, Func<tbl_IVMaster, bool> predicate)
        {
            List<tbl_IVMaster> list = new List<tbl_IVMaster>();
            try
            {
                list = tbl_IVMaster.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    //var data = db.tbl_IVMaster.ToList().Where(predicate);
                //    list = db.tbl_IVMaster.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_IVMaster"></param>
        /// <returns></returns>
        public static List<tbl_IVMaster> SelectAll(this tbl_IVMaster tbl_IVMaster)
        {
            List<tbl_IVMaster> list = new List<tbl_IVMaster>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_IVMaster] WHERE FlagDel = 0 ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_IVMaster), sql);
                list = dynamicListReturned.Cast<tbl_IVMaster>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_IVMaster>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_IVMaster.OrderBy(x => x.DocNo).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_IVMaster"></param>
        /// <returns></returns>
        public static void Insert(this tbl_IVMaster tbl_IVMaster, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start IVMasterDao=>InsertWithDB";
            msg.WriteLog(null);

            try
            {
                db.tbl_IVMaster.Attach(tbl_IVMaster);
                db.tbl_IVMaster.Add(tbl_IVMaster);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            msg = "end IVMasterDao=>InsertWithDB";
            msg.WriteLog(null);
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_IVMaster"></param>
        /// <returns></returns>
        public static int Insert(this tbl_IVMaster tbl_IVMaster)
        {
            string msg = "start IVMasterDao=>Insert";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_IVMaster.Attach(tbl_IVMaster);
                    db.tbl_IVMaster.Add(tbl_IVMaster);
                    ret = db.SaveChanges();
                }
            }
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            msg = "end IVMasterDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        public static int UpdateEntity(this tbl_IVMaster tbl_IVMaster, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start IVMasterDao=>UpdateEntity";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var updateData = db.tbl_IVMaster.FirstOrDefault(x => x.DocNo == tbl_IVMaster.DocNo);
                if (updateData != null)
                {
                    foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                    {
                        foreach (PropertyInfo tbl_IVMasterItem in tbl_IVMaster.GetType().GetProperties())
                        {
                            if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_IVMasterItem.Name)
                            {
                                var value = tbl_IVMasterItem.GetValue(tbl_IVMaster, null);

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
                    //tbl_IVMaster.Delete(db);
                    tbl_IVMaster.Insert(db);
                }

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            msg = "end IVMasterDao=>UpdateEntity";
            msg.WriteLog(null);

            return ret;
        }

        public static int Update(this List<tbl_IVMaster> tbl_IVMasters)
        {
            string msg = "start IVMasterDao=>UpdateList";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_IVMaster in tbl_IVMasters)
                    {
                        var updateData = db.tbl_IVMaster.FirstOrDefault(x => x.DocNo == tbl_IVMaster.DocNo);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_IVMasterItem in tbl_IVMaster.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_IVMasterItem.Name)
                                    {
                                        var value = tbl_IVMasterItem.GetValue(tbl_IVMaster, null);

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
                            tbl_IVMaster.Delete(db);
                            tbl_IVMaster.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
                //ex.WriteLog(tbl_IVMaster);
            }

            msg = "end IVMasterDao=>UpdateList";
            msg.WriteLog(null);

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_IVMaster"></param>
        /// <returns></returns>
        public static int Update(this tbl_IVMaster tbl_IVMaster)
        {
            string msg = "start IVMasterDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_IVMaster.FirstOrDefault(x => x.DocNo == tbl_IVMaster.DocNo);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_IVMasterItem in tbl_IVMaster.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_IVMasterItem.Name)
                                {
                                    var value = tbl_IVMasterItem.GetValue(tbl_IVMaster, null);

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
                        ret = tbl_IVMaster.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            msg = "end IVMasterDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_IVMaster"></param>
        /// <returns></returns>
        public static void Delete(this tbl_IVMaster tbl_IVMaster, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start IVMasterDao=>DeleteWithDB";
            msg.WriteLog(null);

            try
            {
                db.Entry(tbl_IVMaster).State = EntityState.Deleted;
                db.tbl_IVMaster.Remove(tbl_IVMaster);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            msg = "end IVMasterDao=>DeleteWithDB";
            msg.WriteLog(null);
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_IVMaster"></param>
        /// <returns></returns>
        public static int Delete(this tbl_IVMaster tbl_IVMaster)
        {
            string msg = "start IVMasterDao=>Delete";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_IVMaster).State = EntityState.Deleted;
                    db.tbl_IVMaster.Remove(tbl_IVMaster);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            msg = "end IVMasterDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkInsert(this tbl_IVMaster tbl_IVMaster)
        {
            string msg = "start IVMasterDao=>BulkInsert";
            msg.WriteLog(null);

            int ret = 1;

            List<tbl_IVMaster> tbl_IVMasters = new List<tbl_IVMaster>();
            tbl_IVMasters.Add(tbl_IVMaster);

            var table = tbl_IVMasters.ToDataTable();
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
                                bcp.DestinationTableName = "tbl_IVMaster";
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

            msg = "end IVMasterDao=>BulkInsert";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkUpdate(this tbl_IVMaster tbl_IVMaster)
        {
            string msg = "start IVMasterDao=>BulkUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                string sql = " DELETE FROM tbl_IVMaster WHERE DocNo = '" + tbl_IVMaster.DocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////

                ret = tbl_IVMaster.BulkInsert();
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end IVMasterDao=>UpdateList";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkRemove(this tbl_IVMaster tbl_IVMaster)
        {
            string msg = "start IVMasterDao=>BulkRemove";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                string sql = " DELETE FROM tbl_IVMaster WHERE DocNo = '" + tbl_IVMaster.DocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////

                ret = 1;
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end IVMasterDao=>BulkRemove";
            msg.WriteLog(null);

            return ret;
        }

        public static int PerformUpdate(this tbl_IVMaster tbl_IVMaster, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start IVMasterDao=>PerformUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var updateData = new tbl_IVMaster();
                var docNo = tbl_IVMaster.DocNo;
                updateData = db.tbl_IVMaster.FirstOrDefault(x => x.DocNo == docNo);

                if (updateData != null)
                {
                    ret = tbl_IVMaster.BulkUpdate();
                }
                else
                {
                    ret = tbl_IVMaster.BulkInsert();
                }
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }

            msg = "end IVMasterDao=>PerformUpdate";
            msg.WriteLog(null);

            return ret;
        }
    }
}
