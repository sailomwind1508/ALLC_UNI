using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class SQLPrompt : BaseControl
    {
        private SqlCommand m_rCommand;

        public SqlCommand Command
        {
            get { return m_rCommand; }
            set { m_rCommand = value; }
        }

        public SQLPrompt() : base("")
        {

        }

        public Dictionary<DataTable, string> Execute(string sqlCmd)
        {
            Dictionary<DataTable, string> ret = new Dictionary<DataTable, string>();
            try
            {
                var dt = new DataTable();
                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sqlCmd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string message = "(" + dt.Rows.Count + " row(s) affected)";
                    ret.Add(dt, message);
                }
                else
                {
                    string message = "(0 row(s) affected)";
                    ret.Add(new DataTable(), message);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                ret.Add(new DataTable(), ex.Message);
            }

            return ret;
        }
        
        public string ExecuteMessage()
        {
            string ret = "";
            try
            {

                ret = "";
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                ret = "";
            }

            return ret;
        }

        public bool ExportExcel()
        {
            bool ret = false;
            try
            {
                
                ret = true;
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

        public DataTable RepairVE(DateTime docDateF, DateTime docDateT)
        {
            DataTable newTable = new DataTable();
            try
            {
                string sql = "proc_manual_repair_ve";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocDateF", docDateF.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@DocDateT", docDateT.ToString("yyyyMMdd", new CultureInfo("en-US")));

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable RecoveryPO(string whid, DateTime docDate, DateTime newDocDate, string mode)
        {
            DataTable newTable = new DataTable();
            try
            {
                string sql = "proc_manual_unlock_endday";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@whid", whid);
                sqlParmas.Add("@docdate", docDate.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@new_docdate", newDocDate.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@tmpMode", mode);

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable UnlockEndDay(string branchID, DateTime docDate)
        {
            DataTable newTable = new DataTable();
            try
            {
                string sql = "proc_manual_unlock_endday";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@BranchIDs", branchID);
                sqlParmas.Add("@DocDate", docDate.ToString("yyyyMMdd", new CultureInfo("en-US")));

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable SendToCenter(DateTime docDateF, DateTime docDateT)
        {
            DataTable newTable = new DataTable();
            try
            {
                string sql = "proc_manual_send_to_center";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocDateF", docDateF.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@DocDateT", docDateT.ToString("yyyyMMdd", new CultureInfo("en-US")));

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable SendToCenterByBranch(string branchIDs, DateTime docDateF, DateTime docDateT)
        {
            DataTable newTable = new DataTable();
            try
            {
                string sql = "proc_manual_send_to_center_by_branch";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocDateF", docDateF.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@DocDateT", docDateT.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@BranchIds", branchIDs);

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

    }
}
