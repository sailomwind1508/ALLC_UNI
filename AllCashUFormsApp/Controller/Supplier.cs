using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class Supplier : IObject
    {
        public List<tbl_ApSupplier> GetAllData()
        {
           return (new tbl_ApSupplier()).SelectAll();
        }

        public int AddData(tbl_ApSupplier tbl_ApSupplier)
        {
            return tbl_ApSupplier.Insert();
        }

        public int UpdateData(tbl_ApSupplier tbl_ApSupplier)
        {
            return tbl_ApSupplier.Update();
        }

        public int RemoveData(tbl_ApSupplier tbl_ApSupplier)
        {
            return tbl_ApSupplier.Delete();
        }
        public DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                return new tbl_ApSupplier().GetDataTable();
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
            List<tbl_ApSupplier> tbl_ApSuppliers = new List<tbl_ApSupplier>();

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
