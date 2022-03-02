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
    public static class BranchWarehouseMapping
    {
        public static List<tbl_BranchWarehouseMapping> SelectAll(this tbl_BranchWarehouseMapping tbl_BranchWarehouseMapping)
        {
            var list = new List<tbl_BranchWarehouseMapping>();
            try
            {
                string sql = "SELECT * FROM tbl_BranchWarehouseMapping";
                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_BranchWarehouseMapping), sql);
                list = dynamicListReturned.Cast<tbl_BranchWarehouseMapping>().ToList();
            }
            catch (Exception ex)
            {

            }

            return list;
        }

        public static DataTable GetBranchWarehouseMappingDT(this tbl_BranchWarehouseMapping tbl_BranchWarehouseMapping)
        {
            var dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_BranchWarehouseMapping";
                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            }
            catch (Exception ex)
            {

            }

            return dt;
        }

        public static int SaveWithStore(this List<tbl_BranchWarehouseMapping> tbl_BranchWarehouseMapping)
        {
            int ret = 0;
            try
            {
                {
                    SqlConnection con = new SqlConnection(Connection.ConnectionString);

                    con.Open();

                    for (int i = 0; i < tbl_BranchWarehouseMapping.Count; i++)
                    {
                        SqlCommand cmd = new SqlCommand("proc_tbl_BranchWarehouseMapping_Save", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        foreach (PropertyInfo updateDataItem in tbl_BranchWarehouseMapping[i].GetType().GetProperties())
                        {
                            var value = updateDataItem.GetValue(tbl_BranchWarehouseMapping[i], null);

                            Type t = Nullable.GetUnderlyingType(updateDataItem.PropertyType) ?? updateDataItem.PropertyType;
                            object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                            cmd.Parameters.Add(new SqlParameter("@" + updateDataItem.Name, safeValue));
                        }

                        ret = cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouseMapping.GetType());
            }

            return ret;
        }

        public static int DeleteWithStore(this tbl_BranchWarehouseMapping tbl_BranchWarehouseMapping)
        {
            int ret = 0;
            try
            {
                {
                    SqlConnection con = new SqlConnection(Connection.ConnectionString);

                    con.Open();

                    SqlCommand cmd = new SqlCommand("proc_tbl_BranchWarehouseMapping_Delete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    foreach (PropertyInfo updateDataItem in tbl_BranchWarehouseMapping.GetType().GetProperties())
                    {
                        var value = updateDataItem.GetValue(tbl_BranchWarehouseMapping, null);

                        Type t = Nullable.GetUnderlyingType(updateDataItem.PropertyType) ?? updateDataItem.PropertyType;
                        object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                        cmd.Parameters.Add(new SqlParameter("@" + updateDataItem.Name, safeValue));
                    }

                    ret = cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchWarehouseMapping.GetType());
            }

            return ret;
        }
    }
}
