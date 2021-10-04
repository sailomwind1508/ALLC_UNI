using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class ArCustomerDao
    {
        private static List<tbl_ArCustomer> tbl_ArCustomers = new List<tbl_ArCustomer>();

        public static DataTable GetCustomerPre(this tbl_ArCustomer tbl_ArCustomer)
        {
            DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();
            try
            {
                string sql = "proc_IMPre_GetCustomer";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg.ShowErrorMessage();
                newTable = null;
            }

            return newTable;
        }

        public static List<tbl_ArCustomer> SelectSalArea(this tbl_ArCustomer tbl_ArCustomer, string SalAreaID)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {
                //if (tbl_ArCustomers.Count == 0)
                //{
                DataTable dt = new DataTable();
                string sql = "";
                sql += "SELECT * FROM tbl_ArCustomer WHERE SalAreaID = '" + SalAreaID + "'";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ArCustomer), sql);
                list = dynamicListReturned.Cast<tbl_ArCustomer>().ToList();

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return list;
        }

        public static List<tbl_ArCustomer> SelectSingle(this tbl_ArCustomer tbl_ArCustomer, string customerID)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {
                //if (tbl_ArCustomers.Count == 0)
                //{
                DataTable dt = new DataTable();
                string sql = "";
                sql += "SELECT * FROM tbl_ArCustomer WHERE CustomerID = '" + customerID + "'";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ArCustomer), sql);
                list = dynamicListReturned.Cast<tbl_ArCustomer>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_ArCustomer>(dt);

                //}
                //else
                //    list.AddRange(tbl_ArCustomers.Where(x => x.CustomerID == customerID));
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return list;
        }

        public static List<tbl_ArCustomer> Select(this tbl_ArCustomer tbl_ArCustomer, string salAreaID)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {
                if (tbl_ArCustomers.Count == 0)
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += CreateSelectCmd();
                    sql += " FROM tbl_ArCustomer WHERE SalAreaID = '" + salAreaID + "'";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ArCustomer), sql);
                    list = dynamicListReturned.Cast<tbl_ArCustomer>().ToList();

                    //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                    //da.Fill(dt);

                    //list = ConvertHelper.ConvertDataTable<tbl_ArCustomer>(dt);

                }
                else
                    list.AddRange(tbl_ArCustomers.Where(x => x.SalAreaID == salAreaID));
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return list;
        }

        public static List<tbl_ArCustomer> SelectEntity(this tbl_ArCustomer tbl_ArCustomer, Func<tbl_ArCustomer, bool> predicate)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {
                list = tbl_ArCustomer.SelectAllEntity().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ArCustomer.Where(func).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return list;
        }

        public static List<tbl_ArCustomer> SelectCustomerID(this tbl_ArCustomer tbl_ArCustomer, string CustomerID, int flagDel)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {

                string sql = "SELECT * FROM tbl_ArCustomer WHERE CustomerID = '" + CustomerID + "'";
                sql += " AND flagDel = " + flagDel + "";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ArCustomer), sql);
                list = dynamicListReturned.Cast<tbl_ArCustomer>().ToList();

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return list;
        }

        public static List<tbl_ArCustomer> SelectAllEntity(this tbl_ArCustomer tbl_ArCustomer)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {

                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_ArCustomer] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ArCustomer), sql);
                list = dynamicListReturned.Cast<tbl_ArCustomer>().ToList();


                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ArCustomer.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return list;
        }

        public static List<tbl_ArCustomer> SelectAll(this tbl_ArCustomer tbl_ArCustomer)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {
                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ArCustomer.ToList();
                //}

                VerifyNewData();

                if (tbl_ArCustomers.Count == 0)
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += CreateSelectCmd();
                    sql += " FROM tbl_ArCustomer ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ArCustomer), sql);
                    list = dynamicListReturned.Cast<tbl_ArCustomer>().ToList();

                    //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                    //da.Fill(dt);

                    //list = ConvertHelper.ConvertDataTable<tbl_ArCustomer>(dt);

                    tbl_ArCustomers = list;
                }
                else
                {
                    list = tbl_ArCustomers;
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return list;
        }

        private static void VerifyNewData()
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += "SELECT COUNT(CustomerID) AS countCust FROM tbl_ArCustomer WHERE FlagDel = 0";

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);
                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                int count = Convert.ToInt32(dt.Rows[0][0]);

                if (count != tbl_ArCustomers.Count)
                {
                    dt = new DataTable();
                    sql = "";
                    sql += " SELECT * FROM tbl_ArCustomer WHERE FlagDel = 0 ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ArCustomer), sql);
                    var list = dynamicListReturned.Cast<tbl_ArCustomer>().ToList();

                    //da = new SqlDataAdapter(sql, Connection.ConnectionString);
                    //da.Fill(dt);

                    //var list = ConvertHelper.ConvertDataTable<tbl_ArCustomer>(dt);

                    tbl_ArCustomers = list;
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
            }
        }

        private static string CreateSelectCmd()
        {
            string sql = "";
            sql += " SELECT CustomerID ";
            sql += " ,CustomerTypeID  ";
            sql += " ,CustomerCode ";
            sql += " ,CustTitle ";
            sql += " ,CustName ";
            sql += " ,ShopTypeID ";
            sql += " ,SalAreaID ";
            sql += " ,WHID ";
            sql += " ,CustomerRefCode ";
            sql += " ,CustomerImg ";
            sql += " ,CustShortName ";
            sql += " ,Latitude ";
            sql += " ,Longitude ";
            sql += " ,CrDate ";
            sql += " ,BillTo ";
            sql += " ,ShipTo ";
            sql += " ,Contact ";
            sql += " ,Telephone ";
            sql += " ,EmpID ";
            sql += " ,CustomerSAPCode ";
            sql += " ,CreditDay ";
            sql += " ,Seq ";
            sql += " ,FlagMember ";
            sql += " ,ProvinceID ";
            sql += " ,DistrictID ";
            sql += " ,AreaID ";
            sql += " ,Discount ";
            sql += " ,Email ";
            sql += " ,TaxId ";
            sql += " ,Fax ";
            sql += " ,FlagDel ";
            sql += " ,PriceGroupID ";
            sql += " ,AddressNo ";
            sql += " ,lane ";
            sql += " ,Street ";
            sql += " ,PostalCode ";

            return sql;
        }

        public static void Insert(this tbl_ArCustomer tbl_ArCustomer, DB_ALL_CASH_UNIEntities db)
        {
            try
            {

                db.tbl_ArCustomer.Attach(tbl_ArCustomer);
                db.tbl_ArCustomer.Add(tbl_ArCustomer);

            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }

        }

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

        public static int UpdateEntity(this tbl_ArCustomer tbl_ArCustomer, DB_ALL_CASH_UNIEntities db)
        {
            int ret = 0;
            try
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

                }
                else
                {
                    tbl_ArCustomer.Insert(db);
                }
                ret = 1;
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            return ret;
        }

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
                    else
                    {
                        ret = tbl_ArCustomer.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return ret;
        }

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

        public static DataTable GetCustomerData(this tbl_ArCustomer tbl_ArCustomer, Dictionary<string, object> _params)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_GetCustomerData_New";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetTransferCustomerData(this tbl_ArCustomer tbl_ArCustomer, Dictionary<string, object> _params)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_GetTransferCustomerData";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}