using AllCashUFormsApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmVerifyDNS : Form
    {
        MasterDataControl bu = new MasterDataControl();
        MenuBU menuBU = new MenuBU();
        public frmVerifyDNS()
        {
            InitializeComponent();
        }

        #region Method
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

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void InitialData()
        {
            grdBranchDNS.AutoGenerateColumns = false;
            grdBranchDNS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            btnSearch.Enabled = false;

            if (Helper.BranchName != "CENTER")
            {
                string msg = "สามารถใช้ได้เมื่อต่อ CENTER DB เท่านั้น !!";
                msg.ShowWarningMessage();
            }
            else
            {
                btnSearch.Enabled = true;
                lbl_TimeRefresh.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                BindBranchDNS();
                timer1.Enabled = true;
                double MilliSeconds = TimeSpan.FromMinutes(Convert.ToInt32(numericUpDown1.Value)).TotalMilliseconds;
                timer1.Interval = Convert.ToInt32(MilliSeconds);
                timer1.Tick += timer1_Tick;
            }
        }

        private void SetNewTable(DataTable newTable)
        {
            newTable.Columns.Add("BranchID", typeof(string));
            newTable.Columns.Add("BranchRefCode", typeof(string));
            newTable.Columns.Add("BranchName", typeof(string));
            newTable.Columns.Add("db_server", typeof(string));
            newTable.Columns.Add("Pic", typeof(Bitmap));
            newTable.Columns.Add("OnlineStatus", typeof(bool));
        }

        private void BindBranchDNS()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                bu.GetSendProductInfoPrepareData();
                var dtBranchDNS = bu.Get_proc_SendProductInfo_GetDataTable();

                DataTable newTable = new DataTable();
                SetNewTable(newTable);

                Bitmap wifi_Img = new Bitmap(Properties.Resources.wifi); // new Resource Image
                Bitmap power_off_lmg = new Bitmap(Properties.Resources.closeBtn);

                foreach (DataRow r in dtBranchDNS.Rows)
                {
                    Bitmap colPic = Convert.ToBoolean(r["OnlineStatus"]) == true ? wifi_Img : power_off_lmg;
                    newTable.Rows.Add( r["BranchID"].ToString()
                        , r["BranchRefCode"].ToString()
                        , r["BranchName"].ToString()
                        , r["db_server"].ToString()
                        , colPic
                        , r["OnlineStatus"]);
                }

                grdBranchDNS.DataSource = newTable;
                lblrowqty.Text = newTable.Rows.Count.ToNumberFormat();

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        #endregion

        #region Event
        private void frmVerifyDNS_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindBranchDNS();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //double MilliSeconds = TimeSpan.FromMinutes(Convert.ToInt32(numericUpDown1.Value)).TotalMilliseconds;
            //timer1.Interval = Convert.ToInt32(MilliSeconds);
            //timer1.Enabled = true;
            lbl_TimeRefresh.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            BindBranchDNS();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numericUpDown1.Value.ToString()))
            {
                double MilliSeconds = TimeSpan.FromMinutes(Convert.ToInt32(numericUpDown1.Value)).TotalMilliseconds;
                timer1.Interval = Convert.ToInt32(MilliSeconds);
            }
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != '.'))
                e.Handled = true;
            else
                return;
        }

        #endregion

        private void grdBranchDNS_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdBranchDNS.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmVerifyDNS_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
