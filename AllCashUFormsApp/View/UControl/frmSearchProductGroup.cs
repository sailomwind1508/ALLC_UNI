using AllCashUFormsApp.Controller;
using System;
using System.Data;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.AControl
{
    public partial class frmSearchProductGroup : Form
    {
        ProductGroup bu = new ProductGroup();
        public frmSearchProductGroup()
        {
            InitializeComponent();
        }
        private void BindData()
        {
            DataTable dt = new DataTable();
            dt = bu.GetProductGroupTable(0);

            grdList.DataSource = dt;

            grdList.CreateCheckBoxHeaderColumn("colSelect");

            lblCountList.Text = dt.Rows.Count.ToNumberFormat();
        }
        private void frmSearchProductGroup_Load(object sender, EventArgs e)
        {
            BindData();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }
    }
}
