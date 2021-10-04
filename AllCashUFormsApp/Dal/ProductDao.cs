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
    public static class ProductDao
    {
        //private static List<tbl_Product> tbl_Products = new List<tbl_Product>();

        public static List<tbl_Product> SelectEntity(this tbl_Product tbl_Product, Func<tbl_Product, bool> predicate)
        {
            List<tbl_Product> list = new List<tbl_Product>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_Product] ORDER BY ProductGroupID, ProductSubGroupID, Seq ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Product), sql);
                list = dynamicListReturned.Cast<tbl_Product>().ToList();

                list = list.Where(predicate).OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ThenBy(x => x.Seq).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Product.Where(predicate).OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ThenBy(x => x.Seq).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
            }

            return list;
        }
        
        public static List<tbl_Product> SelectAllEntity(this tbl_Product tbl_Product)
        {
            List<tbl_Product> list = new List<tbl_Product>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_Product] ORDER BY ProductGroupID, ProductSubGroupID, Seq ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Product), sql);
                list = dynamicListReturned.Cast<tbl_Product>().ToList();

                list = list.OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ThenBy(x => x.Seq).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Product.OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ToList();
                //}

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
            }

            return list;
        }
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_Product"></param>
        /// <returns></returns>
        public static List<tbl_Product> Select(this tbl_Product tbl_Product, Func<tbl_Product, bool> predicate)
        {
            List<tbl_Product> list = new List<tbl_Product>();
            try
            {
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Product.Where(x => x.FlagDel == false).Where(predicate).AsQueryable().ToList();
                //}

                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_Product] ORDER BY ProductGroupID, ProductSubGroupID, Seq ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Product), sql);
                list = dynamicListReturned.Cast<tbl_Product>().ToList();

                var tbl_Products = list;

                list = tbl_Products.Where(x => x.FlagDel == false).Where(predicate).OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ThenBy(x => x.Seq).ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
            }

            return list;
        }
        
        public static List<tbl_Product> SelectNonFlagDel(this tbl_Product tbl_Product, Func<tbl_Product, bool> predicate)
        {
            List<tbl_Product> list = new List<tbl_Product>();
            try
            {
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Product.Where(predicate).OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ToList(); 
                //}

                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_Product] ORDER BY ProductGroupID, ProductSubGroupID, Seq  ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Product), sql);
                list = dynamicListReturned.Cast<tbl_Product>().ToList();

                var tbl_Products = list;//.OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ToList();

                list = tbl_Products.Where(predicate).OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ThenBy(x => x.Seq).ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Product"></param>
        /// <returns></returns>
        public static List<tbl_Product> SelectAll(this tbl_Product tbl_Product)
        {
            List<tbl_Product> list = new List<tbl_Product>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_Product] WHERE FlagDel = 0 ORDER BY ProductGroupID, ProductSubGroupID, Seq ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Product), sql);
                list = dynamicListReturned.Cast<tbl_Product>().OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ThenBy(x => x.Seq).ToList();

                //tbl_Products = list;

            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_Product.GetType());
            }

            return list;
        }

        ///// <summary>
        ///// select all data
        ///// </summary>
        ///// <param name="tbl_Product"></param>
        ///// <returns></returns>
        //public static List<tbl_Product> SelectAll(this tbl_Product tbl_Product)
        //{
        //    List<tbl_Product> list = new List<tbl_Product>();
        //    try
        //    {
        //        VerifyNewData();

        //        if (tbl_Products.Count == 0)
        //        {
        //            DataTable dt = new DataTable();
        //            string sql = "";
        //            sql += " SELECT * ";
        //            sql += "  FROM [dbo].[tbl_Product] ";

        //            List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Product), sql);
        //            list = dynamicListReturned.Cast<tbl_Product>().ToList();

        //            //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
        //            //da.Fill(dt);

        //            //list = ConvertHelper.ConvertDataTable<tbl_Product>(dt);

        //            tbl_Products = list;

        //            //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
        //            //{
        //            //    list = db.tbl_Product.Where(x => x.FlagDel == false).ToList();
        //            //    tbl_Products = list;
        //            //}
        //        }
        //        else
        //        {
        //            list = tbl_Products;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        ex.WriteLog(tbl_Product.GetType());
        //    }

        //    return list;
        //}

        //private static void VerifyNewData()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        string sql = "";
        //        sql += "SELECT COUNT(ProductID) AS countPrd FROM tbl_Product WHERE FlagDel = 0";

        //        //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
        //        //da.Fill(dt);

        //        dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

        //        int count = Convert.ToInt32(dt.Rows[0][0]);

        //        if (count != tbl_Products.Count)
        //        {
        //            dt = new DataTable();
        //            sql = "";
        //            sql += " SELECT * ";
        //            sql += " FROM tbl_Product WHERE FlagDel = 0";

        //            //da = new SqlDataAdapter(sql, Connection.ConnectionString);
        //            //da.Fill(dt);

        //            //var list = ConvertHelper.ConvertDataTable<tbl_Product>(dt);

        //            List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Product), sql);
        //            var list = dynamicListReturned.Cast<tbl_Product>().ToList();

        //            tbl_Products = list;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.WriteLog(null);
        //    }
        //}

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_Product"></param>
        /// <returns></returns>
        public static int Insert(this tbl_Product tbl_Product)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_Product.Attach(tbl_Product);
                    db.tbl_Product.Add(tbl_Product);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Product"></param>
        /// <returns></returns>
        public static int Update(this tbl_Product tbl_Product)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_Product.FirstOrDefault(x => x.ProductID == tbl_Product.ProductID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ProductItem in tbl_Product.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ProductItem.Name)
                                {
                                    var value = tbl_ProductItem.GetValue(tbl_Product, null);

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
                        ret = tbl_Product.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Product"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Product tbl_Product)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_Product).State = EntityState.Deleted;
                    db.tbl_Product.Remove(tbl_Product);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
            }

            return ret;
        }
        
        public static DataTable GetDataTable(this tbl_Product tbl_Product)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_Product_GetDataTable";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                return newTable;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static DataTable GetProductView(this tbl_Product tbl_Product,int flagDel,int ProductGroupID,int ProductSubGroupID,string Text)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "SELECT * FROM ProductView as t1 WHERE t1.FlagDel = " + flagDel + "";

                sql += " AND " + ProductGroupID + " = CASE WHEN " + ProductGroupID + " <> 0 THEN t1.ProductGroupID ELSE 0 END";
                sql += " AND " + ProductSubGroupID + " = CASE WHEN " + ProductSubGroupID + " <> 0 THEN t1.ProductSubGroupID ELSE 0 END";

                if (!string.IsNullOrEmpty(Text))
                {
                    sql += " AND (t1.ProductID like '%" + Text + "%'";
                    sql += " OR t1.ProductName like '%" + Text + "%')";
                }

                sql += " ORDER BY t1.ProductGroupID,t1.ProductSubGroupID";

                newTable = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                return newTable;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static DataTable GetSendData_Product(this tbl_Product tbl_Product, Dictionary<string, object> _params)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_SendData_Product";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql,_params);

                return newTable;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static bool CallSendDataProduct(this tbl_Product tbl_Product,Dictionary<string, object> _params)//
        {
            bool ret = false;
            try
            {
                string sql = "proc_SendProductInfo_SendDataToBranch";
                DataTable dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

                if (dt != null && dt.Rows.Count != 0)
                {
                    if (dt.Rows[0]["Result"].ToString() == "1")
                    {
                        ret = true;
                    }
                }

            }
            catch (Exception ex)
            {
                ret = false;
            }

            return ret;
        }
        
        public static DataTable GetProductViewCheck(this tbl_Product tbl_Product,string ProductID)
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM GetProduct WHERE ProductID = '" + ProductID + "'";
                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetProductGroupPriceData(this tbl_Product tbl_Product, Dictionary<string, object> _params)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_GetProductGroupPriceData";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

                return newTable;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
