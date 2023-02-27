using AllCashUFormsApp.Dal;
using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class PayMaster : BaseControl
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

        public PayMaster() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        public List<tbl_PayMaster> GetAllPayMaster()
        {
            return new tbl_PayMaster().SelectAll();
        }

        public int UpdateData(tbl_PayMaster tbl_PayMaster)
        {
            return tbl_PayMaster.Update();
        }


    }
}
