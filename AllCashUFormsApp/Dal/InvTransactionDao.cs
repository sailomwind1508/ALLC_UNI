using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;


namespace AllCashUFormsApp
{
    public static class InvTransactionDao
    {
        public static List<tbl_InvTransaction> Select(this tbl_InvTransaction tbl_InvTransaction, string docNo)
        {
            List<tbl_InvTransaction> list = new List<tbl_InvTransaction>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_InvTransaction] ";
                sql += " WHERE FlagDel = 0 ";
                sql += " AND RefDocNo = '" + docNo.Trim() + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_InvTransaction), sql);
                list = dynamicListReturned.Cast<tbl_InvTransaction>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvTransaction.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_InvTransaction"></param>
        /// <returns></returns>
        public static List<tbl_InvTransaction> Select(this tbl_InvTransaction tbl_InvTransaction, Func<tbl_InvTransaction, bool> predicate, string docTypeCode = "")
        {
            List<tbl_InvTransaction> list = new List<tbl_InvTransaction>();
            try
            {
                if (!string.IsNullOrEmpty(docTypeCode))
                {
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += " SELECT * ";
                    sql += " FROM [dbo].[tbl_InvTransaction] ";
                    sql += " WHERE DocTypeCode = '" + docTypeCode.Trim() + "' ";

                    List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_InvTransaction), sql);
                    list = dynamicListReturned.Cast<tbl_InvTransaction>().ToList();
                }
                else
                    list = tbl_InvTransaction.SelectAll().Where(predicate).ToList();


                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_InvTransaction.Where(predicate).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvTransaction.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_InvTransaction"></param>
        /// <returns></returns>
        public static List<tbl_InvTransaction> SelectAll(this tbl_InvTransaction tbl_InvTransaction)
        {
            List<tbl_InvTransaction> list = new List<tbl_InvTransaction>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += " FROM [dbo].[tbl_InvTransaction] WHERE FlagDel = 0 ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_InvTransaction), sql);
                list = dynamicListReturned.Cast<tbl_InvTransaction>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_InvTransaction>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_InvTransaction.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvTransaction.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_InvTransaction"></param>
        /// <returns></returns>
        public static void Insert(this tbl_InvTransaction tbl_InvTransaction, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.tbl_InvTransaction.Attach(tbl_InvTransaction);
                db.tbl_InvTransaction.Add(tbl_InvTransaction);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvTransaction.GetType());
            }
        }

        public static void Insert(this List<tbl_InvTransaction> tbl_InvTransactions, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                foreach (var tbl_InvTransaction in tbl_InvTransactions)
                {
                    db.tbl_InvTransaction.Attach(tbl_InvTransaction);
                    db.tbl_InvTransaction.Add(tbl_InvTransaction);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
            }
        }

        public static int UpdateEntity(this List<tbl_InvTransaction> tbl_InvTransactions, DB_ALL_CASH_UNIEntities db, string docTypeCode = "")
        {
            int ret = 0;

            try
            {
                var refDocNo = tbl_InvTransactions.First().RefDocNo;
                var tbl_InvTransactionList = db.tbl_InvTransaction.Where(x => x.RefDocNo == refDocNo).ToList();

                if (tbl_InvTransactionList.Count > 0)
                {
                    foreach (var tbl_InvTransaction in tbl_InvTransactions)
                    {
                        var updateData = tbl_InvTransactionList.FirstOrDefault(x => x.TransactionID == tbl_InvTransaction.TransactionID);// db.tbl_InvTransaction.FirstOrDefault(x => x.TransactionID == tbl_InvTransaction.TransactionID);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_InvTransactionItem in tbl_InvTransaction.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "transactonid" && updateDataItem.Name == tbl_InvTransactionItem.Name)
                                    {
                                        var value = tbl_InvTransactionItem.GetValue(tbl_InvTransaction, null);

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
                            //tbl_InvTransaction.Delete(db);
                            tbl_InvTransaction.Insert(db);
                        }
                    }
                }
                else
                    tbl_InvTransactions.Insert(db);

                ret = 1;

            }
            catch (Exception ex)
            {
                ex.WriteLog(db.GetType());
                ret = 0;
            }

            return ret;
        }

        public static int Update(this List<tbl_InvTransaction> tbl_InvTransactions)
        {
            int ret = 0;

            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    foreach (var tbl_InvTransaction in tbl_InvTransactions)
                    {
                        var updateData = db.tbl_InvTransaction.FirstOrDefault(x => x.TransactionID == tbl_InvTransaction.TransactionID);
                        if (updateData != null)
                        {
                            foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                            {
                                foreach (PropertyInfo tbl_InvTransactionItem in tbl_InvTransaction.GetType().GetProperties())
                                {
                                    if (updateDataItem.Name.ToLower() != "transactonid" && updateDataItem.Name == tbl_InvTransactionItem.Name)
                                    {
                                        var value = tbl_InvTransactionItem.GetValue(tbl_InvTransaction, null);

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
                            tbl_InvTransaction.Delete(db);
                            tbl_InvTransaction.Insert(db);
                        }

                    }

                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //ex.WriteLog(tbl_InvTransaction);
            }

            return ret != 0 ? 1 : 0;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_InvTransaction"></param>
        /// <returns></returns>
        public static void Delete(this tbl_InvTransaction tbl_InvTransaction, DB_ALL_CASH_UNIEntities db)
        {
            try
            {
                db.Entry(tbl_InvTransaction).State = EntityState.Deleted;
                db.tbl_InvTransaction.Remove(tbl_InvTransaction);
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvTransaction.GetType());
            }
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_InvTransaction"></param>
        /// <returns></returns>
        public static int Insert(this tbl_InvTransaction tbl_InvTransaction)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_InvTransaction.Attach(tbl_InvTransaction);
                    db.tbl_InvTransaction.Add(tbl_InvTransaction);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvTransaction.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_InvTransaction"></param>
        /// <returns></returns>
        public static int Update(this tbl_InvTransaction tbl_InvTransaction)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_InvTransaction.FirstOrDefault(x => x.TransactionID == tbl_InvTransaction.TransactionID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_InvTransactionItem in tbl_InvTransaction.GetType().GetProperties())
                            {
                                if (updateDataItem.Name.ToLower() != "transactonid" && updateDataItem.Name == tbl_InvTransactionItem.Name)
                                {
                                    var value = tbl_InvTransactionItem.GetValue(tbl_InvTransaction, null);

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
                        ret = tbl_InvTransaction.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvTransaction.GetType());
            }

            return ret;
        }


        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_InvTransaction"></param>
        /// <returns></returns>
        public static int Delete(this tbl_InvTransaction tbl_InvTransaction)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_InvTransaction).State = EntityState.Deleted;
                    //db.tbl_InvTransaction.Attach(tbl_InvTransaction);
                    db.tbl_InvTransaction.Remove(tbl_InvTransaction);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_InvTransaction.GetType());
            }

            return ret;
        }
    }
}