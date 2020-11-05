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

        int runDigit = 0;
        List<Control> searchBranchControls = new List<Control>();
        List<Control> searchFromBWHControls = new List<Control>();
        List<Control> searchToBWHControls = new List<Control>();
        List<Control> searchFromPrdControls = new List<Control>();
        List<Control> searchToPrdControls = new List<Control>();
        List<string> readOnlyControls = new List<string>();

        Func<tbl_BranchWarehouse, bool> fbiPredicate = null;
        private string[] productArr = null;
        private List<string> selectProdect = new List<string>();

        public frmProductMovement()
        {
            InitializeComponent();

            searchBranchControls = new List<Control>() { txtBranchCode, txtBranchName };
            searchFromBWHControls = new List<Control> { txtFromWHCode };
            searchToBWHControls = new List<Control> { txtToWHCode };

            readOnlyControls = new string[] { txtBranchCode.Name }.ToList();

            ccbProductCode.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ccb_ItemCheck);
        }

        #region private methods

        private void InitPage()
        {
            var menu = bu.GetAllFromMenu().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
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

            var headerPic = menuBU.GetAllData().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
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
            this.BindData("FromBranchID", searchBranchControls, "503");
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
            var allProduct = bu.GetProduct().OrderBy(x => x.ProductGroupID).ThenBy(x => x.ProductSubGroupID).ToList();
            productArr = allProduct.Select(x => x.ProductCode + ":" + x.ProductName).ToArray();

            for (int i = 0; i < productArr.Length; i++)
            {
                CCBoxItem item = new CCBoxItem(productArr[i], i.ToString());
                ccbProductCode.Items.Add(item);
            }
            // If more then 5 items, add a scroll bar to the dropdown.
            //ccbProductCode.MaxDropDownItems = 5;
            // Make the "Name" property the one to display, rather than the ToString() representation.
            ccbProductCode.DisplayMember = "Name";
            ccbProductCode.ValueSeparator = ", ";
            // Check the first 2 items.
            //ccbProductCode.SetItemChecked(0, true);
            //ccbProductCode.SetItemChecked(1, true);
        }

        private void SearchSummary()
        {
            Func<tbl_BranchWarehouse, bool> func = (x => x.WHCode == txtFromWHCode.Text);
            var bwh = bu.GetBranchWarehouse(func);
            if (bwh != null)
            {
                string whid = bwh.WHID;
                string prdGroupID = ddlProductGroup.SelectedValue.ToString();
                string prdSupGroupID = ddlProductSubGroup.SelectedValue.ToString();

                DateTime fDate = dtpFromDate.Value;
                DateTime tDate = dtpToDate.Value;

                DataTable dt = new DataTable();

                dt = bu.GetDataTable(whid, prdGroupID, prdSupGroupID, selectProdect, fDate, tDate);

                if (dt != null)
                {
                    grdList.DataSource = dt;

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

        private void SearchDetails()
        {
            Func<tbl_BranchWarehouse, bool> func = (x => x.WHCode == txtFromWHCode.Text);
            var bwh = bu.GetBranchWarehouse(func);
            if (bwh != null)
            {
                string whid = bwh.WHID;
                string prdGroupID = ddlProductGroup.SelectedValue.ToString();
                string prdSupGroupID = ddlProductSubGroup.SelectedValue.ToString();

                DateTime fDate = dtpFromDate.Value;
                DateTime tDate = dtpToDate.Value;

                DataTable dt = new DataTable();

                dt = bu.GetDataTable_Details(whid, prdGroupID, prdSupGroupID, selectProdect, fDate, tDate);

                if (dt != null)
                {
                    grdList.DataSource = dt;

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
                    col2.SetColumnStyle(80, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0);
                    col3.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                    col4.SetColumnStyle(60, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0);
                    col5.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                    col6.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                    col7.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                    col8.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                    col9.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleRight, "N0", 0);
                }
            }
        }

        public void SetDefaultGridViewEvent(DataGridView grd)
        {
            grd.CellFormatting -= new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdList_CellFormatting);
            grd.CellPainting -= new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdList_CellPainting);

            grd.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdList_CellFormatting);
            grd.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdList_CellPainting);
        }

        #endregion

        #region event methods

        private void ccb_DropDownClosed(object sender, EventArgs e)
        {
            selectProdect = new List<string>();

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
                    selectProdect.Add(_prdCode);
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

            if (rdoSummary.Checked)
                SearchSummary();
            else
                SearchDetails();

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

            ccbProductCode.Items.Clear();
            ccbProductCode.Text = string.Empty;
            BindCcb();
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
    }
}
