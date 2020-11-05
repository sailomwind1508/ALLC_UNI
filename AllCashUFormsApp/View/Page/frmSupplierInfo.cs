using AllCashUFormsApp.Controller;
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
    public partial class frmSupplierInfo : Form
    {
        MenuBU menuBU = new MenuBU();

        public frmSupplierInfo()
        {
            InitializeComponent();
        }

        private void InitPage()
        {
            FormHeader.Text = this.Text;

            btnAdd.Enabled = true;

            var headerPic = menuBU.GetAllData().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage(30, 30);
            }

        }

        private void frmSupplierInfo_Load(object sender, EventArgs e)
        {
            InitPage();
        }
    }
}
