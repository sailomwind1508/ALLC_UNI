using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class SaleAreaDistrict : IObject
    {
        BaseControl b = new BaseControl("");

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
            try
            {
                DataTable _dt = new DataTable("DistrictTable");
                _dt.Columns.Add("ProvinceName", typeof(string));
                _dt.Columns.Add("AreaName", typeof(string));
                _dt.Columns.Add("DistrictCode", typeof(string));
                _dt.Columns.Add("DistrictName", typeof(string));

                var saleAreaDistricts = b.GetSaleAreaDistrict();
                if (saleAreaDistricts != null && saleAreaDistricts.Count > 0)
                {
                    List<string> provinceList = saleAreaDistricts.Select(x => x.ProvinceName).Distinct().ToList();

                    Func<tbl_MstProvince, bool> tbl_MstProvinceFunc = (x => provinceList.Contains(x.ProvinceName));
                    var provices = b.GetMstProvince(tbl_MstProvinceFunc);
                    var provinceIDList = provices.Select(x => x.ProvinceID).ToList();

                    Func<tbl_MstArea, bool> tbl_MstAreaFunc = (x => provinceIDList.Contains(x.ProvinceID.Value));
                    var areas = b.GetMstArea(tbl_MstAreaFunc);
                    var areaIDList = areas.Select(x => x.AreaID).ToList();

                    Func<tbl_MstDistrict, bool> tbl_MstDistrictFunc = (x => areaIDList.Contains(x.AreaID.Value));
                    var districts = b.GetMstDistrict(tbl_MstDistrictFunc);

                    var query = from p in provices
                                join a in areas on p.ProvinceID equals a.ProvinceID
                                join d in districts on a.AreaID equals d.AreaID
                                select new
                                {
                                    ProvinceName = p.ProvinceName,
                                    AreaName = a.AreaName,
                                    DistrictCode = d.DistrictCode,
                                    DistrictName = d.DistrictName
                                };
                    foreach (var item in query)
                    {
                        _dt.Rows.Add(item.ProvinceName, item.AreaName, item.DistrictCode, item.DistrictName);
                    }
                }

                return _dt;
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
    }
}
