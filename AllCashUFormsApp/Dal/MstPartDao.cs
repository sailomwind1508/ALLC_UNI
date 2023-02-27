using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class MstPartDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_MstPart"></param>
        /// <returns></returns>
        public static List<tbl_MstPart> Select(this tbl_MstPart tbl_MstPart, Func<tbl_MstPart, bool> predicate)
        {
            List<tbl_MstPart> list = new List<tbl_MstPart>();
            try
            {
                list = tbl_MstPart.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_MstPart.Where(x => x.FlagDel == false).Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstPart.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_MstPart"></param>
        /// <returns></returns>
        public static List<tbl_MstPart> SelectAll(this tbl_MstPart tbl_MstPart)
        {
            List<tbl_MstPart> list = new List<tbl_MstPart>();
            try
            {
                string sql = "";
                sql += " SELECT * FROM [dbo].[tbl_MstPart] WHERE FlagDel = 0 ORDER BY PartID ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_MstPart), sql);
                list = dynamicListReturned.Cast<tbl_MstPart>().ToList();


                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_MstPart.Where(x => x.FlagDel == false).OrderBy(x => x.PartID).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstPart.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_MstPart"></param>
        /// <returns></returns>
        public static int Insert(this tbl_MstPart tbl_MstPart)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_MstPart.Attach(tbl_MstPart);
                    db.tbl_MstPart.Add(tbl_MstPart);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstPart.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_MstPart"></param>
        /// <returns></returns>
        public static int Update(this tbl_MstPart tbl_MstPart)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_MstPart.FirstOrDefault(x => x.PartID == tbl_MstPart.PartID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_MstPartItem in tbl_MstPart.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_MstPartItem.Name)
                                {
                                    var value = tbl_MstPartItem.GetValue(tbl_MstPart, null);

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
                ex.WriteLog(tbl_MstPart.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_MstPart"></param>
        /// <returns></returns>
        public static int Delete(this tbl_MstPart tbl_MstPart)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_MstPart).State = EntityState.Deleted;
                    db.tbl_MstPart.Remove(tbl_MstPart);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_MstPart.GetType());
            }

            return ret;
        }
    }
}
