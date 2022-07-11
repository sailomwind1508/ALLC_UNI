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

        public DataTable GetPOMstData(string docNo, DateTime docdate, string whid, string docStatus)
        {
            return POMaster_PREDao.GetPOMstData(docNo, docdate, whid, docStatus);
        }

        public DataTable GetPOData(string docNo, DateTime docdate, string whid)
        {
            return POMaster_PREDao.GetPOData(docNo, docdate, whid);
        }

        public DataTable UpdateRL(string docNo, DateTime docdate, string user, string reason)
        {
            DataTable ret = new DataTable();
            try
            {
                //Check Doc Pre Order
                DataTable newTable = new DataTable();

                string sql = "proc_PreOrder_UpdateRL";
                
                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@PODocNo", docNo);
                sqlParmas.Add("@DocDate", docdate.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@LoginUser", user);
                sqlParmas.Add("@Reason", reason);

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    ret = newTable;
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GenerateRB(string docNo, string user)
        {
            DataTable ret = new DataTable();
            try
            {
                //Check Doc Pre Order
                DataTable newTable = new DataTable();

                string sql = "proc_PreOrder_GenerateRB";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@PODocNo", docNo);
                sqlParmas.Add("@LoginUser", user);

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    ret = newTable;
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }


        public bool RollbackOrder(string whid, DateTime docdate, string rlDocNo)
        {
            string ret = "";
            try
            {
                DataTable dt = new DataTable();

                string sql = "proc_PreOrder_RollBackConfirmOrder";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@WHID", whid);
                sqlParmas.Add("@DocDate", docdate.ToString("yyyyMMdd", new CultureInfo("en-US")));
                sqlParmas.Add("@RLDocNo", rlDocNo);

                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ret = dt.Rows[0]["Result"].ToString();
                }

                return (!string.IsNullOrEmpty(ret) && ret == "1") ? true : false;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return false;
            }
        }

        public DateTime GetDefaultDocDate()
        {
            DateTime ret = new DateTime();
            try
            {
                DataTable newTable = new DataTable();

                string sql = "SELECT MAX(DocDate) AS DocDate FROM dbo.tbl_POMaster_PRE WHERE DocStatus IN (3, 4)";

                newTable = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    ret = Convert.ToDateTime(newTable.Rows[0]["DocDate"]);
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return new DateTime();
            }
        }

        public DataTable GetSignaturePicture(string docNo, bool isPreOrder)
        {
            DataTable ret = new DataTable();
            try
            {
                //Check Doc Pre Order
                DataTable newTable = new DataTable();

                string sql = "";
                if (isPreOrder)
                    sql = "proc_PreOrder_GetSignaturePicture_PRE";
                else
                    sql = "proc_PreOrder_GetSignaturePicture";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocNo", docNo);

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    ret = newTable;
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public string FilterDocNoWithAutoGen(string docNos)
        {
            string ret = "";
            try
            {
                //Check Doc Pre Order
                DataTable newTable = new DataTable();

                string sql = "proc_PreOrder_FilterAutoGenDocNo";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocNos", docNos);

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    List<string> listDocNo = new List<string>();
                    docNos = "";
                    foreach (DataRow row in newTable.Rows)
                    {
                        listDocNo.Add(row["DocNo"].ToString());
                    }

                    docNos = string.Join(",", listDocNo);
                    ret = docNos;
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return "";
            }
        }

        public string FilterDocNoWithAutoGenByPRD(string docNo, string whid, string productID, int productUomID, decimal orderQty)
        {
            string ret = "";
            try
            {
                //Check Doc Pre Order
                DataTable newTable = new DataTable();

                string sql = "proc_PreOrder_FilterAutoGenDocNo_ByPRD";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocNo", docNo);
                sqlParmas.Add("@WHID", whid);
                sqlParmas.Add("@ProductID", productID);
                sqlParmas.Add("@ProductUomID", productUomID);
                sqlParmas.Add("@OrderQty", orderQty);

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    List<string> listDocNo = new List<string>();
                    docNo = "";
                    foreach (DataRow row in newTable.Rows)
                    {
                        listDocNo.Add(row["DocNo"].ToString());
                    }

                    docNo = string.Join(",", listDocNo);
                    ret = docNo;
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return "";
            }
        }

        public int TransferPreOrderToPO(DateTime docdate, string userName, string docNos)
        {
            int ret = 0;
            try
            {
                var newTable = new DataTable();
                var sqlParmas = new Dictionary<string, object>();
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

        public string GenerateRL(DateTime podate, string whid, string userName, string docNos)
        {
            string ret = "";
            try
            {
                DataTable newTable = new DataTable();
                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                //sqlParmas.Add("@DocDate", docdate.ToString("yyyyMMdd", new CultureInfo("en-US"))); //for support pre-order abd change RL date = po date Last edit  by sailom .k 05/04/2022 
                sqlParmas.Add("@PODate", podate.ToString("yyyyMMdd", new CultureInfo("en-US")));
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
