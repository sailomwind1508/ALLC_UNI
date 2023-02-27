using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ShopTypeDao
    {
        public static DataTable GetShopTypeGirdData(this tbl_ShopType tbl_ShopType, int flagDel, string text)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_ShopType WHERE FlagDel = " + flagDel + "";
              
                if (!string.IsNullOrEmpty(text))
                {
                    sql += " AND cast(ShopTypeID as nvarchar(50)) like '%" + text + "%'";
                    sql += " OR ShopTypeName like '%" + text + "%'";
                }

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<tbl_ShopType> Select(this tbl_ShopType tbl_ShopType, Func<tbl_ShopType, bool> predicate)
        {
            List<tbl_ShopType> list = new List<tbl_ShopType>();
            try
            {
                list = tbl_ShopType.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ShopType.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ShopType.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ShopType"></param>
        /// <returns></returns>
        public static List<tbl_ShopType> SelectAll(this tbl_ShopType tbl_ShopType)
        {
            List<tbl_ShopType> list = new List<tbl_ShopType>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_ShopType] WHERE FlagDel = 0 Order By ShopTypeID";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ShopType), sql);
                list = dynamicListReturned.Cast<tbl_ShopType>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ShopType.Where(x => x.FlagDel == false).OrderBy(x => x.ShopTypeID).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ShopType.GetType());
            }

            return list;
        }

        public static List<tbl_ShopType> SelectFlag(this tbl_ShopType tbl_ShopType, Func<tbl_ShopType, bool> predicate)//
        {
            List<tbl_ShopType> list = new List<tbl_ShopType>();
            try
            {
                list = tbl_ShopType.SelectAllFlag().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ShopType.GetType());
            }

            return list;
        }

        public static List<tbl_ShopType> SelectAllFlag(this tbl_ShopType tbl_ShopType)//
        {
            List<tbl_ShopType> list = new List<tbl_ShopType>();
            try
            {
                string sql = "SELECT * FROM tbl_ShopType";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ShopType), sql);
                list = dynamicListReturned.Cast<tbl_ShopType>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ShopType.GetType());
            }

            return list;
        }

        public static List<tbl_ShopType> SelectAllFlagPredi(this tbl_ShopType tbl_ShopType, Func<tbl_ShopType, bool> predicate)
        {
            List<tbl_ShopType> list = new List<tbl_ShopType>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ShopType.Where(predicate).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ShopType.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ShopType"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ShopType tbl_ShopType)
        {
            string msg = "start ShopTypeDao=>Insert";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ShopType.Attach(tbl_ShopType);
                    db.tbl_ShopType.Add(tbl_ShopType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ShopType.GetType());
            }

            msg = "end ShopTypeDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        public static void Insert(this tbl_ShopType tbl_ShopType, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start ShopTypeDao=>InsertWithDB";
            msg.WriteLog(null);

            try
            {
                db.tbl_ShopType.Attach(tbl_ShopType);
                db.tbl_ShopType.Add(tbl_ShopType);
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }

            msg = "end ShopTypeDao=>InsertWithDB";
            msg.WriteLog(null);
        }

        public static int UpdateEntity(this tbl_ShopType tbl_ShopType, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start ShopTypeDao=>UpdateEntity";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                var updateData = db.tbl_ShopType.FirstOrDefault(x => x.ShopTypeID == tbl_ShopType.ShopTypeID);
                if (updateData != null)
                {
                    foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                    {
                        foreach (PropertyInfo tbl_ShopTypeItem in tbl_ShopType.GetType().GetProperties())
                        {
                            if (updateDataItem.Name == tbl_ShopTypeItem.Name)
                            {
                                var value = tbl_ShopTypeItem.GetValue(tbl_ShopType, null);

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
                    tbl_ShopType.Insert(db);
                }

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            msg = "end ShopTypeDao=>UpdateEntity";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ShopType"></param>
        /// <returns></returns>
        public static int Update(this tbl_ShopType tbl_ShopType)
        {
            string msg = "start ShopTypeDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ShopType.FirstOrDefault(x => x.ShopTypeID == tbl_ShopType.ShopTypeID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ShopTypeItem in tbl_ShopType.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ShopTypeItem.Name)
                                {
                                    var value = tbl_ShopTypeItem.GetValue(tbl_ShopType, null);

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
                        ret = tbl_ShopType.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ShopType.GetType());
            }

            msg = "end ShopTypeDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ShopType"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ShopType tbl_ShopType)
        {
            string msg = "start ShopTypeDao=>Delete";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ShopType).State = EntityState.Deleted;
                    db.tbl_ShopType.Remove(tbl_ShopType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ShopType.GetType());
            }

            msg = "end ShopTypeDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }
    }
}
