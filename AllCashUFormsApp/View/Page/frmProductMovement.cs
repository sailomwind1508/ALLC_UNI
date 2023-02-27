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
    public partial class frmProductMovement : Form
    {
        MenuBU menuBU = new MenuBU();
        ProductMovement bu = new ProductMovement();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();

        //int runDigit = 0;
        List<Control> searchBranchControls = new List<Control>();
        List<Control> searchFromBWHControls = new List<Control>();
        List<Control> searchToBWHControls = new List<Control>();
        List<Control> searchFromPrdControls = new List<Control>();
        List<Control> searchToPrdControls = new List<Control>();
        List<string> readOnlyControls = new List<string>();

        Func<tbl_BranchWarehouse, bool> fbiPredicate = null;
        private string[] productArr = null;
        private static List<string> selectProduct = new List<string>();
        static DataTable tmpDTData = new DataTable();
        string excelName = "";
        bool isPopup = false;
        public static string listProds = "";

        public frmProductMovement()
        {
            InitializeComponent();

            searchBranchControls = new List<Control>() { txtBranchCode, txtBranchName };
            searchFromBWHControls = new List<Control> { txtFromWHCode };
            searchToBWHControls = new List<Control> { txtToWHCode };

            readOnlyControls = new string[] { txtBranchCode.Name, txtProID.Name }.ToList();

            ccbProductCode.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ccb_ItemCheck);
        }

        public void BindProductMovement(string fromWHID, string toWHID, string productID, bool flagPopup)
        {
            isPopup = flagPopup;

            //if (ccbProductCode.Items.Count == 0)
            //{
            //    BindCcb();
            //}

            var allProduct = bu.tbl_Product.Where(x => x.ProductID == productID).ToList();
            productArr = allProduct.Select(x => x.ProductCode + ":" + x.ProductName).ToArray();

            //ccbProductCode.Text = productArr[0];

            selectProduct = new List<string>();
            selectProduct.Add(productID);

            txtFromWHCode.Text = fromWHID;
            txtToWHCode.Text = toWHID;

            var cDate = DateTime.Now.AddDays(1);
            dtpFromDate.Value = cDate.AddMonths(-1);
            dtpToDate.Value = cDate;

            btnSearch.PerformClick();
        }

        #region private methods

        private void InitPage()
        {
            var menu = bu.tbl_AdmFormList.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");

                //this.ClearControl(docTypeCode, runDigit);
            }

            btnAdd.Enabled = true;

            grdList.DataSource = null;

            this.OpenControl(true, readOnlyControls.ToArray(), null);

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");

            var headerPic = bu.tbl_MstMenu.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            dtpFromDate.Value = DateTime.Now;
            dtpToDate.Value = DateTime.Now;
            dtpFromDate.SetDateTimePickerFormat();
            dtpToDate.SetDateTimePickerFormat();

            grdList.SetDataGridViewStyle();

            InitialData();

            SetDefaultGridViewEvent(grdList);
        }

        private void InitialData()
        {
            var b = bu.GetBranch();
            if (b != null)
            {
                this.BindData("FromBranchID", searchBranchControls, b[0].BranchID);
                //this.BindData("FromBranchID", searchBranchControls, "503");
            }
            this.BindData("BranchWarehouse", searchFromBWHControls, "1000");
            this.BindData("BranchWarehouse", searchToBWHControls, "1000");

            var allProductGroup = new List<tbl_ProductGroup>();
            allProductGroup.Add(new tbl_ProductGroup { ProductGroupID = -1, ProductGroupName = "==เลือก==" });
            allProductGroup.AddRange(bu.GetProductGroup());
            ddlProductGroup.BindDropdownList(allProductGroup, "ProductGroupName", "ProductGroupID", 0);

            var allProductSubGroup = new List<tbl_ProductSubGroup>();
            allProductSubGroup.Add(new tbl_ProductSubGroup { ProductSubGroupID = -1, ProductSubGroupName = "==เลือก==" });
            ddlProductSubGroup.BindDropdownList(allProductSubGroup, "ProductSubGroupName", "ProductSubGroupID", 0);

            BindCcb();

            ddlProductGroup.Enabled = true;
            ddlProductSubGroup.Enabled = true;

            rdoSummary.Checked = true;

            btnSearchBranchCode.Enabled = false;

            btnPrint.Enabled = true;
            btnClose.Enabled = btnPrint.Enabled;
            btnAdd.Enabled = !btnPrint.Enabled;
            btnEdit.Enabled = !btnPrint.Enabled;
            btnRemove.Enabled = !btnPrint.Enabled;
            btnCopy.Enabled = !btnPrint.Enabled;
            btnSave.Enabled = !btnPrint.Enabled;
            btnCancel.Enabled = !btnPrint.Enabled;
            btnExcel.Enabled = !btnPrint.Enabled;
        }

        private void BindCcb()
        {
            var allProduct = bu.GetProduct().OrderBy(x => x.ProductID).ToList(); //.OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ThenBy(x => x.Seq).ToList();
            productArr = allProduct.Select(x => x.ProductCode + ":" + x.ProductName).ToArray();

            for (int i = 0; i < productArr.Length; i++)
            {
                CCBoxItem item = new CCBoxItem(productArr[i], i.ToString());
                ccbProductCode.Items.Add(item);
            }
            // If more then 5 items, add a scroll bar to the dropdown.
            ccbProductCode.MaxDropDownItems = 20;
            // Make the "Name" property the one to display, rather than the ToString() representation.
            ccbProductCode.DisplayMember = "Name";
            ccbProductCode.ValueSeparator = ", ";
            // Check the first 2 items.
            //ccbProductCode.SetItemChecked(0, true);
            //ccbProductCode.SetItemChecked(1, true);
        }

        private void SearchSummary()
        {
            var allBWH = bu.GetAllBranchWarehouse();
            var fbwh = allBWH.FirstOrDefault(x => x.WHCode == txtFromWHCode.Text);
            if (fbwh != null)
            {
                string fwhid = chkAllMM.Checked ? "-1" : fbwh.WHID;
                var tbwh = allBWH.FirstOrDefault(x => x.WHCode == txtToWHCode.Text);

                if (tbwh != null)
                {
                    string twhid = tbwh.WHID;

                    string prdGroupID = ddlProductGroup.SelectedValue.ToString();
                    string prdSupGroupID = ddlProductSubGroup.SelectedValue.ToString();

                    DateTime fDate = dtpFromDate.Value;
                    DateTime tDate = dtpToDate.Value;

                    DataTable dt = new DataTable();

                    dt = bu.GetDataTable(fwhid, twhid, prdGroupID, prdSupGroupID, selectProduct, fDate, tDate);

                    if (dt != null)
                    {
                        grdList.DataSource = dt;
                        tmpDTData = dt;
                        excelName = "รายงานสินค้าเคลื่อนไหว (สรุป)";

                        DataGridViewColumn col0 = grdList.Columns[0];
                        DataGridViewColumn col1 = grdList.Columns[1];
                        DataGridViewColumn col2 = grdList.Columns[2];
                        DataGridViewColumn col3 = grdList.Columns[3];
                        DataGridViewColumn col4 = grdList.Columns[4];
                        DataGridViewColumn col5 = grdList.Columns[5];
                        DataGridViewColumn col6 = grdList.Columns[6];
                        DataGridViewColumn col7 = grdList.Columns[7];
                        DataGridViewColumn col8 = grdList.Columns[8];

                        col0.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                        col1.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.Fill, DataGridViewContentAlignment.MiddleLeft, "", 120);
                        col2.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                        col3.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                        col4.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                        col5.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                        col6.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                        col7.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                        col8.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                    }
                }
            }
        }

        private void SearchDetails()
        {
            var fbwh = bu.GetBranchWarehouse(x => x.WHCode == txtFromWHCode.Text);
            if (fbwh != null)
            {
                string fwhid = chkAllMM.Checked ? "-1" : fbwh.WHID;
                var tbwh = bu.GetBranchWarehouse(x => x.WHCode == txtToWHCode.Text);

                if (tbwh != null)
                {
                    string twhid = tbwh.WHID;
                    {
                        string prdGroupID = ddlProductGroup.SelectedValue.ToString();
                        string prdSupGroupID = ddlProductSubGroup.SelectedValue.ToString();

                        DateTime fDate = dtpFromDate.Value;
                        DateTime tDate = dtpToDate.Value;

                        DataTable dt = new DataTable();

                        MemoryManagement.FlushMemory();

                        dt = bu.GetDataTable_Details(fwhid, twhid, prdGroupID, prdSupGroupID, selectProduct, fDate, tDate);

                        MemoryManagement.FlushMemory();

                        if (dt != null)
                        {
                            grdList.DataSource = dt;
                            tmpDTData = dt;
                            excelName = "รายงานสินค้าเคลื่อนไหว (รายละเอียด)";

                            DataGridViewColumn col0 = grdList.Columns[0];
                            DataGridViewColumn col1 = grdList.Columns[1];
                            DataGridViewColumn col2 = grdList.Columns[2];
                            DataGridViewColumn col3 = grdList.Columns[3];
                            DataGridViewColumn col4 = grdList.Columns[4];
                            DataGridViewColumn col5 = grdList.Columns[5];
                            DataGridViewColumn col6 = grdList.Columns[6];
                            DataGridViewColumn col7 = grdList.Columns[7];
                            DataGridViewColumn col8 = grdList.Columns[8];
                            DataGridViewColumn col9 = grdList.Columns[9];

                            col0.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                            col1.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.Fill, DataGridViewContentAlignment.MiddleLeft, "", 120);
                            col2.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0);
                            col3.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                            col4.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0);
                            col5.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                            col6.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                            col7.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                            col8.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                            col9.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                        }
                    }
                }
            }
        }

        public void SetDefaultGridViewEvent(DataGridView grd)
        {
            grd.CellFormatting -= new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdList_CellFormatting);
            grd.CellPainting -= new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdList_CellPainting);

            grd.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdList_CellFormatting);
            grd.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdList_CellPainting);

            grd.RowsDefaultCellStyle.BackColor = Color.White;
            grd.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
        }

        #endregion

        #region event methods

        private void ccb_DropDownClosed(object sender, EventArgs e)
        {
            GetProductList();
        }

        private void GetProductList()
        {
            selectProduct = new List<string>();

            //txtOut.AppendText("DropdownClosed\r\n");
            //txtOut.AppendText(string.Format("value changed: {0}\r\n", ccbProductCode.ValueChanged));
            //txtOut.AppendText(string.Format("value: {0}\r\n", ccbProductCode.Text));
            // Display all checked items.
            StringBuilder sb = new StringBuilder();
            if (ccbProductCode.CheckedItems.Count > 0)
            {
                foreach (CCBoxItem item in ccbProductCode.CheckedItems)
                {
                    sb.Append(item.Name).Append(ccbProductCode.ValueSeparator);
                }
                sb.Remove(sb.Length - ccbProductCode.ValueSeparator.Length, ccbProductCode.ValueSeparator.Length);

                var selPrd = sb.ToString();
                var selPrdList = selPrd.Split(',');
                foreach (var item in selPrdList)
                {
                    var _prdCode = item.Split(':')[0].ToString().Replace(" ", string.Empty);
                    selectProduct.Add(_prdCode);
                }
            }
            //txtOut.AppendText(sb.ToString());
            //txtOut.AppendText("\r\n");
        }

        private void ccb_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CCBoxItem item = ccbProductCode.Items[e.Index] as CCBoxItem;
            //txtOut.AppendText(string.Format("Item '{0}' is about to be {1}\r\n", item.Name, e.NewValue.ToString()));
        }

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
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("BranchWarehouse", searchFromBWHControls, txt.Text);
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

        private void frmProductMovement_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //frmWait wait = new frmWait();
            //wait.Show();

            //FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

            //string dir = @"C:\AllCashExcels";
            //if (!Directory.Exists(dir))
            //{
            //    Directory.CreateDirectory(dir);
            //}

            ////string _excelName = dir + @"\" + excelName + ".xlsx";
            //string cDate = DateTime.Now.ToString("yyMMddhhmmss");
            //string _excelName = dir + @"\" + string.Join("", excelName, '_', cDate) + ".xls";

            //My_DataTable_Extensions.ExportToExcelR2(new List<DataTable>() { tmpDTData }, _excelName, "ตรวจสอบสินค้าเคลื่อนไหว");

            //wait.Hide();
            //wait.Dispose();
            //wait.Close();
            if (grdList.RowCount > 0 && rdoDetails.Checked)
            {
                DataTable dtDetails = new DataTable("Details");
                SetProductMovement_Details(dtDetails);

                this.OpenExcelReportsPopup("รายงานสินค้าเคลื่อนไหว(รายละเอียด)", "ProductMovement_Details.xslt", dtDetails, true);
            }
            else if (grdList.RowCount > 0 && rdoSummary.Checked)
            {
                DataTable dtSummary = new DataTable("Summary");
                SetProductMovement_Summary(dtSummary);

                this.OpenExcelReportsPopup("รายงานสินค้าเคลื่อนไหว(สรุป)", "ProductMovement_Summary.xslt", dtSummary, true);
            }
        }

        private void SetProductMovement_Details(DataTable _dt)
        {
            var branch = bu.GetBranch();
            var branchWH = bu.GetBranchWarehouse(x => x.WHCode == txtFromWHCode.Text);

            _dt.Columns.Add("ProductID", typeof(string));
            _dt.Columns.Add("ProductName", typeof(string));
            _dt.Columns.Add("TrnDate", typeof(string));
            _dt.Columns.Add("RefDocNo", typeof(string));
            _dt.Columns.Add("TrnType", typeof(string));
            _dt.Columns.Add("ToWHID", typeof(string));
            _dt.Columns.Add("ForwardQty", typeof(string));
            _dt.Columns.Add("InQty", typeof(string));
            _dt.Columns.Add("OutQty", typeof(string));
            _dt.Columns.Add("DTBalance", typeof(string));
            _dt.Columns.Add("DateFr", typeof(string));
            _dt.Columns.Add("DateTo", typeof(string));
            _dt.Columns.Add("BranchName", typeof(string));

            _dt.Columns.Add("FromWHID", typeof(string));
            _dt.Columns.Add("WHName", typeof(string));

            foreach (DataRow r in tmpDTData.Rows)
            {
                var tmpDate = r["เวลา"].ToString().Split('/');
                var tmpDate2 = string.Join("-", tmpDate[2], tmpDate[1], tmpDate[0]);  //last edit by sailom .k 27-09/2022 // string.Join("/", tmpDate[1], tmpDate[0], tmpDate[2]);
                DateTime _dateTime = Convert.ToDateTime(tmpDate2);
                _dt.Rows.Add(r["รหัสสินค้า"], r["ชื่อสินค้า"]
                    , _dateTime.ToString("dd/MM/yyyy", new CultureInfo("en-US"))
                    , r["เลขที่เอกสาร"]
                    , r["ประเภท"], r["จาก/ไป(คลัง)"], r["ยกมา"], r["เข้า"], r["ออก"], r["คงเหลือ"]
                    , dtpFromDate.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US"))
                    , dtpToDate.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US"))
                    , branch == null ? "" : branch[0].BranchName
                    , branchWH == null ? "" : branchWH.WHID
                    , branchWH == null ? "" : branchWH.WHName);
            }
        }

        private void SetProductMovement_Summary(DataTable _dt)
        {
            var branch = bu.GetBranch();
            var branchWH = bu.GetBranchWarehouse(x => x.WHCode == txtFromWHCode.Text);

            _dt.Columns.Add("ProductCode", typeof(string));
            _dt.Columns.Add("ProductName", typeof(string));
            _dt.Columns.Add("BaseQty", typeof(string));
            _dt.Columns.Add("ImpLargeQty", typeof(string));
            _dt.Columns.Add("ImpSmallQty", typeof(string));
            _dt.Columns.Add("InQty", typeof(string));
            _dt.Columns.Add("OutQty", typeof(string));
            _dt.Columns.Add("QtyOnHandLarge", typeof(string));
            _dt.Columns.Add("QtyOnHandSmall", typeof(string));

            _dt.Columns.Add("DateFr", typeof(string));
            _dt.Columns.Add("DateTo", typeof(string));
            _dt.Columns.Add("BranchName", typeof(string));
            _dt.Columns.Add("FromWHID", typeof(string));
            _dt.Columns.Add("WHName", typeof(string));

            foreach (DataRow r in tmpDTData.Rows)
            {
                _dt.Rows.Add(r[0], r[1], r[2], r[3], r[4], r[5], r[6], r[7], r[8]
                , dtpFromDate.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US"))
                , dtpToDate.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US"))
                , branch == null ? null : branch[0].BranchName
                , branchWH == null ? "" : branchWH.WHID
                , branchWH == null ? "" : branchWH.WHName);
            }
            //รหัสสินค้า,ชื่อสินค้า,หน่วยคูณ,ยกมา\n(ใหญ่),ยกมา\n(เล็ก),เข้า\n(เล็ก),ออก\n(เล็ก),คงเหลือ\n(ใหญ่),คงเหลือ\n(เล็ก)
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

        private void btnSearchFromWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchFromBWHControls, "เลือกคลังสินค้า", fbiPredicate);
        }

        private void btnSeatchToWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchToBWHControls, "เลือกคลังสินค้า", fbiPredicate);
        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //if (!isPopup)
            //    GetProductList();

            if (!string.IsNullOrEmpty(txtProID.Text))
            {
                selectProduct = new List<string>();
                var selPrdList = txtProID.Text.Split(',');
                foreach (var item in selPrdList)
                {
                    selectProduct.Add(item);
                }
            }

            tmpDTData = new DataTable();
            if (rdoSummary.Checked)
                SearchSummary();
            else
                SearchDetails();

            MemoryManagement.FlushMemory();

            Cursor.Current = Cursors.Default;
        }

        private void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProductSubGroup.Items.Count > 0)
            {
                ddlProductSubGroup.DataSource = null;

                var allProductSubGroup = new List<tbl_ProductSubGroup>();
                allProductSubGroup.Add(new tbl_ProductSubGroup { ProductSubGroupID = -1, ProductSubGroupName = "==เลือก==" });

                var prdSubGroupList = bu.GetProductSubGroup().Where(x => x.ProductGroupID.ToString() == ddlProductGroup.SelectedValue.ToString());
                allProductSubGroup.AddRange(prdSubGroupList);
                ddlProductSubGroup.BindDropdownList(allProductSubGroup, "ProductSubGroupName", "ProductSubGroupID", 0);
            }
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            InitPage();

            txtProID.Text = string.Empty;

            //ccbProductCode.Items.Clear();
            //ccbProductCode.Text = string.Empty;
            //BindCcb();
        }

        private void grdList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (rdoDetails.Checked)
            {
                grdList.SetCellPainting(sender, e, 0);

                grdList.SetCellPainting(sender, e, 1);
            }
        }

        private void grdList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (rdoDetails.Checked)
            {
                grdList.SetCellFormatting(sender, e, 0);

                grdList.SetCellFormatting(sender, e, 1);
            }
        }

        private void rdoDetails_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoDetails.Checked)
            //{
            //    string msg = "รูปแบบรายงาน 'แบบรายละเอียด' จะดูได้ทีละคลังเท่านั้น!!!";
            //    msg.ShowInfoMessage();
            //    return;
            //}
        }

        private void frmProductMovement_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void chkAllMM_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllMM.Checked)
            //{
            //    string msg = "การเลือกดึงข้อมูลทุกคลัง อาจใช้เวลานานขึ้น!!!";
            //    msg.ShowInfoMessage();
            //    return;
            //}
        }

        private void ccbProductCode_Click(object sender, EventArgs e)
        {

            
        }

        public void BindListProduct(string txt)
        {
            listProds = txt;
            if (!string.IsNullOrEmpty(txt))
                txtProID.Text = listProds;
        }

        private void txtProID_Click(object sender, EventArgs e)
        {
            frmSearchProduct _frm = new frmSearchProduct();
            frmSearchProduct.isMovement = true;
            frmSearchProduct.isReportPage = false; //edit by sailom.k 06/02/2023
            _frm.ShowDialog();
        }
    }
}
