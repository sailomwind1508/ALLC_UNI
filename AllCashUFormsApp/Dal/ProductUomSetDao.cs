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
    public static class ProductUomSetDao
    {
        private static List<tbl_ProductUomSet> tbl_ProductUomSets = new List<tbl_ProductUomSet>();

        public static List<tbl_ProductUomSet> SelectEntity(this tbl_ProductUomSet tbl_ProductUomSet, Func<tbl_ProductUomSet, bool> predicate)
        {
            List<tbl_ProductUomSet> list = new List<tbl_ProductUomSet>();
            try
            {
                list = tbl_ProductUomSet.SelectAll().Where(predicate).OrderBy(x => x.UomSetID).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductUomSet.Where(predicate).OrderBy(x => x.UomSetID).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUomSet.GetType());
            }

            return list;
        }

        public static List<tbl_ProductUomSet> SelectAllEntity(this tbl_ProductUomSet tbl_ProductUomSet)
        {
            List<tbl_ProductUomSet> list = new List<tbl_ProductUomSet>();
            try
            {
                list = tbl_ProductUomSet.SelectAll().OrderBy(x => x.UomSetID).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductUomSet.OrderBy(x => x.UomSetID).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUomSet.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_ProductUomSet"></param>
        /// <returns></returns>
        public static List<tbl_ProductUomSet> Select(this tbl_ProductUomSet tbl_ProductUomSet, Func<tbl_ProductUomSet, bool> predicate)
        {
            List<tbl_ProductUomSet> list = new List<tbl_ProductUomSet>();
            try
            {
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductUomSet.Where(x => x.FlagDel == false).Where(predicate).OrderBy(x => x.UomSetID).AsQueryable().ToList();
                //}

                list = tbl_ProductUomSets.Where(predicate).OrderBy(x => x.UomSetID).AsQueryable().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUomSet.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ProductUomSet"></param>
        /// <returns></returns>
        public static List<tbl_ProductUomSet> SelectAll(this tbl_ProductUomSet tbl_ProductUomSet)
        {
            List<tbl_ProductUomSet> list = new List<tbl_ProductUomSet>();
            try
            {
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductUomSet.Where(x => x.FlagDel == false).OrderBy(x => x.UomSetID).ToList();
                //}

                VerifyNewData();

                if (tbl_ProductUomSets.Count == 0)
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * ";
                    sql += " FROM [dbo].[tbl_ProductUomSet] WHERE FlagDel = 0 Order By UomSetID ";

                    //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                    //da.Fill(dt);

                    //list = ConvertHelper.ConvertDataTable<tbl_ProductUomSet>(dt);

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductUomSet), sql);
                    list = dynamicListReturned.Cast<tbl_ProductUomSet>().ToList();

                    tbl_ProductUomSets = list;

                    //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                    //{
                    //    list = db.tbl_ProductUomSet.Where(x => x.FlagDel == false).OrderBy(x => x.UomSetID).ToList();
                    //    tbl_ProductUomSets = list;
                    //}
                }
                else
                {
                    list = tbl_ProductUomSets;
                }
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ProductUomSet.GetType());
            }

            return list;
        }

        private static void VerifyNewData()
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += "SELECT COUNT(*) AS countPrdUOM FROM tbl_ProductUomSet WHERE FlagDel = 0";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                int count = Convert.ToInt32(dt.Rows[0][0]);

                if (count != tbl_ProductUomSets.Count)
                {
                    dt = new DataTable();
                    sql = "";
                    sql += " SELECT * ";
                    sql += " FROM tbl_ProductUomSet WHERE FlagDel = 0";

                    //da = new SqlDataAdapter(sql, Connection.ConnectionString);
                    //da.Fill(dt);

                    //var list = ConvertHelper.ConvertDataTable<tbl_ProductUomSet>(dt);

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductUomSet), sql);
                    var list = dynamicListReturned.Cast<tbl_ProductUomSet>().ToList();

                    tbl_ProductUomSets = list;
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
        /// <param name="tbl_ProductUomSet"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ProductUomSet tbl_ProductUomSet)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ProductUomSet.Attach(tbl_ProductUomSet);
                    db.tbl_ProductUomSet.Add(tbl_ProductUomSet);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUomSet.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ProductUomSet"></param>
        /// <returns></returns>
        public static int Update(this tbl_ProductUomSet tbl_ProductUomSet)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ProductUomSet.FirstOrDefault(x => x.ProductID == tbl_ProductUomSet.ProductID && x.UomSetID == tbl_ProductUomSet.UomSetID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ProductUomSetItem in tbl_ProductUomSet.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ProductUomSetItem.Name)
                                {
                                    var value = tbl_ProductUomSetItem.GetValue(tbl_ProductUomSet, null);

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
                        ret = tbl_ProductUomSet.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUomSet.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ProductUomSet"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ProductUomSet tbl_ProductUomSet)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ProductUomSet).State = EntityState.Deleted;
                    db.tbl_ProductUomSet.Remove(tbl_ProductUomSet);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUomSet.GetType());
            }

            return ret;
        }
        public static DataTable GetPrdUomSetData(this tbl_ProductUomSet tbl_ProductUomSet, int ProductGroupID, int ProductSubGroupID, string ProductID, bool flagPrdPriceTable)
        {
            try
            {
                DataTable dt = new DataTable();

                string sql = "SELECT t1.ProductID,t2.ProductName,t2.ProductGroupID,t3.ProductGroupName";
                sql += " ,t2.ProductSubGroupID,t1.UomSetID,t1.UomSetName FROM tbl_ProductUomSet t1";
                sql += " LEFT JOIN tbl_Product t2 on t1.ProductID = t2.ProductID";
                sql += " LEFT JOIN tbl_ProductGroup t3 on t2.ProductGroupID = t3.ProductGroupID";
                sql += " WHERE t2.FlagDel = 0 AND t2.FlagSend = 0 and t3.FlagDel = 0";

                if (flagPrdPriceTable == false)
                {
                    sql += " AND t1.BaseQty = 1";
                }

                sql += " AND " + ProductGroupID + " = CASE WHEN " + ProductGroupID + " <> 0 THEN t2.ProductGroupID ELSE 0 END";
                sql += " AND " + ProductSubGroupID + " = CASE WHEN " + ProductSubGroupID + " <> 0 THEN t2.ProductSubGroupID ELSE 0 END";

                if (!string.IsNullOrEmpty(ProductID)) //where ไม่ให้มีบรรทัด LIKE เมื่อ ProductID เป็นค่า '' ไม่ได้ เลยไม่ใช้ Store
                {
                    sql += " AND t1.ProductID LIKE '%'+'" + ProductID + "'+'%'";
                }

                sql += " ORDER BY t2.ProductGroupID,t2.ProductID,t2.ProductSubGroupID";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                return dt;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                return null;
            }
        }
    }
}
