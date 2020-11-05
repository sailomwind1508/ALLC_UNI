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
    public static class PRDetailDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static List<tbl_PRDetail> Select(this tbl_PRDetail tbl_PRDetail, Func<tbl_PRDetail, bool> predicate)
        {
            List<tbl_PRDetail> list = new List<tbl_PRDetail>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_PRDetail.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static List<tbl_PRDetail> SelectAll(this tbl_PRDetail tbl_PRDetail)
        {
            List<tbl_PRDetail> list = new List<tbl_PRDetail>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_PRDetail.OrderBy(x => x.DocNo).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static int Insert(this tbl_PRDetail tbl_PRDetail)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_PRDetail.Attach(tbl_PRDetail);
                    db.tbl_PRDetail.Add(tbl_PRDetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static int Update(this tbl_PRDetail tbl_PRDetail)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_PRDetail.FirstOrDefault(x => x.DocNo == tbl_PRDetail.DocNo && x.ProductID == tbl_PRDetail.ProductID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PRDetailItem in tbl_PRDetail.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_PRDetailItem.Name)
                                {
                                    var value = tbl_PRDetailItem.GetValue(tbl_PRDetail, null);

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
                        ret = tbl_PRDetail.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PRDetail"></param>
        /// <returns></returns>
        public static int Delete(this tbl_PRDetail tbl_PRDetail)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_PRDetail).State = EntityState.Deleted;
                    db.tbl_PRDetail.Remove(tbl_PRDetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRDetail.GetType());
            }

            return ret;
        }
    }
}
