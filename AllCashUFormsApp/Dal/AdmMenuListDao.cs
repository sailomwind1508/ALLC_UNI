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
    public static class AdmMenuListDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_AdmMenuList"></param>
        /// <returns></returns>
        public static List<tbl_AdmMenuList> Select(this tbl_AdmMenuList tbl_AdmMenuList, Func<tbl_AdmMenuList, bool> predicate)
        {
            List<tbl_AdmMenuList> list = new List<tbl_AdmMenuList>();
            try
            {
                list = tbl_AdmMenuList.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_AdmMenuList.Where(predicate).AsQueryable().ToList();

                //    //list = db.tbl_AdmMenuList.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmMenuList.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_AdmMenuList"></param>
        /// <returns></returns>
        public static List<tbl_AdmMenuList> SelectAll(this tbl_AdmMenuList tbl_AdmMenuList)
        {
            List<tbl_AdmMenuList> list = new List<tbl_AdmMenuList>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_AdmMenuList] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_AdmMenuList), sql);
                list = dynamicListReturned.Cast<tbl_AdmMenuList>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_AdmMenuList.OrderBy(x => x.GrpAutoID).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmMenuList.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_AdmMenuList"></param>
        /// <returns></returns>
        public static int Insert(this tbl_AdmMenuList tbl_AdmMenuList)
        {
            string msg = "start AdmMenuListDao=>Insert";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_AdmMenuList.Attach(tbl_AdmMenuList);
                    db.tbl_AdmMenuList.Add(tbl_AdmMenuList);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmMenuList.GetType());
            }

            msg = "end AdmMenuListDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_AdmMenuList"></param>
        /// <returns></returns>
        public static int Update(this tbl_AdmMenuList tbl_AdmMenuList)
        {
            string msg = "start AdmMenuListDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_AdmMenuList.FirstOrDefault(x => x.GrpAutoID == tbl_AdmMenuList.GrpAutoID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_AdmMenuListItem in tbl_AdmMenuList.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_AdmMenuListItem.Name)
                                {
                                    var value = tbl_AdmMenuListItem.GetValue(tbl_AdmMenuList, null);

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
                        ret = tbl_AdmMenuList.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmMenuList.GetType());
            }

            msg = "end AdmMenuListDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_AdmMenuList"></param>
        /// <returns></returns>
        public static int Delete(this tbl_AdmMenuList tbl_AdmMenuList)
        {
            string msg = "start AdmMenuListDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_AdmMenuList).State = EntityState.Deleted;
                    db.tbl_AdmMenuList.Remove(tbl_AdmMenuList);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_AdmMenuList.GetType());
            }

            msg = "end AdmMenuListDao=>Update";
            msg.WriteLog(null);

            return ret;
        }
    }
}
