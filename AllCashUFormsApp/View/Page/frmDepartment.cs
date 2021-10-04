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
    public partial class frmDepartment : Form
    {
        MasterDataControl bu = new MasterDataControl();
        MenuBU menuBU = new MenuBU();
        List<string> PanelEditControls = new List<string>();
        Dictionary<Control, Label> ValidateSaveCtrls = new Dictionary<Control, Label>();
        public frmDepartment()
        {
            InitializeComponent();
            PanelEditControls = new string[] { txtDepartmentCode.Name }.ToList();
            ValidateSaveCtrls.Add(txtDepartmentCode, lbl_Code);
            ValidateSaveCtrls.Add(txtDepartmentName, lbl_Name);
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

            grdDepartment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
        private void SetButton()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }
        private void InitialData()
        {
            OpenPanelEdit(false);
            SetButton();
            pnlStatus.Visible = false;
        }
        private void frmDepartment_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }
        private void ClearTextboxEdit()
        {
            txtDepartmentCode.Clear();
            txtDepartmentName.Clear();
            txtDepartmentDesc.Clear();
        }
        private void BindData()
        {
            ClearTextboxEdit();

            DataTable dt = new DataTable();

            int flagDel = rdoN.Checked ? 0 : 1;

            dt = bu.GetDepartmentData(flagDel, txtSearch.Text);

            grdDepartment.DataSource = dt;
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
        private void grdDepartment_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdDepartment.SetRowPostPaint(sender, e, this.Font);
        }
        private void SelectDepartmentDetails(DataGridViewCellEventArgs e)
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
                    grdRows = grdDepartment.Rows[e.RowIndex];
                }
            }
            else
            {
                grdRows = grdDepartment.CurrentRow;
            }
 
            if (grdRows != null)
            {
                txtDepartmentCode.Text = grdRows.Cells["colDepartmentCode"].Value.ToString();
                txtDepartmentName.Text = grdRows.Cells["colDepartmentName"].Value.ToString();
                txtDepartmentDesc.Text = grdRows.Cells["colDepartmentDesc"].Value.ToString();
            }
        }
        private void grdDepartment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectDepartmentDetails(e);
        }
        private void grdDepartment_SelectionChanged(object sender, EventArgs e)
        {
            SelectDepartmentDetails(null);
        }
        private void PrePareRunningNo()
        {
            var Departments = bu.GetDepartment();

            if (Departments.Count > 0)
            {
                int maxDeptCode = Convert.ToInt32(Departments.Select(x => x.DepartmentCode).Max()) + 1;

                if (maxDeptCode <= 9)
                {
                    txtDepartmentCode.Text = "0" + (maxDeptCode + 1);
                }
                else
                {
                    txtDepartmentCode.Text = maxDeptCode.ToString();
                }
            }
            else
            {
                txtDepartmentCode.Text = "01";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            OpenPanelGridView(false);
            OpenPanelEdit(true);
            
            grdDepartment.Enabled = false;

            ClearTextboxEdit();

            txtDepartmentName.Focus();

            PrePareRunningNo();
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

            txtDepartmentCode.DisableTextBox(true);
            txtDepartmentCode.Focus();
        }
        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //true
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
        private void Save()
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

                var DepartmentList = bu.GetDepartment(x => x.DepartmentCode == txtDepartmentCode.Text);

                var Department = new tbl_Department();

                if (DepartmentList.Count > 0) 
                {
                    Department = DepartmentList[0];
                    PrePare_Department(Department);
                    Department.EdDate = DateTime.Now;//
                    Department.EdUser = Helper.tbl_Users.Username;//
                }
                else //New Department
                {
                    var deptList = bu.GetDepartment();

                    if (deptList.Count > 0)
                    {
                        Department.DepartmentID = deptList.Select(x => x.DepartmentID).Max() + 1;
                    }
                    else
                    {
                        Department.DepartmentID = 1;
                    }

                    PrePare_Department(Department);

                    Department.CrDate = DateTime.Now;//
                    Department.CrUser = Helper.tbl_Users.Username;//

                    Department.EdDate = null;//
                    Department.EdUser = null;//

                    Department.FlagDel = false;
                    Department.FlagSend = false;//
                }

                ret = bu.UpdateDepartmentData(Department);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    msg.ShowInfoMessage();
                    OpenPanelEdit(false);
                    OpenPanelGridView(true);
                    grdDepartment.Enabled = true;
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
        private void PrePare_Department(tbl_Department Department,bool EditData = false)
        {
            Department.DepartmentCode = txtDepartmentCode.Text;
            Department.DepartmentName = txtDepartmentName.Text;
            Department.DepartmentDesc = txtDepartmentDesc.Text;

            if (EditData == true)
            {
                if (grdDepartment.CurrentRow.Cells["colCrDate"].Value != null)
                {
                    Department.CrDate = Convert.ToDateTime(grdDepartment.CurrentRow.Cells["colCrDate"].Value);
                }
                else
                {
                    Department.CrDate = null;
                }
                Department.CrUser = grdDepartment.CurrentRow.Cells["colCrUser"].Value.ToString();

                Department.EdDate = DateTime.Now;
                Department.EdUser = Helper.tbl_Users.Username;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearTextboxEdit();
            OpenPanelGridView(true);

            SetButton();
            btnSearch.PerformClick();
            OpenPanelEdit(false);

            grdDepartment.Enabled = true;
        }
        private void btnN_Click(object sender, EventArgs e)
        {
            Remove(false);
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

                int DepartmentID = Convert.ToInt32(grdDepartment.CurrentRow.Cells["colDepartmentID"].Value);

                var Department = bu.GetDepartment(x => x.DepartmentID == DepartmentID);

                if (Department.Count > 0)
                {
                    if (flagRemove == true)
                    {
                        Department[0].FlagDel = true;
                    }
                    else
                    {
                        Department[0].FlagDel = false;
                    }
                    
                    Department[0].EdDate = DateTime.Now;
                    Department[0].EdUser = Helper.tbl_Users.Username;

                    ret = bu.UpdateDepartmentData(Department[0]);
                }

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
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
        private void txtDepartmentCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void frmDepartment_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
