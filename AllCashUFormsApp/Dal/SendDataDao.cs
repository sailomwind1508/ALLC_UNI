using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;

namespace AllCashUFormsApp
{
    public static class SendDataDao
    {
        public static List<tbl_SendData> Select(this tbl_SendData tbl_SendData, Func<tbl_SendData, bool> predicate)
        {
            List<tbl_SendData> list = new List<tbl_SendData>();
            try
            {
                list = tbl_SendData.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_SendData.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SendData.GetType());
            }

            return list;
        }

        public static List<tbl_SendData> SelectAll(this tbl_SendData tbl_SendData)
        {
            List<tbl_SendData> list = new List<tbl_SendData>();
            try
            {
                string sql = "";

                sql += " SELECT * FROM [dbo].[tbl_SendData] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_SendData), sql);
                list = dynamicListReturned.Cast<tbl_SendData>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_SendData.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SendData.GetType());
            }

            return list;
        }

        public static int Insert(this tbl_SendData tbl_SendData)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_SendData.Attach(tbl_SendData);
                    db.tbl_SendData.Add(tbl_SendData);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SendData.GetType());
            }

            return ret;
        }

        public static int Update(this tbl_SendData tbl_SendData)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_SendData.FirstOrDefault(x => x.DateSend == tbl_SendData.DateSend && x.WHID == tbl_SendData.WHID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_SendDataItem in tbl_SendData.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_SendDataItem.Name)
                                {
                                    var value = tbl_SendDataItem.GetValue(tbl_SendData, null);

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
                        ret = tbl_SendData.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SendData.GetType());
            }

            return ret;
        }

        public static int Delete(this tbl_SendData tbl_SendData)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_SendData).State = EntityState.Deleted;
                    db.tbl_SendData.Remove(tbl_SendData);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SendData.GetType());
            }

            return ret;
        }

        public static DataTable GetWHID_FromSendData(this tbl_SendData tbl_SendData)
        {
            var dt = new DataTable();
            try
            {
                string sql = "";

                //sql = @"SELECT t2.BranchID,T2.WHName ,t1.WHID FROM tbl_SendData t1
                //               INNER JOIN tbl_BranchWarehouse t2 ON t1.WHID = T2.WHID
                //               WHERE CAST(DateSend AS DATE) IN (SELECT MAX(CAST(DocDate AS DATE)) FROM tbl_POMaster)";

                //sql += @"SELECT DISTINCT t2.BranchID,T2.WHName ,t1.WHID FROM tbl_ArCustomer t1
                //        INNER JOIN tbl_BranchWarehouse t2 ON t1.WHID = T2.WHID
                //        INNER JOIN 
                //        (
                //         SELECT DISTINCT t3.SalAreaName, t1.SalAreaID, t1.WHID  
                //         FROM tbl_ArCustomer t1
                //         INNER JOIN ( SELECT WHID FROM tbl_SendData 
                //          WHERE CAST(DateSend AS DATE) IN (SELECT MAX(CAST(DocDate AS DATE)) FROM tbl_POMaster)
                //         )t2 ON t1.WHID = t2.WHID 
                //         INNER JOIN tbl_SalArea t3 ON t1.SalAreaID = t3.SalAreaID
                //         WHERE T1.FlagDel = 0
                //        )T3 on t1.WHID = t3.WHID
                //        WHERE t1.FlagDel = 0
                //        ORDER BY WHID";

                //sql += @"SELECT DISTINCT t2.BranchID,T2.WHName ,t1.WHID FROM tbl_ArCustomer t1
                //        INNER JOIN tbl_BranchWarehouse t2 ON t1.WHID = T2.WHID
                //        INNER JOIN 
                //        (
                //         SELECT DISTINCT t3.SalAreaName, t1.SalAreaID, t1.WHID  
                //         FROM tbl_ArCustomer t1
                //         INNER JOIN tbl_SalArea t3 ON t1.SalAreaID = t3.SalAreaID
                //         WHERE T1.FlagDel = 0
                //        )T3 on t1.WHID = t3.WHID
                //        WHERE t1.FlagDel = 0
                //        ORDER BY WHID";

                sql += @" SELECT DISTINCT t2.BranchID,T2.WHName, t2.WHID 
						FROM tbl_BranchWarehouse t2
                        INNER JOIN 
                        (
	                        SELECT DISTINCT t3.SalAreaName, t1.SalAreaID, t1.WHID  
	                        FROM tbl_ArCustomer t1
	                        INNER JOIN tbl_SalArea t3 ON t1.SalAreaID = t3.SalAreaID
	                        WHERE T1.FlagDel = 0
                        ) T3 on t2.WHID = t3.WHID
                        WHERE t2.FlagDel = 0
						AND t2.WHType <> 0
                        ORDER BY WHID ";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SendData.GetType());
            }

            return dt;
        }

        //public static DataTable GetWHID_FromSendData(this tbl_SendData tbl_SendData)
        //{
        //    var dt = new DataTable();
        //    try
        //    {
        //        string sql = @"SELECT t2.BranchID,T2.WHName ,t1.WHID FROM tbl_SendData t1
        //                       INNER JOIN tbl_BranchWarehouse t2 ON t1.WHID = T2.WHID
        //                       WHERE CAST(DateSend AS DATE) IN (SELECT MAX(CAST(DateSend AS DATE)) FROM tbl_SendData)";

        //        dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.WriteLog(tbl_SendData.GetType());
        //    }

        //    return dt;
        //}

        public static List<tbl_SendData> GetSendDataSingle(this tbl_SendData tbl_SendData, string DateSend, string WHID)
        {
            var list = new List<tbl_SendData>();
            try
            {
                string sql = "SELECT * FROM tbl_SendData WHERE 1=1";

                if (!string.IsNullOrEmpty(DateSend))
                    sql += " AND CAST(DateSend AS DATE) = '" + DateSend.Trim() + "'";

                if (!string.IsNullOrEmpty(WHID))
                    sql += " AND WHID = '" + WHID.Trim() + "'";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_SendData), sql);
                list = dynamicListReturned.Cast<tbl_SendData>().ToList();

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SendData != null ? tbl_SendData.GetType() : null);
                return null;
            }
            return list;
        }

        public static int UpdateSendData(this tbl_SendData tbl_SendData, string oldDocDate, string NewDocDate, string WHID, string UserName)
        {
            string msg = "start SendDataDao=>UpdateSendData";
            msg.WriteLog(null);

            int ret = new int();

            try
            {
                SqlConnection con = new SqlConnection(Connection.ConnectionString);
                string sql = "UPDATE tbl_SendData";
                sql += " SET DateSend = @DateSend";
                sql += ", UserSend = @UserSend";
                sql += " WHERE CAST(DateSend AS DATE) = '" + oldDocDate + "'";
                sql += " AND WHID = '" + WHID + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.AddWithValue("@DateSend", NewDocDate);
                cmd.Parameters.AddWithValue("@UserSend", UserName);
                ret = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SendData != null ? tbl_SendData.GetType() : null);
            }

            msg = "end SendDataDao=>UpdateSendData";
            msg.WriteLog(null);

            return ret;
        }
    }
}
