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
    public partial class frmSignaturePicture : Form
    {
        PreOrder bu = new PreOrder();
        Dictionary<string, string> URLImage = new Dictionary<string, string>();
        string DocNo = "";
        bool IsPreOrder = false;

        public frmSignaturePicture()
        {
            InitializeComponent();
        }

        private void frmSignaturePicture_Load(object sender, EventArgs e)
        {
            picSignature.Image = null;

            GetImageFromDB();
        }

        public void PrepareLoadSignaturePicture(string docNo, bool isPreOrder = false)
        {
            DocNo = docNo;
            IsPreOrder = isPreOrder;
        }

        private void GetImageFromDB()
        {
            if (!string.IsNullOrEmpty(DocNo))
            {
                var dt = bu.GetSignaturePicture(DocNo, IsPreOrder);

                if (dt.Rows.Count > 0 && dt.Rows[0]["Signature"] != DBNull.Value)
                {
                    var img = (byte[])dt.Rows[0]["Signature"];
                    picSignature.SizeMode = PictureBoxSizeMode.StretchImage;
                    picSignature.Image = img.byteArrayToImage();
                }
            }
        }
    }
}
