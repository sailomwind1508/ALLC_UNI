using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class Province : BaseControl, IObject // สร้าง DropDown ต้อง มี BaseControl
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
        public Province() : base("")   //
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }
        public List<tbl_MstDistrict> GetAllData()
        {
            return (new tbl_MstDistrict()).SelectAll();
        }

        public virtual int AddData(tbl_MstDistrict tbl_MstDistrict)
        {
            return tbl_MstDistrict.Insert();
        }

        public int UpdateData(tbl_MstArea tbl_MstArea)
        {
            return tbl_MstArea.Update();
        }

        public int UpdateData(tbl_MstProvince tbl_MstProvince)
        {
            return tbl_MstProvince.Update();
        }

        public int UpdateData(tbl_MstDistrict tbl_MstDistrict)
        {
            return tbl_MstDistrict.Update();
        }

        public int RemoveData(tbl_MstDistrict tbl_MstDistrict)
        {
            return tbl_MstDistrict.Delete();
        }

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                var query = (new tbl_MstDistrict()).SelectAll();

                DataTable newTable = new DataTable();
                newTable.Columns.Add("DistrictCode", typeof(string));
                newTable.Columns.Add("DistrictCode", typeof(string));

                foreach (var r in query)
                {
                    newTable.Rows.Add(r.DistrictCode, r.DistrictName);
                }

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetDataTable(Func<tbl_MstDistrict, bool> predicate)
        {
            try
            {
                var query = (new tbl_MstDistrict()).Select(predicate);

                DataTable newTable = new DataTable();
                newTable.Columns.Add("DistrictCode", typeof(string));
                newTable.Columns.Add("DistrictCode", typeof(string));

                foreach (var r in query)
                {
                    newTable.Rows.Add(r.DistrictCode, r.DistrictName);
                }

                return newTable;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetDataTableByCondition(Func<tbl_MstDistrict, bool> predicate)
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

        public virtual DataTable GetDataTableByCondition(string[] filters)
        {
            DataTable dt = new DataTable();
            List<tbl_MstDistrict> tbl_MstDistricts = new List<tbl_MstDistrict>();

            if (filters != null)
            {

            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        public DataTable GetProvinceTable(int flagDel, string Text)
        {
            return new tbl_MstProvince().GetProvinceTable(flagDel, Text);
        }

        public DataTable GetAreaTable(int flagDel, int provinceID, string Text)
        {
            return new tbl_MstArea().GetAreaTable(flagDel, provinceID, Text);
        }

        public DataTable GetDistrictTable(int flagDel, int AreaID, string Text)
        {
            return new tbl_MstDistrict().GetDistrictTable(flagDel, AreaID, Text);
        }

        public DataTable GetProvinceFromSalAreaDistrict()
        {
            return new tbl_MstProvince().GetProvinceFromSalAreaDistrict();
        }

    }
}
