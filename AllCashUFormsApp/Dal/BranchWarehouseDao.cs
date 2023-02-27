using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace AllCashUFormsApp
{
    public static class BranchWarehouseDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_BranchWarehouse"></param>
        /// <returns></returns>
        public static tbl_BranchWarehouse SelectSingle(this tbl_BranchWarehouse tbl_BranchWarehouse, string whCode)
        {
            List<tbl_BranchWarehouse> list = new List<tbl_BranchWarehouse>();
            tbl_BranchWarehouse ret = null;
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_BranchWarehouse] WHERE WHCode = '" + whCode + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_BranchWarehouse), sql);
                list = dynamicListReturned.Cast<tbl_BranchWarehouse>().ToList();

                if (list.Count > 0)
                {
                    ret = list.First();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
            }

            return ret;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_BranchWarehouse"></param>
        /// <returns></returns>
        public static tbl_BranchWarehouse SelectSingle(this tbl_BranchWarehouse tbl_BranchWarehouse, string whCode, int vanType)
        {
            List<tbl_BranchWarehouse> list = new List<tbl_BranchWarehouse>();
            tbl_BranchWarehouse ret = null;
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_BranchWarehouse] WHERE WHCode = '" + whCode + "' AND VanType = " + vanType + " ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_BranchWarehouse), sql);
                list = dynamicListReturned.Cast<tbl_BranchWarehouse>().ToList();

                if (list.Count > 0)
                {
                    ret = list.First();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
            }

            return ret;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_BranchWarehouse"></param>
        /// <returns></returns>
        public static List<tbl_BranchWarehouse> Select(this tbl_BranchWarehouse tbl_BranchWarehouse, Func<tbl_BranchWarehouse, bool> predicate)
        {
            List<tbl_BranchWarehouse> list = new List<tbl_BranchWarehouse>();
            try
            {
                list = tbl_BranchWarehouse.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_BranchWarehouse.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_BranchWarehouse"></param>
        /// <returns></returns>
        public static List<tbl_BranchWarehouse> SelectAll(this tbl_BranchWarehouse tbl_BranchWarehouse)
        {
            List<tbl_BranchWarehouse> list = new List<tbl_BranchWarehouse>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_BranchWarehouse] WHERE WHCode <> '0' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_BranchWarehouse), sql);
                list = dynamicListReturned.Cast<tbl_BranchWarehouse>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_BranchWarehouse>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_BranchWarehouse.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_BranchWarehouse"></param>
        /// <returns></returns>
        public static int Insert(this tbl_BranchWarehouse tbl_BranchWarehouse)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_BranchWarehouse.Attach(tbl_BranchWarehouse);
                    db.tbl_BranchWarehouse.Add(tbl_BranchWarehouse);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_BranchWarehouse"></param>
        /// <returns></returns>
        public static int Update(this tbl_BranchWarehouse tbl_BranchWarehouse)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_BranchWarehouse.FirstOrDefault(x => x.WHID == tbl_BranchWarehouse.WHID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_BranchWarehouseItem in tbl_BranchWarehouse.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_BranchWarehouseItem.Name)
                                {
                                    var value = tbl_BranchWarehouseItem.GetValue(tbl_BranchWarehouse, null);

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
                        ret = tbl_BranchWarehouse.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_BranchWarehouse"></param>
        /// <returns></returns>
        public static int Delete(this tbl_BranchWarehouse tbl_BranchWarehouse)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_BranchWarehouse).State = EntityState.Deleted;
                    db.tbl_BranchWarehouse.Remove(tbl_BranchWarehouse);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
            }

            return ret;
        }

        public static int SaveWithStore(this List<tbl_BranchWarehouse> tbl_BranchWarehouse)
        {
            int ret = 0;
            try
            {
                {
                    for (int i = 0; i < tbl_BranchWarehouse.Count; i++)
                    {
                        SqlConnection con = new SqlConnection(Connection.ConnectionString);
                        con.Open();

                        SqlCommand cmd = new SqlCommand("proc_tbl_BranchWarehouse_Save", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        foreach (PropertyInfo updateDataItem in tbl_BranchWarehouse[i].GetType().GetProperties())
                        {
                            if (updateDataItem.Name != "CrDate" && updateDataItem.Name != "EdDate")
                            {
                                var value = updateDataItem.GetValue(tbl_BranchWarehouse[i], null);

                                Type t = Nullable.GetUnderlyingType(updateDataItem.PropertyType) ?? updateDataItem.PropertyType;
                                object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                                cmd.Parameters.Add(new SqlParameter("@" + updateDataItem.Name, safeValue));
                            }
                        }

                        ret = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
            }

            return ret;
        }

        public static DataTable GetBranchWarehouseData(this tbl_BranchWarehouse tbl_BranchWarehouse, string _WHID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @" SELECT t1.WHID
                        , t1.WHName
                        , t1.WHType
                        , t1.VanType
                        , t1.License
                        , t1.SaleEmpID
                        , t1.DriverEmpID
                        , t1.FlagDel
                        , t1.POSNo
                        , t1.SaleTypeID
                        , t2.EmpIDCard
                        , T2.TitleName
                        , t2.FirstName
                        , T2.LastName
                        , t3.AutoID
                        , t3.[Name]
                        FROM tbl_BranchWarehouse t1
                        INNER JOIN tbl_Employee t2 ON t1.SaleEmpID = t2.EmpID
                        INNER JOIN tbl_VanType t3 ON t1.WHType = t3.WHType AND t1.VanType = t3.AutoID ";
                sql += " WHERE WHID = '" + _WHID + "'";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouse.GetType());
                return null;
            }

            return dt;
        }
    }
}
