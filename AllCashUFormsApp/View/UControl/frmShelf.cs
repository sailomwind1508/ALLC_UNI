using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.View.Page;
using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;

namespace AllCashUFormsApp.View.UControl.A_UC
{
  
    public partial class frmShelf : Form
    {

        CustomerShelf bu = new CustomerShelf();
        public frmShelf()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtShelfNo.Text != "")
            {
                frmCustomerInfo.shelfID = txtShelfNo.Text;
                this.Close();
            }
            else
            {
                string msg = "กรุณากรอกเลชShelf !!";
                msg.ShowWarningMessage();
            }
        }
        public void frmShelf_Load(object sender, EventArgs e)
        {
            if (frmCustomerInfo.page == "add")
            {
                this.Text = "Add";
                this.Update();
                label1.Text = "เพิ่มเลข Shelf :";
            }
            //else if (frmCustomerInfo_.page == "edit")
            //{
            //    this.Text = "Edit";
            //    this.Update();
            //    label1.Text = "แก้เลข Shelf :";
            //    txtShelfNo.Text = frmCustomerInfo_.shelfID;
            //}

            txtShelfNo.Focus();

        }

        private void txtShelfNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
            else
            {
                return;
            }
        }
    }
}
