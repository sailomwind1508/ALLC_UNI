using AllCashUFormsApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.View.Page;
namespace AllCashUFormsApp.View.UControl
{
    public partial class frmConfirmTabletData : Form
    {
        Login bu = new Login();
        public frmConfirmTabletData()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var tbl_Users = bu.GetAllData().FirstOrDefault(x => x.Username.ToLower() == txtUserName.Text.ToLower() && x.Password == txtPassword.Text);
                if (tbl_Users != null)
                {
                    frmReceiveTabletData.confirmDelete = true;
                    this.Close();
                }
                else
                {
                    frmReceiveTabletData.confirmDelete = false;
                    FlexibleMessageBox.Show("Invalid Username or Password!", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
            Cursor.Current = Cursors.Default;
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == "ชื่อผู้ใช้งาน")
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.Black;
            }
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
    }
}
