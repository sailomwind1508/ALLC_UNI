using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
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
using System.Globalization;


namespace AllCashUFormsApp.View.Page
{
    public partial class frmUser : Form
    {
        MenuBU menuBU = new MenuBU();
        Users bu = new Users();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();

        List<Control> empList = new List<Control>();
        Dictionary<Control, Label> validateUserCtrls = new Dictionary<Control, Label>();
        List<string> readOnlyUserControls = new List<string>();

        string docTypeCode = "";
        int runDigit = 0;

        public frmUser()
        {
            InitializeComponent();

            readOnlyUserControls = new string[] { txtEmpName.Name }.ToList();

            validateUserCtrls.Add(txtUsername, lblUsername);
            validateUserCtrls.Add(txtPassword, lblPassword);
            validateUserCtrls.Add(txtEmpCode, lblEmpID);
            empList = new List<Control>() { txtEmpCode, txtEmpName };
        }

        #region private methods

        private void InitPage()
        {
            var menu = bu.tbl_AdmFormList.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "1");
            btnAdd.Enabled = true;
            btnCopy.Enabled = false;
            btnRemove.Enabled = false;
            btnCancel.Enabled = false;
            btnEdit.Enabled = false;

            var headerPic = bu.tbl_MstMenu.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            InitialData();

            grdUserList.SetDataGridViewStyle();
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            InitHeader();
        }

        private void InitHeader()
        {
            var rolesList = bu.GetAllRoles();
            ddlRoleID.BindDropdownList(rolesList, "RoleName", "RoleID");

            pnluDT.OpenControl(false, readOnlyUserControls.ToArray());
        }

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //validate header
            {
                errList.SetErrMessage(validateUserCtrls);

                if (errList.Count > 0)
                {
                    string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                    message.ShowWarningMessage();
                    ret = false;
                }
            }

