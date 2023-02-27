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
    public partial class frmCyCle : Form
    {
        public frmCyCle()
        {
            InitializeComponent();
        }

        private void Test_AddImage_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            this.monthCalendar1.SelectionStart = new DateTime(Convert.ToInt32(2022), Convert.ToInt32(1), 1);
        }
    }
}
