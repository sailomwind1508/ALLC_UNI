using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmLogin : Form
    {
        Login bu = new Login();
        Customer cust = new Customer();
        Product prod = new Product();
        Dictionary<string, string> depoList = new Dictionary<string, string>();

        public frmLogin()
        {
            InitializeComponent();
            this.lblcopyR1.Text = ConfigurationManager.AppSettings["CopyRightTextR1"];
            this.lblcopyR2.Text = ConfigurationManager.AppSettings["CopyRightTextR2"];
            this.lblVersion.Text = ConfigurationManager.AppSettings["Version"];
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.SetTitleForm();
            depoList = bu.GetConfigData();

            cbDepo.DataSource = depoList.ToList();
            cbDepo.ValueMember = "Value";
            cbDepo.DisplayMember = "Key";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            KeyValuePair<string, string> depoSel = (KeyValuePair<string, string>)cbDepo.SelectedItem;
            string conStr = depoSel.Value;
            Helper.BranchName = depoSel.Key;

            Helper.ConnectionString = conStr; //Init connection string

            if (bu.ValidateData(txtUserName.Text, txtPassword.Text, conStr))
            {
                bool resault = bu.VerifyData(txtUserName.Text, txtPassword.Text);
                if (resault)
                {
                    Connection.GetConnectionStringsManual(); //for manual connect 04112020

                    Helper.tbl_Users = bu.GetAllData().FirstOrDefault(x => x.Username.ToLower() == txtUserName.Text.ToLower()  && x.Password == txtPassword.Text);

                    cust.GetAllData();
                    prod.GetAllData();
                    cust.GetUOM();
                    cust.GetUOMSet();
                    cust.GetDiscountType();

                    MainForm frm = new MainForm();
                    frm.Show();

                    //this.Dispose();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    btnLogin.PerformClick();
            //}
            if (e.KeyCode == Keys.Enter)
            {
                cbDepo.Focus();
            }
        }

        private void cbDepo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}
