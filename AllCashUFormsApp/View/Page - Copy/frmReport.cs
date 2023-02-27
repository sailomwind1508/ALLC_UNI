using AllCashUFormsApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.View.UControl;
using AllCashUFormsApp.View.AControl;
using System.Globalization;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmReport : Form
    {
        public static string _txtBW = "";
        public static string _txtPro = "";
        public static string _txtSubGroupPro = "";
        public static string _txtSubGroupPro2 = "";
        public static string _txtProType = "";
        public static string _RptStock = "";
        Report bu = new Report();
        MenuBU menuBU = new MenuBU();
        List<Control> searchBranchControls = new List<Control>();
        List<Control> searchFromWHControls = new List<Control>();
        List<Control> searchToWHControls = new List<Control>();
        List<string> PanelList = new List<string>() { "pnlMainPage", "pnlDocType", "pnlDistribution", "pnlBranchWH", "pnlProSubGroup", "pnlProID", "pnlProType" };
        public frmReport()
        {
            InitializeComponent();
            searchBranchControls = new List<Control>() { txtBranchCode };
            searchFromWHControls = new List<Control>() { txtWHCode_FromWH, txtWHName_FromWH };
            searchToWHControls = new List<Control>() { txtWHCode_ToWH, txtWHName_ToWH };
        }

        #region private methods

        private void pnl_V()
        {
            pnlDocType.Visible = false;
            pnlBranchWH.Visible = false;
            pnlProSubGroup.Visible = false;
            pnlProID.Visible = false;
            pnlProType.Visible = false;

            pnlFromWHID.Visible = false;
            pnlToWHID.Visible = false;
        }

        private void BindBranch()
        {
            var b = bu.GetBranch();
            if (b != null)
            {
                this.BindData("FromBranchID", searchBranchControls, b[0].BranchID);
            }
        }

        private void BindDropDown(ComboBox ddl1, ComboBox ddl2, ComboBox ddl3, ComboBox ddl4)
        {
            ddl1.Items.Clear();
            ddl2.Items.Clear();
            ddl3.Items.Clear();
            ddl4.Items.Clear();

            for (int i = 2564; i > 2553; i--)
            {
                ddl1.Items.Add(i);
                ddl2.Items.Add(i);
            }

            for (int i = 1; i <= 13; i++)
            {
                ddl3.Items.Add(i);
                ddl4.Items.Add(i);
            }

            ddl1.SelectedIndex = 0;
            ddl2.SelectedIndex = 0;
            ddl3.SelectedIndex = 0;
            ddl4.SelectedIndex = 0;
        }

        private void Control_V_btn(CheckBox chk, Button btn, TextBox txt)
        {
            if (chk.Checked == true)
            {
                btn.Enabled = false;
                txt.Clear();
                txt.DisableTextBox(true);
            }
            else
            {
                btn.Enabled = true;
                txt.Clear();
                txt.DisableTextBox(true);
            }
        }

        private void Call9Condition(string formName, string reportName, string storeName)
        {
            int year = -1;
            int month = -1;
            DateTime cDate = DateTime.Now;

            string branchID = bu.GetBranch()[0].BranchID;
            string prdSubGroupID = txtProSubGroup.Text;
            string prdID = txtProID.Text;

            Dictionary<string, object> _params = new Dictionary<string, object>();

            _params.Add("@DocStatus", GetDocStatus());
            if (rdoCycle.Checked)
            {
                year = Convert.ToInt32(ddlFromToYear.Text) - 543;
                month = Convert.ToInt32(ddlAroundFromYear.Text);
                _params.Add("@DateFr", cDate);
                _params.Add("@DateTo", cDate);
            }
            else if (rdoDaily.Checked)
            {
                _params.Add("@DateFr", dtpFromToD.Value);
                if (dtpToD.Enabled)
                    _params.Add("@DateTo", dtpToD.Value);
                else
                    _params.Add("@DateTo", dtpFromToD.Value);
            }

            _params.Add("@WHID", txtWHCode.Text);

            _params.Add("@BranchID", branchID);
            _params.Add("@Year", year);
            _params.Add("@Month", month);
            _params.Add("@ProductSubGroupID", prdSubGroupID);
            _params.Add("@ProductID", prdID);

            this.OpenCrystalReportsPopup(formName, reportName, storeName, _params);
        }

        private int GetDocStatus()
        {
            int docStatus = -1;
            try
            {
                if (rdoN.Checked == true)
                    docStatus = 4;
                else if (rdoC.Checked == true)
                    docStatus = 5;
                else if (rdoALL.Checked == true)
                    docStatus = -1;
            }
            catch (Exception)
            {

            }

            return docStatus;
        }

        private void InitPage()
        {
            var menu = bu.GetAllFromMenu().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }
            var headerPic = menuBU.GetAllData().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            dtpFromToD.SetDateTimePickerFormat();
            dtpToD.SetDateTimePickerFormat();
        }
        private void PreParePanelANDTextbox()
        {
            pnlDistribution.Visible = true;
            pnlBranchWH.Visible = false;
            pnlProSubGroup.Visible = false;
            pnlProID.Visible = false;
            pnlProType.Visible = false;

            pnlDocType.Visible = false; //เลือกประเภทเอกสาร

            pnlFromWHID.Visible = false;
            pnlToWHID.Visible = false;

            txtBranchCode.DisableTextBox(true);
            txtWHCode.DisableTextBox(true);
            txtProSubGroup.DisableTextBox(true);
            txtProID.DisableTextBox(true);
            txtProType.DisableTextBox(true);
            txtWHName_FromWH.DisableTextBox(true);
            txtWHName_ToWH.DisableTextBox(true);
        }
        private void InitialData()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnPrint.Enabled = true;

            BindBranch();
            dtpToD.Enabled = false;

            rdoDaily.Checked = true;
            //groupCycle.Visible = false;

            PreParePanelANDTextbox();
        }

        private void VisibleMainPanel()
        {
            pnlMainPage.Visible = true;
            pnlDocType.Visible = false;
            pnlDistribution.Visible = true;
            pnlBranchWH.Visible = false;
            pnlProSubGroup.Visible = false;
            pnlProID.Visible = false;
            pnlProType.Visible = false;
            pnlFromWHID.Visible = false;
            pnlToWHID.Visible = false;

            //BindBranch();
        }

        private void SubSecondCriteria(Control obj)
        {
            foreach (var c in obj.Controls)
            {
                if (c is TextBox)
                {
                    var txt = c as TextBox;
                    if (txt != null)
                    {
                        if (txt.Name != "txtBranchCode")
                        {
                            txt.Text = string.Empty;
                        }
                    }
                }
                else if (c is ComboBox)
                {
                    var cbo = c as ComboBox;
                    if (cbo != null)
                    {
                        if (cbo.Items.Count > 0)
                            cbo.SelectedIndex = 0;
                    }
                }
                else if (c is RadioButton)
                {
                    var rdo = c as RadioButton;
                    if (rdo != null)
                    {
                        rdo.Checked = false;
                    }

                }
                else if (c is CheckBox)
                {
                    var chk = c as CheckBox;
                    if (chk != null)
                    {
                        chk.Checked = false;
                    }
                }
                else if (c is DateTimePicker)
                {
                    var dtp = c as DateTimePicker;
                    if (dtp != null)
                    {
                        dtp.Value = DateTime.Now;
                    }
                }
            }
        }

        private void SubClearCriteria(Panel obj)
        {
            SubSecondCriteria(obj);
        }

        private void SubClearCriteria(GroupBox obj)
        {
            SubSecondCriteria(obj);
        }

        private void ClearCriteria()
        {
            //pnlMainPage", "pnlDocType", "pnlDistribution", "pnlBranchWH", "pnlProSubGroup", "pnlProID", "pnlProType"
            SubClearCriteria(grbSingleCycle);
            SubClearCriteria(groupCycle);
            SubClearCriteria(groupDaily);
            SubClearCriteria(pnlDocType);
            SubClearCriteria(pnlDistribution);
            SubClearCriteria(pnlBranchWH);
            SubClearCriteria(pnlProSubGroup);
            SubClearCriteria(pnlProID);
            SubClearCriteria(pnlProType);

            rdoRangeD.Enabled = true;
            pnlFromWHID.Visible = false;
            pnlToWHID.Visible = false;
        }

        #endregion

        #region event methods

        private void frmReport_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }

        private void btnSearchBranchWarehouse_Click(object sender, EventArgs e)
        {
            frmSearchBranchWareHouseList _frm = new frmSearchBranchWareHouseList();
            _frm.ShowDialog();
            if (_txtBW != "")
            {
                txtWHCode.Text = _txtBW;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProID_Click(object sender, EventArgs e)
        {
            _txtSubGroupPro2 = txtProSubGroup.Text; //งง
            frmSearchProduct _frm = new frmSearchProduct();
            _frm.ShowDialog();

            if (_txtPro != "")
            {
                txtProID.Text = _txtPro;
            }
        }

        private void btnProSubGroup_Click(object sender, EventArgs e)
        {
            frmSearchProductSubGroup _frm = new frmSearchProductSubGroup();
            _frm.ShowDialog();
            if (_txtSubGroupPro != "")
            {
                txtProSubGroup.Text = _txtSubGroupPro;
            }
        }

        private void btnProType_Click(object sender, EventArgs e)
        {
            frmSearchProductType _frm = new frmSearchProductType();
            _frm.ShowDialog();
            if (_txtProType != "")
            {
                txtProType.Text = _txtProType;
            }
        }

        private void rdoDaily_CheckedChanged(object sender, EventArgs e)
        {
            groupDaily.Visible = true;
            groupCycle.Visible = false;
            rdoSingleD.Checked = true;
        }

        private void rdoCycle_CheckedChanged(object sender, EventArgs e)
        {
            groupDaily.Visible = false;
            groupCycle.Visible = true;
            rdoSingleC.Checked = true;

            BindDropDown(ddlFromToYear, ddlToYear, ddlAroundFromYear, ddlAroundToYear);
        }

        private void btnDistribution_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกสาขา/ซุ้ม");
        }

        private void rdoSingleD_CheckedChanged(object sender, EventArgs e)
        {
            dtpToD.Enabled = false;
        }

        private void rdoRangeD_CheckedChanged(object sender, EventArgs e)
        {
            dtpToD.Enabled = true;
        }

        private void rdoSingleC_CheckedChanged(object sender, EventArgs e)
        {
            label4.Enabled = false;
            ddlToYear.Enabled = false;
            label7.Enabled = false;
            ddlAroundToYear.Enabled = false;
        }

        private void rdoRangeC_CheckedChanged(object sender, EventArgs e)
        {
            label4.Enabled = true;
            ddlToYear.Enabled = true;
            label7.Enabled = true;
            ddlAroundToYear.Enabled = true;
        }

        private void chkboxAllDistribution_CheckedChanged(object sender, EventArgs e)
        {
            Control_V_btn(chkDistribution, btnDistribution, txtBranchCode);
        }

        private void chkEmp_CheckedChanged(object sender, EventArgs e)
        {
            Control_V_btn(chkEmp, btnBranchWarehouse, txtWHCode);
        }

        private void chkProSubGroup_CheckedChanged(object sender, EventArgs e)
        {
            Control_V_btn(chkProSubGroup, btnProSubGroup, txtProSubGroup);
        }

        private void chkProID_CheckedChanged(object sender, EventArgs e)
        {
            Control_V_btn(chkProID, btnProID, txtProID);
        }

        private void chkProType_CheckedChanged(object sender, EventArgs e)
        {
            Control_V_btn(chkProType, btnProType, txtProType);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// select report menu event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                ClearCriteria();

                label1.Text = e.Node.Text;
                if (e.Node.Name == "Node1")
                { // การขาย

                    VisibleMainPanel();

                    chkDistribution.Enabled = true;
                    rdoCycle.Enabled = true;
                    rdoDaily.Checked = true;

                    pnl_V();
                }
                else if (e.Node.Name == "Node2" || e.Node.Name == "Node3")
                { //อย่างย่อ || เต็มรูป
                    VisibleMainPanel();

                    chkDistribution.Enabled = false;
                    rdoCycle.Enabled = false;
                    rdoDaily.Checked = true;

                    pnl_V();
                }
                else if (e.Node.Name == "Node4" || e.Node.Name == "Node6" || e.Node.Name == "Rpt_ActualSalesByBill" || e.Node.Name == "Rpt_ActualSalesByVan" || e.Node.Name == "Node9")
                { //สรุปยอดขายแยกตามเอกสาร //รายงานรายละเอียดขายสินค้า(แยกตามเอกสาร)  //รายงานรายละเอียดขายสินค้า (แยกตามลูกค้า) 
                    pnlMainPage.Visible = true;
                    pnlDocType.Visible = true;
                    pnlDistribution.Visible = true;
                    pnlBranchWH.Visible = true;

                    if (e.Node.Name == "Node4" || e.Node.Name == "Node9")
                        pnlProSubGroup.Visible = false;
                    else
                        pnlProSubGroup.Visible = true;

                    pnlProID.Visible = false;
                    pnlProType.Visible = false;

                    BindBranch();
                    rdoN.Checked = true;
                    chkEmp.Visible = true;
                    btnBranchWarehouse.Visible = true;
                    txtWHCode.Clear();
                    txtWHCode.DisableTextBox(true);

                    chkDistribution.Enabled = false;
                    chkEmp.Enabled = false;

                    rdoCycle.Enabled = false;
                    rdoALL.Visible = true;
                    rdoDaily.Checked = true;

                    if (e.Node.Name == "Rpt_ActualSalesByBill")
                    {
                        pnlProID.Visible = true;
                    }
                }
                else if (e.Node.Name == "Node5" || e.Node.Name == "Node8")
                {//รายงานยอดขายแยกตามลูกค้า
                    pnlMainPage.Visible = true;
                    pnlDocType.Visible = true;
                    pnlDistribution.Visible = true;
                    pnlBranchWH.Visible = true;

                    pnlProSubGroup.Visible = false;
                    pnlProID.Visible = false;
                    pnlProType.Visible = false;

                    BindBranch();
                    rdoN.Checked = true;
                    rdoALL.Visible = false;
                    rdoCycle.Enabled = false;
                    rdoDaily.Checked = true;

                    chkDistribution.Enabled = false;
                    btnDistribution.Enabled = true;

                    txtBranchCode.DisableTextBox(true);
                    txtWHCode.Clear();
                    txtWHCode.DisableTextBox(true);

                }
                else if (e.Node.Name == "Node7") //รายงานรายละเอียดขายสินค้า (แยกตามลูกค้า)
                {
                    pnlMainPage.Visible = true;
                    pnlDocType.Visible = true;
                    pnlDistribution.Visible = true;
                    pnlProSubGroup.Visible = true;
                    pnlBranchWH.Visible = true;

                    pnlProID.Visible = true;
                    pnlProType.Visible = false;

                    BindBranch();
                    rdoN.Checked = true;
                    rdoALL.Visible = true;
                    rdoCycle.Enabled = false;
                    rdoDaily.Checked = true;

                    chkDistribution.Enabled = false;
                    btnDistribution.Enabled = true;

                    txtBranchCode.DisableTextBox(true);

                    txtWHCode.Clear();
                    txtWHCode.DisableTextBox(true);

                    txtProSubGroup.Clear();
                    txtProSubGroup.DisableTextBox(true);

                    txtProID.Clear();
                    txtProID.DisableTextBox(true);

                }
                else if (e.Node.Name == "Node9") //รายงานการขายประจำวัน
                {
                    pnlDocType.Visible = true;
                    pnlDistribution.Visible = true;
                    pnlBranchWH.Visible = true;

                    BindBranch();
                    rdoN.Checked = true;
                    rdoALL.Visible = false;
                    rdoCycle.Enabled = false;
                    rdoDaily.Checked = true;

                    chkDistribution.Enabled = false;
                    btnDistribution.Enabled = true;

                    txtBranchCode.DisableTextBox(true);
                    txtWHCode.Clear();
                    txtWHCode.DisableTextBox(true);
                }
                else if (e.Node.Name == "NodeBill" || e.Node.Name == "NodeBaht" || e.Node.Name == "NodeBahtExcVat" || e.Node.Name == "NodeBrick" || e.Node.Name == "NodeCarton")
                {//รายงานบิลขาย(นับทุกบิล),รายงานยอดขาย(บาท),รายงานยอดขายไม่รวมVat(บาท),รายงานยอดขาย(หน่วยเล็ก),รายงานยอดขาย(หน่วยใหญ่)
                    pnl_V();
                    pnlMainPage.Visible = true;
                    pnlDistribution.Visible = true;

                    rdoDaily.Checked = true;

                    rdoRangeD.Checked = true;
                    chkDistribution.Enabled = false;
                }

                //else if ()
                //{
                //    BindBranch();

                //    chkDistribution.Enabled = false;

                //    rdoDaily.Checked = true;
                //    rdoSingleC.Checked = true;
                //    rdoN.Checked = true;

                //    pnlDocType.Visible = true;
                //    pnlDistribution.Visible = true;
                //    pnlBranchWH.Visible = true;
                //    pnlProSubGroup.Visible = true;
                //    pnlProID.Visible = true;
                //    pnlProType.Visible = false;

                //    txtWHCode.Clear();
                //    txtProSubGroup.Clear();
                //    txtProID.Clear();

                //    txtWHCode.DisableTextBox(true);
                //    txtProSubGroup.DisableTextBox(true);
                //    txtProID.DisableTextBox(true);
                //}
                else if (e.Node.Name == "Node24")
                {//รายงานสินค้าคงเหลือ แยกตามคลัง
                    pnlMainPage.Visible = true;
                    pnlDocType.Visible = false;

                    pnlProID.Visible = false;
                    pnlProType.Visible = false;

                    chkDistribution.Checked = false;
                    chkEmp.Checked = false;
                    chkProSubGroup.Checked = false;

                    txtBranchCode.Clear();
                    txtWHCode.Clear();
                    txtProSubGroup.Clear();

                    pnlDistribution.Visible = true;
                    pnlBranchWH.Visible = true;
                    pnlProSubGroup.Visible = true;
                    rdoDaily.Checked = true;

                    //จุดกระจาย emp group
                    BindBranch();
                    rdoRangeD.Enabled = false;
                    _RptStock = "ALL";
                }
                else if (e.Node.Name == "Node28") //รายงานรายละเอียดโอนสินค้า
                {
                    pnl_V();
                    pnlMainPage.Visible = true;
                    pnlDistribution.Visible = true;
                    rdoDaily.Checked = true;
                    rdoRangeD.Checked = true;
                    chkDistribution.Enabled = false;
                    // pnlProSubGroup.Visible = true;
                    //pnlProID.Visible = true;
                    pnlFromWHID.Visible = true;
                    pnlToWHID.Visible = true;

                    // txtProSubGroup.Clear();
                    // txtProID.Clear();

                    txtWHCode_FromWH.Clear();
                    txtWHName_FromWH.Clear();
                    txtWHCode_ToWH.Clear();
                    txtWHName_ToWH.Clear();

                }
                else if (e.Node.Name == "Node30")
                {// Distribution
                    pnlMainPage.Visible = true;
                    pnlDocType.Visible = false;
                    pnlDistribution.Visible = true;
                    pnlBranchWH.Visible = false;
                    pnlProSubGroup.Visible = false;
                    pnlProID.Visible = false;
                    pnlProType.Visible = false;

                    rdoCycle.Enabled = true;
                    rdoCycle.Checked = true;
                    rdoSingleC.Checked = true;
                    BindBranch();

                    chkDistribution.Enabled = true;
                    chkDistribution.Checked = false;
                    btnDistribution.Enabled = true;
                    txtBranchCode.DisableTextBox(true);
                }
                //else if (e.Node.Name == "Node33" || e.Node.Name == "Node34")
                //{ //รายงานยอดขาย DSR  //DSR SKU
                //    pnlMainPage.Visible = true;
                //    pnlDocType.Visible = false;
                //    pnlDistribution.Visible = true;
                //    pnlBranchWH.Visible = true;
                //    pnlProSubGroup.Visible = false;
                //    pnlProID.Visible = false;
                //    pnlProType.Visible = true;

                //    rdoCycle.Enabled = true;
                //    rdoCycle.Checked = true;
                //    rdoSingleC.Checked = true;


                //    chkDistribution.Checked = false;
                //    chkDistribution.Enabled = false;
                //    txtBranchCode.DisableTextBox(true);

                //    chkEmp.Enabled = false;
                //    txtWHCode.Clear();
                //    BindBranch();
                //    txtWHCode.DisableTextBox(true);
                //    chkProSubGroup.Enabled = false;

                //    chkProType.Enabled = true;
                //    btnProType.Enabled = true;
                //    txtProType.Clear();
                //    txtProType.DisableTextBox(true);
                //}
                else if (e.Node.Name == "Node48" || e.Node.Name == "Node50" || e.Node.Name == "Node53" || 
                    e.Node.Name == "Node51" || e.Node.Name == "Node52" || e.Node.Name == "Node0" || e.Node.Name == "Node34")
                {
                    //รายงานสรุป Shelf สินค้า
                    pnlMainPage.Visible = true;
                    pnlDistribution.Visible = true;
                    pnlProSubGroup.Visible = true;
                    pnlProID.Visible = true;

                    pnlDocType.Visible = false;
                    pnlBranchWH.Visible = false;
                    pnlProType.Visible = false;

                    BindBranch();

                    rdoCycle.Checked = true;
                    rdoSingleC.Checked = true;

                    chkDistribution.Enabled = false;
                    chkProSubGroup.Enabled = false;
                    chkProID.Enabled = false;

                    txtProSubGroup.ClearTextBox(); ;
                    txtProSubGroup.DisableTextBox(true);
                    txtProID.ClearTextBox();
                    txtProID.DisableTextBox(true);
                    txtWHCode.DisableTextBox(true);

                    rdoDaily.Enabled = false;
                    rdoCycle.Enabled = true;
                    rdoCycle.Checked = true;

                    if (e.Node.Name == "Node50" || e.Node.Name == "Node53" || e.Node.Name == "Node51" || e.Node.Name == "Node52" || e.Node.Name == "Node34")
                    {
                        rdoDaily.Enabled = true;
                        rdoDaily.Checked = true;
                        rdoCycle.Checked = false;
                        if (e.Node.Name == "Node51" || e.Node.Name == "Node52" || e.Node.Name == "Node34")
                        {
                            rdoCycle.Enabled = false;
                        }
                    }
                    if (e.Node.Name == "Node53")
                    {
                        pnlProSubGroup.Visible = false;
                        pnlProID.Visible = false;
                    }
                }
                else if (e.Node.Name == "Node52")
                {

                }
                else
                {
                    pnlMainPage.Visible = false;
                    pnlDocType.Visible = false;
                    pnlDistribution.Visible = false;
                    pnlBranchWH.Visible = false;
                    pnlProSubGroup.Visible = false;
                    pnlProID.Visible = false;
                    pnlProType.Visible = false;
                    pnlFromWHID.Visible = false;
                    pnlToWHID.Visible = false;
                }

            }
            catch (Exception ex)
            {

                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        /// <summary>
        /// print event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (label1.Text == "รายงานสรุปยอดขาย แยกตามเอกสาร")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DocStatus", GetDocStatus());
                    _params.Add("@DateFr", dtpFromToD.Value);

                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);

                    _params.Add("@WHID", txtWHCode.Text);
                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                    this.OpenCrystalReportsPopup("รายงานสรุปยอดขาย แยกตามเอกสาร", "Rep_Sales_By_Doc.rpt", "Rep_Sales_By_Doc", _params);
                }
                //if (label1.Text == "รายงานการขายประจำวัน")
                //{
                //    Dictionary<string, object> _params = new Dictionary<string, object>();
                //    _params.Add("@DocStatus", GetDocStatus());
                //    _params.Add("@DateFr", dtpFromToD.Value);

                //    if (dtpToD.Enabled)
                //        _params.Add("@DateTo", dtpToD.Value);
                //    else
                //        _params.Add("@DateTo", dtpFromToD.Value);

                //    _params.Add("@WHID", txtWHCode.Text);
                //    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                //    this.OpenCrystalReportsPopup("รายงานสรุปยอดขาย แยกตามเอกสาร", "Rep_Sum_Sales_Daily.5.rpt", "Rep_Sum_Sales_Daily", _params);
                //}
                else if (label1.Text == "รายงานยอดขาย แยกตามลูกค้า")//Node5
                {
                    int dateYear = dtpFromToD.Value.Year;
                    int dateMonth = dtpFromToD.Value.Month;
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@monthIn", dateMonth);
                    _params.Add("@YearIn", dateYear);

                    this.OpenCrystalReportsPopup("รายงานยอดขาย แยกตามลูกค้า", "ActualSales_By_Day.rpt", "proc_Actualsales_by_day", _params);
                }

                else if (label1.Text == "รายงานภาษีขายอย่างย่อ")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DateFr", dtpFromToD.Value);
                    _params.Add("@DateFr", dtpFromToD.Value);

                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);
                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                    this.OpenCrystalReportsPopup("รายงานภาษีขายอย่างย่อ", "Rep_IV_Sales.rpt", "Rep_IV_Sales", _params);
                }
                else if (label1.Text == "รายงานภาษีขายเต็มรูป")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DateFr", dtpFromToD.Value);
                    _params.Add("@DateFr", dtpFromToD.Value);

                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);
                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                    this.OpenCrystalReportsPopup("รายงานภาษีขายเต็มรูป", "Rev_V_Sales.rpt", "Rep_V_Sales", _params);
                }
                else if (label1.Text == "รายงานบิลขาย(นับทุกบิล)")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DateFr", dtpFromToD.Value);

                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);

                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                    this.OpenCrystalReportsPopup("รายงานบิลขาย(นับทุกบิล)", "Rep_BillByDate_AllBill.rpt", "Rep_BillByDate_AllBill", _params);
                }
                else if (label1.Text == "รายงานรายละเอียดขายสินค้า (แยกตามลูกค้า)")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DateFr", dtpFromToD.Value);

                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);

                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));
                    _params.Add("@WHID", txtWHCode.Text);
                    _params.Add("@ProductSubGroupCode", txtProSubGroup.Text);
                    _params.Add("@ProductID", txtProID.Text);
                    this.OpenCrystalReportsPopup("รายงานรายละเอียดขายสินค้า (แยกตามลูกค้า)", "Rep_Detail_Sales_By_Cust.rpt", "Rep_Detail_Sales_By_Cust", _params);
                }
                else if (label1.Text == "รายงานการขายประจำวัน")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DateFr", dtpFromToD.Value);
                    
                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);

                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));
                    _params.Add("@WHID", txtWHCode.Text);
                    this.OpenCrystalReportsPopup("รายงานการขายประจำวัน", "Rep_Daily_Sales.rpt", "Rep_Daily_Sales", _params);
                }
                else if (label1.Text == "รายงานยอดขาย(บาท)")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DateFr", dtpFromToD.Value);

                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);

                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                    this.OpenCrystalReportsPopup("รายงานยอดขาย(บาท)", "Rep_BillByDate_TotalDue.rpt", "Rep_BillByDate_TotalDue", _params);
                }
                else if (label1.Text == "รายงานยอดขายไม่รวมVat(บาท)")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DateFr", dtpFromToD.Value);

                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);

                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                    this.OpenCrystalReportsPopup("รายงานยอดขายไม่รวมVat(บาท)", "Rep_BillByDate_ExcVat.rpt", "Rep_BillByDate_ExcVat", _params);
                }
                else if (label1.Text == "รายงานยอดขาย(หน่วยเล็ก)")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DateFr", dtpFromToD.Value);

                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);

                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                    this.OpenCrystalReportsPopup("รายงานยอดขาย(หน่วยเล็ก)", "Rep_BillByDate_Brick.rpt", "Rep_BillByDate_Brick", _params);
                }
                else if (label1.Text == "รายงานยอดขาย(หน่วยใหญ่)")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DateFr", dtpFromToD.Value);

                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);

                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                    this.OpenCrystalReportsPopup("รายงานยอดขาย(หน่วยใหญ่)", "Rep_BillByDate_Carton.rpt", "Rep_BillByDate_Carton", _params);
                }
                else if (label1.Text == "รายงานสินค้าคงเหลือ แยกตามคลัง")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@WHID", txtWHCode.Text);
                    _params.Add("@DocDate", dtpFromToD.Value);

                    this.OpenCrystalReportsPopup("รายงานสินค้าคงเหลือ แยกตามคลัง", "RptStock_R3.rpt", "proc_RPTStock", _params);
                }
                else if (label1.Text == "รายงานรายละเอียดโอนสินค้า")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DateFr", dtpFromToD.Value);

                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);

                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));
                    string fromWh = txtBranchCode.Text + txtWHCode_FromWH.Text;
                    string toWh = txtBranchCode.Text + txtWHCode_ToWH.Text;

                    _params.Add("@FromWH", fromWh);
                    _params.Add("@ToWH", toWh);
                    this.OpenCrystalReportsPopup("รายงานรายละเอียดโอนสินค้า", "Rep_Product_Transfer.rpt", "Rep_Product_Transfer", _params);
                }
                else if (label1.Text == "รายงานสรุปShelfสินค้า")
                {
                    int YearFr = Convert.ToInt32(ddlFromToYear.Text) - 543;
                    int YearTo = Convert.ToInt32(ddlToYear.Text) - 543;

                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@YearFr", YearFr);
                    _params.Add("@YearTo", YearTo);
                    _params.Add("@MonthFr", Convert.ToInt32(ddlAroundFromYear.Text));

                    if (ddlAroundToYear.Enabled)
                        _params.Add("@MonthTo", Convert.ToInt32(ddlAroundToYear.Text));
                    else
                        _params.Add("@MonthTo", Convert.ToInt32(ddlAroundFromYear.Text));

                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                    this.OpenCrystalReportsPopup("รายงานสรุปShelfสินค้า", "Rep_Sum_Shelf.rpt", "Rep_Sum_Shelf", _params);
                }
                else if (label1.Text == "รายงานสัดส่วน")
                {
                    int year = -1;
                    int month = -1;
                    DateTime cDate = DateTime.Now;

                    string branchID = bu.GetBranch()[0].BranchID;
                    string prdSubGroupID = ""; // txtProSubGroup.Text;
                    string prdID = ""; // txtProID.Text;

                    Dictionary<string, object> _params = new Dictionary<string, object>();

                    if (rdoCycle.Checked)
                    {
                        year = Convert.ToInt32(ddlFromToYear.Text) - 543;
                        month = Convert.ToInt32(ddlAroundFromYear.Text);
                        _params.Add("@DateFr", cDate);
                        _params.Add("@DateTo", cDate);
                    }
                    else if (rdoDaily.Checked)
                    {
                        _params.Add("@DateFr", dtpFromToD.Value);
                        if (dtpToD.Enabled)
                            _params.Add("@DateTo", dtpToD.Value);
                        else
                            _params.Add("@DateTo", dtpFromToD.Value);
                    }

                    _params.Add("@Year", year);
                    _params.Add("@Month", month);
                    _params.Add("@BranchID", branchID);
                    _params.Add("@ProductSubGroupID", prdSubGroupID);
                    _params.Add("@ProductID", prdID);

                    this.OpenCrystalReportsPopup("รายงานสัดส่วน", "RPT-DSR2.rpt", "proc_Rpt_DSR", _params);
                }
                else if (label1.Text == "รายงานสัดส่วน(KPI)")
                {
                    int year = Convert.ToInt32(ddlFromToYear.Text) - 543;

                    Dictionary<string, object> _params = new Dictionary<string, object>();

                    _params.Add("@DateFr", dtpFromToD.Value);
                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);

                    _params.Add("@Year", year);
                    _params.Add("@Month", Convert.ToInt32(ddlAroundFromYear.Text));
                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                    this.OpenCrystalReportsPopup("รายงานสัดส่วน (KPI)", "RPT-DSR-KPI.rpt", "proc_Rpt_DSR_KPI", _params);
                }
                else if (label1.Text == "รายงาน Eff.Call (KPI)")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();

                    _params.Add("@DateFr", dtpFromToD.Value);
                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);

                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                    this.OpenCrystalReportsPopup("รายงาน Eff.Call(KPI)", "Rep_DSR_EffectiveCall_KPI2.rpt", "Rep_DSR_EffectiveCall_KPI", _params); //wait for edit----------------------
                }
                else if (label1.Text == "รายงานสรุปยอดขาย")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();

                    _params.Add("@FDate", dtpFromToD.Value);
                    if (dtpToD.Enabled)
                        _params.Add("@TDate", dtpToD.Value);
                    else
                        _params.Add("@TDate", dtpFromToD.Value);

                    this.OpenCrystalReportsPopup("รายงานสรุปยอดขาย", "RPT-DSR-Summary.rpt", "proc_DSR_Sales_Summary", _params);
                }
                else if (label1.Text == "รายงานยอดขาย DSR (SKU)")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();

                    _params.Add("@FDate", dtpFromToD.Value);
                    if (dtpToD.Enabled)
                        _params.Add("@TDate", dtpToD.Value);
                    else
                        _params.Add("@TDate", dtpFromToD.Value);

                    this.OpenCrystalReportsPopup("รายงานยอดขาย DSR (SKU)", "RPT-DSR-Summary-By-SKU.rpt", "proc_DSR_Sales_Summary_By_Sku", _params);
                }
                else if (label1.Text == "รายงานยอดขาย DSR (SKU)")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();

                    _params.Add("@FDate", dtpFromToD.Value);
                    if (dtpToD.Enabled)
                        _params.Add("@TDate", dtpToD.Value);
                    else
                        _params.Add("@TDate", dtpFromToD.Value);

                    this.OpenCrystalReportsPopup("รายงานยอดขาย DSR (SKU)", "RPT-DSR-Summary-By-SKU.rpt", "proc_DSR_Sales_Summary_By_Sku", _params);
                }
                else if (label1.Text == "รายงานยอดขาย แยกตามลูกค้า")
                {
                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DocStatus", GetDocStatus());
                    _params.Add("@DateFr", dtpFromToD.Value);

                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);

                    _params.Add("@WHID", txtWHCode.Text);
                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                    this.OpenCrystalReportsPopup("รายงานยอดขาย แยกตามลูกค้า", "Rep_Sales_By_Customer.rpt", "Rep_Sales_By_Customer", _params);
                }
                else if (label1.Text == "รายงานสรุปยอดขายแยกตามพนักงาน (รายวัน/รายเดือน)") //รายงานสรุปยอดขายแยกตามพนักงาน (รายวัน/รายเดือน)
                {
                    int year = -1;
                    int month = -1;
                    DateTime cDate = DateTime.Now;

                    string branchID = bu.GetBranch()[0].BranchID;
                    string prdSubGroupID = ""; // txtProSubGroup.Text;
                    string prdID = ""; // txtProID.Text;

                    Dictionary<string, object> _params = new Dictionary<string, object>();

                    _params.Add("@DocStatus", GetDocStatus());
                    if (rdoCycle.Checked)
                    {
                        year = Convert.ToInt32(ddlFromToYear.Text) - 543;
                        month = Convert.ToInt32(ddlAroundFromYear.Text);
                        _params.Add("@DateFr", cDate);
                        _params.Add("@DateTo", cDate);
                    }
                    else if (rdoDaily.Checked)
                    {
                        _params.Add("@DateFr", dtpFromToD.Value);
                        if (dtpToD.Enabled)
                            _params.Add("@DateTo", dtpToD.Value);
                        else
                            _params.Add("@DateTo", dtpFromToD.Value);
                    }

                    _params.Add("@WHID", txtWHCode.Text);

                    _params.Add("@BranchID", branchID);
                    _params.Add("@Year", year);
                    _params.Add("@Month", month);

                    this.OpenCrystalReportsPopup("รายงานสรุปยอดขายแยกตามพนักงาน (รายวัน/รายเดือน)", "Rep_Sales_Per_Emp.rpt", "Rep_Sales_Per_Emp", _params);
                }
                else if (label1.Text == "รายงานรายละเอียดขายสินค้า (แยกตามเอกสาร)")
                {
                    Call9Condition(label1.Text, "Rep_Sales_By_Doc_Detail.rpt", "proc_ActualSalesByBill");
                }
                else if (label1.Text == "รายงานรายละเอียดขายสินค้า (แยกตามแวน)")
                {
                    Call9Condition(label1.Text, "Rpt_ActualSalesByVan.rpt", "proc_ActualSalesByBill");
                }

                else if (label1.Text == "รายงานสรุป Shelf สินค้า (รายวัน)")
                {
                    int YearFr = Convert.ToInt32(ddlFromToYear.Text) - 543;
                    int YearTo = Convert.ToInt32(ddlToYear.Text) - 543;

                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@DateFr", dtpFromToD.Value);

                    if (dtpToD.Enabled)
                        _params.Add("@DateTo", dtpToD.Value);
                    else
                        _params.Add("@DateTo", dtpFromToD.Value);

                    _params.Add("@MonthFr", Convert.ToInt32(ddlAroundFromYear.Text));

                    if (ddlAroundToYear.Enabled)
                        _params.Add("@MonthTo", Convert.ToInt32(ddlAroundToYear.Text));
                    else
                        _params.Add("@MonthTo", Convert.ToInt32(ddlAroundFromYear.Text));

                    _params.Add("@BranchID", Convert.ToInt32(txtBranchCode.Text));

                    this.OpenCrystalReportsPopup("รายงานสรุป Shelf สินค้า (รายวัน)", "Rep_Sum_Shelf_Daily.rpt", "Rep_Sum_Shelf_Daily", _params);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        #endregion

        private void btnFromWHID_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchFromWHControls, "เลือกคลังสินค้า");
        }

        private void btnToWHID_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchToWHControls, "เลือกคลังสินค้า");
        }
        private void GetBranchWare(TextBox txtCode, TextBox txtName)
        {
            var WarehouseList = bu.GetAllBranchWarehouse(x => x.WHCode == txtCode.Text);
            txtName.Text = WarehouseList.Count > 0 ? WarehouseList.First().WHName : "";
        }
        private void txtWHCode_FromWH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetBranchWare(txtWHCode_FromWH, txtWHName_FromWH);
            }
        }

        private void txtWHCode_ToWH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetBranchWare(txtWHCode_ToWH, txtWHName_ToWH);
            }
        }
    }
}
