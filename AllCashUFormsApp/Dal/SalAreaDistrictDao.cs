using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class SalAreaDistrictDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static List<tbl_SalAreaDistrict> Select(this tbl_SalAreaDistrict tbl_SalAreaDistrict, Func<tbl_SalAreaDistrict, bool> predicate)
        {
            List<tbl_SalAreaDistrict> list = new List<tbl_SalAreaDistrict>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_SalAreaDistrict.Where(predicate).AsQueryable().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static List<tbl_SalAreaDistrict> SelectAll(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            List<tbl_SalAreaDistrict> list = new List<tbl_SalAreaDistrict>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_SalAreaDistrict.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static int Insert(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_SalAreaDistrict.Attach(tbl_SalAreaDistrict);
                    db.tbl_SalAreaDistrict.Add(tbl_SalAreaDistrict);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static int Update(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_SalAreaDistrict.FirstOrDefault(x => x.SalAreaID == tbl_SalAreaDistrict.SalAreaID && x.DistrictID == tbl_SalAreaDistrict.DistrictID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_SalAreaDistrictItem in tbl_SalAreaDistrict.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_SalAreaDistrictItem.Name)
                                {
                                    var value = tbl_SalAreaDistrictItem.GetValue(tbl_SalAreaDistrict, null);

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
                        ret = tbl_SalAreaDistrict.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static int Delete(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_SalAreaDistrict).State = EntityState.Deleted;
                    db.tbl_SalAreaDistrict.Remove(tbl_SalAreaDistrict);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return ret;
        }
    }
}
