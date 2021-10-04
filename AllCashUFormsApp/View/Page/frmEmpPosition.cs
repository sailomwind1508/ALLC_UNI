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
    public partial class frmEmpPosition : Form
    {
        MasterDataControl bu = new MasterDataControl();
        MenuBU menuBU = new MenuBU();
        Dictionary<Control, Label> ValidateSaveCtrls = new Dictionary<Control, Label>();
        List<string> PanelEditControls = new List<string>();
        public frmEmpPosition()
        {
            InitializeComponent();
            PanelEditControls = new string[] { txtPositionCode.Name }.ToList();
            ValidateSaveCtrls.Add(txtPositionCode, lbl_Code);
            ValidateSaveCtrls.Add(txtPositionName, lbl_Name);
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
            grdPosition.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void OpenPanelEdit(bool flagEnable)
        {
            pnl_Edit.OpenControl(flagEnable, PanelEditControls.ToArray());

            foreach (Control item in pnl_Edit.Controls)
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
        private void PrePareButton()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }
        private void InitialData()
        {
            OpenPanelEdit(false);
            PrePareButton();
            pnlStatus.Visible = false;
        }
        private void frmEmpPosition_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }
        private void ClearPanelEdit()
        {
            txtPositionCode.Clear();
            txtPositionName.Clear();
            txtPositionDesc.Clear();
        }
        private void BindData()
        {
            ClearPanelEdit();

            DataTable dt = new DataTable();

            int flagDel = rdoN.Checked ? 0 : 1;
            
            dt = bu.GetPositionData(flagDel,txtSearch.Text);

            grdPosition.DataSource = dt;
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
                pnlStatus.Visible = true;//
            }
            else if (rdoC.Checked && dt.Rows.Count == 0)
            {
                btnAdd.Enabled = false;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        private void SelectPositionDetails(DataGridViewCellEventArgs e)
        {
            if (e != null)
            {
                if (e.RowIndex == -1)
                    return;
            }

            DataGridViewRow grdRows = null;

            if (e != null)
            {
                grdRows = grdPosition.Rows[e.RowIndex];
            }
            else
            {
                grdRows = grdPosition.CurrentRow;
            }

            txtPositionCode.Text = grdRows.Cells["colPositionCode"].Value.ToString();
            txtPositionName.Text = grdRows.Cells["colPositionName"].Value.ToString();
            txtPositionDesc.Text = grdRows.Cells["colPositionDesc"].Value.ToString();
        }
        private void grdPosition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectPositionDetails(e);
        }
        private void grdPosition_SelectionChanged(object sender, EventArgs e)
        {
            SelectPositionDetails(null);
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
        private void txtPositionCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }
        private void grdPosition_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPosition.SetRowPostPaint(sender, e, this.Font);
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
        private void PrePare_Position(tbl_Position Position)
        {
            Position.PositionCode = txtPositionCode.Text;
            Position.PositionName = txtPositionName.Text;
            Position.PositionDesc = txtPositionDesc.Text;

        }
        private void Save()
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

                var PositionList = bu.GetPosition(x => x.PositionCode == txtPositionCode.Text);

                var Position = new tbl_Position();

                if (PositionList.Count == 0)
                {
                    var positionList = bu.GetPosition();

                    if (positionList.Count > 0)
                    {
                        Position.PositionID = positionList.Select(x => x.PositionID).Max() + 1;
                    }
                    else
                    {
                        Position.PositionID = 1;
                    }

                    PrePare_Position(Position);

                    Position.CrDate = DateTime.Now;//
                    Position.CrUser = Helper.tbl_Users.Username;//

                    Position.EdDate = null;//
                    Position.EdUser = null;//

                    Position.FlagDel = false;
                    Position.FlagSend = false;///
                }
                else
                {
                    Position = PositionList[0];
                    PrePare_Position(Position);
                    Position.EdDate = DateTime.Now;//
                    Position.EdUser = Helper.tbl_Users.Username;//
                }

                ret = bu.UpdatePositionData(Position);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    msg.ShowInfoMessage();

                    OpenPanelEdit(false);
                    OpenPanelGridView(true);

                    grdPosition.Enabled = true;

                    PrePareButton();

                    BindData();
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
        private void PrePareRunningNo()
        {
            var positionList = bu.GetPosition();

            if (positionList.Count > 0)
            {
                int maxPositionCode = Convert.ToInt32(positionList.Select(x => x.PositionCode).Max()) + 1;

                if (maxPositionCode <= 9)
                {
                    txtPositionCode.Text = "0" + (maxPositionCode + 1);
                }
                else
                {
                    txtPositionCode.Text = maxPositionCode.ToString();
                }
            }
            else
            {
                txtPositionCode.Text = "01";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearPanelEdit();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            OpenPanelGridView(false);

            grdPosition.Enabled = false;

            OpenPanelEdit(true);
            
            PrePareRunningNo();

            txtPositionName.Focus();
        }
        private void OpenPanelGridView(bool flagEnable)
        {
            txtSearch.DisableTextBox(!flagEnable);
            btnSearch.Enabled = flagEnable;
            rdoN.Enabled = flagEnable;
            rdoC.Enabled = flagEnable;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            txtSearch.DisableTextBox(true);

            OpenPanelGridView(false);
            OpenPanelEdit(true);

            txtPositionCode.DisableTextBox(true);
            txtPositionCode.Focus();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPanelEdit();
            OpenPanelGridView(true);

            PrePareButton();
            BindData(); // ค้นหาข้อมูลใหม่
            OpenPanelEdit(false);

            grdPosition.Enabled = true;
        }
        private void PrePare_Edit_Position(tbl_Position Position)
        {
            Position.PositionID = Convert.ToInt32(grdPosition.CurrentRow.Cells["colPositionID"].Value);//
            PrePare_Position(Position);

            Position.CrDate = Convert.ToDateTime(grdPosition.CurrentRow.Cells["colCrDate"].Value);//
            Position.CrUser = grdPosition.CurrentRow.Cells["colCrUser"].Value.ToString();//

            Position.EdDate = DateTime.Now;
            Position.EdUser = Helper.tbl_Users.Username;
        }
        private void RemovePosition(bool flagRemove = true)
        {
            try
            {
                if (flagRemove == true) //ยกเลิก
                {
                    string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
                    string title = "ทำการยืนยัน!!";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;
                }

                int ret = 0;

                var Position = new tbl_Position();

                Position.PositionID = Convert.ToInt32(grdPosition.CurrentRow.Cells["colPositionID"].Value);

                PrePare_Edit_Position(Position);

                if (flagRemove == true) //ยกเลิก
                {
                    Position.FlagDel = true;
                }
                else //ปกติ
                {
                    Position.FlagDel = false;
                }

                ret = bu.UpdatePositionData(Position);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    msg.ShowInfoMessage();

                    OpenPanelEdit(true);

                    if (flagRemove == true)
                    {
                        btnSearch.PerformClick();
                    }
                   
                    if (flagRemove == false)
                    {
                        OpenPanelEdit(false);
                        OpenPanelGridView(true);

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
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
        private void Remove()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPositionCode.Text))
                {
                    string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
                    string title = "ทำการยืนยัน!!";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    int ret = 0;

                    var Position = new tbl_Position();

                    Position.PositionID = Convert.ToInt32(grdPosition.CurrentRow.Cells["colPositionID"].Value);

                    PrePare_Edit_Position(Position);

                    Position.FlagDel = true;

                    ret = bu.UpdatePositionData(Position);

                    if (ret == 1)
                    {
                        string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
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
                    string msg = "เลือกข้อมูลตำแหน่งที่ต้องการลบ !!";
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
        private void btnN_Click(object sender, EventArgs e)
        {
            RemovePosition(false);
        }

        private void frmEmpPosition_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
