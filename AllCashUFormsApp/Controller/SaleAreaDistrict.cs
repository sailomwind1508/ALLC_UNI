using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class SaleAreaDistrict : BaseControl, IObject
    {
        BaseControl b = new BaseControl("");
        private Func<tbl_PRMaster, bool> _docTypePredicate = null; //
        public virtual Func<tbl_PRMaster, bool> docTypePredicate //
        {
            get { return _docTypePredicate; }
            set
            {
                _docTypePredicate = value;
            }
        }
        public SaleAreaDistrict() : base("") //
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        //public DataTable GetSalAreaByWHID(int flagDel)
        //{
        //    return (new tbl_SalAreaDistrict()).GetSalAreaByWHID(flagDel);
        //}

        public List<tbl_SalAreaDistrict> GetAllData()
        {
            return (new tbl_SalAreaDistrict()).SelectAll();
        }

        public int AddData(tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            return tbl_SalAreaDistrict.Insert();
        }

        public int UpdateData(tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            return tbl_SalAreaDistrict.Update();
        }

        public int RemoveData(tbl_SalAreaDistrict tbl_SalAreaDistrict)
        {
            return tbl_SalAreaDistrict.Delete();
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            return new tbl_SalAreaDistrict().GetDataTable();
        }

        public virtual DataTable GetDataTableByCondition(string[] filters)
        {
            DataTable dt = new DataTable();
            List<tbl_SalAreaDistrict> tbl_SalAreaDistricts = new List<tbl_SalAreaDistrict>();

            if (filters != null)
            {

            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        public DataTable GetDataTable(Func<tbl_SalAreaDistrict, bool> predicate)
        {
            try
            {
                var query = (new tbl_SalAreaDistrict()).Select(predicate);

                DataTable newTable = new DataTable();
                newTable.Columns.Add("จังหวัด", typeof(string));
                newTable.Columns.Add("อำเภอ", typeof(string));
                newTable.Columns.Add("รหัสตำบล", typeof(string));
                newTable.Columns.Add("ชื่อตำบล", typeof(string));

                foreach (var r in query)
                {
                    newTable.Rows.Add(r.ProvinceName, r.AreaName, r.DistrictCode, r.DistrictName);
                }

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable SelectSingleDT(string _SalAreaID)
        {
            return new tbl_SalAreaDistrict().SelectSingleDT(_SalAreaID);
        }

        public DataTable GetDataTableByCondition(Func<tbl_SalAreaDistrict, bool> predicate)
        {
            DataTable dt = new DataTable();

            if (predicate != null)
            {
                dt = GetDataTable(predicate);
            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        public DataTable GetProvinceFromSalAreaDistrict()
        {
            return new tbl_SalAreaDistrict().GetProvinceFromSalAreaDistrict();
        }

        public DataTable GetSalAreaDistrictData(string _WHID = "")
        {
            return new tbl_SalAreaDistrict().GetSalAreaDistrictData(_WHID);
        }

        public int DeleteByWHID(string WHID)
        {
            return new tbl_SalAreaDistrict().DeleteByWHID(WHID);
        }

        public int InsertSalAreaDistrict(DataRow drs)
        {
            return new tbl_SalAreaDistrict().InsertSalAreaDistrict(drs);
        }

        public DataTable GetSalAreaDistrictByWHID(string _WHID = "")
        {
            return new tbl_SalAreaDistrict().GetSalAreaDistrictByWHID(_WHID);
        }

        public DataTable GetSalAreaDistrictByProvince(int _ProvinceID, string _WHID)
        {
            return new tbl_SalAreaDistrict().GetSalAreaDistrictByProvince(_ProvinceID, _WHID);
        }

        public DataTable GetSalAreaDistrict_BySendData()
        {
            return new tbl_SalAreaDistrict().GetSalAreaDistrict_BySendData();
        }
    }
}
