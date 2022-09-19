using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class HQ_Promotion_MasterDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Master"></param>
        /// <returns></returns>
        public static IEnumerable<tbl_HQ_Promotion_Master> Select(this tbl_HQ_Promotion_Master obj, object condition)
        {
            return obj.Select(x => x.PromotionID.Trim() == condition.ToString().Trim()).AsEnumerable();
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Master"></param>
        /// <returns></returns>
        public static List<tbl_HQ_Promotion_Master> Select(this tbl_HQ_Promotion_Master tbl_HQ_Promotion_Master, Func<tbl_HQ_Promotion_Master, bool> predicate)
        {
            List<tbl_HQ_Promotion_Master> list = new List<tbl_HQ_Promotion_Master>();
            try
            {
                list = tbl_HQ_Promotion_Master.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_HQ_Promotion_Master.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Master.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Master"></param>
        /// <returns></returns>
        public static List<tbl_HQ_Promotion_Master> SelectAll(this tbl_HQ_Promotion_Master tbl_HQ_Promotion_Master)
        {
            List<tbl_HQ_Promotion_Master> list = new List<tbl_HQ_Promotion_Master>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_HQ_Promotion_Master] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_HQ_Promotion_Master), sql);
                list = dynamicListReturned.Cast<tbl_HQ_Promotion_Master>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_HQ_Promotion_Master.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Master.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Master"></param>
        /// <returns></returns>
        public static int Insert(this tbl_HQ_Promotion_Master tbl_HQ_Promotion_Master)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_HQ_Promotion_Master.Attach(tbl_HQ_Promotion_Master);
                    db.tbl_HQ_Promotion_Master.Add(tbl_HQ_Promotion_Master);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Master.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Master"></param>
        /// <returns></returns>
        public static int Update(this tbl_HQ_Promotion_Master tbl_HQ_Promotion_Master)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_HQ_Promotion_Master.FirstOrDefault(x => x.PromotionID == tbl_HQ_Promotion_Master.PromotionID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_HQ_Promotion_MasterItem in tbl_HQ_Promotion_Master.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_HQ_Promotion_MasterItem.Name)
                                {
                                    var value = tbl_HQ_Promotion_MasterItem.GetValue(tbl_HQ_Promotion_Master, null);

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
                        ret = tbl_HQ_Promotion_Master.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Master.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Master"></param>
        /// <returns></returns>
        public static int Delete(this tbl_HQ_Promotion_Master tbl_HQ_Promotion_Master)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_HQ_Promotion_Master).State = EntityState.Deleted;
                    db.tbl_HQ_Promotion_Master.Remove(tbl_HQ_Promotion_Master);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Master.GetType());
            }

            return ret;
        }
        public static DataTable GetHQ_Promotion_MasterData(this tbl_HQ_Promotion_Master tbl_HQ_Promotion_Master,string search)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM tbl_HQ_Promotion_Master";
                if (!string.IsNullOrEmpty(search))
                {
                    sql += " WHERE PromotionID like '%" + search + "%'";
                    sql += " OR PromotionName like '%" + search + "%'";
                }
                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
                return null;
            }
        }
        public static List<tbl_HQ_Promotion_Master> SelectPromotionID_Master(this tbl_HQ_Promotion_Master tbl_HQ_Promotion_Master, string PromotionID = "")
        {
            List<tbl_HQ_Promotion_Master> list = new List<tbl_HQ_Promotion_Master>();
            try
            {

                string sql = "SELECT * FROM tbl_HQ_Promotion_Master";

                if (!string.IsNullOrEmpty(PromotionID))
                {
                    sql += " WHERE PromotionID like '%" + PromotionID + "%'";
                }
                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_HQ_Promotion_Master), sql);
                list = dynamicListReturned.Cast<tbl_HQ_Promotion_Master>().ToList();

            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
            }

            return list;
        }
    }
}
