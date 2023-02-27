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

        #region Event Method
        private void chkBoxSelectBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (grdBranch.Rows.Count > 0)
            {
                for (int i = 0; i < grdBranch.Rows.Count; i++)
                {
                    //bool ChkBranch = Convert.ToBoolean(grdBranch.Rows[i].Cells["colChkBranch"].Value);
                    bool OnlineStatus = Convert.ToBoolean(grdBranch.Rows[i].Cells["colOnlineStatus"].Value);

                    if (OnlineStatus == true && chkBoxSelectBranch.Checked == true)
                        grdBranch.Rows[i].Cells["colChkBranch"].Value = true;
                    else
                        grdBranch.Rows[i].Cells["colChkBranch"].Value = false;
                }
            }
            else
            {
                chkBoxSelectBranch.Checked = false;
            }
        }

        private void frmProductTable_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }

        private void grdPriceGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectPriceGroupDetails(e);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch.PerformClick();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "กลุ่มราคา":
                    {
                        btnRefresh_Branch.PerformClick();
                        OpenPanelSearch(true);
                        OpenPanelEdit(false);
                        btnSearch.PerformClick();
                        chkBoxSelectBranch.Checked = false;
                    }
                    break;
                case "ตารางราคา":
                    {
                        List<ComboBox> cbbList = new List<ComboBox>();
                        cbbList.Add(ddlPriceGroup);
                        cbbList.Add(ddlProductGroup);
                        cbbList.Add(ddlProductSubGroup);

                        SetPanelSearch(true, cbbList, txtSearchProGroup, btnSearchPriceGroup);
                        PrePareDataToCombobox(cbbList);
                        //btnSearchPriceGroup.PerformClick(); ใน Combobox กลุ่มราคา มี Function ค้นหา
                    }
                    break;
                case "ตารางค่าคอมมิชชั่น":
                    {
                        List<ComboBox> cbbList = new List<ComboBox>();
                        cbbList.Add(cbbPriceGroup);
                        cbbList.Add(cbbProductGroup);
                        cbbList.Add(cbbProductSubGroup);

                        SetPanelSearch(true, cbbList, txtSearchCommission, btnSearchCommission);
                        PrePareDataToCombobox(cbbList);
                        //btnSearchCommission.PerformClick(); ใน Combobox กลุ่มราคา มี Function ค้นหา
                    }
                    break;
                default:
                    break;
            }
        }

        private void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ComboBox> cbbList = new List<ComboBox>();
            cbbList.Add(ddlProductGroup);
            cbbList.Add(ddlProductSubGroup);
            BindDataToCombobox_ProductGroup(cbbList);
        }

        private void txtSearchProGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearchPriceGroup.PerformClick();
        }

        private void txtSearchCommission_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearchCommission.PerformClick();
        }

        private void ddlPriceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearchPriceGroup.PerformClick();
        }

        private void cbbPriceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearchCommission.PerformClick();
        }

        private void cbbProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ComboBox> cbbList = new List<ComboBox>();
            cbbList.Add(cbbProductGroup);
            cbbList.Add(cbbProductSubGroup);
            BindDataToCombobox_ProductGroup(cbbList);
        }

        #endregion

        #region Button Event
        private void btnRefresh_Branch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            RefreshBranch();


            Cursor.Current = Cursors.Default;
        }

        private void RefreshBranch()
        {
            var dtBranch = bu.Get_proc_SendProductInfo_GetDataTable();

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
                    , r["BranchID"].ToString(), r["BranchRefCode"].ToString(), r["BranchName"].ToString()
                    , colPic, r["OnlineStatus"]);
            }

            grdBranch.DataSource = newTable;

            //if (grdBranch.Rows.Count > 0)
            //grdBranch.CreateCheckBoxHeaderColumn_Copy("colChkBranch","colOnlineStatus");

            for (int i = 0; i < grdBranch.Rows.Count; i++)
            {
                bool flagOn = Convert.ToBoolean(grdBranch.Rows[i].Cells["colOnlineStatus"].Value);
                if (flagOn == false) //= true can edit row data
                    grdBranch.Rows[i].Cells["colChkBranch"].ReadOnly = true;
            }

            lblgrdQty.Text = newTable.Rows.Count.ToNumberFormat();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            txtPriceGroupCode.Text = "";
            txtPriceGroupName.Text = "";

            if (grdBranch.RowCount == 0)
                return;

            var dt = bu.GetPriceGroupData(txtSearch.Text, rdoN.Checked ? 0 : 1);
            grdPriceGroup.DataSource = dt;
            lbl_qty_groupPrice.Text = dt.Rows.Count.ToNumberFormat();

            if (dt.Rows.Count > 0)
                grdPriceGroup.CreateCheckBoxHeaderColumn("colChkPriceGroup");

            SelectPriceGroupDetails(null);
            SetButtonAfterBindGrid(grdPriceGroup.Rows.Count);

            if (rdoC.Checked)
                btnAdd.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnCopy.Enabled = false;
            btnPrint.Enabled = false;

            switch (tabControl1.SelectedTab.Text)
            {
                case "กลุ่มราคา":
                    {
                        OpenPanelSearch(false);
                        OpenPanelEdit();
                        txtPriceGroupCode.DisableTextBox(true);
                    }
                    break;
                case "ตารางราคา":
                    {
                        SetColumnsGridView(false);

                        SetColorDataGridView(grdPrdPriceGroup);

                        List<ComboBox> cbbList = new List<ComboBox>();
                        cbbList.Add(ddlPriceGroup);
                        cbbList.Add(ddlProductGroup);
                        cbbList.Add(ddlProductSubGroup);
                        SetPanelSearch(false, cbbList, txtSearchProGroup, btnSearchPriceGroup);
                    }
                    break;
                case "ตารางค่าคอมมิชชั่น":
                    {
                        grdCommission.Columns["colComPrice_t3"].DefaultCellStyle.BackColor = Color.White;
                        grdCommission.Columns["colComPrice_t3"].ReadOnly = false;

                        List<ComboBox> cbbList = new List<ComboBox>();
                        cbbList.Add(cbbPriceGroup);
                        cbbList.Add(cbbProductGroup);
                        cbbList.Add(cbbProductSubGroup);
                        SetPanelSearch(false, cbbList, txtSearchCommission, btnSearchCommission);
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "กลุ่มราคา":
                    {
                        OpenPanelEdit(false);
                        OpenPanelSearch();

                        grdPriceGroup.Enabled = true;
                        btnSearch.PerformClick();
                    }
                    break;
                case "ตารางราคา":
                    {
                        List<ComboBox> cbbList = new List<ComboBox>();
                        cbbList.Add(ddlPriceGroup);
                        cbbList.Add(ddlProductGroup);
                        cbbList.Add(ddlProductSubGroup);
                        SetPanelSearch(true, cbbList, txtSearchProGroup, btnSearchPriceGroup);

                        SetColorDataGridView(grdPrdPriceGroup, false);
                        SetColumnsGridView(true);
                        btnSearchPriceGroup.PerformClick();
                    }
                    break;
                case "ตารางค่าคอมมิชชั่น":
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
                    break;
                default:
                    break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string tabName = tabControl1.SelectedTab.Text.ToString();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnCopy.Enabled = false;

            if (tabName == "กลุ่มราคา")
            {
                OpenPanelSearch(false);
                OpenPanelEdit(true);

                txtPriceGroupCode.Text = "";
                txtPriceGroupName.Text = "";

                dtpStartDate.Value = DateTime.Now;
                dtpEndDate.Value = DateTime.Now;

                SetPriceGroupCode(); //AutoID

                txtPriceGroupCode.DisableTextBox(true);
                grdPriceGroup.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateSave(tabControl1.SelectedTab.Text))
                return;

            string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการบันทึก!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                switch (tabControl1.SelectedTab.Text)
                {
                    case "กลุ่มราคา":
                        SavePriceGroup();
                        break;
                    case "ตารางราคา":
                        SaveProductPriceGroup();
                        break;
                    case "ตารางค่าคอมมิชชั่น":
                        UpdateProductPriceGroup();
                        break;
                    default:
                        break;
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string cfMsg = "คุณแน่ใจหรือไม่ที่จะลบข้อมูลรายการนี้?";
            string title = "ทำการยืนยัน!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                switch (tabControl1.SelectedTab.Text)
                {
                    case "กลุ่มราคา":
                        RemovePriceGroup();
                        break;
                    case "ตารางราคา":
                        RemoveProductPriceGroupWithSQL();//adisorn 17-10-2022 
                        break;
                    case "ตารางค่าคอมมิชชั่น":
                        UpdateProductPriceGroup(true);
                        break;
                    default:
                        break;
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            grdPriceGroup.DataSource = new DataGridView();
            grdBranch.DataSource = new DataGridView();
            grdPrdPriceGroup.DataSource = new DataGridView();
            this.Close();
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            string msg = "";

            if (ValidateBranchCheck() == false)
                msg += "เลือกศูนย์ที่ต้องการส่งข้อมูล !!\n";

            if (ValidatePriceGroupTL1() == true)
                msg += "เลือกข้อมูลตารางสินค้าที่ต้องการส่งข้อมูล !!\n";

            if (!string.IsNullOrEmpty(msg))
            {
                msg.ShowWarningMessage();
                return;
            }

            string cfMsg = "ต้องการส่งข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการส่ง!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            bool ret = false;

            Cursor.Current = Cursors.WaitCursor;

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

            Cursor.Current = Cursors.Default;
            if (ret)
            {
                msg = "ส่งข้อมูลเรียบร้อยแล้ว!!";
                msg.ShowInfoMessage();
            }
            else
            {
                msg = "ส่งข้อมูลล้มเหลว!!";
                msg.ShowErrorMessage();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "กลุ่มราคา":
                    {
                    }
                    break;
                case "ตารางราคา":
                    {
                        frmWait wait = new frmWait();
                        wait.Show();

                        string dir = @"C:\AllCashExcels";

                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

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
                            _newTable.Rows.Add(r["ProductGroupName"], r["ProductID"], r["ProductName"], r["UomSetName"], r["SellPrice"], r["SellPriceVat"], r["BuyPrice"]);
                        }

                        string cDate = DateTime.Now.ToString("yyMMddhhmmss");
                        string _excelName = dir + @"\" + string.Join("", "รายงานตารางราคาสินค้า", '_', cDate) + ".xls";
                        My_DataTable_Extensions.ExportToExcelR2(new List<DataTable>() { _newTable }, _excelName, "รายงานตารางราคาสินค้า");

                        wait.Hide();
                        wait.Dispose();
                        wait.Close();
                    }
                    break;
                case "ตารางค่าคอมมิชชั่น":
                    {
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnSearchPriceGroup_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            SearchPriceGroup();

            Cursor.Current = Cursors.Default;
        }

        private void SearchPriceGroup()
        {
            int PriceGroupID = Convert.ToInt32(ddlPriceGroup.SelectedIndex) == 0 ? 0 : Convert.ToInt32(ddlPriceGroup.SelectedValue);
            int ProductGroupID = Convert.ToInt32(ddlProductGroup.SelectedIndex) == 0 ? 0 : Convert.ToInt32(ddlProductGroup.SelectedValue);
            int ProductSubGroupID = Convert.ToInt32(ddlProductSubGroup.SelectedIndex) == 0 ? 0 : Convert.ToInt32(ddlProductSubGroup.SelectedValue);

            var dtProData = bu.GetPrdUomSetData(ProductGroupID, ProductSubGroupID, txtSearchProGroup.Text, true);

            //DataTable dtProPriceGroup = PriceGroupID == 0 ? bu.GetProductPriceGroup(null).ToDataTable() : bu.GetProductPriceGroup(x => x.PriceGroupID == PriceGroupID).ToDataTable();
            var dtProPriceGroup = bu.GetProductPriceGroup("", PriceGroupID);

            DataTable newTable = new DataTable();
            SetProductPriceGroup_Table(newTable);

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
                        break;
                    else  //เช็คว่า มีข้อมูลใน tbl_ProductPriceGroup
                    {
                        item = tmp.FirstOrDefault(x => x.Field<string>("ProductID") == proID && x.Field<int>("ProductUomID") == UomSetID && x.Field<int>("PriceGroupID") == PriceGroupID);
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
            btnAdd.Enabled = false;
            SetColumnsGridView(true);
        }

        private void btnSearchCommission_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            int PriceGroupID = Convert.ToInt32(cbbPriceGroup.SelectedIndex) == 0 ? 0 : Convert.ToInt32(cbbPriceGroup.SelectedValue);
            int ProductGroupID = Convert.ToInt32(cbbProductGroup.SelectedIndex) == 0 ? 0 : Convert.ToInt32(cbbProductGroup.SelectedValue);
            int ProductSubGroupID = Convert.ToInt32(cbbProductSubGroup.SelectedIndex) == 0 ? 0 : Convert.ToInt32(cbbProductSubGroup.SelectedValue);

            var dtPrdUomset = bu.GetPrdUomSetData(ProductGroupID, ProductSubGroupID, txtSearchCommission.Text, false);

            DataTable dtProPriceGroup = bu.GetProductPriceGroup(x => x.PriceGroupID == PriceGroupID).ToDataTable();

            DataTable newTable = new DataTable();
            SetProductPriceGroup_Table(newTable);
            newTable.Columns.Add("ComPrice", typeof(decimal));

            if (dtProPriceGroup.Rows.Count > 0)
            {
                var tmp = dtProPriceGroup.AsEnumerable().ToList();

                foreach (DataRow r in dtPrdUomset.Rows)
                {
                    string proID = r["ProductID"].ToString();
                    int UomSetID = Convert.ToInt32(r["UomSetID"]);
                    DataRow item;

                    if (dtProPriceGroup.Rows.Count > 0) // tbl_ProductPriceGroup มีข้อมูล
                    {
                        if (PriceGroupID == 0) //กลุ่มราคา = "==เลือก==" ให้หยุดทำงาน
                            break;

                        item = tmp.FirstOrDefault(x => x.Field<string>("ProductID") == proID
                                    && x.Field<int>("ProductUomID") == UomSetID
                                    && x.Field<int>("PriceGroupID") == PriceGroupID
                                    && (x.Field<decimal>("SellPrice") > 0
                                    || x.Field<decimal>("SellPriceVat") > 0
                                    || x.Field<decimal>("BuyPrice") > 0));

                        if (item != null) //มีข้อมูล
                        {
                            newTable.Rows.Add(r["ProductGroupID"], r["ProductSubGroupID"], proID, r["ProductName"], r["ProductGroupName"], r["UomSetID"], r["UomSetName"]
                                , item["PriceGroupID"], item["SellPrice"], item["SellPriceVat"], item["BuyPrice"], item["ComPrice"]);
                        }
                    }
                }
            }

            grdCommission.DataSource = newTable;
            grdCommission.Columns["colComPrice_t3"].ReadOnly = newTable.Rows.Count > 0 ? false : true;
            label1.Text = newTable.Rows.Count.ToNumberFormat();

            SetButtonAfterBindGrid(grdCommission.RowCount);
            Cursor.Current = Cursors.Default;
        }
        #endregion

        #region Private Method
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

            //btnRefresh_Branch.PerformClick();

            RefreshBranch();

            //grdBranch.CreateCheckBoxHeaderColumn("colChkBranch");

            //btnSearch.PerformClick();
            Search();

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

                //btnSearch.PerformClick();
                //btnSearchPriceGroup.PerformClick();

                Search();
                SearchPriceGroup();
            }

            string msg = "สามารถใช้ได้เมื่อต่อ CENTER DB เท่านั้น !!";
            msg.ShowWarningMessage();
        }

        private void SelectPriceGroupDetails(DataGridViewCellEventArgs e)
        {
            DataGridViewRow grdRows = null;

            if (e != null)
            {
                if (e.RowIndex == -1)
                    return;
                else
                    grdRows = grdPriceGroup.Rows[e.RowIndex];
            }
            else
            {
                grdRows = grdPriceGroup.CurrentRow;
            }

            txtPriceGroupCode.Text = "";
            txtPriceGroupName.Text = "";

            if (grdRows != null)
            {
                txtPriceGroupCode.Text = grdRows.Cells["colPriceGroupCode"].Value.ToString();
                txtPriceGroupName.Text = grdRows.Cells["colPriceGroupName"].Value.ToString();

                if (!string.IsNullOrEmpty(grdRows.Cells["colStartDate"].Value.ToString()))
                    dtpStartDate.Value = Convert.ToDateTime(grdRows.Cells["colStartDate"].Value);

                if (!string.IsNullOrEmpty(grdRows.Cells["colEndDate"].Value.ToString()))
                    dtpEndDate.Value = Convert.ToDateTime(grdRows.Cells["colEndDate"].Value);
            }
        }

        private void PrePareDataToCombobox(List<ComboBox> cbbList)
        {
            var PriceGroup = bu.GetPriceGroup(x => x.FlagDel == false).OrderBy(x => x.PriceGroupID).ToList();
            var PriceGroupList = new List<tbl_PriceGroup>();
            PriceGroupList.Add(new tbl_PriceGroup { PriceGroupID = -1, PriceGroupName = "==เลือก==" });
            PriceGroupList.AddRange(PriceGroup);
            cbbList[0].BindDropdownList(PriceGroupList, "PriceGroupName", "PriceGroupID", 1);

            var PrdGroup = bu.GetProductGroup();
            var PrdGroupList = new List<tbl_ProductGroup>();
            PrdGroupList.Add(new tbl_ProductGroup { ProductGroupID = -1, ProductGroupName = "==เลือก==" });
            PrdGroupList.AddRange(PrdGroup);
            cbbList[1].BindDropdownList(PrdGroupList, "ProductGroupName", "ProductGroupID");

            var PrdSubGroup = new List<tbl_ProductSubGroup>();
            PrdSubGroup.Add(new tbl_ProductSubGroup { ProductSubGroupID = -1, ProductSubGroupName = "==เลือก==" });
            cbbList[2].BindDropdownList(PrdSubGroup, "ProductSubGroupName", "ProductSubGroupID");
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

        private void SetPriceGroupCode()
        {
            var PriceGroupList = bu.GetPriceGroup();

            txtPriceGroupCode.Text = "00";

            if (PriceGroupList != null && PriceGroupList.Count > 0)
            {
                int maxPGcode = Convert.ToInt32(PriceGroupList.Select(x => x.PriceGroupCode).Max()) + 1;
                txtPriceGroupCode.Text = maxPGcode.ToString("00");
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

            if (flagRemove)
                tbl_PriceGroups.FlagDel = true;
        }

        private int RemoveProductPriceGroupWithSQL()
        {
            int ret = 0;
            List<string> listProductID = new List<string>();
            for (int i = 0; i < grdPrdPriceGroup.RowCount; i++)
            {
                string _ProductID = grdPrdPriceGroup.Rows[i].Cells["colProductID_t2"].Value.ToString();
                listProductID.Add(_ProductID.Trim());
            }

            string AllProductID = string.Join(",", listProductID.Distinct());
            int _PriceGroupID = Convert.ToInt32(ddlPriceGroup.SelectedValue);

            ret = bu.Remove_ProductPriceGroup(AllProductID, _PriceGroupID);
            return ret;
        }

        private void SaveProductPriceGroup()
        {
            int ret = 0;
            DateTime _CrDate = DateTime.Now;
            //RemoveData_ProductPriceGroup(); //ลบข้อมูลก่อน ทำการ Insert ใหม่
            RemoveProductPriceGroupWithSQL(); //adisorn 17/10/2022

            var list = new List<tbl_ProductPriceGroup>();
            for (int i = 0; i < grdPrdPriceGroup.RowCount; i++)
            {
                var data = new tbl_ProductPriceGroup();
                data.PriceGroupID = Convert.ToInt32(grdPrdPriceGroup.Rows[i].Cells["colPriceGroupID_t2"].Value);
                data.ProductID = grdPrdPriceGroup.Rows[i].Cells["colProductID_t2"].Value.ToString();
                data.ProductUomID = Convert.ToInt32(grdPrdPriceGroup.Rows[i].Cells["colUomSetID_t2"].Value);

                data.SellPrice = Convert.ToDecimal(grdPrdPriceGroup.Rows[i].Cells["colSellPrice_t2"].Value).ToDecimalN2();
                data.BuyPrice = Convert.ToDecimal(grdPrdPriceGroup.Rows[i].Cells["colBuyPrice_t2"].Value).ToDecimalN2();
                data.SellPriceVat = Convert.ToDecimal(grdPrdPriceGroup.Rows[i].Cells["colSellPriceVat_t2"].Value).ToDecimalN2();

                data.CrDate = _CrDate;
                data.CrUser = Helper.tbl_Users.Username;

                data.BuyPriceVat = 0; //ไม่มีข้อมูลในตารางให้กรอก
                data.ComPrice = 0; //ไม่มีข้อมูลในตารางให้กรอก

                list.Add(data);
            }
            ret = bu.UpdateProductPriceGroupData(list); //adisorn 17/10/2022 
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

                SetColumnsGridView(true);
                SetColorDataGridView(grdPrdPriceGroup, false);
            }
            else
            {
                this.ShowProcessErr();
            }
        }

        private void SavePriceGroup()
        {
            int ret = 0;

            var tbl_PriceGroup = new tbl_PriceGroup();
            var priceGroupList = bu.GetPriceGroup(x => x.PriceGroupCode == txtPriceGroupCode.Text);

            if (tbl_PriceGroup != null)
            {
                PrePareEdit_PriceGroup(false, tbl_PriceGroup);
            }
            else
            {
                int _PriceGroupID = 1;

                if (priceGroupList.Count > 0)
                    _PriceGroupID = priceGroupList.Select(x => x.PriceGroupID).Max() + 1;

                tbl_PriceGroup.PriceGroupID = _PriceGroupID;
                tbl_PriceGroup.PriceGroupCode = txtPriceGroupCode.Text;
                tbl_PriceGroup.PriceGroupName = txtPriceGroupName.Text;

                tbl_PriceGroup.CrDate = DateTime.Now;
                tbl_PriceGroup.CrUser = Helper.tbl_Users.Username;

                tbl_PriceGroup.StartDate = dtpStartDate.Value;
                tbl_PriceGroup.EndDate = dtpEndDate.Value;

                tbl_PriceGroup.BranchID = bu.tbl_Branchs[0].BranchID;
            }

            ret = bu.UpdatePriceGroupData(tbl_PriceGroup);

            if (ret == 1)
            {
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
            }
        }

        private void UpdateProductPriceGroup(bool flagRemove = false)
        {
            int ret = 0;

            var _ProductPriceGroupList = bu.GetProductPriceGroup();

            var tbl_ProductPriceGroup = new tbl_ProductPriceGroup();
            var list = new List<tbl_ProductPriceGroup>();

            for (int i = 0; i < grdCommission.RowCount; i++)
            {
                int PriceGroupID = Convert.ToInt32(grdCommission.Rows[i].Cells["colPriceGroupID_t3"].Value);
                string ProductID = grdCommission.Rows[i].Cells["colProductID_t3"].Value.ToString();
                int ProductUomID = Convert.ToInt32(grdCommission.Rows[i].Cells["colUomSetID_t3"].Value);

                var item = _ProductPriceGroupList.FirstOrDefault(x => x.PriceGroupID == PriceGroupID && x.ProductID == ProductID && x.ProductUomID == ProductUomID);

                item.ComPrice = 0;

                if (flagRemove == false)
                    item.ComPrice = Convert.ToDecimal(grdCommission.Rows[i].Cells["colComPrice_t3"].Value).ToDecimalN2();

                item.EdDate = DateTime.Now;
                item.EdUser = Helper.tbl_Users.Username;

                list.Add(item);
            }

            foreach (var item in list)
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

                    btnSearch.PerformClick();
                    grdPriceGroup.Enabled = true;
                }
                else
                {
                    this.ShowProcessErr();
                }               
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void SetProductPriceGroup_Table(DataTable newTable)
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

        private void SetButtonAfterBindGrid(int RowCount)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnCopy.Enabled = false;
            btnPrint.Enabled = false;

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

            if (tabControl1.SelectedTab.Text == "ตารางราคา" && grdPrdPriceGroup.Rows.Count > 0)
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

        private void DataGridView_CellEndEdit(DataGridViewCellEventArgs e, DataGridView grd)
        {
            string columns = grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (string.IsNullOrEmpty(columns))
                grd.CurrentRow.Cells[e.ColumnIndex].Value = 0.00;
            else
                grd.CurrentRow.Cells[e.ColumnIndex].Value = Convert.ToDecimal(columns).ToDecimalN2();
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

        #endregion

        #region AddOnGridView
        private void SetColumnsGridView(bool flagRead)
        {
            grdPrdPriceGroup.Columns["colSellPrice_t2"].ReadOnly = flagRead;
            grdPrdPriceGroup.Columns["colSellPriceVat_t2"].ReadOnly = flagRead;
            grdPrdPriceGroup.Columns["colBuyPrice_t2"].ReadOnly = flagRead;
        }

        private void SetColorDataGridView(DataGridView grd, bool flagColor = true)
        {
            grd.Columns["colSellPrice_t2"].DefaultCellStyle.BackColor = flagColor == true ? Color.White : Color.FromArgb(224, 224, 224);
            grd.Columns["colSellPriceVat_t2"].DefaultCellStyle.BackColor = flagColor == true ? Color.White : Color.FromArgb(224, 224, 224);
            grd.Columns["colBuyPrice_t2"].DefaultCellStyle.BackColor = flagColor == true ? Color.White : Color.FromArgb(224, 224, 224);
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

        private void grdBranch_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdBranch.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdPriceGroup_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPriceGroup.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdCommission_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdCommission.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdPrdPriceGroup_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPrdPriceGroup.SetRowPostPaint(sender, e, this.Font);
        }

        #endregion

        private void frmProductTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}