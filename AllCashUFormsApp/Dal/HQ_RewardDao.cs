using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
namespace AllCashUFormsApp
{
    public static class HQ_RewardDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_Reward"></param>
        /// <returns></returns>
        public static IEnumerable<tbl_HQ_Reward> Select(this tbl_HQ_Reward obj, object condition)
        {
            return new tbl_HQ_Reward().Select(x => x.RewardID.Trim() == condition.ToString().Trim()).AsEnumerable();
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_Reward"></param>
        /// <returns></returns>
        public static List<tbl_HQ_Reward> Select(this tbl_HQ_Reward tbl_HQ_Reward, Func<tbl_HQ_Reward, bool> predicate)
        {
            List<tbl_HQ_Reward> list = new List<tbl_HQ_Reward>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_HQ_Reward.Where(predicate).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Reward.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_HQ_Reward"></param>
        /// <returns></returns>
        public static List<tbl_HQ_Reward> SelectAll(this tbl_HQ_Reward tbl_HQ_Reward)
        {
            List<tbl_HQ_Reward> list = new List<tbl_HQ_Reward>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_HQ_Reward.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Reward.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_HQ_Reward"></param>
        /// <returns></returns>
        public static int Insert(this tbl_HQ_Reward tbl_HQ_Reward)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_HQ_Reward.Attach(tbl_HQ_Reward);
                    db.tbl_HQ_Reward.Add(tbl_HQ_Reward);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Reward.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_HQ_Reward"></param>
        /// <returns></returns>
        public static int Update(this tbl_HQ_Reward tbl_HQ_Reward)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_HQ_Reward.FirstOrDefault(x => x.RewardID == tbl_HQ_Reward.RewardID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_HQ_RewardItem in tbl_HQ_Reward.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_HQ_RewardItem.Name)
                                {
                                    var value = tbl_HQ_RewardItem.GetValue(tbl_HQ_Reward, null);

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
                        ret = tbl_HQ_Reward.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Reward.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_HQ_Reward"></param>
        /// <returns></returns>
        public static int Delete(this tbl_HQ_Reward tbl_HQ_Reward)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_HQ_Reward).State = EntityState.Deleted;
                    db.tbl_HQ_Reward.Remove(tbl_HQ_Reward);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_Reward.GetType());
            }

            return ret;
        }
    }
}
