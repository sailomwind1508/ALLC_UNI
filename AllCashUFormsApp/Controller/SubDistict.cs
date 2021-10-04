using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class SubDistict : IObject
    {
        public List<tbl_MstDistrict> GetAllData()
        {
            return (new tbl_MstDistrict()).SelectAll();
        }


        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_MstDistrict> tbl_MstDistricts = new List<tbl_MstDistrict>();
                tbl_MstDistricts = (new tbl_MstDistrict()).SelectAll();

                return tbl_MstDistricts.ToDataTable();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public virtual DataTable GetDataTableByCondition(Func<tbl_MstDistrict, bool> predicate)
        {
            DataTable dt = new DataTable();
            List<tbl_MstDistrict> tbl_MstDistricts = new List<tbl_MstDistrict>();

            if (predicate != null)
            {
                tbl_MstDistricts = (new tbl_MstDistrict()).Select(predicate).ToList();
                dt = tbl_MstDistricts.ToDataTable();
            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        public DataTable GetDataTableByCondition(string[] filters)
        {
            throw new NotImplementedException();
        }
    }
}
