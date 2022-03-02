using AllCashUFormsApp.Controller;
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

namespace AllCashUFormsApp.View.Page
{
    public partial class frmProductTable : Form
    {
        MasterDataControl bu = new MasterDataControl();
        MenuBU menuBU = new MenuBU();
        Dictionary<Control, Label> Validate_PriceGroup = new Dictionary<Control, Label>();
        static DataTable tmpDTData = new DataTable();

        public frmProductTable()
        {
            InitializeComponent();
            Validate_PriceGroup.Add(txtPriceGroupCode, lbl_Code);
        }
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
        }
        private void BindBranchData()
        {
            DataTable dtBranch = new DataTable();

            dtBranch = bu.Get_proc_SendProductInfo_GetDataTable();

            DataTable newTable = new DataTable();

            newTable.Columns.Add("ChkBranch", typeof(bool));
            newTable.Columns.Add("BranchID", typeof(string));
            newTable.Columns.Add("BranchRefCode", typeof(string));
            newTable.Columns.Add("BranchName", typeof(string));
            newTable.Columns.Add("Pic", typeof(Bitmap));
            newTable.Columns.Add("OnlineStatus", typeof(bool));

            Bitmap wifi_Img = new Bitmap(Properties.Resources.addBtn); // new Resource Image
            Bitmap power_off_lmg = new Bitmap(Properties.Resources.closeBtn);

            foreach (DataRow r in dtBranch.Rows)
            {
                Bitmap colPic = Convert.ToBoolean(r["OnlineStatus"]) == true ? wifi_Img : power_off_lmg;
                newTable.Rows.Add(false
                    , r["BranchID"].ToString()
                    , r["BranchRefCode"].ToString()
                    , r["BranchName"].ToString()
                    , colPic
                    , r["OnlineStatus"]);
            }

            grdBranch.DataSource = newTable;

            //if (grdBranch.Rows.Count > 0)
            //{
            //    grdBranch.CreateCheckBoxHeaderColumn_Copy("colChkBranch","colOnlineStatus");
            //}

            for (int i = 0; i < grdBranch.Rows.Count; i++)
            {
                bool OnlineStatus = Convert.ToBoolean(grdBranch.Rows[i].Cells["colOnlineStatus"].Value);
                // OnlineStatus = true คือ แก้ไขได้
                if (OnlineStatus == false)
                {
                    grdBranch.Rows[i].Cells["colChkBranch"].ReadOnly = true;
                }
            }

            lblgrdQty.Text = newTable.Rows.Count.ToNumberFormat();
        }
        private void StatusVisible(bool visible)
        {
            lbl_Status.Visible = visible;
            btnN.Visible = visible;
        }
        private void PrePareDataToCombobox(List<ComboBox> cbbList)
        {
            var PriceGroup = bu.GetPriceGroup(x => x.FlagDel == false).OrderBy(x => x.PriceGroupID).ToList();
            var PriceGroupList = new List<tbl_PriceGroup>();
            PriceGroupList.Add(new tbl_PriceGroup { PriceGroupID = -1, PriceGroupName = "==เลือก==" });
            PriceGroupList.AddRange(PriceGroup);
            cbbList[0].BindDropdownList(PriceGroupList, "PriceGroupName", "PriceGroupID", 1);

            var PrdGroup = bu.GetProductGroup(); // NEW 
            var PrdGroupList = new List<tbl_ProductGroup>();
            PrdGroupList.Add(new tbl_ProductGroup { ProductGroupID = -1, ProductGroupName = "==เลือก==" });
            PrdGroupList.AddRange(PrdGroup);
            cbbList[1].BindDropdownList(PrdGroupList, "ProductGroupName", "ProductGroupID");

            var PrdSubGroup = new List<tbl_ProductSubGroup>();
            PrdSubGroup.Add(new tbl_ProductSubGroup { ProductSubGroupID = -1, ProductSubGroupName = "==เลือก==" });
            cbbList[2].BindDropdownList(PrdSubGroup, "ProductSubGroupName", "ProductSubGroupID");
        }
        private void InitialData()
        {
            dtpStartDate.SetDateTimePickerFormat();
            dtpEndDate.SetDateTimePickerFormat();

            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;

            grdBranch.AutoGenerateColumns = false;
            grdPriceGroup.AutoGenerateColumns = false;
            grdCommission.AutoGenerateColumns = false;

            grdBranch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdPriceGroup.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            StatusVisible(false);

            btnRefresh_Branch.PerformClick();

            //grdBranch.CreateCheckBoxHeaderColumn("colChkBranch");

            btnSearch.PerformClick();
            OpenPanelEdit(false);

            grdPrdPriceGroup.AutoGenerateColumns = false;

            if (grdBranch.Rows.Count > 0)
            {
                List<ComboBox> cbbList = new List<ComboBox>();
                cbbList.Add(ddlPriceGroup);
                cbbList.Add(ddlProductGroup);
                cbbList.Add(ddlProductSubGroup);
                PrePareDataToCombobox(cbbList);

                cbbList = new List<ComboBox>();
                cbbList.Add(cbbPriceGroup);
                cbbList.Add(cbbProductGroup);
                cbbList.Add(cbbProductSubGroup);
                PrePareDataToCombobox(cbbList);

                BindPriceGroupData();
                BindPrdPriceGroupData();
            }

            string msg = "สามารถใช้ได้เมื่อต่อ CENTER DB เท่านั้น !!";
            msg.ShowWarningMessage();
        }
        private void frmProductTable_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }
        private void BindPriceGroupData()
        {
            if (grdBranch.RowCount > 0)
            {
                int flagDel = rdoN.Checked ? 0 : 1;

                DataTable dtPriceGroup = bu.GetPriceGroupData(txtSearch.Text, flagDel);

                grdPriceGroup.DataSource = dtPriceGroup;
                lbl_qty_groupPrice.Text = dtPriceGroup.Rows.Count.ToNumberFormat();

                if (dtPriceGroup.Rows.Count > 0)
                {
                    grdPriceGroup.CreateCheckBoxHeaderColumn("colChkPriceGroup");
                }

                SetButtonAfterBindGrid(grdPriceGroup.Rows.Count);

                StatusVisible(false);

                if (rdoC.Checked == true && grdPriceGroup.Rows.Count > 0)
                {
                    StatusVisible(true);
                }
                if (rdoC.Checked == true)
                {
                    btnAdd.Enabled = false;
                }
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindPriceGroupData();
        }
        private void SelectPriceGroupDetails(DataGridViewCellEventArgs e)
        {
            DataGridViewRow grdRows = null;

            if (e != null)
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                else
                {
                    grdRows = grdPriceGroup.Rows[e.RowIndex];
                }
            }
            else
            {
                grdRows = grdPriceGroup.CurrentRow;
            }

            if (grdRows != null)
            {
                txtPriceGroupCode.Text = grdRows.Cells["colPriceGroupCode"].Value.ToString();
                txtPriceGroupName.Text = grdRows.Cells["colPriceGroupName"].Value.ToString();

                if (!string.IsNullOrEmpty(grdRows.Cells["colStartDate"].Value.ToString()))
                {
                    dtpStartDate.Value = Convert.ToDateTime(grdRows.Cells["colStartDate"].Value);
                }

                if (!string.IsNullOrEmpty(grdRows.Cells["colEndDate"].Value.ToString()))
                {
                    dtpEndDate.Value = Convert.ToDateTime(grdRows.Cells["colEndDate"].Value);
                }

            }
        }
        private void grdPriceGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectPriceGroupDetails(e);
        }
        private void grdBranch_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdBranch.SetRowPostPaint(sender, e, this.Font);
        }
        private void grdPriceGroup_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPriceGroup.SetRowPostPaint(sender, e, this.Font);
        }
        private void grdPriceGroup_SelectionChanged(object sender, EventArgs e)
        {
            SelectPriceGroupDetails(null);
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindPriceGroupData();
            }
        }
        private void rdoN_CheckedChanged(object sender, EventArgs e)
        {
            BindPriceGroupData();
        }
        private void rdoC_CheckedChanged(object sender, EventArgs e)
        {
            BindPriceGroupData();
        }
        private void tab2_ReadOnlyColumnsGridView(bool Readonly)
        {
            grdPrdPriceGroup.Columns["colSellPrice_t2"].ReadOnly = Readonly;
            grdPrdPriceGroup.Columns["colSellPriceVat_t2"].ReadOnly = Readonly;
            grdPrdPriceGroup.Columns["colBuyPrice_t2"].ReadOnly = Readonly;
        }
        private void SetColorDataGridView(DataGridView grd, bool flagEnable = true)
        {
            if (flagEnable == true)
            {
                grd.Columns["colSellPrice_t2"].DefaultCellStyle.BackColor = Color.White;
                grd.Columns["colSellPriceVat_t2"].DefaultCellStyle.BackColor = Color.White;
                grd.Columns["colBuyPrice_t2"].DefaultCellStyle.BackColor = Color.White;
            }
            else
            {
                grd.Columns["colSellPrice_t2"].DefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
                grd.Columns["colSellPriceVat_t2"].DefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
                grd.Columns["colBuyPrice_t2"].DefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string tabName = tabControl1.SelectedTab.Text.ToString();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            if (tabName == "กลุ่มราคา")
            {
                OpenPanelSearch(false);
                OpenPanelEdit();
                txtPriceGroupCode.DisableTextBox(true);
            }
            else if (tabName == "ตารางราคา")
            {
                tab2_ReadOnlyColumnsGridView(false);

                SetColorDataGridView(grdPrdPriceGroup);

                List<ComboBox> cbbList = new List<ComboBox>();
                cbbList.Add(ddlPriceGroup);
                cbbList.Add(ddlProductGroup);
                cbbList.Add(ddlProductSubGroup);
                SetPanelSearch(false, cbbList, txtSearchProGroup, btnSearchPriceGroup);
            }
            else if (tabName == "ตารางค่าคอมมิชชั่น")
            {
                grdCommission.Columns["colComPrice_t3"].DefaultCellStyle.BackColor = Color.White;

                grdCommission.Columns["colComPrice_t3"].ReadOnly = false;

                List<ComboBox> cbbList = new List<ComboBox>();
                cbbList.Add(cbbPriceGroup);
                cbbList.Add(cbbProductGroup);
                cbbList.Add(cbbProductSubGroup);
                SetPanelSearch(false, cbbList, txtSearchCommission, btnSearchCommission);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string tabName = tabControl1.SelectedTab.Text.ToString();

            if (tabName == "กลุ่มราคา")
            {
                txtPriceGroupCode.Text = "";
                txtPriceGroupName.Text = "";

                OpenPanelEdit(false);
                OpenPanelSearch();

                grdPriceGroup.Enabled = true;

                btnSearch.PerformClick();
            }
            else if (tabName == "ตารางราคา")
            {
                List<ComboBox> cbbList = new List<ComboBox>();
                cbbList.Add(ddlPriceGroup);
                cbbList.Add(ddlProductGroup);
                cbbList.Add(ddlProductSubGroup);
                SetPanelSearch(true, cbbList, txtSearchProGroup, btnSearchPriceGroup);

                SetColorDataGridView(grdPrdPriceGroup, false);
                tab2_ReadOnlyColumnsGridView(true);
                btnSearchPriceGroup.PerformClick();
            }
            else if (tabName == "ตารางค่าคอมมิชชั่น")
            {
                List<ComboBox> cbbList = new List<ComboBox>();
                cbbList.Add(cbbPriceGroup);
                cbbList.Add(cbbProductGroup);
                cbbList.Add(cbbProductSubGroup);
                SetPanelSearch(true, cbbList, txtSearchCommission, btnSearchCommission);

                grdCommission.Columns["colComPrice_t3"].DefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
                grdCommission.Columns["colComPrice_t3"].ReadOnly = true;

                btnSearchCommission.PerformClick();
            }
        }
        private void OpenPanelSearch(bool Enable = true)
        {
            txtSearch.DisableTextBox(!Enable);
            rdoN.Enabled = Enable;
            rdoC.Enabled = Enable;
            btnSearch.Enabled = Enable;
        }
        private void OpenPanelEdit(bool Enable = true)
        {
            txtPriceGroupCode.DisableTextBox(!Enable);
            txtPriceGroupName.DisableTextBox(!Enable);
            dtpStartDate.Enabled = Enable;
            dtpEndDate.Enabled = Enable;
        }
        private void FormatRunningNo_PriceGroup()
        {
            var PriceGroupList = bu.GetPriceGroup();

            if (PriceGroupList != null && PriceGroupList.Count > 0)
            {
                int maxPGcode = Convert.ToInt32(PriceGroupList.Select(x => x.PriceGroupCode).Max()) + 1;

                txtPriceGroupCode.Text = maxPGcode.ToString("00");
            }
            else
            {
                txtPriceGroupCode.Text = "00";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string tabName = tabControl1.SelectedTab.Text.ToString();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            if (tabName == "กลุ่มราคา")
            {
                OpenPanelSearch(false);
                OpenPanelEdit(true);

                txtPriceGroupCode.Text = "";
                txtPriceGroupName.Text = "";

                dtpStartDate.Value = DateTime.Now;
                dtpEndDate.Value = DateTime.Now;

                FormatRunningNo_PriceGroup(); //AutoID

                txtPriceGroupCode.DisableTextBox(true);
                grdPriceGroup.Enabled = false;
            }
        }
        private void tabControl1_Click(object sender, EventArgs e)
        {
            string tabName = tabControl1.SelectedTab.Text.ToString();

            if (tabName == "กลุ่มราคา")
            {
                BindBranchData();

                OpenPanelSearch(true);
                OpenPanelEdit(false);

                btnSearch.PerformClick();

                chkBoxSelectBranch.Checked = false;

                BindPriceGroupData();
            }
            else if (tabName == "ตารางราคา")
            {
                List<ComboBox> cbbList = new List<ComboBox>();
                cbbList.Add(ddlPriceGroup);
                cbbList.Add(ddlProductGroup);
                cbbList.Add(ddlProductSubGroup);

                SetPanelSearch(true, cbbList, txtSearchProGroup, btnSearchPriceGroup);

                PrePareDataToCombobox(cbbList);

                btnSearchPriceGroup.PerformClick();
            }
            else if (tabName == "ตารางค่าคอมมิชชั่น")
            {
                List<ComboBox> cbbList = new List<ComboBox>();
                cbbList.Add(cbbPriceGroup);
                cbbList.Add(cbbProductGroup);
                cbbList.Add(cbbProductSubGroup);

                SetPanelSearch(true, cbbList, txtSearchCommission, btnSearchCommission);

                PrePareDataToCombobox(cbbList);

                btnSearchCommission.PerformClick();
            }
        }
        private void chkBoxSelectBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (grdBranch.Rows.Count > 0)
            {
                for (int i = 0; i < grdBranch.Rows.Count; i++)
                {
                    //bool ChkBranch = Convert.ToBoolean(grdBranch.Rows[i].Cells["colChkBranch"].Value);
                    bool OnlineStatus = Convert.ToBoolean(grdBranch.Rows[i].Cells["colOnlineStatus"].Value);

                    if (OnlineStatus == true && chkBoxSelectBranch.Checked == true)
                    {
                        grdBranch.Rows[i].Cells["colChkBranch"].Value = true;
                    }
                    else
                    {
                        grdBranch.Rows[i].Cells["colChkBranch"].Value = false;
                    }
                }
            }
            else
            {
                chkBoxSelectBranch.Checked = false;
            }
        }
        private bool ValidateSave(string tabName)
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret)
            {
                if (tabName == "กลุ่มราคา")
                {
                    errList.SetErrMessage(Validate_PriceGroup);
                }
            }
            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }
            return ret;
        }
        private void PrePareEdit_PriceGroup(bool flagRemove, tbl_PriceGroup tbl_PriceGroups)
        {
            tbl_PriceGroups.PriceGroupCode = txtPriceGroupCode.Text;
            tbl_PriceGroups.PriceGroupName = txtPriceGroupName.Text;

            tbl_PriceGroups.EdDate = DateTime.Now;
            tbl_PriceGroups.EdUser = Helper.tbl_Users.Username;

            tbl_PriceGroups.StartDate = dtpStartDate.Value;
            tbl_PriceGroups.EndDate = dtpEndDate.Value;

            tbl_PriceGroups.BranchID = grdBranch.Rows[0].Cells["colBranchID"].Value.ToString();

            tbl_PriceGroups.FlagSend = Convert.ToBoolean(grdPriceGroup.CurrentRow.Cells["colFlagSend"].Value);

            if (flagRemove == true)
            {
                tbl_PriceGroups.FlagDel = true;
            }
        }
        private void Remove_ProductPriceGroup(bool flagRemove = false)
        {
            try
            {
                int ret = 0;

                var ProductPriceGroup = new tbl_ProductPriceGroup();
                var ProductPriceGroupList = new List<tbl_ProductPriceGroup>();

                int PriceGroupID = Convert.ToInt32(ddlPriceGroup.SelectedValue);
                int ProductGroupID = ddlProductGroup.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlProductGroup.SelectedValue);
                int ProductSubGroupID = ddlProductSubGroup.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlProductSubGroup.SelectedValue);

                var tbl_ProductPriceGroupList = new List<tbl_ProductPriceGroup>();

                tbl_ProductPriceGroupList = bu.GetProductPriceGroup(x => x.PriceGroupID == PriceGroupID);

                if (ProductGroupID == 0 && ProductSubGroupID == 0) // ถ้าค้นหา แค่กลุ่มราคา ให้ ลบทั้งหมดที่เป็นกลุ่มราคานั้น 
                {
                    foreach (var item in tbl_ProductPriceGroupList)
                    {
                        ProductPriceGroupList.Add(item);
                    }
                }
                else
                {
                    for (int i = 0; i < grdPrdPriceGroup.Rows.Count; i++)
                    {
                        string ProID = grdPrdPriceGroup.Rows[i].Cells["colProductID_t2"].Value.ToString();
                        int UomSetID = Convert.ToInt32(grdPrdPriceGroup.Rows[i].Cells["colUomSetID_t2"].Value);

                        var item = tbl_ProductPriceGroupList.FirstOrDefault(x => x.ProductID == ProID && x.ProductUomID == UomSetID);

                        if (item != null)
                        {
                            ProductPriceGroupList.Add(item);
                        }
                    }
                }

                foreach (var item in ProductPriceGroupList)   //ลบ ข้อมูลเก่า  -x- Update ไม่ได้ -x-
                {
                    ret = bu.RemoveProductPriceGroup(item);
                }

                if (flagRemove == true)
                {
                    if (ret == 1)
                    {
                        string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();
                        btnSearchPriceGroup.PerformClick();
                    }
                    else
                    {
                        this.ShowProcessErr();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
        private void SaveProductPriceGroup()
        {
            try
            {
                int ret = 0;

                Remove_ProductPriceGroup(); //ลบข้อมูลก่อน ทำการ Insert ใหม่

                var ProductPriceGroup = new tbl_ProductPriceGroup();
                var ProductPriceGroupList = new List<tbl_ProductPriceGroup>();

                decimal Price = 0;

                for (int i = 0; i < grdPrdPriceGroup.Rows.Count; i++)
                {
                    ProductPriceGroup.PriceGroupID = Convert.ToInt32(grdPrdPriceGroup.Rows[i].Cells["colPriceGroupID_t2"].Value);
                    ProductPriceGroup.ProductID = grdPrdPriceGroup.Rows[i].Cells["colProductID_t2"].Value.ToString();
                    ProductPriceGroup.ProductUomID = Convert.ToInt32(grdPrdPriceGroup.Rows[i].Cells["colUomSetID_t2"].Value);

                    ProductPriceGroup.SellPrice = Convert.ToDecimal(grdPrdPriceGroup.Rows[i].Cells["colSellPrice_t2"].Value).ToDecimalN2();
                    ProductPriceGroup.BuyPrice = Convert.ToDecimal(grdPrdPriceGroup.Rows[i].Cells["colBuyPrice_t2"].Value).ToDecimalN2();
                    ProductPriceGroup.SellPriceVat = Convert.ToDecimal(grdPrdPriceGroup.Rows[i].Cells["colSellPriceVat_t2"].Value).ToDecimalN2();

                    ProductPriceGroup.CrDate = DateTime.Now;
                    ProductPriceGroup.CrUser = Helper.tbl_Users.Username;

                    ProductPriceGroup.EdDate = null; //
                    ProductPriceGroup.EdUser = null; //

                    ProductPriceGroup.FlagDel = false; //
                    ProductPriceGroup.FlagSend = false;

                    ProductPriceGroup.FlagNew = false;
                    ProductPriceGroup.FlagEdit = false;

                    ProductPriceGroup.BuyPriceVat = Price.ToDecimalN2(); //ไม่มีข้อมูลในตารางให้กรอก
                    ProductPriceGroup.ComPrice = Price.ToDecimalN2();//ไม่มีข้อมูลในตารางให้กรอก

                    ret = bu.UpdateProductPriceGroupData(ProductPriceGroup);
                }

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    List<ComboBox> cbbList = new List<ComboBox>();
                    cbbList.Add(ddlPriceGroup);
                    cbbList.Add(ddlProductGroup);
                    cbbList.Add(ddlProductSubGroup);
                    SetPanelSearch(true, cbbList, txtSearchProGroup, btnSearchPriceGroup);

                    btnSearchPriceGroup.PerformClick();

                    tab2_ReadOnlyColumnsGridView(true);

                    SetColorDataGridView(grdPrdPriceGroup, false);
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
        private void SavePriceGroup()
        {
            try
            {
                int ret = 0;

                var tbl_PriceGroup = new tbl_PriceGroup();
                var tbl_PriceGroupList = bu.GetPriceGroup();

                if (tbl_PriceGroupList.Count > 0)
                {
                    tbl_PriceGroup = tbl_PriceGroupList.FirstOrDefault(x => x.PriceGroupCode == txtPriceGroupCode.Text);
                }

                if (tbl_PriceGroup != null)
                {
                    PrePareEdit_PriceGroup(false, tbl_PriceGroup);
                }
                else
                {
                    tbl_PriceGroup = new tbl_PriceGroup();
                    if (tbl_PriceGroupList.Count > 0)
                    {
                        tbl_PriceGroup.PriceGroupID = tbl_PriceGroupList.Select(x => x.PriceGroupID).Max() + 1;
                    }
                    else
                    {
                        tbl_PriceGroup.PriceGroupID = 1;
                    }

                    tbl_PriceGroup.PriceGroupCode = txtPriceGroupCode.Text;
                    tbl_PriceGroup.PriceGroupName = txtPriceGroupName.Text;

                    tbl_PriceGroup.CrDate = DateTime.Now;
                    tbl_PriceGroup.CrUser = Helper.tbl_Users.Username;

                    tbl_PriceGroup.EdDate = null;
                    tbl_PriceGroup.EdUser = null;

                    tbl_PriceGroup.StartDate = dtpStartDate.Value;
                    tbl_PriceGroup.EndDate = dtpEndDate.Value;

                    tbl_PriceGroup.BranchID = grdBranch.CurrentRow.Cells["colBranchID"].Value.ToString();

                    tbl_PriceGroup.FlagDel = false;
                    tbl_PriceGroup.FlagSend = false;
                }

                ret = bu.UpdatePriceGroupData(tbl_PriceGroup);

                if (ret == 1)
                {
                    txtPriceGroupCode.Clear();
                    txtPriceGroupName.Clear();

                    dtpStartDate.Value = DateTime.Now;
                    dtpEndDate.Value = DateTime.Now;

                    OpenPanelEdit(false);
                    OpenPanelSearch(true);

                    btnSearch.PerformClick();

                    grdPriceGroup.Enabled = true;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
        private void Save_Edit_ProPriceGroup(bool flagRemove = false)
        {
            try
            {
                int ret = 0;

                var tbl_ProductPriceGroupList = bu.GetProductPriceGroup();

                var tbl_ProductPriceGroup = new tbl_ProductPriceGroup();
                var _tbl_ProductPriceGroupList = new List<tbl_ProductPriceGroup>();

                decimal price = 0;

                for (int i = 0; i < grdCommission.Rows.Count; i++)
                {
                    int PriceGroupID = Convert.ToInt32(grdCommission.Rows[i].Cells["colPriceGroupID_t3"].Value);
                    string ProductID = grdCommission.Rows[i].Cells["colProductID_t3"].Value.ToString();
                    int ProductUomID = Convert.ToInt32(grdCommission.Rows[i].Cells["colUomSetID_t3"].Value);

                    var item = tbl_ProductPriceGroupList.FirstOrDefault(x => x.PriceGroupID == PriceGroupID && x.ProductID == ProductID && x.ProductUomID == ProductUomID);

                    if (flagRemove == true)
                    {
                        item.ComPrice = price.ToDecimalN2();
                    }
                    else
                    {
                        item.ComPrice = Convert.ToDecimal(grdCommission.Rows[i].Cells["colComPrice_t3"].Value).ToDecimalN2();
                    }

                    item.EdDate = DateTime.Now;
                    item.EdUser = Helper.tbl_Users.Username;

                    _tbl_ProductPriceGroupList.Add(item);
                }

                foreach (var item in _tbl_ProductPriceGroupList)
                {
                    ret = bu.UpdateProductPriceGroupData(item);
                }

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    if (flagRemove == false)
                    {
                        List<ComboBox> cbbList = new List<ComboBox>();
                        cbbList.Add(cbbPriceGroup);
                        cbbList.Add(cbbProductGroup);
                        cbbList.Add(cbbProductSubGroup);
                        SetPanelSearch(true, cbbList, txtSearchCommission, btnSearchCommission);

                        grdCommission.Columns["colComPrice_t3"].DefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
                        grdCommission.Columns["colComPrice_t3"].ReadOnly = true;
                    }

                    btnSearchCommission.PerformClick();
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string tabName = tabControl1.SelectedTab.Text.ToString();

            if (!ValidateSave(tabName))
            {
                return;
            }

            string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการบันทึก!!";

            if (!cfMsg.ConfirmMessageBox(title))
                return;

            if (tabName == "กลุ่มราคา")
            {
                SavePriceGroup();
            }
            else if (tabName == "ตารางราคา")
            {
                SaveProductPriceGroup();
            }
            else if (tabName == "ตารางค่าคอมมิชชั่น")
            {
                Save_Edit_ProPriceGroup();
            }
        }
        private void RemovePriceGroup()
        {
            try
            {
                int ret = 0;
                var PriceGroupList = bu.GetPriceGroup(x => x.PriceGroupCode == txtPriceGroupCode.Text);
                var PriceGroupData = PriceGroupList[0];
                PrePareEdit_PriceGroup(true, PriceGroupData);

                ret = bu.UpdatePriceGroupData(PriceGroupData);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    txtPriceGroupCode.Clear();
                    txtPriceGroupName.Clear();

                    btnSearch.PerformClick();

                    grdPriceGroup.Enabled = true;
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
        private void btnRemove_Click(object sender, EventArgs e)
        {
            string tabName = tabControl1.SelectedTab.Text.ToString();

            string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
            string title = "ทำการยืนยัน!!";

            if (!cfMsg.ConfirmMessageBox(title))
                return;

            if (tabName == "กลุ่มราคา")
            {
                RemovePriceGroup();
            }
            else if (tabName == "ตารางราคา")
            {
                Remove_ProductPriceGroup(true);
            }
            else if (tabName == "ตารางค่าคอมมิชชั่น")
            {
                Save_Edit_ProPriceGroup(true);
            }
        }
        private void ChangeStatus()
        {
            try
            {
                int ret = 0;

                var PriceGroupList = bu.GetPriceGroup(x => x.PriceGroupCode == txtPriceGroupCode.Text);
                var PriceGroup = PriceGroupList[0];

                PrePareEdit_PriceGroup(false, PriceGroup);

                PriceGroup.FlagDel = false;

                ret = bu.UpdatePriceGroupData(PriceGroup);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    msg.ShowInfoMessage();

                    OpenPanelEdit(false);
                    OpenPanelSearch(true);

                    rdoN.PerformClick();
                    rdoC.PerformClick();
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
        private void btnN_Click(object sender, EventArgs e)
        {
            ChangeStatus();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            grdPriceGroup.DataSource = new DataGridView();
            grdBranch.DataSource = new DataGridView();
            grdPrdPriceGroup.DataSource = new DataGridView();
            this.Close();
        }
        private void PrePareNewDataTable(DataTable newTable)
        {
            newTable.Columns.Add("ProductGroupID", typeof(int));
            newTable.Columns.Add("ProductSubGroupID", typeof(int));
            newTable.Columns.Add("ProductID", typeof(string));
            newTable.Columns.Add("ProductName", typeof(string));
            newTable.Columns.Add("ProductGroupName", typeof(string));
            newTable.Columns.Add("UomSetID", typeof(int));
            newTable.Columns.Add("UomSetName", typeof(string));
            newTable.Columns.Add("PriceGroupID", typeof(int));
            newTable.Columns.Add("SellPrice", typeof(decimal));
            newTable.Columns.Add("SellPriceVat", typeof(decimal));
            newTable.Columns.Add("BuyPrice", typeof(decimal));
        }
        private void BindPrdPriceGroupData()
        {
            //if (grdBranch.RowCount > 0) //edit by sailom .k 17/12/2021
            {
                DataTable dtProData = new DataTable();

                int PriceGroupID = Convert.ToInt32(ddlPriceGroup.SelectedIndex) == 0 ? 0 : Convert.ToInt32(ddlPriceGroup.SelectedValue);
                int ProductGroupID = Convert.ToInt32(ddlProductGroup.SelectedIndex) == 0 ? 0 : Convert.ToInt32(ddlProductGroup.SelectedValue);
                int ProductSubGroupID = Convert.ToInt32(ddlProductSubGroup.SelectedIndex) == 0 ? 0 : Convert.ToInt32(ddlProductSubGroup.SelectedValue);

                dtProData = bu.GetPrdUomSetData(ProductGroupID, ProductSubGroupID, txtSearchProGroup.Text, true);

                DataTable dtProPriceGroup = PriceGroupID == 0 ? bu.GetProductPriceGroup(null).ToDataTable() : bu.GetProductPriceGroup(x => x.PriceGroupID == PriceGroupID).ToDataTable();

                DataTable newTable = new DataTable();

                PrePareNewDataTable(newTable);

                var tmp = dtProPriceGroup.AsEnumerable().ToList();

                decimal Price = 0;

                foreach (DataRow r in dtProData.Rows)
                {
                    string proID = r["ProductID"].ToString();
                    int UomSetID = Convert.ToInt32(r["UomSetID"]);
                    DataRow item;

                    if (dtProPriceGroup.Rows.Count > 0) // tbl_ProductPriceGroup มีข้อมูล
                    {
                        if (PriceGroupID == 0) //กลุ่มราคา = "==เลือก==" ให้หยุดทำงาน
                        {
                            break;
                        }
                        else  //เช็คว่า มีข้อมูลใน tbl_ProductPriceGroup
                        {
                            item = tmp.FirstOrDefault(x => x.Field<string>("ProductID") == proID
                                    && x.Field<int>("ProductUomID") == UomSetID && x.Field<int>("PriceGroupID") == PriceGroupID);
                        }
                        if (item != null) //มีข้อมูล
                        {
                            newTable.Rows.Add(r["ProductGroupID"], r["ProductSubGroupID"]
                                , proID, r["ProductName"], r["ProductGroupName"]
                                , r["UomSetID"], r["UomSetName"]
                                , item["PriceGroupID"], item["SellPrice"]
                                , item["SellPriceVat"], item["BuyPrice"]);
                        }
                        else //ไม่มีข้อมูล
                        {
                            newTable.Rows.Add(r["ProductGroupID"], r["ProductSubGroupID"]
                                , proID, r["ProductName"], r["ProductGroupName"]
                                , r["UomSetID"], r["UomSetName"]
                                , PriceGroupID, Price.ToDecimalN2()
                                , Price.ToDecimalN2(), Price.ToDecimalN2());
                        }
                    }
                    else // tbl_ProductPriceGroup ไม่มีข้อมูล
                    {
                        if (PriceGroupID > 0)//กลุ่มราคา ไม่เท่ากับ "==เลือก==" 
                        {
                            newTable.Rows.Add(r["ProductGroupID"], r["ProductSubGroupID"]
                                 , proID, r["ProductName"], r["ProductGroupName"]
                                 , r["UomSetID"], r["UomSetName"]
                                 , PriceGroupID, Price.ToDecimalN2()
                                 , Price.ToDecimalN2(), Price.ToDecimalN2());
                        }
                    }
                }

                grdPrdPriceGroup.DataSource = newTable;
                tmpDTData.TableName = "Sheet1";
                tmpDTData = newTable;
                label1.Text = newTable.Rows.Count.ToNumberFormat();

                SetButtonAfterBindGrid(grdPrdPriceGroup.RowCount);

                tab2_ReadOnlyColumnsGridView(true);
            }
        }
        private void SetButtonAfterBindGrid(int RowCount)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            if (RowCount > 0)
            {
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
            }
            //btnExcel.Enabled = false;
        }
        private void btnSearchPriceGroup_Click(object sender, EventArgs e)
        {
            BindPrdPriceGroupData();
            btnPrint.Enabled = true;
        }
        private void SetPanelSearch(bool flag, List<ComboBox> cbbList, TextBox txtSearch, Button btnSearch)
        {
            cbbList[0].Enabled = flag; //cbbPriceGroup
            cbbList[1].Enabled = flag;//cbbProductGroup
            cbbList[2].Enabled = flag;//cbbProductSubGroup
            txtSearch.DisableTextBox(!flag);
            btnSearch.Enabled = flag;
        }
        private void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ComboBox> cbbList = new List<ComboBox>();
            cbbList.Add(ddlProductGroup);
            cbbList.Add(ddlProductSubGroup);
            BindDataToCombobox_ProductGroup(cbbList);
        }
        private void grdPrdPriceGroup_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPrdPriceGroup.SetRowPostPaint(sender, e, this.Font);
        }
        private void grdPrdPriceGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int[] numberCell = new int[] { 8, 9, 10 };
                grdPrdPriceGroup.SetCellNumberOnly(sender, e, numberCell.ToList());
            }
            catch (Exception)
            {
            }
        }
        private void grdPrdPriceGroup_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;

                if (grdPrdPriceGroup.CurrentCell.ColumnIndex >= 8 && grdPrdPriceGroup.CurrentCell.ColumnIndex <= 10)
                {
                    tb.KeyPress -= grdPrdPriceGroup_KeyPress;
                    tb.KeyPress += grdPrdPriceGroup_KeyPress;
                }
            }
            catch (Exception)
            {
            }
        }
        private void grdPrdPriceGroup_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView_CellEndEdit(e, grdPrdPriceGroup);
        }
        private void txtSearchProGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindPrdPriceGroupData();
            }
        }
        private void ddlPriceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPrdPriceGroupData();
        }
        private void grdPrdPriceGroup_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            grdPrdPriceGroup.SetCellPainting(sender, e, 4); //Merge Rows
            grdPrdPriceGroup.SetCellPainting(sender, e, 5);
            grdPrdPriceGroup.SetCellPainting(sender, e, 6);
        }
        private void grdPrdPriceGroup_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            grdPrdPriceGroup.SetCellFormatting(sender, e, 4); //ลบข้อมูลที่ซ้ำกัน แต่ยังไม่ Merge Rows
            grdPrdPriceGroup.SetCellFormatting(sender, e, 5);
            grdPrdPriceGroup.SetCellFormatting(sender, e, 6);
        }
        private void BindCommissionData()
        {
            //if (grdBranch.RowCount > 0)  //edit by sailom .k 17/12/2021
            {
                DataTable dtPrdUomset = new DataTable();

                int PriceGroupID = Convert.ToInt32(cbbPriceGroup.SelectedIndex) == 0 ? 0 : Convert.ToInt32(cbbPriceGroup.SelectedValue);
                int ProductGroupID = Convert.ToInt32(cbbProductGroup.SelectedIndex) == 0 ? 0 : Convert.ToInt32(cbbProductGroup.SelectedValue);
                int ProductSubGroupID = Convert.ToInt32(cbbProductSubGroup.SelectedIndex) == 0 ? 0 : Convert.ToInt32(cbbProductSubGroup.SelectedValue);

                dtPrdUomset = bu.GetPrdUomSetData(ProductGroupID, ProductSubGroupID, txtSearchCommission.Text, false);

                DataTable dtProPriceGroup = bu.GetProductPriceGroup(x => x.PriceGroupID == PriceGroupID).ToDataTable();

                DataTable newTable = new DataTable();

                PrePareNewDataTable(newTable);

                newTable.Columns.Add("ComPrice", typeof(decimal));

                if (dtProPriceGroup.Rows.Count > 0)
                {
                    var tmp = dtProPriceGroup.AsEnumerable().ToList();

                    decimal Price = 0;

                    foreach (DataRow r in dtPrdUomset.Rows)
                    {
                        string proID = r["ProductID"].ToString();
                        int UomSetID = Convert.ToInt32(r["UomSetID"]);
                        DataRow item;

                        if (dtProPriceGroup.Rows.Count > 0) // tbl_ProductPriceGroup มีข้อมูล
                        {
                            if (PriceGroupID == 0) //กลุ่มราคา = "==เลือก==" ให้หยุดทำงาน
                            {
                                break;
                            }
                            else  //เช็คว่า มีข้อมูลใน tbl_ProductPriceGroup
                            {
                                item = tmp.FirstOrDefault(x => x.Field<string>("ProductID") == proID
                                        && x.Field<int>("ProductUomID") == UomSetID && x.Field<int>("PriceGroupID") == PriceGroupID
                                            && (x.Field<decimal>("SellPrice") > 0 || x.Field<decimal>("SellPriceVat") > 0 || x.Field<decimal>("BuyPrice") > 0));
                            }
                            if (item != null) //มีข้อมูล
                            {
                                newTable.Rows.Add(r["ProductGroupID"], r["ProductSubGroupID"]
                                    , proID, r["ProductName"], r["ProductGroupName"]
                                    , r["UomSetID"], r["UomSetName"]
                                    , item["PriceGroupID"], item["SellPrice"]
                                    , item["SellPriceVat"], item["BuyPrice"]
                                    , item["ComPrice"]);
                            }
                        }
                    }
                }
                grdCommission.DataSource = newTable;
                label1.Text = newTable.Rows.Count.ToNumberFormat();

                SetButtonAfterBindGrid(grdCommission.RowCount);

                if (newTable.Rows.Count > 0)
                {
                    grdCommission.Columns["colComPrice_t3"].ReadOnly = false;
                }
                else
                {
                    grdCommission.Columns["colComPrice_t3"].ReadOnly = true;
                }
            }
        }
        private void btnSearchCommission_Click(object sender, EventArgs e)
        {
            BindCommissionData();
        }
        private void grdCommission_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int[] numberCell = new int[] { 11 };

                grdCommission.SetCellNumberOnly(sender, e, numberCell.ToList());
            }
            catch (Exception)
            {

            }
        }
        private void DataGridView_CellEndEdit(DataGridViewCellEventArgs e, DataGridView grd)
        {
            string columns = grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (string.IsNullOrEmpty(columns))
            {
                decimal price = 0;
                grd.CurrentRow.Cells[e.ColumnIndex].Value = price.ToDecimalN2();
            }
            else
            {
                grd.CurrentRow.Cells[e.ColumnIndex].Value = Convert.ToDecimal(columns).ToDecimalN2();
            }
        }
        private void grdCommission_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {  //Commission ColumnIndex = 11
            DataGridView_CellEndEdit(e, grdCommission);
        }
        private void grdCommission_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            grdCommission.SetCellFormatting(sender, e, 4);//2 proID 3 proName 4 progroup
        }
        private void grdCommission_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            grdCommission.SetCellPainting(sender, e, 4);
        }
        private void grdCommission_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;

                if (grdCommission.CurrentCell.ColumnIndex == 11)
                {
                    tb.KeyPress -= grdCommission_KeyPress;
                    tb.KeyPress += grdCommission_KeyPress;
                }
            }
            catch (Exception)
            {
            }
        }
        private void grdCommission_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdCommission.SetRowPostPaint(sender, e, this.Font);
        }
        private void cbbPriceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCommissionData();
        }
        private void BindDataToCombobox_ProductGroup(List<ComboBox> cbbList)
        {
            var PrdSubGroup = new List<tbl_ProductSubGroup>();
            PrdSubGroup.Add(new tbl_ProductSubGroup { ProductSubGroupID = -1, ProductSubGroupName = "==เลือก==" });

            if (cbbList[0].SelectedIndex > 0)
            {
                int PrdGroupID = Convert.ToInt32(cbbList[0].SelectedValue);
                PrdSubGroup.AddRange(bu.GetProductSubGroup(x => x.ProductGroupID == PrdGroupID));
            }

            cbbList[1].BindDropdownList(PrdSubGroup, "ProductSubGroupName", "ProductSubGroupID");
        }
        private void cbbProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ComboBox> cbbList = new List<ComboBox>();
            cbbList.Add(cbbProductGroup);
            cbbList.Add(cbbProductSubGroup);
            BindDataToCombobox_ProductGroup(cbbList);
        }
        private void btnRefresh_Branch_Click(object sender, EventArgs e)
        {
            BindBranchData();
        }
        public bool ValidateBranchCheck()
        {
            bool ValidateBranch = false;

            if (grdBranch.Rows.Count > 0)
            {
                var dtBranch = (DataTable)grdBranch.DataSource;

                DataRow dr = dtBranch.AsEnumerable().FirstOrDefault(x => x.Field<bool>("ChkBranch") == true && x.Field<bool>("OnlineStatus") == true);

                if (dr != null)
                {
                    ValidateBranch = true;
                }
                else
                {
                    for (int i = 0; i < grdBranch.Rows.Count; i++)
                    {
                        grdBranch.Rows[i].Cells["colChkBranch"].Value = false;
                    }
                }
            }

            return ValidateBranch;
        }
        public bool ValidatePriceGroupTL1()
        {
            bool ValidatePriceGroup = false;

            if (grdPriceGroup.Rows.Count > 0)
            {
                for (int i = 0; i < grdPriceGroup.RowCount; i++)
                {
                    bool _ChkPriceGroup = Convert.ToBoolean(grdPriceGroup.Rows[i].Cells["colChkPriceGroup"].Value);
                    if (_ChkPriceGroup == false)
                    {
                        ValidatePriceGroup = true;
                        break;
                    }
                }
            }

            return ValidatePriceGroup;
        }
        private void SelectBranchList(List<string> selectList_Branch)
        {
            for (int i = 0; i < grdBranch.Rows.Count; i++)
            {
                bool colChkBranch = Convert.ToBoolean(grdBranch.Rows[i].Cells["colChkBranch"].Value);
                bool colOnlineStatus = Convert.ToBoolean(grdBranch.Rows[i].Cells["colOnlineStatus"].Value);

                if (colChkBranch == true && colOnlineStatus == true)
                {
                    selectList_Branch.Add(grdBranch.Rows[i].Cells["colBranchID"].Value.ToString());
                }
            }
        }
        private void SelectPrdPriceGroupList(List<string> PriceGroupID, List<string> ProductID)
        {
            for (int i = 0; i < grdPrdPriceGroup.Rows.Count; i++)
            {
                string _PriceGroupID_TL2 = grdPrdPriceGroup.Rows[i].Cells["colPriceGroupID_t2"].Value.ToString();
                string _ProductIDs = grdPrdPriceGroup.Rows[i].Cells["colProductID_t2"].Value.ToString();
                PriceGroupID.Add(_PriceGroupID_TL2);
                ProductID.Add(_ProductIDs);
            }
        }
        private void SelectTLPriceGroup(List<string> PriceGroupID)
        {

            for (int i = 0; i < grdPriceGroup.RowCount; i++)
            {
                bool ChkPriceGroup = Convert.ToBoolean(grdPriceGroup.Rows[i].Cells["colChkPriceGroup"].Value);

                if (ChkPriceGroup == true)
                {
                    string _PriceGroupID = grdPriceGroup.Rows[i].Cells["colPriceGroupID"].Value.ToString();
                    PriceGroupID.Add(_PriceGroupID);
                }
            }
        }
        private void btnSendData_Click(object sender, EventArgs e)
        {
            string msgCheckError = "";

            if (ValidateBranchCheck() == false)
            {
                msgCheckError += "เลือกศูนย์ที่ต้องการส่งข้อมูล !!\n";
            }

            if (ValidatePriceGroupTL1() == true)
            {
                msgCheckError += "เลือกข้อมูลตารางสินค้าที่ต้องการส่งข้อมูล !!\n";
            }

            if (!string.IsNullOrEmpty(msgCheckError))
            {
                msgCheckError.ShowWarningMessage();
                return;
            }

            string cfMsg = "ต้องการส่งข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการส่ง!!";

            if (!cfMsg.ConfirmMessageBox(title))
                return;

            bool ret = false;

            List<string> BranchID_List = new List<string>();
            SelectBranchList(BranchID_List);
            var allBranchID = string.Join(",", BranchID_List);

            List<string> PrdPriceGroupID_List = new List<string>();
            List<string> ProductID_List = new List<string>();
            SelectPrdPriceGroupList(PrdPriceGroupID_List, ProductID_List);

            var allPrdGroupID = string.Join(",", PrdPriceGroupID_List);
            var allProductID = string.Join(",", ProductID_List);

            List<string> TLPriceGroupID = new List<string>();
            SelectTLPriceGroup(TLPriceGroupID);
            var allTLPriceGroupID = string.Join(",", TLPriceGroupID);

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@BranchIDs", allBranchID);
            _params.Add("@PrdPriceGroupIDs", allPrdGroupID);//ProductPriceGroup
            _params.Add("@ProductIDs", allProductID);//14
            _params.Add("@PriceGroupIDs", allTLPriceGroupID);


            ret = bu.CallSendAllProductPriceGroupData(_params);

            if (ret == true)
            {
                string msg = "ส่งข้อมูลเรียบร้อยแล้ว!!";
                msg.ShowInfoMessage();
            }
            else
            {
                string msg = "ส่งข้อมูลล้มเหลว!!";
                msg.ShowErrorMessage();
                return;
            }

        }

        private void frmProductTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmWait wait = new frmWait();
            wait.Show();

            string dir = @"C:\AllCashExcels";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var _newTable = new DataTable("Sheet1");
            _newTable.Columns.Add("ชื่อกลุ่มสินค้า", typeof(string));
            _newTable.Columns.Add("รหัสสินค้า", typeof(string));
            _newTable.Columns.Add("ชื่อสินค้า", typeof(string));
            _newTable.Columns.Add("หน่วย", typeof(string));
            _newTable.Columns.Add("ก่อนภาษี(ราคาขาย)", typeof(decimal));
            _newTable.Columns.Add("รวมภาษี(ราคาขาย)", typeof(decimal));
            _newTable.Columns.Add("ก่อนภาษี(ราคาซื้อ)", typeof(decimal));

            foreach (DataRow r in tmpDTData.Rows)
            {
                _newTable.Rows.Add(r["ProductGroupName"], r["ProductID"], r["ProductName"]
                    , r["UomSetName"], r["SellPrice"], r["SellPriceVat"], r["BuyPrice"]);

            }

            //string _excelName = dir + @"\" + excelName + ".xlsx";
            string cDate = DateTime.Now.ToString("yyMMddhhmmss");
            string _excelName = dir + @"\" + string.Join("", "รายงานตารางราคาสินค้า", '_', cDate) + ".xls";

            My_DataTable_Extensions.ExportToExcelR2(new List<DataTable>() { _newTable }, _excelName, "รายงานตารางราคาสินค้า");

            wait.Hide();
            wait.Dispose();
            wait.Close();
        }
    }
}
