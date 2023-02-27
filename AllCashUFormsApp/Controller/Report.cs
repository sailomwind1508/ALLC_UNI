using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class Report : BaseControl
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

        public Report() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }


        public bool CheckBranchCycle(string branchID)
        {
            bool ret = false; //true = no send, false = sent
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_Check_Branch_Cycle";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@BranchID", branchID);

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    ret = Convert.ToBoolean(newTable.Rows[0][0]);
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return false;
            }
        }

        public int GetCycleNo(DateTime custDate)
        {
            int ret = 0; 
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_Get_CycleNo";

                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@SDate", custDate.ToString("yyyyMMdd", new CultureInfo("en-US")));

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
    }
}
