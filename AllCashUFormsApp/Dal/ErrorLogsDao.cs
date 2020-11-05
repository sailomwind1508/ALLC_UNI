using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class ErrorLogsDao
    {
        public static int Insert(this tbl_error_logs tbl_error_logs)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_error_logs.Add(tbl_error_logs);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_error_logs.Add(new tbl_error_logs { user_code = Helper.user_name, form_name = tbl_error_logs.GetType().Name, function_name = Helper.GetCurrentMethod(), err_desc = ex.Message });
                    ret = db.SaveChanges();
                }
                MessageBox.Show(ex.Message, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;
        }
    }
}