            return ret;
        }

        private void Save()
        {
            try
            {
                int ret = 0;
                tbl_Users uData = new tbl_Users();

                if (!ValidateSave())
                    return;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                pnluDT.Controls.SetObjectFromControl(uData);
                var cDate = DateTime.Now;

                uData.Username = txtUsername.Text;
                uData.Password = txtPassword.Text;

                string fullName = txtEmpName.Text;
                var splName = fullName.Split(' ');
                if (splName.Length == 3)
                {
                    uData.FirstName = splName[1].ToString();
                    uData.LastName = splName[2].ToString();
                }
                if (splName.Length == 2 || splName.Length == 1)
                {
                    uData.FirstName = splName[1].ToString();
                    uData.LastName = string.Empty;
                }

                uData.EmpID = txtEmpCode.Text;
                uData.RoleID = Convert.ToInt32(ddlRoleID.SelectedValue);

                uData.CrDate = cDate;
                uData.CrUser = Helper.tbl_Users.Username;
                uData.EdDate = null;
                uData.EdUser = null;

                for (int i = 0; i < grdUserList.Rows.Count; i++)
                {
                    var cellUserID = grdUserList.Rows[i].Cells["colUserID"];
                    var cellUserName = grdUserList.Rows[i].Cells["colUsername"];
                    var cellCancel = grdUserList.Rows[i].Cells["colFlagDel"];

                    if (cellCancel.IsNotNullOrEmptyCell() && cellUserName.IsNotNullOrEmptyCell())
                    {
                        if (cellUserName.Value.ToString() == uData.Username)
                        {
                            if (cellCancel is DataGridViewCheckBoxCell)
                            {
                                DataGridViewCheckBoxCell chk = cellCancel as DataGridViewCheckBoxCell;
                                uData.FlagDel = Convert.ToBoolean(chk.Value);

                                if (cellUserID.IsNotNullOrEmptyCell())
                                {
                                    uData.UserID = Convert.ToInt32(cellUserID.Value);
                                }
                            }
                        }
                    }
                }

                ret = bu.UpdateData(uData);

                if (ret == 1)
                {
                    pnluDT.OpenControl(false, readOnlyUserControls.ToArray());

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, "");
                    btnAdd.Enabled = true;
                    btnCopy.Enabled = false;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

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
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void BindUserData()
        {
            string text = txtSearch.Text;
            var dt = bu.GetFastDataTable();

            DataTable _dt = new DataTable();
            _dt = dt.Clone();
            DataRow[] filteredRows = null;

            filteredRows = dt.Select(string.Format("ยกเลิก = {0} AND ({1} LIKE '%{2}%' OR {3} LIKE '%{4}%' OR {5} LIKE '%{6}%')", rdoUserC.Checked, "ชื่อผู้ใช้งาน", text, "รหัสพนักงาน", text, "ชื่อพนักงาน", text));
            if (filteredRows != null)
            {
                _dt.AddDataTableRow(ref filteredRows);
            }

            if (_dt != null)
            {
                grdUserList.AutoGenerateColumns = false;
                grdUserList.DataSource = null;
                var grd = grdUserList;
                grd.DataSource = _dt;

                lblCountRow.Text = _dt.Rows.Count.ToNumberFormat();
            }

            btnAdd.Enabled = true;
            btnEdit.Enabled = true;

            grdUserList.Columns["colFlagDel"].ReadOnly = txtUsername.ReadOnly;
        }

        private void BindUserDetail()
        {
            if (grdUserList.RowCount > 0 && grdUserList.CurrentCell != null)
            {
                int rowIndex = grdUserList.CurrentCell.RowIndex;

                if (rowIndex != -1)
                {
                    var cell0 = grdUserList.Rows[rowIndex].Cells["colUserID"];
                    var cell2 = grdUserList.Rows[rowIndex].Cells["colEmpCode"];
                    var cell3 = grdUserList.Rows[rowIndex].Cells["colEmpName"];
                    var cellRoleID = grdUserList.Rows[rowIndex].Cells["colRoleID"];

                    if (cell0.IsNotNullOrEmptyCell())
                    {
                        var userID = cell0.Value.ToString();
                        Func<tbl_Users, bool> tbl_UsersFunc = (x => x.UserID.ToString() == userID);
                        var uDT = bu.GetUser(tbl_UsersFunc);

                        if (uDT != null && uDT.Count > 0)
                        {
                            pnluDT.Controls.SetTextBoxControlValue(uDT[0]);

                            txtEmpCode.Text = cell2.Value.ToString();
                            txtEmpName.Text = cell3.Value.ToString();
                            ddlRoleID.SelectedValue = Convert.ToInt32(cellRoleID.Value);

                            pnluDT.OpenControl(false, readOnlyUserControls.ToArray());

                            btnSearch.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
                            btnAdd.Enabled = true;
                            btnCopy.Enabled = false;
                            btnRemove.Enabled = false;
                        }
                    }
                }
            }
        }

        #endregion

        #region event methods

        private void frmUser_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindUserData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            grdUserList.ClearSelection();

            pnluDT.OpenControl(true, readOnlyUserControls.ToArray());

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnCopy.Enabled = false;

            pnluDT.ClearControl();

            txtEmpName.DisableTextBox(true);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            pnluDT.OpenControl(true, readOnlyUserControls.ToArray());
            btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");

            grdUserList.Columns["colFlagDel"].ReadOnly = txtUsername.ReadOnly;

            btnCancel.Enabled = true;
            btnCopy.Enabled = false;

            txtUsername.DisableTextBox(true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnluDT.ClearControl();

            btnAdd.Enabled = true;
            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnCopy.Enabled = false;

            pnluDT.OpenControl(false, readOnlyUserControls.ToArray());
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchEmp_Click(object sender, EventArgs e)
        {
            this.OpenEmployeePopup(empList, "เลือกพนักงาน", null);
        }

        private void txtSearchEmp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("Employee", empList, txtEmpCode.Text);
            }
        }

        private void grdUserList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //BindUserDetail();
        }

        private void grdUserList_SelectionChanged(object sender, EventArgs e)
        {
            BindUserDetail();
            grdUserList.Columns["colFlagDel"].ReadOnly = txtUsername.ReadOnly;
        }

        private void grdUserList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdUserList.SetRowPostPaint(sender, e, this.Font);
        }

        private void rdoUserN_CheckedChanged(object sender, EventArgs e)
        {
            BindUserData();
        }

        private void rdoUserC_CheckedChanged(object sender, EventArgs e)
        {
            BindUserData();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BindUserData();
        }

        #endregion

        private void frmUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
