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
                string sql = "SELECT FROM tbl_DocRunning";

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
            try
            {
                db.tbl_DocRunning.Attach(tbl_DocRunning);
                db.tbl_DocRunning.Add(tbl_DocRunning);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocRunning.GetType());
            }
        }

        public static int UpdateEntity(this List<tbl_DocRunning> tbl_DocRunnings, DB_ALL_CASH_UNIEntities db)
        {
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

            return ret;
        }

        public static int Update(this List<tbl_DocRunning> tbl_DocRunnings)
        {
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
            }

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_DocRunning"></param>
        /// <returns></returns>
        public static void Delete(this tbl_DocRunning tbl_DocRunning, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.Entry(tbl_DocRunning).State = EntityState.Deleted;
                db.tbl_DocRunning.Remove(tbl_DocRunning);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocRunning.GetType());
            }
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_DocRunning"></param>
        /// <returns></returns>
        public static int Insert(this tbl_DocRunning tbl_DocRunning)
        {
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

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_DocRunning"></param>
        /// <returns></returns>
        public static int Update(this tbl_DocRunning tbl_DocRunning)
        {
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

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_DocRunning"></param>
        /// <returns></returns>
        public static int Delete(this tbl_DocRunning tbl_DocRunning)
        {
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

            return ret;
        }
    }
}
