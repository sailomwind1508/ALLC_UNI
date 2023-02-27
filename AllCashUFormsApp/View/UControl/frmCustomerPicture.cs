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
    public partial class frmCustomerPicture : Form
    {
        Customer bu = new Customer();
        public frmCustomerPicture()
        {
            InitializeComponent();
        }

        private void GetImageFromUrl()
        {
            picCustomerImg.Image = null;

            if (string.IsNullOrEmpty(frmCustomerInfo._CustImage))
            {
                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@CustomerID", frmCustomerInfo._CustomerID);
                var dt = bu.GetCustomerImage(_params);

                if (dt.Rows.Count > 0 && dt.Rows[0]["CustomerImg"] != DBNull.Value)
                {
                    var img = (byte[])dt.Rows[0]["CustomerImg"];
                    picCustomerImg.SizeMode = PictureBoxSizeMode.StretchImage;
                    picCustomerImg.Image = img.byteArrayToImage();
                }
            }
            else
            {
                picCustomerImg.SizeMode = PictureBoxSizeMode.StretchImage;
                picCustomerImg.ImageLocation = frmCustomerInfo._CustImage;
            }
        }

        private void frmCustomerPicture_Load(object sender, EventArgs e)
        {
            GetImageFromUrl();
        }
    }
}
