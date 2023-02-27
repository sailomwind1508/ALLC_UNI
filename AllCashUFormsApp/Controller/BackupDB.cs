using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class BackupDB : BaseControl
    {
        private SqlCommand m_rCommand;

        public SqlCommand Command
        {
            get { return m_rCommand; }
            set { m_rCommand = value; }
        }

        public BackupDB() : base("")
        {

        }

        public bool CallBackupDB(string filePath, string dbName, bool compressFlag, BackgroundWorker self)
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
                        Command.CommandText = "proc_backup_db_with_path";
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.CommandTimeout = 0;
                        Command.Parameters.Add(new SqlParameter("@FilePath", filePath));
                        Command.Parameters.Add(new SqlParameter("@DBName", dbName));
                        Command.Parameters.Add(new SqlParameter("@CompressFlag", compressFlag));
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

        public bool CallStopBackupDB()
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
