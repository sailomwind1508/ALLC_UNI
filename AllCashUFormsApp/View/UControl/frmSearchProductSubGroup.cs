using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class frmSearchProductSubGroup : Form
    {
        ProductSubGroup bu = new ProductSubGroup();
        public frmSearchProductSubGroup()
        {
            InitializeComponent();
        }

        private void BindProductSubGroup()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                var dt = bu.GetProductSubGroupData_Popup(txtSearch.Text);
                grdList.DataSource = dt;
                lblgridCount.Text = dt.Rows.Count.ToNumberFormat();
                grdList.CreateCheckBoxHeaderColumn("colSelect");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void frmSearchProductSubGroup_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;

            BindProductSubGroup(); //22-06-2022 adisorn  
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
                    selectList.Add(grdList.Rows[i].Cells[3].Value.ToString());
                }
            }

            var joinString = string.Join(",", selectList);
            frmReport._txtSubGroupPro = joinString;
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
                BindProductSubGroup();
            }
        }

        private void frmSearchProductSubGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
