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
        string docTypeCode = "";
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
            var documentType = bu.GetDocumentType().FirstOrDefault(x => x.DocTypeCode == "RL");
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

            uoms.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
            //Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => uomList.Contains(x.ProductUomID));
            uoms.AddRange(bu.GetUOM()); // bu.GetUOM(tbl_ProductUomPre));

            //data gridview
            grdList.SetDataGridViewStyle();
            SetDefaultGridViewEvent(grdList);
        }

        private void AddNewRow(DataGridView grd, int rowIndex)
        {
            if (!validateNewRow)
            {
                return;
            }

            grd.Rows.Add(1);
            //InitRowData("", rowIndex);
            grd.InitRowData(this, initDataGridList, 1, "", rowIndex, uoms, bu, 1);

            ManageCheckAllProduct(grd);
        }

        public void BindRLData(string rlDocNo)
        {
            bu.GetDocData(rlDocNo);

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

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");

            grdList.CellContentClick -= grdList_CellContentClick;

            bool checkEditMode = bu.CheckExistsPR(rlDocNo);
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

            Func<tbl_BranchWarehouse, bool> bwhPredicate = (x => x.WHID == pr.ToWHID);
            var bwh = bu.GetBranchWarehouse(bwhPredicate);
            if (bwh != null)
            {
                txtWHCode.Text = pr.ToWHID;
                txtWHName.Text = bwh.WHName;
            }

            Func<tbl_Employee, bool> empPredicate = (x => x.EmpCode == pr.ToWHID);
            var emp = bu.GetEmployee(pr.RequestBy);
            if (emp != null)
            {
                txtEmpCode.Text = pr.RequestBy;
                txtEmpName.Text = string.Join(" ", emp.TitleName, emp.FirstName);
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

            txtComment.Text = pr.Comment;
        }

        private void BindPRDetail(List<tbl_PRDetail> prDts)
        {
            grdList.Rows.Clear();

            var allProduct = bu.GetProduct();

            if (chkShowAllProduct.Checked)
            {
                var _allProduct = new Product().GetDataTable();

                if (_allProduct != null && _allProduct.Rows.Count > 0)
                {
                    for (int i = 0; i < _allProduct.Rows.Count; i++)
                    {
                        AddNewRow(grdList, i);
                        //BindDataGrid(_allProduct, i, true);
                        grdList.BindDataGrid(dataGridList, initDataGridList, _allProduct, 1, i, validateNewRow, this, uoms, bu, 1, chkShowAllProduct.Checked);

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
                                    
                                    grdList.Rows[i].Cells[6].Value = prDts[j].SendQty;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                var allUOM = bu.GetUOM();

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

                    //BindComboBoxCell(grdList.Rows[i], i, false);
                    grdList.BindComboBoxCell(grdList.Rows[i], i, false, 4, uoms, this, bu, 1);

                    grdList.Rows[i].Cells[3].Value = productName;
                    grdList.Rows[i].Cells[4].Value = prDts[i].OrderUom; // allUOM.First(x => x.ProductUomID == prDts[i].OrderUom).ProductUomName;

                    Func<tbl_ProductUomSet, bool> predicate = (x => x.ProductID == prDts[i].ProductID && x.UomSetID == prDts[i].OrderUom);
                    var uomSet = bu.GetUOMSet(predicate);
                    if (uomSet != null && uomSet.Count > 0)
                    {
                        grdList.Rows[i].Cells[5].Value = "1*" + uomSet[0].BaseQty;
                    }
                    else
                    {
                        grdList.Rows[i].Cells[5].Value = "";
                    }

                    grdList.Rows[i].Cells[6].Value = prDts[i].SendQty;
                }
            }

            grdList.Columns[0].Visible = chkShowAllProduct.Checked;
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
            grdList.BindDataGrid(dataGridList, initDataGridList, productDT, 1, rowIndex, validateNewRow, this, uoms, bu, 1, chkShowAllProduct.Checked);
        }

        private void InitHeader()
        {
            this.BindData("FromBranchID", searchBranchControls, "503");

            var employee = bu.GetEmployee(Helper.tbl_Users.EmpID);
            txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName);

            dtpDocDate.SetDateTimePickerFormat();
            //txtComment.Text = "หมายเหตุ PR";

            var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
            ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            InitHeader();

            grdList.AutoGenerateColumns = false;
            validateNewRow = true;

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;

            grdList.Rows.Clear();

            if (chkShowAllProduct.Checked)
            {
                var allProduct = new Product().GetDataTable(false);
                if (allProduct != null && allProduct.Rows.Count > 0)
                {
                    for (int i = 0; i < allProduct.Rows.Count; i++)
                    {
                        AddNewRow(grdList, i);

                        grdList.CurrentCell = grdList.Rows[0].Cells[5];
                        grdList.BindDataGrid(dataGridList, initDataGridList, allProduct, 1, i, validateNewRow, this, uoms, bu, 1, chkShowAllProduct.Checked);
                    }
                }
            }
            else
            {
                AddNewRow(grdList, 0);
            }

            txtBranchCode.Focus();
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

        private void PreparePRMaster(bool editFlag = false)
        {
            bu.tbl_PRMaster = new tbl_PRMaster();

            var emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            var supp = bu.GetSupplier(txtBranchCode.Text);
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

            pr.RevisionNo = 0;
            pr.DocTypeCode = docTypeCode;
            pr.DocStatus = ddlDocStatus.SelectedValue.ToString();
            pr.DocDate = dtpDocDate.Value.ToDateTimeFormat();
            pr.DocRef = txtDocRef.Text;
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
        }

        private void PreparePRDetail(bool editFlag = false)
        {
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
                        var allPrdUOM = bu.GetUOM();// tbl_ProductUomPre);
                        var prdUOM = allPrdUOM.FirstOrDefault(x => x.ProductUomName == cell4.EditedFormattedValue.ToString());
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
            var prod = bu.GetProduct();
            var prodGroup = bu.GetProductGroup();
            var prodSubGroup = bu.GetProductSubGroup();

            DateTime crDate = DateTime.Now;

            foreach (var prDt in prDts)
            {
                for (int i = 0; i < 2; i++)
                {
                    var invMm = new tbl_InvMovement();

                    invMm.ProductID = prDt.ProductID;
                    invMm.ProductName = prDt.ProductName;
                    invMm.RefDocNo = prDt.DocNo;
                    invMm.TrnDate = crDate.ToDateTimeFormat();
                    invMm.TrnType = "T";
                    invMm.DocTypeCode = pr.DocTypeCode;

                    decimal unitQty = 0;
                    Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => x.BaseUomID == 2 && x.ProductID == prDt.ProductID);
                    var prdUOMSets = bu.GetUOMSet(tbl_ProductUomSetPre);
                    if (prdUOMSets != null && prdUOMSets.Count > 0)
                    {
                        if (prDt.OrderUom != 2)
                            unitQty = (prDt.ReceivedQty.Value * prdUOMSets[0].BaseQty);
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

                    var prodItem = prod.FirstOrDefault(x => x.ProductID == prDt.ProductID);
                    var prodGroupItem = prodGroup.FirstOrDefault(x => x.ProductGroupID == prodItem.ProductGroupID);
                    var prodSubGroupItem = prodSubGroup.FirstOrDefault(x => x.ProductSubGroupID == prodItem.ProductSubGroupID);

                    invMm.ProductGroupCode = prodGroupItem.ProductGroupCode;
                    invMm.ProductGroupName = prodGroupItem.ProductGroupName;
                    invMm.ProductSubGroupCode = prodSubGroupItem.ProductSubGroupCode;
                    invMm.ProductSubGroupName = prodSubGroupItem.ProductSubGroupName;
                    invMm.FlagSend = false;

                    invMms.Add(invMm);
                }
            }
        }

        private void PrepareQtyOnHand(tbl_InvWarehouse invWh, string productID, string whid, decimal unitQty)
        {
            var invWhItem = bu.GetInvWarehouse(productID, whid);
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

            SubPrepareInvWarehouse(pr.FromWHID, editFlag);
        }

        private void PrepareInvWarehouseTo(bool editFlag = false)
        {
            var pr = bu.tbl_PRMaster;

            SubPrepareInvWarehouse(pr.ToWHID, editFlag);
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
            var invWhs = bu.tbl_InvWarehouses;
            var prDts = bu.tbl_PRDetails;

            DateTime crDate = DateTime.Now;

            foreach (var prDt in prDts)
            {
                var invWh = new tbl_InvWarehouse();

                invWh.ProductID = prDt.ProductID;
                invWh.WHID = whid;
                invWh.QtyOnHand = 0;

                decimal unitQty = 0;
                Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => x.BaseUomID == 2 && x.ProductID == prDt.ProductID);
                var prdUOMSets = bu.GetUOMSet(tbl_ProductUomSetPre);
                if (prdUOMSets != null && prdUOMSets.Count > 0)
                {
                    if (prDt.OrderUom != 2)
                        unitQty = (prDt.ReceivedQty.Value * prdUOMSets[0].BaseQty);
                    else
                        unitQty = prDt.ReceivedQty.Value;

                    PrepareQtyOnHand(invWh, prDt.ProductID, whid, unitQty);
                }
                else
                {
                    unitQty = prDt.ReceivedQty.Value;

                    PrepareQtyOnHand(invWh, prDt.ProductID, whid, unitQty);
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

                    bu = new RL();

                    DateTime crDate = DateTime.Now;

                    docno = txdDocNo.Text;
                    editFlag = true;
                    bu.tbl_DocRunning = new List<tbl_DocRunning>();

                    bu.tbl_PRMaster = null;
                    bu.tbl_PRMaster = bu.GetPRMaster(docno);
                    bu.tbl_PRMaster.DocStatus = ddlDocStatus.SelectedValue.ToString();
                    bu.tbl_POMaster.EdDate = crDate;
                    bu.tbl_POMaster.EdUser = Helper.tbl_Users.Username;

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
                        SetWarehousesQTY(prDt, fromWHID, editFlag);
                        SetWarehousesQTY(prDt, toWHID, editFlag);
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

                    bu = new RL();

                    docno = bu.GenDocNo(docTypeCode);
                    editFlag = false;
                    bu.PrepareDocRunning(docTypeCode);

                    PreparePRMaster(editFlag);
                    PreparePRDetail(editFlag);
                    PrepareInvMovement(editFlag);
                    PrepareInvWarehouseFrom(editFlag);
                    PrepareInvWarehouseTo(editFlag);

                    ret = bu.RemovePRDetails();
                    if (ret == 0)
                    {
                        this.ShowProcessErr();
                        return;
                    }

                    ret = bu.RemoveInvMovements();
                    if (ret == 0)
                    {
                        this.ShowProcessErr();
                        return;
                    }

                    ret = bu.UpdateData();
                }

                if (ret == 1)
                {
                    this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, txdDocNo.Text);

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

        private void SetWarehousesQTY(tbl_PRDetail prDt, string whid, bool editFlag)
        {
            var invWh = new tbl_InvWarehouse();
            var invWhs = bu.GetInvWarehouse(prDt.ProductID, whid);
            invWh = invWhs[0];

            decimal unitQty = 0;
            Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => x.BaseUomID == 2 && x.ProductID == prDt.ProductID);
            var prdUOMSets = bu.GetUOMSet(tbl_ProductUomSetPre);
            if (prdUOMSets != null && prdUOMSets.Count > 0)
            {
                if (prDt.OrderUom != 2)
                    unitQty = (prDt.ReceivedQty.Value * prdUOMSets[0].BaseQty);
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

            if (dtpDocDate.Value != null && dtpDocDate.Value.Ticks < DateTime.Now.ToDateTimeFormat().Ticks) //validate date
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
                errList.SetErrMessageList(txtWHCode, lblWHCode);
                errList.SetErrMessageList(txtEmpCode, lblEmpCode);

                if (errList.Count == 0)
                {
                    var branch = bu.GetBranch();
                    if (branch == null)
                    {
                        string t = lblBranchCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtBranchCode.ErrorTextBox();
                    }

                    Func<tbl_BranchWarehouse, bool> branchWarehousePre = (x => x.VanType != 0 //x.WHID.Contains("V") 
                    && x.WHCode.ToLower() == txtWHCode.Text.ToLower());

                    var wh = bu.GetBranchWarehouse(branchWarehousePre);
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
                var allProduct = bu.GetProduct();
                var whid = txtBranchCode.Text + "1000";

                if (!chkShowAllProduct.Checked) //normal product
                {
                    ret = grdList.ValiadteDataGridView(allProduct, 1, 4, 6, 0, bu, whid, true);
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
            InitialData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, txdDocNo.Text);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");
            ddlDocStatus.Enabled = true;
            btnCancel.Enabled = true;

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

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            ddlDocStatus.Enabled = false;
            txdDocNo.Text = string.Empty;

            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == "4"; };
            ddlDocStatus.SelectedValueDropdownList(condition);
            ddlDocStatus.Enabled = false;

            validateNewRow = true;

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;

            ManageCheckAllProduct(grdList);
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

            validateNewRow = true;

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            btnCancel.EnableButton(btnSearchDoc);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;

            grdList.CellContentClick -= grdList_CellContentClick;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

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
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกสาขา/ซุ้ม");
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
            if (e.ColumnIndex == 1)
            {
                grdList.ModifyComboBoxCell(e.RowIndex, bu, 4, 1);
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
                    if (curentColIndex == 1)
                    {
                        if (cell1.IsNotNullOrEmptyCell())
                        {
                            validateNewRow = true;

                            //ValidateDuplicateSKU(cell1.EditedFormattedValue.ToString(), currentRowIndex);
                            grd.ValidateDuplicateSKU(cell1.EditedFormattedValue.ToString(), 1, currentRowIndex, ref validateNewRow);

                            //BindDataGridViewRow(grd, cell1.EditedFormattedValue.ToString(), currentRowIndex);
                            grd.BindDataGridViewRow(dataGridList, initDataGridList, dt, 1, cell1.EditedFormattedValue.ToString(), currentRowIndex, 1, ref validateNewRow, "RLProduct");
                        }

                        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 3];
                    }

                    else if (curentColIndex == 4)
                    {
                        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 2];
                    }
                    else if (curentColIndex == 6)
                    {
                        if (cell1.IsNotNullOrEmptyCell())
                        {
                            if ((grd.RowCount - 1) == currentRowIndex)
                            {
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

                var cell1 = grd.Rows[currentRowIndex].Cells[1];

                if (cell1.IsNotNullOrEmptyCell())
                {
                    if (e.ColumnIndex == 1)
                    {
                        //BindComboBoxCell(grd.Rows[currentRowIndex], currentRowIndex, false);
                        grdList.BindComboBoxCell(grd.Rows[currentRowIndex], currentRowIndex, false, 4, uoms, this, bu, 1);
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
            grdList.SetCellPainting(sender, e, 0);
        }

        private void grdList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            grdList.SetCellFormatting(sender, e, 0);
        }

        #endregion


    }
}
