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
    public partial class frmRecoveryPOPopup : Form
    {
        public static string brnTxt = "";
        static string Mode = "";
        List<Control> searchBranchControls = new List<Control>();
        SQLPrompt bu = new SQLPrompt();
        List<Control> searchBWHControls = new List<Control>();
        Dictionary<string, string> depoList = new Dictionary<string, string>();

        public frmRecoveryPOPopup()
        {
            InitializeComponent();

            searchBWHControls = new List<Control> { txtWHCode, txtWHName };
            searchBranchControls = new List<Control>() { txtBranchCode };
        }

        public void SetMode(string _mode)
        {
            Mode = _mode;
        }

        private void frmRecoveryPOPopup_Load(object sender, EventArgs e)
        {
            dtpDocDate.SetDateTimePickerFormat();
            dtpEdDate.SetDateTimePickerFormat();

            dtpDocDate.Value = DateTime.Now;
            dtpEdDate.Value = dtpDocDate.Value;

            Dictionary<string, string> RecMode = new Dictionary<string, string>();
            RecMode.Add("HIS", "HIS");
            RecMode.Add("TL", "TL");

            ddlRecoveryMode.BindDropdownList(RecMode, "Value", "Key");

            //var b = bu.GetBranch();
            //if (b != null)
            //{
            //    this.BindData("FromBranchID", searchBranchControls, b[0].BranchID);
            //}

            depoList = bu.GetConfigDataNonChose();

            if (Mode.ToLower() == "repo")
            {
                txtBranchCode.DisableTextBox(true);
                txtBranchCode.Enabled = false;

                btnSearchWHCode.Enabled = true;
                txtWHCode.DisableTextBox(false);
                txtWHName.DisableTextBox(false);
                dtpDocDate.Enabled = true;
                dtpEdDate.Enabled = true;
                ddlRecoveryMode.Enabled = true;

                txtBranchCode.DisableTextBox(false);
                dtpDocDate.Enabled = true;
                chkAllBranch.Enabled = false;
            }
            else if (Mode.ToLower() == "reendday")
            {
                txtWHCode.DisableTextBox(true);
                txtWHName.DisableTextBox(true);
                btnSearchWHCode.Enabled = false;
                dtpEdDate.Enabled = false;
                ddlRecoveryMode.Enabled = false;
                ddlRecoveryMode.Enabled = false;

                txtBranchCode.DisableTextBox(false);
                dtpDocDate.Enabled = true;
                chkAllBranch.Enabled = true;
            }
            else if (Mode.ToLower() == "stc" || Mode.ToLower() == "stc_all")
            {
                
                txtWHCode.DisableTextBox(true);
                txtWHName.DisableTextBox(true);
                btnSearchWHCode.Enabled = false;
                ddlRecoveryMode.Enabled = false;

                txtBranchCode.DisableTextBox(false);
                txtBranchCode.Enabled = true;
                dtpDocDate.Enabled = true;
                dtpEdDate.Enabled = true;
                chkAllBranch.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {           
            bool completeFlag = false;
            var dt = new DataTable();

            if (Mode.ToLower() == "repo")
            {
                Cursor.Current = Cursors.WaitCursor;
                dt = bu.RecoveryPO(txtWHCode.Text, dtpDocDate.Value, dtpEdDate.Value, ddlRecoveryMode.SelectedValue.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        completeFlag = true;
                    }
                }

                if (completeFlag)
                {
                    Cursor.Current = Cursors.Default;
                    string message = "ดึงข้อมูลเอกสาร เรียบร้อยแล้ว!!!";
                    message.ShowInfoMessage();

                    this.Close();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    string message = "ไม่พบเอกสาร!!!";
                    message.ShowWarningMessage();
                    this.Close();
                }
            }
            else if (Mode.ToLower() == "reendday")
            {
                Cursor.Current = Cursors.WaitCursor;
                string branchIDs = "";
                var tmp = txtBranchCode.Text.Split(',').ToList();
                branchIDs = string.Join(",", tmp.Select(a => string.Join("", a.Split('(')[1].Take(3))));

                dt = bu.UnlockEndDay(branchIDs, dtpDocDate.Value);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        completeFlag = true;
                    }
                }

                if (completeFlag)
                {
                    Cursor.Current = Cursors.Default;
                    string message = "ปล็ดล็อก ปิดวันเรียบร้อยแล้ว!!!";
                    message.ShowInfoMessage();

                    this.Close();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    string message = "เกิดข้อผิดพลาด กรุณาลองใหม่!!!";
                    message.ShowWarningMessage();
                    this.Close();
                }
            }
            else if (Mode.ToLower() == "stc" || Mode.ToLower() == "stc_all")
            {
                Cursor.Current = Cursors.WaitCursor;
                string branchIDs = "";
                var tmp = txtBranchCode.Text.Split(',').ToList();
                branchIDs = string.Join(",", tmp.Select(a => string.Join("", a.Split('(')[1].Take(3))));
                
                dt = bu.SendToCenterByBranch(branchIDs, dtpDocDate.Value, dtpEdDate.Value);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        completeFlag = true;
                    }
                }

                if (completeFlag)
                {
                    Cursor.Current = Cursors.Default;
                    string message = "ส่งข้อมูลเข้า Data Center เรียบร้อยแล้ว!!!";
                    message.ShowInfoMessage();

                    this.Close();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    string message = "เกิดข้อผิดพลาด กรุณาลองใหม่!!!";
                    message.ShowWarningMessage();
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchBWHControls, "เลือกคลังสินค้า");
        }

        private void txtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("BranchWarehouse", searchBWHControls, txt.Text);

            }
        }

        private void btnDistribution_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกเดโป้/สาขา");
        }

        private void txtBranchCode_Click(object sender, EventArgs e)
        {
            //chkAllBranch.Checked = false;

            frmSearchBranch frm = new frmSearchBranch();
            frm.ShowDialog();
        }

        public void BindSearchBranch(string txt)
        {
            brnTxt = txt;
            txtBranchCode.Text = txt;
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
        }

        
    }
}
