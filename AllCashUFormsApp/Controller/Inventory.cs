       using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class Inventory : BaseControl
    {
        public Inventory() : base("")
        {

        }

        public bool UpdateInventoryWH(BackgroundWorker self)
        {
            bool ret = false;
            try
            {

                using (var cn = new SqlConnection(Connection.ConnectionString))
                {
                    cn.FireInfoMessageEventOnUserErrors = true;
                    cn.Open();
                    cn.InfoMessage += (o, args) => self.ReportProgress(0, args.Message);

                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "proc_manual_update_invwarehouse_daily";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();

                        ret = true;
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                ret = false;
            }

            return ret;
        }
    }
}
