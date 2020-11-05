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
    public static class DocumentStatusDao
    {
        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_DocumentStatus"></param>
        /// <returns></returns>
        public static List<tbl_DocumentStatus> SelectAll(this tbl_DocumentStatus tbl_DocumentStatus)
        {
            List<tbl_DocumentStatus> list = new List<tbl_DocumentStatus>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_DocumentStatus.OrderBy(x => x.DocStatusCode).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocumentStatus.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_DocumentStatus"></param>
        /// <returns></returns>
        public static int Insert(this tbl_DocumentStatus tbl_DocumentStatus)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_DocumentStatus.Attach(tbl_DocumentStatus);
                    db.tbl_DocumentStatus.Add(tbl_DocumentStatus);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocumentStatus.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_DocumentStatus"></param>
        /// <returns></returns>
        public static int Update(this tbl_DocumentStatus tbl_DocumentStatus)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_DocumentStatus.FirstOrDefault(x => x.DocStatusCode == tbl_DocumentStatus.DocStatusCode);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_DocumentStatusItem in tbl_DocumentStatus.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_DocumentStatusItem.Name)
                                {
                                    var value = tbl_DocumentStatusItem.GetValue(tbl_DocumentStatus, null);

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
                ex.WriteLog(tbl_DocumentStatus.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_DocumentStatus"></param>
        /// <returns></returns>
        public static int Delete(this tbl_DocumentStatus tbl_DocumentStatus)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_DocumentStatus).State = EntityState.Deleted;
                    db.tbl_DocumentStatus.Remove(tbl_DocumentStatus);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocumentStatus.GetType());
            }

            return ret;
        }
    }
}
