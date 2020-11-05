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
    public static class InvMovementDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static List<tbl_InvMovement> Select(this tbl_InvMovement tbl_InvMovement, Func<tbl_InvMovement, bool> predicate)
        {
            List<tbl_InvMovement> list = new List<tbl_InvMovement>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_InvMovement.Where(predicate).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static List<tbl_InvMovement> SelectAll(this tbl_InvMovement tbl_InvMovement)
        {
            List<tbl_InvMovement> list = new List<tbl_InvMovement>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_InvMovement.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static int Insert(this tbl_InvMovement tbl_InvMovement)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_InvMovement.Attach(tbl_InvMovement);
                    db.tbl_InvMovement.Add(tbl_InvMovement);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static int Update(this tbl_InvMovement tbl_InvMovement)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_InvMovement.FirstOrDefault(x => x.TransactionID == tbl_InvMovement.TransactionID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_InvMovementItem in tbl_InvMovement.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "transactonid" && updateDataItem.Name == tbl_InvMovementItem.Name)
                                {
                                    var value = tbl_InvMovementItem.GetValue(tbl_InvMovement, null);

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
                        ret = tbl_InvMovement.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return ret;
        }


        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_InvMovement"></param>
        /// <returns></returns>
        public static int Delete(this tbl_InvMovement tbl_InvMovement)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_InvMovement).State = EntityState.Deleted;
                    //db.tbl_InvMovement.Attach(tbl_InvMovement);
                    db.tbl_InvMovement.Remove(tbl_InvMovement);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvMovement.GetType());
            }

            return ret;
        }
    }
}
