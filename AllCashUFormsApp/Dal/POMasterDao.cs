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
    public static class POMasterDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static List<tbl_POMaster> Select(this tbl_POMaster tbl_POMaster, Func<tbl_POMaster, bool> predicate)
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_POMaster.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static List<tbl_POMaster> SelectAll(this tbl_POMaster tbl_POMaster)
        {
            List<tbl_POMaster> list = new List<tbl_POMaster>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_POMaster.OrderBy(x => x.DocNo).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static int Insert(this tbl_POMaster tbl_POMaster)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_POMaster.Attach(tbl_POMaster);
                    db.tbl_POMaster.Add(tbl_POMaster);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static int Update(this tbl_POMaster tbl_POMaster)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_POMaster.FirstOrDefault(x => x.DocNo == tbl_POMaster.DocNo);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_POMasterItem in tbl_POMaster.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_POMasterItem.Name)
                                {
                                    var value = tbl_POMasterItem.GetValue(tbl_POMaster, null);

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
                        ret = tbl_POMaster.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_POMaster"></param>
        /// <returns></returns>
        public static int Delete(this tbl_POMaster tbl_POMaster)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_POMaster).State = EntityState.Deleted;
                    db.tbl_POMaster.Remove(tbl_POMaster);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_POMaster.GetType());
            }

            return ret;
        }
    }
}
