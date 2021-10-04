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
    public class PreOrder : BaseControl, IObject
    {
        private Func<tbl_POMaster_PRE, bool> _ivDocTypePredicate = null;

        public PreOrder() : base("")
        {
            this.tbl_IVMaster = new tbl_IVMaster();
            this.tbl_IVDetails = new List<tbl_IVDetail>();

            _ivDocTypePredicate = (x => x.DocTypeCode.Trim() == "IV");
        }

        public DataTable GetStockData(string branchID, DateTime docdate, bool currentStock)
        {
            return POMaster_PREDao.GetStockData(branchID, docdate, currentStock);
        }

        public DataTable GetPOMstData(string docNo, DateTime docdate, string whid)
        {
            return POMaster_PREDao.GetPOMstData(docNo, docdate, whid);
        }

        public DataTable GetPOData(string docNo, DateTime docdate, string whid)
        {
            return POMaster_PREDao.GetPOData(docNo, docdate, whid);
        }

        public int TransferPreOrderToPO(DateTime docdate, string userName, string docNos)
        {
            int ret = 0;
            try
            {
                DataTable newTable = new DataTable();
                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocDate", docdate.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@LoginUser", userName);
                sqlParmas.Add("@DocNos", docNos);

                string sql = "proc_PreOrder_TransferPreOrderToPO";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    ret = Convert.ToInt32(newTable.Rows[0][0]);
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return 0;
            }
        }

        public string GenerateRL(DateTime docdate, string whid, string userName, string docNos)
        {
            string ret = "";
            try
            {
                DataTable newTable = new DataTable();
                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocDate", docdate.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@WHID", whid);
                sqlParmas.Add("@LoginUser", userName);
                sqlParmas.Add("@DocNos", docNos);

                string sql = "proc_PreOrder_GenerateRL";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    ret = newTable.Rows[0][0].ToString();
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return "";
            }
        }

        public DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();

                string sql = "proc_PreOrder_GetDataTable";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetDataTableByCondition(string[] filters)
        {
            DataTable dt = new DataTable();

            if (filters != null)
            {

            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }
    }
}
