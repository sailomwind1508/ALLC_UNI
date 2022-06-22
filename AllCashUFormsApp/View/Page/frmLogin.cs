using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmLogin : Form
    {
        Login bu = new Login();
        AppVersion apVerbu = new AppVersion();
        Customer cust = new Customer();
        Product prod = new Product();
        Dictionary<string, string> depoList = new Dictionary<string, string>();
        static bool isLogOff = false;

        public frmLogin()
        {
            InitializeComponent();
            //this.lblcopyR1.Text = ConfigurationManager.AppSettings["CopyRightTextR1"];
            //this.lblcopyR2.Text = ConfigurationManager.AppSettings["CopyRightTextR2"];

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            this.lblVersion.Text = string.Join(" ", GetAttributeValue<AssemblyTitleAttribute>(a => a.Title), version);
            lblcopyR1.Text = GetAttributeValue<AssemblyCopyrightAttribute>(a => a.Copyright);
            lblcopyR2.Text = GetAttributeValue<AssemblyTrademarkAttribute>(a => a.Trademark);
        }

        private string GetAttributeValue<TAttr>(Func<TAttr, string> resolveFunc, string defaultResult = null) where TAttr : Attribute
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            object[] attributes = assembly.GetCustomAttributes(typeof(TAttr), false);
            if (attributes.Length > 0)
                return resolveFunc((TAttr)attributes[0]);
            else
                return defaultResult;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.SetTitleForm();
            depoList = bu.GetConfigData();

            cbDepo.DataSource = depoList.ToList();
            cbDepo.ValueMember = "Value";
            cbDepo.DisplayMember = "Key";

            try
            {
                if (IsServerConnected())
                {
                    string updateFlag = ConfigurationManager.AppSettings["update_app_flag"].ToString();
                    if (!string.IsNullOrEmpty(updateFlag) && updateFlag == "1")
                        CheckAppVersion(); //edit by sailom .k 20/04/2022
                }
                else
                {
                    MessageBox.Show("พบข้อผิดพลาดในการอัพเดตแอปออนไลน์!!!");
                }
            }
            catch { }
           
            if (isLogOff)
            {
                cbDepo.Focus();
                cbDepo.Select();
            }
            else
                txtUserName.Focus();
        }

        public void PrepareDefaultLogin(string userName, string password)
        {
            txtUserName.Text = userName;
            txtPassword.Text = password;
            txtUserName.ForeColor = Color.Black;
            txtPassword.ForeColor = Color.Black;
            isLogOff = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                isLogOff = false;

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

                        Helper.tbl_Users = bu.GetAllData().FirstOrDefault(x => x.Username.ToLower() == txtUserName.Text.ToLower() && x.Password == txtPassword.Text);

                        cust.GetAllData();
                        bu.tbl_AdmFormList = bu.GetAllFromMenu();
                        bu.tbl_DocumentType = bu.GetDocumentType();
                        bu.tbl_MstMenu = bu.GetAllMenuData();

                        //prod.GetAllData();
                        bu.tbl_ProductUom = cust.GetUOM();
                        bu.tbl_ProductUomSet = cust.GetUOMSet();
                        bu.tbl_DiscountType = cust.GetDiscountType();

                        //edit by sailom .k 14/12/2021------------------------------------------
                        bu.tbl_Branchs = bu.GetBranch();
                        bu.tbl_Companies = bu.GetAllCompany();
                        bu.tbl_ProductPriceGroup = cust.GetProductPriceGroup();
                        //edit by sailom .k 14/12/2021------------------------------------------

                        bu.tbl_SalArea = bu.GetAllSaleArea();
                        bu.tbl_SalAreaDistrict = bu.GetAllSaleAreaDistrict();
                        bu.tbl_Product = bu.GetProductNonFlag(); //for support when user open old document have a cancel product!! last edit by sailom.k 05/05/2022
                        bu.tbl_ProductGroup = bu.GetProductGroup();
                        bu.tbl_ProductSubGroup = bu.GetProductSubGroup();

                        //Write user login log by sailom.k 18/10/2021------------------------------------------------------------
                        string myHost = System.Net.Dns.GetHostName();
                        string myIP = System.Net.Dns.GetHostByName(myHost).AddressList[0].ToString();

                        string log = string.Format("computer name : {0}, ip : {1}, time : {2}, user login : {3}", myHost, myIP, DateTime.Now.ToString(), Helper.tbl_Users.Username);
                        log.WriteLog(this.GetType());
                        //ErrorLogsDao.Insert(new tbl_error_logs { user_code = Helper.user_name, form_name = this.GetType().Name, function_name = Helper.GetCurrentMethod(), err_desc = log });
                        //Write user login log by sailom.k 18/10/2021------------------------------------------------------------

                        //MainForm frm = new MainForm();
                        //frm.Show();

                        //Last edit by sailom .k 28/02/2022-----------------------------------------------------
                        string name = this.Name;
                        List<Form> openForms = new List<Form>();

                        foreach (Form f in Application.OpenForms)
                        {
                            openForms.Add(f);
                        }

                        foreach (Form f in openForms)
                        {
                            if (openForms.Count == 1 && f.Name.ToLower() == name.ToLower()) //(f.Name == "frmOD")
                            {
                                MainForm frm = new MainForm();
                                frm.Show();
                                break;
                            }
                            else
                            {
                                if (f.Visible == false)
                                {
                                    if (f.Name.ToLower() != name.ToLower())
                                    {
                                        f.Visible = true;

                                        f.MdiParent = this;
                                        f.StartPosition = FormStartPosition.CenterParent;
                                        f.WindowState = FormWindowState.Minimized;
                                        //frm.Dock = DockStyle.Fill;
                                        f.Text = ((ToolStripItem)sender).Text;

                                        MemoryManagement.FlushMemory();

                                        f.Show();
                                        f.WindowState = FormWindowState.Maximized;

                                        f.Focus();
                                    }
                                }
                                else
                                {
                                    f.Focus();
                                }
                            }
                        }
                        //Last edit by sailom .k 28/02/2022-----------------------------------------------------

                        Cursor.Current = Cursors.Default;

                        //this.Dispose();
                        this.Hide();
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        FlexibleMessageBox.Show("Invalid Username or Password!", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                FlexibleMessageBox.Show("ไม่สามารถ connect server ศูนย์ได้!!! กรุณาติดต่อ IT : " + ex.Message, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckAppVersion()
        {
            bool ret = false;
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;

                DataTable dt = apVerbu.CheckAppVersion(version);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //192.168.1.12 => D:\WEB\UForce\Files\Back-End is a deploy path
                    frmAppVersion frm = new frmAppVersion();
                    frm.PrepareAppVersion(dt);
                    frm.ShowDialog();
                }
                else
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("พบข้อผิดพลาดในการอัพเดตแอปออนไลน์ => " + ex.Message);
                ret = false;
            }
            
            return ret;
        }

        private bool IsServerConnected()
        {
            var conStr = ConfigurationManager.AppSettings["CenterConnect"].ToString();
            using (var l_oConnection = new SqlConnection(conStr))
            {
                try
                {
                    l_oConnection.Open();                   
                    l_oConnection.Close();
                }
                catch (SqlException)
                {
                    return false;
                }

                return true;
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

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

    }
}
