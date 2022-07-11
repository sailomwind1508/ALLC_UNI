using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AllCashUFormsApp.Dal;
using AllCashUFormsApp.Model;
namespace AllCashUFormsApp.Controller
{
    public class CustomerShelf : BaseControl
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

        public CustomerShelf() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        public List<tbl_ArCustomerShelf> GetCustShelf(Func<tbl_ArCustomerShelf,bool> func)
        {
            return (new tbl_ArCustomerShelf()).Select(func);
        }

        public List<tbl_ArCustomerShelf> GetCustShelf()
        {
            return (new tbl_ArCustomerShelf()).SelectAll();
        }

        public int UpdateData(tbl_ArCustomerShelf tbl_ArCustomerShelf)
        {
            return tbl_ArCustomerShelf.Update();
        }

        public int RemoveData(tbl_ArCustomerShelf tbl_ArCustomerShelf)
        {
            return tbl_ArCustomerShelf.Delete();
        }

        public DataTable GetCustomerShelfData(string Search, int flagDel, string WHID)
        {
            return new tbl_ArCustomerShelf().GetCustomerShelfData(Search, flagDel, WHID);
        }

        public int UpdateCustomerShelf(tbl_ArCustomerShelf tbl_ArCustomerShelf)
        {
            return tbl_ArCustomerShelf.UpdateCustomerShelf();
        }
    }
}
