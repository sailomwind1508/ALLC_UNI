using AllCashUFormsApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmInventories : Form
    {
        MenuBU menuBU = new MenuBU();
        Inventory bu = new Inventory();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();

        string docTypeCode = "";
        int runDigit = 0;

        public frmInventories()
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
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            InitHeader();
        }

        private void InitHeader()
        {

        }


        #endregion

        #region event methods

        private void frmInventories_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private void btnUpdateHW_Click(object sender, EventArgs e)
        {
            string cfMsg = "ต้องการปรับปรุงข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการปรับปรุง!!";
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            var self = (BackgroundWorker)sender;

            bu.UpdateInventoryWH(self);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor.Current = Cursors.Default;

            txtComment.AppendText("--------------------------Complete Precess-----------------------------");
            txtComment.AppendText(Environment.NewLine);

            string msg = "ปรับปรุงข้อมูลเรียบร้อยแล้ว!!";
            msg.ShowInfoMessage();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
            Cursor.Current = Cursors.WaitCursor;

            var message = Convert.ToString(e.UserState);

            if (message.EndsWith(" PERCENT COMPLETE"))
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void frmInventories_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
