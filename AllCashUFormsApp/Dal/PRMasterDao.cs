using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class PRMasterDao
    {
        public static List<tbl_PRMaster> Select(this tbl_PRMaster tbl_PRMaster, DateTime docDate)
        {
            List<tbl_PRMaster> list = new List<tbl_PRMaster>();
            try
            {
                string _docDate = docDate.ToString("yyyyMMdd", new CultureInfo("en-US"));

                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_PRMaster] ";
                sql += " WHERE FlagDel = 0 ";
                sql += " AND CAST(DocDate AS DATE) = '" + _docDate + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PRMaster), sql);
                list = dynamicListReturned.Cast<tbl_PRMaster>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            return list;
        }

        public static List<tbl_PRMaster> SelectExists(this tbl_PRMaster tbl_PRMaster, string docNo, string docTypeCode = "")
        {
            List<tbl_PRMaster> list = new List<tbl_PRMaster>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_PRMaster] ";
                sql += " WHERE FlagDel = 0 ";
                sql += " AND DocNo = '" + docNo.Trim() + "' ";

                if (!string.IsNullOrEmpty(docTypeCode))
                    sql += " AND DocTypeCode = '" + docTypeCode.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PRMaster), sql);
                list = dynamicListReturned.Cast<tbl_PRMaster>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            return list;
        }

        public static List<tbl_PRMaster> SelectRefMaxAutoID(this tbl_PRMaster tbl_PRMaster, string docTypeCode = "")
        {
            List<tbl_PRMaster> list = new List<tbl_PRMaster>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * FROM dbo.tbl_PRMaster WHERE AutoID = (SELECT MAX(AutoID) FROM dbo.tbl_PRMaster ";
                    sql += " WHERE DocTypeCode = '" + docTypeCode.Trim() + "' AND DocRef Like '%" + docTypeCode.Trim() + "%' ) ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PRMaster), sql);
                    list = dynamicListReturned.Cast<tbl_PRMaster>().ToList();

                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            return list;
        }

        public static List<tbl_PRMaster> SelectVMaxAutoID(this tbl_PRMaster tbl_PRMaster, string docTypeCode = "")
        {
            List<tbl_PRMaster> list = new List<tbl_PRMaster>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * FROM dbo.tbl_PRMaster WHERE AutoID = (SELECT MAX(AutoID) FROM dbo.tbl_PRMaster ";
                    sql += " WHERE DocTypeCode = '" + docTypeCode.Trim() + "' AND DocNo NOT Like '%V%' ) ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PRMaster), sql);
                    list = dynamicListReturned.Cast<tbl_PRMaster>().ToList();

                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            return list;
        }

        public static List<tbl_PRMaster> SelectMaxAutoID(this tbl_PRMaster tbl_PRMaster, string docTypeCode = "")
        {
            List<tbl_PRMaster> list = new List<tbl_PRMaster>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * FROM dbo.tbl_PRMaster WHERE AutoID = (SELECT MAX(AutoID) FROM dbo.tbl_PRMaster ";
                    sql += " WHERE DocTypeCode = '" + docTypeCode.Trim() + "') ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PRMaster), sql);
                    list = dynamicListReturned.Cast<tbl_PRMaster>().ToList();

                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PRMaster"></param>
        /// <returns></returns>
        public static List<tbl_PRMaster> Select(this tbl_PRMaster tbl_PRMaster, Func<tbl_PRMaster, bool> predicate, string docTypeCode = "")
        {
            List<tbl_PRMaster> list = new List<tbl_PRMaster>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * ";
                    sql += " FROM [dbo].[tbl_PRMaster] ";
                    sql += " WHERE FlagDel = 0 ";
                    sql += " AND DocTypeCode = '" + docTypeCode.Trim() + "' ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PRMaster), sql);
                    list = dynamicListReturned.Cast<tbl_PRMaster>().ToList();
                    list = list.Where(predicate).ToList();
                }
                else
                    list = tbl_PRMaster.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PRMaster.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_PRMaster"></param>
        /// <returns></returns>
        public static List<tbl_PRMaster> SelectAll(this tbl_PRMaster tbl_PRMaster, string docTypeCode = "")
        {
            List<tbl_PRMaster> list = new List<tbl_PRMaster>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_PRMaster] WHERE FlagDel = 0 ";

                if (!string.IsNullOrEmpty(docTypeCode))
                    sql += " AND DocTypeCode = '" + docTypeCode.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PRMaster), sql);
                list = dynamicListReturned.Cast<tbl_PRMaster>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_PRMaster>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PRMaster.OrderBy(x => x.DocNo).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PRMaster"></param>
        /// <returns></returns>
        public static void Insert(this tbl_PRMaster tbl_PRMaster, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PRMasterDao=>InsertWithDB";
            msg.WriteLog(null);

            try
            {
                db.tbl_PRMaster.Attach(tbl_PRMaster);
                db.tbl_PRMaster.Add(tbl_PRMaster);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            msg = "end PRMasterDao=>InsertWithDB";
            msg.WriteLog(null);
        }

        public static void Insert(this List<tbl_PRMaster> tbl_PRMasters, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PRMasterDao=>InsertListWithDB";
            msg.WriteLog(null);

            try
            {
                foreach (var tbl_PRMaster in tbl_PRMasters)
                {
                    db.tbl_PRMaster.Attach(tbl_PRMaster);
                    db.tbl_PRMaster.Add(tbl_PRMaster);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }

            msg = "end PRMasterDao=>InsertListWithDB";
            msg.WriteLog(null);
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PRMaster"></param>
        /// <returns></returns>
        public static int Insert(this tbl_PRMaster tbl_PRMaster)
        {
            string msg = "start PRMasterDao=>Insert";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_PRMaster.Attach(tbl_PRMaster);
                    db.tbl_PRMaster.Add(tbl_PRMaster);
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
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            msg = "end PRMasterDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        public static int UpdateEntity(this tbl_PRMaster tbl_PRMaster, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PRMasterDao=>UpdateEntity";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var updateData = db.tbl_PRMaster.FirstOrDefault(x => x.DocNo == tbl_PRMaster.DocNo);
                if (updateData != null)
                {
                    foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                    {
                        foreach (PropertyInfo tbl_PRMasterItem in tbl_PRMaster.GetType().GetProperties())
                        {
                            if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_PRMasterItem.Name)
                            {
                                var value = tbl_PRMasterItem.GetValue(tbl_PRMaster, null);

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
                    //tbl_PRMaster.Delete(db);
                    tbl_PRMaster.Insert(db);
                }

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            msg = "end PRMasterDao=>UpdateEntity";
            msg.WriteLog(null);

            return ret;
        }

        public static int Update(this List<tbl_PRMaster> tbl_PRMasters)
        {
            string msg = "start PRMasterDao=>UpdateList";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_PRMaster in tbl_PRMasters)
                    {
                        var updateData = db.tbl_PRMaster.FirstOrDefault(x => x.DocNo == tbl_PRMaster.DocNo);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_PRMasterItem in tbl_PRMaster.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_PRMasterItem.Name)
                                    {
                                        var value = tbl_PRMasterItem.GetValue(tbl_PRMaster, null);

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
                            tbl_PRMaster.Delete(db);
                            tbl_PRMaster.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //ex.WriteLog(tbl_PRMaster);
            }

            msg = "end PRMasterDao=>UpdateList";
            msg.WriteLog(null);

            return ret != 0 ? 1 : 0;
        }

        public static int UpdateSQL(this tbl_PRMaster tbl_PRMaster, string sqlCmd)
        {
            string msg = "start PRMasterDao=>UpdateSQL";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                ret = My_DataTable_Extensions.ExecuteSQLScalar(sqlCmd, CommandType.Text);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            msg = "end PRMasterDao=>UpdateSQL";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_PRMaster"></param>
        /// <returns></returns>
        public static int Update(this tbl_PRMaster tbl_PRMaster)
        {
            string msg = "start PRMasterDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_PRMaster.FirstOrDefault(x => x.DocNo == tbl_PRMaster.DocNo);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PRMasterItem in tbl_PRMaster.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_PRMasterItem.Name)
                                {
                                    var value = tbl_PRMasterItem.GetValue(tbl_PRMaster, null);

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
                        ret = tbl_PRMaster.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            msg = "end PRMasterDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PRMaster"></param>
        /// <returns></returns>
        public static int Delete(this tbl_PRMaster tbl_PRMaster)
        {
            string msg = "start PRMasterDao=>Delete";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_PRMaster).State = EntityState.Deleted;
                    db.tbl_PRMaster.Remove(tbl_PRMaster);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            msg = "end PRMasterDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PRMaster"></param>
        /// <returns></returns>
        public static void Delete(this tbl_PRMaster tbl_PRMaster, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start PRMasterDao=>DeleteWithDB";
            msg.WriteLog(null);

            try
            {
                db.Entry(tbl_PRMaster).State = EntityState.Deleted;
                db.tbl_PRMaster.Remove(tbl_PRMaster);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            msg = "end PRMasterDao=>DeleteWithDB";
            msg.WriteLog(null);
        }
    }
}
