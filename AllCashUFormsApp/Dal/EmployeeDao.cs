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
    public static class EmployeeDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_Employee"></param>
        /// <returns></returns>
        public static List<tbl_Employee> Select(this tbl_Employee tbl_Employee, Func<tbl_Employee, bool> predicate)
        {
            List<tbl_Employee> list = new List<tbl_Employee>();
            try
            {
                list = tbl_Employee.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Employee.Where(predicate).OrderBy(x => x.EmpCode).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Employee.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Employee"></param>
        /// <returns></returns>
        public static List<tbl_Employee> SelectAll(this tbl_Employee tbl_Employee)
        {
            List<tbl_Employee> list = new List<tbl_Employee>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_Employee] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Employee), sql);
                list = dynamicListReturned.Cast<tbl_Employee>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_Employee>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Employee.OrderBy(x => x.EmpCode).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Employee.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_Employee"></param>
        /// <returns></returns>
        public static int Insert(this tbl_Employee tbl_Employee)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_Employee.Attach(tbl_Employee);
                    db.tbl_Employee.Add(tbl_Employee);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Employee.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Employee"></param>
        /// <returns></returns>
        public static int Update(this tbl_Employee tbl_Employee)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_Employee.FirstOrDefault(x => x.EmpID == tbl_Employee.EmpID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_EmployeeItem in tbl_Employee.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_EmployeeItem.Name)
                                {
                                    var value = tbl_EmployeeItem.GetValue(tbl_Employee, null);

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
                        ret = tbl_Employee.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Employee.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Employee"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Employee tbl_Employee)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_Employee).State = EntityState.Deleted;
                    db.tbl_Employee.Remove(tbl_Employee);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Employee.GetType());
            }

            return ret;
        }

        public static DataTable GetSaleEmployee(this tbl_Employee tbl_Employee, int PositionID)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM SaleEmployeeView ";
                sql += " WHERE PositionID = " + PositionID + "";
                //sql += " AND WHID IS NULL";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetAllSaleEmployee(this tbl_Employee tbl_Employee)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM AllSaleEmployeeView";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetEmployeePopup(this tbl_Employee tbl_Employee)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT EmpID, CONCAT(TitleName, ' ', FirstName) AS FullName FROM tbl_Employee ";
                sql += " WHERE PositionID = 4 AND DepartmentID = 3";

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
