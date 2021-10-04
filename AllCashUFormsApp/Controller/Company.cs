using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class Company : BaseControl
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
        public Company() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }
        public DataTable GetCompanyTable()
        {
            return (new tbl_Company()).GetCompanyTable();
        }
        public DataTable GetCfgSettingData()
        {
            return new tbl_CfgSetting().GetCfgSettingData();
        }
        public int UpdateData(tbl_Company tbl_Company)
        {
            return tbl_Company.Update();
        }
        public int UpdateData(tbl_CfgSetting tbl_CfgSetting)
        {
            return tbl_CfgSetting.Update();
        }
    }
}
