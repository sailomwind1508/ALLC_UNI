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
    public static class DepartmentDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_Department"></param>
        /// <returns></returns>
        public static List<tbl_Department> Select(this tbl_Department tbl_Department, Func<tbl_Department, bool> predicate)
        {
            List<tbl_Department> list = new List<tbl_Department>();
            try
            {
                list = tbl_Department.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Department.Where(predicate).Where(x => x.FlagDel == false).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Department.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Department"></param>
        /// <returns></returns>
        public static List<tbl_Department> SelectAll(this tbl_Department tbl_Department)
        {
            List<tbl_Department> list = new List<tbl_Department>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_Department] WHERE FlagDel = 0 ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Department), sql);
                list = dynamicListReturned.Cast<tbl_Department>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Department.Where(x => x.FlagDel == false).OrderBy(x => x.DepartmentID).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Department.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_Department"></param>
        /// <returns></returns>
        public static int Insert(this tbl_Department tbl_Department)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_Department.Attach(tbl_Department);
                    db.tbl_Department.Add(tbl_Department);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Department.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Department"></param>
        /// <returns></returns>
        public static int Update(this tbl_Department tbl_Department)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_Department.FirstOrDefault(x => x.DepartmentID == tbl_Department.DepartmentID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_DepartmentItem in tbl_Department.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_DepartmentItem.Name)
                                {
                                    var value = tbl_DepartmentItem.GetValue(tbl_Department, null);

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
                        ret = tbl_Department.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Department.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Department"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Department tbl_Department)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_Department).State = EntityState.Deleted;
                    db.tbl_Department.Remove(tbl_Department);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Department.GetType());
            }

            return ret;
        }
        public static List<tbl_Department> SelectAllNonFlag(this tbl_Department tbl_Department)
        {
            List<tbl_Department> list = new List<tbl_Department>();
            try
            {
                string sql = "SELECT * FROM tbl_Department";
                
                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Department), sql);
                list = dynamicListReturned.Cast<tbl_Department>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Department.GetType());
            }
            return list;
        }
        public static List<tbl_Department> SelectNonFlag(this tbl_Department tbl_Department, Func<tbl_Department, bool> predicate)
        {
            List<tbl_Department> list = new List<tbl_Department>();
            try
            {
                list = tbl_Department.SelectAllNonFlag().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Department.GetType());
            }
            return list;
        }
        public static DataTable GetDepartmentData(this tbl_Department tbl_Department, int flagDel, string Search)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM tbl_Department WHERE FlagDel = " + flagDel + "";
            if (!string.IsNullOrEmpty(Search))
            {
                sql += " AND (DepartmentCode like '%" + Search + "%'";
                sql += " OR DepartmentName like '%" + Search + "%')";
            }
            dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            return dt;
        }
    }
}
