using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class IVDetailDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_IVDetail"></param>
        /// <returns></returns>
        public static List<tbl_IVDetail> Select(this tbl_IVDetail tbl_IVDetail, Func<tbl_IVDetail, bool> predicate)
        {
            List<tbl_IVDetail> list = new List<tbl_IVDetail>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_IVDetail.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_IVDetail"></param>
        /// <returns></returns>
        public static List<tbl_IVDetail> SelectAll(this tbl_IVDetail tbl_IVDetail)
        {
            List<tbl_IVDetail> list = new List<tbl_IVDetail>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_IVDetail.OrderBy(x => x.DocNo).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_IVDetail"></param>
        /// <returns></returns>
        public static int Insert(this tbl_IVDetail tbl_IVDetail)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_IVDetail.Attach(tbl_IVDetail);
                    db.tbl_IVDetail.Add(tbl_IVDetail);
                    ret = db.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_IVDetail"></param>
        /// <returns></returns>
        public static int Update(this tbl_IVDetail tbl_IVDetail)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_IVDetail.FirstOrDefault(x => x.DocNo == tbl_IVDetail.DocNo && x.ProductID == tbl_IVDetail.ProductID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_IVDetailItem in tbl_IVDetail.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_IVDetailItem.Name)
                                {
                                    var value = tbl_IVDetailItem.GetValue(tbl_IVDetail, null);

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
                        ret = tbl_IVDetail.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_IVDetail"></param>
        /// <returns></returns>
        public static int Delete(this tbl_IVDetail tbl_IVDetail)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_IVDetail).State = EntityState.Deleted;
                    db.tbl_IVDetail.Remove(tbl_IVDetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVDetail.GetType());
            }

            return ret;
        }
    }
}
