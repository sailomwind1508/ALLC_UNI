using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmProductSlip : Form
    {
        RL bu = new RL();
        MenuBU menuBU = new MenuBU();

        List<Control> BranchWarehouseControl = new List<Control>();
        List<tbl_ProductUom> PrdUomList = new List<tbl_ProductUom>();
        public static bool callProductSlip = false;
        public static string allBranchWH;
        public frmProductSlip()
        {
            InitializeComponent();

            this.Load += frmProductSlip_Load;

            btnSearchWarehouse.Click += btnSearchWarehouse_Click;
            btnSearchRL.Click += btnSearchRL_Click;
            btnRL_Report.Click += btnRL_Report_Click;
            btnPreOrderCustCtrl_Report.Click += btnPreOrderCustCtrl_Report_Click;
            btnClose.Click += btnClose_Click;

            //BranchWarehouseControl = new List<Control>() { txtWHCode, txtWHName };

            grdList.RowPostPaint += grdList_RowPostPaint;
            this.FormClosed += frmProductSlip_FormClosed;
        }

        #region Private Method

        private void SetControlNotUse()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnPrintCrys.Enabled = false;

            button1.Enabled = false; //ผู้สั่งสินค้า
            button2.Enabled = false; //พิมพ์ใบจัดสินค้าคืน

            txtWHCode.DisableTextBox(true);
            //txtWHName.DisableTextBox(true);

            txtRequestBy.DisableTextBox(true);
            txtFullName.DisableTextBox(true);

            TextBox1.DisableTextBox(true);

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

            dtpRLDate.SetDateTimePickerFormat();
            dtpRLDate.Value = DateTime.Now;

            rdoProdCode.Checked = true;

            PrdUomList = bu.GetProductUom();

            grdList.SetDataGridViewStyle();
            grdList.RowsDefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224, 224);
            grdList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224, 224);

            SetControlNotUse();
        }

        private void SetRLData(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                grdList.Rows.Add(1);
                grdList.Rows[i].Cells["colProductID"].Value = dt.Rows[i].Field<string>("ProductID");
                grdList.Rows[i].Cells["colProductName"].Value = dt.Rows[i].Field<string>("ProductName");

                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)grdList.Rows[i].Cells["colUnit"];
                comboBoxCell.DataSource = PrdUomList;
                comboBoxCell.DisplayMember = "ProductUomName";
                comboBoxCell.ValueMember = "ProductUomID";

                comboBoxCell.Value = dt.Rows[i].Field<int>("OrderUom");

                grdList.Rows[i].Cells["colBaseQty"].Value = dt.Rows[i].Field<string>("BaseQty");
                grdList.Rows[i].Cells["colReceivedQty"].Value = dt.Rows[i].Field<decimal>("ReceivedQty");

                if (txtWHCode.TextLength == 6)
                {
                    grdList.Rows[i].Cells["colDocNo"].Value = dt.Rows[i].Field<string>("DocNo");
                }

            }
        }

        #endregion

        #region Event Method

        private void frmProductSlip_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
        }

        private void btnSearchWarehouse_Click(object sender, EventArgs e)
        {
            callProductSlip = true;
            frmSearchBranchWareHouseList frm = new frmSearchBranchWareHouseList();
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(frmProductSlip.allBranchWH))
                txtWHCode.Text = allBranchWH;
            
            //this.OpenBranchWarehousePopup(BranchWarehouseControl, "เลือกคลังสินค้า", x => x.FlagDel == false && x.WHType != 0);
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnSearchRL_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtWHCode.Text))
                {
                    string msg = "กรุณาเลือกคลังรถ !!";
                    msg.ShowWarningMessage();
                    return;
                }

                grdList.Rows.Clear();

                Dictionary<string, object> _params = new Dictionary<string, object>();
                string _DocDate = dtpRLDate.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));
                _params.Add("@WHID", txtWHCode.Text);
                _params.Add("@DocDate", _DocDate);
                _params.Add("@AllFlag", chkShowAll.Checked ? true : false);
                //string _DocDate = dtpDocDate.Value.AddDays(-1).ToString("yyyyMMdd", new CultureInfo("en-US"));

                bool flagAllWHID = txtWHCode.TextLength > 6 ? true : false;
                var dt = bu.proc_GetRLData(_params, flagAllWHID);

                txtRemark.Text = "";
                txtRequestBy.Text = "";
                txtFullName.Text = "";

                if (dt.Rows.Count > 0)
                {
                    SetRLData(dt);
                    if (txtWHCode.TextLength == 6)
                    {
                        txtRemark.Text = dt.Rows[0].Field<string>("Remark");
                        txtRequestBy.Text = dt.Rows[0].Field<string>("RequestBy");
                        txtFullName.Text = dt.Rows[0].Field<string>("FullName");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void btnPreOrderCustCtrl_Report_Click(object sender, EventArgs e)
        {
            if (grdList.RowCount == 0)
            {
                string msg = "ไม่พบข้อมูลใบ PreOrder !!";
                msg.ShowWarningMessage();
                return;
            }

            //string _DocNo = grdList.Rows[0].Cells["colDocNo"].Value.ToString();
            //string _DocDate = dtpDocDate.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));
            string _DocDate = dtpRLDate.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));
            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@DocNo", "");
            _params.Add("@WHID", txtWHCode.Text);
            _params.Add("@DocDateFr", _DocDate);
            _params.Add("@DocDateTo", _DocDate);
            _params.Add("@AllFlag", chkShowAll.Checked ? true : false);
            this.OpenReportingReportsPopup("รายงานใบคุมส่งสินค้าตามคลังรถ", "proc_PreOrder_Cust_Ctrl_Report2" +
                ".rdlc", "proc_PreOrder_Cust_Ctrl_Report2", _params);
        }

        private void btnRL_Report_Click(object sender, EventArgs e)
        {
            if (grdList.RowCount == 0)
            {
                string msg = "ไม่พบข้อมูลใบ PreOrder !!";
                msg.ShowWarningMessage();
                return;
            }
            
            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@AllFlag", chkShowAll.Checked ? true : false);
            if (txtWHCode.TextLength > 6) //More 1 van
            {
                _params.Add("@DocDate", dtpRLDate.Value);
                _params.Add("@WHID", txtWHCode.Text);
                _params.Add("@UserName", Helper.tbl_Users.FirstName + ' ' + Helper.tbl_Users.LastName);
                this.OpenReportingReportsPopup("ใบจัดสินค้า", "Form_RL_Report2_AllWh.rdlc", "Form_RL_PRE_WH", _params);
            }
            else //single van
            {
                _params.Add("@DocNo", grdList.Rows[0].Cells["colDocNo"].Value.ToString());
                this.OpenReportingReportsPopup("ใบจัดสินค้า", "Form_RL_Report2.rdlc", "Form_RL_PRE", _params);
            }
          
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProductSlip_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        #endregion
    }
}
