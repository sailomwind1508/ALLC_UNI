using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AllCashUFormsApp
{
    public static class PriceGroupDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PriceGroup"></param>
        /// <returns></returns>
        public static DataTable GetPriceGroupDataGridView(this tbl_PriceGroup tbl_PriceGroup, string search, int flagDel)
        {
            try
            {
                string sql = "SELECT * FROM tbl_PriceGroup WHERE FlagDel = " + flagDel + "";

                if (!string.IsNullOrEmpty(search))
                {
                    sql += " AND PriceGroupCode like '%" + search + "%'";
                    sql += " OR PriceGroupName like '%" + search + "%'";
                }

                DataTable dt = new DataTable("PriceGroup");

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                return dt;
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PriceGroup.GetType());
                return null;
            }
        }
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_PriceGroup"></param>
        /// <returns></returns>
        public static List<tbl_PriceGroup> Select(this tbl_PriceGroup tbl_PriceGroup, Func<tbl_PriceGroup, bool> predicate)
        {
            List<tbl_PriceGroup> list = new List<tbl_PriceGroup>();
            try
            {
                list = tbl_PriceGroup.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PriceGroup.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PriceGroup.GetType());
            }

            return list;
        }
        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_PriceGroup"></param>
        /// <returns></returns>
        public static List<tbl_PriceGroup> SelectAll(this tbl_PriceGroup tbl_PriceGroup)
        {
            List<tbl_PriceGroup> list = new List<tbl_PriceGroup>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_PriceGroup] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_PriceGroup), sql);
                list = dynamicListReturned.Cast<tbl_PriceGroup>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_PriceGroup.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PriceGroup.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_PriceGroup"></param>
        /// <returns></returns>
        public static int Insert(this tbl_PriceGroup tbl_PriceGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_PriceGroup.Attach(tbl_PriceGroup);
                    db.tbl_PriceGroup.Add(tbl_PriceGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PriceGroup.GetType());
            }

            return ret;
        }

        public static void Insert(this tbl_PriceGroup tbl_PriceGroup, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.tbl_PriceGroup.Attach(tbl_PriceGroup);
                db.tbl_PriceGroup.Add(tbl_PriceGroup);

            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }
        }

        public static int UpdateEntity(this tbl_PriceGroup tbl_PriceGroup, DB_ALL_CASH_UNIEntities db)
        {
            int ret = 0;
            try
            {
                var updateData = db.tbl_PriceGroup.FirstOrDefault(x => x.PriceGroupID == tbl_PriceGroup.PriceGroupID);
                if (updateData != null)
                {
                    foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                    {
                        foreach (PropertyInfo tbl_PriceGroupItem in tbl_PriceGroup.GetType().GetProperties())
                        {
                            if (updateDataItem.Name == tbl_PriceGroupItem.Name)
                            {
                                var value = tbl_PriceGroupItem.GetValue(tbl_PriceGroup, null);

                                Type t = Nullable.GetUnderlyingType(updateDataItem.PropertyType) ?? updateDataItem.PropertyType;
                                object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                                updateDataItem.SetValue(updateData, safeValue, null);
                            }
                        }
                    }

                    db.Entry(updateData).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    tbl_PriceGroup.Insert(db);
                }

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_PriceGroup"></param>
        /// <returns></returns>
        public static int Update(this tbl_PriceGroup tbl_PriceGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_PriceGroup.FirstOrDefault(x => x.PriceGroupID == tbl_PriceGroup.PriceGroupID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_PriceGroupItem in tbl_PriceGroup.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_PriceGroupItem.Name)
                                {
                                    var value = tbl_PriceGroupItem.GetValue(tbl_PriceGroup, null);

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
                        ret = tbl_PriceGroup.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PriceGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_PriceGroup"></param>
        /// <returns></returns>
        public static int Delete(this tbl_PriceGroup tbl_PriceGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_PriceGroup).State = EntityState.Deleted;
                    db.tbl_PriceGroup.Remove(tbl_PriceGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_PriceGroup.GetType());
            }

            return ret;
        }
    }
}
