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
    public static class AdmControlListDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_AdmControlList"></param>
        /// <returns></returns>
        public static List<tbl_AdmControlList> Select(this tbl_AdmControlList tbl_AdmControlList, Func<tbl_AdmControlList, bool> predicate)
        {
            List<tbl_AdmControlList> list = new List<tbl_AdmControlList>();
            try
            {
                list = tbl_AdmControlList.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_AdmControlList.Where(predicate).AsQueryable().ToList();

                //    //list = db.tbl_AdmControlList.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmControlList.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_AdmControlList"></param>
        /// <returns></returns>
        public static List<tbl_AdmControlList> SelectAll(this tbl_AdmControlList tbl_AdmControlList)
        {
            List<tbl_AdmControlList> list = new List<tbl_AdmControlList>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_AdmControlList] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_AdmControlList), sql);
                list = dynamicListReturned.Cast<tbl_AdmControlList>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_AdmControlList.OrderBy(x => x.ControlID).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmControlList.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_AdmControlList"></param>
        /// <returns></returns>
        public static int Insert(this tbl_AdmControlList tbl_AdmControlList)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_AdmControlList.Attach(tbl_AdmControlList);
                    db.tbl_AdmControlList.Add(tbl_AdmControlList);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmControlList.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_AdmControlList"></param>
        /// <returns></returns>
        public static int Update(this tbl_AdmControlList tbl_AdmControlList)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_AdmControlList.FirstOrDefault(x => x.FormID == tbl_AdmControlList.FormID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_AdmControlListItem in tbl_AdmControlList.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_AdmControlListItem.Name)
                                {
                                    var value = tbl_AdmControlListItem.GetValue(tbl_AdmControlList, null);

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
                        ret = tbl_AdmControlList.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmControlList.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_AdmControlList"></param>
        /// <returns></returns>
        public static int Delete(this tbl_AdmControlList tbl_AdmControlList)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_AdmControlList).State = EntityState.Deleted;
                    db.tbl_AdmControlList.Remove(tbl_AdmControlList);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmControlList.GetType());
            }

            return ret;
        }
    }
}
