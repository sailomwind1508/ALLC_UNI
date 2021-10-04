using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
using AllCashUFormsApp.View.UControl.A_UC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        MenuBU menuBU = new MenuBU();
        CustomerShelf buShelf = new CustomerShelf();

        List<string> PanelSearchControls = new List<string>();
        List<string> PanelEditControls = new List<string>();

        public static SearchAddressModel SAM = new SearchAddressModel();
        List<Control> searchBranchWHControls = new List<Control>();
        List<Control> searchBranchWHControlsEdit = new List<Control>();

        static DataTable dtCustomer;
        public static string shoptypeID = "";
        public static string pricegroupID = "";

        public static string page = "";// Shelf
        public static string shelfID = "";//Shelf

        Dictionary<Control, Label> validateSaveCtrls = new Dictionary<Control, Label>();

        public frmCustomerInfo()
        {
            InitializeComponent();
            PanelSearchControls = new string[] { txtBranchName.Name }.ToList();
            PanelEditControls = new string[] { txtBranchName_.Name }.ToList();

            searchBranchWHControls = new List<Control>() { txtBranchCode_, txtBranchName_ };
            searchBranchWHControlsEdit = new List<Control>() { txtWHCode, txtWHName };

            validateSaveCtrls.Add(txtBranchCode_,lbl_Depo);//
            validateSaveCtrls.Add(txtWHCode, lbl_WH);//
            validateSaveCtrls.Add(cbbSalArea, lbl_SalArea);//
            validateSaveCtrls.Add(txtDistrictCode, lblDistrict);//
            validateSaveCtrls.Add(txtCustName, lbl_CName);//
            validateSaveCtrls.Add(txtBillTo, lbl_Bill);//
        }

        #region #--Method
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
        }

        private void SetDefaultPage()
        {
            grdList.AutoGenerateColumns = false;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            chkNormal.Visible = false;

            txtBranchName.DisableTextBox(true);
            chkBoxPresale.Enabled = false;
        }

        private void PrePareTitileName(ComboBox cbbTitle)
        {
            cbbTitle.Items.Add("บริษัท");
            cbbTitle.Items.Add("หจก.");
            cbbTitle.Items.Add("บมจ.");
            cbbTitle.Items.Add("คุณ");
            cbbTitle.SelectedIndex = 0;
        }

        private void InitialData()
        {
            SetDefaultPage();

            pnlEdit.OpenControl(false, PanelEditControls.ToArray()); //

            GetBranch(txtBranchCode, txtBranchName);

            var allWH = new List<tbl_BranchWarehouse>();
            allWH.Add(new tbl_BranchWarehouse { WHID = "-1", WHCode = "==เลือก==" });
            allWH.AddRange(bu.GetAllBranchWarehouse(x => x.WHType == 1));
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

            PrePareTitileName(cbbTitle);
        }

        private void SelectDetails(DataGridViewCellEventArgs e)
        {
            try
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

                    DataRow r = dtCustomer.AsEnumerable().FirstOrDefault(x => x.Field<string>("CustomerID") == CustomerID);

                    if (r != null)
                    {
                        txtCustomerID.Text = r["CustomerID"].ToString();
                        txtCustName.Text = r["CustName"].ToString();

                        cbbShopType.SelectedValue = Convert.ToInt32(r["ShopTypeID"]);

                        txtWHCode.Text = r["WHID"].ToString();
                        txtWHName.Text = r["WHName"].ToString();

                        txtCustomerRefCode.Text = r["CustomerRefCode"].ToString();
                        txtCustShortName.Text = r["CustShortName"].ToString();

                        txtLatitude.Clear();
                        if (!string.IsNullOrEmpty(r["Latitude"].ToString()))
                        {
                            txtLatitude.Text = r["Latitude"].ToString();
                        }

                        txtLongitude.Clear();
                        if (!string.IsNullOrEmpty(r["Longitude"].ToString()))
                        {
                            txtLongitude.Text = r["Longitude"].ToString();
                        }

                        chkRequestTaxBill.Checked = false;

                        if (!string.IsNullOrEmpty(r["FlagBill"].ToString()) && Convert.ToBoolean(r["FlagBill"]) == true)
                        {
                            chkRequestTaxBill.Checked = true;
                        }

                        chkNormal.Checked = true;

                        if (Convert.ToBoolean(r["FlagDel"]) == true) //ยกเลิก
                        {
                            chkNormal.Checked = false;
                        }

                        dtpCrDate.Text = r["CrDate"].ToString();
                        txtBillTo.Text = r["BillTo"].ToString();
                        txtShipTo.Text = r["ShipTo"].ToString();
                        txtContact.Text = r["Contact"].ToString();
                        txtTelephone.Text = r["Telephone"].ToString();
                        txtEmpID.Text = r["EmpID"].ToString();
                        txtFirstName.Text = r["FirstName"].ToString();
                        txtCustomerSAPCode.Text = r["CustomerSAPCode"].ToString();
                        txtCreditDay.Text = r["CreditDay"].ToString();

                        txtDistrictCode.Text = r["DistrictCode"].ToString();
                        txtDistrictName.Text = r["DistrictName"].ToString();

                        cbbTitle.Text = r["CustTitle"].ToString();
                        cbbSalArea.SelectedValue = r["SalAreaID"].ToString();
                        txtSeq.Text = r["Seq"].ToString();
                        txtCustomerRefCode.Text = r["CustomerRefCode"].ToString();

                        txtTaxId.Text = "";
                        string TaxID = r["TaxID"].ToString();
                        if (!string.IsNullOrEmpty(TaxID))
                        {
                            txtTaxId.Text = TaxID;
                        }

                        txtFax.Text = r["Fax"].ToString();

                        txtEmail.Text = r["Email"].ToString();
                        cbbPriceGroup.SelectedValue = Convert.ToInt32(r["PriceGroupID"]);

                        SAM.AddressNo = r["AddressNo"].ToString();
                        SAM.lane = r["lane"].ToString();
                        SAM.Street = r["Street"].ToString();
                        SAM.AreaID = Convert.ToInt32(r["AreaID"]);
                        SAM.ProvinceID = Convert.ToInt32(r["ProvinceID"]);

                        SAM.DistrictID = 0;

                        SAM.PostalCode = r["PostalCode"].ToString();

                        if (!string.IsNullOrEmpty(r["DistrictID"].ToString()))
                        {
                            SAM.DistrictID = Convert.ToInt32(r["DistrictID"]);
                        }

                        PrePareAddShelf(CustomerID);

                        picCustomerImg.Image = null; // 

                        string CustImage = r["CustImage"].ToString();

                        if (!string.IsNullOrEmpty(CustImage))
                        {
                            GetImageFromUrl(CustImage);
                        }


                        //if (!string.IsNullOrEmpty(r["CustImage"].ToString()))
                        //{
                        //    Byte[] data = new Byte[0];
                        //    data = (Byte[])r["CustImage"];
                        //    picCustomerImg.Image = data.byteArrayToImage();
                        //}


                        GetBranch(txtBranchCode_, txtBranchName_); //

                        txtCustomerRefCode.Text = txtBranchCode_.Text; //
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void GetImageFromUrl(string CustImage)
        {
            string URL = "http://ubn.dnsdojo.net:82/CU";
            //URL = "http://192.168.1.10/CU";
            //string URLCenter = "http://192.168.1.10/CU";
            var chkList = CustImage.ToCharArray().ToList();
            string CustImagePath = "";

            for (int i = 0; i < chkList.Count; i++)
            {
                if (chkList[i].ToString() != "~")
                {
                    CustImagePath += chkList[i].ToString();

                }
            }

            string Src = URL + CustImagePath;
            picCustomerImg.SizeMode = PictureBoxSizeMode.StretchImage;
            picCustomerImg.ImageLocation = Src;
            //picCustomerImg.Load(Src);

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

        private void SetFlagMemberOnGridView()
        {
            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                if (Convert.ToBoolean(grdList.Rows[i].Cells["colFlagMember"].Value) == true)
                {
                    grdList.Rows[i].Cells["colFlagMember"].Value = true;
                }
            }
        }

        private void SetButtonAfterBindGridView()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            chkNormal.Visible = false;

            if (grdList.Rows.Count > 0)
            {
                btnEdit.Enabled = true;
                btnCopy.Enabled = true;
                btnChangeSeq.Enabled = true;
            }
            if (rdoN.Checked == true)
            {
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
            }
            if (rdoN.Checked && grdList.Rows.Count > 0)
            {
                btnRemove.Enabled = true;
            }
            else if (rdoC.Checked == true)
            {
                btnAdd.Enabled = false;
            }
            else if (rdoC.Checked && grdList.Rows.Count > 0)
            {
                chkNormal.Visible = true;//
            }
        }

        private void BindData()
        {
            pnlEdit.ClearControl();
            pnlEdit.OpenControl(false, PanelEditControls.ToArray());

            int flagDel = rdoN.Checked ? 0 : 1;

            string WHID = ddlWH.SelectedIndex == 0 ? "" : ddlWH.SelectedValue.ToString();

            string SalAreaID = "";

            if (ddlSalArea.SelectedItem != null)
            {
                SalAreaID = ddlSalArea.SelectedValue.ToString() == "-1" ? "" : ddlSalArea.SelectedValue.ToString();
            }

            int shoptypeID = ddlShopType.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlShopType.SelectedValue);

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@FlagDel", flagDel);
            _params.Add("@WHID", WHID);
            _params.Add("@SalAreaID", SalAreaID);
            _params.Add("@ShopTypeID", shoptypeID);
            _params.Add("@Search", txtSearch.Text);

            dtCustomer = new DataTable();
            dtCustomer = bu.GetCustomerData(_params);

            grdList.DataSource = dtCustomer;

            lblQtyCount.Text = dtCustomer.Rows.Count.ToNumberFormat();

            SetFlagMemberOnGridView();

            SetButtonAfterBindGridView();
        }

        private void PrePareBranchWHToSalArea()
        {
            //if (ddlWH.SelectedIndex > 0)
            //{
            //    if (!string.IsNullOrEmpty(ddlWH.SelectedValue.ToString()))
            //    {
            //        var AllSalAreaID = bu.GetSaleAreaDistrict(x => x.WHID.Contains(ddlWH.SelectedValue.ToString()));

            //        var ListSalAreaID = AllSalAreaID.Select(x => x.SalAreaID).ToList();

            //        var allSalArea = new List<tbl_SalArea>();
            //        allSalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
            //        allSalArea.AddRange(bu.GetSaleArea(x => ListSalAreaID.Contains(x.SalAreaID)));
            //        ddlSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID");
            //    }
            //}
            //else
            //{
            //    var allSalArea = new List<tbl_SalArea>();
            //    allSalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
            //    ddlSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID", 0);
            //}

            if (txtWHCode.TextLength == 6 && !string.IsNullOrEmpty(txtWHCode.Text))
            {
                var BranchWH = bu.GetAllBranchWarehouse(x => x.WHID == txtWHCode.Text);

                if (BranchWH.Count > 0)
                {
                    txtWHName.Text = BranchWH[0].WHName;

                    var AllSalAreaID = bu.GetSaleAreaDistrict(x => x.WHID.Contains(txtWHCode.Text));

                    if (AllSalAreaID.Count > 0)
                    {
                        var ListSalAreaID = AllSalAreaID.Select(x => x.SalAreaID).Distinct().ToList();

                        var allSalArea = bu.GetSaleArea(x => ListSalAreaID.Contains(x.SalAreaID));
                        cbbSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID");
                    }

                    else
                    {
                        string WHID = "";

                        if (!string.IsNullOrEmpty(txtWHCode.Text))
                        {
                            WHID = txtWHCode.Text.Substring(3, 3);

                            var allSalArea = new List<tbl_SalArea>();
                            allSalArea = bu.GetSaleArea(x => x.SalAreaName.Contains(WHID));
                            cbbSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID");
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
            {
                txtCustomerSAPCode.DisableTextBox(false);
            }
            else
            {
                txtCustomerSAPCode.DisableTextBox(true);
            }

            txtBranchCode_.DisableTextBox(true);
            txtWHName.DisableTextBox(true);
            cbbPreOrder.Enabled = false;

            txtDistrictCode.DisableTextBox(true);
            txtDistrictName.DisableTextBox(true);

            txtCustomerRefCode.DisableTextBox(true);

            txtNoSequence.DisableTextBox(true);
            txtCustomerID.DisableTextBox(true);
            txtBillTo.DisableTextBox(true);

            txtEmpID.DisableTextBox(true);
            txtFirstName.DisableTextBox(true);
            txtTextBox3.DisableTextBox(true);
            txtTextBox4.DisableTextBox(true);
            txtTextBox5.DisableTextBox(true);

            btnSaleEmp.Enabled = false;
            btnDriverEmp.Enabled = false;

            cbbComboBox1.Enabled = false;
            cbbPromotion.Enabled = false;
        }

        private void PrePareKeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
            else
            {
                return;
            }
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

        private void AutoFormatID(TextBox txtCustID)
        {
            string ForMatCustomerID = txtBranchCode_.Text + txtDistrictCode.Text;

            var allCust = bu.GetCustomer(x => x.CustomerID.Contains(ForMatCustomerID));

            if (allCust.Count > 0)
            {
                string MaxID = allCust.Max(x => x.CustomerID);
                var no = Convert.ToInt32(MaxID.Substring(9, MaxID.Length - 9)) + 1;  //Subเพราะ ตัวเลขมากเกินตัวแปรรับได้
                txtCustID.Text = ForMatCustomerID + no.ToString("0000");
            }
            else
            {
                txtCustID.Text = ForMatCustomerID + "0001";
            }
        }

        private void PrePareSave(tbl_ArCustomer Customer)
        {
            Customer.SalAreaID = cbbSalArea.SelectedValue.ToString();
            Customer.ShopTypeID = Convert.ToInt32(cbbShopType.SelectedValue);
            Customer.CustTitle = cbbTitle.Text;
            Customer.PriceGroupID = Convert.ToInt32(cbbPriceGroup.SelectedValue);
            Customer.Seq = Convert.ToSByte(txtSeq.Text);

            Customer.AddressNo = SAM.AddressNo;
            Customer.lane = SAM.lane;
            Customer.Street = SAM.Street;
            Customer.AreaID = SAM.AreaID;
            Customer.ProvinceID = SAM.ProvinceID;
            Customer.PostalCode = SAM.PostalCode;
            Customer.DistrictID = SAM.DistrictID;

            Customer.FlagBill = chkRequestTaxBill.Checked ? true : false;
        }

        private void Save()
        {
            if (!ValidateSave())
            {
                return;
            }

            var branchWarehouse = bu.GetAllBranchWarehouse(x => x.WHName == txtWHName.Text);

            if (branchWarehouse.Count == 0)
            {
                string msg = "--> รหัส Van ไม่ถูกต้อง !!";
                msg.ShowWarningMessage();
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

                    Customer.FlagDel = chkNormal.Checked ? false : true;
                    Customer.FlagNew = false;
                    Customer.FlagEdit = true;

                    if (string.IsNullOrEmpty(Customer.CrUser))
                    {
                        Customer.CrUser = "";
                    }

                    Customer.EdUser = Helper.tbl_Users.Username;
                    Customer.EdDate = DateTime.Now;
                }
                else
                {
                    AutoFormatID(txtCustomerID);

                    pnlEdit.Controls.SetObjectFromControl(Customer);

                    Customer.CustomerCode = txtCustomerID.Text;

                    var custType = bu.GetAllCustomerType();

                    if (custType.Count > 0)
                    {
                        Customer.CustomerTypeID = custType[0].ArCustomerTypeID;
                    }

                    PrePareSave(Customer);

                    Customer.WHID = txtWHCode.Text;
                    Customer.CrDate = DateTime.Now;
                    Customer.CrUser = Helper.tbl_Users.Username;

                    Customer.EdDate = null;
                    Customer.EdUser = null;

                    Customer.GPSDate = null;
                    Customer.IsNewMember = null;

                    Customer.FlagNew = false;
                    Customer.FlagSend = false;
                    Customer.FlagMember = false;
                    Customer.FlagEdit = false;
                    Customer.FlagDel = false;

                    Customer.NetPoint = 0;
                    Customer.PromotionVanID = false;

                    Customer.CustomerImg = null;
                    Customer.CustImage = null;
                }

                ret = bu.UpdateData(Customer);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    pnlEdit.OpenControl(false, PanelEditControls.ToArray());

                    pnlSearch.OpenControl(true, PanelSearchControls.ToArray());
                    grdList.Enabled = true;

                    btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                    btnAdd.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;

                    btnSearch.PerformClick();
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

        #endregion

        #region #--EventPage

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }

        private void ddlWH_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PrePareWareHouseData(); --Old
            //Edit 29/7/2564
            if (ddlWH.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(ddlWH.SelectedValue.ToString()))
                {
                    var AllSalAreaID = bu.GetSaleAreaDistrict(x => x.WHID.Contains(ddlWH.SelectedValue.ToString()));

                    if (AllSalAreaID.Count > 0)
                    {
                        var ListSalAreaID = AllSalAreaID.Select(x => x.SalAreaID).Distinct().ToList();

                        var allSalArea = new List<tbl_SalArea>();
                        allSalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
                        allSalArea.AddRange(bu.GetSaleArea(x => ListSalAreaID.Contains(x.SalAreaID)));
                        ddlSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID");
                    }
                    else
                    {
                        string WHID = "";

                        if (!string.IsNullOrEmpty(ddlWH.SelectedValue.ToString()))
                        {
                            WHID = ddlWH.SelectedValue.ToString().Substring(3, 3);

                            var allSalArea = new List<tbl_SalArea>();
                            allSalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
                            allSalArea.AddRange(bu.GetSaleArea(x => x.SalAreaName.Contains(WHID)));
                            ddlSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID");
                        }
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

        private void txtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PrePareBranchWHToSalArea();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            dtCustomer = new DataTable();
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectDetails(e);
        }

        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            SelectDetails(null);
        }

        private void txtWHCode_TextChanged(object sender, EventArgs e)
        {
            PrePareBranchWHToSalArea();
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
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlEdit.ClearControl();
            pnlEdit.OpenControl(false, PanelEditControls.ToArray());

            pnlSearch.OpenControl(true, PanelSearchControls.ToArray());
            grdList.Enabled = true;
            txtSearch.Focus();

            if (grdList.Rows.Count > 0)
            {
                grdList[0, 0].Selected = true;
            }

            SetButtonAfterBindGridView();
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
        }

        private void btnDepo_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchWHControls, "เลือกสาขา/ซุ้ม");
            txtCustomerRefCode.Text = txtBranchCode_.Text;
        }

        private void btnSearchVan_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchBranchWHControlsEdit, "เลือกคลังสินค้า", (x => x.VanType != 0));
        }

        private void btnSearchShopType_Click(object sender, EventArgs e)
        {
            frmShopType frm = new frmShopType(); //ประเภทร้านค้า
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(shoptypeID))
            {
                cbbShopType.SelectedValue = Convert.ToInt32(shoptypeID);
            }
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

            txtWHCode.Focus();

            pnlSearch.OpenControl(false, PanelSearchControls.ToArray());
            grdList.Enabled = false;

            pnlEdit.OpenControl(true, PanelEditControls.ToArray());

            PanelEditControlsNotUse();

            btnAddShelf.Enabled = false;
            btnRemoveShelf.Enabled = false;
        }

        private void txtSeq_KeyPress(object sender, KeyPressEventArgs e)
        {
            PrePareKeyPress(e);
        }

        private void txtCreditDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            PrePareKeyPress(e);
        }

        private void txtTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            PrePareKeyPress(e);
        }

        private void txtLatitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            PrePareKeyPress(e);
        }

        private void txtLongitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            PrePareKeyPress(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
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
            string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการ Shelf นี้?";
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
                    return;
                }
            }
            else
            {
                string msg = "กรุณาเลือกเลข Shelf ที่ต้องการลบ !!";
                msg.ShowWarningMessage();
                return;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
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
                return;
            }
        }

        private void rdoN_CheckedChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void rdoC_CheckedChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindData();
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

        private void frmCustomerInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        #endregion

        //private void PrePareWareHouseData()
        //{
        //    if (ddlWH.SelectedIndex > 0)
        //    {
        //        string WHID = "";

        //        if (!string.IsNullOrEmpty(ddlWH.SelectedValue.ToString()))
        //        {
        //            WHID = ddlWH.SelectedValue.ToString().Substring(3, 3);

        //            var allSalArea = new List<tbl_SalArea>();
        //            allSalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
        //            allSalArea.AddRange(bu.GetSaleArea(x => x.SalAreaName.Contains(WHID)));
        //            ddlSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID");
        //        }
        //    }
        //    else
        //    {
        //        var allSalArea = new List<tbl_SalArea>();
        //        allSalArea.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
        //        ddlSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID", 0);
        //    }
        //}

    }
}
