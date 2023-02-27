using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class HQ_Promotion_Hit_TempDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Hit_Temp"></param>
        /// <returns></returns>
        public static IEnumerable<tbl_HQ_Promotion_Hit_Temp> Select(this tbl_HQ_Promotion_Hit_Temp obj, object condition)
        {
            return obj.Select(x => x.PromotionID.Trim() == condition.ToString().Trim()).AsEnumerable();
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Hit_Temp"></param>
        /// <returns></returns>
        public static List<tbl_HQ_Promotion_Hit_Temp> Select(this tbl_HQ_Promotion_Hit_Temp tbl_HQ_Promotion_Hit_Temp, Func<tbl_HQ_Promotion_Hit_Temp, bool> predicate)
        {
            List<tbl_HQ_Promotion_Hit_Temp> list = new List<tbl_HQ_Promotion_Hit_Temp>();
            try
            {
                list = tbl_HQ_Promotion_Hit_Temp.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_HQ_Promotion_Hit_Temp.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Hit_Temp.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Hit_Temp"></param>
        /// <returns></returns>
        public static List<tbl_HQ_Promotion_Hit_Temp> SelectAll(this tbl_HQ_Promotion_Hit_Temp tbl_HQ_Promotion_Hit_Temp)
        {
            List<tbl_HQ_Promotion_Hit_Temp> list = new List<tbl_HQ_Promotion_Hit_Temp>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_HQ_Promotion_Hit_Temp] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_HQ_Promotion_Hit_Temp), sql);
                list = dynamicListReturned.Cast<tbl_HQ_Promotion_Hit_Temp>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_HQ_Promotion_Hit_Temp.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Hit_Temp.GetType());
            }

            return list;
        }


        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Hit_Temp"></param>
        /// <returns></returns>
        public static int Insert(this tbl_HQ_Promotion_Hit_Temp tbl_HQ_Promotion_Hit_Temp)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_HQ_Promotion_Hit_Temp.Attach(tbl_HQ_Promotion_Hit_Temp);
                    db.tbl_HQ_Promotion_Hit_Temp.Add(tbl_HQ_Promotion_Hit_Temp);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Hit_Temp.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Hit_Temp"></param>
        /// <returns></returns>
        public static int Update(this tbl_HQ_Promotion_Hit_Temp tbl_HQ_Promotion_Hit_Temp)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_HQ_Promotion_Hit_Temp.FirstOrDefault(x => x.PromotionID == tbl_HQ_Promotion_Hit_Temp.PromotionID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_HQ_Promotion_Hit_TempItem in tbl_HQ_Promotion_Hit_Temp.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_HQ_Promotion_Hit_TempItem.Name)
                                {
                                    var value = tbl_HQ_Promotion_Hit_TempItem.GetValue(tbl_HQ_Promotion_Hit_Temp, null);

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
                        ret = tbl_HQ_Promotion_Hit_Temp.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Hit_Temp.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Hit_Temp"></param>
        /// <returns></returns>
        public static int Delete(this tbl_HQ_Promotion_Hit_Temp tbl_HQ_Promotion_Hit_Temp)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_HQ_Promotion_Hit_Temp).State = EntityState.Deleted;
                    db.tbl_HQ_Promotion_Hit_Temp.Remove(tbl_HQ_Promotion_Hit_Temp);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Hit_Temp.GetType());
            }

            return ret;
        }
    }
}
