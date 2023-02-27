using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmButtonTamplate : Form
    {
        public frmButtonTamplate()
        {
            InitializeComponent();
        }

        private void frmButtonTamplate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnAdd.PerformClick();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnEdit.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnRemove.PerformClick();
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnCopy.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnSave.PerformClick();
            }
            else if (e.KeyCode == Keys.F6)
            {
                btnCancel.PerformClick();
            }
            else if (e.KeyCode == Keys.F7)
            {
                btnPrint.PerformClick();
            }
            else if (e.KeyCode == Keys.F8)
            {
                btnPrintCrys.PerformClick();
            }
            else if (e.KeyCode == Keys.F9)
            {
                btnExcel.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnClose.PerformClick();
            }
        }
    }
}
