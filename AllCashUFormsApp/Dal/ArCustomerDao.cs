using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            var list = new List<tbl_ArCustomer>();
            try
            {
                string sql = "SELECT * FROM tbl_ArCustomer";

                if (customerID.Length == 8)
                {
                    sql += " WHERE CustomerID = '" + customerID + "'";
                }
                else
                {
                    sql += " WHERE CustomerID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + customerID + "', ',')) ";
                }

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ArCustomer), sql);
                list = dynamicListReturned.Cast<tbl_ArCustomer>().ToList();
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

        public static List<tbl_ArCustomer> SelectCustomerID(this tbl_ArCustomer tbl_ArCustomer, string CustomerID)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {
                string sql = "SELECT * FROM tbl_ArCustomer WHERE CustomerID = '" + CustomerID + "'";

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
                    sql += CreateSelectCmd();
                    sql += " FROM tbl_ArCustomer WHERE FlagDel = 0 ";

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
            //sql += " ,CustomerImg "; //edti sailom .k 15/12/2021
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
            string msg = "start ArCustomerDao=>InsertWithDB";
            msg.WriteLog(null);

            try
            {
                db.tbl_ArCustomer.Attach(tbl_ArCustomer);
                db.tbl_ArCustomer.Add(tbl_ArCustomer);

            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }

            msg = "end ArCustomerDao=>InsertWithDB";
            msg.WriteLog(null);
        }

        public static int Insert(this tbl_ArCustomer tbl_ArCustomer)
        {
            string msg = "start ArCustomerDao=>Insert";
            msg.WriteLog(null);

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

            msg = "end ArCustomerDao=>Insert";
            msg.WriteLog(null);

            return ret;
        }

        public static int UpdateEntity(this tbl_ArCustomer tbl_ArCustomer, DB_ALL_CASH_UNIEntities db)
        {
            string msg = "start ArCustomerDao=>UpdateEntity";
            msg.WriteLog(null);

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

            msg = "end ArCustomerDao=>UpdateEntity";
            msg.WriteLog(null);

            return ret;
        }

        public static int Update(this tbl_ArCustomer tbl_ArCustomer)
        {
            string msg = "start ArCustomerDao=>Update";
            msg.WriteLog(null);

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

            msg = "end ArCustomerDao=>Update";
            msg.WriteLog(null);

            return ret;
        }


        /// <summary>
        /// create by sailom.k 27/05/2022
        /// </summary>
        /// <param name="tbl_ArCustomers"></param>
        /// <returns></returns>
        public static int Update(this List<tbl_ArCustomer> tbl_ArCustomers)
        {
            string msg = "start ArCustomerDaoList=>Update";
            msg.WriteLog(null);

            int ret = 0;
            try
            {
                DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString);

                foreach (var _tbl_ArCustomer in tbl_ArCustomers)
                {
                    var updateData = db.tbl_ArCustomer.FirstOrDefault(x => x.CustomerID == _tbl_ArCustomer.CustomerID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ArCustomerItem in _tbl_ArCustomer.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ArCustomerItem.Name)
                                {
                                    var value = tbl_ArCustomerItem.GetValue(_tbl_ArCustomer, null);

                                    Type t = Nullable.GetUnderlyingType(updateDataItem.PropertyType) ?? updateDataItem.PropertyType;
                                    object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                                    updateDataItem.SetValue(updateData, safeValue, null);
                                }
                            }
                        }

                        db.Entry(updateData).State = System.Data.Entity.EntityState.Modified;
                    }
                }

                ret = db.SaveChanges();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomers.GetType());
            }

            msg = "end ArCustomerDaoList=>Update";
            msg.WriteLog(null);

            return ret;
        }

        public static int Delete(this tbl_ArCustomer tbl_ArCustomer)
        {
            string msg = "start ArCustomerDao=>Delete";
            msg.WriteLog(null);

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

            msg = "end ArCustomerDao=>Delete";
            msg.WriteLog(null);

            return ret;
        }

        public static DataTable GetCustomerImage(this tbl_ArCustomer tbl_ArCustomer, Dictionary<string, object> _params)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "proc_GetCustomerData_Image";

                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
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

        public static DataTable VerifyFlagBill(this tbl_ArCustomer tbl_ArCustomer, string docDate)
        {
            var dt = new DataTable();
            try
            {
                //string sql = " SELECT t1.* FROM tbl_ArCustomer t1 ";
                //sql += " INNER JOIN dbo.tbl_POMaster t2 ON t2.DocStatus = '4' AND t2.DocTypeCode = 'IV' AND t1.CustomerID = t2.CustomerID ";
                //sql += " WHERE (FlagBill = 1 OR FlagMember = 1) AND ISNULL(CustomerSAPCode,'') = '' ";
                //sql += " AND ISNULL(t2.CustInvNO, '') = '' ";
                //sql += " AND CAST(t1.GPSDate AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) ";
                //sql += " ORDER BY t1.WHID,t1.SalAreaID ";

                string sql = "";
                sql += @" SELECT t2.*, t1.TotalDue, t1.DocNo, t1.DocTypeCode  
                FROM dbo.tbl_POMaster t1 
                INNER JOIN tbl_ArCustomer t2 ON t1.CustomerID = t2.CustomerID 
                WHERE ISNULL(CustInvNO, '') = '' ";
                sql += @" AND CAST(DocDate AS DATE) = '" + docDate + "' AND t1.DocStatus = '4' AND t1.DocTypeCode = 'IV' AND (FlagBill = 1 OR FlagMember = 1) ORDER BY t1.WHID, t1.CustName ";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return dt;
        }

        public static List<tbl_ArCustomer> SelectMaxCustomerID(this tbl_ArCustomer tbl_ArCustomer, string formatCustomerID)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {
                string sql = "SELECT CustomerID FROM tbl_ArCustomer";
                sql += " WHERE CustomerID LIKE '%" + formatCustomerID + "%'";
                sql += " AND LEN(CustomerID) > 12";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ArCustomer), sql);
                list = dynamicListReturned.Cast<tbl_ArCustomer>().ToList();

                if (list.Count == 0)
                {
                    sql = "SELECT CustomerID FROM tbl_ArCustomer";
                    sql += " WHERE CustomerID LIKE '%" + formatCustomerID + "%'";

                    dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ArCustomer), sql);
                    list = dynamicListReturned.Cast<tbl_ArCustomer>().ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return list;
        }

        public static List<tbl_ArCustomer> GetCustomerByWHID(this tbl_ArCustomer tbl_ArCustomer, string WHID)
        {
            List<tbl_ArCustomer> list = new List<tbl_ArCustomer>();
            try
            {
                string sql = "SELECT * FROM tbl_ArCustomer";
                sql += "  WHERE WHID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + WHID + "', ','))";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ArCustomer), sql);
                list = dynamicListReturned.Cast<tbl_ArCustomer>().ToList();

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return list;
        }

        public static DataTable GetCustomerByWHID_DataTable(this tbl_ArCustomer tbl_ArCustomer, string WHID)
        {
            DataTable ret = new DataTable();
            try
            {
                //string sql = "SELECT * FROM tbl_ArCustomer";
                //sql += "  WHERE WHID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + WHID + "', ','))";

                string sql = @"SELECT [CustomerID]
                  ,[CustName]
                  ,[BillTo]
                  ,[Telephone] 
	              ,Latitude
	              ,Longitude
	              ,CustomerImg
	              ,CustImage
	              ,t2.SalAreaName
	              ,'pin_mark' + CAST(t2.Seq AS NVARCHAR) + '.png' AS [MarkerImage]
	              FROM dbo.tbl_ArCustomer t1 
	              INNER JOIN dbo.tbl_SalArea t2 ON t1.SalAreaID = t2.SalAreaID ";
                sql += "  WHERE t1.WHID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + WHID + "', ','))";

                ret = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return ret;
        }

        public static DataTable GetCustomerByWHID_DataTable(this tbl_ArCustomer tbl_ArCustomer, string _WHID = "", string _SalAreaID = "")
        {
            DataTable ret = new DataTable();
            try
            {
                string sql = "";
                //string sql = "SELECT * FROM tbl_ArCustomer";
                //sql += "  WHERE WHID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + WHID + "', ','))";

                // string sql = @"SELECT [CustomerID]
                //   ,[CustName]
                //   ,[BillTo]
                //   ,[Telephone] 
                //,Latitude
                //,Longitude
                //,CustomerImg
                //,CustImage
                //,t2.SalAreaName
                //,'pin_mark' + CAST(t2.Seq AS NVARCHAR) + '.png' AS [MarkerImage]
                //FROM dbo.tbl_ArCustomer t1 
                //INNER JOIN dbo.tbl_SalArea t2 ON t1.SalAreaID = t2.SalAreaID ";

                sql = @"SELECT [CustomerID]
                    ,REPLACE(REPLACE([CustName],',',''), '|', '') AS CustName
                    ,REPLACE(REPLACE(ISNULL([BillTo],''),',',''), '|', '') AS BillTo
                    ,REPLACE(REPLACE(ISNULL([Telephone],''),',',''), '|', '') AS Telephone
                    ,Latitude
                    ,Longitude
                    ,t2.SalAreaName ";

                if (_WHID.Length > 6)
                {
                    sql += " ,'pin_mark' + CAST(CAST(SUBSTRING(t1.WHID, 5, 2) AS INT) AS NVARCHAR(5))  + '.png' AS [MarkerImage]";
                }
                else
                {
                    sql += " ,'pin_mark' + CAST(t2.Seq AS NVARCHAR) + '.png' AS [MarkerImage]";
                }

                sql += @" , (CASE WHEN ISNULL(CAST(t1.CustImage AS NVARCHAR), '') = '' then NULL
                    ELSE 
	                    IIF(CAST(t1.CustImage AS NVARCHAR) LIKE '%Images%', t1.CustImage, NULL) 
                    END) AS CustImage
                    , t1.FlagDel
                    , t1.WHID
                    FROM tbl_ArCustomer t1 
                    INNER JOIN tbl_SalArea t2 ON t1.SalAreaID = t2.SalAreaID ";

                //sql += @" INNER JOIN ( SELECT WHID FROM tbl_SendData 
                //        WHERE CAST(DateSend AS DATE) IN (SELECT MAX(CAST(DateSend AS DATE)) FROM tbl_SendData)
                //    )t3 ON t1.WHID = t3.WHID";

                sql += "  WHERE 1=1";
                if (!string.IsNullOrEmpty(_SalAreaID))
                {
                    sql += " AND t1.SalAreaID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + _SalAreaID + "', ','))";
                }
                else
                {
                    sql += " AND t1.WHID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + _WHID + "', ','))";
                }

                sql += "  AND t1.FlagDel = 0";
                sql += @"  AND Latitude LIKE '%.%' AND Longitude LIKE '%.%'
                           AND Latitude <> '0.0' AND Longitude <> '0.0' ";

                ret = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return ret;
        }

        public static DataTable GetCountCustomer(this tbl_ArCustomer tbl_ArCustomer)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "";

                //sql += @"SELECT COUNT(CustomerID) AS 'CountCust'
                //                , WHID, SalAreaID 
                //                FROM tbl_ArCustomer
                //                WHERE FlagDel = 0
                //                GROUP BY WHID,SalAreaID
                //                ORDER BY WHID,SalAreaID";

                //last edit by sailom .k 07/06/2022
                sql += @"SELECT COUNT(CustomerID) AS 'CountCust'
						, ISNULL(WHID, '') AS 'WHID'
						, ISNULL(SalAreaID, '') AS 'SalAreaID'
                        FROM tbl_ArCustomer
                        WHERE FlagDel = 0
                        AND ISNULL(WHID, '') <> '' AND ISNULL(SalAreaID, '') <> ''
                        AND Latitude LIKE '%.%' AND Longitude LIKE '%.%'
                        AND Latitude <> '0.0' AND Longitude <> '0.0'
                        GROUP BY WHID,SalAreaID
                        ORDER BY WHID, SalAreaID";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return dt;
        }

        public static DataTable GetServerImagePath(this tbl_ArCustomer tbl_ArCustomer, string BranchID)
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@BranchID", BranchID);

                string sql = "proc_get_customer_serverIMG_path";

                var conStr = ConfigurationManager.AppSettings["CenterConnect"].ToString();
                var conn = new SqlConnection(conStr);
                dt = My_DataTable_Extensions.GetDataTable(sql, conn, CommandType.StoredProcedure, _params);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ArCustomer.GetType());
            }

            return dt;
        }

        public static bool ManualUpdateCustomerImage(this tbl_ArCustomer tbl_ArCustomer)
        {
            bool ret = false;
            try
            {
                string sql = "proc_manual_update_customer_data_daily";
                DataTable dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                ret = true;

            }
            catch (Exception ex)
            {
                ret = false;
            }

            return ret;
        }

    }
}