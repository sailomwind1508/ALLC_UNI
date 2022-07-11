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
                    string whid = "";
                    string whName = "";
                    List<string> whids = new List<string>();
                    List<string> whNames = new List<string>();

                    DateTime docdate = new DateTime();
                    decimal sendAmt = 0;
                    decimal cBill = 0;
                    DateTime edDate = new DateTime();
                    decimal sob = 0;
                    decimal cob = 0;
                    decimal totalDue = 0;
                    decimal totalBill = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        //whid += row["WHID"].ToString();
                        //whName += row["WHName"].ToString();
                        docdate = Convert.ToDateTime(dt.Rows[0]["DocDate"]).ToDateTimeFormat();
                        sendAmt += Convert.ToDecimal(row["SendAmt"]);
                        cBill += Convert.ToDecimal(row["CountBill"]);

                        edDate = docdate;
                        sob += Convert.ToDecimal(0);
                        cob += Convert.ToDecimal(0);

                        totalDue += sob + Convert.ToDecimal(row["SendAmt"]);
                        totalBill += cob + Convert.ToDecimal(row["CountBill"]);

                        whids.Add(row["WHID"].ToString());
                        whNames.Add(row["WHName"].ToString());
                    }

                    txtWHCode.Text = string.Join(",", whids);
                    txtWHName.Text = string.Join(",", whNames);
                    dtpDocDate.Value = docdate.ToDateTimeFormat();
                    txnSendAmt.Text = sendAmt.ToStringN2();
                    txtCountBill.Text = cBill.ToStringN0();

                    dtpEditDocDate.Value = edDate.ToDateTimeFormat();
                    txtSendOldBill.Text = sob.ToStringN2();
                    txtCountOldBill.Text = cob.ToStringN0();

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtWHCode.Text))
                {
                    string msg = "ยังไม่พบข้อมูลที่มาจาก Tablet!!";
                    msg.ShowInfoMessage();

                    this.Close();
                }

                Cursor.Current = Cursors.WaitCursor;

                var dt = bu.SendPreOrderData(txtWHCode.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var ret = dt.Rows[0][0].ToString();
                    if (ret == "1")
                    {
                        Cursor.Current = Cursors.Default;

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
                        Cursor.Current = Cursors.Default;

                        string msg = "ดึงข้อมูลไม่สำเร็จ กรุณาลองใหม่อีกครั้ง!!";
                        msg.ShowWarningMessage();

                        this.Close();
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;

                    string msg = "ดึงข้อมูลไม่สำเร็จ กรุณาลองใหม่อีกครั้ง!!";
                    msg.ShowWarningMessage();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;

                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
            
        }
    }
}
