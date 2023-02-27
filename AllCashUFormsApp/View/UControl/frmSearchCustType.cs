using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.Page;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmSearchCustType : Form
    {
        ArCustomerType bu = new ArCustomerType();
        public frmSearchCustType()
        {
            InitializeComponent();
        }
        
        private void frmSearchCustType_Load(object sender, EventArgs e)
        {
            BindCustomerTypeData();

        }
        DataSet ds = new DataSet();
        
        DataTable _dt = new DataTable();
        private void BindCustomerTypeData()
        {
            int flagDel = rdoFlagDel0.Checked ? 0 : 1;
            string searchtext="";
            if(txtSearchCustType.Text !="")
            {
                searchtext = txtSearchCustType.Text;
            }
            _dt = bu.GetCustomerTypeGridData(flagDel, searchtext);
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("ArCustomerTypeID", typeof(int)));
            dt.Columns.Add(new DataColumn("ArCustomerTypeCode", typeof(string)));
            dt.Columns.Add(new DataColumn("ArCustomerTypeName", typeof(string)));
            dt.Columns.Add(new DataColumn("CrDate", typeof(string)));
            dt.Columns.Add(new DataColumn("CrUser", typeof(string)));
            dt.Columns.Add(new DataColumn("EdDate", typeof(string)));
            dt.Columns.Add(new DataColumn("EdUser", typeof(string)));
            dt.Columns.Add(new DataColumn("FlagDel", typeof(bool)));
            dt.Columns.Add(new DataColumn("FlagSend", typeof(bool))); //bit sql = boolean c#
            foreach (DataRow r in _dt.Rows)
            {
                dt.Rows.Add(r["ArCustomerTypeID"].ToString(), r["ArCustomerTypeCode"].ToString(), r["ArCustomerTypeName"].ToString(), r["CrDate"].ToString(), r["CrUser"].ToString(), r["EdDate"].ToString(), r["EdUser"].ToString(), r["FlagDel"].ToString(), r["FlagSend"].ToString()); 
            }
            
            gridCustType.DataSource = dt;
            gridCustType.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lblCustTypeCount.Text = dt.Rows.Count.ToNumberFormat();

        }
            private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridCustType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            else
            {
                DataGridViewRow gridrow = gridCustType.Rows[e.RowIndex];
                txtCustTypeCode.Text = gridrow.Cells["colArCustomerTypeCode"].Value.ToString();
                txtCustTypeName.Text = gridrow.Cells["colArCustomerTypeName"].Value.ToString();
            }
        }

        private void gridCustType_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow gridrow = gridCustType.Rows[e.RowIndex];
            //frmCustomerInfo_.ArCustomerTypeID = gridrow.Cells["colArCustomerTypeID"].Value.ToString();
            this.Close();
        }

        private void btnSearchCustType_Click(object sender, EventArgs e)
        {
            BindCustomerTypeData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BindCustomerTypeData();
        }

        private void rdoFlagDel0_CheckedChanged(object sender, EventArgs e)
        {
            BindCustomerTypeData();
        }

        private void rdoFlagDel1_CheckedChanged(object sender, EventArgs e)
        {
            BindCustomerTypeData();
        }

        private void gridCustType_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            gridCustType.SetRowPostPaint(sender, e, this.Font);
        }
    }
}
