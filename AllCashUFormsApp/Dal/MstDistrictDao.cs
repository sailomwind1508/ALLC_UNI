﻿using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class MstDistrictDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_MstDistrict"></param>
        /// <returns></returns>
        public static List<tbl_MstDistrict> Select(this tbl_MstDistrict tbl_MstDistrict, Func<tbl_MstDistrict, bool> predicate)
        {
            List<tbl_MstDistrict> list = new List<tbl_MstDistrict>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_MstDistrict.Where(predicate).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstDistrict.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_MstDistrict"></param>
        /// <returns></returns>
        public static List<tbl_MstDistrict> SelectAll(this tbl_MstDistrict tbl_MstDistrict)
        {
            List<tbl_MstDistrict> list = new List<tbl_MstDistrict>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_MstDistrict.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstDistrict.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_MstDistrict"></param>
        /// <returns></returns>
        public static int Insert(this tbl_MstDistrict tbl_MstDistrict)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_MstDistrict.Attach(tbl_MstDistrict);
                    db.tbl_MstDistrict.Add(tbl_MstDistrict);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstDistrict.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_MstDistrict"></param>
        /// <returns></returns>
        public static int Update(this tbl_MstDistrict tbl_MstDistrict)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_MstDistrict.FirstOrDefault(x => x.DistrictID == tbl_MstDistrict.DistrictID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_MstDistrictItem in tbl_MstDistrict.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_MstDistrictItem.Name)
                                {
                                    var value = tbl_MstDistrictItem.GetValue(tbl_MstDistrict, null);

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
                ex.WriteLog(tbl_MstDistrict.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_MstDistrict"></param>
        /// <returns></returns>
        public static int Delete(this tbl_MstDistrict tbl_MstDistrict)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_MstDistrict).State = EntityState.Deleted;
                    db.tbl_MstDistrict.Remove(tbl_MstDistrict);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstDistrict.GetType());
            }

            return ret;
        }
    }
}
