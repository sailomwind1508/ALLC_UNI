using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class PODetailDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PODetail"></param>
        /// <returns></returns>
        public static List<tbl_PODetail> Select(this tbl_PODetail tbl_PODetail, Func<tbl_PODetail, bool> predicate)
        {
            List<tbl_PODetail> list = new List<tbl_PODetail>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_PODetail.Where(predicate).AsQueryable().ToList();

                    //list = db.tbl_PODetail.Where(predicate).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_PODetail"></param>
        /// <returns></returns>
        public static List<tbl_PODetail> SelectAll(this tbl_PODetail tbl_PODetail)
        {
            List<tbl_PODetail> list = new List<tbl_PODetail>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_PODetail.OrderBy(x => x.DocNo).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PODetail"></param>
        /// <returns></returns>
        public static int Insert(this tbl_PODetail tbl_PODetail)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_PODetail.Attach(tbl_PODetail);
                    db.tbl_PODetail.Add(tbl_PODetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_PODetail"></param>
        /// <returns></returns>
        public static int Update(this tbl_PODetail tbl_PODetail)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_PODetail.FirstOrDefault(x => x.DocNo == tbl_PODetail.DocNo && x.ProductID == tbl_PODetail.ProductID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PODetailItem in tbl_PODetail.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_PODetailItem.Name)
                                {
                                    var value = tbl_PODetailItem.GetValue(tbl_PODetail, null);

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
                        ret = tbl_PODetail.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PODetail"></param>
        /// <returns></returns>
        public static int Delete(this tbl_PODetail tbl_PODetail)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_PODetail).State = EntityState.Deleted;
                    db.tbl_PODetail.Remove(tbl_PODetail);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PODetail.GetType());
            }

            return ret;
        }
    }
}
