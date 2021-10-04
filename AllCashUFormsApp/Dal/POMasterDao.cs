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
                ex.WriteLog(tbl_POMaster.GetType());
            }

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
                ex.WriteLog(tbl_POMaster.GetType());
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
                ex.WriteLog(tbl_POMaster.GetType());
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
                ex.WriteLog(tbl_POMaster.GetType());
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
                ex.WriteLog(tbl_POMaster.GetType());
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
                ex.WriteLog(tbl_POMaster.GetType());
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
                ex.WriteLog(tbl_POMaster.GetType());
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
            try
            {
                db.tbl_POMaster.Attach(tbl_POMaster);
                db.tbl_POMaster.Add(tbl_POMaster);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster.GetType());
            }
        }

        public static int UpdateEntity(this tbl_POMaster tbl_POMaster, DB_ALL_CASH_UNIEntities db, string docTypeCode = "")
        {
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

            return ret;
        }

        public static int Update(this List<tbl_POMaster> tbl_POMasters)
        {
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
                //ex.WriteLog(tbl_POMaster);
            }

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static void Delete(this tbl_POMaster tbl_POMaster, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.Entry(tbl_POMaster).State = EntityState.Deleted;
                db.tbl_POMaster.Remove(tbl_POMaster);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster.GetType());
            }
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static int Insert(this tbl_POMaster tbl_POMaster)
        {
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
                ex.WriteLog(tbl_POMaster.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static int Update(this tbl_POMaster tbl_POMaster)
        {
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
                ex.WriteLog(tbl_POMaster.GetType());
            }

            return ret;
        }

        public static int UpdateSQL(this tbl_POMaster tbl_POMaster, string sqlCmd)
        {
            int ret = 0;
            try
            {
                ret = My_DataTable_Extensions.ExecuteSQLScalar(sqlCmd, CommandType.Text);  
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static int Delete(this tbl_POMaster tbl_POMaster)
        {
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
                ex.WriteLog(tbl_POMaster.GetType());
            }

            return ret;
        }
    }
}
