using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class RE : BaseControl, IObject
    {
        public RE() : base("RE")
        {

        }

        //public Dictionary<int, string> GetProductUOM(string productID)
        //{
        //    try
        //    {
        //        Dictionary<int, string> ret = new Dictionary<int, string>();

        //        List<tbl_ProductPriceGroup> tbl_ProductPriceGroup = new List<tbl_ProductPriceGroup>();
        //        Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductID == productID);
        //        tbl_ProductPriceGroup = (new tbl_ProductPriceGroup()).Select(tbl_ProductPriceGroupPre);

        //        List<tbl_ProductUom> tbl_ProductUom = new List<tbl_ProductUom>();
        //        Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => tbl_ProductPriceGroup.Select(p => p.ProductUomID).Contains(x.ProductUomID));
        //        tbl_ProductUom = (new tbl_ProductUom()).Select(tbl_ProductUomPre).ToList();

        //        foreach (var item in tbl_ProductUom)
        //        {
        //            ret.Add(item.ProductUomID, item.ProductUomName);
        //        }

        //        return ret;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.WriteLog(this.GetType());
        //        return null;
        //    }
        //}

        public virtual DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_POMaster> tbl_POMaster = new List<tbl_POMaster>();
                tbl_POMaster = (new tbl_POMaster()).Select(docTypepredicate);

                var docStatus = GetDocStatus();
                var allEmp = GetEmployee();

                DataTable newTable = new DataTable(); // tbl_POMaster.ToDataTable();
                //newTable.Clear();

                newTable.Columns.Add("DocNo", typeof(string));
                newTable.Columns.Add("DocStatusImg", typeof(Bitmap));  //Type.GetType("System.Byte[]"));
                newTable.Columns.Add("DocStatus", typeof(string));
                newTable.Columns.Add("DocRef", typeof(string));
                newTable.Columns.Add("DocDate", typeof(DateTime));
                newTable.Columns.Add("SuppName", typeof(string));
                newTable.Columns.Add("CreditDay", typeof(short));
                newTable.Columns.Add("DueDate", typeof(DateTime));
                newTable.Columns.Add("TotalDue", typeof(decimal));
                newTable.Columns.Add("CrUser", typeof(string));
                newTable.Columns.Add("Remark", typeof(string));

                foreach (var r in tbl_POMaster)
                {
                    Bitmap closeImg = new Bitmap(Properties.Resources.power_off);
                    Bitmap cancelmg = new Bitmap(Properties.Resources.closeBtn);
                    Bitmap statusImg = r.DocStatus == "4" ? closeImg : cancelmg;

                    string docStatusName = docStatus.First(x => x.DocStatusCode == r.DocStatus).DocStatusName;
                    tbl_Employee emp = allEmp.FirstOrDefault(x => x.EmpID == r.EmpID);
                    string crUser = "";
                    if (emp != null)
                        crUser = string.Join(" ", emp.TitleName, emp.FirstName);

                    newTable.Rows.Add(r.DocNo, statusImg, docStatusName, r.DocRef, r.DocDate,
                        r.SuppName, r.CreditDay, r.DueDate, r.TotalDue, crUser, r.Remark);
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
