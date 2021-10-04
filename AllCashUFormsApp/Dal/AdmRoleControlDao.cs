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
    public static class AdmRoleControlDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_AdmRoleControl"></param>
        /// <returns></returns>
        public static List<tbl_AdmRoleControl> Select(this tbl_AdmRoleControl tbl_AdmRoleControl, Func<tbl_AdmRoleControl, bool> predicate)
        {
            List<tbl_AdmRoleControl> list = new List<tbl_AdmRoleControl>();
            try
            {
                list = tbl_AdmRoleControl.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_AdmRoleControl.Where(predicate).AsQueryable().ToList();

                //    //list = db.tbl_AdmRoleControl.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmRoleControl.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_AdmRoleControl"></param>
        /// <returns></returns>
        public static List<tbl_AdmRoleControl> SelectAll(this tbl_AdmRoleControl tbl_AdmRoleControl)
        {
            List<tbl_AdmRoleControl> list = new List<tbl_AdmRoleControl>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_AdmRoleControl] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_AdmRoleControl), sql);
                list = dynamicListReturned.Cast<tbl_AdmRoleControl>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_AdmRoleControl.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmRoleControl.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_AdmRoleControl"></param>
        /// <returns></returns>
        public static int Insert(this tbl_AdmRoleControl tbl_AdmRoleControl)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_AdmRoleControl.Attach(tbl_AdmRoleControl);
                    db.tbl_AdmRoleControl.Add(tbl_AdmRoleControl);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmRoleControl.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_AdmRoleControl"></param>
        /// <returns></returns>
        public static int Update(this tbl_AdmRoleControl tbl_AdmRoleControl)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_AdmRoleControl.FirstOrDefault(x => x.RoleID == tbl_AdmRoleControl.RoleID && x.ControlID == tbl_AdmRoleControl.ControlID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_AdmRoleControlItem in tbl_AdmRoleControl.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_AdmRoleControlItem.Name)
                                {
                                    var value = tbl_AdmRoleControlItem.GetValue(tbl_AdmRoleControl, null);

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
                        ret = tbl_AdmRoleControl.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmRoleControl.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_AdmRoleControl"></param>
        /// <returns></returns>
        public static int Delete(this tbl_AdmRoleControl tbl_AdmRoleControl)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_AdmRoleControl).State = EntityState.Deleted;
                    db.tbl_AdmRoleControl.Remove(tbl_AdmRoleControl);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmRoleControl.GetType());
            }

            return ret;
        }
    }
}
