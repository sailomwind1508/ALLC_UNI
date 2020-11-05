using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class ProductMovement : BaseControl, IObject
    {
        private Func<tbl_PRMaster, bool> _docTypePredicate = null;
        public virtual Func<tbl_PRMaster, bool> docTypePredicate
        {
            get { return _docTypePredicate; }
            set
            {
                _docTypePredicate = value;
            }
        }

        List<tbl_Product> productList = new List<tbl_Product>();
        List<tbl_Product> allProductList = new List<tbl_Product>();
        List<tbl_InvMovement> invMovementList = new List<tbl_InvMovement>();
        List<tbl_InvWarehouse> invWarehouseList = new List<tbl_InvWarehouse>();
        List<tbl_InvMovement> invMovementSumList = new List<tbl_InvMovement>();

        public ProductMovement() : base("")
        {
            _docTypePredicate = (x => x.DocTypeCode == "");
        }

        public List<tbl_SalArea> GetAllSaleArea()
        {
            return new tbl_SalArea().SelectAll();
        }

        public List<tbl_SalAreaDistrict> GetAllSaleAreaDistrict()
        {
            return new tbl_SalAreaDistrict().SelectAll();
        }

        public List<tbl_ArCustomer> GetCustomer(Func<tbl_ArCustomer, bool> predicate)
        {
            return new tbl_ArCustomer().Select(predicate);
        }

        public List<ProductMovementModel> PrepareProductMovementModel(string whid, string prdGroupID, string prdSubGroupID, string productID, DateTime fDate, DateTime tDate)
        {
            List<ProductMovementModel> pmmList = new List<ProductMovementModel>();

            if (!string.IsNullOrEmpty(productID))
                productList = productList.Where(x => productID == x.ProductCode).ToList();

            var _invMovementList = invMovementList.Where(x => x.ProductID == productID).ToList();

            var _invMovementSumList = invMovementSumList.Where(x => x.ProductID == productID && x.WHID == whid && x.TrnType != "X"
                && fDate.ToDateTimeFormat() >= x.TrnDate.ToDateTimeFormat()).ToList();

            _invMovementList = _invMovementList.OrderBy(x => x.TrnDate).ThenByDescending(x => x.TrnType).ToList();

            for (int i = 0; i < _invMovementList.Count; i++)
            {
                ProductMovementModel pmm = new ProductMovementModel();
                pmm.ProductID = _invMovementList[i].ProductID;

                if (!string.IsNullOrEmpty(_invMovementList[i].ProductName))
                    pmm.ProductName = _invMovementList[i].ProductName;
                else
                {
                    var prd = allProductList.FirstOrDefault(x => x.ProductID == _invMovementList[i].ProductID);
                    if (prd != null)
                        pmm.ProductName = prd.ProductName;
                }

                var prdItem = allProductList.FirstOrDefault(x => x.ProductID == _invMovementList[i].ProductID);
                if (prdItem != null)
                {
                    pmm.ProductGroupID = prdItem.ProductGroupID;
                    pmm.ProductSubGroupID = prdItem.ProductSubGroupID;
                }

                pmm.TrnDate = _invMovementList[i].TrnDate;
                pmm.RefDocNo = _invMovementList[i].RefDocNo;
                pmm.TrnType = _invMovementList[i].TrnType;
                pmm.ToWHID = _invMovementList[i].ToWHID;
                pmm.InQty = _invMovementList[i].TrnQtyIn;
                pmm.OutQty = _invMovementList[i].TrnQtyOut;

                decimal sumTrnQty = 0;
                if (i > 0)
                {
                    if (pmmList[i - 1].ProductID == _invMovementList[i].ProductID)
                        sumTrnQty = (pmmList[i - 1].DTBalance + _invMovementList[i].TrnQtyIn) - _invMovementList[i].TrnQtyOut;

                    var _pmm = pmmList.OrderBy(x => x.TrnDate).FirstOrDefault(x => x.ProductID == _invMovementList[i].ProductID);
                    if (_pmm != null)
                        pmm.ForwardQty = _pmm.ForwardQty;
                }
                else
                {
                    if (_invMovementSumList != null && _invMovementSumList.Count > 0)
                    {
                        sumTrnQty = (_invMovementSumList.Sum(x => x.TrnQty) + _invMovementList[i].TrnQtyIn) - _invMovementList[i].TrnQtyOut;
                        pmm.ForwardQty = _invMovementSumList.Sum(x => x.TrnQty);
                        pmm.DTBalance = sumTrnQty;
                    }
                }


                pmm.DTBalance = sumTrnQty;
                pmm.WHID = whid;

                pmmList.Add(pmm);
            }

            return pmmList;
        }

        public DataTable GetDataTable_Details(string whid, string prdGroupID, string prdSubGroupID, List<string> prdCodeList, DateTime fDate, DateTime tDate)
        {
            try
            {
                List<ProductMovementModel> pmmList = new List<ProductMovementModel>();

                Func<tbl_Product, bool> tbl_ProductFunc = (x => x.FlagDel == false && x.ProductGroupID.ToString() == (prdGroupID != "-1" ? prdGroupID : x.ProductGroupID.ToString()) &&
                x.ProductSubGroupID.ToString() == (prdSubGroupID != "-1" ? prdSubGroupID : x.ProductSubGroupID.ToString()));

                productList = (new tbl_Product()).Select(tbl_ProductFunc);
                allProductList = productList;

                if (prdCodeList.Count > 0)
                {
                    productList = productList.Where(x => prdCodeList.Contains(x.ProductCode)).ToList();
                }

                Func<tbl_InvMovement, bool> tbl_InvMovementFunc = (x => productList.Select(p => p.ProductID).Contains(x.ProductID)
                && x.TrnType != "X" && x.WHID == whid
                && (x.TrnDate.ToDateTimeFormat() >= fDate.ToDateTimeFormat() && x.TrnDate.ToDateTimeFormat() <= tDate.ToDateTimeFormat()));
                invMovementList = new tbl_InvMovement().Select(tbl_InvMovementFunc);

                Func<tbl_InvWarehouse, bool> tbl_InvWarehouseFunc = (x => productList.Select(p => p.ProductID).Contains(x.ProductID) && x.WHID == whid);
                invWarehouseList = new tbl_InvWarehouse().Select(tbl_InvWarehouseFunc);

                Func<tbl_InvMovement, bool> tbl_InvMovement_SumFunc = (x => productList.Select(p => p.ProductID).Contains(x.ProductID) && x.WHID == whid && x.TrnType != "X"
                && fDate.ToDateTimeFormat() >= x.TrnDate.ToDateTimeFormat());
                invMovementSumList = new tbl_InvMovement().Select(tbl_InvMovement_SumFunc);

                //invMovementList = invMovementList.OrderBy(x => x.ProductID).ThenBy(x => x.TrnDate).ToList();

                var productIDList = invMovementList.Select(x => x.ProductID).Distinct().OrderBy(x => x).ToList();
                foreach (var productID in productIDList)
                {
                    pmmList.AddRange(PrepareProductMovementModel(whid, prdGroupID, prdSubGroupID, productID, fDate, tDate));
                }

                DataTable _dt = new DataTable("pmmDTTable");
                _dt.Columns.Add("รหัสสินค้า", typeof(string));
                _dt.Columns.Add("ชื่อสินค้า", typeof(string));
                _dt.Columns.Add("เวลา", typeof(string));
                _dt.Columns.Add("เลขที่เอกสาร", typeof(string));
                _dt.Columns.Add("ประเภท", typeof(string));
                _dt.Columns.Add("จาก/ไป(คลัง)", typeof(string));
                _dt.Columns.Add("ยกมา", typeof(string));
                _dt.Columns.Add("เข้า", typeof(string));
                _dt.Columns.Add("ออก", typeof(string));
                _dt.Columns.Add("คงเหลือ", typeof(string));

                var data = pmmList.OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ThenBy(x => x.ProductID).ToList();
                foreach (var r in data)
                {
                    _dt.Rows.Add(r.ProductID, r.ProductName, r.TrnDate.ToString("dd/MM/yyyy"), r.RefDocNo, r.TrnType, r.ToWHID, r.ForwardQty.ToStringN0(),
                        r.InQty.ToStringN0(), r.OutQty.ToStringN0(), r.DTBalance.ToStringN0());
                }

                return _dt;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public virtual DataTable GetDataTable(string whid, string prdGroupID, string prdSubGroupID, List<string> prdCodeList, DateTime fDate, DateTime tDate)
        {
            try
            {
                List<tbl_Product> productList = new List<tbl_Product>();
                Func<tbl_Product, bool> tbl_ProductFunc = (x => x.FlagDel == false && x.ProductGroupID.ToString() == (prdGroupID != "-1" ? prdGroupID : x.ProductGroupID.ToString()) &&
                x.ProductSubGroupID.ToString() == (prdSubGroupID != "-1" ? prdSubGroupID : x.ProductSubGroupID.ToString()));

                productList = (new tbl_Product()).Select(tbl_ProductFunc);
                if (prdCodeList.Count > 0)
                {
                    productList = productList.Where(x => prdCodeList.Contains(x.ProductCode)).ToList();
                }

                List<tbl_ProductUomSet> productUomSetList = new List<tbl_ProductUomSet>();
                Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => productList.Select(p => p.ProductID).Contains(x.ProductID)); // && x.UomSetID == 1);
                productUomSetList = (new tbl_ProductUomSet()).Select(tbl_ProductUomSetPre);

                List<tbl_ProductGroup> productGroupList = new List<tbl_ProductGroup>();
                Func<tbl_ProductGroup, bool> tbl_ProductGroupPre = (x => productList.Select(p => p.ProductGroupID).Contains(x.ProductGroupID));
                productGroupList = (new tbl_ProductGroup()).Select(tbl_ProductGroupPre);

                List<tbl_ProductSubGroup> productSubGroupList = new List<tbl_ProductSubGroup>();
                Func<tbl_ProductSubGroup, bool> tbl_ProductSubGroupPre = (x => productList.Select(p => p.ProductSubGroupID).Contains(x.ProductSubGroupID));
                productSubGroupList = (new tbl_ProductSubGroup()).Select(tbl_ProductSubGroupPre);

                List<tbl_InvMovement> invMovementList = new List<tbl_InvMovement>();
                Func<tbl_InvMovement, bool> tbl_InvMovementFunc = (x => productList.Select(p => p.ProductID).Contains(x.ProductID)
                && x.TrnType != "X" && x.WHID == whid
                && (x.TrnDate.ToDateTimeFormat() >= fDate.ToDateTimeFormat() && x.TrnDate.ToDateTimeFormat() <= tDate.ToDateTimeFormat()));

                invMovementList = new tbl_InvMovement().Select(tbl_InvMovementFunc);

                List<tbl_InvWarehouse> invWarehouseList = new List<tbl_InvWarehouse>();
                Func<tbl_InvWarehouse, bool> tbl_InvWarehouseFunc = (x => productList.Select(p => p.ProductID).Contains(x.ProductID) && x.WHID == whid);
                invWarehouseList = new tbl_InvWarehouse().Select(tbl_InvWarehouseFunc);

                List<ProductMovementModel> pmmList = new List<ProductMovementModel>();
                foreach (var p in productList)
                {
                    ProductMovementModel pmm = new ProductMovementModel();
                    pmm.ProductID = p.ProductID;
                    pmm.ProductCode = p.ProductCode;
                    pmm.ProductName = p.ProductName;

                    var pg = productGroupList.FirstOrDefault(x => x.ProductGroupID == p.ProductGroupID);
                    if (pg != null)
                        pmm.ProductGroupID = pg.ProductGroupID;

                    var psg = productSubGroupList.FirstOrDefault(x => x.ProductSubGroupID == p.ProductSubGroupID);
                    if (psg != null)
                        pmm.ProductSubGroupID = psg.ProductSubGroupID;

                    var uomSet = productUomSetList.Where(x => x.ProductID == p.ProductID).ToList();
                    var puom = uomSet.FirstOrDefault(x => x.UomSetID == 1);
                    if (puom != null)
                    {
                        pmm.BaseQty = puom.BaseQty;
                        pmm.UomSetID = 1;
                    }
                    else
                    {
                        var _puom = uomSet.FirstOrDefault();
                        if (_puom != null)
                        {
                            pmm.BaseQty = _puom.BaseQty;
                            pmm.UomSetID = _puom.UomSetID;

                            puom = _puom;
                        }
                    }

                    var invmm = invMovementList.Where(x => x.WHID == whid && x.ProductID == p.ProductID).ToList();

                    var invwh = invWarehouseList.FirstOrDefault(x => x.WHID == whid && x.ProductID == p.ProductID);

                    decimal qtyOnHand = 0;
                    decimal sumTrnQtyOut = 0;
                    decimal sumTrnQtyIn = 0;

                    if (invmm != null && invmm.Count > 0)
                    {
                        sumTrnQtyOut = invmm.Sum(x => x.TrnQtyOut);
                        sumTrnQtyIn = invmm.Sum(x => x.TrnQtyIn);
                    }

                    if (invwh != null)
                        qtyOnHand = invwh.QtyOnHand;

                    int impLargeQty = 0;
                    int impSmallQty = 0;
                    if (puom != null)
                    {
                        decimal temp_impLargeQty = ((qtyOnHand + sumTrnQtyOut) - sumTrnQtyIn) / puom.BaseQty;
                        impLargeQty = Convert.ToInt32(temp_impLargeQty.ToString().Split('.')[0]);
                        impSmallQty = Convert.ToInt32(((qtyOnHand + sumTrnQtyOut) - sumTrnQtyIn) % puom.BaseQty);

                        pmm.ImpLargeQty = impLargeQty;
                        pmm.ImpSmallQty = impSmallQty;
                        pmm.InQty = sumTrnQtyIn;
                        pmm.OutQty = sumTrnQtyOut;

                        decimal temp_qtyOnHandLarge = qtyOnHand / puom.BaseQty;
                        pmm.QtyOnHandLarge = Convert.ToInt32(temp_qtyOnHandLarge.ToString().Split('.')[0]);
                        pmm.QtyOnHandSmall = Convert.ToInt32(qtyOnHand % puom.BaseQty);
                    }

                    //pmm.TrnDate = null;
                    pmm.WHID = whid;

                    pmmList.Add(pmm);
                }

                DataTable _dt = new DataTable("pmmTable");
                _dt.Columns.Add("รหัสสินค้า", typeof(string));
                _dt.Columns.Add("ชื่อสินค้า", typeof(string));
                _dt.Columns.Add("หน่วยคูณ", typeof(string));
                _dt.Columns.Add("ยกมา\n(ใหญ่)", typeof(string));
                _dt.Columns.Add("ยกมา\n(เล็ก)", typeof(string));
                _dt.Columns.Add("เข้า\n(เล็ก)", typeof(string));
                _dt.Columns.Add("ออก\n(เล็ก)", typeof(string));
                _dt.Columns.Add("คงเหลือ\n(ใหญ่)", typeof(string));
                _dt.Columns.Add("คงเหลือ\n(เล็ก)", typeof(string));

                var data = pmmList.OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ToList();
                foreach (var r in data)
                {
                    _dt.Rows.Add(r.ProductCode, r.ProductName, r.BaseQty.ToNumberFormat(), r.ImpLargeQty.ToNumberFormat(), r.ImpSmallQty,
                        r.InQty.ToStringN0(), r.OutQty.ToStringN0(), r.QtyOnHandLarge.ToStringN0(), r.QtyOnHandSmall.ToStringN0());
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

            if (filters != null)
            {

            }
            else
            {
                dt = GetDataTable();
            }

            return dt;
        }

        public DataTable GetDataTable(bool isPopup = true)
        {
            return null;
        }
    }
}




//public DataTable GetDataTable_Details_old(string whid, string prdGroupID, string prdSubGroupID, List<string> prdCodeList, DateTime fDate, DateTime tDate)
//{
//    try
//    {
//        List<tbl_Product> productList = new List<tbl_Product>();
//        Func<tbl_Product, bool> tbl_ProductFunc = (x => x.FlagDel == false && x.ProductGroupID.ToString() == (prdGroupID != "-1" ? prdGroupID : x.ProductGroupID.ToString()) &&
//        x.ProductSubGroupID.ToString() == (prdSubGroupID != "-1" ? prdSubGroupID : x.ProductSubGroupID.ToString()));

//        productList = (new tbl_Product()).Select(tbl_ProductFunc);
//        if (prdCodeList.Count > 0)
//        {
//            productList = productList.Where(x => prdCodeList.Contains(x.ProductCode)).ToList();
//        }

//        //List<tbl_ProductUomSet> productUomSetList = new List<tbl_ProductUomSet>();
//        //Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => productList.Select(p => p.ProductID).Contains(x.ProductID)); // && x.UomSetID == 1);
//        //productUomSetList = (new tbl_ProductUomSet()).Select(tbl_ProductUomSetPre);

//        //List<tbl_ProductGroup> productGroupList = new List<tbl_ProductGroup>();
//        //Func<tbl_ProductGroup, bool> tbl_ProductGroupPre = (x => productList.Select(p => p.ProductGroupID).Contains(x.ProductGroupID));
//        //productGroupList = (new tbl_ProductGroup()).Select(tbl_ProductGroupPre);

//        //List<tbl_ProductSubGroup> productSubGroupList = new List<tbl_ProductSubGroup>();
//        //Func<tbl_ProductSubGroup, bool> tbl_ProductSubGroupPre = (x => productList.Select(p => p.ProductSubGroupID).Contains(x.ProductSubGroupID));
//        //productSubGroupList = (new tbl_ProductSubGroup()).Select(tbl_ProductSubGroupPre);

//        List<tbl_InvMovement> invMovementList = new List<tbl_InvMovement>();
//        Func<tbl_InvMovement, bool> tbl_InvMovementFunc = (x => productList.Select(p => p.ProductID).Contains(x.ProductID)
//        && x.TrnType != "X" && x.WHID == whid
//        && (x.TrnDate.ToDateTimeFormat() >= fDate.ToDateTimeFormat() && x.TrnDate.ToDateTimeFormat() <= tDate.ToDateTimeFormat()));

//        invMovementList = new tbl_InvMovement().Select(tbl_InvMovementFunc);

//        List<tbl_InvWarehouse> invWarehouseList = new List<tbl_InvWarehouse>();
//        Func<tbl_InvWarehouse, bool> tbl_InvWarehouseFunc = (x => productList.Select(p => p.ProductID).Contains(x.ProductID) && x.WHID == whid);
//        invWarehouseList = new tbl_InvWarehouse().Select(tbl_InvWarehouseFunc);

//        List<tbl_InvMovement> _invMovementList = new List<tbl_InvMovement>();
//        Func<tbl_InvMovement, bool> _tbl_InvMovementFunc = (x => productList.Select(p => p.ProductID).Contains(x.ProductID) && x.WHID == whid && x.TrnType != "X"
//        && fDate.ToDateTimeFormat() >= x.TrnDate.ToDateTimeFormat());

//        _invMovementList = new tbl_InvMovement().Select(_tbl_InvMovementFunc);

//        List<ProductMovementModel> pmmList = new List<ProductMovementModel>();
//        invMovementList = invMovementList.OrderBy(x => x.ProductID).ThenBy(x => x.TrnDate).ToList();
//        for (int i = 0; i < invMovementList.Count; i++)
//        {
//            ProductMovementModel pmm = new ProductMovementModel();
//            pmm.ProductID = invMovementList[i].ProductID;

//            if (!string.IsNullOrEmpty(invMovementList[i].ProductName))
//            {
//                pmm.ProductName = invMovementList[i].ProductName;
//            }
//            else
//            {
//                var prd = invMovementList.FirstOrDefault(x => x.ProductID == invMovementList[i].ProductID);
//                if (prd != null)
//                {
//                    pmm.ProductName = prd.ProductName;
//                }
//            }

//            var prdItem = productList.FirstOrDefault(x => x.ProductID == invMovementList[i].ProductID);
//            if (prdItem != null)
//            {
//                pmm.ProductGroupID = prdItem.ProductGroupID;
//                pmm.ProductSubGroupID = prdItem.ProductSubGroupID;
//            }

//            pmm.TrnDate = invMovementList[i].TrnDate;
//            pmm.RefDocNo = invMovementList[i].RefDocNo;
//            pmm.TrnType = invMovementList[i].TrnType;
//            pmm.ToWHID = invMovementList[i].ToWHID;
//            pmm.InQty = invMovementList[i].TrnQtyIn;
//            pmm.OutQty = invMovementList[i].TrnQtyOut;

//            decimal sumTrnQty = 0;
//            if (i > 0)
//            {
//                var _pmm = pmmList.OrderBy(x => x.TrnDate).FirstOrDefault(x => x.ProductID == invMovementList[i].ProductID);
//                if (pmmList[i - 1].ProductID == invMovementList[i].ProductID)
//                {
//                    sumTrnQty = (pmmList[i - 1].DTBalance + invMovementList[i].TrnQtyIn) - invMovementList[i].TrnQtyOut;
//                }

//                if (_pmm != null)
//                {
//                    pmm.ForwardQty = _pmm.ForwardQty;
//                }

//            }
//            else
//            {
//                if (_invMovementList != null && _invMovementList.Count > 0)
//                {
//                    sumTrnQty = (_invMovementList.Sum(x => x.TrnQty) + invMovementList[i].TrnQtyIn) - invMovementList[i].TrnQtyOut;
//                    pmm.ForwardQty = _invMovementList.Sum(x => x.TrnQty);
//                    pmm.DTBalance = sumTrnQty;
//                }
//            }


//            pmm.DTBalance = sumTrnQty;
//            pmm.WHID = whid;

//            pmmList.Add(pmm);
//        }

//        DataTable _dt = new DataTable("pmmDTTable");
//        _dt.Columns.Add("รหัสสินค้า", typeof(string));
//        _dt.Columns.Add("ชื่อสินค้า", typeof(string));
//        _dt.Columns.Add("เวลา", typeof(string));
//        _dt.Columns.Add("เลขที่เอกสาร", typeof(string));
//        _dt.Columns.Add("ประเภท", typeof(string));
//        _dt.Columns.Add("จาก/ไป(คลัง)", typeof(string));
//        _dt.Columns.Add("ยกมา", typeof(string));
//        _dt.Columns.Add("เข้า", typeof(string));
//        _dt.Columns.Add("ออก", typeof(string));
//        _dt.Columns.Add("คงเหลือ", typeof(string));

//        var data = pmmList.OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ThenBy(x => x.ProductID).ThenBy(x => x.TrnDate).ThenBy(x => x.TrnType).ToList();
//        foreach (var r in data)
//        {
//            _dt.Rows.Add(r.ProductID, r.ProductName, r.TrnDate.ToString("dd/MM/yyyy"), r.RefDocNo, r.TrnType, r.ToWHID, r.ForwardQty, r.InQty, r.OutQty, r.DTBalance);
//        }

//        return _dt;
//    }
//    catch (Exception ex)
//    {
//        ex.WriteLog(this.GetType());
//        return null;
//    }
//}
