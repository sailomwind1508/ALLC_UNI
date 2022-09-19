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
        Customer bu = new Customer();
        SaleArea buSaleArea = new SaleArea();
        List<Control> DepoControls = new List<Control>();
        List<Control> BranchWareHouseControls = new List<Control>();
        List<Control> BranchWareHouseControls_ = new List<Control>();
        List<Control> BranchControls = new List<Control>();
        static List<string> whCodeList = new List<string>() { "1000", "1800", "1900" };
        public frmTransferCustomer()
        {
            InitializeComponent();
            DepoControls = new List<Control>() { txtBranchCode, txtBranchName };
            BranchWareHouseControls = new List<Control>() { txtWHCode, txtWHName };
            BranchWareHouseControls_ = new List<Control>() { _txtWHCode, _txtWHName };
            BranchControls = new List<Control>() { txtBranchCode, txtBranchName };
        }

        #region Method
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

            grdList.RowsDefaultCellStyle.BackColor = Color.White;
            grdList.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            grdList2.RowsDefaultCellStyle.BackColor = Color.White;
            grdList2.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
        }

        private void SetSalAreaData(ComboBox Combobox)
        {
            var data = new List<tbl_SalArea>();
            data.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
            Combobox.BindDropdownList(data, "SalAreaName", "SalAreaID");
        }

        private void InitialData()
        {
            this.BindData("FromBranchID", DepoControls, bu.tbl_Branchs[0].BranchID);

            SetSalAreaData(ddlSalArea);
            SetSalAreaData(_ddlSalArea);

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

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void BindDDLVan(ComboBox ddl, TextBox txt)
        {
            //List<tbl_SalAreaDistrict> SalAreaDistrictList = bu.GetSaleAreaDistrict(x => x.WHID == txt.Text);
            //if (SalAreaDistrictList.Count != 0)
            //{
            //    List<string> listSalArea = SalAreaDistrictList.Select(x => x.SalAreaID).ToList();
            //    var SalAreaList = bu.GetSaleArea(x => listSalArea.Contains(x.SalAreaID));
            //    List<tbl_SalArea> AddList = new List<tbl_SalArea>();
            //    AddList.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
            //    AddList.AddRange(SalAreaList);
            //    ddl.BindDropdownList(AddList, "SalAreaName", "SalAreaID");
            //}

            var AllSalAreaID = bu.GetSaleAreaDistrict(x => x.WHID.Contains(txt.Text)); //adisorn 02/06/2022
            if (AllSalAreaID.Count > 0)
            {
                var ListSalAreaID = AllSalAreaID.Select(x => x.SalAreaID).Distinct().ToList();

                var allSalArea = bu.GetSaleArea(x => ListSalAreaID.Contains(x.SalAreaID) && x.FlagDel == false).OrderBy(x => x.Seq).ToList();

                var AddList = new List<tbl_SalArea>();
                AddList.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
                AddList.AddRange(allSalArea);
                ddl.BindDropdownList(AddList, "SalAreaName", "SalAreaID");
            }
            else //ไม่มีข้อมูล SalAreaDistrict
            {
                string WHID = txt.Text.Substring(3, 3);
                var _salearea = bu.GetSaleArea(x => x.SalAreaName.Contains(WHID));

                var AddList = new List<tbl_SalArea>();
                AddList.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
                AddList.AddRange(_salearea);
                ddl.BindDropdownList(AddList, "SalAreaName", "SalAreaID");
            }
        }

        private void BindTransferCustomerData()
        {
            try
            {
                var tbl_BranchWarehouse = bu.GetBranchWarehouse(x => x.WHName == txtWHName.Text);

                string _SalAreaID = ddlSalArea.SelectedValue.ToString() != "" ? ddlSalArea.SelectedValue.ToString() : "";

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@SalAreaID", _SalAreaID);
                _params.Add("@CustName", txtSearch.Text.Trim());
                _params.Add("@WHID", tbl_BranchWarehouse != null ? tbl_BranchWarehouse.WHID : "");

                var dt = bu.GetTransferCustomerData(_params);

                DataTable _dt = new DataTable();
                _dt.Columns.Add("เลือก", typeof(bool));
                _dt.Columns.Add("รหัสลูกค้า", typeof(string));
                _dt.Columns.Add("ชื่อลูกค้า", typeof(string));
                _dt.Columns.Add("ที่อยู่", typeof(string));
                _dt.Columns.Add("ประเภทร้านค้า", typeof(string));
                _dt.Columns.Add("Seq", typeof(string));

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _dt.Rows.Add(false, dt.Rows[i]["CustomerID"], dt.Rows[i]["CustName"]
                            , dt.Rows[i]["BillTo"], dt.Rows[i]["ShopTypeName"], dt.Rows[i]["Seq"]);
                    }

                    PrePareData(_dt);
                }
                else
                {
                    grdList.DataSource = _dt;
                }

                lblTFCount.Text = grdList.Rows.Count.ToNumberFormat();
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }

        }

        private void PrePareData(DataTable dt)
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

        private void SetSalAreaData(ComboBox combobox, string _WHID, TextBox txtWareHouseName)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var listWH = bu.GetAllBranchWarehouse(x => x.WHCode == _WHID);
                if (listWH.Count > 0)
                {
                    txtWareHouseName.Text = listWH[0].WHName;
                }

                var data = new List<tbl_SalArea>();
                data.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });

                if (!string.IsNullOrEmpty(_WHID) && _WHID.Length == 6)
                {
                    //var list = bu.GetSaleAreaDistrict(x => x.WHID.Contains(_WHID));

                    //var listSalAreaID = new List<string>();

                    //if (list.Count > 0)
                    //{
                    //    listSalAreaID = list.Select(x => x.SalAreaID).Distinct().ToList();
                    //}

                    //if (listSalAreaID.Count >= 15)
                    //{
                    //    data.AddRange(bu.GetSaleArea(x => listSalAreaID.Contains(x.SalAreaID)));
                    //}
                    //else
                    //{
                    //    _WHID = _WHID.Substring(3, 3);

                    //    var GetSaleArea = bu.GetSaleArea(x => x.SalAreaName.Contains(_WHID));
                    //    if (GetSaleArea.Count > 0)
                    //    {
                    //        data.AddRange(GetSaleArea);
                    //    }
                    //}

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
                ex.Message.ShowErrorMessage();
                Cursor.Current = Cursors.Default;
            }
        }

        private void CheckAll(DataGridView grd1)
        {
            if (grd1.Rows.Count == 0)
            {
                FlexibleMessageBox.Show("ไม่พบข้อมูลลูกค้า", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

        private void NewSave()
        {
            int ret = 0;
            string msg = "";
            try
            {
                if (grdList2.RowCount == 0)
                    return;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                var tbl_ArCustomerList = new List<tbl_ArCustomer>();

                var ListCustomerID = new List<string>();
                for (int i = 0; i < grdList2.RowCount; i++)
                {
                    string _CustomerID = grdList2.Rows[i].Cells[1].Value.ToString();
                    ListCustomerID.Add(_CustomerID);
                }

                var allCustomerID = string.Join(",", ListCustomerID);
                var cData = bu.SelectCustomerList(allCustomerID);
                var branchWH = bu.GetAllBranchWarehouse(x => x.WHName == _txtWHName.Text);
                if (cData.Count > 0)
                {
                    for (int i = 0; i < cData.Count; i++)
                    {
                        cData[i].WHID = branchWH[0].WHCode;
                        cData[i].SalAreaID = _ddlSalArea.SelectedValue.ToString();
                        cData[i].EdDate = DateTime.Now;
                        cData[i].EdUser = Helper.tbl_Users.Username;
                        cData[i].EmpID = txtEmpID.Text;
                        if (string.IsNullOrEmpty(cData[i].CrUser))
                        {
                            cData[i].CrUser = "";
                        }

                        //cData[i].Seq = Convert.ToInt16(r["Seq"]);
                        //cData[i].EdDate = DateTime.Now;
                        //cData[i].EdUser = Helper.tbl_Users.Username;

                    }
                    foreach (var data in cData)
                    {
                        ret = bu.UpdateData(data);
                    }
                    if (ret == 1)
                    {
                        DataTable temp = new DataTable();
                        temp = ((DataTable)grdList2.DataSource).Clone(); //ได้แค่โครงสร้าง Columns
                        temp.Clear();

                        grdList2.DataSource = temp; //Remove

                        //BindCustomerData();
                        BindTransferCustomerData();

                        SetSalAreaData(_ddlSalArea);

                        ClearData();

                        lblCount2.Text = grdList2.Rows.Count.ToNumberFormat();

                        msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();
                    }
                    else
                    {
                        this.ShowProcessErr();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void ClearData()
        {
            _txtWHCode.Clear();
            _txtWHName.Clear();
            txtEmpID.Clear();
            txtFirstName.Clear();
        }

        #endregion

        #region Event
        private void frmTransferCustomer_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }
     
        private void txtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetSalAreaData(ddlSalArea, txtWHCode.Text, txtWHName); //adisorn 16-06-2022
            }

            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (txtWHCode.Text != "")
            //    {
            //        List<tbl_BranchWarehouse> warehouse = bu.GetAllBranchWarehouse(x => x.WHCode == txtWHCode.Text); // ดึงชื่อ
            //        if (warehouse.Count != 0)
            //        {
            //            txtWHName.Text = warehouse[0].WHName;
            //            BindDDLVan(ddlSalArea, txtWHCode);
            //        }
            //        else
            //        {
            //            FlexibleMessageBox.Show("ไม่พบรหัสคลังสินค้านี้ กรุณากรอกรหัสคลังสินค้าใหม่", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //            return;
            //        }
            //        if (whCodeList.Contains(txtWHCode.Text) == true)
            //        {
            //            ddlPresale.Checked = true;
            //        }
            //        else
            //        {
            //            ddlPresale.Checked = false;
            //        }
            //    }
            //    else
            //    {
            //        txtWHName.Clear();
            //        BindDropDownSalArea(ddlSalArea);
            //        ddlPresale.Checked = false;
            //        return;
            //    }
            //}

        }
       
        private void btnSearchVan_Click(object sender, EventArgs e)
        {
            //Func<tbl_BranchWarehouse, bool> fbiPredicate = (x => whCodeList.Contains(x.WHCode) || x.WHType == 1);
            this.OpenBranchWarehousePopup(BranchWareHouseControls, "เลือกคลังสินค้า", (x => x.VanType != 0));

            SetSalAreaData(ddlSalArea, txtWHCode.Text, txtWHName);
            //if (txtWHCode.Text != "")
            //{
            //    BindDDLVan(ddlSalArea, txtWHCode);
            //}
            if (whCodeList.Contains(txtWHCode.Text) == true)
            {
                ddlPresale.Checked = true;
            }
            else
            {
                ddlPresale.Checked = false;
            }
                
        }

        private void btnSearchVan2_Click(object sender, EventArgs e)
        {
            //Func<tbl_BranchWarehouse, bool> fbiPredicate = (x => whCodeList.Contains(x.WHCode) || x.WHType == 1); //
            this.OpenBranchWarehousePopup(BranchWareHouseControls_, "เลือกคลังสินค้า", (x => x.VanType != 0)); // 

            SetSalAreaData(_ddlSalArea, _txtWHCode.Text, _txtWHName);

            //if (_txtWHCode.Text != "")
            //{
            //    BindDDLVan(_ddlSalArea, _txtWHCode);

            //    List<tbl_BranchWarehouse> branchwarehouseList = bu.GetAllBranchWarehouse(x => x.WHID == _txtWHCode.Text);
            //    if (branchwarehouseList.Count != 0)
            //    {
            //        List<tbl_Employee> empList = bu.GetEmployee(x => x.EmpID == branchwarehouseList[0].SaleEmpID);

            //        txtEmpID.Text = empList[0].EmpID;
            //        txtFirstName.Text = empList[0].TitleName + " " + empList[0].FirstName;
            //    }
            //}

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
                SetSalAreaData(_ddlSalArea, _txtWHCode.Text, _txtWHName); //adisorn 16-06-2022
                //if(_txtWHCode.Text != "")
                //{
                //    List<tbl_BranchWarehouse> warehouse = bu.GetAllBranchWarehouse(x => x.WHCode == _txtWHCode.Text); // ดึงชื่อ
                //    if (warehouse.Count != 0)
                //    {
                //        _txtWHName.Text = warehouse[0].WHName;
                //        BindDDLVan(_ddlSalArea, _txtWHCode);
                //    }
                //    else
                //    {
                //        FlexibleMessageBox.Show("ไม่พบรหัสคลังสินค้านี้ กรุณากรอกรหัสคลังสินค้าใหม่", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //        _txtWHName.Clear();
                //        txtEmpID.Clear();
                //        txtFirstName.Clear();
                //        BindDropDownSalArea(_ddlSalArea);
                //        ddlPresale2.Checked = false;
                //        return;
                //    }
                //    if (whCodeList.Contains(_txtWHCode.Text) == true)
                //    {
                //        ddlPresale2.Checked = true;
                //    }
                //    else
                //    {
                //        ddlPresale2.Checked = false;
                //    }
                //}
                //else
                //{
                //    _txtWHName.Clear();
                //    BindDropDownSalArea(_ddlSalArea);
                //    ddlPresale2.Checked = false;
                //    return;
                //}
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindTransferCustomerData();//BindCustomerData();
            chkBoxALL1.Checked = false;
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

        private void btnSaveChange_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (string.IsNullOrEmpty(_txtWHCode.Text))
                msg += "กรุณาเลือกคลังที่ต้องการย้ายลูกค้าไป\n";

            if (string.IsNullOrEmpty(_ddlSalArea.SelectedValue.ToString()))
                msg += "กรุณาเลือกตลาดที่ต้องการย้ายลูกค้าไป\n";

            if (grdList2.Rows.Count == 0)
                msg += "ไม่พบข้อมูลลูกค้าในตาราง\n";

            var branchWH = bu.GetBranchWarehouse(x => x.WHCode == _txtWHCode.Text);
            if (branchWH == null)
                msg += "รหัสคลังสินค้าไม่ถูกต้อง กรุณากรอกรหัสคลังสินค้าใหม่\n";

            if (!string.IsNullOrEmpty(msg))
            {
                msg.ShowWarningMessage();
                return;
            }

            //Save();
            NewSave();
        }

        private void chkBoxALL1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdList.Rows.Count > 0)
                    CheckAll(grdList);
                else
                    chkBoxALL1.Checked = false;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void chkALL2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdList2.Rows.Count != 0)
                    CheckAll(grdList2);
                else
                    chkALL2.Checked = false;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void btnCancelChange_Click(object sender, EventArgs e)
        {
            ClearData();

            var data = new List<tbl_SalArea>();
            data.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
            _ddlSalArea.BindDropdownList(data, "SalAreaName", "SalAreaID");
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindTransferCustomerData();
                //BindCustomerData();
                chkBoxALL1.Checked = false;
            }
        }

        private void btnSearchBranch_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(BranchControls, "เลือกเดโป้/สาขา");
        }

        #endregion

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdList2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList2.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTransferCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        //private void BindCustomerData()
        //{
        //    string text = "";
        //    text = txtSearchCust.Text;
        //    Func<tbl_ArCustomer, bool> tbl_ArCustomerFunc = null;

        //    var allWHID = bu.GetBranchWarehouse(x => x.WHName == txtWHName.Text);

        //    tbl_ArCustomerFunc = (x => x.FlagDel == false &&
        //        x.SalAreaID == (ddlSalArea.SelectedValue.ToString() != "" ? ddlSalArea.SelectedValue.ToString() : x.SalAreaID)
        //            && x.CustName.Contains(text) && (x.WHID == (allWHID != null ? allWHID.WHCode : x.WHID)));

        //    var dt = bu.GetCustTable(tbl_ArCustomerFunc);

        //    if (dt.Rows.Count > 0)
        //    {
        //        PrePareData(dt);
        //    }
        //    else
        //    {
        //        lblTFCount.Text = grdList.Rows.Count.ToNumberFormat();
        //        grdList.DataSource = dt;
        //    }
        //}

        //private void SetSaleArea(bool flagChange, ComboBox ddl, string _WHID)
        //{
        //    if (ddl.SelectedIndex > 0)
        //    {
        //        if (!string.IsNullOrEmpty(_WHID))
        //        {
        //            string WHID = _WHID.Substring(3, 3);

        //            var GetSaleArea = bu.GetSaleArea(x => x.SalAreaName.Contains(_WHID));
        //            if (GetSaleArea.Count > 0 && flagChange == false)
        //            {
        //                var allSalArea = new List<tbl_SalArea>();
        //                allSalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
        //                allSalArea.AddRange(GetSaleArea);
        //                ddl.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID");
        //            }
        //            else
        //            {
        //                var AllSalAreaID = bu.GetSaleAreaDistrict(x => x.WHID.Contains(_WHID));

        //                if (AllSalAreaID.Count > 0 && flagChange == true)
        //                {
        //                    var ListSalAreaID = AllSalAreaID.Select(x => x.SalAreaID).Distinct().ToList();

        //                    var allSalArea = new List<tbl_SalArea>();
        //                    allSalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
        //                    allSalArea.AddRange(bu.GetSaleArea(x => ListSalAreaID.Contains(x.SalAreaID)));
        //                    ddl.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            var allSalArea = new List<tbl_SalArea>();
        //            allSalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
        //            ddl.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID", 0);
        //        }
        //    }
        //    else
        //    {
        //        var allSalArea = new List<tbl_SalArea>();
        //        allSalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
        //        ddl.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID", 0);
        //    }
        //}
        //private void Save() 
        //{
        //    try
        //    {
        //        string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
        //        string title = "ยืนยันการบันทึก!!";

        //        if (!cfMsg.ConfirmMessageBox(title))
        //            return;

        //        int ret = 0;
        //        bu = new Customer();
        //        var tbl_ArCustomerList = new List<tbl_ArCustomer>();

        //        var allCust = bu.GetCustomer(x => x.FlagDel == false); // where ด้วย SalAreaIDได้ ??

        //        var bwhName = bu.GetAllBranchWarehouse(x => x.WHName == _txtWHName.Text);

        //        foreach (DataGridViewRow r in grdList2.Rows)  //เช็ค Validate ไม่ให้ทำก่อน loop foreach
        //        {
        //            string _CustomerID = r.Cells[1].Value.ToString();
        //            var custList = allCust.Where(x => x.CustomerID == _CustomerID).ToList();

        //            var cData = new tbl_ArCustomer();
        //            cData = custList[0];
        //            cData.WHID = bwhName[0].WHCode;
        //            cData.SalAreaID = _ddlSalArea.SelectedValue.ToString();
        //            cData.EdDate = DateTime.Now;
        //            cData.EdUser = Helper.tbl_Users.Username;
        //            cData.EmpID = txtEmpID.Text;
        //            cData.CrUser = "";
        //            tbl_ArCustomerList.Add(cData);
        //        }
        //        foreach (var cData in tbl_ArCustomerList)
        //        {
        //            ret = bu.UpdateData(cData);
        //        }
        //        if (ret == 1)
        //        {
        //            DataTable temp = new DataTable();
        //            temp = ((DataTable)grdList2.DataSource).Clone(); //ได้แค่โครงสร้าง Columns
        //            temp.Clear();

        //            grdList2.DataSource = temp; //Remove

        //            //BindCustomerData();
        //            BindTransferCustomerData();

        //            BindDropDownSalArea(_ddlSalArea);

        //            ClearData();

        //            lblCount2.Text = grdList2.Rows.Count.ToNumberFormat();

        //            string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
        //            msg.ShowInfoMessage();
        //        }
        //        else
        //        {
        //            this.ShowProcessErr();
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ShowErrorMessage();
        //        return;
        //    }
        //}
    }
}
