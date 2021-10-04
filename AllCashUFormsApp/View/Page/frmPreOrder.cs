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
    public partial class frmPreOrder : Form
    {
        MenuBU menuBU = new MenuBU();
        PreOrder preBu = new PreOrder();
        TabletSales bu = new TabletSales();
        Promotion pro = new Promotion();
        static PromotionTemp proTmp = new PromotionTemp();
        static string rlDocNo = "";
        public static DateTime sendDate;

        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        List<tbl_ProductUom> uoms = new List<tbl_ProductUom>();
        List<tbl_SalArea> saleAreaList = new List<tbl_SalArea>();
        List<tbl_SalAreaDistrict> salAreaDistrictList = new List<tbl_SalAreaDistrict>();
        Dictionary<string, decimal> listDiscount = new Dictionary<string, decimal>();

        bool validateNewRow = true;
        string docTypeCode = "";
        int runDigit = 0;

        List<Control> searchCustControls = new List<Control>();
        List<Control> searchPromotionControls = new List<Control>();
        List<Control> searchBWHControls = new List<Control>();
        List<Control> searchMstBWHControls = new List<Control>();
        List<Control> searchPOBWHControls = new List<Control>();
        List<Control> searchEmpControls = new List<Control>();
        List<string> readOnlyControls = new List<string>();

        int[] cellEdit = new int[] { 0, 1, 3, 4, 5 }; // new int[] { 0, 3, 4, 5, 7, 8 };
        int[] numberCell = new int[] { 4, 5 };

        Dictionary<int, string> dataGridList = new Dictionary<int, string>();
        Dictionary<int, string> initDataGridList = new Dictionary<int, string>();

        Func<tbl_BranchWarehouse, bool> whFunc = (x => x.VanType == 3); // x.WHID.Contains("V"));
        Func<tbl_Employee, bool> empFunc = (x => x.PositionID == 4);
        List<tbl_Product> allProduct = new List<tbl_Product>();
        List<tbl_ProductUomSet> allUomSet = new List<tbl_ProductUomSet>();
        List<tbl_ProductUom> allUOM = new List<tbl_ProductUom>();
        List<tbl_ProductPriceGroup> allProductPrice = new List<tbl_ProductPriceGroup>();
        List<tbl_ProductGroup> allProdGroup = new List<tbl_ProductGroup>();
        List<tbl_ProductSubGroup> allProdSubGroup = new List<tbl_ProductSubGroup>();
        static bool isConfirmFTB = false;

        private ContextMenuStrip printContextMenuStrip;

        public frmPreOrder()
        {
            InitializeComponent();

            searchCustControls = new List<Control> { txtCustomerCode, txtCustName, txtBillTo, txtContact, txtTelephone };
            searchBWHControls = new List<Control> { txtWHCode, txtWHName };
            searchMstBWHControls = new List<Control> { txtMstWHCode, txtMstWHName };
            searchPOBWHControls = new List<Control> { txtWHCodePO, txtWHNamePO };
            searchEmpControls = new List<Control> { txtEmpCode };
            readOnlyControls = new List<string>() { txtCustName.Name, txtMstWHName.Name, txtEmpCode.Name, txtCrUser.Name };

            dataGridList = new Dictionary<int, string>() { { 0, "ProductID" }, { 2, "ProductName" }, { 3, "UOMSetID" }, { 5, "SellPriceVat" }, { 6, "VatType" }, { 11, "SellPrice" } };
            initDataGridList = new Dictionary<int, string>() { { 0, "" }, { 2, "" }, { 3, "combobox" }, { 4, "0" }, { 5, "0.00" }, { 6, "0" }, { 7, "N" }, { 8, "0.00" }, { 9, "0.00" }, { 10, "" }, { 11, "0.00" } };

            txdDocNo.KeyDown += TxdDocNo_KeyDown;
            txtDocNoPOMst.KeyDown += TxtDocNo_KeyDown;
            txtDocNoPO.KeyDown += TxtDocNoPO_KeyDown;
            txtMstWHCode.KeyDown += TxtMstWHCode_KeyDown;
            txtMstWHCode.TextChanged += TxtMstWHCode_TextChanged;
            txtWHCode.KeyDown += TxtWHCode_KeyDown;
            txtWHCode.TextChanged += TxtWHCode_TextChanged;
            txtWHCodePO.KeyDown += TxtWHCodePO_KeyDown;
            txtWHCodePO.TextChanged += TxtWHCodePO_TextChanged;
            txtCustomerCode.KeyDown += TxtCustomerCode_KeyDown;

            dtpDocDate.ValueChanged += DtpDocDate_ValueChanged;
            nudCreditDay.ValueChanged += NudCreditDay_ValueChanged;
        }

        private void CreatePrintBtnList()
        {
            printContextMenuStrip = new ContextMenuStrip();

            printContextMenuStrip.Items.Clear();
            printContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(cms_Opening);

            btnPrint.ContextMenuStrip = printContextMenuStrip;
        }

        void cms_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Acquire references to the owning control and item.
            Control c = printContextMenuStrip.SourceControl as Control;
            ToolStripDropDownItem tsi = printContextMenuStrip.OwnerItem as ToolStripDropDownItem;

            // Clear the ContextMenuStrip control's Items collection.
            printContextMenuStrip.Items.Clear();

            // Populate the ContextMenuStrip control with its default items.
            var printImage = new Bitmap(AllCashUFormsApp.Properties.Resources.printBtn).ImageToByte();

            List<tbl_MstMenu> menuList = new List<tbl_MstMenu>();
            tbl_MstMenu m = new tbl_MstMenu();
            m.MenuID = 100;
            m.MenuName = "PreRL";
            m.MenuText = "พิมพ์ใบจัดสินค้า(RL)";
            m.FormName = "frmCrystalReport";
            m.MenuImage = printImage;
            menuList.Add(m);

            m = new tbl_MstMenu();
            m.MenuID = 101;
            m.MenuName = "PreCtrl";
            m.MenuText = "พิมพ์ใบคุมส่งสินค้าตามคลังรถ";
            m.FormName = "frmCrystalReport";
            m.MenuImage = printImage;
            menuList.Add(m);

            m = new tbl_MstMenu();
            m.MenuID = 102;
            m.MenuName = "PrePO";
            m.MenuText = "พิมพ์ใบกำกับภาษีอย่างย่อ";
            m.FormName = "frmCrystalReport";
            m.MenuImage = printImage;
            menuList.Add(m);

            foreach (var item in menuList)
            {
                printContextMenuStrip.Items.Add(item.MenuText, item.MenuImage.byteArrayToImage(), ToolStripMenuItem_Click);
            }

            // Set Cancel to false. 
            // It is optimized to true based on empty entry.
            e.Cancel = false;
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> _params = new Dictionary<string, object>();

            string frmText = ((System.Windows.Forms.ToolStripItem)sender).Text;
            if (frmText == "พิมพ์ใบจัดสินค้า(RL)")
            {
                if (grdPO.RowCount > 0)
                {
                    //string rlDocNo = "";
                    //rlDocNo = grdPO.Rows[grdPO.CurrentRow.Index].Cells["colRLDocNo"].Value.ToString();

                    //if (!string.IsNullOrEmpty(rlDocNo))
                    //{
                    //    _params = new Dictionary<string, object>();

                    Dictionary<string, string> rlList = new Dictionary<string, string>();
                    foreach (DataGridViewRow row in grdPO.Rows)
                    {
                        string _rlDocNo = "";
                        string _whName = "";
                        _rlDocNo = row.Cells["colRLDocNo"].Value.ToString();
                        _whName = row.Cells["colWHNamePO"].Value.ToString();

                        if (!string.IsNullOrEmpty(_rlDocNo) && !string.IsNullOrEmpty(_whName))
                        {
                            if (rlList.Count(x => x.Key == _rlDocNo) == 0)
                            {
                                rlList.Add(_rlDocNo, _whName);
                            }
                        }
                    }

                    foreach (var item in rlList)
                    {
                        string rlDocNo = "";
                        string whName = "";
                        rlDocNo = item.Key;// grdPO.Rows[grdPO.CurrentRow.Index].Cells["colRLDocNo"].Value.ToString();
                        whName = item.Value;

                        _params = new Dictionary<string, object>();
                        _params.Add("@DocNo", rlDocNo);
                        this.OpenCrystalReportsPopup("ใบจัดสินค้า(RL)", "Form_RL.rpt", "Form_RL", _params);
                    }
                }
            }
            else if (frmText == "พิมพ์ใบคุมส่งสินค้าตามคลังรถ")
            {
                if (grdPO.RowCount > 0)
                {
                    Dictionary<string, string> rlList = new Dictionary<string, string>();
                    foreach (DataGridViewRow row in grdPO.Rows)
                    {
                        string _rlDocNo = "";
                        string _whName = "";
                        _rlDocNo = row.Cells["colRLDocNo"].Value.ToString();
                        _whName = row.Cells["colWHNamePO"].Value.ToString();

                        if (!string.IsNullOrEmpty(_rlDocNo) && !string.IsNullOrEmpty(_whName))
                        {
                            if (rlList.Count(x => x.Key == _rlDocNo) == 0)
                            {
                                rlList.Add(_rlDocNo, _whName);
                            }
                        }
                    }

                    foreach (var item in rlList)
                    {
                        string rlDocNo = "";
                        string whName = "";
                        rlDocNo = item.Key;// grdPO.Rows[grdPO.CurrentRow.Index].Cells["colRLDocNo"].Value.ToString();
                        whName = item.Value;

                        _params = new Dictionary<string, object>();
                        _params.Add("@DocNo", rlDocNo);
                        _params.Add("@WHName", whName);
                        _params.Add("@DocDateFr", dtpDocDatePO.Value);
                        _params.Add("@DocDateTo", dtpDocDatePO.Value);

                        this.OpenCrystalReportsPopup("ใบคุมส่งสินค้าตามคลังรถ", "Rep_PreOrder_Cust_Ctrl.rpt", "proc_PreOrder_Cust_Ctrl_Report", _params);
                    }
                }
            }
            else if (frmText == "พิมพ์ใบกำกับภาษีอย่างย่อ")
            {
                _params = new Dictionary<string, object>();
                if (grdPO.RowCount > 0)
                {
                    string poDocNo = "";
                    poDocNo = grdPO.Rows[grdPO.CurrentRow.Index].Cells["colDocNoPO"].Value.ToString();

                    if (!string.IsNullOrEmpty(poDocNo))
                    {
                        _params.Add("@DocNo", poDocNo);
                        this.OpenCrystalReportsPopup("ใบกำกับภาษีอย่างย่อ", "Form_IV.rpt", "Form_IV", _params);
                    }
                }
            }
        }

        public void SetDefaultDate(DateTime _sendDate)
        {
            sendDate = _sendDate;
            isConfirmFTB = true;
        }

        #region private methods

        private void InitPage()
        {
            var menu = bu.GetAllFromMenu().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }

            var headerPic = menuBU.GetAllData().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            var documentType = bu.GetDocumentType().FirstOrDefault(x => x.DocTypeCode.Trim() == "IV");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                runDigit = documentType.DocFormat.Length - 2;

                this.ClearControl(docTypeCode, runDigit);
                txtComment.Text = documentType.DocRemark;
            }

            //this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            validateNewRow = true;

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit); ////////////////////

            InitHeader();

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            dtpSDocDate.Enabled = true;
            btnSearchPOMst.Enabled = true;
            btnUpdateAddress.Enabled = false;
            btnGenCustIVNo.Enabled = false;
            btnCreatePO.Enabled = true;
            dtpStockDate.Enabled = false;
            btnSearchStock.Enabled = true;
            rdoCurrent.Enabled = true;
            rdoAtDate.Enabled = true;
            btnAdd.Enabled = false;

            //header control
            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;
            //btnSearchDoc.Enabled = true;
            //txtMstWHCode.Enabled = true;
            //btnWHCode.Enabled = true;

            dtpDocDate.SetDateTimePickerFormat();
            dtpDueDate.SetDateTimePickerFormat();

            allUOM = bu.GetUOM();

            uoms.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
            uoms.AddRange(allUOM);

            saleAreaList.AddRange(bu.GetAllSaleArea());
            salAreaDistrictList.AddRange(bu.GetAllSaleAreaDistrict());

            grdStock.SetDataGridViewStyle();
            SetDefaultGridViewEventStock(grdStock);

            grdList.AutoGenerateColumns = false;
            grdList.SetDataGridViewStyle();
            SetDefaultGridViewEvent(grdList);

            grdPOMst.SetDataGridViewStyle();
            grdPO.SetDataGridViewStyle();

            SetDefaultGridViewEventMst(grdPOMst);
            SetDefaultGridViewEventPO(grdPO);

            allProduct = bu.GetProduct();
            allUomSet = bu.GetUOMSet();

            allProductPrice = bu.GetProductPriceGroup();

            allProdGroup = bu.GetProductGroup();
            allProdSubGroup = bu.GetProductSubGroup();

            CreatePrintBtnList();
            ClearPromotionTemp();
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            //btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            //btnAdd.Enabled = false;

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            InitHeader();


        }

        private void InitHeader()
        {
            //var b = bu.GetBranch();
            //if (b != null)
            //{
            //    this.BindData("FromBranchID", searchBranchControls, b[0].BranchID);
            //}

            dtpSDocDate.SetDateTimePickerFormat();
            dtpDueDate.SetDateTimePickerFormat();
            dtpDocDate.SetDateTimePickerFormat();
            dtpStockDate.SetDateTimePickerFormat();
            dtpDocDatePO.SetDateTimePickerFormat();

            grdStock.AutoGenerateColumns = false;
            grdPOMst.AutoGenerateColumns = false;
            grdList.AutoGenerateColumns = false;
            grdPO.AutoGenerateColumns = false;

            grdPO.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (sendDate >= DateTime.Now)
            {
                dtpSDocDate.Value = sendDate;
                dtpDocDatePO.Value = sendDate;

                SearchPOMst();
                SearchPO();
            }
            else
            {
                var sendData = bu.GetSendData();
                if (sendData != null && sendData.Count > 0)
                {
                    var listPreOrderWHID = bu.GetAllBranchWarehouse(x => x.WHType == 2).Select(a => a.WHID).ToList();
                    if (listPreOrderWHID != null && listPreOrderWHID.Count > 0)
                    {
                        var _sendData = sendData.Where(x => listPreOrderWHID.Contains(x.WHID)).ToList();
                        if (_sendData != null && _sendData.Count > 0)
                        {
                            var _maxDate = _sendData.Max(x => x.DateSend);

                            sendDate = _maxDate;
                            dtpSDocDate.Value = sendDate;
                            dtpDocDatePO.Value = sendDate;
                        }
                    }
                }
            }
        }

        private void ClearData()
        {
            bu.tbl_IVMaster = null;
            bu.tbl_IVDetails.Clear();
            bu.tbl_POMaster_PRE = null;
            bu.tbl_PODetails_PRE.Clear();
            bu.tbl_PRMaster = null;
            bu.tbl_PRDetails.Clear();
            bu.tbl_InvMovements.Clear();
            bu.tbl_InvTransactions.Clear();
            bu.tbl_InvWarehouses.Clear();
        }

        private void btnSearchStock_Click(object sender, EventArgs e)
        {
            SearchStock();
        }

        private void SearchStock()
        {
            try
            {
                var branchs = bu.GetBranch();
                var currentStock = rdoCurrent.Checked ? true : false;
                var dt = preBu.GetStockData(branchs[0].BranchID, dtpStockDate.Value, currentStock);

                lblStockCount.Text = "0";
                if (dt != null && dt.Rows.Count > 0)
                {
                    BindGridview(grdStock, dt);
                    lblStockCount.Text = dt.Rows.Count.ToNumberFormat();

                    DateTime stockDate = new DateTime();
                    stockDate = currentStock ? DateTime.MaxValue : dtpStockDate.Value;

                    //FormHelper.CheckOverStok1000(stockDate); //Check Over Stock in main stock(1000)
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void BindGridview(DataGridView grd, DataTable dt)
        {
            grd.DataSource = dt;
        }

        private void grdStock_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdStock.SetRowPostPaint(sender, e, this.Font);

            foreach (DataGridViewRow row in grdStock.Rows)
            {
                var _diffCarQty = Convert.ToInt32(row.Cells["colDiffCarQty"].EditedFormattedValue);
                var _diffPckQty = Convert.ToInt32(row.Cells["colDiffPckQty"].EditedFormattedValue);

                if (_diffCarQty < 0)
                {
                    row.Cells["colDiffCarQty"].Style.ForeColor = Color.Red;
                }
                if (_diffPckQty < 0)
                {
                    row.Cells["colDiffPckQty"].Style.ForeColor = Color.Red;
                }
            }
        }


        #endregion

        #region event methods

        private void frmPreOrder_Load(object sender, EventArgs e)
        {
            InitPage();

            SearchStock();

            InitPODtHeader();

            btnSearchPOMst.PerformClick();

            if (isConfirmFTB)
                btnCancel.PerformClick();
        }

        private void tabPOMasterPre_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //tabPOMasterPre.SelectedIndex = 1;

            //this.ClearControl(docTypeCode, runDigit);

            //btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            //this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            //InitHeader();

            //txdDocNo.ReadOnly = false;
            //grdList.AutoGenerateColumns = false;
            //validateNewRow = true;

            //grdList.CellContentClick -= grdList_CellContentClick;
            //grdList.CellContentClick += grdList_CellContentClick;

            //grdList.Rows.Clear();
            ////AddNewRow(grdList, 0);
            //grdList.AddNewRow(allProduct, initDataGridList, 0, "", 0, validateNewRow, uoms, bu, this, 0);

            //txtCustomerCode.Focus();

            //SearchStock();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (bu.tbl_POMaster_PRE != null)
            {
                if (bu.tbl_POMaster_PRE.FlagSend == true)
                {
                    string message = "ไม่สามารถแก้ไขเอกสารได้ เนื่องจากได้ส่งข้อมูลเข้า Data Center แล้ว";
                    message.ShowWarningMessage();
                    return;
                }
            }

            btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, txdDocNo.Text);

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            txdDocNo.DisableTextBox(false);
            txtCustomerCode.DisableTextBox(false);
            txtCustPONo.DisableTextBox(false);
            btnSearchCust.Enabled = true;
            txtCustInvNO.DisableTextBox(false);

            txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");
            ddlDocStatus.Enabled = true;
            btnCancel.Enabled = true;
            btnReCalc.Enabled = false;
            //btnShowPromotion.Enabled = false;
            btnUpdateAddress.Enabled = false;
            btnGenCustIVNo.Enabled = false;
            validateNewRow = true;
            btnAdd.Enabled = false;

            if (!string.IsNullOrEmpty(txdDocNo.Text) && bu.CheckExistsPO_PRE(txdDocNo.Text)) //20210506 by sailom                
            {
                grdList.CellContentClick -= grdList_CellContentClick;
                grdList.CellContentClick += grdList_CellContentClick;
            }

            dtpDocDate.Focus();

            allProduct = bu.GetProduct();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            btnCopy.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnPrint, txdDocNo.Text);

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == "3" || x.DocStatusCode == "4" || x.DocStatusCode == "5"; };
            ddlDocStatus.SelectedValueDropdownList(condition);
            ddlDocStatus.Enabled = false;

            txdDocNo.Text = string.Empty;
            txtCustPONo.Text = string.Empty;
            txtCustInvNO.Text = string.Empty;

            validateNewRow = true;
            ddlSaleArea.Enabled = true;

            ClearPromotionTemp();

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();

            SearchStock();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            InitPage();

            SearchStock();

            InitPODtHeader();

            ////this.ClearControl(bu, docTypeCode, runDigit);
            //btnAdd.Enabled = false;

            //this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");

            //validateNewRow = true;

            //this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            //btnCancel.EnableButton(btnSearchDoc);

            //txdDocNo.DisableTextBox(false);
            //txdDocNo.BackColor = Color.Turquoise;
            //btnSave.Enabled = false;

            grdList.CellContentClick -= grdList_CellContentClick;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Dictionary<string, object> _params = new Dictionary<string, object>();
            //_params.Add("@DocNo", txdDocNo.Text);
            //this.OpenCrystalReportsPopup("ใบกำกับภาษีอย่างย่อ", "Form_IV.rpt", "Form_IV", _params);

            printContextMenuStrip.Show(btnPrint, new Point(0, btnPrint.Height));
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //grdList.Controls.Clear();
            //grdList.ClearControl();

            this.Controls.Clear();
            this.Dispose();
            this.Close();
        }

        #endregion


        #region Tab 1

        private void SearchPOMst()
        {
            try
            {
                bool isOverStock = FormHelper.CheckOverStok1000PreOrder(dtpSDocDate.Value); //Check Over Stock in main stock(1000)

                var docNo = txtDocNoPOMst.Text;
                var whid = txtMstWHCode.Text;
                var dt = preBu.GetPOMstData(docNo, dtpSDocDate.Value, whid);

                if (dt != null && dt.Rows.Count > 0)
                {
                    BindGridview(grdPOMst, dt);
                    lblPreOrderCount.Text = dt.Rows.Count.ToNumberFormat();
                    btnCreatePO.Enabled = true;
                }
                else
                {
                    BindGridview(grdPOMst, null);
                    lblPreOrderCount.Text = "0";
                    btnCreatePO.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void btnSearchPreDoc_Click(object sender, EventArgs e)
        {
            this.OpenIVDocPopup("ใบกำกับสินค้า/กำกับภาษี", "PreOrder");
        }

        private void btnCreatePO_Click(object sender, EventArgs e)
        {
            bool isOverStock = FormHelper.CheckOverStok1000PreOrder(dtpSDocDate.Value); //Check Over Stock in main stock(1000)
            if (isOverStock)
            {
                return;
            }

            rlDocNo = "";

            if (grdPOMst == null || grdPOMst.Rows.Count == 0)
            {
                string msg = "ไม่พบข้อมูล PO!!";
                msg.ShowWarningMessage();

                return;
            }

            string docNos = "";
            List<string> docNoList = new List<string>();

            foreach (DataGridViewRow row in grdPOMst.Rows)
            {
                var sel = grdPOMst.Rows[row.Index].Cells["colSelectRow"].Value;
                string _docNo = grdPOMst.Rows[row.Index].Cells["colDocNo"].Value.ToString();

                if (grdPOMst.Rows[row.Index].Cells["colSelectRow"].IsNotNullOrEmptyCell())
                {
                    bool chk = false;
                    if (sel != null && bool.TryParse(sel.ToString(), out chk))
                    {
                        if (chk)
                            docNoList.Add(_docNo);
                    }

                }
            }

            docNos = string.Join(",", docNoList);

            if (string.IsNullOrEmpty(docNos))
            {
                string msg = "กรุณาเลือกเอกสาร ก่อนทำรายการ!!!";
                msg.ShowWarningMessage();

                return;
            }

            string cfMsg = "ต้องการสร้าง PO ใช่หรือไม่?";
            string title = "ยืนยันการบันทึก!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            if (!string.IsNullOrEmpty(docNos))
            {
                var ret = preBu.TransferPreOrderToPO(dtpSDocDate.Value, Helper.tbl_Users.Username, docNos);
                if (ret == 1)
                {
                    string msg = "สร้าง PO เรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    if (grdPOMst != null && grdPOMst.Rows.Count > 0)
                    {
                        string whName = grdPOMst.Rows[0].Cells["colWHName"].Value.ToString();
                        var wh = bu.GetBranchWarehouse(x => x.WHName == whName);
                        if (wh != null)
                        {
                            string whid = wh.WHID;

                            var docNo = preBu.GenerateRL(dtpSDocDate.Value, whid, Helper.tbl_Users.Username, docNos);
                            if (!string.IsNullOrEmpty(docNo))
                            {
                                rlDocNo = docNo;
                                msg = "สร้างใบจัดสินค้า เลขที่ : " + docNo + " เรียบร้อยแล้ว!!";
                                msg.ShowInfoMessage();
                            }
                        }
                    }

                    SearchStock();

                    SearchPOMst();

                    tabPOMasterPre.SelectedIndex = 2;
                    btnSearchPO.PerformClick();
                }
                else
                {
                    string msg = "สร้าง PO ไม่สำเร็จ กรุณาลองใหม่อีกครั้ง!!!";
                    msg.ShowErrorMessage();
                }
            }
        }

        private void TxtDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchPOMst();
            }
        }

        private void TxtMstWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("BranchWarehouse", searchMstBWHControls, txt.Text);

                FilterSaleArea(txt.Text);
            }
        }

        private void TxtMstWHCode_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (!string.IsNullOrEmpty(txt.Text))
            {
                var bwh = bu.GetBranchWarehouse(x => x.WHID == txt.Text);
                if (bwh != null)
                {
                    this.BindData("BranchWarehouse", searchMstBWHControls, txt.Text);

                    FilterSaleArea(txt.Text);
                }
            }
        }

        private void btnSearchPOMst_Click(object sender, EventArgs e)
        {
            SearchPOMst();
        }

        private void btnWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchMstBWHControls, "เลือกคลังสินค้า", whFunc);
        }

        private void grdPOMstList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var docNo = grdPOMst.Rows[e.RowIndex].Cells["colDocNo"].EditedFormattedValue.ToString();
            if (!string.IsNullOrEmpty(docNo))
            {
                BindVanSalesData(docNo, "IV2");
                tabPOMasterPre.SelectedIndex = 1;
            }
        }

        private void grdPOMstList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPOMst.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdPOMstList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var grd = sender as DataGridView;
            string colImg = grd.Name == "grdPOMst" ? "colImg" : "colImg2";

            if (grd.Columns[e.ColumnIndex].Name == colImg)
            {
                if (grd.Name == "grdPOMst" || grd.Name == "grdPO")
                {
                    string colName = grd.Name == "grdPOMst" ? "colDocStatusName" : "colDocStatusNamePO";

                    var rowIdx = e.RowIndex;
                    var docStatus = grd.Rows[rowIdx].Cells[colName].Value;

                    if (grd.Rows[e.RowIndex].Cells[colName].Value != null && !string.IsNullOrEmpty(docStatus.ToString()))
                    {
                        string tgColName = grd.Name == "grdPOMst" ? "colImg" : "colImg2";

                        Bitmap closeImg = new Bitmap(Properties.Resources.power_off);
                        Bitmap cancelmg = new Bitmap(Properties.Resources.closeBtn);
                        Bitmap inProcessmg = new Bitmap(Properties.Resources.timeBtn);

                        Bitmap statusImg = null;
                        if (docStatus.ToString() == "Closed")
                        {
                            statusImg = closeImg;
                        }
                        else if (docStatus.ToString() == "Cancelled")
                        {
                            statusImg = cancelmg;
                        }
                        else if (docStatus.ToString() == "In Process")
                        {
                            statusImg = inProcessmg;
                        }
                        if (statusImg != null)
                        {
                            e.Value = statusImg;
                        }
                    }
                }
            }
        }

        private void grdPOMst_DataSourceChanged(object sender, EventArgs e)
        {
            if (grdPOMst.DataSource != null && grdPOMst.RowCount > 0)
            {
                grdPOMst.CreateCheckBoxHeaderColumn("colSelectRow");
            }

        }

        #endregion

        #region Tab 2

        public void BindVanSalesData(string ivDocNo, string _docTypeCode)
        {
            bu.GetDocData(ivDocNo, _docTypeCode);

            var po = bu.tbl_POMaster_PRE;
            var poDts = bu.tbl_PODetails_PRE;

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

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            btnSearchPOMst.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);

            txtDocNoPOMst.DisableTextBox(false);
            txtDocNoPOMst.BackColor = Color.Turquoise;
            btnReCalc.Enabled = false;
            btnAdd.Enabled = false;

            btnSave.Enabled = false;
            btnCancel.Enabled = true;
            dtpSDocDate.Enabled = true;
            btnSearchPOMst.Enabled = true;
            btnUpdateAddress.Enabled = false;
            btnGenCustIVNo.Enabled = false;
            btnCreatePO.Enabled = true;
            dtpStockDate.Enabled = true;
            btnSearchStock.Enabled = true;
            rdoCurrent.Enabled = true;
            rdoAtDate.Enabled = true;
            btnAdd.Enabled = false;
            //btnShowPromotion.Enabled = false;

            grdList.CellContentClick -= grdList_CellContentClick;

            //bool checkEditMode = bu.CheckExistsPO_PRE(ivDocNo);
            //po.DocStatus.CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);

            //btnUpdateAddress.Enabled = true;
            //btnGenCustIVNo.Enabled = true;
        }

        private void BindPOMaster(tbl_POMaster_PRE po)
        {
            txdDocNo.Text = po.DocNo;

            dtpDocDate.Value = po.DocDate.ToDateTimeFormat();

            txtCustomerCode.Text = po.CustomerID;
            txtCustName.Text = po.CustName;
            txtContact.Text = po.ContactName;
            txtTelephone.Text = po.ContactTel;
            txtBillTo.Text = po.Address;
            txtCustPONo.Text = po.CustPONo;
            txtCustInvNO.Text = po.CustInvNO;

            var employee = bu.GetEmployee(po.SaleEmpID);
            if (employee != null)
                txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName);

            var emp = bu.GetEmployee().FirstOrDefault(x => x.EmpID == po.SaleEmpID);
            if (emp != null)
                txtEmpCode.Text = emp.TitleName + " " + emp.FirstName;

            txtWHCode.Text = po.WHID;
            Func<tbl_BranchWarehouse, bool> func = (x => x.WHID == po.WHID);
            var wh = bu.GetBranchWarehouse(func);
            if (wh != null)
            {
                txtWHName.Text = wh.WHName;
            }

            FilterSaleArea(po.CustomerID);
            ddlDocStatus.BindDropdown3DocStatus(bu, po.DocStatus);

            txtComment.Text = po.Comment;

            txnAmount.Text = po.IncVat.Value.ToStringN2(); //po.Amount.Value.ToStringN2();
            txnDiscountType.Text = "0";// po.DiscountType;
            txnDiscountAmt.Text = po.Discount.Value.ToStringN2();
            txnBeforeVat.Text = (po.IncVat.Value - po.Discount.Value - po.VatAmt.Value - po.ExcVat.Value).ToStringN2(); //(po.IncVat.Value - po.VatAmt.Value).ToStringN2();
            txnVatAmt.Text = po.VatAmt.Value.ToStringN2();
            lblVatType.Text = po.VatRate != null ? po.VatRate.Value.ToStringN0() : "";
            txnExcVat.Text = po.ExcVat.Value.ToStringN2();
            txnTotalDue.Text = po.TotalDue.ToStringN2();
        }

        private void BindPODetail(List<tbl_PODetail_PRE> poDts)
        {
            grdList.Rows.Clear();

            //var allUOM = bu.GetUOM();
            var allProduct = bu.GetProduct();
            //var allProductPrice = bu.GetProductPriceGroup();

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

                grdList.BindComboBoxCell(allProduct, grdList.Rows[i], i, false, 3, uoms, this, bu, 0);
                grdList.Rows[i].Cells[3].Value = poDts[i].OrderUom;

                grdList.Rows[i].Cells[4].Value = poDts[i].OrderQty;
                grdList.Rows[i].Cells[5].Value = poDts[i].UnitPrice;
                grdList.Rows[i].Cells[6].Value = poDts[i].VatType;
                grdList.Rows[i].Cells[7].Value = poDts[i].LineDiscountType;
                grdList.Rows[i].Cells[8].Value = poDts[i].LineDiscount;
                grdList.Rows[i].Cells[9].Value = poDts[i].LineTotal;
                grdList.Rows[i].Cells[10].Value = poDts[i].OrderUom;

                if (poDts[i].OrderUom != -1)
                {
                    string prdCode = poDts[i].ProductID;
                    //Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == poDts[i].OrderUom && x.ProductID == prdCode);
                    var prdPriceList = allProductPrice.Where(x => x.ProductUomID == poDts[i].OrderUom && x.ProductID == prdCode).ToList(); //bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);

                    if (prdPriceList != null && prdPriceList.Count > 0)
                        grdList.Rows[i].Cells[11].Value = prdPriceList[0].SellPrice.Value;
                }
            }
        }

        public void BindSearchProduct(DataTable productDT, int rowIndex)
        {
            validateNewRow = true;
            grdList.ValidateDuplicateSKU(productDT.Rows[0]["ProductID"].ToString(), 0, rowIndex, ref validateNewRow);

            var allProduct = bu.GetProduct();
            grdList.BindDataGrid(allProduct, dataGridList, initDataGridList, productDT, 0, rowIndex, validateNewRow, this, uoms, bu, 0);
        }

        private void ClearPromotionTemp()
        {
            //pro.RemoveTempData();
        }

        private void CalcPromotion(bool isShowPopup = false)
        {
            try
            {
                pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                pro.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();
                var ret = pro.RemoveTempData();

                if (ret == 1)
                {
                    //var allProduct = bu.GetProduct();
                    bool validateQty = true;
                    if (grdList.RowCount > 0)
                    {
                        for (int i = 0; i < grdList.RowCount; i++)
                        {
                            var prdCodeCell = grdList.Rows[i].Cells[0];
                            var qtyCell = grdList.Rows[i].Cells[4];
                            var discountType = grdList.Rows[i].Cells[6];
                            if (prdCodeCell.IsNotNullOrEmptyCell() && discountType.IsNotNullOrEmptyCell()
                                && discountType.EditedFormattedValue.ToString() != "F" && discountType.EditedFormattedValue.ToString() != "S")
                            {
                                if (qtyCell.IsNotNullOrEmptyCell() && Convert.ToDecimal(qtyCell.EditedFormattedValue.ToString()) <= 0) //check qty
                                {
                                    var message = "จำนวนสินค้าไม่ถูกต้อง กรุณาตรวจสอบอีกครั้ง !!!";
                                    message.ShowWarningMessage();
                                    validateQty = false;
                                    break;
                                }
                            }
                        }
                    }
                    //validateQty = grdList.ValiadteDataGridView(allProduct, 0, 3, 4, 5, null, "", false);

                    if (txtCustName.Text.Contains("ไม่ระบุ")) //validate except customer
                    {
                        return;
                    }
                    if (string.IsNullOrEmpty(txtCustName.Text))
                    {
                        var message = "กรุณาเลือกร้านค้า !!!";
                        message.ShowWarningMessage();
                        txtCustomerCode.ErrorTextBox();
                        return;
                    }

                    decimal totalDiscount = 0.00m;

                    if (validateQty)
                    {
                        pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                        pro.tbl_HQ_Promotion_Hits = new List<tbl_HQ_Promotion_Hit>();

                        proTmp.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                        bu.tbl_POMaster_PRE = new tbl_POMaster_PRE();
                        bu.tbl_PODetails_PRE = new List<tbl_PODetail_PRE>();

                        PreparePOMaster(false);
                        PreparePODetail(0, false);

                        var proList = pro.CalculatePromotion(bu.tbl_PODetails_PRE.Where(x => x.UnitPrice > 0).ToList());
                        if (proList != null && proList.Count > 0)
                        {
                            var delFlag = pro.RemoveTempData();
                            if (delFlag == 1)
                            {
                                PreparePromotionTemp(proList);
                                var addFlag = pro.AddTempData();

                                var po = bu.tbl_POMaster_PRE;
                                po.Discount = pro.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt);
                                totalDiscount = po.Discount.Value;

                                //decimal reCaclBeforeVat = Convert.ToDecimal(txnBeforeVat.Text) - po.Discount.Value;
                                //txnBeforeVat.Text = reCaclBeforeVat.ToStringN2();
                                if (isShowPopup && addFlag == 1)
                                {
                                    if (pro.tbl_HQ_Promotion_Hit_Temps != null && pro.tbl_HQ_Promotion_Hit_Temps.Count > 0)
                                    {
                                        ValidateInputShelf();

                                        this.OpenPromotionTempPopup(searchPromotionControls, "โปรโมชั่น");
                                    }
                                }
                            }
                        }
                    }

                    txnDiscountAmt.Text = totalDiscount.ToStringN2();

                    CalculateTotal();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage(); ;
            }
        }

        private void CalcPromotionBeforeSave()
        {
            CalcPromotion(true);

            PreparePODetail(2, false); //Calc Pro

            DistributeFreePromotion(bu.tbl_PODetails_PRE);

            BindPODetail(bu.tbl_PODetails_PRE);

            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                CalculateRow(grdList, i);
            }

            CalculateTotal();

            decimal totalDiscount = 0.00m;
            proTmp.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();

            if (proTmp.tbl_HQ_Promotion_Hit_Temps.Any(x => x.DisCountAmt.Value > 0))
                totalDiscount = proTmp.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value);

            txnDiscountAmt.Text = totalDiscount.ToStringN2();
        }

        //private void CalculateRow(DataGridView grd, int rowIndex)
        //{
        //    decimal orderQty = 0;
        //    decimal unitPrice = 0;
        //    decimal unitSellPrice = 0;
        //    decimal discount = 0;
        //    int orderUom = -1;
        //    decimal discountSellPrice = 0;

        //    var cell0 = grd.Rows[rowIndex].Cells[0];
        //    var cell3 = grd.Rows[rowIndex].Cells[3];
        //    var cell4 = grd.Rows[rowIndex].Cells[4];
        //    var cell5 = grd.Rows[rowIndex].Cells[5];
        //    var cell6 = grd.Rows[rowIndex].Cells[6];
        //    var cell7 = grd.Rows[rowIndex].Cells[7];
        //    var cell8 = grd.Rows[rowIndex].Cells[8];
        //    var cell9 = grd.Rows[rowIndex].Cells[9];
        //    var cell11 = grd.Rows[rowIndex].Cells[11];

        //    if (cell3.IsNotNullOrEmptyCell())
        //    {
        //        //Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => uomList.Contains(x.ProductUomID));
        //        //var allPrdUOM = bu.GetUOM(); //bu.GetUOM(tbl_ProductUomPre);

        //        var prdUOM = allUOM.FirstOrDefault(x => x.ProductUomName == cell3.EditedFormattedValue.ToString());
        //        if (prdUOM != null)
        //        {
        //            orderUom = prdUOM.ProductUomID;
        //            if (orderUom != -1)
        //            {
        //                string prdCode = cell0.EditedFormattedValue.ToString();
        //                Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == orderUom && x.ProductID == prdCode);
        //                var prdPriceList = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList(); // bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);

        //                if (prdPriceList != null && prdPriceList.Count > 0)
        //                {
        //                    cell5.Value = prdPriceList[0].SellPriceVat.Value;
        //                    cell11.Value = prdPriceList[0].SellPrice.Value;
        //                }
        //            }
        //            else
        //            {
        //                cell5.Value = 0;
        //                cell11.Value = 0;
        //            }
        //        }
        //        else
        //        {
        //            cell5.Value = 0;
        //            cell11.Value = 0;
        //        }
        //    }
        //    if (cell4.IsNotNullOrEmptyCell())
        //    {
        //        orderQty = Convert.ToDecimal(cell4.EditedFormattedValue);
        //    }
        //    if (cell5.IsNotNullOrEmptyCell())
        //    {
        //        unitPrice = Convert.ToDecimal(cell5.EditedFormattedValue);
        //    }
        //    if (cell11.IsNotNullOrEmptyCell())
        //    {
        //        unitSellPrice = Convert.ToDecimal(cell11.EditedFormattedValue);
        //    }
        //    if (cell8.IsNotNullOrEmptyCell())
        //    {
        //        if (cell7.IsNotNullOrEmptyCell())
        //        {
        //            discount = bu.CalDiscountType(cell7.EditedFormattedValue.ToString(), cell8.EditedFormattedValue.ToString(), orderQty, unitPrice);

        //            if (cell6.FormattedValue.ToString() != "0")
        //                discountSellPrice = bu.CalDiscountType(cell7.EditedFormattedValue.ToString(), cell8.EditedFormattedValue.ToString(), orderQty, unitSellPrice);
        //        }
        //        else
        //        {
        //            discount = 0;
        //        }

        //        if (discountSellPrice != 0)
        //        {
        //            listDiscount.Remove(cell0.EditedFormattedValue.ToString());
        //            listDiscount.Add(cell0.EditedFormattedValue.ToString(), discountSellPrice);
        //        }
        //        else
        //            listDiscount.Remove(cell0.EditedFormattedValue.ToString());

        //        //cell9.Value = ((orderQty * unitPrice) - discount).ToDecimalN2().ToStringN2();
        //        cell9.Value = (orderQty * unitPrice).ToDecimalN2().ToStringN2();
        //    }

        //    CalculateTotal();
        //}

        //private void CalculateTotal()
        //{
        //    decimal amount = 0;
        //    decimal excVat = 0;
        //    decimal incVat = 0;
        //    decimal vatAmt = 0;
        //    decimal totalDue = 0;
        //    decimal vatRate = 0;
        //    string discountType = "";
        //    decimal totalDiscountAmt = 0;

        //    //var allPrdUOM = bu.GetUOM();
        //    //var allProductPrice = bu.GetProductPriceGroup();

        //    decimal totalDiscount = 0;
        //    if (!string.IsNullOrEmpty(txnDiscountAmt.Text))
        //        totalDiscount = Convert.ToDecimal(txnDiscountAmt.Text).ToDecimalN2();

        //    for (int i = 0; i < grdList.RowCount; i++)
        //    {
        //        var cell0 = grdList.Rows[i].Cells[0];
        //        var cell3 = grdList.Rows[i].Cells[3];
        //        var cell4 = grdList.Rows[i].Cells[4];
        //        var vatCell = grdList.Rows[i].Cells[6];
        //        var lineTotalCell = grdList.Rows[i].Cells[9];
        //        var discountTypeCell = grdList.Rows[i].Cells[7];
        //        var discountAmtCell = grdList.Rows[i].Cells[8];
        //        var sellPriceCell = grdList.Rows[i].Cells[11];
        //        string prdCode = cell0.EditedFormattedValue.ToString();
        //        var _qty = Convert.ToDecimal(cell4.EditedFormattedValue);

        //        if (!string.IsNullOrEmpty(prdCode))
        //        {
        //            if (totalDiscount > 0)
        //            {
        //                decimal discountPerSku = totalDiscount / (_qty <= 0 ? 1 : _qty);
        //                if (listDiscount.Count(x => x.Key == prdCode) == 0)
        //                    listDiscount.Add(prdCode, discountPerSku);
        //            }

        //            if (lineTotalCell.IsNotNullOrEmptyCell())
        //            {
        //                amount += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
        //            }
        //            if (vatCell.IsNotNullOrEmptyCell())
        //            {
        //                decimal _vateRate = Convert.ToDecimal(vatCell.EditedFormattedValue);
        //                if (_vateRate > 0) //have VAT
        //                {
        //                    vatRate = _vateRate;
        //                }
        //                else //No VAT
        //                {
        //                    excVat += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
        //                }
        //            }
        //            if (discountTypeCell.IsNotNullOrEmptyCell())
        //            {
        //                if (discountTypeCell.EditedFormattedValue.ToString() != "ไม่มี")
        //                {
        //                    discountType += discountTypeCell.EditedFormattedValue.ToString() + ", ";
        //                }
        //            }
        //            if (discountAmtCell.IsNotNullOrEmptyCell())
        //            {
        //                totalDiscountAmt += Convert.ToDecimal(discountAmtCell.EditedFormattedValue);
        //            }
        //        }
        //    }

        //    excVat = excVat.ToDecimalN2();
        //    totalDue = amount - totalDiscount;

        //    incVat = ((amount - excVat - totalDiscount) * 100) / (100 + vatRate);
        //    vatAmt = incVat * (vatRate / 100).ToDecimalN2();

        //    lblVatType.Text = vatRate.ToDecimalN0().ToString();
        //    txnAmount.Text = amount.ToDecimalN2().ToStringN2();
        //    txnBeforeVat.Text = incVat.ToStringN2();
        //    txnVatAmt.Text = vatAmt.ToStringN2();
        //    txnExcVat.Text = excVat.ToStringN2();
        //    txnTotalDue.Text = totalDue.ToStringN2();
        //    txnCommission.Text = "0.00";
        //}

        private void CalculateRow(DataGridView grd, int rowIndex)
        {
            decimal orderQty = 0;
            decimal unitPrice = 0;
            decimal unitSellPrice = 0;
            decimal discount = 0;
            int orderUom = -1;
            decimal discountSellPrice = 0;

            var cell0 = grd.Rows[rowIndex].Cells[0];
            var cell3 = grd.Rows[rowIndex].Cells[3];
            var cell4 = grd.Rows[rowIndex].Cells[4];
            var cell5 = grd.Rows[rowIndex].Cells[5];
            var cell6 = grd.Rows[rowIndex].Cells[6];
            var cell7 = grd.Rows[rowIndex].Cells[7];
            var cell8 = grd.Rows[rowIndex].Cells[8];
            var cell9 = grd.Rows[rowIndex].Cells[9];
            var cell11 = grd.Rows[rowIndex].Cells[11];

            if (cell3.IsNotNullOrEmptyCell())
            {
                //Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => uomList.Contains(x.ProductUomID));
                //var allPrdUOM = bu.GetUOM(); //bu.GetUOM(tbl_ProductUomPre);

                var prdUOM = allUOM.FirstOrDefault(x => x.ProductUomName == cell3.EditedFormattedValue.ToString());
                if (prdUOM != null)
                {
                    orderUom = prdUOM.ProductUomID;
                    if (orderUom != -1)
                    {
                        string prdCode = cell0.EditedFormattedValue.ToString();
                        Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == orderUom && x.ProductID == prdCode);
                        var prdPriceList = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList(); // bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);

                        if (prdPriceList != null && prdPriceList.Count > 0)
                        {
                            cell5.Value = prdPriceList[0].SellPriceVat.Value;
                            cell11.Value = prdPriceList[0].SellPrice.Value;
                        }
                    }
                    else
                    {
                        cell5.Value = 0;
                        cell11.Value = 0;
                    }
                }
                else
                {
                    cell5.Value = 0;
                    cell11.Value = 0;
                }
            }
            if (cell4.IsNotNullOrEmptyCell())
            {
                orderQty = Convert.ToDecimal(cell4.EditedFormattedValue);
            }
            if (cell5.IsNotNullOrEmptyCell())
            {
                unitPrice = Convert.ToDecimal(cell5.EditedFormattedValue);
            }
            if (cell11.IsNotNullOrEmptyCell())
            {
                unitSellPrice = Convert.ToDecimal(cell11.EditedFormattedValue);
            }
            if (cell8.IsNotNullOrEmptyCell())
            {
                if (cell7.IsNotNullOrEmptyCell())
                {
                    discount = bu.CalDiscountType(cell7.EditedFormattedValue.ToString(), cell8.EditedFormattedValue.ToString(), orderQty, unitPrice);

                    if (cell6.FormattedValue.ToString() != "0")
                        discountSellPrice = bu.CalDiscountType(cell7.EditedFormattedValue.ToString(), cell8.EditedFormattedValue.ToString(), orderQty, unitSellPrice);
                }
                else
                {
                    discount = 0;
                }

                if (discountSellPrice != 0)
                {
                    listDiscount.Remove(cell0.EditedFormattedValue.ToString());
                    listDiscount.Add(cell0.EditedFormattedValue.ToString(), discountSellPrice);
                }
                else
                    listDiscount.Remove(cell0.EditedFormattedValue.ToString());

                //cell9.Value = ((orderQty * unitPrice) - discount).ToDecimalN2().ToStringN2();
                cell9.Value = (orderQty * unitPrice).ToDecimalN2().ToStringN2();
            }

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
            string discountType = "";
            decimal totalDiscountAmt = 0;

            //var allPrdUOM = bu.GetUOM();
            //var allProductPrice = bu.GetProductPriceGroup();

            decimal totalDiscount = 0;
            if (!string.IsNullOrEmpty(txnDiscountAmt.Text))
                totalDiscount = Convert.ToDecimal(txnDiscountAmt.Text).ToDecimalN2();

            for (int i = 0; i < grdList.RowCount; i++)
            {
                var cell0 = grdList.Rows[i].Cells[0];
                var cell3 = grdList.Rows[i].Cells[3];
                var cell4 = grdList.Rows[i].Cells[4];
                var vatCell = grdList.Rows[i].Cells[6];
                var lineTotalCell = grdList.Rows[i].Cells[9];
                var discountTypeCell = grdList.Rows[i].Cells[7];
                var discountAmtCell = grdList.Rows[i].Cells[8];
                var sellPriceCell = grdList.Rows[i].Cells[11];
                string prdCode = cell0.EditedFormattedValue.ToString();
                var _qty = Convert.ToDecimal(cell4.EditedFormattedValue);

                if (!string.IsNullOrEmpty(prdCode))
                {
                    if (totalDiscount > 0)
                    {
                        decimal discountPerSku = totalDiscount / (_qty <= 0 ? 1 : _qty);
                        if (listDiscount.Count(x => x.Key == prdCode) == 0)
                            listDiscount.Add(prdCode, discountPerSku);
                    }

                    if (lineTotalCell.IsNotNullOrEmptyCell())
                    {
                        amount += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                    }
                    if (vatCell.IsNotNullOrEmptyCell())
                    {
                        decimal _vateRate = Convert.ToDecimal(vatCell.EditedFormattedValue);
                        if (_vateRate > 0) //have VAT
                        {
                            vatRate = _vateRate;
                        }
                        else //No VAT
                        {
                            excVat += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                        }
                    }
                    if (discountTypeCell.IsNotNullOrEmptyCell())
                    {
                        if (discountTypeCell.EditedFormattedValue.ToString() != "ไม่มี")
                        {
                            discountType += discountTypeCell.EditedFormattedValue.ToString() + ", ";
                        }
                    }
                    if (discountAmtCell.IsNotNullOrEmptyCell())
                    {
                        totalDiscountAmt += Convert.ToDecimal(discountAmtCell.EditedFormattedValue);
                    }
                }
            }

            excVat = excVat.ToDecimalN2();
            totalDue = amount - totalDiscount;

            incVat = ((amount - excVat - totalDiscount) * 100) / (100 + vatRate);
            vatAmt = incVat * (vatRate / 100).ToDecimalN2();

            lblVatType.Text = vatRate.ToDecimalN0().ToString();
            txnAmount.Text = amount.ToDecimalN2().ToStringN2();
            txnBeforeVat.Text = incVat.ToStringN2();
            txnVatAmt.Text = vatAmt.ToStringN2();
            txnExcVat.Text = excVat.ToStringN2();
            txnTotalDue.Text = totalDue.ToStringN2();
            txnCommission.Text = "0.00";
        }

        private void FilterSaleArea(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                //Func<tbl_ArCustomer, bool> func = (x => x.CustomerID == text.Trim());
                var cust = bu.GetCustomer(text.Trim());
                if (cust != null && cust.Count > 0)
                {
                    var _saleAreaList = salAreaDistrictList.Where(x => x.WHID == cust[0].WHID);
                    var listOfSalAreaID = _saleAreaList.Select(a => a.SalAreaID).ToList();
                    saleAreaList = bu.GetAllSaleArea().Where(x => listOfSalAreaID.Contains(x.SalAreaID)).ToList();

                    //Predicate<tbl_SalArea> predicate = delegate (tbl_SalArea p) { return p.SalAreaID == cust[0].SalAreaID; };
                    //ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID", null, predicate);

                    //edit by sailom 07-06-2021---------------------------------------
                    saleAreaList = saleAreaList.Where(p => p.SalAreaID == cust[0].SalAreaID).ToList();

                    if (saleAreaList.Count == 0)
                        saleAreaList = bu.GetSaleArea(x => x.SalAreaName.Contains(cust[0].WHID.Substring(3, 3)));

                    if (saleAreaList.Count > 0)
                    {
                        Predicate<tbl_SalArea> defaultSelect = delegate (tbl_SalArea p) { return p.SalAreaID == cust[0].SalAreaID; };
                        ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID", null, defaultSelect);
                    }
                    //edit by sailom 07-06-2021---------------------------------------

                    var emp = bu.GetEmployee(cust[0].EmpID);
                    if (emp != null)
                        txtEmpCode.Text = emp.TitleName + " " + emp.FirstName;

                    Func<tbl_BranchWarehouse, bool> whFunc = (x => x.WHID == cust[0].WHID);
                    var wh = bu.GetBranchWarehouse(whFunc);
                    if (wh != null)
                    {
                        txtWHCode.Text = wh.WHCode;
                        txtWHName.Text = wh.WHName;
                    }
                }
                else
                {
                    if (text.Length > 6) //check customer code and whid
                    {
                        return;
                    }
                    else
                    {
                        var _saleAreaList = salAreaDistrictList.Where(x => x.WHID == text).ToList();
                        var listOfSalAreaID = _saleAreaList.Select(a => a.SalAreaID).ToList();
                        saleAreaList = bu.GetAllSaleArea().Where(x => listOfSalAreaID.Contains(x.SalAreaID)).ToList();

                        ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID");

                        var bwh = bu.GetBranchWarehouse(x => x.WHID == text);
                        var emp = bu.GetEmployee(bwh.SaleEmpID);
                        if (emp != null)
                            txtEmpCode.Text = emp.TitleName + " " + emp.FirstName;
                    }
                }
            }
        }

        private void InitPODtHeader()
        {
            dtpDocDate.SetDateTimePickerFormat();
            dtpDueDate.SetDateTimePickerFormat();

            ddlDocStatus.BindDropdown3DocStatus(bu);

            ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID", null, null);
            ddlSaleArea.Enabled = true;
        }

        private void InitialPODtData()
        {
            var allProduct = bu.GetProduct();

            this.ClearControl(bu, docTypeCode, runDigit);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            InitHeader();

            grdList.AutoGenerateColumns = false;
            validateNewRow = true;

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;

            grdList.Rows.Clear();
            //AddNewRow(grdList, 0);
            grdList.AddNewRow(allProduct, initDataGridList, 0, "", 0, validateNewRow, uoms, bu, this, 0);

            var employee = bu.GetEmployee(Helper.tbl_Users.EmpID);
            txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName);
            //btnShowPromotion.Enabled = true;
            btnReCalc.Enabled = true;
            btnAdd.Enabled = false;

            //txtCustomerCode.Focus();
        }

        private void PreparePOMaster(bool editFlag = false)
        {
            bu.tbl_POMaster_PRE = new tbl_POMaster_PRE();

            var comp = bu.GetCompany();
            var emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            var supp = bu.GetSupplier(txtCustomerCode.Text);

            Dictionary<string, string> allEmp = new Dictionary<string, string>();
            KeyValuePair<string, string> selEmp = new KeyValuePair<string, string>();

            bu.GetEmployee().ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty))));
            selEmp = allEmp.FirstOrDefault(x => x.Value == txtEmpCode.Text.Replace(" ", string.Empty));
            var vanWH = bu.GetAllBranchWarehouse().FirstOrDefault(x => x.SaleEmpID == selEmp.Key);

            var po = bu.tbl_POMaster_PRE;
            bool checkEditMode = bu.CheckExistsPO_PRE(txdDocNo.Text);
            po.DocNo = txdDocNo.Text;

            //if (checkEditMode)
            //    po.DocNo = txdDocNo.Text;
            //else
            //    po.DocNo = bu.GenDocNo(docTypeCode, txtWHCode.Text);

            po.RevisionNo = 0;
            po.DocTypeCode = "IV";
            po.DocStatus = "3";
            po.DocDate = dtpDocDate.Value.ToDateTimeFormat();
            po.DocRef = "";
            po.StatusInOut = null;
            po.SupplierID = "0";
            po.SuppName = "";

            if (vanWH != null)
                po.WHID = vanWH.WHID;

            if (emp != null)
                po.EmpID = emp.EmpID;

            if (!string.IsNullOrEmpty(selEmp.Key))
                po.SaleEmpID = selEmp.Key;

            po.SalAreaID = ddlSaleArea.SelectedValue.ToString();
            po.Address = txtBillTo.Text;
            po.ContactName = txtContact.Text;
            po.ContactTel = txtTelephone.Text;
            po.Shipto = txtBillTo.Text;

            po.CreditDay = Convert.ToInt16(nudCreditDay.Value);
            Func<tbl_ArCustomer, bool> func = (x => x.CustomerCode == txtCustomerCode.Text);
            var cust = bu.GetCustomer(func);
            if (cust != null && cust.Count > 0)
            {
                po.CreditDay = cust[0].CreditDay;
                po.CustType = cust[0].CustomerTypeID.Value.ToString();
            }
            po.DueDate = dtpDocDate.Value.AddDays(po.CreditDay.Value);
            po.CustomerID = txtCustomerCode.Text;
            po.CustName = txtCustName.Text;
            po.CustInvNO = string.IsNullOrEmpty(txtCustInvNO.Text) ? null : txtCustInvNO.Text; //05022021
            po.CustInvDate = null;
            po.CustPODate = dtpDocDate.Value;
            po.CustPONo = txtCustPONo.Text;
            po.Remark = "";
            po.Comment = txtComment.Text;
            po.OldAmount = Convert.ToDecimal(txnAmount.Text);
            po.Amount = Convert.ToDecimal(txnAmount.Text);
            po.OldExcVat = Convert.ToDecimal(txnExcVat.Text);
            po.ExcVat = Convert.ToDecimal(txnExcVat.Text);

            var incVat = Convert.ToDecimal(txnBeforeVat.Text) + Convert.ToDecimal(txnVatAmt.Text);
            po.OldIncVat = incVat;
            po.IncVat = incVat;

            decimal _vatRate = 0;
            for (int i = 0; i < grdList.RowCount; i++)
            {
                if (Convert.ToDecimal(grdList.Rows[i].Cells[6].EditedFormattedValue.ToString()) > 0)
                {
                    _vatRate = Convert.ToDecimal(grdList.Rows[i].Cells[6].EditedFormattedValue);
                    break;
                }
            }

            po.VatRate = _vatRate;

            po.VatAmt = Convert.ToDecimal(txnVatAmt.Text);
            po.Freight = 0.00m;
            po.DiscountType = "A";
            po.OldDiscount = null;

            if (pro.tbl_HQ_Promotion_Hit_Temps != null && pro.tbl_HQ_Promotion_Hit_Temps.Count > 0)
                po.Discount = pro.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt).Value.ToDecimalN2();
            else
                po.Discount = 0;

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
        }

        private void PreparePODetail(int isSave, bool editFlag = false)
        {
            bu.tbl_PODetails_PRE.Clear();

            var poDts = bu.tbl_PODetails_PRE;
            DateTime crDate = DateTime.Now;

            for (int i = 0; i < grdList.RowCount; i++)
            {
                var poDt = new tbl_PODetail_PRE();

                var prdCodeCell = grdList.Rows[i].Cells[0];
                var prdNameCell = grdList.Rows[i].Cells[2];
                var uomTypeCell = grdList.Rows[i].Cells[3];
                var orderQtyCell = grdList.Rows[i].Cells[4];
                var priceCell = grdList.Rows[i].Cells[5];
                var vatCell = grdList.Rows[i].Cells[6];
                var discountTypeCell = grdList.Rows[i].Cells[7];
                var discountAmtCell = grdList.Rows[i].Cells[8];
                var lineAmt = grdList.Rows[i].Cells[9];

                if (prdCodeCell.IsNotNullOrEmptyCell() && prdNameCell.IsNotNullOrEmptyCell() && priceCell.IsNotNullOrEmptyCell() && discountTypeCell.IsNotNullOrEmptyCell())
                {
                    decimal _unitPrice = 0;
                    if (decimal.TryParse(priceCell.Value.ToString(), out _unitPrice))
                    {
                        if (!string.IsNullOrEmpty(prdNameCell.EditedFormattedValue.ToString()) && (discountTypeCell.Value.ToString() == "N" || discountTypeCell.Value.ToString() == "A")) //(_unitPrice > 0) //05022021
                        {
                            int lineNo = poDts.Count > 0 ? Convert.ToInt32(poDts.Max(x => x.Line)) + 1 : 0;

                            poDt.DocNo = bu.tbl_POMaster_PRE.DocNo;
                            poDt.Line = Convert.ToInt16(lineNo);
                            poDt.ProductID = prdCodeCell.EditedFormattedValue.ToString();
                            poDt.ProductName = prdNameCell.EditedFormattedValue.ToString();

                            var uom = allUOM.FirstOrDefault(x => x.ProductUomName == uomTypeCell.EditedFormattedValue.ToString());
                            if (uom != null)
                            {
                                poDt.OrderUom = Convert.ToInt32(uom.ProductUomID);
                            }

                            poDt.OrderQty = Convert.ToDecimal(orderQtyCell.EditedFormattedValue);
                            poDt.ReceivedQty = Convert.ToDecimal(orderQtyCell.EditedFormattedValue); ;
                            poDt.RejectedQty = 0;
                            poDt.StockedQty = 0;

                            decimal unitCost = 0;
                            Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == poDt.OrderUom && x.ProductID == poDt.ProductID);
                            var prdPriceGroup = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList();// bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);
                            if (prdPriceGroup != null && prdPriceGroup.Count > 0)
                                unitCost = prdPriceGroup[0].BuyPrice.Value;

                            poDt.LineTotal = Convert.ToDecimal(lineAmt.EditedFormattedValue);
                            poDt.UnitCost = unitCost.ToDecimalN5();
                            poDt.UnitPrice = Convert.ToDecimal(priceCell.EditedFormattedValue);
                            poDt.VatType = Convert.ToByte(vatCell.EditedFormattedValue);

                            //var allLineDiscountType = bu.GetDiscountType();
                            //var ldt = allLineDiscountType.FirstOrDefault(x => x.DiscountTypeName == discountTypeCell.EditedFormattedValue.ToString());
                            //if (ldt != null)
                            //{
                            //    poDt.LineDiscountType = ldt.DiscountTypeCode;
                            //    poDt.LineDiscountRate = 0;
                            //    poDt.LineDiscount = Convert.ToDecimal(discountAmtCell.EditedFormattedValue);
                            //}

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
                            poDt.LineDiscountType = "N";

                            if (isSave == 1)
                                DistributeDiscountPromotion(poDt);
                            else if (isSave == 2)
                                DistributeDiscountPromotionTemp(poDt);

                            poDts.Add(poDt);

                            //if (isSave == 1)
                            //    DistributeFreePromotion(poDts, poDt);
                        }
                    }
                }
            }
        }

        private void SunDistributeFreePromotion(List<tbl_PODetail_PRE> poDts, tbl_HQ_Promotion_Hit_Temp item, string sKUGroupRewardID, int? sKUGroupRewardAmt, DateTime crDate)
        {
            var itemGroup = pro.GetHQSKUGroup(x => x.SKUGroupID == sKUGroupRewardID);
            if (itemGroup.Count > 0)
            {
                var _poDt = new tbl_PODetail_PRE();
                //poDt.CopyTo(_poDt);
                if (_poDt != null)
                {
                    var allProduct = bu.GetProduct();

                    int _lineNo = poDts.Count > 0 ? Convert.ToInt32(poDts.Max(x => x.Line)) + 1 : 0;
                    _poDt.DocNo = bu.tbl_POMaster_PRE.DocNo;
                    _poDt.ProductID = itemGroup[0].SKU_ID;

                    var freePrd = allProduct.Where(x => x.ProductID == itemGroup[0].SKU_ID).ToList(); //bu.GetProduct(x => x.ProductID == itemGroup[0].SKU_ID);
                    if (freePrd != null && freePrd.Count > 0)
                    {
                        var prd = freePrd[0];
                        _poDt.ProductName = prd.ProductName;

                        if (prd.SaleUomID == 1 || prd.SaleUomID == 2)
                            _poDt.OrderUom = 2;
                        else
                            _poDt.OrderUom = prd.SaleUomID;

                        _poDt.OrderQty = sKUGroupRewardAmt;
                        _poDt.ReceivedQty = sKUGroupRewardAmt;
                        _poDt.VatType = 0;
                        _poDt.RejectedQty = 0;
                        _poDt.StockedQty = 0;

                        _poDt.Line = Convert.ToInt16(_lineNo);
                        _poDt.LineTotal = 0;

                        var prdPrices = allProductPrice.Where(x => x.ProductUomID == prd.SaleUomID && x.ProductID == itemGroup[0].SKU_ID).ToList(); // bu.GetProductPriceGroup(x => x.ProductUomID == prd.SaleUomID && x.ProductID == itemGroup[0].SKU_ID);
                        if (prdPrices != null && prdPrices.Count > 0)
                        {
                            _poDt.LineDiscount = prdPrices[0].SellPriceVat.Value;
                            _poDt.UnitPrice = prdPrices[0].SellPriceVat.Value;
                            _poDt.UnitCost = prdPrices[0].BuyPrice.Value;
                        }
                        else
                        {
                            _poDt.LineDiscount = 0.00m;
                            _poDt.UnitPrice = 0.00m;
                            _poDt.UnitPrice = 0.00m;
                        }

                        _poDt.LineDiscountRate = 0;
                        _poDt.LineDiscountType = "F";
                        _poDt.FlagDel = false;
                        _poDt.FlagSend = false;
                        _poDt.UnitComPrice = 0;
                        _poDt.LineComTotal = 0;
                        _poDt.LineRemark = "";
                        _poDt.FreeQty = 0;
                        _poDt.FreeUom = 0;
                        _poDt.FreeUnit = 0;

                        _poDt.CustType = "";
                        _poDt.CrDate = crDate;
                        _poDt.CrUser = Helper.tbl_Users.Username;

                        _poDt.LineRemark = pro.GetHQReward(x => x.RewardID == item.RewardID)[0].RewardName;
                        bu.tbl_PODetails_PRE.Add(_poDt);
                    }
                }
            }
        }

        private void DistributeFreePromotion(List<tbl_PODetail_PRE> poDts)
        {
            DateTime crDate = DateTime.Now;

            pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
            pro.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();

            RemoveShelfItem(); //remove null shelf

            if (pro.tbl_HQ_Promotion_Hit_Temps != null && pro.tbl_HQ_Promotion_Hit_Temps.Count > 0)
            {
                List<tbl_HQ_Promotion_Hit_Temp> proHits = pro.tbl_HQ_Promotion_Hit_Temps.Where(x => !string.IsNullOrEmpty(x.SKUGroupRewardID)).ToList();

                if (proHits != null && proHits.Count > 0)
                {
                    foreach (tbl_HQ_Promotion_Hit_Temp item in proHits)
                    {
                        SunDistributeFreePromotion(poDts, item, item.SKUGroupRewardID, item.SKUGroupRewardAmt, crDate);

                        if (!string.IsNullOrEmpty(item.SKUGroupRewardID2))
                        {
                            SunDistributeFreePromotion(poDts, item, item.SKUGroupRewardID2, item.SKUGroupRewardAmt2, crDate);
                        }
                    }
                }
            }
        }

        private void DistributeDiscountPromotion(tbl_PODetail_PRE poDt)
        {
            if (pro.tbl_HQ_Promotion_Hits != null && pro.tbl_HQ_Promotion_Hits.Count > 0)
            {
                List<tbl_HQ_Promotion_Hit> proHits = new List<tbl_HQ_Promotion_Hit>();

                proHits = pro.tbl_HQ_Promotion_Hits.Where(x => x.DocNo == poDt.DocNo && string.IsNullOrEmpty(x.SKUGroupRewardID)).ToList();

                var newObj = proHits.Select(x => new PromotionHitTempModel { SKUGroupID = x.SKUGroupID, PromotionID = x.PromotionID, DisCountAmt = x.DisCountAmt }).ToList();
                SubDistributeDiscountPro(newObj, poDt);
            }
        }

        private void DistributeDiscountPromotionTemp(tbl_PODetail_PRE poDt)
        {
            pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
            pro.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();

            if (pro.tbl_HQ_Promotion_Hit_Temps != null && pro.tbl_HQ_Promotion_Hit_Temps.Count > 0)
            {
                List<tbl_HQ_Promotion_Hit_Temp> proHits = new List<tbl_HQ_Promotion_Hit_Temp>();

                proHits = pro.tbl_HQ_Promotion_Hit_Temps.Where(x => string.IsNullOrEmpty(x.SKUGroupRewardID)).ToList();

                var newObj = proHits.Select(x => new PromotionHitTempModel { SKUGroupID = x.SKUGroupID, PromotionID = x.PromotionID, DisCountAmt = x.DisCountAmt }).ToList();
                SubDistributeDiscountPro(newObj, poDt);
            }
        }

        private void SubDistributeDiscountPro(List<PromotionHitTempModel> proHits, tbl_PODetail_PRE poDt)
        {
            Dictionary<decimal, decimal> _allDiscount = new Dictionary<decimal, decimal>();
            List<string> _lineRemarkList = new List<string>();

            if (proHits != null && proHits.Count > 0)
            {
                _allDiscount = CalcTotalDiscountPro(proHits, _lineRemarkList, poDt.ProductID);

                var allHQSKUGroup = pro.GetHQSKUGroup();

                decimal totalPrdQty = 0;
                decimal totalTxnQty = 0;

                for (int i = 0; i < grdList.RowCount; i++)
                {
                    string prdID = "";
                    string uomName = "";
                    decimal _orderQty = 0.00m;
                    decimal unitPrc = 0;

                    var prdCodeCell = grdList.Rows[i].Cells[0];
                    var prdNameCell = grdList.Rows[i].Cells[2];
                    var uomTypeCell = grdList.Rows[i].Cells[3];
                    var orderQtyCell = grdList.Rows[i].Cells[4];
                    var unitPriceCell = grdList.Rows[i].Cells[5];

                    if (prdCodeCell.IsNotNullOrEmptyCell() && prdNameCell.IsNotNullOrEmptyCell())
                    {
                        prdID = prdCodeCell.EditedFormattedValue.ToString();
                        uomName = uomTypeCell.EditedFormattedValue.ToString();
                        _orderQty = Convert.ToDecimal(orderQtyCell.EditedFormattedValue);
                        unitPrc = Convert.ToDecimal(unitPriceCell.EditedFormattedValue);

                        decimal unitQty = 0;
                        if (unitPrc > 0)
                        {
                            var prdUOMSets = bu.GetProductUOMSet(allUomSet, prdID);
                            int _orderUom = 0;

                            var uom = allUOM.FirstOrDefault(x => x.ProductUomName == uomName);
                            if (uom != null)
                                _orderUom = Convert.ToInt32(uom.ProductUomID);

                            if (_orderUom == 1 && prdUOMSets != null && prdUOMSets.Count > 0)
                                unitQty = (_orderQty * prdUOMSets.First().BaseQty);
                            else
                                unitQty = _orderQty;

                            totalTxnQty += unitQty;
                        }

                        foreach (var item in proHits)
                        {
                            var prdList = allHQSKUGroup.Where(x => x.SKUGroupID == item.SKUGroupID).ToList();
                            if (prdList.Select(x => x.SKU_ID).ToList().Contains(prdID))
                            {
                                totalPrdQty += unitQty;
                            }
                        }
                    }
                }

                if (_allDiscount.Count > 0 && _lineRemarkList.Count > 0)
                {
                    decimal _lineDisount = 0.00m;

                    string prdID = poDt.ProductID;
                    decimal _orderQty = poDt.OrderQty.Value;

                    decimal _unitQty = 0;
                    var _prdUOMSets = bu.GetProductUOMSet(allUomSet, prdID);

                    if (poDt.OrderUom == 1 && _prdUOMSets != null && _prdUOMSets.Count > 0)
                        _unitQty = (_orderQty * _prdUOMSets.First().BaseQty);
                    else
                        _unitQty = _orderQty;

                    if (_allDiscount.First().Key > 0) //prd
                    {
                        var prdDis = _allDiscount.Sum(x => x.Key);
                        var prdDiscount = (prdDis / totalPrdQty) * (_unitQty);
                        _lineDisount += prdDiscount.ToDecimalN2();
                    }
                    if (_allDiscount.First().Value > 0) //txn
                    {
                        var txnDis = _allDiscount.Sum(x => x.Value);
                        var txnDiscount = (txnDis / totalTxnQty) * (_unitQty);
                        _lineDisount += txnDiscount.ToDecimalN2();
                    }

                    poDt.LineDiscount = _lineDisount.ToDecimalN2();
                    poDt.LineRemark = string.Join(",", _lineRemarkList.Distinct().ToList());
                    if (poDt.LineDiscount > 0)
                    {
                        poDt.LineDiscountType = "A";
                        poDt.LineDiscountRate = 0;
                    }
                }
            }
        }

        private Dictionary<decimal, decimal> CalcTotalDiscountPro(List<PromotionHitTempModel> proHits, List<string> _lineRemarkList, string productID)
        {
            Dictionary<decimal, decimal> ret = new Dictionary<decimal, decimal>();
            decimal _linePrdDiscount = 0;
            decimal _lineTxnDiscount = 0;
            try
            {
                foreach (var item in proHits)
                {
                    var promotions = pro.GetHQPromotion(x => x.PromotionID == item.PromotionID);

                    if (promotions != null && promotions.Count > 0)
                    {
                        var prdProItems = promotions.Where(x => x.PromotionPattern == "prd").ToList();
                        var txnProItems = promotions.Where(x => x.PromotionPattern == "txn").ToList();

                        if (prdProItems.Count > 0)
                        {
                            foreach (tbl_HQ_Promotion prdItem in prdProItems)
                            {
                                var skuGroups = pro.GetHQSKUGroup(x => x.SKU_ID == productID);

                                if (skuGroups != null && skuGroups.Count > 0)
                                {
                                    var listGroup = skuGroups.Select(x => x.SKUGroupID).ToList();
                                    if (listGroup.Contains(prdItem.SKUGroupID1))
                                    {
                                        _linePrdDiscount += item.DisCountAmt.Value;

                                        if (_lineRemarkList.All(x => x != prdItem.RewardID))
                                            _lineRemarkList.Add(prdItem.RewardID);
                                    }
                                }

                            }
                        }
                        if (txnProItems.Count > 0)
                        {
                            _lineTxnDiscount += item.DisCountAmt.Value;

                            foreach (tbl_HQ_Promotion txnItem in txnProItems)
                            {
                                if (_lineRemarkList.All(x => x != txnItem.RewardID))
                                    _lineRemarkList.Add(txnItem.RewardID);
                            }
                        }
                    }
                }

                ret.Add(_linePrdDiscount, _lineTxnDiscount);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ret;
        }

        //private void PrepareInvMovement(bool editFlag = false)
        //{
        //    bu.tbl_InvMovements.Clear();

        //    var invMms = bu.tbl_InvMovements;
        //    var poDts = bu.tbl_PODetails; //05022021 remove stock like a tablet. //bu.tbl_PODetails_PRE.Where(x => x.LineTotal != 0 || x.LineDiscountType == "F").ToList();  //for free product from promotion
        //    var po = bu.tbl_POMaster;
        //    //var prod = bu.GetProduct();
        //    //var prodGroup = bu.GetProductGroup();
        //    //var prodSubGroup = bu.GetProductSubGroup();

        //    DateTime crDate = DateTime.Now;

        //    foreach (var poDt in poDts)
        //    {
        //        var invMm = new tbl_InvMovement();

        //        invMm.ProductID = poDt.ProductID;
        //        invMm.ProductName = poDt.ProductName;
        //        invMm.RefDocNo = poDt.DocNo;
        //        invMm.TrnDate = Helper.tbl_Users.RoleID == 10 ? dtpDocDate.Value.ToDateTimeFormat() : crDate.ToDateTimeFormat(); //31012021
        //        invMm.TrnType = "S";
        //        invMm.DocTypeCode = po.DocTypeCode;
        //        invMm.WHID = po.WHID;
        //        invMm.FromWHID = "";
        //        invMm.ToWHID = "";

        //        decimal unitQty = 0;

        //        var prdUOMSets = bu.GetProductUOMSet(allUomSet, poDt.ProductID);
        //        if (prdUOMSets != null && prdUOMSets.Count > 0)
        //        {
        //            //if (poDt.OrderUom != 2) //06042021 by sailom.k
        //            var minUomID = 2;
        //            minUomID = allUomSet.GetMinUOM(poDt);

        //            if (poDt.OrderUom != minUomID)
        //                unitQty = (poDt.ReceivedQty.Value * prdUOMSets[0].BaseQty);
        //            else
        //                unitQty = poDt.ReceivedQty.Value;
        //        }
        //        else
        //        {
        //            unitQty = poDt.ReceivedQty.Value;
        //        }

        //        invMm.TrnQtyIn = 0;
        //        invMm.TrnQtyOut = unitQty;
        //        invMm.TrnQty = -invMm.TrnQtyOut;
        //        invMm.CrDate = crDate;

        //        if (editFlag)
        //        {
        //            invMm.EdDate = crDate;
        //            invMm.TrnType = ddlDocStatus.SelectedValue.ToString() == "5" ? "X" : "S";
        //        }

        //        var prodItem = allProduct.FirstOrDefault(x => x.ProductID == poDt.ProductID);
        //        var prodGroupItem = allProdGroup.FirstOrDefault(x => x.ProductGroupID == prodItem.ProductGroupID);
        //        var prodSubGroupItem = allProdSubGroup.FirstOrDefault(x => x.ProductSubGroupID == prodItem.ProductSubGroupID);

        //        invMm.ProductGroupCode = prodGroupItem.ProductGroupCode;
        //        invMm.ProductGroupName = prodGroupItem.ProductGroupName;
        //        invMm.ProductSubGroupCode = prodSubGroupItem.ProductSubGroupCode;
        //        invMm.ProductSubGroupName = prodSubGroupItem.ProductSubGroupName;
        //        invMm.FlagSend = false;

        //        invMms.Add(invMm);

        //    }
        //}

        //private void PrepareInvTransaction()
        //{
        //    bu.tbl_InvTransactions.Clear();

        //    var invTrs = bu.tbl_InvTransactions;
        //    var poDts = bu.tbl_PODetails; //05022021 remove stock like a tablet. //bu.tbl_PODetails_PRE.Where(x => x.LineTotal != 0 || x.LineDiscountType == "F").ToList();  //for free product from promotion
        //    var po = bu.tbl_POMaster;
        //    //var prod = bu.GetProduct();
        //    //var prodGroup = bu.GetProductGroup();
        //    //var prodSubGroup = bu.GetProductSubGroup();
        //    var comp = bu.GetCompany();

        //    DateTime crDate = DateTime.Now;

        //    foreach (var poDt in poDts)
        //    {
        //        var invtr = new tbl_InvTransaction();
        //        invtr.ProductID = poDt.ProductID;
        //        invtr.RefDocNo = poDt.DocNo;
        //        invtr.RefLineID = poDt.Line;
        //        invtr.TrnDate = crDate.ToDateTimeFormat();
        //        invtr.BranchFrom = comp.CompanyCode;
        //        invtr.WHFrom = po.WHID;
        //        invtr.BranchTo = comp.CompanyCode;
        //        invtr.WHTo = comp.CompanyCode;
        //        invtr.TrnType = "S";
        //        invtr.DocTypeCode = po.DocTypeCode;
        //        invtr.TrnUomID = poDt.OrderUom;
        //        invtr.TrnUom = null;
        //        invtr.BringQty = 0;

        //        decimal unitQty = 0;

        //        var prdUOMSets = bu.GetProductUOMSet(allUomSet, poDt.ProductID);
        //        if (prdUOMSets != null && prdUOMSets.Count > 0)
        //        {
        //            //if (poDt.OrderUom != 2) //06042021 by sailom.k
        //            var minUomID = 2;
        //            minUomID = allUomSet.GetMinUOM(poDt);

        //            if (poDt.OrderUom != minUomID)
        //                unitQty = (poDt.ReceivedQty.Value * prdUOMSets[0].BaseQty);
        //            else
        //                unitQty = poDt.ReceivedQty.Value;
        //        }
        //        else
        //        {
        //            unitQty = poDt.ReceivedQty.Value;
        //        }

        //        decimal unitCost = 0;
        //        Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == poDt.OrderUom && x.ProductID == poDt.ProductID);
        //        var prdPriceGroup = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList(); // bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);
        //        if (prdPriceGroup != null && prdPriceGroup.Count > 0)
        //            unitCost = prdPriceGroup[0].BuyPrice.Value;

        //        invtr.TrnQtyIn = 0;
        //        invtr.TrnQtyOut = poDt.ReceivedQty.Value;
        //        invtr.TrnQty = -unitQty;
        //        invtr.RemainQty = 0;
        //        invtr.UnitPrice = poDt.UnitPrice;
        //        invtr.UnitCost = unitCost.ToDecimalN5(); //(poDt.LineTotal.Value / unitQty).ToDecimalN5();
        //        invtr.LineDiscountType = poDt.LineDiscountType;
        //        invtr.LineDiscount = poDt.LineDiscount;
        //        invtr.TrnVat = poDt.VatType;
        //        invtr.TrnValue = -poDt.LineTotal.Value;
        //        invtr.TrnTotal = po.Amount.Value;
        //        invtr.CostValue = -unitCost;
        //        invtr.Supplier = Convert.ToInt32(po.SupplierID);
        //        invtr.Customer = po.CustomerID;
        //        invtr.RefSONo = null;
        //        invtr.CustPONo = po.CustPONo;
        //        invtr.CustInvoiceNo = po.CustInvNO;
        //        invtr.Salesperson = Convert.ToInt32(comp.CompanyCode);
        //        invtr.SalAreaID = po.SalAreaID;
        //        invtr.ModifiedDate = crDate;
        //        invtr.FlagDel = false;
        //        invtr.FlagSend = false;
        //        //invtr.LineDiscount = poDt.LineDiscount;
        //        //invtr.LineDiscountType = poDt.LineDiscountType;

        //        invTrs.Add(invtr);
        //    }
        //}

        //private void PrepareInvWarehouse(bool editFlag = false)
        //{
        //    bu.tbl_InvWarehouses.Clear();

        //    var invWhs = bu.tbl_InvWarehouses;
        //    var poDts = bu.tbl_PODetails; //05022021 remove stock like a tablet. //bu.tbl_PODetails_PRE.Where(x => x.LineTotal != 0 || x.LineDiscountType == "F").ToList();  //for free product from promotion
        //    var po = bu.tbl_POMaster;

        //    DateTime crDate = DateTime.Now;

        //    foreach (var poDt in poDts)
        //    {
        //        var invWh = new tbl_InvWarehouse();

        //        invWh.ProductID = poDt.ProductID;
        //        invWh.WHID = po.WHID;
        //        //invWh.QtyOnHand = 0;

        //        decimal unitQty = 0;

        //        var prdUOMSets = bu.GetProductUOMSet(allUomSet, poDt.ProductID);
        //        if (prdUOMSets != null && prdUOMSets.Count > 0)
        //        {
        //            //if (poDt.OrderUom != 2) //06042021 by sailom.k
        //            var minUomID = 2;
        //            minUomID = allUomSet.GetMinUOM(poDt);

        //            if (poDt.OrderUom != minUomID)
        //                unitQty = (poDt.ReceivedQty.Value * prdUOMSets[0].BaseQty);
        //            else
        //                unitQty = poDt.ReceivedQty.Value;
        //        }
        //        else
        //        {
        //            unitQty = poDt.ReceivedQty.Value;
        //        }

        //        SetQtyOnHand(invWh, unitQty, poDt.ProductID, po.WHID, editFlag);

        //        invWh.QtyOnOrder = 0;
        //        invWh.QtyOnBackOrder = 0;
        //        invWh.QtyInTransit = 0;
        //        invWh.QtyOutTransit = 0;
        //        invWh.QtyOnReject = 0;
        //        invWh.MinimumQty = 0;
        //        invWh.MaximumQty = 0;
        //        invWh.ReOrderQty = 0;
        //        invWh.CrDate = crDate;
        //        invWh.CrUser = Helper.tbl_Users.Username;

        //        if (editFlag)
        //        {
        //            invWh.EdDate = crDate;
        //            invWh.EdUser = Helper.tbl_Users.Username;
        //        }

        //        invWh.FlagDel = false;
        //        invWh.FlagSend = false;

        //        if (invWhs.Any(x => x.ProductID == poDt.ProductID && x.WHID == po.WHID))
        //        {
        //            var whItem = invWhs.FirstOrDefault(x => x.ProductID == poDt.ProductID && x.WHID == po.WHID);
        //            whItem.QtyOnHand += invWh.QtyOnHand;
        //        }
        //        else
        //            invWhs.Add(invWh);
        //    }
        //}

        private void PreparePromotion()
        {
            if (pro.tbl_HQ_Promotion_Hit_Temps != null && pro.tbl_HQ_Promotion_Hit_Temps.Count > 0)
            {
                pro.tbl_HQ_Promotion_Hits = new List<tbl_HQ_Promotion_Hit>();
                var _pro = new List<tbl_HQ_Promotion_Hit>();

                DateTime crDate = DateTime.Now;

                foreach (var pItem in pro.tbl_HQ_Promotion_Hit_Temps)
                {
                    var p = new tbl_HQ_Promotion_Hit();

                    p.DocNo = bu.tbl_POMaster.DocNo;
                    p.PromotionID = pItem.PromotionID;
                    p.RoundHit = pItem.RoundHit;
                    p.SKUGroupID = pItem.SKUGroupID;
                    p.DisCountAmt = pItem.DisCountAmt;
                    //p.SKUGroupID = pItem.
                    p.ShelfID = pItem.ShelfID;
                    p.SKUGroupRewardID = pItem.SKUGroupRewardID;
                    p.SKUGroupRewardAmt = pItem.SKUGroupRewardAmt;
                    p.SKUGroupRewardID2 = pItem.SKUGroupRewardID2;
                    p.SKUGroupRewardAmt2 = pItem.SKUGroupRewardAmt2;
                    p.RewardID = pItem.RewardID;
                    p.CrDate = crDate;
                    p.CrUser = Helper.tbl_Users.Username;
                    p.FlagDel = false;
                    p.FlagSend = false;

                    _pro.Add(p);
                }

                pro.tbl_HQ_Promotion_Hits = _pro;
            }

        }

        private void PreparePromotionTemp(List<PromotionRuleModel> proList)
        {
            pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();

            var _pro = new List<tbl_HQ_Promotion_Hit_Temp>();
            DateTime crDate = DateTime.Now;

            foreach (var pItem in proList)
            {
                var p = new tbl_HQ_Promotion_Hit_Temp();

                p.DocNo = bu.tbl_IVMaster.DocNo;
                p.PromotionID = pItem.PromotionID;
                p.RoundHit = pItem.RoundHit;
                p.SKUGroupID = pItem.ProductGroupID;
                p.DisCountAmt = pItem.DisCountAmt;
                p.SKUGroupRewardID = pItem.PruductGroupRewardID;
                p.SKUGroupRewardAmt = pItem.PruductGroupRewardAmt;
                p.SKUGroupRewardID2 = pItem.PruductGroupRewardID2;
                p.SKUGroupRewardAmt2 = pItem.PruductGroupRewardAmt2;
                p.RewardID = pItem.RewardID;
                p.CrDate = crDate;
                p.CrUser = Helper.tbl_Users.Username;
                p.FlagDel = false;
                p.FlagSend = false;

                _pro.Add(p);
            }

            pro.tbl_HQ_Promotion_Hit_Temps = _pro;
        }

        private void PrepareArCustomerShelf()
        {
            var cDate = DateTime.Now;
            var po = bu.tbl_POMaster;
            if (po != null)
            {
                bu.tbl_ArCustomerShelfs = new List<tbl_ArCustomerShelf>();
                var proShelf = pro.tbl_HQ_Promotion_Hits.Where(x => !string.IsNullOrEmpty(x.ShelfID)).ToList();

                foreach (var item in proShelf)
                {
                    var obj = new tbl_ArCustomerShelf();
                    obj.CustomerID = po.CustomerID;
                    obj.ShelfID = item.ShelfID;
                    obj.WHID = po.WHID;

                    var skuGroupRewardID = pro.GetHQSKUGroup(x => x.SKUGroupID == item.SKUGroupRewardID);
                    if (skuGroupRewardID != null && skuGroupRewardID.Count > 0)
                    {
                        obj.ProductID = skuGroupRewardID[0].SKU_ID;
                    }

                    obj.FlagNew = true;
                    obj.FlagEdit = false;
                    obj.CrDate = cDate;
                    obj.CrUser = item.CrUser;
                    obj.EdDate = null;
                    obj.EdUser = null;
                    obj.FlagDel = false;
                    obj.FlagSend = false;

                    bu.tbl_ArCustomerShelfs.Add(obj);
                }
            }
        }

        private void PrepareInvMaster(tbl_POMaster po)
        {
            bu.tbl_IVMaster = new tbl_IVMaster();

            var bwh = bu.GetAllBranchWarehouse(x => !x.FlagDel && x.WHType == 1);

            var iv = bu.tbl_IVMaster;

            iv.DocNo = txtCustInvNO.Text;
            iv.RevisionNo = 0;
            iv.DocTypeCode = "V";
            iv.DocStatus = po.DocStatus;
            iv.DocDate = po.DocDate;
            iv.DocRef = po.DocNo;
            iv.StatusInOut = null;
            iv.SupplierID = "0";
            iv.SuppName = "";
            iv.WHID = po.WHID;

            if (bwh != null && bwh.Count > 0)
            {
                string saleEmpID = bwh.First(x => x.WHID == po.WHID).SaleEmpID;
                iv.EmpID = saleEmpID;
                iv.SaleEmpID = saleEmpID;
            }
            else
            {
                iv.EmpID = "";
                iv.SaleEmpID = "";
            }

            iv.SalAreaID = po.SalAreaID;
            iv.Address = po.Address;
            iv.ContactName = po.ContactName;
            iv.ContactTel = po.ContactTel;
            iv.Shipto = po.Shipto;
            iv.CreditDay = po.CreditDay;
            iv.CustType = po.CustType;
            iv.DueDate = po.DueDate;
            iv.CustomerID = po.CustomerID;
            iv.CustName = po.CustName;
            iv.CustInvNO = "";
            iv.CustInvDate = null;
            iv.CustPODate = po.CustPODate;
            iv.CustPONo = po.DocRef;
            iv.Remark = "สร้างใบกำกับจากการใบกำกับภาษีอย่างย่อ : " + po.DocNo;
            iv.Comment = po.Comment;
            iv.OldAmount = 0;
            iv.Amount = po.Amount;
            iv.OldExcVat = 0;
            iv.ExcVat = po.ExcVat;
            iv.OldIncVat = 0;
            iv.IncVat = po.IncVat;
            iv.VatRate = po.VatRate;
            iv.VatAmt = po.VatAmt;
            iv.Freight = 0.00m;
            iv.DiscountType = "";
            iv.OldDiscount = null;
            iv.Discount = bu.tbl_POMaster_PRE.Discount; //pro.GetAllData().Sum(x => x.DisCountAmt);
            iv.TotalDue = po.TotalDue;
            iv.ApprovedBy = null;
            iv.ApprovedDate = null;
            iv.PayType = 0;
            iv.CrDate = DateTime.Now;
            iv.CrUser = Helper.tbl_Users.Username;
            iv.EdDate = null;
            iv.EdUser = Helper.tbl_Users.Username;
            iv.FlagDel = false;
            iv.FlagSend = false;
            iv.OldTotalCom = 0.00m;
            iv.TotalCom = 0.00m;
            iv.CNType = 0;

            Func<tbl_Branch, bool> whFunc = (x => x.BranchCode == txtWHCode.Text.Split('V')[0]);
            var wh = bu.GetBranch(whFunc);
            if (wh != null && wh.Count > 0)
                iv.FromWHCode = wh[0].VanCode;

            iv.FromLocCode = "VC4STV" + iv.WHID.Split('V')[1].ToString();
            iv.ToWHCode = null;
            iv.ToLocCode = null;
        }

        private void PrepareInvDetail(List<tbl_PODetail> poDts)
        {
            bu.tbl_IVDetails = new List<tbl_IVDetail>();
            //var allUomSet = bu.GetUOMSet();
            var allProduct = bu.GetProduct();
            //var allProductPrice = bu.GetProductPriceGroup();

            var ivDts = bu.tbl_IVDetails;
            DateTime crDate = DateTime.Now;

            foreach (tbl_PODetail _podt in poDts)
            {
                var ivDt = new tbl_IVDetail();
                ivDt.DocNo = bu.tbl_IVMaster.DocNo;
                ivDt.Line = _podt.Line;
                ivDt.ProductID = _podt.ProductID;
                ivDt.ProductName = _podt.ProductName;

                //if (_podt.OrderUom == 1 || _podt.OrderUom == 2)
                //    ivDt.OrderUom = 2;// _podt.OrderUom;
                //else
                //    ivDt.OrderUom = _podt.OrderUom;

                //decimal unitQty = 0;

                //var prdUOMSets = bu.GetProductUOMSet(ivDt.ProductID);
                //if (_podt.OrderUom == 1 && prdUOMSets != null && prdUOMSets.Count > 0)
                //    unitQty = (_podt.OrderQty.Value * prdUOMSets.First().BaseQty);
                //else
                //    unitQty = _podt.OrderQty.Value;

                decimal unitQty = 0;

                var prdUOMSets = allUomSet.Where(x => x.ProductID == _podt.ProductID).ToList();
                if (_podt.OrderUom == 1 && prdUOMSets != null && prdUOMSets.Count > 0)
                    unitQty = (_podt.OrderQty.Value * prdUOMSets.First().BaseQty);
                else
                    unitQty = _podt.OrderQty.Value;

                int tmpOrderUom = _podt.OrderUom;

                var prd = allProduct.FirstOrDefault(x => x.ProductID == _podt.ProductID);
                if (prd != null)
                {
                    var saleUom = allUomSet.FirstOrDefault(x => x.ProductID == prd.ProductID && x.UomSetID == prd.SaleUomID);
                    if (saleUom != null)
                        tmpOrderUom = saleUom.UomSetID; //25012021
                }

                ivDt.OrderUom = tmpOrderUom; //25012021

                ivDt.OrderQty = unitQty;
                ivDt.ReceivedQty = unitQty;

                decimal unitPrice = 0;
                Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == tmpOrderUom && x.ProductID == _podt.ProductID);
                var prdPriceGroup = allProductPrice.FirstOrDefault(tbl_ProductPriceGroupPre); //29012021
                if (prdPriceGroup != null)
                    unitPrice = prdPriceGroup.SellPriceVat.Value;

                ivDt.UnitPrice = unitPrice;

                ivDt.RejectedQty = 0;
                ivDt.StockedQty = 0;
                ivDt.LineTotal = _podt.LineTotal;
                ivDt.UnitCost = 0;
                ivDt.VatType = _podt.VatType;
                ivDt.LineDiscountType = _podt.LineDiscountType;
                ivDt.LineDiscount = _podt.LineDiscount;
                ivDt.CustType = _podt.CustType;
                ivDt.CrDate = crDate;
                ivDt.CrUser = Helper.tbl_Users.Username;
                ivDt.EdDate = null;
                ivDt.EdUser = Helper.tbl_Users.Username;
                ivDt.FlagDel = false;
                ivDt.FlagSend = false;
                ivDt.UnitComPrice = 0;
                ivDt.LineComTotal = 0;

                ivDts.Add(ivDt);
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
                    invWh.QtyOnHand = -unitQty;
                }
            }
        }

        private void Save()
        {
            try
            {
                if (!ValidateSave())
                    return;

                string cfMsg = "";
                string title = "";

                string docno = string.Empty;
                bool editFlag = true;
                int ret = 0;

                bu = new TabletSales();

                if (ddlDocStatus.SelectedValue.ToString() == "5") //cancel doc
                {
                    cfMsg = "ต้องการยกเลิกเอกสารใช่หรือไม่?";
                    title = "ยืนยันการยกเลิก!!";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    docno = txdDocNo.Text;
                    bu.tbl_POMaster_PRE = bu.GetPOMasterPRE(docno);
                    bu.tbl_POMaster_PRE.DocStatus = ddlDocStatus.SelectedValue.ToString();

                    ret = bu.UpdateData(docTypeCode);
                }
                else //save to PO
                {
                    cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                    title = "ยืนยันการบันทึก!!";
                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    bool checkEditMode = bu.CheckExistsPO_PRE(txdDocNo.Text);
                    if (checkEditMode)
                    {
                        docno = txdDocNo.Text;
                        editFlag = true;
                    }
                    else
                    {
                        docno = txdDocNo.Text;
                        editFlag = false;
                        //bu.PrepareDocRunning(docTypeCode);
                    }

                    CalcPromotion(true);

                    //ValidateInputShelf();
                    PreparePOMaster(editFlag);
                    PreparePromotion();
                    PreparePODetail(1, editFlag); //Calc Pro

                    DistributeFreePromotion(bu.tbl_PODetails_PRE);

                    //PrepareInvMovement(editFlag);
                    //PrepareInvTransaction(); //Calc Pro
                    //PrepareInvWarehouse();

                    ret = bu.UpdateData(docTypeCode);

                    if (ret == 1 && pro.tbl_HQ_Promotion_Hits != null && pro.tbl_HQ_Promotion_Hits.Count > 0)
                    {
                        pro.tbl_HQ_Promotion_Hits = new List<tbl_HQ_Promotion_Hit>();
                        pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                        pro.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();

                        RemoveShelfItem();

                        PreparePromotion();
                        PrepareArCustomerShelf();

                        ret = pro.RemoveTempData();
                        if (ret == 1)
                        {
                            ret = pro.RemoveData();
                            if (ret == 1)
                                ret = pro.UpdateData();
                            if (ret == 1)
                                ret = bu.UpdateCustomerShelf();
                        }
                    }

                    //if (ret == 1) //Remove PO Pre-Order
                    //{
                    //    bu.tbl_POMaster_PRE = bu.GetPOMasterPRE(docno);
                    //    bu.tbl_POMaster_PRE.FlagDel = true;

                    //    bu.tbl_PODetails_PRE = bu.GetPODetailsPRE(docno);
                    //    foreach (var pdt_preItem in bu.tbl_PODetails_PRE)
                    //    {
                    //        pdt_preItem.FlagDel = true;
                    //    }

                    //    ret = bu.UpdateData(docTypeCode);
                    //}
                }

                if (ret == 1)
                {
                    //this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

                    //btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, txdDocNo.Text);

                    //txdDocNo.Text = docno;

                    grdList.CellContentClick -= grdList_CellContentClick;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    ddlDocStatus.SelectedValue.ToString().CheckCancelDoc(false, btnAdd, btnCopy, btnEdit);
                    //CheckCancelDoc(ddlDocStatus.SelectedValue.ToString());

                    //btnUpdateAddress.Enabled = true;
                    //btnGenCustIVNo.Enabled = true;
                    btnReCalc.Enabled = false;
                    //btnShowPromotion.Enabled = false;

                    //BindVanSalesData(txdDocNo.Text, "IV2");

                    sendDate = dtpDocDate.Value;
                    tabPOMasterPre.SelectedIndex = 0;
                    //btnSearchPO.PerformClick();
                    btnCancel.PerformClick();
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

        private void ValidateInputShelf()
        {
            var allProduct = bu.GetProduct();

            bool isValidateShelf = true;
            var proFreeList = pro.tbl_HQ_Promotion_Hit_Temps.Where(x => !string.IsNullOrEmpty(x.SKUGroupRewardID)).ToList();
            foreach (var proFree in proFreeList)
            {
                var skuGroups = pro.GetHQSKUGroup(x => x.SKUGroupID == proFree.SKUGroupRewardID).ToList();
                foreach (var g in skuGroups)
                {
                    var prd = allProduct.Where(x => x.ProductID == g.SKU_ID).ToList(); // bu.GetProduct(x => x.ProductID == g.SKU_ID).ToList();
                    if (prd != null && prd.Count > 0)
                    {
                        bool isShelf = prd.Any(x => x.Flavour == "ชั้นวาง" || x.ProductName.Contains("ชั้นวาง"));

                        if (string.IsNullOrEmpty(proFree.ShelfID) && isShelf)
                        {
                            isValidateShelf = false;
                            break;
                        }
                    }
                }
            }

            if (!isValidateShelf)
            {
                var message = "กรุณากรอกเลขที่ Shelf !!!";
                message.ShowWarningMessage();
            }
        }

        private void RemoveShelfItem()
        {
            var allProduct = bu.GetProduct();

            var proFreeList = pro.tbl_HQ_Promotion_Hit_Temps.Where(x => !string.IsNullOrEmpty(x.SKUGroupRewardID)).ToList();
            foreach (var proFree in proFreeList)
            {
                var skuGroups = pro.GetHQSKUGroup(x => x.SKUGroupID == proFree.SKUGroupRewardID).ToList();
                foreach (var g in skuGroups)
                {
                    var prd = allProduct.Where(x => x.ProductID == g.SKU_ID).ToList(); // bu.GetProduct(x => x.ProductID == g.SKU_ID).ToList();
                    if (prd != null && prd.Count > 0)
                    {
                        Dictionary<tbl_HQ_Promotion_Hit_Temp, bool> validateShelf = new Dictionary<tbl_HQ_Promotion_Hit_Temp, bool>();
                        bool isRemove = false;

                        var _cust = new tbl_ArCustomerShelf().Select(x => x.ProductID == g.SKU_ID && x.FlagDel == false && x.CustomerID == txtCustomerCode.Text).ToList();
                        if (_cust != null && _cust.Count > 0)
                        {
                            var _custGroup = _cust.GroupBy(x => new { x.CustomerID, x.ProductID }).Select(a => a.First()).ToList();

                            foreach (var item in _custGroup)
                            {
                                var cust = _cust.Where(x => x.ProductID == item.ProductID).ToList();
                                if (cust != null && cust.Count > 0)
                                {
                                    if (cust.Count >= 2)
                                    {
                                        validateShelf.Add(proFree, false);
                                        break;
                                    }
                                }
                            }

                            if (validateShelf.Any(x => !x.Value))
                                isRemove = true;
                        }

                        bool isShelf = prd.Any(x => x.Flavour == "ชั้นวาง" || x.ProductName.Contains("ชั้นวาง"));

                        if (isRemove && string.IsNullOrEmpty(proFree.ShelfID) && isShelf)
                            isRemove = true;

                        if ((isRemove && isShelf) || proFree.ShelfID == "000")
                            pro.tbl_HQ_Promotion_Hit_Temps.Remove(proFree);
                    }
                }
            }
        }

        //private void SaveIV()
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(txtCustInvNO.Text) && !string.IsNullOrEmpty(txdDocNo.Text))
        //        {
        //            string msg = "ใบกำกับภาษีถูกสร้างไปแล้ว!!!";
        //            msg.ShowInfoMessage();
        //            return;
        //        }

        //        bool checkEditMode = bu.CheckExistsPO(txdDocNo.Text);

        //        if (checkEditMode)
        //        {
        //            string cfMsg = "ต้องการสร้างใบกำกับภาษีใช่หรือไม่?";
        //            string title = "ยืนยันการสร้าง!!";
        //            if (!cfMsg.ConfirmMessageBox(title))
        //                return;

        //            List<tbl_PODetail> temptbl_PODetails = new List<tbl_PODetail>();
        //            temptbl_PODetails = SerializeHelper.CloneObject<List<tbl_PODetail>>(bu.tbl_PODetails);

        //            tbl_POMaster temptbl_POMaster = new tbl_POMaster();
        //            temptbl_POMaster = SerializeHelper.CloneObject<tbl_POMaster>(bu.tbl_POMaster);

        //            bu = new TabletSales();

        //            txtCustInvNO.Text = bu.GenDocNo("V", txtWHCode.Text);

        //            if (!string.IsNullOrEmpty(txtCustInvNO.Text))
        //            {
        //                temptbl_POMaster.CustInvNO = txtCustInvNO.Text;
        //                temptbl_POMaster.FlagSend = false;
        //            }

        //            bu.tbl_POMaster = temptbl_POMaster;
        //            PrepareInvMaster(bu.tbl_POMaster);

        //            PrepareInvDetail(temptbl_PODetails);

        //            int ret = bu.UpdateData();

        //            if (ret == 1)
        //            {
        //                bool result = bu.UpdateCustomerSAPCode(txtCustomerCode.Text); //update customer SAP code
        //                if (result)
        //                {
        //                    string msg = "สร้างใบกำกับภาษีเรียบร้อยแล้ว";
        //                    msg.ShowInfoMessage();
        //                }
        //            }
        //            else
        //            {
        //                string msg = "สร้างใบกำกับภาษีไม่สำเร็จ";
        //                msg.ShowInfoMessage();
        //                txtCustInvNO.Text = "";
        //            }
        //        }
        //        else
        //        {
        //            string msg = "กรุณาบันทึกเอกสารอย่างย่อ ก่อนสร้างใบกำกับภาษี!!!";
        //            msg.ShowInfoMessage();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.WriteLog(this.GetType());

        //        string msg = ex.Message;
        //        msg.ShowErrorMessage();
        //    }
        //}

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

        public void SetDefaultGridViewEventStock(DataGridView grd)
        {
            grd.RowPostPaint -= new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(grdPOMstList_RowPostPaint);
            grd.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(grdPOMstList_RowPostPaint);
        }

        public void SetDefaultGridViewEventMst(DataGridView grd)
        {
            grd.CellDoubleClick -= new System.Windows.Forms.DataGridViewCellEventHandler(grdPOMstList_CellDoubleClick);
            grd.RowPostPaint -= new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(grdPOMstList_RowPostPaint);

            grd.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(grdPOMstList_CellDoubleClick);
            grd.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(grdPOMstList_RowPostPaint);
        }

        public void SetDefaultGridViewEventPO(DataGridView grd)
        {
            grd.CellDoubleClick -= new System.Windows.Forms.DataGridViewCellEventHandler(grdPO_CellDoubleClick);
            grd.RowPostPaint -= new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(grdPO_RowPostPaint);

            grd.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(grdPO_CellDoubleClick);
            grd.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(grdPO_RowPostPaint);
        }

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();
            var allProduct = bu.GetProduct();

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
                if (!dtpDocDate.ValidateEndDay(bu))
                {
                    string message = "ระบบปิดวันไปแล้ว ไม่สามารถเลือกวันที่นี้ได้ !!!";
                    message.ShowWarningMessage();
                    ret = false;
                }
            }


            if (ret) //validate customer shelf
            {
                var _cust = new tbl_ArCustomerShelf().Select(x => x.FlagDel == false && x.CustomerID == txtCustomerCode.Text).ToList();
                var _custGroup = _cust.GroupBy(x => new { x.CustomerID, x.ProductID }).Select(a => a.First()).ToList();
                bool validateShelf = true;

                foreach (var item in _custGroup)
                {
                    var cust = _cust.Where(x => x.ProductID == item.ProductID).ToList();
                    if (cust != null && cust.Count > 0)
                    {
                        if (cust.Count >= 2)
                        {
                            validateShelf = false;
                            break;
                        }
                    }
                }

                if (!validateShelf)
                {
                    string message = "ร้านค้านี้ได้ Shelf ครบตามจำนวนที่กำหนดแล้ว ห้ามแถม Shelf ให้ลูกค้าอีก !!!";
                    message.ShowWarningMessage();
                }
            }

            if (ret)
            {
                errList.SetErrMessageList(txtCustomerCode, lblCustomerCode);
                errList.SetErrMessageList(txtBillTo, lblBillTo);
                errList.SetErrMessageList(txtContact, lblContact);
                errList.SetErrMessageList(txtEmpCode, lblEmpCode);
                errList.SetErrMessageList(txtWHCode, lblWHCode);

                if (errList.Count == 0)
                {
                    Func<tbl_ArCustomer, bool> func = (x => x.CustomerCode == txtCustomerCode.Text);
                    var sup = bu.GetCustomer(func);
                    if (sup == null)
                    {
                        string t = lblCustomerCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtCustomerCode.ErrorTextBox();
                    }
                    if (sup.Count == 0) //edit by sailom 21-06-2021
                    {
                        string message = "ไม่พบข้อมูลร้านค้านี้ในระบบ!!!";
                        errList.Add(message);
                        txtCustomerCode.ErrorTextBox();
                    }

                    Func<tbl_BranchWarehouse, bool> whFunc = (x => x.WHID == txtWHCode.Text);
                    var wh = bu.GetBranchWarehouse(whFunc);
                    if (wh == null)
                    {
                        string t = lblWHCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtWHCode.ErrorTextBox();
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
                    //var allProduct = bu.GetProduct();
                    ret = grdList.ValiadteDataGridView(allProduct, 0, 3, 4, 5, bu, "", false);

                    if (ret)
                    {
                        List<bool> checkEmptyCell = new List<bool>();
                        List<bool> checkPrdCode = new List<bool>();
                        List<bool> checkQty = new List<bool>();
                        List<bool> checkPrice = new List<bool>();

                        var validateRLList = new List<ValidateRL>();

                        if (grdList.RowCount > 0)
                        {
                            for (int i = 0; i < grdList.RowCount; i++)
                            {
                                var prdCodeCell = grdList.Rows[i].Cells[0];
                                var qtyCell = grdList.Rows[i].Cells[4];
                                var uomCell = grdList.Rows[i].Cells[3];

                                if (prdCodeCell.IsNotNullOrEmptyCell() && qtyCell.IsNotNullOrEmptyCell()) //check over recieve
                                {
                                    decimal qtyValue = 0;
                                    if (decimal.TryParse(qtyCell.EditedFormattedValue.ToString(), out qtyValue))
                                    {
                                        if (qtyValue > 0)
                                        {
                                            var productID = prdCodeCell.EditedFormattedValue.ToString();
                                            var productUomName = uomCell.EditedFormattedValue.ToString();
                                            Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => x.ProductUomName == productUomName);
                                            var prdUOMs = allUOM.Where(tbl_ProductUomPre).ToList();

                                            decimal unitQty = 0;

                                            var prdUOMSets = bu.GetProductUOMSet(allUomSet, productID);
                                            if (prdUOMSets != null && prdUOMSets.Count > 0 && prdUOMs != null && prdUOMs.Count > 0)
                                            {
                                                if (prdUOMs[0].ProductUomID == prdUOMSets[0].UomSetID) //if (prdUOMs[0].ProductUomID != 2) //15022021
                                                    unitQty = (qtyValue * prdUOMSets[0].BaseQty);
                                                else
                                                    unitQty = qtyValue;
                                            }
                                            else
                                                unitQty = qtyValue;

                                            string mianStock = bu.GetBranch().First().BranchID + "1000";
                                            var invWhItem = bu.GetStockMovement(productID, mianStock); // bu.GetInvWarehouse(productID, txtWHCode.Text);
                                            decimal whQty = 0;

                                            if (invWhItem != null && invWhItem.Count > 0)
                                                whQty = invWhItem.Sum(x => x.TrnQty); //invWhItem[0].QtyOnHand;

                                            if (unitQty > whQty)
                                            {
                                                validateRLList.Add(new ValidateRL
                                                {
                                                    ProductID = productID,
                                                    StockQty = whQty,
                                                    InputQty = unitQty
                                                });

                                            }
                                        }
                                    }
                                }
                            }

                            if (validateRLList.Count > 0)
                            {
                                var message = "";
                                var tempMsg = "";

                                tempMsg += string.Format("ไม่สามารถทำรายการสินค้านี้ได้ \n\n");
                                foreach (var item in validateRLList)
                                {
                                    tempMsg += string.Format("--> เนื่องจากสินค้า {0} ใน Stock มีแค่ {1} หน่วยเล็ก \n", item.ProductID, item.StockQty.ToStringN2());
                                }
                                message = tempMsg;

                                message.ShowWarningMessage();
                            }
                        }
                    }

                }
            }

            return ret;
        }

        private void btnSearchDoc_Click(object sender, EventArgs e)
        {
            this.OpenIVDocPopup("ใบกำกับสินค้า/กำกับภาษี", "PreOrder");
        }

        private void btnSearchWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchBWHControls, "เลือกคลังสินค้า", whFunc);
        }

        private void btnSearchEmpCode_Click(object sender, EventArgs e)
        {
            this.OpenEmployeeNamePopup(searchEmpControls, "เลือกพนักงาน", empFunc);
        }

        private void btnSearchCust_Click(object sender, EventArgs e)
        {
            this.OpenCustomerPrePopup(searchCustControls, "เลือกลูกค้า", x => x.FlagDel == false);

            FilterSaleArea(txtCustomerCode.Text);
        }

        private void btnCustInfo_Click(object sender, EventArgs e)
        {
        }

        private void btnGenCustIVNo_Click(object sender, EventArgs e)
        {
            //SaveIV();
        }

        private void btnReCalc_Click(object sender, EventArgs e)
        {
            ClearPromotionTemp();

            CalcPromotionBeforeSave();
        }

        private void TxtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    this.BindData("Customer", searchCustControls, txt.Text);
                }

                FilterSaleArea(txt.Text);
            }
        }

        private void TxtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("BranchWarehouse", searchBWHControls, txt.Text);

                FilterSaleArea(txt.Text);
            }
        }

        private void TxtWHCode_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (!string.IsNullOrEmpty(txt.Text))
            {
                var bwh = bu.GetBranchWarehouse(x => x.WHID == txt.Text);
                if (bwh != null)
                {
                    this.BindData("BranchWarehouse", searchBWHControls, txt.Text);

                    FilterSaleArea(txt.Text);
                }
            }
        }

        private void TxdDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var txt = sender as MaskedTextBox;
                BindVanSalesData(txt.Text, "IV2");
            }
        }

        private void DtpDocDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDocDate.CalcDueDate(dtpDueDate, nudCreditDay);
        }

        private void NudCreditDay_ValueChanged(object sender, EventArgs e)
        {
            nudCreditDay.CalcDueDate(dtpDocDate, dtpDueDate);
        }

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grdList.SetCellContentClick(this, sender, e, "PreOrderProduct", 4);
        }

        private void grdList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                grdList.ModifyComboBoxCell(allProduct, e.RowIndex, bu, 3, 0);
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
                CalculateRow(grdList, grdList.CurrentRow.Index);
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

                        if (Helper.tbl_Users.RoleID != 10) //aloow super admin add duplicate item for support promotion 24022021
                        {
                            var checkDup = grd.ValidateDuplicateSKU(cell0.EditedFormattedValue.ToString(), 0, currentRowIndex, ref validateNewRow);
                            if (!checkDup)
                            {
                                GridViewHelper.ShowDupSKUMessage();
                                cell0.Value = "";
                                grd.Rows.RemoveAt(currentRowIndex);
                                isNewRow = false;
                                validateNewRow = false;
                            }
                        }

                        if (isNewRow)
                        {
                            grd.BindDataGridViewRow(dataGridList, initDataGridList, dt, 0, cell0.EditedFormattedValue.ToString(), currentRowIndex, 0, ref validateNewRow, "PreOrderProduct", false);
                            //if (!validateNewRow)
                            //{
                            //    isNewRow = false;
                            //}
                        }
                    }

                    if (isNewRow)
                    {
                        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 3];
                        grd.BeginEdit(true);
                    }
                }
                else if (curentColIndex == 3 || curentColIndex == 4)
                {
                    grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 1];
                    grd.BeginEdit(true);
                }
                else if (curentColIndex == 5)
                {
                    if (cell2.IsNotNullOrEmptyCell())
                    {
                        if ((grd.RowCount - 1) == currentRowIndex)
                        {
                            validateNewRow = true;

                            if (Helper.tbl_Users.RoleID != 10) //aloow super admin add duplicate item for support promotion 24022021
                            {
                                var checkDup = grd.ValidateDuplicateSKU(cell0.EditedFormattedValue.ToString(), 0, currentRowIndex, ref validateNewRow);
                                if (!checkDup)
                                {
                                    validateNewRow = false;
                                }
                            }

                            grdList.AddNewRow(allProduct, initDataGridList, 0, "", currentRowIndex + 1, validateNewRow, uoms, bu, this, 0);

                            if (validateNewRow)
                            {
                                if (grd.RowCount > currentRowIndex + 1)
                                {
                                    grd.CurrentCell = grd.Rows[currentRowIndex + 1].Cells[0];
                                    grd.BeginEdit(true);
                                }
                            }
                        }
                        else
                        {
                            if (grd.RowCount > currentRowIndex + 1)
                            {
                                grd.CurrentCell = grd.Rows[currentRowIndex + 1].Cells[0];
                                grd.BeginEdit(true);
                            }
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
            try
            {
                DataGridView grd = (DataGridView)sender;
                if (grd != null && grd.CurrentRow != null)
                {
                    int currentRowIndex = grd.CurrentCell.RowIndex;
                    int curentColIndex = grd.CurrentCell.ColumnIndex;

                    var numCals = new int[] { 0, 3, 4, 5, 7, 8 };
                    if (numCals.Contains(curentColIndex))
                    {
                        var cell4 = grd.Rows[currentRowIndex].Cells[4];
                        if (cell4.IsNotNullOrEmptyCell())
                            CalculateRow(grd, currentRowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void grdList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            grdList.SetUserDeletingRow(sender, e);
        }

        private void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataGridView _grd = (DataGridView)sender;
                if (_grd != null && _grd.CurrentRow != null)
                {
                    int rowIndex = _grd.CurrentRow.Index;
                    if (_grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "White" || _grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "0")
                        _grd.Rows.RemoveAt(rowIndex);
                    else
                        return;
                }
            }
        }

        private void grdList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            grdList.SetCellClick(sender, e, cellEdit, 0);
        }

        private void btnShowPromotion_Click(object sender, EventArgs e)
        {
            CalcPromotionBeforeSave();
        }

        #endregion

        #region Tab 3

        public void BindVanSalesPOData(string ivDocNo)
        {
            txtDocNoPO.Text = ivDocNo;
        }

        private void SearchPO()
        {
            try
            {
                var docNo = txtDocNoPO.Text;
                var whid = txtWHCodePO.Text;
                var dt = preBu.GetPOData(docNo, dtpDocDatePO.Value, whid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    BindGridview(grdPO, dt);
                    lblPOCount.Text = dt.Rows.Count.ToNumberFormat();
                    btnPrint.Enabled = true;
                }
                else
                {
                    BindGridview(grdPO, null);
                    lblPOCount.Text = "0";
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void btnSearchPODoc_Click(object sender, EventArgs e)
        {
            this.OpenIVDocPopup("ใบกำกับสินค้า/กำกับภาษี", "IVPrePO");
        }

        private void btnSearchPO_Click(object sender, EventArgs e)
        {
            SearchPO();
        }

        private void btnSearchWHCodePO_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchPOBWHControls, "เลือกคลังสินค้า", whFunc);
        }

        private void TxtDocNoPO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchPO();
            }
        }

        private void TxtWHCodePO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("BranchWarehouse", searchPOBWHControls, txt.Text);

                FilterSaleArea(txt.Text);
            }
        }

        private void TxtWHCodePO_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (!string.IsNullOrEmpty(txt.Text))
            {
                var bwh = bu.GetBranchWarehouse(x => x.WHID == txt.Text);
                if (bwh != null)
                {
                    this.BindData("BranchWarehouse", searchPOBWHControls, txt.Text);

                    FilterSaleArea(txt.Text);
                }
            }
        }

        private void grdPO_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var docNo = grdPO.Rows[e.RowIndex].Cells["colDocNoPO"].EditedFormattedValue.ToString();
            if (!string.IsNullOrEmpty(docNo))
            {
                MainForm mfrm = null;
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name.ToLower() == "mainform") //(f.Name == "frmOD")
                    {
                        mfrm = (MainForm)f;
                    }
                }

                frmTabletSalesPre frm = new frmTabletSalesPre();
                frm.MdiParent = mfrm;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.WindowState = FormWindowState.Minimized;
                frm.Show();
                frm.WindowState = FormWindowState.Maximized;
                frm.BindTabletSalesData(docNo, "IV");
            }
        }

        private void grdPO_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPO.SetRowPostPaint(sender, e, this.Font);
        }





        #endregion

        private void rdoCurrent_CheckedChanged(object sender, EventArgs e)
        {
            dtpStockDate.Enabled = rdoCurrent.Checked ? false : true;
        }

        private void rdoAtDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpStockDate.Enabled = rdoAtDate.Checked ? true : false;
        }

        private void frmPreOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
