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
    public static class RolesDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_Roles"></param>
        /// <returns></returns>
        public static List<tbl_Roles> Select(this tbl_Roles tbl_Roles, Func<tbl_Roles, bool> predicate)
        {
            List<tbl_Roles> list = new List<tbl_Roles>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_Roles.Where(predicate).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Roles.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Roles"></param>
        /// <returns></returns>
        public static List<tbl_Roles> SelectAll(this tbl_Roles tbl_Roles)
        {
            List<tbl_Roles> list = new List<tbl_Roles>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_Roles.OrderBy(x => x.RoleID).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Roles.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_Roles"></param>
        /// <returns></returns>
        public static int Insert(this tbl_Roles tbl_Roles)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_Roles.Attach(tbl_Roles);
                    db.tbl_Roles.Add(tbl_Roles);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Roles.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Roles"></param>
        /// <returns></returns>
        public static int Update(this tbl_Roles tbl_Roles)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_Roles.FirstOrDefault(x => x.RoleID == tbl_Roles.RoleID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_RolesItem in tbl_Roles.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_RolesItem.Name)
                                {
                                    var value = tbl_RolesItem.GetValue(tbl_Roles, null);

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
                ex.WriteLog(tbl_Roles.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Roles"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Roles tbl_Roles)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_Roles).State = EntityState.Deleted;
                    db.tbl_Roles.Remove(tbl_Roles);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Roles.GetType());
            }

            return ret;
        }
    }
}
