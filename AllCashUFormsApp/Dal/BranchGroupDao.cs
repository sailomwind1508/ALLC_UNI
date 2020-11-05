using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class BranchGroupDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_BranchGroup"></param>
        /// <returns></returns>
        public static List<tbl_BranchGroup> Select(this tbl_BranchGroup tbl_BranchGroup, Func<tbl_BranchGroup, bool> predicate)
        {
            List<tbl_BranchGroup> list = new List<tbl_BranchGroup>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_BranchGroup.Where(x => x.FlagDel == false).Where(predicate).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchGroup.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_BranchGroup"></param>
        /// <returns></returns>
        public static List<tbl_BranchGroup> SelectAll(this tbl_BranchGroup tbl_BranchGroup)
        {
            List<tbl_BranchGroup> list = new List<tbl_BranchGroup>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_BranchGroup.Where(x => x.FlagDel == false).OrderBy(x => x.BranchGroupCode).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchGroup.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_BranchGroup"></param>
        /// <returns></returns>
        public static int Insert(this tbl_BranchGroup tbl_BranchGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_BranchGroup.Attach(tbl_BranchGroup);
                    db.tbl_BranchGroup.Add(tbl_BranchGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_BranchGroup"></param>
        /// <returns></returns>
        public static int Update(this tbl_BranchGroup tbl_BranchGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_BranchGroup.FirstOrDefault(x => x.BranchGroupID == tbl_BranchGroup.BranchGroupID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_BranchGroupItem in tbl_BranchGroup.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_BranchGroupItem.Name)
                                {
                                    var value = tbl_BranchGroupItem.GetValue(tbl_BranchGroup, null);

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
                ex.WriteLog(tbl_BranchGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_BranchGroup"></param>
        /// <returns></returns>
        public static int Delete(this tbl_BranchGroup tbl_BranchGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_BranchGroup).State = EntityState.Deleted;
                    db.tbl_BranchGroup.Remove(tbl_BranchGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_BranchGroup.GetType());
            }

            return ret;
        }
    }
}
