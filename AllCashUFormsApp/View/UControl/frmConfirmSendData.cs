using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using AllCashUFormsApp.View.Page;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmConfirmSendData : Form
    {
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        private string formName = "";
        private string formText = "";
        SendData bu = new SendData();
        DateTime _docdate;
        string _whid;
        List<string> readOnlyControls = new List<string>();

        public frmConfirmSendData()
        {
            InitializeComponent();

            readOnlyControls = new List<string>() { txtWHCode.Name, txtWHName.Name, dtpDocDate.Name
                , txnSendAmt.Name, txtCountBill.Name, dtpEditDocDate.Name, txtSendOldBill.Name
                , txtCountOldBill.Name, txtTotalDue.Name, txtTotalBil.Name };
        }

        private void frmConfirmSendData_Load(object sender, EventArgs e)
        {
            this.Text = formText;
            dtpDocDate.SetDateTimePickerFormat();
            dtpEditDocDate.SetDateTimePickerFormat();

            Search();

            this.OpenControl(false, readOnlyControls.ToArray(), null);

            btnSubmit.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void PreparePopupForm(string type, string frmName, string popUPText, Dictionary<string, DateTime> _params)//DateTime docdate, string whid)
        {
            formName = frmName;
            formText = popUPText;
            _docdate = _params.Min(x => x.Value);
            _whid = string.Join(",", _params.Keys.ToList());
        }

        private void Search()
        {
            try
            {
                var dt = bu.GetConfirmData(_docdate, _whid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var row0 = dt.Rows[0];
                    txtWHCode.Text = row0["WHID"].ToString();
                    txtWHName.Text = row0["WHName"].ToString();
                    dtpDocDate.Value = Convert.ToDateTime(row0["DocDate"]).ToDateTimeFormat();
                    txnSendAmt.Text = Convert.ToDecimal(row0["SendAmt"]).ToStringN2();
                    txtCountBill.Text = Convert.ToDecimal(row0["CountBill"]).ToStringN0();

                    dtpEditDocDate.Value = Convert.ToDateTime(row0["DocDate"]).ToDateTimeFormat();
                    txtSendOldBill.Text = Convert.ToDecimal(0).ToStringN2();
                    txtCountOldBill.Text = Convert.ToDecimal(0).ToStringN0();

                    decimal totalDue = Convert.ToDecimal(txtSendOldBill.Text) + Convert.ToDecimal(row0["SendAmt"]);
                    decimal totalBill = Convert.ToDecimal(txtCountOldBill.Text) + Convert.ToDecimal(row0["CountBill"]);
                    txtTotalDue.Text = totalDue.ToStringN2();
                    txtTotalBil.Text = totalBill.ToStringN0();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void frmPromotion_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void frmPromotion_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var dt = bu.SendPreOrderData(txtWHCode.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                var ret = dt.Rows[0][0].ToString();
                if (ret == "1")
                {
                    string msg = "ดึงข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    this.Close();

                    MainForm mfrm = null;
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f.Name.ToLower() == "mainform") //(f.Name == "frmOD")
                        {
                            mfrm = (MainForm)f;
                        }
                    }

                    frmPreOrder frm = new frmPreOrder();
                    frm.MdiParent = mfrm;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.WindowState = FormWindowState.Minimized;

                    frm.SetDefaultDate(dtpDocDate.Value);

                    frm.Show();
                    frm.WindowState = FormWindowState.Maximized;
                    
                }
                else
                {

                }

                
            }
        }
    }
}
