using AllCashUFormsApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmSendToHQ : Form
    {
        CultureInfo cultures = System.Globalization.CultureInfo.GetCultureInfo("th-TH");

        MenuBU menuBU = new MenuBU();
        SendToHQ bu = new SendToHQ();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        bool isComplete = false;
        bool isCancel = false;

        string docTypeCode = "";
        int runDigit = 0;


        public frmSendToHQ()
        {
            InitializeComponent();
        }

        #region private methods

        private void InitPage()
        {
            var menu = bu.tbl_AdmFormList.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "1");
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnCopy.Enabled = false;
            btnRemove.Enabled = false;
            btnCancel.Enabled = false;

            var headerPic = bu.tbl_MstMenu.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            InitialData();

            progressBar1.Value = 0;
            txtComment.Text = string.Empty;
            //grdPermission.SetDataGridViewStyle();

            bool connectFlag = bu.VerifyHQConnection();
            btnCStatus.Visible = connectFlag;
            btnDStatus.Visible = !connectFlag;
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            InitHeader();
        }

        private void InitHeader()
        {
            dtpDocDate.SetDateTimePickerFormat();
        }


        #endregion

        #region event methods

        private void frmSendToHQ_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            try
            {
                string cfMsg = "ต้องการส่งข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการส่ง!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                isComplete = true;
                isCancel = false;
                bool connectFlag = bu.VerifyHQConnection();
                if (!connectFlag)
                {
                    string msg = "ไม่สามารถ Connect Server สำนักงานใหญ่ได้!!! กรุณาติดต่อ IT Support";
                    msg.ShowInfoMessage();

                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                txtComment.AppendText("--------------------------Begin Process-----------------------------");
                txtComment.AppendText(Environment.NewLine);

                progressBar1.Minimum = 0;
                progressBar1.Maximum = 100;
                progressBar1.Step = 10;

                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            txtComment.Text = string.Empty;

            bu.CallStopSendToHQ();

            isComplete = false;
            isCancel = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //var backgroundWorker = sender as BackgroundWorker;
                //for (int j = 0; j < 100000; j++)
                //{
                //    Calculate(j);
                //    // Use progress to notify UI thread that progress has
                //    // changed
                //    if (backgroundWorker1 != null)
                //        backgroundWorker1.ReportProgress((j + 1) * 100 / 100000);
                //}
                Cursor.Current = Cursors.WaitCursor;

                var self = (BackgroundWorker)sender;

                //var cb = new SqlConnectionStringBuilder
                //{
                //    DataSource = ".",
                //    InitialCatalog = "Sandbox",
                //    IntegratedSecurity = true
                //};

                if (chkAllData.Checked)
                {
                    DateTime allDate = new DateTime(1900, 1, 1);

                    bu.CallSendToHQ(allDate.ToString("yyyyMMdd", new CultureInfo("en-US")), self);
                }
                else
                    bu.CallSendToHQ(dtpDocDate.Value, self);


                //Send Customer Visit------------------------------------------------------------------
                string dateC = dtpDocDate.Value.ToString("yyMM", new CultureInfo("en-US"));
                string months = dtpDocDate.Value.Month.ToString();
                string _months = dtpDocDate.Value.ToString("MM", new CultureInfo("en-US"));
                string date = dtpDocDate.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));

                string years = dtpDocDate.Value.Year.ToString(new CultureInfo("en-US"));
                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DateC", dateC);
                _params.Add("@I", months);
                _params.Add("@O", _months);
                _params.Add("@DATE", date);

                bu.CallSendAmtArCustomerData(_params);
                //Send Customer Visit------------------------------------------------------------------



                //using (var cn = new SqlConnection(Connection.ConnectionString))
                //{
                //    cn.FireInfoMessageEventOnUserErrors = true;
                //    cn.Open();
                //    cn.InfoMessage += (o, args) => self.ReportProgress(0, args.Message);

                //    using (var cmd = cn.CreateCommand())
                //    {
                //        cmd.CommandText = "usp_LongProcess";
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.ExecuteNonQuery();
                //    }
                //    cn.Close();
                //}

                isComplete = true;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                isComplete = false;
                Cursor.Current = Cursors.Default;
            }

        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor.Current = Cursors.Default;

            txtComment.AppendText("--------------------------Complete Process-----------------------------");
            txtComment.AppendText(Environment.NewLine);

            string msg = "";
            if (isComplete)
            {
                msg = "ส่งข้อมูลเรียบร้อยแล้ว!!";

                //Send mail to HQ //edit by sailom .k 07/01/2022---------------------------------------

                var cdate = DateTime.Now.ToString("dd/MM/yyyy", cultures);

                FormHelper.CreateAndSendMail("พบการ ส่งข้อมูลเข้า Data Center", bu.tbl_Branchs[0].BranchName, cdate);

                //Send mail to HQ //edit by sailom .k 07/01/2022---------------------------------------
            }
            else
            {
                msg = "เกิดข้อผิดพลาดในการ ส่งข้อมูล กรุณาลองใหม่อีกครั้งหรือติดต่อ IT Support!!!";
                if (isCancel)
                {
                    msg = "ยกเลิก ส่งข้อมูลเรียบร้อยแล้ว!!!";
                }
            }

            msg.ShowInfoMessage();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
            Cursor.Current = Cursors.WaitCursor;

            var message = Convert.ToString(e.UserState);
            //Debug.WriteLine(message);
            //statusLabel.Text = message;

            txtComment.AppendText(message);
            txtComment.AppendText(Environment.NewLine);

            if (message.EndsWith(" PERCENT COMPLETE"))
            {
                int percent;
                if (int.TryParse(message.Split(' ')[0], out percent))
                    progressBar1.Value = percent;
            }
            if (message.EndsWith("row(s)."))
            {
                txtComment.AppendText(message);
                txtComment.AppendText(Environment.NewLine);

            }
        }

        private void chkAllData_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllData.Checked)
            {
                dtpDocDate.Enabled = false;
            }
            else
                dtpDocDate.Enabled = true;
        }


        #endregion

        private void btnSendAmtCustData_Click(object sender, EventArgs e)
        {
            try
            {
                bool ret = false;
                //last edit by sailom .k 22/12/2022
                bool isCycle = false;
                isCycle = (new Report()).CheckBranchCycle(bu.tbl_Branchs[0].BranchID);

                var cycleNo = (new Report()).GetCycleNo(dtpDocDate.Value).ToString();
                string _tmpCycleNo = cycleNo.Length == 1 ? ("0" + cycleNo) : cycleNo;
                //last edit by sailom .k 22/12/2022

                string dateC = dtpDocDate.Value.ToString("yyMM", new CultureInfo("en-US"));
                string months = isCycle ? cycleNo : dtpDocDate.Value.Month.ToString(); //last edit by sailom .k 22/12/2022
                string _months = isCycle ? _tmpCycleNo : dtpDocDate.Value.ToString("MM", new CultureInfo("en-US")); //last edit by sailom .k 22/12/2022
                string date = dtpDocDate.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));

                string years = dtpDocDate.Value.Year.ToString(new CultureInfo("en-US"));

                string cfMsg = "ต้องการส่งข้อมูลร้านเยี่ยม รอบการขายที่ " + months + " ประจำปี " + years + " ใช่หรือไม่?";
                string title = "ยืนยันการส่ง!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Cursor.Current = Cursors.WaitCursor;
                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DateC", dateC);
                _params.Add("@I", months);
                _params.Add("@O", _months);
                _params.Add("@DATE", date);

                ret = bu.CallSendAmtArCustomerData(_params);

                if (ret == true)
                {
                    string msg = "ส่งข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                }
                else
                {
                    string msg = "ส่งข้อมูลล้มเหลว!!";
                    msg.ShowErrorMessage();
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                Cursor.Current = Cursors.Default;
            }
        }

        private void frmSendToHQ_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
