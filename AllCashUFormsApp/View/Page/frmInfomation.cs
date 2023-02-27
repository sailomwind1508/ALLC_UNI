using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmInfomation : Form
    {
        MasterDataControl bu = new MasterDataControl();
        MenuBU menuBU = new MenuBU();
        Dictionary<Control, Label> ValidateSave_DisplayImage = new Dictionary<Control, Label>();
        public frmInfomation()
        {
            InitializeComponent();
            ValidateSave_DisplayImage.Add(txtName, lbl_Name);
            ValidateSave_DisplayImage.Add(txtDescription, lbl_Description);
            ValidateSave_DisplayImage.Add(numericUpDown1, lbl_Seq);
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
        private void InitialData()
        {
            grdDisplayImage.AutoGenerateColumns = false;
            grdDisplayImage.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            VisibleStatus();

            ReadOnlyPanelEdit(true);

            txtAutoID.DisableTextBox(true);
        }
        private void OpenPanelSearch(bool Enable = true)
        {
            txtSearch.DisableTextBox(!Enable);
            rdoN.Enabled = Enable;
            rdoC.Enabled = Enable;
            btnSearch.Enabled = Enable;
        }
        private void ReadOnlyPanelEdit(bool flagReadOnly)
        {
            numericUpDown1.ReadOnly = flagReadOnly;
            numericUpDown1.BackColor = numericUpDown1.ReadOnly ? ColorTranslator.FromHtml("#DCDCDC") : Color.White;

            txtName.ReadOnly = flagReadOnly;
            txtName.BackColor = txtName.ReadOnly ? ColorTranslator.FromHtml("#DCDCDC") : Color.White;

            txtDescription.ReadOnly = flagReadOnly;
            txtDescription.BackColor = txtDescription.ReadOnly ? ColorTranslator.FromHtml("#DCDCDC") : Color.White;

            btnBrowse.Enabled = !flagReadOnly;
            btnBrowse.BackColor = btnBrowse.Enabled ? Color.PaleTurquoise : ColorTranslator.FromHtml("#DCDCDC");

            DisplayImagePicture.Enabled = !flagReadOnly;
            DisplayImagePicture.BackColor = DisplayImagePicture.Enabled ? Color.White : ColorTranslator.FromHtml("#DCDCDC");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Image";
            openFileDialog1.Filter = "PNG files (*.PNG)|*.PNG";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(openFileDialog1.FileName);

                DisplayImagePicture.Image = image.ImageToByte().byteArrayToImage(501, 280);

                string picPath = openFileDialog1.FileName;
            }
        }
        private void BindDisplayImageData()
        {
            ClearPanelEdit();

            int flagDel = rdoN.Checked ? 0 : 1;
            DataTable dt = new DataTable();
            dt = bu.GetDisplayImageData(flagDel,txtSearch.Text);

            grdDisplayImage.DataSource = dt;
            lblgrdQty.Text = dt.Rows.Count.ToNumberFormat();

            VisibleStatus(false);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            if (dt.Rows.Count > 0 && rdoN.Checked == true)
            {
                btnRemove.Enabled = true;
                btnEdit.Enabled = true;
            }
            if (rdoC.Checked == true)
            {
                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnSave.Enabled = false;
                btnCancel.Enabled = false;

                if (dt.Rows.Count > 0)
                {
                    VisibleStatus(true);
                }
            }
        }
        private void VisibleStatus(bool flagVisible = false)
        {
            lbl_Status.Visible = flagVisible;
            btnN.Visible = flagVisible;
        }
        private void frmInfomation_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
            InitialData();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindDisplayImageData();
        }
        private void grdDisplayImage_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdDisplayImage.SetRowPostPaint(sender, e, this.Font);
        }
        private void SelectDisplayImageDetails(DataGridViewCellEventArgs e)
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
                    grdRows = grdDisplayImage.Rows[e.RowIndex];
                }
            }
            else
            {
                grdRows = grdDisplayImage.CurrentRow;
            }

            if (grdRows != null)
            {
                numericUpDown1.Value = Convert.ToDecimal(grdRows.Cells["colSeq"].Value).ToDecimalN2();
                txtAutoID.Text = grdRows.Cells["colAutoID"].Value.ToString();
                txtName.Text = grdRows.Cells["colName"].Value.ToString();
                txtDescription.Text = grdRows.Cells["colDescription"].Value.ToString();

                if (!string.IsNullOrEmpty(grdRows.Cells["colImage"].Value.ToString()))
                {
                    Byte[] data = new Byte[0];
                    data = (Byte[])grdRows.Cells["colImage"].Value;
                    DisplayImagePicture.Image = data.byteArrayToImage(501, 280);
                }
                else
                {
                    DisplayImagePicture.Image = null;
                }
            }
            
        }
        private void grdDisplayImage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectDisplayImageDetails(e);
        }
        private void rdoN_CheckedChanged(object sender, EventArgs e)
        {
            BindDisplayImageData();
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindDisplayImageData();
            }
        }
        private void rdoC_CheckedChanged(object sender, EventArgs e)
        {
            BindDisplayImageData();
        }
        private void grdDisplayImage_SelectionChanged(object sender, EventArgs e)
        {
            SelectDisplayImageDetails(null);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            OpenPanelSearch(false);
            ReadOnlyPanelEdit(false);
            numericUpDown1.Focus();
        }
        private void FormatRunningNo()
        {
            var MaxID = bu.GetDisplayImage();

            if (MaxID.Count == 0)
            {
                txtAutoID.Text = "1";
            }
            else
            {
                txtAutoID.Text = (Convert.ToInt32(MaxID.Select(x => x.AutoID).Max() + 1)).ToString();
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            OpenPanelSearch(false);
            ReadOnlyPanelEdit(false);

            ClearPanelEdit();

            FormatRunningNo();

            grdDisplayImage.Enabled = false;

            numericUpDown1.Focus();
        }
        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret)
            {
                errList.SetErrMessage(ValidateSave_DisplayImage);
            }
            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }
            return ret;
        }
        private void PrePareDisplayImage(tbl_DisplayImage DisplayImage)
        {
            DisplayImage.Seq = Convert.ToInt32(numericUpDown1.Value);
            DisplayImage.Name = txtName.Text;
            DisplayImage.Description = txtDescription.Text;

            DisplayImage.FlagDel = false;

            if (DisplayImagePicture.Image != null)
            {
                DisplayImage.Image = DisplayImagePicture.Image.ImageToByte(501, 280);
            }
            else
            {
                DisplayImage.Image = null;
            }
        }
        private void Save()
        {
            try
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

                var DisplayImage = new tbl_DisplayImage();

                var DisplayImageList = bu.GetDisplayImage(x => x.AutoID == Convert.ToInt32(txtAutoID.Text)); //

                if (DisplayImageList.Count > 0)
                {
                    DisplayImage = DisplayImageList[0];
                    DisplayImage.EdDate = DateTime.Now;
                    DisplayImage.EdUser = Helper.tbl_Users.Username;
                }
                else
                {
                    var MaxID = bu.GetDisplayImage();

                    DisplayImage.AutoID = Convert.ToInt32(txtAutoID.Text);
                    
                    DisplayImage.CrDate = DateTime.Now;
                    DisplayImage.CrUser = Helper.tbl_Users.Username;

                    DisplayImage.EdDate = null;
                    DisplayImage.EdUser = null;
                }
 
                PrePareDisplayImage(DisplayImage);

                ret = bu.UpdateDisplayImageData(DisplayImage);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    OpenPanelSearch(true);
                    ReadOnlyPanelEdit(true);
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
        private void ClearPanelEdit()
        {
            numericUpDown1.Value = 0;
            txtAutoID.Clear();
            txtName.Clear();
            txtDescription.Clear();
            DisplayImagePicture.Image = null;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            OpenPanelSearch(true);
            ReadOnlyPanelEdit(true);

            grdDisplayImage.Enabled = true;

            btnSearch.PerformClick();
        }
        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
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

                var DisplayImage = new tbl_DisplayImage();

                var DisplayImageList = bu.GetDisplayImage(x => x.AutoID == Convert.ToInt32(txtAutoID.Text)); //

                if (DisplayImageList.Count > 0)
                {
                    DisplayImage = DisplayImageList[0];

                    DisplayImage.EdDate = DateTime.Now;
                    DisplayImage.EdUser = Helper.tbl_Users.Username;

                    if (flagRemove == true)
                    {
                        DisplayImage.FlagDel = true;
                    }
                    else
                    {
                        DisplayImage.FlagDel = false;
                    }

                    ret = bu.UpdateDisplayImageData(DisplayImage);
                }
                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    if (flagRemove == false)
                    {
                        rdoC.Checked = false;
                        rdoC.Checked = true;
                    }
                    else
                    {
                        btnSearch.PerformClick();
                    }
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
        private void btnN_Click(object sender, EventArgs e)
        {
            Remove(false);
        }

        private void frmInfomation_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
