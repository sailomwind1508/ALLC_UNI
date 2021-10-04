using AllCashUFormsApp.Controller;
using AllCashUFormsApp.View.Page;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Model;
namespace AllCashUFormsApp.View.UControl
{
    public partial class frmSearchSalAreaDistrict : Form
    {
        SaleAreaDistrict bu = new SaleAreaDistrict();
        public frmSearchSalAreaDistrict()
        {
            InitializeComponent();
        }
      
        private void BindSalAreaDistrict()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable _dt = new DataTable();

            if (ds.Tables.Contains("Area"))
                ds.Tables.Remove("Area");
            int flagDel = rdoN.Checked ? 0 : 1;
            string searchtext = "";
            if(txtSearchSalArea.Text != "")
            {
                searchtext = txtSearchSalArea.Text;
            }
            //_dt = bu.GetSalAreaByWHID(flagDel);
            dt.Columns.Add("SalAreaID", typeof(string));
            dt.Columns.Add("SalAreaName", typeof(string));
            dt.Columns.Add("CrDate", typeof(string));
            dt.Columns.Add("CrUser", typeof(string));
            dt.Columns.Add("EdDate", typeof(string));
            dt.Columns.Add("EdUser", typeof(string));
            dt.Columns.Add("FlagDel", typeof(bool));
            foreach (DataRow r in _dt.Rows)
            {
                dt.Rows.Add(r["SalAreaID"].ToString(),r["SalAreaName"].ToString(),r["CrDate"].ToString(),r["CrUser"].ToString(),r["EdDate"].ToString(),r["EdUser"].ToString(),r["FlagDel"].ToString());
            }
            grd.DataSource = dt;
            grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lblSalAreaCount.Text = dt.Rows.Count.ToNumberFormat();
        }
       
        private void btnSearchSalArea_Click(object sender, EventArgs e)
        {
            BindSalAreaDistrict();
        }

        private void frmSearchSalArea_Load(object sender, EventArgs e)
        {
            BindSalAreaDistrict();
        }

       

        private void gridSalArea_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                    return;
             
                    DataGridViewRow gridrow = grd.Rows[e.RowIndex];
                    txtSalAreaID.Text = gridrow.Cells["colSalAreaID"].Value.ToString();  //อย่าลืม .Value
                    txtSalAreaName.Text = gridrow.Cells["colSalAreaName"].Value.ToString();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void gridSalArea_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                    return;
                DataGridViewRow gridrow = grd.Rows[e.RowIndex];
                string cells = gridrow.Cells["colSalAreaID"].Value.ToString();
                if (cells !="")
                {
                    frmCustomerInfo.salAreaID = cells;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
          
        }
        private void AddID()
        {
            pnlGrid.Enabled = false;

            pnlEdit.Enabled = true;
            grd.Enabled = false;
            txtSalAreaID.DisableTextBox(true);
            txtSalAreaName.Clear();
            txtSalAreaName.DisableTextBox(false);
            txtSalAreaName.Focus();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            
            
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddID();
        }

        private void rdoN_CheckedChanged(object sender, EventArgs e)
        {
            BindSalAreaDistrict();
        }

        private void rdoC_CheckedChanged(object sender, EventArgs e)
        {
            BindSalAreaDistrict();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindSalAreaDistrict();
        }
     
        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void grd_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grd.SetRowPostPaint(sender, e, this.Font);
        }

        private void lblSalAreaCount_Click(object sender, EventArgs e)
        {

        }
    }
}
