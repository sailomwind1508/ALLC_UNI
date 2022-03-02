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
    public partial class frmVerifyCustomer : Form
    {
        EndDay bu = new EndDay();
        static DataTable VerifyFlagBillDT = new DataTable();
        public frmVerifyCustomer()
        {
            InitializeComponent();
        }

        private void frmVerifyCustomer_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            var dt = VerifyFlagBillDT;
            grdList.DataSource = dt;

            grdList.CreateCheckBoxHeaderColumn("colSelect");
            lblgridCount.Text = dt.Rows.Count.ToNumberFormat();
        }

        public void PreparefrmVerifyCustomer(DataTable data)
        {
            VerifyFlagBillDT = data;
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            Cursor.Current = Cursors.WaitCursor;
            string _customerID = grdList.Rows[e.RowIndex].Cells["colCustomerID"].Value.ToString();
            string docTypeCode = grdList.Rows[e.RowIndex].Cells["colDocTypeCode"].Value.ToString();
            string docNo = grdList.Rows[e.RowIndex].Cells["colDocNo"].Value.ToString();

            if (!string.IsNullOrEmpty(_customerID))
            {
                frmTabletSales frm = new frmTabletSales();
                frm.docTypeCode = docTypeCode;

                MainForm mfrm = null;
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name.ToLower() == "mainform")
                    {
                        mfrm = (MainForm)f;
                    }
                }

                frm.MdiParent = mfrm;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                frm.BindTabletSalesData(docNo);

                this.Close();
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            List<int> rowIndexList = new List<int>();

            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                if (Convert.ToBoolean(grdList.Rows[i].Cells[0].Value) == true)
                    rowIndexList.Add(i);
            }

            if (rowIndexList.Count > 5)
            {
                string cfMsg = "เลือกได้ไม่เกิน 5 ใบต่อครั้ง!!!";
                cfMsg.ShowWarningMessage();
                return;
            }
            else if (rowIndexList.Count == 0)
            {
                string cfMsg = "กรุณาเลือกร้านค้า!!!";
                cfMsg.ShowWarningMessage();
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            foreach (var i in rowIndexList)
            {
                string _customerID = grdList.Rows[i].Cells["colCustomerID"].Value.ToString();
                string docTypeCode = grdList.Rows[i].Cells["colDocTypeCode"].Value.ToString();
                string docNo = grdList.Rows[i].Cells["colDocNo"].Value.ToString();

                if (!string.IsNullOrEmpty(_customerID))
                {
                    frmTabletSales frm = new frmTabletSales();
                    frm.docTypeCode = docTypeCode;

                    MainForm mfrm = null;
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f.Name.ToLower() == "mainform")
                        {
                            mfrm = (MainForm)f;
                        }
                    }

                    frm.MdiParent = mfrm;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                    frm.BindTabletSalesData(docNo);
                }
            }
            Cursor.Current = Cursors.Default;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
