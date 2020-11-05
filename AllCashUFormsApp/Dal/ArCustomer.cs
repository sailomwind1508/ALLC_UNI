using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ArCustomer
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_ArCustomer"></param>
        /// <returns></returns>
        public static List<tbl_ArCustomer> Select(this tbl_ArCustomer tbl_ArCustomer, Func<tbl_ArCustomer, bool> predicate)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ArCustomer.Where(predicate).Where(x => x.FlagDel == false).OrderBy(x => x.WHID).ThenBy(x => x.SalAreaID).ThenBy(x => x.Seq).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ArCustomer"></param>
        /// <returns></returns>
        public static List<tbl_ArCustomer> SelectAll(this tbl_ArCustomer tbl_ArCustomer)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ArCustomer.Where(x => x.FlagDel == false).OrderBy(x => x.WHID).ThenBy(x => x.SalAreaID).ThenBy(x => x.Seq).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ArCustomer"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ArCustomer tbl_ArCustomer)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ArCustomer.Attach(tbl_ArCustomer);
                    db.tbl_ArCustomer.Add(tbl_ArCustomer);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ArCustomer"></param>
        /// <returns></returns>
        public static int Update(this tbl_ArCustomer tbl_ArCustomer)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ArCustomer.FirstOrDefault(x => x.CustomerID == tbl_ArCustomer.CustomerID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ArCustomerItem in tbl_ArCustomer.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ArCustomerItem.Name)
                                {
                                    var value = tbl_ArCustomerItem.GetValue(tbl_ArCustomer, null);

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
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ArCustomer"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ArCustomer tbl_ArCustomer)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ArCustomer).State = EntityState.Deleted;
                    db.tbl_ArCustomer.Remove(tbl_ArCustomer);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return ret;
        }
    }
}