using AllCashUFormsApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace AllCashUFormsApp.View.UControl
{
    public partial class frmConfirmUpdateTabletData : Form
    {
        Login bu = new Login();
        public frmConfirmUpdateTabletData()
        {
            InitializeComponent();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "รหัสผ่าน")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                frmUpdateSendDate.UserName = "";
                Cursor.Current = Cursors.WaitCursor;

                var tbl_Users = bu.GetAllData().FirstOrDefault(x => x.Username.ToLower() == txtUserName.Text.ToLower() && x.Password == txtPassword.Text);
                if (tbl_Users != null)
                {
                    if (tbl_Users.RoleID == 10)
                    {
                        frmUpdateSendDate.UserName = tbl_Users.Username;
                        frmUpdateSendDate.confirmUpdate = true;
                        this.Close();
                    }
                    else if (tbl_Users.FirstName.Contains("สุกัญญา") && tbl_Users.LastName.Contains("วิเชียรวรรณ"))
                    {
                        frmUpdateSendDate.UserName = tbl_Users.Username;
                        frmUpdateSendDate.confirmUpdate = true;
                        this.Close();
                    }
                    else
                    {
                        frmUpdateSendDate.confirmUpdate = false;
                        FlexibleMessageBox.Show("Invalid Username or Password!", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    frmUpdateSendDate.confirmUpdate = false;
                    FlexibleMessageBox.Show("Invalid Username or Password!", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.Message.ShowErrorMessage();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmUpdateSendDate.UserName = "";
            frmUpdateSendDate.confirmUpdate = false;
            this.Close();
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == "ชื่อผู้ใช้งาน")
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.Black;
            }
        }

        private void frmUpdateDatePrePareData_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }
    }
}
