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
    public static class HQ_RewardDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_Reward"></param>
        /// <returns></returns>
        public static IEnumerable<tbl_HQ_Reward> Select(this tbl_HQ_Reward obj, object condition)
        {
            return obj.Select(x => x.RewardID.Trim() == condition.ToString().Trim()).AsEnumerable();
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
                list = tbl_HQ_Reward.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_HQ_Reward.Where(predicate).ToList();
                //}
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
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_HQ_Reward] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_HQ_Reward), sql);
                list = dynamicListReturned.Cast<tbl_HQ_Reward>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_HQ_Reward.ToList();
                //}
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
        public static DataTable GetHQ_RewardData(this tbl_HQ_Reward tbl_HQ_Reward, string search)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM tbl_HQ_Reward";
                if (!string.IsNullOrEmpty(search))
                {
                    sql += " WHERE RewardID like '%" + search + "%'";
                    sql += " OR RewardName like '%" + search + "%'";
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
        private static string PrePareUpdateQuery()
        {
            string query = "UPDATE tbl_HQ_Reward SET RewardName = @RewardName WHERE RewardID = @RewardID";
            return query;
        }
        private static string PrePareInsertQuery()
        {
            string query = "INSERT INTO tbl_HQ_Reward (RewardID,RewardName) VALUES (@RewardID,@RewardName)";
       
            return query;
        }
        public static int UpdateHQ_Reward(this List<tbl_HQ_Reward> tbl_HQ_RewardList)
        {
            int ret = 0;
            var list = new List<tbl_HQ_Reward>();
            try
            {
                var tbl_HQ_Rewardlist = new List<tbl_HQ_Reward>();

                SqlConnection con = new SqlConnection(Connection.ConnectionString);


                var HQ_Promotion = new tbl_HQ_Promotion();

                SqlCommand cmd = new SqlCommand();

                string sql = "SELECT * FROM tbl_HQ_Reward";
                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_HQ_Reward), sql);
                list = dynamicListReturned.Cast<tbl_HQ_Reward>().ToList();

                foreach (var item in tbl_HQ_RewardList)
                {
                    var Reward = list.FirstOrDefault(x => x.RewardID == item.RewardID);

                    string query = "";

                    if (Reward != null)
                    {
                        query = PrePareUpdateQuery();
                    }
                    else
                    {
                        query = PrePareInsertQuery();
                    }

                    cmd = new SqlCommand(query, con);
                    con.Open();

                    cmd.Parameters.AddWithValue("@RewardID" , item.RewardID);
                    cmd.Parameters.AddWithValue("@RewardName" , item.RewardName);

                    ret = cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
            }

            return ret;
        }
    }
}
