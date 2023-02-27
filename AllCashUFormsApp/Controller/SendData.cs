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

        public DataTable GetSendDataTableView(DateTime sendDate)
        {
            DataTable dt = new DataTable();
            //string sql = "select * from ReceiveTabletDataView AS t1 ORDER BY t1.WHID,t1.DateSend ASC";

            Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
            sqlParmas.Add("@SendDate", sendDate.ToString("yyyyMMdd", new CultureInfo("en-US")));
            string sql = "proc_ReceiveTabletDataView"; //last edit by sailom.k 04/07/2022
            dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
            return dt;
        }
        
        public DataTable GetSendDataTableViewLastest()
        {
            DataTable dt = new DataTable();
            //string sql = "select * from ReceiveTabletDataView_Lastest AS t1 ORDER BY t1.WHID,t1.DateSend ASC";

            string sql = "proc_ReceiveTabletDataView_Lastest"; //last edit by sailom.k 04/07/2022
            dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);
            return dt;
        }

        public bool ManualUpdateSendDate(string whid, DateTime sendDate)
        {
            bool ret = false;
            //string sql = "select * from ReceiveTabletDataView_Lastest AS t1 ORDER BY t1.WHID,t1.DateSend ASC";

            Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
            sqlParmas.Add("@WHID", whid);
            sqlParmas.Add("@DateSend", sendDate.ToString("yyyyMMdd", new CultureInfo("en-US")));
            string sql = "proc_PreOrder_Update_SendDate"; //last edit by sailom.k 04/07/2022
            var dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
            if (dt != null && dt.Rows.Count > 0)
            {
                ret = dt.Rows[0][0].ToString() == "1" ? true : false;
            }

            return ret;
        }

        public int RemovePrepareSalesDateLog(string user)
        {
            int ret = 0;

            Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
            sqlParmas.Add("@User", user);

            string sql = "proc_ReceiveTabletData_WriteLog"; //last edit by sailom.k 06/12/2022
            var dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
            if (dt != null && dt.Rows.Count > 0)
            {
                ret = dt.Rows[0][0].ToString() == "1" ? 1 : 0;
            }

            return ret;
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

        public int ValidatePOMaster(string _DocDate, string _WHID)
        {
            return new tbl_POMaster().ValidatePOMaster(_DocDate, _WHID);
        }

        public List<tbl_SendData> GetSendDataSingle(string DateSend, string WHID)
        {
            return new tbl_SendData().GetSendDataSingle(DateSend, WHID);
        }

        public int UpdateSendData(string oldDocDate, string NewDocDate, string WHID, string UserName)
        {
            return new tbl_SendData().UpdateSendData(oldDocDate, NewDocDate, WHID, UserName);
        }

        public int UpdateInvMovementData(Dictionary<string, object> _params)
        {
            return new tbl_InvMovement().UpdateInvMovementData(_params);
        }
    }
}
