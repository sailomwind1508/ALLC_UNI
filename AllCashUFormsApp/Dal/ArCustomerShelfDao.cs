using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ArCustomerShelfDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_ArCustomerShelf"></param>
        /// <returns></returns>
        public static List<tbl_ArCustomerShelf> SelectByCustID(this tbl_ArCustomerShelf tbl_ArCustomerShelf, string customerID)
        {
            List<tbl_ArCustomerShelf> list = new List<tbl_ArCustomerShelf>();
            try
            {
                string sql = " SELECT CustomerID, WHID, ProductID, ShelfID FROM [dbo].[tbl_ArCustomerShelf] WHERE FlagDel = 0 AND CustomerID = '" + customerID + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ArCustomerShelf), sql);
                list = dynamicListReturned.Cast<tbl_ArCustomerShelf>().ToList();

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerShelf.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_ArCustomerShelf"></param>
        /// <returns></returns>
        public static List<tbl_ArCustomerShelf> Select(this tbl_ArCustomerShelf tbl_ArCustomerShelf, Func<tbl_ArCustomerShelf, bool> predicate)
        {
            List<tbl_ArCustomerShelf> list = new List<tbl_ArCustomerShelf>();
            try
            {
                list = tbl_ArCustomerShelf.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ArCustomerShelf.Where(predicate).OrderBy(x => x.CustomerID).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerShelf.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ArCustomerShelf"></param>
        /// <returns></returns>
        public static List<tbl_ArCustomerShelf> SelectAll(this tbl_ArCustomerShelf tbl_ArCustomerShelf)
        {
            List<tbl_ArCustomerShelf> list = new List<tbl_ArCustomerShelf>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_ArCustomerShelf] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ArCustomerShelf), sql);
                list = dynamicListReturned.Cast<tbl_ArCustomerShelf>().ToList();


                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ArCustomerShelf.OrderBy(x => x.CustomerID).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerShelf.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ArCustomerShelf"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ArCustomerShelf tbl_ArCustomerShelf)
        {
            string msg = "start ArCustomerShelfDao=>Insert";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ArCustomerShelf.Attach(tbl_ArCustomerShelf);
                    db.tbl_ArCustomerShelf.Add(tbl_ArCustomerShelf);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerShelf.GetType());
            }

            msg = "end ArCustomerShelfDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        public static void Insert(this tbl_ArCustomerShelf tbl_ArCustomerShelf, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start ArCustomerShelfDao=>InsertWithDB";
            msg.WriteLog(null);

            try
            {

                db.tbl_ArCustomerShelf.Attach(tbl_ArCustomerShelf);
                db.tbl_ArCustomerShelf.Add(tbl_ArCustomerShelf);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerShelf.GetType());
            }

            msg = "end ArCustomerShelfDao=>InsertWithDB";
            msg.WriteLog(null);
        }

        public static int UpdateEntity(this tbl_ArCustomerShelf tbl_ArCustomerShelf, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start ArCustomerShelfDao=>UpdateEntity";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                var updateData = db.tbl_ArCustomerShelf.FirstOrDefault(x => x.CustomerID == tbl_ArCustomerShelf.CustomerID && x.ShelfID == tbl_ArCustomerShelf.ShelfID && x.FlagDel == false);
                if (updateData != null)
                {
                    foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                    {
                        foreach (PropertyInfo tbl_ArCustomerShelfItem in tbl_ArCustomerShelf.GetType().GetProperties())
                        {
                            if (updateDataItem.Name == tbl_ArCustomerShelfItem.Name)
                            {
                                var value = tbl_ArCustomerShelfItem.GetValue(tbl_ArCustomerShelf, null);

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
                    tbl_ArCustomerShelf.Insert(db);
                }

                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            msg = "end ArCustomerShelfDao=>UpdateEntity";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ArCustomerShelf"></param>
        /// <returns></returns>
        public static int Update(this tbl_ArCustomerShelf tbl_ArCustomerShelf)
        {
            string msg = "start ArCustomerShelfDao=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ArCustomerShelf.FirstOrDefault(x => x.CustomerID == tbl_ArCustomerShelf.CustomerID && x.ShelfID == tbl_ArCustomerShelf.ShelfID && x.FlagDel == false);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ArCustomerShelfItem in tbl_ArCustomerShelf.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ArCustomerShelfItem.Name)
                                {
                                    var value = tbl_ArCustomerShelfItem.GetValue(tbl_ArCustomerShelf, null);

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
                        ret = tbl_ArCustomerShelf.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerShelf.GetType());
            }

            msg = "end ArCustomerShelfDao=>Update";
            msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ArCustomerShelf"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ArCustomerShelf tbl_ArCustomerShelf)
        {
            string msg = "start ArCustomerShelfDao=>Delete";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ArCustomerShelf).State = EntityState.Deleted;
                    db.tbl_ArCustomerShelf.Remove(tbl_ArCustomerShelf);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerShelf.GetType());
            }

            msg = "end ArCustomerShelfDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }
    }
}