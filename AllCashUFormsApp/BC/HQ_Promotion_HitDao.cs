using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class HQ_Promotion_HitDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Hit"></param>
        /// <returns></returns>
        public static IEnumerable<tbl_HQ_Promotion_Hit> Select(this tbl_HQ_Promotion_Hit obj, object condition)
        {
            return obj.Select(x => x.PromotionID.Trim() == condition.ToString().Trim()).ToList();

            //return new tbl_HQ_Promotion_Hit().Select(x => x.PromotionID.Trim() == condition.ToString().Trim()).AsEnumerable();
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Hit"></param>
        /// <returns></returns>
        public static List<tbl_HQ_Promotion_Hit> Select(this tbl_HQ_Promotion_Hit tbl_HQ_Promotion_Hit, Func<tbl_HQ_Promotion_Hit, bool> predicate)
        {
            List<tbl_HQ_Promotion_Hit> list = new List<tbl_HQ_Promotion_Hit>();
            try
            {
                list = tbl_HQ_Promotion_Hit.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_HQ_Promotion_Hit.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Hit.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Hit"></param>
        /// <returns></returns>
        public static List<tbl_HQ_Promotion_Hit> SelectAll(this tbl_HQ_Promotion_Hit tbl_HQ_Promotion_Hit)
        {
            List<tbl_HQ_Promotion_Hit> list = new List<tbl_HQ_Promotion_Hit>();
            try
            {
                string sql = "";

                sql += " SELECT * FROM [dbo].[tbl_HQ_Promotion_Hit] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_HQ_Promotion_Hit), sql);
                list = dynamicListReturned.Cast<tbl_HQ_Promotion_Hit>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_HQ_Promotion_Hit.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Hit.GetType());
            }

            return list;
        }


        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Hit"></param>
        /// <returns></returns>
        public static int Insert(this tbl_HQ_Promotion_Hit tbl_HQ_Promotion_Hit)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_HQ_Promotion_Hit.Attach(tbl_HQ_Promotion_Hit);
                    db.tbl_HQ_Promotion_Hit.Add(tbl_HQ_Promotion_Hit);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Hit.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Hit"></param>
        /// <returns></returns>
        public static int Update(this tbl_HQ_Promotion_Hit tbl_HQ_Promotion_Hit)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_HQ_Promotion_Hit.FirstOrDefault(x => x.DocNo == tbl_HQ_Promotion_Hit.DocNo && x.PromotionID == tbl_HQ_Promotion_Hit.PromotionID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_HQ_Promotion_HitItem in tbl_HQ_Promotion_Hit.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_HQ_Promotion_HitItem.Name)
                                {
                                    var value = tbl_HQ_Promotion_HitItem.GetValue(tbl_HQ_Promotion_Hit, null);

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
                        ret = tbl_HQ_Promotion_Hit.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Hit.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_HQ_Promotion_Hit"></param>
        /// <returns></returns>
        public static int Delete(this tbl_HQ_Promotion_Hit tbl_HQ_Promotion_Hit)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_HQ_Promotion_Hit).State = EntityState.Deleted;
                    db.tbl_HQ_Promotion_Hit.Remove(tbl_HQ_Promotion_Hit);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Promotion_Hit.GetType());
            }

            return ret;
        }
    }
}
