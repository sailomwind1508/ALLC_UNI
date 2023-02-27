using AllCashUFormsApp.Dal;
using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class ArCustomerType : BaseControl
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
        public ArCustomerType() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }
        public DataTable GetCustomerTypeGridData(int flagDel, string searchtext)
        {
            return (new tbl_ArCustomerType()).GetCustomerTypeGirdData(flagDel, searchtext);
        }
        public virtual int AddData(tbl_ArCustomerType tbl_ArCustomerType)
        {
            return tbl_ArCustomerType.Insert();
        }
        public int UpdateData(tbl_ArCustomerType tbl_ArCustomerType)
        {
            return tbl_ArCustomerType.Update();
        }
        public int RemoveData(tbl_ArCustomerType tbl_ArCustomerType)
        {
            return tbl_ArCustomerType.Delete();
        }
        public List<tbl_ArCustomerType> GetTbl_ArCustomerTypes()
        {
            return null;
        }
    }
}


