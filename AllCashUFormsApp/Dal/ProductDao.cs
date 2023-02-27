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
        public static tbl_Product SelectSingle(this tbl_Product tbl_Product, string productID)
        {
            List<tbl_Product> list = new List<tbl_Product>();
            tbl_Product ret = null;
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_Product] WHERE ProductID = '"+ productID + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Product), sql);
                list = dynamicListReturned.Cast<tbl_Product>().ToList();

                if (list.Count > 0)
                {
                    ret = list.First();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
            }

            return ret;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_Product"></param>
        /// <returns></returns>
        public static List<tbl_Product> SelectForMovement(this tbl_Product tbl_Product, string prdGroupID, string prdSubGroupID)
        {
            List<tbl_Product> list = new List<tbl_Product>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_Product] WHERE FlagDel = 0 ";

                if (prdGroupID != "-1")
                    sql += " AND ProductGroupID  = '" + prdGroupID + "' ";
                if (prdSubGroupID != "-1")
                    sql += " AND ProductSubGroupID  = '" + prdSubGroupID + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Product), sql);
                list = dynamicListReturned.Cast<tbl_Product>().ToList();

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
            }

            return list;
        }

        //private static List<tbl_Product> tbl_Products = new List<tbl_Product>();

        public static List<tbl_Product> SelectEntity(this tbl_Product tbl_Product, Func<tbl_Product, bool> predicate)
        {
            var list = new List<tbl_Product>();
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

        public static List<tbl_Product> SelectAllNonFlag(this tbl_Product tbl_Product)
        {
            List<tbl_Product> list = new List<tbl_Product>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_Product] ORDER BY ProductGroupID, ProductSubGroupID, Seq ";

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
            string msg = "start ProductDao=>Insert";
           msg.WriteLog(null);

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

            msg = "end ProductDao=>Insert";
           msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Product"></param>
        /// <returns></returns>
        public static int Update(this tbl_Product tbl_Product)
        {
            string msg = "start ProductDao=>Update";
           msg.WriteLog(null);

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

            msg = "end ProductDao=>Update";
           msg.WriteLog(null);

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Product"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Product tbl_Product)
        {
            string msg = "start ProductDao=>Delete";
           msg.WriteLog(null);

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

            msg = "end ProductDao=>Delete";
           msg.WriteLog(null);

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
                ex.WriteLog(null);
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
                ex.WriteLog(null);
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

        public static DataTable proc_tbl_Product_Data(this tbl_Product tbl_Product, Dictionary<string, object> _params)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_tbl_Product_Data";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql,_params);
                return newTable;

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
                return null;
            }
        
        }

        public static List<tbl_Product> SelectProductList(this tbl_Product tbl_Product, string _ProductID)
        {
            var list = new List<tbl_Product>();
            try
            {
                string sql = "SELECT * FROM tbl_Product";

                if (_ProductID.Length == 8)
                {
                    sql += " WHERE ProductID = '" + _ProductID + "'";
                }
                else
                {
                    sql += " WHERE ProductID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + _ProductID + "', ',')) ";
                }

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Product), sql);
                list = dynamicListReturned.Cast<tbl_Product>().ToList();
                return list;
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
                return null;
            }
        }

        public static List<tbl_Product> SelectSingleProduct(this tbl_Product tbl_Product)
        {
            var list = new List<tbl_Product>();
            try
            {
                string sql = "SELECT TOP 1 * FROM tbl_Product";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Product), sql);
                list = dynamicListReturned.Cast<tbl_Product>().ToList();
                return list;
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
                return null;
            }
        }

        public static DataTable GetProductData_Popup(this tbl_Product tbl_Product, string _ProductSubGroupID, string Search)
        {
            try
            {
                DataTable newTable = new DataTable(); //22-06-2022 adisorn หน้าค้นหาสินค้า ในรายงาน 6.1

                string sql = "SELECT ProductCode, ProductName FROM tbl_Product";
                sql += " WHERE FlagDel = 0";

                if (!string.IsNullOrEmpty(_ProductSubGroupID))
                {
                    sql += " AND ProductSubGroupID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + _ProductSubGroupID + "', ','))";
                }
                if (!string.IsNullOrEmpty(Search))
                {
                    sql += " AND ProductID LIKE '%" + Search + "%' OR ProductName LIKE '%" + Search + "%' ";
                }

                sql += " ORDER BY ProductGroupID, ProductSubGroupID, Seq ";

                newTable = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return newTable;

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
                return null;
            }

        }

        public static DataTable GetProductDataForReport_Popup(this tbl_Product tbl_Product, string _ProductSubGroupID, string Search)
        {
            try
            {
                DataTable newTable = new DataTable(); //22-06-2022 adisorn หน้าค้นหาสินค้า ในรายงาน 6.1

                string sql = "SELECT ProductCode, ProductName FROM tbl_Product";
                sql += " WHERE 1 = 1 ";

                if (!string.IsNullOrEmpty(_ProductSubGroupID))
                {
                    sql += " AND ProductSubGroupID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + _ProductSubGroupID + "', ','))";
                }
                if (!string.IsNullOrEmpty(Search))
                {
                    sql += " AND ProductID LIKE '%" + Search + "%' OR ProductName LIKE '%" + Search + "%' ";
                }

                sql += " ORDER BY ProductGroupID, ProductSubGroupID, Seq ";

                newTable = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return newTable;

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
                return null;
            }

        }

        public static DataTable GetProductDataMovement_Popup(this tbl_Product tbl_Product, string _ProductSubGroupID, string Search)
        {
            try
            {
                DataTable newTable = new DataTable(); //22-06-2022 adisorn หน้าค้นหาสินค้า ในรายงาน 6.1

                string sql = "SELECT ProductCode, ProductName FROM tbl_Product";
                sql += " WHERE FlagDel = 0";

                if (!string.IsNullOrEmpty(_ProductSubGroupID))
                {
                    sql += " AND ProductSubGroupID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + _ProductSubGroupID + "', ','))";
                }
                if (!string.IsNullOrEmpty(Search))
                {
                    sql += " AND ProductID LIKE '%" + Search + "%' OR ProductName LIKE '%" + Search + "%' ";
                }

                sql += " ORDER BY ProductID ";

                newTable = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return newTable;

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
                return null;
            }

        }

        public static DataTable GetProductDataMovementForReport_Popup(this tbl_Product tbl_Product, string _ProductSubGroupID, string Search)
        {
            try
            {
                DataTable newTable = new DataTable(); //22-06-2022 adisorn หน้าค้นหาสินค้า ในรายงาน 6.1

                string sql = "SELECT ProductCode, ProductName FROM tbl_Product";
                sql += " WHERE 1 = 1";

                if (!string.IsNullOrEmpty(_ProductSubGroupID))
                {
                    sql += " AND ProductSubGroupID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + _ProductSubGroupID + "', ','))";
                }
                if (!string.IsNullOrEmpty(Search))
                {
                    sql += " AND ProductID LIKE '%" + Search + "%' OR ProductName LIKE '%" + Search + "%' ";
                }

                sql += " ORDER BY ProductID ";

                newTable = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return newTable;

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Product.GetType());
                return null;
            }

        }        
    }
}
