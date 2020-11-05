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
    public static class PRMasterDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PRMaster"></param>
        /// <returns></returns>
        public static List<tbl_PRMaster> Select(this tbl_PRMaster tbl_PRMaster, Func<tbl_PRMaster, bool> predicate)
        {
            List<tbl_PRMaster> list = new List<tbl_PRMaster>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_PRMaster.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_PRMaster"></param>
        /// <returns></returns>
        public static List<tbl_PRMaster> SelectAll(this tbl_PRMaster tbl_PRMaster)
        {
            List<tbl_PRMaster> list = new List<tbl_PRMaster>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_PRMaster.OrderBy(x => x.DocNo).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PRMaster"></param>
        /// <returns></returns>
        public static int Insert(this tbl_PRMaster tbl_PRMaster)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_PRMaster.Attach(tbl_PRMaster);
                    db.tbl_PRMaster.Add(tbl_PRMaster);
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
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_PRMaster"></param>
        /// <returns></returns>
        public static int Update(this tbl_PRMaster tbl_PRMaster)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_PRMaster.FirstOrDefault(x => x.DocNo == tbl_PRMaster.DocNo);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PRMasterItem in tbl_PRMaster.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_PRMasterItem.Name)
                                {
                                    var value = tbl_PRMasterItem.GetValue(tbl_PRMaster, null);

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
                        ret = tbl_PRMaster.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PRMaster"></param>
        /// <returns></returns>
        public static int Delete(this tbl_PRMaster tbl_PRMaster)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_PRMaster).State = EntityState.Deleted;
                    db.tbl_PRMaster.Remove(tbl_PRMaster);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PRMaster.GetType());
            }

            return ret;
        }
    }
}
