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
using System.Reflection;

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
        Dictionary<string, decimal> lineTotalDisc = new Dictionary<string, decimal>();

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

        List<tbl_PODetail> allPODetails = new List<tbl_PODetail>();
        List<tbl_ProductUom> prodUOMs = new List<tbl_ProductUom>();
        bool isAdd = false;
        string confirmWHID = "";

        Dictionary<string, string> allEmp = new Dictionary<string, string>();
        tbl_Employee emp = new tbl_Employee();
        tbl_ApSupplier supp = new tbl_ApSupplier();

        List<tbl_Employee> allEmp2 = new List<tbl_Employee>();
        List<tbl_BranchWarehouse> allBranchWarehouse = new List<tbl_BranchWarehouse>();

        private ContextMenuStrip printContextMenuStrip;

        public frmPreOrder()
        {
            InitializeComponent();

            searchCustControls = new List<Control> { txtCustomerID, txtCustName, txtBillTo, txtContact, txtTelephone };
            searchBWHControls = new List<Control> { txtWHCode, txtWHName };
            searchMstBWHControls = new List<Control> { txtMstWHCode, txtMstWHName };
            searchPOBWHControls = new List<Control> { txtWHCodePO, txtWHNamePO };
            searchEmpControls = new List<Control> { txtEmpCode };
            readOnlyControls = new List<string>() { txtCustName.Name, txtMstWHName.Name, txtEmpCode.Name, txtCrUser.Name };

            dataGridList = new Dictionary<int, string>() { { 0, "ProductID" }, { 2, "ProductName" }, { 3, "UOMSetID" }, { 5, "SellPriceVat" }, { 6, "VatType" }, { 11, "SellPrice" } };
            initDataGridList = new Dictionary<int, string>() { { 0, "" }, { 2, "" }, { 3, "combobox" }, { 4, "0" }, { 5, "0.00" }, { 6, "0" }, { 7, "N" }, { 8, "0.00" }, { 9, "0.00" }, { 10, "" }, { 11, "0.00" } };

            txdDocNo.KeyDown += TxdDocNo_KeyDown;
            //txtDocNoPOMst.KeyDown += TxtDocNo_KeyDown;
            txtDocNoPO.KeyDown += TxtDocNoPO_KeyDown;
            txtMstWHCode.KeyDown += TxtMstWHCode_KeyDown;
            txtMstWHCode.TextChanged += TxtMstWHCode_TextChanged;
            txtWHCode.KeyDown += TxtWHCode_KeyDown;
            txtWHCode.TextChanged += TxtWHCode_TextChanged;
            txtWHCodePO.KeyDown += TxtWHCodePO_KeyDown;
            txtWHCodePO.TextChanged += TxtWHCodePO_TextChanged;
            txtCustomerID.KeyDown += TxtCustomerCode_KeyDown;

            dtpDocDate.ValueChanged += DtpDocDate_ValueChanged;
            nudCreditDay.ValueChanged += NudCreditDay_ValueChanged;

            //grdPOMst.CreateCheckBoxHeaderColumn("colSelectRow");
            //grdPO.CreateCheckBoxHeaderColumn("colSelectRowPO");

            SetDefaultDate(preBu.GetDefaultDocDate());

            //AddHeaderCheckBox(grdPOMst, chkbx);
            //AddHeaderCheckBox(grdPO, chkbx2);
            ////Register Events of Checkbox
            //chkbx.KeyUp += new KeyEventHandler(chkbx_KeyUp);
            //chkbx.MouseClick += new MouseEventHandler(chkbx_MouseClick);

            //chkbx2.KeyUp += new KeyEventHandler(chkbx2_KeyUp);
            //chkbx2.MouseClick += new MouseEventHandler(chkbx2_MouseClick);
        }

        public void SetDefaultWHID(string whid)
        {
            confirmWHID = whid;
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
            m.MenuText = "พิมพ์ใบส่งของ/ใบกำกับภาษีอย่างย่อ";
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
                    Dictionary<DateTime, string> rlList = new Dictionary<DateTime, string>();
                    foreach (DataGridViewRow row in grdPO.Rows)
                    {
                        DateTime _rlDocDate;
                        string _whName = "";
                        _rlDocDate = Convert.ToDateTime(row.Cells["colDocDatePO"].Value);
                        _whName = row.Cells["colWHNamePO"].Value.ToString();

                        var sel = row.Cells["colSelectRowPO"].Value;

                        if (row.Cells["colSelectRowPO"].IsNotNullOrEmptyCell())
                        {
                            bool chk = false;
                            if (sel != null && bool.TryParse(sel.ToString(), out chk))
                            {
                                if (chk)
                                {
                                    if (!string.IsNullOrEmpty(_whName))
                                    {
                                        if (rlList.Count(x => x.Key == _rlDocDate) == 0)
                                        {
                                            var wh = bu.GetBranchWarehouse(x => x.WHName == _whName);
                                            if (wh != null)
                                                rlList.Add(_rlDocDate, wh.WHID);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    foreach (var item in rlList)
                    {
                        DateTime rlDocDate;
                        string whName = "";
                        rlDocDate = item.Key;
                        whName = item.Value;

                        _params = new Dictionary<string, object>();
                        _params.Add("@DocDate", rlDocDate.ToString("yyyyMMdd", new CultureInfo("en-US")));
                        _params.Add("@WHID", whName);
                        this.OpenReportingReportsPopup("ใบจัดสินค้า(RL)", "Form_RL_Report2.rdlc", "Form_RL_PRE_New", _params);
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
                        _rlDocNo = row.Cells["colDocNoPO"].Value.ToString(); ;// row.Cells["colRLDocNo"].Value.ToString();
                        _whName = row.Cells["colWHNamePO"].Value.ToString();

                        var sel = row.Cells["colSelectRowPO"].Value;

                        if (row.Cells["colSelectRowPO"].IsNotNullOrEmptyCell())
                        {
                            bool chk = false;
                            if (sel != null && bool.TryParse(sel.ToString(), out chk))
                            {
                                if (chk)
                                {
                                    if (!string.IsNullOrEmpty(_rlDocNo) && !string.IsNullOrEmpty(_whName))
                                    {
                                        if (rlList.Count(x => x.Key == _rlDocNo) == 0)
                                        {
                                            var wh = bu.GetBranchWarehouse(x => x.WHName == _whName);
                                            if (wh != null)
                                                rlList.Add(_rlDocNo, wh.WHID);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    var tempList = new Dictionary<string, List<string>>();
                    foreach (var item in rlList)
                    {
                        if (tempList.Any(x => x.Key == item.Value))
                        {
                            var tmp = tempList.FirstOrDefault(x => x.Key == item.Value);
                            if (tmp.Key != null)
                            {
                                tmp.Value.Add(item.Key);
                            }
                        }
                        else
                            tempList.Add(item.Value, new List<string> { item.Key });
                    }

                    foreach (var item in tempList)
                    {
                        string rlDocNos = "";
                        string whName = "";
                        //rlDocNo = item.Key;
                        //whName = item.Value;
                        rlDocNos = string.Join(",", item.Value);
                        whName = item.Key;

                        _params = new Dictionary<string, object>();
                        _params.Add("@DocNo", rlDocNos);
                        _params.Add("@WHID", whName);
                        _params.Add("@DocDateFr", dtpDocDatePO.Value);
                        _params.Add("@DocDateTo", dtpDocDatePO.Value);

                        this.OpenReportingReportsPopup("ใบคุมส่งสินค้าตามคลังรถ", "proc_PreOrder_Cust_Ctrl_Report2.rdlc", "proc_PreOrder_Cust_Ctrl_Report", _params);
                    }
                }
            }
            else if (frmText == "พิมพ์ใบส่งของ/ใบกำกับภาษีอย่างย่อ")
            {
                if (grdPO.RowCount > 0)
                {
                    List<string> poList = new List<string>();
                    foreach (DataGridViewRow row in grdPO.Rows)
                    {
                        string poDocNo = "";
                        poDocNo = row.Cells["colDocNoPO"].Value.ToString();

                        var sel = row.Cells["colSelectRowPO"].Value;

                        if (row.Cells["colSelectRowPO"].IsNotNullOrEmptyCell())
                        {
                            bool chk = false;
                            if (sel != null && bool.TryParse(sel.ToString(), out chk))
                            {
                                if (chk)
                                {
                                    if (!string.IsNullOrEmpty(poDocNo))
                                    {
                                        poList.Add(poDocNo);
                                    }
                                }
                            }
                        }
                    }

                    if (poList.Count > 0)
                    {
                        foreach (var _poDocNo in poList)
                        {
                            _params = new Dictionary<string, object>();
                            _params.Add("@DocNo", _poDocNo);
                            //this.OpenCrystalReportsPopup("ใบกำกับภาษีอย่างย่อ", "Form_IV.rpt", "Form_IV", _params);
                            //this.OpenReportingReportsPopup("ใบกำกับภาษีอย่างย่อ", "Form_IV.rdlc", "Form_IV", _params);

                            this.OpenReportingReportsPopup("ใบส่งของ/ใบกำกับภาษีอย่างย่อ(ต้นฉบับ)", "Form_IV1.rdlc", "Form_IV", _params); //change report header edit by sailom 29/03/2022
                            this.OpenReportingReportsPopup("ใบส่งของ/ใบกำกับภาษีอย่างย่อ(สำเนา)", "Form_IV1_Copy.rdlc", "Form_IV", _params); //change report header edit by sailom 29/03/2022
                        }
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
            var menu = bu.tbl_AdmFormList.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }

            var headerPic = bu.tbl_MstMenu.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            var documentType = bu.tbl_DocumentType.FirstOrDefault(x => x.DocTypeCode.Trim() == "IV");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                runDigit = documentType.DocFormat.Length;// - 2;

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

            allUOM = bu.tbl_ProductUom;

            uoms.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
            uoms.AddRange(allUOM);

            saleAreaList.AddRange(bu.tbl_SalArea);
            salAreaDistrictList.AddRange(bu.tbl_SalAreaDistrict);

            //grdStock.SetDataGridViewStyle();
            //SetDefaultGridViewEventStock(grdStock);

            grdList.AutoGenerateColumns = false;
            grdList.SetDataGridViewStyle();
            SetDefaultGridViewEvent(grdList);

            grdPOMst.SetDataGridViewStyle();
            grdPO.SetDataGridViewStyle();
            //grdPOMst.CreateCheckBoxHeaderColumn("colSelectRow");
            //grdPO.CreateCheckBoxHeaderColumn("colSelectRowPO");

            SetDefaultGridViewEventMst(grdPOMst);
            SetDefaultGridViewEventPO(grdPO);

            allProduct = bu.tbl_Product;
            allUomSet = bu.tbl_ProductUomSet;

            allProductPrice = bu.tbl_ProductPriceGroup;

            allProdGroup = bu.tbl_ProductGroup;
            allProdSubGroup = bu.tbl_ProductSubGroup;

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

            //grdPO.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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

            btnFixRL.Visible = Helper.tbl_Users.RoleID == 10; //Fix RL when login with superadmin user by sailom.k 21/04/2022

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

        private void SearchStock(bool isInitial = false)
        {
            try
            {
                if (!isInitial)
                    FormHelper.CheckOverStok1000PreOrder(dtpSDocDate.Value); //Check Over Stock in main stock(1000)

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
            //grdStock.SetRowPostPaint(sender, e, this.Font);

            var row = grdStock.Rows[e.RowIndex];
            var _diffCarQty = Convert.ToInt32(row.Cells["colDiffCarQty"].Value);
            var _diffPckQty = Convert.ToInt32(row.Cells["colDiffPckQty"].Value);
            var _1000CarQty = Convert.ToInt32(row.Cells["colST000CarQty"].Value);
            var _1000PckQty = Convert.ToInt32(row.Cells["colST1000PckQty"].Value);

            if (_diffCarQty < 0)
            {
                row.DefaultCellStyle.BackColor = Color.Red;
                row.DefaultCellStyle.ForeColor = Color.White;
            }
            if (_diffPckQty < 0)
            {
                row.DefaultCellStyle.BackColor = Color.Red;
                row.DefaultCellStyle.ForeColor = Color.White;
            }
            if (_1000CarQty <= 0 && _1000PckQty <= 0 && _diffCarQty == 0 && _diffPckQty == 0)
            {
                row.DefaultCellStyle.BackColor = Color.LightYellow;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        #endregion

        #region event methods

        private void frmPreOrder_Load(object sender, EventArgs e)
        {
            InitPage();

            grdList.DataSource = null;
            grdPOMst.DataSource = null;
            grdPO.DataSource = null;
            grdStock.DataSource = null;

            //SearchStock(true);
            SearchStock();

            InitPODtHeader();

            SearchPO();

            if (!string.IsNullOrEmpty(confirmWHID))
            {
                txtMstWHCode.Text = confirmWHID;
            }

            btnSearchPOMst.PerformClick();

            if (isConfirmFTB)
                btnCancel.PerformClick();

            if (Helper.tbl_Users.RoleID == 10) //for super admin only
            {
                btnRollback.Visible = true;
            }
        }

        private void tabPOMasterPre_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = ((System.Windows.Forms.TabControl)sender).SelectedIndex;
            if (index == 0)
            {
                btnSearchPOMst.PerformClick();
            }
            else if (index == 2)
            {
                btnSearchPO.PerformClick();
            }
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
            btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, txdDocNo.Text);

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            txdDocNo.DisableTextBox(false);
            txtCustomerID.DisableTextBox(false);
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
            cboRemark.Enabled = true;

            if (!string.IsNullOrEmpty(txdDocNo.Text) && bu.CheckExistsPO_PRE(txdDocNo.Text)) //20210506 by sailom                
            {
                grdList.CellContentClick -= grdList_CellContentClick;
                grdList.CellContentClick += grdList_CellContentClick;
            }

            dtpDocDate.Focus();

            allProduct = bu.tbl_Product;

            BindVanSalesDataForEdit(txdDocNo.Text, "IV2");

            cboRemark.Enabled = true;
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

            cboRemark.Enabled = true;
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
            //InitPage();

            //SearchStock();

            InitPODtHeader();

            //pnlPODT_PRE.ClearControl(bu, docTypeCode, runDigit);
            var documentType = bu.tbl_DocumentType.FirstOrDefault(x => x.DocTypeCode.Trim() == "IV");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                runDigit = documentType.DocFormat.Length;// - 2;

                pnlPODT_PRE.ClearControl(docTypeCode, runDigit);
                txtComment.Text = documentType.DocRemark;
            }

            btnAdd.Enabled = false;

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");

            validateNewRow = true;

            pnlPODT_PRE.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            btnCancel.EnableButton(btnSearchDoc);

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            tabPOMasterPre.SelectedIndex = 0;
            btnPrint.Enabled = true;

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

        CheckBox headerCheckBox = new CheckBox();
        private void SearchPOMst(bool isInitial = false)
        {
            try
            {
                //if (!isInitial)
                //    FormHelper.CheckOverStok1000PreOrder(dtpSDocDate.Value); //Check Over Stock in main stock(1000)

                Cursor.Current = Cursors.WaitCursor;

                var docNo = ""; // txtDocNoPOMst.Text;
                var whid = txtMstWHCode.Text;
                string docStatus = "3";
                docStatus = rdoStatusN.Checked ? "3" : "5";

                var dt = preBu.GetPOMstData(docNo, dtpSDocDate.Value, whid, docStatus);

                if (dt != null && dt.Rows.Count > 0)
                {
                    BindGridview(grdPOMst, dt);

                    try
                    {
                        //Add a CheckBox Column to the DataGridView Header Cell.

                        //Find the Location of Header Cell.
                        Point headerCellLocation = grdPOMst.GetCellDisplayRectangle(0, -1, true).Location;
                        //headerCheckBox = new CheckBox();
                        //Place the Header CheckBox in the Location of the Header Cell.
                        headerCheckBox.Location = new Point(headerCellLocation.X + 8, headerCellLocation.Y + 2);
                        headerCheckBox.BackColor = Color.White;
                        headerCheckBox.Size = new Size(18, 18);

                        headerCheckBox.Checked = false; // uncheck 

                        //Assign Click event to the Header CheckBox.
                        headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);
                        grdPOMst.Controls.Add(headerCheckBox);
                        //grdList.CurrentCell = grdList.Rows[0].Cells[0];

                        //Assign Click event to the DataGridView Cell.
                        grdPOMst.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellClick);
                    }
                    catch { }

                    lblPreOrderCount.Text = dt.Rows.Count.ToNumberFormat();
                    btnCreatePO.Enabled = true;
                }
                else
                {
                    BindGridview(grdPOMst, null);

                    lblPreOrderCount.Text = "0";
                    btnCreatePO.Enabled = false;
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;

                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Necessary to end the edit mode of the Cell.
                grdPOMst.EndEdit();

                //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
                foreach (DataGridViewRow row in grdPOMst.Rows)
                {
                    DataGridViewCheckBoxCell checkBox = (row.Cells["colSelectRow"] as DataGridViewCheckBoxCell);
                    checkBox.Value = headerCheckBox.Checked;
                }
            }
            catch { }

        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Check to ensure that the row CheckBox is clicked.
                if (e.RowIndex >= 0 && e.ColumnIndex == 0)
                {
                    //Loop to verify whether all row CheckBoxes are checked or not.
                    bool isChecked = true;
                    foreach (DataGridViewRow row in grdPOMst.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["colSelectRow"].EditedFormattedValue) == false)
                        {
                            isChecked = false;
                            break;
                        }
                    }
                    headerCheckBox.Checked = isChecked;
                }
            }
            catch { }

        }

        private void btnSearchPreDoc_Click(object sender, EventArgs e)
        {
            this.OpenIVDocPopup("ใบกำกับสินค้า/กำกับภาษี", "PreOrder");
        }

        private void btnCreatePO_Click(object sender, EventArgs e)
        {
            //bool isOverStock = FormHelper.CheckOverStok1000PreOrder(dtpSDocDate.Value); //Check Over Stock in main stock(1000)
            //if (isOverStock)
            //{
            //    return;
            //}

            try
            {
                rlDocNo = "";

                if (grdPOMst == null || grdPOMst.Rows.Count == 0)
                {
                    string msg = "ไม่พบข้อมูล บิลขาย!!";
                    msg.ShowWarningMessage();

                    return;
                }

                //validate end day----------------------------
                if (Helper.tbl_Users.RoleID != 10 && !dtpDocDate.ValidateEndDay(bu))
                {
                    string message = "ระบบปิดวันไปแล้ว ไม่สามารถเลือกวันที่นี้ได้ !!!";
                    message.ShowWarningMessage();
                    return;
                }
                //validate end day----------------------------

                string docNos = "";
                List<string> docNoList = new List<string>();

                foreach (DataGridViewRow row in grdPOMst.Rows)
                {
                    var sel = grdPOMst.Rows[row.Index].Cells["colSelectRow"].Value;
                    string _docNo = grdPOMst.Rows[row.Index].Cells["colDocNo"].Value.ToString();
                    string _docStatus = grdPOMst.Rows[row.Index].Cells["colDocStatusName"].Value.ToString();

                    if (grdPOMst.Rows[row.Index].Cells["colSelectRow"].IsNotNullOrEmptyCell() && _docStatus != "Cancelled")
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

                string cfMsg = "ต้องการยืนยัน Order ใช่หรือไม่??";
                string title = "ยืนยัน Order!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                if (!string.IsNullOrEmpty(docNos))
                {
                    Cursor.Current = Cursors.WaitCursor;

                    docNos = preBu.FilterDocNoWithAutoGen(docNos); //filter docno for auto generate process edit by sailom 08/03/2022 
                    if (!string.IsNullOrEmpty(docNos))
                    {
                        var ret = preBu.TransferPreOrderToPO(dtpSDocDate.Value, Helper.tbl_Users.Username, docNos);
                        if (ret == 1)
                        {
                            string msg = "ยืนยัน Order เรียบร้อยแล้ว!!";
                            msg.ShowInfoMessage();
                            Cursor.Current = Cursors.Default;

                            Cursor.Current = Cursors.WaitCursor;

                            if (grdPOMst != null && grdPOMst.Rows.Count > 0)
                            {
                                GenerateRL(docNos);

                                Cursor.Current = Cursors.WaitCursor;

                                SearchStock();

                                SearchPOMst();

                                tabPOMasterPre.SelectedIndex = 2;

                                //last edit by sailom .k for bp 02/07/2022--------------------
                                if (!string.IsNullOrEmpty(docNos))
                                {
                                    var firstDocNo = docNos.Split(',')[0].ToString();
                                    var po = bu.GetPOMaster(firstDocNo);
                                    txtWHCodePO.Text = po.WHID;
                                }
                                //last edit by sailom .k for bp 02/07/2022--------------------

                                btnSearchPO.PerformClick();

                                pnlCompletePO.OpenControl(true, readOnlyControls.ToArray(), cellEdit);
                                pnlPOMst_PRE.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

                                Cursor.Current = Cursors.Default;
                            }
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            string msg = "ยืนยัน Order ไม่สำเร็จ กรุณาลองใหม่อีกครั้ง!!!";
                            msg.ShowErrorMessage();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;

                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void GenerateRL(string docNos)
        {
            Dictionary<string, string> createRLList = new Dictionary<string, string>();
            for (int i = 0; i < grdPOMst.Rows.Count; i++)
            {
                string whName = grdPOMst.Rows[i].Cells["colWHName"].Value.ToString();
                string _docNo = grdPOMst.Rows[i].Cells["colDocNo"].Value.ToString();

                if (docNos.Contains(_docNo))
                {
                    var tmp = createRLList.FirstOrDefault(x => x.Key == whName);
                    if (tmp.Key != null)
                    {
                        createRLList[tmp.Key] += "," + _docNo;
                    }
                    else
                        createRLList.Add(whName, _docNo);
                }
            }

            List<string> tmpDocNo = new List<string>();
            var _cDate = DateTime.Now;
            foreach (var item in createRLList)
            {
                string whName = item.Key; // grdPOMst.Rows[i].Cells["colWHName"].Value.ToString();
                string _docNoList = item.Value;
                var wh = bu.GetBranchWarehouse(x => x.WHName == whName);
                if (wh != null)
                {
                    string whid = wh.WHID;

                    //var docNo = preBu.GenerateRL(dtpSDocDate.Value, whid, Helper.tbl_Users.Username, docNoLsit);
                    var docNo = preBu.GenerateRL(dtpSDocDate.Value, whid, Helper.tbl_Users.Username, _docNoList); //วันที่ในใบเบิกจะต้องเท่ากับ วันที่ admin ทำการยืนยันเอกสาร PO by sailom .k 11/03/2022 confirm by P'Jang Acc
                    if (!string.IsNullOrEmpty(docNo))
                    {
                        tmpDocNo.Add(docNo);
                    }
                }
            }

            string msg = "";
            if (tmpDocNo.Count > 0)
            {
                Cursor.Current = Cursors.Default;
                rlDocNo = string.Join(",", tmpDocNo);
                msg = "สร้างใบจัดสินค้า เลขที่ : " + rlDocNo + " เรียบร้อยแล้ว!!";
                msg.ShowInfoMessage();

            }
            else
            {
                Cursor.Current = Cursors.Default;
                msg = "ไม่สามารถสร้างใบจัดสินค้าได้!!";
                msg.ShowErrorMessage();
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

                pnlCompletePO.OpenControl(true, readOnlyControls.ToArray(), cellEdit);
                pnlPOMst_PRE.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

                if (!string.IsNullOrEmpty(txdDocNo.Text))
                {
                    var tmpPO = bu.GetPOMasterPRE(txdDocNo.Text);
                    if (tmpPO != null)
                    {
                        if (tmpPO.DocStatus == "5" && string.IsNullOrEmpty(tmpPO.EdUser))
                            btnEdit.Enabled = false;
                        else
                            btnEdit.Enabled = true;
                    }
                    else
                        btnEdit.Enabled = false;
                }
            }
        }

        private void grdPOMstList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPOMst.SetRowPostPaint(sender, e, this.Font);

            if (grdPOMst.Rows.Count > 0)
            {
                try
                {
                    var row = grdPOMst.Rows[e.RowIndex];

                    var _edDate = row.Cells["colEdDatePM"].Value.ToString();
                    var _edUser = row.Cells["colEdUserPM"].Value.ToString();

                    if (!string.IsNullOrEmpty(_edDate) && !string.IsNullOrEmpty(_edUser))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    }

                    var _signature = row.Cells["Signature"].Value;
                    if (_signature != DBNull.Value)
                    {
                        grdPOMst.Rows[e.RowIndex].Cells["colSignature"].Style.BackColor = Color.LightGreen;
                    }
                    else
                        grdPOMst.Rows[e.RowIndex].Cells["colSignature"].Style.BackColor = Color.Gray;
                }
                catch
                {
                }
            }
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
                //grdPOMst.CreateCheckBoxHeaderColumn("colSelectRow");
            }
        }

        #endregion

        #region Tab 2

        private void ClearPromotionTemp()
        {
            pro.RemoveTempData();
        }

        public void BindVanSalesDataForEdit(string ivDocNo, string _docTypeCode)
        {
            string msg = "ท่านต้องเลือกของแถมใหม่!!!";
            msg.ShowWarningMessage();

            bu.GetDocData(ivDocNo, _docTypeCode);

            PreparePOMasterPREToC(bu.tbl_POMaster_PRE);
            PreparePODetailPREToC(bu.tbl_PODetails_PRE);

            var po = bu.tbl_POMaster;
            var poDts = bu.tbl_PODetails;

            if (string.IsNullOrEmpty(po.DocNo))
            {
                msg = "ไม่พบข้อมูลเลขที่เอกสาร!!!";
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
                BindPODetailForEditItem(poDts);
            }

            CreateGridBtnList();
        }

        public void BindVanSalesData(string ivDocNo, string _docTypeCode)
        {
            bu.GetDocData(ivDocNo, _docTypeCode);

            PreparePOMasterPREToC(bu.tbl_POMaster_PRE);
            PreparePODetailPREToC(bu.tbl_PODetails_PRE);

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

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            btnSearchPOMst.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);

            //txtDocNoPOMst.DisableTextBox(false);
            //txtDocNoPOMst.BackColor = Color.Turquoise;
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

            pnlCompletePO.OpenControl(true, readOnlyControls.ToArray(), cellEdit);
            pnlPOMst_PRE.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            grdList.CellContentClick -= grdList_CellContentClick;

            //bool checkEditMode = bu.CheckExistsPO_PRE(ivDocNo);
            //po.DocStatus.CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);

            //btnUpdateAddress.Enabled = true;
            //btnGenCustIVNo.Enabled = true;

            CreateGridBtnList();
        }

        private void BindPOMaster(tbl_POMaster po)
        {
            txdDocNo.Text = po.DocNo;

            dtpDocDate.Value = po.DocDate.ToDateTimeFormat();

            txtCustomerID.Text = po.CustomerID;
            txtCustName.Text = po.CustName;
            txtContact.Text = po.ContactName;
            txtTelephone.Text = po.ContactTel;
            txtBillTo.Text = po.Address;
            txtCustPONo.Text = po.CustPONo;
            txtCustInvNO.Text = po.CustInvNO;

            if (allEmp2.Count == 0)
            {
                allEmp2 = bu.GetEmployee();
            }

            var employee = allEmp2.FirstOrDefault(x => x.EmpID == po.EmpID);// edit by sailom.k 13/12/2021 Helper.tbl_Users.EmpID); // bu.GetEmployee(Helper.tbl_Users.EmpID);
            if (employee != null)
                txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName, employee.LastName);

            var emp = allEmp2.FirstOrDefault(x => x.EmpID == po.SaleEmpID);
            if (emp != null)
                txtEmpCode.Text = emp.TitleName + " " + emp.FirstName + " " + emp.LastName;

            txtWHCode.Text = po.WHID;
            Func<tbl_BranchWarehouse, bool> func = (x => x.WHID == po.WHID);
            var wh = allBranchWarehouse.FirstOrDefault(func);// bu.GetBranchWarehouse(func);
            if (wh != null)
            {
                txtWHName.Text = wh.WHName;
            }

            //FilterSaleArea(po.CustomerID);
            FilterSaleArea(po.SalAreaID); //edit by sailom .k 13/12/2021
            ddlDocStatus.BindDropdown3DocStatus(bu, po.DocStatus);

            txtComment.Text = po.Comment;

            //cboRemark.SelectedValue = po.Remark;
            txnAmount.Text = po.Amount.Value.ToStringN2();
            //txnDiscountType.Text = "0";// po.DiscountType;
            txnDiscountAmt.Text = po.Discount.Value.ToStringN2();

            decimal amt = 0;
            decimal excVat = 0;
            decimal discount = 0;
            decimal vatRate = 0;
            if (po.Amount != null)
                amt = po.Amount.Value;
            if (po.ExcVat != null)
                excVat = po.ExcVat.Value;
            if (po.Discount != null)
                discount = po.Discount.Value;
            if (po.VatRate != null)
                vatRate = po.VatRate.Value;

            decimal incVat = ((amt - excVat - discount) * 100) / (100 + vatRate);

            txnBeforeVat.Text = incVat.ToStringN2();
            txnVatAmt.Text = po.VatAmt.Value.ToStringN2();
            lblVatType.Text = po.VatRate != null ? po.VatRate.Value.ToStringN0() : "0";
            txnExcVat.Text = po.ExcVat.Value.ToStringN2();
            txnTotalDue.Text = po.TotalDue.ToStringN2();

            Dictionary<string, string> remarkList = new Dictionary<string, string>();
            remarkList.Add("สินค้าคงคลังไม่เพียงพอ", "สินค้าคงคลังไม่เพียงพอ");
            remarkList.Add("ลูกค้ายกเลิกหลังส่งออเดอร์", "ลูกค้ายกเลิกหลังส่งออเดอร์");
            remarkList.Add("อื่น ๆ", "อื่น ๆ");
            cboRemark.BindDropdownList(remarkList, "Key", "Value", 0);

            cboRemark.Text = po.Remark;
        }

        private void BindPODetail(List<tbl_PODetail> poDts)
        {
            grdList.Rows.Clear();

            var tmpStock1000 = (DataTable)grdStock.DataSource;
            if (tmpStock1000 != null && tmpStock1000.Rows.Count > 0)
            {
                //var allUOM = bu.tbl_ProductUom;
                var allProduct = bu.tbl_Product;
                //var allProductPrice = bu.tbl_ProductPriceGroup;

                //Last edit by sailom.k 14/09/2021 tunning performance-------------------------
                isAdd = false;
                allPODetails = poDts;

                List<tbl_DiscountType> disTypeList = new List<tbl_DiscountType>();
                disTypeList = bu.tbl_DiscountType;

                prodUOMs = new List<tbl_ProductUom>();
                var listPrdID = allPODetails.Select(x => x.ProductID).ToList();
                prodUOMs.AddRange(bu.GetProductUOM(listPrdID));
                //Last edit by sailom.k 14/09/2021 tunning performance--------------------

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

                    //grdList.BindComboBoxCell(allProduct, grdList.Rows[i], i, false, 3, uoms, this, bu, 0);
                    grdList.BindComboBoxDiscountTypeCell(allProduct, grdList.Rows[i], i, false, 3, uoms, this, bu, 0, disTypeList, 7); //Last edit by sailom.k 14/09/2021 tunning performance

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

                    for (int j = 0; j < tmpStock1000.Rows.Count; j++)
                    {
                        if (poDts[i].ProductID == tmpStock1000.Rows[j]["ProductID"].ToString())
                        {
                            var _uom = poDts[i].OrderUom;
                            var _poQty = poDts[i].OrderQty;

                            var _tmpUOM = allUomSet.FirstOrDefault(x => x.ProductID == poDts[i].ProductID && x.UomSetID == _uom);
                            if (_tmpUOM != null)
                            {
                                var _totalPOQty = _poQty * _tmpUOM.BaseQty;

                                int _stockQty = Convert.ToInt32(tmpStock1000.Rows[j]["ST1000Qty"]);
                                if (_stockQty <= 0 || _stockQty < _totalPOQty)
                                {
                                    //grdList.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                    grdList.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                                }
                            }
                        }
                    }

                }
            }
        }

        private void BindPODetailForEditItem(List<tbl_PODetail> poDts)
        {
            poDts = poDts.Where(x => !x.ProductID.Contains("9000")).ToList();

            var tmpStock1000 = (DataTable)grdStock.DataSource;
            if (tmpStock1000 != null && tmpStock1000.Rows.Count > 0)
            {
                grdList.Rows.Clear();

                //var allUOM = bu.tbl_ProductUom;
                var allProduct = bu.tbl_Product;
                //var allProductPrice = bu.tbl_ProductPriceGroup;

                //Last edit by sailom.k 14/09/2021 tunning performance-------------------------
                isAdd = false;
                allPODetails = poDts;

                List<tbl_DiscountType> disTypeList = new List<tbl_DiscountType>();
                disTypeList = bu.tbl_DiscountType;

                prodUOMs = new List<tbl_ProductUom>();
                var listPrdID = allPODetails.Select(x => x.ProductID).ToList();
                prodUOMs.AddRange(bu.GetProductUOM(listPrdID));
                //Last edit by sailom.k 14/09/2021 tunning performance--------------------

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

                    //grdList.BindComboBoxCell(allProduct, grdList.Rows[i], i, false, 3, uoms, this, bu, 0);
                    grdList.BindComboBoxDiscountTypeCell(allProduct, grdList.Rows[i], i, false, 3, uoms, this, bu, 0, disTypeList, 7); //Last edit by sailom.k 14/09/2021 tunning performance

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

                    for (int j = 0; j < tmpStock1000.Rows.Count; j++)
                    {
                        if (poDts[i].ProductID == tmpStock1000.Rows[j]["ProductID"].ToString())
                        {
                            var _uom = poDts[i].OrderUom;
                            var _poQty = poDts[i].OrderQty;

                            var _tmpUOM = allUomSet.FirstOrDefault(x => x.ProductID == poDts[i].ProductID && x.UomSetID == _uom);
                            if (_tmpUOM != null)
                            {
                                var _totalPOQty = _poQty * _tmpUOM.BaseQty;

                                int _stockQty = Convert.ToInt32(tmpStock1000.Rows[j]["ST1000Qty"]);
                                if (_stockQty <= 0 || _stockQty < _totalPOQty)
                                {
                                    //grdList.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                    grdList.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void BindSearchProduct(DataTable productDT, int rowIndex)
        {
            validateNewRow = true;
            if (Helper.tbl_Users.RoleID != 10) //allow super admin add duplicate item for support promotion 24022021
            {
                grdList.ValidateDuplicateSKU(productDT.Rows[0]["ProductID"].ToString(), 0, rowIndex, ref validateNewRow);
            }

            var allProduct = bu.tbl_Product;
            grdList.BindDataGrid(allProduct, dataGridList, initDataGridList, productDT, 0, rowIndex, validateNewRow, this, uoms, bu, 0);
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
                    //var allProduct = bu.tbl_Product;
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
                        txtCustomerID.ErrorTextBox();
                        return;
                    }

                    decimal totalDiscount = 0.00m;

                    if (validateQty)
                    {
                        pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                        pro.tbl_HQ_Promotion_Hits = new List<tbl_HQ_Promotion_Hit>();

                        proTmp.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                        bu.tbl_POMaster = new tbl_POMaster();
                        bu.tbl_PODetails = new List<tbl_PODetail>();

                        PreparePOMaster(false);
                        PreparePODetail(0, false);

                        //var proList = pro.CalculatePromotion(bu.tbl_PODetails.Where(x => x.UnitPrice > 0).ToList(), txtCustomerCode.Text.Trim());
                        var proList = pro.CalculatePromotion(bu.tbl_PODetails, txtCustomerID.Text.Trim(), bu.tbl_POMaster.WHID); //for support split van last edit by sailom.k 19/10/2021
                        if (proList != null && proList.Count > 0)
                        {
                            var delFlag = pro.RemoveTempData();
                            if (delFlag == 1)
                            {
                                PreparePromotionTemp(proList);
                                var addFlag = pro.AddTempData();

                                var po = bu.tbl_POMaster;

                                proTmp.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();
                                if (proTmp.tbl_HQ_Promotion_Hit_Temps.Any(x => x.DisCountAmt.Value > 0))
                                    po.Discount = proTmp.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value);

                                //po.Discount = pro.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value);
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

            DistributeFreePromotion(bu.tbl_PODetails);

            BindPODetail(bu.tbl_PODetails);

            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                CalculateRow(grdList, i);
            }

            CalculateTotal();

            //decimal totalDiscount = 0.00m;
            //proTmp.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();

            //if (proTmp.tbl_HQ_Promotion_Hit_Temps.Any(x => x.DisCountAmt.Value > 0))
            //    totalDiscount = proTmp.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value);

            //txnDiscountAmt.Text = totalDiscount.ToStringN2();
        }

        private void CalculateRow(DataGridView grd, int rowIndex)
        {
            try
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

                        //edit by sailom 21-06-2021
                        discountSellPrice = discount;
                        //if (cell6.FormattedValue.ToString() != "0")
                        //    discountSellPrice = bu.CalDiscountType(cell7.EditedFormattedValue.ToString(), cell8.EditedFormattedValue.ToString(), orderQty, unitSellPrice);
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

                    //edit by sailom 21-06-2021
                    cell9.Value = ((orderQty * unitPrice) - discountSellPrice).ToDecimalN2().ToStringN2();
                    //cell9.Value = (orderQty * unitPrice).ToDecimalN2().ToStringN2();
                }

                CalculateTotal();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }

        }

        private void CalculateTotal()
        {
            try
            {
                decimal amount = 0;
                decimal excVat = 0;
                decimal incVat = 0;
                decimal vatAmt = 0;
                decimal totalDue = 0;
                decimal vatRate = 0;
                string discountType = "";
                decimal totalDiscountAmt = 0;

                //for support U-Online
                bool isUOnline = false;
                //if (!string.IsNullOrEmpty(txtCustomerCode.Text))
                //{
                //    var cust = bu.GetCustomer(txtCustomerCode.Text);
                //    if (cust != null && cust.Count > 0)
                //    {
                //        if (cust.FirstOrDefault(x => uOnlineShopType.Contains(x.ShopTypeID)) != null)
                //            isUOnline = true; 
                //    }
                //}

                //var allPrdUOM = bu.GetUOM();
                //var allProductPrice = bu.tbl_ProductPriceGroup;

                //edit by sailom 21-06-2021
                decimal totalDiscount = 0;
                //if (!string.IsNullOrEmpty(txnDiscountAmt.Text) && Convert.ToDecimal(txnDiscountAmt.Text) > 0)
                //    totalDiscount = Convert.ToDecimal(txnDiscountAmt.Text).ToDecimalN2();

                pro.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();
                if (pro.tbl_HQ_Promotion_Hit_Temps.Any(x => x.DisCountAmt.Value > 0))
                    totalDiscount = pro.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value).ToDecimalN2();
                else
                    totalDiscount = 0;

                for (int i = 0; i < grdList.RowCount; i++)
                {
                    var prodIDCell = grdList.Rows[i].Cells[0];
                    var unitCell = grdList.Rows[i].Cells[3];
                    var qtyCell = grdList.Rows[i].Cells[4];
                    var priceCell = grdList.Rows[i].Cells[5];
                    var vatCell = grdList.Rows[i].Cells[6];
                    var lineTotalCell = grdList.Rows[i].Cells[9];
                    var discountTypeCell = grdList.Rows[i].Cells[7];
                    var discountAmtCell = grdList.Rows[i].Cells[8];
                    var sellPriceCell = grdList.Rows[i].Cells[11];
                    string prdCode = prodIDCell.EditedFormattedValue.ToString();
                    var _qty = Convert.ToDecimal(qtyCell.EditedFormattedValue);

                    if (!string.IsNullOrEmpty(prdCode))
                    {
                        //edit by sailom 21-06-2021
                        //if (totalDiscount > 0)
                        //{
                        //    decimal discountPerSku = totalDiscount / (_qty <= 0 ? 1 : _qty);
                        //    if (listDiscount.Count(x => x.Key == prdCode) == 0)
                        //        listDiscount.Add(prdCode, discountPerSku);
                        //}

                        if (lineTotalCell.IsNotNullOrEmptyCell())
                        {
                            //edit by sailom 21-06-2021
                            amount += (_qty * Convert.ToDecimal(priceCell.EditedFormattedValue));
                            //amount += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
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
                            else
                            {
                                discountAmtCell.Value = 0;
                            }
                        }
                        if (discountAmtCell.IsNotNullOrEmptyCell())
                        {
                            //edit by sailom 21-06-2021
                            //totalDiscountAmt += Convert.ToDecimal(discountAmtCell.EditedFormattedValue);

                            if (isUOnline)
                            {
                                decimal unitPrice = 0;
                                unitPrice = Convert.ToDecimal(priceCell.EditedFormattedValue);

                                var _discount = bu.CalDiscountType(discountTypeCell.EditedFormattedValue.ToString(), discountAmtCell.EditedFormattedValue.ToString(), _qty, unitPrice);

                                totalDiscountAmt += _discount;
                            }

                        }
                    }
                }

                excVat = excVat.ToDecimalN2();

                //edit by sailom 21-06-2021
                totalDiscount += totalDiscountAmt;
                totalDue = amount - totalDiscount; //edit by sailom 21-06-2021

                incVat = ((amount - excVat - totalDiscount) * 100) / (100 + vatRate);

                vatAmt = incVat * (vatRate / 100).ToDecimalN2();

                lblVatType.Text = vatRate.ToDecimalN0().ToString();
                txnAmount.Text = amount.ToDecimalN2().ToStringN2();
                txnBeforeVat.Text = incVat.ToStringN2();
                txnVatAmt.Text = vatAmt.ToStringN2();
                txnExcVat.Text = excVat.ToStringN2();
                txnTotalDue.Text = totalDue.ToStringN2();
                txnCommission.Text = "0.00";

                //edit by sailom 21-06-2021
                txnDiscountAmt.Text = totalDiscount.ToStringN2();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
        }

        private void FilterSaleArea(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                //Func<tbl_ArCustomer, bool> func = (x => x.CustomerID == text.Trim());

                //24/11/2021 by sailom .k-----------------------------------------------------------------
                bool ret = FindSaleAreaByCust(text); //find sale area by customer id

                if (!ret)
                {
                    FindSaleAreaByWHID(text); //find sale area by whid
                }
                //24/11/2021 by sailom .k-----------------------------------------------------------------
            }
        }

        /// <summary>
        /// 24/11/2021 by sailom .k
        /// </summary>
        /// <param name="text"></param>
        private bool FindSaleAreaByCust(string text)
        {
            bool ret = false;
            var cust = bu.GetCustomer(text.Trim());
            if (cust != null && cust.Count > 0)
            {
                var _saleAreaList = salAreaDistrictList.Where(x => x.WHID == cust[0].WHID);
                var listOfSalAreaID = _saleAreaList.Select(a => a.SalAreaID).ToList();
                //saleAreaList = bu.GetAllSaleArea().Where(x => listOfSalAreaID.Contains(x.SalAreaID)).ToList();
                saleAreaList = bu.GetAllSaleArea(listOfSalAreaID); //Last edit by sailom.k 14/09/2021 tunning performance

                //edit by sailom 07-06-2021---------------------------------------
                saleAreaList = saleAreaList.Where(p => p.SalAreaID == cust[0].SalAreaID).ToList();

                if (saleAreaList.Count == 0)
                    saleAreaList = bu.GetSaleArea(x => x.SalAreaName.Contains(cust[0].WHID.Substring(3, 3)));

                if (saleAreaList.Count > 0)
                {
                    Predicate<tbl_SalArea> defaultSelect = delegate (tbl_SalArea p) { return p.SalAreaID == cust[0].SalAreaID; };
                    ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID", null, defaultSelect);
                    ret = saleAreaList.Count > 0;
                }

                //edit by sailom 07-06-2021---------------------------------------
                string empID = cust[0].EmpID;
                var emp = allEmp2.FirstOrDefault(x => x.EmpID == empID);// bu.GetEmployee(empID);
                if (emp != null)
                    txtEmpCode.Text = emp.TitleName + " " + emp.FirstName + " " + emp.LastName;

                Func<tbl_BranchWarehouse, bool> whFunc = (x => x.WHID == cust[0].WHID);
                var wh = bu.GetBranchWarehouse(whFunc);
                if (wh != null)
                {
                    if (string.IsNullOrEmpty(txtWHCode.Text))
                    {
                        txtWHCode.Text = wh.WHCode;
                        txtWHName.Text = wh.WHName;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// 24/11/2021 by sailom .k
        /// </summary>
        /// <param name="text"></param>
        private void FindSaleAreaByWHID(string text)
        {
            var _saleAreaList = salAreaDistrictList.Where(x => x.WHID == text.Trim());
            var listOfSalAreaID = _saleAreaList.Select(a => a.SalAreaID).ToList();
            //saleAreaList = bu.GetAllSaleArea().Where(x => listOfSalAreaID.Contains(x.SalAreaID)).ToList();
            saleAreaList = bu.GetAllSaleArea(listOfSalAreaID); //Last edit by sailom.k 14/09/2021 tunning performance

            if (saleAreaList.Count == 0)
            {
                if (!string.IsNullOrEmpty(txtCustomerID.Text))
                {
                    var cust = bu.GetCustomer(txtCustomerID.Text.Trim());
                    if (cust != null && cust.Count > 0)
                    {
                        _saleAreaList = salAreaDistrictList.Where(x => x.WHID == cust[0].WHID);
                        listOfSalAreaID = _saleAreaList.Select(a => a.SalAreaID).ToList();
                        saleAreaList = bu.GetAllSaleArea(listOfSalAreaID); //Last edit by sailom.k 14/09/2021 tunning performance
                        saleAreaList = saleAreaList.Where(p => p.SalAreaID == cust[0].SalAreaID).ToList();

                        if (saleAreaList.Count == 0)
                            saleAreaList = bu.GetSaleArea(x => x.SalAreaName.Contains(cust[0].WHID.Substring(3, 3)));
                    }
                }
            }

            ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID");

            string empFullName = "";
            var bwh = bu.GetBranchWarehouse(x => x.WHID == text);
            if (bwh != null)
            {
                var emp = allEmp2.FirstOrDefault(x => x.EmpID == bwh.SaleEmpID);//bu.GetEmployee(bwh.SaleEmpID);
                if (emp != null)
                    empFullName = emp.TitleName + " " + emp.FirstName + " " + emp.LastName;
            }

            if (!string.IsNullOrEmpty(empFullName))
            {
                txtEmpCode.Text = empFullName;
            }

        }

        /// <summary>
        /// Last edit by sailom.k 14/09/2021 tunning performance //for support U-Online
        /// </summary>
        private void ValidateUCustomer(string customerID, int currentRowIndex)
        {
            //try
            //{
            //    int i = currentRowIndex;
            //    var cust = bu.GetCustomer(customerID);
            //    if (cust != null && cust.Count > 0)
            //    {
            //        bool isReadOnly = true;
            //        if (cust.FirstOrDefault(x => uOnlineShopType.Contains(x.ShopTypeID)) != null)
            //            isReadOnly = false;

            //        CalculateRow(grdList, i);

            //        grdList.Rows[i].Cells["colLineDiscountType"].ReadOnly = isReadOnly;
            //        grdList.Rows[i].Cells["colLineDiscount"].ReadOnly = isReadOnly;

            //        if (isReadOnly) //normal
            //        {
            //            grdList.Rows[i].Cells["colLineDiscountType"].Style.BackColor = ColorTranslator.FromHtml("#DCDCDC");
            //            grdList.Rows[i].Cells["colLineDiscount"].Style.BackColor = ColorTranslator.FromHtml("#DCDCDC");


            //            grdList.Rows[i].Cells["colLineDiscount"].Value = 0;
            //            CalculateTotal();
            //        }
            //        else //u-online
            //        {
            //            grdList.Rows[i].Cells["colLineDiscountType"].Style.BackColor = Color.White;
            //            grdList.Rows[i].Cells["colLineDiscount"].Style.BackColor = Color.White;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.WriteLog(this.GetType());

            //    string msg = ex.Message;
            //    msg.ShowErrorMessage();
            //}
        }

        /// <summary>
        /// for support U-Online
        /// </summary>
        /// <param name="customerID"></param>
        private void ValidateUCustomer(string customerID)
        {
            //try
            //{
            //    var cust = bu.GetCustomer(customerID);
            //    if (cust != null && cust.Count > 0)
            //    {
            //        bool isReadOnly = true;
            //        if (cust.FirstOrDefault(x => uOnlineShopType.Contains(x.ShopTypeID)) != null)
            //            isReadOnly = false;

            //        for (int i = 0; i < grdList.RowCount; i++)
            //        {
            //            CalculateRow(grdList, i);

            //            grdList.Rows[i].Cells["colLineDiscountType"].ReadOnly = isReadOnly;
            //            grdList.Rows[i].Cells["colLineDiscount"].ReadOnly = isReadOnly;

            //            if (isReadOnly) //normal
            //            {
            //                grdList.Rows[i].Cells["colLineDiscountType"].Style.BackColor = ColorTranslator.FromHtml("#DCDCDC");
            //                grdList.Rows[i].Cells["colLineDiscount"].Style.BackColor = ColorTranslator.FromHtml("#DCDCDC");

            //                //if (grdList.Rows[i].Cells["colLineDiscountType"] is DataGridViewComboBoxCell)
            //                //{
            //                //    var cbo = (DataGridViewComboBoxCell)grdList.Rows[i].Cells["colLineDiscountType"];
            //                //    cbo.Value = "N";
            //                //}

            //                grdList.Rows[i].Cells["colLineDiscount"].Value = 0;
            //                CalculateTotal();
            //            }
            //            else //u-online
            //            {
            //                grdList.Rows[i].Cells["colLineDiscountType"].Style.BackColor = Color.White;
            //                grdList.Rows[i].Cells["colLineDiscount"].Style.BackColor = Color.White;
            //            }

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.WriteLog(this.GetType());

            //    string msg = ex.Message;
            //    msg.ShowErrorMessage();
            //}
        }

        private void InitPODtHeader()
        {
            dtpDocDate.SetDateTimePickerFormat();
            dtpDueDate.SetDateTimePickerFormat();

            ddlDocStatus.BindDropdown3DocStatus(bu);

            Dictionary<string, string> remarkList = new Dictionary<string, string>();
            remarkList.Add("สินค้าคงคลังไม่เพียงพอ", "สินค้าคงคลังไม่เพียงพอ");
            remarkList.Add("ลูกค้ายกเลิกหลังส่งออเดอร์", "ลูกค้ายกเลิกหลังส่งออเดอร์");
            remarkList.Add("อื่น ๆ", "อื่น ๆ");
            cboRemark.BindDropdownList(remarkList, "Key", "Value", 0);

            ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID", null, null);
            ddlSaleArea.Enabled = true;

            allEmp2 = bu.GetEmployee();
            allBranchWarehouse = bu.GetAllBranchWarehouse();

            var employee = allEmp2.FirstOrDefault(x => x.EmpID == Helper.tbl_Users.EmpID); //bu.GetEmployee(Helper.tbl_Users.EmpID);
            txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName, employee.LastName);
            btnReCalc.Enabled = true;

            ClearPromotionTemp();

            emp = employee;// bu.GetEmployee(Helper.tbl_Users.EmpID);
            supp = bu.GetSupplier(txtCustomerID.Text);

            allEmp = new Dictionary<string, string>();
            allEmp2.ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty) + x.LastName.Replace(" ", string.Empty))));
        }

        private void InitialPODtData()
        {
            allProduct = bu.tbl_Product;

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
            isAdd = true; //Last edit by sailom.k 14/09/2021 tunning performance

            allEmp2 = bu.GetEmployee();
            allBranchWarehouse = bu.GetAllBranchWarehouse();

            var employee = allEmp2.FirstOrDefault(x => x.EmpID == Helper.tbl_Users.EmpID); //bu.GetEmployee(Helper.tbl_Users.EmpID);
            txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName, employee.LastName);
            btnReCalc.Enabled = true;

            ClearPromotionTemp();

            emp = employee;// bu.GetEmployee(Helper.tbl_Users.EmpID);
            supp = bu.GetSupplier(txtCustomerID.Text);

            allEmp = new Dictionary<string, string>();
            allEmp2.ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty) + x.LastName.Replace(" ", string.Empty))));



            //txtCustomerCode.Focus();
        }

        private void PreparePOMaster(bool editFlag = false)
        {
            var cDate = DateTime.Now;

            bu.tbl_POMaster = new tbl_POMaster();

            if (emp == null || emp.EmpID == null)
                emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            if (supp == null || supp.SupplierID == null)
                supp = bu.GetSupplier(txtCustomerID.Text);

            if (allEmp.Count == 0)
                allEmp2.ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty) + x.LastName.Replace(" ", string.Empty))));

            var selEmp = allEmp.FirstOrDefault(x => x.Value == txtEmpCode.Text.Replace(" ", string.Empty));
            //KeyValuePair<string, string> selEmp = new KeyValuePair<string, string>();
            //bu.GetEmployee().ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty))));
            //selEmp = allEmp.FirstOrDefault(x => x.Value == txtEmpCode.Text.Replace(" ", string.Empty));
            var vanWH = bu.GetAllBranchWarehouse().FirstOrDefault(x => x.SaleEmpID == selEmp.Key);

            var po = bu.tbl_POMaster;
            bool checkEditMode = bu.CheckExistsPO_PRE(txdDocNo.Text);

            if (checkEditMode)
                po.DocNo = txdDocNo.Text;
            else
                po.DocNo = bu.GenDocNoPre(docTypeCode, txtWHCode.Text);

            po.RevisionNo = 0;
            po.DocTypeCode = "IV";
            po.DocStatus = "3";// ddlDocStatus.SelectedValue.ToString();
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

            //if (ddlSaleArea.SelectedValue == null)
            //{
            //    saleAreaList.AddRange(bu.tbl_SalArea);
            //    ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID", null, null);
            //}

            if (ddlSaleArea.SelectedValue != null)
            {
                po.SalAreaID = ddlSaleArea.SelectedValue.ToString();
            }

            po.Address = txtBillTo.Text;
            po.ContactName = txtContact.Text;
            po.ContactTel = txtTelephone.Text;
            po.Shipto = txtBillTo.Text;

            po.CreditDay = Convert.ToInt16(nudCreditDay.Value);
            //Func<tbl_ArCustomer, bool> func = (x => x.CustomerCode == txtCustomerCode.Text);
            //var cust = bu.GetCustomer(func);
            var cust = bu.GetCustomer(txtCustomerID.Text); //edit by sailom.k 10/12/2021
            if (cust != null && cust.Count > 0)
            {
                po.CreditDay = cust[0].CreditDay;
                po.CustType = cust[0].CustomerTypeID.Value.ToString();
            }
            po.DueDate = dtpDocDate.Value.AddDays(po.CreditDay.Value);
            po.CustomerID = txtCustomerID.Text;
            po.CustName = txtCustName.Text;
            po.CustInvNO = string.IsNullOrEmpty(txtCustInvNO.Text) ? null : txtCustInvNO.Text; //05022021
            po.CustInvDate = null;
            po.CustPODate = dtpDocDate.Value;
            po.CustPONo = txtCustPONo.Text;
            po.Remark = "";
            if (!string.IsNullOrEmpty(cboRemark.Text)) //(cboRemark.SelectedValue != null && !string.IsNullOrEmpty(cboRemark.SelectedValue.ToString()))
            {
                po.Remark = cboRemark.Text;
            }

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

            //if (pro.tbl_HQ_Promotion_Hit_Temps != null && pro.tbl_HQ_Promotion_Hit_Temps.Count > 0)
            //po.Discount = pro.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt).Value.ToDecimalN2();

            proTmp.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();
            if (proTmp.tbl_HQ_Promotion_Hit_Temps.Any(x => x.DisCountAmt.Value > 0))
                po.Discount = proTmp.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value).ToDecimalN2();
            else
                po.Discount = 0;

            po.TotalDue = Convert.ToDecimal(txnTotalDue.Text);
            po.ApprovedBy = null;
            po.ApprovedDate = null;
            po.PayType = 0;

            var tmpPO = bu.GetPOMasterPRE(po.DocNo); //when edit po pre-order by asmin
            if (tmpPO != null)
            {
                po.CrDate = tmpPO.CrDate;
                po.CrUser = tmpPO.CrUser;
            }
            else //when create po pre-order by admin
            {
                po.CrDate = cDate;
                po.CrUser = Helper.tbl_Users.Username;
                po.EdDate = cDate;
                po.EdUser = Helper.tbl_Users.Username;
            }

            if (editFlag)
            {
                po.EdDate = cDate;
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
            bu.tbl_PODetails.Clear();

            var poDts = bu.tbl_PODetails;
            DateTime crDate = DateTime.Now;
            lineTotalDisc = new Dictionary<string, decimal>();

            for (int i = 0; i < grdList.RowCount; i++)
            {
                var poDt = new tbl_PODetail();

                var prdCodeCell = grdList.Rows[i].Cells[0];
                var prdNameCell = grdList.Rows[i].Cells[2];
                var uomTypeCell = grdList.Rows[i].Cells[3];
                var orderQtyCell = grdList.Rows[i].Cells[4];
                var priceCell = grdList.Rows[i].Cells[5];
                var vatCell = grdList.Rows[i].Cells[6];
                var discountTypeCell = grdList.Rows[i].Cells[7];
                var discountAmtCell = grdList.Rows[i].Cells[8];
                var lineAmt = grdList.Rows[i].Cells[9];
                var lineTotalDiscCell = grdList.Rows[i].Cells["colLineDiscount"];

                if (prdCodeCell.IsNotNullOrEmptyCell() && prdNameCell.IsNotNullOrEmptyCell() && priceCell.IsNotNullOrEmptyCell() && discountTypeCell.IsNotNullOrEmptyCell())
                {
                    decimal _unitPrice = 0;
                    if (decimal.TryParse(priceCell.Value.ToString(), out _unitPrice))
                    {
                        if (!string.IsNullOrEmpty(prdNameCell.EditedFormattedValue.ToString()) && (discountTypeCell.Value.ToString() == "N" || discountTypeCell.Value.ToString() == "A")) //(_unitPrice > 0) //05022021
                        {
                            int lineNo = poDts.Count > 0 ? Convert.ToInt32(poDts.Max(x => x.Line)) + 1 : 0;

                            poDt.DocNo = bu.tbl_POMaster.DocNo;
                            poDt.Line = Convert.ToInt16(lineNo);
                            poDt.ProductID = prdCodeCell.EditedFormattedValue.ToString();
                            poDt.ProductName = prdNameCell.EditedFormattedValue.ToString();

                            var uom = allUOM.FirstOrDefault(x => x.ProductUomName == uomTypeCell.EditedFormattedValue.ToString());
                            if (uom != null)
                            {
                                poDt.OrderUom = Convert.ToInt32(uom.ProductUomID);
                            }

                            poDt.OrderQty = Convert.ToDecimal(orderQtyCell.EditedFormattedValue);
                            poDt.ReceivedQty = Convert.ToDecimal(orderQtyCell.EditedFormattedValue);
                            poDt.RejectedQty = 0;
                            poDt.StockedQty = 0;

                            decimal unitCost = 0;
                            //Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == poDt.OrderUom && x.ProductID == poDt.ProductID);
                            //var prdPriceGroup = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList();// bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);
                            //if (prdPriceGroup != null && prdPriceGroup.Count > 0)
                            //    unitCost = prdPriceGroup[0].BuyPrice.Value;

                            //for support pre-order-------------------------
                            var _uom = allUomSet.FirstOrDefault(x => x.UomSetID == poDt.OrderUom && x.ProductID == poDt.ProductID);
                            if (_uom != null)
                            {
                                unitCost = _uom.BaseQty;
                            }

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

                            //Distribute discount prd promotion edit by sailom 30402021-------------------------------------------------------------------------------------------------------------------------------
                            bool isDistributeDiscountPromotion = false;
                            if (pro.tbl_HQ_Promotion_Hits != null && pro.tbl_HQ_Promotion_Hits.Count > 0)
                            {
                                foreach (var item in pro.tbl_HQ_Promotion_Hits)
                                {
                                    var promotions = pro.GetHQPromotion(x => x.PromotionID == item.PromotionID);

                                    if (promotions != null && promotions.Count > 0)
                                    {
                                        var prdProItems = promotions.Where(x => x.PromotionPattern == "prd").ToList();
                                        var txnProItems = promotions.Where(x => x.PromotionPattern == "txn").ToList();

                                        if (prdProItems.Count > 0 &&
                                            prdProItems.All(x => string.IsNullOrEmpty(x.PruductGroupRewardID)) &&
                                            prdProItems.Sum(x => x.DisCountAmt) > 0) //prd + discount
                                        {
                                            var skuG = pro.GetHQSKUGroup(x => prdProItems.Select(y => y.SKUGroupID1).ToList().Contains(x.SKUGroupID));
                                            foreach (var prdItem in skuG)
                                            {
                                                if (poDt.ProductID == prdItem.SKU_ID)
                                                {
                                                    isDistributeDiscountPromotion = true;
                                                }
                                            }
                                        }
                                        if (txnProItems.Count > 0 && txnProItems[0].DisCountAmt != null && txnProItems[0].DisCountAmt > 0) //txn + discount
                                        {
                                            var excList = pro.GetHQSKUGroup_EXC().Select(x => x.SKU_ID).ToList(); //No Distribute discount to except product 02032021 by sailom
                                            if (!excList.Contains(poDt.ProductID))
                                            {
                                                isDistributeDiscountPromotion = true;
                                            }
                                        }
                                    }
                                }
                            }

                            if (isDistributeDiscountPromotion)
                            {
                                if (isSave == 1)
                                    DistributeDiscountPromotion(poDt);
                                else if (isSave == 2)
                                    DistributeDiscountPromotionTemp(poDt);

                                lineTotalDisc.Add(poDt.ProductID, poDt.LineDiscount);
                            }
                            //Distribute discount prd promotion edit by sailom 30402021-------------------------------------------------------------------------------------------------------------------------------

                            poDts.Add(poDt);

                            //if (isSave == 1)
                            //    DistributeFreePromotion(poDts, poDt);
                        }
                    }
                }
            }


            //Distribute discount 02032021 by sailom
            decimal totalDiscount = 0.00m;
            proTmp.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();

            if (proTmp.tbl_HQ_Promotion_Hit_Temps.Any(x => x.DisCountAmt.Value > 0))
                totalDiscount = proTmp.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value);

            decimal totalDisc = totalDiscount;
            //if (decimal.TryParse(totalDiscount, out totalDisc))
            {
                if (totalDiscount > 0 && lineTotalDisc.Sum(x => x.Value) > 0)
                {
                    if (totalDisc != lineTotalDisc.Sum(x => x.Value))
                    {
                        decimal diff = 0.00m;
                        diff = lineTotalDisc.Sum(x => x.Value) - totalDisc;

                        if (diff < 0)
                            diff = diff * -1;

                        var updateDisc = poDts.FirstOrDefault(x => x.ProductID == lineTotalDisc.Last().Key);
                        if (updateDisc != null)
                        {
                            if (lineTotalDisc.Sum(x => x.Value) > totalDisc)
                            {
                                updateDisc.LineDiscount -= diff;
                            }
                            else if (totalDisc > lineTotalDisc.Sum(x => x.Value))
                            {
                                updateDisc.LineDiscount += diff;
                            }
                        }
                    }
                }
            }
        }

        private void PreparePOMasterCToPRE(tbl_POMaster obj)
        {
            bu.tbl_POMaster_PRE = new tbl_POMaster_PRE();

            var po = bu.tbl_POMaster_PRE;
            po.DocNo = obj.DocNo;
            po.RevisionNo = obj.RevisionNo;
            po.DocTypeCode = obj.DocTypeCode;
            po.DocStatus = obj.DocStatus;
            po.DocDate = obj.DocDate;
            po.DocRef = obj.DocRef;
            po.StatusInOut = obj.StatusInOut;
            po.SupplierID = obj.SupplierID;
            po.SuppName = obj.SuppName;
            po.WHID = obj.WHID;
            po.EmpID = obj.EmpID;
            po.SaleEmpID = obj.SaleEmpID;
            po.SalAreaID = obj.SalAreaID;
            po.Address = obj.Address;
            po.ContactName = obj.ContactName;
            po.ContactTel = obj.ContactTel;
            po.Shipto = obj.Shipto;
            po.CreditDay = obj.CreditDay;
            po.CustType = obj.CustType;
            po.DueDate = obj.DueDate;
            po.CustomerID = obj.CustomerID;
            po.CustName = obj.CustName;
            po.CustInvNO = obj.CustInvNO;
            po.CustInvDate = obj.CustInvDate;
            po.CustPODate = obj.CustPODate;
            po.CustPONo = obj.CustPONo;
            po.Remark = obj.Remark;
            po.Comment = obj.Comment;
            po.OldAmount = obj.OldAmount;
            po.Amount = obj.Amount;
            po.OldExcVat = obj.OldExcVat;
            po.ExcVat = obj.ExcVat;
            po.OldIncVat = obj.OldIncVat;
            po.IncVat = obj.IncVat;
            po.VatRate = obj.VatRate;
            po.VatAmt = obj.VatAmt;
            po.Freight = obj.Freight;
            po.DiscountType = obj.DiscountType;
            po.OldDiscount = obj.OldDiscount;
            po.Discount = obj.Discount;
            po.TotalDue = obj.TotalDue;
            po.ApprovedBy = obj.ApprovedBy;
            po.ApprovedDate = obj.ApprovedDate;
            po.PayType = obj.PayType;
            po.CrDate = obj.CrDate;
            po.CrUser = obj.CrUser;
            po.EdDate = obj.EdDate;
            po.EdUser = obj.EdUser;
            po.FlagDel = obj.FlagDel;
            po.FlagSend = obj.FlagSend;
            po.OldTotalCom = obj.OldTotalCom;
            po.TotalCom = obj.TotalCom;
            po.CNType = obj.CNType;
            po.DiscountRate = obj.DiscountRate;

            obj = null;
        }

        private void PreparePODetailCToPRE(List<tbl_PODetail> objs)
        {
            bu.tbl_PODetails_PRE.Clear();

            var poDts = bu.tbl_PODetails_PRE;

            foreach (tbl_PODetail obj in objs)
            {
                var poDt = new tbl_PODetail_PRE();

                poDt.DocNo = obj.DocNo;
                poDt.Line = obj.Line;
                poDt.ProductID = obj.ProductID;
                poDt.ProductName = obj.ProductName;
                poDt.OrderUom = obj.OrderUom;
                poDt.OrderQty = obj.OrderQty;
                poDt.ReceivedQty = obj.ReceivedQty;
                poDt.RejectedQty = obj.RejectedQty;
                poDt.StockedQty = obj.StockedQty;
                poDt.LineTotal = obj.LineTotal;
                poDt.UnitCost = obj.UnitCost;
                poDt.UnitPrice = obj.UnitPrice;
                poDt.VatType = obj.VatType;
                poDt.CustType = obj.CustType;
                poDt.CrDate = obj.CrDate;
                poDt.CrUser = obj.CrUser;
                poDt.EdDate = obj.EdDate;
                poDt.EdUser = obj.EdUser;
                poDt.FlagDel = obj.FlagDel;
                poDt.FlagSend = obj.FlagSend;
                poDt.UnitComPrice = obj.UnitComPrice;
                poDt.LineComTotal = obj.LineComTotal;
                poDt.LineRemark = obj.LineRemark;
                poDt.FreeQty = obj.FreeQty;
                poDt.FreeUom = obj.FreeUom;
                poDt.FreeUnit = obj.FreeUnit;
                poDt.LineDiscountType = obj.LineDiscountType;

                poDts.Add(poDt);
            }

            objs.Clear();
        }

        private void PreparePOMasterPREToC(tbl_POMaster_PRE obj)
        {
            bu.tbl_POMaster = new tbl_POMaster();

            var po = bu.tbl_POMaster;
            po.DocNo = obj.DocNo;
            po.RevisionNo = obj.RevisionNo;
            po.DocTypeCode = obj.DocTypeCode;
            po.DocStatus = obj.DocStatus;
            po.DocDate = obj.DocDate;
            po.DocRef = obj.DocRef;
            po.StatusInOut = obj.StatusInOut;
            po.SupplierID = obj.SupplierID;
            po.SuppName = obj.SuppName;
            po.WHID = obj.WHID;
            po.EmpID = obj.EmpID;
            po.SaleEmpID = obj.SaleEmpID;
            po.SalAreaID = obj.SalAreaID;
            po.Address = obj.Address;
            po.ContactName = obj.ContactName;
            po.ContactTel = obj.ContactTel;
            po.Shipto = obj.Shipto;
            po.CreditDay = obj.CreditDay;
            po.CustType = obj.CustType;
            po.DueDate = obj.DueDate;
            po.CustomerID = obj.CustomerID;
            po.CustName = obj.CustName;
            po.CustInvNO = obj.CustInvNO;
            po.CustInvDate = obj.CustInvDate;
            po.CustPODate = obj.CustPODate;
            po.CustPONo = obj.CustPONo;
            po.Remark = obj.Remark;
            po.Comment = obj.Comment;
            po.OldAmount = obj.OldAmount;
            po.Amount = obj.Amount;
            po.OldExcVat = obj.OldExcVat;
            po.ExcVat = obj.ExcVat;
            po.OldIncVat = obj.OldIncVat;
            po.IncVat = obj.IncVat;
            po.VatRate = obj.VatRate;
            po.VatAmt = obj.VatAmt;
            po.Freight = obj.Freight;
            po.DiscountType = obj.DiscountType;
            po.OldDiscount = obj.OldDiscount;
            po.Discount = obj.Discount;
            po.TotalDue = obj.TotalDue;
            po.ApprovedBy = obj.ApprovedBy;
            po.ApprovedDate = obj.ApprovedDate;
            po.PayType = obj.PayType;
            po.CrDate = obj.CrDate;
            po.CrUser = obj.CrUser;
            po.EdDate = obj.EdDate;
            po.EdUser = obj.EdUser;
            po.FlagDel = obj.FlagDel;
            po.FlagSend = obj.FlagSend;
            po.OldTotalCom = obj.OldTotalCom;
            po.TotalCom = obj.TotalCom;
            po.CNType = obj.CNType;
            po.DiscountRate = obj.DiscountRate;

            obj = null;
        }

        private void PreparePODetailPREToC(List<tbl_PODetail_PRE> objs)
        {
            bu.tbl_PODetails.Clear();

            var poDts = bu.tbl_PODetails;

            foreach (tbl_PODetail_PRE obj in objs)
            {
                var poDt = new tbl_PODetail();

                poDt.DocNo = obj.DocNo;
                poDt.Line = obj.Line;
                poDt.ProductID = obj.ProductID;
                poDt.ProductName = obj.ProductName;
                poDt.OrderUom = obj.OrderUom;
                poDt.OrderQty = obj.OrderQty;
                poDt.ReceivedQty = obj.ReceivedQty;
                poDt.RejectedQty = obj.RejectedQty;
                poDt.StockedQty = obj.StockedQty;
                poDt.LineTotal = obj.LineTotal;
                poDt.UnitCost = obj.UnitCost;
                poDt.UnitPrice = obj.UnitPrice;
                poDt.VatType = obj.VatType;
                poDt.CustType = obj.CustType;
                poDt.CrDate = obj.CrDate;
                poDt.CrUser = obj.CrUser;
                poDt.EdDate = obj.EdDate;
                poDt.EdUser = obj.EdUser;
                poDt.FlagDel = obj.FlagDel;
                poDt.FlagSend = obj.FlagSend;
                poDt.UnitComPrice = obj.UnitComPrice;
                poDt.LineComTotal = obj.LineComTotal;
                poDt.LineRemark = obj.LineRemark;
                poDt.FreeQty = obj.FreeQty;
                poDt.FreeUom = obj.FreeUom;
                poDt.FreeUnit = obj.FreeUnit;
                poDt.LineDiscountType = obj.LineDiscountType;

                poDts.Add(poDt);
            }

            objs.Clear();
        }

        private void SubDistributeFreePromotion(List<tbl_PODetail> poDts, tbl_HQ_Promotion_Hit_Temp item, string sKUGroupRewardID, int? sKUGroupRewardAmt, DateTime crDate)
        {
            var itemGroup = pro.GetHQSKUGroup(x => x.SKUGroupID == sKUGroupRewardID);
            if (itemGroup.Count > 0)
            {
                var _poDt = new tbl_PODetail();
                //poDt.CopyTo(_poDt);
                if (_poDt != null)
                {
                    int _lineNo = poDts.Count > 0 ? Convert.ToInt32(poDts.Max(x => x.Line)) + 1 : 0;
                    _poDt.DocNo = bu.tbl_POMaster.DocNo;
                    _poDt.ProductID = itemGroup[0].SKU_ID;

                    var allProduct = bu.tbl_Product;
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
                            //_poDt.UnitCost = prdPrices[0].BuyPrice.Value;
                            //for support pre-order-------------------------
                            var _uom = allUomSet.FirstOrDefault(x => x.UomSetID == _poDt.OrderUom && x.ProductID == _poDt.ProductID);
                            if (_uom != null)
                            {
                                _poDt.UnitCost = _uom.BaseQty;
                            }
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

                        if (_poDt.ProductName.Contains("ชั้นวาง")) // edit by sailom .k 17/11/2021
                        {
                            if (!string.IsNullOrEmpty(item.ShelfID) && item.ShelfID != "000")
                                bu.tbl_PODetails.Add(_poDt);
                        }
                        else
                        {
                            bu.tbl_PODetails.Add(_poDt);
                        }
                    }
                }
            }
        }

        private void SubDistributeFreeMMCHPromotion(List<tbl_PODetail> poDts, Dictionary<string, int> mmchProList, string rewardID, DateTime crDate)
        {

            if (mmchProList.Count > 0)
            {
                foreach (var item in mmchProList)
                {
                    var _poDt = new tbl_PODetail();
                    //poDt.CopyTo(_poDt);
                    if (_poDt != null)
                    {
                        int _lineNo = poDts.Count > 0 ? Convert.ToInt32(poDts.Max(x => x.Line)) + 1 : 0;
                        _poDt.DocNo = bu.tbl_POMaster.DocNo;
                        _poDt.ProductID = item.Key;

                        var allProduct = bu.tbl_Product;
                        var freePrd = allProduct.Where(x => x.ProductID == item.Key).ToList();
                        if (freePrd != null && freePrd.Count > 0)
                        {
                            var prd = freePrd[0];
                            _poDt.ProductName = prd.ProductName;

                            if (prd.SaleUomID == 1 || prd.SaleUomID == 2)
                                _poDt.OrderUom = 2;
                            else
                                _poDt.OrderUom = prd.SaleUomID;

                            _poDt.OrderQty = item.Value;
                            _poDt.ReceivedQty = item.Value;
                            _poDt.VatType = 0;
                            _poDt.RejectedQty = 0;
                            _poDt.StockedQty = 0;

                            _poDt.Line = Convert.ToInt16(_lineNo);
                            _poDt.LineTotal = 0;

                            var prdPrices = allProductPrice.Where(x => x.ProductUomID == prd.SaleUomID && x.ProductID == item.Key).ToList(); // bu.GetProductPriceGroup(x => x.ProductUomID == prd.SaleUomID && x.ProductID == itemGroup[0].SKU_ID);
                            if (prdPrices != null && prdPrices.Count > 0)
                            {
                                _poDt.LineDiscount = prdPrices[0].SellPriceVat.Value;
                                _poDt.UnitPrice = prdPrices[0].SellPriceVat.Value;
                                //_poDt.UnitCost = prdPrices[0].BuyPrice.Value;
                                //for support pre-order-------------------------
                                var _uom = allUomSet.FirstOrDefault(x => x.UomSetID == _poDt.OrderUom && x.ProductID == _poDt.ProductID);
                                if (_uom != null)
                                {
                                    _poDt.UnitCost = _uom.BaseQty;
                                }
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

                            _poDt.LineRemark = pro.GetHQReward(x => x.RewardID == rewardID)[0].RewardName;
                            bu.tbl_PODetails.Add(_poDt);
                        }
                    }
                }
            }
        }

        private void DistributeFreePromotion(List<tbl_PODetail> poDts)
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
                        var proInfo = pro.GetHQPromotion(a => a.PromotionID == item.PromotionID);
                        if (proInfo.Count > 0)
                        {
                            if (proInfo[0].PromotionType == "mmch")
                            {
                                var frm = new frmPromotionProduct();
                                SubDistributeFreeMMCHPromotion(poDts, frm.GetMMCHList(), proInfo[0].RewardID, crDate);
                            }
                            else
                            {
                                SubDistributeFreePromotion(poDts, item, item.SKUGroupRewardID, item.SKUGroupRewardAmt, crDate);

                                if (!string.IsNullOrEmpty(item.SKUGroupRewardID2))
                                {
                                    SubDistributeFreePromotion(poDts, item, item.SKUGroupRewardID2, item.SKUGroupRewardAmt2, crDate);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DistributeDiscountPromotion(tbl_PODetail poDt)
        {
            if (pro.tbl_HQ_Promotion_Hits != null && pro.tbl_HQ_Promotion_Hits.Count > 0)
            {
                List<tbl_HQ_Promotion_Hit> proHits = new List<tbl_HQ_Promotion_Hit>();

                proHits = pro.tbl_HQ_Promotion_Hits.Where(x => x.DocNo == poDt.DocNo && string.IsNullOrEmpty(x.SKUGroupRewardID)).ToList();

                var newObj = proHits.Select(x => new PromotionHitTempModel { SKUGroupID = x.SKUGroupID, PromotionID = x.PromotionID, DisCountAmt = x.DisCountAmt }).ToList();
                SubDistributeDiscountPro(newObj, poDt);
            }
        }

        private void DistributeDiscountPromotionTemp(tbl_PODetail poDt)
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

        private void SubDistributeDiscountPro(List<PromotionHitTempModel> proHits, tbl_PODetail poDt)
        {
            Dictionary<decimal, decimal> _allDiscount = new Dictionary<decimal, decimal>();
            List<string> _lineRemarkList = new List<string>();
            var skuExcept = pro.GetHQSKUGroup_EXC();

            if (proHits != null && proHits.Count > 0)
            {
                _allDiscount = CalcTotalDiscountPro(proHits, _lineRemarkList, poDt.ProductID);

                var allHQSKUGroup = pro.GetHQSKUGroup();

                decimal totalPrdQty = 0;
                decimal totalTxnQty = 0;
                //Dictionary<string, decimal> lineTotal = new Dictionary<string, decimal>();
                decimal lineTotal = 0;
                decimal exceptAmt = 0;

                for (int i = 0; i < grdList.RowCount; i++)
                {
                    string prdID = "";
                    string uomName = "";
                    decimal _orderQty = 0.00m;
                    decimal unitPrc = 0;
                    decimal _lineTotal = 0.00m;

                    var prdCodeCell = grdList.Rows[i].Cells[0];
                    var prdNameCell = grdList.Rows[i].Cells[2];
                    var uomTypeCell = grdList.Rows[i].Cells[3];
                    var orderQtyCell = grdList.Rows[i].Cells[4];
                    var unitPriceCell = grdList.Rows[i].Cells[5];
                    var lineTotalCell = grdList.Rows[i].Cells["colLineTotal"];

                    if (prdCodeCell.IsNotNullOrEmptyCell() && prdNameCell.IsNotNullOrEmptyCell())
                    {
                        prdID = prdCodeCell.EditedFormattedValue.ToString();
                        uomName = uomTypeCell.EditedFormattedValue.ToString();
                        _orderQty = Convert.ToDecimal(orderQtyCell.EditedFormattedValue);
                        unitPrc = Convert.ToDecimal(unitPriceCell.EditedFormattedValue);
                        _lineTotal = Convert.ToDecimal(lineTotalCell.EditedFormattedValue);

                        //02032021 by sailom
                        if (poDt.ProductID == prdID)
                        {
                            lineTotal = _lineTotal;
                            //lineTotal.Add(poDt.ProductID, _lineTotal);
                        }

                        var excList = skuExcept.Select(x => x.SKU_ID).ToList(); //No Distribute discount to except product 02032021 by sailom
                        if (excList.Contains(prdID))
                        {
                            exceptAmt += _lineTotal;
                        }

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
                        var prdDiscount = (prdDis / totalPrdQty) * (_unitQty);/////////////////////////////////////////////////////////// cal prd promotion
                        _lineDisount += prdDiscount.ToDecimalN2();
                    }
                    if (_allDiscount.Last().Value > 0) //txn
                    {
                        bool isPlusDiscount = true;

                        var txtPro = pro.GetHQPromotion(x => proHits.Select(a => a.PromotionID).ToList().Contains(x.PromotionID));
                        if (txtPro != null && txtPro.Count > 0)
                        {
                            if (txtPro.Any(x => x.PromotionPattern.ToLower() == "txn"))
                            {
                                var exSku = pro.GetHQSKUGroup_EXC(x => x.SKU_ID == poDt.ProductID);
                                if (exSku != null && exSku.Count > 0)
                                {
                                    isPlusDiscount = false;
                                }
                            }
                        }

                        if (isPlusDiscount)
                        {
                            var txnDis = _allDiscount.Sum(x => x.Value);//////////////////////////////////////////////////////////////must except prd except put-------------------------------------
                                                                        //02032021 by sailom
                            decimal totalDue = 1;
                            if (decimal.TryParse(txnAmount.Text, out totalDue))
                            {
                                totalDue = totalDue - exceptAmt;
                                _lineDisount += txnDis * (lineTotal / totalDue);
                            }

                            //var txnDiscount = (txnDis / totalTxnQty) * (_unitQty);
                            //_lineDisount += txnDiscount.ToDecimalN2();
                        }
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
            var po = bu.tbl_POMaster_PRE;
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

        private void Save()
        {
            try
            {
                if (!ValidateSave())
                    return;

                string docno = string.Empty;
                bool editFlag = true;
                int ret = 0;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                bu.tbl_POMaster = new tbl_POMaster();
                bu.tbl_PODetails.Clear();
                bu.tbl_InvMovements.Clear();
                bu.tbl_InvTransactions.Clear();
                bu.tbl_DocRunning.Clear();

                bool checkEditMode = bu.CheckExistsPO_PRE(txdDocNo.Text);
                if (checkEditMode)
                {
                    bu = new TabletSales();

                    docno = txdDocNo.Text;
                    editFlag = true;
                }
                else
                {
                    bu = new TabletSales();

                    docno = txdDocNo.Text;
                    editFlag = false;
                }

                if (ddlDocStatus.SelectedValue.ToString() == "5")
                {
                    bu.tbl_POMaster_PRE = new tbl_POMaster_PRE();
                    bu.tbl_POMaster_PRE = bu.GetPOMasterPRE(docno);

                    var po = bu.tbl_POMaster_PRE;
                    po.DocNo = txdDocNo.Text;
                    po.DocStatus = ddlDocStatus.SelectedValue.ToString();
                    po.Remark = "";
                    if (!string.IsNullOrEmpty(cboRemark.Text))  //if (cboRemark.SelectedValue != null && !string.IsNullOrEmpty(cboRemark.SelectedValue.ToString()))
                    {
                        po.Remark = cboRemark.Text;
                    }
                    bu.tbl_POMaster.EdDate = DateTime.Now;
                    bu.tbl_POMaster.EdUser = Helper.tbl_Users.Username;

                    //po.Remark = txtRemark.Text;
                    po.Comment = txtComment.Text;
                }
                else
                {
                    bool validateOverStockByProduct = false;
                    string msg = "มีสินค้าบางรายการ ไม่มีในคลัง 1000!!";
                    for (int i = 0; i < grdList.RowCount; i++)
                    {
                        var prdCodeCell = grdList.Rows[i].Cells[0];
                        var prdNameCell = grdList.Rows[i].Cells[2];
                        var uomTypeCell = grdList.Rows[i].Cells[3];
                        var orderQtyCell = grdList.Rows[i].Cells[4];
                        var priceCell = grdList.Rows[i].Cells[5];
                        var vatCell = grdList.Rows[i].Cells[6];
                        var discountTypeCell = grdList.Rows[i].Cells[7];
                        var discountAmtCell = grdList.Rows[i].Cells[8];
                        var lineAmt = grdList.Rows[i].Cells[9];
                        var lineTotalDiscCell = grdList.Rows[i].Cells["colLineDiscount"];

                        if (prdCodeCell.IsNotNullOrEmptyCell() && prdNameCell.IsNotNullOrEmptyCell() && priceCell.IsNotNullOrEmptyCell() && discountTypeCell.IsNotNullOrEmptyCell())
                        {
                            decimal _unitPrice = 0;
                            if (decimal.TryParse(priceCell.Value.ToString(), out _unitPrice))
                            {
                                if (!string.IsNullOrEmpty(prdNameCell.EditedFormattedValue.ToString()) && (discountTypeCell.Value.ToString() == "N" || discountTypeCell.Value.ToString() == "A")) //(_unitPrice > 0) //05022021
                                {
                                    var _orderUom = 0;
                                    var _productID = prdCodeCell.EditedFormattedValue.ToString();
                                    var uom = allUOM.FirstOrDefault(x => x.ProductUomName == uomTypeCell.EditedFormattedValue.ToString());
                                    if (uom != null)
                                        _orderUom = Convert.ToInt32(uom.ProductUomID);

                                    var _receivedQty = Convert.ToDecimal(orderQtyCell.EditedFormattedValue);

                                    if (string.IsNullOrEmpty(docno)) //when create new order by copu function before confirm order
                                        docno = bu.GenDocNoPre(docTypeCode, txtWHCode.Text);

                                    docno = preBu.FilterDocNoWithAutoGenByPRD(docno, txtWHCode.Text, _productID, _orderUom, _receivedQty);

                                    if (string.IsNullOrEmpty(docno))
                                    {
                                        validateOverStockByProduct = true;
                                        msg = _productID + ":" + prdNameCell.EditedFormattedValue.ToString() + " ไม่มีในคลัง 1000!!";
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (validateOverStockByProduct)
                    {
                        msg.ShowInfoMessage();
                        return;
                    }

                    CalcPromotion(true);

                    bu.PrepareDocRunning(docTypeCode);

                    PreparePOMaster(editFlag);

                    //validate docno by sailom.k 27-05-2021
                    if (bu.tbl_POMaster.DocNo.Length < 12)
                    {
                        this.ShowDocNoProcessErr();
                        return;
                    }

                    PreparePromotion();
                    PreparePODetail(1, editFlag); //Calc Pro

                    DistributeFreePromotion(bu.tbl_PODetails);

                    bu.tbl_POMaster_PRE = new tbl_POMaster_PRE();
                    bu.tbl_PODetails_PRE = new List<tbl_PODetail_PRE>();
                    PreparePOMasterCToPRE(bu.tbl_POMaster);
                    PreparePODetailCToPRE(bu.tbl_PODetails);
                }

                bu.tbl_POMaster = new tbl_POMaster();
                bu.tbl_PODetails.Clear();
                bu.tbl_InvMovements.Clear();
                bu.tbl_InvTransactions.Clear();
                bu.tbl_DocRunning.Clear();

                Cursor.Current = Cursors.WaitCursor;

                ret = bu.PerformUpdateData(docTypeCode); //edit by sailom .k 14/12/2021

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

                BindVanSalesData(docno, "IV2");

                if (ret == 1)
                {
                    //this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

                    //btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, txdDocNo.Text);

                    //txdDocNo.Text = docno;

                    Cursor.Current = Cursors.Default;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();



                    InitPODtHeader();

                    //pnlPODT_PRE.ClearControl(bu, docTypeCode, runDigit);
                    var documentType = bu.tbl_DocumentType.FirstOrDefault(x => x.DocTypeCode.Trim() == "IV");
                    if (documentType != null)
                    {
                        docTypeCode = documentType.DocTypeCode;
                        runDigit = documentType.DocFormat.Length;// - 2;

                        pnlPODT_PRE.ClearControl(docTypeCode, runDigit);
                        txtComment.Text = documentType.DocRemark;
                    }

                    btnAdd.Enabled = false;

                    this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");

                    validateNewRow = true;

                    pnlPODT_PRE.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

                    btnCancel.EnableButton(btnSearchDoc);

                    this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

                    BindVanSalesData(docno, "IV2");
                    tabPOMasterPre.SelectedIndex = 1;

                    txdDocNo.DisableTextBox(false);
                    txdDocNo.BackColor = Color.Turquoise;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnCopy.Enabled = false;

                    pnlCompletePO.OpenControl(true, readOnlyControls.ToArray(), cellEdit);
                    pnlPOMst_PRE.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

                    grdList.CellContentClick -= grdList_CellContentClick;




                    //ddlDocStatus.SelectedValue.ToString().CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
                    //btnUpdateAddress.Enabled = true;
                    //btnGenCustIVNo.Enabled = true;
                    //btnReCalc.Enabled = false;

                    //btnEdit.Enabled = false;// ddlDocStatus.SelectedValue.ToString() == "4";
                    SearchPOMst();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    this.ShowProcessErr();
                    return;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void ValidateInputShelf()
        {
            var allProduct = bu.tbl_Product;

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
            var allProduct = bu.tbl_Product;

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

                        var _cust = new tbl_ArCustomerShelf().Select(x => x.ProductID == g.SKU_ID && x.FlagDel == false && x.CustomerID == txtCustomerID.Text).ToList();
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
            var allProduct = bu.tbl_Product;

            var cDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Ticks;
            var docDate = new DateTime(dtpDocDate.Value.Year, dtpDocDate.Value.Month, dtpDocDate.Value.Day).Ticks;

            //for support pre-order edit by sailom.k 02/04/2022
            //if (Helper.tbl_Users.RoleID != 10 && dtpDocDate.Value != null && docDate < cDate)
            //{
            //    string message = "ห้ามเลือกวันที่ย้อนหลัง !!!";
            //    message.ShowWarningMessage();
            //    ret = false;
            //}

            if (ret)
            {
                if (Helper.tbl_Users.RoleID != 10 && !dtpDocDate.ValidateEndDay(bu))
                {
                    string message = "ระบบปิดวันไปแล้ว ไม่สามารถเลือกวันที่นี้ได้ !!!";
                    message.ShowWarningMessage();
                    ret = false;
                }
            }


            if (ret) //validate customer shelf
            {
                var _cust = bu.GetCustomerShelfByCustID(txtCustomerID.Text); // new tbl_ArCustomerShelf().Select(x => x.FlagDel == false && x.CustomerID == txtCustomerID.Text).ToList();
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
                errList.SetErrMessageList(txtCustomerID, lblCustomerCode);
                errList.SetErrMessageList(txtBillTo, lblBillTo);
                errList.SetErrMessageList(txtContact, lblContact);
                errList.SetErrMessageList(txtEmpCode, lblEmpCode);
                errList.SetErrMessageList(txtWHCode, lblWHCode);

                if (errList.Count == 0)
                {
                    //Func<tbl_ArCustomer, bool> func = (x => x.CustomerCode == txtCustomerCode.Text);
                    //var sup = bu.GetCustomer(func);
                    var sup = bu.GetCustomer(txtCustomerID.Text); //edit by sailom.k 10/12/2021
                    if (sup == null)
                    {
                        string t = lblCustomerCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtCustomerID.ErrorTextBox();
                    }
                    if (sup.Count == 0) //edit by sailom 21-06-2021
                    {
                        string message = "ไม่พบข้อมูลร้านค้านี้ในระบบ!!!";
                        errList.Add(message);
                        txtCustomerID.ErrorTextBox();
                    }

                    //Func<tbl_BranchWarehouse, bool> whFunc = (x => x.WHID == txtWHCode.Text);
                    //var wh = bu.GetBranchWarehouse(whFunc);
                    var wh = bu.GetBranchWarehouse(txtWHCode.Text); //Last edit by sailom .k 07/02/2022
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
                    //var allProduct = bu.tbl_Product;
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
                            var invWhItem = bu.ValidatetStockMovement();

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

                                            //var invWhItem = bu.GetInvWarehouse(productID, txtWHCode.Text);
                                            //decimal whQty = 0;

                                            //if (invWhItem != null && invWhItem.Count > 0)
                                            //    whQty = invWhItem[0].QtyOnHand;


                                            decimal whQty = 0;

                                            if (invWhItem != null && invWhItem.Count > 0)
                                                whQty = invWhItem.Where(x => x.WHID == txtWHCode.Text && x.ProductID == productID).Sum(x => x.TrnQty);

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
                                    tempMsg += string.Format("--> เนื่องจากสินค้า {0} ใน Stock มีแค่ {1} หีบ \n", item.ProductID, item.StockQty.ToStringN2());
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

            FilterSaleArea(txtCustomerID.Text);
        }

        private void btnCustInfo_Click(object sender, EventArgs e)
        {
            string _custID = txtCustomerID.Text;

            if (!string.IsNullOrEmpty(_custID))
            {
                MainForm mfrm = null;
                frmCustomerInfo frm = new frmCustomerInfo();
                bool isActive = false;

                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name.ToLower() == "mainform")
                    {
                        mfrm = (MainForm)f;
                    }

                    if (f.Name.ToLower() == frm.Name.ToLower())
                    {
                        f.Activate();
                        isActive = true;
                    }
                }

                if (!isActive)
                {
                    frm.MdiParent = mfrm;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                    frm.BindCustomerInfo(_custID);
                }
            }
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

        CheckBox headerCheckBox2 = new CheckBox();
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
                    //grdPO.CreateCheckBoxHeaderColumn("colSelectRowPO");
                    //Add a CheckBox Column to the DataGridView Header Cell.

                    try
                    {
                        //Find the Location of Header Cell.
                        Point headerCellLocation = grdPO.GetCellDisplayRectangle(0, -1, true).Location;
                        //headerCheckBox = new CheckBox();
                        //Place the Header CheckBox in the Location of the Header Cell.
                        headerCheckBox2.Location = new Point(headerCellLocation.X + 8, headerCellLocation.Y + 2);
                        headerCheckBox2.BackColor = Color.White;
                        headerCheckBox2.Size = new Size(18, 18);

                        headerCheckBox2.Checked = false; // uncheck 

                        //Assign Click event to the Header CheckBox.
                        headerCheckBox2.Click += new EventHandler(HeaderCheckBox2_Clicked);
                        grdPO.Controls.Add(headerCheckBox2);
                        //grdList.CurrentCell = grdList.Rows[0].Cells[0];

                        //Assign Click event to the DataGridView Cell.
                        grdPO.CellContentClick += new DataGridViewCellEventHandler(DataGridView2_CellClick);
                    }
                    catch { }


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

        private void HeaderCheckBox2_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Necessary to end the edit mode of the Cell.
                grdPO.EndEdit();

                //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
                foreach (DataGridViewRow row in grdPO.Rows)
                {
                    DataGridViewCheckBoxCell checkBox = (row.Cells["colSelectRowPO"] as DataGridViewCheckBoxCell);
                    checkBox.Value = headerCheckBox2.Checked;
                }
            }
            catch { }

        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Check to ensure that the row CheckBox is clicked.
                if (e.RowIndex >= 0 && e.ColumnIndex == 0)
                {
                    //Loop to verify whether all row CheckBoxes are checked or not.
                    bool isChecked = true;
                    foreach (DataGridViewRow row in grdPO.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["colSelectRowPO"].EditedFormattedValue) == false)
                        {
                            isChecked = false;
                            break;
                        }
                    }
                    headerCheckBox2.Checked = isChecked;
                }
            }
            catch { }

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

                if (docNo.Contains("M"))
                {
                    frm1000SalesPre frm = new frm1000SalesPre();
                    frm.MdiParent = mfrm;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.WindowState = FormWindowState.Minimized;
                    frm.Show();
                    frm.WindowState = FormWindowState.Maximized;
                    frm.BindVanSalesData(docNo);
                }
                else
                {
                    frmTabletSalesPre frm = new frmTabletSalesPre();
                    frm.MdiParent = mfrm;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.WindowState = FormWindowState.Minimized;
                    frm.Show();
                    frm.WindowState = FormWindowState.Maximized;
                    frm.BindTabletSalesData(docNo);
                    //frm.BindTabletSalesData(docNo, "IV");
                }
            }
        }

        private void grdPO_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPO.SetRowPostPaint(sender, e, this.Font);
            var row = grdPO.Rows[e.RowIndex];

            var _edDate = row.Cells["colEdDate"].Value.ToString();
            var _edUser = row.Cells["colEdUser"].Value.ToString();
            var _flagBill = row.Cells["colFlagBill"].Value.ToString();

            if (!string.IsNullOrEmpty(_edDate) && !string.IsNullOrEmpty(_edUser))
            {
                row.DefaultCellStyle.BackColor = Color.LightYellow;
                row.DefaultCellStyle.ForeColor = Color.Red;
            }
            if (!string.IsNullOrEmpty(_flagBill))
            {
                if (_flagBill == "มีใบกำกับ")
                {
                    row.Cells["colFlagBill"].Style.BackColor = Color.LightGreen;
                    row.Cells["colFlagBill"].Style.ForeColor = Color.Black;
                }
                else if (_flagBill == "ขอใบกำกับ")
                {
                    row.Cells["colFlagBill"].Style.BackColor = Color.DarkOrange;
                    row.Cells["colFlagBill"].Style.ForeColor = Color.Black;
                }
                else if (_flagBill == "-")
                {
                    row.Cells["colFlagBill"].Style.BackColor = Color.LightGray;
                    row.Cells["colFlagBill"].Style.ForeColor = Color.Black;
                }
            }

            var _signaturePO = row.Cells["SignaturePO"].Value;
            if (_signaturePO != DBNull.Value)
            {
                grdPO.Rows[e.RowIndex].Cells["colSignaturePO"].Style.BackColor = Color.LightGreen;
            }
            else
                grdPO.Rows[e.RowIndex].Cells["colSignaturePO"].Style.BackColor = Color.Gray;
        }

        private void grdPO_DataSourceChanged(object sender, EventArgs e)
        {
            if (grdPO.DataSource != null && grdPO.RowCount > 0)
            {
                //grdPO.CreateCheckBoxHeaderColumn("colSelectRowPO");
            }
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

        private void btnDocStatusReport_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> _params = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(dtpSDocDate.Value.ToString()))
            {
                _params.Add("@DocDate", dtpSDocDate.Value);
                this.OpenReportingReportsPopup("รายงานสถานะเอกสาร", "proc_PreOrder_POStatus_Report.rdlc", "proc_PreOrder_POStatus_Report", _params);
            }
        }

        private void grdStock_Sorted(object sender, EventArgs e)
        {
            var grd = (DataGridView)sender;
            if (grd != null && grd.DataSource != null)
            {
                var row = grd.Rows[grd.CurrentCell.RowIndex];
                //foreach (DataGridViewRow row in grdStock.Rows)
                {
                    var _diffCarQty = Convert.ToInt32(row.Cells["colDiffCarQty"].Value);
                    var _diffPckQty = Convert.ToInt32(row.Cells["colDiffPckQty"].Value);
                    var _1000CarQty = Convert.ToInt32(row.Cells["colST000CarQty"].Value);
                    var _1000PckQty = Convert.ToInt32(row.Cells["colST1000PckQty"].Value);

                    if (_diffCarQty < 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    if (_diffPckQty < 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    if (_1000CarQty <= 0 && _1000PckQty <= 0 && _diffCarQty == 0 && _diffPckQty == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void grdPOMst_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            var grd = (DataGridView)sender;
            var docNo = grd.Rows[e.RowIndex].Cells["colDocNo"].Value.ToString();
            if (!string.IsNullOrEmpty(docNo))
            {
                if (e.ColumnIndex == 9) //check signature
                {
                    frmSignaturePicture frm = new frmSignaturePicture();
                    frm.PrepareLoadSignaturePicture(docNo, true);
                    frm.ShowDialog();
                }
                else if (e.ColumnIndex == 10)
                {
                    try
                    {
                        string docno = string.Empty;
                        int ret = 0;

                        string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                        string title = "ยืนยันการบันทึก!!";
                        if (!cfMsg.ConfirmMessageBox(title))
                            return;

                        Cursor.Current = Cursors.WaitCursor;

                        bu.tbl_POMaster = new tbl_POMaster();
                        bu.tbl_PODetails.Clear();
                        bu.tbl_InvMovements.Clear();
                        bu.tbl_InvTransactions.Clear();
                        bu.tbl_DocRunning.Clear();

                        bool checkEditMode = bu.CheckExistsPO_PRE(docNo);
                        if (checkEditMode)
                        {
                            bu = new TabletSales();

                            bu.tbl_POMaster_PRE = new tbl_POMaster_PRE();
                            bu.tbl_POMaster_PRE = bu.GetPOMasterPRE(docNo);
                            if (bu.tbl_POMaster_PRE.DocStatus == "5")
                            {
                                Cursor.Current = Cursors.Default;
                                return;
                            }

                            var po = bu.tbl_POMaster_PRE;
                            po.DocNo = docNo;
                            po.DocStatus = "5";
                            po.Remark = "สินค้าคงคลังไม่เพียงพอ";
                            bu.tbl_POMaster.EdDate = DateTime.Now;
                            bu.tbl_POMaster.EdUser = Helper.tbl_Users.Username;

                            po.Comment = "ยกเลิก pre-order ก่อน confirm order";

                            bu.tbl_POMaster = new tbl_POMaster();
                            bu.tbl_PODetails.Clear();
                            bu.tbl_InvMovements.Clear();
                            bu.tbl_InvTransactions.Clear();
                            bu.tbl_DocRunning.Clear();

                            ret = bu.PerformUpdateData(docTypeCode); //edit by sailom .k 14/12/2021

                            if (ret == 1)
                            {
                                Cursor.Current = Cursors.Default;

                                string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                                msg.ShowInfoMessage();

                                SearchPOMst();
                            }
                            else
                            {
                                Cursor.Current = Cursors.Default;
                                this.ShowProcessErr();
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        ex.WriteLog(this.GetType());

                        string msg = ex.Message;
                        msg.ShowErrorMessage();
                    }
                }
            }
        }

        private void grdPO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            var grd = (DataGridView)sender;
            var docNo = grd.Rows[e.RowIndex].Cells["colDocNoPO"].Value.ToString();
            if (e.ColumnIndex == 13) //check signature
            {
                frmSignaturePicture frm = new frmSignaturePicture();
                frm.PrepareLoadSignaturePicture(docNo, false);
                frm.ShowDialog();
            }
        }

        private void rdoStatusC_CheckedChanged(object sender, EventArgs e)
        {
            SearchPOMst();
        }

        private void rdoStatusN_CheckedChanged(object sender, EventArgs e)
        {
            SearchPOMst();
        }

        private void grdPOMst_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            grdPOMst.ClearSelection();
            if (grdPOMst.Rows.Count > 0)
            {
                grdPOMst.Rows[0].Cells[0].Selected = false;
                grdPOMst.Rows[0].Cells[1].Selected = true;
            }
        }

        private void grdPO_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            grdPO.ClearSelection();
            if (grdPO.Rows.Count > 0)
            {
                grdPO.Rows[0].Cells[0].Selected = false;
                grdPO.Rows[0].Cells[1].Selected = true;
            }
        }

        /// <summary>
        /// for fix error when auto generate RL has fail!!! by sailom.k 21/04/2022
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFixRL_Click(object sender, EventArgs e)
        {
            try
            {
                string cfMsg = "ต้องการสร้าง RL ใช่หรือไม่??";
                string title = "ยืนยัน!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Cursor.Current = Cursors.WaitCursor;
                Dictionary<string, string> rlList = new Dictionary<string, string>();
                foreach (DataGridViewRow row in grdPO.Rows)
                {
                    string _poDocNo = "";
                    string _whName = "";
                    string _rlDocNo = "";
                    _poDocNo = row.Cells["colDocNoPO"].Value.ToString(); // row.Cells["colRLDocNo"].Value.ToString();
                    _whName = row.Cells["colWHNamePO"].Value.ToString();
                    _rlDocNo = row.Cells["colRLDocNo"].Value.ToString();

                    var sel = row.Cells["colSelectRowPO"].Value;

                    if (row.Cells["colSelectRowPO"].IsNotNullOrEmptyCell())
                    {
                        bool chk = false;
                        if (sel != null && bool.TryParse(sel.ToString(), out chk))
                        {
                            if (chk)
                            {
                                if (!string.IsNullOrEmpty(_poDocNo) && !string.IsNullOrEmpty(_whName) && string.IsNullOrEmpty(_rlDocNo))
                                {
                                    if (rlList.Count(x => x.Key == _poDocNo) == 0)
                                    {
                                        var wh = bu.GetBranchWarehouse(x => x.WHName == _whName);
                                        if (wh != null)
                                            rlList.Add(_poDocNo, wh.WHID);
                                    }
                                }
                            }
                        }
                    }
                }

                Cursor.Current = Cursors.Default;

                if (rlList.Count == 0)
                {
                    string _msg = "กรุณาเลือกบิลขาย!!!";
                    _msg.ShowWarningMessage();
                    return;
                }
                if (string.IsNullOrEmpty(txtWHCodePO.Text))
                {
                    string _msg = "กรุณาเลือกครั้งละ 1 แวน!!!";
                    _msg.ShowWarningMessage();
                    return;
                }
                if (rlList.Count > 0 && rlList.Select(x => x.Value).ToList().Distinct().Count() > 1)
                {
                    string _msg = "กรุณาเลือกครั้งละ 1 แวน!!!";
                    _msg.ShowWarningMessage();
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                List<string> tmpDocNo = new List<string>();
                if (rlList.Count > 0)
                {
                    string _docNoList = string.Join(",", rlList.Keys);
                    string whid = rlList.First().Value;

                    var docNo = preBu.GenerateRL(dtpDocDatePO.Value, whid, Helper.tbl_Users.Username, _docNoList); //วันที่ในใบเบิกจะต้องเท่ากับ วันที่ admin ทำการยืนยันเอกสาร PO by sailom .k 11/03/2022 confirm by P'Jang Acc
                    if (!string.IsNullOrEmpty(docNo))
                    {
                        tmpDocNo.Add(docNo);
                    }
                }

                string msg = "";
                if (tmpDocNo.Count > 0)
                {
                    SearchPO();

                    Cursor.Current = Cursors.Default;
                    rlDocNo = string.Join(",", tmpDocNo);

                    msg = "สร้างใบจัดสินค้า เลขที่ : " + rlDocNo + " เรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                }
                else
                {
                    SearchPO();

                    Cursor.Current = Cursors.Default;
                    msg = "ไม่สามารถสร้างใบจัดสินค้าได้!!";
                    msg.ShowInfoMessage();
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;

                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void btnShowVE_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustInvNO.Text))
            {
                MainForm mfrm = null;
                var frmList = Application.OpenForms;
                if (frmList != null && frmList.Count > 0)
                {
                    frmVE frm = new frmVE();
                    bool isActive = false;

                    foreach (Form f in frmList)
                    {
                        if (f.Name.ToLower() == "mainform") //(f.Name == "frmOD")
                        {
                            mfrm = (MainForm)f;
                        }

                        if (f.Name.ToLower() == frm.Name.ToLower())
                        {
                            f.Activate();
                            isActive = true;
                        }
                    }

                    if (!isActive)
                    {
                        frm.MdiParent = mfrm;
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.WindowState = FormWindowState.Minimized;

                        frm.Show();
                        frm.docTypeCode = "V";
                        frm.BindVEData(txtCustInvNO.Text);
                        frm.WindowState = FormWindowState.Maximized;
                    }
                }
            }
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
                                frm.BindProductMovement(txtWHCode.Text, txtWHCode.Text, productID, true);

                            }
                            break;
                        case "รายงานสินค้าคงเหลือ แยกตามคลัง":
                            {
                                var cDate = DateTime.Now.AddDays(2);
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

        private void btnRollback_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdPO.RowCount > 0)
                {
                    string cfMsg = "ต้องการ Rollback บิลขาย ใช่หรือไม่?";
                    string title = "ยืนยันการ Rollback!!";
                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    Cursor.Current = Cursors.WaitCursor;
                    Dictionary<string, string> rlList = new Dictionary<string, string>();
                    foreach (DataGridViewRow row in grdPO.Rows)
                    {
                        string _rlDocNo;
                        string _whName = "";
                        _rlDocNo = row.Cells["colRLDocNo"].Value.ToString();
                        _whName = row.Cells["colWHNamePO"].Value.ToString();

                        var sel = row.Cells["colSelectRowPO"].Value;

                        if (row.Cells["colSelectRowPO"].IsNotNullOrEmptyCell())
                        {
                            bool chk = false;
                            if (sel != null && bool.TryParse(sel.ToString(), out chk))
                            {
                                if (chk)
                                {
                                    if (!string.IsNullOrEmpty(_whName))
                                    {
                                        if (rlList.Count(x => x.Key == _rlDocNo) == 0)
                                        {
                                            var wh = bu.GetBranchWarehouse(x => x.WHName == _whName);
                                            if (wh != null)
                                                rlList.Add(_rlDocNo, wh.WHID);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    foreach (var item in rlList.Distinct().ToList())
                    {
                        string rlDocNo = "";
                        string whid = "";
                        rlDocNo = item.Key;
                        whid = item.Value;

                        bool ret = false;
                        ret = preBu.RollbackOrder(whid, dtpDocDatePO.Value, rlDocNo);

                        string msg = "";
                        if (ret)
                        {
                            Cursor.Current = Cursors.Default;
                            msg = "Rollback บิลขายเรียบร้อยแล้ว!!";
                            msg.ShowInfoMessage();

                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            msg = "Rollback บิลขายไม่สำเร็จ!!";
                            msg.ShowWarningMessage();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }

        }
    }
}
