using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class TabletSales : BaseControl, IObject
    {
        private List<tbl_ArCustomerShelf> _tbl_ArCustomerShelfs = null;
        public virtual List<tbl_ArCustomerShelf> tbl_ArCustomerShelfs
        {
            get { return _tbl_ArCustomerShelfs; }
            set
            {
                _tbl_ArCustomerShelfs = value;
            }
        }

        public TabletSales() : base("IV")
        {
            this.tbl_IVMaster = new tbl_IVMaster();
            this.tbl_IVDetails = new List<tbl_IVDetail>();
        }

        public string GenerateRL(DateTime docdate, DateTime podate, string whid, string userName, string docNos)
        {
            string ret = "";
            try
            {
                DataTable newTable = new DataTable();
                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocDate", docdate.ToString("yyyyMMdd", new CultureInfo("en-US")));
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

        public string RecoveryPO(string docNo, string userName)
        {
            string ret = "";
            try
            {
                DataTable newTable = new DataTable();
                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocNo", docNo);
                sqlParmas.Add("@UserName", userName);

                string sql = "proc_tbl_POMaster_Recovery";

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

        public string GenerateCancelRL(DateTime docdate, string whid, string userName, string docNos)
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

                string sql = "proc_PreOrder_GenerateCanelRL";

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

        public bool UpdateCustomerAddress(string docNo)
        {
            try
            {
                bool ret = false;

                using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("proc_update_v_customer_address", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add(new SqlParameter("@DocNo", docNo));
                    var result = cmd.ExecuteNonQuery();
                    ret = true;

                    con.Close();

                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return false;
            }

        }

        public DataTable GetRefRB(string docNo)
        {
            DataTable ret = new DataTable();
            try
            {
                //Check Doc Pre Order
                DataTable newTable = new DataTable();

                string sql = "proc_PreOrder_GetRefRB";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@PODocNo", docNo);

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

        public List<tbl_SalArea> GetAllSaleArea()
        {
            return new tbl_SalArea().SelectAll();
        }

        public List<tbl_SalArea> GetAllSaleArea(List<string> listOfSalAreaID)
        {
            return new tbl_SalArea().Select(listOfSalAreaID);
        }

        public List<tbl_SalAreaDistrict> GetAllSaleAreaDistrict()
        {
            return new tbl_SalAreaDistrict().SelectAll();
        }

        public List<tbl_ArCustomer> GetCustomer(Func<tbl_ArCustomer, bool> predicate)
        {
            return new tbl_ArCustomer().SelectAll().Where(predicate).ToList();
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_POMaster> tbl_POMaster = new List<tbl_POMaster>();
                tbl_POMaster = (new tbl_POMaster()).Select(docTypepredicate);

                var docStatus = GetDocStatus();

                var allEmp = GetEmployee();

                DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();
                //newTable.Clear();

                newTable.Columns.Add("DocNo", typeof(string));
                newTable.Columns.Add("DocStatusImg", typeof(Bitmap));  //Type.GetType("System.Byte[]"));
                newTable.Columns.Add("DocStatus", typeof(string));
                newTable.Columns.Add("DocRef", typeof(string));
                newTable.Columns.Add("DocDate", typeof(DateTime));
                newTable.Columns.Add("SuppName", typeof(string));
                newTable.Columns.Add("CreditDay", typeof(short));
                newTable.Columns.Add("DueDate", typeof(DateTime));
                newTable.Columns.Add("TotalDue", typeof(decimal));
                newTable.Columns.Add("CrUser", typeof(string));
                newTable.Columns.Add("Remark", typeof(string));

                foreach (var r in tbl_POMaster)
                {
                    Bitmap closeImg = new Bitmap(Properties.Resources.power_off);
                    Bitmap cancelmg = new Bitmap(Properties.Resources.closeBtn);
                    Bitmap statusImg = r.DocStatus == "4" ? closeImg : cancelmg;

                    string docStatusName = docStatus.First(x => x.DocStatusCode == r.DocStatus).DocStatusName;
                    tbl_Employee emp = allEmp.FirstOrDefault(x => x.EmpID == r.EmpID);
                    string crUser = "";
                    if (emp != null)
                        crUser = string.Join(" ", emp.TitleName, emp.FirstName);

                    newTable.Rows.Add(r.DocNo, statusImg, docStatusName, r.DocRef, r.DocDate,
                        r.SuppName, r.CreditDay, r.DueDate, r.TotalDue, crUser, r.Remark);
                }

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public virtual DataTable GetDataTableByCondition(string[] filters)
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

        public int UpdateCustomerShelf()
        {
            List<int> ret = new List<int>();

            if (tbl_ArCustomerShelfs != null && tbl_ArCustomerShelfs.Count > 0)
            {
                foreach (var item in tbl_ArCustomerShelfs)
                {
                    ret.Add(item.Update());
                }
                return ret.All(x => x == 1) ? 1 : 0;
            }
            else
                return 1;
            
        }
    }
}
