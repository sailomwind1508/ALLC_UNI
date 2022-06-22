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
    public partial class frmPermission : Form
    {
        MenuBU menuBU = new MenuBU();
        Permission bu = new Permission();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();

        string docTypeCode = "";
        int runDigit = 0;

        public frmPermission()
        {
            InitializeComponent();
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
            btnAdd.Enabled = false;
            btnCopy.Enabled = false;
            btnRemove.Enabled = false;
            btnCancel.Enabled = false;

            var headerPic = bu.tbl_MstMenu.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            InitialData();

            grdPermission.SetDataGridViewStyle();
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            InitHeader();
        }

        private void InitHeader()
        {
            var allRoles = bu.GetAllRoles().Where(x => !x.FlagDel).ToList();
            ddlRoles.BindDropdownList(allRoles, "RoleName", "RoleID", 0);

            lblCountRow.Text = "0";
        }

        private void Search()
        {
            int roleID = 0;
            try
            {
                roleID = Convert.ToInt32(ddlRoles.SelectedValue);
            }
            catch
            {

            }

            DataTable dt = bu.GetDataTable(x => x.RoleID == roleID);
            grdPermission.DataSource = dt;

            if (grdPermission.Columns["colVisible"] != null)
                grdPermission.Columns["colVisible"].ReadOnly = true;
            if (grdPermission.Columns["colEnable"] != null)
                grdPermission.Columns["colEnable"].ReadOnly = true;

            lblCountRow.Text = grdPermission.Rows.Count.ToNumberFormat();
        }

        private void Save()
        {
            try
            {
                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                List<tbl_AdmRoleControl> tbl_AdmRoleControls = new List<tbl_AdmRoleControl>();

                for (int i = 0; i < grdPermission.RowCount; i++)
                {
                    var roleIDCell = grdPermission.Rows[i].Cells["colRoleID"];
                    var controlIDCell = grdPermission.Rows[i].Cells["colControlID"];
                    var visibleCell = grdPermission.Rows[i].Cells["colVisible"];
                    var enableCell = grdPermission.Rows[i].Cells["colEnable"];

                    if (roleIDCell.IsNotNullOrEmptyCell() && controlIDCell.IsNotNullOrEmptyCell())
                    {
                        var rcl = new tbl_AdmRoleControl();
                        rcl.RoleID = Convert.ToInt32(roleIDCell.Value);
                        rcl.ControlID = Convert.ToInt32(controlIDCell.Value);
                        rcl.Visible = Convert.ToBoolean(visibleCell.Value);
                        rcl.Enable = Convert.ToBoolean(enableCell.Value);
                        rcl.DefaultValue = string.Empty;
                        tbl_AdmRoleControls.Add(rcl);
                    }
                }

                if (tbl_AdmRoleControls.Count > 0)
                {
                    int ret = bu.UpdateAdmRoleControl(tbl_AdmRoleControls);
                    if (ret == 1)
                    {
                        btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, "");

                        string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();

                        CancelEvent();
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

        private void CancelEvent()
        {
            Search();

            btnAdd.Enabled = false;
            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnEdit.Enabled = true;
        }

        #endregion

        #region event methods

        private void frmPermission_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CancelEvent();
        }


        private void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbo = sender as ComboBox;
            if (!string.IsNullOrEmpty(cbo.SelectedValue.ToString()))
            {
                CancelEvent();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InitialData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint);

            ddlRoles.Enabled = true;
            btnCancel.Enabled = true;

            grdPermission.Columns["colVisible"].ReadOnly = false;
            grdPermission.Columns["colEnable"].ReadOnly = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelEvent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdPermission_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPermission.SetRowPostPaint(sender, e, this.Font);
        }


        #endregion

        private void frmPermission_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
