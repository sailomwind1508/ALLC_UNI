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
    public class SendToHQ : BaseControl
    {
        private  SqlCommand m_rCommand;

        public SqlCommand Command
        {
            get { return m_rCommand; }
            set { m_rCommand = value; }
        }

        public SendToHQ() : base("")
        {

        }

        public bool CallSendToHQ(DateTime docDate, BackgroundWorker self)
        {
            bool ret = false;
            try
            {
                //var conStr = ConfigurationManager.AppSettings["HQ_Center"];
                //using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                //{
                //    con.Open();

                //    SqlCommand cmd = new SqlCommand("proc_Send_Data_Center", con);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.CommandTimeout = 0;
                //    //cmd.Parameters.Add(new SqlParameter("@DocNo", docNo));
                //    var result = cmd.ExecuteNonQuery();
                //    ret = true;

                //    con.Close();
                //}

                using (var cn = new SqlConnection(Connection.ConnectionString))
                {
                    cn.FireInfoMessageEventOnUserErrors = true;
                    cn.Open();
                    cn.InfoMessage += (o, args) => self.ReportProgress(0, args.Message);

                    using (Command = cn.CreateCommand())
                    {
                        Command.CommandText = "proc_Send_Data_Center";
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.CommandTimeout = 0;
                        Command.Parameters.Add(new SqlParameter("@DocDate", docDate));
                        Command.ExecuteNonQuery();

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

        public bool CallSendToHQ(string docDate, BackgroundWorker self)
        {
            bool ret = false;
            try
            {
                using (var cn = new SqlConnection(Connection.ConnectionString))
                {
                    cn.FireInfoMessageEventOnUserErrors = true;
                    cn.Open();
                    cn.InfoMessage += (o, args) => self.ReportProgress(0, args.Message);

                    using (Command = cn.CreateCommand())
                    {
                        Command.CommandText = "proc_Send_Data_Center";
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.CommandTimeout = 0;
                        Command.Parameters.Add(new SqlParameter("@DocDate", docDate));
                        Command.ExecuteNonQuery();

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

        public bool CallStopSendToHQ()
        {
            bool ret = false;
            try
            {
                Command.Cancel();
                ret = true;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                ret = false;
            }

            return ret;
        }

        public bool VerifyHQConnection()
        {
            bool ret = false;
            try
            {
                DataTable dt = new DataTable();

                string sql = "proc_Send_Data_Center_CheckOnlineStatus";

                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                ret = false;
            }

            return ret;
        }

        public bool CallSendAmtArCustomerData(Dictionary<string, object> _params)//
        {
            bool ret = false;
            try
            {
                return new tbl_AmtArCustomer().CallSendAmtArCustomerData(_params);
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
