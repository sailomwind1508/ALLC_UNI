using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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

        public static int UpdateShelf(this tbl_ArCustomerShelf tbl_ArCustomerShelf)
        {
            string msg = "start ArCustomerShelfDao=>UpdateShelf";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ArCustomerShelf.FirstOrDefault(x => x.CustomerID == tbl_ArCustomerShelf.CustomerID && x.ShelfID == tbl_ArCustomerShelf.ShelfID);
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

            msg = "end ArCustomerShelfDao=>UpdateShelf";
            msg.WriteLog(null);

            return ret;
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

        public static DataTable GetCustomerShelfData(this tbl_ArCustomerShelf tbl_ArCustomerShelf, string Search, int flagDel, string WHID)
        {
            DataTable dt = new DataTable("CustomerShelf");
            try
            {
                string sql = @"SELECT t1.CustomerID
                            , t2.CustName
                            , ISNULL(t1.ImagePath, '') AS 'ImagePath'
                            , ISNULL(t1.ProductID, '') AS 'ProductID'
                            , ISNULL(t3.ProductName, '') AS 'ProductName'
                            , t1.ShelfID
                            , t1.WHID
                            , t4.WHName
                            , t1.AutoID
                            , t1.FlagDel
                            FROM tbl_ArCustomerShelf t1
                            left join tbl_ArCustomer t2 ON t1.CustomerID = t2.CustomerID
                            left join tbl_Product t3 ON t1.ProductID = t3.ProductID
                            left join tbl_BranchWarehouse t4 ON t1.WHID = t4.WHID";

                sql += " WHERE 1=1";

                if (!string.IsNullOrEmpty(WHID))
                    sql += " AND t1.WHID = '" + WHID + "'";

                if (!string.IsNullOrEmpty(Search))
                    sql += " AND (t1.CustomerID like '%" + @Search + "%' OR t2.CustName like '%" + @Search + "%' OR t1.ShelfID like '%" + @Search + "%')";

                sql += " AND t1.FlagDel = " + flagDel;

                sql += " order by t1.WHID, t1.CustomerID"; 

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerShelf.GetType());
                ex.Message.ShowErrorMessage();
            }

            return dt;
        }

        public static int UpdateCustomerShelf(this tbl_ArCustomerShelf tbl_ArCustomerShelf)
        {
            int ret = 0;
            SqlConnection con = new SqlConnection(Connection.ConnectionString);
            try
            {
                string sql = "UPDATE tbl_ArCustomerShelf";
                sql += " SET ShelfID = @ShelfID, EdDate = @EdDate, EdUser = @EdUser, ImagePath = @ImagePath";
                sql += " WHERE AutoID = @AutoID";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.AddWithValue("@ShelfID", tbl_ArCustomerShelf.ShelfID);
                cmd.Parameters.AddWithValue("@AutoID", tbl_ArCustomerShelf.AutoID);
                cmd.Parameters.AddWithValue("@ImagePath", tbl_ArCustomerShelf.ImagePath);
                cmd.Parameters.AddWithValue("@EdDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@EdUser", Helper.tbl_Users.Username);

                ret = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerShelf.GetType());
            }

            return ret;
        }

        public static DataTable GetShelfData(this tbl_ArCustomerShelf tbl_ArCustomerShelf, Dictionary<string, object> _params)
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_GetShelfManagement";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return dt;
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerShelf.GetType());
                return null;
            }
        }

        public static int UpdateShelfManagement(this tbl_ArCustomerShelf tbl_ArCustomerShelf, Dictionary<string, object> _params)
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_Update_ShelfManagement";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomerShelf.GetType());
                return 0;
            }
        }
    }
}