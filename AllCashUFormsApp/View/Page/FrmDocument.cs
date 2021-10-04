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
    public partial class FrmDocument : Form
    {
        MenuBU menuBU = new MenuBU();
        MasterDataControl bu = new MasterDataControl();
        static DataTable dtDocument = new DataTable();
        List<string> PanelEditControls = new List<string>();
        Dictionary<Control, Label> ValidateCtrls = new Dictionary<Control, Label>();
        bool flagEdit = false;
        public FrmDocument()
        {
            InitializeComponent();
            PanelEditControls = new string[] { txtDocTypeCode.Name }.ToList();
            ValidateCtrls.Add(txtDocTypeCode, lbl_DocTypeCode);
            ValidateCtrls.Add(txtDocTypeName, lbl_DocTypeName);
            ValidateCtrls.Add(txtDocFormat, lbl_DocFormat);
            ValidateCtrls.Add(txtDocHeader, lbl_DocHeader);
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
        private void EnableBtn()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }
        private void PrePareCbbDocRunning(string DocTypeCode)
        {
            DataTable dt = new DataTable();
            dt = bu.GetDocRunning(txtDocTypeCode.Text, "", "");

            cbbYears.Items.Clear();
            cbbMonths.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                List<string> YearDocList = new List<string>();
                foreach (DataRow r in dt.Rows)
                {
                    YearDocList.Add(r["YearDoc"].ToString());
                }

                cbbYears.Items.Add("==เลือก==");
                cbbMonths.Items.Add("==เลือก==");

                var years = YearDocList.Distinct();

                foreach (var item in years)
                {
                    cbbYears.Items.Add(item);
                }
            }
            else
            {
                cbbYears.Items.Add("==เลือก==");
                cbbMonths.Items.Add("==เลือก==");
            }
            cbbYears.SelectedIndex = 0;
            cbbMonths.SelectedIndex = 0;
        }
        private void SetCbbYear_Month()
        {
            cbbYears.Items.Clear();
            cbbMonths.Items.Clear();

            cbbYears.Items.Add("==เลือก==");
            cbbMonths.Items.Add("==เลือก==");

            cbbYears.SelectedIndex = 0;
            cbbMonths.SelectedIndex = 0;
        }
        private void InitialData()
        {
            EnableBtn();

            grdDocument.AutoGenerateColumns = false;
            grdDocument.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            grdDocRunning.AutoGenerateColumns = false;
            grdDocRunning.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            SetCbbYear_Month();

            OpenPanelEdit(false);
        }
        private void OpenPanelEdit(bool flagEnable)
        {
            pnlEdit.OpenControl(flagEnable, PanelEditControls.ToArray());

            foreach (Control item in pnlEdit.Controls)
            {
                if (flagEnable == false) // สั่งปิด   //ใส่สีเทาใน Control Panel
                {
                    if (item is Label || item is Panel || item is PictureBox || item is CheckBox || item is Button || item is GroupBox || item is ComboBox || item is ListBox || item is NumericUpDown)
                    {

                    }
                    else
                    {
                        item.BackColor = ColorTranslator.FromHtml("#DCDCDC");
                    }
                }
            }
        }
        private void FrmDocument_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            dtDocument = new DataTable();
            this.Close();
        }
        private void EnableEditRemoveBtn(bool flagEnable)
        {
            btnEdit.Enabled = flagEnable;
            btnRemove.Enabled = flagEnable;
        }
        private void BindDocumentData()
        {
            dtDocument = new DataTable();
            dtDocument = bu.GetDocumentTypeData(txtSearch.Text);

            if (dtDocument.Rows.Count > 0)
            {
                EnableEditRemoveBtn(true);
                cbbYears.Enabled = true;
                cbbMonths.Enabled = true;
            }
            else
            {
                EnableEditRemoveBtn(false);
            }
            
            grdDocument.DataSource = dtDocument;
            lbl_Qty_grd.Text = dtDocument.Rows.Count.ToNumberFormat();

            if (dtDocument.Rows.Count == 0)
            {
                cbbYears.Enabled = false;
                cbbMonths.Enabled = false;

                txtDocTypeCode.Clear();
                txtDocTypeName.Clear();
                txtDocFormat.Clear();
                txtDocHeader.Clear();
                txtDocRemark.Clear();
                numericRunLength.Value = 0;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindDocumentData();
        }
        private void grdDocument_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdDocument.SetRowPostPaint(sender, e, this.Font);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlEdit.ClearControl();
            numericRunLength.Value = 0;
            OpenPanelEdit(false);

            OpenPanelSearch(false);

            SetDefaultPnlDocRunning(true);

            EnableBtn();
            cbbMonths.Enabled = true;
            cbbYears.Enabled = true;
            BindDocumentData();
        }
        private void grdDocument_SelectionChanged(object sender, EventArgs e)
        {
            SelectDocumentDetails(null);
        }
        private void SelectDocumentDetails(DataGridViewCellEventArgs e)
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
                    grdRows = grdDocument.Rows[e.RowIndex];
                }
            }
            else
            {
                grdRows = grdDocument.CurrentRow;
            }

            if (grdRows != null)
            {
                string DocTypeCode = grdRows.Cells["colDocTypeCode"].Value.ToString();

                DataRow r = dtDocument.AsEnumerable().FirstOrDefault(x => x.Field<string>("DocTypeCode") == DocTypeCode);

                if (r != null)
                {
                    txtDocTypeCode.Text = r["DocTypeCode"].ToString();
                    txtDocTypeName.Text = r["DocTypeName"].ToString();
                    txtDocFormat.Text = r["DocFormat"].ToString();
                    numericRunLength.Text = r["RunLength"].ToString();
                    txtDocHeader.Text = r["DocHeader"].ToString();
                    txtDocRemark.Text = r["DocRemark"].ToString();
                    PrePareCbbDocRunning(txtDocTypeCode.Text);
                }
                
            }
           
        }
        private void grdDocument_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectDocumentDetails(e);
        }
        private void SetDefaultPnlDocRunning(bool flagEnable)
        {
            DataTable dt = new DataTable();
            grdDocRunning.DataSource = dt;

            cbbYears.Enabled = flagEnable;
            cbbMonths.Enabled = flagEnable;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            flagEdit = false;
            PanelCtrl();
            pnlEdit.ClearControl();
            numericRunLength.Value = 0;
            txtDocTypeCode.Focus();

            SetDefaultPnlDocRunning(false);
        }
        private void OpenPanelSearch(bool flagEnable)
        {
            txtSearch.DisableTextBox(flagEnable);
            btnSearch.Enabled = !flagEnable;
            grdDocument.Enabled = !flagEnable;
        }
        private void PanelCtrl()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            OpenPanelSearch(true);
            OpenPanelEdit(true);
            txtDocTypeCode.DisableTextBox(false);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            flagEdit = true;
            PanelCtrl();
            txtDocTypeCode.DisableTextBox(true);
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
        private void PrePareDocument(tbl_DocumentType DocType)
        {
            DocType.DocTypeName = txtDocTypeName.Text;
            DocType.DocFormat = txtDocFormat.Text;
            DocType.RunLength = Convert.ToInt16(numericRunLength.Value);
            DocType.DocHeader = txtDocHeader.Text;
            DocType.DocRemark = txtDocRemark.Text;
        }
        private void Save()
        {
            if (!ValidateSave())
            {
                return;
            }

            try
            {
                var DocTypeList = new List<tbl_DocumentType>();
                DocTypeList = bu.GetDocumentType(x => x.DocTypeCode == txtDocTypeCode.Text);

                if (flagEdit == false)
                {
                    if (DocTypeList.Count > 0)
                    {
                        string msg = "รหัสเอกสารซ้ำในระบบ กรุณาเปลี่ยนรหัสใหม่ !!";
                        msg.ShowWarningMessage();
                        return;
                    }
                }

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;
                var DocType = new tbl_DocumentType();

                if (DocTypeList.Count > 0)
                {
                    DocType = DocTypeList[0];

                    PrePareDocument(DocType);

                    DocType.EdDate = DateTime.Now;
                    DocType.EdUser = Helper.tbl_Users.Username;
                }
                else
                {
                    DocType.DocTypeCode = txtDocTypeCode.Text;

                    PrePareDocument(DocType);

                    DocType.CrDate = DateTime.Now;
                    DocType.CrUser = Helper.tbl_Users.Username;

                    DocType.EdDate = null;
                    DocType.EdUser = null;

                    DocType.FlagDel = false;
                    DocType.FlagSend = false;
                }

                ret = bu.UpdateDocumentTypeData(DocType);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    msg.ShowInfoMessage();

                    pnlEdit.ClearControl();
                    OpenPanelEdit(false);
                    EnableBtn();
                    OpenPanelSearch(false);
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Remove()
        {
            try
            {
                string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
                string title = "ทำการยืนยัน!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;
                var DocType = new tbl_DocumentType();
                var DocTypeList = new List<tbl_DocumentType>();

                DocTypeList = bu.GetDocumentType(x => x.DocTypeCode == txtDocTypeCode.Text);
                if (DocTypeList.Count > 0)
                {
                    DocType = DocTypeList[0];
                    DocType.EdDate = DateTime.Now;
                    DocType.EdUser = Helper.tbl_Users.Username;
                    DocType.FlagDel = true;

                    ret = bu.UpdateDocumentTypeData(DocType);

                    if (ret == 1)
                    {
                        string msg = "ลบข้อมูลเรียบร้อยแล้ว";
                        msg.ShowInfoMessage();
                        btnSearch.PerformClick();
                    }
                    else
                    {
                        this.ShowProcessErr();
                        return;
                    }
                }
                else
                {
                    string msg = "ไม่พบข้อมูลประเภทเอกสาร กรุณาเลือกประเภทเอกสารที่ต้องการลบ !!";
                    msg.ShowWarningMessage();
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
        private void BindDocRunning(bool flagMclick = false)
        {
            string @YearDoc = "";
            if (cbbYears.Text != "==เลือก==")
            {
                @YearDoc = cbbYears.Text;
            }

            string @MonthDoc = "";
            if (cbbMonths.Text != "==เลือก==")
            {
                @MonthDoc = cbbMonths.Text;
            }

            DataTable dtDocRunning = new DataTable();
            dtDocRunning = bu.GetDocRunning(txtDocTypeCode.Text,@YearDoc, @MonthDoc);

            if (flagMclick == false)
            {
                List<string> MonthDocs = new List<string>();

                cbbMonths.Items.Clear();
                cbbMonths.Items.Add("==เลือก==");

                if (dtDocRunning.Rows.Count > 0)
                {
                    foreach (DataRow r in dtDocRunning.Rows)
                    {
                        MonthDocs.Add(r["MonthDoc"].ToString());
                    }

                    MonthDocs = MonthDocs.Distinct().ToList();

                    foreach (var item in MonthDocs)
                    {
                        cbbMonths.Items.Add(item);
                    }
                }

                cbbMonths.SelectedIndex = 0;
            }
            if (grdDocument.Rows.Count > 0)
            {
                grdDocRunning.DataSource = dtDocRunning;
            }
            
        }
        private void cbbYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbYears.SelectedIndex > 0)
            {
                BindDocRunning();
            }
            else
            {
                cbbMonths.Items.Clear();
                cbbMonths.Items.Add("==เลือก==");
                cbbMonths.SelectedIndex = 0;
            }
        }
        private void grdDocRunning_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdDocRunning.SetRowPostPaint(sender, e, this.Font);
        }
        private void cbbMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDocRunning(true);
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindDocumentData();
            }
        }

        private void FrmDocument_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
