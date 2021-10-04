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
    public partial class frmSearchSalArea : Form
    {
        SaleArea bu = new SaleArea();
        public frmSearchSalArea()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmSearchSalArea_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;

            BindData();
            lbl_RowCount.Text = grdList.RowCount.ToString();
        }
        private void BindData()
        {
            DataTable newDT = new DataTable();

            newDT = bu.GetSalAreaData(0, txtSearch.Text);

            if (newDT != null && newDT.Rows.Count > 0)
            {
                grdList.DataSource = newDT;
                grdList.CreateCheckBoxHeaderColumn("colSelect");
            }
            else
            {
                grdList.DataSource = null;
            }
                
        }
        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender,e,this.Font);
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            List<string> selectList = new List<string>();
            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                if (Convert.ToBoolean(grdList.Rows[i].Cells["colSelect"].Value) == true)
                {
                    selectList.Add(grdList.Rows[i].Cells["colSalAreaID"].Value.ToString());
                }
            }
            var joinString = string.Join(",", selectList);
            frmReport._SalArea = joinString;
            this.Close();
        }
    }
}
