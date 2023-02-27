using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using AllCashUFormsApp.View.Page;
namespace AllCashUFormsApp.View.UControl
{
    public partial class frmSearchBranch : Form
    {
        SQLPrompt bu = new SQLPrompt();
        public frmSearchBranch()
        {
            InitializeComponent();
        }

        private void BindBranchData(bool allConfig = false)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var dt = allConfig ? bu.GetConfigDataNonChose().ToList() : bu.GetConfigDataNonChose().Where(x => x.Key != "CENTER" && !x.Key.Contains("Test")).ToList();
                var _dt = new List<KeyValuePair<string, string>>();

                if (!string.IsNullOrEmpty(txtSearch.Text))
                    _dt = dt.Where(x => x.Key.Contains(txtSearch.Text)).ToList();
                else
                    _dt = dt;

                grdList.DataSource = _dt.ToDataTable();
                lblgridCount.Text = _dt.Count.ToNumberFormat();

                grdList.CreateCheckBoxHeaderColumn("colSelect");

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                Cursor.Current = Cursors.Default;
            }
        }

        private void frmSearchProduct_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;

            BindBranchData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<string> selectList = new List<string>();
            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                    if (Convert.ToBoolean(grdList.Rows[i].Cells[0].Value) == true)
                    {
                        selectList.Add(grdList.Rows[i].Cells[1].Value.ToString());
                    }
            }
            var joinString = string.Join(",", selectList);

            //edit by sailom.k 21/10/2022-------------------
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name.ToLower() == "frmSQLPrompt".ToLower())
                {
                    Form frm = (Form)f;
                    ((frmSQLPrompt)frm).BindSearchBranch(joinString);
                }
                if (f.Name.ToLower() == "frmRecoveryPOPopup".ToLower())
                {
                    Form frm = (Form)f;
                    ((frmRecoveryPOPopup)frm).BindSearchBranch(joinString);
                }
            }
            //edit by sailom.k 21/10/2022-------------------

            Cursor.Current = Cursors.Default;
            this.Close();
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindBranchData();
            }
        }

        private void frmSearchProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void chkAllBranch_CheckedChanged(object sender, EventArgs e)
        {
            BindBranchData(chkAllBranch.Checked);
        }
    }
}
