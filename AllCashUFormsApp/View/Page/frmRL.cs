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
    public partial class frmRL : Form
    {
        MenuBU menuBU = new MenuBU();
        RL bu = new RL();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        List<tbl_ProductUom> uoms = new List<tbl_ProductUom>();
        bool validateNewRow = true;
        public string docTypeCode = "";
        int runDigit = 0;
        List<Control> searchBranchControls = new List<Control>();
        List<Control> searchBWHControls = new List<Control>();
        List<Control> searchEmpControls = new List<Control>();
        List<string> readOnlyControls = new List<string>();
        int[] cellEdit = new int[] { 1, 4, 6 };
        int[] cellReadOnly = new int[] { 1, 2, 4 };
        //int[] uomList = new int[] { 1, 2, 3, 4 };
        int[] numberCell = new int[] { 6 };
        Dictionary<int, string> dataGridList = new Dictionary<int, string>();
        Dictionary<int, string> initDataGridList = new Dictionary<int, string>();

        Func<tbl_BranchWarehouse, bool> fbiPredicate = (x => x.VanType != 0); //x.WHID.Contains("V"));
        List<tbl_Product> allProduct = new List<tbl_Product>();
        List<tbl_ProductUomSet> allUomSet = new List<tbl_ProductUomSet>();
        List<tbl_ProductUom> allUOM = new List<tbl_ProductUom>();
        //List<tbl_ProductPriceGroup> allProductPrice = new List<tbl_ProductPriceGroup>();
        List<tbl_ProductGroup> allProdGroup = new List<tbl_ProductGroup>();
        List<tbl_ProductSubGroup> allProdSubGroup = new List<tbl_ProductSubGroup>();

        List<tbl_PRDetail> allPRDetails = new List<tbl_PRDetail>();
        List<tbl_ProductUom> prodUOMs = new List<tbl_ProductUom>();
        tbl_Employee emp = new tbl_Employee();
        tbl_ApSupplier supp = new tbl_ApSupplier();

        bool isAdd = false;

        public frmRL()
        {
            InitializeComponent();

            searchBranchControls = new List<Control>() { txtBranchCode, txtBranchName };
            searchBWHControls = new List<Control> { txtWHCode, txtWHName, txtSaleEmpID };
            searchEmpControls = new List<Control> { txtEmpCode, txtEmpName };
            readOnlyControls = new string[] { txtBranchName.Name, txtWHName.Name, txtEmpName.Name, txtCrUser.Name }.ToList();

            dataGridList = new Dictionary<int, string>() { { 0, "ProductSubGroupName" }, { 1, "ProductID" }, { 3, "ProductName" }, { 4, "UomSetID" }, { 5, "BaseQtyDT" } };
            initDataGridList = new Dictionary<int, string>() { { 0, "" }, { 1, "" }, { 3, "" }, { 4, "combobox" }, { 5, "" }, { 6, "0" } };

            txtBranchCode.KeyDown += TxtFromBranchID_KeyDown;
            txtWHCode.KeyDown += TxtWHCode_KeyDown;
            txdDocNo.KeyDown += TxdDocNo_KeyDown;
            txtEmpCode.KeyDown += TxtEmpCode_KeyDown;

            //txtBranchCode.SetSearchControl("FromBranchID", searchBranchControls);
            //txtWHCode.SetSearchControl("BranchWarehouse", searchBWHControls);
            //txtEmpCode.SetSearchControl("Employee", searchEmpControls);
        }

        #region private methods

        private void InitPage()
        {
            var documentType = bu.tbl_DocumentType.FirstOrDefault(x => x.DocTypeCode.Trim() == "RL");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                runDigit = documentType.DocFormat.Length;// - 2; edit by sailom 03/10/2021 for support tablet docno 14 digit
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
            btnUpdateInvWharehouse.Enabled = btnClose.Enabled;

            dtpDocDate.SetDateTimePickerFormat();

            allUOM = bu.tbl_ProductUom;

            uoms = new List<tbl_ProductUom>();
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

            emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            supp = bu.GetSupplier(txtBranchCode.Text);
        }

        private void AddNewRow(DataGridView grd, int rowIndex)
        {
            if (!validateNewRow)
            {
                return;
            }

            grd.Rows.Add(1);
            //InitRowData("", rowIndex);
            grd.InitRowData(this, initDataGridList, 1, "", rowIndex, allProduct, uoms, bu, 1);

            ManageCheckAllProduct(grd);
        }

        public void BindRLData(string rlDocNo)
        {
            FormHelper.invWhItems = new List<tbl_InvMovement>(); // edit by sailom .k 13/12/2021

            bu.GetDocData(rlDocNo, docTypeCode);

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

            bool checkEditMode = bu.CheckExistsPR(rlDocNo);
            pr.DocStatus.CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);

            if (ddlDocStatus.SelectedValue != null && ddlDocStatus.SelectedValue.ToString() == "3")
                btnPrint.Enabled = false;
            else
                btnPrint.Enabled = true;

            CreateGridBtnList();
        }

        private void BindPRMaster(tbl_PRMaster pr)
        {
            txdDocNo.Text = pr.DocNo;
            txtDocRef.Text = pr.DocRef;  //edit by sailom .k 13/12/2021
            dtpDocDate.Value = pr.DocDate.ToDateTimeFormat();

            var branch = bu.GetBranch();
            if (branch != null && branch.Count > 0)
            {
                txtBranchCode.Text = pr.FromBranchID;
                txtBranchName.Text = branch.FirstOrDefault(x => x.BranchCode == pr.FromBranchID).BranchName;
            }

            Func<tbl_BranchWarehouse, bool> bwhPredicate = (x => x.WHID == pr.ToWHID);
            var bwh = bu.GetBranchWarehouse(bwhPredicate);
            if (bwh != null)
            {
                txtWHCode.Text = pr.ToWHID;
                txtWHName.Text = bwh.WHName;
            }

            Func<tbl_Employee, bool> empPredicate = (x => x.EmpCode == pr.ToWHID);

            var emp = new tbl_Employee();
            if (ddlDocStatus.SelectedValue != null && ddlDocStatus.SelectedValue.ToString() == "3")
            {
                if (bwh != null)
                    emp = bu.GetEmployee(bwh.SaleEmpID);
            }
            else
            {
                emp = bu.GetEmployee(pr.RequestBy);
            }

            if (emp == null)
            {
                if (bwh != null)
                    emp = bu.GetEmployee(bwh.SaleEmpID);
            }

            if (emp != null)
            {
                txtEmpCode.Text = pr.RequestBy;
                txtEmpName.Text = string.Join(" ", emp.TitleName, emp.FirstName, emp.LastName);

                if (string.IsNullOrEmpty(txtEmpCode.Text))
                {
                    txtEmpCode.Text = bwh.SaleEmpID;
                }
            }

            if (ddlDocStatus.DataSource == null)
            {
                var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "3" || x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
                ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
            }
            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == pr.DocStatus; };
            ddlDocStatus.SelectedValueDropdownList(condition);

            var user = bu.GetEmployeeByUserName(pr.CrUser); //edit by sailom 22/03/2022 //bu.GetEmployeeByUserName(pr.EmpID); //edit by sailom .k 13/12/2021
            if (user != null)
                txtCrUser.Text = string.Join(" ", user.TitleName, user.FirstName, user.LastName);
            else
                txtCrUser.Text = pr.CrUser;

            txtComment.Text = pr.Comment;
        }

        private void BindPRDetail(List<tbl_PRDetail> prDts)
        {
            grdList.Rows.Clear();

            //Last edit by sailom.k 14/09/2021 tunning performance-------------------------
            if (bu.tbl_PRMaster.DocStatus != "3")
                prDts = prDts.Where(a => a.ReceivedQty != 0).ToList();

            allPRDetails = prDts;
            isAdd = false;

            var listPrdID = new List<string>();


            listPrdID = allPRDetails.Select(x => x.ProductID).ToList();

            prodUOMs.AddRange(bu.GetProductUOM(listPrdID));
            //Last edit by sailom.k 14/09/2021 tunning performance--------------------

            //var allProduct = bu.tbl_Product;
            //var allUomSet = bu.tbl_ProductUomSet;

            if (chkShowAllProduct.Checked)
            {
                var _allProduct = new Product().GetDataTable();

                if (_allProduct != null && _allProduct.Rows.Count > 0)
                {

                    for (int i = 0; i < _allProduct.Rows.Count; i++)
                    {
                        AddNewRow(grdList, i);
                        //BindDataGrid(_allProduct, i, true);
                        grdList.BindDataGrid(allProduct, dataGridList, initDataGridList, _allProduct, 1, i, validateNewRow, this, uoms, bu, 1, chkShowAllProduct.Checked);

                        for (int j = 0; j < prDts.Count; j++)
                        {
                            var cell1 = grdList.Rows[i].Cells[1];
                            if (cell1.IsNotNullOrEmptyCell())
                            {
                                if (cell1.EditedFormattedValue.ToString() == prDts[j].ProductID)
                                {
                                    //string productName = string.Empty;
                                    //if (!string.IsNullOrEmpty(prDts[j].ProductName))
                                    //{
                                    //    productName = prDts[j].ProductName;
                                    //}
                                    //else
                                    //{
                                    //    var data = allProduct.FirstOrDefault(x => x.ProductID == prDts[j].ProductID);
                                    //    if (data != null)
                                    //    {
                                    //        productName = data.ProductName;
                                    //    }
                                    //}

                                    //grdList.BindComboBoxCell(grdList.Rows[i], i, false, 4, uoms, this, bu, 1);

                                    //grdList.Rows[i].Cells[3].Value = productName;
                                    //grdList.Rows[i].Cells[4].Value = prDts[j].OrderUom; 

                                    //Func<tbl_ProductUomSet, bool> predicate = (x => x.ProductID == prDts[j].ProductID && x.UomSetID == prDts[j].OrderUom);
                                    //var uomSet = bu.GetUOMSet(predicate);
                                    //if (uomSet != null && uomSet.Count > 0)
                                    //{
                                    //    grdList.Rows[i].Cells[5].Value = "1*" + uomSet[0].BaseQty;
                                    //}
                                    //else
                                    //{
                                    //    grdList.Rows[i].Cells[5].Value = "";
                                    //}


                                    //last edit by sailom.k 19/05/2022-------------------------------------

                                    decimal? _rlQty = 0;
                                    decimal sQty = (prDts[j].SendQty != null ? prDts[j].SendQty.Value : 0);
                                    decimal rQty = (prDts[j].ReceivedQty != null ? prDts[j].ReceivedQty.Value : 0);

                                    if (bu.tbl_PRMaster.DocStatus == "3") //in process
                                        _rlQty = sQty;
                                    else if (bu.tbl_PRMaster.DocStatus == "4") //close
                                        _rlQty = rQty;
                                    else //cancel
                                        _rlQty = rQty != 0 ? rQty : sQty;

                                    grdList.Rows[i].Cells[6].Value = _rlQty;

                                    //last edit by sailom.k 19/05/2022----------------------------------------
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //var allUOM = bu.tbl_ProductUom;

                for (int i = 0; i < prDts.Count; i++)
                {
                    grdList.Rows.Add(1);

                    grdList.Rows[i].Cells[1].Value = prDts[i].ProductID;

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

                    if (prDts[i].ProductID == "90000069")//for test
                    {

                    }

                    //BindComboBoxCell(grdList.Rows[i], i, false);
                    var prd = allProduct.FirstOrDefault(x => x.ProductID == prDts[i].ProductID);
                    if (prd != null)
                        uoms = allUOM.Where(x => x.ProductUomID == prd.SaleUomID || x.ProductUomID == prd.PurchaseUomID).ToList();

                    grdList.BindComboBoxCell(allProduct, grdList.Rows[i], i, true, 4, uoms, this, bu, 1);

                    grdList.Rows[i].Cells[3].Value = productName;
                    //grdList.Rows[i].Cells[4].Value = prDts[i].OrderUom; // allUOM.First(x => x.ProductUomID == prDts[i].OrderUom).ProductUomName;

                    //Func<tbl_ProductUomSet, bool> predicate = (x => x.ProductID == prDts[i].ProductID && x.UomSetID == prDts[i].OrderUom);
                    var uomSet = allUomSet.Where(x => x.ProductID == prDts[i].ProductID && x.UomSetID == prDts[i].OrderUom).ToList(); //bu.GetUOMSet(predicate);

                    //last edit by sailom 25/10/2021----------------------------
                    //if (uomSet != null && uomSet.Count > 0)
                    //{
                    //    grdList.Rows[i].Cells[5].Value = "1*" + uomSet[0].BaseQty;
                    //}
                    //else
                    //{
                    //    grdList.Rows[i].Cells[5].Value = "";
                    //}

                    //last edit by sailom .k 14/05/2022--------------------------------
                    //grdList.Rows[i].Cells[6].Value = prDts[i].SendQty;

                    //last edit by sailom.k 19/05/2022-------------------------------------

                    decimal? _rlQty = 0;
                    decimal sQty = (prDts[i].SendQty != null ? prDts[i].SendQty.Value : 0);
                    decimal rQty = (prDts[i].ReceivedQty != null ? prDts[i].ReceivedQty.Value : 0);

                    if (bu.tbl_PRMaster.DocStatus == "3") //in process
                        _rlQty = sQty;
                    else if (bu.tbl_PRMaster.DocStatus == "4") //close
                        _rlQty = rQty;
                    else //cancel
                        _rlQty = rQty != 0 ? rQty : sQty;

                    grdList.Rows[i].Cells[6].Value = _rlQty;

                    //last edit by sailom.k 19/05/2022----------------------------------------

                    grdList.Rows[i].Cells[4].Value = prDts[i].OrderUom;
                    grdList.Rows[i].Cells[5].Value = "1*" + uomSet[0].BaseQty;
                    //last edit by sailom .k 14/05/2022--------------------------------

                    ////last edit by sailom 25/10/2021----------------------------
                    //if (bu.tbl_PRMaster.DocStatus == "3") //no adjust
                    //{
                    //    grdList.Rows[i].Cells[6].Value = prDts[i].SendQty;
                    //    grdList.Rows[i].Cells[4].Value = prDts[i].OrderUom;
                    //    grdList.Rows[i].Cells[5].Value = "1*" + uomSet[0].BaseQty;
                    //}
                    //else
                    //{
                    //    SetDefaultCbo(prDts[i].ProductID, prDts[i].ReceivedQty.Value, prDts[i].OrderUom, i);
                    //}
                    ////last edit by sailom 25/10/2021----------------------------
                }
            }

            grdList.Columns[0].Visible = chkShowAllProduct.Checked;
        }

        private void SetDefaultCbo(string productID, decimal receivedQty, int orderUom, int i)
        {
            var _baseUom = allUomSet.Where(x => x.ProductID == productID).ToList();
            if (_baseUom != null && _baseUom.Count > 0)
            {
                //var baseUOMID = _baseUom.FirstOrDefault().BaseUomID;
                //var maxBaseQty = _baseUom.Max(x => x.BaseQty);
                //var minBaseQty = _baseUom.Min(x => x.BaseQty);

                //var tmpMaxUOM = _baseUom.FirstOrDefault(x => x.BaseQty == maxBaseQty).UomSetID;
                //var tmpMinUOM = _baseUom.FirstOrDefault(x => x.BaseQty == minBaseQty).UomSetID;

                //var baseQty = _baseUom.FirstOrDefault(x => x.UomSetID == orderUom);
                //if (baseQty != null)
                //{
                //    if (orderUom == baseUOMID)
                //    {
                //        var tmpQty = receivedQty * minBaseQty;
                //        if (tmpQty > maxBaseQty)
                //        {
                //            grdList.Rows[i].Cells[6].Value = receivedQty / maxBaseQty;
                //            grdList.Rows[i].Cells[5].Value = "1*" + maxBaseQty.ToString();
                //            grdList.Rows[i].Cells[4].Value = orderUom;
                //        }
                //        else
                //        {
                //            grdList.Rows[i].Cells[6].Value = receivedQty * minBaseQty;
                //            grdList.Rows[i].Cells[5].Value = "1*" + minBaseQty.ToString();
                //            grdList.Rows[i].Cells[4].Value = orderUom;
                //        }
                //    }
                //    else
                //    {
                //        grdList.Rows[i].Cells[6].Value = receivedQty / minBaseQty;
                //        grdList.Rows[i].Cells[5].Value = "1*" + minBaseQty.ToString();
                //        grdList.Rows[i].Cells[4].Value = orderUom;
                //    }
                //}


                var maxBaseQty = _baseUom.Max(x => x.BaseQty);
                if (maxBaseQty != 1 && receivedQty >= maxBaseQty && (receivedQty % maxBaseQty) == 0) //Big UOM adjust
                {
                    grdList.Rows[i].Cells[6].Value = receivedQty / maxBaseQty;
                    grdList.Rows[i].Cells[5].Value = "1*" + maxBaseQty.ToString();
                    var _uom = allUomSet.FirstOrDefault(x => x.ProductID == productID && x.BaseQty == maxBaseQty);
                    if (_uom != null)
                        grdList.Rows[i].Cells[4].Value = _uom.UomSetID;
                }
                else
                {
                    if (orderUom == _baseUom.First().BaseUomID) //small UOM
                    {
                        grdList.Rows[i].Cells[6].Value = receivedQty;
                        grdList.Rows[i].Cells[4].Value = orderUom;
                        grdList.Rows[i].Cells[5].Value = "1*1";
                    }
                    else  //Big UOM normal
                    {
                        grdList.Rows[i].Cells[6].Value = receivedQty;
                        grdList.Rows[i].Cells[4].Value = orderUom;
                        grdList.Rows[i].Cells[5].Value = "1*" + maxBaseQty.ToString();
                    }

                }
            }
        }

        public void BindSearchProduct(DataTable productDT, int rowIndex)
        {
            if (!string.IsNullOrEmpty(productDT.Rows[0]["ProductID"].ToString()))
            {
                if (!string.IsNullOrEmpty(grdList.Rows[rowIndex].Cells[1].EditedFormattedValue.ToString()))
                {
                }
            }

            validateNewRow = true;
            //ValidateDuplicateSKU(productDT.Rows[0]["ProductID"].ToString(), rowIndex);
            grdList.ValidateDuplicateSKU(productDT.Rows[0]["ProductID"].ToString(), 1, rowIndex, ref validateNewRow);

            //BindDataGrid(productDT, rowIndex);
            grdList.BindDataGrid(allProduct, dataGridList, initDataGridList, productDT, 1, rowIndex, validateNewRow, this, uoms, bu, 1, chkShowAllProduct.Checked);
        }

        private void InitHeader()
        {
            var b = bu.GetBranch();
            if (b != null)
            {
                this.BindData("FromBranchID", searchBranchControls, b[0].BranchID);
            }

            var employee = bu.GetEmployee(Helper.tbl_Users.EmpID);
            txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName, employee.LastName);

            dtpDocDate.SetDateTimePickerFormat();
            //txtComment.Text = "หมายเหตุ PR";

            var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "3" || x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
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

            if (chkShowAllProduct.Checked)
            {
                var _allProduct = new Product().GetDataTable(false);
                if (_allProduct != null && _allProduct.Rows.Count > 0)
                {
                    for (int i = 0; i < _allProduct.Rows.Count; i++)
                    {
                        AddNewRow(grdList, i);

                        grdList.CurrentCell = grdList.Rows[0].Cells[5];
                        grdList.BindDataGrid(allProduct, dataGridList, initDataGridList, _allProduct, 1, i, validateNewRow, this, bu.tbl_ProductUom, bu, 1, chkShowAllProduct.Checked); //last edit by sailom .k 07/06/2022
                    }
                }
            }
            else
            {
                AddNewRow(grdList, 0);
            }

            txtBranchCode.Focus();
            ddlDocStatus.SelectedValue = "4";
            isAdd = true;

            FormHelper.invWhItems = new List<tbl_InvMovement>(); // edit by sailom .k 13/12/2021
            //txtComment.Text = "หมายเหตุ PR";
        }

        private void ManageCheckAllProduct(DataGridView grd)
        {
            grd.Columns[0].Visible = chkShowAllProduct.Checked;
            if (chkShowAllProduct.Checked)
            {
                grdList.CellContentClick -= grdList_CellContentClick;
                Color c = ColorTranslator.FromHtml("#E3E3E3");

                grd.Columns[1].DefaultCellStyle.BackColor = c;
            }
            else
            {
                Color c = Color.White;
                grd.Columns[1].DefaultCellStyle.BackColor = c;
            }
        }

        private string PreparePRMaster(bool editFlag = false)
        {
            bu.tbl_PRMaster = new tbl_PRMaster();
            var branch = bu.tbl_Branchs;

            var pr = bu.tbl_PRMaster;

            if (ddlDocStatus.SelectedValue != null && ddlDocStatus.SelectedValue.ToString() == "3")
            {
                pr.DocNo = txdDocNo.Text;
            }
            else
            {

                bool checkEditMode = bu.CheckExistsPR(txdDocNo.Text);
                if (checkEditMode)
                {
                    pr.DocNo = txdDocNo.Text;
                }
                else
                {
                    pr.DocNo = bu.GenDocNo(docTypeCode, txtWHCode.Text);
                }
            }

            pr.RevisionNo = 0;
            pr.DocTypeCode = docTypeCode;

            if (ddlDocStatus.SelectedValue != null && ddlDocStatus.SelectedValue.ToString() == "3")
                pr.DocStatus = "4";
            else
                pr.DocStatus = ddlDocStatus.SelectedValue.ToString();

            pr.DocDate = dtpDocDate.Value.ToDateTimeFormat();
            pr.DocRef = ""; // txtDocRef.Text; //edit by sailom .k 13/12/2021
            pr.FromBranchID = branch[0].BranchCode;
            pr.FromWHID = branch[0].BranchCode + "1000"; //txtBranchCode.Text;
            pr.RequestBy = txtEmpCode.Text;
            pr.ToBranchID = branch[0].BranchCode;
            pr.ToWHID = txtWHCode.Text;
            pr.StatusInOut = null;
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
            //var allPrdUOM = bu.GetUOM();// tbl_ProductUomPre);

            bu.tbl_PRDetails.Clear();

            var prDts = bu.tbl_PRDetails;
            DateTime crDate = DateTime.Now;

            for (int i = 0; i < grdList.RowCount; i++)
            {
                var prDt = new tbl_PRDetail();

                var cell1 = grdList.Rows[i].Cells[1];
                var cell3 = grdList.Rows[i].Cells[3];
                var cell4 = grdList.Rows[i].Cells[4];
                var cell6 = grdList.Rows[i].Cells[6];

                if (cell1.IsNotNullOrEmptyCell() && cell3.IsNotNullOrEmptyCell() && cell6.IsNotNullOrEmptyCell())
                {
                    if (Convert.ToDecimal(cell6.EditedFormattedValue) > 0)
                    {
                        prDt.DocNo = bu.tbl_PRMaster.DocNo;
                        prDt.ProductID = cell1.EditedFormattedValue.ToString();
                        prDt.Line = Convert.ToInt16(i);
                        prDt.ProductName = cell3.EditedFormattedValue.ToString();

                        //Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => uomList.Contains(x.ProductUomID));

                        var prdUOM = allUOM.FirstOrDefault(x => x.ProductUomName == cell4.EditedFormattedValue.ToString());
                        if (prdUOM != null)
                        {
                            prDt.OrderUom = prdUOM.ProductUomID;
                        }

                        prDt.OrderQty = 0;
                        prDt.SendQty = Convert.ToDecimal(cell6.EditedFormattedValue);
                        prDt.ReceivedQty = Convert.ToDecimal(cell6.EditedFormattedValue);
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
                    invMm.TrnDate = dtpDocDate.Value.ToDateTimeFormat(); //crDate.ToDateTimeFormat();
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

                    //if (editFlag)
                    {
                        invMm.EdDate = crDate;
                        invMm.TrnType = ddlDocStatus.SelectedValue.ToString() == "5" || ddlDocStatus.SelectedValue.ToString() == "3" ? "X" : "T";
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

        private void PrepareQtyOnHand(tbl_InvWarehouse invWh, List<tbl_InvWarehouse> allWHStock, string productID, string whid, decimal unitQty)
        {
            //var invWhItem = bu.GetInvWarehouse(productID, whid);
            var invWhItem = allWHStock.Where(x => x.ProductID == productID && x.WHID == whid).ToList(); //edit by sailom .k 13/12/2021
            if (invWhItem != null && invWhItem.Count > 0)
            {
                if (whid.Contains("1000"))
                    invWh.QtyOnHand = invWhItem[0].QtyOnHand - unitQty;
                else
                    invWh.QtyOnHand = invWhItem[0].QtyOnHand + unitQty;
            }
            else
            {
                if (whid.Contains("1000"))
                    invWh.QtyOnHand = -unitQty;
                else
                    invWh.QtyOnHand = +unitQty;
            }
        }

        private void PrepareInvWarehouseFrom(bool editFlag = false)
        {
            bu.tbl_InvWarehouses.Clear();

            var pr = bu.tbl_PRMaster;
            string whid = bu.tbl_Companies.First().WHID;
            SubPrepareInvWarehouse(whid, editFlag);
        }

        private void PrepareInvWarehouseTo(bool editFlag = false)
        {
            var pr = bu.tbl_PRMaster;
            string whid = txtWHCode.Text;
            SubPrepareInvWarehouse(whid, editFlag);
        }

        private void SetQtyOnHand(tbl_InvWarehouse invWh, decimal unitQty, string productID, string whID, bool editFlag)
        {
            var invWhItem = bu.GetInvWarehouse(productID, whID);
            if (editFlag)
            {
                if (invWhItem != null && invWhItem.Count > 0)
                {
                    if (ddlDocStatus.SelectedValue.ToString() == "5")
                    {
                        if (whID.Contains("1000"))
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
                var prdCodeCell = grdList.Rows[i].Cells[1];
                var qtyCell = grdList.Rows[i].Cells[6];
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
                editFlag = checkEditMode;

                //if (checkEditMode && ddlDocStatus.SelectedValue.ToString() != "3")
                //{
                //    string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                //    string title = "ยืนยันการบันทึก!!";
                //    if (!cfMsg.ConfirmMessageBox(title))
                //        return;

                //    bu = new RL();

                //    DateTime crDate = DateTime.Now;

                //    docno = txdDocNo.Text;
                //    editFlag = true;
                //    bu.tbl_DocRunning = new List<tbl_DocRunning>();

                //    bu.tbl_PRMaster = null;
                //    bu.tbl_PRMaster = bu.GetPRMaster(docno);
                //    bu.tbl_PRMaster.DocStatus = ddlDocStatus.SelectedValue.ToString();
                //    //bu.tbl_PRMaster.DocDate = dtpDocDate.Value.ToDateTimeFormat();
                //    bu.tbl_POMaster.EdDate = crDate;
                //    bu.tbl_POMaster.EdUser = Helper.tbl_Users.Username;

                //    bu.tbl_PRDetails = bu.GetPRDetails(docno);
                //    PreparePRDetail(editFlag);

                //    bu.tbl_InvMovements.Clear();
                //    bu.tbl_InvMovements.AddRange(bu.GetInvMovement(docno));

                //    if (ddlDocStatus.SelectedValue.ToString() == "5")
                //        bu.tbl_InvMovements.ForEach(x => x.TrnType = "X");

                //    bu.tbl_InvWarehouses.Clear();

                //    string fromWHID = bu.tbl_PRMaster.FromWHID;
                //    string toWHID = bu.tbl_PRMaster.ToWHID;
                //    //var prDts = bu.GetPRDetails(docno);

                //    foreach (var prDt in bu.tbl_PRDetails)
                //    {
                //        SetWarehousesQTY(prDt, fromWHID, editFlag);
                //        SetWarehousesQTY(prDt, toWHID, editFlag);

                //        var invWhItem = bu.GetStockMovement(prDt.ProductID, fromWHID); // bu.GetInvWarehouse(productID, whid); //15-05-2021 by sailom.k
                //        if (invWhItem != null && invWhItem.Count > 0)
                //        {
                //            txt.Text = invWhItem.Sum(x => x.TrnQty).ToStringN0();  //invWhItem[0].QtyOnHand.ToStringN0(); //15-05-2021 by sailom.k
                //        }

                //        ret = bu.UpdateData();
                //}
                //else
                {
                    if (!ValidateSave())
                        return;

                    string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                    string title = "ยืนยันการบันทึก!!";
                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    bu = new RL();

                    //docno = bu.GenDocNo(docTypeCode, txtWHCode.Text);
                    //editFlag = false;
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
                    //docno = bu.tbl_POMaster.DocNo;
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

                    var poTmp = bu.GetPRMaster(txdDocNo.Text);
                    if (poTmp != null)
                        ddlDocStatus.SelectedValue = poTmp.DocStatus;

                    ddlDocStatus.SelectedValue.ToString().CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }

                btnUpdateInvWharehouse.Enabled = btnClose.Enabled;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void SetWarehousesQTY(tbl_PRDetail prDt, string whid, bool editFlag)
        {
            var invWh = new tbl_InvWarehouse();
            var invWhs = bu.GetInvWarehouse(prDt.ProductID, whid);
            if (invWhs != null && invWhs.Count > 0)
            {
                invWh = invWhs[0];
            }
            else
            {
                invWh.WHID = whid;
                invWh.ProductID = prDt.ProductID;
                invWh.CrDate = DateTime.Now;
                invWh.CrUser = Helper.tbl_Users.Username;
            }

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

            SetQtyOnHand(invWh, unitQty, prDt.ProductID, whid, editFlag);

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
            grd.CellFormatting -= new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdList_CellFormatting);
            grd.CellPainting -= new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdList_CellPainting);

            grd.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellContentClick);
            grd.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellEndEdit);
            grd.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(grdList_CellValidating);
            grd.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellValueChanged);
            grd.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(grdList_EditingControlShowing);
            grd.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(grdList_RowPostPaint);
            grd.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(grdList_UserDeletingRow);
            grd.KeyDown += new System.Windows.Forms.KeyEventHandler(grdList_KeyDown);
            grd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellClick);
            grd.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdList_CellFormatting);
            grd.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdList_CellPainting);

            grdList.Columns[0].Visible = false;
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

            if (ret) //validate header
            {
                errList.SetErrMessageList(txtBranchCode, lblBranchCode);
                errList.SetErrMessageList(txtWHCode, lblWHCode);
                errList.SetErrMessageList(txtEmpCode, lblEmpCode);

                if (errList.Count == 0)
                {
                    var branch = bu.tbl_Branchs; //bu.GetBranch(); //Last edit by sailom .k 07/02/2022
                    if (branch == null)
                    {
                        string t = lblBranchCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtBranchCode.ErrorTextBox();
                    }

                    //Func<tbl_BranchWarehouse, bool> branchWarehousePre = (x => x.VanType != 0 //x.WHID.Contains("V") 
                    //&& x.WHCode.ToLower() == txtWHCode.Text.ToLower());

                    //var wh = bu.GetBranchWarehouse(branchWarehousePre);
                    var wh = bu.GetBranchWarehouse(txtWHCode.Text); //Last edit by sailom .k 07/02/2022
                    if (wh == null)
                    {
                        string t = lblWHCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtWHCode.ErrorTextBox();
                    }

                    var emp = bu.GetEmployee(txtEmpCode.Text);
                    if (emp == null)
                    {
                        string t = lblEmpCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtEmpCode.ErrorTextBox();
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
                var whid = txtBranchCode.Text + "1000";

                if (!chkShowAllProduct.Checked) //normal product
                {
                    if (ddlDocStatus.SelectedValue.ToString() != "5")
                    {
                        ret = grdList.ValiadteDataGridView(allProduct, 1, 4, 6, 0, bu, whid, true);
                    }
                }
                else //show all product
                {
                    List<bool> checkQty = new List<bool>();

                    if (grdList.RowCount > 0)
                    {
                        for (int i = 0; i < grdList.RowCount; i++)
                        {
                            var prdCodeCell = grdList.Rows[i].Cells[1];
                            var qtyCell = grdList.Rows[i].Cells[6];

                            if (qtyCell.IsNotNullOrEmptyCell() && Convert.ToDecimal(qtyCell.EditedFormattedValue.ToString()) > 0) //check qty
                            {
                                checkQty.Add(true);
                            }
                        }
                    }

                    if (ret && checkQty.All(x => !x))
                    {
                        var message = "จำนวนสินค้าไม่ถูกต้อง กรุณาตรวจสอบอีกครั้ง !!!";
                        message.ShowWarningMessage();
                        ret = false;
                    }
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
                //txtEmpCode.SetSearchControl("Employee", searchEmpControls);
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

        private void TxtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("BranchWarehouse", searchBWHControls, txt.Text);
                this.BindData("Employee", searchEmpControls, txtSaleEmpID.Text);
            }
        }

        private void TxdDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txdDocNo.Text))
                {
                    BindRLData(txdDocNo.Text);
                }
            }
        }

        private void frmRL_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string msg = "start frmRL=>btnAdd_Click";
            msg.WriteLog(this.GetType());

            InitialData();

            msg = "end frmRL=>btnAdd_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string msg = "start frmRL=>btnEdit_Click";
            msg.WriteLog(this.GetType());

            isAdd = true; //Last edit by sailom.k 14/09/2021 tunning performance

            if (ddlDocStatus.SelectedValue != null && ddlDocStatus.SelectedValue.ToString() == "3")
            {
                btnCopy.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnPrint, txdDocNo.Text);
                btnPrintCrys.Enabled = btnPrint.Enabled;

                this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

                //Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == "4"; };
                //ddlDocStatus.SelectedValueDropdownList(condition);
                ddlDocStatus.Enabled = true;
                //ddlDocStatus.SelectedValue = "4";
                btnUpdateInvWharehouse.Enabled = btnClose.Enabled;

                validateNewRow = true;

                grdList.CellContentClick -= grdList_CellContentClick;
                grdList.CellContentClick += grdList_CellContentClick;

                ManageCheckAllProduct(grdList);
            }
            else
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


                btnUpdateInvWharehouse.Enabled = btnClose.Enabled;
                this.ActiveControl = dtpDocDate;
            }

            msg = "end frmRL=>btnEdit_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string msg = "start frmRL=>btnCopy_Click";
            msg.WriteLog(this.GetType());

            btnCopy.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnPrint, txdDocNo.Text);
            btnPrintCrys.Enabled = btnPrint.Enabled;

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            ddlDocStatus.Enabled = false;
            txdDocNo.Text = string.Empty;

            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == "4"; };
            ddlDocStatus.SelectedValueDropdownList(condition);
            ddlDocStatus.Enabled = false;
            btnUpdateInvWharehouse.Enabled = btnClose.Enabled;

            validateNewRow = true;
            isAdd = true;

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;

            ManageCheckAllProduct(grdList);

            txtDocRef.Text = "";

            msg = "end frmRL=>btnCopy_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "start frmRL=>btnSave_Click";
            msg.WriteLog(this.GetType());

            Save();

            msg = "end frmRL=>btnSave_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string msg = "start frmRL=>btnCancel_Click";
            msg.WriteLog(this.GetType());

            this.ClearControl(docTypeCode, runDigit);

            btnAdd.Enabled = true;
            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnPrintCrys.Enabled = btnPrint.Enabled;

            validateNewRow = true;

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            btnCancel.EnableButton(btnSearchDoc);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;
            btnUpdateInvWharehouse.Enabled = btnClose.Enabled;

            grdList.CellContentClick -= grdList_CellContentClick;

            msg = "end frmRL=>btnCancel_Click";
            msg.WriteLog(this.GetType());
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
                //this.OpenCrystalReportsPopup("ใบโอนย้ายสินค้า", "Form_RL.rpt", "Form_RL", _params);

                this.OpenReportingReportsNonPreViewPopup("ใบโอนย้ายสินค้า", "Form_RL.rdlc", "Form_RL", _params); //Reporting service by sailom 30/11/2021
            }
            else
            {
                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocNo", txdDocNo.Text);
                //this.OpenCrystalReportsPopup("ใบโอนย้ายสินค้า", "Form_RL.rpt", "Form_RL", _params);

                this.OpenReportingReportsPopup("ใบโอนย้ายสินค้า", "Form_RL.rdlc", "Form_RL", _params); //Reporting service by sailom 30/11/2021
            }
        }

        private void btnPrintCrys_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txdDocNo.Text))
            {
                FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocNo", txdDocNo.Text);
                this.OpenCrystalReportsPopup("ใบโอนย้ายสินค้า", "Form_RL.rpt", "Form_RL", _params);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchBranchCode_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกเดโป้/สาขา");
        }

        private void btnSearchWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchBWHControls, "เลือกคลังสินค้า", fbiPredicate);

            this.BindData("Employee", searchEmpControls, txtSaleEmpID.Text);
        }

        private void btnSearchEmpCode_Click(object sender, EventArgs e)
        {
            this.OpenEmployeePopup(searchEmpControls, "เลือกพนักงาน");
        }

        private void btnSearchDoc_Click(object sender, EventArgs e)
        {
            this.OpenDocPopup("ใบโอนสินค้าให้สาขา", docTypeCode);
        }

        private void btnDocRef_Click(object sender, EventArgs e)
        {
            //this.OpenDocPopup("ใบสั่งสินค้า", "???");
        }

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grdList.SetCellContentClick(this, sender, e, "RLProduct", 7);
        }

        private void grdList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isAdd) //when click add button
            {
                if (e.ColumnIndex == 1)
                    grdList.ModifyComboBoxCell(allProduct, e.RowIndex, bu, 4, 1);
            }
            else //when search data
            {
                if (e.ColumnIndex == 1)
                {
                    //Last edit by sailom.k 14/09/2021 tunning performance
                    if (allPRDetails.Count > 0 && prodUOMs.Count > 0)
                    {
                        grdList.ModifyComboBoxCell_Tunning(allProduct, e.RowIndex, bu, 4, 1, prodUOMs);
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
                if (grdList.Rows.Count > 0)
                {
                    int rowIndex = grdList.CurrentRow.Index;
                    var cell1 = grdList.Rows[rowIndex].Cells[1];
                    var cell4 = grdList.Rows[rowIndex].Cells[4];
                    var cell5 = grdList.Rows[rowIndex].Cells[5];

                    if (cell1.IsNotNullOrEmptyCell())
                    {
                        string productID = cell1.Value.ToString();
                        string uomSetName = cell4.EditedFormattedValue.ToString();

                        Func<tbl_ProductUomSet, bool> predicate = (x => x.ProductID == productID && x.UomSetName == uomSetName);
                        var uomSet = allUomSet.Where(predicate).ToList();// bu.GetUOMSet(predicate);
                        if (uomSet != null && uomSet.Count > 0)
                            cell5.Value = "1*" + uomSet[0].BaseQty;
                        else
                            cell5.Value = "";
                    }
                }
            }
        }

        private void DataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            grdList.SetCellNumberOnly(sender, e, numberCell.ToList());

            if (grdList.CurrentCell.ColumnIndex == 1)
            {
                e.Handled = chkShowAllProduct.Checked;
            }
        }

        private void DataGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!chkShowAllProduct.Checked)
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

                    var cell1 = grd.Rows[currentRowIndex].Cells[1];
                    var cell3 = grd.Rows[currentRowIndex].Cells[3];
                    if (curentColIndex == 1)
                    {
                        bool isNewRow = true;
                        if (cell1.IsNotNullOrEmptyCell())
                        {
                            validateNewRow = true;

                            var checkDup = grd.ValidateDuplicateSKU(cell1.EditedFormattedValue.ToString(), 1, currentRowIndex, ref validateNewRow);
                            if (!checkDup)
                            {
                                GridViewHelper.ShowDupSKUMessage();
                                cell1.Value = "";
                                validateNewRow = false;
                                grd.Rows.RemoveAt(currentRowIndex);
                                isNewRow = false;
                            }

                            if (isNewRow)
                                grd.BindDataGridViewRow(dataGridList, initDataGridList, dt, 0, cell1.EditedFormattedValue.ToString(), currentRowIndex, curentColIndex, ref validateNewRow, "RLProduct", false);
                        }

                        if (isNewRow)
                            grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 3];
                    }

                    else if (curentColIndex == 4)
                    {
                        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 2];
                    }
                    else if (curentColIndex == 6)
                    {
                        if (cell3.IsNotNullOrEmptyCell())
                        {
                            if ((grd.RowCount - 1) == currentRowIndex)
                            {
                                validateNewRow = true;

                                var checkDup = grd.ValidateDuplicateSKU(cell1.EditedFormattedValue.ToString(), 1, currentRowIndex, ref validateNewRow);
                                if (!checkDup)
                                {
                                    validateNewRow = false;
                                }

                                if (validateNewRow)
                                    AddNewRow(grd, currentRowIndex + 1);

                                if (validateNewRow)
                                {
                                    if (grd.RowCount > currentRowIndex + 1)
                                        grd.CurrentCell = grd.Rows[currentRowIndex + 1].Cells[1];
                                }
                            }
                            else
                            {
                                if (grd.RowCount > currentRowIndex + 1)
                                    grd.CurrentCell = grd.Rows[currentRowIndex + 1].Cells[1];
                            }
                        }
                        //else
                        //{
                        //    grd.CurrentCell = grd.Rows[currentRowIndex].Cells[7];
                        //}
                    }
                }
            }
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                grdList.SetRowPostPaint(sender, e, this.Font);
            }
            catch
            {

            }
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

                var cell1 = grd.Rows[currentRowIndex].Cells[1];

                if (cell1.IsNotNullOrEmptyCell())
                {
                    if (e.ColumnIndex == 1)
                    {
                        //BindComboBoxCell(grd.Rows[currentRowIndex], currentRowIndex, false);
                        var prd = allProduct.FirstOrDefault(x => x.ProductID == cell1.Value.ToString());
                        if (prd != null)
                            uoms = allUOM.Where(x => x.ProductUomID == prd.SaleUomID || x.ProductUomID == prd.PurchaseUomID).ToList();

                        grdList.BindComboBoxCell(allProduct, grd.Rows[currentRowIndex], currentRowIndex, false, 4, uoms, this, bu, 1);
                    }
                    else
                    {
                        grdList.BindComboBoxCell(allProduct, grd.Rows[currentRowIndex], currentRowIndex, false, 4, uoms, this, bu, 1);
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
            grdList.SetDeleteKeyDown(sender, e);
        }

        private void grdList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            grdList.SetCellClick(sender, e, cellEdit, 1);
        }

        private void grdList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                grdList.SetCellPainting(sender, e, 0);
            }
            catch
            {

            }

        }

        private void grdList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                grdList.SetCellFormatting(sender, e, 0);
            }
            catch
            {

            }
        }

        private void btnUpdateInvWharehouse_Click(object sender, EventArgs e)
        {
            //bool ret = bu.ManualUpdateInvWarehouse(dtpDocDate.Value);
            //if (ret)
            //{
            //    string msg = "อัพเดต Stock เรียบร้อยแล้ว!!";
            //    msg.ShowInfoMessage();
            //}
            //else
            //{
            //    string msg = "อัพเดต Stock ไม่สำเร็จ!!";
            //    msg.ShowErrorMessage();
            //}
        }

        #endregion

        private void frmRL_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
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
                string productID = grdList.Rows[rowIndex].Cells[1].EditedFormattedValue.ToString();

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
                                frm.BindProductMovement(txtWHCode.Text, txtWHCode.Text, productID, true);

                            }
                            break;
                        case "รายงานสินค้าคงเหลือ แยกตามคลัง":
                            {
                                var cDate = DateTime.Now.AddDays(1);
                                Dictionary<string, object> _params = new Dictionary<string, object>();

                                _params.Add("@DateFr", cDate);
                                _params.Add("@DateTo", cDate);
                                _params.Add("@YearFr", -1);
                                _params.Add("@MonthFr", -1);
                                _params.Add("@YearTo", -1);
                                _params.Add("@MonthTo", -1);
                                //Doc Status--------------------------------------
                                _params.Add("@DocStatus", "4");
                                _params.Add("@BranchID", bu.tbl_Branchs[0].BranchID);
                                _params.Add("@WHID", txtWHCode.Text);
                                //WHID--------------------------------------
                                //ProductSubGroupID--------------------------------------
                                _params.Add("@ProductSubGroupID", "");
                                //ProductSubGroupID--------------------------------------
                                //ProductID--------------------------------------
                                _params.Add("@ProductID", productID);
                                //ProductID--------------------------------------
                                _params.Add("@FromWH", txtWHCode.Text);
                                _params.Add("@ToWH", txtWHCode.Text);
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
