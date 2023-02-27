﻿using AllCashUFormsApp.Model;
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
    public partial class frmRB : Form
    {
        MenuBU menuBU = new MenuBU();
        RB bu = new RB();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();

        List<tbl_ProductUom> uoms = new List<tbl_ProductUom>();
        bool validateNewRow = true;
        public string docTypeCode = "";
        int runDigit = 0;
        List<Control> searchBranchControls = new List<Control>();
        List<Control> searchFromBWHControls = new List<Control>();
        List<Control> searchToBWHControls = new List<Control>();
        List<Control> searchEmpControls = new List<Control>();
        List<string> readOnlyControls = new List<string>();
        int[] cellEdit = new int[] { 0, 3, 5 };
        int[] numberCell = new int[] { 5 };
        Dictionary<int, string> dataGridList = new Dictionary<int, string>();
        Dictionary<int, string> initDataGridList = new Dictionary<int, string>();
        //List<int> uomList = new List<int>();
        Func<tbl_BranchWarehouse, bool> fbiPredicate = null;
        List<tbl_Product> allProduct = new List<tbl_Product>();
        List<tbl_ProductUomSet> allUomSet = new List<tbl_ProductUomSet>();
        List<tbl_ProductUom> allUOM = new List<tbl_ProductUom>();
        //List<tbl_ProductPriceGroup> allProductPrice = new List<tbl_ProductPriceGroup>();
        List<tbl_ProductGroup> allProdGroup = new List<tbl_ProductGroup>();
        List<tbl_ProductSubGroup> allProdSubGroup = new List<tbl_ProductSubGroup>();

        List<tbl_PRDetail> allPRDetails = new List<tbl_PRDetail>();
        List<tbl_ProductUom> prodUOMs = new List<tbl_ProductUom>();
        bool isAdd = false;

        public frmRB()
        {
            InitializeComponent();

            searchBranchControls = new List<Control>() { txtBranchCode, txtBranchName };
            searchFromBWHControls = new List<Control> { txtFromWHCode, txtFromWHName, txtSaleEmpID };
            searchToBWHControls = new List<Control> { txtToWHCode, txtToWHName };
            searchEmpControls = new List<Control> { txtEmpCode, txtEmpName };
            readOnlyControls = new string[] { txtBranchName.Name, txtFromWHName.Name, txtToWHName.Name, txtEmpName.Name, dtpDocDate.Name }.ToList();

            dataGridList = new Dictionary<int, string>() { { 0, "ProductID" }, { 2, "ProductName" }, { 3, "UomSetID" }, { 4, "BaseQtyDT" } };
            initDataGridList = new Dictionary<int, string>() { { 0, "" }, { 2, "" }, { 3, "combobox" }, { 4, "" }, { 5, "0" } };

            txdDocNo.KeyDown += TxdDocNo_KeyDown;
            dtpDocDate.ValueChanged += DtpDocDate_ValueChanged;

            txtBranchCode.KeyDown += TxtFromBranchID_KeyDown;
            txtFromWHCode.KeyDown += TxtFromWHCode_KeyDown;
            txtToWHCode.KeyDown += TxtToWHCode_KeyDown;
            txtEmpCode.KeyDown += TxtEmpCode_KeyDown;
        }

        #region private methods

        private void InitPage()
        {
            var documentType = bu.tbl_DocumentType.FirstOrDefault(x => x.DocTypeCode.Trim() == "RB");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                runDigit = documentType.DocFormat.Length - 2;
                FormHeader.Text = documentType.DocHeader;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");

                this.ClearControl(docTypeCode, runDigit);

                txtComment.Text = documentType.DocRemark;
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

            //data gridview
            grdList.SetDataGridViewStyle();
            SetDefaultGridViewEvent(grdList);

            allProduct = bu.tbl_Product;
            allUomSet = bu.tbl_ProductUomSet;

            //allProductPrice = bu.tbl_ProductPriceGroup;
            allProdGroup = bu.tbl_ProductGroup;
            allProdSubGroup = bu.tbl_ProductSubGroup;

            var bw = bu.GetAllBranchWarehouse();
            if (bw.Count > 0)
                pnlPODocNo.Visible = bw.Any(x => x.WHType == 2);
        }

        private void AddNewRow(DataGridView grd, int rowIndex)
        {
            if (!validateNewRow)
            {
                return;
            }

            grd.Rows.Add(1);
            //InitRowData("", rowIndex);
            grd.InitRowData(this, initDataGridList, 0, "", rowIndex, allProduct, uoms, bu, 0);
        }

        //public void BindRBWithPo(string poDocNo)
        //{
        //    string rbDocNo = "";
        //    var refRB = bu.GetRefRB(poDocNo);
        //    if (refRB != null && refRB.Rows.Count > 0)
        //    {
        //        var _docNo = refRB.Rows[0]["DocNo"].ToString();
        //        if (!string.IsNullOrEmpty(_docNo))
        //        {
        //            rbDocNo = _docNo;
        //        }
        //    }

        //    bu.GetDocData(rbDocNo, docTypeCode);

        //    var pr = bu.tbl_PRMaster;
        //    var prDts = bu.tbl_PRDetails;

        //    if (string.IsNullOrEmpty(pr.DocNo))
        //    {
        //        string msg = "ไม่พบข้อมูลเลขที่เอกสาร!!!";
        //        msg.ShowWarningMessage();

        //        btnCancel.PerformClick();

        //        return;
        //    }

        //    if (pr != null)
        //    {
        //        BindPRMaster(pr);
        //    }
        //    if (prDts != null && prDts.Count > 0)
        //    {
        //        BindPRDetail(prDts);
        //    }

        //    this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

        //    btnSearchDoc.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
        //    btnPrintCrys.Enabled = btnPrint.Enabled;

        //    txdDocNo.DisableTextBox(false);
        //    txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");

        //    grdList.CellContentClick -= grdList_CellContentClick;

        //    //CheckCancelDoc(po.DocStatus);
        //    bool checkEditMode = bu.CheckExistsPR(rbDocNo);
        //    pr.DocStatus.CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
        //}

        public void BindRBFromPOData(string poDocNo, string _docTypeCode, bool isOtherForm = true)
        {
            if (isOtherForm)
                btnAdd.PerformClick();

            bu.GetDocData(poDocNo, _docTypeCode);

            var po = bu.tbl_POMaster;
            var poDts = bu.tbl_PODetails;

            if (string.IsNullOrEmpty(po.DocNo))
            {
                string msg = "ไม่พบข้อมูลเลขที่เอกสาร!!!";
                msg.ShowWarningMessage();

                btnCancel.PerformClick();

                return;
            }

            if (po != null)
            {
                BindPOMaster(po);
            }
            if (poDts != null && poDts.Count > 0)
            {
                BindPODetail(poDts);
            }

            pnlPODocNo.Visible = false;
            bool checkPreOrder = false;

            if (string.IsNullOrEmpty(txtFromWHCode.Text))
            {
                var bw = bu.GetAllBranchWarehouse();
                if (bw.Count > 0)
                    checkPreOrder = bw.Any(x => x.WHType == 2);
            }
            else
            {
                var bw = bu.GetBranchWarehouse(txtFromWHCode.Text);
                if (bw != null)
                    checkPreOrder = bw.WHType == 2;
            }

            if (checkPreOrder)
            {
                pnlPODocNo.Visible = true;
                foreach (Control item in pnlPODocNo.Controls)
                {
                    item.Enabled = true;
                }
            }
            else
            {
                pnlPODocNo.Visible = false;
            }

            //CheckCancelDoc(po.DocStatus);
            //bool checkEditMode = bu.CheckExistsPR(rbDocNo);
            //pr.DocStatus.CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
        }

        private void BindPOMaster(tbl_POMaster pr)
        {
            txtPODoc.Text = pr.DocNo;
            dtpDocDate.Value = pr.DocDate.ToDateTimeFormat();

            var branch = bu.tbl_Branchs;
            if (branch != null && branch.Count > 0)
            {
                txtBranchCode.Text = branch[0].BranchID;
                txtBranchName.Text = branch[0].BranchName;
            }

            var bwh = bu.GetAllBranchWarehouse();
            Func<tbl_BranchWarehouse, bool> bwhPredicate_From = (x => x.WHID == pr.WHID);
            var frombwh = bwh.Where(bwhPredicate_From).ToList();
            if (frombwh != null && frombwh.Count > 0)
            {
                txtFromWHCode.Text = frombwh[0].WHCode;
                txtFromWHName.Text = frombwh[0].WHName;
            }

            Func<tbl_BranchWarehouse, bool> bwhPredicate_To = (x => x.WHCode == "1000");
            var tobwh = bwh.Where(bwhPredicate_To).ToList();
            if (tobwh != null && tobwh.Count > 0)
            {
                txtToWHCode.Text = tobwh[0].WHCode;
                txtToWHName.Text = tobwh[0].WHName;
            }

            var emp = bu.GetEmployee(pr.SaleEmpID);
            if (emp != null)
            {
                txtEmpCode.Text = pr.SaleEmpID;
                txtEmpName.Text = string.Join(" ", emp.TitleName, emp.FirstName, emp.LastName);
            }

            if (ddlDocStatus.DataSource == null)
            {
                var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
                ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
            }

            var user = bu.GetEmployeeByUserName(pr.CrUser); //edit by sailom 22/03/2022 //bu.GetEmployeeByUserName(pr.EmpID);
            if (user != null)
                txtCrUser.Text = string.Join(" ", user.TitleName, user.FirstName, user.LastName);
            else
                txtCrUser.Text = pr.CrUser;

            txtComment.Text = pr.Comment  + (!string.IsNullOrEmpty(pr.DocNo) ? (" บิลขายเลขที่ : " + pr.DocNo) : "");
        }

        private void BindPODetail(List<tbl_PODetail> prDts)
        {
            grdList.Rows.Clear();

            //Last edit by sailom.k 14/09/2021 tunning performance-------------------------
            isAdd = false;
            allPRDetails = new List<tbl_PRDetail>();
            foreach (var item in prDts)
            {
                var prDt = new tbl_PRDetail();
                prDt.DocNo = "";
                prDt.ProductID = item.ProductID;
                prDt.Line = item.Line;
                prDt.ProductName = item.ProductName;

                var prdUOM = allUOM.FirstOrDefault(x => x.ProductUomName == item.OrderUom.ToString());
                if (prdUOM != null)
                    prDt.OrderUom = prdUOM.ProductUomID;

                var cell5Value = Convert.ToDecimal(item.OrderQty);
                prDt.OrderQty = cell5Value;
                prDt.SendQty = cell5Value;
                prDt.ReceivedQty = cell5Value;
                prDt.RejectedQty = 0;
                prDt.StockedQty = 0;
                prDt.UnitCost = 0;
                prDt.UnitPrice = 0;
                prDt.VatType = 0;
                prDt.LineTotal = 0;
                prDt.LineRemark = "";
                allPRDetails.Add(prDt);
            }

            var listPrdID = prDts.Select(x => x.ProductID).ToList();
            prodUOMs.AddRange(bu.GetProductUOM(listPrdID));

            for (int i = 0; i < prDts.Count; i++)
            {
                grdList.Rows.Add(1);

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
                grdList.Rows[i].Cells[3].Value = prDts[i].OrderUom; //edit by sailom 25-05-2021 about order uom 

                Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => x.ProductID == prDts[i].ProductID && x.UomSetID == prDts[i].OrderUom);
                var productUOMSet = allUomSet.Where(tbl_ProductUomSetPre).ToList(); // bu.GetUOMSet(tbl_ProductUomSetPre);
                if (productUOMSet != null && productUOMSet.Count > 0)
                {
                    grdList.Rows[i].Cells[4].Value = "1*" + productUOMSet[0].BaseQty;
                }
                grdList.Rows[i].Cells[5].Value = prDts[i].OrderQty;
            }
        }

        public void BindRBData(string rbDocNo)
        {
            bu.GetDocData(rbDocNo, docTypeCode);

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
            else
            {
                grdList.Rows.Clear(); //last edit by sailom .k 28/09/2022
            }

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            btnSearchDoc.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
            btnPrintCrys.Enabled = btnPrint.Enabled;

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");

            grdList.CellContentClick -= grdList_CellContentClick;

            //CheckCancelDoc(po.DocStatus);
            bool checkEditMode = bu.CheckExistsPR(rbDocNo);
            pr.DocStatus.CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);

            CreateGridBtnList();
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

            Func<tbl_Employee, bool> empPredicate = (x => x.EmpCode == pr.ToWHID);
            var emp = bu.GetEmployee(pr.RequestBy);
            if (emp != null)
            {
                txtEmpCode.Text = pr.RequestBy;
                txtEmpName.Text = string.Join(" ", emp.TitleName, emp.FirstName, emp.LastName);
            }

            if (ddlDocStatus.DataSource == null)
            {
                var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
                ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
            }
            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == pr.DocStatus; };
            ddlDocStatus.SelectedValueDropdownList(condition);

            var user = bu.GetEmployeeByUserName(pr.CrUser); //edit by sailom 22/03/2022 //bu.GetEmployeeByUserName(pr.EmpID);
            if (user != null)
                txtCrUser.Text = string.Join(" ", user.TitleName, user.FirstName, user.LastName);
            else
                txtCrUser.Text = pr.CrUser;

            txtComment.Text = pr.Comment;

            //var comment = "";
            //comment = string.Join(", ", (!string.IsNullOrEmpty(pr.ApprovedBy) ? "RB = " + pr.ApprovedBy : ""), pr.Comment);
            //txtComment.Text = comment;
        }

        private void BindPRDetail(List<tbl_PRDetail> prDts)
        {
            grdList.Rows.Clear();

            //Last edit by sailom.k 14/09/2021 tunning performance-------------------------
            allPRDetails = prDts;
            isAdd = false;

            var listPrdID = allPRDetails.Select(x => x.ProductID).ToList();
            prodUOMs.AddRange(bu.GetProductUOM(listPrdID));
            //Last edit by sailom.k 14/09/2021 tunning performance--------------------

            //var allUOM = bu.tbl_ProductUom;
            //var allProduct = bu.tbl_Product;

            for (int i = 0; i < prDts.Count; i++)
            {
                grdList.Rows.Add(1);

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
                grdList.Rows[i].Cells[3].Value = prDts[i].OrderUom; //edit by sailom 25-05-2021 about order uom 

                Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => x.ProductID == prDts[i].ProductID && x.UomSetID == prDts[i].OrderUom);
                var productUOMSet = allUomSet.Where(tbl_ProductUomSetPre).ToList(); // bu.GetUOMSet(tbl_ProductUomSetPre);
                if (productUOMSet != null && productUOMSet.Count > 0)
                {
                    grdList.Rows[i].Cells[4].Value = "1*" + productUOMSet[0].BaseQty;
                }
                grdList.Rows[i].Cells[5].Value = prDts[i].OrderQty; 
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
                    }
                }
            }

            validateNewRow = true;
            grdList.ValidateDuplicateSKU(productDT.Rows[0]["ProductID"].ToString(), 0, rowIndex, ref validateNewRow);


            grdList.BindDataGrid(allProduct, dataGridList, initDataGridList, productDT, 0, rowIndex, validateNewRow, this, uoms, bu, 0);
        }

        private void CalculateRow(DataGridView grd, int rowIndex)
        {
            var cell0 = grd.Rows[rowIndex].Cells[0];
            var cell3 = grd.Rows[rowIndex].Cells[3];
            var cell4 = grd.Rows[rowIndex].Cells[4];

            if (cell3.IsNotNullOrEmptyCell())
            {
                Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => x.ProductID == cell0.EditedFormattedValue.ToString() && x.UomSetName == cell3.EditedFormattedValue.ToString());
                var productUOMSet = allUomSet.Where(tbl_ProductUomSetPre).ToList(); // bu.GetUOMSet(tbl_ProductUomSetPre);
                if (productUOMSet != null && productUOMSet.Count > 0)
                {
                    cell4.Value = "1*" + productUOMSet[0].BaseQty;
                }
                else
                {
                    cell4.Value = "";
                }
            }
        }

        private void InitHeader()
        {
            //var company = bu.GetCompany();
            //txtAddress.Text = company.Address;
            var b = bu.GetBranch();
            if (b != null)
            {
                this.BindData("FromBranchID", searchBranchControls, b[0].BranchID);
            }

            var employee = bu.GetEmployee(Helper.tbl_Users.EmpID);
            txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName, employee.LastName);

            dtpDocDate.SetDateTimePickerFormat();
            //dtpDueDate.SetDateTimePickerFormat();

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

            isAdd = true;
            AddNewRow(grdList, 0);

            //txtComment.Text = "หมายเหตุ RB";
        }

        private string PreparePRMaster(bool editFlag = false)
        {
            bu.tbl_PRMaster = new tbl_PRMaster();

            //var emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            //var supp = bu.GetSupplier(txtBranchCode.Text);
            //var branch = bu.GetBranch();
            var branch = bu.tbl_Branchs;

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

            pr.RevisionNo = 0;
            pr.DocTypeCode = docTypeCode;
            pr.DocStatus = ddlDocStatus.SelectedValue.ToString();
            pr.DocDate = dtpDocDate.Value.ToDateTimeFormat();
            pr.DocRef = "";
            pr.FromBranchID = branch[0].BranchCode;

            pr.RequestBy = txtEmpCode.Text;
            pr.ToBranchID = branch[0].BranchCode;

            var allBwh = bu.GetAllBranchWarehouse();
            var fromWhid = txtFromWHCode.Text;
            var frombwh = allBwh.FirstOrDefault(x => x.WHCode == fromWhid);
            if (frombwh != null)
            {
                pr.FromWHID = frombwh.WHID;
            }

            var toWhid = txtToWHCode.Text;
            var tobwh = allBwh.FirstOrDefault(x => x.WHCode == toWhid);
            if (tobwh != null)
            {
                pr.ToWHID = tobwh.WHID;
            }

            pr.StatusInOut = "O";
            pr.Address = null;
            pr.ReceiveDate = null;
            pr.ReceiveBy = "0";
            pr.ShipDate = null;
            pr.ShipBy = "0";
            pr.ShipWHID = "0";
            pr.SalAreaID = "0";
            pr.EmpID = branch[0].BranchCode + "E000";

            //case new document and no have empid Last edit by sailom .k 01/07/2022---------------------------------------
            var _user = bu.GetUser().FirstOrDefault(x => x.Username == Helper.tbl_Users.Username);
            if (_user != null)
            {
                pr.EmpID = _user.EmpID;
            }
            //case new document and no have empid Last edit by sailom .k 01/07/2022---------------------------------------

            pr.ContactName = null;
            pr.ContactTel = null;
            pr.Shipto = null;
            pr.Remark = null;
            pr.Comment = txtComment.Text;
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
                var cell5 = grdList.Rows[i].Cells[5];

                if (cell0.IsNotNullOrEmptyCell() && cell2.IsNotNullOrEmptyCell() && cell5.IsNotNullOrEmptyCell())
                {
                    if (Convert.ToDecimal(cell5.EditedFormattedValue) > 0)
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

                        var cell5Value = Convert.ToDecimal(cell5.EditedFormattedValue);
                        prDt.OrderQty = cell5Value;
                        prDt.SendQty = cell5Value;
                        prDt.ReceivedQty = cell5Value;
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
                for (int i = 0; i < 2; i++)
                {
                    var invMm = new tbl_InvMovement();

                    invMm.ProductID = prDt.ProductID;
                    invMm.ProductName = prDt.ProductName;
                    invMm.RefDocNo = prDt.DocNo;
                    //invMm.TrnDate = crDate.ToDateTimeFormat();
                    invMm.TrnDate = dtpDocDate.Value.ToDateTimeFormat();//last edit by sailom .k 10/08/2022 
                    invMm.TrnType = "T";
                    invMm.DocTypeCode = pr.DocTypeCode;

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

                    if (i == 0)
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
                        invMm.TrnQtyOut = unitQty;
                        invMm.TrnQty = -(unitQty);
                    }

                    invMm.CrDate = crDate;

                    if (editFlag)
                    {
                        invMm.EdDate = crDate;
                        invMm.TrnType = ddlDocStatus.SelectedValue.ToString() == "5" ? "X" : "T";
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
            string whid = txtFromWHCode.Text.Contains("V") ? txtFromWHCode.Text : bu.tbl_Companies[0].CompanyCode + txtFromWHCode.Text;
            SubPrepareInvWarehouse(whid, editFlag);
        }

        private void PrepareInvWarehouseTo(bool editFlag = false)
        {
            var pr = bu.tbl_PRMaster;
            string whid = txtToWHCode.Text.Contains("V") ? txtToWHCode.Text : bu.tbl_Companies[0].CompanyCode + txtToWHCode.Text;
            SubPrepareInvWarehouse(whid, editFlag);
        }

        private void SubPrepareInvWarehouse(string whid, bool editFlag = false)
        {
            //edti by sailom .k 14/12/2021----------------------------
            var invWhs = bu.tbl_InvWarehouses;
            var prDts = bu.tbl_PRDetails;

            DateTime crDate = DateTime.Now;

            //var allWHStock = bu.GetInvWarehouse(whid); //edit by sailom 13/12/2021

            //edit by sailom .k 16/12/201----------------------------------------------------
            List<tbl_InvMovement> invWhItems = new List<tbl_InvMovement>();
            List<string> prdList = new List<string>();
            for (int i = 0; i < grdList.RowCount; i++)
            {
                var prdCodeCell = grdList.Rows[i].Cells[0];
                var qtyCell = grdList.Rows[i].Cells[5];
                if (prdCodeCell.IsNotNullOrEmptyCell() && qtyCell.IsNotNullOrEmptyCell()) //check over recieve
                {
                    var _productID = prdCodeCell.EditedFormattedValue.ToString();
                    prdList.Add(_productID);
                }
            }
            //edit by sailom .k 16/12/201----------------------------------------------------

            if (prdList.Count > 0)
                invWhItems = bu.GetTotalStockMovement(prdList, whid); //  edit by sailom 13/12/2021

            foreach (var item in invWhItems)
            {
                var invWh = new tbl_InvWarehouse();

                invWh.ProductID = item.ProductID;
                invWh.WHID = item.WHID;
                invWh.QtyOnHand = item.TrnQty;

                //if (item.WHID.Contains("1000"))
                //    invWh.QtyOnHand = -item.TrnQty;
                //else
                //    invWh.QtyOnHand = item.TrnQty;

                //decimal unitQty = 0;
                //var prdUOMSets = bu.GetProductUOMSet(allUomSet, prDt.ProductID);
                //if (prdUOMSets != null && prdUOMSets.Count > 0)
                //{
                //    var uom = allUomSet.FirstOrDefault(x => x.ProductID == prDt.ProductID && x.UomSetID == prDt.OrderUom);

                //    if (uom != null)//if (prDt.OrderUom != 2)
                //        unitQty = (prDt.ReceivedQty.Value * uom.BaseQty);
                //    else
                //        unitQty = prDt.ReceivedQty.Value;

                //    PrepareQtyOnHand(invWh, allWHStock, prDt.ProductID, whid, unitQty);
                //}
                //else
                //{
                //    unitQty = prDt.ReceivedQty.Value;

                //    PrepareQtyOnHand(invWh, allWHStock, prDt.ProductID, whid, unitQty);
                //}

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

                    bu = new RB();

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
                    bu.tbl_PRMaster.EdDate = DateTime.Now;
                    bu.tbl_PRMaster.EdUser = Helper.tbl_Users.Username;

                    bu.tbl_InvMovements.Clear();
                    bu.tbl_InvMovements.AddRange(bu.GetInvMovement(docno));

                    if (ddlDocStatus.SelectedValue.ToString() == "5")
                        bu.tbl_InvMovements.ForEach(x => x.TrnType = "X");

                    //bu.tbl_InvWarehouses.Clear();

                    //string fromWHID = bu.tbl_PRMaster.FromWHID;
                    //string toWHID = bu.tbl_PRMaster.ToWHID;
                    //var prDts = bu.GetPRDetails(docno);

                    //foreach (var prDt in prDts)
                    //{
                    //    SetWarehousesQTY(prDt, fromWHID, editFlag, true);
                    //    SetWarehousesQTY(prDt, toWHID, editFlag, false);
                    //}

                    //ret = bu.UpdateData();
                    ret = bu.PerformUpdateData(); //edit by sailom .k 14/12/2021

                    //edit by sailom .k 14/12/2021
                    bu.tbl_PRMaster = new tbl_PRMaster();
                    bu.tbl_PRDetails.Clear();
                    bu.tbl_InvMovements.Clear();
                    bu.tbl_DocRunning.Clear();
                    PrepareInvWarehouseFrom(editFlag);
                    PrepareInvWarehouseTo(editFlag); //edit by sailom .k 16/12/2021

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
                    //PrepareInvWarehouseFrom(editFlag);
                    //PrepareInvWarehouseTo(editFlag);

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

                    //ret = bu.UpdateData();
                    ret = bu.PerformUpdateData(); //edit by sailom .k 14/12/2021

                    //edit by sailom .k 14/12/2021
                    bu.tbl_PRMaster = new tbl_PRMaster();
                    bu.tbl_PRDetails.Clear();
                    bu.tbl_InvMovements.Clear();
                    bu.tbl_DocRunning.Clear();
                    PrepareInvWarehouseFrom(editFlag);
                    PrepareInvWarehouseTo(editFlag); //edit by sailom .k 16/12/2021

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

            if (!(new List<int> { 5, 10 }).Contains(Helper.tbl_Users.RoleID.Value) && dtpDocDate.Value != null && docDate < cDate)
            {
                string message = "ห้ามเลือกวันที่ย้อนหลัง !!!";
                message.ShowWarningMessage();
                ret = false;
            }

            if (ret)
            {
                if (!(new List<int> { 5, 10 }).Contains(Helper.tbl_Users.RoleID.Value) && !dtpDocDate.ValidateEndDay(bu))
                {
                    string message = "ระบบปิดวันไปแล้ว ไม่สามารถเลือกวันที่นี้ได้ !!! \n ***หากต้องการทำรายการนี้ต้องแจ้งทาง IT เท่านั้น***";
                    message.ShowWarningMessage();
                    ret = false;
                }
            }

            if (ret) //validate header
            {
                errList.SetErrMessageList(txtBranchCode, lblBranchCode);
                errList.SetErrMessageList(txtFromWHCode, lblFromWHCode);
                errList.SetErrMessageList(txtToWHCode, lblToWHCode);
                //errList.SetErrMessageList(txtEmpCode, lblEmpCode);
                errList.SetErrMessageList(txtCrUser, lblCrUser);

                if (errList.Count == 0)
                {
                    var branch = bu.tbl_Branchs; //bu.GetBranch(); //Last edit by sailom .k 07/02/2022
                    if (branch == null)
                    {
                        string t = lblBranchCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtBranchCode.ErrorTextBox();
                    }

                    //var bwh = bu.GetAllBranchWarehouse();
                    //Func<tbl_BranchWarehouse, bool> fromBranchWarehousePre = (x => x.WHCode.ToLower() == txtFromWHCode.Text.ToLower());
                    //var fromwh = bwh.Where(fromBranchWarehousePre).ToList();
                    var fromwh = bu.GetBranchWarehouse(txtFromWHCode.Text); //Last edit by sailom .k 07/02/2022
                    if (fromwh == null)
                    {
                        string t = lblFromWHCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtFromWHCode.ErrorTextBox();
                    }

                    //Func<tbl_BranchWarehouse, bool> toBranchWarehousePre = (x => x.WHCode.ToLower() == txtToWHCode.Text.ToLower());
                    //var towh = bwh.Where(toBranchWarehousePre).ToList();
                    var towh = bu.GetBranchWarehouse(txtToWHCode.Text); //Last edit by sailom .k 07/02/2022
                    if (towh == null)
                    {
                        string t = lblFromWHCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtToWHCode.ErrorTextBox();
                    }

                    //var emp = bu.GetEmployee(txtEmpCode.Text);
                    //if (emp == null)
                    //{
                    //    string t = lblToWHCode.Text;
                    //    errList.Add(string.Format("--> {0}", t));
                    //    txtEmpCode.ErrorTextBox();
                    //}
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
                var allProduct = bu.tbl_Product;
                var whid = txtFromWHCode.Text;
                //var allBwh = bu.GetAllBranchWarehouse();
                //var bwh = allBwh.FirstOrDefault(x => x.WHCode == whid);
                //if (bwh != null)
                var bwh = bu.GetBranchWarehouse(whid); //Last edit by sailom .k 07/02/2022
                if (bwh != null)
                {
                    ret = grdList.ValiadteDataGridView(allProduct, 0, 3, 5, 0, bu, bwh.WHID, (!(new List<int> { 5, 10 }).Contains(Helper.tbl_Users.RoleID.Value) ? true : false));
                    //ret = grdList.ValiadteDataGridView(allProduct, 0, 3, 5, 0, bu, bwh.WHID, true);
                }                                                                                                                                     

            }

            if (ret)
            {
                if (txtFromWHName.Text.ToLower() == txtToWHName.Text.ToLower())
                {
                    string message = "กรุณาเลือกคลังต้นทาง และคลังปลายทาง ให้ถูกต้อง !!!";
                    message.ShowWarningMessage();
                    ret = false;
                }
            }

            return ret;
        }

        #endregion

        #region event methods

        private void TxtEmpCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("Employee", searchEmpControls, txt.Text);
            }
        }

        private void TxtFromBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("FromBranchID", searchBranchControls, txt.Text);
            }
        }

        private void TxtFromWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("BranchWarehouse", searchFromBWHControls, txt.Text);
                this.BindData("Employee", searchEmpControls, txtSaleEmpID.Text);
            }
        }

        private void TxtToWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("BranchWarehouse", searchToBWHControls, txt.Text);
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
                    BindRBData(txdDocNo.Text);
                }
            }
        }

        private void frmRB_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

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
            isAdd = true; //Last edit by sailom.k 14/09/2021 tunning performance
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

            string cfMsg = "ต้องการพิมพ์โดยที่ไม่ดูรายงานใช่หรือไม่?";
            string title = "ยืนยันการพิมพ์!!";
            var confirmResult = FlexibleMessageBox.Show(cfMsg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (confirmResult == DialogResult.Yes)
            {
                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocNo", txdDocNo.Text);
                //this.OpenCrystalReportsPopup("ใบโอนย้ายสินค้า", "Form_RB.rpt", "Form_RB", _params);

                this.OpenReportingReportsNonPreViewPopup("ใบโอนย้ายสินค้า", "Form_RB.rdlc", "Form_RB", _params); //Reporting service by sailom 30/11/2021
            }
            else
            {
                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocNo", txdDocNo.Text);
                //this.OpenCrystalReportsPopup("ใบโอนย้ายสินค้า", "Form_RB.rpt", "Form_RB", _params);

                this.OpenReportingReportsPopup("ใบโอนย้ายสินค้า", "Form_RB.rdlc", "Form_RB", _params); //Reporting service by sailom 30/11/2021
            }
        }

        private void btnPrintCrys_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txdDocNo.Text))
            {
                FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocNo", txdDocNo.Text);
                this.OpenCrystalReportsPopup("ใบโอนย้ายสินค้า", "Form_RB.rpt", "Form_RB", _params);
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

        private void btnSearchBranchCode_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกเดโป้/สาขา");
        }

        private void btnSearchFromWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchFromBWHControls, "เลือกคลังสินค้า", fbiPredicate);

            this.BindData("Employee", searchEmpControls, txtSaleEmpID.Text);
        }

        private void btnSeatchToWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchToBWHControls, "เลือกคลังสินค้า", fbiPredicate);
        }

        private void btnSearchEmpCode_Click(object sender, EventArgs e)
        {
            this.OpenEmployeePopup(searchEmpControls, "เลือกพนักงาน");
        }

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grdList.SetCellContentClick(this, sender, e, "RBProduct", 5);
        }

        private void grdList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0)
            //{
            //    grdList.ModifyComboBoxCell(allProduct, e.RowIndex, bu, 3, 0);
            //}

            if (isAdd) //when click add button
            {
                if (e.ColumnIndex == 0)
                    grdList.ModifyComboBoxCell(allProduct, e.RowIndex, bu, 3, 0);
            }
            else //when search data
            {
                if (e.ColumnIndex == 0)
                {
                    //Last edit by sailom.k 14/09/2021 tunning performance
                    if (allPRDetails.Count > 0 && prodUOMs.Count > 0)
                    {
                        grdList.ModifyComboBoxCell_Tunning(allProduct, e.RowIndex, bu, 3, 0, prodUOMs);
                    }
                }
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
            ComboBox cb = (ComboBox)sender;
            if (cb.Items != null && cb.Items.Count > 0)
            {
                CalculateRow(grdList, grdList.CurrentRow.Index);
            }
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
                            grd.Rows.RemoveAt(currentRowIndex);
                            isNewRow = false;
                        }

                        if (isNewRow)
                            grd.BindDataGridViewRow(dataGridList, initDataGridList, dt, 0, cell0.EditedFormattedValue.ToString(), currentRowIndex, 0, ref validateNewRow, "RBProduct", false);
                    }

                    if (isNewRow)
                        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 3];
                }
                else if (curentColIndex == 3)
                {
                    grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 2];
                }
                else if (curentColIndex == 5)
                {
                    if (cell0.IsNotNullOrEmptyCell())
                    {
                        if ((grd.RowCount - 1) == currentRowIndex)
                        {
                            //AddNewRow(grd, currentRowIndex + 1);
                            grdList.AddNewRow(allProduct, initDataGridList, 0, "", currentRowIndex + 1, validateNewRow, uoms, bu, this, 0);

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
                        grdList.BindComboBoxCell(allProduct, grd.Rows[currentRowIndex], currentRowIndex, false, 3, uoms, this, bu, 0);
                        CalculateRow(grd, currentRowIndex);
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
                        if (rowIndex <= _grd.RowCount - 1)
                        {
                            CalculateRow(_grd, rowIndex);
                        }
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

        private void frmRB_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void btnPODoc_Click(object sender, EventArgs e)
        {
            this.OpenIVDocPopup("ใบกำกับสินค้า/กำกับภาษี", "IVPreRB");
        }

        private void txtPODoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BindRBFromPOData(txtPODoc.Text, "IV", false);
        }

        private void CreateGridBtnList()
        {
            contextMenuStrip1 = new ContextMenuStrip();

            contextMenuStrip1.Items.Clear();
            contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(grdMenu_Opening);

            grdList.ContextMenuStrip = contextMenuStrip1;
        }

        void grdMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Acquire references to the owning control and item.
            Control c = contextMenuStrip1.SourceControl as Control;
            ToolStripDropDownItem tsi = contextMenuStrip1.OwnerItem as ToolStripDropDownItem;

            // Clear the ContextMenuStrip control's Items collection.
            contextMenuStrip1.Items.Clear();

            // Populate the ContextMenuStrip control with its default items.
            var printImage = new Bitmap(AllCashUFormsApp.Properties.Resources.copyBtn).ImageToByte();

            List<tbl_MstMenu> menuList = new List<tbl_MstMenu>();
            tbl_MstMenu m = new tbl_MstMenu();
            m.MenuID = 300;
            m.MenuName = "prdDetails";
            m.MenuText = "รายละเอียดสินค้า";
            m.FormName = "prdDetails";
            m.MenuImage = printImage;
            menuList.Add(m);

            printImage = new Bitmap(AllCashUFormsApp.Properties.Resources.depo).ImageToByte();
            m = new tbl_MstMenu();
            m.MenuID = 301;
            m.MenuName = "prdStock";
            m.MenuText = "ตรวจสอบสินค้าเคลื่อนไหว";
            m.FormName = "prdStock";
            m.MenuImage = printImage;
            menuList.Add(m);

            printImage = new Bitmap(AllCashUFormsApp.Properties.Resources.invoiceFull).ImageToByte();
            m = new tbl_MstMenu();
            m.MenuID = 302;
            m.MenuName = "repStock";
            m.MenuText = "รายงานสินค้าคงเหลือ แยกตามคลัง";
            m.FormName = "repStock";
            m.MenuImage = printImage;
            menuList.Add(m);

            foreach (var item in menuList)
            {
                contextMenuStrip1.Items.Add(item.MenuText, item.MenuImage.byteArrayToImage(), ToolGrdStripMenuItem_Click);
            }

            // Set Cancel to false. 
            // It is optimized to true based on empty entry.
            e.Cancel = false;
        }

        private void ToolGrdStripMenuItem_Click(object sender, EventArgs e)
        {
            string menuStripTxt = ((System.Windows.Forms.ToolStripItem)sender).Text;
            if (grdList.CurrentCell.RowIndex != -1 && grdList.CurrentCell.ColumnIndex != -1)
            {
                int rowIndex = grdList.CurrentCell.RowIndex;
                int colIndex = grdList.CurrentCell.ColumnIndex;
                string productID = grdList.Rows[rowIndex].Cells[0].EditedFormattedValue.ToString();

                if (!string.IsNullOrEmpty(productID))
                {
                    switch (menuStripTxt)
                    {
                        case "รายละเอียดสินค้า":
                            {
                                MainForm mfrm = null;
                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Name.ToLower() == "mainform")
                                    {
                                        mfrm = (MainForm)f;
                                    }
                                }

                                frmProductInfo frm = new frmProductInfo();
                                frm.MdiParent = mfrm;
                                frm.StartPosition = FormStartPosition.CenterParent;
                                frm.WindowState = FormWindowState.Maximized;
                                frm.Show();
                                frm.BindProductInfo(productID);
                            }
                            break;
                        case "ตรวจสอบสินค้าเคลื่อนไหว":
                            {
                                MainForm mfrm = null;
                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Name.ToLower() == "mainform")
                                    {
                                        mfrm = (MainForm)f;
                                    }
                                }

                                frmProductMovement frm = new frmProductMovement();
                                frm.MdiParent = mfrm;
                                frm.StartPosition = FormStartPosition.CenterParent;
                                frm.WindowState = FormWindowState.Maximized;
                                frm.Show();
                                frm.BindProductMovement(txtFromWHCode.Text, txtFromWHCode.Text, productID, true);

                            }
                            break;
                        case "รายงานสินค้าคงเหลือ แยกตามคลัง":
                            {
                                var cDate = DateTime.Now.AddDays(1);
                                Dictionary<string, object> _params = new Dictionary<string, object>();
                                var whid = txtFromWHCode.Text.Length < 6 ? (bu.tbl_Branchs[0].BranchID + txtFromWHCode.Text) : txtFromWHCode.Text;

                                _params.Add("@DateFr", cDate);
                                _params.Add("@DateTo", cDate);
                                _params.Add("@YearFr", -1);
                                _params.Add("@MonthFr", -1);
                                _params.Add("@YearTo", -1);
                                _params.Add("@MonthTo", -1);
                                //Doc Status--------------------------------------
                                _params.Add("@DocStatus", "4");
                                _params.Add("@BranchID", bu.tbl_Branchs[0].BranchID);
                                _params.Add("@WHID", whid);
                                //WHID--------------------------------------
                                //ProductSubGroupID--------------------------------------
                                _params.Add("@ProductSubGroupID", "");
                                //ProductSubGroupID--------------------------------------
                                //ProductID--------------------------------------
                                _params.Add("@ProductID", productID);
                                //ProductID--------------------------------------
                                
                                _params.Add("@FromWH", whid);
                                _params.Add("@ToWH", whid);
                                //FromWH And ToWH--------------------------------------
                                //SalAreaID--------------------------------------
                                _params.Add("@SalAreaID", "");
                                //SalAreaID--------------------------------------
                                //ShopTypeID--------------------------------------
                                _params.Add("@ShopTypeID", 0);

                                this.OpenExcelReportsPopup(menuStripTxt, "proc_RPTStock.XSLT", "proc_RPTStock_XSLT", _params, true);
                            }
                            break;
                        default: break;
                    }
                }
            }

        }
    }
}
