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

            this.Load += frmSearchProductSubGroup_Load;

            btnAccept.Click += btnAccept_Click;
            btnCancel.Click += btnCancel_Click;

            grdList.RowPostPaint += grdList_RowPostPaint;
        }

        private void BindData()
        {
            DataTable dt = new DataTable();
            
            dt = bu.GetProductSubGroupTable();

            lblgridCount.Text = dt.Rows.Count.ToNumberFormat();

            if (dt != null && dt.Rows.Count > 0)
            {
                grdList.DataSource = dt;

                try
                {
                    grdList.CreateCheckBoxHeaderColumn("colSelect");
                }
                catch { }
            }
        }

        private void frmSearchProductSubGroup_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;

            BindData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
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

            this.Close();
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }
    }
}
