using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class SalAreaDistrictDao
    {
        //public static DataTable GetSalAreaByWHID(this tbl_SalAreaDistrict tbl_SalAreaDistrict, int flagDel)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        string sql = " ";
        //        sql += "select t1.WHID,t1.SalAreaID,t2.SalAreaName,CrDate,CrUser,EdDate,EdUser,FlagDel from tbl_SalAreaDistrict AS t1 left join tbl_SalArea as t2 on t1.SalAreaID = t2.SalAreaID";
        //        if (frmCustomerInfo._WHID != null)
        //        {
        //            sql += " where WHID='" + frmCustomerInfo._WHID + "'" + " AND FlagDel=" + flagDel + "";
        //        }
        //        else
        //        {
        //            sql += " where FlagDel=" + flagDel + "";
        //        }
        //        SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
        //        da.Fill(ds, "area");
        //        return ds.Tables["area"];

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.WriteLog(tbl_SalAreaDistrict.GetType());
        //        return null;
        //    }

        //}


        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static List<tbl_SalAreaDistrict> Select(this tbl_SalAreaDistrict tbl_SalAreaDistrict, Func<tbl_SalAreaDistrict, bool> predicate)
        {
            List<tbl_SalAreaDistrict> list = new List<tbl_SalAreaDistrict>();
            try
            {
                list = tbl_SalAreaDistrict.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_SalAreaDistrict.Where(predicate).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static List<tbl_SalAreaDistrict> SelectAll(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            List<tbl_SalAreaDistrict> list = new List<tbl_SalAreaDistrict>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_SalAreaDistrict] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_SalAreaDistrict), sql);
                list = dynamicListReturned.Cast<tbl_SalAreaDistrict>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_SalAreaDistrict>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_SalAreaDistrict.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static int Insert(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_SalAreaDistrict.Attach(tbl_SalAreaDistrict);
                    db.tbl_SalAreaDistrict.Add(tbl_SalAreaDistrict);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static int Update(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_SalAreaDistrict.FirstOrDefault(x => x.SalAreaID == tbl_SalAreaDistrict.SalAreaID && x.DistrictID == tbl_SalAreaDistrict.DistrictID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_SalAreaDistrictItem in tbl_SalAreaDistrict.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_SalAreaDistrictItem.Name)
                                {
                                    var value = tbl_SalAreaDistrictItem.GetValue(tbl_SalAreaDistrict, null);

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
                        ret = tbl_SalAreaDistrict.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_SalAreaDistrict"></param>
        /// <returns></returns>
        public static int Delete(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_SalAreaDistrict).State = EntityState.Deleted;
                    db.tbl_SalAreaDistrict.Remove(tbl_SalAreaDistrict);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SalAreaDistrict.GetType());
            }

            return ret;
        }
        public static DataTable GetDataTable(this tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            try
            {
                DataTable newTable = new DataTable("DistrictTable");

                string sql = "proc_SalAreaDistrict_GetDataTable";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                return newTable;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
