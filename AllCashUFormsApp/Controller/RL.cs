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
    public class RL : BaseControl, IObject
    {
        private Func<tbl_PRMaster, bool> _rlDocTypePredicate = null;
        public virtual Func<tbl_PRMaster, bool> rlDocTypePredicate
        {
            get { return _rlDocTypePredicate; }
            set
            {
                _rlDocTypePredicate = value;
            }
        }

        public RL() : base("RL")
        {
            _rlDocTypePredicate = (x => x.DocTypeCode.Trim() == "RL");
        }

        public bool ManualUpdateInvWarehouse(DateTime docDate)
        {
            try
            {
                bool ret = false;
                int checkManualUpdate = 0;

                try
                {
                    string checkDate = DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US"));
                    DataTable dt = new DataTable();
                    string sql = "";
                    sql += "SELECT COUNT(*) AS CountRow FROM tbl_UpdateInvWH WHERE UpdateInvWarehouseFlag = 1 AND CAST(EdDate as DATE) = '" + checkDate + "'";

                    SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                    da.Fill(dt);

                    if (dt != null && dt.Rows.Count > 0)
                        checkManualUpdate = Convert.ToInt32(dt.Rows[0][0]);

                }
                catch (Exception ex)
                {
                    ex.WriteLog(this.GetType());
                }

                if (checkManualUpdate == 0)
                {
                    using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("proc_tbl_InvWarehouses_RL_Schedule_update", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add(new SqlParameter("@DocDate", docDate));
                        var result = cmd.ExecuteNonQuery();
                        ret = true;

                        cmd = new SqlCommand("proc_tbl_UpdateInvWH_update", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add(new SqlParameter("@User", Helper.tbl_Users.Username));
                        cmd.ExecuteNonQuery();
                        ret = true;

                        con.Close();

                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return false;
            }

        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_RL_GetDataTable";
                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

                //List<tbl_PRMaster> tbl_PRMaster = new List<tbl_PRMaster>();
                //tbl_PRMaster = (new tbl_PRMaster()).Select(rlDocTypePredicate);

                //var docStatus = GetDocStatus();

                //var allEmp = GetEmployee();
                //var allBranch = GetBranch();

                //DataTable newTable = new DataTable();
                //newTable.Columns.Add("DocNo", typeof(string));
                //newTable.Columns.Add("DocStatusImg", typeof(Bitmap));
                //newTable.Columns.Add("DocStatus", typeof(string));
                //newTable.Columns.Add("WHID", typeof(string));
                //newTable.Columns.Add("DocRef", typeof(string));
                //newTable.Columns.Add("DocDate", typeof(DateTime));
                //newTable.Columns.Add("SuppName", typeof(string));
                //newTable.Columns.Add("CreditDay", typeof(short));
                //newTable.Columns.Add("DueDate", typeof(DateTime));
                //newTable.Columns.Add("TotalDue", typeof(decimal));
                //newTable.Columns.Add("CrUser", typeof(string));
                //newTable.Columns.Add("Remark", typeof(string));

                //foreach (var r in tbl_PRMaster)
                //{
                //    Bitmap closeImg = new Bitmap(Properties.Resources.power_off);
                //    Bitmap cancelmg = new Bitmap(Properties.Resources.closeBtn);
                //    Bitmap inProcessmg = new Bitmap(Properties.Resources.timeBtn);
                //    Bitmap statusImg = null;
                //    if (r.DocStatus == "4")
                //    {
                //        statusImg = closeImg;
                //    }
                //    else if (r.DocStatus == "5")
                //    {
                //        statusImg = cancelmg;
                //    }
                //    else if (r.DocStatus == "3")
                //    {
                //        statusImg = inProcessmg;
                //    }

                //    string docStatusName = docStatus.First(x => x.DocStatusCode == r.DocStatus).DocStatusName;

                //    tbl_Employee emp = allEmp.FirstOrDefault(x => x.EmpID == r.EmpID);
                //    string crUser = "";
                //    if (emp != null)
                //        crUser = string.Join(" ", emp.TitleName, emp.FirstName);

                //    string SuppName = "";
                //    short CreditDay = 0;
                //    DateTime DueDate = DateTime.MinValue;
                //    decimal TotalDue = 0;

                //    var branch = allBranch;
                //    if (branch != null && branch.Count > 0)
                //    {
                //        SuppName = branch.FirstOrDefault(x => x.BranchCode == r.FromBranchID).BranchName;
                //    }

                //    newTable.Rows.Add(r.DocNo, statusImg, docStatusName, r.ToWHID, r.DocRef, r.DocDate,
                //        SuppName, CreditDay, DueDate, TotalDue, crUser, r.Remark);
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

        public virtual DataTable proc_GetRLData(Dictionary<string, object> _params, bool flagAllWHID = false)
        {
            try
            {
                DataTable newTable = new DataTable();
                string sql = "";

                if (flagAllWHID == true)
                    sql = "Form_RL_PRE_WH";
                else
                    sql = "proc_PreOrder_GetRLData";

                //string sql = "proc_PreOrder_GetRLData_Test";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);
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
