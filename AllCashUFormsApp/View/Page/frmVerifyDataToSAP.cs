using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmVerifyDataToSAP : Form
    {
        bool isComplete = false;
        bool isCancel = false;


        CultureInfo cultures = System.Globalization.CultureInfo.GetCultureInfo("en-US");

        VerifyDataToSAP bu = new VerifyDataToSAP();

        public frmVerifyDataToSAP()
        {
            InitializeComponent();
        }

        private void frmVerifyDataToSAP_Load(object sender, EventArgs e)
        {
            InitPage();

            InitialData();
        }

        private void InitPage()
        {
            var menu = bu.tbl_AdmFormList.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }
            var headerPic = bu.tbl_MstMenu.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            dtpDateFrom.SetDateTimePickerFormat();
            dtpDateTo.SetDateTimePickerFormat();

            grdResult.SetDataGridViewStyle();
        }

        private void InitialData()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnPrint.Enabled = false;

            //BindDropDown(ddlYear, ddlMonth);

            progressBar1.Value = 0;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 10;
        }

        private void BindDropDown(ComboBox ddl1, ComboBox ddl2)
        {
            ddl1.Items.Clear();
            ddl2.Items.Clear();
            var cDate = DateTime.Now.ToString("yyyy", cultures);
            int year = Convert.ToInt32(cDate);

            for (int i = year; i > (year - 10); i--)
            {
                ddl1.Items.Add(i);
            }

            for (int i = 1; i <= 13; i++)
            {
                ddl2.Items.Add(i);
            }

            ddl1.SelectedIndex = 0;
            ddl2.SelectedIndex = 0;
        }

        private void btnCheckPO_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            progressBar1.Value = 0;

            var dt = bu.VerifyPO();
            if (dt != null && dt.Rows.Count > 0)
            {
                string msg = "ตรวจพบข้อมูล บิลขาย ที่มีปัญหา!!!";
                msg.ShowErrorMessage();

                grdResult.DataSource = dt;

                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();

                progressBar1.Value = 100;
            }
            else
            {
                grdResult.DataSource = null;

                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();

                string msg = "ไม่พบข้อมูล บิลขาย ที่มีปัญหา!!!";
                msg.ShowInfoMessage();

                progressBar1.Value = 100;
            }

            
            Cursor.Current = Cursors.Default;
        }

        private void btnCheckVE_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            progressBar1.Value = 0;

            var dt = bu.VerifyVE();
            if (dt != null && dt.Rows.Count > 0)
            {
                string msg = "ตรวจพบข้อมูล ใบกำกับภาษีเต็มรูป ที่มีปัญหา!!!";
                msg.ShowErrorMessage();

                grdResult.DataSource = dt;

                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();

                progressBar1.Value = 100;
            }
            else
            {
                grdResult.DataSource = null;

                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();

                string msg = "ไม่พบข้อมูล ใบกำกับภาษีเต็มรูป ที่มีปัญหา!!!";
                msg.ShowInfoMessage();

                progressBar1.Value = 100;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnCheckStock_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            progressBar1.Value = 0;

            var dt = bu.VerifyStock(dtpDateFrom.Value, dtpDateTo.Value);
            if (dt != null && dt.Rows.Count > 0)
            {
                string msg = "ตรวจพบข้อมูล Stock ที่มีปัญหา!!!";
                msg.ShowErrorMessage();

                grdResult.DataSource = dt;

                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();

                progressBar1.Value = 100;
            }
            else
            {
                grdResult.DataSource = null;

                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();

                string msg = "ไม่พบข้อมูล Stock ที่มีปัญหา!!!";
                msg.ShowInfoMessage();

                progressBar1.Value = 100;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnCheckUOM_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            progressBar1.Value = 0;
            var dt = bu.VerifyUOM();
            if (dt != null && dt.Rows.Count > 0)
            {
                //string msg = "ตรวจพบข้อมูลที่สามารถนำเข้า DB Center ได้!!!";
                string msg = "ตรวจพบข้อมูล UOM Entry ที่มีปัญหา!!!";
                msg.ShowErrorMessage();

                grdResult.DataSource = dt;

                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();

                progressBar1.Value = 100;
            }
            else
            {
                grdResult.DataSource = null;

                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();

                //string msg = "ไม่พบข้อมูลที่จะนำเข้า DB Center!!!";
                string msg = "ไม่พบข้อมูล UOM Entry ที่มีปัญหา!!!";
                msg.ShowInfoMessage();

                progressBar1.Value = 100;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSendToSAP_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            progressBar1.Value = 0;

            var dt = bu.SendDataToDBCenter();
            if (dt != null)
            {
                //string msg = "ตรวจพบข้อมูลที่สามารถนำเข้า DB Center ได้!!!";
                string msg = "ส่งข้อมูลเข้า DB Center เรียบร้อยแล้ว!!";
                msg.ShowInfoMessage();

                grdResult.DataSource = dt;

                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();

                progressBar1.Value = 100;
            }
            else
            {
                //string msg = "ไม่พบข้อมูลที่จะนำเข้า DB Center!!!";
                string msg = "เกิดข้อผิดพลาด กรุณาลองใหม่อีกครั้ง!!!";
                msg.ShowErrorMessage();

                progressBar1.Value = 100;
            }
            Cursor.Current = Cursors.Default;

            //isComplete = true;
            //isCancel = false;

            //progressBar1.Minimum = 0;
            //progressBar1.Maximum = 100;
            //progressBar1.Step = 10;

            //backgroundWorker1.RunWorkerAsync();
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
            //    Cursor.Current = Cursors.WaitCursor;

            //    var self = (BackgroundWorker)sender;

            //    bu.SendDataToDBCenter(self);

            //    isComplete = true;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                isComplete = false;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
            //Cursor.Current = Cursors.WaitCursor;

            //var message = Convert.ToString(e.UserState);

            //if (message.EndsWith(" PERCENT COMPLETE"))
            //{
            //    int percent;
            //    if (int.TryParse(message.Split(' ')[0], out percent))
            //        progressBar1.Value = percent;
            //}
            //if (message.EndsWith("row(s)."))
            //{

            //}
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Cursor.Current = Cursors.Default;

            //string msg = "";
            //if (isComplete)
            //{
            //    msg = "ส่งข้อมูลเข้า DB Center เรียบร้อยแล้ว!!";
            //}
            //else
            //{
            //    msg = "เกิดข้อผิดพลาดในการ ส่งข้อมูล กรุณาลองใหม่อีกครั้งหรือติดต่อ IT Support!!!";
            //    if (isCancel)
            //    {
            //        msg = "ยกเลิก ส่งข้อมูลเรียบร้อยแล้ว!!!";
            //    }
            //}

            //msg.ShowInfoMessage();
        }

        private void btnCheckStock2_Click(object sender, EventArgs e)
        {

        }

        private void btnCheckUOM2_Click(object sender, EventArgs e)
        {

        }

        private void btnSendToSAP2_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemoveVE_Click(object sender, EventArgs e)
        {
            string cfMsg = "ต้องการลบข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการลบ!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            List<string> veDocNumList = new List<string>();
            for (int i = 0; i < grdResult.RowCount; i++)
            {
                var cell0 = grdResult.Rows[i].Cells[0];

                if (cell0.IsNotNullOrEmptyCell())
                {
                    veDocNumList.Add(cell0.EditedFormattedValue.ToString());
                }
            }

            if (veDocNumList.Count > 0)
            {
                veDocNumList = veDocNumList.Distinct().ToList();

                string docnums = string.Join(",", veDocNumList);
                Cursor.Current = Cursors.WaitCursor;

                var ret = bu.RemoveINV(docnums);
                if (ret)
                {
                    string msg = "ลบข้อมูลเรียบร้อยแล้ว!!!";
                    msg.ShowInfoMessage();
                }
                else
                {
                    string msg = "เกิดข้อผิพลาดไม่สามารถลบข้อมูลได้!!!";
                    msg.ShowInfoMessage();
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
    
            var dt = bu.SearchInv(txtSearch.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdResult.DataSource = dt;

                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();
            }
            else
            {
                grdResult.DataSource = null;

                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();

                string msg = "ไม่พบข้อมูลที่ค้นหา!!!";
                msg.ShowInfoMessage();
            }
            Cursor.Current = Cursors.Default;
        }

        private void grdResult_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdResult.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnVerQtyAmt_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            progressBar1.Value = 0;

            var dt = bu.VerifyQtyAmt();
            if (dt != null && dt.Rows.Count > 0)
            {
                string msg = "ตรวจพบข้อมูล ใบกำกับภาษี ที่มีปัญหา!!!";
                msg.ShowErrorMessage();

                grdResult.DataSource = dt;

                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();

                progressBar1.Value = 100;
            }
            else
            {
                grdResult.DataSource = null;

                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();

                string msg = "ไม่พบข้อมูล ใบกำกับภาษี ที่มีปัญหา!!!";
                msg.ShowInfoMessage();

                progressBar1.Value = 100;
            }


            Cursor.Current = Cursors.Default;
        }

        private void grdResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    string _ivDocNo = grdResult.Rows[e.RowIndex].Cells[6].Value.ToString();

                    if (!string.IsNullOrEmpty(_ivDocNo))
                    {
                        MainForm mfrm = null;
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f.Name.ToLower() == "mainform")
                            {
                                mfrm = (MainForm)f;
                            }
                        }

                        frmVE frm = new frmVE();
                        frm.docTypeCode = "V";
                        frm.MdiParent = mfrm;
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.WindowState = FormWindowState.Maximized;
                        frm.Show();
                        frm.BindVEData(_ivDocNo);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }
    }
}
