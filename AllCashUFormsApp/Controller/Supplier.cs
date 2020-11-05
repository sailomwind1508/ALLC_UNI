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

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_ApSupplier> tbl_ApSuppliers = new List<tbl_ApSupplier>();
                tbl_ApSuppliers = (new tbl_ApSupplier()).SelectAll();

                List<tbl_ApSupplierType> tbl_ApSupplierTypes = new List<tbl_ApSupplierType>();
                tbl_ApSupplierTypes = (new tbl_ApSupplierType()).SelectAll();

                var query = from s in tbl_ApSuppliers
                            join st in tbl_ApSupplierTypes on s.SupplierTypeID equals st.APSupplierTypeID
                            select new
                            {
                                SupplierCode = s.SupplierCode,
                                SuppName = s.SuppName,
                                SupplierRefCode = s.SupplierRefCode,
                                ApSupplierTypeName = st.ApSupplierTypeName
                            };

                DataTable newTable = query.ToList().ToDataTable();
                newTable.Clear();
                //newTable.Columns.Add("SupplierCode", typeof(string));
                //newTable.Columns.Add("SuppName", typeof(string));
                //newTable.Columns.Add("SupplierRefCode", typeof(string));
                //newTable.Columns.Add("ApSupplierTypeName", typeof(string));

                foreach (var rowInfo in query)
                {
                    newTable.Rows.Add(rowInfo.SupplierCode, rowInfo.SuppName, rowInfo.SupplierRefCode, rowInfo.ApSupplierTypeName);
                }

                return newTable;
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
