using AllCashUFormsApp.Controller;//
using AllCashUFormsApp.Model;//
using AllCashUFormsApp.View.UControl;//
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;//
using System.Linq;
using System.Windows.Forms;
using AllCashUFormsApp.View.UControl.A_UC;//
namespace AllCashUFormsApp.View.Page
{
    public partial class frmCustomerInfo : Form
    {
        MenuBU menuBU = new MenuBU();

        Customer bu = new Customer();
        CustomerShelf buShelf = new CustomerShelf();
        List<Control> searchBranchControls = new List<Control>();
        List<Control> searchBranchWHControls = new List<Control>();
        List<Control> BranchWareHouseIDControls = new List<Control>();
        List<Control> searchEmpControls = new List<Control>();//
        List<Control> subDControls = new List<Control>();

        List<string> readOnlyControls = new List<string>();
        static DataTable allCustomer = new DataTable();

        Dictionary<Control, Label> validateCustCtrls = new Dictionary<Control, Label>(); // Validate Save
        List<string> readOnlyPnlGridControls = new List<string>();
        List<string> readOnlyPnlEditControls = new List<string>();
        public static SearchAddressModel SAM = new SearchAddressModel(); // หน้าค้นหาที่อยู่
        public static string page = "";// Shelf
        public static string shelfID = "";//Shelf

        public frmCustomerInfo()
        {
            InitializeComponent();
            searchBranchControls = new List<Control>() { txtBranchCode, txtBranchName };
            searchBranchWHControls = new List<Control>() { txtBranchCode_, txtBranchName_ };
            BranchWareHouseIDControls = new List<Control>() { txtWHCode, txtWHName };
            searchEmpControls = new List<Control>() { txtEmpID, txtFirstName };//
            readOnlyControls = new string[] { txtBranchCode.Name }.ToList();
            subDControls = new List<Control>() { txtDistrictCode, txtDistrictName, txtDistrictID };

            readOnlyPnlEditControls = new string[] { txtCustomerID.Name }.ToList(); //เปิด ปิด Panel Edit
            readOnlyPnlGridControls = new string[] { txtSearch.Text }.ToList(); //เปิด ปิด Panel GridView

            validateCustCtrls.Add(txtWHID, lbl_WH);//
            validateCustCtrls.Add(_ddlSalArea, lbl_SalArea);//
            validateCustCtrls.Add(txtDistrictID, lblDistrict);//
            validateCustCtrls.Add(txtCustName, lblC_Name);//
            validateCustCtrls.Add(txtBillTo, lbl_Bill);//
        }

