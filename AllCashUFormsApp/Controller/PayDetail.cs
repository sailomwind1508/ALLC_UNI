using AllCashUFormsApp.Dal;
using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class PayDetail : BaseControl
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

        public PayDetail() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        public List<tbl_PayDetail> GetAllPayDetail()
        {
            return new tbl_PayDetail().SelectAll();
        }

        public int UpdateData(tbl_PayDetail tbl_PayDetail)
        {
            return tbl_PayDetail.Update();
        }


    }
}
