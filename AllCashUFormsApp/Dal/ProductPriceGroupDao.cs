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
    public static class ProductPriceGroupDao
    {
        private static List<tbl_ProductPriceGroup> tbl_ProductPriceGroups = new List<tbl_ProductPriceGroup>();

        public static List<tbl_ProductPriceGroup> SelectAllEX(this tbl_ProductPriceGroup tbl_ProductPriceGroup)
        {
            List<tbl_ProductPriceGroup> list = new List<tbl_ProductPriceGroup>();
            try
            {
                if (tbl_ProductPriceGroups.Count == 0)
                {
                    DataTable dt = new DataTable();
                    string sql = @"SELECT [PriceGroupID] 
                    ,[ProductID] 
                    ,[ProductUomID] 
                    ,[SellPrice] 
                    ,[BuyPrice] 
                    ,[SellPriceVat] 
                    ,[BuyPriceVat] 
                    FROM [dbo].[tbl_ProductPriceGroup] ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductPriceGroup), sql);
                    list = dynamicListReturned.Cast<tbl_ProductPriceGroup>().ToList();

                    tbl_ProductPriceGroups = list;
                }
                else
                {
                    list = tbl_ProductPriceGroups;
                }
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ProductPriceGroup.GetType());
            }
            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ProductPriceGroup"></param>
        /// <returns></returns>
        public static List<tbl_ProductPriceGroup> Select(this tbl_ProductPriceGroup tbl_ProductPriceGroup, string prodId)
        {
            List<tbl_ProductPriceGroup> list = new List<tbl_ProductPriceGroup>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT [ProductID], [ProductUomID] ";
                sql += " FROM [dbo].[tbl_ProductPriceGroup] WHERE FlagDel = 0 AND [ProductID] = '" + prodId + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductPriceGroup), sql);
                list = dynamicListReturned.Cast<tbl_ProductPriceGroup>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_ProductPriceGroup>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductPriceGroup.Where(x => x.FlagDel == false).ToList();
                //}
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ProductPriceGroup.GetType());
            }

            return list;
        }

        public static List<tbl_ProductPriceGroup> Select(this tbl_ProductPriceGroup tbl_ProductPriceGroup, Func<tbl_ProductPriceGroup, bool> predicate)
        {
            List<tbl_ProductPriceGroup> list = new List<tbl_ProductPriceGroup>();
            try
            {
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductPriceGroup.Where(x => x.FlagDel == false).Where(predicate).OrderBy(x => x.ProductUomID).AsQueryable().ToList();
                //}

                list = tbl_ProductPriceGroups.Where(x => x.FlagDel == false).Where(predicate).AsQueryable().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductPriceGroup.GetType());
            }

            return list;
        }
        
        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ProductPriceGroup"></param>
        /// <returns></returns>
        public static List<tbl_ProductPriceGroup> SelectAll(this tbl_ProductPriceGroup tbl_ProductPriceGroup)
        {
            List<tbl_ProductPriceGroup> list = new List<tbl_ProductPriceGroup>();
            try
            {
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductPriceGroup.Where(x => x.FlagDel == false).OrderBy(x => x.ProductUomID).ToList();
                //}

                VerifyNewData();

                if (tbl_ProductPriceGroups.Count == 0)
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * ";
                    sql += "  FROM [dbo].[tbl_ProductPriceGroup] ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductPriceGroup), sql);
                    list = dynamicListReturned.Cast<tbl_ProductPriceGroup>().ToList();

                    //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                    //da.Fill(dt);

                    //list = ConvertHelper.ConvertDataTable<tbl_ProductPriceGroup>(dt);

                    tbl_ProductPriceGroups = list;

                    //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                    //{
                    //    list = db.tbl_ProductPriceGroup.Where(x => x.FlagDel == false).OrderBy(x => x.ProductUomID).ToList();
                    //    tbl_ProductPriceGroups = list;
                    //}
                }
                else
                {
                    list = tbl_ProductPriceGroups;
                }

            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ProductPriceGroup.GetType());
            }

            return list;
        }

        private static void VerifyNewData()
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += "SELECT COUNT(*) AS countPrdUOM FROM tbl_ProductPriceGroup WHERE FlagDel = 0";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                int count = Convert.ToInt32(dt.Rows[0][0]);

                if (count != tbl_ProductPriceGroups.Count)
                {
                    dt = new DataTable();
                    sql = "";
                    sql += " SELECT * ";
                    sql += " FROM tbl_ProductPriceGroup WHERE FlagDel = 0";

                    //da = new SqlDataAdapter(sql, Connection.ConnectionString);
                    //da.Fill(dt);

                    //var list = ConvertHelper.ConvertDataTable<tbl_ProductPriceGroup>(dt);

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductPriceGroup), sql);
                    var list = dynamicListReturned.Cast<tbl_ProductPriceGroup>().ToList();


                    tbl_ProductPriceGroups = list;
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
            }
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ProductPriceGroup"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ProductPriceGroup tbl_ProductPriceGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ProductPriceGroup.Attach(tbl_ProductPriceGroup);
                    db.tbl_ProductPriceGroup.Add(tbl_ProductPriceGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductPriceGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ProductPriceGroup"></param>
        /// <returns></returns>
        public static int Update(this tbl_ProductPriceGroup tbl_ProductPriceGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ProductPriceGroup.FirstOrDefault(x => x.PriceGroupID == tbl_ProductPriceGroup.PriceGroupID
                    && x.ProductID == tbl_ProductPriceGroup.ProductID
                    && x.ProductUomID == tbl_ProductPriceGroup.ProductUomID);

                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ProductPriceGroupItem in tbl_ProductPriceGroup.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ProductPriceGroupItem.Name)
                                {
                                    var value = tbl_ProductPriceGroupItem.GetValue(tbl_ProductPriceGroup, null);

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
                        ret = tbl_ProductPriceGroup.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductPriceGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ProductPriceGroup"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ProductPriceGroup tbl_ProductPriceGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ProductPriceGroup).State = EntityState.Deleted;
                    db.tbl_ProductPriceGroup.Remove(tbl_ProductPriceGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductPriceGroup.GetType());
            }

            return ret;
        }

        public static bool CallSendAllProductPriceGroupData(this tbl_ProductPriceGroup tbl_ProductPriceGroup, Dictionary<string, object> _params)//
        {
            bool ret = false;
            try
            {
                string sql = "proc_SendAllPrdPriceTable_SendDataToBranch";
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
                ex.WriteLog(null);
                ret = false;
            }

            return ret;
        }

        public static DataTable SelectSingle(this tbl_ProductPriceGroup tbl_ProductPriceGroup, string ProductID, int PriceGroupID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_ProductPriceGroup WHERE 1=1 ";

                if (!string.IsNullOrEmpty(ProductID))
                    sql += " AND ProductID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + ProductID + "', ',')) ";

                if (PriceGroupID != 0)
                    sql += " AND PriceGroupID = " + PriceGroupID;

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductPriceGroup.GetType());
            }

            return dt;
        }

        public static int Remove_ProductPriceGroup(this tbl_ProductPriceGroup tbl_ProductPriceGroup, string ProductID, int PriceGroupID)
        {
            int ret = 0;
            try
            {
                string sql = "";

                if (!string.IsNullOrEmpty(ProductID))
                {
                    sql += "DELETE FROM tbl_ProductPriceGroup";
                    sql += " WHERE PriceGroupID = " + PriceGroupID;
                    sql += " AND ProductID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + ProductID + "', ','))";
                    My_DataTable_Extensions.ExecuteSQL(CommandType.Text, sql);
                }

                ret = 1;
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(tbl_ProductPriceGroup.GetType());
            }

            return ret;
        }

        public static int UpdateDataList(this List<tbl_ProductPriceGroup> list)
        {
            string msg = "start ProductPriceGroupDao=>UpdateDataList";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var item in list)
                    {
                        var updateData = db.tbl_ProductPriceGroup.FirstOrDefault(x => x.PriceGroupID == item.PriceGroupID
                        && x.ProductID == item.ProductID
                        && x.ProductUomID == item.ProductUomID);

                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_ProductPriceGroupItem in list.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name == tbl_ProductPriceGroupItem.Name)
                                    {
                                        var value = tbl_ProductPriceGroupItem.GetValue(list, null);

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
                            db.tbl_ProductPriceGroup.Attach(item);
                            db.tbl_ProductPriceGroup.Add(item);
                        }
                    }

                    db.SaveChanges();
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                ret = 0;
                ex.WriteLog(tbl_ProductPriceGroups.GetType());
            }

            msg = "end ProductPriceGroupDao=>UpdateDataList";
            msg.WriteLog(null);

            return ret;
        }
    }
}