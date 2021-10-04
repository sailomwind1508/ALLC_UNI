using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AllCashUFormsApp.Model;//
namespace AllCashUFormsApp.Controller
{
    public class PriceGroup : BaseControl , IObject
    {
        public DataTable GetPriceGroupGridData(string searchtext, int flagDel)
        {
            return (new tbl_PriceGroup()).GetPriceGroupDataGridView(searchtext, flagDel);
        }
        public List<tbl_PriceGroup> GetPriceGroupAllData()
        {
            return (new tbl_PriceGroup()).SelectAll();
        }

        public virtual int AddData(tbl_PriceGroup tbl_PriceGroup)
        {
            return tbl_PriceGroup.Insert();
        }
        public int UpdateData(tbl_PriceGroup tbl_PriceGroup)
        {
            return tbl_PriceGroup.Update();
        }

        public int RemoveData(tbl_PriceGroup tbl_PriceGroup)
        {
            return tbl_PriceGroup.Delete();
        }

        public DataTable GetDataTable(bool isPopup = true)
        {
            throw new NotImplementedException();
        }

        public DataTable GetDataTableByCondition(string[] filters)
        {
            throw new NotImplementedException();
        }
        private Func<tbl_PRMaster, bool> _docTypePredicate = null;
        public virtual Func<tbl_PRMaster, bool> docTypePredicate
        {
            get { return _docTypePredicate; }
            set
            {
                _docTypePredicate = value;
            }
        }
        public PriceGroup() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

    }
}
