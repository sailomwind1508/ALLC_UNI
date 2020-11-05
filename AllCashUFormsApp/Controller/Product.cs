using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class Product : IObject
    {
        public List<tbl_Product> GetAllData()
        {
            return (new tbl_Product()).SelectAll();
        }

        public int AddData(tbl_Product tbl_Product)
        {
            return tbl_Product.Insert();
        }

        public int UpdateData(tbl_Product tbl_Product)
        {
            return tbl_Product.Update();
        }

        public int RemoveData(tbl_Product tbl_Product)
        {
            return tbl_Product.Delete();
        }

        public DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                List<tbl_Product> tbl_Product = new List<tbl_Product>();
                if (isPopup) //pop up product
                {
                    tbl_Product = (new tbl_Product()).SelectAll();
                }
                else //RL
                {
                    tbl_Product = (new tbl_Product()).SelectAll().Where(x => x.IsFulfill == false).ToList();
                }

                List<tbl_ProductGroup> tbl_ProductGroup = new List<tbl_ProductGroup>();
                Func<tbl_ProductGroup, bool> tbl_ProductGroupPre = (x => tbl_Product.Select(p => p.ProductGroupID).Contains(x.ProductGroupID));
                tbl_ProductGroup = (new tbl_ProductGroup()).Select(tbl_ProductGroupPre);

                List<tbl_ProductSubGroup> tbl_ProductSubGroup = new List<tbl_ProductSubGroup>();
                Func<tbl_ProductSubGroup, bool> tbl_ProductSubGroupPre = (x => tbl_Product.Select(p => p.ProductSubGroupID).Contains(x.ProductSubGroupID));
                tbl_ProductSubGroup = (new tbl_ProductSubGroup()).Select(tbl_ProductSubGroupPre);

                List<tbl_ProductUomSet> tbl_ProductUomSet = new List<tbl_ProductUomSet>();
                Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => tbl_Product.Select(p => p.ProductID).Contains(x.ProductID)); // && x.UomSetID == 1);
                tbl_ProductUomSet = (new tbl_ProductUomSet()).Select(tbl_ProductUomSetPre);

                List<tbl_ProductPriceGroup> tbl_ProductPriceGroup = new List<tbl_ProductPriceGroup>();
                //Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => tbl_Product.Select(p => p.ProductGroupID).Contains(x.ProductGroupID));
                tbl_ProductPriceGroup = (new tbl_ProductPriceGroup()).SelectAll();

                var query = from p in tbl_Product
                            join pg in tbl_ProductGroup on p.ProductGroupID equals pg.ProductGroupID
                            join psg in tbl_ProductSubGroup on p.ProductSubGroupID equals psg.ProductSubGroupID
                            join puom in tbl_ProductUomSet on p.ProductID equals puom.ProductID
                            from ppri in tbl_ProductPriceGroup
                            where puom.ProductID == ppri.ProductID && puom.UomSetID == ppri.ProductUomID && p.FlagDel == false && p.PurchaseUomID == puom.UomSetID  
                            select new
                            {
                                ProductCode = p.ProductCode,
                                ProductRefCode = p.ProductRefCode,
                                ProductName = p.ProductName,
                                ProductGroupName = pg.ProductGroupName,
                                ProductGroupID = pg.ProductGroupID,
                                ProductSubGroupID = psg.ProductSubGroupID,
                                ProductSubGroupName = psg.ProductSubGroupName,
                                ProductID = p.ProductID,
                                UomSetID = p.PurchaseUomID, //puom.UomSetID,
                                UomSetName = puom.UomSetName,
                                VatType = p.VatType ? "7" : "0",
                                OrderQty = 0,
                                UnitPrice = ppri.BuyPrice,
                                LineTotal = (ppri.BuyPrice * 0),
                                BaseQtyDT = "1*" + puom.BaseQty.ToString(),
                                Cause = 0,
                                SellPriceVat = ppri.SellPriceVat, //p.VatType ? ppri.SellPriceVat : ppri.SellPrice
                                SellPrice = ppri.SellPrice
                            };

                DataTable newTable = query.ToList().ToDataTable();
                newTable.Clear();

                var data = query.OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ToList();
                foreach (var r in data)
                {
                    newTable.Rows.Add(r.ProductCode, r.ProductRefCode, r.ProductName, r.ProductGroupName, r.ProductGroupID, r.ProductSubGroupID, r.ProductSubGroupName, 
                        r.ProductID, r.UomSetID, r.UomSetName, r.VatType, r.OrderQty, r.UnitPrice, r.LineTotal, r.BaseQtyDT, r.Cause, r.SellPriceVat, r.SellPrice);
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
            List<tbl_Product> tbl_Products = new List<tbl_Product>();

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
