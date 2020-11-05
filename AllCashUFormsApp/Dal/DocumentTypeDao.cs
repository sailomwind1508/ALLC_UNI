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
    public static class DocumentTypeDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_DocumentType"></param>
        /// <returns></returns>
        public static List<tbl_DocumentType> Select(this tbl_DocumentType tbl_DocumentType, Func<tbl_DocumentType, bool> predicate)
        {
            List<tbl_DocumentType> list = new List<tbl_DocumentType>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_DocumentType.Where(x => x.FlagDel == false).Where(predicate).AsQueryable().ToList();

                    //list = db.tbl_DocumentType.Where(predicate).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocumentType.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_DocumentType"></param>
        /// <returns></returns>
        public static List<tbl_DocumentType> SelectAll(this tbl_DocumentType tbl_DocumentType)
        {
            List<tbl_DocumentType> list = new List<tbl_DocumentType>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_DocumentType.Where(x => x.FlagDel == false).OrderBy(x => x.DocTypeCode).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocumentType.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_DocumentType"></param>
        /// <returns></returns>
        public static int Insert(this tbl_DocumentType tbl_DocumentType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_DocumentType.Attach(tbl_DocumentType);
                    db.tbl_DocumentType.Add(tbl_DocumentType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocumentType.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_DocumentType"></param>
        /// <returns></returns>
        public static int Update(this tbl_DocumentType tbl_DocumentType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_DocumentType.FirstOrDefault(x => x.DocTypeCode == tbl_DocumentType.DocTypeCode);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_DocumentTypeItem in tbl_DocumentType.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_DocumentTypeItem.Name)
                                {
                                    var value = tbl_DocumentTypeItem.GetValue(tbl_DocumentType, null);

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
                ex.WriteLog(tbl_DocumentType.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_DocumentType"></param>
        /// <returns></returns>
        public static int Delete(this tbl_DocumentType tbl_DocumentType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_DocumentType).State = EntityState.Deleted;
                    db.tbl_DocumentType.Remove(tbl_DocumentType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DocumentType.GetType());
            }

            return ret;
        }
    }
}
