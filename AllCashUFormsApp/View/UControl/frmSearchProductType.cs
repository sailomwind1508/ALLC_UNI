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

namespace AllCashUFormsApp.View.AControl
{
    public partial class frmSearchProductType : Form
    {
        ProductType bu = new ProductType();
        public frmSearchProductType()
        {
            InitializeComponent();
        }
        private void BindData()
        {
            DataTable dt = new DataTable();
            dt = bu.GetProductTypeTable();
            grdList.DataSource = dt;
            lblgridCount.Text = dt.Rows.Count.ToNumberFormat();

            grdList.CreateCheckBoxHeaderColumn("colSelect");
        }
        private void frmSearchProductType_Load(object sender, EventArgs e)
        {
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
                if (Convert.ToBoolean(grdList.Rows[i].Cells["colSelect"].Value) == true)
                {
                    selectList.Add(grdList.Rows[i].Cells["colProductTypeID"].Value.ToString());
                }
            }

            var joinString = string.Join(",", selectList);
            frmReport._txtProType = joinString;
            this.Close();
        }
    }
}
