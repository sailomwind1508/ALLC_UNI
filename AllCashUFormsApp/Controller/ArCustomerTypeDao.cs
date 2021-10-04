using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp.Dal
{
    public static class ArCustomerTypeDao
    {
        public static List<tbl_ArCustomerType> Select(this tbl_ArCustomerType tbl_ArCustomerType, Func<tbl_ArCustomerType, bool> predicate)
        {
            List<tbl_ArCustomerType> list = new List<tbl_ArCustomerType>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ArCustomerType.Where(predicate).Where(x => x.FlagDel == false).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerType.GetType());
            }

            return list;
        }
        public static List<tbl_ArCustomerType> SelectAll(this tbl_ArCustomerType tbl_ArCustomerType)
        {
            List<tbl_ArCustomerType> list = new List<tbl_ArCustomerType>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ArCustomerType.Where(x => x.FlagDel == false).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerType.GetType());
            }

            return list;
        }
        public static int Insert(this tbl_ArCustomerType tbl_ArCustomerType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ArCustomerType.Attach(tbl_ArCustomerType);
                    db.tbl_ArCustomerType.Add(tbl_ArCustomerType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerType.GetType());
            }

            return ret;
        }
        public static int Update(this tbl_ArCustomerType tbl_ArCustomerType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ArCustomerType.FirstOrDefault(x => x.ArCustomerTypeID == tbl_ArCustomerType.ArCustomerTypeID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ArCustomerTypeItem in tbl_ArCustomerType.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ArCustomerTypeItem.Name)
                                {
                                    var value = tbl_ArCustomerTypeItem.GetValue(tbl_ArCustomerType, null);

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
                ex.WriteLog(tbl_ArCustomerType.GetType());
            }

            return ret;
        }
        public static int Delete(this tbl_ArCustomerType tbl_ArCustomerType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ArCustomerType).State = EntityState.Deleted;
                    db.tbl_ArCustomerType.Remove(tbl_ArCustomerType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerType.GetType());
            }

            return ret;
        }
    }
}
