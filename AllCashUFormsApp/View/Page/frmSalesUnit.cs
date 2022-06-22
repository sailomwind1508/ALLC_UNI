using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmSalesUnit : Form
    {
        SaleArea bu = new SaleArea();
        SaleAreaDistrict buSalAreaDistrict = new SaleAreaDistrict();
        BranchWarehouse buWH = new BranchWarehouse();
        Employee buEmp = new Employee();

        Province buProvince = new Province();
        MenuBU menuBU = new MenuBU();

        List<Control> searchBranchWHControls = new List<Control>();

        List<string> pnlGridViewControls = new List<string>();
        List<string> TempCode = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        List<tbl_Zone> tbl_ZoneList = new List<tbl_Zone>();
        List<tbl_MstArea> tempArea;
        List<tbl_MstDistrict> tempDistrict;

        DataTable tempSalAreaDistrictByWHID = new DataTable();
        DataTable tempSalAreaDistrict = new DataTable();
        DataTable dtArea = new DataTable();
        DataTable dtDistrict = new DataTable();
        DataTable dtProvince = new DataTable();
        public frmSalesUnit()
        {
            InitializeComponent();
            searchBranchWHControls = new List<Control>() { txtWHCode, txtWHName };
            pnlGridViewControls = new string[] { txtBranchName.Name }.ToList();

            this.Load += FrmSalePersonSetting_Load;

            btnEdit.Click += btnEdit_Click;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            btnClose.Click += btnClose_Click;

            btnSearchVan.Click += btnSearchVan_Click;
            btnSearchEmployee.Click += btnSearchEmployee_Click;

            tabSaleArea.SelectedIndexChanged += tabSaleArea_SelectedIndexChanged;
            treeView1.AfterCheck += treeView1_AfterCheck;

            grdSaleArea.EditingControlShowing += grdSaleArea_EditingControlShowing;
            grdSaleArea.RowPostPaint += grdSaleArea_RowPostPaint;
            grdSaleArea.KeyPress += grdSaleArea_KeyPress;

            cbbWHType.SelectedIndexChanged += cbbWHTypeVan_SelectedIndexChanged;

            txtDriverEmpID.TextChanged += txtDriverEmpID_TextChanged;

            this.FormClosed += frmSalesUnit_FormClosed;
        }

        #region Event_Method
        private void FrmSalePersonSetting_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }

        private void btnSearchVan_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchBranchWHControls, "เลือกคลังสินค้า", x => x.WHID.Length == 6);

            if (!string.IsNullOrEmpty(txtWHCode.Text))
            {
                Tab();
            }
        }

        private void btnSearchEmployee_Click(object sender, EventArgs e)
        {
            frmEmployeeInfo frm = new frmEmployeeInfo();
            MainForm mfrm = null;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name.ToLower() == "mainform")
                {
                    mfrm = (MainForm)f;
                }
            }

            frm.MdiParent = mfrm;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void grdSaleArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (grdSaleArea.CurrentCell.ColumnIndex == 4)
                {
                    int[] numberCell = new int[] { 4 };
                    grdSaleArea.SetCellNumberOnly(sender, e, numberCell.ToList());
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void grdSaleArea_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (grdSaleArea.CurrentCell.ColumnIndex == 4)
                {
                    DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
                    tb.KeyPress -= grdSaleArea_KeyPress;
                    tb.KeyPress += grdSaleArea_KeyPress;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void grdSaleArea_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdSaleArea.SetRowPostPaint(sender, e, this.Font);
        }

        private void cbbWHTypeVan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbWHType.SelectedIndex != 0)
            {
                int _whtype = Convert.ToInt32(cbbWHType.SelectedValue);

                var allWHType = bu.GetWHType(x => x.WHType == _whtype);
                if (allWHType.Count == 0)
                    return;
                cbbWHTypeVanType.BindDropdownList(allWHType, "Name", "AutoID", 0);
            }
            else
            {
                var allWHType = bu.GetWHType(x => x.WHType != 0); // edit by sailom .k 03/03/2022 for support pre-order
                if (allWHType.Count == 0)
                    return;
                cbbWHTypeVanType.BindDropdownList(allWHType, "Name", "AutoID", 0);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (tabSaleArea.SelectedIndex == 0)
            {
                if (grdSaleArea.RowCount > 0)
                {
                    grdSaleArea.ReadOnly = false;

                    grdSaleArea.Columns["colSalAreaID"].ReadOnly = true;
                    grdSaleArea.Columns["colSalAreaCode"].ReadOnly = true;
                    grdSaleArea.Columns["colCountCust"].ReadOnly = true;
                }
            }

            btnSearchVan.Enabled = false;
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            txtEmpIDCard.DisableTextBox(false);
            txtPOSNo.DisableTextBox(false);
            txtLicense.DisableTextBox(false);

            cbbWHType.Enabled = true;
            cbbWHTypeVanType.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                List<int> ret = new List<int>();

                var BranchWH = PrePareBranchWarehouse();
                ret.Add(buWH.UpdateData(BranchWH));

                ret.Add(SaveEmployee(BranchWH.SaleEmpID));

                if (ret.All(x => x != 1))
                {
                    this.ShowProcessErr();
                    return;
                }

                if (tabSaleArea.SelectedIndex == 0) //Tab ตลาด
                {
                    Save_TabMarket();
                }
                else if (tabSaleArea.SelectedIndex == 1)
                {
                    Save_TabSalAreaDistrict();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (tabSaleArea.SelectedIndex == 0)
            {
                grdSaleArea.ReadOnly = true;
            }
            else if (tabSaleArea.SelectedIndex == 1)
            {
                treeView1.Nodes.Clear();
                tbl_ZoneList.Clear();
                tempArea.Clear();
                tempDistrict.Clear();
                tempSalAreaDistrictByWHID.Clear();

                tempSalAreaDistrict.Rows.Clear();
                dtArea.Rows.Clear();
                dtDistrict.Rows.Clear();
                dtProvince.Rows.Clear();
            }

            Tab();

            MemoryManagement.FlushMemory();
        }

        private void txtDriverEmpID_TextChanged(object sender, EventArgs e)
        {
            txtDriverEmpName.Text = "";

            var emp = bu.GetEmployee(x => x.EmpID == txtDriverEmpID.Text);
            if (emp.Count > 0)
            {
                txtDriverEmpName.Text = emp[0].TitleName + " " + emp[0].FirstName + " " + emp[0].LastName;
            }
        }

        private void tabSaleArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tab();
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                foreach (TreeNode tn in e.Node.Nodes)
                {
                    tn.Checked = e.Node.Checked;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void frmSalesUnit_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        #endregion

        #region Private_Method
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

        private void SetBranch()
        {
            var b = bu.GetBranch();
            if (b != null)
            {
                txtBranchCode.Text = b[0].BranchID;
                txtBranchName.Text = b[0].BranchName;
            }
        }

        private void SetZone()
        {
            var zonelist = bu.GetZone();
            tbl_ZoneList.Add(new tbl_Zone { ZoneID = -1, ZoneName = "==เลือก==" });
            tbl_ZoneList.AddRange(bu.GetZone());
        }

        private void InitialData()
        {
            pnlFindData.OpenControl(false, pnlGridViewControls.ToArray());

            btnSearchVan.Enabled = true;
            btnSearchEmployee.Enabled = true;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            grdSaleArea.ReadOnly = true;
            grdSaleArea.AutoGenerateColumns = false;

            tabSaleArea.Enabled = true;

            SetBranch();
            SetCashVan();
            SetZone();

            SetAreaColumns(dtArea);
            SetDistrictColumns(dtDistrict);
        }

        private void SetCashVan()
        {
            Dictionary<string, string> Dics = new Dictionary<string, string>();
            Dics.Add("1", "Cash Van");
            Dics.Add("2", "Pre Order");
            cbbWHType.BindDropdownList(Dics, "value", "key");

            var allWHType = bu.GetWHType(x => x.Name.Contains("CashVan"));
            //var allWHType = bu.GetWHType(x => x.WHType != 0); // edit by sailom .k 03/03/2022 for support pre-order
            cbbWHTypeVanType.BindDropdownList(allWHType, "Name", "WHType");
        }

        private void SetAreaColumns(DataTable dtArea)
        {
            dtArea.Columns.Add("SelectArea", typeof(bool));
            dtArea.Columns.Add("AreaCode", typeof(string));
            dtArea.Columns.Add("AreaName", typeof(string));
            dtArea.Columns.Add("AreaID", typeof(int));
            dtArea.Columns.Add("ProvinceName", typeof(string));
        }

        private void SetDistrictColumns(DataTable dtDistrict)
        {
            dtDistrict.Columns.Add("SelectDistrict", typeof(bool));
            dtDistrict.Columns.Add("DistrictCode", typeof(string));
            dtDistrict.Columns.Add("DistrictName", typeof(string));
            dtDistrict.Columns.Add("AreaID", typeof(int));
        }

        private void SetMarketData(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                grdSaleArea.Rows.Add(1);

                grdSaleArea.Rows[i].Cells["colSelect"].Value = false;
                grdSaleArea.Rows[i].Cells["colSalAreaID"].Value = dt.Rows[i].Field<string>("SalAreaID");
                grdSaleArea.Rows[i].Cells["colSalAreaCode"].Value = dt.Rows[i].Field<string>("SalAreaCode");
                grdSaleArea.Rows[i].Cells["colSalAreaName"].Value = dt.Rows[i].Field<string>("SalAreaName");
                grdSaleArea.Rows[i].Cells["colSeq"].Value = dt.Rows[i].Field<short>("Seq");

                DataGridViewComboBoxCell cbbZone = (DataGridViewComboBoxCell)grdSaleArea.Rows[i].Cells["colZoneID"];
                cbbZone.DataSource = tbl_ZoneList;
                cbbZone.DisplayMember = "ZoneName";
                cbbZone.ValueMember = "ZoneID";

                // 0 = index -1 เลือก , 1 = index 0 ในเมือง, 2 index 1 นอกเมือง
                if (dt.Rows[i].Field<int>("ZoneID") == 0)
                    cbbZone.Value = -1;
                else
                    cbbZone.Value = dt.Rows[i].Field<int>("ZoneID");

                grdSaleArea.Rows[i].Cells["colCountCust"].Value = dt.Rows[i].Field<int>("CountCust");
            }
        }

        private void Tab()
        {
            try
            {
                pnlFindData.OpenControl(false, pnlGridViewControls.ToArray());

                btnSearchVan.Enabled = true;
                btnSearchEmployee.Enabled = true;

                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnSave.Enabled = false;
                btnCancel.Enabled = false;

                BindPanelFindData();

                BindMarketData();

                if (tabSaleArea.SelectedIndex == 1)
                {
                    tempSalAreaDistrict = null;
                    tempArea = null;
                    tempDistrict = null;
                    treeView1.Nodes.Clear();

                    if (!string.IsNullOrEmpty(txtWHCode.Text))
                    {
                        dtProvince = buProvince.GetProvinceFromSalAreaDistrict();

                        tempSalAreaDistrict = buSalAreaDistrict.GetSalAreaDistrictData(txtWHCode.Text);
                        tempArea = bu.GetMstArea(x => x.FlagDel == false && x.ProvinceID == dtProvince.Rows[0].Field<int>("ProvinceID"));

                        var filterAreaID = tempArea.Select(x => x.AreaID).ToList();
                        Func<tbl_MstDistrict, bool> Func = (x => filterAreaID.Contains(Convert.ToInt32(x.AreaID)));
                        tempDistrict = bu.GetMstDistrict(Func);
                        tempSalAreaDistrictByWHID = buSalAreaDistrict.GetSalAreaDistrictByWHID(txtWHCode.Text);

                        SetTreeViewData();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void BindPanelFindData()
        {
            var dt = buWH.GetBranchWarehouseData(txtWHCode.Text);
            if (dt.Rows.Count > 0)
            {
                btnEdit.Enabled = true;
                txtEmpIDCard.Text = dt.Rows[0].Field<string>("EmpIDCard"); //รหัสบัญชี
                txtSaleEmpID.Text = dt.Rows[0].Field<string>("SaleEmpID");
                txtSaleEmpName.Text = dt.Rows[0].Field<string>("TitleName") + " " + dt.Rows[0].Field<string>("FirstName") + " " + dt.Rows[0].Field<string>("LastName");
                cbbWHType.SelectedIndex = dt.Rows[0].Field<int>("WHType") == 1 ? 0 : 1;
                int _autoID = dt.Rows[0].Field<int>("AutoID");
                int _vantypeIndex = 0; //AutoID 1,3
                if (cbbWHType.SelectedIndex == 0 && _autoID == 2 || cbbWHType.SelectedIndex == 1 && _autoID == 4)
                {
                    _vantypeIndex = 1;
                }
                cbbWHTypeVanType.SelectedIndex = _vantypeIndex;
                txtDriverEmpID.Text = dt.Rows[0].Field<string>("DriverEmpID");
                txtPOSNo.Text = dt.Rows[0].Field<string>("POSNo");
                txtLicense.Text = dt.Rows[0].Field<string>("License");
            }
            else
            {
                txtEmpIDCard.Text = "";
                txtSaleEmpID.Text = "";
                txtSaleEmpName.Text = "";
                cbbWHType.SelectedIndex = 0;
                txtDriverEmpID.Text = "";
                txtPOSNo.Text = "";
                txtLicense.Text = "";
            }
        }

        private void BindMarketData()
        {
            try
            {
                grdSaleArea.Rows.Clear();

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@WHID", txtWHCode.Text);
                var dt = bu.proc_GetMarketData(_params);

                if (tabSaleArea.SelectedIndex == 0 && dt.Rows.Count > 0)
                {
                    SetMarketData(dt);
                }

                lbl_qty.Text = dt.Rows.Count.ToNumberFormat();
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private List<TreeNode> CheckedNodes()
        {
            List<TreeNode> checked_nodes = new List<TreeNode>();

            for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
            {
                if (treeView1.Nodes[0].Nodes[i].Checked == true)
                {
                    checked_nodes.Add(treeView1.Nodes[0].Nodes[i]);
                }
            }

            return checked_nodes;
        }

        private tbl_BranchWarehouse PrePareBranchWarehouse()
        {   //tbl_BranchWarehouse POSNo,WHType
            var branchWH = new tbl_BranchWarehouse();
            branchWH = bu.GetBranchWarehouse(x => x.WHID == txtWHCode.Text);
            branchWH.POSNo = txtPOSNo.Text;
            branchWH.WHType = Convert.ToInt32(cbbWHType.SelectedValue);
            branchWH.VanType = Convert.ToInt32(cbbWHTypeVanType.SelectedValue);
            branchWH.EdDate = DateTime.Now;
            branchWH.EdUser = Helper.tbl_Users.Username;
            branchWH.License = txtLicense.Text;
            return branchWH;
        }

        private int SaveEmployee(string _SaleEmpID)
        {
            int ret = 0;
            //SaleEmpID จาก tbl_BranchWarehouse ไป update EmpIDCard  tbl_Employee
            var empList = bu.GetEmployee(x => x.EmpID == _SaleEmpID);
            empList[0].EmpIDCard = txtEmpIDCard.Text;
            empList[0].EdDate = DateTime.Now;
            empList[0].EdUser = Helper.tbl_Users.Username;
            ret = buEmp.UpdateData(empList[0]);
            return ret;
        }

        private void Save_TabMarket()
        {
            List<int> ret = new List<int>();

            var listSalArea = bu.GetSalAreaByWHID(txtWHCode.Text);

            //tbl_SalArea Seq,ZoneID จาก gridview
            var _SalAreaList = new List<tbl_SalArea>();

            for (int i = 0; i < grdSaleArea.RowCount; i++)
            {
                string _SalAreaID = grdSaleArea.Rows[i].Cells["colSalAreaID"].Value.ToString();

                var _salarea2 = listSalArea.Where(x => x.SalAreaID == _SalAreaID).ToList();
                if (_salarea2.Count > 0)
                {
                    _salarea2[0].SalAreaName = grdSaleArea.Rows[i].Cells["colSalAreaName"].EditedFormattedValue.ToString();
                    _salarea2[0].EdDate = DateTime.Now;
                    _salarea2[0].EdUser = Helper.tbl_Users.Username;
                    _salarea2[0].Seq = Convert.ToInt16(grdSaleArea.Rows[i].Cells["colSeq"].Value);

                    int _zoneID = Convert.ToInt32(grdSaleArea.Rows[i].Cells["colZoneID"].Value);
                    _salarea2[0].ZoneID = _zoneID == -1 ? 0 : _zoneID;
                    _SalAreaList.Add(_salarea2[0]);
                }
            }

            foreach (var data in _SalAreaList)
            {
                ret.Add(bu.UpdateData(data));
            }

            if (ret.All(x => x == 1))
            {
                string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                msg.ShowInfoMessage();

                pnlFindData.OpenControl(false, pnlGridViewControls.ToArray());

                grdSaleArea.ReadOnly = true;

                Tab();
            }
            else
            {
                this.ShowProcessErr();
            }
        }

        private void Save_TabSalAreaDistrict()
        {
            List<int> ret = new List<int>();
            string msg = "";

            try
            {
                List<TreeNode> _listArea = new List<TreeNode>();
                _listArea = CheckedNodes();

                if (_listArea.Count == 0)
                {
                    msg = "กรุณาเลือกเขตการขาย !!";
                    msg.ShowWarningMessage();
                    return;
                }

                //Remove SalAreaDistrict
                if (tempSalAreaDistrict.Rows.Count > 0)
                {
                    ret.Add(buSalAreaDistrict.DeleteByWHID(txtWHCode.Text));

                    if (ret[0] == 0)
                    {
                        this.ShowProcessErr();
                        return;
                    }
                }

                var _ProvinceName = dtProvince.AsEnumerable().Select(x => x.Field<string>("ProvinceName")).First();

                var list = new List<tbl_SalAreaDistrict>();

                for (int i = 0; i < _listArea.Count; i++)
                {
                    int indexArea = _listArea[i].Index;

                    for (int y = 0; y < treeView1.Nodes[0].Nodes[indexArea].Nodes.Count; y++)
                    {
                        if (!treeView1.Nodes[0].Nodes[indexArea].Nodes[y].Checked)
                            continue;

                        List<char> areaText2 = treeView1.Nodes[0].Nodes[indexArea].Nodes[y].Parent.Text.ToCharArray().ToList();

                        string _AreaName2 = "";

                        for (int x = 0; x < areaText2.Count; x++)
                        {
                            string text = areaText2[x].ToString();

                            if (!TempCode.Contains(text) && text != ":")
                            {
                                _AreaName2 += text;
                            }
                        }

                        List<char> districtText2 = treeView1.Nodes[0].Nodes[indexArea].Nodes[y].Text.ToCharArray().ToList();

                        string _DistrictCode2 = "";
                        string _DistrictName2 = "";

                        for (int z = 0; z < districtText2.Count; z++)
                        {
                            string text = districtText2[z].ToString();

                            if (!TempCode.Contains(text) && text != ":")
                            {
                                _DistrictName2 += text;
                            }
                            else if (TempCode.Contains(text))
                            {
                                _DistrictCode2 += text;
                            }
                        }

                        var data = new tbl_SalAreaDistrict();
                        data.ProvinceName = _ProvinceName;
                        data.SalAreaID = "";
                        data.WHID = txtWHCode.Text;
                        data.AreaName = _AreaName2;

                        data.DistrictCode = _DistrictCode2;
                        data.DistrictID = Convert.ToInt32(treeView1.Nodes[0].Nodes[indexArea].Nodes[y].Name);
                        data.DistrictName = _DistrictName2;

                        data.PostalCode = "";
                        data.FlagSend = false;

                        list.Add(data);

                    }
                }

                var dtList = new DataTable();
                dtList = SetSalAreaID(list);

                Cursor.Current = Cursors.WaitCursor;

                foreach (DataRow r in dtList.Rows)
                {
                    ret.Add(buSalAreaDistrict.InsertSalAreaDistrict(r));
                }

                if (ret.All(x => x == 1))
                {
                    Cursor.Current = Cursors.Default;

                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    Tab();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    this.ShowProcessErr();
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.Message.ShowErrorMessage();
            }
        }

        private void SetSalAreaDistrict_Table(DataTable dt)
        {
            dt.Columns.Add("SalAreaID", typeof(string));
            dt.Columns.Add("WHID", typeof(string));
            dt.Columns.Add("DistrictID", typeof(int));
            dt.Columns.Add("DistrictCode", typeof(string));
            dt.Columns.Add("DistrictName", typeof(string));
            dt.Columns.Add("AreaName", typeof(string));
            dt.Columns.Add("ProvinceName", typeof(string));
            dt.Columns.Add("PostalCode", typeof(string));
            dt.Columns.Add("FlagSend", typeof(bool));
        }

        private DataTable SetSalAreaID(List<tbl_SalAreaDistrict> dtList) 
        {
            string _SalAreaName = txtWHCode.Text.Substring(3,3);

            var _dtSalArea = bu.GetSalAreaData(0, _SalAreaName);

            var _dt = new DataTable("SalAreaDistrict");
            SetSalAreaDistrict_Table(_dt);

            if (_dtSalArea.Rows.Count > 0)
            {
                for (int i = 0; i < _dtSalArea.Rows.Count; i++)
                {
                    string _SalAreaID = _dtSalArea.Rows[i].Field<string>("SalAreaID");

                    for (int x = 0; x < dtList.Count; x++)
                    {
                        _dt.Rows.Add(_SalAreaID
                            , dtList[x].WHID
                            , dtList[x].DistrictID
                            , dtList[x].DistrictCode
                            , dtList[x].DistrictName
                            , dtList[x].AreaName
                            , dtList[x].ProvinceName
                            , dtList[x].PostalCode
                            , dtList[x].FlagSend);
                    }
                }
            }
            else
            {
                //var SalAreaDistrictList = bu.GetSaleAreaDistrict(x => x.WHID.Contains(txtWHCode.Text));
                if (tempSalAreaDistrictByWHID.Rows.Count > 0)
                {
                    for (int i = 0; i < tempSalAreaDistrictByWHID.Rows.Count; i++)
                    {
                        string _SalAreaID = tempSalAreaDistrictByWHID.Rows[i].Field<string>("SalAreaID");

                        for (int x = 0; x < dtList.Count; x++)
                        {
                            _dt.Rows.Add(_SalAreaID
                                , dtList[x].WHID
                                , dtList[x].DistrictID
                                , dtList[x].DistrictCode
                                , dtList[x].DistrictName
                                , dtList[x].AreaName
                                , dtList[x].ProvinceName
                                , dtList[x].PostalCode
                                , dtList[x].FlagSend);
                        }
                    }
                }
            }
            return _dt;
        }

        private void SetProvince(DataRow r)
        {
            string _key = r["ProvinceID"].ToString();
            string _value = r["ProvinceCode"].ToString() + ":" + r["ProvinceName"].ToString();

            treeView1.Nodes.Add(_key, _value);

            treeView1.Nodes[0].Checked = tempSalAreaDistrict.Rows.Count > 0 ? true : false;
        }

        private List<tbl_MstArea> SetArea(DataRow r)
        {
            var list = new List<tbl_MstArea>();

            if (r != null)
            {
                //int _ProvinceID = Convert.ToInt32(r["ProvinceID"]);
                //list = tempArea.Where(x => x.ProvinceID == _ProvinceID).ToList();
                list = tempArea;

                for (int i = 0; i < list.Count; i++)
                {
                    string _ProvinceID1 = list[i].ProvinceID.ToString();
                    int index = treeView1.Nodes.IndexOfKey(_ProvinceID1);

                    string _key = list[i].AreaID.ToString();
                    string _value = list[i].AreaCode + ":" + list[i].AreaName;

                    treeView1.Nodes[index].Nodes.Add(_key, _value);

                    var row = tempSalAreaDistrict.AsEnumerable().FirstOrDefault(x => x.Field<string>("AreaCode") == list[i].AreaCode);

                    treeView1.Nodes[index].Nodes[i == 0 ? 0 : i].Checked = row != null ? true : false;
                }
            }

            return list;
        }

        private void SetDistrict(List<int> _areaID1)
        {
            var list = tempDistrict.Where(x => _areaID1.Contains(Convert.ToInt32(x.AreaID))).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                string _DistrictID = list[i].DistrictID.ToString();
                string _AreaID = list[i].AreaID.ToString();

                string _FullName = list[i].DistrictCode + ":" + list[i].DistrictName;

                //Fix ว่ามีจังหวัดเดียว index = 0
                //var indexProvince = treeView1.Nodes.IndexOfKey(_AreaID);
                var indexArea = treeView1.Nodes[0].Nodes.IndexOfKey(_AreaID);

                var row = tempSalAreaDistrict.AsEnumerable().FirstOrDefault(x => x.Field<string>("DistrictCode") == list[i].DistrictCode);

                treeView1.Nodes[0].Nodes[indexArea].Nodes.Add(_DistrictID, _FullName);

                if (treeView1.Nodes[0].Nodes[indexArea].FirstNode == null)
                {
                    treeView1.Nodes[0].Nodes[indexArea].Nodes[0].Checked = row != null ? true : false;
                }
                else
                {
                    treeView1.Nodes[0].Nodes[indexArea].LastNode.Checked = row != null ? true : false;
                }
            }
        }

        private void SetTreeViewData()
        {
            DataRow r = dtProvince.AsEnumerable().First();
            SetProvince(r);

            var _AreaList = new List<tbl_MstArea>();
            _AreaList = SetArea(r);

            var _areaID1 = _AreaList.Select(x => x.AreaID).ToList();
            SetDistrict(_areaID1);
        }

        #endregion
    }
}
