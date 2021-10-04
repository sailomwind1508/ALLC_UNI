using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class HQ_PromotionDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_Promotion"></param>
        /// <returns></returns>
        public static IEnumerable<tbl_HQ_Promotion> Select(this tbl_HQ_Promotion obj, object condition)
        {
            DateTime cDate = DateTime.Now;
            return obj.Select(x => x.PromotionPattern.ToLower() == "prd" &&
            cDate >= x.EffectiveDate && cDate <= x.ExpireDate &&
            x.SKUGroupID1.Trim() == condition.ToString().Trim()).AsEnumerable();
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_Promotion"></param>
        /// <returns></returns>
        public static List<tbl_HQ_Promotion> Select(this tbl_HQ_Promotion tbl_HQ_Promotion, Func<tbl_HQ_Promotion, bool> predicate)
        {
            DateTime cDate = DateTime.Now;

            List<tbl_HQ_Promotion> list = new List<tbl_HQ_Promotion>();
            try
            {
                list = tbl_HQ_Promotion.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_HQ_Promotion.Where(x => cDate >= x.EffectiveDate && cDate <= x.ExpireDate).Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion.GetType());
            }

            return list;
        }
        public static List<tbl_HQ_Promotion> SelectPromotion(this tbl_HQ_Promotion tbl_HQ_Promotion, string PromotionID = "")
        {
            List<tbl_HQ_Promotion> list = new List<tbl_HQ_Promotion>();
            try
            {

                string sql = "SELECT * FROM tbl_HQ_Promotion";

                if (!string.IsNullOrEmpty(PromotionID))
                {
                    sql += " WHERE PromotionID like '%" + PromotionID + "%'";
                }

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_HQ_Promotion), sql);
                list = dynamicListReturned.Cast<tbl_HQ_Promotion>().ToList();

            }
            catch (Exception ex)
            {

            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_HQ_Promotion"></param>
        /// <returns></returns>
        public static List<tbl_HQ_Promotion> SelectAll(this tbl_HQ_Promotion tbl_HQ_Promotion)
        {
            DateTime cDate = DateTime.Now;
            List<tbl_HQ_Promotion> list = new List<tbl_HQ_Promotion>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_HQ_Promotion] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_HQ_Promotion), sql);
                list = dynamicListReturned.Cast<tbl_HQ_Promotion>().ToList();

                list = list.Where(x => cDate >= x.EffectiveDate && cDate <= x.ExpireDate).ToList(); //for support u-online last edit by sailom .k 18-06-2021

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_HQ_Promotion.Where(x => cDate >= x.EffectiveDate && cDate <= x.ExpireDate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_HQ_Promotion"></param>
        /// <returns></returns>
        public static int Insert(this tbl_HQ_Promotion tbl_HQ_Promotion)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_HQ_Promotion.Attach(tbl_HQ_Promotion);
                    db.tbl_HQ_Promotion.Add(tbl_HQ_Promotion);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_HQ_Promotion"></param>
        /// <returns></returns>
        public static int Update(this tbl_HQ_Promotion tbl_HQ_Promotion)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_HQ_Promotion.FirstOrDefault(x => x.PromotionID == tbl_HQ_Promotion.PromotionID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_HQ_PromotionItem in tbl_HQ_Promotion.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_HQ_PromotionItem.Name)
                                {
                                    var value = tbl_HQ_PromotionItem.GetValue(tbl_HQ_Promotion, null);

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
                        ret = tbl_HQ_Promotion.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_HQ_Promotion"></param>
        /// <returns></returns>
        public static int Delete(this tbl_HQ_Promotion tbl_HQ_Promotion)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_HQ_Promotion).State = EntityState.Deleted;
                    db.tbl_HQ_Promotion.Remove(tbl_HQ_Promotion);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion.GetType());
            }

            return ret;
        }
        public static DataTable GetHQ_PromotionData(this tbl_HQ_Promotion tbl_HQ_Promotion, string search)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM tbl_HQ_Promotion";
                if (!string.IsNullOrEmpty(search))
                {
                    sql += " WHERE PromotionID like '%" + search + "%'";
                }
                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool CallSendAllPromotionInfo(this tbl_HQ_Promotion tbl_HQ_Promotion, Dictionary<string, object> _params)//
        {
            bool ret = false;
            try
            {
                string sql = "proc_SendAllPromotionInfo_SendDataToBranch";
                DataTable dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

                if (dt != null && dt.Rows.Count != 0)
                {
                    if (dt.Rows[0]["Result"].ToString() == "1")
                    {
                        ret = true;
                    }
                }

            }
            catch (Exception ex)
            {
                ret = false;
            }

            return ret;
        }
    }
}
