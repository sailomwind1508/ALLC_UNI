using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ApSupplierTypeDao
    {
        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ApSupplierType"></param>
        /// <returns></returns>
        public static List<tbl_ApSupplierType> SelectAll(this tbl_ApSupplierType tbl_ApSupplierType)
        {
            List<tbl_ApSupplierType> list = new List<tbl_ApSupplierType>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_ApSupplierType] WHERE FlagDel = 0 ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ApSupplierType), sql);
                list = dynamicListReturned.Cast<tbl_ApSupplierType>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ApSupplierType.Where(x => x.FlagDel == false).OrderBy(x => x.ApSupplierTypeCode).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplierType.GetType());
               
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ApSupplierType"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ApSupplierType tbl_ApSupplierType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ApSupplierType.Attach(tbl_ApSupplierType);
                    db.tbl_ApSupplierType.Add(tbl_ApSupplierType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplierType.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ApSupplierType"></param>
        /// <returns></returns>
        public static int Update(this tbl_ApSupplierType tbl_ApSupplierType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ApSupplierType.FirstOrDefault(x => x.APSupplierTypeID == tbl_ApSupplierType.APSupplierTypeID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ApSupplierTypeItem in tbl_ApSupplierType.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ApSupplierTypeItem.Name)
                                {
                                    var value = tbl_ApSupplierTypeItem.GetValue(tbl_ApSupplierType, null);

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
                        ret = tbl_ApSupplierType.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplierType.GetType());
            }

            return ret;
        }
        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ApSupplierType"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ApSupplierType tbl_ApSupplierType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ApSupplierType).State = EntityState.Deleted;
                    db.tbl_ApSupplierType.Remove(tbl_ApSupplierType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplierType.GetType());
            }

            return ret;
        }
        public static List<tbl_ApSupplierType> SelectAllNonFlag(this tbl_ApSupplierType tbl_ApSupplierType)
        {
            List<tbl_ApSupplierType> list = new List<tbl_ApSupplierType>();
            try
            {
                string sql = "SELECT * FROM tbl_ApSupplierType";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ApSupplierType), sql);
                list = dynamicListReturned.Cast<tbl_ApSupplierType>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplierType.GetType());

            }
            return list;
        }
        public static List<tbl_ApSupplierType> SelectNonFlag(this tbl_ApSupplierType tbl_ApSupplierType, Func<tbl_ApSupplierType, bool> Func)
        {
            List<tbl_ApSupplierType> list = new List<tbl_ApSupplierType>();
            try
            {
                list = tbl_ApSupplierType.SelectAllNonFlag().Where(Func).ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplierType.GetType());

            }
            return list;
        }
        public static DataTable GetApSupplierTypeData(this tbl_ApSupplierType tbl_ApSupplierType, int flagDel, string Text)
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM tbl_ApSupplierType WHERE FlagDel = " + flagDel + "";
                if (!string.IsNullOrEmpty(Text))
                {
                    sql += " AND (ApSupplierTypeCode like '%" + Text + "%'";
                    sql += " OR ApSupplierTypeName like '%" + Text + "%')";
                }
                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
