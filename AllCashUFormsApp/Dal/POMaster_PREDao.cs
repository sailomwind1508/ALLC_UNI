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
    public static class POMaster_PREDao
    {
        public static DataTable GetStockData(string branchID, DateTime docdate, bool currentStock)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_PreOrder_GetStockDataTable";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@BranchID", branchID);
                sqlParmas.Add("@DocDate", docdate.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@CurrentStock", currentStock);

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
                return null;
            }
        }

        public static DataTable GetPOData(string docNo, DateTime docdate, string whid)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_PreOrder_GetPOPreOrder_DataTable";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocNo", docNo);
                sqlParmas.Add("@DocDate", docdate.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@WHID", whid);

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
                return null;
            }
        }

        public static DataTable GetPOMstData(string docNo, DateTime docdate, string whid)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_PreOrder_GetPOMstPreOrder_DataTable";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocNo", docNo);
                sqlParmas.Add("@DocDate", docdate.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@WHID", whid);

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
                return null;
            }
        }

        public static List<tbl_POMaster_PRE> Select(this tbl_POMaster_PRE tbl_POMaster_PRE, DateTime docDate)
        {
            List<tbl_POMaster_PRE> list = new List<tbl_POMaster_PRE>();
            try
            {
                string _docDate = docDate.ToString("yyyyMMdd", new CultureInfo("en-US"));

                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_POMaster_PRE] ";
                sql += " WHERE FlagDel = 0 ";
                sql += " AND CAST(DocDate AS DATE) = '" + _docDate + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster_PRE), sql);
                list = dynamicListReturned.Cast<tbl_POMaster_PRE>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster_PRE.GetType());
            }

            return list;
        }

        public static List<tbl_POMaster_PRE> SelectExistsCustInvNO(this tbl_POMaster_PRE tbl_POMaster_PRE, string docNo, string docTypeCode = "")
        {
            List<tbl_POMaster_PRE> list = new List<tbl_POMaster_PRE>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_POMaster_PRE] ";
                sql += " WHERE FlagDel = 0 ";
                sql += " AND CustInvNO = '" + docNo.Trim() + "' ";

                if (!string.IsNullOrEmpty(docTypeCode))
                    sql += " AND DocTypeCode = '" + docTypeCode.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster_PRE), sql);
                list = dynamicListReturned.Cast<tbl_POMaster_PRE>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster_PRE.GetType());
            }

            return list;
        }

        public static List<tbl_POMaster_PRE> SelectExists(this tbl_POMaster_PRE tbl_POMaster_PRE, string docNo, string docTypeCode = "")
        {
            List<tbl_POMaster_PRE> list = new List<tbl_POMaster_PRE>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_POMaster_PRE] ";
                sql += " WHERE FlagDel = 0 ";
                sql += " AND DocNo = '" + docNo.Trim() + "' ";

                if (!string.IsNullOrEmpty(docTypeCode))
                    sql += " AND DocTypeCode = '" + docTypeCode.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster_PRE), sql);
                list = dynamicListReturned.Cast<tbl_POMaster_PRE>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster_PRE.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_POMaster_PRE"></param>
        /// <returns></returns>
        public static List<tbl_POMaster_PRE> Select(this tbl_POMaster_PRE tbl_POMaster_PRE, Func<tbl_POMaster_PRE, bool> predicate, string docTypeCode = "")
        {
            List<tbl_POMaster_PRE> list = new List<tbl_POMaster_PRE>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * ";
                    sql += " FROM [dbo].[tbl_POMaster_PRE] ";
                    sql += " WHERE FlagDel = 0 ";
                    sql += " AND DocTypeCode = '" + docTypeCode.Trim() + "' ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster_PRE), sql);
                    list = dynamicListReturned.Cast<tbl_POMaster_PRE>().ToList();
                    list = list.Where(predicate).ToList();
                }
                else
                    list = tbl_POMaster_PRE.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_POMaster_PRE.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster_PRE.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_POMaster_PRE"></param>
        /// <returns></returns>
        public static List<tbl_POMaster_PRE> SelectAll(this tbl_POMaster_PRE tbl_POMaster_PRE, string docTypeCode = "")
        {
            List<tbl_POMaster_PRE> list = new List<tbl_POMaster_PRE>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_POMaster_PRE] WHERE FlagDel = 0 ";

                if (!string.IsNullOrEmpty(docTypeCode))
                    sql += " AND DocTypeCode = '" + docTypeCode.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_POMaster_PRE), sql);
                list = dynamicListReturned.Cast<tbl_POMaster_PRE>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_POMaster_PRE>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_POMaster_PRE.OrderBy(x => x.DocNo).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster_PRE.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_POMaster_PRE"></param>
        /// <returns></returns>
        public static void Insert(this tbl_POMaster_PRE tbl_POMaster_PRE, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start POMaster_PREDao=>InsertWithDB";
            msg.WriteLog(null);

            try
            {
                db.tbl_POMaster_PRE.Attach(tbl_POMaster_PRE);
                db.tbl_POMaster_PRE.Add(tbl_POMaster_PRE);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster_PRE.GetType());
            }

            msg = "end POMaster_PREDao=>InsertWithDB";
            msg.WriteLog(null);
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_POMaster_PRE"></param>
        /// <returns></returns>
        public static int Insert(this tbl_POMaster_PRE tbl_POMaster_PRE)
        {
            string msg = "start POMaster_PREDao=>Insert";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_POMaster_PRE.Attach(tbl_POMaster_PRE);
                    db.tbl_POMaster_PRE.Add(tbl_POMaster_PRE);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster_PRE.GetType());
            }

            msg = "end POMaster_PREDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        public static int UpdateEntity(this tbl_POMaster_PRE tbl_POMaster_PRE, DB_ALL_CASH_UNIEntities db, string docTypeCode = "")
        {
            string msg = "start POMaster_PREDao=>UpdateEntity";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                var updateData = db.tbl_POMaster_PRE.FirstOrDefault(x => x.DocNo == tbl_POMaster_PRE.DocNo);
                if (updateData != null)
                {
                    foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                    {
                        foreach (PropertyInfo tbl_POMaster_PREItem in tbl_POMaster_PRE.GetType().GetProperties())
                        {
                            if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_POMaster_PREItem.Name)
                            {
                                var value = tbl_POMaster_PREItem.GetValue(tbl_POMaster_PRE, null);

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
                    //tbl_POMaster_PRE.Delete(db);
                    tbl_POMaster_PRE.Insert(db);
                }

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            msg = "end POMaster_PREDao=>UpdateEntity";
            msg.WriteLog(null);

            return ret;
        }

        public static int Update(this List<tbl_POMaster_PRE> tbl_POMaster_PREs)
        {
            string msg = "start POMaster_PREDao=>UpdateList";
            msg.WriteLog(null);

            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_POMaster_PRE in tbl_POMaster_PREs)
                    {
                        var updateData = db.tbl_POMaster_PRE.FirstOrDefault(x => x.DocNo == tbl_POMaster_PRE.DocNo);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_POMaster_PREItem in tbl_POMaster_PRE.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_POMaster_PREItem.Name)
                                    {
                                        var value = tbl_POMaster_PREItem.GetValue(tbl_POMaster_PRE, null);

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
                            tbl_POMaster_PRE.Delete(db);
                            tbl_POMaster_PRE.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //ex.WriteLog(tbl_POMaster_PRE);
            }

            msg = "end POMaster_PREDao=>UpdateList";
            msg.WriteLog(null);

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_POMaster_PRE"></param>
        /// <returns></returns>
        public static int Update(this tbl_POMaster_PRE tbl_POMaster_PRE)
        {
            string msg = "start POMaster_PREDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_POMaster_PRE.FirstOrDefault(x => x.DocNo == tbl_POMaster_PRE.DocNo);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_POMaster_PREItem in tbl_POMaster_PRE.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_POMaster_PREItem.Name)
                                {
                                    var value = tbl_POMaster_PREItem.GetValue(tbl_POMaster_PRE, null);

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
                        ret = tbl_POMaster_PRE.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster_PRE.GetType());
            }

            msg = "end POMaster_PREDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_POMaster_PRE"></param>
        /// <returns></returns>
        public static void Delete(this tbl_POMaster_PRE tbl_POMaster_PRE, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start POMaster_PREDao=>DeleteWithDB";
            msg.WriteLog(null);

            try
            {
                db.Entry(tbl_POMaster_PRE).State = EntityState.Deleted;
                db.tbl_POMaster_PRE.Remove(tbl_POMaster_PRE);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster_PRE.GetType());
            }

            msg = "end POMaster_PREDao=>DeleteWithDB";
            msg.WriteLog(null);
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_POMaster_PRE"></param>
        /// <returns></returns>
        public static int Delete(this tbl_POMaster_PRE tbl_POMaster_PRE)
        {
            string msg = "start POMaster_PREDao=>Delete";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_POMaster_PRE).State = EntityState.Deleted;
                    db.tbl_POMaster_PRE.Remove(tbl_POMaster_PRE);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster_PRE.GetType());
            }

            msg = "end POMaster_PREDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }
    }
}
