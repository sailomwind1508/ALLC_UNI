using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
using AllCashUFormsApp.View.UControl.A_UC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmCustomerInfo : Form
    {
        Customer bu = new Customer();
        SaleArea buSaleArea = new SaleArea();
        MenuBU menuBU = new MenuBU();
        CustomerShelf buShelf = new CustomerShelf();

        List<string> PanelSearchControls = new List<string>();
        List<string> PanelEditControls = new List<string>();

        public static SearchAddressModel SAM = new SearchAddressModel();
        List<Control> searchBranchWHControls = new List<Control>();
        List<Control> searchBranchWHControlsEdit = new List<Control>();
        List<Control> searchBranchControl = new List<Control>();

        static DataTable dtCustomer;
        public static string shoptypeID = "";
        public static string pricegroupID = "";

        public static string page = "";// Shelf
        public static string shelfID = "";//Shelf

        public static string _CustImage, _CustomerID;

        Dictionary<Control, Label> validateSaveCtrls = new Dictionary<Control, Label>();
        private static string URLPathImage = "";

        public frmCustomerInfo()
        {
            InitializeComponent();
            PanelSearchControls = new string[] { txtBranchName.Name }.ToList();
            PanelEditControls = new string[] { txtBranchName_.Name }.ToList();

            searchBranchControl = new List<Control>() { txtBranchCode, txtBranchName };
            searchBranchWHControls = new List<Control>() { txtBranchCode_, txtBranchName_ };
            searchBranchWHControlsEdit = new List<Control>() { txtWHCode, txtWHName };

            validateSaveCtrls.Add(txtBranchCode_,lbl_Depo);
            validateSaveCtrls.Add(txtWHCode, lbl_WH);
            validateSaveCtrls.Add(cbbSalArea, lbl_SalArea);
            validateSaveCtrls.Add(txtDistrictCode, lblDistrict);
            validateSaveCtrls.Add(txtCustName, lbl_CName);
            validateSaveCtrls.Add(txtBillTo, lbl_Bill);

            try
            {
                string _BranchID = bu.tbl_Branchs[0].BranchID;

                var _dt = bu.GetServerImagePath(_BranchID);

                string _ServerNameFromCenter = "";
                string imgPathTmp = "";
                if (_dt.Rows.Count > 0 && _BranchID != "104")
                {
                    _ServerNameFromCenter = _dt.Rows[0].Field<string>("ServerPath");
                    imgPathTmp = _ServerNameFromCenter.Split(',')[0].ToString();
                    imgPathTmp = @"http://" + imgPathTmp + @":82/CU";
                }

                if (_BranchID == "104") //เป็นศูนย์ที่ใช้ IP
                {
                    // ConfigurationManager.AppSettings["ServerName"];
                    string ServerName = Connection.ConnectionString;
                    imgPathTmp = ServerName.Split('=')[1].Split(';')[0].ToString();
                    imgPathTmp = @"http://" + imgPathTmp + @"/CU";
                }
                URLPathImage = imgPathTmp;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        #region Method
        private void GetBranch(TextBox txtCode, TextBox txtName)
        {
            var b = bu.GetBranch();
            if (b != null)
            {
                txtCode.Text = b[0].BranchID;
                txtName.Text = b[0].BranchName;
            }
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

            dtpCrDate.SetDateTimePickerFormat();

            grdList.RowsDefaultCellStyle.BackColor = Color.White;
            grdList.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
        }

        private void InitialData()
        {
            grdList.AutoGenerateColumns = false;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            txtBranchName.DisableTextBox(true);
            chkBoxPresale.Enabled = false;

            pnlEdit.OpenControl(false, PanelEditControls.ToArray()); //

            GetBranch(txtBranchCode, txtBranchName);

            var allWH = new List<tbl_BranchWarehouse>();
            allWH.Add(new tbl_BranchWarehouse { WHID = "-1", WHCode = "==เลือก==" });
            allWH.AddRange(bu.GetAllBranchWarehouse(x => x.WHID.Length == 6));
            ddlWH.BindDropdownList(allWH, "WHCode", "WHID", 0);

            var allSalArea = new List<tbl_SalArea>();
            allSalArea.Add(new tbl_SalArea { SalAreaID = "-1", SalAreaName = "==เลือก==" });
            ddlSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID", 0);

            var ShopType = bu.GetAllShopType();

            var allShopType = new List<tbl_ShopType>();
            allShopType.Add(new tbl_ShopType { ShopTypeID = -1, ShopTypeName = "==เลือก==" });
            allShopType.AddRange(ShopType);
            ddlShopType.BindDropdownList(allShopType, "ShopTypeName", "ShopTypeID", 0); // Search

            cbbShopType.BindDropdownList(ShopType, "ShopTypeName", "ShopTypeID"); // Edit

            var PriceGroup = bu.GetPriceGroup();
            cbbPriceGroup.BindDropdownList(PriceGroup, "PriceGroupName", "PriceGroupID", 0); // Edit

            cbbTitle.Items.Add("บริษัท");
            cbbTitle.Items.Add("หจก.");
            cbbTitle.Items.Add("บมจ.");
            cbbTitle.Items.Add("คุณ");
            cbbTitle.SelectedIndex = 0;

            chkSaleAreaEdit.Enabled = false;
        }

        public void BindCustomerInfo(string customerID)
        {
            InitPage();
            InitialData();

            txtSearch.Text = customerID;

            BindData();
        }

        private void SelectDetails(DataGridViewCellEventArgs e)
        {
            try
            {
               
                DataGridViewRow grdRows = null;

                if (e != null)
                {
                    if (e.RowIndex == -1)
                        return;
                    else
                    {
                        if (grdList.CurrentCell.ColumnIndex == 2)
                            return;
                        else
                            grdRows = grdList.Rows[e.RowIndex];
                    } 
                }
                else
                {
                    grdRows = grdList.CurrentRow;
                }

                if (grdRows != null)
                {
                    string CustomerID = grdRows.Cells["colCustomerID"].Value.ToString();

                    _CustomerID = CustomerID;

                    DataRow r = dtCustomer.AsEnumerable().FirstOrDefault(x => x.Field<string>("CustomerID") == CustomerID);

                    if (r != null)
                    {
                        txtCustomerID.Text = r["CustomerID"].ToString().Trim();
                        txtCustName.Text = r["CustName"].ToString().Trim();

                        cbbShopType.SelectedValue = Convert.ToInt32(r["ShopTypeID"]);

                        txtWHCode.Text = r["WHID"].ToString().Trim();
                        txtWHName.Text = r["WHName"].ToString().Trim();

                        txtCustomerRefCode.Text = r["CustomerRefCode"].ToString().Trim();
                        txtCustShortName.Text = r["CustShortName"].ToString().Trim();

                        txtLatitude.Text = "";
                        if (!string.IsNullOrEmpty(r["Latitude"].ToString()))
                            txtLatitude.Text = r["Latitude"].ToString().Trim();

                        txtLongitude.Text = "";
                        if (!string.IsNullOrEmpty(r["Longitude"].ToString()))
                            txtLongitude.Text = r["Longitude"].ToString().Trim();

                        chkRequestTaxBill.Checked = false;
                        if (!string.IsNullOrEmpty(r["FlagBill"].ToString()) && Convert.ToBoolean(r["FlagBill"]) == true)
                            chkRequestTaxBill.Checked = true;

                        rdoNormal.Checked = true;
                        if (Convert.ToBoolean(r["FlagDel"]) == true)
                            rdoCancel.Checked = true;

                        dtpCrDate.Text = r["CrDate"].ToString();
                        txtBillTo.Text = r["BillTo"].ToString().Trim();
                        txtShipTo.Text = r["ShipTo"].ToString().Trim();
                        txtContact.Text = r["Contact"].ToString().Trim();
                        txtTelephone.Text = r["Telephone"].ToString().Trim();
                        txtEmpID.Text = r["EmpID"].ToString().Trim();
                        txtFirstName.Text = r["FirstName"].ToString().Trim();
                        txtCustomerSAPCode.Text = r["CustomerSAPCode"].ToString().Trim();
                        txtCreditDay.Text = r["CreditDay"].ToString().Trim();

                        txtDistrictCode.Text = r["DistrictCode"].ToString().Trim();
                        txtDistrictName.Text = r["DistrictName"].ToString().Trim();

                        cbbTitle.Text = r["CustTitle"].ToString();
                        cbbSalArea.SelectedValue = r["SalAreaID"].ToString();
                        txtSeq.Text = r["Seq"].ToString().Trim();
                        txtCustomerRefCode.Text = r["CustomerRefCode"].ToString().Trim();

                        txtTaxId.Text = "";
                        string TaxID = r["TaxID"].ToString().Trim(); //Trim ตัดช่องว่างทิ้ง
                        if (!string.IsNullOrEmpty(TaxID))
                            txtTaxId.Text = TaxID;

                        txtFax.Text = r["Fax"].ToString().Trim();

                        txtEmail.Text = r["Email"].ToString().Trim();
                        cbbPriceGroup.SelectedValue = Convert.ToInt32(r["PriceGroupID"]);

                        if (r["BillTo"].ToString().Contains("ม")) //08/04/2022 Adisorn
                        {
                            string _Moo = "ม" + r["BillTo"].ToString().Split('ม')[1].Split(' ')[0].ToString();
                            SAM.Moo = _Moo;
                        }

                        SAM.AddressNo = r["AddressNo"].ToString();
                        SAM.lane = r["lane"].ToString();
                        SAM.Street = r["Street"].ToString();
                        SAM.AreaID = Convert.ToInt32(r["AreaID"]);
                        SAM.ProvinceID = Convert.ToInt32(r["ProvinceID"]);
                        SAM.PostalCode = r["PostalCode"].ToString();
                        SAM.Moo = r["Moo"].ToString();

                        SAM.DistrictID = 0;
                        if (!string.IsNullOrEmpty(r["DistrictID"].ToString()))
                            SAM.DistrictID = Convert.ToInt32(r["DistrictID"]);

                        PrePareAddShelf(CustomerID);

                        picCustomerImg.Image = null;
                        _CustImage = ""; //ส่งURLไปยังหน้าโชว์รูปภาพร้านค้า
                        GetImageFromUrl(r["CustImage"].ToString(), CustomerID);

                        GetBranch(txtBranchCode_, txtBranchName_); //

                        txtCustomerRefCode.Text = txtBranchCode_.Text; //

                        txtRemark.Text = r["Remark"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void GetImageFromUrl(string CustImage, string CustomerID)
        {
            //if (!string.IsNullOrEmpty(r["CustImage"].ToString()))
            //{
            //    Byte[] data = new Byte[0];
            //    data = (Byte[])r["CustImage"];
            //    picCustomerImg.Image = data.byteArrayToImage();
            //}

            if (!string.IsNullOrEmpty(CustImage) && CustImage.Contains("Images"))
            {
                CustImage = CustImage + " ";
                string CustImagePath = CustImage.Split('~')[1].Split(' ')[0].ToString();
                string Src2 = URLPathImage + CustImagePath;
                _CustImage = Src2;
                picCustomerImg.SizeMode = PictureBoxSizeMode.StretchImage;
                picCustomerImg.ImageLocation = Src2;
            }
            else
            {
                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@CustomerID", CustomerID);
                var dt = bu.GetCustomerImage(_params);
                if (dt.Rows.Count > 0 && dt.Rows[0]["CustomerImg"] != DBNull.Value)
                {
                    var img = (byte[])dt.Rows[0]["CustomerImg"];
                    picCustomerImg.SizeMode = PictureBoxSizeMode.StretchImage;
                    picCustomerImg.Image = img.byteArrayToImage();
                }
            }
        }

        private void PrePareAddShelf(string CustomerID)
        {
            listBox_Shelf.Items.Clear();

            var shelfList = buShelf.GetCustomerShelf(x => x.CustomerID == CustomerID && x.FlagDel == false);

            if (shelfList.Count > 0)
            {
                foreach (var item in shelfList)
                {
                    string ShelfID = item.ShelfID.ToString();
                    listBox_Shelf.Items.Add(ShelfID);
                }
                listBox_Shelf.SelectedIndex = 0;
            }
        }

        private void SetGridView()
        {
            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                if (Convert.ToBoolean(grdList.Rows[i].Cells["colFlagMember"].Value) == true)
                {
                    grdList.Rows[i].Cells["colFlagMember"].Value = true;
                }

                var cells = grdList.Rows[i].Cells["colImageCustomer"];

                int _select = 0;
                bool flagColor = false;
                if (int.TryParse(cells.Value.ToString(), out _select))
                {
                    if (_select == 1)
                        flagColor = true;
                }

                cells.Value = flagColor;
                cells.Style.BackColor = flagColor == true ? Color.LightGreen : Color.Red;
            }
        }

        private void SetButtonAfterBindGridView()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            if (grdList.Rows.Count > 0)
            {
                btnPrint.Enabled = true;
                btnEdit.Enabled = true;
                btnChangeSeq.Enabled = true;
                btnCopy.Enabled = true;
            }
            if (rdoN.Checked && grdList.Rows.Count > 0)
                btnRemove.Enabled = true;
            if (rdoC.Checked == true)
                btnAdd.Enabled = false;
        }

        private void BindData()
        {
            pnlEdit.ClearControl();
            pnlEdit.OpenControl(false, PanelEditControls.ToArray());

            string WHID = ddlWH.SelectedIndex == 0 ? "" : ddlWH.SelectedValue.ToString();
            int shoptypeID = ddlShopType.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlShopType.SelectedValue);

            string SalAreaID = "";
            if (ddlSalArea.SelectedItem != null)
                SalAreaID = ddlSalArea.SelectedValue.ToString() == "-1" ? "" : ddlSalArea.SelectedValue.ToString();

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@FlagDel", rdoN.Checked ? 0 : 1);
            _params.Add("@WHID", WHID);
            _params.Add("@SalAreaID", SalAreaID);
            _params.Add("@ShopTypeID", shoptypeID);
            _params.Add("@Search", txtSearch.Text);

            dtCustomer = new DataTable();
            dtCustomer = bu.GetCustomerData(_params);

            if (dtCustomer.Rows.Count > 0)
                dtCustomer.Columns["Seq"].ReadOnly = false; //last edit by adisorn 02/02/2022

            grdList.DataSource = dtCustomer;
            lblQtyCount.Text = dtCustomer.Rows.Count.ToNumberFormat();

            SetGridView(); //SetFlagMember and ImageCustomer
            SetButtonAfterBindGridView();
            SelectDetails(null);
        }

        private void PrePareSaleArea(bool flagChange = false)
        {
            if (txtWHCode.TextLength == 6 && !string.IsNullOrEmpty(txtWHCode.Text))
            {
                var BranchWH = bu.GetAllBranchWarehouse(x => x.WHID == txtWHCode.Text);

                if (BranchWH.Count > 0)
                {
                    txtWHName.Text = BranchWH[0].WHName;
                    string WHID = txtWHCode.Text.Substring(3, 3);

                    var allSalArea = new List<tbl_SalArea>();
                    allSalArea = bu.GetSaleArea(x => x.SalAreaName.Contains(WHID));
                    if (allSalArea.Count > 0 && flagChange == false)
                    {
                        cbbSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID");
                    }
                    else
                    {
                        var AllSalAreaID = bu.GetSaleAreaDistrict(x => x.WHID.Contains(txtWHCode.Text));
                        if (AllSalAreaID.Count > 0 && flagChange == true)
                        {
                            var ListSalAreaID = AllSalAreaID.Select(x => x.SalAreaID).Distinct().ToList();
                            var _allSalArea = bu.GetSaleArea(x => ListSalAreaID.Contains(x.SalAreaID));
                            cbbSalArea.BindDropdownList(_allSalArea, "SalAreaName", "SalAreaID");
                        }
                    }
                }
            }
            else
            {
                var allSalArea = new List<tbl_SalArea>();
                allSalArea.Add(new tbl_SalArea { SalAreaID = "-1", SalAreaName = "" });
                cbbSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID");
            }
        }

        private void PanelEditControlsNotUse()
        {
            //allow edit sapcode for Admin and Superadmin
            if (Helper.tbl_Users.RoleID == 5 || Helper.tbl_Users.RoleID == 10)
                txtCustomerSAPCode.DisableTextBox(false);
            else
                txtCustomerSAPCode.DisableTextBox(true);

            txtBranchCode_.DisableTextBox(true);
            txtWHName.DisableTextBox(true);
            cbbPreOrder.Enabled = false;

            txtDistrictCode.DisableTextBox(true);
            txtDistrictName.DisableTextBox(true);
            txtCustomerRefCode.DisableTextBox(true);
            txtCustomerID.DisableTextBox(true);
            txtBillTo.DisableTextBox(true);
            txtEmpID.DisableTextBox(true);
            txtFirstName.DisableTextBox(true);

            btnSaleEmp.Enabled = false;

            //edit Superadmin Only 
            if (Helper.tbl_Users.RoleID == 10)  //adisorn 17-06-2022
            {
                txtLatitude.DisableTextBox(false);
                txtLongitude.DisableTextBox(false);
            }
            else
            {
                txtLatitude.DisableTextBox(true);
                txtLongitude.DisableTextBox(true);
            }
        }

        private void SetKeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
                e.Handled = true;
        }

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret)
            {
                errList.SetErrMessage(validateSaveCtrls);
            }
            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }
            return ret;
        }

        private void SetCustomerID()
        {
            string _formatCustomerID = txtBranchCode_.Text + txtDistrictCode.Text;
            var allCust = bu.SelectMaxCustomerID(_formatCustomerID);
            if (allCust.Count > 0)
            {
                string MaxID = allCust.Max(x => x.CustomerID);
                var number = Convert.ToInt32(MaxID.Substring(9, MaxID.Length - 9)) + 1;  //Subเพราะ ตัวเลขมากเกินตัวแปรรับได้
                txtCustomerID.Text = _formatCustomerID + number.ToString("0000");
            }
            else
            {
                txtCustomerID.Text = _formatCustomerID + "0001";
            }
        }

        private void PrePareSave(tbl_ArCustomer Customer)
        {
            Customer.SalAreaID = cbbSalArea.SelectedValue.ToString();
            Customer.ShopTypeID = Convert.ToInt32(cbbShopType.SelectedValue);
            Customer.CustTitle = cbbTitle.Text;
            Customer.PriceGroupID = Convert.ToInt32(cbbPriceGroup.SelectedValue);
            Customer.Seq = Convert.ToInt16(txtSeq.Text); //Adisorn 05/04/2022 ลำดับร้านค้า 
            if (Customer.BillTo.Contains("ม")) //08/04/2022 Adisorn
            {
                string _Moo = "ม" + Customer.BillTo.Split('ม')[1].Split(' ')[0].ToString();
                Customer.Moo = _Moo;
            }
            Customer.AddressNo = SAM.AddressNo;
            Customer.lane = SAM.lane;
            Customer.Street = SAM.Street;
            Customer.AreaID = SAM.AreaID;
            Customer.ProvinceID = SAM.ProvinceID;
            Customer.PostalCode = SAM.PostalCode;
            Customer.DistrictID = SAM.DistrictID;
            Customer.FlagBill = chkRequestTaxBill.Checked ? true : false;
            Customer.Remark = txtRemark.Text;
        }

        private void BindSaleAreaPanelSearch(bool flagChange = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlWH.SelectedValue.ToString()) && ddlWH.SelectedValue.ToString() != "-1")
                {
                    string WHID = ddlWH.SelectedValue.ToString().Substring(3, 3);

                    var GetSaleArea = bu.GetSaleArea(x => x.SalAreaName.Contains(WHID));
                    if (GetSaleArea.Count > 0 && flagChange == false)
                    {
                        var allSalArea = new List<tbl_SalArea>();
                        allSalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
                        allSalArea.AddRange(GetSaleArea);
                        ddlSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID");
                    }
                    else
                    {
                        var AllSalAreaID = bu.GetSaleAreaDistrict(x => x.WHID.Contains(ddlWH.SelectedValue.ToString()));

                        if (AllSalAreaID.Count > 0 && flagChange == true)
                        {
                            var ListSalAreaID = AllSalAreaID.Select(x => x.SalAreaID).Distinct().ToList();

                            var allSalArea = new List<tbl_SalArea>();
                            allSalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
                            allSalArea.AddRange(bu.GetSaleArea(x => ListSalAreaID.Contains(x.SalAreaID)));
                            ddlSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID");
                        }
                        else //ชื่อใน SalAreaName ตั้งตามตลาด ไม่ได้เป็น V01
                        {
                            var _SalArea = new List<tbl_SalArea>();
                            _SalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
                            ddlSalArea.BindDropdownList(_SalArea, "SalAreaName", "SalAreaID", 0);
                        }
                    }
                }
                else
                {
                    var allSalArea = new List<tbl_SalArea>();
                    allSalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
                    ddlSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID", 0);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void SetSalAreaData(ComboBox combobox, string _WHID, bool flagSearch = true)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var data = new List<tbl_SalArea>();
                if (flagSearch) //หน้าค้นหา
                {
                    data.Add(new tbl_SalArea { SalAreaID = "-1", SalAreaName = "==เลือก==" });
                }
               
                if (!string.IsNullOrEmpty(_WHID) && _WHID.Length == 6)
                {
                    //var list = bu.GetSaleAreaDistrict(x => x.WHID.Contains(_WHID));

                    //var listSalAreaID = new List<string>();

                    //if (list.Count > 0)
                    //{
                    //    listSalAreaID = list.Select(x => x.SalAreaID).Distinct().ToList();
                    //}

                    //var listSalArea = bu.GetSaleArea(x => listSalAreaID.Contains(x.SalAreaID) && x.Seq != 0);

                    //if (listSalAreaID.Count >= 15)
                    //{
                    //    data.AddRange(listSalArea);
                    //}
                    //else
                    //{
                    //    _WHID = _WHID.Substring(3, 3);
                    //    var SalArea = bu.GetSaleArea(x => x.SalAreaName.Contains(_WHID) && x.Seq != 0);
                    //    data.AddRange(SalArea);
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
                if (data.Count == 0)
                {
                    data.Add(new tbl_SalArea { SalAreaID = "-1", SalAreaName = "" });
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

        #endregion

        #region Method
        private void frmCustomer_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }

        private void ddlWH_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PrePareWareHouseData(); --Old
            //Edit 29/7/2564

            //BindSaleAreaPanelSearch();
            string _whid = "";
            if (!string.IsNullOrEmpty(ddlWH.SelectedValue.ToString()) && ddlWH.SelectedValue.ToString() != "-1" )
            {
                 _whid = ddlWH.SelectedValue.ToString();
            }
            SetSalAreaData(ddlSalArea, _whid);//16-06-2022 adisorn
        }

        private void txtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //var flagChange = chkSaleAreaEdit.Checked ? true : false;
                //PrePareSaleArea(flagChange);

                SetSalAreaData(cbbSalArea, txtWHCode.Text, false);//16-06-2022 adisorn
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void grdList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectDetails(e);
        }

        private void txtWHCode_TextChanged(object sender, EventArgs e)
        {
            SetSalAreaData(cbbSalArea, txtWHCode.Text, false);//16-06-2022 adisorn
            //PrePareSaleArea();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SAM = new SearchAddressModel();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            pnlSearch.OpenControl(false, PanelSearchControls.ToArray());
            grdList.Enabled = false;

            pnlEdit.ClearControl();
            pnlEdit.OpenControl(true, PanelEditControls.ToArray());

            PanelEditControlsNotUse();

            GetBranch(txtBranchCode_, txtBranchName_); //
            txtCustomerRefCode.Text = txtBranchCode_.Text; //

            txtWHCode.Focus();

            txtSeq.Text = "0";

            cbbShopType.SelectedIndex = 0;
            cbbTitle.SelectedIndex = 3;
            cbbPriceGroup.SelectedIndex = 0;

            btnAddShelf.Enabled = false;
            btnRemoveShelf.Enabled = false;

            chkSaleAreaEdit.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlEdit.ClearControl();
            pnlEdit.OpenControl(false, PanelEditControls.ToArray());

            pnlSearch.OpenControl(true, PanelSearchControls.ToArray());
            grdList.Enabled = true;
            txtSearch.Focus();

            SetButtonAfterBindGridView();

            chkChangeSaleArea.Enabled = true;

            chkSaleAreaEdit.Checked = false;
            chkSaleAreaEdit.Enabled = false;

            SelectDetails(null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            pnlSearch.OpenControl(false, PanelSearchControls.ToArray());
            pnlEdit.OpenControl(true, PanelEditControls.ToArray());

            btnAddShelf.Enabled = true;
            btnRemoveShelf.Enabled = true;
            PanelEditControlsNotUse();
            txtWHCode.Focus();

            chkSaleAreaEdit.Enabled = true;
        }

        private void btnDepo_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchWHControls, "เลือกเดโป้/สาขา");
            txtCustomerRefCode.Text = txtBranchCode_.Text;
        }

        private void btnSearchVan_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchBranchWHControlsEdit, "เลือกคลังสินค้า", (x => x.VanType != 0));
        }

        private void btnSearchShopType_Click(object sender, EventArgs e)
        {
            if (cbbShopType.SelectedValue is int)
                shoptypeID = cbbShopType.SelectedValue.ToString();

            frmShopType frm = new frmShopType(); //ประเภทร้านค้า
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(shoptypeID))
                cbbShopType.SelectedValue = Convert.ToInt32(shoptypeID);
        }

        private void btnSearchAddress_Click(object sender, EventArgs e)
        {
            SAM.Page = "Customer";
            frmSearchAddress frm = new frmSearchAddress();
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(SAM.billTo))
            {
                txtBillTo.Text = SAM.billTo;
                txtDistrictCode.Text = SAM.DistrictCode;
                txtDistrictName.Text = SAM.districtName;
            }
        }

        private void chkSelectBillTo_CheckedChanged(object sender, EventArgs e)
        {
            txtShipTo.Text = chkSelectBillTo.Checked ? txtBillTo.Text : "";
        }

        private void btnSearchPriceGroup_Click(object sender, EventArgs e)
        {
            frmPriceGroup frm = new frmPriceGroup();
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(pricegroupID))
            {
                cbbPriceGroup.SelectedValue = Convert.ToInt32(pricegroupID);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            txtCustomerID.Text = "";
            txtEmpID.Text = "";
            txtFirstName.Text = "";
            txtWHCode.Focus();

            grdList.Enabled = false;
            pnlSearch.OpenControl(false, PanelSearchControls.ToArray());
            pnlEdit.OpenControl(true, PanelEditControls.ToArray());
            PanelEditControlsNotUse();

            btnAddShelf.Enabled = false;
            btnRemoveShelf.Enabled = false;
            chkSaleAreaEdit.Enabled = true;
        }

        private void txtSeq_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetKeyPress(e);
        }

        private void txtCreditDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetKeyPress(e);
        }

        private void txtTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetKeyPress(e);
        }

        private void txtLatitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != '.'))
                e.Handled = true;
        }

        private void txtLongitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != '.'))
                e.Handled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateSave())
                return;

            var branchWarehouse = bu.GetAllBranchWarehouse(x => x.WHName == txtWHName.Text);
            if (branchWarehouse.Count == 0)
            {
                string msg = "--> รหัส Van ไม่ถูกต้อง !!";
                msg.ShowWarningMessage();
                txtWHCode.ErrorTextBox();
                txtWHName.ErrorTextBox();
                return;
            }

            try
            {
                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;
                var Customer = new tbl_ArCustomer();
                int flagDel = rdoN.Checked ? 0 : 1;
                var CustomerList = bu.GetSelectCustomerID(txtCustomerID.Text, flagDel);
                if (CustomerList.Count > 0)
                {
                    Customer = CustomerList[0];
                    pnlEdit.Controls.SetObjectFromControl(Customer);
                    Customer.WHID = branchWarehouse[0].WHID;
                    PrePareSave(Customer);
                    Customer.FlagDel = rdoNormal.Checked ? false : true;
                    Customer.FlagNew = false;
                    Customer.FlagEdit = true;
                    Customer.FlagSend = false; //last edit by sailom .k 27/10/2021

                    if (string.IsNullOrEmpty(Customer.CrUser))
                        Customer.CrUser = "";

                    Customer.EdUser = Helper.tbl_Users.Username;
                    Customer.EdDate = DateTime.Now;
                }
                else
                {
                    SetCustomerID(); //รันรหัส ลูกค้าใหม่ให้หา 4 หลักล่าสุดเจอ Edit By Adisorn 20/12/2564 
                    pnlEdit.Controls.SetObjectFromControl(Customer);
                    Customer.CustomerCode = txtCustomerID.Text;

                    var custType = bu.GetAllCustomerType();
                    if (custType.Count > 0)
                        Customer.CustomerTypeID = custType[0].ArCustomerTypeID;

                    PrePareSave(Customer);

                    Customer.WHID = txtWHCode.Text;
                    Customer.CrDate = DateTime.Now;
                    Customer.CrUser = Helper.tbl_Users.Username;

                    Customer.EdDate = null;
                    Customer.EdUser = null;
                    Customer.GPSDate = null;
                    Customer.IsNewMember = null;
                    Customer.CustomerImg = null;
                    Customer.CustImage = null;

                    Customer.FlagNew = false;
                    Customer.FlagSend = false;
                    Customer.FlagMember = false;
                    Customer.FlagEdit = false;
                    Customer.FlagDel = false;

                    Customer.NetPoint = 0;
                    Customer.PromotionVanID = false;
                }

                ret = bu.UpdateData(Customer);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    pnlSearch.OpenControl(true, PanelSearchControls.ToArray());
                    grdList.Enabled = true;
                    btnSearch.PerformClick();
                    chkSaleAreaEdit.Checked = false;
                    chkSaleAreaEdit.Enabled = false;
                    chkChangeSaleArea.Enabled = true;
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

        private void txtSeq_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSeq.Text))
            {
                txtSeq.Text = "0";
            }
        }

        private void btnAddShelf_Click(object sender, EventArgs e)
        {
            try
            {
                page = "add";
                frmShelf f = new frmShelf();
                f.ShowDialog();

                int ret = 0;

                if (!string.IsNullOrEmpty(shelfID))
                {
                    var Shelf_List = new List<tbl_ArCustomerShelf>();

                    var custShelf = new tbl_ArCustomerShelf();

                    listBox_Shelf.Items.Add(shelfID);

                    foreach (var item in listBox_Shelf.Items)
                    {
                        Shelf_List = buShelf.GetCustomerShelf(x => x.CustomerID == txtCustomerID.Text && x.FlagDel == false && x.ShelfID == item.ToString());

                        if (Shelf_List.Count == 0)
                        {
                            custShelf = new tbl_ArCustomerShelf();

                            var CustShelfList = buShelf.GetCustShelf();

                            if (CustShelfList.Count > 0)
                            {
                                custShelf.AutoID = CustShelfList.Max(x => x.AutoID) + 1;
                            }
                            else
                            {
                                custShelf.AutoID = 0;
                            }

                            custShelf.CustomerID = txtCustomerID.Text;
                            custShelf.ProductID = bu.GetProduct(x => x.ProductShortName.Contains("ชั้นวาง")).First().ProductID;
                            custShelf.ShelfID = item.ToString();
                            custShelf.WHID = txtWHCode.Text;

                            custShelf.CrDate = DateTime.Now;
                            custShelf.CrUser = Helper.tbl_Users.Username;

                            custShelf.FlagDel = false;
                            custShelf.FlagSend = false;
                            custShelf.FlagNew = true;
                            custShelf.FlagEdit = false;

                            ret = buShelf.UpdateData(custShelf);
                        }

                    }
                    if (ret == 1)
                    {
                        string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();
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

        private void btnRemoveShelf_Click(object sender, EventArgs e)
        {
            string cfMsg = "คุณแน่ใจหรือไม่ที่จะลบข้อมูลรายการ Shelf นี้?";
            string title = "ทำการยืนยัน!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            if (listBox_Shelf.SelectedIndex != -1)
            {
                int ret = 0;
                var Shelf_List = new List<tbl_ArCustomerShelf>();
                string shelf_id = listBox_Shelf.SelectedItem.ToString();
                Shelf_List = buShelf.GetCustomerShelf(x => x.ShelfID == shelf_id && x.CustomerID == txtCustomerID.Text && x.FlagDel == false);
                if (Shelf_List.Count > 0)
                {
                    Shelf_List[0].FlagDel = true;
                    Shelf_List[0].EdDate = DateTime.Now;
                    Shelf_List[0].EdUser = Helper.tbl_Users.Username;

                    foreach (var item in Shelf_List)
                    {
                        ret = buShelf.UpdateData(item);
                    }
                }

                if (ret == 1)
                {
                    string msg = "ลบข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                    listBox_Shelf.Items.RemoveAt(listBox_Shelf.Items.IndexOf(listBox_Shelf.SelectedItem));
                }
                else
                {
                    this.ShowProcessErr();
                }
            }
            else
            {
                string msg = "กรุณาเลือกเลข Shelf ที่ต้องการลบ !!";
                msg.ShowWarningMessage();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string cfMsg = "คุณแน่ใจหรือไม่ที่จะลบข้อมูลรายการนี้?";
            string title = "ทำการยืนยัน!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            int ret = 0;
            var cData = new tbl_ArCustomer();

            int flagDel = rdoN.Checked ? 0 : 1;
            var custList = bu.GetSelectCustomerID(txtCustomerID.Text, flagDel);
            if (custList.Count > 0)
            {
                cData = custList[0];
                cData.FlagDel = true;
                cData.EdDate = DateTime.Now;
                cData.EdUser = Helper.tbl_Users.Username;
            }

            ret = bu.UpdateData(cData);

            if (ret == 1)
            {
                string msg = "บันทึกข้อมูลเรียบร้อยแล้ว !!";
                msg.ShowInfoMessage();
                grdList.Enabled = true;
                txtSearch.Focus();
                btnSearch.PerformClick();
            }
            else
            {
                this.ShowProcessErr();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var b = bu.GetBranch(x => x.BranchCode == txtBranchCode.Text);
                if (b.Count > 0)
                {
                    txtBranchName.Text = b[0].BranchName;
                }
                else
                {
                    txtBranchCode.Clear();
                    txtBranchName.Clear();
                }
            }
        }

        private void btnChangeSeq_Click(object sender, EventArgs e)
        {
            string msg = "start frmCustomerInfo=>btnChangeSeq_Click";
            msg.WriteLog(this.GetType());

            int ret = 0;
            try
            {
                if (grdList.RowCount == 0)
                    return;

                var ListCustomerID = new List<string>();
                for (int i = 0; i < grdList.RowCount; i++)
                {
                    string _CustomerID = grdList.Rows[i].Cells["colCustomerID"].Value.ToString();
                    ListCustomerID.Add(_CustomerID);
                }

                var allCustomerID = string.Join(",", ListCustomerID);
                var cData = bu.SelectCustomerList(allCustomerID);
                if (cData.Count > 0)
                {
                    var dt = (DataTable)grdList.DataSource;
                    for (int i = 0; i < cData.Count; i++)
                    {
                        string _CustID = cData[i].CustomerID;
                        DataRow r = dt.AsEnumerable().FirstOrDefault(x => x.Field<string>("CustomerID") == _CustID);
                        if (r != null)
                        {
                            cData[i].Seq = Convert.ToInt16(r["Seq"]);
                            cData[i].EdDate = DateTime.Now;
                            cData[i].EdUser = Helper.tbl_Users.Username;
                        }
                    }
                    foreach (var data in cData)
                    {
                        ret = bu.UpdateData(data);
                    }
                    if (ret == 1)
                    {
                        msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();
                        btnSearch.PerformClick();
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

            msg = "end frmCustomerInfo=>btnChangeSeq_Click";
            msg.WriteLog(this.GetType());
        }

        private void chkSaleAreaEdit_Click(object sender, EventArgs e)
        {
            //var flagChange = chkSaleAreaEdit.Checked ? true : false;
            //PrePareSaleArea(flagChange);
        }

        private void chkChangeSaleArea_Click(object sender, EventArgs e)
        {
            bool flagChange = chkChangeSaleArea.Checked ? true : false;
            BindSaleAreaPanelSearch(flagChange);
        }

        private void ddlSalArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSalArea.SelectedIndex > 0)
            {
                chkChangeSaleArea.Checked = false;
            }
        }

        private void picCustomerImg_DoubleClick(object sender, EventArgs e)
        {
            //if (picCustomerImg.Image != null)
            //{
            //    frmCustomerPicture frm = new frmCustomerPicture();
            //    frm.ShowDialog();
            //}
        }

        private void cbbSalArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSalArea.SelectedIndex > 0)
            {
                chkSaleAreaEdit.Checked = false;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string msg = "start frmCustomerInfo=>btnPrint_Click";
            msg.WriteLog(this.GetType());
           
            var dtCust = new DataTable("proc_GetCustomerData_New");
            dtCust.Columns.Add("CustomerID", typeof(string));
            dtCust.Columns.Add("CustName", typeof(string));
            dtCust.Columns.Add("ImageCustomer", typeof(bool));
            dtCust.Columns.Add("ShopTypeName", typeof(string));
            dtCust.Columns.Add("SalAreaID", typeof(string));
            dtCust.Columns.Add("SalAreaName", typeof(string));
            dtCust.Columns.Add("WHID", typeof(string));
            dtCust.Columns.Add("Seq", typeof(short));
            dtCust.Columns.Add("FlagMember", typeof(bool));

            var dt = (DataTable)grdList.DataSource;

            foreach (DataRow r in dt.Rows)
            {
                dtCust.Rows.Add(r["CustomerID"], r["CustName"], r["ImageCustomer"]
                    , r["ShopTypeName"], r["SalAreaID"], r["SalAreaName"]
                    , r["WHID"], r["Seq"], r["FlagMember"]);
            }

            this.OpenExcelReportsPopup("รายงานร้านค้าทั้งหมด", "proc_GetCustomerDataToExcel.xslt", dtCust, true);
            
            msg = "end frmCustomerInfo=>btnPrint_Click";
            msg.WriteLog(this.GetType());
        }

        #endregion

        private void frmCustomerInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            dtCustomer = new DataTable();
            this.Close();
        }

        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            //SelectDetails(null); //edit by sailom 24/10/2021
        }

        private void picCustomerImg_Click(object sender, EventArgs e)
        {
            if (picCustomerImg.Image != null)
            {
                frmCustomerPicture frm = new frmCustomerPicture();
                frm.ShowDialog();
            }
        }

        private void btnSearchBranch_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControl, "เลือกเดโป้/สาขา");
        }

        private void btnUpdateImage_Click(object sender, EventArgs e)
        {
            string cfMsg = "ต้องการปรับปรุงข้อมูลรูปภาพใช่หรือไม่?";
            string title = "ยืนยัน!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                bool ret = false;
                ret = bu.ManualUpdateCustomerImage();

                if (ret == true)
                {
                    cfMsg = "ปรับปรุงข้อมูลรูปภาพ เรียบร้อยแล้ว!!";
                    cfMsg.ShowInfoMessage();
                }
                else
                {
                    cfMsg = "ปรับปรุงข้อมูลรูปภาพ ล้มเหลว!!";
                    cfMsg.ShowErrorMessage();
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.Message.ShowErrorMessage();
            }
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);

            //var cells = grdList.Rows[e.RowIndex].Cells["colImageCustomer"];

            //int _select = 0;
            //bool flagColor = false;
            //if (int.TryParse(cells.Value.ToString(), out _select))
            //{
            //    if (_select == 1)
            //    {
            //        flagColor = true;
            //    }
            //}

            //if (flagColor)
            //{
            //    cells.Style.BackColor = Color.LightGreen;
            //}
            //else
            //    cells.Style.BackColor = Color.Red;

        }
    }
}
