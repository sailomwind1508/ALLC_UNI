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
    public static class ProductSubGroupDao
    {
        public static List<tbl_ProductSubGroup> SelectNonFlag(this tbl_ProductSubGroup tbl_ProductSubGroup, Func<tbl_ProductSubGroup, bool> predicate)
        {
            List<tbl_ProductSubGroup> list = new List<tbl_ProductSubGroup>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_ProductSubGroup] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductSubGroup), sql);
                list = dynamicListReturned.Cast<tbl_ProductSubGroup>().ToList();
                list = list.Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductSubGroup.Where(predicate).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductSubGroup.GetType());
            }

            return list;
        }
       
        public static List<tbl_ProductSubGroup> SelectAllNonFlag(this tbl_ProductSubGroup tbl_ProductSubGroup)
        {
            List<tbl_ProductSubGroup> list = new List<tbl_ProductSubGroup>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_ProductSubGroup] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductSubGroup), sql);
                list = dynamicListReturned.Cast<tbl_ProductSubGroup>().ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductSubGroup.OrderBy(x => x.ProductSubGroupID).ToList();
                //}
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ProductSubGroup.GetType());
            }

            return list;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_ProductSubGroup"></param>
        /// <returns></returns>
        public static List<tbl_ProductSubGroup> Select(this tbl_ProductSubGroup tbl_ProductSubGroup, Func<tbl_ProductSubGroup, bool> predicate)
        {
            List<tbl_ProductSubGroup> list = new List<tbl_ProductSubGroup>();
            try
            {
                list = tbl_ProductSubGroup.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductSubGroup.Where(x => x.FlagDel == false).Where(predicate).AsQueryable().ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductSubGroup.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_ProductSubGroup"></param>
        /// <returns></returns>
        public static List<tbl_ProductSubGroup> SelectAll(this tbl_ProductSubGroup tbl_ProductSubGroup)
        {
            List<tbl_ProductSubGroup> list = new List<tbl_ProductSubGroup>();
            try
            {

                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_ProductSubGroup] WHERE FlagDel = 0 Order By ProductSubGroupID ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_ProductSubGroup), sql);
                list = dynamicListReturned.Cast<tbl_ProductSubGroup>().ToList();


                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_ProductSubGroup.Where(x => x.FlagDel == false).OrderBy(x => x.ProductSubGroupID).ToList();
                //}
            }
            catch (Exception ex)
            {

                ex.WriteLog(tbl_ProductSubGroup.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_ProductSubGroup"></param>
        /// <returns></returns>
        public static int Insert(this tbl_ProductSubGroup tbl_ProductSubGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_ProductSubGroup.Attach(tbl_ProductSubGroup);
                    db.tbl_ProductSubGroup.Add(tbl_ProductSubGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductSubGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_ProductSubGroup"></param>
        /// <returns></returns>
        public static int Update(this tbl_ProductSubGroup tbl_ProductSubGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_ProductSubGroup.FirstOrDefault(x => x.ProductGroupID == tbl_ProductSubGroup.ProductGroupID && x.ProductSubGroupCode == tbl_ProductSubGroup.ProductSubGroupCode);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_ProductSubGroupItem in tbl_ProductSubGroup.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_ProductSubGroupItem.Name)
                                {
                                    var value = tbl_ProductSubGroupItem.GetValue(tbl_ProductSubGroup, null);

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
                        ret = tbl_ProductSubGroup.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductSubGroup.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_ProductSubGroup"></param>
        /// <returns></returns>
        public static int Delete(this tbl_ProductSubGroup tbl_ProductSubGroup)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_ProductSubGroup).State = EntityState.Deleted;
                    db.tbl_ProductSubGroup.Remove(tbl_ProductSubGroup);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductSubGroup.GetType());
            }

            return ret;
        }

        public static DataTable GetProductSubGroupData_Popup(this tbl_ProductSubGroup tbl_ProductSubGroup, string Search)
        {
            try
            {
                DataTable newTable = new DataTable(); //22-06-2022 adisorn หน้าค้นหากลุ่มย่อยสินค้า ในรายงาน 6.1

                string sql = "SELECT * FROM tbl_ProductSubGroup";
                sql += " WHERE FlagDel = 0";

                if (!string.IsNullOrEmpty(Search))
                {
                    sql += " AND ProductSubGroupCode LIKE '%" + Search + "%' OR ProductSubGroupName LIKE '%" + Search + "%' ";
                }

                sql += " ORDER BY ProductSubGroupID ";

                newTable = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return newTable;

            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_ProductSubGroup.GetType());
                return null;
            }

        }


    }
}
