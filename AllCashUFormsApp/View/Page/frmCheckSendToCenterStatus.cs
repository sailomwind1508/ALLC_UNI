using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace AllCashUFormsApp.View.Page
{
    public partial class frmCheckSendToCenterStatus : Form
    {
        CultureInfo cultures = System.Globalization.CultureInfo.GetCultureInfo("en-US");

        VerifyDataToSAP bu = new VerifyDataToSAP();
        static DataTable resultData = new DataTable();

        public frmCheckSendToCenterStatus()
        {
            InitializeComponent();
        }

        private void InitPage()
        {
            resultData = null;

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

            dtpDocDate.SetDateTimePickerFormat();
            dtpDocDateTo.SetDateTimePickerFormat();

            grdResult.SetDataGridViewStyle();
        }

        private void InitialData()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnPrint.Enabled = false;
            btnExcel.Enabled = true;
        }

        private void frmCheckSendToCenterStatus_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();

            InitialData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            var dt = bu.SearchInvSendToCenter(txtSearch.Text, dtpDocDate.Value);
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

            if (grdResult.Rows.Count > 0)
            {
                try
                {
                    var row = grdResult.Rows[e.RowIndex];

                    var _status = row.Cells[2].Value.ToString();
                    var _status2 = row.Cells[3].Value.ToString();

                    if (!string.IsNullOrEmpty(_status) && _status == "ยังไม่ส่ง")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                    else if (!string.IsNullOrEmpty(_status2) && _status2 == "ยังไม่ส่ง")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                }
                catch
                {
                }
            }
        }

        private void btnSearchStatus_Click(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            resultData = null;
            this.Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (resultData != null && resultData.Rows.Count > 0)
            {
                DataTable dtDetails = new DataTable("CheckSTSStatus");
                dtDetails = resultData;
                dtDetails.TableName = "Sheet1";
                //this.OpenExcelReportsPopup("SQL_Results_Report", "SQL_Results.xslt", dtDetails , true);

                frmWait wait = new frmWait();
                wait.Show();

                string dir = @"C:\AllCashExcels";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string cDate = DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss");
                string _excelName = dir + @"\" + string.Join("", "Check Send To SAP Status", '_', cDate) + ".xls";

                My_DataTable_Extensions.ExportToExcelR3(new List<DataTable>() { resultData }, _excelName, "CheckSTSStatus");

                wait.Hide();
                wait.Dispose();
                wait.Close();
            }
        }

        private void btnCheckStatus_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            var dt = bu.CheckSendToCenterStatus(dtpDocDate.Value, dtpDocDateTo.Value);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdResult.DataSource = dt;
                resultData = dt;
                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();
            }
            else
            {
                grdResult.DataSource = null;
                resultData = null;
                lblRowCount.Text = dt.Rows.Count.ToNumberFormat();

                string msg = "ไม่มีข้อมูลการ ส่งข้อมูลเข้า Data Center!!!";
                msg.ShowInfoMessage();
            }

            Cursor.Current = Cursors.Default;
        }
    }
}
