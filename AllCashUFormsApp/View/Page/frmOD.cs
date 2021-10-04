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
    public partial class frmOD : Form
    {
        MenuBU menuBU = new MenuBU();
        OD bu = new OD();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();

        bool validateNewRow = true;
        string docTypeCode = "";
        int runDigit = 0;
        List<Control> searchSuppControls = new List<Control>();
        int[] cellEdit = new int[] { 0, 5, 6 };
        int[] numberCell = new int[] { 5, 6 };
        Dictionary<int, string> dataGridList = new Dictionary<int, string>();
        Dictionary<int, string> initDataGridList = new Dictionary<int, string>();
        List<tbl_Product> allProduct = new List<tbl_Product>();
        //List<tbl_ProductUomSet> allprdUOMSets = new List<tbl_ProductUomSet>();

        public frmOD()
        {
            InitializeComponent();

            searchSuppControls = new List<Control>() { txtSupplierCode, txtSuppName, txtContact, txtTelephone, nudCreditDay, dtpDocDate, dtpDueDate };

            dataGridList = new Dictionary<int, string>() { { 0, "ProductID" }, { 2, "ProductName" }, { 3, "UomSetName" }, { 4, "VatType" }, { 6, "UnitPrice" }, { 8, "UomSetID" } };
            initDataGridList = new Dictionary<int, string>() { { 0, "" }, { 2, "" }, { 3, "เลือก" }, { 4, "0" }, { 5, "0" }, { 6, "0.00" }, { 7, "0.00" }, { 8, "" } };

            txdDocNo.KeyDown += TxdDocNo_KeyDown;
            dtpDocDate.ValueChanged += DtpDocDate_ValueChanged;
            nudCreditDay.ValueChanged += NudCreditDay_ValueChanged;
            txtSupplierCode.KeyDown += TxtSupplierCode_KeyDown;

            //txtSupplierCode.SetSearchControl("Supplier", searchSuppControls);
        }

        #region private methods

        private void InitPage()
        {
            var documentType = bu.GetDocumentType().FirstOrDefault(x => x.DocTypeCode.Trim() == "OD");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                runDigit = documentType.DocFormat.Length - 2;
                FormHeader.Text = documentType.DocHeader;
                //FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");

                this.ClearControl(docTypeCode, runDigit);
            }

            validateNewRow = true;
            btnAdd.Enabled = true;

            this.OpenControl(false, new string[] { txtSuppName.Name, txtCrUser.Name, txtAddress.Name }, cellEdit);

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
            dtpDueDate.SetDateTimePickerFormat();

            //data gridview
            grdList.SetDataGridViewStyle();
            SetDefaultGridViewEvent(grdList);

            allProduct = bu.GetProduct();
            //allprdUOMSets = bu.GetUOMSet();
        }

        public void BindODData(string odDocNo)
        {
            bu.GetDocData(odDocNo, docTypeCode);

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

            this.OpenControl(false, new string[] { txtSuppName.Name, txtCrUser.Name, txtAddress.Name }, cellEdit);

            btnSearchDoc.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");

            grdList.CellContentClick -= grdList_CellContentClick;

            //CheckCancelDoc(po.DocStatus);
            bool checkEditMode = bu.CheckExistsPO(odDocNo);
            po.DocStatus.CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
        }

        private void BindPOMaster(tbl_POMaster po)
        {
            txdDocNo.Text = po.DocNo;

            dtpDocDate.Value = po.DocDate.ToDateTimeFormat();

            txtSupplierCode.Text = po.SupplierID;
            txtSuppName.Text = po.SuppName;

            txtContact.Text = po.ContactName;
            txtTelephone.Text = po.ContactTel;
            txtAddress.Text = po.Shipto;
            nudCreditDay.Value = po.CreditDay.Value;
            dtpDueDate.Value = po.DueDate.Value.ToDateTimeFormat();

            if (ddlDocStatus.DataSource == null)
            {
                var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
                ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
            }
            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == po.DocStatus; };
            ddlDocStatus.SelectedValueDropdownList(condition);

            var emp = bu.GetEmployeeByUserName(po.CrUser);
            txtCrUser.Text = string.Join(" ", emp.TitleName, emp.FirstName);

            txtRemark.Text = po.Remark;
            txtComment.Text = po.Comment;

            txnAmount.Text = po.Amount.Value.ToStringN2();
            txnExcVat.Text = po.ExcVat.Value.ToStringN2();
            txnIncVat.Text = po.IncVat.Value.ToStringN2();
            txnVatAmt.Text = po.VatAmt.Value.ToStringN2();
            txnTotalDue.Text = po.TotalDue.ToStringN2();
        }

        private void BindPODetail(List<tbl_PODetail> poDts)
        {
            grdList.Rows.Clear();

            var allUOM = bu.GetUOM();

            for (int i = 0; i < poDts.Count; i++)
            {
                grdList.Rows.Add(1);

                grdList.Rows[i].Cells[0].Value = poDts[i].ProductID;

                string productName = string.Empty;
                if (!string.IsNullOrEmpty(poDts[i].ProductName))
                {
                    productName = poDts[i].ProductName;
                }
                else
                {
                    var data = allProduct.FirstOrDefault(x => x.ProductID == poDts[i].ProductID);
                    if (data != null)
                    {
                        productName = data.ProductName;
                    }
                }

                grdList.Rows[i].Cells[2].Value = productName;
                grdList.Rows[i].Cells[3].Value = allUOM.First(x => x.ProductUomID == poDts[i].OrderUom).ProductUomName;
                grdList.Rows[i].Cells[4].Value = poDts[i].VatType;
                grdList.Rows[i].Cells[5].Value = poDts[i].OrderQty;
                grdList.Rows[i].Cells[6].Value = poDts[i].UnitPrice;
                grdList.Rows[i].Cells[7].Value = poDts[i].LineTotal;
                grdList.Rows[i].Cells[8].Value = poDts[i].OrderUom;
            }
        }

        public void BindSearchProduct(DataTable productDT, int rowIndex)
        {
            if (!string.IsNullOrEmpty(productDT.Rows[0]["ProductID"].ToString()))
            {
                if (!string.IsNullOrEmpty(grdList.Rows[rowIndex].Cells[0].EditedFormattedValue.ToString()))
                {
                    var cell5 = grdList.Rows[rowIndex].Cells[5];
                    if (cell5.IsNotNullOrEmptyCell())
                    {
                        CalculateRow(grdList, rowIndex);
                    }
                }
            }

            validateNewRow = true;
            grdList.ValidateDuplicateSKU(productDT.Rows[0]["ProductID"].ToString(), 0, rowIndex, ref validateNewRow);

            //(this DataGridView grdList, Dictionary<int, string> dataGridList, Dictionary<int, string> initDataGridList, DataTable _dt, int idIndex, int rowIndex, bool validateNewRow)
            grdList.BindDataGrid(dataGridList, initDataGridList, productDT, 0, rowIndex, validateNewRow);
            //BindDataGrid(productDT, rowIndex);
        }

        private void CalculateRow(DataGridView grd, int rowIndex)
        {
            decimal orderQty = 0;
            decimal unitPrice = 0;

            var cell5 = grd.Rows[rowIndex].Cells[5];
            var cell6 = grd.Rows[rowIndex].Cells[6];
            if (cell5.IsNotNullOrEmptyCell())
            {
                orderQty = Convert.ToDecimal(cell5.EditedFormattedValue);
            }
            if (cell6.IsNotNullOrEmptyCell())
            {
                unitPrice = Convert.ToDecimal(cell6.EditedFormattedValue);
            }

            grd.Rows[rowIndex].Cells[7].Value = (orderQty * unitPrice).ToDecimalN2().ToStringN2();

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            decimal amount = 0;
            decimal excVat = 0;
            decimal incVat = 0;
            decimal vatAmt = 0;
            decimal totalDue = 0;
            decimal vatRate = 0;

            for (int i = 0; i < grdList.RowCount; i++)
            {
                var vatCell = grdList.Rows[i].Cells[4];
                var lineTotalCell = grdList.Rows[i].Cells[7];

                if (lineTotalCell.IsNotNullOrEmptyCell())
                {
                    amount += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                }
                if (vatCell.IsNotNullOrEmptyCell())
                {
                    decimal _vateRate = Convert.ToDecimal(vatCell.EditedFormattedValue);
                    if (_vateRate > 0) //have VAT
                    {
                        incVat += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                        vatRate = _vateRate;
                    }
                    else //No VAT
                    {
                        excVat += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                    }
                }
            }

            excVat = excVat.ToDecimalN2();

            incVat = incVat.ToDecimalN2(); //(amount - excVat).ToDecimalN2();

            vatAmt = (incVat * (vatRate / 100.00m)).ToDecimalN2();

            totalDue = (amount + vatAmt).ToDecimalN2();

            txnAmount.Text = amount.ToDecimalN2().ToStringN2();
            txnExcVat.Text = excVat.ToDecimalN2().ToStringN2();
            txnIncVat.Text = incVat.ToStringN2();
            txnVatAmt.Text = vatAmt.ToStringN2();
            txnTotalDue.Text = totalDue.ToStringN2();
        }

        private void InitHeader()
        {
            var company = bu.GetCompany();
            txtAddress.Text = company.Address;

            var employee = bu.GetEmployee(Helper.tbl_Users.EmpID);
            txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName);

            dtpDocDate.SetDateTimePickerFormat();
            dtpDueDate.SetDateTimePickerFormat();

            var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
            ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            this.OpenControl(true, new string[] { txtSuppName.Name, txtCrUser.Name, txtAddress.Name }, cellEdit);

            InitHeader();

            txdDocNo.ReadOnly = false;
            grdList.AutoGenerateColumns = false;
            validateNewRow = true;

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;

            grdList.Rows.Clear();
            //AddNewRow(grdList, 0);
            grdList.AddNewRow(initDataGridList, 0, "", 0, validateNewRow);

            txtSupplierCode.Focus();
        }

        private string PreparePOMaster(bool editFlag = false)
        {
            string docNo = string.Empty;

            bu.tbl_POMaster = new tbl_POMaster();

            var comp = bu.GetCompany();
            var emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            var supp = bu.GetSupplier(txtSupplierCode.Text);

            var po = bu.tbl_POMaster;
            bool checkEditMode = bu.CheckExistsPO(txdDocNo.Text);
            if (checkEditMode)
            {
                po.DocNo = txdDocNo.Text;
            }
            else
            {
                po.DocNo = bu.GenDocNo(docTypeCode);
            }

            docNo = po.DocNo;
            po.RevisionNo = 0;
            po.DocTypeCode = docTypeCode;
            po.DocStatus = ddlDocStatus.SelectedValue.ToString();
            po.DocDate = dtpDocDate.Value.ToDateTimeFormat();
            po.DocRef = "";
            po.StatusInOut = null;
            po.SupplierID = txtSupplierCode.Text;
            po.SuppName = txtSuppName.Text;
            po.WHID = comp.WHID;
            po.EmpID = emp.EmpID;
            po.SaleEmpID = "0";
            po.SalAreaID = "0";
            po.Address = supp.BillTo;
            po.ContactName = txtContact.Text;
            po.ContactTel = txtTelephone.Text;
            po.Shipto = txtAddress.Text;
            po.CreditDay = Convert.ToInt16(nudCreditDay.Value);
            po.DueDate = dtpDueDate.Value.ToDateTimeFormat();
            po.CustomerID = "0";
            po.CustType = "";
            po.CustName = "";
            po.CustInvNO = "";
            po.CustInvDate = null;
            po.CustPODate = null;
            po.CustPONo = "";
            po.Remark = txtRemark.Text;
            po.Comment = txtComment.Text;
            po.OldAmount = 0.00m;
            po.Amount = Convert.ToDecimal(txnAmount.Text);
            po.OldExcVat = 0.00m;
            po.ExcVat = Convert.ToDecimal(txnExcVat.Text);
            po.OldIncVat = 0.00m;
            po.IncVat = Convert.ToDecimal(txnIncVat.Text);

            decimal _vatRate = 0;
            for (int i = 0; i < grdList.RowCount; i++)
            {
                if (Convert.ToDecimal(grdList.Rows[i].Cells[4].EditedFormattedValue.ToString()) > 0)
                {
                    _vatRate = Convert.ToDecimal(grdList.Rows[i].Cells[4].EditedFormattedValue);
                    break;
                }
            }

            po.VatRate = _vatRate;

            po.VatAmt = Convert.ToDecimal(txnVatAmt.Text);
            po.Freight = 0.00m;
            po.DiscountType = "";
            po.OldDiscount = null;
            po.Discount = 0.00m;
            po.TotalDue = Convert.ToDecimal(txnTotalDue.Text);
            po.ApprovedBy = null;
            po.ApprovedDate = null;
            po.PayType = 0;
            po.CrDate = DateTime.Now;
            po.CrUser = Helper.tbl_Users.Username;

            if (editFlag)
            {
                po.EdDate = DateTime.Now;
                po.EdUser = Helper.tbl_Users.Username;
            } 

            po.FlagDel = false;
            po.FlagSend = false;
            po.OldTotalCom = 0.00m;
            po.TotalCom = 0.00m;
            po.CNType = 0;
            po.DiscountRate = 0.00m;

            return docNo;
        }

        private void PreparePODetail(bool editFlag = false)
        {
            bu.tbl_PODetails.Clear();

            var poDts = bu.tbl_PODetails;
            DateTime crDate = DateTime.Now;

            for (int i = 0; i < grdList.RowCount; i++)
            {
                var poDt = new tbl_PODetail();

                var cell0 = grdList.Rows[i].Cells[0];
                var cell2 = grdList.Rows[i].Cells[2];
                var cell4 = grdList.Rows[i].Cells[4];
                var cell5 = grdList.Rows[i].Cells[5];
                var cell6 = grdList.Rows[i].Cells[6];
                var cell7 = grdList.Rows[i].Cells[7];
                var cell8 = grdList.Rows[i].Cells[8];

                if (cell0.IsNotNullOrEmptyCell() && cell2.IsNotNullOrEmptyCell())
                {
                    poDt.DocNo = bu.tbl_POMaster.DocNo;
                    poDt.Line = Convert.ToInt16(i);
                    poDt.ProductID = cell0.EditedFormattedValue.ToString();
                    poDt.ProductName = cell2.EditedFormattedValue.ToString();
                    poDt.OrderUom = Convert.ToInt32(cell8.EditedFormattedValue);
                    poDt.OrderQty = Convert.ToDecimal(cell5.EditedFormattedValue);
                    poDt.ReceivedQty = Convert.ToDecimal(cell5.EditedFormattedValue);
                    poDt.RejectedQty = 0;
                    poDt.StockedQty = 0;
                    poDt.UnitCost = 0;
                    poDt.UnitPrice = Convert.ToDecimal(cell6.EditedFormattedValue);
                    poDt.VatType = Convert.ToByte(cell4.EditedFormattedValue);
                    poDt.LineDiscountType = "N";
                    poDt.LineDiscountRate = 0;
                    poDt.LineDiscount = 0;
                    poDt.LineTotal = Convert.ToDecimal(cell7.EditedFormattedValue);
                    poDt.CustType = "";
                    poDt.CrDate = crDate;
                    poDt.CrUser = Helper.tbl_Users.Username;

                    if (editFlag)
                    {
                        poDt.EdDate = crDate;
                        poDt.EdUser = Helper.tbl_Users.Username;
                    }

                    poDt.FlagDel = false;
                    poDt.FlagSend = false;
                    poDt.UnitComPrice = 0;
                    poDt.LineComTotal = 0;
                    poDt.LineRemark = "";
                    poDt.FreeQty = 0;
                    poDt.FreeUom = 0;
                    poDt.FreeUnit = 0;

                    poDts.Add(poDt);
                }
            }
        }

        private void PrepareInvMovement(bool editFlag = false)
        {
            bu.tbl_InvMovements.Clear();

            var invMms = bu.tbl_InvMovements;
            var poDts = bu.tbl_PODetails;
            var po = bu.tbl_POMaster;
            var prod = bu.GetProduct();
            var prodGroup = bu.GetProductGroup();
            var prodSubGroup = bu.GetProductSubGroup();

            DateTime crDate = DateTime.Now;

            foreach (var poDt in poDts)
            {
                var invMm = new tbl_InvMovement();

                invMm.ProductID = poDt.ProductID;
                invMm.ProductName = poDt.ProductName;
                invMm.RefDocNo = poDt.DocNo;
                invMm.TrnDate = crDate.ToDateTimeFormat();
                invMm.TrnType = "";
                invMm.DocTypeCode = po.DocTypeCode;
                invMm.WHID = po.WHID;
                invMm.FromWHID = "";
                invMm.ToWHID = "";
                invMm.TrnQtyIn = 0;
                invMm.TrnQtyOut = 0;
                invMm.TrnQty = 0;
                invMm.CrDate = crDate;

                if (editFlag)
                {
                    invMm.EdDate = crDate;
                    invMm.TrnType = ddlDocStatus.SelectedValue.ToString() == "5" ? "X" : "";
                }

                var prodItem = prod.FirstOrDefault(x => x.ProductID == poDt.ProductID);
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

        private void PrepareInvWarehouse(bool editFlag = false)
        {
            bu.tbl_InvWarehouses.Clear();

            var invWhs = bu.tbl_InvWarehouses;
            var poDts = bu.tbl_PODetails;
            var po = bu.tbl_POMaster;
            var allInvWhs = bu.GetInvWarehouse();

            DateTime crDate = DateTime.Now;

            foreach (var poDt in poDts)
            {
                var invWh = new tbl_InvWarehouse();

                invWh.ProductID = poDt.ProductID;
                invWh.WHID = po.WHID;
                invWh.QtyOnHand = 0;

                var invWhItem = allInvWhs.Where(x => x.ProductID == poDt.ProductID && x.WHID == po.WHID).ToList(); //bu.GetInvWarehouse(poDt.ProductID, po.WHID); //allInvWhs.FirstOrDefault(x => x.ProductID == poDt.ProductID && x.WHID == po.WHID);
                if (invWhItem != null && invWhItem.Count > 0)
                {
                    invWh.QtyOnHand = invWhItem[0].QtyOnHand;
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
                if (!ValidateSave())
                    return;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                string docno = string.Empty;
                bool editFlag = true;
                int ret = 0;

                bool checkEditMode = bu.CheckExistsPO(txdDocNo.Text);
                if (checkEditMode)
                {
                    bu = new OD();

                    DateTime crDate = DateTime.Now;

                    //docno = txdDocNo.Text;
                    editFlag = true;
                    bu.tbl_DocRunning = new List<tbl_DocRunning>();

                    bu.tbl_POMaster = null;
                    //bu.tbl_POMaster = bu.GetPOMaster(docno);
                    docno = PreparePOMaster(editFlag);

                    //validate docno by sailom.k 27-05-2021
                    if (bu.tbl_POMaster.DocNo.Length < 11)
                    {
                        this.ShowDocNoProcessErr();
                        return;
                    }

                    bu.tbl_POMaster.DocStatus = ddlDocStatus.SelectedValue.ToString();
                    bu.tbl_POMaster.EdDate = crDate;
                    bu.tbl_POMaster.EdUser = Helper.tbl_Users.Username;

                    bu.tbl_PODetails.Clear();
                    //List<tbl_PODetail> tbl_PODetails = bu.GetPODetails(docno);
                    List<tbl_PODetail> tbl_PODetails = bu.tbl_PODetails;
                    PreparePODetail(editFlag);

                    ret = bu.RemovePODetails(bu.tbl_POMaster.DocNo); //remove old po details befor save edit by sailom 10-06-2021
                    if (ret == 1)
                    {
                        tbl_PODetails.ForEach(x =>
                        {
                            x.EdUser = Helper.tbl_Users.Username; x.EdDate = crDate;
                        });
                        //bu.tbl_PODetails.AddRange(tbl_PODetails); //edit by sailom 10-06-2021

                        bu.tbl_InvMovements.Clear();
                        List<tbl_InvMovement> tbl_InvMovements = bu.GetInvMovement(docno);
                        tbl_InvMovements.ForEach(x =>
                        {
                            x.EdDate = crDate;

                            if (ddlDocStatus.SelectedValue.ToString() == "5")
                                x.TrnType = "X";
                        });
                        bu.tbl_InvMovements.AddRange(tbl_InvMovements);

                        //bu.tbl_InvTransactions.Clear();
                        //List<tbl_InvTransaction> tbl_InvTransactions = bu.GetInvTransaction(docno);
                        //tbl_InvTransactions.ForEach(x =>
                        //{
                        //    x.EdUser = Helper.tbl_Users.Username; x.EdDate = crDate;
                        //});
                        //bu.tbl_InvTransactions.AddRange(tbl_InvTransactions);

                        bu.tbl_InvWarehouses.Clear();
                        var allInvWhs = bu.GetInvWarehouse();

                        foreach (var poDt in bu.tbl_PODetails)
                        {
                            List<tbl_InvWarehouse> tbl_InvWarehouses = allInvWhs.Where(x => x.ProductID == poDt.ProductID && x.WHID == bu.tbl_POMaster.WHID).ToList();
                            tbl_InvWarehouses.ForEach(x =>
                            {
                                x.EdUser = Helper.tbl_Users.Username; x.EdDate = crDate;
                            });
                            bu.tbl_InvWarehouses.AddRange(tbl_InvWarehouses);
                        }

                        ret = bu.UpdateData();
                    }
                }
                else
                {
                    bu = new OD();

                    //docno = bu.GenDocNo(docTypeCode);
                    editFlag = false;
                    bu.PrepareDocRunning(docTypeCode);

                    docno = PreparePOMaster(editFlag);

                    //validate docno by sailom.k 27-05-2021
                    if (bu.tbl_POMaster.DocNo.Length < 11)
                    {
                        this.ShowDocNoProcessErr();
                        return;
                    }

                    PreparePODetail(editFlag);
                    PrepareInvMovement(editFlag);
                    PrepareInvWarehouse();

                    //ret = bu.RemovePODetails();
                    //if (ret == 0)
                    //{
                    //    this.ShowProcessErr();
                    //    return;
                    //}

                    //ret = bu.RemoveInvMovements();
                    //if (ret == 0)
                    //{
                    //    this.ShowProcessErr();
                    //    return;
                    //}

                    ret = bu.UpdateData(docTypeCode);
                }

                if (ret == 1)
                {
                    this.OpenControl(false, new string[] { txtSuppName.Name, txtCrUser.Name, txtAddress.Name }, cellEdit);

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, txdDocNo.Text);

                    txdDocNo.Text = docno;

                    grdList.CellContentClick -= grdList_CellContentClick;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    ddlDocStatus.SelectedValue.ToString().CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
                    //CheckCancelDoc(ddlDocStatus.SelectedValue.ToString());
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

            if (ret)
            {
                errList.SetErrMessageList(txtSupplierCode, lblSupplierCode);
                errList.SetErrMessageList(txtAddress, lblAddress);
                errList.SetErrMessageList(txtContact, lblContact);
                errList.SetErrMessageList(txtTelephone, lblTelephone);

                if (errList.Count == 0)
                {
                    var sup = bu.GetSupplier(txtSupplierCode.Text);
                    if (sup == null)
                    {
                        string t = lblSupplierCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtSupplierCode.ErrorTextBox();
                    }
                }

                if (errList.Count > 0) //validate header
                {
                    string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                    message.ShowWarningMessage();
                    ret = false;
                }
                else //validate data grid
                {
                    ret = grdList.ValiadteDataGridView(allProduct, 0, 3, 5, 6, bu, "", false);
                }
            }

            return ret;
        }

        #endregion

        #region event methods

        private void TxtSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtSupplierCode.SetSearchControl("Supplier", searchSuppControls);
                TextBox txt = (TextBox)sender;
                this.BindData("Supplier", searchSuppControls, txt.Text);
            }
        }

        private void NudCreditDay_ValueChanged(object sender, EventArgs e)
        {
            nudCreditDay.CalcDueDate(dtpDocDate, dtpDueDate);
        }

        private void DtpDocDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDocDate.CalcDueDate(dtpDueDate, nudCreditDay);
        }

        private void TxdDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindODData(txdDocNo.Text);
            }
        }

        private void frmOD_Load(object sender, EventArgs e)
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

            this.OpenControl(true, new string[] { txtSuppName.Name, txtCrUser.Name, txtAddress.Name }, cellEdit);

            ddlDocStatus.Enabled = true;
            validateNewRow = true;
            btnCancel.Enabled = true;
            txdDocNo.ReadOnly = true;

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;

            txtSupplierCode.Focus();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            btnCopy.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnPrint, txdDocNo.Text);

            this.OpenControl(true, new string[] { txtSuppName.Name, txtCrUser.Name, txtAddress.Name }, cellEdit);

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

            validateNewRow = true;

            this.OpenControl(false, new string[] { txtSuppName.Name, txtCrUser.Name, txtAddress.Name }, cellEdit);

            btnCancel.EnableButton(btnSearchDoc);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;

            grdList.CellContentClick -= grdList_CellContentClick;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@DocNo", txdDocNo.Text);
            this.OpenCrystalReportsPopup("ใบสั่งสินค้า", "Form_OD.rpt", "Form_OD", _params);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchSupp_Click(object sender, EventArgs e)
        {
            this.OpenSupplierPopup(searchSuppControls, "เลือกผู้จำหน่าย");
        }

        private void btnSearchDoc_Click(object sender, EventArgs e)
        {
            this.OpenDocPopup("ใบสั่งสินค้า", docTypeCode);
        }

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grdList.SetCellContentClick(this, sender, e, "ODProduct", 5);
        }

        private void grdList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
            tb.PreviewKeyDown -= DataGridView_PreviewKeyDown;
            tb.PreviewKeyDown += DataGridView_PreviewKeyDown;

            tb.KeyPress -= DataGridView_KeyPress;
            tb.KeyPress += DataGridView_KeyPress;
        }

        private void DataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            grdList.SetCellNumberOnly(sender, e, numberCell.ToList());
        }

        private void DataGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            DataGridViewTextBoxEditingControl _grd = (DataGridViewTextBoxEditingControl)sender;

            DataGridView grd = _grd.EditingControlDataGridView;
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
                            grd.BindDataGridViewRow(dataGridList, initDataGridList, dt, 0, cell0.EditedFormattedValue.ToString(), currentRowIndex, 0, ref validateNewRow, "ODProduct", false);
                    }

                    if (isNewRow)
                        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 5];
                }
                else if (curentColIndex == 5)
                {
                    grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 1];
                }
                else if (curentColIndex == 6)
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

                            grdList.AddNewRow(initDataGridList, 0, "", currentRowIndex + 1, validateNewRow);

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
                    else
                    {
                        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[7];
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
                    if (curentColIndex == 0 || curentColIndex == 5 || curentColIndex == 6)
                    {
                        var cell5 = grd.Rows[grd.CurrentRow.Index].Cells[5];
                        if (cell5.IsNotNullOrEmptyCell())
                        {
                            CalculateRow(grd, currentRowIndex);
                        }
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
                    //edit by sailom 10-06-2021
                    foreach (DataGridViewRow rowselected in _grd.SelectedRows)
                    {
                        int rowIndex = rowselected.Index;
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

                    //int rowIndex = _grd.CurrentRow.Index;
                    //if (_grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "White" || _grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "0")
                    //{
                    //    _grd.Rows.RemoveAt(rowIndex);

                    //    if (rowIndex <= _grd.RowCount - 1)
                    //    {
                    //        CalculateRow(_grd, rowIndex);
                    //    }
                    //}
                    //else
                    //{
                    //    return;
                    //}
                }
            }
        }

        private void grdList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            grdList.SetCellClick(sender, e, cellEdit, 0);
        }

        #endregion

        private void frmOD_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
