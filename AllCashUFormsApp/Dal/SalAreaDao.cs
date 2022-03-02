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
    public static class SalAreaDao
    {
        public static DataTable GetSalAreaData(this tbl_SalArea tbl_SalArea, int flagDel, string searchtext)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = " ";
                sql += "select * from tbl_SalArea";

                if (!string.IsNullOrEmpty(searchtext))
                {
                    sql += " WHERE SalAreaID like '%" + searchtext + "%'" + " OR SalAreaName like '%" + searchtext + "%'" + " AND FlagDel = " + flagDel + "";
                }
                else
                {
                    sql += " WHERE FlagDel=" + flagDel + "";
                }

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(ds, "salarea");

                //return ds.Tables["salarea"]; // public static Datatable

                var dt = new DataTable("salarea");
                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                return dt;
            }

            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
                return null;
            }
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_SalArea"></param>
        /// <returns></returns>
        public static List<tbl_SalArea> Select(this tbl_SalArea tbl_SalArea, Func<tbl_SalArea, bool> predicate)
        {
            List<tbl_SalArea> list = new List<tbl_SalArea>();
            try
            {
                list = tbl_SalArea.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_SalArea.Where(predicate).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
            }

            return list;
        }

        /// <summary>
        /// Last edit by sailom.k 14/09/2021 tunning performance
        /// </summary>
        /// <param name="tbl_SalArea"></param>
        /// <param name="listOfSalAreaID"></param>
        /// <returns></returns>
        public static List<tbl_SalArea> Select(this tbl_SalArea tbl_SalArea, List<string> listOfSalAreaID)
        {
            List<tbl_SalArea> list = new List<tbl_SalArea>();
            try
            {
                DataTable dt = new DataTable();

                string _listOfSalAreaID = "'" + string.Join("','", listOfSalAreaID) + "'";

                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_SalArea] WHERE SalAreaID IN (" + _listOfSalAreaID + ")  Order By SalAreaCode, Seq ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_SalArea), sql);
                list = dynamicListReturned.Cast<tbl_SalArea>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_SalArea"></param>
        /// <returns></returns>
        public static List<tbl_SalArea> SelectAll(this tbl_SalArea tbl_SalArea)
        {
            List<tbl_SalArea> list = new List<tbl_SalArea>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_SalArea] Order By Seq ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_SalArea), sql);
                list = dynamicListReturned.Cast<tbl_SalArea>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_SalArea>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_SalArea.OrderBy(x => x.SalAreaCode).ThenBy(x => x.Seq).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_SalArea"></param>
        /// <returns></returns>
        public static int Insert(this tbl_SalArea tbl_SalArea)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_SalArea.Attach(tbl_SalArea);
                    db.tbl_SalArea.Add(tbl_SalArea);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_SalArea"></param>
        /// <returns></returns>
        public static int Update(this tbl_SalArea tbl_SalArea)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_SalArea.FirstOrDefault(x => x.SalAreaID == tbl_SalArea.SalAreaID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_SalAreaItem in tbl_SalArea.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_SalAreaItem.Name)
                                {
                                    var value = tbl_SalAreaItem.GetValue(tbl_SalArea, null);

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
                        ret = tbl_SalArea.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_SalArea"></param>
        /// <returns></returns>
        public static int Delete(this tbl_SalArea tbl_SalArea)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_SalArea).State = EntityState.Deleted;
                    db.tbl_SalArea.Remove(tbl_SalArea);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
            }

            return ret;
        }

        public static DataTable proc_GetMKT_Data(this tbl_SalArea tbl_SalArea, Dictionary<string, object> _params)
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_GetMKT_Data";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable proc_GetMarketData(this tbl_SalArea tbl_SalArea, Dictionary<string, object> _params)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "proc_GetMarketData";

                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
                return null;
            }

            return dt;
        }

        public static List<tbl_SalArea> GetSalAreaByWHID(this tbl_SalArea tbl_SalArea, string _WHID)
        {
            List<tbl_SalArea> list = new List<tbl_SalArea>();
            try
            {
                string sql = @"SELECT * FROM tbl_SalArea t1 
                            INNER JOIN (SELECT DISTINCT SalAreaID FROM tbl_SalAreaDistrict 
                            WHERE WHID = '" + _WHID + "' )t2 ON t1.SalAreaID = T2.SalAreaID ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_SalArea), sql);
                list = dynamicListReturned.Cast<tbl_SalArea>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalArea.GetType());
                return null;
            }

            return list;
        }
    }
}
