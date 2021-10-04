using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class SendDataDao
    {
        public static List<tbl_SendData> Select(this tbl_SendData tbl_SendData, Func<tbl_SendData, bool> predicate)
        {
            List<tbl_SendData> list = new List<tbl_SendData>();
            try
            {
                list = tbl_SendData.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_SendData.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SendData.GetType());
            }

            return list;
        } 
        public static List<tbl_SendData> SelectAll(this tbl_SendData tbl_SendData)
        {
            List<tbl_SendData> list = new List<tbl_SendData>();
            try
            {
                string sql = "";

                sql += " SELECT * FROM [dbo].[tbl_SendData] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_SendData), sql);
                list = dynamicListReturned.Cast<tbl_SendData>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_SendData.ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SendData.GetType());
            }

            return list;
        }
        public static int Insert(this tbl_SendData tbl_SendData)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_SendData.Attach(tbl_SendData);
                    db.tbl_SendData.Add(tbl_SendData);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SendData.GetType());
            }

            return ret;
        }
        public static int Update(this tbl_SendData tbl_SendData)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_SendData.FirstOrDefault(x => x.DateSend == tbl_SendData.DateSend && x.WHID == tbl_SendData.WHID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_SendDataItem in tbl_SendData.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_SendDataItem.Name)
                                {
                                    var value = tbl_SendDataItem.GetValue(tbl_SendData, null);

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
                        ret = tbl_SendData.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SendData.GetType());
            }

            return ret;
        }
        public static int Delete(this tbl_SendData tbl_SendData)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_SendData).State = EntityState.Deleted;
                    db.tbl_SendData.Remove(tbl_SendData);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_SendData.GetType());
            }

            return ret;
        }
    }
}
