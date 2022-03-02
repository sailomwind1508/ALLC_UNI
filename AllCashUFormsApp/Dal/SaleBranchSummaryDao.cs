using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace AllCashUFormsApp
{
    public static class SaleBranchSummaryDao
    {

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_SaleBranchSummary"></param>
        /// <returns></returns>
        public static tbl_SaleBranchSummary Select(this tbl_SaleBranchSummary tbl_SaleBranchSummary, string branchID, DateTime saleDate)
        {
            List<tbl_SaleBranchSummary> list = new List<tbl_SaleBranchSummary>();
            tbl_SaleBranchSummary ret = null;

            try
            {
                DataTable dt = new DataTable();
                string _saleDate = saleDate.ToString("yyyyMMdd", new CultureInfo("en-US"));

                string sql = @" SELECT * FROM [dbo].[tbl_SaleBranchSummary] 
                WHERE FlagDel = 0 AND BranchID = '" + branchID + "' AND CAST(SaleDate AS DATE) = '" + _saleDate + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_SaleBranchSummary), sql);
                list = dynamicListReturned.Cast<tbl_SaleBranchSummary>().ToList();

                if (list.Count > 0)
                    ret = list.First();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SaleBranchSummary.GetType());
            }

            return ret;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_SaleBranchSummary"></param>
        /// <returns></returns>
        public static List<tbl_SaleBranchSummary> Select(this tbl_SaleBranchSummary tbl_SaleBranchSummary, Func<tbl_SaleBranchSummary, bool> predicate)
        {
            List<tbl_SaleBranchSummary> list = new List<tbl_SaleBranchSummary>();
            try
            {
                list = tbl_SaleBranchSummary.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    //var data = db.tbl_SaleBranchSummary.ToList().Where(predicate);
                //    list = db.tbl_SaleBranchSummary.Where(predicate).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SaleBranchSummary.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_SaleBranchSummary"></param>
        /// <returns></returns>
        public static List<tbl_SaleBranchSummary> SelectAll(this tbl_SaleBranchSummary tbl_SaleBranchSummary)
        {
            List<tbl_SaleBranchSummary> list = new List<tbl_SaleBranchSummary>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_SaleBranchSummary] ";


                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_SaleBranchSummary), sql);
                list = dynamicListReturned.Cast<tbl_SaleBranchSummary>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_SaleBranchSummary>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_SaleBranchSummary.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SaleBranchSummary.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_SaleBranchSummary"></param>
        /// <returns></returns>
        public static int Insert(this tbl_SaleBranchSummary tbl_SaleBranchSummary)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_SaleBranchSummary.Attach(tbl_SaleBranchSummary);
                    db.tbl_SaleBranchSummary.Add(tbl_SaleBranchSummary);
                    ret = db.SaveChanges();
                }
            }
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SaleBranchSummary.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_SaleBranchSummary"></param>
        /// <returns></returns>
        public static int Update(this tbl_SaleBranchSummary tbl_SaleBranchSummary)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_SaleBranchSummary.FirstOrDefault(x => x.BranchID == tbl_SaleBranchSummary.BranchID && x.SaleDate == tbl_SaleBranchSummary.SaleDate);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_SaleBranchSummaryItem in tbl_SaleBranchSummary.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_SaleBranchSummaryItem.Name)
                                {
                                    var value = tbl_SaleBranchSummaryItem.GetValue(tbl_SaleBranchSummary, null);

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
                        ret = tbl_SaleBranchSummary.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SaleBranchSummary.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_SaleBranchSummary"></param>
        /// <returns></returns>
        public static int Delete(this tbl_SaleBranchSummary tbl_SaleBranchSummary)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_SaleBranchSummary).State = EntityState.Deleted;
                    db.tbl_SaleBranchSummary.Remove(tbl_SaleBranchSummary);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SaleBranchSummary.GetType());
            }

            return ret;
        }
    }
}
