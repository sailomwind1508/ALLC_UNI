using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class HQ_SKUGroup_EXCDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_SKUGroup_EXC"></param>
        /// <returns></returns>
        public static IEnumerable<tbl_HQ_SKUGroup_EXC> Select(this tbl_HQ_SKUGroup_EXC obj, object condition)
        {
            return obj.Select(x => x.SKU_ID.Trim() == condition.ToString().Trim()).AsEnumerable();
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_HQ_SKUGroup_EXC"></param>
        /// <returns></returns>
        public static List<tbl_HQ_SKUGroup_EXC> Select(this tbl_HQ_SKUGroup_EXC tbl_HQ_SKUGroup_EXC, Func<tbl_HQ_SKUGroup_EXC, bool> predicate)
        {
            List<tbl_HQ_SKUGroup_EXC> list = new List<tbl_HQ_SKUGroup_EXC>();
            try
            {
                list = tbl_HQ_SKUGroup_EXC.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_HQ_SKUGroup_EXC.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_SKUGroup_EXC.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_HQ_SKUGroup_EXC"></param>
        /// <returns></returns>
        public static List<tbl_HQ_SKUGroup_EXC> SelectAll(this tbl_HQ_SKUGroup_EXC tbl_HQ_SKUGroup_EXC)
        {
            List<tbl_HQ_SKUGroup_EXC> list = new List<tbl_HQ_SKUGroup_EXC>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_HQ_SKUGroup_EXC] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_HQ_SKUGroup_EXC), sql);
                list = dynamicListReturned.Cast<tbl_HQ_SKUGroup_EXC>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_HQ_SKUGroup_EXC.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_SKUGroup_EXC.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_HQ_SKUGroup_EXC"></param>
        /// <returns></returns>
        public static int Insert(this tbl_HQ_SKUGroup_EXC tbl_HQ_SKUGroup_EXC)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_HQ_SKUGroup_EXC.Attach(tbl_HQ_SKUGroup_EXC);
                    db.tbl_HQ_SKUGroup_EXC.Add(tbl_HQ_SKUGroup_EXC);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_SKUGroup_EXC.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_HQ_SKUGroup_EXC"></param>
        /// <returns></returns>
        public static int Update(this tbl_HQ_SKUGroup_EXC tbl_HQ_SKUGroup_EXC)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_HQ_SKUGroup_EXC.FirstOrDefault(x => x.SKU_ID == tbl_HQ_SKUGroup_EXC.SKU_ID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_HQ_SKUGroup_EXCItem in tbl_HQ_SKUGroup_EXC.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_HQ_SKUGroup_EXCItem.Name)
                                {
                                    var value = tbl_HQ_SKUGroup_EXCItem.GetValue(tbl_HQ_SKUGroup_EXC, null);

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
                        ret = tbl_HQ_SKUGroup_EXC.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_SKUGroup_EXC.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_HQ_SKUGroup_EXC"></param>
        /// <returns></returns>
        public static int Delete(this tbl_HQ_SKUGroup_EXC tbl_HQ_SKUGroup_EXC)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_HQ_SKUGroup_EXC).State = EntityState.Deleted;
                    db.tbl_HQ_SKUGroup_EXC.Remove(tbl_HQ_SKUGroup_EXC);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_HQ_SKUGroup_EXC.GetType());
            }

            return ret;
        }
    }
}
