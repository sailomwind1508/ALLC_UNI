using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class SaleArea : BaseControl, IObject //
    {
        private Func<tbl_PRMaster, bool> _docTypePredicate = null; //
        public virtual Func<tbl_PRMaster, bool> docTypePredicate //
        {
            get { return _docTypePredicate; }
            set
            {
                _docTypePredicate = value;
            }
        }

        public SaleArea() : base("") //
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        //public DataTable GetCustomerTypeGridData(int flagDel, string searchtext)
        //{
        //    return (new tbl_ArCustomerType()).GetCustomerTypeGirdData(flagDel, searchtext);
        //}

        public DataTable GetSalAreaData(int flagDel, string searchtext)
        {
            return (new tbl_SalArea()).GetSalAreaData(flagDel, searchtext);
        }

        public List<tbl_SalArea> GetAllData()
        {
            return (new tbl_SalArea()).SelectAll();
        }

        public int AddData(tbl_SalArea tbl_SalArea)
        {
            return tbl_SalArea.Insert();
        }

        public int UpdateData(tbl_SalArea tbl_SalArea)
        {
            return tbl_SalArea.Update();
        }

        public int RemoveData(tbl_SalArea tbl_SalArea)
        {
            return tbl_SalArea.Delete();
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_SalArea> tbl_SalAreas = new List<tbl_SalArea>();
                tbl_SalAreas = (new tbl_SalArea()).SelectAll();

                return tbl_SalAreas.ToDataTable();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public virtual DataTable GetDataTableByCondition(string[] filters)
        {
            DataTable dt = new DataTable();
            List<tbl_SalArea> tbl_SalAreas = new List<tbl_SalArea>();

            if (filters != null)
            {

            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        //public DataTable VanTypeList()
        //{
        //    return new tbl_VanType().VanTypeList();
        //}

        public DataTable proc_GetMarketData(Dictionary<string, object> _params)
        {
            return new tbl_SalArea().proc_GetMarketData(_params);
        }

        public List<tbl_SalArea> GetSalAreaByWHID(string _WHID)
        {
            return new tbl_SalArea().GetSalAreaByWHID(_WHID);
        }
    }
}