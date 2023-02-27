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
    public static class ApSupplierDao
    {
        public static List<tbl_ApSupplier> SelectAllNonFlag(this tbl_ApSupplier tbl_ApSupplier)
        {
            List<tbl_ApSupplier> list = new List<tbl_ApSupplier>();
            try
            {
                string sql = "SELECT * FROM tbl_ApSupplier";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ApSupplier), sql);
                list = dynamicListReturned.Cast<tbl_ApSupplier>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplier.GetType());
            }

            return list;
        }
        public static List<tbl_ApSupplier> SelectNonFlag(this tbl_ApSupplier tbl_ApSupplier, Func<tbl_ApSupplier, bool> Condition)
        {
            List<tbl_ApSupplier> list = new List<tbl_ApSupplier>();
            try
            {
                list = tbl_ApSupplier.SelectAllNonFlag().Where(Condition).ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplier.GetType());
            }

            return list;
        }
        /// <summary>
        /// select data by criteria
        /// </summary>
        /// <param name="tbl_ApSupplier"></param>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static tbl_ApSupplier Select(this tbl_ApSupplier tbl_ApSupplier, string supplierID)
        {
            tbl_ApSupplier ret = new tbl_ApSupplier();
            try
            {
                ret = tbl_ApSupplier.SelectAll().FirstOrDefault(x => x.SupplierID == supplierID);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    ret = db.tbl_ApSupplier.Where(x => x.FlagDel == false).FirstOrDefault(x => x.SupplierID == supplierID);
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplier.GetType());
            }

            return ret;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ApSupplier"></param>
        /// <returns></returns>
        public static List<tbl_ApSupplier> SelectAll(this tbl_ApSupplier tbl_ApSupplier)
        {
            List<tbl_ApSupplier> list = new List<tbl_ApSupplier>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_ApSupplier] WHERE FlagDel = 0 ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ApSupplier), sql);
                list = dynamicListReturned.Cast<tbl_ApSupplier>().ToList();


                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ApSupplier.Where(x => x.FlagDel == false).OrderBy(x => x.SupplierCode).ToList();
                //}
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ApSupplier.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ApSupplier"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ApSupplier tbl_ApSupplier)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ApSupplier.Attach(tbl_ApSupplier);
                    db.tbl_ApSupplier.Add(tbl_ApSupplier);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplier.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ApSupplier"></param>
        /// <returns></returns>
        public static int Update(this tbl_ApSupplier tbl_ApSupplier)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ApSupplier.FirstOrDefault(x => x.SupplierID == tbl_ApSupplier.SupplierID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ApSupplierItem in tbl_ApSupplier.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ApSupplierItem.Name)
                                {
                                    var value = tbl_ApSupplierItem.GetValue(tbl_ApSupplier, null);

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
                        ret = tbl_ApSupplier.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplier.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ApSupplier"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ApSupplier tbl_ApSupplier)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ApSupplier).State = EntityState.Deleted;
                    db.tbl_ApSupplier.Remove(tbl_ApSupplier);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ApSupplier.GetType());
            }

            return ret;
        }
        public static DataTable GetApSupplierData(this tbl_ApSupplier tbl_ApSupplier, int flagDel, string Text)
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM tbl_ApSupplier t1";
                sql += " LEFT JOIN tbl_ApSupplierType t2 on t1.SupplierTypeID = t2.APSupplierTypeID";
                sql += " WHERE t1.FlagDel = " + flagDel + "";
                if (!string.IsNullOrEmpty(Text))
                {
                    sql += " AND (t1.SupplierCode like '%" + Text + "%'";
                    sql += " OR t1.SuppName like '%" + Text + "%')";
                }
                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataTable GetDataTable(this tbl_ApSupplier tbl_ApSupplier)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_Supplier_GetDataTable";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                return newTable;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
