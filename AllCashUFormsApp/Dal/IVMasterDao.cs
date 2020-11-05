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
    public static class IVMasterDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_IVMaster"></param>
        /// <returns></returns>
        public static List<tbl_IVMaster> Select(this tbl_IVMaster tbl_IVMaster, Func<tbl_IVMaster, bool> predicate)
        {
            List<tbl_IVMaster> list = new List<tbl_IVMaster>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_IVMaster.Where(predicate).OrderBy(x => x.DocNo).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_IVMaster"></param>
        /// <returns></returns>
        public static List<tbl_IVMaster> SelectAll(this tbl_IVMaster tbl_IVMaster)
        {
            List<tbl_IVMaster> list = new List<tbl_IVMaster>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_IVMaster.OrderBy(x => x.DocNo).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_IVMaster"></param>
        /// <returns></returns>
        public static int Insert(this tbl_IVMaster tbl_IVMaster)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_IVMaster.Attach(tbl_IVMaster);
                    db.tbl_IVMaster.Add(tbl_IVMaster);
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
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_IVMaster"></param>
        /// <returns></returns>
        public static int Update(this tbl_IVMaster tbl_IVMaster)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_IVMaster.FirstOrDefault(x => x.DocNo == tbl_IVMaster.DocNo);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_IVMasterItem in tbl_IVMaster.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "autoid" && updateDataItem.Name == tbl_IVMasterItem.Name)
                                {
                                    var value = tbl_IVMasterItem.GetValue(tbl_IVMaster, null);

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
                        ret = tbl_IVMaster.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_IVMaster"></param>
        /// <returns></returns>
        public static int Delete(this tbl_IVMaster tbl_IVMaster)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_IVMaster).State = EntityState.Deleted;
                    db.tbl_IVMaster.Remove(tbl_IVMaster);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_IVMaster.GetType());
            }

            return ret;
        }
    }
}
