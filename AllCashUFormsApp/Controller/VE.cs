using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class VE : BaseControl, IObject
    {
        private Func<tbl_IVMaster, bool> _ivDocTypePredicate = null;
        public virtual Func<tbl_IVMaster, bool> ivDocTypePredicate
        {
            get { return _ivDocTypePredicate; }
            set
            {
                _ivDocTypePredicate = value;
            }
        }

        public VE() : base("V")
        {
            _ivDocTypePredicate = (x => x.DocTypeCode.Trim() == "V");
        }

        public bool UpdateCustomerAddress(string docNo, bool isVE = false)
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

                    if (isVE)
                    {
                        cmd = new SqlCommand("proc_update_ve_customer_address", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add(new SqlParameter("@DocNo", docNo));
                        result = cmd.ExecuteNonQuery();
                    }

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

        public List<tbl_IVMaster> GetInvVEMaster(string docNo)
        {
            //Func<tbl_IVMaster, bool> tbl_IVMasterPre = (x => x.DocNo == docNo);
            //return new tbl_IVMaster().Select(tbl_IVMasterPre);
            return new tbl_IVMaster().SelectWithDocNo(docNo);
        }

        public List<tbl_IVDetail> GetInvVEDetails(string docNo)
        {
            //Func<tbl_IVDetail, bool> tbl_IVDetailPre = (x => x.DocNo == docNo);
            //return new tbl_IVDetail().Select(tbl_IVDetailPre);
            return new tbl_IVDetail().SelectWithDocNo(docNo);
        }

        public List<tbl_SalArea> GetAllSaleArea()
        {
            return new tbl_SalArea().SelectAll();
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
                DataTable newTable = new DataTable();
                string sql = "proc_V_GetDataTable";
                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                //List<tbl_IVMaster> tbl_IVMasters = new List<tbl_IVMaster>();
                //tbl_IVMasters = (new tbl_IVMaster()).SelectAll().Where(x => x.DocTypeCode.Trim() == "V").ToList();

                //var docStatus = GetDocStatus();

                //var allEmp = GetEmployee();
                //var allCust = GetCustomer();

                //DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();

                //newTable.Columns.Add("DocNo", typeof(string));
                //newTable.Columns.Add("DocStatusImg", typeof(Bitmap));
                //newTable.Columns.Add("DocStatus", typeof(string));
                //newTable.Columns.Add("DocDate", typeof(DateTime));
                //newTable.Columns.Add("CustomerID", typeof(string));
                //newTable.Columns.Add("CustomerName", typeof(string));
                //newTable.Columns.Add("WHID", typeof(string));
                //newTable.Columns.Add("SaleEmpID", typeof(string));
                //newTable.Columns.Add("TotalDue", typeof(decimal));
                //newTable.Columns.Add("CrUser", typeof(string));
                //newTable.Columns.Add("Remark", typeof(string));

                //foreach (var r in tbl_IVMasters)
                //{
                //    Bitmap closeImg = new Bitmap(Properties.Resources.power_off);
                //    Bitmap cancelmg = new Bitmap(Properties.Resources.closeBtn);
                //    Bitmap statusImg = r.DocStatus == "4" ? closeImg : cancelmg;

                //    string docStatusName = docStatus.First(x => x.DocStatusCode == r.DocStatus).DocStatusName;
                //    tbl_Employee emp = allEmp.FirstOrDefault(x => x.EmpID == r.EmpID);
                //    string crUser = "";
                //    if (emp != null)
                //        crUser = string.Join(" ", emp.TitleName, emp.FirstName);

                //    string customerName = "";

                //    var cust = new List<tbl_ArCustomer>();
                //    var tmpCust = allCust.FirstOrDefault(x => x.CustomerID.Trim() == r.CustomerID.Trim());

                //    if (tmpCust != null)
                //        cust.Add(tmpCust);
                //    else
                //        cust = new tbl_ArCustomer().SelectSingle(r.CustomerID.Trim());

                //    if (cust != null && cust.Count > 0)
                //    {
                //        customerName = string.Join(" ", cust[0].CustTitle, cust[0].CustName);
                //    }
                //    else
                //    {
                //        emp = allEmp.FirstOrDefault(x => x.EmpID == r.SaleEmpID); //GetEmployee(r.SaleEmpID);
                //        if (emp != null)
                //        {
                //            r.CustomerID = r.EmpID;
                //            customerName = string.Join(" ", emp.TitleName, emp.FirstName);
                //        }
                //    }

                //    newTable.Rows.Add(r.DocNo, statusImg, docStatusName, r.DocDate,
                //        r.CustomerID, customerName, r.WHID, r.SaleEmpID, r.TotalDue, crUser, r.Remark);
                //}

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

    }
}
