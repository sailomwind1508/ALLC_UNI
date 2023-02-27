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
    public static class CompanyDao
    {
        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Company"></param>
        /// <returns></returns>
        public static List<tbl_Company> SelectAll(this tbl_Company tbl_Company)
        {
            List<tbl_Company> list = new List<tbl_Company>();
            try
            {

                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_Company] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Company), sql);
                list = dynamicListReturned.Cast<tbl_Company>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Company.OrderBy(x => x.CompanyCode).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Company.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_Company"></param>
        /// <returns></returns>
        public static int Insert(this tbl_Company tbl_Company)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_Company.Attach(tbl_Company);
                    db.tbl_Company.Add(tbl_Company);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Company.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Company"></param>
        /// <returns></returns>
        public static int Update(this tbl_Company tbl_Company)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_Company.FirstOrDefault(x => x.CompanyID == tbl_Company.CompanyID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_CompanyItem in tbl_Company.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_CompanyItem.Name)
                                {
                                    var value = tbl_CompanyItem.GetValue(tbl_Company, null);

                                    Type t = Nullable.GetUnderlyingType(updateDataItem.PropertyType) ?? updateDataItem.PropertyType;
                                    object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                                    updateDataItem.SetValue(updateData, safeValue, null);
                                }
                            }
                        }

                        db.Entry(updateData).State = System.Data.Entity.EntityState.Modified;
                        ret = db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Company.GetType());
            }

            return ret;
        }


        public static int Test(this tbl_Company tbl_Company)
        {
            int ret = 0;
            try
            {

                SqlConnection con = new SqlConnection(Connection.ConnectionString);

                con.Open();

                SqlCommand cmd = new SqlCommand("proc_tbl_Company_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                foreach (PropertyInfo updateDataItem in tbl_Company.GetType().GetProperties())
                {
                    var value = updateDataItem.GetValue(tbl_Company, null);

                    Type t = Nullable.GetUnderlyingType(updateDataItem.PropertyType) ?? updateDataItem.PropertyType;
                    object safeValue = (value == null) ? null : Convert.ChangeType(value, t);


                    cmd.Parameters.Add(new SqlParameter("@" + updateDataItem.Name, safeValue));


                }

                var result = cmd.ExecuteNonQuery();

                con.Close();

                ret = 1;


            }
            catch (Exception ex)
            {
                ret = -1;
                ex.WriteLog(tbl_Company.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Company"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Company tbl_Company)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_Company).State = EntityState.Deleted;
                    db.tbl_Company.Remove(tbl_Company);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Company.GetType());
            }

            return ret;
        }
        public static DataTable GetCompanyTable(this tbl_Company tbl_Company)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM tbl_Company";
            dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            return dt;
        }
    }
}
