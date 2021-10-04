using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllCashUFormsApp
{
    public static class CfgSettingDao
    {
        public static List<tbl_CfgSetting> SelectAll(this tbl_CfgSetting tbl_CfgSetting)
        {
            List<tbl_CfgSetting> list = new List<tbl_CfgSetting>();
            try
            {
                string sql = "SELECT * FROM tbl_CfgSetting";
                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_CfgSetting), sql);
                list = dynamicListReturned.Cast<tbl_CfgSetting>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_CfgSetting.GetType());
            }

            return list;
        }
        public static int Update(this tbl_CfgSetting tbl_CfgSetting)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_CfgSetting.FirstOrDefault(x => x.cfgName == tbl_CfgSetting.cfgName);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_CfgSettingItem in tbl_CfgSetting.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_CfgSettingItem.Name)
                                {
                                    var value = tbl_CfgSettingItem.GetValue(tbl_CfgSetting, null);

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
                ex.WriteLog(tbl_CfgSetting.GetType());
            }

            return ret;
        }
        public static DataTable GetCfgSettingData(this tbl_CfgSetting tbl_CfgSetting)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM tbl_CfgSetting";
            dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            return dt;
        }
    }
}
