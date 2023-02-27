using AllCashUFormsApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Model;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmTransferCustomer : Form
    {
        MenuBU menuBU = new MenuBU();
        Customer bu = new Customer();
        List<Control> DepoControls = new List<Control>();
        List<Control> BranchWareHouseControls = new List<Control>();
        List<Control> BranchWareHouseControls_ = new List<Control>();
        static List<string> whCodeList = new List<string>() { "1000", "1800", "1900" };
        public frmTransferCustomer()
        {
            InitializeComponent();
            DepoControls = new List<Control>() { txtBranchCode, txtBranchName };
            BranchWareHouseControls = new List<Control>() { txtWHCode, txtWHName };
            BranchWareHouseControls_ = new List<Control>() { _txtWHCode, _txtWHName };
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
        private void BindDropDownSalArea(ComboBox ddl1)
        {
            var AddList = new List<tbl_SalArea>();
            AddList.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
            ddl1.BindDropdownList(AddList, "SalAreaName", "SalAreaID");
        }
        private void InitialData()
        {
            var b = bu.GetBranch();
            if (b != null)
            {
                this.BindData("FromBranchID", DepoControls, b[0].BranchID);
            }

            BindDropDownSalArea(ddlSalArea);

            BindDropDownSalArea(_ddlSalArea);

            btnSearchEmp.Enabled = false;

            txtBranchCode.DisableTextBox(true);
            txtBranchName.DisableTextBox(true);
            txtWHName.DisableTextBox(true);

            textBox1.DisableTextBox(true);
            _txtWHName.DisableTextBox(true);
            txtEmpID.DisableTextBox(true);
            txtFirstName.DisableTextBox(true);

            lblTFCount.Text = "0";
            lblCount2.Text = "0";

            ddlPresale.Enabled = false;
            ddlPresale2.Enabled = false;
        }
        private void frmTransferCustomer_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdList2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList2.SetRowPostPaint(sender, e, this.Font);
        }
     
        private void txtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtWHCode.Text != "")
                {
                    List<tbl_BranchWarehouse> warehouse = bu.GetAllBranchWarehouse(x => x.WHCode == txtWHCode.Text); // ดึงชื่อ
                    if (warehouse.Count != 0)
                    {
                        txtWHName.Text = warehouse[0].WHName;
                        BindDDLVan(ddlSalArea, txtWHCode);
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบรหัสคลังสินค้านี้ กรุณากรอกรหัสคลังสินค้าใหม่", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    if (whCodeList.Contains(txtWHCode.Text) == true)
                    {
                        ddlPresale.Checked = true;
                    }
                    else
                    {
                        ddlPresale.Checked = false;
                    }
                }
                else
                {
                    txtWHName.Clear();
                    BindDropDownSalArea(ddlSalArea);
                    ddlPresale.Checked = false;
                    return;
                }
            }
        }
       
        private void btnSearchVan_Click(object sender, EventArgs e)
        {
            //Func<tbl_BranchWarehouse, bool> fbiPredicate = (x => whCodeList.Contains(x.WHCode) || x.WHType == 1);
            this.OpenBranchWarehousePopup(BranchWareHouseControls, "เลือกคลังสินค้า", (x => x.VanType == 1));

            if (txtWHCode.Text != "")
            {
                BindDDLVan(ddlSalArea, txtWHCode);
            }
            if (whCodeList.Contains(txtWHCode.Text) == true)
            {
                ddlPresale.Checked = true;
            }
            else
            {
                ddlPresale.Checked = false;
            }
                
        }
        private void BindDDLVan(ComboBox ddl,TextBox txt)
        {
            List<tbl_SalAreaDistrict> SalAreaDistrictList = bu.GetSaleAreaDistrict(x => x.WHID == txt.Text);
            if (SalAreaDistrictList.Count != 0)
            {
                List<string> listSalArea = SalAreaDistrictList.Select(x => x.SalAreaID).ToList();
                var SalAreaList = bu.GetSaleArea(x => listSalArea.Contains(x.SalAreaID));
                List<tbl_SalArea> AddList = new List<tbl_SalArea>();
                AddList.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
                AddList.AddRange(SalAreaList);
                ddl.BindDropdownList(AddList, "SalAreaName", "SalAreaID");
            }
        }
      
        private void btnSearchVan2_Click(object sender, EventArgs e)
        {
            //Func<tbl_BranchWarehouse, bool> fbiPredicate = (x => whCodeList.Contains(x.WHCode) || x.WHType == 1); //
            this.OpenBranchWarehousePopup(BranchWareHouseControls_, "เลือกคลังสินค้า", (x => x.VanType == 1)); // 

            if (_txtWHCode.Text != "")
            {
                BindDDLVan(_ddlSalArea, _txtWHCode);

                List<tbl_BranchWarehouse> branchwarehouseList = bu.GetAllBranchWarehouse(x => x.WHID == _txtWHCode.Text);
                if (branchwarehouseList.Count != 0)
                {
                    List<tbl_Employee> empList = bu.GetEmployee(x => x.EmpID == branchwarehouseList[0].SaleEmpID);

                    txtEmpID.Text = empList[0].EmpID;
                    txtFirstName.Text = empList[0].TitleName + " " + empList[0].FirstName;
                }
            }
            if (whCodeList.Contains(_txtWHCode.Text) == true)
            {
                ddlPresale2.Checked = true;
            }
            else
            {
                ddlPresale2.Checked = false;
            }
        }

        private void _txtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(_txtWHCode.Text != "")
                {
                    List<tbl_BranchWarehouse> warehouse = bu.GetAllBranchWarehouse(x => x.WHCode == _txtWHCode.Text); // ดึงชื่อ
                    if (warehouse.Count != 0)
                    {
                        _txtWHName.Text = warehouse[0].WHName;
                        BindDDLVan(_ddlSalArea, _txtWHCode);
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบรหัสคลังสินค้านี้ กรุณากรอกรหัสคลังสินค้าใหม่", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        _txtWHName.Clear();
                        txtEmpID.Clear();
                        txtFirstName.Clear();
                        BindDropDownSalArea(_ddlSalArea);
                        ddlPresale2.Checked = false;
                        return;
                    }
                    if (whCodeList.Contains(_txtWHCode.Text) == true)
                    {
                        ddlPresale2.Checked = true;
                    }
                    else
                    {
                        ddlPresale2.Checked = false;
                    }
                }
                else
                {
                    _txtWHName.Clear();
                    BindDropDownSalArea(_ddlSalArea);
                    ddlPresale2.Checked = false;
                    return;
                }
            }
        }

        private void BindCustomerData()
        {
            string text = "";
            text = txtSearchCust.Text;
            Func<tbl_ArCustomer, bool> tbl_ArCustomerFunc = null;

            var allWHID = bu.GetBranchWarehouse(x => x.WHName == txtWHName.Text);

            tbl_ArCustomerFunc = (x => x.FlagDel == false &&
                x.SalAreaID == (ddlSalArea.SelectedValue.ToString() != "" ? ddlSalArea.SelectedValue.ToString() : x.SalAreaID)
                    && x.CustName.Contains(text) && (x.WHID == (allWHID != null ? allWHID.WHCode : x.WHID)));

            var dt = bu.GetCustTable(tbl_ArCustomerFunc);

            if (dt.Rows.Count != 0)
            {
                DataTable tmp = dt.Clone();
                tmp.Clear();

                if (grdList2.DataSource != null && grdList2.Rows.Count > 0)
                {
                    DataTable _dt = (DataTable)grdList2.DataSource;
                    var list = _dt.AsEnumerable().ToList();

                    foreach (DataRow row in dt.Rows)
                    {
                        if (!list.Select(x => x.Field<string>("รหัสลูกค้า")).ToList().Contains(row["รหัสลูกค้า"].ToString()))
                        {
                            tmp.Rows.Add(false, row["รหัสลูกค้า"].ToString(), row["ชื่อลูกค้า"].ToString(), row["ที่อยู่"].ToString(), row["ประเภทร้านค้า"].ToString(), Convert.ToInt16(row["Seq"]));
                        }
                    }
                }
                else
                {
                    tmp = dt;
                }
                grdList.DataSource = tmp;


                DataGridViewCheckBoxColumn chk = grdList.Columns[0] as DataGridViewCheckBoxColumn;
                chk.DataPropertyName = "เลือก";
                chk.Name = "cChkID";
                chk.HeaderText = "";
                chk.Width = 30;

                for (int i = 1; i < grdList.ColumnCount; i++)
                {
                    grdList.Columns[i].ReadOnly = true;
                }

                lblTFCount.Text = grdList.Rows.Count.ToNumberFormat();

            }
            else
            {
                lblTFCount.Text = grdList.Rows.Count.ToNumberFormat();
                grdList.DataSource = dt;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindCustomerData();
            chkBoxALL1.Checked = false;
        }
        public void chkboxAdd(DataGridView grdFrom, DataGridView grdTo)
        {
            DataTable dtFrom = (DataTable)grdFrom.DataSource;
            DataTable dtTo = (DataTable)grdTo.DataSource;
            //เตรียมข้อมูลที่เลือกเพื่อเอาไปใส่อีก table----------------------------
            List<CustomerInfo> cList = new List<CustomerInfo>();
            if (grdFrom.Rows.Count != 0)
            {
                foreach (DataGridViewRow r in grdFrom.Rows)
                {
                    if (Convert.ToBoolean(r.Cells[0].Value) == true)
                    {
                        CustomerInfo cModel = new CustomerInfo();

                        string cID = r.Cells[1].Value.ToString();
                        if (cID != "")
                        {
                            cModel.Choose = Convert.ToBoolean(r.Cells[0].Value);
                            cModel.CustomerID = r.Cells[1].Value.ToString();
                            cModel.CustName = r.Cells[2].Value.ToString();
                            cModel.BillTo = r.Cells[3].Value.ToString();
                            cModel.ShopTypeName = r.Cells[4].Value.ToString();
                            cModel.Seq = Convert.ToInt16(r.Cells[5].Value);

                            cList.Add(cModel);
                        }
                    }
                }

                DataTable newData = new DataTable();
                if (newData.Columns.Count == 0)
                {
                    newData.Columns.Add("เลือก", typeof(bool));
                    newData.Columns.Add("รหัสลูกค้า", typeof(string));
                    newData.Columns.Add("ชื่อลูกค้า", typeof(string));
                    newData.Columns.Add("ที่อยู่", typeof(string));
                    newData.Columns.Add("ประเภทร้านค้า", typeof(string));
                    newData.Columns.Add("Seq", typeof(short));

                    if (dtTo != null && dtTo.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtTo.Rows)
                        {
                            newData.Rows.Add(false, row["รหัสลูกค้า"].ToString(), row["ชื่อลูกค้า"].ToString(), row["ที่อยู่"].ToString(), row["ประเภทร้านค้า"].ToString(), Convert.ToInt16(row["Seq"]));
                        }

                        foreach (var item in cList)
                        {
                            newData.Rows.Add(false, item.CustomerID, item.CustName, item.BillTo, item.ShopTypeName, item.Seq);
                        }
                        grdTo.DataSource = null;
                        grdTo.DataSource = newData;
                    }
                    else
                    {
                        foreach (var item in cList)
                        {
                            newData.Rows.Add(false, item.CustomerID, item.CustName, item.BillTo, item.ShopTypeName, item.Seq);
                        }
                        grdTo.DataSource = newData;
                    }
                }
                //เตรียมข้อมูลที่เลือกเพื่อเอาไปใส่อีก table----------------------------
                if (grdTo.Name == "gridCust2")
                {
                    DataGridViewCheckBoxColumn chk = grdTo.Columns[0] as DataGridViewCheckBoxColumn;
                    chk.DataPropertyName = "เลือก";
                    chk.Name = "cChkID";
                    chk.HeaderText = "";
                    chk.Width = 10;
                    
                }
                for (int i = 1; i < grdList2.ColumnCount; i++)
                {
                    grdList2.Columns[i].ReadOnly = true;
                }

                //Remove old Data----------------------------------------------------------
                DataTable temp = new DataTable();
                temp = ((DataTable)grdFrom.DataSource).Clone(); //ใช้ clone เพื่อให้ตัดขาด ไม่ให้ allData เปลี่ยน
                temp.Clear();

                foreach (DataRow row in dtFrom.Rows)
                {
                    if (!cList.Select(x => x.CustomerID).ToList().Contains(row["รหัสลูกค้า"].ToString()))
                    {
                        temp.Rows.Add(false, row["รหัสลูกค้า"].ToString(), row["ชื่อลูกค้า"].ToString(), row["ที่อยู่"].ToString(), row["ประเภทร้านค้า"].ToString(), Convert.ToInt16(row["Seq"]));
                    }
                }
                grdFrom.DataSource = temp;
                lblTFCount.Text = grdList.Rows.Count.ToNumberFormat();
                //Remove old Data----------------------------------------------------------
            }
        }
        private void btnFromTo_Click(object sender, EventArgs e)
        {
            if(grdList.Rows.Count != 0)
            {
                chkboxAdd(grdList, grdList2); //ไม่มี Validate
                lblCount2.Text = grdList2.Rows.Count.ToNumberFormat();
                chkBoxALL1.Checked = false;
            }
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            chkboxAdd(grdList2, grdList); //ไม่มี Validate
            chkALL2.Checked = false;
            lblCount2.Text = grdList2.Rows.Count.ToNumberFormat();
        }
        private void Save() 
        {
            try
            {
                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;
                bu = new Customer();
                List<tbl_ArCustomer> tbl_ArCustomerList = new List<tbl_ArCustomer>();

                var allCust = bu.GetCustomer(x => x.FlagDel == false); // where ด้วย SalAreaIDได้ ??

                foreach (DataGridViewRow r in grdList2.Rows)  //เช็ค Validate ไม่ให้ทำก่อน loop foreach
                {
                    string _CustomerID = r.Cells[1].Value.ToString();
                    List<tbl_ArCustomer> custList = allCust.Where(x => x.CustomerID == _CustomerID).ToList();

                    tbl_ArCustomer cData = new tbl_ArCustomer();
                    cData = custList[0];
                    List<tbl_BranchWarehouse> bwhName = bu.GetAllBranchWarehouse(x => x.WHName == _txtWHName.Text);
                    cData.WHID = bwhName[0].WHCode;
                    cData.SalAreaID = _ddlSalArea.SelectedValue.ToString();
                    cData.EdDate = DateTime.Now;
                    cData.EdUser = Helper.tbl_Users.Username;
                    cData.EmpID = txtEmpID.Text;
                    cData.CrUser = "";
                    tbl_ArCustomerList.Add(cData);
                }
                foreach (var cData in tbl_ArCustomerList)
                {
                    ret = bu.UpdateData(cData);
                }
                if (ret == 1)
                {
                    DataTable temp = new DataTable();
                    temp = ((DataTable)grdList2.DataSource).Clone(); //ได้แค่โครงสร้าง Columns
                    temp.Clear();

                    grdList2.DataSource = temp; //Remove

                    BindCustomerData();

                    BindDropDownSalArea(_ddlSalArea);

                    ClearData();

                    lblCount2.Text = grdList2.Rows.Count.ToNumberFormat();

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
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }

        }
        private void ClearData()
        {
            _txtWHCode.Clear();
            _txtWHName.Clear();
            txtEmpID.Clear();
            txtFirstName.Clear();
        }
        private void btnSaveChange_Click(object sender, EventArgs e)
        {
            string MsgError = "";
            if (_txtWHCode.Text == "")
                MsgError += "กรุณาเลือกคลังที่ต้องการย้ายลูกค้าไป\n";
            if (_ddlSalArea.SelectedValue.ToString() == "")
                MsgError += "กรุณาเลือกตลาดที่ต้องการย้ายลูกค้าไป\n";
            if (grdList2.Rows.Count == 0)
                MsgError += "ไม่พบข้อมูลลูกค้าในตาราง\n";
            var bwh = bu.GetBranchWarehouse(x => x.WHCode == _txtWHCode.Text);
            if (bwh == null)
                MsgError += "รหัสคลังสินค้าไม่ถูกต้อง กรุณากรอกรหัสคลังสินค้าใหม่\n";
            if (MsgError != "")
            {
                MessageBox.Show(MsgError, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                Save();
            }
        }
        private void CheckAll(DataGridView grd1)
        {
            if (grd1.Rows.Count == 0)
            {
                MessageBox.Show("ไม่พบข้อมูลลูกค้า", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                for (int i = 0; i < grd1.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(grd1.Rows[i].Cells[0].Value) == false)
                    {
                        grd1.Rows[i].Cells[0].Value = true;
                    }
                    else
                    {
                        grd1.Rows[i].Cells[0].Value = false;
                    }
                }
            }
        }
        private void chkBoxALL1_CheckedChanged(object sender, EventArgs e)
        {
            if (grdList.Rows.Count != 0)
            {
                CheckAll(grdList);
            }
            else
            {
                chkBoxALL1.Checked = false;
                return;
            }
        }

        private void chkALL2_CheckedChanged(object sender, EventArgs e)
        {
            if (grdList2.Rows.Count != 0)
            {
                CheckAll(grdList2);
            }
            else
            {
                chkALL2.Checked = false;
                return;
            }
              
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnCancelChange_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
