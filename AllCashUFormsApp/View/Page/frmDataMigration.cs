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
    public partial class frmDataMigration : Form
    {
        MenuBU menuBU = new MenuBU();
        MigrateData bu = new MigrateData();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        bool isComplete = false;
        bool isCancel = false;

        string docTypeCode = "";
        int runDigit = 0;

        public frmDataMigration()
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
            chkAllData.Checked = true;
            dtpDateTo.Value = DateTime.Now.ToDateTimeFormat();
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            InitHeader();
        }

        private void InitHeader()
        {
            dtpDateFrom.SetDateTimePickerFormat();
            dtpDateTo.SetDateTimePickerFormat();
        }


        #endregion

        private void frmDataMigration_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDataMigration_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var self = (BackgroundWorker)sender;
                string dbName = txtDBName.Text;
                string df = "";
                string dt = "";

                if (chkAllData.Checked)
                {
                    DateTime allDate = new DateTime(1900, 1, 1);
                    df = allDate.ToString("yyyyMMdd", new CultureInfo("en-US"));
                    dt = dtpDateTo.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));
                }
                else
                {
                    df = dtpDateFrom.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));
                    dt = dtpDateTo.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));
                }

                bu.CallMigrateData(dbName, df, dt, self);

                if (!isCancel)
                    isComplete = true;
            }
            catch (Exception ex)
            {

                isComplete = false;
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
                msg = "Migrate ข้อมูลเรียบร้อยแล้ว!!";
            }
            else
            {
                msg = "เกิดข้อผิดพลาดในการ Migrate ข้อมูล กรุณาลองใหม่อีกครั้งหรือติดต่อ IT Support!!!";
                if (isCancel)
                {
                    msg = "ยกเลิก Migrate ข้อมูลแล้ว!!!";
                }
            }

            msg.ShowInfoMessage();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            var message = Convert.ToString(e.UserState);
  
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

        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            txtComment.Text = string.Empty;

            bu.CallStopMigrateData();

            isComplete = false;
            isCancel = true;
        }

        private void btnMigrateData_Click(object sender, EventArgs e)
        {
            List<string> errList = new List<string>();
            if (string.IsNullOrEmpty(txtDBName.Text))
            {
                string msg = "กรุณาระบุ DB Name!!!";
                msg.ShowWarningMessage();
                errList.SetErrMessageList(txtDBName, lblDBName);

                return;
            }

            if (!chkAllData.Checked)
            {
                if (string.IsNullOrEmpty(dtpDateFrom.Value.ToString()))
                {
                    string msg = "กรุณาเลือกวันที่เริ่มต้น!!!";
                    msg.ShowWarningMessage();
                    //dtpDateFrom.Focus();
                    errList.SetErrMessageList(dtpDateFrom, lblDateFrom);

                    return;
                }
            }

            string cfMsg = "ต้องการ Migrate ข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการ Migrate!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            Cursor.Current = Cursors.WaitCursor;

            txtComment.AppendText("--------------------------Begin Process-----------------------------");
            txtComment.AppendText(Environment.NewLine);

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;

            backgroundWorker1.RunWorkerAsync();
        }

        private void chkAllData_CheckedChanged(object sender, EventArgs e)
        {
            dtpDateFrom.Enabled = !chkAllData.Checked;
        }
    }
}
