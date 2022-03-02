using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace AllCashUFormsApp
{
    public static class BranchDao
    {
        public static DataTable GetDataTable(this tbl_Branch tbl_Branch)
        {
            try
            {
                List<tbl_Branch> tbl_Branchs = new List<tbl_Branch>();
                tbl_Branchs = (new tbl_Branch()).SelectAll();

                return tbl_Branchs.ToDataTable();
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                return null;
            }
        }
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_Branch"></param>
        /// <returns></returns>
        public static List<tbl_Branch> Select(this tbl_Branch tbl_Branch, Func<tbl_Branch, bool> predicate)
        {
            List<tbl_Branch> list = new List<tbl_Branch>();
            try
            {
                list = tbl_Branch.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Branch.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Branch.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Branch"></param>
        /// <returns></returns>
        public static List<tbl_Branch> SelectAll(this tbl_Branch tbl_Branch)
        {
            List<tbl_Branch> list = new List<tbl_Branch>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM tbl_Branch";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Branch), sql);
                list = dynamicListReturned.Cast<tbl_Branch>().ToList();

                //list = ConvertHelper.ConvertDataTable<tbl_Branch>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Branch.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Branch.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_Branch"></param>
        /// <returns></returns>
        public static int Insert(this tbl_Branch tbl_Branch)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_Branch.Attach(tbl_Branch);
                    db.tbl_Branch.Add(tbl_Branch);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Branch.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Branch"></param>
        /// <returns></returns>
        public static int Update(this tbl_Branch tbl_Branch)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_Branch.FirstOrDefault(x => x.BranchID == tbl_Branch.BranchID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_BranchItem in tbl_Branch.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_BranchItem.Name)
                                {
                                    var value = tbl_BranchItem.GetValue(tbl_Branch, null);

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
                        ret = tbl_Branch.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Branch.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Branch"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Branch tbl_Branch)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_Branch).State = EntityState.Deleted;
                    db.tbl_Branch.Remove(tbl_Branch);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Branch.GetType());
            }

            return ret;
        }
        public static DataTable Get_proc_SendProductInfo_GetDataTable(this tbl_Branch tbl_Branch)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_SendProductInfo_GetDataTable";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                return newTable;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataTable GetSendProductInfoPrepareData(this tbl_Branch tbl_Branch)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_SendProductInfo_PrepareData";

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
