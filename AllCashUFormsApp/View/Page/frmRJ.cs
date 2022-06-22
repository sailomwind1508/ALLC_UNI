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
    public partial class frmRJ : Form
    {
        MenuBU menuBU = new MenuBU();
        RJ bu = new RJ();
        MasterDataControl mdBU = new MasterDataControl();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();

        List<tbl_ProductUom> uoms = new List<tbl_ProductUom>();
        List<tbl_Cause> causeList = new List<tbl_Cause>();

        bool validateNewRow = true;
        string docTypeCode = "";
        int runDigit = 0;

        List<Control> searchFromBWHControls = new List<Control>();
        List<string> readOnlyControls = new List<string>();

        int[] cellEdit = new int[] { 0, 3, 4, 5 };
        int[] numberCell = new int[] { 4 };
        Dictionary<int, string> dataGridList = new Dictionary<int, string>();
        Dictionary<int, string> initDataGridList = new Dictionary<int, string>();
        Func<tbl_BranchWarehouse, bool> fbiPredicate = (x => x.WHCode == "1900");
        List<tbl_Product> allProduct = new List<tbl_Product>();
        List<tbl_ProductUomSet> allUomSet = new List<tbl_ProductUomSet>();
        List<tbl_ProductUom> allUOM = new List<tbl_ProductUom>();
        //List<tbl_ProductPriceGroup> allProductPrice = new List<tbl_ProductPriceGroup>();
        List<tbl_ProductGroup> allProdGroup = new List<tbl_ProductGroup>();
        List<tbl_ProductSubGroup> allProdSubGroup = new List<tbl_ProductSubGroup>();

        public frmRJ()
        {
            InitializeComponent();
            searchFromBWHControls = new List<Control> { txtWHCode, txtWHName };
            readOnlyControls = new string[] { txtWHName.Name, txtCrUser.Name, dtpDocDate.Name }.ToList();

            dataGridList = new Dictionary<int, string>() { { 0, "ProductID" }, { 2, "ProductName" }, { 3, "UomSetID" } };
            initDataGridList = new Dictionary<int, string>() { { 0, "" }, { 2, "" }, { 3, "combobox1" }, { 4, "" }, { 5, "combobox2" } };

            txdDocNo.KeyDown += TxdDocNo_KeyDown;
            txtRBDoc.KeyDown += TxtRBDoc_KeyDown;
            dtpDocDate.ValueChanged += DtpDocDate_ValueChanged;

            txtWHCode.KeyDown += TxtFromWHCode_KeyDown;
        }

        #region private methods

        private void InitPage()
        {
            var documentType = bu.tbl_DocumentType.FirstOrDefault(x => x.DocTypeCode.Trim() == "RJ");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                runDigit = documentType.DocFormat.Length - 2;
                FormHeader.Text = documentType.DocHeader;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");

                this.ClearControl(docTypeCode, runDigit);
            }

            validateNewRow = true;
            btnAdd.Enabled = true;

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnPrintCrys.Enabled = btnPrint.Enabled;

            var headerPic = bu.tbl_MstMenu.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
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
            //dtpDueDate.SetDateTimePickerFormat();

            allUOM = bu.tbl_ProductUom;

            uoms.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
            //Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => uomList.Contains(x.ProductUomID));
            uoms.AddRange(allUOM); // bu.GetUOM(tbl_ProductUomPre));

            causeList.Add(new tbl_Cause { CauseID = -1, CauseName = "เลือก" });
            var cList = mdBU.GetProductRemarkRejectData("", 0).AsEnumerable().ToList();
            causeList.AddRange(cList.Select(x => new tbl_Cause { CauseID = x.Field<int>("RemarkRejectID"), CauseName = x.Field<string>("RemarkRejectName") })); //edit by sailom .k 21/12/2021

            //data gridview
            grdList.SetDataGridViewStyle();
            SetDefaultGridViewEvent(grdList);

            allProduct = bu.tbl_Product;
            allUomSet = bu.tbl_ProductUomSet;

            //allProductPrice = bu.tbl_ProductPriceGroup;
            allProdGroup = bu.tbl_ProductGroup;
            allProdSubGroup = bu.tbl_ProductSubGroup;
        }

        private void AddNewRow(DataGridView grd, int rowIndex)
        {
            if (!validateNewRow)
            {
                return;
            }

            grd.Rows.Add(1);
            //InitRowData("", rowIndex);
            grd.InitRowData(this, initDataGridList, 0, "", rowIndex, allProduct, uoms, causeList, bu, 0);
        }

        public void BindRJData(string rjDocNo)
        {
            bu.GetDocData(rjDocNo, docTypeCode);

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

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            btnSearchDoc.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
            btnPrintCrys.Enabled = btnPrint.Enabled;

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");

            grdList.CellContentClick -= grdList_CellContentClick;

            //CheckCancelDoc(po.DocStatus);
            bool checkEditMode = bu.CheckExistsPR(rjDocNo);
            pr.DocStatus.CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
        }

        public void BindRBData(string rbDocNo)
        {
            bu.GetDocData(rbDocNo, "RB");

            var pr = bu.tbl_PRMaster;
            var prDts = bu.tbl_PRDetails;

            if (string.IsNullOrEmpty(pr.DocNo))
            {
                string msg = "ไม่พบข้อมูลเลขที่เอกสาร!!!";
                msg.ShowWarningMessage();

                btnCancel.PerformClick();

                return;
            }

            txtRBDoc.Text = rbDocNo;

            if (prDts != null && prDts.Count > 0)
            {
                Func<tbl_POMaster, bool> poMaster = (x => x.DocNo == rbDocNo && x.DocTypeCode.Trim() == "RB");
                var checkCancelOD = bu.GetPOMaster(docTypeCode, poMaster);

                if (checkCancelOD.Count > 0 && checkCancelOD[0].DocStatus == "5")
                {
                    string msg = "ใบสั่งซื้อเลขที่ : " + pr.DocRef + " ถูกยกเลิกแล้ว ไม่สามารถอ้างอิงได้!";
                    msg.ShowWarningMessage();

                    return;
                }

                BindPRDetail(prDts, true);
            }

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;
        }

        private void BindPRMaster(tbl_PRMaster pr)
        {
            var bwh = bu.GetAllBranchWarehouse();
            Func<tbl_BranchWarehouse, bool> bwhPredicate = (x => x.WHID == pr.FromWHID);
            var _bwh = bwh.Where(bwhPredicate).ToList();
            if (_bwh != null && _bwh.Count > 0)
            {
                txtWHCode.Text = _bwh[0].WHCode;
                txtWHName.Text = _bwh[0].WHName;
            }

            var user = bu.GetEmployeeByUserName(pr.CrUser); //edit by sailom 22/03/2022 //bu.GetEmployeeByUserName(pr.EmpID);
            if (user != null)
                txtCrUser.Text = string.Join(" ", user.TitleName, user.FirstName, user.LastName);
            else
                txtCrUser.Text = pr.CrUser;

            txtRemark.Text = pr.Remark;

            txdDocNo.Text = pr.DocNo;
            dtpDocDate.Value = pr.DocDate.ToDateTimeFormat();
            txtRBDoc.Text = pr.DocRef;
            if (ddlDocStatus.DataSource == null)
            {
                var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
                ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
            }
            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == pr.DocStatus; };
            ddlDocStatus.SelectedValueDropdownList(condition);
        }

        private void BindPRDetail(List<tbl_PRDetail> prDts, bool isRB = false)
        {
            grdList.Rows.Clear();

            //var allUOM = bu.tbl_ProductUom;
            //var allProduct = bu.tbl_Product;

            for (int i = 0; i < prDts.Count; i++)
            {
                grdList.Rows.Add(1);

                grdList.InitRowData(this, initDataGridList, 0, "", i, allProduct, uoms, causeList, bu, 0);//edit by sailom 13-09-2021 erro seach data

                grdList.Rows[i].Cells[0].Value = prDts[i].ProductID;

                string productName = string.Empty;
                if (!string.IsNullOrEmpty(prDts[i].ProductName))
                {
                    productName = prDts[i].ProductName;
                }
                else
                {
                    var data = allProduct.FirstOrDefault(x => x.ProductID == prDts[i].ProductID);
                    if (data != null)
                    {
                        productName = data.ProductName;
                    }
                }

                grdList.Rows[i].Cells[2].Value = productName;
                grdList.BindComboBoxCell(allProduct, grdList.Rows[i], i, false, 3, uoms, this, bu, 0);
                grdList.BindComboBoxCell(grdList.Rows[i], i, false, 5, causeList, this, bu, 0);

                //edit by sailom 07/03/2022-------------------------------------------------
                decimal prDtQty = 0;
                if (isRB)
                    prDtQty = prDts[i].ReceivedQty.Value;
                else
                    prDtQty = prDts[i].RejectedQty.Value;

                grdList.Rows[i].Cells[3].Value = prDts[i].OrderUom;
                grdList.Rows[i].Cells[4].Value = prDtQty;
                //edit by sailom 07/03/2022-------------------------------------------------

                //edit by sailom 07/03/2022-------------------------------------------------
                //Func<tbl_Product, bool> predicate = (x => x.ProductID == prDts[i].ProductID);
                //int _minUOM = bu.GetMinProductUOM(predicate);

                //grdList.Rows[i].Cells[3].Value = _minUOM;
                //decimal prDtQty = 0;
                //if (isRB)
                //    prDtQty = prDts[i].ReceivedQty.Value;
                //else
                //    prDtQty = prDts[i].RejectedQty.Value;

                //decimal unitQty = 0;
                //if (isRB)
                //{
                //    var prdUOMSets = bu.GetProductUOMSet(allUomSet, prDts[i].ProductID);
                //    if (prdUOMSets != null && prdUOMSets.Count > 0)
                //    {
                //        if (prDts[i].OrderUom != _minUOM)
                //            unitQty = prDtQty * prdUOMSets[0].BaseQty;
                //        else
                //            unitQty = prDtQty;
                //    }
                //    else
                //        unitQty = prDtQty;
                //}
                //else
                //    unitQty = prDtQty;

                //grdList.Rows[i].Cells[4].Value = unitQty;
                //edit by sailom 07/03/2022-------------------------------------------------

                var causeID = -1;
                if (!string.IsNullOrEmpty(prDts[i].LineRemark))
                    causeID = causeList.FirstOrDefault(x => x.CauseName == prDts[i].LineRemark).CauseID;

                grdList.Rows[i].Cells[5].Value = causeID;
            }
        }

        public void BindSearchProduct(DataTable productDT, int rowIndex)
        {
            if (!string.IsNullOrEmpty(productDT.Rows[0]["ProductID"].ToString()))
            {
                if (!string.IsNullOrEmpty(grdList.Rows[rowIndex].Cells[0].EditedFormattedValue.ToString()))
                {
                    var cell0 = grdList.Rows[rowIndex].Cells[0];
                    int currentRowIndex = grdList.CurrentCell.RowIndex;
                    if (cell0.IsNotNullOrEmptyCell())
                    {
                        grdList.BindComboBoxCell(allProduct, grdList.Rows[currentRowIndex], currentRowIndex, false, 3, uoms, this, bu, 0);
                        //grdList.BindComboBoxCell(grdList.Rows[currentRowIndex], currentRowIndex, false, 5, causeList, this, bu, 0);
                    }
                }
            }

            validateNewRow = true;
            grdList.ValidateDuplicateSKU(productDT.Rows[0]["ProductID"].ToString(), 0, rowIndex, ref validateNewRow);

            grdList.BindDataGrid(allProduct, dataGridList, initDataGridList, productDT, 0, rowIndex, validateNewRow, this, uoms, bu, 0, false, 1);
        }

        private void InitHeader()
        {
            var employee = bu.GetEmployee(Helper.tbl_Users.EmpID);
            txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName, employee.LastName);

            dtpDocDate.SetDateTimePickerFormat();

            this.BindData("BranchWarehouse", searchFromBWHControls, "1900");

            var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
            ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnPrintCrys.Enabled = btnPrint.Enabled;

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            InitHeader();

            grdList.AutoGenerateColumns = false;
            validateNewRow = true;

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;

            grdList.Rows.Clear();
            AddNewRow(grdList, 0);
        }

        private string PreparePRMaster(bool editFlag = false)
        {
            bu.tbl_PRMaster = new tbl_PRMaster();

            var emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            //var supp = bu.GetSupplier(txtBranchCode.Text);
            var branch = bu.GetBranch();

            var pr = bu.tbl_PRMaster;
            bool checkEditMode = bu.CheckExistsPR(txdDocNo.Text);
            if (checkEditMode)
            {
                pr.DocNo = txdDocNo.Text;
            }
            else
            {
                pr.DocNo = bu.GenDocNo(docTypeCode);
            }

            pr.RequestBy = emp.EmpCode;
            pr.RevisionNo = 0;
            pr.DocTypeCode = docTypeCode;
            pr.DocStatus = ddlDocStatus.SelectedValue.ToString();
            pr.DocDate = dtpDocDate.Value.ToDateTimeFormat();
            pr.DocRef = txtRBDoc.Text;
            pr.FromBranchID = branch[0].BranchCode;
            pr.ToBranchID = branch[0].BranchCode;

            var allBwh = bu.GetAllBranchWarehouse();
            var whid = txtWHCode.Text;
            var _bwh = allBwh.FirstOrDefault(x => x.WHCode == whid);
            if (_bwh != null)
            {
                pr.FromWHID = _bwh.WHID;
                pr.ToWHID = _bwh.WHID;
            }

            pr.StatusInOut = "O";
            pr.Address = "";
            pr.ReceiveDate = null;
            pr.ReceiveBy = "0";
            pr.ShipDate = null;
            pr.ShipBy = "0";
            pr.ShipWHID = "0";
            pr.SalAreaID = "0";
            pr.EmpID = branch[0].BranchCode + "E000";
            pr.ContactName = "";
            pr.ContactTel = "";
            pr.Shipto = "";
            pr.Remark = txtRemark.Text;
            pr.Comment = "หมายเหตุ RB";
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

            return pr.DocNo;
        }

        private void PreparePRDetail(bool editFlag = false)
        {
            bu.tbl_PRDetails.Clear();

            var prDts = bu.tbl_PRDetails;
            DateTime crDate = DateTime.Now;

            for (int i = 0; i < grdList.RowCount; i++)
            {
                var prDt = new tbl_PRDetail();

                var cell0 = grdList.Rows[i].Cells[0];
                var cell2 = grdList.Rows[i].Cells[2];
                var cell3 = grdList.Rows[i].Cells[3];
                var cell4 = grdList.Rows[i].Cells[4];
                var cell5 = grdList.Rows[i].Cells[5];

                if (cell0.IsNotNullOrEmptyCell() && cell2.IsNotNullOrEmptyCell() && cell4.IsNotNullOrEmptyCell())
                {
                    if (Convert.ToDecimal(cell4.EditedFormattedValue) > 0)
                    {
                        prDt.DocNo = bu.tbl_PRMaster.DocNo;
                        prDt.ProductID = cell0.EditedFormattedValue.ToString();
                        prDt.Line = Convert.ToInt16(i);
                        prDt.ProductName = cell2.EditedFormattedValue.ToString();

                        //Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => uomList.Contains(x.ProductUomID));
                        //var allPrdUOM = bu.GetUOM();// tbl_ProductUomPre);
                        var prdUOM = allUOM.FirstOrDefault(x => x.ProductUomName == cell3.EditedFormattedValue.ToString());
                        if (prdUOM != null)
                        {
                            prDt.OrderUom = prdUOM.ProductUomID;
                        }

                        var cell4Value = Convert.ToDecimal(cell4.EditedFormattedValue);
                        prDt.OrderQty = 0;
                        prDt.SendQty = 0;
                        prDt.ReceivedQty = 0;
                        prDt.RejectedQty = cell4Value;
                        prDt.StockedQty = 0;
                        prDt.UnitCost = 0;
                        prDt.UnitPrice = 0;
                        prDt.VatType = 0;
                        prDt.LineTotal = 0;
                        prDt.LineRemark = cell5.EditedFormattedValue.ToString();
                        prDt.CrDate = crDate;
                        prDt.CrUser = Helper.tbl_Users.Username;
                        if (editFlag)
                        {
                            prDt.EdDate = crDate;
                            prDt.EdUser = Helper.tbl_Users.Username;
                        }
                        prDt.FlagDel = false;
                        prDt.FlagSend = false;

                        prDts.Add(prDt);
                    }
                }
            }
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

            foreach (var prDt in prDts)
            {
                var invMm = new tbl_InvMovement();

                invMm.ProductID = prDt.ProductID;
                invMm.ProductName = prDt.ProductName;
                invMm.RefDocNo = prDt.DocNo;
                invMm.TrnDate = crDate.ToDateTimeFormat();
                invMm.TrnType = "I";
                invMm.DocTypeCode = pr.DocTypeCode;

                decimal unitQty = 0;

                var prdUOMSets = bu.GetProductUOMSet(allUomSet, prDt.ProductID);
                if (prdUOMSets != null && prdUOMSets.Count > 0)
                {
                    //if (prDt.OrderUom != 2)
                    //    unitQty = (prDt.RejectedQty.Value * prdUOMSets[0].BaseQty);
                    var uom = allUomSet.FirstOrDefault(x => x.ProductID == prDt.ProductID && x.UomSetID == prDt.OrderUom);

                    if (uom != null)//if (prDt.OrderUom != 2)
                        unitQty = (prDt.RejectedQty.Value * uom.BaseQty);
                    else
                        unitQty = prDt.RejectedQty.Value;
                }
                else
                {
                    unitQty = prDt.RejectedQty.Value;
                }

                invMm.WHID = pr.FromWHID;
                invMm.FromWHID = pr.FromWHID;
                invMm.ToWHID = pr.ToWHID;
                invMm.TrnQtyIn = 0;
                invMm.TrnQtyOut = unitQty;
                invMm.TrnQty = -unitQty;

                invMm.CrDate = crDate;

                if (editFlag)
                {
                    invMm.EdDate = crDate;
                    invMm.TrnType = ddlDocStatus.SelectedValue.ToString() == "5" ? "X" : "I";
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

        private void PrepareInvWarehouse(bool editFlag = false)
        {
            bu.tbl_InvWarehouses.Clear();

            var invWhs = bu.tbl_InvWarehouses;
            //var poDts = bu.tbl_PODetails;
            //var po = bu.tbl_POMaster;

            DateTime crDate = DateTime.Now;

            //var allWHStock = bu.GetInvWarehouse(po.WHID); //edit by sailom 13/12/2021

            //edit by sailom .k 16/12/201----------------------------------------------------
            List<tbl_InvMovement> invWhItems = new List<tbl_InvMovement>();
            List<string> prdList = new List<string>();
            for (int i = 0; i < grdList.RowCount; i++)
            {
                var prdCodeCell = grdList.Rows[i].Cells[0];
                var qtyCell = grdList.Rows[i].Cells[4];
                if (prdCodeCell.IsNotNullOrEmptyCell() && qtyCell.IsNotNullOrEmptyCell()) //check over recieve
                {
                    var _productID = prdCodeCell.EditedFormattedValue.ToString();
                    prdList.Add(_productID);
                }
            }

            if (prdList.Count > 0)
                invWhItems = bu.GetTotalStockMovement(prdList, bu.tbl_Companies.First().WHOutTransit); //  edit by sailom 13/12/2021

            //edit by sailom .k 16/12/201----------------------------------------------------

            foreach (var item in invWhItems)
            {
                var invWh = new tbl_InvWarehouse();

                invWh.ProductID = item.ProductID;
                invWh.WHID = item.WHID;
                invWh.QtyOnHand = item.TrnQty;

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

        private void SetQtyOnHand(tbl_InvWarehouse invWh, decimal unitQty, string productID, string whID, bool editFlag)
        {
            var invWhItem = bu.GetInvWarehouse(productID, whID);
            if (editFlag)
            {
                if (invWhItem != null && invWhItem.Count > 0)
                {
                    if (ddlDocStatus.SelectedValue.ToString() == "5")
                        invWh.QtyOnHand = invWhItem[0].QtyOnHand + unitQty;
                }
            }
            else
            {
                if (invWhItem != null && invWhItem.Count > 0)
                {
                    invWh.QtyOnHand = invWhItem[0].QtyOnHand - unitQty;
                }
                else
                {
                    invWh.QtyOnHand = unitQty;
                }
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

                    bu = new RJ();

                    //validate edit PR and not change status by sailom/k 07/10/2021
                    var chkPR = bu.GetPRMaster(txdDocNo.Text);
                    if (chkPR != null && chkPR.DocStatus == "4")
                    {
                        if (ddlDocStatus.SelectedValue.ToString() == chkPR.DocStatus)
                        {
                            string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                            msg.ShowInfoMessage();

                            return;
                        }
                    }

                    docno = txdDocNo.Text;
                    editFlag = true;
                    bu.tbl_DocRunning = new List<tbl_DocRunning>();

                    bu.tbl_PRMaster = null;
                    bu.tbl_PRMaster = bu.GetPRMaster(docno);

                    //validate docno by sailom.k 27-05-2021
                    if (bu.tbl_PRMaster.DocNo.Length < 12)
                    {
                        this.ShowDocNoProcessErr();
                        return;
                    }

                    bu.tbl_PRMaster.DocStatus = ddlDocStatus.SelectedValue.ToString();

                    bu.tbl_InvMovements.Clear();
                    bu.tbl_InvMovements.AddRange(bu.GetInvMovement(docno));

                    if (ddlDocStatus.SelectedValue.ToString() == "5")
                        bu.tbl_InvMovements.ForEach(x => x.TrnType = "X");

                    bu.tbl_InvWarehouses.Clear();

                    //string _WHID = bu.tbl_PRMaster.FromWHID;
                    //var prDts = bu.GetPRDetails(docno);

                    //foreach (var prDt in prDts)
                    //{
                    //    SetWarehousesQTY(prDt, _WHID, editFlag, true);
                    //}

                    ret = bu.PerformUpdateData();

                    //edit by sailom .k 16/12/2021
                    bu.tbl_POMaster = new tbl_POMaster();
                    bu.tbl_PODetails.Clear();
                    bu.tbl_InvMovements.Clear();
                    bu.tbl_InvTransactions.Clear();
                    PrepareInvWarehouse();//edit by sailom .k 16/12/2021

                    ret = bu.PerformUpdateData(); //edit by sailom .k 16/12/2021
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

                    docno = PreparePRMaster(editFlag);

                    //validate docno by sailom.k 27-05-2021
                    if (bu.tbl_PRMaster.DocNo.Length < 12)
                    {
                        this.ShowDocNoProcessErr();
                        return;
                    }

                    PreparePRDetail(editFlag);
                    PrepareInvMovement(editFlag);
                    //PrepareInvWarehouse(editFlag);

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

                    ret = bu.PerformUpdateData();

                    //edit by sailom .k 16/12/2021
                    bu.tbl_POMaster = new tbl_POMaster();
                    bu.tbl_PODetails.Clear();
                    bu.tbl_InvMovements.Clear();
                    bu.tbl_InvTransactions.Clear();
                    PrepareInvWarehouse();//edit by sailom .k 16/12/2021

                    ret = bu.PerformUpdateData(); //edit by sailom .k 16/12/2021
                }

                if (ret == 1)
                {
                    this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, txdDocNo.Text);
                    btnPrintCrys.Enabled = btnPrint.Enabled;

                    txdDocNo.Text = docno;

                    grdList.CellContentClick -= grdList_CellContentClick;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    ddlDocStatus.SelectedValue.ToString().CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
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
                    //invWh.QtyOnHand = invWhItem[0].QtyOnHand - unitQty;
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

            var prdUOMSets = bu.GetProductUOMSet(allUomSet, prDt.ProductID); ;
            if (prdUOMSets != null && prdUOMSets.Count > 0)
            {
                //if (prDt.OrderUom != 2)
                //    unitQty = (prDt.RejectedQty.Value * prdUOMSets[0].BaseQty);
                var uom = allUomSet.FirstOrDefault(x => x.ProductID == prDt.ProductID && x.UomSetID == prDt.OrderUom);

                if (uom != null)//if (prDt.OrderUom != 2)
                    unitQty = (prDt.ReceivedQty.Value * uom.BaseQty);
                else
                    unitQty = prDt.RejectedQty.Value;
            }
            else
            {
                unitQty = prDt.RejectedQty.Value;
            }

            SetQtyOnHand(invWh, unitQty, prDt.ProductID, whid, editFlag, isFrom);

            bu.tbl_InvWarehouses.Add(invWh);
        }

        public void SetDefaultGridViewEvent(DataGridView grd)
        {
            grd.CellContentClick -= new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellContentClick);
            grd.CellEndEdit -= new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellEndEdit);
            grd.CellValidating -= new System.Windows.Forms.DataGridViewCellValidatingEventHandler(grdList_CellValidating);
            grd.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellValueChanged);
            grd.EditingControlShowing -= new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(grdList_EditingControlShowing);
            grd.RowPostPaint -= new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(grdList_RowPostPaint);
            grd.UserDeletingRow -= new System.Windows.Forms.DataGridViewRowCancelEventHandler(grdList_UserDeletingRow);
            grd.KeyDown -= new System.Windows.Forms.KeyEventHandler(grdList_KeyDown);
            grd.CellClick -= new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellClick);

            grd.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellContentClick);
            grd.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellEndEdit);
            grd.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(grdList_CellValidating);
            grd.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellValueChanged);
            grd.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(grdList_EditingControlShowing);
            grd.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(grdList_RowPostPaint);
            grd.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(grdList_UserDeletingRow);
            grd.KeyDown += new System.Windows.Forms.KeyEventHandler(grdList_KeyDown);
            grd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellClick);
        }

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            var cDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Ticks;
            var docDate = new DateTime(dtpDocDate.Value.Year, dtpDocDate.Value.Month, dtpDocDate.Value.Day).Ticks;

            if (Helper.tbl_Users.RoleID != 10 && dtpDocDate.Value != null && docDate < cDate)
            {
                string message = "ห้ามเลือกวันที่ย้อนหลัง !!!";
                message.ShowWarningMessage();
                ret = false;
            }

            if (ret)
            {
                if (Helper.tbl_Users.RoleID != 10 && !dtpDocDate.ValidateEndDay(bu))
                {
                    string message = "ระบบปิดวันไปแล้ว ไม่สามารถเลือกวันที่นี้ได้ !!!";
                    message.ShowWarningMessage();
                    ret = false;
                }
            }

            if (ret)
            {
                if (string.IsNullOrEmpty(txtRBDoc.Text))
                {
                    string message = "กรุณาอ้างอิงใบโอนสินค้าระหว่างคลัง (RB)!!!";
                    message.ShowWarningMessage();
                    txtRBDoc.ErrorTextBox();
                    txtRBDoc.Focus();
                    ret = false;
                }
            }

            if (ret)
            {
                if (string.IsNullOrEmpty(txtRemark.Text))
                {
                    string message = "กรุณาใส่หมายเหตุ!!!";
                    message.ShowWarningMessage();
                    txtRemark.ErrorTextBox();
                    txtRemark.Focus();
                    ret = false;
                }
            }

            if (ret) //validate header
            {
                errList.SetErrMessageList(txtWHCode, lblWHCode);

                if (errList.Count == 0)
                {
                    var bwh = bu.GetAllBranchWarehouse();
                    Func<tbl_BranchWarehouse, bool> fromBranchWarehousePre = (x => x.WHCode.ToLower() == txtWHCode.Text.ToLower());
                    var _mwh = bwh.Where(fromBranchWarehousePre).ToList();
                    if (_mwh == null || _mwh.Count == 0)
                    {
                        string t = lblWHCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtWHCode.ErrorTextBox();
                    }
                }

                if (errList.Count > 0)
                {
                    string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                    message.ShowWarningMessage();
                    ret = false;
                }
            }

            if (ret) //validate data grid
            {
                //var allProduct = bu.tbl_Product;
                //var whid = txtWHCode.Text;
                //var allBwh = bu.GetAllBranchWarehouse();
                //var bwh = allBwh.FirstOrDefault(x => x.WHCode == whid);
                //if (bwh != null)
                //{
                //    ret = grdList.ValiadteDataGridView(allProduct, 0, 3, 4, 0, bu, bwh.WHID, true, true);
                //}

                var allBwh = bu.GetAllBranchWarehouse();
                var bwh = allBwh.FirstOrDefault(x => x.WHCode == txtWHCode.Text);
                if (bwh != null)
                {
                    ret = grdList.ValiadteDataGridView(allProduct, 0, 3, 4, 0, bu, bwh.WHID, true, true);
                }
            }

            return ret;
        }

        #endregion

        #region event methods

        private void TxtFromWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("BranchWarehouse", searchFromBWHControls, txt.Text);
            }
        }

        private void NudCreditDay_ValueChanged(object sender, EventArgs e)
        {
            //nudCreditDay.CalcDueDate(dtpDocDate, dtpDueDate);
        }

        private void DtpDocDate_ValueChanged(object sender, EventArgs e)
        {
            //dtpDocDate.CalcDueDate(dtpDueDate, nudCreditDay);
        }

        private void TxdDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txdDocNo.Text))
                {
                    BindRJData(txdDocNo.Text);
                }
            }
        }

        private void TxtRBDoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtRBDoc.Text))
                {
                    BindRBData(txtRBDoc.Text);
                }
            }
        }

        private void frmRJ_Load(object sender, EventArgs e)
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
            btnPrintCrys.Enabled = btnPrint.Enabled;

            validateNewRow = true;

            if (!string.IsNullOrEmpty(txdDocNo.Text) && bu.CheckExistsPR(txdDocNo.Text))
            {
                grdList.CellContentClick -= grdList_CellContentClick;
            }
            else
            {
                grdList.CellContentClick -= grdList_CellContentClick;
                grdList.CellContentClick += grdList_CellContentClick;
            }

            dtpDocDate.Focus();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            btnCopy.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnPrint, txdDocNo.Text);
            btnPrintCrys.Enabled = btnPrint.Enabled;

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == "4"; };
            ddlDocStatus.SelectedValueDropdownList(condition);
            ddlDocStatus.Enabled = false;

            txdDocNo.Text = string.Empty;

            validateNewRow = true;

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;
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
            btnPrintCrys.Enabled = btnPrint.Enabled;

            validateNewRow = true;

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);


            btnCancel.EnableButton(btnSearchDoc);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;

            grdList.CellContentClick -= grdList_CellContentClick;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@DocNo", txdDocNo.Text);
        
            this.OpenReportingReportsPopup("ใบทำลายสินค้า", "Form_RJ.rdlc", "Form_RJ", _params); //Reporting service by sailom 30/11/2021
        }

        private void btnPrintCrys_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txdDocNo.Text))
            {
                FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocNo", txdDocNo.Text);
                this.OpenCrystalReportsPopup("ใบทำลายสินค้า", "Form_RJ.rpt", "Form_RJ", _params);
            }
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
            this.OpenDocPopup("ใบโอนสินค้าระหว่างสาขา", docTypeCode);
        }

        private void btnRB_Click(object sender, EventArgs e)
        {
            this.OpenDocPopup("ใบโอนสินค้าระหว่างสาขา", "RJRB");

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;
        }

        private void btnSearchFromWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchFromBWHControls, "เลือกคลังสินค้า", fbiPredicate);
        }

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grdList.SetCellContentClick(this, sender, e, "RJProduct", 4);
        }

        private void grdList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //grdList.ModifyComboBoxCell(e.RowIndex, bu, 3, 0, true);
                grdList.ModifyComboBoxCell(allProduct, e.RowIndex, bu, 3, 0);
            }
        }

        private void grdList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
                tb.PreviewKeyDown -= DataGridView_PreviewKeyDown;
                tb.PreviewKeyDown += DataGridView_PreviewKeyDown;

                tb.KeyPress -= DataGridView_KeyPress;
                tb.KeyPress += DataGridView_KeyPress;
            }
            else if (e.Control is DataGridViewComboBoxEditingControl)
            {
                e.CellStyle.BackColor = Color.White;

                DataGridViewComboBoxEditingControl tb = (DataGridViewComboBoxEditingControl)e.Control;
                tb.PreviewKeyDown -= DataGridView_PreviewKeyDown;
                tb.PreviewKeyDown += DataGridView_PreviewKeyDown;

                tb.KeyPress -= DataGridView_KeyPress;
                tb.KeyPress += DataGridView_KeyPress;

                ComboBox combo = e.Control as ComboBox;
                if (combo != null)
                {
                    combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                    combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                }
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            grdList.SetCellNumberOnly(sender, e, numberCell.ToList());
        }

        private void DataGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            DataGridView grd = null;
            if (sender is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl _grd = (DataGridViewTextBoxEditingControl)sender;

                grd = _grd.EditingControlDataGridView;
            }
            else if (sender is DataGridViewComboBoxEditingControl)
            {
                DataGridViewComboBoxEditingControl _grd = (DataGridViewComboBoxEditingControl)sender;

                grd = _grd.EditingControlDataGridView;
            }

            if (e.KeyCode == Keys.Enter)
            {
                int currentRowIndex = grd.CurrentCell.RowIndex;
                int curentColIndex = grd.CurrentCell.ColumnIndex;

                grd.ClearSelection();

                var cell0 = grd.Rows[currentRowIndex].Cells[0];
                var cell2 = grd.Rows[currentRowIndex].Cells[2];

                if (curentColIndex == 0)
                {
                    bool isNewRow = true;
                    if (cell0.IsNotNullOrEmptyCell())
                    {
                        validateNewRow = true;

                        var checkDup = grd.ValidateDuplicateSKU(cell0.EditedFormattedValue.ToString(), 0, currentRowIndex, ref validateNewRow);
                        if (!checkDup)
                        {
                            GridViewHelper.ShowDupSKUMessage();
                            cell0.Value = "";
                            validateNewRow = false;
                            grd.Rows.RemoveAt(currentRowIndex);
                            isNewRow = false;
                        }

                        if (isNewRow)
                            grd.BindDataGridViewRow(dataGridList, initDataGridList, dt, 0, cell0.EditedFormattedValue.ToString(), currentRowIndex, 0, ref validateNewRow, "RJProduct", false);
                    }

                    if (isNewRow)
                        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 4];
                }
                else if (curentColIndex == 4)
                {
                    if (cell2.IsNotNullOrEmptyCell())
                    {
                        if ((grd.RowCount - 1) == currentRowIndex)
                        {
                            validateNewRow = true;

                            var checkDup = grd.ValidateDuplicateSKU(cell0.EditedFormattedValue.ToString(), 0, currentRowIndex, ref validateNewRow);
                            if (!checkDup)
                            {
                                validateNewRow = false;
                            }

                            grdList.AddNewRow(allProduct, initDataGridList, 0, "", currentRowIndex + 1, validateNewRow, uoms, causeList, bu, this, 0);

                            if (validateNewRow)
                            {
                                if (grd.RowCount > currentRowIndex + 1)
                                    grd.CurrentCell = grd.Rows[currentRowIndex + 1].Cells[0];
                            }
                        }
                        else
                        {
                            if (grd.RowCount > currentRowIndex + 1)
                                grd.CurrentCell = grd.Rows[currentRowIndex + 1].Cells[0];
                        }
                    }
                }
            }
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void grdList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grd = (DataGridView)sender;
            if (grd != null && grd.CurrentRow != null)
            {
                int currentRowIndex = grd.CurrentCell.RowIndex;
                int curentColIndex = grd.CurrentCell.ColumnIndex;

                var cell0 = grd.Rows[currentRowIndex].Cells[0];

                if (cell0.IsNotNullOrEmptyCell())
                {
                    if (e.ColumnIndex == 0)
                    {
                        grdList.BindComboBoxCell(allProduct, grd.Rows[currentRowIndex], currentRowIndex, false, 3, uoms, this, bu, 0, true);
                        //grdList.BindComboBoxCell(grd.Rows[currentRowIndex], currentRowIndex, false, 5, causeList, this, bu, 0);
                    }
                }
            }
        }

        private void grdList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            grdList.SetUserDeletingRow(sender, e);
        }

        private void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            //grdList.SetDeleteKeyDown(sender, e);
            if (e.KeyCode == Keys.Delete)
            {
                DataGridView _grd = (DataGridView)sender;
                if (_grd != null && _grd.CurrentRow != null)
                {
                    int rowIndex = _grd.CurrentRow.Index;
                    if (_grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "White" || _grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "0")
                    {
                        _grd.Rows.RemoveAt(rowIndex);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void grdList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            grdList.SetCellClick(sender, e, cellEdit, 0);
        }


        #endregion

        private void frmRJ_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }


    }
}
