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
    public static class ProductGroupDao
    {
        public static List<tbl_ProductGroup> SelectAllOrderByProductGroupCode(this tbl_ProductGroup tbl_ProductGroup)
        {
            List<tbl_ProductGroup> list = new List<tbl_ProductGroup>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_ProductGroup] ORDER BY ProductGroupCode ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductGroup), sql);
                list = dynamicListReturned.Cast<tbl_ProductGroup>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductGroup.OrderBy(x => x.ProductGroupCode).ToList();
                //}
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ProductGroup.GetType());
            }

            return list;
        }

        public static List<tbl_ProductGroup> SelectNonFlag(this tbl_ProductGroup tbl_ProductGroup, Func<tbl_ProductGroup, bool> predicate)
        {
            List<tbl_ProductGroup> list = new List<tbl_ProductGroup>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_ProductGroup] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductGroup), sql);
                list = dynamicListReturned.Cast<tbl_ProductGroup>().ToList();

                list = list.Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductGroup.Where(predicate).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductGroup.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_ProductGroup"></param>
        /// <returns></returns>
        public static List<tbl_ProductGroup> Select(this tbl_ProductGroup tbl_ProductGroup, Func<tbl_ProductGroup, bool> predicate)
        {
            List<tbl_ProductGroup> list = new List<tbl_ProductGroup>();
            try
            {
                list = tbl_ProductGroup.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductGroup.Where(x => x.FlagDel == false).Where(predicate).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductGroup.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ProductGroup"></param>
        /// <returns></returns>
        public static List<tbl_ProductGroup> SelectAll(this tbl_ProductGroup tbl_ProductGroup)
        {
            List<tbl_ProductGroup> list = new List<tbl_ProductGroup>();
            try
            {

                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_ProductGroup] WHERE FlagDel = 0 Order By ProductGroupID ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductGroup), sql);
                list = dynamicListReturned.Cast<tbl_ProductGroup>().ToList();


                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductGroup.Where(x => x.FlagDel == false).OrderBy(x => x.ProductGroupID).ToList();
                //}
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ProductGroup.GetType());
            }

            return list;
        }


        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ProductGroup"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ProductGroup tbl_ProductGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ProductGroup.Attach(tbl_ProductGroup);
                    db.tbl_ProductGroup.Add(tbl_ProductGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ProductGroup"></param>
        /// <returns></returns>
        public static int Update(this tbl_ProductGroup tbl_ProductGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ProductGroup.FirstOrDefault(x => x.ProductGroupID == tbl_ProductGroup.ProductGroupID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ProductGroupItem in tbl_ProductGroup.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ProductGroupItem.Name)
                                {
                                    var value = tbl_ProductGroupItem.GetValue(tbl_ProductGroup, null);

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
                        ret = tbl_ProductGroup.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ProductGroup"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ProductGroup tbl_ProductGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ProductGroup).State = EntityState.Deleted;
                    db.tbl_ProductGroup.Remove(tbl_ProductGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductGroup.GetType());
            }

            return ret;
        }
        public static DataTable GetProductGroupTable(this tbl_ProductGroup tbl_ProductGroup)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM tbl_ProductGroup WHERE FlagDel = 0 ORDER BY ProductGroupID";
            dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            return dt;
        }
        public static DataTable GetPrdGroupTable(this tbl_ProductGroup tbl_ProductGroup)
        {
            DataTable newTable = new DataTable();

            string sql = "proc_GetPrdGroupTable";

            newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

            return newTable;
        }
    }
}
