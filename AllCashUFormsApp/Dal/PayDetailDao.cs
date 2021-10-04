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
    public static class PayDetailDao
    {
        public static List<tbl_PayDetail> Select(this tbl_PayDetail tbl_PayDetail, Func<tbl_PayDetail, bool> predicate)
        {
            List<tbl_PayDetail> list = new List<tbl_PayDetail>();
            try
            {
                list = tbl_PayDetail.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PayDetail.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
            }

            return list;
        }

        public static List<tbl_PayDetail> SelectAll(this tbl_PayDetail tbl_PayDetail)
        {
            List<tbl_PayDetail> list = new List<tbl_PayDetail>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_PayDetail] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PayDetail), sql);
                list = dynamicListReturned.Cast<tbl_PayDetail>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_PayDetail>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PayDetail.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PayDetail"></param>
        /// <returns></returns>
        public static void Insert(this tbl_PayDetail tbl_PayDetail, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.tbl_PayDetail.Attach(tbl_PayDetail);
                db.tbl_PayDetail.Add(tbl_PayDetail);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
            }
        }

        public static void Insert(this List<tbl_PayDetail> tbl_PayDetails, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                foreach (var tbl_PayDetail in tbl_PayDetails)
                {

                    db.tbl_PayDetail.Attach(tbl_PayDetail);
                    db.tbl_PayDetail.Add(tbl_PayDetail);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }
        }

        public static int UpdateEntity(this List<tbl_PayDetail> tbl_PayDetails, DB_ALL_CASH_UNIEntities db)
        {
            int ret = 0;

            try
            {

                var docNo = tbl_PayDetails.First().DocNo;
                var tbl_PayDetailList = db.tbl_PayDetail.Where(x => x.DocNo == docNo).ToList();

                if (tbl_PayDetailList.Count > 0)
                {
                    foreach (var tbl_PayDetail in tbl_PayDetails)
                    {
                        var updateData = tbl_PayDetailList.FirstOrDefault(x => x.DocNo == tbl_PayDetail.DocNo && x.WHID == tbl_PayDetail.WHID && x.AutoID == tbl_PayDetail.AutoID);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_PayDetailItem in tbl_PayDetail.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name == tbl_PayDetailItem.Name)
                                    {
                                        var value = tbl_PayDetailItem.GetValue(tbl_PayDetail, null);

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
                            //tbl_PayDetail.Delete(db);
                            tbl_PayDetail.Insert(db);
                        }

                    }
                }
                else
                    tbl_PayDetails.Insert(db);

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            return ret;
        }

        public static int Update(this List<tbl_PayDetail> tbl_PayDetails)
        {
            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_PayDetail in tbl_PayDetails)
                    {
                        var updateData = db.tbl_PayDetail.FirstOrDefault(x => x.DocNo == tbl_PayDetail.DocNo && x.WHID == tbl_PayDetail.WHID && x.AutoID == tbl_PayDetail.AutoID);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_PayDetailItem in tbl_PayDetail.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name == tbl_PayDetailItem.Name)
                                    {
                                        var value = tbl_PayDetailItem.GetValue(tbl_PayDetail, null);

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
                            tbl_PayDetail.Delete(db);
                            tbl_PayDetail.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //ex.WriteLog(tbl_PayDetail);
            }

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PayDetail"></param>
        /// <returns></returns>
        public static void Delete(this tbl_PayDetail tbl_PayDetail, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.Entry(tbl_PayDetail).State = EntityState.Deleted;
                db.tbl_PayDetail.Remove(tbl_PayDetail);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
            }
        }

        public static int Insert(this tbl_PayDetail tbl_PayDetail)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_PayDetail.Attach(tbl_PayDetail);
                    db.tbl_PayDetail.Add(tbl_PayDetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
            }

            return ret;
        }

        public static int Update(this tbl_PayDetail tbl_PayDetail)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_PayDetail.FirstOrDefault(x => x.DocNo == tbl_PayDetail.DocNo && x.WHID == tbl_PayDetail.WHID && x.AutoID == tbl_PayDetail.AutoID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PayDetailItem in tbl_PayDetail.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_PayDetailItem.Name)
                                {
                                    var value = tbl_PayDetailItem.GetValue(tbl_PayDetail, null);

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
                        ret = tbl_PayDetail.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
                ex.ToString();
            }

            return ret;
        }

        public static int Delete(this tbl_PayDetail tbl_PayDetail)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_PayDetail).State = EntityState.Deleted;
                    db.tbl_PayDetail.Remove(tbl_PayDetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PayDetail.GetType());
            }

            return ret;
        }
    }
}
