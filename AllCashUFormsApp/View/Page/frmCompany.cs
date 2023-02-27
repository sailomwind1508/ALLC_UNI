using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
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
    public partial class frmCompany : Form
    {
        Company bu = new Company();
        MenuBU menuBU = new MenuBU();
        List<Control> searchBranchControls = new List<Control>();
        List<Control> searchWH1000Controls = new List<Control>();
        List<Control> searchWH1900Controls = new List<Control>();
        DataTable dt = new DataTable();
        DataTable dtConfig = new DataTable();
        Dictionary<Control, Label> validateSave = new Dictionary<Control, Label>();
        public frmCompany()
        {
            InitializeComponent();
            searchBranchControls = new List<Control>() { txtBranchCode, txtBranchName };
            searchWH1000Controls = new List<Control>() { txtWHCode, txtWHName };
            searchWH1900Controls = new List<Control>() { txtWHCode_, txtWHName_ };

            validateSave.Add(txtBranchName, lbl_Depo);
            validateSave.Add(txtWHName, lbl_WH1000);
            validateSave.Add(txtWHName_, lbl_WH1900);
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

        private void BindConfigSetting()
        {
            dtConfig = bu.GetCfgSettingData();
            if (dt.Rows.Count > 0)
            {
                var ConfigList = dtConfig.AsEnumerable().ToList();
                decimal vat = Convert.ToDecimal(ConfigList.FirstOrDefault(x => x.Field<string>("cfgName") == "VatRate").Field<string>("cfgValue")).ToDecimalN0();
                nuVatRate.Value = vat;//ภาษี
                
                //ตั้งค่าใบเสร็จ
                txtInvHeader.Text = ConfigList.FirstOrDefault(x=>x.Field<string>("cfgName") == "InvHeader").Field<string>("cfgValue"); //หัวใบเสร็จ
                txtInvTaxID.Text = ConfigList.FirstOrDefault(x=>x.Field<string>("cfgName") == "InvTaxID").Field<string>("cfgValue"); //เลขประจำตัวผู้เสียภาษี
                txtReportServerPath.Text = ConfigList.FirstOrDefault(x=>x.Field<string>("cfgName") == "ReportServerPath").Field<string>("cfgValue"); //ตำแหน่งไฟล์รายงาน
                //ตั้งค่า Pocket PC
                txtCEDBPath_IN.Text = ConfigList.FirstOrDefault(x=>x.Field<string>("cfgName") == "CEDBPath_IN").Field<string>("cfgValue"); //ตำแหน่ง Upload
                txtCEDBPath.Text = ConfigList.FirstOrDefault(x=>x.Field<string>("cfgName") == "CEDBPath").Field<string>("cfgValue");//ตำแหน่ง Download
                txtCEBackup.Text = ConfigList.FirstOrDefault(x=>x.Field<string>("cfgName") == "CEBackup").Field<string>("cfgValue");//ตำแหน่ง Backup
                txtCESchemaPath.Text = ConfigList.FirstOrDefault(x=>x.Field<string>("cfgName") == "CESchemaPath").Field<string>("cfgValue");//ตำแหน่ง ต้นแบบ
                //ตั้งค่า Tablet
                txtWebServiceHost.Text = ConfigList.FirstOrDefault(x => x.Field<string>("cfgName") == "WebServiceHost").Field<string>("cfgValue");
                txtWebServiceOnline.Text = ConfigList.FirstOrDefault(x => x.Field<string>("cfgName") == "WebServiceOnline").Field<string>("cfgValue");
            }
        }

        private void BindDescription()
        {   //รายละเอียด
            dt = bu.GetCompanyTable(); //new

            if (dt.Rows.Count > 0)
            {
                var company = dt.AsEnumerable().First();

                var b = bu.GetBranch(x=>x.BranchCode == company.Field<string>("BranchID"));
                if (b != null)
                {
                    this.BindData("FromBranchID", searchBranchControls, b[0].BranchID);
                }

                string wh1000 = company.Field<string>("WHID");//WHID --- Company

                var WH = bu.GetAllBranchWarehouse(x => x.WHType == 0);
                for (int i = 0; i < WH.Count; i++)
                {
                    if (WH[i].WHID == wh1000) //คลัง1000
                    {
                        txtWHCode.Text = WH[i].WHCode;
                        txtWHName.Text = WH[i].WHName;
                        break;
                    }
                }

                string WHInTransit = company.Field<string>("WHInTransit");//Company

                string _whID = WHInTransit.Substring(3,4);

                var WareHouseID = WH.FirstOrDefault(x => x.WHCode == _whID);

                if (WareHouseID != null)
                {
                    txtWHCode_.Text = WareHouseID.WHCode;
                    txtWHName_.Text = WareHouseID.WHName;

                }

                txtCompanyName.Text = company.Field<string>("CompanyName");
                txtAddress.Text = company.Field<string>("Address");
                txtCity.Text = company.Field<string>("City");
                txtState.Text = company.Field<string>("State");
                txtZipCode.Text = company.Field<string>("ZipCode");
                txtEmail.Text = company.Field<string>("Email");
                txtPhone.Text = company.Field<string>("Phone");
                txtFax.Text = company.Field<string>("Fax");

                if (!string.IsNullOrEmpty(company.Field<Byte[]>("Logo").ToString()))
                {
                    picLogo.Image = company.Field<Byte[]>("Logo").byteArrayToImage(100, 97);
                }
                else
                {
                    picLogo.Image = null;
                }
            }
            
        }

        private void InitialData()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");

            txtBranchName.DisableTextBox(true);
            txtWHName.DisableTextBox(true);
            txtWHName_.DisableTextBox(true);
        }

        private void txtKeyPress(KeyPressEventArgs e)
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

        private void frmCompany_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
            InitialData();
            BindDescription();
            BindConfigSetting();
        }

        private void btnSearchBranch_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกเดโป้/สาขา");
        }

        private void btnSearchWH1000_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchWH1000Controls, "เลือกคลังสินค้า", (x => x.WHType == 0));
        }

        private void btnSearchWH1900_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchWH1900Controls, "เลือกคลังสินค้า", (x => x.WHType == 0));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //true
            {
                errList.SetErrMessage(validateSave);
            }
            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }
            return ret;
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateSave())
            {
                return;
            }

            string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการบันทึก!!";

            if (!cfMsg.ConfirmMessageBox(title))
                return;

            int ret = 0;

            var tbl_Companys = new tbl_Company();
            tbl_Companys = PrePareCompany();
            if (tbl_Companys != null)
            {
                ret = bu.UpdateData(tbl_Companys);
            }
            if (ret == 1)
            {
                var CfgSettingList = new List<tbl_CfgSetting>();
                CfgSettingList = PrePareCfgSettings();
                foreach (var item in CfgSettingList)
                {
                    ret = bu.UpdateData(item);
                }
                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                }
            }
            else
            {
                this.ShowProcessErr();
                return;
            }

        }

        private tbl_Company PrePareCompany()
        {
            var tbl_Companys = new tbl_Company();
            tbl_Companys = bu.GetCompany();
            if (tbl_Companys != null)
            {
                tbl_Companys.CompanyName = txtCompanyName.Text;
                tbl_Companys.Address = txtAddress.Text;
                tbl_Companys.City = txtCity.Text; //จังหวัด
                tbl_Companys.State = txtState.Text; //เขต
                tbl_Companys.ZipCode = txtZipCode.Text; //รหัส
                tbl_Companys.Email = txtEmail.Text;
                tbl_Companys.Phone = txtPhone.Text;
                tbl_Companys.Fax = txtFax.Text;
                tbl_Companys.Logo = picLogo.Image.ImageToByte(100, 97);
                tbl_Companys.BranchID = txtBranchCode.Text;
                tbl_Companys.WHID = txtBranchCode.Text + txtWHCode.Text; //คลังหลัก
                tbl_Companys.WHInTransit = txtBranchCode.Text + txtWHCode_.Text; //คลังทำลาย

                tbl_Companys.EdDate = DateTime.Now;
                tbl_Companys.EdUser = Helper.tbl_Users.Username;
            }
            return tbl_Companys;
        }

        private List<tbl_CfgSetting> PrePareCfgSettings()
        {
            var CfgSettingList = new List<tbl_CfgSetting>();
            CfgSettingList = bu.GetCfgSetting();
            CfgSettingList.FirstOrDefault(x => x.cfgName == "InvHeader").cfgValue = txtInvHeader.Text;//หัวใบเสร็จ
            CfgSettingList.FirstOrDefault(x => x.cfgName == "InvHeader").ModifiedDate = DateTime.Now;
            CfgSettingList.FirstOrDefault(x => x.cfgName == "InvTaxID").cfgValue = txtInvTaxID.Text;//เลขประจำตัวผู้เสียภาษี
            CfgSettingList.FirstOrDefault(x => x.cfgName == "InvTaxID").ModifiedDate = DateTime.Now;
            CfgSettingList.FirstOrDefault(x => x.cfgName == "ReportServerPath").cfgValue = txtReportServerPath.Text;//ตำแหน่งไฟล์รายงาน
            CfgSettingList.FirstOrDefault(x => x.cfgName == "ReportServerPath").ModifiedDate = DateTime.Now;
            CfgSettingList.FirstOrDefault(x => x.cfgName == "CEDBPath_IN").cfgValue = txtCEDBPath_IN.Text; //ตำแหน่งupload
            CfgSettingList.FirstOrDefault(x => x.cfgName == "CEDBPath_IN").ModifiedDate = DateTime.Now;
            CfgSettingList.FirstOrDefault(x => x.cfgName == "CEDBPath").cfgValue = txtCEDBPath.Text;//ตำแหน่งDownload
            CfgSettingList.FirstOrDefault(x => x.cfgName == "CEDBPath").ModifiedDate = DateTime.Now;
            CfgSettingList.FirstOrDefault(x => x.cfgName == "CEBackup").cfgValue = txtCEBackup.Text;//ตำแหน่งBackup
            CfgSettingList.FirstOrDefault(x => x.cfgName == "CEBackup").ModifiedDate = DateTime.Now;
            CfgSettingList.FirstOrDefault(x => x.cfgName == "CESchemaPath").cfgValue = txtCESchemaPath.Text;//ตำแหน่ง ต้นแบบ
            CfgSettingList.FirstOrDefault(x => x.cfgName == "CESchemaPath").ModifiedDate = DateTime.Now;
            CfgSettingList.FirstOrDefault(x => x.cfgName == "WebServiceHost").cfgValue = txtWebServiceHost.Text;
            CfgSettingList.FirstOrDefault(x => x.cfgName == "WebServiceHost").ModifiedDate = DateTime.Now;
            CfgSettingList.FirstOrDefault(x => x.cfgName == "WebServiceOnline").cfgValue = txtWebServiceOnline.Text;
            CfgSettingList.FirstOrDefault(x => x.cfgName == "WebServiceOnline").ModifiedDate = DateTime.Now;
            return CfgSettingList;
        }

        private void txtBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var b = bu.GetBranch(x=>x.BranchCode == txtBranchCode.Text);
                txtBranchName.Text =  b.Count > 0 ? b.First().BranchName : "";
            }
        }

        private string PrePareWareHouse(TextBox txt)
        {
            string whName = "";
            var wh = bu.GetAllBranchWarehouse(x => x.WHCode == txt.Text);
            whName = wh.Count > 0 ? wh.First().WHName : "";
            return whName;
        }

        private void txtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWHName.Text =  PrePareWareHouse(txtWHCode);
            }
        }

        private void txtWHCode__KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWHName_.Text = PrePareWareHouse(txtWHCode_);
            }
        }

        private void txtBranchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txtWHCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txtWHCode__KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txtInvTaxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void PrePareSelectPath(TextBox txt)
        {
            string ServerPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                ServerPath = folderBrowserDialog1.SelectedPath;
                if (!string.IsNullOrEmpty(ServerPath))
                {
                    txt.Text = ServerPath;
                }
            }
        }

        private void btnReportServerPath_Click(object sender, EventArgs e)
        {
            PrePareSelectPath(txtReportServerPath);
        }

        private void btnCEDBPath_IN_Click(object sender, EventArgs e)
        {
            PrePareSelectPath(txtCEDBPath_IN);
        }

        private void btnCEDBPath_Click(object sender, EventArgs e)
        {
            PrePareSelectPath(txtCEDBPath);
        }

        private void btnCEBackup_Click(object sender, EventArgs e)
        {
             PrePareSelectPath(txtCEBackup);
        }

        private void btnCESchemaPath_Click(object sender, EventArgs e)
        {
             PrePareSelectPath(txtCESchemaPath);
        }

        private void frmCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

    }
}
