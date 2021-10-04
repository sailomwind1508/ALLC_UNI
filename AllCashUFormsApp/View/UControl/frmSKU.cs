using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.View.Page;
namespace AllCashUFormsApp.View.UControl
{
    public partial class frmSKU : Form
    {
        public frmSKU()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (mtbAddSKU.MaskCompleted == true)
            {
                frmPromotionSetting.skuID = mtbAddSKU.Text;
                this.Close();
            }
            else
            {
                string msg = "กรุณากรอก SKU_ID ให้ครบ 8 หลัก !!";
                msg.ShowWarningMessage();
            }
        }
    }
}
