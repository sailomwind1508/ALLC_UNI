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
    public static class DiscountTypeDao
    {
        private static List<tbl_DiscountType> tbl_DiscountTypes = new List<tbl_DiscountType>();

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_DiscountType"></param>
        /// <returns></returns>
        public static List<tbl_DiscountType> SelectAll(this tbl_DiscountType tbl_DiscountType)
        {
            List<tbl_DiscountType> list = new List<tbl_DiscountType>();
            try
            {
                VerifyNewData();

                if (tbl_DiscountTypes.Count == 0)
                {
                    string sql = "SELECT * FROM tbl_DiscountType";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_DiscountType), sql);
                    list = dynamicListReturned.Cast<tbl_DiscountType>().ToList();

                    //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                    //{
                    //    list = db.tbl_DiscountType.ToList();
                    //}
                }
                else
                {
                    list = tbl_DiscountTypes;
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DiscountType.GetType());
            }

            return list;
        }

        private static void VerifyNewData()
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += "SELECT COUNT(*) AS countDiscountType FROM tbl_DiscountType ";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                int count = Convert.ToInt32(dt.Rows[0][0]);

                if (count != tbl_DiscountTypes.Count)
                {
                    dt = new DataTable();
                    sql = "";
                    sql += " SELECT * ";
                    sql += " FROM tbl_DiscountType ";

                    //da = new SqlDataAdapter(sql, Connection.ConnectionString);
                    //da.Fill(dt);

                    //var list = ConvertHelper.ConvertDataTable<tbl_ProductUomSet>(dt);

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_DiscountType), sql);
                    var list = dynamicListReturned.Cast<tbl_DiscountType>().ToList();

                    tbl_DiscountTypes = list;
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
        /// <param name="tbl_DiscountType"></param>
        /// <returns></returns>
        public static int Insert(this tbl_DiscountType tbl_DiscountType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_DiscountType.Attach(tbl_DiscountType);
                    db.tbl_DiscountType.Add(tbl_DiscountType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DiscountType.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_DiscountType"></param>
        /// <returns></returns>
        public static int Update(this tbl_DiscountType tbl_DiscountType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_DiscountType.FirstOrDefault(x => x.DiscountTypeCode == tbl_DiscountType.DiscountTypeCode);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_DiscountTypeItem in tbl_DiscountType.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_DiscountTypeItem.Name)
                                {
                                    var value = tbl_DiscountTypeItem.GetValue(tbl_DiscountType, null);

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
                ex.WriteLog(tbl_DiscountType.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_DiscountType"></param>
        /// <returns></returns>
        public static int Delete(this tbl_DiscountType tbl_DiscountType)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_DiscountType).State = EntityState.Deleted;
                    db.tbl_DiscountType.Remove(tbl_DiscountType);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_DiscountType.GetType());
            }

            return ret;
        }
    }
}
