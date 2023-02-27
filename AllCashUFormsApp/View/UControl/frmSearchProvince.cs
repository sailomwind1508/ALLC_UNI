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
    public partial class frmSearchProvince : Form
    {
        Province bu = new Province();
        public frmSearchProvince()
        {
            InitializeComponent();

            this.Load += frmSearchProvince_Load;

            grdProvince.RowPostPaint += grdProvince_RowPostPaint;
            grdProvince.CellDoubleClick += grdProvince_CellDoubleClick;

            txtSearch.KeyDown += txtSearch_KeyDown;

            this.FormClosed += frmSearchProvince_FormClosed;
        }

        private void BindProvinceData()
        {
            int flagDel = 0;
            DataTable newTable = new DataTable();
            newTable = bu.GetProvinceTable(flagDel, txtSearch.Text);
            grdProvince.DataSource = newTable;
            labelRowCount.Text = grdProvince.RowCount.ToNumberFormat();
        }

        private void frmSearchProvince_Load(object sender, EventArgs e)
        {
            grdProvince.AutoGenerateColumns = false;
            grdProvince.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            BindProvinceData();
        }

        private void grdProvince_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdProvince.SetRowPostPaint(sender,e,this.Font);
        }

        private void grdProvince_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                    return;

                else
                {
                    frmSalesArea._ProvinceCode = "";
                    frmSalesArea._ProvinceName = "";
                    frmSalesArea._ProvinceID = "";

                    string _ProvinceCode = grdProvince.Rows[e.RowIndex].Cells["colProvinceCode"].Value.ToString();

                    if (!string.IsNullOrEmpty(_ProvinceCode))
                    {
                        frmSalesArea._ProvinceCode = _ProvinceCode;
                        frmSalesArea._ProvinceName = grdProvince.Rows[e.RowIndex].Cells["colProvinceName"].Value.ToString();
                        frmSalesArea._ProvinceID = grdProvince.Rows[e.RowIndex].Cells["colProvinceID"].Value.ToString();
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindProvinceData();
            }
        }

        private void frmSearchProvince_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