        private void InitPage()
        {
            //EnableData();

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

            txtCreditDay.TextAlign = HorizontalAlignment.Right;
            //dtpCrDate.Format = DateTimePickerFormat.Custom;
            //dtpCrDate.CustomFormat = "dd/MM/yyyy";
            dtpCrDate.SetDateTimePickerFormat();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnCopy.Enabled = false;
            btnExcel.Enabled = false;
            btnPrint.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void InitialData()
        {
            var b = bu.GetBranch();
            if (b != null)
            {
                this.BindData("FromBranchID", searchBranchControls, b[0].BranchID);
                this.BindData("FromBranchID", searchBranchWHControls, b[0].BranchID);
            }

            var allVanList = new List<tbl_BranchWarehouse>();
            allVanList.Add(new tbl_BranchWarehouse { WHID = "", WHCode = "==เลือก==" });
            allVanList.AddRange(bu.GetAllVan().Where(x => x.WHType == 1).OrderBy(x => x.WHSeq).ToList());
            ddlVan.BindDropdownList(allVanList, "WHCode", "WHID"); //

            if (ddlVan.SelectedIndex == 0)
            {
                BindDDLSalArea(ddlSalArea);
            }

            var shopTypeList = new List<tbl_ShopType>();
            var _shopTypeList = bu.GetAllShopType();
            shopTypeList.Add(new tbl_ShopType { ShopTypeID = -1, ShopTypeName = "==เลือก==" });
            shopTypeList.AddRange(_shopTypeList);
            ddlShopType.BindDropdownList(shopTypeList, "ShopTypeName", "ShopTypeID");


            List<tbl_ShopType> shopTypeList2 = new List<tbl_ShopType>();
            shopTypeList2 = bu.GetShopType();
            _ddlShopType.BindDropdownList(shopTypeList2, "ShopTypeName", "ShopTypeID", 0);

            var pricegroupList = new List<tbl_PriceGroup>();
            pricegroupList.AddRange(bu.GetAllPriceGroup());
            ddlPriceGroup.BindDropdownList(pricegroupList, "PriceGroupName", "PriceGroupID", 0);

            ddlTitle.Items.Add("บริษัท");
            ddlTitle.Items.Add("หจก.");
            ddlTitle.Items.Add("บมจ.");
            ddlTitle.Items.Add("คุณ");
            ddlTitle.SelectedIndex = 0;

            gridCust.AutoGenerateColumns = false;
            txtBranchName.DisableTextBox(true);
            cbPresale.Enabled = false;
            PanelEdit(false);

        }

        private void PanelGrid(bool flag)
        {
            pnlGridView.OpenControl(flag, readOnlyPnlGridControls.ToArray());
            foreach (Control item in pnlGridView.Controls)
            {
                if (item is Label || item is Panel || item is PictureBox || item is Button || item is GroupBox || item is CheckBox || item is ComboBox || item is ListBox)
                {

                }
                else
                {
                    item.BackColor = ColorTranslator.FromHtml("#DCDCDC");
                }
            }
            cbPresale.Enabled = false; //ไม่ได้ใช้
            txtBranchName.DisableTextBox(true);
            lblCustCount.Text = gridCust.Rows.Count.ToNumberFormat();
        }

        private void PanelEdit(bool flag)
        {
            dtpCrDate.Value = DateTime.Now;
            pnlEdit.OpenControl(flag, readOnlyPnlEditControls.ToArray());
            foreach (Control item in pnlEdit.Controls)
            {
                if (flag == false) // สั่งปิด   //ใส่สีเทาใน Control Panel
                {
                    if (item is Label || item is Panel || item is PictureBox || item is Button || item is GroupBox || item is CheckBox || item is ComboBox || item is ListBox)
                    {

                    }
                    else
                    {
                        item.BackColor = ColorTranslator.FromHtml("#DCDCDC");
                    }
                }
            }
            dtpCrDate.Value = DateTime.Now;
            cbbPreOrder_.Enabled = false;
            chkRequestTaxBill.Checked = false; //ขอใบกำกับภาษี
            cbbBillAddress_.Enabled = flag;
            chkRequestTaxBill.Enabled = flag;
            if (flag == false) // ปิดแก้ไข
            {
                cbbBillAddress_.Checked = false;
                cbbPreOrder_.Enabled = false;
                chkRequestTaxBill.Checked = false;
                chkRequestTaxBill.Enabled = false;
            }
        }

        private void DisableControl()
        {
            txtCustomerID.DisableTextBox(true);
            txtBranchCode_.DisableTextBox(true);
            txtBranchName_.DisableTextBox(true);
            txtWHName.DisableTextBox(true);
            txtDistrictCode.DisableTextBox(true);
            txtDistrictName.DisableTextBox(true);
            txtNoNameSeq_.DisableTextBox(true);
            txtBillTo.DisableTextBox(true);
            txtEmpID.DisableTextBox(true);
            txtFirstName.DisableTextBox(true);
            txtEmp_.DisableTextBox(true);
            txtEmpName_.DisableTextBox(true);

            btnSearchSalArea.Enabled = false;// ไม่ได้ใช้
            btnSearchDistrict_.Enabled = false;// ไม่ได้ใช้
            btnDriverEmp_.Enabled = false;// ไม่ได้ใช้

            TextBox_Discount.DisableTextBox(true);// ไม่ได้ใช้
            DropDownDisCount.Enabled = false;//ไม่ได้ใช้
            ddlPromotion.Enabled = false; //ไม่ได้ใช้
            cbbPreOrder_.Enabled = false; //ไม่ได้ใช้

            //Listbox_shef.Enabled = false;//ไม่ได้ใช้
            //btnAddShelf.Enabled = false;//ไม่ได้ใช้
            //btnRemoveShelf.Enabled = false;//ไม่ได้ใช้
        }

        private void BindDDLSalArea(ComboBox ddl)
        {
            var salesAreaList = new List<tbl_SalArea>();
            salesAreaList.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
            ddl.BindDropdownList(salesAreaList, "SalAreaName", "SalAreaID", 0);
        }

        private void BindCustomerData()
        {
            int flagDel = rdoN.Checked ? 0 : 1;

            string whid = ddlVan.SelectedIndex == 0 ? "" : ddlVan.SelectedValue.ToString();

            string salareID = "";
            string searchtext = "";
            if (txtSearch.Text != "")
            {
                searchtext = txtSearch.Text;
            }

            if (ddlSalArea.SelectedItem != null)
            {
                salareID = ddlSalArea.SelectedValue.ToString() == "" ? "" : ddlSalArea.SelectedValue.ToString();
            }

            int shoptypeID = ddlShopType.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlShopType.SelectedValue);

            allCustomer = bu.GetCustomerGridData(whid, salareID, shoptypeID, flagDel, searchtext); //CellClick


            gridCust.DataSource = allCustomer;
            lblCustCount.Text = gridCust.Rows.Count.ToNumberFormat();
        }

