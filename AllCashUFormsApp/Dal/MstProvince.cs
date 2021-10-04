using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class MstProvince
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_MstProvince"></param>
        /// <returns></returns>
        public static List<tbl_MstProvince> Select(this tbl_MstProvince tbl_MstProvince, Func<tbl_MstProvince, bool> predicate)
        {
            List<tbl_MstProvince> list = new List<tbl_MstProvince>();
            try
            {
                list = tbl_MstProvince.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_MstProvince.Where(x => x.FlagDel == false).Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstProvince.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_MstProvince"></param>
        /// <returns></returns>
        public static List<tbl_MstProvince> SelectAll(this tbl_MstProvince tbl_MstProvince)
        {
            List<tbl_MstProvince> list = new List<tbl_MstProvince>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_MstProvince] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_MstProvince), sql);
                list = dynamicListReturned.Cast<tbl_MstProvince>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_MstProvince.Where(x => x.FlagDel == false).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstProvince.GetType());
            }

            return list;
        }
      
        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_MstProvince"></param>
        /// <returns></returns>
        public static int Insert(this tbl_MstProvince tbl_MstProvince)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_MstProvince.Attach(tbl_MstProvince);
                    db.tbl_MstProvince.Add(tbl_MstProvince);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstProvince.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_MstProvince"></param>
        /// <returns></returns>
        public static int Update(this tbl_MstProvince tbl_MstProvince) //
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_MstProvince.FirstOrDefault(x => x.ProvinceID == tbl_MstProvince.ProvinceID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_MstProvinceItem in tbl_MstProvince.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_MstProvinceItem.Name)
                                {
                                    var value = tbl_MstProvinceItem.GetValue(tbl_MstProvince, null);

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
                        ret = tbl_MstProvince.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstProvince.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_MstProvince"></param>
        /// <returns></returns>
        public static int Delete(this tbl_MstProvince tbl_MstProvince)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_MstProvince).State = EntityState.Deleted;
                    db.tbl_MstProvince.Remove(tbl_MstProvince);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstProvince.GetType());
            }

            return ret;
        }
    }
}
