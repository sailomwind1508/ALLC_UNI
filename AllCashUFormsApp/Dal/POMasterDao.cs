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
    public static class POMasterDao
    {
        public static int UpdateCustInvNo(this tbl_POMaster tbl_POMaster)
        {
            string msg = "start POMasterDao=>UpdateCustInvNo";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " UPDATE dbo.tbl_POMaster ";
                sql += " SET CustInvNO = '" + tbl_POMaster.CustInvNO + "', FlagSend = 0 ";
                sql += " WHERE DocNo = '" + tbl_POMaster.DocNo + "' ";

                ret = My_DataTable_Extensions.ExecuteSQLScalar(sql, CommandType.Text, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            msg = "end POMasterDao=>UpdateCustInvNo";
            msg.WriteLog(null);

            return ret;
        }

        public static List<tbl_POMaster> Select(this tbl_POMaster tbl_POMaster, DateTime docDate)
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                string _docDate = docDate.ToString("yyyyMMdd", new CultureInfo("en-US"));

                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_POMaster] ";
                sql += " WHERE FlagDel = 0 ";
                sql += " AND CAST(DocDate AS DATE) = '" + _docDate + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                list = dynamicListReturned.Cast<tbl_POMaster>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            return list;
        }

        public static List<tbl_POMaster> SelectExistsCustInvNO(this tbl_POMaster tbl_POMaster, string docNo, string docTypeCode = "")
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_POMaster] ";
                sql += " WHERE FlagDel = 0 ";
                sql += " AND CustInvNO = '" + docNo.Trim() + "' ";

                if (!string.IsNullOrEmpty(docTypeCode))
                    sql += " AND DocTypeCode = '" + docTypeCode.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                list = dynamicListReturned.Cast<tbl_POMaster>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            return list;
        }

        public static List<tbl_POMaster> SelectExists(this tbl_POMaster tbl_POMaster, string docNo, string docTypeCode = "")
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_POMaster] ";
                sql += " WHERE FlagDel = 0 ";
                sql += " AND DocNo = '" + docNo.Trim() + "' ";

                if (!string.IsNullOrEmpty(docTypeCode))
                    sql += " AND DocTypeCode = '" + docTypeCode.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                list = dynamicListReturned.Cast<tbl_POMaster>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static List<tbl_POMaster> Select(this tbl_POMaster tbl_POMaster, string sqlFilter)
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                if (!string.IsNullOrEmpty(sqlFilter))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * ";
                    sql += " FROM [dbo].[tbl_POMaster] ";
                    sql += " WHERE FlagDel = 0 ";
                    sql += " AND " + sqlFilter;

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                    list = dynamicListReturned.Cast<tbl_POMaster>().ToList();

                }
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_POMaster.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            return list;
        }

        //x.DocTypeCode.Trim() == "IV" && !string.IsNullOrEmpty(x.DocRef) && x.DocRef.Trim() == docTypeCode

        public static List<tbl_POMaster> SelectRefMaxAutoIDPRE(this tbl_POMaster tbl_POMaster, string whid, string docTypeCode = "")
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * FROM dbo.tbl_POMaster WHERE AutoID = (SELECT MAX(AutoID) FROM dbo.tbl_POMaster ";
                    sql += " WHERE ISNULL(DocRef,'') <> '' AND DocTypeCode = 'IV'  AND WHID = '" + whid + "' AND ISNULL(DocRef,'') = '" + docTypeCode.Trim() + "') ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                    list = dynamicListReturned.Cast<tbl_POMaster>().ToList();

                }

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_POMaster.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            return list;
        }

        public static string SelectMaxAutoIDPRE_ReGen(this tbl_POMaster tbl_POMaster, string docNo)
        {
            string result = "";
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_PreOrder_SelectMaxPODocNo";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@TemDocNo", docNo);

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                if (newTable != null && newTable.Rows.Count > 0)
                {
                    result = newTable.Rows[0]["DocNo"].ToString();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            return result;
        }

        public static List<tbl_POMaster> SelectMaxAutoIDPRE(this tbl_POMaster tbl_POMaster, string whid, string docTypeCode = "")
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    if (docTypeCode.Trim() == "IV")
                    {
                        sql += " SELECT * FROM dbo.tbl_POMaster WHERE AutoID = (SELECT MAX(AutoID) FROM dbo.tbl_POMaster ";
                        sql += " WHERE DocTypeCode = '" + docTypeCode.Trim() + "' AND WHID = '" + whid + "' AND ISNULL(DocRef, '') = '' )";
                    }
                    else
                    {
                        sql += " SELECT * FROM dbo.tbl_POMaster WHERE AutoID = (SELECT MAX(AutoID) FROM dbo.tbl_POMaster ";
                        sql += " WHERE DocTypeCode = '" + docTypeCode.Trim() + "' AND WHID = '" + whid + "' )";
                    }


                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                    list = dynamicListReturned.Cast<tbl_POMaster>().ToList();

                }

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_POMaster.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            return list;
        }

        public static List<tbl_POMaster> SelectRefMaxAutoID(this tbl_POMaster tbl_POMaster, string docTypeCode = "")
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * FROM dbo.tbl_POMaster WHERE AutoID = (SELECT MAX(AutoID) FROM dbo.tbl_POMaster ";
                    sql += " WHERE ISNULL(DocRef,'') <> '' AND DocTypeCode = 'IV' AND ISNULL(DocRef,'') = '" + docTypeCode.Trim() + "') ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                    list = dynamicListReturned.Cast<tbl_POMaster>().ToList();
                }

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_POMaster.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            return list;
        }

        public static List<tbl_POMaster> SelectMaxAutoID(this tbl_POMaster tbl_POMaster, string docTypeCode = "")
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    if (docTypeCode.Trim() == "IV")
                    {
                        //sql += " SELECT * FROM dbo.tbl_POMaster WHERE AutoID = (SELECT MAX(AutoID) FROM dbo.tbl_POMaster "; //edit by sailom.k 19/11/2022
                        sql += " SELECT * FROM dbo.tbl_POMaster WHERE CrDate = (SELECT MAX(CrDate) FROM dbo.tbl_POMaster ";
                        sql += " WHERE DocTypeCode = '" + docTypeCode.Trim() + "' AND ISNULL(DocRef, '') = '' )";
                    }
                    else
                    {
                        //sql += " SELECT * FROM dbo.tbl_POMaster WHERE AutoID = (SELECT MAX(AutoID) FROM dbo.tbl_POMaster "; //edit by sailom.k 19/11/2022
                        sql += " SELECT * FROM dbo.tbl_POMaster WHERE CrDate = (SELECT MAX(CrDate) FROM dbo.tbl_POMaster ";
                        sql += " WHERE DocTypeCode = '" + docTypeCode.Trim() + "' )";
                    }
                    
                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                    list = dynamicListReturned.Cast<tbl_POMaster>().ToList();
       
                }
                
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_POMaster.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            return list;
        }

        /// <summary>
        /// for support max docno from tablet sales transaction by sailom .k 21/11/2022
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <param name="docTypeCode"></param>
        /// <param name="whid"></param>
        /// <param name="docdate"></param>
        /// <returns></returns>
        public static List<tbl_POMaster> SelectNewMaxAutoID(this tbl_POMaster tbl_POMaster, string docTypeCode, string whid, DateTime docdate)
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    if (docTypeCode.Trim() == "IV")
                    {
                        sql += @" SELECT * FROM dbo.tbl_POMaster 
                                WHERE CrDate = 
                                (
	                                SELECT MAX(CrDate) 
	                                FROM dbo.tbl_POMaster 
                                    WHERE ISNULL(DocRef, '') = '' ";
                        sql += "    AND DocTypeCode = '" + docTypeCode.Trim() + "' ";
                        sql += "    AND WHID = '" + whid + "' ";
                        sql += "    AND DocDate = '" + docdate.ToString("yyyyMMdd", new CultureInfo("en-US")) + "' ";
                        sql += " ) ";
                    }

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                    list = dynamicListReturned.Cast<tbl_POMaster>().ToList();

                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static List<tbl_POMaster> Select(this tbl_POMaster tbl_POMaster, Func<tbl_POMaster, bool> predicate, string docTypeCode = "")
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * ";
                    sql += " FROM [dbo].[tbl_POMaster] ";
                    sql += " WHERE FlagDel = 0 ";
                    sql += " AND DocTypeCode = '" + docTypeCode.Trim() + "' ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                    list = dynamicListReturned.Cast<tbl_POMaster>().ToList();
                    list = list.Where(predicate).ToList();
                }
                else
                    list = tbl_POMaster.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_POMaster.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static List<tbl_POMaster> SelectAll(this tbl_POMaster tbl_POMaster, string docTypeCode = "")
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_POMaster] WHERE FlagDel = 0 ";

                if (!string.IsNullOrEmpty(docTypeCode))
                    sql += " AND DocTypeCode = '" + docTypeCode.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                list = dynamicListReturned.Cast<tbl_POMaster>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_POMaster>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_POMaster.OrderBy(x => x.DocNo).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static void Insert(this tbl_POMaster tbl_POMaster, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start POMasterDao=>InsertWithDB";
            msg.WriteLog(null);

            try
            {
                db.tbl_POMaster.Attach(tbl_POMaster);
                db.tbl_POMaster.Add(tbl_POMaster);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            msg = "end POMasterDao=>InsertWithDB";
            msg.WriteLog(null);
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static int Insert(this tbl_POMaster tbl_POMaster)
        {
            string msg = "start POMasterDao=>Insert";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_POMaster.Attach(tbl_POMaster);
                    db.tbl_POMaster.Add(tbl_POMaster);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            msg = "end POMasterDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        public static int UpdateEntity(this tbl_POMaster tbl_POMaster, DB_ALL_CASH_UNIEntities db, string docTypeCode = "")
        {
            string msg = "start POMasterDao=>UpdateEntity";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var updateData = db.tbl_POMaster.FirstOrDefault(x => x.DocNo == tbl_POMaster.DocNo);
                if (updateData != null)
                {
                    foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                    {
                        foreach (PropertyInfo tbl_POMasterItem in tbl_POMaster.GetType().GetProperties())
                        {
                            if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_POMasterItem.Name)
                            {
                                var value = tbl_POMasterItem.GetValue(tbl_POMaster, null);

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
                    //tbl_POMaster.Delete(db);
                    tbl_POMaster.Insert(db);
                }

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            msg = "end POMasterDao=>UpdateEntity";
            msg.WriteLog(null);

            return ret;
        }

        public static int Update(this List<tbl_POMaster> tbl_POMasters)
        {
            string msg = "start POMasterDao=>Update->List<tbl_POMaster>";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_POMaster in tbl_POMasters)
                    {
                        var updateData = db.tbl_POMaster.FirstOrDefault(x => x.DocNo == tbl_POMaster.DocNo);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_POMasterItem in tbl_POMaster.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_POMasterItem.Name)
                                    {
                                        var value = tbl_POMasterItem.GetValue(tbl_POMaster, null);

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
                            tbl_POMaster.Delete(db);
                            tbl_POMaster.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
                //ex.WriteLog(tbl_POMaster);
            }

            msg = "end POMasterDao=>Update->List<tbl_POMaster>";
            msg.WriteLog(null);

            return ret != 0 ? 1 : 0;
        }

        public static int UpdateDate_POMaster(this List<tbl_POMaster> tbl_POMasters)
        {
            string msg = "start POMasterDao=>UpdateDate_POMaster";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_POMaster in tbl_POMasters)
                    {
                        var updateData = db.tbl_POMaster.FirstOrDefault(x => x.DocNo == tbl_POMaster.Remark);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_POMasterItem in tbl_POMaster.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_POMasterItem.Name)
                                    {
                                        var value = tbl_POMasterItem.GetValue(tbl_POMaster, null);

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
                            tbl_POMaster.Delete(db);
                            tbl_POMaster.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMasters != null ? tbl_POMasters.GetType() : null);
            }

            msg = "end POMasterDao=>UpdateDate_POMaster";
            msg.WriteLog(null);

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static int Update(this tbl_POMaster tbl_POMaster)
        {
            string msg = "start POMasterDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_POMaster.FirstOrDefault(x => x.DocNo == tbl_POMaster.DocNo);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_POMasterItem in tbl_POMaster.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_POMasterItem.Name)
                                {
                                    var value = tbl_POMasterItem.GetValue(tbl_POMaster, null);

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
                        ret = tbl_POMaster.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            msg = "end POMasterDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        public static int UpdateSQL(this tbl_POMaster tbl_POMaster, string sqlCmd)
        {
            string msg = "start POMasterDao=>UpdateSQL";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                ret = My_DataTable_Extensions.ExecuteSQLScalar(sqlCmd, CommandType.Text);  
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            msg = "end POMasterDao=>UpdateSQL";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static int Delete(this tbl_POMaster tbl_POMaster)
        {
            string msg = "start POMasterDao=>Delete";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_POMaster).State = EntityState.Deleted;
                    db.tbl_POMaster.Remove(tbl_POMaster);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            msg = "end POMasterDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static void Delete(this tbl_POMaster tbl_POMaster, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start POMasterDao=>DeleteWithDB";
            msg.WriteLog(null);

            try
            {
                db.Entry(tbl_POMaster).State = EntityState.Deleted;
                db.tbl_POMaster.Remove(tbl_POMaster);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            msg = "end POMasterDao=>DeleteWithDB";
            msg.WriteLog(null);
        }

        public static List<tbl_POMaster> SelectCustomer_POMaster(this tbl_POMaster tbl_POMaster,string CustomerID)
        {
            string msg = "start POMasterDao=>SelectCustomer_POMaster";
            msg.WriteLog(null);

            var list = new List<tbl_POMaster>();
            try
            {
                string sql = "SELECT TOP 1 * FROM tbl_POMaster WHERE CustomerID = '" + CustomerID + "' ORDER BY DocDate DESC";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                list = dynamicListReturned.Cast<tbl_POMaster>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            msg = "end POMasterDao=>SelectCustomer_POMaster";
            msg.WriteLog(null);

            return list;
        }

        public static List<tbl_POMaster> GetPOMasterSingle(this tbl_POMaster tbl_POMaster, string _DocTypeCode, string _DocStatus, string _DocDate)
        {
            string msg = "start POMasterDao=>GetPOMasterSingle";
            msg.WriteLog(null);

            var list = new List<tbl_POMaster>();
            try
            {
                string sql = "SELECT *  FROM tbl_POMaster ";
                sql += " WHERE FlagDel = 0 ";
                if (!string.IsNullOrEmpty(_DocTypeCode))
                {
                    sql += " AND DocTypeCode = '" + _DocTypeCode.Trim() + "'";
                }
                if (!string.IsNullOrEmpty(_DocStatus))
                {
                    sql += " AND DocStatus = '" + _DocStatus.Trim() + "'";
                }
                if (!string.IsNullOrEmpty(_DocDate))
                {
                    sql += " AND cast(DocDate as date) = '" + _DocDate + "'";
                }

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                list = dynamicListReturned.Cast<tbl_POMaster>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            msg = "end POMasterDao=>GetPOMasterSingle";
            msg.WriteLog(null);

            return list;
        }

        public static int ValidatePOMaster(this tbl_POMaster tbl_POMaster, string _DocDate, string _WHID)
        {
            string msg = "start POMasterDao=>ValidatePOMaster";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                string sql = "SELECT TOP 1 1 AS Result FROM tbl_POMaster WHERE 1=1";

                if (!string.IsNullOrEmpty(_DocDate))
                    sql += " AND CAST(DocDate AS DATE) = '" + _DocDate + "'";

                if (!string.IsNullOrEmpty(_WHID))
                    sql += " AND WHID = '" + _WHID + "'";

                var dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                if (dt.Rows.Count > 0)
                {
                    ret = dt.Rows[0].Field<int>("Result");
                }
                
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            msg = "end POMasterDao=>ValidatePOMaster";
            msg.WriteLog(null);

            return ret;
        }

        public static List<tbl_POMaster> GetPOMaster_ByWHID(this tbl_POMaster tbl_POMaster, string _DocDate, string WHID)
        {
            string msg = "start POMasterDao=>GetPOMaster_ByWHID";
            msg.WriteLog(null);

            var list = new List<tbl_POMaster>();
            try
            {
                string sql = "SELECT * FROM tbl_POMaster WHERE 1=1";

                if (!string.IsNullOrEmpty(_DocDate))
                    sql += " AND CAST(DocDate AS DATE) = '" + _DocDate + "'";

                if (!string.IsNullOrEmpty(WHID))
                    sql += " AND WHID = '" + WHID.Trim() + "'";

                sql += " ORDER BY AutoID ";
                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster), sql);
                list = dynamicListReturned.Cast<tbl_POMaster>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            msg = "end POMasterDao=>GetPOMaster_ByWHID";
            msg.WriteLog(null);

            return list;
        }

        public static int UpdateDatePOMaster(this List<tbl_POMaster> tbl_POMaster)
        {
            string msg = "start POMasterDao=>UpdateDatePOMaster";
            msg.WriteLog(null);
            
            List<int> ret = new List<int>();
            int ret1 = new int();
            
            try
            {
                for (int i = 0; i < tbl_POMaster.Count; i++)
                {
                    SqlConnection con = new SqlConnection(Connection.ConnectionString);
                    string sql = "UPDATE tbl_POMaster ";
                    sql += "SET DocDate = @DocDate";
                    sql += ", EdDate = @EdDate";
                    sql += ", EdUser = @EdUser";
                    sql += ", Remark = @Remark";
                    sql += ", DocNo = @DocNo";

                    sql += " WHERE DocNo = '" + tbl_POMaster[i].Remark.Trim() + "'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@Remark", tbl_POMaster[i].Remark.Trim());
                    cmd.Parameters.AddWithValue("@DocDate", tbl_POMaster[i].DocDate);
                    cmd.Parameters.AddWithValue("@EdDate", tbl_POMaster[i].EdDate);
                    cmd.Parameters.AddWithValue("@EdUser", tbl_POMaster[i].EdUser);
                    cmd.Parameters.AddWithValue("@DocNo", tbl_POMaster[i].DocNo);

                    ret.Add(cmd.ExecuteNonQuery());
                    con.Close();
                }

                if (ret.All(x=>x == 1))
                    ret1 = 1;
                else
                    ret1 = 0;

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
                ret1 = 0;
            }

            msg = "end POMasterDao=>UpdateDatePOMaster";
            msg.WriteLog(null);

            return ret1;
        }

        public static int BulkInsert(this tbl_POMaster tbl_POMaster)
        {
            string msg = "start POMasterDao=>BulkInsert";
            msg.WriteLog(null);

            int ret = 1;

            List<tbl_POMaster> tbl_POMasters = new List<tbl_POMaster>();
            tbl_POMasters.Add(tbl_POMaster);

            var table = tbl_POMasters.ToDataTable();
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
                                bcp.DestinationTableName = "tbl_POMaster";
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

            msg = "end POMasterDao=>BulkInsert";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkUpdate(this tbl_POMaster tbl_POMaster)
        {
            string msg = "start POMasterDao=>BulkUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                string sql = " DELETE FROM tbl_POMaster WHERE DocNo = '" + tbl_POMaster.DocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////

                ret = tbl_POMaster.BulkInsert();
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end POMasterDao=>UpdateList";
            msg.WriteLog(null);

            return ret;
        }

        public static int BulkRemove(this tbl_POMaster tbl_POMaster)
        {
            string msg = "start POMasterDao=>BulkRemove";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                string sql = " DELETE FROM tbl_POMaster WHERE DocNo = '" + tbl_POMaster.DocNo + "' ";

                My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);/////////////////////////

                ret = 1;
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }


            msg = "end POMasterDao=>BulkRemove";
            msg.WriteLog(null);

            return ret;
        }

        public static int PerformUpdate(this tbl_POMaster tbl_POMaster, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start POMasterDao=>PerformUpdate";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var updateData = new tbl_POMaster();
                var docNo = tbl_POMaster.DocNo;
                updateData = db.tbl_POMaster.FirstOrDefault(x => x.DocNo == docNo);

                if (updateData != null)
                {
                    ret = tbl_POMaster.BulkUpdate();
                }
                else
                {
                    ret = tbl_POMaster.BulkInsert();
                }
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(null);
            }

            msg = "end POMasterDao=>PerformUpdate";
            msg.WriteLog(null);

            return ret;
        }

        public static List<tbl_POMaster> GetPOMaster_AllBranch(this tbl_POMaster tbl_POMaster, Dictionary<string, object> Params)
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                string sql = "proc_POMaster_GetData_AllBranch";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteStoreToList(typeof(tbl_POMaster), sql, Params);
                list = dynamicListReturned.Cast<tbl_POMaster>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster != null ? tbl_POMaster.GetType() : null);
            }

            return list;
        }
    }
}
