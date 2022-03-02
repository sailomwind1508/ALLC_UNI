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
            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@CustomerID", frmCustomerInfo._CustomerID);
            var dt = bu.GetCustomerImage(_params);
            if (dt.Rows.Count > 0 && dt.Rows[0]["CustomerImg"] != DBNull.Value)
            {
                var img = (byte[])dt.Rows[0]["CustomerImg"];
                picCustomerImg.SizeMode = PictureBoxSizeMode.StretchImage;
                picCustomerImg.Image = img.byteArrayToImage();
            }
            else
            {
                string URL = "http://ubn.dnsdojo.net:82/CU";
                //URL = "http://192.168.1.10/CU";
                //string URLCenter = "http://192.168.1.10/CU";
                var chkList = frmCustomerInfo._CustImage.ToCharArray().ToList();
                string CustImagePath = "";

                for (int i = 0; i < chkList.Count; i++)
                {
                    if (chkList[i].ToString() != "~")
                    {
                        CustImagePath += chkList[i].ToString();
                    }
                }

                string Src = URL + CustImagePath;
                picCustomerImg.SizeMode = PictureBoxSizeMode.StretchImage;
                picCustomerImg.ImageLocation = Src;
                //picCustomerImg.Load(Src);
            }
        }

        private void frmCustomerPicture_Load(object sender, EventArgs e)
        {
            GetImageFromUrl();
        }
    }
}
