using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;


namespace AllCashUFormsApp
{
    public static class InvTransactionDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_InvTransaction"></param>
        /// <returns></returns>
        public static List<tbl_InvTransaction> Select(this tbl_InvTransaction tbl_InvTransaction, Func<tbl_InvTransaction, bool> predicate)
        {
            List<tbl_InvTransaction> list = new List<tbl_InvTransaction>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_InvTransaction.Where(predicate).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvTransaction.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_InvTransaction"></param>
        /// <returns></returns>
        public static List<tbl_InvTransaction> SelectAll(this tbl_InvTransaction tbl_InvTransaction)
        {
            List<tbl_InvTransaction> list = new List<tbl_InvTransaction>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_InvTransaction.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvTransaction.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_InvTransaction"></param>
        /// <returns></returns>
        public static int Insert(this tbl_InvTransaction tbl_InvTransaction)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_InvTransaction.Attach(tbl_InvTransaction);
                    db.tbl_InvTransaction.Add(tbl_InvTransaction);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvTransaction.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_InvTransaction"></param>
        /// <returns></returns>
        public static int Update(this tbl_InvTransaction tbl_InvTransaction)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_InvTransaction.FirstOrDefault(x => x.TransactionID == tbl_InvTransaction.TransactionID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_InvTransactionItem in tbl_InvTransaction.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "transactonid" && updateDataItem.Name == tbl_InvTransactionItem.Name)
                                {
                                    var value = tbl_InvTransactionItem.GetValue(tbl_InvTransaction, null);

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
                        ret = tbl_InvTransaction.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvTransaction.GetType());
            }

            return ret;
        }


        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_InvTransaction"></param>
        /// <returns></returns>
        public static int Delete(this tbl_InvTransaction tbl_InvTransaction)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_InvTransaction).State = EntityState.Deleted;
                    //db.tbl_InvTransaction.Attach(tbl_InvTransaction);
                    db.tbl_InvTransaction.Remove(tbl_InvTransaction);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvTransaction.GetType());
            }

            return ret;
        }
    }
}