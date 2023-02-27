using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmShelfManagement : Form
    {
        Customer buCust = new Customer();
        CustomerShelf bu = new CustomerShelf();
        SaleArea buSaleArea = new SaleArea();
        List<Control> bwhControlsForm = new List<Control>();
        List<Control> bwhControlsTo = new List<Control>();

        public frmShelfManagement()
        {
            InitializeComponent();
            bwhControlsForm = new List<Control>() { txtWHCode, txtWHName };
            bwhControlsTo = new List<Control>() { txtWHCodeTo, txtWHNameTo };
        }

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

            grdFrom.RowsDefaultCellStyle.BackColor = Color.White;
            grdFrom.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            grdTo.RowsDefaultCellStyle.BackColor = Color.White;
            grdTo.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void InitialData()
        {
            txtBranchCode.Text = bu.tbl_Branchs[0].BranchCode;
            txtBranchName.Text = bu.tbl_Branchs[0].BranchName;

            SetSalAreaData(ddlSalArea);
            SetSalAreaData(ddlSalAreaTo);
            SetCustomerData(cbbCustomer);

            txtBranchCode.DisableTextBox(true);
            txtBranchName.DisableTextBox(true);
            txtWHName.DisableTextBox(true);
            txtWHNameTo.DisableTextBox(true);
        }

        private void SetSalAreaData(ComboBox Combobox)
        {
            var data = new List<tbl_SalArea>();
            data.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
            Combobox.BindDropdownList(data, "SalAreaName", "SalAreaID");
        }

        private void SetCustomerData(ComboBox Combobox)
        {
            var data = new List<tbl_ArCustomer>();
            data.Add(new tbl_ArCustomer { CustomerID = "", CustName = "==เลือก==" });
            Combobox.BindDropdownList(data, "CustName", "CustomerID");
        }

        private void SetSalAreaData(ComboBox combobox, string _WHID, TextBox txtWareHouseName)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                txtWareHouseName.Text = "";
                var listWH = bu.GetAllBranchWarehouse(x => x.WHCode == _WHID);
                if (listWH.Count > 0)
                {
                    txtWareHouseName.Text = listWH[0].WHName;
                }

                var data = new List<tbl_SalArea>();
                data.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });

                if (!string.IsNullOrEmpty(_WHID) && _WHID.Length == 6)
                {
                    var listSalArea = buSaleArea.GetSalAreaByWHID(_WHID);

                    if (listSalArea.Count == 24) //มีครบ 24 ตลาด
                    {
                        var listSaleArea = listSalArea.OrderBy(x => x.Seq).ToList();
                        data.AddRange(listSaleArea);
                    }
                    else
                    {
                        _WHID = _WHID.Substring(3, 3);
                        var SalAreaList = bu.GetSaleArea(x => x.SalAreaName.Contains(_WHID) && x.Seq != 0);

                        for (int i = 0; i < listSalArea.Count; i++)  //20-06-2022 adisorn //ในกรณีที่ตลาด เปลี่ยนชื่อ 
                        {
                            var row = SalAreaList.Where(x => x.SalAreaID == listSalArea[i].SalAreaID).ToList();
                            if (row.Count == 0)
                            {
                                SalAreaList.Add(listSalArea[i]);
                            }
                        }

                        var _SalAreaID = SalAreaList.OrderBy(x => x.Seq).ToList();
                        data.AddRange(_SalAreaID);
                    }
                }

                combobox.BindDropdownList(data, "SalAreaName", "SalAreaID");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.Message.ShowErrorMessage();
            }
        }

        private void SetTableData(DataTable dt)
        {
            dt.Columns.Add("เลือก", typeof(bool));
            dt.Columns.Add("RefDocNo", typeof(string));
            dt.Columns.Add("แวน", typeof(string));
            dt.Columns.Add("รหัสลูกค้า", typeof(string));
            dt.Columns.Add("ชื่อลูกค้า", typeof(string));
            dt.Columns.Add("รหัสชั้นวาง", typeof(string));
            dt.Columns.Add("ที่อยู่", typeof(string));
            dt.Columns.Add("เลขประจำตัวผู้เสียภาษี", typeof(string));
        }

        public void ReverseRowData(DataGridView grdFrom_, DataGridView grdTo_)
        {
            chkboxFrom.Checked = false;
            chkboxTo.Checked = false;

            DataTable dtFrom = (DataTable)grdFrom_.DataSource;
            DataTable dtTo = (DataTable)grdTo_.DataSource;

            if (grdFrom_.RowCount == 0)
                return;

            Cursor.Current = Cursors.WaitCursor;

            //เตรียมข้อมูลที่เลือกเพื่อเอาไปใส่อีก table----------------------------
            List<CustomerInfo> cList = new List<CustomerInfo>();
            var dtFilter = dtFrom.AsEnumerable().Where(x => x.Field<bool>("เลือก") == true);

            foreach (DataRow r in dtFilter)
            {
                CustomerInfo cModel = new CustomerInfo();

                string cID = r[3].ToString();
                if (cID != "")
                {
                    cModel.Choose = Convert.ToBoolean(r[0]);
                    cModel.DocNo = r[1].ToString();
                    cModel.WHID = r[2].ToString();
                    cModel.CustomerID = cID;
                    cModel.CustName = r[4].ToString();
                    cModel.ShelfID = r[5].ToString();
                    cModel.BillTo = r[6].ToString();
                    cModel.TaxId = r[7].ToString();
                    cList.Add(cModel);
                }
            }

            DataTable newData = new DataTable();
            SetTableData(newData);

            grdTo_.Columns.Clear();

            if (dtTo != null && dtTo.Rows.Count > 0)
            {
                foreach (DataRow row in dtTo.Rows)
                {
                    newData.Rows.Add(false, row["RefDocNo"], row["แวน"], row["รหัสลูกค้า"], row["ชื่อลูกค้า"], row["รหัสชั้นวาง"], row["ที่อยู่"], row["เลขประจำตัวผู้เสียภาษี"]);
                }

                foreach (var item in cList)
                {
                    newData.Rows.Add(false, item.DocNo, item.WHID, item.CustomerID, item.CustName, item.ShelfID, item.BillTo, item.TaxId);
                }
                grdTo_.DataSource = null;
                grdTo_.DataSource = newData;
            }
            else
            {
                foreach (var item in cList)
                {
                    newData.Rows.Add(false, item.DocNo , item.WHID, item.CustomerID, item.CustName, item.ShelfID, item.BillTo, item.TaxId);
                }
                grdTo_.DataSource = newData;
            }
            //เตรียมข้อมูลที่เลือกเพื่อเอาไปใส่อีก table----------------------------

            //Remove old Data----------------------------------------------------------
            DataTable temp = new DataTable();
            SetTableData(temp);
            grdFrom_.Columns.Clear();

            foreach (DataRow row in dtFrom.Rows)
            {
                bool _colChoose = Convert.ToBoolean(row["เลือก"]);
                if (!_colChoose)
                {
                    temp.Rows.Add(false, row["RefDocNo"], row["แวน"], row["รหัสลูกค้า"], row["ชื่อลูกค้า"], row["รหัสชั้นวาง"], row["ที่อยู่"], row["เลขประจำตัวผู้เสียภาษี"]);
                }
            }
            grdFrom_.DataSource = temp;

            lblFrom.Text = grdFrom.RowCount.ToNumberFormat();
            lblTo.Text = grdTo.RowCount.ToNumberFormat();
            //Remove old Data----------------------------------------------------------

            AddColumnButtonToGridView();

            grdFrom_.Columns[0].Width = 50;
            grdFrom_.Columns[2].Width = 60;
            grdFrom_.Columns[7].Width = 160;

            grdTo_.Columns[0].Width = 50;
            grdTo_.Columns[2].Width = 60;
            grdTo_.Columns[7].Width = 160;

            for (int i = 1; i < grdTo_.ColumnCount; i++)
            {
                grdTo_.Columns[i].ReadOnly = true;
            }

            Cursor.Current = Cursors.Default;
        }

        private void AddColumnButtonToGridView()
        {
            DataGridViewButtonColumn btnRefCust = new DataGridViewButtonColumn();
            btnRefCust.Text = "ข้อมูลลูกค้า";
            btnRefCust.DataPropertyName = "ข้อมูลลูกค้า";
            btnRefCust.HeaderText = "";
            btnRefCust.UseColumnTextForButtonValue = true;
            btnRefCust.Width = 90;
            grdFrom.Columns.Add(btnRefCust);

            DataGridViewButtonColumn btnRefPO = new DataGridViewButtonColumn();
            btnRefPO.Text = "ข้อมูลบิล";
            btnRefPO.DataPropertyName = "ข้อมูลบิล";
            btnRefPO.HeaderText = "";
            btnRefPO.UseColumnTextForButtonValue = true;
            btnRefPO.Width = 90;
            grdFrom.Columns.Add(btnRefPO);
        }

        private void SelectRowData(DataGridView grd1, CheckBox chkbox)
        {
            if (grd1.Rows.Count == 0)
            {
                chkbox.Checked = false;
                FlexibleMessageBox.Show("ไม่พบข้อมูลลูกค้า", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                for (int i = 0; i < grd1.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(grd1.Rows[i].Cells[0].Value) == false)
                        grd1.Rows[i].Cells[0].Value = true;
                    else
                        grd1.Rows[i].Cells[0].Value = false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
        #endregion

        private void frmShelfManagement_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var branchWH = bu.GetBranchWarehouse(x => x.WHName == txtWHName.Text);
                string _SalAreaID = ddlSalArea.SelectedValue.ToString() != "" ? ddlSalArea.SelectedValue.ToString() : "";

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@SalAreaID", _SalAreaID);
                _params.Add("@WHID", branchWH != null ? branchWH.WHID : "");
                _params.Add("@CustomerID", txtSearch.Text.Trim());
                var dt = bu.GetShelfData(_params);

                var _dt = new DataTable();
                SetTableData(_dt);

                foreach (DataRow r in dt.Rows)
                {
                    _dt.Rows.Add(false, r["DocNo"], r["WHID"], r["CustomerID"], r["CustName"], r["ShelfID"], r["BillTo"], r["TaxId"]);
                }
                grdFrom.Columns.Clear();
                grdFrom.DataSource = _dt;

                var tempDT = new DataTable();
                SetTableData(tempDT);
                grdTo.DataSource = tempDT;

                AddColumnButtonToGridView();

                grdFrom.Columns[0].Width = 50;
                grdTo.Columns[0].Width = 50;
                grdFrom.Columns[2].Width = 60;
                grdTo.Columns[2].Width = 60;
                grdFrom.Columns[7].Width = 160;
                grdTo.Columns[7].Width = 160;

                for (int i = 1; i < grdFrom.ColumnCount; i++)
                {
                    grdFrom.Columns[i].ReadOnly = true;
                }

                for (int i = 1; i < grdTo.ColumnCount; i++)
                {
                    grdTo.Columns[i].ReadOnly = true;
                }

                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnSave.Enabled = grdFrom.RowCount > 0 ? true : false;

                lblFrom.Text = grdFrom.RowCount.ToNumberFormat();
                lblTo.Text = grdTo.RowCount.ToNumberFormat();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.Message.ShowErrorMessage();
            }
        }

        private void txtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SetSalAreaData(ddlSalArea, txtWHCode.Text, txtWHName);
        }

        private void btnSearchVan_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(bwhControlsForm, "เลือกคลังสินค้า", (x => x.VanType != 0));
            SetSalAreaData(ddlSalArea, txtWHCode.Text, txtWHName);
        }

        private void btnSearchVanTo_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(bwhControlsTo, "เลือกคลังสินค้า", (x => x.VanType != 0));
            SetSalAreaData(ddlSalAreaTo, txtWHCodeTo.Text, txtWHNameTo);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> ret = new List<int>();
                string msg = "";

                if (grdTo.RowCount == 0)
                    msg += "กรุณาเลือกร้านค้า !!\n";

                var branchWH = bu.GetBranchWarehouse(x => x.WHCode == txtWHCodeTo.Text.Trim());
                if (branchWH == null)
                {
                    msg += "รหัสคลังสินค้าไม่ถูกต้อง !!\n";
                    txtWHCodeTo.ErrorTextBox();
                }

                string _SalAreaID = "";
                if (ddlSalAreaTo.SelectedItem != null)
                    _SalAreaID = ddlSalAreaTo.SelectedValue.ToString() == "-1" ? "" : ddlSalAreaTo.SelectedValue.ToString();

                string _ToCustomerID = "";
                if (cbbCustomer.SelectedItem != null)
                    _ToCustomerID = cbbCustomer.SelectedValue.ToString() == "-1" ? "" : cbbCustomer.SelectedValue.ToString();

                if (string.IsNullOrEmpty(_ToCustomerID))
                    msg += "กรุณาเลือกร้านค้า !!\n";

                if (string.IsNullOrEmpty(_SalAreaID))
                    msg += "กรุณาเลือกตลาด !!\n";

                if (!string.IsNullOrEmpty(msg))
                {
                    msg.ShowWarningMessage();
                    return;
                }

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Cursor.Current = Cursors.WaitCursor;

                var ListParams = new List<Dictionary<string, object>>();

                for (int i = 0; i < grdTo.RowCount; i++)
                {
                    string _FromCustomerID = grdTo.Rows[i].Cells[3].Value.ToString().Trim();
                    string _ShelfID = grdTo.Rows[i].Cells[5].Value.ToString().Trim();
                    var Params = new Dictionary<string, object>();
                    Params.Add("@FromCustomerID", _FromCustomerID);
                    Params.Add("@ToCustomerID", _ToCustomerID);
                    Params.Add("@WHID", branchWH.WHCode);
                    Params.Add("@CrUser", Helper.tbl_Users.Username);
                    Params.Add("@ShelfID", _ShelfID);
                    Params.Add("@SalAreaID", _SalAreaID);
                    ListParams.Add(Params);
                }

                for (int i = 0; i < ListParams.Count; i++)
                {
                    ret.Add(bu.UpdateShelfManagement(ListParams[i]));
                }
                
                if (ret.All(x=>x == 1))
                {
                    chkboxFrom.Checked = false;
                    chkboxTo.Checked = false;

                    txtWHCodeTo.Text = "";
                    txtWHNameTo.Text = "";

                    SetSalAreaData(ddlSalAreaTo);
                    SetCustomerData(cbbCustomer);

                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                    btnSearch.PerformClick();
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

        private void btnFromTo_Click(object sender, EventArgs e)
        {
            ReverseRowData(grdFrom, grdTo);
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            ReverseRowData(grdTo, grdFrom);
        }

        private void chkboxFrom_Click(object sender, EventArgs e)
        {
            SelectRowData(grdFrom, chkboxFrom);
        }

        private void chkboxTo_Click(object sender, EventArgs e)
        {
            SelectRowData(grdTo, chkboxTo);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtWHCodeTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetSalAreaData(ddlSalAreaTo, txtWHCodeTo.Text, txtWHNameTo);
            }
        }

        private void grdFrom_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdFrom.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdTo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdTo.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShelfManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string _SalAreaID = ddlSalAreaTo.SelectedValue.ToString() != "" ? ddlSalAreaTo.SelectedValue.ToString() : "";

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@FlagDel", 0);
            _params.Add("@SalAreaID", _SalAreaID);
            _params.Add("@WHID", txtWHCodeTo.Text.Trim());
            _params.Add("@ShopTypeID", 0);
            _params.Add("@Search", "");

            var dt = new DataTable();
            dt.Columns.Add("CustomerID", typeof(string));
            dt.Columns.Add("CustName", typeof(string));

            var dtCustomer = buCust.GetCustomerData(_params);

            foreach (DataRow r in dtCustomer.Rows)
            {
                dt.Rows.Add(r["CustomerID"], r["CustomerID"] + " " + r["CustName"]);
            }

            cbbCustomer.DataSource = dt;
            cbbCustomer.DisplayMember = "CustName";
            cbbCustomer.ValueMember = "CustomerID";

            Cursor.Current = Cursors.Default;
        }

        private void ddlSalAreaTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSalAreaTo.SelectedValue.ToString() != "-1")
            {
                btnSearchCustomer.PerformClick();
            }
        }

        private void grdFrom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string _CustomerID = grdFrom.Rows[e.RowIndex].Cells[3].Value.ToString();
                string _DocNo = grdFrom.Rows[e.RowIndex].Cells[1].Value.ToString();
                string _whid = grdFrom.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (e.ColumnIndex == 8) //Ref Customer
                {
                    if (string.IsNullOrEmpty(_CustomerID))
                        return;

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
                        frm.BindCustomerInfo(_CustomerID);
                    }
                }
                else if (e.ColumnIndex == 9) //Ref PO
                {
                    if (string.IsNullOrEmpty(_DocNo))
                        return;

                    if (string.IsNullOrEmpty(_whid))
                        return;

                    MainForm mfrm = null;
                    var frmList = Application.OpenForms;

                    if (frmList == null && frmList.Count == 0)
                        return;

                    foreach (Form f in frmList)
                    {
                        if (f.Name.ToLower() == "mainform")
                            mfrm = (MainForm)f;
                    }

                    var wh = bu.GetBranchWarehouse(_whid);
                    if (wh == null)
                        return;

                    if (wh.WHType == 2) //Pre-Order
                    {
                        if (_DocNo.Contains("M"))
                        {
                            frm1000SalesPre frm = new frm1000SalesPre();
                            bool isActive = false;
                            foreach (Form f in frmList)
                            {
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
                                frm.BindVanSalesData(_DocNo);
                            }
                        }
                        else
                        {
                            frmTabletSalesPre frm = new frmTabletSalesPre();
                            bool isActive = false;
                            foreach (Form f in frmList)
                            {
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
                                frm.BindTabletSalesData(_DocNo);
                            }
                        }
                    }
                    else //Cash VAN
                    {
                        if (_DocNo.Contains("M"))
                        {
                            frmVanSales frm = new frmVanSales();
                            bool isActive = false;
                            foreach (Form f in frmList)
                            {
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
                                frm.BindVanSalesData(_DocNo);
                            }
                        }
                        else
                        {
                            frmTabletSales frm = new frmTabletSales();
                            bool isActive = false;
                            foreach (Form f in frmList)
                            {
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
                                frm.BindTabletSalesData(_DocNo);
                            }
                        }
                    }
                }
            }
        }
    }
}
