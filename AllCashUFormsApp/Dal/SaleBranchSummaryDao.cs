using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
namespace AllCashUFormsApp
{
    public static class SaleBranchSummaryDao
    {
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
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_SaleBranchSummary.Where(x => x.FlagDel == false).Where(predicate).AsQueryable().ToList();
                }
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
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_SaleBranchSummary.Where(x => x.FlagDel == false).ToList();
                }
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
                                if (updateDataItem.Name == tbl_SaleBranchSummaryItem.Name)
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
