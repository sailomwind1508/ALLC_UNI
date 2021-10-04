using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class ProductFlavourDao
    {
        public static List<tbl_ProductFlavour> Select(this tbl_ProductFlavour tbl_ProductFlavour, Func<tbl_ProductFlavour, bool> predicate)
        {
            List<tbl_ProductFlavour> list = new List<tbl_ProductFlavour>();
            try
            {
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductFlavour.Where(predicate).OrderBy(x => x.ProductFlavourID).ToList();
                //}
                list = tbl_ProductFlavour.SelectAll().Where(predicate).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return list;
        }
        public static List<tbl_ProductFlavour> SelectAll(this tbl_ProductFlavour tbl_ProductFlavour)
        {
            List<tbl_ProductFlavour> list = new List<tbl_ProductFlavour>();
            try
            {
                string sql = "SELECT * FROM tbl_ProductFlavour ORDER BY ProductFlavourID";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductFlavour), sql);
                list = dynamicListReturned.Cast<tbl_ProductFlavour>().ToList();
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductFlavour.OrderBy(x => x.ProductFlavourID).ToList();
                //}
            }
            catch (Exception)
            {
                return null;
            }

            return list;
        }
        public static int Insert(this tbl_ProductFlavour tbl_ProductFlavour)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ProductFlavour.Attach(tbl_ProductFlavour);
                    db.tbl_ProductFlavour.Add(tbl_ProductFlavour);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductFlavour.GetType());
            }

            return ret;
        }
        public static int Update(this tbl_ProductFlavour tbl_ProductFlavour)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ProductFlavour.FirstOrDefault(x => x.ProductFlavourID == tbl_ProductFlavour.ProductFlavourID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ProductFlavourItem in tbl_ProductFlavour.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ProductFlavourItem.Name)
                                {
                                    var value = tbl_ProductFlavourItem.GetValue(tbl_ProductFlavour, null);

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
                        ret = tbl_ProductFlavour.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductFlavour.GetType());
            }

            return ret;
        }
        public static DataTable GetProductFlavourData(this tbl_ProductFlavour tbl_ProductFlavour, int flagDel, string Search)
        {
            DataTable dt = new DataTable();

            string sql = "SELECT * FROM tbl_ProductFlavour WHERE FlagDel = " + flagDel + "";

            if (!string.IsNullOrEmpty(Search))
            {
                sql += " AND ProductFlavourName like '%" + Search + "%'";
            }

            dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            return dt;
        }
    }
}
