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
        List<tbl_InvMovement> allInvMovementList = new List<tbl_InvMovement>();
        List<tbl_InvMovement> invMovementList = new List<tbl_InvMovement>();
        List<tbl_InvMovement> invMovementList_BLC = new List<tbl_InvMovement>();
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
            return new tbl_ArCustomer().SelectAll().Where(predicate).ToList();
        }

        public List<ProductMovementModel> PrepareProductMovementModel(string fwhid, string twhid, string prdGroupID, string prdSubGroupID, string productID, DateTime fDate, DateTime tDate)
        {
            List<ProductMovementModel> pmmList = new List<ProductMovementModel>();

            if (!string.IsNullOrEmpty(productID))
                productList = productList.Where(x => productID == x.ProductCode).ToList();

            var _invMovementList = invMovementList.Where(x => x.ProductID == productID).ToList();

            var _invMovementSumList_BLC = invMovementList_BLC.Where(x => x.ProductID == productID).ToList();

            var _invMovementSumList = invMovementSumList.Where(x => x.ProductID == productID).ToList();

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
                pmm.TrnQty = _invMovementList[i].TrnQty;
                pmm.CrDate = _invMovementList[i].CrDate;

                decimal sumTrnQty = 0;
                if (i > 0)
                {
                    if (pmmList[i - 1].ProductID == _invMovementList[i].ProductID)
                        sumTrnQty = (pmmList[i - 1].DTBalance + _invMovementList[i].TrnQtyIn) - _invMovementList[i].TrnQtyOut;

                    var _pmm = pmmList.OrderBy(x => x.TrnDate).FirstOrDefault(x => x.ProductID == _invMovementList[i].ProductID);
                    if (_pmm != null)
                    {
                        //pmm.ForwardQty = _pmm.ForwardQty;
                        pmm.ForwardQty = pmmList[i - 1].DTBalance; //08012021
                    }
                }
                else
                {
                    pmm.ForwardQty = _invMovementSumList_BLC.Count > 0 ? _invMovementSumList_BLC.Sum(x => x.TrnQty) : 0;
                    sumTrnQty = (pmm.ForwardQty + _invMovementList[i].TrnQtyIn) - _invMovementList[i].TrnQtyOut;

                    //if (_invMovementSumList != null && _invMovementSumList.Count > 0)
                    //{
                    //    sumTrnQty = (_invMovementSumList.Sum(x => x.TrnQty) + _invMovementList[i].TrnQtyIn) - _invMovementList[i].TrnQtyOut;
                    //    //pmm.ForwardQty = _invMovementSumList.Sum(x => x.TrnQty);
                    //    pmm.DTBalance = sumTrnQty;
                    //}
                    //else
                    //{
                    //    //11012021 case TrnQtyIn from TR
                    //    sumTrnQty = (_invMovementSumList_BLC.Sum(x => x.TrnQty) + _invMovementSumList_BLC[i].TrnQtyIn) - _invMovementSumList_BLC[i].TrnQtyOut;
                    //    //pmm.ForwardQty = _invMovementSumList.Sum(x => x.TrnQty);
                    //    pmm.DTBalance = sumTrnQty;
                    //}
                }


                pmm.DTBalance = sumTrnQty;
                pmm.WHID = _invMovementList[i].WHID; //whid;

                pmmList.Add(pmm);
            }

            return pmmList;
        }

        private void PeapreData(string prdGroupID, string prdSubGroupID, List<string> prdCodeList)
        {
            Func<tbl_Product, bool> tbl_ProductFunc = (x => x.FlagDel == false && x.ProductGroupID.ToString() == (prdGroupID != "-1" ? prdGroupID : x.ProductGroupID.ToString()) &&
                x.ProductSubGroupID.ToString() == (prdSubGroupID != "-1" ? prdSubGroupID : x.ProductSubGroupID.ToString()));

            productList = (new tbl_Product()).Select(tbl_ProductFunc);
            allProductList = productList;

            if (prdCodeList.Count > 0)
                productList = productList.Where(x => prdCodeList.Contains(x.ProductCode)).ToList();

            Func<tbl_InvMovement, bool> allInvMovementList_func = (x => productList.Select(p => p.ProductID).Contains(x.ProductID));// && x.TrnType != "X");

            MemoryManagement.FlushMemory();

            var tmp = new tbl_InvMovement().SelectForMovement();

            MemoryManagement.FlushMemory();

            List<tbl_InvMovement> mmList = new List<tbl_InvMovement>();
            foreach (DataRow row in tmp.Rows)
            {
                tbl_InvMovement pmm = new tbl_InvMovement();
                pmm.WHID = row["WHID"].ToString();
                pmm.ToWHID = row["ToWHID"].ToString();
                pmm.ProductID = row["ProductID"].ToString();
                pmm.ProductName = row["ProductName"].ToString();
                pmm.TrnDate = Convert.ToDateTime(row["TrnDate"]);
                pmm.RefDocNo = row["RefDocNo"].ToString();
                pmm.TrnType = row["TrnType"].ToString();
                pmm.TrnQtyIn = Convert.ToDecimal(row["TrnQtyIn"]);
                pmm.TrnQtyOut = Convert.ToDecimal(row["TrnQtyOut"]);
                pmm.TrnQty = Convert.ToDecimal(row["TrnQty"]);
                pmm.CrDate = Convert.ToDateTime(row["CrDate"]);

                mmList.Add(pmm);
            }

            allInvMovementList = mmList.Where(allInvMovementList_func).ToList();
        }

        public List<ProductMovementModel> GetDataTable_SubDetails(string fwhid, string twhid, string prdGroupID, string prdSubGroupID, List<string> prdCodeList, DateTime fDate, DateTime tDate, bool isSummary = false)
        {
            try
            {
                if (!isSummary)
                    PeapreData(prdGroupID, prdSubGroupID, prdCodeList);

                invMovementList = new List<tbl_InvMovement>();
                invMovementList_BLC = new List<tbl_InvMovement>();

                List<ProductMovementModel> pmmList = new List<ProductMovementModel>();

                Func<tbl_InvMovement, bool> tbl_InvMovementFunc_BLC_func = (x => (x.TrnDate.ToDateTimeFormat() < fDate.ToDateTimeFormat()));
                var tmp_invMovementList_BLC = allInvMovementList.Where(tbl_InvMovementFunc_BLC_func).ToList(); //new tbl_InvMovement().Select(tbl_InvMovementFunc_BLC);

                Func<tbl_InvMovement, bool> tbl_InvMovementFunc = (x => (x.TrnDate.ToDateTimeFormat() >= fDate.ToDateTimeFormat() && x.TrnDate.ToDateTimeFormat() <= tDate.ToDateTimeFormat()));
                var tmp_invMovementList = allInvMovementList.Where(tbl_InvMovementFunc).ToList(); // new tbl_InvMovement().Select(tbl_InvMovementFunc);

                if (isSummary)
                {
                    var allWHID = GetAllBranchWarehouse();
                    int fSeq = Convert.ToInt32(allWHID.First(x => x.WHID == fwhid).WHSeq);
                    int tSeq = Convert.ToInt32(allWHID.First(x => x.WHID == twhid).WHSeq);

                    var listOfWHID = allWHID.Where(x => Convert.ToInt32(x.WHSeq) >= fSeq && Convert.ToInt32(x.WHSeq) <= tSeq).Select(a => a.WHID).ToList();

                    if (fwhid != twhid)
                    {
                        invMovementList_BLC.AddRange(tmp_invMovementList_BLC.Where(x => listOfWHID.Contains(x.WHID)).ToList());
                        invMovementList.AddRange(tmp_invMovementList.Where(x => listOfWHID.Contains(x.WHID)).ToList());
                    }
                    else
                    {
                        invMovementList_BLC.AddRange(tmp_invMovementList_BLC.Where(x => x.WHID == fwhid));
                        invMovementList.AddRange(tmp_invMovementList.Where(x => x.WHID == fwhid));
                    }
                }
                else
                {
                    invMovementList_BLC.AddRange(tmp_invMovementList_BLC.Where(x => x.WHID == fwhid));
                    invMovementList.AddRange(tmp_invMovementList.Where(x => x.WHID == fwhid));
                }

                var productIDList = invMovementList.Select(x => x.ProductID).Distinct().OrderBy(x => x).ToList();
                foreach (var productID in productIDList)
                {
                    pmmList.AddRange(PrepareProductMovementModel(fwhid, twhid, prdGroupID, prdSubGroupID, productID, fDate, tDate));
                }

                return pmmList;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                return null;
            }
        }

        public DataTable GetDataTable_Details(string fwhid, string twhid, string prdGroupID, string prdSubGroupID, List<string> prdCodeList, DateTime fDate, DateTime tDate)
        {
            try
            {
                List<ProductMovementModel> pmmList = new List<ProductMovementModel>();

                pmmList = GetDataTable_SubDetails(fwhid, twhid, prdGroupID, prdSubGroupID, prdCodeList, fDate, tDate);

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

                var data = pmmList.OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ThenBy(x => x.ProductID).ThenBy(x => x.TrnDate).ThenBy(y => y.CrDate).ToList();
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

        public virtual DataTable GetDataTable(string fwhid, string twhid, string prdGroupID, string prdSubGroupID, List<string> prdCodeList, DateTime fDate, DateTime tDate)
        {
            try
            {
                PeapreData(prdGroupID, prdSubGroupID, prdCodeList);

                var tmp_invMovementList = allInvMovementList;

                var _tmp_invMovementList = new List<tbl_InvMovement>();

                var allWHID = GetAllBranchWarehouse();
                int fSeq = Convert.ToInt32(allWHID.First(x => x.WHID == fwhid).WHSeq);
                int tSeq = Convert.ToInt32(allWHID.First(x => x.WHID == twhid).WHSeq);

                var listOfWHID = allWHID.Where(x => Convert.ToInt32(x.WHSeq) >= fSeq && Convert.ToInt32(x.WHSeq) <= tSeq).Select(a => a.WHID).ToList();

                if (fwhid != twhid)
                {
                    _tmp_invMovementList.AddRange(tmp_invMovementList.Where(x => listOfWHID.Contains(x.WHID)).ToList());
                    //_tmp_invMovementList.AddRange(tmp_invMovementList.Where(x => x.WHID == fwhid));
                    //_tmp_invMovementList.AddRange(tmp_invMovementList.Where(x => x.WHID == twhid));
                }
                else
                {
                    _tmp_invMovementList.AddRange(tmp_invMovementList.Where(x => x.WHID == fwhid));
                }

                List<ProductMovementModel> pmmList = new List<ProductMovementModel>();

                List<tbl_ProductUomSet> productUomSetList = new List<tbl_ProductUomSet>();
                productUomSetList = (new tbl_ProductUomSet()).SelectAll();

                pmmList = GetDataTable_SubDetails(fwhid, twhid, prdGroupID, prdSubGroupID, prdCodeList, fDate, tDate, true);

                List<tbl_Product> prds = new List<tbl_Product>();
                if (prdCodeList.Count == 0)
                    prds = allProductList.GroupBy(x => x.ProductID).Select(a => a.First()).ToList();
                else
                {
                    //prds = productList.GroupBy(x => x.ProductID).Select(a => a.First()).ToList();
                    var tmp = pmmList.GroupBy(x => x.ProductID).Select(a => a.First()).ToList();
                    if (tmp != null && tmp.Count > 0)
                        prds = allProductList.Where(x => tmp.Select(a => a.ProductID).ToList().Contains(x.ProductID)).ToList();
                }

                var pmmListSummary = new List<ProductMovementModel>();

                foreach (var p in prds)
                {
                    ProductMovementModel pmm = new ProductMovementModel();
                    pmm.ProductID = p.ProductID;
                    pmm.ProductCode = p.ProductID;
                    pmm.ProductName = p.ProductName;

                    pmm.ProductGroupID = p.ProductGroupID;
                    pmm.ProductSubGroupID = p.ProductSubGroupID;

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

                    var tmp = pmmList.Where(x => x.ProductID == p.ProductID).ToList();
                    if (tmp != null && tmp.Count > 0)
                    {
                        decimal forwardQty = tmp.First().ForwardQty;
                        decimal bastQty = puom.BaseQty;

                        decimal temp_qty = forwardQty / bastQty;
                        pmm.ImpLargeQty = Convert.ToInt32(temp_qty.ToString().Split('.')[0]);
                        pmm.ImpSmallQty = Convert.ToInt32(forwardQty % bastQty);

                        temp_qty = tmp.Sum(x => x.InQty);
                        pmm.InQty = Convert.ToInt32(temp_qty);
                        temp_qty = tmp.Sum(x => x.OutQty);
                        pmm.OutQty = Convert.ToInt32(temp_qty);

                        var _invMovementList = _tmp_invMovementList.Where(x => x.ProductID == p.ProductID).ToList();
                        temp_qty = _invMovementList.Sum(x => x.TrnQty) / bastQty;
                        pmm.QtyOnHandLarge = Convert.ToInt32(temp_qty.ToString().Split('.')[0]);
                        pmm.QtyOnHandSmall = Convert.ToInt32(_invMovementList.Sum(x => x.TrnQty) % bastQty);
                    }
                    else
                    {
                        pmm.ImpLargeQty = 0;
                        pmm.ImpSmallQty = 0;
                        pmm.InQty = 0;
                        pmm.OutQty = 0;
                        pmm.QtyOnHandLarge = 0;
                        pmm.QtyOnHandSmall = 0;
                    }

                    pmmListSummary.Add(pmm);
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

                var data = pmmListSummary.OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ToList();
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
