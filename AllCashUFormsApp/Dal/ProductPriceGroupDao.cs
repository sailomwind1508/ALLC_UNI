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
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_ProductPriceGroup"></param>
        /// <returns></returns>
        public static List<tbl_ProductPriceGroup> Select(this tbl_ProductPriceGroup tbl_ProductPriceGroup, Func<tbl_ProductPriceGroup, bool> predicate)
        {
            List<tbl_ProductPriceGroup> list = new List<tbl_ProductPriceGroup>();
            try
            {
                list = tbl_ProductPriceGroup.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductPriceGroup.Where(x => x.FlagDel == false).Where(predicate).AsQueryable().ToList();
                //}
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
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_ProductPriceGroup] WHERE FlagDel = 0 ";

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
                ret = false;
            }

            return ret;
        }
    }
}