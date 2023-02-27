using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using System.Globalization;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmTR : Form
    {
        MenuBU menuBU = new MenuBU();
        TR bu = new TR();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();

        string docTypeCode = "";
        int runDigit = 0;
        List<Control> searchBranchControls = new List<Control>();
        List<Control> searchFromBWHControls = new List<Control>();
        List<Control> searchToBWHControls = new List<Control>();
        List<Control> searchFromPrdControls = new List<Control>();
        List<Control> searchToPrdControls = new List<Control>();
        List<string> readOnlyControls = new List<string>();

        Func<tbl_BranchWarehouse, bool> fbiPredicate = null;
        List<tbl_Product> allProduct = new List<tbl_Product>();
        List<tbl_ProductUomSet> allUomSet = new List<tbl_ProductUomSet>();
        List<tbl_ProductUom> allUOM = new List<tbl_ProductUom>();
        //List<tbl_ProductPriceGroup> allProductPrice = new List<tbl_ProductPriceGroup>();
        List<tbl_ProductGroup> allProdGroup = new List<tbl_ProductGroup>();
        List<tbl_ProductSubGroup> allProdSubGroup = new List<tbl_ProductSubGroup>();

        public frmTR()
        {
            InitializeComponent();

            searchBranchControls = new List<Control>() { txtBranchCode, txtBranchName };
            searchFromBWHControls = new List<Control> { txtFromWHCode, txtFromWHName };
            searchToBWHControls = new List<Control> { txtToWHCode, txtToWHName };
            searchFromPrdControls = new List<Control> { txtFromProductCode, txtFromProductName };
            searchToPrdControls = new List<Control> { txtToProductCode, txtToProductName };

            readOnlyControls = new string[] { txtBranchCode.Name, txtBranchName.Name, txtFromWHCode.Name, txtFromWHName.Name,
                txtToWHCode.Name, txtToWHName.Name, txtFromProductName.Name,
                txtFromStock.Name, txtToStock.Name, txtToProductName.Name, txtCrUser.Name, dtpDocDate.Name }.ToList();

            txdDocNo.KeyDown += TxdDocNo_KeyDown;

            //txtBranchCode.KeyDown += TxtFromBranchID_KeyDown;
            //txtFromWHCode.KeyDown += TxtFromWHCode_KeyDown;
            //txtToWHCode.KeyDown += TxtToWHCode_KeyDown;
            txtFromProductCode.KeyDown += TxtFromProductCode_KeyDown;
            txtToProductCode.KeyDown += TxtToProductCode_KeyDown;
        }

        #region private methods

        private void InitPage()
        {
            var documentType = bu.GetDocumentType().FirstOrDefault(x => x.DocTypeCode.Trim() == "TR");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                runDigit = documentType.DocFormat.Length - 2;
                FormHeader.Text = documentType.DocHeader;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");

                this.ClearControl(docTypeCode, runDigit);
            }

            btnAdd.Enabled = true;

            this.OpenControl(false, readOnlyControls.ToArray(), null);

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");

            var headerPic = menuBU.GetAllData().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            //header control
            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;
            btnSearchDoc.Enabled = true;

            dtpDocDate.SetDateTimePickerFormat();

            allProduct = bu.GetProduct();
            allUomSet = bu.GetUOMSet();

            //allProductPrice = bu.GetProductPriceGroup();
            allProdGroup = bu.GetProductGroup();
            allProdSubGroup = bu.GetProductSubGroup();
        }

        public void BindTRData(string trDocNo)
        {
            bu.GetDocData(trDocNo, docTypeCode);

            var pr = bu.tbl_PRMaster;
            var prDts = bu.tbl_PRDetails;

            if (string.IsNullOrEmpty(pr.DocNo))
            {
                string msg = "ไม่พบข้อมูลเลขที่เอกสาร!!!";
                msg.ShowWarningMessage();

                btnCancel.PerformClick();

                return;
            }

            if (pr != null)
            {
                BindPRMaster(pr);
            }
            if (prDts != null && prDts.Count > 0)
            {
                BindPRDetail(prDts);
            }

            this.OpenControl(false, readOnlyControls.ToArray(), null);

            btnSearchDoc.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");

            bool checkEditMode = bu.CheckExistsPR(trDocNo);
            pr.DocStatus.CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
        }

        private void BindPRMaster(tbl_PRMaster pr)
        {
            txdDocNo.Text = pr.DocNo;
            dtpDocDate.Value = pr.DocDate.ToDateTimeFormat();

            var branch = bu.GetBranch();
            if (branch != null && branch.Count > 0)
            {
                txtBranchCode.Text = pr.FromBranchID;
                txtBranchName.Text = branch.FirstOrDefault(x => x.BranchCode == pr.FromBranchID).BranchName;
            }

            var bwh = bu.GetAllBranchWarehouse();
            Func<tbl_BranchWarehouse, bool> bwhPredicate_From = (x => x.WHID == pr.FromWHID);
            var frombwh = bwh.Where(bwhPredicate_From).ToList();
            if (frombwh != null && frombwh.Count > 0)
            {
                txtFromWHCode.Text = frombwh[0].WHCode;
                txtFromWHName.Text = frombwh[0].WHName;
            }

            Func<tbl_BranchWarehouse, bool> bwhPredicate_To = (x => x.WHID == pr.ToWHID);
            var tobwh = bwh.Where(bwhPredicate_To).ToList();
            if (tobwh != null && tobwh.Count > 0)
            {
                txtToWHCode.Text = tobwh[0].WHCode;
                txtToWHName.Text = tobwh[0].WHName;
            }

            if (ddlDocStatus.DataSource == null)
            {
                var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
                ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
            }
            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == pr.DocStatus; };
            ddlDocStatus.SelectedValueDropdownList(condition);

            var user = bu.GetEmployeeByUserName(pr.CrUser);
            txtCrUser.Text = string.Join(" ", user.TitleName, user.FirstName);

            txtRemark.Text = pr.Remark;
        }

        private void BindPRDetail(List<tbl_PRDetail> prDts)
        {
            var whid = txtBranchCode.Text + txtToWHCode.Text;

            for (int i = 0; i < prDts.Count; i++)
            {
                if (prDts[i].OrderQty > 0)
                {
                    txtToProductCode.Text = prDts[i].ProductID;
                    txtToProductName.Text = prDts[i].ProductName;
                    txtToStock.BindStockOnHand(allUomSet, bu, whid, prDts[i].ProductID, prDts[i].OrderQty.Value, txtOrderQty, lblUOM); 
                }
                else
                {
                    txtFromProductCode.Text = prDts[i].ProductID;
                    txtFromProductName.Text = prDts[i].ProductName;
                    txtOrderQty.Text = prDts[i].OrderQty.Value.ToStringN0();
                    txtFromStock.BindStockOnHand(allUomSet, bu, whid, prDts[i].ProductID, 0);
                    if (prDts[i].OrderUom != 0)
                    {
                        lblUOM.Text = allUomSet.First(x => x.UomSetID == prDts[i].OrderUom).UomSetName;
                    }
                    
                }
            }
        }

        private void InitHeader()
        {
            var b = bu.GetBranch();
            if (b != null)
            {
                this.BindData("FromBranchID", searchBranchControls, b[0].BranchID);
            }
            
            this.BindData("BranchWarehouse", searchFromBWHControls, "1000");
            this.BindData("BranchWarehouse", searchToBWHControls, "1000");

            var employee = bu.GetEmployee(Helper.tbl_Users.EmpID);
            txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName);

            dtpDocDate.SetDateTimePickerFormat();

            var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
            ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);

            txtBaseQty.Enabled = chkFixBaseQty.Checked;
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            this.OpenControl(true, readOnlyControls.ToArray(), null);

            InitHeader();

            btnSearchBranchCode.Enabled = false;
            btnSearchFromWHCode.Enabled = false;
            btnSeatchToWHCode.Enabled = false;

            txtOrderQty.Text = "0";
        }

        private void PreparePRMaster(bool editFlag = false)
        {
            bu.tbl_PRMaster = new tbl_PRMaster();

            var emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            var supp = bu.GetSupplier(txtBranchCode.Text);
            var branch = bu.GetBranch();

            var pr = bu.tbl_PRMaster;
            bool checkEditMode = bu.CheckExistsPR(txdDocNo.Text);
            if (checkEditMode)
                pr.DocNo = txdDocNo.Text;
            else
                pr.DocNo = bu.GenDocNo(docTypeCode);

            pr.RevisionNo = 0;
            pr.DocTypeCode = docTypeCode;
            pr.DocStatus = ddlDocStatus.SelectedValue.ToString();
            pr.DocDate = dtpDocDate.Value.ToDateTimeFormat();
            pr.DocRef = "";
            pr.FromBranchID = txtBranchCode.Text;

            pr.RequestBy = emp.EmpCode;
            pr.ToBranchID = txtBranchCode.Text;

            var allBwh = bu.GetAllBranchWarehouse();
            var fromWhid = txtFromWHCode.Text;
            var frombwh = allBwh.FirstOrDefault(x => x.WHCode == fromWhid);

            if (frombwh != null)
                pr.FromWHID = frombwh.WHID;

            var toWhid = txtToWHCode.Text;
            var tobwh = allBwh.FirstOrDefault(x => x.WHCode == toWhid);
            if (tobwh != null)
                pr.ToWHID = tobwh.WHID;

            pr.StatusInOut = "T";
            pr.Address = null;
            pr.ReceiveDate = null;
            pr.ReceiveBy = "0";
            pr.ShipDate = null;
            pr.ShipBy = "0";
            pr.ShipWHID = "0";
            pr.SalAreaID = "0";
            pr.EmpID = emp.EmpID;
            pr.ContactName = null;
            pr.ContactTel = null;
            pr.Shipto = null;
            pr.Remark = txtRemark.Text;

            var documentType = bu.GetDocumentType().FirstOrDefault(x => x.DocTypeCode.Trim() == "TR");
            if (documentType != null)
                pr.Comment = documentType.DocRemark;

            pr.ApprovedBy = null;
            pr.ApprovedDate = null;
            pr.CrDate = DateTime.Now;
            pr.CrUser = Helper.tbl_Users.Username;

            if (editFlag)
            {
                pr.EdDate = DateTime.Now;
                pr.EdUser = Helper.tbl_Users.Username;
            }

            pr.EdDate = null;
            pr.EdUser = null;
            pr.FlagDel = false;
            pr.FlagSend = false;
        }

        private void PreparePRDetail(bool editFlag = false)
        {
            bu.tbl_PRDetails.Clear();

            var prDts = bu.tbl_PRDetails;

            var fromPrdCode = txtFromProductCode;
            var fromPrdName = txtFromProductName;
            var toPrdCode = txtToProductCode;
            var toPrdName = txtToProductName;

            var fromDT = PrepareSubPRDetail(fromPrdCode, fromPrdName, true, editFlag);
            prDts.Add(fromDT);

            var toDT = PrepareSubPRDetail(toPrdCode, toPrdName, false, editFlag);
            prDts.Add(toDT);
        }

        private tbl_PRDetail PrepareSubPRDetail(TextBox prdCode, TextBox prdName, bool isReduce, bool editFlag = false)
        {
            var prDt = new tbl_PRDetail();

            if (!string.IsNullOrEmpty(prdCode.Text) && !string.IsNullOrEmpty(prdName.Text))
            {
                DateTime crDate = DateTime.Now;

                prDt.DocNo = bu.tbl_PRMaster.DocNo;
                prDt.ProductID = prdCode.Text;
                prDt.Line = (isReduce) ? Convert.ToInt16(0) : Convert.ToInt16(1);
                prDt.ProductName = prdName.Text;

                decimal orderQty = 0;

                //spacial case
                bool isSpacialCase = true;
                if (!isReduce)
                {
                    var minUomID = 0;

                    var minUom = allUomSet.Where(x => x.ProductID == prdCode.Text).ToList();
                    if (minUom.Count > 0)
                    {
                        var minItem = minUom.OrderBy(x => x.BaseQty).First();
                        if (minItem != null)
                            minUomID = minItem.UomSetID;
                    }

                    var prdTemp = allProduct.FirstOrDefault(x => x.ProductID == prdCode.Text);

                    if (!chkFixBaseQty.Checked &&
                        (prdTemp != null && prdTemp.PurchaseUomID == 2) &&
                        minUomID != 0)  //for pack and piece
                    {
                        prDt.OrderUom = minUomID;

                        var _orderQty = isReduce ? -Convert.ToDecimal(txtOrderQty.Text) : Convert.ToDecimal(txtOrderQty.Text);
                        var _baseQty = minUom.Max(y => y.BaseQty);

                        orderQty = _orderQty * _baseQty;
                    }
                    else if (chkFixBaseQty.Checked) //01032021 by sailom
                    {
                        prDt.OrderUom = minUomID;

                        var _orderQty = isReduce ? -Convert.ToDecimal(txtOrderQty.Text) : Convert.ToDecimal(txtOrderQty.Text);
                        int _baseQty = 1;
                        if (!string.IsNullOrEmpty(txtBaseQty.Text))
                            _baseQty = Convert.ToInt32(txtBaseQty.Text);
                        else
                        {
                            _baseQty = minUom.Max(y => y.BaseQty);
                        }

                        orderQty = _orderQty * _baseQty;
                    }
                    else
                        isSpacialCase = false;
                }
                else //normal case
                {
                    isSpacialCase = false;
                }

                if (!isSpacialCase)
                {
                    
                    Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => x.ProductID == prdCode.Text && x.UomSetID == 2);
                    var prodUomSetPack = bu.GetUOMSet(tbl_ProductUomSetPre);
                    if (prodUomSetPack != null && prodUomSetPack.Count > 0)
                        prDt.OrderUom = prodUomSetPack[0].UomSetID;
                    else
                    {
                        Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPreOriginal = (x => x.ProductID == prdCode.Text);
                        var prodUomSetOri = bu.GetUOMSet(tbl_ProductUomSetPreOriginal);
                        prDt.OrderUom = prodUomSetOri[0].UomSetID;
                    }

                    orderQty = isReduce ? -Convert.ToDecimal(txtOrderQty.Text) : Convert.ToDecimal(txtOrderQty.Text);
                }

                prDt.OrderQty = orderQty;
                prDt.SendQty = orderQty;
                prDt.ReceivedQty = orderQty;
                prDt.RejectedQty = 0;
                prDt.StockedQty = 0;
                prDt.UnitCost = 0;
                prDt.UnitPrice = 0;
                prDt.VatType = 0;
                prDt.LineTotal = 0;
                prDt.LineRemark = "";
                prDt.CrDate = crDate;
                prDt.CrUser = Helper.tbl_Users.Username;
                if (editFlag)
                {
                    prDt.EdDate = crDate;
                    prDt.EdUser = Helper.tbl_Users.Username;
                }
                prDt.FlagDel = false;
                prDt.FlagSend = false;
            }

            return prDt;
        }

        private void PrepareInvMovement(bool editFlag = false)
        {
            bu.tbl_InvMovements.Clear();

            var invMms = bu.tbl_InvMovements;
            var prDts = bu.tbl_PRDetails;
            var pr = bu.tbl_PRMaster;
            //var prod = bu.GetProduct();
            //var prodGroup = bu.GetProductGroup();
            //var prodSubGroup = bu.GetProductSubGroup();

            DateTime crDate = DateTime.Now;
            string trnType = "T";

            foreach (var prDt in prDts)
            {
                var invMm = new tbl_InvMovement();

                invMm.ProductID = prDt.ProductID;
                invMm.ProductName = prDt.ProductName;
                invMm.RefDocNo = prDt.DocNo;
                invMm.TrnDate = crDate.ToDateTimeFormat();
                invMm.TrnType = trnType;
                invMm.DocTypeCode = pr.DocTypeCode;

                decimal unitQty = 0;
                unitQty = prDt.OrderQty.Value;

                if (prDt.OrderQty > 0)
                {
                    invMm.WHID = pr.ToWHID;
                    invMm.FromWHID = "";
                    invMm.ToWHID = "";
                    invMm.TrnQtyIn = unitQty;
                    invMm.TrnQtyOut = 0;
                    invMm.TrnQty = unitQty;
                }
                else
                {
                    invMm.WHID = pr.FromWHID;
                    invMm.FromWHID = pr.FromWHID;
                    invMm.ToWHID = pr.ToWHID;
                    invMm.TrnQtyIn = 0;
                    invMm.TrnQtyOut = unitQty * -1;
                    invMm.TrnQty = unitQty;
                }

                invMm.CrDate = crDate;

                if (editFlag)
                {
                    invMm.EdDate = crDate;
                    invMm.TrnType = ddlDocStatus.SelectedValue.ToString() == "5" ? "X" : trnType;
                }

                var prodItem = allProduct.FirstOrDefault(x => x.ProductID == prDt.ProductID);
                var prodGroupItem = allProdGroup.FirstOrDefault(x => x.ProductGroupID == prodItem.ProductGroupID);
                var prodSubGroupItem = allProdSubGroup.FirstOrDefault(x => x.ProductSubGroupID == prodItem.ProductSubGroupID);

                invMm.ProductGroupCode = prodGroupItem.ProductGroupCode;
                invMm.ProductGroupName = prodGroupItem.ProductGroupName;
                invMm.ProductSubGroupCode = prodSubGroupItem.ProductSubGroupCode;
                invMm.ProductSubGroupName = prodSubGroupItem.ProductSubGroupName;
                invMm.FlagSend = false;

                invMms.Add(invMm);
            }
        }

        private void PrepareQtyOnHand(tbl_InvWarehouse invWh, string productID, string whid, decimal unitQty, bool isFrom)
        {
            var invWhItem = bu.GetInvWarehouse(productID, whid);
            if (invWhItem != null && invWhItem.Count > 0)
            {
                if (isFrom)
                {
                    if (unitQty > 0)
                        invWh.QtyOnHand = invWhItem[0].QtyOnHand - unitQty;
                    else
                        invWh.QtyOnHand = invWhItem[0].QtyOnHand + unitQty;
                }
                else
                    invWh.QtyOnHand = invWhItem[0].QtyOnHand + unitQty;
            }
            else
            {
                if (isFrom)
                {
                    if (unitQty > 0)
                        invWh.QtyOnHand = -unitQty;
                    else
                        invWh.QtyOnHand = unitQty;
                }
                else
                    invWh.QtyOnHand = +unitQty;
            }
        }

        private void PrepareInvWarehouseFrom(bool editFlag = false)
        {
            bu.tbl_InvWarehouses.Clear();

            var pr = bu.tbl_PRMaster;

            SubPrepareInvWarehouse(pr.FromWHID, true, editFlag);
        }

        private void PrepareInvWarehouseTo(bool editFlag = false)
        {
            var pr = bu.tbl_PRMaster;

            SubPrepareInvWarehouse(pr.ToWHID, false, editFlag);
        }

        private void SubPrepareInvWarehouse(string whid, bool isFrom, bool editFlag = false)
        {
            var invWhs = bu.tbl_InvWarehouses;
            var prDts = bu.tbl_PRDetails;

            DateTime crDate = DateTime.Now;

            if (isFrom) //15022021
                prDts = prDts.Where(x => x.OrderQty < 0).ToList();
            else
                prDts = prDts.Where(x => x.OrderQty > 0).ToList();

            foreach (var prDt in prDts)
            {
                var invWh = new tbl_InvWarehouse();

                invWh.ProductID = prDt.ProductID;
                invWh.WHID = whid;
                invWh.QtyOnHand = 0;

                decimal unitQty = 0;
                unitQty = prDt.OrderQty.Value;
                
                var prdUOMSets = bu.GetProductUOMSet(allUomSet, prDt.ProductID);
                if (prdUOMSets != null && prdUOMSets.Count > 0)
                {
                    PrepareQtyOnHand(invWh, prDt.ProductID, whid, unitQty, isFrom);
                }
                else
                {
                    PrepareQtyOnHand(invWh, prDt.ProductID, whid, unitQty, isFrom);
                }

                invWh.QtyOnOrder = 0;
                invWh.QtyOnBackOrder = 0;
                invWh.QtyInTransit = 0;
                invWh.QtyOutTransit = 0;
                invWh.QtyOnReject = 0;
                invWh.MinimumQty = 0;
                invWh.MaximumQty = 0;
                invWh.ReOrderQty = 0;
                invWh.CrDate = crDate;
                invWh.CrUser = Helper.tbl_Users.Username;

                if (editFlag)
                {
                    invWh.EdDate = crDate;
                    invWh.EdUser = Helper.tbl_Users.Username;
                }

                invWh.FlagDel = false;
                invWh.FlagSend = false;

                invWhs.Add(invWh);
            }
        }

        private void Save()
        {
            try
            {
                string docno = string.Empty;
                bool editFlag = true;
                int ret = 0;

                bool checkEditMode = bu.CheckExistsPR(txdDocNo.Text);
                if (checkEditMode)
                {
                    string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                    string title = "ยืนยันการบันทึก!!";
                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    bu = new TR();

                    docno = txdDocNo.Text;
                    editFlag = true;
                    bu.tbl_DocRunning = new List<tbl_DocRunning>();

                    bu.tbl_PRMaster = null;
                    bu.tbl_PRMaster = bu.GetPRMaster(docno);
                    bu.tbl_PRMaster.DocStatus = ddlDocStatus.SelectedValue.ToString();

                    bu.tbl_InvMovements.Clear();
                    bu.tbl_InvMovements.AddRange(bu.GetInvMovement(docno));

                    if (ddlDocStatus.SelectedValue.ToString() == "5")
                        bu.tbl_InvMovements.ForEach(x => x.TrnType = "X");

                    bu.tbl_InvWarehouses.Clear();

                    string fromWHID = bu.tbl_PRMaster.FromWHID;
                    string toWHID = bu.tbl_PRMaster.ToWHID;
                    var prDts = bu.GetPRDetails(docno);

                    foreach (var prDt in prDts)
                    {
                        SetWarehousesQTY(prDt, fromWHID, editFlag, true);
                        SetWarehousesQTY(prDt, toWHID, editFlag, false);
                    }

                    ret = bu.UpdateData();
                }
                else
                {
                    if (!ValidateSave())
                        return;

                    string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                    string title = "ยืนยันการบันทึก!!";
                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    //docno = bu.GenDocNo(docTypeCode);
                    editFlag = false;
                    bu.PrepareDocRunning(docTypeCode);

                    PreparePRMaster(editFlag);
                    PreparePRDetail(editFlag);
                    PrepareInvMovement(editFlag);
                    PrepareInvWarehouseFrom(editFlag);
                    PrepareInvWarehouseTo(editFlag);

                    ret = bu.RemovePRDetails(bu.tbl_PRMaster.DocNo);
                    if (ret == 0)
                    {
                        this.ShowProcessErr();
                        return;
                    }

                    ret = bu.RemoveInvMovements(bu.tbl_PRMaster.DocNo);
                    if (ret == 0)
                    {
                        this.ShowProcessErr();
                        return;
                    }

                    ret = bu.UpdateData();
                }

                if (ret == 1)
                {
                    this.OpenControl(false, readOnlyControls.ToArray(), null);

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, txdDocNo.Text);

                    txdDocNo.Text = bu.tbl_PRMaster.DocNo;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    ddlDocStatus.SelectedValue.ToString().CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);

                    //refresh InvWarehouse
                    var prDts = bu.GetPRDetails(txdDocNo.Text);
                    BindPRDetail(prDts);
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void SetQtyOnHand(tbl_InvWarehouse invWh, decimal unitQty, string productID, string whID, bool editFlag, bool isFrom)
        {
            var invWhItem = bu.GetInvWarehouse(productID, whID);
            if (editFlag)
            {
                if (invWhItem != null && invWhItem.Count > 0)
                {
                    if (ddlDocStatus.SelectedValue.ToString() == "5")
                    {
                        if (isFrom)
                            invWh.QtyOnHand = invWhItem[0].QtyOnHand + unitQty;
                        else
                            invWh.QtyOnHand = invWhItem[0].QtyOnHand - unitQty;
                    }
                }
            }
            else
            {
                if (invWhItem != null && invWhItem.Count > 0)
                {
                    invWh.QtyOnHand = invWhItem[0].QtyOnHand + unitQty;
                }
                else
                {
                    invWh.QtyOnHand = unitQty;
                }
            }
        }

        private void SetWarehousesQTY(tbl_PRDetail prDt, string whid, bool editFlag, bool isFrom)
        {
            var invWh = new tbl_InvWarehouse();
            var invWhs = bu.GetInvWarehouse(prDt.ProductID, whid);
            invWh = invWhs[0];

            decimal unitQty = 0;

            var prdUOMSets = bu.GetProductUOMSet(allUomSet, prDt.ProductID);
            if (prdUOMSets != null && prdUOMSets.Count > 0)
            {
                //if (prDt.OrderUom != 2)
                //    unitQty = (prDt.ReceivedQty.Value * prdUOMSets[0].BaseQty);
                var uom = allUomSet.FirstOrDefault(x => x.ProductID == prDt.ProductID && x.UomSetID == prDt.OrderUom);

                if (uom != null)//if (prDt.OrderUom != 2)
                    unitQty = (prDt.ReceivedQty.Value * uom.BaseQty);
                else
                    unitQty = prDt.ReceivedQty.Value;
            }
            else
            {
                unitQty = prDt.ReceivedQty.Value;
            }

            SetQtyOnHand(invWh, unitQty, prDt.ProductID, whid, editFlag, isFrom);

            bu.tbl_InvWarehouses.Add(invWh);
        }

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            var cDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Ticks;
            var docDate = new DateTime(dtpDocDate.Value.Year, dtpDocDate.Value.Month, dtpDocDate.Value.Day).Ticks;

            if (dtpDocDate.Value != null && docDate < cDate)
            {
                string message = "ห้ามเลือกวันที่ย้อนหลัง !!!";
                message.ShowWarningMessage();
                ret = false;
            }

            if (ret)
            {
                if (!dtpDocDate.ValidateEndDay(bu))
                {
                    string message = "ระบบปิดวันไปแล้ว ไม่สามารถเลือกวันที่นี้ได้ !!!";
                    message.ShowWarningMessage();
                    ret = false;
                }
            }

            if (ret) //validate header
            {
                errList.SetErrMessageList(txtBranchCode, lblBranchCode);
                errList.SetErrMessageList(txtFromWHCode, lblFromWHCode);
                errList.SetErrMessageList(txtToWHCode, lblToWHCode);
                errList.SetErrMessageList(txtCrUser, lblCrUser);
                errList.SetErrMessageList(txtFromProductCode, lblFromProductCode);
                errList.SetErrMessageList(txtToProductCode, lblToProductCode);
                errList.SetErrMessageList(txtOrderQty, lblOrderQty);

                if (errList.Count == 0)
                {
                    var branch = bu.GetBranch();
                    if (branch == null)
                    {
                        string t = lblBranchCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtBranchCode.ErrorTextBox();
                    }

                    var bwh = bu.GetAllBranchWarehouse();
                    Func<tbl_BranchWarehouse, bool> fromBranchWarehousePre = (x => x.WHCode.ToLower() == txtFromWHCode.Text.ToLower());
                    var fromwh = bwh.Where(fromBranchWarehousePre).ToList();
                    if (fromwh == null || fromwh.Count == 0)
                    {
                        string t = lblFromWHCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtFromWHCode.ErrorTextBox();
                    }

                    Func<tbl_BranchWarehouse, bool> toBranchWarehousePre = (x => x.WHCode.ToLower() == txtToWHCode.Text.ToLower());
                    var towh = bwh.Where(toBranchWarehousePre).ToList();
                    if (towh == null || towh.Count == 0)
                    {
                        string t = lblFromWHCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtToWHCode.ErrorTextBox();
                    }

                    Func<tbl_Product, bool> tbl_ProductFromPre = (x => x.ProductCode.ToLower() == txtFromProductCode.Text.ToLower());
                    var fromPrd = bu.GetProduct(tbl_ProductFromPre);
                    if (fromPrd == null)
                    {
                        string t = lblFromProductCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtFromProductCode.ErrorTextBox();
                    }

                    Func<tbl_Product, bool> tbl_ProductToPre = (x => x.ProductCode.ToLower() == txtToProductCode.Text.ToLower());
                    var toPrd = bu.GetProduct(tbl_ProductToPre);
                    if (toPrd == null)
                    {
                        string t = lblToProductCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtToProductCode.ErrorTextBox();
                    }
                }

                if (errList.Count > 0)
                {
                    string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                    message.ShowWarningMessage();
                    ret = false;
                }
            }

            if (ret)
            {
                if (txtFromProductCode.Text.ToLower() == txtToProductCode.Text.ToLower())
                {
                    string message = "สินค้าปลายทางห้ามเป็น รหัสเดียวกับต้นทาง !!!";
                    message.ShowWarningMessage();
                    ret = false;
                    txtToProductCode.Focus();
                }
            }

            if (ret)
            {
                if (Convert.ToInt32(txtOrderQty.Text) <= 0)
                {
                    string message = "จำนวนโอนต้องมากกว่า 0 !!!";
                    message.ShowWarningMessage();
                    ret = false;
                    txtOrderQty.Focus();
                }
            }

            return ret;
        }

        #endregion

        #region event methods

        private void TxtFromBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    TextBox txt = (TextBox)sender;
            //    this.BindData("FromBranchID", searchBranchControls, txt.Text);
            //}
        }

        private void TxtFromWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    TextBox txt = (TextBox)sender;
            //    this.BindData("BranchWarehouse", searchFromBWHControls, txt.Text);
            //}
        }

        private void TxtToWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    TextBox txt = (TextBox)sender;
            //    this.BindData("BranchWarehouse", searchToBWHControls, txt.Text);
            //}
        }

        private void TxtToProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("TRProduct", searchToPrdControls, txt.Text);

                var whid = txtBranchCode.Text + txtToWHCode.Text;
                string prodID = txtToProductCode.Text;
                txtToStock.BindStockOnHand(allUomSet, bu, whid, prodID, 0);
            }
        }

        private void TxtFromProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("TRProduct", searchFromPrdControls, txt.Text);

                var whid = txtBranchCode.Text + txtFromWHCode.Text;
                string prodID = txtFromProductCode.Text;
                txtFromStock.BindStockOnHand(allUomSet, bu, whid, prodID, 0, null, lblUOM);
            }
        }

        private void TxdDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txdDocNo.Text))
                {
                    BindTRData(txdDocNo.Text);
                }
            }
        }

        private void frmTR_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InitialData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, txdDocNo.Text);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");
            ddlDocStatus.Enabled = true;
            btnCancel.Enabled = true;

            ddlDocStatus.Focus();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            btnCopy.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnPrint, txdDocNo.Text);

            this.OpenControl(true, readOnlyControls.ToArray(), null);

            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == "4"; };
            ddlDocStatus.SelectedValueDropdownList(condition);
            ddlDocStatus.Enabled = false;

            txdDocNo.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ClearControl(docTypeCode, runDigit);

            btnAdd.Enabled = true;
            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");

            this.OpenControl(false, readOnlyControls.ToArray(), null);

            btnCancel.EnableButton(btnSearchDoc);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@DocNo", txdDocNo.Text);
            this.OpenCrystalReportsPopup("ใบโอนย้ายสินค้า", "Form_TR.rpt", "Form_TR", _params);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchDoc_Click(object sender, EventArgs e)
        {
            this.OpenDocPopup("ใบโอนสินค้า Mat to Mat", docTypeCode);
        }

        private void btnSearchBranchCode_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกสาขา/ซุ้ม");
        }

        private void btnSearchFromWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchFromBWHControls, "เลือกคลังสินค้า", fbiPredicate);
        }

        private void btnSeatchToWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchToBWHControls, "เลือกคลังสินค้า", fbiPredicate);
        }

        private void btnSearchFromProductCode_Click(object sender, EventArgs e)
        {
            this.OpenProductPopup(searchFromPrdControls, "เลือกสินค้า");

            var whid = txtBranchCode.Text + txtFromWHCode.Text;
            string prodID = txtFromProductCode.Text;
            txtFromStock.BindStockOnHand(allUomSet, bu, whid, prodID, 0, null, lblUOM);
        }

        private void btnSearchToProductCode_Click(object sender, EventArgs e)
        {
            this.OpenProductPopup(searchToPrdControls, "เลือกสินค้า");

            var whid = txtBranchCode.Text + txtToWHCode.Text;
            string prodID = txtToProductCode.Text;
            txtToStock.BindStockOnHand(allUomSet, bu, whid, prodID, 0);
        }

        #endregion

        private void chkFixBaseQty_CheckedChanged(object sender, EventArgs e)
        {
            txtBaseQty.Enabled = chkFixBaseQty.Checked;
            if (!txtBaseQty.Enabled)
            {
                txtBaseQty.Text = string.Empty;
            }
        }
    }
}
