using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class ShopType : BaseControl   //create method line 22 
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
        public ShopType() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }
        //create DAO Before 
        public DataTable GetShopTypeGridData(int flagDel,string searchtext)
        {
            return (new tbl_ShopType()).GetShopTypeGirdData(flagDel,searchtext);
        }
        public int UpdateData(tbl_ShopType tbl_ShopType)
        {
            return tbl_ShopType.Update();
        }

    }
}
