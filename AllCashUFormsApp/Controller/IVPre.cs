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
    public class IVPre : BaseControl, IObject
    {
        private Func<tbl_POMaster, bool> _ivDocTypePredicate = null;
        public virtual Func<tbl_POMaster, bool> ivDocTypePredicate
        {
            get { return _ivDocTypePredicate; }
            set
            {
                _ivDocTypePredicate = value;
            }
        }

        public IVPre() : base("IVPre")
        {
            _ivDocTypePredicate = (x => x.DocTypeCode.Trim() == "IVPre" && string.IsNullOrEmpty(x.DocRef)); // && x.DocRef.Trim() != "IM");
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();

                string sql = "proc_IVPre_GetDataTable";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql);

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
