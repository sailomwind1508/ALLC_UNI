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
    public static class ProductUomDao
    {
        private static List<tbl_ProductUom> tbl_ProductUoms = new List<tbl_ProductUom>();
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_ProductUom"></param>
        /// <returns></returns>
        public static List<tbl_ProductUom> Select(this tbl_ProductUom tbl_ProductUom, Func<tbl_ProductUom, bool> predicate)
        {
            List<tbl_ProductUom> list = new List<tbl_ProductUom>();
            try
            {
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductUom.Where(x => x.FlagDel == false).Where(predicate).OrderBy(x => x.ProductUomID).AsQueryable().ToList();
                //}

                list = tbl_ProductUoms.Where(x => x.FlagDel == false).Where(predicate).OrderBy(x => x.ProductUomID).AsQueryable().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUom.GetType());
            }

            return list;
        }
        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ProductUom"></param>
        /// <returns></returns>
        public static List<tbl_ProductUom> SelectAll(this tbl_ProductUom tbl_ProductUom)
        {
            List<tbl_ProductUom> list = new List<tbl_ProductUom>();
            try
            {
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductUom.Where(x => x.FlagDel == false).OrderBy(x => x.ProductUomID).ToList();
                //}

                VerifyNewData();

                if (tbl_ProductUoms.Count == 0)
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * ";
                    sql += "  FROM [dbo].[tbl_ProductUom] Order By ProductUomID ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductUom), sql);
                    list = dynamicListReturned.Cast<tbl_ProductUom>().ToList();

                    //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                    //da.Fill(dt);

                    //list = ConvertHelper.ConvertDataTable<tbl_ProductUom>(dt);

                    tbl_ProductUoms = list;

                    //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                    //{
                    //    list = db.tbl_ProductUom.Where(x => x.FlagDel == false).OrderBy(x => x.ProductUomID).ToList();
                    //    tbl_ProductUoms = list;
                    //}
                }
                else
                {
                    list = tbl_ProductUoms;
                }

            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ProductUom.GetType());
            }

            return list;
        }
        public static List<tbl_ProductUom> SelectAllNonFlag(this tbl_ProductUom tbl_ProductUom)
        {
            List<tbl_ProductUom> list = new List<tbl_ProductUom>();
            try
            {
                string sql = "SELECT * FROM tbl_ProductUom Order By ProductUomID";
                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductUom), sql);
                list = dynamicListReturned.Cast<tbl_ProductUom>().ToList();
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ProductUom.GetType());
            }
            return list;
        }
        public static List<tbl_ProductUom> SelectNonFlag(this tbl_ProductUom tbl_ProductUom, Func<tbl_ProductUom, bool> predicate)
        {
            List<tbl_ProductUom> list = new List<tbl_ProductUom>();
            try
            {
                list = tbl_ProductUom.SelectAllNonFlag().Where(predicate).OrderBy(x => x.ProductUomID).ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUom.GetType());
            }
            return list;
        }
        private static void VerifyNewData()
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += "SELECT COUNT(*) AS countPrdUOM FROM tbl_ProductUom WHERE FlagDel = 0";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                int count = Convert.ToInt32(dt.Rows[0][0]);

                if (count != tbl_ProductUoms.Count)
                {
                    dt = new DataTable();
                    sql = "";
                    sql += " SELECT * ";
                    sql += " FROM tbl_ProductUom WHERE FlagDel = 0";

                    //da = new SqlDataAdapter(sql, Connection.ConnectionString);
                    //da.Fill(dt);

                    //var list = ConvertHelper.ConvertDataTable<tbl_ProductUom>(dt);

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductUom), sql);
                    var list = dynamicListReturned.Cast<tbl_ProductUom>().ToList();


                    tbl_ProductUoms = list;
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
        /// <param name="tbl_ProductUom"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ProductUom tbl_ProductUom)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ProductUom.Attach(tbl_ProductUom);
                    db.tbl_ProductUom.Add(tbl_ProductUom);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUom.GetType());
            }

            return ret;
        }
        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ProductUom"></param>
        /// <returns></returns>
        public static int Update(this tbl_ProductUom tbl_ProductUom)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ProductUom.FirstOrDefault(x => x.ProductUomID == tbl_ProductUom.ProductUomID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ProductUomItem in tbl_ProductUom.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ProductUomItem.Name)
                                {
                                    var value = tbl_ProductUomItem.GetValue(tbl_ProductUom, null);

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
                        ret = tbl_ProductUom.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUom.GetType());
            }

            return ret;
        }
        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ProductUom"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ProductUom tbl_ProductUom)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ProductUom).State = EntityState.Deleted;
                    db.tbl_ProductUom.Remove(tbl_ProductUom);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductUom.GetType());
            }

            return ret;
        }
        public static DataTable GetProductUomData(this tbl_ProductUom tbl_ProductUom, int flagDel, string Search)
        {
            DataTable dt = new DataTable();

            string sql = "SELECT * FROM tbl_ProductUom WHERE FlagDel = " + flagDel + "";

            if (!string.IsNullOrEmpty(Search))
            {
                sql += " AND ProductUomCode like '%'+'" + Search + "'+'%'";
                sql += " OR ProductUomName like '%'+'" + Search + "'+'%'";
            }

            dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            return dt;
        }
    }
}
