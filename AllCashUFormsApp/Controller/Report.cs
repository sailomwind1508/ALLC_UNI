using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
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

    }
}
