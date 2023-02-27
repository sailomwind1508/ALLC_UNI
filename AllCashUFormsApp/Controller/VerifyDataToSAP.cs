using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class VerifyDataToSAP : BaseControl
    {
        private Func<tbl_PRMaster, bool> _docTypePredicate = null;
        public virtual Func<tbl_PRMaster, bool> docTypePredicate
        {
            get { return _docTypePredicate; }
            set
            {
                _docTypePredicate = value;
            }
        }

        private SqlCommand m_rCommand;

        public SqlCommand Command
        {
            get { return m_rCommand; }
            set { m_rCommand = value; }
        }

        public VerifyDataToSAP() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        public DataTable VerifyPO()
        {
            DataTable newTable = new DataTable();
            try
            {
                string sql = "proc_Send_Data_To_DBCenter_CheckPO";
                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }


        public DataTable CheckSendToCenterStatus(DateTime docdateF, DateTime docdateT)
        {
            DataTable newTable = new DataTable();
            try
            {
                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocDateFrom", docdateF.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@DocDateTo", docdateT.ToString("yyyyMMdd", new CultureInfo("en-US")));

                string sql = "proc_Send_Data_Center_CheckStatus";
                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable VerifyQtyAmt()
        {
            DataTable newTable = new DataTable();
            try
            {
                string sql = "proc_Send_Data_To_DBCenter_CheckQTYAmount";
                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable VerifyVE()
        {
            DataTable newTable = new DataTable();
            try
            {
                string sql = "proc_Send_Data_To_DBCenter_CheckVE";
                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable VerifyVEDiffData()
        {
            DataTable newTable = new DataTable();
            try
            {
                string sql = "proc_Send_Data_To_DBCenter_Verify_VE";
                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable VerifyStock(DateTime dateFrom, DateTime dateTo)
        {
            DataTable newTable = new DataTable();
            try
            {
                
                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DateFrom", dateFrom.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@DateTo", dateTo.ToString("yyyyMMdd", new CultureInfo("en-US")));

                string sql = "proc_Send_Data_To_DBCenter_CheckStock";
                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable VerifyUOM()
        {
            DataTable newTable = new DataTable();
            try
            {
                string sql = "proc_Send_Data_To_DBCenter_CheckUOM";
                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable SearchInvSendToCenter(string txt, DateTime docdate)
        {
            DataTable newTable = new DataTable();
            try
            {
                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocNo", txt);
                sqlParmas.Add("@DocDate", docdate.ToString("yyyyMMdd", new CultureInfo("en-US")));

                string sql = "proc_Send_Data_Center_Search_INV";
                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable SearchInv(string txt)
        {
            DataTable newTable = new DataTable();
            try
            {
                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocNum", txt);

                string sql = "proc_Send_Data_To_DBCenter_Search_INV";
                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public bool RemoveINV(string docnums)
        {
            DataTable newTable = new DataTable();
            try
            {
                string sql = "proc_Send_Data_To_DBCenter_Remove_INV";
                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocNums", docnums);

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return false;
            }
        }

        public DataTable SendDataToDBCenter()
        {

            DataTable newTable = new DataTable();
            try
            {
                string sql = "proc_Send_Data_To_DBCenter_ConfirmSend";
                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    sql = "proc_Send_Data_To_DBCenter_InsertINV_DT";
                    newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);
                    if (newTable != null && newTable.Rows.Count > 0)
                    {
                        if (newTable.Rows.Count == 1)
                        {
                            if (newTable.Rows[0][0].ToString() == "0")
                            {
                                newTable = null;
                            }
                        }
                    }
                    else
                    {
                        newTable = new DataTable();
                    }
                }
                else
                    newTable = null;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                newTable = null;
            }

            return newTable;

            //using (var cn = new SqlConnection(Connection.ConnectionString))
            //{
            //    cn.FireInfoMessageEventOnUserErrors = true;
            //    cn.Open();
            //    cn.InfoMessage += (o, args) => self.ReportProgress(0, args.Message);

            //    using (Command = cn.CreateCommand())
            //    {
            //        Command.CommandText = "proc_Send_Data_To_DBCenter_ConfirmSend";
            //        Command.CommandType = CommandType.StoredProcedure;
            //        Command.CommandTimeout = 0;
            //        Command.ExecuteNonQuery();

            //        ret = true;
            //    }
            //    cn.Close();


        }
    }
}
