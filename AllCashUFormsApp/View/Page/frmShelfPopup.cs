using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using System.Globalization;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmShelfPopup : Form
    {
        PromotionTemp bu = new PromotionTemp();
        tbl_HQ_Promotion_Hit_Temp tbl_HQ_Promotion_Hit_Temp = new tbl_HQ_Promotion_Hit_Temp();
        string CustomerID;
        public frmShelfPopup()
        {
            InitializeComponent();
        }


        private void frmShelfPopup_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            var temps = bu.tbl_HQ_Promotion_Hit_Temps;
            if (temps != null && temps.Count> 0)
            {
                txtShelfID.Text = temps.First().ShelfID;
            }
        }

        public void SetPromotionTemp(tbl_HQ_Promotion_Hit_Temp obj, string customerID = "")
        {
            tbl_HQ_Promotion_Hit_Temp = obj;
            CustomerID = customerID;
        }

        private void btnSaveShelfCode_Click(object sender, EventArgs e)
        {
            //string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
            //string title = "ยืนยันการบันทึก!!";
            //if (!cfMsg.ConfirmMessageBox(title))
            //    return;

            bu.tbl_HQ_Promotion_Hit_Temps = bu.GetAllData();
            var item = bu.tbl_HQ_Promotion_Hit_Temps.FirstOrDefault(x => x.PromotionID == tbl_HQ_Promotion_Hit_Temp.PromotionID);
            if (item != null)
            {
                item.ShelfID = txtShelfID.Text;

                if (string.IsNullOrEmpty(item.ShelfID))
                {
                    bu.tbl_HQ_Promotion_Hit_Temps.Remove(item);
                }

                int ret = bu.RemoveTempData();

                if (ret == 1)
                    ret = bu.UpdateTempData();

                if (ret == 1)
                {
                    //string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    //var result = msg.ShowInfoMessage();
                    //if (result == DialogResult.OK)
                    {
                        this.Close();
                    }

                }
                else
                {
                    string msg = "เกิดข้อผิดพลาดกรุณาลองใหม่อีกครั้ง";
                    msg.ShowErrorMessage();
                }
            }

        }

    }
}
