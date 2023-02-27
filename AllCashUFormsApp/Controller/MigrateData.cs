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
    public class MigrateData : BaseControl
    {
        private  SqlCommand m_rCommand;

        public SqlCommand Command
        {
            get { return m_rCommand; }
            set { m_rCommand = value; }
        }

        public MigrateData() : base("")
        {

        }

        public bool CallMigrateData(string dbName, DateTime docDateF, DateTime docDateT, BackgroundWorker self)
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
                        Command.CommandText = "proc_SendData_MigrationData";
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.CommandTimeout = 0;
                        Command.Parameters.Add(new SqlParameter("@dbname", dbName));
                        Command.Parameters.Add(new SqlParameter("@DateFr", docDateF));
                        Command.Parameters.Add(new SqlParameter("@DateTo", docDateT));
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

        public bool CallMigrateData(string dbName, string docDateF, string docDateT, BackgroundWorker self)
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
                        Command.CommandText = "proc_SendData_MigrationData";
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.CommandTimeout = 0;
                        Command.Parameters.Add(new SqlParameter("@dbname", dbName));
                        Command.Parameters.Add(new SqlParameter("@DateFr", docDateF));
                        Command.Parameters.Add(new SqlParameter("@DateTo", docDateT));
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

        public bool CallStopMigrateData()
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

    }
}
