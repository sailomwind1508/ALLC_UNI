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
    public static class MenuDao
    {
        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_MstMenu"></param>
        /// <returns></returns>
        public static List<tbl_MstMenu> SelectAll(this tbl_MstMenu tbl_MstMenu)
        {
            List<tbl_MstMenu> list = new List<tbl_MstMenu>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_MstMenu] ORDER BY Seq ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_MstMenu), sql);
                list = dynamicListReturned.Cast<tbl_MstMenu>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_MstMenu.OrderBy(x => x.Seq).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstMenu.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_MstMenu"></param>
        /// <returns></returns>
        public static int Insert(this tbl_MstMenu tbl_MstMenu)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_MstMenu.Add(tbl_MstMenu);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstMenu.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_MstMenu"></param>
        /// <returns></returns>
        public static int Update(this tbl_MstMenu tbl_MstMenu)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_MstMenu.FirstOrDefault(x => x.MenuID == tbl_MstMenu.MenuID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_MstMenuItem in tbl_MstMenu.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_MstMenuItem.Name)
                                {
                                    var value = tbl_MstMenuItem.GetValue(tbl_MstMenu, null);

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
                ex.WriteLog(tbl_MstMenu.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_MstMenu"></param>
        /// <returns></returns>
        public static int Delete(this tbl_MstMenu tbl_MstMenu)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_MstMenu).State = EntityState.Deleted;
                    db.tbl_MstMenu.Remove(tbl_MstMenu);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstMenu.GetType());
            }

            return ret;
        }
    }
}
