using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AllCashUFormsApp.Model;

namespace AllCashUFormsApp
{
    public static class AmtArCustomerDao
    {
        public static bool CallSendAmtArCustomerData(this tbl_AmtArCustomer tbl_AmtArCustomer, Dictionary<string, object> _params) //
        {
            bool ret = false;
            try
            {
                string sql = "proc_SendAmtCustomerData";
                DataTable dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

                if (dt != null && dt.Rows.Count != 0)
                {
                    if (dt.Rows[0]["Result"].ToString() == "1")
                    {
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
                ret = false;
            }

            return ret;
        }
    }
}
