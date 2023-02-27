using AllCashUFormsApp.Controller;
using AllCashUFormsApp.View.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmSQLPrompt : Form
    {
        CultureInfo cultures = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
        Dictionary<string, string> depoList = new Dictionary<string, string>();
        MenuBU menuBU = new MenuBU();
        SQLPrompt bu = new SQLPrompt();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        static DataTable resultData = new DataTable();
        public static string brnTxt = "";
 
        string docTypeCode = "";
        int runDigit = 0;

        string serverName = Helper.ConnectionString.Split('=')[4].Split(';')[0];
        string userName = Helper.ConnectionString.Split('=')[6].Split(';')[0];
        string databaseName = Helper.ConnectionString.Split('=')[5].Split(';')[0];

        public frmSQLPrompt()
        {
            InitializeComponent();

            this.tabSQLResult.ImageList = this.imageList1;
            // Add image to a tab in WinTab by setting the index of the image in ImageList control
            this.tabSQLResult.TabPages[0].ImageIndex = 0;
            this.tabSQLResult.TabPages[1].ImageIndex = 1;
        }

        #region private methods

        private void InitPage()
        {
            resultData = null;

            var menu = bu.tbl_AdmFormList.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "1");
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnCopy.Enabled = false;
            btnRemove.Enabled = false;
            btnCancel.Enabled = false;
            btnPrint.Enabled = true;

            var headerPic = bu.tbl_MstMenu.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            InitialData();
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            InitHeader();
        }

        private void InitHeader()
        {
            //depoList = bu.GetConfigDataNonChose();

            //cbDepo.DataSource = depoList.ToList();
            //cbDepo.ValueMember = "Value";
            //cbDepo.DisplayMember = "Key";

            //var defaultSelect = depoList.FirstOrDefault(x => x.Value.Contains(databaseName)).Key;
            //Predicate<KeyValuePair<string, string>> condition = delegate (KeyValuePair<string, string> x) { return x.Key == defaultSelect; };
            //cbDepo.SelectedValueDropdownList(condition);

            lblRowCount.Text = string.Join(" | ", serverName, userName, databaseName, "(" + Helper.BranchName + ")", "0 rows");
        }

        #endregion

        #region event methods

        private void frmSQLPrompt_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();

            depoList = bu.GetConfigDataNonChose();

            txtBranchCode.DisableTextBox(true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            resultData = null;
            this.Close();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            txtSQLMessage.Clear();
            resultData = null;
            DateTime startTime = DateTime.Now;

            if (!string.IsNullOrEmpty(txtSqlCmd.Text))
            {
                if (chkAllBranch.Checked)
                {
                    var _depoList = depoList.Where(x => x.Key != "CENTER" && !x.Key.Contains("Test")).ToList();

                    GetSelectBranch(_depoList, startTime);
                }
                else
                {
                    var tmpSelBrn = txtBranchCode.Text.Split(',').ToList();
                    var _depoList = depoList.Where(x => tmpSelBrn.Contains(x.Key)).ToList();

                    GetSelectBranch(_depoList, startTime);

                    //KeyValuePair<string, string> depoSel = (KeyValuePair<string, string>)cbDepo.SelectedItem;
                    //Helper.ConnectionString = depoSel.Value; //Init connection string

                    //Connection.GetConnectionStringsManual(); //for manual connect 04112020

                    //bool connectFlag = VerifyDNSConnected(Connection.ConnectionString);

                    //if (connectFlag)
                    //    ExecuteCommand(depoSel, startTime);
                    //else
                    //{
                    //    string branchNames = string.Join("\n", depoSel.Key);
                    //    string message = "Offline DNS => \n" + "- " + branchNames;
                    //    message += "-------------------------------- \n";

                    //    message.ShowWarningMessage();
                    //}
                }
            }
            else
            {
                List<string> errList = new List<string>();
                errList.SetErrMessageList(txtSqlCmd, lblSQLCmd); ;

                string message = "กรุณาใส่คำสั่ง SQL \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                txtSQLMessage.Text = "No result!!!";
                txtSQLMessage.ForeColor = Color.Red;
                tabSQLResult.SelectedIndex = 1;  
            }

            if (!string.IsNullOrEmpty(Helper.Original_ConnectionString))
            {
                Helper.ConnectionString = Helper.Original_ConnectionString;
                Connection.GetConnectionStringsManual();
                Helper.BranchName = Helper.Original_BranchName;
            }

            Cursor.Current = Cursors.Default;
        }

        private void GetSelectBranch(List<KeyValuePair<string, string>> _depoList, DateTime startTime)
        {
            CheckConnection(false);

            DataTable dt = new DataTable();
            List<string> messageList = new List<string>();

            bool verifyCorrectSqlCmd = false;

            //verify sql command-------------------------------------------
            foreach (KeyValuePair<string, string> depoSel in _depoList)
            {
                string conStr = depoSel.Value;
                Helper.BranchName = depoSel.Key;

                Helper.ConnectionString = conStr; //Init connection string

                Connection.GetConnectionStringsManual(); //for manual connect 04112020

                bool connectFlag = VerifyDNSConnected(Connection.ConnectionString);

                if (connectFlag)
                {
                    var _ret = bu.Execute(txtSqlCmd.Text);
                    if (_ret.First().Key.Rows.Count > 0)
                    {
                        dt = _ret.First().Key.Copy();
                        dt.Clear();
                        verifyCorrectSqlCmd = true;
                        break;
                    }
                }
            }

            //verify sql command-------------------------------------------

            int index = 0;
            foreach (KeyValuePair<string, string> depoSel in _depoList)
            {
                string conStr = depoSel.Value;
                Helper.BranchName = depoSel.Key;

                Helper.ConnectionString = conStr; //Init connection string

                Connection.GetConnectionStringsManual(); //for manual connect 04112020

                bool connectFlag = VerifyDNSConnected(Connection.ConnectionString);

                if (connectFlag)
                {
                    var ret = bu.Execute(txtSqlCmd.Text);
                    if (verifyCorrectSqlCmd && ret.First().Key.Rows.Count > 0)
                    {
                        foreach (DataRow row in ret.First().Key.Rows)
                        {
                            DataRow toInsert = dt.NewRow();
                            toInsert.ItemArray = row.ItemArray;
                            dt.Rows.InsertAt(toInsert, index);
                        }
                    }

                    //else
                    //{
                    //    if (verifyCorrectSqlCmd)
                    //        dt.Rows.Add(dt.NewRow());
                    //}
                    //DataRow toInsert = ret.First().Key.Rows.Count > 0 ? ret.First().Key.Rows[0] : dt.NewRow();

                    ////DataRow toInsert = dt.NewRow();
                    ////toInsert = ret.First().Key.Rows.Count > 0 ? ret.First().Key.Rows[0] : dt.NewRow();
                    ////dt.Rows.Add(toInsert);
                    //dt.Rows.InsertAt(toInsert, index);

                    messageList.Add(ret.First().Value);
                }

                index++;
            }

            if (messageList.Count > 0 || dt != null && dt.Rows.Count > 0)
            {
                resultData = dt;
                grdSQLResult.DataSource = dt;

                DateTime endTime = DateTime.Now;
                //double duration = (endTime - startTime).TotalSeconds;
                var _duration = (endTime - startTime);
                var time = _duration.ToString().Split('.')[0];
                lblRowCount.Text = string.Join(" | ", "All", userName, "All", "(ทุกศูนย์)", time, dt.Rows.Count.ToNumberFormat() + " rows");

                //string retMsgs = string.Join("\n\n", messageList); // string.Format("ไม่สามารถทำรายการสินค้านี้ได้ \n\n");
                txtSQLMessage.Clear();
                foreach (var item in messageList)
                {
                    txtSQLMessage.AppendLine(item);
                }
                //txtSQLMessage.Text = retMsgs;
                txtSQLMessage.ForeColor = Color.Black;

                if (dt != null && dt.Rows.Count > 0)
                    tabSQLResult.SelectedIndex = 0;
                else if (messageList.Count > 0)
                    tabSQLResult.SelectedIndex = 1;
            }
        }

        private void ExecuteCommand(KeyValuePair<string, string> depoSel, DateTime startTime)
        {
            string conStr = depoSel.Value;
            Helper.BranchName = depoSel.Key;
            //DateTime startTime = DateTime.Now;
            Helper.ConnectionString = conStr; //Init connection string

            Connection.GetConnectionStringsManual(); //for manual connect 04112020

            serverName = Helper.ConnectionString.Split('=')[4].Split(';')[0];
            userName = Helper.ConnectionString.Split('=')[6].Split(';')[0];
            databaseName = Helper.ConnectionString.Split('=')[5].Split(';')[0];

            var ret = bu.Execute(txtSqlCmd.Text);
            DataTable dt = new DataTable();
            dt = ret.First().Key;
            if (dt != null && dt.Rows.Count > 0)
            {
                resultData = dt;
                grdSQLResult.DataSource = dt;
                DateTime endTime = DateTime.Now;
                var _duration = (endTime - startTime);
                var time = _duration.ToString().Split('.')[0];

                lblRowCount.Text = string.Join(" | ", serverName, userName, databaseName, "(" + Helper.BranchName + ")", time, dt.Rows.Count.ToNumberFormat() + " rows");

                txtSQLMessage.Text = ret.First().Value;
                txtSQLMessage.ForeColor = Color.Black;
                tabSQLResult.SelectedIndex = 0;
            }
            else
            {
                resultData = null;
                grdSQLResult.DataSource = null;

                lblRowCount.Text = string.Join(" | ", serverName, userName, databaseName, "(" + Helper.BranchName + ")", "0 rows");

                txtSQLMessage.Text = ret.First().Value;
                txtSQLMessage.ForeColor = Color.Black;
                tabSQLResult.SelectedIndex = 1;
            }
        }

        private void grdSQLResult_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdSQLResult.SetRowPostPaint(sender, e, this.Font);
        }

        private void chkAllBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllBranch.Checked)
            {
                //cbDepo.Enabled = false;
                txtBranchCode.DisableTextBox(false);
                txtBranchCode.Text = string.Join(",", depoList.Select(x => x.Key));
            }
            else
            {
                //cbDepo.Enabled = true;
                txtBranchCode.DisableTextBox(true);
                txtBranchCode.Text = string.Empty;     
            }

            txtBranchCode.ReadOnly = true;
        }

        private void btnCheckDNS_Click(object sender, EventArgs e)
        {
            CheckConnection();
        }

        private void CheckConnection(bool isShowPopup = true)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                Dictionary<string, bool> connectionList = new Dictionary<string, bool>();
                if (chkAllBranch.Checked)
                {
                    var _depoList = depoList.Where(x => x.Key != "CENTER" && !x.Key.Contains("Test")).ToList();
                    foreach (KeyValuePair<string, string> depoSel in _depoList)
                    {
                        string conStr = depoSel.Value;
                        string branch = depoSel.Key;

                        Helper.ConnectionString = conStr; //Init connection string

                        Connection.GetConnectionStringsManual(); //for manual connect 04112020

                        bool connectFlag = VerifyDNSConnected(Connection.ConnectionString);
                        connectionList.Add(branch, connectFlag);
                    }
                }
                else
                {
                    var tmpSelBrn = txtBranchCode.Text.Split(',').ToList();
                    var _depoList = depoList.Where(x => tmpSelBrn.Contains(x.Key)).ToList();
                    foreach (KeyValuePair<string, string> depoSel in _depoList)
                    {
                        string conStr = depoSel.Value;
                        string branch = depoSel.Key;

                        Helper.ConnectionString = conStr; //Init connection string

                        Connection.GetConnectionStringsManual(); //for manual connect 04112020

                        bool connectFlag = VerifyDNSConnected(Connection.ConnectionString);
                        connectionList.Add(branch, connectFlag);
                    }

                    //KeyValuePair<string, string> depoSel = (KeyValuePair<string, string>)cbDepo.SelectedItem;
                    //Helper.ConnectionString = depoSel.Value; //Init connection string

                    //Connection.GetConnectionStringsManual(); //for manual connect 04112020

                    //bool connectFlag = VerifyDNSConnected(Connection.ConnectionString);
                    //connectionList.Add(depoSel.Key, connectFlag);
                }

                Cursor.Current = Cursors.Default;
                string message = "";
                bool isWarning = false;
                if (connectionList.Any(x => !x.Value))
                {
                    string branchNames = string.Join("\n- ", connectionList.Where(x => !x.Value).Select(a => a.Key).ToList());
                    message += "\nOffline DNS => \n" + "- " + branchNames;
                    message += "\n--------------------------------";

                    branchNames = string.Join("\n- ", connectionList.Where(x => x.Value).Select(a => a.Key).ToList());
                    message += "\nOnline DNS => \n" + "- " + branchNames;
                    message += "\n--------------------------------";

                    isWarning = true;
                }
                else
                {
                    if (isShowPopup)
                    {
                        string branchNames = string.Join("\n- ", connectionList.Where(x => x.Value).Select(a => a.Key).ToList());
                        message += "\nOnline DNS => \n" + "- " + branchNames;
                        message += "\n-------------------------------- \n";
                    }
                }

                if (isShowPopup)
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        if (isWarning)
                            message.ShowWarningMessage();
                        else
                            message.ShowInfoMessage();
                    }
                }
            }
            catch //(Exception ex)
            {
                //ex.WriteLog(this.GetType());
            }
            
        }


        private bool VerifyDNSConnected(string conStr)
        {
            //using (var l_oConnection = new SqlConnection(conStr))
            //{
            //    try
            //    {
            //        l_oConnection.Open();
            //        l_oConnection.Close();
            //    }
            //    catch (SqlException)
            //    {
            //        return false;
            //    }

            //    return true;
            //}

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conStr);
            builder.ConnectTimeout = 5;
            using (var connection = new SqlConnection(builder.ToString()))
            {
                try
                {
                    connection.Open();
                    connection.Close();
                }
                catch (SqlException)
                {
                    return false;
                }

                return true;
            }
        }


        #endregion

        private void txtBranchCode_Click(object sender, EventArgs e)
        {
            chkAllBranch.Checked = false;

            frmSearchBranch frm = new frmSearchBranch();
            frm.ShowDialog();
        }

        public void BindSearchBranch(string txt)
        {
            brnTxt = txt;
            txtBranchCode.Text = txt;
        }

        private void btnRepairVE_Click(object sender, EventArgs e)
        {
            frmDocDate frm = new frmDocDate();
            frm.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (resultData != null && resultData.Rows.Count > 0)
            {
                DataTable dtDetails = new DataTable("SQLResults");
                dtDetails = resultData;
                dtDetails.TableName = "Sheet1";
                //this.OpenExcelReportsPopup("SQL_Results_Report", "SQL_Results.xslt", dtDetails , true);

                frmWait wait = new frmWait();
                wait.Show();

                string dir = @"C:\AllCashExcels";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string cDate = DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss");
                string _excelName = dir + @"\" + string.Join("", "SQL_Results", '_', cDate) + ".xls";

                My_DataTable_Extensions.ExportToExcelR3(new List<DataTable>() { resultData }, _excelName, "SQL_Results");

                wait.Hide();
                wait.Dispose();
                wait.Close();
            }
        }

        private void btnRecoveryPO_Click(object sender, EventArgs e)
        {
            frmRecoveryPOPopup frm = new frmRecoveryPOPopup();
            frm.SetMode("RePO");
            frm.ShowDialog();
        }

        private void btnUnlockEndDAy_Click(object sender, EventArgs e)
        {
            frmRecoveryPOPopup frm = new frmRecoveryPOPopup();
            frm.SetMode("ReEndDay");
            frm.ShowDialog();
        }

        private void btnSendToCenter_Click(object sender, EventArgs e)
        {
            frmRecoveryPOPopup frm = new frmRecoveryPOPopup();

            if (chkAllBranch.Checked)
                frm.SetMode("STC_ALL");
            else
                frm.SetMode("STC");

            frm.ShowDialog();
        }
    }
}
