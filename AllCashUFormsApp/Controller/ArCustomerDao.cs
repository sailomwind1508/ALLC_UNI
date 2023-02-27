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
    public static class ArCustomerDao
    {
        public static DataTable GetCustomerGirdData(this tbl_ArCustomer tbl_ArCustomer, string whid, string salAreaID, int shopTypeID, int flagDel)
        {
            try
            {

                DataSet ds = new DataSet();
                string sql = " ";

                sql += "SELECT t1.CustomerID as CustomerID ";
                sql += " ,CustomerTypeID ";
                sql += ",t1.CustomerCode as CustomerCode ";
                sql += " ,t1.CustTitle as CustTitle ";
                sql += ",t1.CustName as CustName ";
                sql += ",t1.ShopTypeID as ShopTypeID ";
                sql += ",t2.ShopTypeName as ShopTypeName ";
                sql += " ,t1.SalAreaID as SalAreaID ";
                sql += " ,t3.SalAreaName as SalAreaName ";
                sql += " ,t4.WHID as WHID ";
                sql += " ,t1.Seq as Seq ";
                sql += " ,t4.WHName as WHName ";
                sql += " ,t1.CustomerRefCode as CustomerRefCode ";
                sql += " ,t1.CustomerImg as CustomerImg ";
                sql += " ,t1.CustShortName as CustShortName ";
                sql += " ,t1.Latitude as Latitude ";
                sql += " ,t1.Longitude as Longitude ";
                sql += " ,t1.CrDate as CrDate ";
                sql += " ,t1.BillTo as BillTo ";
                sql += " ,t1.Contact as Contact ";
                sql += " ,t1.Telephone as Telephone ";
                sql += " ,t1.EmpID as EmpID ";
                sql += " ,t5.FirstName as FirstName ";
                sql += " ,t1.CustomerSAPCode as CustomerSAPCode ";
                sql += " ,t1.CreditDay as CreditDay ";
                sql += " ,t6.DistrictCode as DistrictCode ";
                sql += " ,t6.DistrictName as DistrictName ";
                sql += " ,t1.Seq as Seq ";
                sql += " ,t7.BranchID as BranchID ";
                sql += " ,t7.BranchName as BranchName ";
                sql += " ,FlagMember ";
                sql += " from tbl_ArCustomer as t1 ";
                sql += " left join tbl_ShopType as t2 on t1.ShopTypeID = t2.ShopTypeID ";
                sql += " left join tbl_SalArea as t3 on t1.SalAreaID = t3.SalAreaID ";
                sql += " left join tbl_BranchWarehouse as t4 on t1.WHID = t4.WHID ";
                sql += " left join tbl_Employee as t5 on t1.EmpID = t5.EmpID ";
                sql += " left join tbl_SalAreaDistrict as t6 on t1.SalAreaID = t6.SalAreaID ";
                sql += " left join tbl_Branch as t7 on t4.BranchID = t7.BranchID ";
                sql += " WHERE '"+ whid + "' = CASE WHEN  '" + whid + "'<> '' THEN t1.WHID ELSE '' END ";
                sql += " AND '" + salAreaID + "' = CASE WHEN '" + salAreaID + "'<> '' THEN t1.SalAreaID ELSE '' END ";
                sql += " AND " + shopTypeID + " = CASE WHEN " + shopTypeID + " <> 0 THEN t1.ShopTypeID ELSE 0 END";
                sql += " AND t1.FlagDel = " + flagDel+ ""+ " order by t1.SalAreaID,t4.WHID,t1.Seq";

                SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                da.Fill(ds, "Cust");

                return ds.Tables["Cust"];
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }

        }


        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_ArCustomer"></param>
        /// <returns></returns>
        public static List<tbl_ArCustomer> Select(this tbl_ArCustomer tbl_ArCustomer, Func<tbl_ArCustomer, bool> predicate)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ArCustomer.Where(predicate).Where(x => x.FlagDel == false).OrderBy(x => x.WHID).ThenBy(x => x.SalAreaID).ThenBy(x => x.Seq).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ArCustomer"></param>
        /// <returns></returns>
        public static List<tbl_ArCustomer> SelectAll(this tbl_ArCustomer tbl_ArCustomer)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_ArCustomer.Where(x => x.FlagDel == false).OrderBy(x => x.WHID).ThenBy(x => x.SalAreaID).ThenBy(x => x.Seq).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ArCustomer"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ArCustomer tbl_ArCustomer)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ArCustomer.Attach(tbl_ArCustomer);
                    db.tbl_ArCustomer.Add(tbl_ArCustomer);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ArCustomer"></param>
        /// <returns></returns>
        public static int Update(this tbl_ArCustomer tbl_ArCustomer)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ArCustomer.FirstOrDefault(x => x.CustomerID == tbl_ArCustomer.CustomerID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ArCustomerItem in tbl_ArCustomer.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ArCustomerItem.Name)
                                {
                                    var value = tbl_ArCustomerItem.GetValue(tbl_ArCustomer, null);

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
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ArCustomer"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ArCustomer tbl_ArCustomer)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ArCustomer).State = EntityState.Deleted;
                    db.tbl_ArCustomer.Remove(tbl_ArCustomer);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return ret;
        }
    }
}