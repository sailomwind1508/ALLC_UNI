using AllCashUFormsApp.Dal;
using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
namespace AllCashUFormsApp.Controller
{
    public class SendData : BaseControl
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
        
        public SendData() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        public DataTable SendPreOrderData(string whid)
        {
            try
            {
                DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@WHIDs", whid);

                string sql = "proc_SendData_From_Tablet_PRE";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetTLData(DateTime sDate)
        {
            try
            {
                DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@SDate", sDate.ToString("yyyyMMdd", new CultureInfo("en-US")));

                string sql = "proc_RTD_Receive_GetDataTable";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetConfirmData(DateTime minDate, string whids)
        {
            try
            {
                DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@SDate", minDate.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@WHIDs", whids);

                string sql = "proc_RTD_Receive_ConfirmData";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public int ClearTLdata(string whid)
        {
            try
            {
                int ret = 0;

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@WHID", whid);

                string sql = "proc_clear_TL_data";

                ret = My_DataTable_Extensions.ExecuteSQLScalar(sql, CommandType.StoredProcedure, sqlParmas);

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return 0;
            }
        }

        public DataTable GetSendDataTableView()
        {
            DataTable dt = new DataTable();
            string sql = "select * from ReceiveTabletDataView AS t1 ORDER BY t1.WHID,t1.DateSend ASC";
            dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            return dt;
        }
        
        public DataTable GetSendDataTableViewLastest()
        {
            DataTable dt = new DataTable();
            string sql = "select * from ReceiveTabletDataView_Lastest AS t1 ORDER BY t1.WHID,t1.DateSend ASC";
            dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            return dt;
        }
        
        public int RemoveData(tbl_SendData tbl_SendData)
        {
            return tbl_SendData.Delete();
        }
        
        public int UpdateData(tbl_SendData tbl_SendData)
        {
            return tbl_SendData.Update();
        }

        public DataTable GetWHID_FromSendData()
        {
            return (new tbl_SendData()).GetWHID_FromSendData();
        }
    }
}