        private void ddlVan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVan.SelectedIndex != 0)
            {
                //edit by sailom 20022021
                //List<tbl_SalAreaDistrict> SalAreaDistrictList2 = bu.GetSaleAreaDistrict(x => x.WHID == ddlVan.SelectedValue.ToString());

                //List<string> listSalAreaID = SalAreaDistrictList2.Select(x => x.SalAreaID).ToList();

                //var SalAreaListAll = bu.GetSaleArea(x => listSalAreaID.Contains(x.SalAreaID));
                string vanId = "";

                if (!string.IsNullOrEmpty(ddlVan.SelectedValue.ToString()))
                {
                    vanId = ddlVan.SelectedValue.ToString().Substring(3, 3);
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtWHCode.Text) && txtWHCode.Text.Length == 6)
                        vanId = txtWHCode.Text.Substring(3, 3);
                }

                if (!string.IsNullOrEmpty(vanId))
                {
                    var SalAreaListAll = bu.GetSaleArea(x => x.SalAreaName.Contains(vanId));

                    var SalAreaList = new List<tbl_SalArea>();
                    SalAreaList.Add(new tbl_SalArea { SalAreaID = "", SalAreaName = "==เลือก==" });
                    SalAreaList.AddRange(SalAreaListAll);
                    ddlSalArea.BindDropdownList(SalAreaList, "SalAreaName", "SalAreaID");
                }
            }
            else
            {
                BindDDLSalArea(ddlSalArea);
            }
        }

        private void SelectCustomerDetails(DataGridViewCellEventArgs e)
        {
            var b = bu.GetBranch();
            if (b != null)
            {
                this.BindData("FromBranchID", searchBranchWHControls, b[0].BranchID);
            }
            btnAdd.Enabled = false;
            btnCancel.Enabled = true;
            btnEdit.Enabled = true;
            btnRemove.Enabled = true;

            ddlTitle.Enabled = true;
            //chkRequestTaxBill.Enabled = true;
            cbbBillAddress_.Enabled = false;

            try
            {
                if (e != null)
                {
                    if (e.RowIndex == -1)
                        return;
                }

                DataGridViewRow gridrow = null;
                if (e != null)
                    gridrow = gridCust.Rows[e.RowIndex];
                else
                    gridrow = gridCust.CurrentRow;

                string custID = gridrow.Cells["colCustomerID"].Value.ToString();

                foreach (DataRow r in allCustomer.Rows)
                {
                    if (r["CustomerID"].ToString() == custID)
                    {
                        txtCustomerID.Text = r["CustomerID"].ToString();
                        txtCustName.Text = r["CustName"].ToString();
                        _ddlShopType.SelectedValue = Convert.ToInt32(r["ShopTypeID"].ToString());
                        txtWHCode.Text = r["WHID"].ToString();
                        txtWHID.Text = r["WHID"].ToString();
                        txtWHName.Text = r["WHName"].ToString();
                        txtCustomerRefCode.Text = r["CustomerRefCode"].ToString();
                        txtCustShortName.Text = r["CustShortName"].ToString();
                        if (r["Latitude"].ToString() != "")
                        {
                            txtLatitude.Text = r["Latitude"].ToString();
                        }
                        else if (r["Latitude"].ToString() == "")
                        {
                            txtLatitude.Text = "";
                        }
                        if (r["Longitude"].ToString() != "")
                        {
                            txtLongitude.Text = r["Longitude"].ToString();
                        }
                        else if (r["Longitude"].ToString() == "")
                        {
                            txtLongitude.Text = "";
                        }
                        if (!string.IsNullOrEmpty(r["FlagBill"].ToString()) && Convert.ToBoolean(r["FlagBill"]) == true)
                        {
                            chkRequestTaxBill.Checked = true;
                        }
                        else if (!string.IsNullOrEmpty(r["FlagBill"].ToString()) && Convert.ToBoolean(r["FlagBill"]) == false)
                        {
                            chkRequestTaxBill.Checked = false;
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
                        txtDistrictID.Text = r["DistrictID"].ToString();
                        txtDistrictName.Text = r["DistrictName"].ToString();
                        ddlTitle.Text = r["CustTitle"].ToString();
                        _ddlSalArea.SelectedValue = r["SalAreaID"].ToString();
                        txtSeq.Text = r["Seq"].ToString();
                        txtCustomerRefCode.Text = r["CustomerRefCode"].ToString();

                        txtTaxId.Text = r["TaxID"].ToString();
                        txtFax.Text = r["Fax"].ToString();

                        txtEmail.Text = r["Email"].ToString();
                        ddlPriceGroup.SelectedValue = Convert.ToInt32(r["PriceGroupID"]);

                        SAM.AddressNo = r["AddressNo"].ToString();
                        SAM.lane = r["lane"].ToString();
                        SAM.Street = r["Street"].ToString();
                        SAM.AreaID = Convert.ToInt32(r["AreaID"]);
                        SAM.ProvinceID = Convert.ToInt32(r["ProvinceID"]);
                        SAM.PostalCode = r["PostalCode"].ToString();
                        SAM.DistrictID = Convert.ToInt32(r["DistrictID"].ToString());

                        Listbox_shef.Items.Clear();


                        List<tbl_ArCustomerShelf> custShelf_List = new List<tbl_ArCustomerShelf>();
                        custShelf_List = buShelf.GetCustomerShelf(x => x.CustomerID == txtCustomerID.Text && x.FlagDel == false);
                        if (custShelf_List.Count > 0)
                        {
                            foreach (var item in custShelf_List)
                            {
                                string ShelfID = item.ShelfID.ToString();
                                Listbox_shef.Items.Add(ShelfID);
                            }
                            Listbox_shef.SelectedIndex = 0;
                        }
                        //DropDownDisCount.SelectedValue = r["DiscountTypeCode"].ToString();
                        //decimal dis2 = Convert.ToDecimal(r["Discount"].ToString());
                        //string _number = string.Format("{0:#,0}", dis2);
                        //TextBox_Discount.Text = _number;


                        Byte[] data = new Byte[0];
                        data = (Byte[])r["CustomerImg"];
                        MemoryStream mem = new MemoryStream(data);
                        picCustomerImg.Image = Image.FromStream(mem);

                    }
                }
            }
            catch (Exception ex)
            {
                picCustomerImg.Image = null;
                return;
            }

        }

        private void gridCust_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectCustomerDetails(e);
        }

        private void cbbBillAddress__CheckedChanged(object sender, EventArgs e)
        {
            if (cbbBillAddress_.Checked)
            {
                txtShipTo.Text = txtBillTo.Text;
            }
            else
            {
                txtShipTo.Text = null;
            }
        }

        private void btnSearchPic__Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Title = "Open Image";
                openFileDialog1.Filter = "bitmap files (*.bit)|*.bit|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Image image = Image.FromFile(openFileDialog1.FileName);
                    picCustomerImg.Image = image.ImageToByte().byteArrayToImage(228, 228);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void btnSearchAddress__Click(object sender, EventArgs e)
        {
            frmSearchAddress frm = new frmSearchAddress();
            frm.ShowDialog();
            if (SAM.billTo != "" && SAM.DistrictID != 0)
            {
                txtBillTo.Text = SAM.billTo;
                txtDistrictID.Text = SAM.DistrictID.ToString();
                txtDistrictCode.Text = SAM.DistrictCode;
                txtDistrictName.Text = SAM.districtName;
            }
            else
            {
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            SAM = new SearchAddressModel();
            this.Close();
        }

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //true
            {
                errList.SetErrMessage(validateCustCtrls);
            }
            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }
            return ret;
        }

        private string CustID = "";

        private void FormatIDCust()//
        {
            string formatCustID = txtBranchCode_.Text + txtDistrictCode.Text;
            var allCust = bu.GetCustomer(x => x.CustomerID.Contains(formatCustID));
            if (allCust != null && allCust.Count > 0) //เจอเลขที่ตรงกัน
            {
                string MaxID = "";
                MaxID = allCust.Max(x => x.CustomerID);
                var no = Convert.ToInt32(MaxID.Substring(9, MaxID.Length - 9)) + 1;  //Subเพราะ ตัวเลขมากเกินตัวแปรรับได้
                CustID = formatCustID + no.ToString("0000");
                txtCustomerID.Text = CustID;
            }
            else //ไม่เจอ
            {
                string maxCustID = formatCustID + "001";
                CustID = maxCustID;
                txtCustomerID.Text = maxCustID;
            }
        }

        private void Save()
        {
            //BindSalArea(); //15022021

            var BranchWareHouseList = bu.GetAllBranchWarehouse(x => x.WHName == txtWHName.Text);
            if (BranchWareHouseList.Count == 0)
            {
                MessageBox.Show("--> รหัส Van ไม่ถูกต้อง", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!ValidateSave())
            {
                return;
            }
            try
            {
                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;
                bu = new Customer();
                tbl_ArCustomer cData = new tbl_ArCustomer();
                bool isEditMode = cData.CheckExistsData(txtCustomerID.Text);
                if (isEditMode)
                {   //แก้ไข
                    List<tbl_ArCustomer> custList = bu.GetCustomerInfo(x => x.CustomerID == txtCustomerID.Text);
                    cData = custList[0];

                    pnlEdit.Controls.SetObjectFromControl(cData);
                    txtWHCode.Text = BranchWareHouseList[0].WHID;
                    cData.WHID = txtWHCode.Text;
                    cData.SalAreaID = _ddlSalArea.SelectedValue.ToString();
                    cData.ShopTypeID = Convert.ToInt32(_ddlShopType.SelectedValue);
                    cData.CustTitle = ddlTitle.Text;
                    cData.PriceGroupID = Convert.ToInt32(ddlPriceGroup.SelectedValue);
                    cData.Seq = Convert.ToSByte(txtSeq.Text);
                    cData.AddressNo = SAM.AddressNo;
                    cData.lane = SAM.lane;
                    cData.Street = SAM.Street;
                    cData.AreaID = SAM.AreaID;
                    cData.ProvinceID = SAM.ProvinceID;
                    cData.PostalCode = SAM.PostalCode;

                    cData.CustomerImg = picCustomerImg.Image.ImageToByte();

                    if (rdoN.Checked == true)
                    {
                        cData.FlagDel = false;
                    }
                    else if (rdoC.Checked == true)
                    {
                        cData.FlagDel = true;
                    }
                    if (chkRequestTaxBill.Checked == true)
                    {
                        cData.FlagBill = true;
                    }
                    else
                    {
                        cData.FlagBill = false;
                    }

                    cData.FlagNew = false;
                    cData.FlagSend = false;
                    cData.FlagMember = false;
                    cData.FlagEdit = true;
                    cData.NetPoint = 0;
                    cData.PromotionVanID = false;

                    cData.CrUser = "";
                    //cData.CrDate = "";
                    cData.EdUser = Helper.tbl_Users.Username;
                    cData.EdDate = DateTime.Now;

                    ret = bu.UpdateData(cData);
                }
                else //เพิ่มใหม่
                {
                    FormatIDCust();
                    pnlEdit.Controls.SetObjectFromControl(cData);
                    txtWHCode.Text = BranchWareHouseList[0].WHID;
                    cData.WHID = txtWHCode.Text;
                    cData.CustomerCode = CustID;
                    cData.SalAreaID = _ddlSalArea.SelectedValue.ToString();
                    cData.ShopTypeID = Convert.ToInt32(_ddlShopType.SelectedValue);
                    cData.CustTitle = ddlTitle.Text;
                    cData.PriceGroupID = Convert.ToInt32(ddlPriceGroup.SelectedValue);
                    cData.Seq = Convert.ToSByte(txtSeq.Text);
                    List<tbl_ArCustomerType> custType = bu.GetAllCustomerType();
                    cData.CustomerTypeID = custType[0].ArCustomerTypeID;

                    cData.AddressNo = SAM.AddressNo;
                    cData.lane = SAM.lane;
                    cData.Street = SAM.Street;
                    cData.AreaID = SAM.AreaID;
                    cData.ProvinceID = SAM.ProvinceID;
                    cData.PostalCode = SAM.PostalCode;

                    cData.CustomerImg = picCustomerImg.Image.ImageToByte();

                    if (rdoN.Checked == true)
                    {
                        cData.FlagDel = false;
                    }
                    else if (rdoC.Checked == true)
                    {
                        cData.FlagDel = true;
                    }
                    if (chkRequestTaxBill.Checked == true)
                    {
                        cData.FlagBill = true;
                    }
                    else if (chkRequestTaxBill.Checked == false)
                    {
                        cData.FlagBill = false;
                    }
                    cData.VatType = true;
                    cData.VatRate = Convert.ToDecimal(7.000);

                    cData.CrUser = Helper.tbl_Users.Username;
                    cData.CrDate = DateTime.Now;

                    cData.EdDate = null;
                    cData.EdUser = null;

                    cData.FlagNew = true;
                    cData.FlagSend = false;
                    cData.FlagMember = false;
                    cData.FlagEdit = false;
                    cData.NetPoint = 0;
                    cData.PromotionVanID = false;

                    ret = bu.UpdateData(cData);
                }

                if (ret == 1)
                {

                    SAM = new SearchAddressModel();

                    pnlEdit.ClearControl();

                    btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                    btnAdd.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;

                    picCustomerImg.Image = null;
                    _ddlSalArea.DataSource = null;

                    PanelEdit(false);
                    BindCustomerData();
                    PanelGrid(true);
                    gridCust.Enabled = true;
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

        private int AutoID_SHELF()
        {
            int autoID = 0;

            List<tbl_ArCustomerShelf> tbl_ArCustomerShelfList = new List<tbl_ArCustomerShelf>();
            tbl_ArCustomerShelfList = buShelf.GetCustShelf();
            if (tbl_ArCustomerShelfList.Count > 0)
            {
                autoID = tbl_ArCustomerShelfList.Max(x => x.AutoID) + 1;
            }
            return autoID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var b = bu.GetBranch();
            if (b != null)
            {
                this.BindData("FromBranchID", searchBranchWHControls, b[0].BranchID);
            }
            btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            PanelGrid(false);
            gridCust.Enabled = true;
            PanelEdit(true);

            DisableControl();
            txtWHCode.Focus();
        }

        public static string shoptypeID = "";

        private void btnSearchShopType__Click(object sender, EventArgs e)
        {
            _ddlShopType.Enabled = true;
            frmSearchShopType frm = new frmSearchShopType();
            frm.ShowDialog();
            if (shoptypeID != "")
            {
                int b = Convert.ToInt32(shoptypeID);
                _ddlShopType.SelectedValue = b;
            }
            else
            {
                return;
            }
        }

        public static string salAreaID = "", _WHID = "";

        private void btnSearchSalArea_Click(object sender, EventArgs e)
        {
            //_WHID = txtWHCode.Text; // ส่ง _WHID ไปค้นหาใน SalAreaDistrict
            //if(_WHID != "")
            //{
            //    frmSearchSalAreaDistrict f = new frmSearchSalAreaDistrict();
            //    f.ShowDialog();
            //    if (salAreaID != "")
            //    {
            //        _ddlSalArea.SelectedValue = salAreaID;
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}

        }

        private void SetDefaultData()
        {
            var b = bu.GetBranch();
            if (b != null)
            {
                this.BindData("FromBranchID", searchBranchWHControls, b[0].BranchID);
            }

            txtNoNameSeq_.Text = "000";
            txtCustomerRefCode.Text = txtBranchCode.Text;
            txtCustomerID.Text = "Auto";
            txtLatitude.Text = "0";
            txtLongitude.Text = "0";
            txtCreditDay.Text = "0";
            TextBox_Discount.Text = "0";
            lblCustCount.Text = "0";

            txtCustomerRefCode.Text = txtBranchCode_.Text;
            ddlTitle.SelectedIndex = 3;
            gridCust.Enabled = false;
            _ddlSalArea.DataSource = null;
            _ddlShopType.SelectedIndex = 0;
            txtSeq.Text = "0";
            ddlPriceGroup.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SAM = new SearchAddressModel();
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, ""); //ชื่อ button ตามด้วย .Enable
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            PanelGrid(false);
            PanelEdit(true);
            pnlEdit.ClearControl();

            picCustomerImg.Image = null;

            DisableControl();
            SetDefaultData();
            txtWHCode.Focus();

            btnAddShelf.Enabled = false;
            btnRemoveShelf.Enabled = false;

        }

        private void Cancel()
        {
            pnlEdit.ClearControl();
            Listbox_shef.Items.Clear();
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            picCustomerImg.Image = null;
            _ddlSalArea.DataSource = null;

            PanelEdit(false);

            SAM = new SearchAddressModel();
            _ddlSalArea.DataSource = null;
            SetDefaultData();
            PanelGrid(true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
            txtSearch.DisableTextBox(false);
            txtBranchCode.DisableTextBox(false);
            gridCust.Enabled = true;
            txtSearch.Focus();
        }

        private void txtSearchCust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindCustomerData();
            }
            else
            {
                return;
            }
        }

        private void txtVanID__KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Bind2SalArea(TextBox txt, ComboBox ddl)
        {
            //edit by sailom 20022021
            //List<tbl_SalAreaDistrict> salesAreaDistrictList = bu.GetSaleAreaDistrict(x => x.WHID == txt.Text);
            //if (salesAreaDistrictList.Count != 0)
            {
                //List<string> listSalAreaID = salesAreaDistrictList.Select(x => x.SalAreaID).ToList();
                if (!string.IsNullOrEmpty(txtWHCode.Text) && txtWHCode.Text.Length == 6)
                {
                    string vanId = txtWHCode.Text.Substring(3, 3);
                    var salesAreaList = bu.GetSaleArea(x => x.SalAreaName.Contains(vanId)); //listSalAreaID.Contains(x.SalAreaID));
                    ddl.BindDropdownList(salesAreaList, "SalAreaName", "SalAreaID", 0);
                }
            }
        }

        //private void BindSalArea()
        //{
        //    var WH2 = bu.GetAllBranchWarehouse(x => x.WHID == txtWHCode.Text);
        //    if (WH2.Count != 0)
        //    {
        //        txtWHName.Text = WH2[0].WHName;
        //        Bind2SalArea(txtWHCode, _ddlSalArea);
        //    }
        //    else
        //    {
        //        MessageBox.Show("รหัสคลัง Van ไม่ถูกต้อง", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        txtWHCode.Clear();
        //        txtWHName.Clear();
        //        return;
        //    }

        //}

        private void txtVanID__KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnDriverEmp__Click(object sender, EventArgs e)
        {
        }

        private void btnSearchSaleEmp__Click(object sender, EventArgs e)
        {
            this.OpenEmployeeNamePopup(searchEmpControls, "เลือกพนักงาน", (x => x.PositionID == 4));
        }

        private void key_press(KeyPressEventArgs e)
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

        private void txtDayCredit__KeyPress(object sender, KeyPressEventArgs e)
        {
            key_press(e);
        }

        private void btnAddShelf__Click(object sender, EventArgs e)
        {
            if (txtCustomerID.Text != "" && txtCustomerID.Text != "Auto")
            {
                int ret = 0;
                page = "add";
                frmShelf f = new frmShelf();
                f.ShowDialog();
                if (shelfID != "")
                {
                    List<tbl_ArCustomerShelf> Shelf_List = new List<tbl_ArCustomerShelf>();

                    tbl_ArCustomerShelf custShelf = new tbl_ArCustomerShelf();
                    Listbox_shef.Items.Add(shelfID);
                    foreach (var item in Listbox_shef.Items)
                    {
                        Shelf_List = buShelf.GetCustomerShelf(x => x.CustomerID == txtCustomerID.Text && x.FlagDel == false && x.ShelfID == item.ToString());
                        if (Shelf_List.Count > 0) // ถ้าเจอรหัส ไม่ต้องทำอะไร
                        {

                        }
                        else
                        {
                            custShelf = new tbl_ArCustomerShelf();
                            custShelf.AutoID = AutoID_SHELF();
                            custShelf.CustomerID = txtCustomerID.Text;
                            custShelf.ProductID = bu.GetProduct(x => x.ProductShortName.Contains("ชั้นวาง")).First().ProductID;
                            custShelf.ShelfID = item.ToString();
                            custShelf.WHID = txtWHCode.Text;
                            custShelf.FlagNew = true;
                            custShelf.FlagEdit = false;

                            custShelf.CrDate = DateTime.Now;
                            custShelf.CrUser = Helper.tbl_Users.Username;
                            custShelf.FlagDel = false;
                            custShelf.FlagSend = false;
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
            else
            {
                string msg = "ไม่พบข้อมูลร้านค้า!!";
                msg.ShowWarningMessage();
            }
        }

        private void Delete_Shelf()
        {
            string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
            string title = "ทำการยืนยัน!!";

            if (!cfMsg.ConfirmMessageBox(title))
                return;

            int ret = 0;
            List<tbl_ArCustomerShelf> Shelf_List = new List<tbl_ArCustomerShelf>();
            string shelf_id = Listbox_shef.SelectedItem.ToString();
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
                Listbox_shef.Items.RemoveAt(Listbox_shef.Items.IndexOf(Listbox_shef.SelectedItem));
            }
            else
            {
                this.ShowProcessErr();
                return;
            }
        }

        private void btnDeleteShelf__Click(object sender, EventArgs e)
        {

        }

        public static string pricegroupID = "";
        private void btnSearchPriceGroup_Click(object sender, EventArgs e)
        {
            ddlPriceGroup.Enabled = true;

            frmSearchPriceGroup frmpriceGroup = new frmSearchPriceGroup();
            frmpriceGroup.ShowDialog();
            if (pricegroupID != "")
            {
                int price = Convert.ToInt32(pricegroupID);
                ddlPriceGroup.SelectedValue = price;
            }
            else
            {
                return;
            }
        }

        private void ChangeSequence()
        {
            try
            {
                bu = new Customer();

                var allCust = bu.GetCustomer(x => x.FlagDel == false);

                int ret = 0;
                List<tbl_ArCustomer> tbl_ArCustomerList = new List<tbl_ArCustomer>();
                for (int i = 0; i < gridCust.RowCount; i++)
                {
                    int RowIndex = i;
                    string _custID = gridCust.Rows[i].Cells[0].Value.ToString();
                    string _Seq = gridCust.Rows[i].Cells["colSeq"].Value.ToString();
                    List<tbl_ArCustomer> custList = allCust.Where(x => x.CustomerID == _custID).ToList();
                    tbl_ArCustomer cData = new tbl_ArCustomer();
                    cData = custList[0];
                    cData.EdDate = DateTime.Now;
                    cData.EdUser = Helper.tbl_Users.Username;
                    cData.Seq = Convert.ToSByte(_Seq);
                    tbl_ArCustomerList.Add(cData);
                }
                foreach (var cData in tbl_ArCustomerList)
                {
                    ret = bu.UpdateData(cData);
                }
                BindCustomerData();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void btnChangeSeq_Click(object sender, EventArgs e)
        {
            if (gridCust.Rows.Count != 0)
            {
                ChangeSequence();
            }
            else
            {
                return;
            }
        }

        private void btnRemoveShelf_Click(object sender, EventArgs e)
        {
            if (Listbox_shef.SelectedIndex != -1)
            {
                Delete_Shelf();
            }
            else
            {
                string msg = "ไม่พบ เลขShelf กรุณาเลือกเลข Shelf ที่ต้องการลบ !!";
                msg.ShowWarningMessage();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindCustomerData();
            for (int i = 0; i < gridCust.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gridCust.Rows[i].Cells[7].Value) == true)
                {
                    gridCust.Rows[i].Cells[7].Value = true;
                }
            }
        }

        private void RemoveCustomer()
        {
            if (txtCustomerID.Text == "")
            {
                return;
            }
            try
            {
                string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
                string title = "ทำการยืนยัน!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                bu = new Customer();
                int ret = 1;
                List<tbl_ArCustomer> custList = bu.GetCustomer(x => x.CustomerID == txtCustomerID.Text);
                tbl_ArCustomer cData = new tbl_ArCustomer();
                cData = custList[0];
                cData.FlagDel = true;
                cData.EdDate = DateTime.Now;
                cData.EdUser = Helper.tbl_Users.Username;
                //ret = bu.UpdateData(cData);
                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                    pnlEdit.ClearControl();
                    PanelEdit(false);
                    SetDefaultData();
                    BindCustomerData();
                    gridCust.Enabled = true;
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
            RemoveCustomer();
        }

        private void gridCust_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            gridCust.SetRowPostPaint(sender, e, this.Font);
        }

        private void Listbox_shef_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Listbox_shef.Items.Count > 0)
            //    {
            //        int ret = 0;
            //        shelfID = "";
            //        page = "edit";
            //        frmShelf f = new frmShelf();
            //        f.ShowDialog();
            //        if (shelfID != "")
            //        {
            //            //Listbox_shef.Items.Add(shelfID);
            //            List<tbl_ArCustomerShelf> _CustShelf = new List<tbl_ArCustomerShelf>();
            //            string _Shelf_No = Listbox_shef.SelectedItem.ToString();
            //            _CustShelf = buShelf.GetCustomerShelf(x => x.CustomerID == txtCustomerID.Text && x.ShelfID == _Shelf_No && x.FlagDel == false);
            //            Listbox_shef.Items.RemoveAt(Listbox_shef.Items.IndexOf(Listbox_shef.SelectedItem));
            //            if (_CustShelf.Count > 0)
            //            {
            //                _CustShelf[0].ShelfID = shelfID;
            //                _CustShelf[0].EdDate = DateTime.Now;
            //                _CustShelf[0].EdUser = Helper.tbl_Users.Username;
            //                _CustShelf[0].FlagNew = false;
            //                _CustShelf[0].FlagEdit = true;
            //                Listbox_shef.Items.Add(shelfID);
            //                foreach (var item in _CustShelf)
            //                {
            //                    ret = buShelf.UpdateData(item);
            //                }
            //                if (ret == 1)
            //                {
            //                    string msg = "แก้ไขข้อมูลเรียบร้อยแล้ว!!";
            //                    msg.ShowInfoMessage();
            //                }
            //                else
            //                {
            //                    this.ShowProcessErr();
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.WriteLog(this.GetType());
            //    string msg = ex.Message;
            //    msg.ShowErrorMessage();
            //}
        }

        private void txtKeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            else
            {
                return;
            }
        }

        private void txtSeq_KeyPress(object sender, KeyPressEventArgs e)
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

        private void frmCustomerInfo_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }
        
        private void txtWHCode_TextChanged(object sender, EventArgs e)
        {
            if (txtWHCode.Text != "")
            {
                var WH2 = bu.GetAllBranchWarehouse(x => x.WHID == txtWHCode.Text);
                if (WH2.Count != 0)
                {
                    txtWHName.Text = WH2[0].WHName;
                    Bind2SalArea(txtWHCode, _ddlSalArea);
                }
                else
                {
                    return;
                }
            }
            else
            {
                BindDDLSalArea(_ddlSalArea);
            }
        }

        private void btnSearchVan_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(BranchWareHouseIDControls, "เลือกคลังสินค้า", (x => x.VanType != 0));
            txtWHID.Text = txtWHCode.Text;
        }

        private void _btnSearchBranch_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchWHControls, "เลือกสาขา/ซุ้ม");
            txtCustomerRefCode.Text = txtBranchCode_.Text;
        }

        private void gridCust_KeyPress(object sender, KeyPressEventArgs e)
        {
            int[] numberCell = new int[] { 6 };
            gridCust.SetCellNumberOnly(sender, e, numberCell.ToList());
        }

        private void gridCust_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
            tb.KeyPress -= gridCust_KeyPress;
            tb.KeyPress += gridCust_KeyPress;
        }

        private void rdoN_CheckedChanged(object sender, EventArgs e)
        {
            BindCustomerData();
        }

        private void rdoC_CheckedChanged(object sender, EventArgs e)
        {
            BindCustomerData();
        }

        private void txtBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBranchCode.Text != "")
                {
                    BindBranch(txtBranchCode, txtBranchName);
                }
                else
                {
                    txtBranchCode.Clear();
                    txtBranchName.Clear();
                    txtBranchCode.Focus();
                    return;
                }
            }
        }

        private void BindBranch(TextBox txtID, TextBox txtName)
        {
            List<tbl_Branch> tbl_BranchesList = new List<tbl_Branch>();
            tbl_BranchesList = bu.GetBranch(x => x.BranchCode == txtID.Text);
            if (tbl_BranchesList.Count != 0)
            {
                txtID.Text = tbl_BranchesList[0].BranchCode;
                txtName.Text = tbl_BranchesList[0].BranchName;
            }
            else
            {
                txtID.Clear();
                txtID.Focus();
                txtName.Clear();
            }
        }

        private void txtLatitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txtLongitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txtBranchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            key_press(e);
        }

        private void btnSearchBranch_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกสาขา/ซุ้ม");
        }

        private void gridCust_SelectionChanged(object sender, EventArgs e)
        {
            SelectCustomerDetails(null);
        }

        private void pnlEdit_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtWHCode.Text != "")
                {
                    Bind2SalArea(txtWHCode, _ddlSalArea);
                }
            }
        }
    }
}
