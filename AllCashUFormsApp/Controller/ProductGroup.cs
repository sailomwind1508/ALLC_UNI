using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class ProductGroup : BaseControl
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

        public ProductGroup() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        public List<tbl_ProductType> GetProductType(Func<tbl_ProductType, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ProductType().Select(condition);
            else
                return new tbl_ProductType().SelectAll();
        }

        public List<tbl_ProductGroup> GetProductGroupNonFlag(Func<tbl_ProductGroup, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ProductGroup().SelectNonFlag(condition);
            else
                return new tbl_ProductGroup().SelectAllOrderByProductGroupCode();
        }

        public List<tbl_ProductSubGroup> GetProductSubGroup(Func<tbl_ProductSubGroup, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ProductSubGroup().SelectNonFlag(condition);
            else
                return new tbl_ProductSubGroup().SelectAllNonFlag();
        }

        public DataTable GetProductGroupTable(int flagDel)
        {
            return new tbl_ProductGroup().GetProductGroupTable(flagDel);
        }

        public DataTable GetPrdGroupTable()
        {
            return new tbl_ProductGroup().GetPrdGroupTable();
        }

        public int UpdateData(tbl_ProductGroup tbl_ProductGroup)
        {
            return tbl_ProductGroup.Update();
        }

        public int UpdateData(tbl_ProductSubGroup tbl_ProductSubGroup)
        {
            return tbl_ProductSubGroup.Update();
        }
    }
}
