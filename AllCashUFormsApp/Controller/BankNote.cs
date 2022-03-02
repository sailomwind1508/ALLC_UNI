using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AllCashUFormsApp.Dal;
namespace AllCashUFormsApp.Controller
{
    public class BankNote : BaseControl
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
        public BankNote() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        public DataTable GetBankNoteTable(DateTime dtDocDate)
        {
            return (new V_BankNote()).GetBankNoteTable(dtDocDate);
        }

        public List<tbl_PayMaster> GetAllPayMaster()
        {
            return new tbl_PayMaster().SelectAll();
        }

        public int UpdateDataPayMaster(List<tbl_PayMaster> tbl_PayMasters)
        {
            return tbl_PayMasters.Update();
        }

        public int UpdateDataPayMaster(tbl_PayMaster tbl_PayMaster)
        {
            return tbl_PayMaster.Update();
        }

        public List<tbl_PayDetail> GetAllPayDetail()
        {
            return new tbl_PayDetail().SelectAll();
        }

        public int UpdateDataPayDetail(List<tbl_PayDetail> tbl_PayDetails)
        {
            return tbl_PayDetails.Update();
        }

        public int RemoveDataPayDetail(List<tbl_PayDetail> tbl_PayDetails)
        {
            List<int> rets = new List<int>();
            foreach (tbl_PayDetail item in tbl_PayDetails)
            {
                rets.Add(item.Delete());
            }

            return rets.All(x => x == 1) ? 1 : 0;
        }

    }
}
