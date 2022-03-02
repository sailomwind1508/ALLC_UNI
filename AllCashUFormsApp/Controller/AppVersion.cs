using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class AppVersion : BaseControl
    {
        public AppVersion() : base("")
        {

        }
    
        public DataTable CheckAppVersion(string version)
        {
            DataTable dt = new DataTable("AppVersion");
            try
            {
                DataTable newTable = new DataTable();
                Dictionary<string, object> sqlParams = new Dictionary<string, object>();
                sqlParams.Add("@versionParam", version);

                string sql = "proc_check_app_version";
                dt = My_DataTable_Extensions.ExecuteCenterStoreToDataTable(sql, sqlParams);
            }
            catch (Exception ex)
            {
                dt = null;
                ex.WriteLog(this.GetType());
            }

            return dt;
        }
    }


}
