using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class SaleArea : IObject
    {
        BaseControl b = new BaseControl("");

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
    }
}