using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace AllCashUFormsApp.Controller
{
    public class Product : BaseControl, IObject
    {
        private Func<tbl_PRMaster, bool> _docTypePredicate = null;
        public Product() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        public List<tbl_ProductFlavour> GetProductFlavour(Func<tbl_ProductFlavour, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ProductFlavour().Select(condition);
            else
                return new tbl_ProductFlavour().SelectAll();
        }

        public List<tbl_ProductUom> GetProductUOM(Func<tbl_ProductUom, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ProductUom().Select(condition);
            else
                return new tbl_ProductUom().SelectAll();
        }

        public List<tbl_Product> GetProductEntity(Func<tbl_Product, bool> condition = null)
        {
            if (condition != null)
                return new tbl_Product().SelectEntity(condition);
            else
                return new tbl_Product().SelectAllEntity();
        }

        public List<tbl_ProductPriceGroup> GetProductPriceGroup(Func<tbl_ProductPriceGroup, bool> predicate = null)
        {
            if (predicate != null)
                return new tbl_ProductPriceGroup().Select(predicate);
            else
                return new tbl_ProductPriceGroup().SelectAll();
        }

        public List<tbl_ProductUomSet> GetProductUOMSet(Func<tbl_ProductUomSet, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ProductUomSet().Select(condition);
            else
                return new tbl_ProductUomSet().SelectAll();
        }

        public List<tbl_ProductType> GetProductType(Func<tbl_ProductType, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ProductType().Select(condition);
            else
                return new tbl_ProductType().SelectAll();
        }

        public List<tbl_ProductGroup> GetProductGroupNonFlag(Func<tbl_ProductGroup, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ProductGroup().SelectNonFlag(condition);
            else
                return new tbl_ProductGroup().SelectAllOrderByProductGroupCode();
        }

        public List<tbl_ProductSubGroup> GetProductSubGroup(Func<tbl_ProductSubGroup, bool> condition = null)
        {
            if (condition != null)
                return new tbl_ProductSubGroup().SelectNonFlag(condition);
            else
                return new tbl_ProductSubGroup().SelectAllNonFlag();
        }

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

        public int RemoveData(tbl_ProductUomSet tbl_ProductUomSet)
        {
            return tbl_ProductUomSet.Delete();
        }

        public int RemoveData(tbl_ProductPriceGroup tbl_ProductPriceGroup)
        {
            return tbl_ProductPriceGroup.Delete();
        }

        public DataTable GetDataTable(bool isPopup = true)
        {
            try
            {
                return new tbl_Product().GetDataTable();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        //public DataTable GetDataTable(bool isPopup = true)
        //{
        //    try
        //    {
        //        List<tbl_Product> tbl_Product = new List<tbl_Product>();
        //        if (isPopup) //pop up product
        //        {
        //            tbl_Product = (new tbl_Product()).SelectAll();
        //        }
        //        else //RL
        //        {
        //            tbl_Product = (new tbl_Product()).SelectAll();//.Where(x => x.IsFulfill == false).ToList(); IsFulfill == false) have action on tablet only
        //        }

        //        List<tbl_ProductGroup> tbl_ProductGroup = new List<tbl_ProductGroup>();
        //        Func<tbl_ProductGroup, bool> tbl_ProductGroupPre = (x => tbl_Product.Select(p => p.ProductGroupID).Contains(x.ProductGroupID));
        //        tbl_ProductGroup = (new tbl_ProductGroup()).Select(tbl_ProductGroupPre);

        //        List<tbl_ProductSubGroup> tbl_ProductSubGroup = new List<tbl_ProductSubGroup>();
        //        Func<tbl_ProductSubGroup, bool> tbl_ProductSubGroupPre = (x => tbl_Product.Select(p => p.ProductSubGroupID).Contains(x.ProductSubGroupID));
        //        tbl_ProductSubGroup = (new tbl_ProductSubGroup()).Select(tbl_ProductSubGroupPre);

        //        List<tbl_ProductUomSet> tbl_ProductUomSet = new List<tbl_ProductUomSet>();
        //        Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => tbl_Product.Select(p => p.ProductID).Contains(x.ProductID)); // && x.UomSetID == 1);
        //        tbl_ProductUomSet = (new tbl_ProductUomSet()).Select(tbl_ProductUomSetPre);

        //        List<tbl_ProductPriceGroup> tbl_ProductPriceGroup = new List<tbl_ProductPriceGroup>();
        //        //Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => tbl_Product.Select(p => p.ProductGroupID).Contains(x.ProductGroupID));
        //        tbl_ProductPriceGroup = (new tbl_ProductPriceGroup()).SelectAll();

        //        var query = from p in tbl_Product
        //                    join pg in tbl_ProductGroup on p.ProductGroupID equals pg.ProductGroupID
        //                    join psg in tbl_ProductSubGroup on p.ProductSubGroupID equals psg.ProductSubGroupID
        //                    join puom in tbl_ProductUomSet on p.ProductID equals puom.ProductID
        //                    from ppri in tbl_ProductPriceGroup
        //                    where puom.ProductID == ppri.ProductID && puom.UomSetID == ppri.ProductUomID && p.FlagDel == false && p.PurchaseUomID == puom.UomSetID  //p.SaleUomID 
        //                    select new
        //                    {
        //                        ProductCode = p.ProductCode,
        //                        ProductRefCode = p.ProductRefCode,
        //                        ProductName = p.ProductName,
        //                        ProductGroupName = pg.ProductGroupName,
        //                        ProductGroupID = pg.ProductGroupID,
        //                        ProductSubGroupID = psg.ProductSubGroupID,
        //                        ProductSubGroupName = psg.ProductSubGroupName,
        //                        ProductID = p.ProductID,
        //                        UomSetID = p.PurchaseUomID, //puom.UomSetID,
        //                        UomSetName = puom.UomSetName,
        //                        VatType = p.VatType ? "7" : "0",
        //                        OrderQty = 0,
        //                        UnitPrice = ppri.BuyPrice,
        //                        LineTotal = (ppri.BuyPrice * 0),
        //                        BaseQtyDT = "1*" + puom.BaseQty.ToString(),
        //                        Cause = 0,
        //                        SellPriceVat = ppri.SellPriceVat, //p.VatType ? ppri.SellPriceVat : ppri.SellPrice
        //                        SellPrice = ppri.SellPrice
        //                    };

        //        DataTable newTable = query.ToList().ToDataTable();
        //        newTable.Clear();

        //        var data = query.OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ToList();
        //        foreach (var r in data)
        //        {
        //            newTable.Rows.Add(r.ProductCode, r.ProductRefCode, r.ProductName, r.ProductGroupName, r.ProductGroupID, r.ProductSubGroupID, r.ProductSubGroupName,
        //                r.ProductID, r.UomSetID, r.UomSetName, r.VatType, r.OrderQty, r.UnitPrice, r.LineTotal, r.BaseQtyDT, r.Cause, r.SellPriceVat, r.SellPrice);
        //        }

        //        return newTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.WriteLog(this.GetType());
        //        return null;
        //    }
        //}

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

        public DataTable GetProductTable(Func<tbl_Product, bool> func)
        {
            DataTable dt = new DataTable();
            List<tbl_Product> tbl_Products = new List<tbl_Product>();
            tbl_Products = new tbl_Product().SelectNonFlagDel(func);
            dt = tbl_Products.ToDataTable();
            return dt;
        }

        public DataTable GetPrdTable(Func<tbl_Product, bool> func = null)
        {
            DataTable dt = new DataTable();
            var tbl_Products = new List<tbl_Product>();
            tbl_Products = new tbl_Product().SelectEntity(func).ToList();

            var tbl_ProductTypes = new List<tbl_ProductType>();
            tbl_ProductTypes = new tbl_ProductType().Select(x => x.FlagDel == false);

            var tbl_ProductGroups = new List<tbl_ProductGroup>();
            tbl_ProductGroups = new tbl_ProductGroup().SelectAllOrderByProductGroupCode();

            var tbl_ProductSubGroups = new List<tbl_ProductSubGroup>();

            tbl_ProductSubGroups = new tbl_ProductSubGroup().SelectAllNonFlag();

            var tbl_ProductUoms = new List<tbl_ProductUom>();
            tbl_ProductUoms = new tbl_ProductUom().SelectAll();

            var query = from t1 in tbl_Products
                        //join t2 in tbl_ProductTypes on 1 equals 1
                        join t3 in tbl_ProductGroups on t1.ProductGroupID equals t3.ProductGroupID
                        join t4 in tbl_ProductSubGroups on t1.ProductSubGroupID equals t4.ProductSubGroupID
                        join t5 in tbl_ProductUoms on t1.SaleUomID equals t5.ProductUomID
                        select new
                        {
                            ProductID = t1.ProductID,
                            ProductRefCode = t1.ProductRefCode,
                            ProductTypeID = tbl_ProductTypes.First().ProductTypeID, //t2.ProductTypeID,
                            ProductTypeName = tbl_ProductTypes.First().ProductTypeName,
                            ProductGroupID = t1.ProductGroupID,
                            ProductGroupName = t3.ProductGroupName,
                            ProductSubGroupID = t1.ProductSubGroupID,
                            ProductSubGroupName = t4.ProductSubGroupName,
                            Barcode = t1.Barcode,
                            ProductName = t1.ProductName,
                            ProductShortName = t1.ProductShortName,
                            SaleUomID = t1.SaleUomID,
                            ProductUomName = t5.ProductUomName,
                            Flavour = t1.Flavour,
                            VatType = t1.VatType,
                            ReorderPoint = t1.ReorderPoint,
                            MinPoint = t1.MinPoint,
                            Weight = t1.Weight,
                            Size = t1.Size,
                            IsFulfill = t1.IsFulfill,
                            Seq = t1.Seq,
                            ProductImg = t1.ProductImg,
                            //เพิ่มใหม่ด้านล่างทั้งหมด
                            SizeUOM = t1.SizeUOM,
                            WeightUOM = t1.WeightUOM,
                            PurchaseUomID = t1.PurchaseUomID,
                            StandardCost = t1.StandardCost,
                            SellPrice = t1.SellPrice,
                            CrDate = t1.CrDate,
                            CrUser = t1.CrUser,
                            EdDate = t1.EdDate,
                            EdUser = t1.EdUser,
                            FlagDel = t1.FlagDel,
                            FlagSend = t1.FlagSend,
                            FlagNew = t1.FlagNew,
                            FlagEdit = t1.FlagEdit,
                            ProductBrandID = t1.ProductBrandID,
                            ProductLine = t1.ProductLine,
                            Remark = t1.Remark
                        };
            dt = query.ToDataTable();
          
            return dt;
        }

        //public DataTable GetPrdPriceGroupTable(Func<tbl_ProductUomSet, bool> func = null)
        //{
        //    DataTable dt = new DataTable();

        //    var tbl_ProductUomSets = new List<tbl_ProductUomSet>();
        //    tbl_ProductUomSets = new tbl_ProductUomSet().SelectEntity(func);

        //    var tbl_ProductPriceGroups = new List<tbl_ProductPriceGroup>();
        //    tbl_ProductPriceGroups = new tbl_ProductPriceGroup().Select(x => tbl_ProductUomSets.Select(p => p.ProductID).Contains(x.ProductID));


        //    var tbl_ProductUoms = new List<tbl_ProductUom>();
        //    tbl_ProductUoms = new tbl_ProductUom().SelectAll();

        //    var query = from t1 in tbl_ProductUomSets
        //                join t2 in tbl_ProductUoms on t1.UomSetID equals t2.ProductUomID
        //                join t3 in tbl_ProductPriceGroups on t1.UomSetID equals t3.ProductUomID
        //                select new
        //                {
        //                    ProductID = t1.ProductID,
        //                    UomSetID = t1.UomSetID,//หน่วย
        //                    UomSetName = t1.UomSetName,//หน่วย
        //                    BaseQty = t1.BaseQty,//จำนวน
        //                    Weight = t1.Weight,//น้ำหนัก

        //                    BuyPrice = t3.BuyPrice,//ราคาก่อนภาษี

        //                    SellPrice = t3.SellPrice,//ราคาก่อนภาษี
        //                    SellPriceVat = t3.SellPriceVat,//ราคารวมภาษี
        //                    ComPrice = t3.ComPrice,//ค่าคอมมิชชั่น
        //                };


        //    dt = query.ToDataTable();
        //    return dt;
        //}

        public DataTable GetProductGroupPriceData(Dictionary<string, object> _params)
        {
            return (new tbl_Product()).GetProductGroupPriceData(_params);
        }

        public int UpdateData(tbl_ProductPriceGroup tbl_ProductPriceGroup)
        {
            return tbl_ProductPriceGroup.Update();
        }

        public int UpdateData(tbl_ProductUomSet tbl_ProductUomSet)
        {
            return tbl_ProductUomSet.Update();
        }

        public DataTable GetProductViewCheck(string ProductID)
        {
            return new tbl_Product().GetProductViewCheck(ProductID);
        }

        public DataTable proc_tbl_Product_Data(Dictionary<string, object> _params)
        {
            return (new tbl_Product()).proc_tbl_Product_Data(_params);
        }

        public List<tbl_SaleType> GetSaleType()
        {
            return new tbl_SaleType().SelectAll();
        }

        public List<tbl_Product> SelectProductList(string _ProductID)
        {
            return (new tbl_Product()).SelectProductList(_ProductID);
        }

        public List<tbl_Product> SelectSingleProduct()//Modified By ADISORN 29/12/2564
        {
            return (new tbl_Product()).SelectSingleProduct();
        }

        public DataTable GetProductData_Popup(string _ProductSubGroupID, string Search)
        {
            return (new tbl_Product()).GetProductData_Popup(_ProductSubGroupID, Search);
        }
    }
}
