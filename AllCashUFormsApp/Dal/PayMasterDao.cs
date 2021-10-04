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

            return ret;
        }

        public static int Update(this List<tbl_PayMaster> tbl_PayMasters)
        {
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

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PayMaster"></param>
        /// <returns></returns>
        public static void Delete(this tbl_PayMaster tbl_PayMaster, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.Entry(tbl_PayMaster).State = EntityState.Deleted;
                db.tbl_PayMaster.Remove(tbl_PayMaster);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayMaster.GetType());
            }
        }

        public static int Insert(this tbl_PayMaster tbl_PayMaster)
        {
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

            return ret;
        }

        public static int Update(this tbl_PayMaster tbl_PayMaster)
        {
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

            return ret;
        }

        public static int Delete(this tbl_PayMaster tbl_PayMaster)
        {
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

            return ret;
        }
    }
}
