using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.Page;
namespace AllCashUFormsApp.View.UControl
{
    public partial class frmSearchBranchWareHouseList : Form
    {
        BranchWarehouse bu = new BranchWarehouse();
        public frmSearchBranchWareHouseList()
        {
            InitializeComponent();
        }
        private void BindBranchWareHouse()
        {
            DataTable dt = new DataTable();

            if (frmReport._RptStock == "ALL")
            {
                dt = bu.GetBranchWareHouseTable(x=>x.WHType == 0 || x.WHType == 1);
            }
            else
            {
                dt = bu.GetBranchWareHouseTable(x => x.WHType == 1);
            }
            
            if (dt != null && dt.Rows.Count > 0)
            {
                grdList.DataSource = dt;

                grdList.CreateCheckBoxHeaderColumn("colSelect");

                lblgridCount.Text = grdList.Rows.Count.ToNumberFormat();
            }

        }
        private void frmSearchBrachWareHouseList_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;
           
            BindBranchWareHouse();
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
                   selectList.Add(grdList.Rows[i].Cells[1].Value.ToString());
                }
            }

            var joinString = string.Join(",", selectList);

            frmReport._txtBW = joinString;

            this.Close();
        }
        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }
    }
}
