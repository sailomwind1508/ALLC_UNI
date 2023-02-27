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
using AllCashUFormsApp.View.UControl;
namespace AllCashUFormsApp.View.Page
{
    public partial class frmSupplierInfo : Form
    {
        MasterDataControl bu = new MasterDataControl();
        MenuBU menuBU = new MenuBU();
        static DataTable dtSupplier = new DataTable();
        List<string> OpenPanelSearchControls = new List<string>();
        List<string> OpenPanelEditControls = new List<string>();
        public static SearchAddressModel SAM = new SearchAddressModel();
        Dictionary<Control, Label> ValidateCtrls = new Dictionary<Control, Label>(); // Validate Save
        public static string SupplierTypeID = "";
        public frmSupplierInfo()
        {
            InitializeComponent();
            OpenPanelSearchControls = new string[] { txtSearch.Name }.ToList();
            OpenPanelEditControls = new string[] { txtSupplierCode.Name }.ToList();
            ValidateCtrls.Add(txtSupplierCode, lbl_Code);
            ValidateCtrls.Add(txtSuppName, lbl_Name);
            ValidateCtrls.Add(txtSuppShortName, lbl_ShortName);
            ValidateCtrls.Add(txtBillTo, lbl_BillTo);
            ValidateCtrls.Add(txtContact, lbl_Contact);
            ValidateCtrls.Add(txtTelephone, lbl_TelePhone);
            ValidateCtrls.Add(txtSaleAp, lbl_SaleAp);
            ValidateCtrls.Add(txtSaleApTel, lbl_SaleApTel);
        }
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
        private void PrePareCbbSupplierType()
        {
            var tbl_ApSupplierType = bu.GetSupplierType(x=>x.FlagDel == false);
            cbbSupplierType.BindDropdownList(tbl_ApSupplierType, "ApSupplierTypeName", "APSupplierTypeID");
        }
        private void PrePareCbbSuppTitle()
        {
            cbbSuppTitle.Items.Clear();
            cbbSuppTitle.Items.Add("บริษัท");
            cbbSuppTitle.Items.Add("หจก.");
            cbbSuppTitle.Items.Add("บมจ.");
            cbbSuppTitle.Items.Add("คุณ");
            cbbSuppTitle.SelectedIndex = 0;
        }
        private void SetDefaultButton()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }
        private void InitialData()
        {
            SetDefaultButton();

            grdSupplier.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdSupplier.AutoGenerateColumns = false;

            PrePareCbbSupplierType();
            PrePareCbbSuppTitle();

            pnlEdit.OpenControl(false, OpenPanelEditControls.ToArray());
        }
        private void BindSupplierData()
        {
            pnlEdit.ClearControl();
            pnlEdit.OpenControl(false, OpenPanelEditControls.ToArray());

            int flagDel = rdoN.Checked ? 0 : 1;
            DataTable dt = new DataTable();
            dt = bu.GetApSupplierData(flagDel,txtSearch.Text);
            dtSupplier = dt; // เก็บไว้ใน Ram
           
            grdSupplier.DataSource = dt;
            lblgrdQty.Text = dt.Rows.Count.ToNumberFormat();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            pnlStatus.Visible = false;
            btnEdit.Enabled = false;
            btnRemove.Enabled = false;

            if (rdoN.Checked && dt.Rows.Count > 0)
            {
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
            }
            else if (rdoC.Checked && dt.Rows.Count > 0)
            {
                btnAdd.Enabled = false;
                pnlStatus.Visible = true;
            }
            else if (rdoC.Checked && dt.Rows.Count == 0)
            {
                btnAdd.Enabled = false;
            }
        }
        private void frmSupplierInfo_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
            InitialData();
            BindSupplierData();
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindSupplierData();
            }
        }
        private void rdoN_CheckedChanged(object sender, EventArgs e)
        {
            BindSupplierData();
        }
        private void rdoC_CheckedChanged(object sender, EventArgs e)
        {
            BindSupplierData();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindSupplierData();
        }
        private void SelectSupplierDetails(DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow gridViewRow = null;

                if (e != null)
                {
                    if (e.RowIndex == -1)
                    {
                        return;
                    }
                    else
                    {
                        gridViewRow = grdSupplier.Rows[e.RowIndex];
                    }
                }
                else
                {
                    gridViewRow = grdSupplier.CurrentRow;
                }

                string colSupplierID = gridViewRow.Cells["colSupplierID"].Value.ToString();

                DataRow r = dtSupplier.AsEnumerable().FirstOrDefault(x => x.Field<string>("SupplierID") == colSupplierID);

                if (r != null)
                {
                    txtSupplierCode.Text = r["SupplierCode"].ToString();
                    cbbSupplierType.SelectedValue = Convert.ToInt32(r["SupplierTypeID"]);
                    cbbSuppTitle.Text = r["SuppTitle"].ToString();
                    txtSuppName.Text = r["SuppName"].ToString();
                    txtSuppShortName.Text = r["SuppShortName"].ToString();
                    txtBillTo.Text = r["BillTo"].ToString();
                    txtContact.Text = r["Contact"].ToString();
                    txtTelephone.Text = r["Telephone"].ToString();
                    txtFax.Text = r["Fax"].ToString();
                    txtCreditDay.Text = r["CreditDay"].ToString();
                    
                    int VatType = Convert.ToInt32(r["VatType"]); // ประเภทภาษี

                    if (VatType == 0)
                    {
                        rdoVatF.Checked = true;
                    }
                    else
                    {
                        rdoVatN.Checked = true;
                    }

                    txtTaxId.Text = r["TaxId"].ToString(); //เลขประจำตัวผู้เสียภาษี
                    txtSaleAp.Text = r["SaleAp"].ToString(); // พนักงานขาย
                    txtSaleApTel.Text = r["SaleApTel"].ToString(); // พนักงานขาย
                    txtLeadTime.Text = r["LeadTime"].ToString(); // ระยะเวลาการส่งสินค้า

                    SAM.AddressNo = r["AddressNo"].ToString();
                    SAM.lane = r["lane"].ToString();
                    SAM.Street = r["Street"].ToString();
                    SAM.AreaID = Convert.ToInt32(r["AreaID"]);
                    SAM.ProvinceID = Convert.ToInt32(r["ProvinceID"]);
                    SAM.PostalCode = r["PostalCode"].ToString();
                    SAM.DistrictID = Convert.ToInt32(r["DistrictID"].ToString());

                    txtEmail.Text = r["Email"].ToString();

                    //DiscountType Discount ไม่มี
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
        private void grdSupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectSupplierDetails(e);
        }
        private void grdSupplier_SelectionChanged(object sender, EventArgs e)
        {
            SelectSupplierDetails(null);
        }
        private void grdSupplier_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdSupplier.SetRowPostPaint(sender, e, this.Font);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            dtSupplier = new DataTable();
            SAM = new SearchAddressModel();
            this.Close();
        }
        private void btnSearchAddress_Click(object sender, EventArgs e)
        {
            SAM.Page = "Supplier";
            frmSearchAddress frm = new frmSearchAddress();
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(SAM.billTo))
            {
                txtBillTo.Text = SAM.billTo;
            }
        }
        private void SetDefaultPnlEdit()
        {
            cbbDiscount.Enabled = false;
            txtDiscount.ReadOnly = true;
            txtCreditDay.Text = "0";
            txtLeadTime.Text = "0";
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            pnlEdit.OpenControl(true, OpenPanelEditControls.ToArray());
            txtBillTo.DisableTextBox(true);
            SetDefaultPnlEdit();
        }
        private void PrePareRunningNo()
        {
            var Supplier = bu.GetSupplier();

            if (Supplier.Count > 0)
            {
                var Max_SupplierCode = Convert.ToInt32(Supplier.Select(x => x.SupplierCode).Max()) + 1;

                string maxCode = "";

                if (Max_SupplierCode <= 9)
                {
                    maxCode = "0" + Max_SupplierCode.ToString();
                }
                else
                {
                    maxCode = Max_SupplierCode.ToString();
                }
                txtSupplierCode.Text = maxCode.ToString();
            }
            else
            {
                txtSupplierCode.Text = "01";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = false;

            pnlSearch.OpenControl(false, OpenPanelSearchControls.ToArray());
            pnlEdit.ClearControl();

            pnlEdit.OpenControl(true, OpenPanelEditControls.ToArray());

            cbbSupplierType.SelectedIndex = 0;
            cbbSuppTitle.SelectedIndex = 0;
            rdoVatN.Checked = true;

            SAM = new SearchAddressModel();

            grdSupplier.Enabled = false;

            PrePareRunningNo();

            txtDiscount.DisableTextBox(true);
            txtSupplierCode.DisableTextBox(true); //
            txtBillTo.DisableTextBox(true); 
            SetDefaultPnlEdit();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetDefaultButton();
            grdSupplier.Enabled = true;
            pnlSearch.OpenControl(true, OpenPanelEditControls.ToArray());
            pnlEdit.ClearControl();
            pnlEdit.OpenControl(false, OpenPanelEditControls.ToArray());
            BindSupplierData();
        }
        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //true
            {
                errList.SetErrMessage(ValidateCtrls);
            }
            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }
            return ret;
        }
        private void PrePare_ApSupplier(tbl_ApSupplier ApSupplier)
        {
            ApSupplier.SupplierCode = txtSupplierCode.Text;
            ApSupplier.SupplierTypeID = Convert.ToInt32(cbbSupplierType.SelectedValue);
            ApSupplier.SuppTitle = cbbSuppTitle.Text;
            ApSupplier.SuppName = txtSuppName.Text;
            ApSupplier.SuppShortName = txtSuppShortName.Text;
            ApSupplier.BillTo = txtBillTo.Text;
            ApSupplier.Contact = txtContact.Text;
            ApSupplier.Telephone = txtTelephone.Text;
            ApSupplier.Fax = txtFax.Text;
            ApSupplier.Email = txtEmail.Text;
            ApSupplier.CreditDay = (byte)Convert.ToInt32(txtCreditDay.Text);
            ApSupplier.VatType = rdoVatN.Checked ? true : false;
            ApSupplier.TaxId = txtTaxId.Text;
            ApSupplier.SaleAp = txtSaleAp.Text;
            ApSupplier.SaleApTel = txtSaleApTel.Text;
            ApSupplier.LeadTime = Convert.ToInt32(txtLeadTime.Text);

            ApSupplier.AddressNo = SAM.AddressNo;
            ApSupplier.lane = SAM.lane;
            ApSupplier.Street = SAM.Street;
            ApSupplier.ProvinceID = SAM.ProvinceID;
            ApSupplier.AreaID = SAM.AreaID;
            ApSupplier.DistrictID = SAM.DistrictID;
            ApSupplier.PostalCode = SAM.PostalCode;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
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

                var Supplier = bu.GetSupplier();

                var ApSupplier = Supplier.FirstOrDefault(x => x.SupplierCode == txtSupplierCode.Text);

                if (ApSupplier != null)
                {
                    PrePare_ApSupplier(ApSupplier);
                    ApSupplier.EdDate = DateTime.Now;
                    ApSupplier.EdUser = Helper.tbl_Users.Username;
                }
                else
                {
                    ApSupplier = new tbl_ApSupplier();
                    PrePare_ApSupplier(ApSupplier);

                    ApSupplier.SupplierID = txtSupplierCode.Text;
                    ApSupplier.SupplierRefCode = "";

                    ApSupplier.CrDate = DateTime.Now;
                    ApSupplier.CrUser = Helper.tbl_Users.Username;

                    ApSupplier.FlagSend = false;
                    ApSupplier.FlagDel = false;

                    ApSupplier.EdDate = null;
                    ApSupplier.EdUser = null;

                    ApSupplier.Discount = null;
                    ApSupplier.DiscountType = null;
                    ApSupplier.ShipTo = null;
                }

                ret = bu.UpdateApSupplierData(ApSupplier);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                    btnAdd.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;

                    pnlSearch.OpenControl(true, OpenPanelSearchControls.ToArray());
                    pnlEdit.ClearControl();
                    pnlEdit.OpenControl(false, OpenPanelEditControls.ToArray());

                    txtSearch.DisableTextBox(false);
                    grdSupplier.Enabled = true;

                    BindSupplierData();
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
        private void txtKeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }
        private void txtLeadTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }
        private void txtTaxId_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress_Mark(e);
        }
        private void txtLeadTime_TextChanged(object sender, EventArgs e)
        {
            ReplaceZeroToTextbox(txtLeadTime);
        }
        private void ReplaceZeroToTextbox(TextBox Textbox)
        {
            if (string.IsNullOrEmpty(Textbox.Text))
            {
                Textbox.Text = "0";
            }
        }
        private void txtCreditDay_TextChanged(object sender, EventArgs e)
        {
            ReplaceZeroToTextbox(txtCreditDay);
        }
        private void txtCreditDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }
        private void txtKeyPress_Mark(KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }
        private void txtSaleApTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress_Mark(e);
        }
        private void Remove(bool flagRemove = true)
        {
            try
            {
                if (flagRemove == true)
                {
                    string cfMsg = "คุณแน่ใจหรือไม่ที่จะลบข้อมูลรายการนี้?";
                    string title = "ทำการยืนยัน!!";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;
                }
               
                int ret = 0;

                var Supplier = bu.GetSupplier();

                var ApSupplier = Supplier.FirstOrDefault(x => x.SupplierCode == txtSupplierCode.Text);

                if (ApSupplier != null)
                {
                    PrePare_ApSupplier(ApSupplier);

                    if (flagRemove == true)
                    {
                        ApSupplier.FlagDel = true;
                    }
                    else
                    {
                        ApSupplier.FlagDel = false;
                    }
                    
                    ApSupplier.EdDate = DateTime.Now;
                    ApSupplier.EdUser = Helper.tbl_Users.Username;
                    ret = bu.UpdateApSupplierData(ApSupplier);
                }

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    pnlEdit.ClearControl();
                    pnlEdit.OpenControl(false, OpenPanelEditControls.ToArray());

                    BindSupplierData();
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
        private void btnRemove_Click(object sender, EventArgs e)
        {
            Remove();
        }
        private void txtTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress_Mark(e);
        }
        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress_Mark(e);
        }
        private void btnN_Click(object sender, EventArgs e)
        {
            Remove(false);
        }
        private void btnSearchSupplierType_Click(object sender, EventArgs e)
        {
            frmSupplierType frm = new frmSupplierType();
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(SupplierTypeID))
            {
                cbbSupplierType.SelectedValue = Convert.ToInt32(SupplierTypeID);
            }
        }

        private void frmSupplierInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
