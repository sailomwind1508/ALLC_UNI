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
    public static class AdmFormListDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_AdmFormList"></param>
        /// <returns></returns>
        public static List<tbl_AdmFormList> Select(this tbl_AdmFormList tbl_AdmFormList, Func<tbl_AdmFormList, bool> predicate)
        {
            List<tbl_AdmFormList> list = new List<tbl_AdmFormList>();
            try
            {
                list = tbl_AdmFormList.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_AdmFormList.Where(predicate).AsQueryable().ToList();

                //    //list = db.tbl_AdmFormList.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmFormList.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_AdmFormList"></param>
        /// <returns></returns>
        public static List<tbl_AdmFormList> SelectAll(this tbl_AdmFormList tbl_AdmFormList)
        {
            List<tbl_AdmFormList> list = new List<tbl_AdmFormList>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_AdmFormList] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_AdmFormList), sql);
                list = dynamicListReturned.Cast<tbl_AdmFormList>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_AdmFormList.OrderBy(x => x.FormID).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmFormList.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_AdmFormList"></param>
        /// <returns></returns>
        public static int Insert(this tbl_AdmFormList tbl_AdmFormList)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_AdmFormList.Attach(tbl_AdmFormList);
                    db.tbl_AdmFormList.Add(tbl_AdmFormList);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmFormList.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_AdmFormList"></param>
        /// <returns></returns>
        public static int Update(this tbl_AdmFormList tbl_AdmFormList)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_AdmFormList.FirstOrDefault(x => x.FormID == tbl_AdmFormList.FormID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_AdmFormListItem in tbl_AdmFormList.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_AdmFormListItem.Name)
                                {
                                    var value = tbl_AdmFormListItem.GetValue(tbl_AdmFormList, null);

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
                ex.WriteLog(tbl_AdmFormList.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_AdmFormList"></param>
        /// <returns></returns>
        public static int Delete(this tbl_AdmFormList tbl_AdmFormList)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_AdmFormList).State = EntityState.Deleted;
                    db.tbl_AdmFormList.Remove(tbl_AdmFormList);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmFormList.GetType());
            }

            return ret;
        }
    }
}
