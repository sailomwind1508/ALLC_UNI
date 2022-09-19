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
using System.IO;
using AllCashUFormsApp.Model;

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
        public static string _SalArea = "";
        public static string _ShopType = "";
        Report bu = new Report();
        MenuBU menuBU = new MenuBU();
        List<Control> searchBranchControls = new List<Control>();
        List<Control> searchFromWHControls = new List<Control>();
        List<Control> searchToWHControls = new List<Control>();
        List<string> PanelList = new List<string>() { "pnlMainPage", "pnlDocType", "pnlDistribution", "pnlBranchWH", "pnlProSubGroup", "pnlProID", "pnlProType" };
        frmWait wait = new frmWait();

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
            CultureInfo cultures = System.Globalization.CultureInfo.GetCultureInfo("th-TH");

            ddl1.Items.Clear();
            ddl2.Items.Clear();
            ddl3.Items.Clear();
            ddl4.Items.Clear();
            var cDate = DateTime.Now.ToString("yyyy", cultures);
            int year = Convert.ToInt32(cDate);

            for (int i = year; i > (year - 10); i--)
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

            if (formName == "รายงานรายละเอียดขายสินค้า (แยกตามเอกสาร)" || formName == "รายงานรายละเอียดขายสินค้า (แยกตามแวน)")
                this.OpenExcelReportsPopup(formName, reportName, storeName, _params);
            else
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

            pnlSalArea.Visible = false;
            pnlShopType.Visible = false;
        }

        private void InitialData()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnPrint.Enabled = true;

            BindBranch();
            //dtpToD.Enabled = false;

            //rdoDaily.Checked = true;
            //groupCycle.Visible = false;

            PreParePanelANDTextbox();

            ClearCriteria();
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
            pnlShopType.Visible = false;
            pnlSalArea.Visible = false;
            pnlShopType.Visible = false;
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

            //rdoRangeD.Enabled = true;
            //pnlFromWHID.Visible = false;
            //pnlToWHID.Visible = false;
            //btnMNSock.Visible = false;

            //pnlSalArea.Visible = false;
            //pnlShopType.Visible = false;

            EnableDailyCycleCtrl(false, false, false, false, false, false, false);
            EnableDocType(false, false, false, false);
            EnableDistribution(false);
            EnableBranchWarehouse(false);
            EnableProductSubGroup(false);
            EnableProductID(false);
            EnableProductType(false);
            EnableFromWarehouse(false);
            EnableToWarehouse(false);
            EnableSaleArea(false);
            EnableShopType(false);
        }

        private void GetBranchWare(TextBox txtCode, TextBox txtName)
        {
            var WarehouseList = bu.GetAllBranchWarehouse(x => x.WHCode == txtCode.Text);
            txtName.Text = WarehouseList.Count > 0 ? WarehouseList.First().WHName : "";
        }

        /// <summary>
        /// รายวัน / รายรอบ
        /// </summary>
        /// <param name="showPanel"></param>
        /// <param name="enableDaily"></param>
        /// <param name="enableCycle"></param>
        /// <param name="enableSingle"></param>
        /// <param name="enableRang"></param>
        /// <param name="enableCycleSingle"></param>
        /// <param name="enableCycleRang"></param>
        private void EnableDailyCycleCtrl(bool showPanel, bool enableDaily, bool enableCycle, bool enableSingle, bool enableRang
            , bool enableCycleSingle, bool enableCycleRang)
        {
            grbSingleCycle.Visible = showPanel;
            rdoDaily.Enabled = enableDaily;
            rdoCycle.Enabled = enableCycle;

            rdoDaily.Checked = (rdoDaily.Enabled && rdoCycle.Enabled) || rdoDaily.Enabled;
            rdoCycle.Checked = rdoCycle.Enabled && !rdoDaily.Enabled;

            bool isShowDailyPanel = showPanel && rdoDaily.Checked;
            bool isShowCyclePanel = showPanel && rdoCycle.Checked;

            EnableDailyCtrl(isShowDailyPanel, enableSingle, enableRang);
            EnableCycleCtrl(isShowCyclePanel, enableCycleSingle, enableCycleRang);

            btnMNSock.Visible = false;
        }

        /// <summary>
        /// Daily Panel
        /// </summary>
        /// <param name="showPanel"></param>
        /// <param name="enableSingle"></param>
        /// <param name="enableRang"></param>
        private void EnableDailyCtrl(bool showPanel, bool enableSingle, bool enableRang)
        {
            groupDaily.Visible = showPanel;
            rdoSingleD.Enabled = enableSingle;
            rdoRangeD.Enabled = enableRang;

            rdoSingleD.Checked = (rdoSingleD.Enabled && rdoRangeD.Enabled) || rdoSingleD.Enabled;
            rdoRangeD.Checked = rdoRangeD.Enabled && !rdoSingleD.Enabled;

            dtpFromToD.Enabled = rdoSingleD.Enabled || rdoRangeD.Enabled ? true : false;
            dtpToD.Enabled = rdoRangeD.Checked;
        }

        /// <summary>
        /// Cycle Panel
        /// </summary>
        /// <param name="showPanel"></param>
        /// <param name="enableSingle"></param>
        /// <param name="enableRang"></param>
        private void EnableCycleCtrl(bool showPanel, bool enableSingle, bool enableRang)
        {
            groupCycle.Visible = showPanel;
            rdoSingleC.Enabled = enableSingle;
            rdoRangeC.Enabled = enableRang;

            ddlFromToYear.Enabled = rdoSingleC.Enabled || rdoRangeC.Enabled ? true : false;
            ddlAroundFromYear.Enabled = rdoSingleC.Enabled || rdoRangeC.Enabled ? true : false;

            rdoSingleC.Checked = (rdoSingleC.Enabled && rdoRangeC.Enabled) || rdoSingleC.Enabled;
            rdoRangeC.Checked = rdoRangeC.Enabled && !rdoSingleC.Enabled;

            ddlToYear.Enabled = rdoRangeC.Checked;
            ddlAroundToYear.Enabled = rdoRangeC.Checked;
        }

        private void EnableDocType(bool showPanel, bool enableNormal, bool enableClose, bool enableAll)
        {
            pnlDocType.Visible = showPanel;
            rdoN.Enabled = enableNormal;
            rdoC.Enabled = enableClose;
            rdoALL.Enabled = enableAll;
            rdoALL.Visible = enableAll;

            rdoN.Checked = enableNormal;
        }

        private void EnableCtrl(Panel panel, bool showPanel, bool enableCtrl = true)
        {
            panel.Visible = showPanel;
            foreach (Control c in panel.Controls)
            {
                c.Enabled = enableCtrl;
                if (c is CheckBox)
                {
                    c.Enabled = false;
                }
            }
        }

        private void EnableDistribution(bool showPanel, bool enableCtrl = true)
        {
            EnableCtrl(pnlDistribution, showPanel, enableCtrl);
        }

        private void EnableBranchWarehouse(bool showPanel, bool enableCtrl = true, bool showAllWH = true)
        {
            EnableCtrl(pnlBranchWH, showPanel, enableCtrl);

            _RptStock = showAllWH ? "ALL" : "";
        }

        private void EnableProductSubGroup(bool showPanel, bool enableCtrl = true)
        {
            EnableCtrl(pnlProSubGroup, showPanel, enableCtrl);
        }

        private void EnableProductID(bool showPanel, bool enableCtrl = true)
        {
            EnableCtrl(pnlProID, showPanel, enableCtrl);
        }

        private void EnableProductType(bool showPanel, bool enableCtrl = true)
        {
            EnableCtrl(pnlProType, showPanel, enableCtrl);
        }

        private void EnableFromWarehouse(bool showPanel, bool enableCtrl = true)
        {
            EnableCtrl(pnlFromWHID, showPanel, enableCtrl);
        }

        private void EnableToWarehouse(bool showPanel, bool enableCtrl = true)
        {
            EnableCtrl(pnlToWHID, showPanel, enableCtrl);
        }

        private void EnableSaleArea(bool showPanel, bool enableCtrl = true)
        {
            EnableCtrl(pnlSalArea, showPanel, enableCtrl);
        }

        private void EnableShopType(bool showPanel, bool enableCtrl = true)
        {
            EnableCtrl(pnlShopType, showPanel, enableCtrl);
        }

        private void ManageControl(TreeViewEventArgs e)
        {
            try
            {
                ClearCriteria();

                pnlMainPage.Enabled = true;

                lblReportHeader.Text = e.Node.Text;

                List<string> set1 = new List<string>() { "Node2", "Node3", "Node20", "Node34", "Node50_2" };
                List<string> set2 = new List<string>() { "Node56", "Node12", "Node43", "Node44", "Node55", "Node14-1", "Node14-2", "count_cust_c", "count_cust_dt", "Rep_Customer_Sales_Yearly" };
                List<string> set3 = new List<string>() { "Node45", "Node51", "Node53", "Node11", "Node29", "Rep_PreOrder_POStatus_XSLT" };
                List<string> set4 = new List<string>() { "Node4", "Node5", "Node8", "Node9", "proc_StockMovement_ByWH", "NodeRLSumm", "Rep_BillDuplicate", "proc_StockMovement_ByWH_RefCode" };
                List<string> set5 = new List<string>() { "Node34_Bath", "Node34_Carton", "Node34_Unit", "Node48", "Node40", "NodeBill", "NodeBaht", "NodeBahtExcVat", "NodeBrick", "NodeCarton", };
                List<string> set6 = new List<string>() { "Node50", "Node52" };
                List<string> set7 = new List<string>() { "Rpt_ActualSalesByBill", "Node7", "Node21", "Node22", "Rpt_ActualSalesByVan", "Node24" };
                List<string> set8 = new List<string>() { "Node41", "Node42-1", "Node42-2", "Node42-3" };
                List<string> set9 = new List<string>() { "Node31" };
                List<string> set10 = new List<string>() { "Node28" };
                List<string> set11 = new List<string>() { "Rep_Sales_By_VanType" };

                //Node1 = Main menu การขาย report
                //Node2 = อย่างย่อ
                //Node3 = เต็มรูป
                //grbSingleCycle.Visible = true; //แสดง รายวัน รายรอบ

                if (set1.Contains(e.Node.Name))//Set 1 รายวัน+Dis+S+R-------------
                {
                    EnableDailyCycleCtrl(true, true, false, true, true, false, false);
                    EnableDistribution(true, true);
                    if (e.Node.Name == "Node20") //Disable Rang
                    {
                        EnableDocType(true, true, true, false);
                    }
                }
                else if (set2.Contains(e.Node.Name))//Set 2 รายรอบ+Dis+S+R--------------
                {
                    EnableDailyCycleCtrl(true, false, true, false, false, true, true);
                    EnableDistribution(true, true);

                    //if (new List<string>() { "Node56", "Node12", "NodeBill", "NodeBaht", "NodeBahtExcVat", "NodeBrick", "NodeCarton" }.Contains(e.Node.Name)) //Disable Rang
                    if (new List<string>() { "Node56", "Node12" }.Contains(e.Node.Name)) //Disable Rang
                    {
                        EnableDailyCycleCtrl(true, false, true, false, false, true, false);
                    }

                    if (new List<string> { "Node14-1", "Node14-2", "count_cust_c", "count_cust_dt", "Rep_Customer_Sales_Yearly" }.Contains(e.Node.Name))
                    {
                        EnableBranchWarehouse(true, true, false);
                    }
                }
                else if (set3.Contains(e.Node.Name))//Set 3 รายวัน/รายรอบ+Dis+S+R---------------
                {
                    EnableDailyCycleCtrl(true, true, true, true, true, true, true);
                    EnableDistribution(true, true);

                    if (e.Node.Name == "Rep_PreOrder_POStatus_XSLT") //Disable Rang
                    {
                        EnableDailyCycleCtrl(true, true, true, true, true, true, false);
                    }
                }
                else if (set4.Contains(e.Node.Name))//Set 4 รายวัน+DocType+Dis+WHID+S+R-------------
                {
                    EnableDailyCycleCtrl(true, true, false, true, true, false, false);
                    EnableDocType(true, true, true, true);
                    EnableDistribution(true, true);
                    EnableBranchWarehouse(true, true, false);
                    if (e.Node.Name == "Node5" || e.Node.Name == "Node9" || e.Node.Name == "Node8") //Hide rdoAll
                    {
                        EnableDocType(true, true, true, false);
                    }
                    if (e.Node.Name == "proc_StockMovement_ByWH" || e.Node.Name == "Rep_BillDuplicate" || e.Node.Name == "proc_StockMovement_ByWH_RefCode") //Hide DocType
                    {
                        EnableDocType(false, false, false, false);
                    }
                    if (e.Node.Name == "proc_StockMovement_ByWH" || e.Node.Name == "NodeRLSumm" || e.Node.Name == "proc_StockMovement_ByWH_RefCode") //Hide DocType
                    {
                        EnableBranchWarehouse(true, true, true);
                    }
                }
                else if (set5.Contains(e.Node.Name))//Set 5 รายรอบ+Dis+PG+P+S----------
                {
                    EnableDailyCycleCtrl(true, false, true, false, false, true, false);
                    EnableDistribution(true, true);
                    EnableProductSubGroup(true, true);
                    EnableProductID(true, true);

                    if (new List<string>() { "NodeBill", "NodeBaht", "NodeBahtExcVat", "NodeBrick", "NodeCarton" }.Contains(e.Node.Name)) //Disable Rang
                    {
                        EnableDailyCycleCtrl(true, false, true, false, false, true, true);
                    }
                }
                else if (set6.Contains(e.Node.Name))//Set 6 รายวัน/รายรอบ+Dis+PG+P+S+R---------------
                {
                    EnableDailyCycleCtrl(true, true, true, true, true, true, true);
                    EnableDistribution(true, true);
                    EnableProductSubGroup(true, true);
                    EnableProductID(true, true);
                    if (e.Node.Name == "Node52")
                    {
                        EnableDailyCycleCtrl(true, true, false, true, true, false, false);
                    }
                }
                else if (set7.Contains(e.Node.Name))//Set 7 รายวัน+DocType+Dis+WHID+PG+P+S+R-------------
                {
                    EnableDailyCycleCtrl(true, true, false, true, true, false, false);
                    EnableDocType(true, true, true, true);
                    EnableDistribution(true, true);
                    EnableBranchWarehouse(true, true, false);
                    EnableProductSubGroup(true, true);
                    EnableProductID(true, true);
                    if (e.Node.Name == "Rpt_ActualSalesByVan" || e.Node.Name == "Node24") //Hide DocType
                    {
                        EnableProductID(false, false);
                        if (e.Node.Name == "Node24")
                        {
                            EnableDocType(false, false, false, false);
                            EnableBranchWarehouse(true, true, true);
                            btnMNSock.Visible = true;
                        }
                    }
                }
                else if (set8.Contains(e.Node.Name))//Set 8 รายรอบ+DocType+Dis+WHID+PG+P+S-------------
                {
                    EnableDailyCycleCtrl(true, false, true, false, false, true, false);
                    EnableDistribution(true, true);
                    EnableBranchWarehouse(true, true, true);
                    EnableProductSubGroup(true, true);
                    EnableProductID(true, true);
                }
                else if (set9.Contains(e.Node.Name))//Set 9 รายรอบ+Dis+WHID+SA+SHType+S+R-------------
                {
                    EnableDailyCycleCtrl(true, false, true, false, false, true, true);
                    EnableDistribution(true, true);
                    EnableBranchWarehouse(true, true, false);
                    EnableSaleArea(true, true);
                    EnableShopType(true, true);
                    
                }
                else if (set10.Contains(e.Node.Name))//Set10 รายวัน+Dis+FWH+TWH+S+R-------------
                {
                    EnableDailyCycleCtrl(true, true, true, true, true, true, false);
                    EnableDistribution(true, true);
                    EnableFromWarehouse(true, true);
                    EnableToWarehouse(true, true);
                }
                else if (set11.Contains(e.Node.Name))//Set11 รายวัน รายรอบ จุดกระจาย 
                {
                    EnableDailyCycleCtrl(true, true, true, true, true, true, true);
                    EnableDistribution(true, true);
                    EnableBranchWarehouse(true, true, false);
                    EnableProductSubGroup(true, true);
                    EnableProductID(true, true);
                    chkProSubGroup.Enabled = true;
                    chkProID.Enabled = true;
                }
                else
                {
                    ClearCriteria();
                }
            }
            catch (Exception ex)
            {

                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void Print(frmWait wait)
        {
            try
            {
                string reportNameTxt = lblReportHeader.Text;
                //if (reportNameTxt == "รายงานบิลซ้ำ")
                //{
                //    if (txtWHCode.TextLength == 0 || txtWHCode.TextLength > 6)
                //    {
                //        txtWHCode.Clear();
                //        string msg = "สามารถเลือกได้เพียงหนึ่ง พนักงานขาย !!";
                //        msg.ShowWarningMessage();
                //        return;
                //    }
                //}


                if (reportNameTxt == "รายงาน Product Hero แยกตามวัน")
                {
                    if (string.IsNullOrEmpty(txtProSubGroup.Text))
                    {
                        string msg = "กรุณาเลือกหมวดสินค้า !!";
                        msg.ShowWarningMessage();
                        return;
                    }
                }
                else if (reportNameTxt == "รายงานยอดขาย DSR (SKU)")
                {
                    if (string.IsNullOrEmpty(txtProSubGroup.Text))
                    {
                        string msg = "ไม่พบรายงาน !!";
                        msg.ShowWarningMessage();
                        return;
                    }
                }
                else if (reportNameTxt == "รายงานบิลซ้ำ" ||
                    reportNameTxt == "รายงาน Distribution" ||
                    reportNameTxt == "รายงานสรุป RL รายแวน" ||
                    reportNameTxt == "รายงานรายละเอียดขายสินค้า (แยกตามลูกค้า)" ||
                    reportNameTxt == "รายงานการขายประจำวัน" 
                   // || reportNameTxt == "สรุปยอดเคลื่อนไหวสินค้า เรียงตามรหัสสินค้า"
                    )
                {
                    string msg = "";
                    if (string.IsNullOrEmpty(txtWHCode.Text))
                    {
                        msg = "กรุณาเลือก พนักงานขาย !!\n";
                    }
                    if (txtWHCode.TextLength > 6)
                    {
                        msg = "สามารถเลือกได้เพียง 1 พนักงานขาย !!\n";
                    }

                    if (!string.IsNullOrEmpty(msg))
                    {
                        msg.ShowWarningMessage();
                        return;
                    }
                }


                Dictionary<string, object> _params = new Dictionary<string, object>();
                DateTime cDate = DateTime.Now;

                int YearFr = !string.IsNullOrEmpty(ddlFromToYear.Text) ? Convert.ToInt32(ddlFromToYear.Text) - 543 : -1;
                int YearTo = !string.IsNullOrEmpty(ddlToYear.Text) ? Convert.ToInt32(ddlToYear.Text) - 543 : -1;

                DateTime df = cDate;
                DateTime dt = cDate;
                int mf = -1;
                int mt = -1;

                if (rdoCycle.Checked) //Cycle From-To--------------------------------------
                {
                    df = cDate;
                    dt = cDate;

                    if (rdoSingleC.Checked)
                    {
                        mf = Convert.ToInt32(ddlAroundFromYear.Text);
                        YearTo = YearFr;
                        mt = Convert.ToInt32(ddlAroundFromYear.Text);
                    }
                    else if (rdoRangeC.Checked)
                    {
                        mf = Convert.ToInt32(ddlAroundFromYear.Text);
                        mt = Convert.ToInt32(ddlAroundToYear.Text);
                    }
                }
                else if (rdoDaily.Checked) //Daily //Date From-To--------------------------------------
                {
                    df = dtpFromToD.Value;
                    dt = dtpToD.Enabled ? dtpToD.Value : dtpFromToD.Value;
                }

                _params.Add("@DateFr", df);
                _params.Add("@DateTo", dt);
                _params.Add("@YearFr", YearFr);
                _params.Add("@MonthFr", mf);
                _params.Add("@YearTo", YearTo);
                _params.Add("@MonthTo", mt);

                //Doc Status--------------------------------------
                _params.Add("@DocStatus", GetDocStatus());

                //Branch--------------------------------------
                if (!string.IsNullOrEmpty(txtBranchCode.Text))
                    _params.Add("@BranchID", txtBranchCode.Text);
                else
                    _params.Add("@BranchID", bu.GetBranch()[0].BranchID);

                //Branch--------------------------------------

                //WHID--------------------------------------
                string whid = "";
                if (string.IsNullOrEmpty(txtWHCode.Text))
                {
                    List<string> WHID = new List<string>();
                    var whList = new List<tbl_BranchWarehouse>();
                    if (_RptStock == "ALL")
                    {
                        whList = bu.GetAllBranchWarehouse();
                    }
                    else
                    {
                        whList = bu.GetAllBranchWarehouse(x => x.WHType != 0); // == 1); // edit by sailom .k 03/03/2022 for support pre-order
                    }

                    if (whList.Count > 0)
                    {
                        foreach (var wh in whList)
                        {
                            WHID.Add(wh.WHID);
                        }

                        var joinStr = string.Join(",", WHID);
                        whid = joinStr;
                    }
                }
                else
                {
                    whid = txtWHCode.Text;
                }

                _params.Add("@WHID", whid);
                //WHID--------------------------------------

                //ProductSubGroupID--------------------------------------
                _params.Add("@ProductSubGroupID", !string.IsNullOrEmpty(txtProSubGroup.Text) ? txtProSubGroup.Text : "");
                //ProductSubGroupID--------------------------------------

                //ProductID--------------------------------------
                _params.Add("@ProductID", !string.IsNullOrEmpty(txtProID.Text) ? txtProID.Text : "");
                //ProductID--------------------------------------

                //FromWH And ToWH--------------------------------------
                string fromWh = "";
                string toWh = "";
                if (!string.IsNullOrEmpty(txtBranchCode.Text) && !string.IsNullOrEmpty(txtWHCode_FromWH.Text))
                    fromWh = txtWHCode_FromWH.Text.Contains("V") ? txtWHCode_FromWH.Text : txtBranchCode.Text + txtWHCode_FromWH.Text;
                if (!string.IsNullOrEmpty(txtBranchCode.Text) && !string.IsNullOrEmpty(txtWHCode_ToWH.Text))
                    toWh = txtWHCode_ToWH.Text.Contains("V") ? txtWHCode_ToWH.Text : txtBranchCode.Text + txtWHCode_ToWH.Text;

                _params.Add("@FromWH", fromWh);
                _params.Add("@ToWH", toWh);
                //FromWH And ToWH--------------------------------------

                //SalAreaID--------------------------------------
                _params.Add("@SalAreaID", !string.IsNullOrEmpty(txtSalAreaID.Text) ? txtSalAreaID.Text : "");
                //SalAreaID--------------------------------------

                //ShopTypeID--------------------------------------
                _params.Add("@ShopTypeID", 0);
                if (!string.IsNullOrEmpty(txtShopType.Text))
                    _params.Add("@ShopTypeID", Convert.ToInt32(txtShopType.Text));
                //ShopTypeID--------------------------------------

                //if (reportNameTxt == "รายงานสรุป RL รายแวน")
                //{
                //    _params = new Dictionary<string, object>();
                //    _params.Add("@DocDate", dtpFromToD.Value);
                //    _params.Add("@WHID", txtWHCode.Text);
                //    _params.Add("@BranchID", txtBranchCode.Text);
                //}
                //else 
                if (reportNameTxt == "รายงานสัดส่วน(SKU)")
                {
                    _params = new Dictionary<string, object>();

                    _params.Add("@FDate", dtpFromToD.Value);
                    if (dtpToD.Enabled)
                        _params.Add("@TDate", dtpToD.Value);
                    else
                        _params.Add("@TDate", dtpFromToD.Value);
                }


                switch (reportNameTxt)
                {
                    case "รายงานสรุปยอดขาย แยกตามเอกสาร": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Sales_By_Doc.XSLT", "Rep_Sales_By_Doc_XSLT", _params, true); } break;
                    case "รายงานรายละเอียดรับสินค้า (แยกตามเอกสาร)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Rec_By_Doc.XSLT", "Rep_Rec_By_Doc_XSLT", _params, true); } break;
                    case "รายงานรายละเอียดรับสินค้า (แยกตามเจ้าหนี้)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Rec_By_Subb.XSLT", "Rep_Rec_By_Subb_XSLT", _params, true); } break;
                    case "รายงานสรุป RL รายแวน": { this.OpenCrystalReportsPopup(reportNameTxt, "Form_RL_Summary.rpt", "Form_RL_Summary", _params, true); } break;
                    case "รายงานภาษีขายอย่างย่อ": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_IV_Sales2.XSLT", "Rep_IV_Sales_XSLT", _params, true); } break;
                    case "รายงานภาษีขายเต็มรูป": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_V_Sales.XSLT", "Rep_V_Sales_XSLT", _params, true); } break;
                    case "รายงานบิลขาย(นับทุกบิล)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_BillByDate_AllBill.XSLT", "Rep_BillByDate_AllBill_XSLT", _params, true); } break;
                    case "รายงานยอดขาย(บาท)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_BillByDate_TotalDue.XSLT", "Rep_BillByDate_TotalDue_XSLT", _params, true); } break;
                    case "รายงานยอดขายไม่รวมVat(บาท)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_BillByDate_ExcVat.XSLT", "Rep_BillByDate_ExcVat_XSLT", _params, true); } break;
                    case "รายงานยอดขาย(หน่วยเล็ก)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_BillByDate_Brick.XSLT", "Rep_BillByDate_Brick_XSLT", _params, true); } break;
                    case "รายงานยอดขาย(หน่วยใหญ่)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_BillByDate_Carton.XSLT", "Rep_BillByDate_Carton_XSLT", _params, true); } break;
                    case "รายงานยอดขาย แยกตามแวนและวันที่": { this.OpenExcelReportsPopup(reportNameTxt, "ActualSales_By_Day.XSLT", "proc_Actualsales_by_day_XSLT", _params, true); } break;
                    case "รายงานรายละเอียดขายสินค้า (แยกตามลูกค้า)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Detail_Sales_By_Cust.XSLT", "Rep_Detail_Sales_By_Cust_XSLT", _params, true); } break;
                    case "รายงานการขายประจำวัน": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Daily_Sales.xslt", "Rep_Daily_Sales_XSLT", _params, true); } break;
                    case "รายงานสินค้าคงเหลือ แยกตามคลัง": { this.OpenExcelReportsPopup(reportNameTxt, "proc_RPTStock.XSLT", "proc_RPTStock_XSLT", _params, true); } break;
                    case "รายงานรายละเอียดโอนสินค้า": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Product_Transfer.xslt", "Rep_Product_Transfer_XSLT", _params, true); } break;
                    case "รายงานสรุป Shelf สินค้า": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Sum_Shelf.XSLT", "Rep_Sum_Shelf_XSLT", _params, true); } break;
                    case "รายงานสรุป Shelf สินค้า (รายวัน)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Sum_Shelf_Daily.XSLT", "Rep_Sum_Shelf_Daily_XSLT", _params, true); } break;
                    case "รายงานสัดส่วน": 
                        {
                            if (Connection.ConnectionString.Contains("DB_SDSS_UNI_CENTER"))
                                this.OpenManualExcelCenterReportsPopup(reportNameTxt, "RPT-DSR2.rpt", "proc_Rpt_DSR_Summary_XSLT", _params, true);
                            else
                                this.OpenManualExcelReportsPopup(reportNameTxt, "RPT-DSR2.rpt", "proc_Rpt_DSR_XSLT", _params, true);
                        } break; //DSR
                    case "รายงานสัดส่วน(KPI)": 
                        {
                            if (Connection.ConnectionString.Contains("DB_SDSS_UNI_CENTER"))
                                this.OpenManualExcelCenterReportsPopup(reportNameTxt, "RPT-DSR-KPI.rpt", "proc_Rpt_DSR_Summary_KPI_XSLT", _params, true);
                            else
                                this.OpenManualExcelReportsPopup(reportNameTxt, "RPT-DSR-KPI.rpt", "proc_Rpt_DSR_KPI_XSLT", _params, true);
                        } break; //DSR KPI
                    case "รายงาน Eff.Call (KPI)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_DSR_EffectiveCall_KPI.xslt", "Rep_DSR_EffectiveCall_KPI_XSLT", _params, true); } break;//wait for edit----------------------}break;
                    case "รายงานจำนวนร้านค้า ตามรอบการขาย": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Customer_By_Cycle.xslt", "Rep_Customer_By_Cycle_XSLT", _params, true); } break;
                    case "รายงานร้านเยี่ยมเฉลี่ย/วัน": { this.OpenExcelReportsPopup("รายงานร้านเยี่ยมเฉลี่ยต่อวัน", "Rep_Visit_Per_Day.xslt", "Rep_Visit_Per_Day_XSLT", _params, true); } break;
                    case "รายงานรายละเอียด Shelf": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Check_Shelf.XSLT", "Rep_Check_Shelf_XSLT", _params, true); } break;
                    case "รายงานสรุปยอดขาย": { this.OpenExcelReportsPopup(reportNameTxt, "proc_DSR_Sales_Summary.xslt", "proc_DSR_Sales_Summary_XSLT", _params, true); } break;
                    case "รายงาน Product Hero แยกตามวัน": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_ProductHero_ByDate_XSLT.xslt", "Rep_ProductHero_ByDate", _params, true); } break;
                    case "รายงาน Distribution": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Distribution.xslt", "Rep_Distribution", _params, true); } break;
                    case "รายงานสัดส่วน(SKU)": { this.OpenExcelReportsPopup(reportNameTxt, "proc_DSR_Sales_Summary_By_Sku.xslt", "proc_DSR_Sales_Summary_By_Sku_XSLT", _params, true); } break;
                    case "รายงานยอดขาย แยกตามลูกค้า": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Sales_By_Customer.xslt", "Rep_Sales_By_Customer_XSLT", _params, true); } break;
                    case "รายงานรับสินค้า": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_ReceiveStock.xslt", "Rep_ReceiveStock_XSLT", _params, true); } break;
                    case "รายงานสรุปยอดขายแยกตามพนักงาน (รายวัน/รายเดือน)": { this.OpenExcelReportsPopup("รายงานสรุปยอดขายแยกตามพนักงาน(รายวัน รายเดือน", "Rep_Sales_Per_Emp.xslt", "Rep_Sales_Per_Emp_XSLT", _params, true); } break;
                    case "รายงานรายละเอียดขายสินค้า (แยกตามเอกสาร)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Sales_By_Doc_Detail.xslt", "Rep_Sales_By_Doc_Detail_XSLT", _params, true); } break;
                    case "รายงานรายละเอียดขายสินค้า (แยกตามแวน)": { this.OpenExcelReportsPopup(reportNameTxt, "proc_ActualSalesByBill.xslt", "proc_ActualSalesByBill_XSLT", _params, true); } break;
                    case "รายงานสรุปจำนวนร้านค้าทั้งหมด (ตามคลังรถ)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Customer_List_By_WH.xslt", "Rep_Customer_List_By_WH_XSLT", _params, true); } break;
                    case "รายงานร้านค้าตาม Customer List": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Customer_By_Sequence.xslt", "Rep_Customer_By_Sequence_XSLT", _params, true); } break;
                    case "สรุปยอดเคลื่อนไหวสินค้า เรียงตามรหัสสินค้า": { this.OpenExcelReportsPopup(reportNameTxt, "proc_StockMovement_ByWH.xslt", "proc_StockMovement_ByWH_XSLT", _params, true); } break;
                    case "สรุปยอดเคลื่อนไหวสินค้า เรียงตามรหัสบัญชีหน่วยเล็ก": { this.OpenExcelReportsPopup(reportNameTxt, "proc_StockMovement_ByWH_RefCode.xslt", "proc_StockMovement_ByWH_RefCode_XSLT", _params, true); } break;
                    case "รายงานยอดขาย DSR (SKU)": { this.OpenExcelReportsPopup(reportNameTxt, "", "", _params, true); } break;
                    case "รายงานยอดขาย DSR (Bath)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_DSR_Sales_By_Sku_Bath.xslt", "Rep_DSR_Sales_By_Sku_Bath_XSLT", _params, true); } break;
                    case "รายงานยอดขาย DSR (Carton)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_DSR_Sales_By_Sku_Carton.xslt", "Rep_DSR_Sales_By_Sku_Carton_XSLT", _params, true); } break;
                    case "รายงานยอดขาย DSR (Unit)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_DSR_Sales_By_Sku_Unit.xslt", "Rep_DSR_Sales_By_Sku_Unit_XSLT", _params, true); } break;
                    case "รายงานบิลซ้ำ": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_BillDuplicate.xslt", "Rep_BillDuplicate", _params, true); } break;
                    case "รายงานร้านซื้อแยกตามตลาด": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Cust_Sale_By_Root.xslt", "Rep_Cust_Sale_By_Root_XSLT", _params, true); } break;
                    case "รายงานรายละเอียดทำลายสินค้า": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_RJ_By_Doc_Detail.xslt", "Rep_RJ_By_Doc_Detail_XSLT", _params, true); } break;
                    case "รายงานยอดขายตามจังหวัด (บาท)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_ActualSale_By_Province.xslt", "Rep_ActualSale_By_Province_XSLT", _params, true); } break;
                    
                    case "รายงานยอดขายแยกตามลูกค้า(Brick)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_ActualSale_By_Customer_Brick.xslt", "Rep_ActualSale_By_Customer_Brick_XSLT", _params, true); } break;
                    case "รายงานยอดขายแยกตามลูกค้า(Carton)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_ActualSale_By_Customer_Carton.xslt", "Rep_ActualSale_By_Customer_Carton_XSLT", _params, true); } break;
                    case "รายงานยอดขายแยกตามลูกค้า(Bath)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_ActualSale_By_Customer_Bath.xslt", "Rep_ActualSale_By_Customer_Bath_XSLT", _params, true); } break;
                    case "รายงานสถานะ (Pre-Order)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_PreOrder_POStatus.xslt", "Rep_PreOrder_POStatus_XSLT", _params, true); } break;
                    case "รายงานยอดขายแยกตามประเภทแวน": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Sales_By_VanType.xslt", "Rep_SaleTarget_XSLT", _params, true); } break;
                    case "รายงานลูกค้าใหม่(จำนวน)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_New_Customer.xslt", "Rep_New_Customer_XSLT", _params, true); } break;
                    case "รายงานลูกค้าใหม่(รายการ)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_New_Customer_Detail.xslt", "Rep_New_Customer_Detail_XSLT", _params, true); } break;
                    case "รายงานร้านค้าจัดกลุ่มตามตำบล(จำนวน)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Count_Cust_By_Branch.xslt", "Rep_Count_Cust_By_Branch_XSLT", _params, true); } break;
                    case "รายงานร้านค้าจัดกลุ่มตามตำบล(รายละเอียด)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Customer_Detail_By_Branch.xslt", "Rep_Customer_Detail_By_Branch_XSLT", _params, true); } break;
                    case "รายงานยอดขายแยกตามร้านค้า(รายปี)": { this.OpenExcelReportsPopup(reportNameTxt, "Rep_Customer_Sales_Yearly.xslt", "Rep_Customer_Sales_Yearly", _params, true); } break;

                    default:
                        break;
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

            //edit by sailom 11-05-2021-------------------
            string reportName = lblReportHeader.Text;
            if (reportName == "รายงานสัดส่วน(KPI)" || reportName == "รายงานสัดส่วน" || reportName == "รายงานร้านเยี่ยมเฉลี่ย/วัน" || reportName == "รายงานรายละเอียด Shelf")
            {
                if (rdoCycle.Checked)
                    rdoRangeC.Enabled = false;
                else
                    rdoRangeC.Enabled = true;
            }
            //edit by sailom 11-05-2021-------------------
        }

        private void btnDistribution_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกเดโป้/สาขา");
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

        private void btnFromWHID_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchFromWHControls, "เลือกคลังสินค้า");
        }

        private void btnToWHID_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchToWHControls, "เลือกคลังสินค้า");
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

        private void btnMNSock_Click(object sender, EventArgs e)
        {
            //Dictionary<string, object> _params = new Dictionary<string, object>();
            //_params.Add("@WHID", txtWHCode.Text);
            //_params.Add("@DocDate", dtpFromToD.Value);

            Dictionary<string, object> _params = new Dictionary<string, object>();
            DateTime cDate = DateTime.Now;

            int YearFr = !string.IsNullOrEmpty(ddlFromToYear.Text) ? Convert.ToInt32(ddlFromToYear.Text) - 543 : -1;
            int YearTo = !string.IsNullOrEmpty(ddlToYear.Text) ? Convert.ToInt32(ddlToYear.Text) - 543 : -1;

            DateTime df = cDate;
            DateTime dt = cDate;
            int mf = -1;
            int mt = -1;

            if (rdoCycle.Checked) //Cycle From-To--------------------------------------
            {
                df = cDate;
                dt = cDate;

                if (rdoSingleC.Checked)
                {
                    mf = Convert.ToInt32(ddlAroundFromYear.Text);
                    YearTo = YearFr;
                    mt = Convert.ToInt32(ddlAroundFromYear.Text);
                }
                else if (rdoRangeC.Checked)
                {
                    mf = Convert.ToInt32(ddlAroundFromYear.Text);
                    mt = Convert.ToInt32(ddlAroundToYear.Text);
                }
            }
            else if (rdoDaily.Checked) //Daily //Date From-To--------------------------------------
            {
                df = dtpFromToD.Value;
                dt = dtpToD.Enabled ? dtpToD.Value : dtpFromToD.Value;
            }

            _params.Add("@DateFr", df);
            _params.Add("@DateTo", dt);
            _params.Add("@YearFr", YearFr);
            _params.Add("@MonthFr", mf);
            _params.Add("@YearTo", YearTo);
            _params.Add("@MonthTo", mt);

            //Doc Status--------------------------------------
            _params.Add("@DocStatus", GetDocStatus());

            //Branch--------------------------------------
            if (!string.IsNullOrEmpty(txtBranchCode.Text))
                _params.Add("@BranchID", txtBranchCode.Text);
            else
                _params.Add("@BranchID", bu.GetBranch()[0].BranchID);

            //Branch--------------------------------------

            //WHID--------------------------------------
            string whid = "";
            if (string.IsNullOrEmpty(txtWHCode.Text))
            {
                List<string> WHID = new List<string>();
                var whList = new List<tbl_BranchWarehouse>();
                if (_RptStock == "ALL")
                {
                    whList = bu.GetAllBranchWarehouse();
                }
                else
                {
                    whList = bu.GetAllBranchWarehouse(x => x.WHType != 0); // edit by sailom .k 03/03/2022 for support pre-order // == 1);
                }

                if (whList.Count > 0)
                {
                    foreach (var wh in whList)
                    {
                        WHID.Add(wh.WHID);
                    }

                    var joinStr = string.Join(",", WHID);
                    whid = joinStr;
                }
            }
            else
            {
                whid = txtWHCode.Text;
            }

            _params.Add("@WHID", whid);
            //WHID--------------------------------------

            //ProductSubGroupID--------------------------------------
            _params.Add("@ProductSubGroupID", !string.IsNullOrEmpty(txtProSubGroup.Text) ? txtProSubGroup.Text : "");
            //ProductSubGroupID--------------------------------------

            //ProductID--------------------------------------
            _params.Add("@ProductID", !string.IsNullOrEmpty(txtProID.Text) ? txtProID.Text : "");
            //ProductID--------------------------------------

            //FromWH And ToWH--------------------------------------
            string fromWh = "";
            string toWh = "";
            if (!string.IsNullOrEmpty(txtBranchCode.Text) && !string.IsNullOrEmpty(txtWHCode_FromWH.Text))
                fromWh = txtWHCode_FromWH.Text.Contains("V") ? txtWHCode_FromWH.Text : txtBranchCode.Text + txtWHCode_FromWH.Text;
            if (!string.IsNullOrEmpty(txtBranchCode.Text) && !string.IsNullOrEmpty(txtWHCode_ToWH.Text))
                toWh = txtWHCode_ToWH.Text.Contains("V") ? txtWHCode_ToWH.Text : txtBranchCode.Text + txtWHCode_ToWH.Text;

            _params.Add("@FromWH", fromWh);
            _params.Add("@ToWH", toWh);
            //FromWH And ToWH--------------------------------------

            //SalAreaID--------------------------------------
            _params.Add("@SalAreaID", !string.IsNullOrEmpty(txtSalAreaID.Text) ? txtSalAreaID.Text : "");
            //SalAreaID--------------------------------------

            //ShopTypeID--------------------------------------
            _params.Add("@ShopTypeID", 0);
            if (!string.IsNullOrEmpty(txtShopType.Text))
                _params.Add("@ShopTypeID", Convert.ToInt32(txtShopType.Text));
            //ShopTypeID--------------------------------------

            string popupName = string.Join(" ", "รายงานสต็อกคงเหลือ", txtWHCode.Text, dtpFromToD.Value.ToDateTimeFormat());
            //this.OpenCrystalReportsPopup("รายงานสต็อกคงเหลือ(เช้า)", "RptStock_MorningStock.rpt", "proc_RPTStock_MorningStock", _params, true);
            this.OpenExcelReportsPopup("รายงานสต็อกคงเหลือ(เช้า)", "proc_RPTStock_Morning.XSLT", "proc_RPTStock_MorningStock_XSLT", _params, true);
        }

        /// <summary>
        /// Change Tree Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ManageControl(e);
        }

        /// <summary>
        /// Print Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //for support excel report
            //frmWait wait = new frmWait();
            //wait.Show();

            //Cursor.Current = Cursors.WaitCursor;
            FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

            Print(wait);

            //Cursor.Current = Cursors.Default;

            //wait.Hide();
            //wait.Dispose();
            //wait.Close();
        }

        private void btnALLSalArea_Click(object sender, EventArgs e)
        {
            frmSearchSalArea frm = new frmSearchSalArea();
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(_SalArea))
            {
                txtSalAreaID.Text = _SalArea;
            }
        }

        private void chkBoxALLSalArea_CheckedChanged(object sender, EventArgs e)
        {
            Control_V_btn(chkBoxALLSalArea, btnALLSalArea, txtSalAreaID);
        }

        private void btnShopType_Click(object sender, EventArgs e)
        {
            frmALLShopType frm = new frmALLShopType();
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(_ShopType))
            {
                txtShopType.Text = _ShopType;
            }
        }

        private void chkAllShopType_CheckedChanged(object sender, EventArgs e)
        {
            Control_V_btn(chkAllShopType, btnShopType, txtShopType);
        }

        #endregion
    }
}
