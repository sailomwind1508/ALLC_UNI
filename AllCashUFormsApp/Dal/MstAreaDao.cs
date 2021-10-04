using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class MstAreaDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_MstArea"></param>
        /// <returns></returns>
        public static List<tbl_MstArea> Select(this tbl_MstArea tbl_MstArea, Func<tbl_MstArea, bool> predicate)
        {
            List<tbl_MstArea> list = new List<tbl_MstArea>();
            try
            {
                list = tbl_MstArea.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_MstArea.Where(x => x.FlagDel == false).Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstArea.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_MstArea"></param>
        /// <returns></returns>
        public static List<tbl_MstArea> SelectAll(this tbl_MstArea tbl_MstArea)
        {
            List<tbl_MstArea> list = new List<tbl_MstArea>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_MstArea] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_MstArea), sql);
                list = dynamicListReturned.Cast<tbl_MstArea>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_MstArea.Where(x => x.FlagDel == false).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstArea.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_MstArea"></param>
        /// <returns></returns>
        public static int Insert(this tbl_MstArea tbl_MstArea)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_MstArea.Attach(tbl_MstArea);
                    db.tbl_MstArea.Add(tbl_MstArea);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstArea.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_MstArea"></param>
        /// <returns></returns>
        public static int Update(this tbl_MstArea tbl_MstArea)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_MstArea.FirstOrDefault(x => x.AreaID == tbl_MstArea.AreaID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_MstAreaItem in tbl_MstArea.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_MstAreaItem.Name)
                                {
                                    var value = tbl_MstAreaItem.GetValue(tbl_MstArea, null);

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
                        ret = tbl_MstArea.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstArea.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_MstArea"></param>
        /// <returns></returns>
        public static int Delete(this tbl_MstArea tbl_MstArea)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_MstArea).State = EntityState.Deleted;
                    db.tbl_MstArea.Remove(tbl_MstArea);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstArea.GetType());
            }

            return ret;
        }
    }
}
