using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class SalAreaDistrictDao
    {
        //public static DataTable GetSalAreaByWHID(this tbl_SalAreaDistrict tbl_SalAreaDistrict, int flagDel)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        string sql = " ";
        //        sql += "select t1.WHID,t1.SalAreaID,t2.SalAreaName,CrDate,CrUser,EdDate,EdUser,FlagDel from tbl_SalAreaDistrict AS t1 left join tbl_SalArea as t2 on t1.SalAreaID = t2.SalAreaID";
        //        if (frmCustomerInfo._WHID != null)
        //        {
        //            sql += " where WHID='" + frmCustomerInfo._WHID + "'" + " AND FlagDel=" + flagDel + "";
        //        }
        //        else
        //        {
        //            sql += " where FlagDel=" + flagDel + "";
        //        }
        //        SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
        //        da.Fill(ds, "area");
        //        return ds.Tables["area"];

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.WriteLog(tbl_SalAreaDistrict.GetType());
        //        return null;
        //    }

        //}


        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static List<tbl_SalAreaDistrict> Select(this tbl_SalAreaDistrict tbl_SalAreaDistrict, Func<tbl_SalAreaDistrict, bool> predicate)
        {
            List<tbl_SalAreaDistrict> list = new List<tbl_SalAreaDistrict>();
            try
            {
                list = tbl_SalAreaDistrict.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_SalAreaDistrict.Where(predicate).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static List<tbl_SalAreaDistrict> SelectAll(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            List<tbl_SalAreaDistrict> list = new List<tbl_SalAreaDistrict>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_SalAreaDistrict] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_SalAreaDistrict), sql);
                list = dynamicListReturned.Cast<tbl_SalAreaDistrict>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_SalAreaDistrict>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_SalAreaDistrict.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static int Insert(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_SalAreaDistrict.Attach(tbl_SalAreaDistrict);
                    db.tbl_SalAreaDistrict.Add(tbl_SalAreaDistrict);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static int Update(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_SalAreaDistrict.FirstOrDefault(x => x.SalAreaID == tbl_SalAreaDistrict.SalAreaID && x.DistrictID == tbl_SalAreaDistrict.DistrictID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_SalAreaDistrictItem in tbl_SalAreaDistrict.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_SalAreaDistrictItem.Name)
                                {
                                    var value = tbl_SalAreaDistrictItem.GetValue(tbl_SalAreaDistrict, null);

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
                        ret = tbl_SalAreaDistrict.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static int Delete(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_SalAreaDistrict).State = EntityState.Deleted;
                    db.tbl_SalAreaDistrict.Remove(tbl_SalAreaDistrict);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return ret;
        }

        public static DataTable GetDataTable(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            try
            {
                DataTable newTable = new DataTable("DistrictTable");

                string sql = "proc_SalAreaDistrict_GetDataTable";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                return newTable;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataTable GetProvinceFromSalAreaDistrict(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            try
            {
                DataTable dt = new DataTable();

                string sql = @"SELECT T1.ProvinceCode,T1.ProvinceName ,T1.ProvinceID FROM tbl_MstProvince T1
                INNER JOIN (SELECT DISTINCT ProvinceName FROM tbl_SalAreaDistrict) T2 ON T1.ProvinceName = T2.ProvinceName
                ORDER BY ProvinceCode";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetSalAreaDistrictData(this tbl_SalAreaDistrict tbl_SalAreaDistrict, string _WHID = "")
        {
            try
            {
                DataTable dt = new DataTable();

                string sql = @"SELECT DISTINCT t2.ProvinceID, t2.ProvinceCode, t1.[ProvinceName]
                                , t3.AreaID, t3.AreaCode, t1.[AreaName]
                                , [DistrictID], [DistrictCode], [DistrictName]
                                , t1.WHID
                                FROM [tbl_SalAreaDistrict] t1
                                INNER JOIN tbl_MstProvince t2 ON t1.ProvinceName = t2.ProvinceName
                                INNER JOIN tbl_MstArea t3 ON t1.AreaName = t3.AreaName 
                                WHERE T1.WHID <> '' ";

                if (!string.IsNullOrEmpty(_WHID))
                {
                    sql += " AND T1.WHID = '" + _WHID + "'";
                }

                sql += " ORDER BY t2.ProvinceID";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<tbl_SalAreaDistrict> SelectSingle(this tbl_SalAreaDistrict tbl_SalAreaDistrict, string _SalAreaID)
        {
            var list = new List<tbl_SalAreaDistrict>();
            try
            {
                string sql = "SELECT * FROM tbl_SalAreaDistrict";

                if (!string.IsNullOrEmpty(_SalAreaID))
                {
                    sql += " WHERE SalAreaID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + _SalAreaID + "', ',')) ";
                }
                   
                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_SalAreaDistrict), sql);
                list = dynamicListReturned.Cast<tbl_SalAreaDistrict>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return list;
        }

        public static DataTable SelectSingleDT(this tbl_SalAreaDistrict tbl_SalAreaDistrict, string _SalAreaID)
        {
            var dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_SalAreaDistrict";

                if (!string.IsNullOrEmpty(_SalAreaID))
                {
                    sql += " WHERE SalAreaID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + _SalAreaID + "', ',')) ";
                }

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return dt;
        }

        public static int DeleteByWHID(this tbl_SalAreaDistrict tbl_SalAreaDistrict, string WHID)
        {
            int ret = 0;

            SqlConnection con = new SqlConnection(Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            try
            {
                string sql = "DELETE tbl_SalAreaDistrict WHERE WHID=@WHID";
                cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.AddWithValue("@WHID", WHID);
                ret = cmd.ExecuteNonQuery();
                con.Close();

                ret = ret > 0 ? 1 : 0;

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return ret;
        }

        public static int InsertSalAreaDistrict(this tbl_SalAreaDistrict tbl_SalAreaDistrict, DataRow drs)
        {
            int ret = 0;

            SqlConnection con = new SqlConnection(Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            try
            {
                string sql = @"INSERT INTO dbo.[tbl_SalAreaDistrict]
                                ([SalAreaID]
                                ,[WHID]
                                ,[DistrictID]
                                ,[DistrictCode]
                                ,[DistrictName]
                                ,[AreaName]
                                ,[ProvinceName]
                                ,[PostalCode]
                                ,[FlagSend] )
                            VALUES
                                ( @SalAreaID
                                , @WHID
                                , @DistrictID
                                , @DistrictCode
                                , @DistrictName
                                , @AreaName
                                , @ProvinceName
                                , @PostalCode
                                , @FlagSend )";

                cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.AddWithValue("@SalAreaID", drs["SalAreaID"]);
                cmd.Parameters.AddWithValue("@WHID", drs["WHID"]);
                cmd.Parameters.AddWithValue("@DistrictID", drs["DistrictID"]);
                cmd.Parameters.AddWithValue("@DistrictCode", drs["DistrictCode"]);
                cmd.Parameters.AddWithValue("@DistrictName", drs["DistrictName"]);
                cmd.Parameters.AddWithValue("@AreaName", drs["AreaName"]);
                cmd.Parameters.AddWithValue("@ProvinceName", drs["ProvinceName"]);
                cmd.Parameters.AddWithValue("@PostalCode", drs["PostalCode"]);
                cmd.Parameters.AddWithValue("@FlagSend", drs["FlagSend"]);

                ret = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return ret;
        }

        public static DataTable GetSalAreaDistrictByWHID(this tbl_SalAreaDistrict tbl_SalAreaDistrict, string _WHID = "")
        {
            try
            {
                DataTable dt = new DataTable();

                string sql = "SELECT DISTINCT SalAreaID FROM tbl_SalAreaDistrict";
                if (!string.IsNullOrEmpty(_WHID))
                {
                    sql += " WHERE WHID = '" + _WHID + "'";
                }

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
