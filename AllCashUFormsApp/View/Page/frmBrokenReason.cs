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
    public partial class frmBrokenReason : Form
    {
        MasterDataControl bu = new MasterDataControl();
        MenuBU menuBU = new MenuBU();
        Dictionary<Control, Label> ValidateSaveCtrls = new Dictionary<Control, Label>();
        public frmBrokenReason()
        {
            InitializeComponent();
            ValidateSaveCtrls.Add(txtRemarkRejectID, lbl_Code);
            ValidateSaveCtrls.Add(txtRemarkRejectName, lbl_Name);
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

            grdPrdRemarkReject.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void SetButton()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }
        private void frmBrokenReason_Load(object sender, EventArgs e)
        {
            InitPage();
            txtRemarkRejectID.DisableTextBox(true);
            txtRemarkRejectName.DisableTextBox(true);
            SetButton();
            pnlStatus.Visible = false;
        }
        private void ClearTextBoxEdit()
        {
            txtRemarkRejectID.Clear();
            txtRemarkRejectName.Clear();
        }
        private void BindData()
        {
            ClearTextBoxEdit();

            DataTable dt = new DataTable();

            int flagDel = rdoN.Checked ? 0 : 1;

            dt = bu.GetProductRemarkRejectData(txtSearch.Text, flagDel);

            btnEdit.Enabled = true;

            pnlStatus.Visible = false;

            btnAdd.Enabled = true;

            btnRemove.Enabled = true;

            if (rdoN.Checked == true)
            {
                if (dt.Rows.Count == 0)
                {
                    btnRemove.Enabled = false;
                    btnEdit.Enabled = false;
                }
            }
            else
            {
                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnSave.Enabled = false;
                btnCancel.Enabled = false;

                if (dt.Rows.Count > 0)
                {
                    pnlStatus.Visible = true;
                }
            }

            grdPrdRemarkReject.DataSource = dt;
            lblgrdQty.Text = dt.Rows.Count.ToNumberFormat();
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindData();
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        private void grdPrdRemarkReject_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPrdRemarkReject.SetRowPostPaint(sender, e, this.Font);
        }
        private void SelectPrdRemarkRejctDetails(DataGridViewCellEventArgs e)
        {
            DataGridViewRow grdRow = null;

            if (e != null)
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                else
                {
                    grdRow = grdPrdRemarkReject.Rows[e.RowIndex];
                }
            }
            else
            {
                grdRow = grdPrdRemarkReject.CurrentRow;
            }

            if (grdRow != null)
            {
                txtRemarkRejectID.Text = grdRow.Cells["colRemarkRejectID"].Value.ToString();
                txtRemarkRejectName.Text = grdRow.Cells["colRemarkRejectName"].Value.ToString();
            }
        }
        private void grdPrdRemarkReject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectPrdRemarkRejctDetails(e);
        }
        private void grdPrdRemarkReject_SelectionChanged(object sender, EventArgs e)
        {
            SelectPrdRemarkRejctDetails(null);
        }
        private void OpenPanelGridView(bool flagEnable)
        {
            txtSearch.DisableTextBox(!flagEnable);
            btnSearch.Enabled = flagEnable;
            rdoN.Enabled = flagEnable;
            rdoC.Enabled = flagEnable;
        }
        private void FormatRunningNo()
        {
            var prdRemark = bu.GetProductRemarkReject();
            if (prdRemark.Count > 0)
            {
                txtRemarkRejectID.Text = (prdRemark.Select(x => x.RemarkRejectID).Max() + 1).ToString();
            }
            else
            {
                txtRemarkRejectID.Text = "1";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            OpenPanelGridView(false);

            ClearTextBoxEdit();
            txtRemarkRejectName.Focus();

            FormatRunningNo();//

            txtRemarkRejectID.DisableTextBox(true);
            txtRemarkRejectName.DisableTextBox(false);


            grdPrdRemarkReject.Enabled = false;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            OpenPanelGridView(false);

            txtRemarkRejectName.DisableTextBox(false);
            txtRemarkRejectName.Focus();
        }
        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) 
            {
                errList.SetErrMessage(ValidateSaveCtrls);
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
            try
            {
                int ret = 0;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                var PrdRemarkRejectList = bu.GetProductRemarkReject(x => x.RemarkRejectID == Convert.ToInt32(txtRemarkRejectID.Text));

                var PrdRemarkReject = new tbl_ProductRemarkReject();

                if (PrdRemarkRejectList.Count > 0)
                {
                    PrdRemarkReject = PrdRemarkRejectList[0];

                    PrdRemarkReject.RemarkRejectID = Convert.ToInt32(txtRemarkRejectID.Text);
                    PrdRemarkReject.RemarkRejectName = txtRemarkRejectName.Text;
                   
                    PrdRemarkReject.EdDate = DateTime.Now;
                    PrdRemarkReject.EdUser = Helper.tbl_Users.Username;
                }
                else
                {
                    PrdRemarkReject.RemarkRejectID = Convert.ToInt32(txtRemarkRejectID.Text);

                    PrdRemarkReject.RemarkRejectName = txtRemarkRejectName.Text;

                    PrdRemarkReject.CrDate = DateTime.Now;//
                    PrdRemarkReject.CrUser = Helper.tbl_Users.Username;//

                    PrdRemarkReject.EdDate = null;
                    PrdRemarkReject.EdUser = null;

                    PrdRemarkReject.FlagDel = false;
                    PrdRemarkReject.FlagSend = false;
                }

                ret = bu.UpdateProductRemarkRejectData(PrdRemarkReject);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    msg.ShowInfoMessage();

                    ClearTextBoxEdit();

                    txtRemarkRejectID.DisableTextBox(true);
                    txtRemarkRejectName.DisableTextBox(true);

                    OpenPanelGridView(true);

                    grdPrdRemarkReject.Enabled = true;

                    SetButton();

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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Remove(bool flagRemove = true)
        {
            try
            {
                if (flagRemove == true)
                {
                    string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
                    string title = "ทำการยืนยัน!!";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;
                }
               
                int ret = 0;

                var PrdRemarkReject = bu.GetProductRemarkReject(x=>x.RemarkRejectID == Convert.ToInt32(txtRemarkRejectID.Text));

                if (PrdRemarkReject.Count > 0)
                {
                    PrdRemarkReject[0].EdDate = DateTime.Now;
                    PrdRemarkReject[0].EdUser = Helper.tbl_Users.Username;

                    if (flagRemove == true)
                    {
                        PrdRemarkReject[0].FlagDel = true;
                    }
                    else
                    {
                        PrdRemarkReject[0].FlagDel = false;
                    }

                    ret = bu.UpdateProductRemarkRejectData(PrdRemarkReject[0]);

                    if (ret == 1)
                    {
                        string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                        msg.ShowInfoMessage();

                        if (flagRemove == true)
                        {
                            btnSearch.PerformClick();
                        }
                        else
                        {
                            rdoC.Checked = false;
                            rdoC.Checked = true;
                        }
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
        private void btnRemove_Click(object sender, EventArgs e)
        {
            Remove();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetButton();
            OpenPanelGridView(true);

            ClearTextBoxEdit();

            txtRemarkRejectID.DisableTextBox(true);
            txtRemarkRejectName.DisableTextBox(true);

            btnSearch.PerformClick();
            grdPrdRemarkReject.Enabled = true;
        }
        private void btnN_Click(object sender, EventArgs e)
        {
            Remove(false);
        }

        private void frmBrokenReason_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
