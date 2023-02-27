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
    public partial class frmSearchEmployee : Form
    {
        Employee bu = new Employee();
        public frmSearchEmployee()
        {
            InitializeComponent();
        }

        private void grdEmp_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdEmp.SetRowPostPaint(sender, e, this.Font);
        }
        private void BindEmployee()
        {
            DataTable dt = new DataTable();
            dt = bu.GetEmployeePopup();
            grdEmp.DataSource = dt;

            label3.Text = dt.Rows.Count.ToNumberFormat();
        }
        private void frmSearchEmp_Load(object sender, EventArgs e)
        {
            BindEmployee();
        }
        private void grdEmp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            else
            {
                string EmpID = grdEmp.Rows[e.RowIndex].Cells["colEmpID"].Value.ToString();
                string EmpName = grdEmp.Rows[e.RowIndex].Cells["colFullName"].Value.ToString();

                if (!string.IsNullOrEmpty(EmpID))
                {
                    frmSalesTransfer.EmpID = EmpID;
                    frmSalesTransfer.EmpName = EmpName;
                }

                this.Close();
            }
        }
    }
}
