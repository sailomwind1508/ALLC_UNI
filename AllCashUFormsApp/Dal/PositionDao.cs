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
    public static class PositionDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_Position"></param>
        /// <returns></returns>
        public static List<tbl_Position> Select(this tbl_Position tbl_Position, Func<tbl_Position, bool> predicate)
        {
            List<tbl_Position> list = new List<tbl_Position>();
            try
            {
                list = tbl_Position.SelectAll().Where(predicate).ToList();
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Position.Where(predicate).Where(x => x.FlagDel == false).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Position.GetType());
            }

            return list;
        }
        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Position"></param>
        /// <returns></returns>
        public static List<tbl_Position> SelectAll(this tbl_Position tbl_Position)
        {
            List<tbl_Position> list = new List<tbl_Position>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_Position] WHERE FlagDel = 0 ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Position), sql);
                list = dynamicListReturned.Cast<tbl_Position>().ToList();


                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Position.Where(x => x.FlagDel == false).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Position.GetType());
            }

            return list;
        }
        public static List<tbl_Position> SelectAllNonFlag(this tbl_Position tbl_Position)
        {
            List<tbl_Position> list = new List<tbl_Position>();
            try
            {
                string sql = "SELECT * FROM tbl_Position";
                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Position), sql);
                list = dynamicListReturned.Cast<tbl_Position>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Position.GetType());
            }

            return list;
        }
        public static List<tbl_Position> SelectNonFlag(this tbl_Position tbl_Position, Func<tbl_Position, bool> predicate)
        {
            List<tbl_Position> list = new List<tbl_Position>();
            try
            {
                list = tbl_Position.SelectAllNonFlag().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Position.GetType());
            }

            return list;
        }
        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_Position"></param>
        /// <returns></returns>
        public static int Insert(this tbl_Position tbl_Position)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_Position.Attach(tbl_Position);
                    db.tbl_Position.Add(tbl_Position);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Position.GetType());
            }

            return ret;
        }
        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Position"></param>
        /// <returns></returns>
        public static int Update(this tbl_Position tbl_Position)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_Position.FirstOrDefault(x => x.PositionID == tbl_Position.PositionID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PositionItem in tbl_Position.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_PositionItem.Name)
                                {
                                    var value = tbl_PositionItem.GetValue(tbl_Position, null);

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
                        ret = tbl_Position.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Position.GetType());
            }

            return ret;
        }
        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Position"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Position tbl_Position)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_Position).State = EntityState.Deleted;
                    db.tbl_Position.Remove(tbl_Position);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Position.GetType());
            }

            return ret;
        }
        public static DataTable GetPositionData(this tbl_Position tbl_Position, int flagDel, string Search)
        {
            DataTable dt = new DataTable();

            string sql = "SELECT * FROM tbl_Position WHERE FlagDel = " + flagDel + "";

            if (!string.IsNullOrEmpty(Search))
            {
                sql += " AND (PositionCode like '%" + Search + "%'";
                sql += " OR PositionName like '%" + Search + "%')";
            }

            dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            return dt;
        }
    }
}
