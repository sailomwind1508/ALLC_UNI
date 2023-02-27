using AllCashUFormsApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmBackup : Form
    {
        CultureInfo cultures = System.Globalization.CultureInfo.GetCultureInfo("th-TH");

        MenuBU menuBU = new MenuBU();
        BackupDB bu = new BackupDB();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        bool isComplete = false;
        bool isCancel = false;

        string docTypeCode = "";
        int runDigit = 0;


        public frmBackup()
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

            //bool connectFlag = bu.VerifyHQConnection();
            //btnCStatus.Visible = connectFlag;
            //btnDStatus.Visible = !connectFlag;
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            InitHeader();
        }

        private void InitHeader()
        {
            var cDate = DateTime.Now;
            var conStr = Helper.ConnectionString;
            string databaseName = conStr.Split('=')[5].Split(';')[0];
            txtPath.Text = @"D:\For_IT\Backup-DBs\Daily\" + databaseName + "_" + cDate.ToString("dd-MM-yyyy_hh_mm_ss") + ".BAK";
        }

        #endregion

        #region event methods

        private void frmBackup_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //saveFileDialog1.Filter = "BAK files (*.BAK) | *.BAK";
            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    string fileName = saveFileDialog1.FileName;
            //    string extesion = Path.GetExtension(fileName);
            //    switch (extesion)
            //    {
            //        case ".bak"://do something here 
            //            break;
            //        case ".BAK"://do something here 
            //            break;
            //        default://do something here
            //            break;
            //    }
            //}

            saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.Filter = "BAK files (*.BAK) | *.BAK";
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                txtPath.Text = filename;
            }
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var self = (BackgroundWorker)sender;
                var conStr = Helper.ConnectionString;
                string databaseName = conStr.Split('=')[5].Split(';')[0];

                bool compressFlag = false;
                compressFlag = rdoCompress.Checked;

                bu.CallBackupDB(txtPath.Text, databaseName, compressFlag, self);

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
                msg = "สำรองข้อมูลเรียบร้อยแล้ว!!";

                
            }
            else
            {
                msg = "เกิดข้อผิดพลาดในการ สำรองข้อมูล กรุณาลองใหม่อีกครั้งหรือติดต่อ IT Support!!!";
                if (isCancel)
                {
                    msg = "ยกเลิก สำรองข้อมูลเรียบร้อยแล้ว!!!";
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

            if (message.EndsWith(" percent processed."))
            {
                int percent;
                if (int.TryParse(message.Split(' ')[0], out percent))
                {
                    percent = percent > 100 ? 100 : percent;
                    progressBar1.Value = percent;
                }
            }
            if (message.EndsWith("row(s)."))
            {
                txtComment.AppendText(message);
                txtComment.AppendText(Environment.NewLine);

            }
        }

        private void btnCancelBackup_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            txtComment.Text = string.Empty;

            bu.CallStopBackupDB();

            isComplete = false;
            isCancel = true;
        }

        private void btnFilePath_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "D:\\";
            openFileDialog1.Filter = "bak files (*.bak)|*.bak|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                var filePath = openFileDialog1.FileName;
                txtPath.Text = filePath;

                ////Read the contents of the file into a stream
                //var fileStream = openFileDialog1.OpenFile();

                //using (StreamReader reader = new StreamReader(fileStream))
                //{
                //    fileContent = reader.ReadToEnd();
                //}
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string cfMsg = "ต้องการสำรองข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการสำรอง!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                isComplete = true;
                isCancel = false;

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
    }
}
