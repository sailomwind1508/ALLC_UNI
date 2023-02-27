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
        MenuBU menuBU = new MenuBU();
        SendToHQ bu = new SendToHQ();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();

        string docTypeCode = "";
        int runDigit = 0;


        public frmSendToHQ()
        {
            InitializeComponent();
        }

        #region private methods

        private void InitPage()
        {
            var menu = bu.GetAllFromMenu().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
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

            var headerPic = menuBU.GetAllData().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            InitialData();

            progressBar1.Value = 0;
            txtComment.Text = string.Empty;
            //grdPermission.SetDataGridViewStyle();
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
            InitPage();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            string cfMsg = "ต้องการส่งข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการส่ง!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            Cursor.Current = Cursors.WaitCursor;

            txtComment.AppendText("--------------------------Begin Precess-----------------------------");
            txtComment.AppendText(Environment.NewLine);

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 10;

            backgroundWorker1.RunWorkerAsync();
        }

        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            txtComment.Text = string.Empty;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
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
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor.Current = Cursors.Default;

            txtComment.AppendText("--------------------------Complete Precess-----------------------------");
            txtComment.AppendText(Environment.NewLine);

            string msg = "ส่งข้อมูลเรียบร้อยแล้ว!!";
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


    }
}
