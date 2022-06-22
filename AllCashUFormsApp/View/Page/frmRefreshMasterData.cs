using AllCashUFormsApp.Controller;
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
    public partial class frmRefreshMasterData : Form
    {
        Login bu = new Login();
        Customer cust = new Customer();

        public frmRefreshMasterData()
        {
            InitializeComponent();
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

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "1");
            btnEdit.Enabled = false;
            btnRemove.Enabled = false;
            btnCancel.Enabled = false;

            txtComment.ReadOnly = true;
        }

        #endregion

        #region Event
        private void frmRefreshMasterData_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private void btnRefreshData_Click(object sender, EventArgs e)
        {
            string cfMsg = "ต้องการดึงข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการดึงข้อมูล!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            Cursor.Current = Cursors.WaitCursor;

            txtComment.Text = "";
            txtComment.AppendText("--------------------------Begin Process-----------------------------");
            txtComment.AppendText(Environment.NewLine);

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            //progressBar1.Step = 10;

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            var self = (BackgroundWorker)sender;

            cust.GetAllData();
            self.ReportProgress(6);

            bu.tbl_AdmFormList = bu.GetAllFromMenu();
            self.ReportProgress(12);

            bu.tbl_DocumentType = bu.GetDocumentType();
            self.ReportProgress(18);

            bu.tbl_MstMenu = bu.GetAllMenuData();
            self.ReportProgress(24);

            bu.tbl_ProductUom = cust.GetUOM();
            self.ReportProgress(30);

            bu.tbl_ProductUomSet = cust.GetUOMSet();
            self.ReportProgress(36);

            bu.tbl_DiscountType = cust.GetDiscountType();
            self.ReportProgress(42);

            bu.tbl_Branchs = bu.GetBranch();
            self.ReportProgress(48);

            bu.tbl_Companies = bu.GetAllCompany();
            self.ReportProgress(54);

            bu.tbl_ProductPriceGroup = cust.GetProductPriceGroup();
            self.ReportProgress(60);

            bu.tbl_SalArea = bu.GetAllSaleArea();
            self.ReportProgress(66);

            bu.tbl_SalAreaDistrict = bu.GetAllSaleAreaDistrict();
            self.ReportProgress(80);

            bu.tbl_Product = bu.GetProduct();
            self.ReportProgress(90);

            bu.tbl_ProductGroup = bu.GetProductGroup();
            self.ReportProgress(95);

            bu.tbl_ProductSubGroup = bu.GetProductSubGroup();
            self.ReportProgress(100);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor.Current = Cursors.Default;

            txtComment.AppendText("--------------------------Complete Process-----------------------------");
            txtComment.AppendText(Environment.NewLine);

            string msg = "ดึงข้อมูลเรียบร้อยแล้ว!!";
            msg.ShowInfoMessage();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string msg = "";
            if (e.ProgressPercentage == 6)
                msg = "ดึงข้อมูลร้านค้า สำเร็จ";
            else if (e.ProgressPercentage == 12)
                msg = bu.tbl_AdmFormList.Count > 0 ? "ดึงข้อมูลเมนูฟอร์ม สำเร็จ" : "ดึงข้อมูลเมนูฟอร์ม ไม่สำเร็จ";
            else if (e.ProgressPercentage == 18)
                msg = bu.tbl_DocumentType.Count > 0 ? "ดึงข้อมูลประเภทเอกสาร สำเร็จ" : "ดึงข้อมูลประเภทเอกสาร ไม่สำเร็จ";
            else if (e.ProgressPercentage == 24)
                msg = bu.tbl_MstMenu.Count > 0 ? "ดึงข้อมูลเมนู สำเร็จ" : "ดึงข้อมูลเมนู ไม่สำเร็จ";
            else if (e.ProgressPercentage == 30)
                msg = bu.tbl_ProductUom.Count > 0 ? "ดึงข้อมูลหน่วยสินค้า สำเร็จ" : "ดึงข้อมูลหน่วยสินค้า ไม่สำเร็จ";
            else if (e.ProgressPercentage == 36)
                msg = bu.tbl_ProductUomSet.Count > 0 ? "ดึงข้อมูลตั้งค่าหน่วยสินค้า สำเร็จ" : "ดึงข้อมูลตั้งค่าหน่วยสินค้า ไม่สำเร็จ";
            else if (e.ProgressPercentage == 42)
                msg = bu.tbl_DiscountType.Count > 0 ? "ดึงข้อมูลประเภทส่วนลด สำเร็จ" : "ดึงข้อมูลประเภทส่วนลด ไม่สำเร็จ";
            else if (e.ProgressPercentage == 48)
                msg = bu.tbl_Branchs.Count > 0 ? "ดึงข้อมูลศูนย์ สำเร็จ" : "ดึงข้อมูลศูนย์ ไม่สำเร็จ";
            else if (e.ProgressPercentage == 54)
                msg = bu.tbl_Companies.Count > 0 ? "ดึงข้อมูลบริษัท สำเร็จ" : "ดึงข้อมูลบริษัท ไม่สำเร็จ";
            else if (e.ProgressPercentage == 60)
                msg = bu.tbl_ProductPriceGroup.Count > 0 ? "ดึงข้อมูลกลุ่มราคาสินค้า สำเร็จ" : "ดึงข้อมูลกลุ่มราคาสินค้า ไม่สำเร็จ";
            else if (e.ProgressPercentage == 66)
                msg = bu.tbl_SalArea.Count > 0 ? "ดึงข้อมูลพื้นที่ขาย สำเร็จ" : "ดึงข้อมูลพื้นที่ขาย ไม่สำเร็จ";
            else if (e.ProgressPercentage == 80)
                msg = bu.tbl_SalAreaDistrict.Count > 0 ? "ดึงข้อมูลเขตการขาย สำเร็จ" : "ดึงข้อมูลเขตการขาย ไม่สำเร็จ";
            else if (e.ProgressPercentage == 90)
                msg = bu.tbl_Product.Count > 0 ? "ดึงข้อมูลสินค้า สำเร็จ" : "ดึงข้อมูลสินค้า ไม่สำเร็จ";
            else if (e.ProgressPercentage == 95)
                msg = bu.tbl_ProductGroup.Count > 0 ? "ดึงข้อมูลกลุ่มสินค้า สำเร็จ" : "ดึงข้อมูลกลุ่มสินค้า ไม่สำเร็จ";
            else if (e.ProgressPercentage == 100)
                msg = bu.tbl_ProductSubGroup.Count > 0 ? "ดึงข้อมูลกลุ่มย่อยสินค้า สำเร็จ" : "ดึงข้อมูลกลุ่มย่อยสินค้า ไม่สำเร็จ";

            txtComment.AppendText(msg);
            txtComment.AppendText(Environment.NewLine);

            //txtComment.Text = e.ProgressPercentage.ToString() + '%';
            progressBar1.Value = e.ProgressPercentage;
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRefreshMasterData_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
