﻿using AllCashUFormsApp.Model;
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
    public static class PODetail_PREDao
    {
        public static List<tbl_PODetail_PRE> Select(this tbl_PODetail_PRE tbl_PODetail_PRE, DateTime docDate)
        {
            List<tbl_PODetail_PRE> list = new List<tbl_PODetail_PRE>();
            try
            {
                string _docDate = docDate.ToString("yyyyMMdd", new CultureInfo("en-US"));

                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT t1.* ";
                sql += " FROM [dbo].[tbl_PODetail_PRE] t1 ";
                sql += " INNER JOIN dbo.tbl_POMaster_PRE t2 ON t1.DocNo = t2.DocNo ";
                sql += " WHERE t1.FlagDel = 0 ";
                sql += " AND CAST(t2.DocDate AS DATE) = '" + _docDate + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PODetail_PRE), sql);
                list = dynamicListReturned.Cast<tbl_PODetail_PRE>().ToList();

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail_PRE.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PODetail_PRE"></param>
        /// <returns></returns>
        public static List<tbl_PODetail_PRE> Select(this tbl_PODetail_PRE tbl_PODetail_PRE, string docNo)
        {
            List<tbl_PODetail_PRE> list = new List<tbl_PODetail_PRE>();
            try
            {
                if (!string.IsNullOrEmpty(docNo))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT t1.* ";
                    sql += " FROM [dbo].[tbl_PODetail_PRE] t1 ";
                    sql += " INNER JOIN dbo.tbl_POMaster_PRE t2 ON t1.DocNo = t2.DocNo ";
                    sql += " WHERE t1.FlagDel = 0 ";
                    sql += " AND t2.DocNo = '" + docNo.Trim() + "' ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PODetail_PRE), sql);
                    list = dynamicListReturned.Cast<tbl_PODetail_PRE>().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail_PRE.GetType());
            }

            return list;
        }


        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PODetail_PRE"></param>
        /// <returns></returns>
        public static List<tbl_PODetail_PRE> Select(this tbl_PODetail_PRE tbl_PODetail_PRE, Func<tbl_PODetail_PRE, bool> predicate, string docTypeCode = "", string docNo = "")
        {
            List<tbl_PODetail_PRE> list = new List<tbl_PODetail_PRE>();
            try
            {

                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT t1.* ";
                sql += " FROM [dbo].[tbl_PODetail_PRE] t1 ";
                sql += " INNER JOIN dbo.tbl_POMaster_PRE t2 ON t1.DocNo = t2.DocNo ";
                sql += " WHERE t1.FlagDel = 0 ";

                if (!string.IsNullOrEmpty(docTypeCode))
                    sql += " AND t2.DocTypeCode = '" + docTypeCode.Trim() + "' ";

                if (!string.IsNullOrEmpty(docNo))
                    sql += " AND t1.DocNo = '" + docNo.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PODetail_PRE), sql);
                list = dynamicListReturned.Cast<tbl_PODetail_PRE>().ToList();

                if (predicate != null)
                    list = list.Where(predicate).ToList();

                if (string.IsNullOrEmpty(docTypeCode) && string.IsNullOrEmpty(docNo))
                    list = tbl_PODetail_PRE.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PODetail_PRE.Where(predicate).AsQueryable().ToList();

                //    //list = db.tbl_PODetail_PRE.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail_PRE.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_PODetail_PRE"></param>
        /// <returns></returns>
        public static List<tbl_PODetail_PRE> SelectAll(this tbl_PODetail_PRE tbl_PODetail_PRE, string docTypeCode = "")
        {
            List<tbl_PODetail_PRE> list = new List<tbl_PODetail_PRE>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM tbl_PODetail_PRE WHERE FlagDel = 0 ";

                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    sql = "";
                    sql += " SELECT t1.* ";
                    sql += " FROM [dbo].[tbl_PODetail_PRE] t1 ";
                    sql += " INNER JOIN dbo.tbl_POMaster_PRE t2 ON t1.DocNo = t2.DocNo ";
                    sql += " WHERE t1.FlagDel = 0 ";
                    sql += " AND t2.DocTypeCode = '" + docTypeCode.Trim() + "' ";
                }

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PODetail_PRE), sql);
                list = dynamicListReturned.Cast<tbl_PODetail_PRE>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_PODetail_PRE>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PODetail_PRE.OrderBy(x => x.DocNo).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail_PRE.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PODetail_PRE"></param>
        /// <returns></returns>
        public static void Insert(this tbl_PODetail_PRE tbl_PODetail_PRE, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.tbl_PODetail_PRE.Attach(tbl_PODetail_PRE);
                db.tbl_PODetail_PRE.Add(tbl_PODetail_PRE);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail_PRE.GetType());
            }
        }

        public static void Insert(this List<tbl_PODetail_PRE> tbl_PODetail_PREs, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                foreach (var tbl_PODetail_PRE in tbl_PODetail_PREs)
                {
                    db.tbl_PODetail_PRE.Attach(tbl_PODetail_PRE);
                    db.tbl_PODetail_PRE.Add(tbl_PODetail_PRE);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }
        }

        public static int Update(this List<tbl_PODetail_PRE> tbl_PODetail_PREs)
        {
            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_PODetail_PRE in tbl_PODetail_PREs)
                    {
                        var updateData = db.tbl_PODetail_PRE.FirstOrDefault(x => x.DocNo == tbl_PODetail_PRE.DocNo && x.ProductID == tbl_PODetail_PRE.ProductID && tbl_PODetail_PRE.Line == x.Line);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_PODetail_PREItem in tbl_PODetail_PRE.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_PODetail_PREItem.Name)
                                    {
                                        var value = tbl_PODetail_PREItem.GetValue(tbl_PODetail_PRE, null);

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
                            tbl_PODetail_PRE.Delete(db);
                            tbl_PODetail_PRE.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //ex.WriteLog(tbl_PODetail_PRE);
            }

            return ret != 0 ? 1 : 0;
        }

        public static int UpdateEntity(this List<tbl_PODetail_PRE> tbl_PODetail_PREs, DB_ALL_CASH_UNIEntities db, string docTypeCode = "")
        {
            int ret = 0;
            try
            {
                var docNo = tbl_PODetail_PREs.First().DocNo;
                var tbl_PODetail_PREList = db.tbl_PODetail_PRE.Where(x => x.DocNo == docNo).ToList();

                if (tbl_PODetail_PREList.Count > 0)
                {
                    foreach (var tbl_PODetail_PRE in tbl_PODetail_PREs)
                    {
                        var updateData = tbl_PODetail_PREList.FirstOrDefault(x => x.DocNo == tbl_PODetail_PRE.DocNo && x.ProductID == tbl_PODetail_PRE.ProductID && tbl_PODetail_PRE.Line == x.Line); // db.tbl_PODetail_PRE.FirstOrDefault(x => x.DocNo == tbl_PODetail_PRE.DocNo && x.ProductID == tbl_PODetail_PRE.ProductID && tbl_PODetail_PRE.Line == x.Line);

                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_PODetail_PREItem in tbl_PODetail_PRE.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_PODetail_PREItem.Name)
                                    {
                                        var value = tbl_PODetail_PREItem.GetValue(tbl_PODetail_PRE, null);

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
                            //tbl_PODetail_PRE.Delete(db);
                            tbl_PODetail_PRE.Insert(db);
                        }
                    }
                }
                else
                    tbl_PODetail_PREs.Insert(db);

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PODetail_PRE"></param>
        /// <returns></returns>
        public static void Delete(this tbl_PODetail_PRE tbl_PODetail_PRE, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.Entry(tbl_PODetail_PRE).State = EntityState.Deleted;
                db.tbl_PODetail_PRE.Remove(tbl_PODetail_PRE);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail_PRE.GetType());
            }
        }

        public static int Insert(this tbl_PODetail_PRE tbl_PODetail_PRE)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_PODetail_PRE.Attach(tbl_PODetail_PRE);
                    db.tbl_PODetail_PRE.Add(tbl_PODetail_PRE);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail_PRE.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_PODetail_PRE"></param>
        /// <returns></returns>
        public static int Update(this tbl_PODetail_PRE tbl_PODetail_PRE)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_PODetail_PRE.FirstOrDefault(x => x.DocNo == tbl_PODetail_PRE.DocNo && x.ProductID == tbl_PODetail_PRE.ProductID && tbl_PODetail_PRE.Line == x.Line);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PODetail_PREItem in tbl_PODetail_PRE.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_PODetail_PREItem.Name)
                                {
                                    var value = tbl_PODetail_PREItem.GetValue(tbl_PODetail_PRE, null);

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
                        ret = tbl_PODetail_PRE.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail_PRE.GetType());
            }

            return ret;
        }

        public static int Delete(this tbl_PODetail_PRE tbl_PODetail_PRE)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_PODetail_PRE).State = EntityState.Deleted;
                    db.tbl_PODetail_PRE.Remove(tbl_PODetail_PRE);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail_PRE.GetType());
            }

            return ret;
        }
    }
}
