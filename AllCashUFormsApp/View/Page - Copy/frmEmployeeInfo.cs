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
    public partial class frmEmployeeInfo : Form
    {
        MenuBU menuBU = new MenuBU();
        DistributionCenter bu = new DistributionCenter();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();

        List<Control> saleEmpList = new List<Control>();
        List<Control> driverEmpList = new List<Control>();
        List<Control> helperEmpList = new List<Control>();

        List<tbl_SalArea> tbl_SalAreaList = new List<tbl_SalArea>();

        private List<string> selectProdect = new List<string>();

        Dictionary<Control, Label> validateDepoCtrls = new Dictionary<Control, Label>();
        List<string> readOnlyDepoControls = new List<string>();

        Dictionary<Control, Label> validateEmpCtrls = new Dictionary<Control, Label>();
        List<string> readOnlyEmpControls = new List<string>();

        Dictionary<Control, Label> validateBWHCtrls = new Dictionary<Control, Label>();
        List<string> readOnlyBWHControls = new List<string>();

        Dictionary<Control, Label> validateVANCtrls = new Dictionary<Control, Label>();
        List<string> readOnlyVANDTControls = new List<string>();

        Dictionary<Control, Label> validateMKTCtrls = new Dictionary<Control, Label>();
        List<string> readOnlyMKTControls = new List<string>();

        public frmEmployeeInfo()
        {
            InitializeComponent();

            readOnlyEmpControls = new string[] { txtEmpID.Name }.ToList();

            validateEmpCtrls.Add(txtEmpID, lblEmpID);
            validateEmpCtrls.Add(ddlTitleName, lblTitleName);
            validateEmpCtrls.Add(txtFirstName, lblFirstName);
            validateEmpCtrls.Add(ddlDepartmentID, lblDepartmentID);
            validateEmpCtrls.Add(ddlPositionID, lblPositionID);
            validateEmpCtrls.Add(ddlDepo, lblDepo);
            validateEmpCtrls.Add(txtUsername, lblUsername);
            validateEmpCtrls.Add(txtPassword, lblPassword);
            validateEmpCtrls.Add(ddlRoleID, lblRoleID);

            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnRemove.Click += btnRemove_Click;
            btnCopy.Click += btnCopy_Click;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            btnPrint.Click += btnPrint_Click;
            btnExcel.Click += btnExcel_Click;
            btnClose.Click += btnClose_Click;

            txtSearchEmp.KeyDown += txtSearchEmp_KeyDown;
            btnSearchEmp.Click += btnSearchEmp_Click;
            rdoEmpN.CheckedChanged += rdoEmpN_CheckedChanged;
            rdoEmpC.CheckedChanged += rdoEmpC_CheckedChanged;
            ddlDepartment.SelectedIndexChanged += ddlDepartment_SelectedIndexChanged;
            ddlPosition.SelectedIndexChanged += ddlPosition_SelectedIndexChanged;

            grdEmpList.CellClick += grdEmpList_CellClick;
            grdEmpList.SelectionChanged += grdEmpList_SelectionChanged;
            grdEmpList.RowPostPaint += grdEmpList_RowPostPaint;
        }

        #region Medhods

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

            btnAdd.Enabled = true;

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnCopy.Enabled = false;


            var titleList = new Dictionary<string, string>();
            titleList = bu.GetAllTitleName();
            ddlTitleName.BindDropdownList(titleList, "key", "value");

            var departmentList = new List<tbl_Department>();
            var _departmentList = bu.GetAllDepartment();
            departmentList.Add(new tbl_Department { DepartmentID = -1, DepartmentName = "==เลือก==" });
            departmentList.AddRange(_departmentList);

            ddlDepartment.BindDropdownList(departmentList, "DepartmentName", "DepartmentID");

            departmentList = new List<tbl_Department>();
            departmentList.AddRange(_departmentList);
            ddlDepartmentID.BindDropdownList(departmentList, "DepartmentName", "DepartmentID");

            var positionList = new List<tbl_Position>();
            var _positionList = bu.GetAllPosition();
            positionList.Add(new tbl_Position { PositionID = -1, PositionName = "==เลือก==" });
            positionList.AddRange(_positionList);

            ddlPosition.BindDropdownList(positionList, "PositionName", "PositionID");

            positionList = new List<tbl_Position>();
            positionList.AddRange(_positionList);
            ddlPositionID.BindDropdownList(positionList, "PositionName", "PositionID");

            var rolesList = bu.GetAllRoles();
            ddlRoleID.BindDropdownList(rolesList, "RoleName", "RoleID");

            var branchList = bu.GetBranch();
            ddlDepo.BindDropdownList(branchList, "BranchName", "BranchID");

            BindEmployeeData();

            grdEmpList.SetDataGridViewStyle();

            pnlEmpDT.ClearControl();

            pnlEmpDT.OpenControl(false, readOnlyDepoControls.ToArray());

            txtEmpID.Text = ddlDepo.SelectedValue + "Exxx";

            rdoEmpStatusN.Enabled = false;
            rdoEmpStatusC.Enabled = false;
        }

        private void PrepareEmpID()
        {
            string _empID = "";

            var code = bu.GetCompany().BranchID;

            Func<tbl_Employee, bool> tbl_EmployeeFunc = null;
            var tbl_EmployeeList = bu.GetEmployee(tbl_EmployeeFunc);
            if (tbl_EmployeeList != null && tbl_EmployeeList.Count > 0)
            {
                string _no = tbl_EmployeeList.Max(x => x.EmpID);
                var no = Convert.ToInt32(_no.Substring(4, _no.Length - 4)) + 1;
                _empID = code + no.ToString("000");
            }
            else
                _empID = code + "E001";

            txtEmpID.Text = _empID;
        }

        private void RemoveEmployee(string empID)
        {
            try
            {
                int ret = 0;

                string cfMsg = "ต้องการลบข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการลบ!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Employee b = new Employee();
                Func<tbl_Employee, bool> tbl_EmployeeFunc = (x => x.EmpID == empID);
                var tbl_EmployeeList = bu.GetEmployee(tbl_EmployeeFunc);
                if (tbl_EmployeeList != null && tbl_EmployeeList.Count > 0)
                {
                    tbl_Employee eData = new tbl_Employee();
                    eData = tbl_EmployeeList[0];
                    eData.FlagDel = true;

                    ret = b.UpdateData(eData);

                    if (ret == 1)
                    {
                        Users u = new Users();
                        Func<tbl_Users, bool> tbl_UsersFunc = (x => x.EmpID == empID);
                        var tbl_UsersList = bu.GetUser(tbl_UsersFunc);
                        if (tbl_UsersList != null && tbl_UsersList.Count > 0)
                        {
                            tbl_Users uData = new tbl_Users();
                            uData = tbl_UsersList[0];
                            uData.FlagDel = true;

                            ret = u.UpdateData(uData);
                        }
                    }

                    if (ret == 1)
                    {
                        string msg = "ลบข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();

                        btnSearchEmp.PerformClick();
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
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void SaveEmployee()
        {
            try
            {
                int ret = 0;
                tbl_Employee eData = new tbl_Employee();

                bool isEditMode = eData.CheckExistsData(txtEmpID.Text);

                if (!isEditMode && !ValidateSave())
                    return;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                //add employee
                Employee e = new Employee();

                pnlEmpDT.Controls.SetObjectFromControl(eData);

                if (isEditMode)
                {
                    eData.EmpID = txtEmpID.Text;
                }
                else
                {
                    Func<tbl_Employee, bool> tbl_EmployeeFunc = null;
                    var allEmp = bu.GetEmployee(tbl_EmployeeFunc);
                    if (allEmp != null && allEmp.Count > 0)
                    {
                        string _maxEmpID = allEmp.Max(x => x.EmpID);
                        var maxEmpID = Convert.ToInt32(_maxEmpID.Substring(4, _maxEmpID.Length - 4)) + 1;

                        string formateRunNo = "";
                        for (int i = 0; i < 3; i++)
                        {
                            formateRunNo += "0";
                        }
                        string tempRunningNo = maxEmpID.ToString(formateRunNo);

                        eData.EmpID = ddlDepo.SelectedValue + "E" + tempRunningNo;
                    }
                    else
                        eData.EmpID = ddlDepo.SelectedValue + "E000";
                }

                eData.LastName = string.IsNullOrEmpty(txtLastName.Text) ? "" : txtLastName.Text;
                eData.NickName = "";
                eData.EmpCode = eData.EmpID;
                eData.EmpIDCard = txtEmp_ID_Card.Text;
                eData.IDCard = txtIDCard.Text;

                var currentDate = DateTime.Now;
                eData.CrDate = currentDate;
                eData.CrUser = Helper.tbl_Users.Username;
                eData.EdDate = null;
                eData.EdUser = null;

                if (isEditMode)
                {
                    eData.EdDate = DateTime.Now;
                    eData.EdUser = Helper.tbl_Users.Username;
                }

                eData.FlagSend = false;

                if (rdoEmpStatusN.Checked)
                    eData.FlagDel = false;
                else if (rdoEmpStatusC.Checked)
                    eData.FlagDel = true;

                ret = e.UpdateData(eData);

                if (ret == 1) //add user
                {
                    Users u = new Users();
                    tbl_Users uData = new tbl_Users();

                    isEditMode = uData.CheckExistsData(txtUsername.Text);

                    if (isEditMode)
                    {
                        Func<tbl_Users, bool> tbl_UsersFunc = (x => x.Username == txtUsername.Text);
                        var userList = bu.GetUser(tbl_UsersFunc);
                        if (userList != null && userList.Count > 0)
                        {
                            uData = userList[0];
                            uData.FlagDel = rdoEmpStatusC.Checked;
                            uData.Username = txtUsername.Text;
                            uData.Password = txtPassword.Text;
                            uData.EdDate = DateTime.Now;
                            uData.EdUser = Helper.tbl_Users.Username;
                        }
                    }
                    else
                    {
                        gbUsers.Controls.SetObjectFromControl(uData);

                        uData.FirstName = eData.FirstName;
                        uData.LastName = eData.LastName;
                        uData.Email = "";
                        uData.EmpID = eData.EmpID;

                        uData.CrDate = currentDate;
                        uData.CrUser = Helper.tbl_Users.Username;
                        uData.EdDate = null;
                        uData.EdUser = null;

                        uData.FlagSend = false;

                        if (rdoEmpStatusN.Checked)
                            uData.FlagDel = false;
                        else if (rdoEmpStatusC.Checked)
                            uData.FlagDel = true;
                    }

                    ret = u.UpdateData(uData);
                }

                if (ret == 1)
                {
                    pnlEmpDT.OpenControl(false, readOnlyEmpControls.ToArray());

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, "");
                    btnAdd.Enabled = true;
                    btnCopy.Enabled = false;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    btnSearchEmp.PerformClick();
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

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //validate header
            {
                errList.SetErrMessage(validateEmpCtrls);

                if (errList.Count == 0)
                {
                    var fName = txtFirstName.Text;
                    var lName = txtLastName.Text;

                    if (!string.IsNullOrEmpty(fName))
                    {
                        Func<tbl_Employee, bool> tbl_EmployeeFunc = (x => x.FirstName == fName && x.LastName == lName);
                        var emps = bu.GetEmployee(tbl_EmployeeFunc);
                        if (emps != null && emps.Count > 0)
                        {
                            string message = "ชื่อ-นามสกุลซ้ำ กรุณาระบุใหม่ !!!";
                            message.ShowWarningMessage();
                            ret = false;
                            txtFirstName.ErrorTextBox();
                            txtFirstName.Focus();
                        }
                    }

                    if (ret)
                    {
                        Func<tbl_Users, bool> tbl_UsersFunc = (x => x.Username == txtUsername.Text);
                        var users = bu.GetUser(tbl_UsersFunc);
                        if (users != null && users.Count > 0)
                        {
                            string message = "ชื่อเข้าระบบซ้ำ กรุณาระบุใหม่ !!!";
                            message.ShowWarningMessage();
                            ret = false;
                            txtUsername.ErrorTextBox();
                            txtUsername.Focus();
                        }
                    }
                }

                if (errList.Count > 0)
                {
                    string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                    message.ShowWarningMessage();
                    ret = false;
                }
            }

            return ret;
        }

        private void BindEmployeeData()
        {
            string text = txtSearchEmp.Text;

            Func<tbl_Employee, bool> tbl_EmployeeFunc = null;
            if (ddlDepartment.SelectedValue != null && ddlDepartment.SelectedValue.ToString() == "-1")
            {
                tbl_EmployeeFunc = (x => x.FlagDel == rdoEmpC.Checked &&
                x.PositionID == (Convert.ToInt32(ddlPosition.SelectedValue) != -1 ? Convert.ToInt32(ddlPosition.SelectedValue) : x.PositionID) &&
                (x.EmpCode.Contains(text) || x.TitleName.Contains(text) || x.FirstName.Contains(text) || x.LastName.Contains(text)));
            }
            else if (ddlPosition.SelectedValue != null && ddlPosition.SelectedValue.ToString() == "-1")
            {
                tbl_EmployeeFunc = (x => x.FlagDel == rdoEmpC.Checked &&
                x.DepartmentID == (Convert.ToInt32(ddlDepartment.SelectedValue) != -1 ? Convert.ToInt32(ddlDepartment.SelectedValue) : x.DepartmentID) &&
                (x.EmpCode.Contains(text) || x.TitleName.Contains(text) || x.FirstName.Contains(text) || x.LastName.Contains(text)));
            }
            else
            {
                tbl_EmployeeFunc = (x => x.FlagDel == rdoEmpC.Checked &&
                x.DepartmentID == (Convert.ToInt32(ddlDepartment.SelectedValue) != -1 ? Convert.ToInt32(ddlDepartment.SelectedValue) : x.DepartmentID) &&
                x.PositionID == (Convert.ToInt32(ddlPosition.SelectedValue) != -1 ? Convert.ToInt32(ddlPosition.SelectedValue) : x.PositionID) &&
                (x.EmpCode.Contains(text) || x.TitleName.Contains(text) || x.FirstName.Contains(text) || x.LastName.Contains(text)));
            }

            var dt = bu.GetEmpTable(tbl_EmployeeFunc);

            if (dt != null)
            {
                grdEmpList.DataSource = null;

                var grd = grdEmpList;
                grd.DataSource = dt;

                DataGridViewColumn col0 = grd.Columns[0];
                DataGridViewColumn col1 = grd.Columns[1];
                DataGridViewColumn col2 = grd.Columns[2];
                DataGridViewColumn col3 = grd.Columns[3];
                DataGridViewColumn col4 = grd.Columns[4];
                DataGridViewColumn col5 = grd.Columns[5];
                DataGridViewColumn col6 = grd.Columns[6];
                DataGridViewColumn col7 = grd.Columns[7];
                DataGridViewColumn col8 = grd.Columns[8];
                DataGridViewColumn col9 = grd.Columns[9];
                DataGridViewColumn col10 = grd.Columns[10];

                col0.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col1.SetColumnStyle(140, DataGridViewAutoSizeColumnMode.Fill, DataGridViewContentAlignment.MiddleLeft, "", 140);
                col2.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col3.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col4.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col5.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col6.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0);
                col7.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col8.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0);
                col9.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col10.SetColumnStyle(60, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0, new DataGridViewCheckBoxColumn());

                lblEmpCount.Text = dt.Rows.Count.ToNumberFormat();
            }
        }

        private void BindEmployeeDetail()
        {
            if (grdEmpList.RowCount > 0 && grdEmpList.CurrentCell != null)
            {
                int rowIndex = grdEmpList.CurrentCell.RowIndex;

                if (rowIndex != -1)
                {
                    var empCode = grdEmpList.Rows[rowIndex].Cells[0].Value.ToString();
                    Func<tbl_Employee, bool> tbl_EmployeeFunc = (x => x.EmpCode == empCode);
                    var empDT = bu.GetEmpDetails(tbl_EmployeeFunc);

                    if (empDT != null && empDT.Count > 0)
                    {
                        rdoEmpStatusN.Checked = empDT[0].FlagDel == false;
                        rdoEmpStatusC.Checked = empDT[0].FlagDel == true;

                        pnlEmpDT.Controls.SetTextBoxControlValue(empDT[0]);

                        ddlDepartmentID.SelectedValue = empDT[0].DepartmentID;
                        ddlPositionID.SelectedValue = empDT[0].PositionID;

                        if (ddlDepo.Items != null && ddlDepo.Items.Count > 0)
                            ddlDepo.SelectedIndex = 0;

                        ddlRoleID.SelectedValue = empDT[0].RoleID;
                        ddlTitleName.SelectedValue = empDT[0].TitleName;

                        pnlEmpDT.OpenControl(false, readOnlyEmpControls.ToArray());

                        btnSearchEmp.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
                        btnAdd.Enabled = true;
                        btnCopy.Enabled = false;
                        btnRemove.Enabled = true;

                        btnRemove.Enabled = !empDT[0].FlagDel;
                    }
                }
            }
        }

        #endregion

        #region Event

        private void frmEmployeeInfo_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            pnlEmpDT.OpenControl(true, readOnlyEmpControls.ToArray());

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnCopy.Enabled = false;
            pnlEmpDT.ClearControl();

            PrepareEmpID();

            txtEmpID.DisableTextBox(true);

            rdoEmpStatusN.Checked = true;
            rdoEmpStatusC.Checked = false;

            rdoEmpStatusN.Enabled = false;
            rdoEmpStatusC.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            pnlEmpDT.OpenControl(true, readOnlyEmpControls.ToArray());
            btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnCancel.Enabled = true;
            btnCopy.Enabled = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int rowIndex = grdEmpList.CurrentCell.RowIndex;
            if (rowIndex != -1)
            {
                var code = grdEmpList.Rows[rowIndex].Cells[0].Value.ToString();
                RemoveEmployee(code);

                pnlEmpDT.ClearControl();

                txtEmpID.DisableTextBox(true);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveEmployee();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlEmpDT.ClearControl();

            btnAdd.Enabled = true;
            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnCopy.Enabled = false;

            pnlEmpDT.OpenControl(false, readOnlyEmpControls.ToArray());

            PrepareEmpID();
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

        private void grdEmpList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BindEmployeeDetail();
        }

        private void grdEmpList_SelectionChanged(object sender, EventArgs e)
        {
            BindEmployeeDetail();
        }

        private void grdEmpList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdEmpList.SetRowPostPaint(sender, e, this.Font);
        }

        private void rdoEmpN_CheckedChanged(object sender, EventArgs e)
        {
            BindEmployeeData();
        }

        private void rdoEmpC_CheckedChanged(object sender, EventArgs e)
        {
            BindEmployeeData();
        }

        private void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmployeeData();
        }

        private void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmployeeData();
        }

        private void btnSearchEmp_Click(object sender, EventArgs e)
        {
            BindEmployeeData();
        }

        private void txtSearchEmp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BindEmployeeData();
        }

        #endregion


    }
}
