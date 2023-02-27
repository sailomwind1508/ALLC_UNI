using AllCashUFormsApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Model;
using System.Data.OleDb;
using AllCashUFormsApp.View.UControl;
using System.Globalization;
using System.IO;
namespace AllCashUFormsApp.View.Page
{
    public partial class frmPromotionSetting : Form
    {
        MasterDataControl bu = new MasterDataControl();
        MenuBU menuBU = new MenuBU();
        Dictionary<Control, Label> Validate_HQ_PromotionMaster = new Dictionary<Control, Label>();
        Dictionary<Control, Label> Validate_HQ_Reward = new Dictionary<Control, Label>();
        Dictionary<Control, Label> Validate_HQ_SKUGroup = new Dictionary<Control, Label>();
        Dictionary<Control, Label> Validate_HQ_SKUGroup_Exc = new Dictionary<Control, Label>();
        Dictionary<Control, Label> Validate_HQ_Promotion = new Dictionary<Control, Label>();
        List<string> PanelEdit_HQPromotion_Controls = new List<string>();
        static DataTable dtHQPromotion;
        int EditFlag = 0;
        public static string skuID;
        CultureInfo cultures = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
        public frmPromotionSetting()
        {
            InitializeComponent();

            PanelEdit_HQPromotion_Controls = new string[] { txtPromotionID_HQPro.Name }.ToList();

            Validate_HQ_PromotionMaster.Add(mtbPromotionID_Master, lblPromotionID);
            Validate_HQ_PromotionMaster.Add(txtPromotionName, lbl_PromotionName);

            Validate_HQ_Reward.Add(mtb_RewardID, lblRewardID);
            Validate_HQ_Reward.Add(txtRewardName, lblRewardName);

            Validate_HQ_SKUGroup.Add(mtbHQ_SKUGroupID, lblSKUGroupID);
            Validate_HQ_SKUGroup.Add(lsbSKU_ID, lblSKU_ID);

            Validate_HQ_SKUGroup_Exc.Add(mtbSKU_ID_Exc, lblSKU_ID_Exc);
        }

        private void ValidateHQ_PromotionList()
        {
            if (Validate_HQ_Promotion.Count > 0)
            {
                Validate_HQ_Promotion.Clear();
            }

            Validate_HQ_Promotion.Add(txtPromotionID_HQPro, lblPromotionID);

            if (cbbPromotionPattern.SelectedIndex == 0)//prd
            {
                Validate_HQ_Promotion.Add(mtbSKUGroupID1, lbl_SKUGroupID1);
            }

            Validate_HQ_Promotion.Add(txtConditionStart, lblConditionStart);

            if (cbbDisCountPattern.SelectedIndex == 0) //free=free
            {
                Validate_HQ_Promotion.Add(mtbPruductGroupRewardID, lbl_PruductGroupRewardID);
            }

            if (cbbDisCountPattern.SelectedIndex != 0)
            {
                Validate_HQ_Promotion.Add(txtDisCountAmt, lbl_DisCountAmt);
            }

            Validate_HQ_Promotion.Add(mtbRewardID, lbl_RewardID_HQPro);
            Validate_HQ_Promotion.Add(txtPromotionPriority, lbl_PromotionPriority);
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
        }

        private void InitialData()
        {
            grdBranch.AutoGenerateColumns = false;
            grdHQPromotionMaster.AutoGenerateColumns = false;
            grdHQ_Reward.AutoGenerateColumns = false;
            grdSkuGroup.AutoGenerateColumns = false;
            grdSKUGroup_EXC.AutoGenerateColumns = false;

            grdBranch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdHQPromotionMaster.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnExportExcel.Enabled = false;

            BindBranchData();

            // Bind Data to Combobox HQ_Promotion
            BindPromotionType();
            BindPromotionPattern();
            BindStepCondition1();
            BindDisCountPattern();
            BindStepCondition2();
            // 

            if (grdBranch.Rows.Count > 0)
            {
                BindHQ_Promotion();
                BindHQ_Promotion_Master();
                BindHQ_Reward();
                BindHQ_SKUGroup();
                BindHQ_SKUGroup_Exc();
                BindSaleType();

                int colNo = grdHQ_Promotion.Rows.Count;
                SetButtonAfterBindGrid(colNo);

                dtpEffectiveDate.SetDateTimePickerFormat();
                dtpExpireDate.SetDateTimePickerFormat();

                grdHQ_Promotion.Columns["colEffectiveDate"].DefaultCellStyle.Format = "dd/MM/yyyy h:mm:ss.fff";
                grdHQ_Promotion.Columns["colExpireDate"].DefaultCellStyle.Format = "dd/MM/yyyy h:mm:ss.fff";
            }

            else
            {
                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnExcel.Enabled = false;
                btnExportExcel.Enabled = false;

                string msg = "สามารถใช้ได้เมื่อต่อ CENTER DB เท่านั้น !!";
                msg.ShowWarningMessage();
            }
        }

        private void BindBranchData()
        {
            DataTable dtBranch = new DataTable();

            bu.GetSendProductInfoPrepareData();
            dtBranch = bu.Get_proc_SendProductInfo_GetDataTable();

            DataTable newTable = new DataTable();

            newTable.Columns.Add("ChkBranch", typeof(bool));
            newTable.Columns.Add("BranchID", typeof(string));
            newTable.Columns.Add("BranchRefCode", typeof(string));
            newTable.Columns.Add("BranchName", typeof(string));
            newTable.Columns.Add("Pic", typeof(Bitmap));
            newTable.Columns.Add("OnlineStatus", typeof(bool));

            Bitmap wifi_Img = new Bitmap(Properties.Resources.addBtn); // new Resource Image
            Bitmap power_off_lmg = new Bitmap(Properties.Resources.closeBtn);

            foreach (DataRow r in dtBranch.Rows)
            {
                Bitmap colPic = Convert.ToBoolean(r["OnlineStatus"]) == true ? wifi_Img : power_off_lmg;
                newTable.Rows.Add(false
                    , r["BranchID"].ToString()
                    , r["BranchRefCode"].ToString()
                    , r["BranchName"].ToString()
                    , colPic
                    , r["OnlineStatus"]);
            }

            grdBranch.DataSource = newTable;

            for (int i = 0; i < grdBranch.Rows.Count; i++)
            {
                bool OnlineStatus = Convert.ToBoolean(grdBranch.Rows[i].Cells["colOnlineStatus"].Value);
                // OnlineStatus = true คือ แก้ไขได้
                if (OnlineStatus == false)
                {
                    grdBranch.Rows[i].Cells["colChkBranch"].ReadOnly = true;
                }
            }

            lblgrdQty.Text = newTable.Rows.Count.ToNumberFormat();
        }

        private void BindHQ_Promotion_Master()
        {
            mtbPromotionID_Master.Text = "";
            txtPromotionName.Text = "";

            if (grdBranch.Rows.Count > 0)
            {
                DataTable HQ_ProMaster = new DataTable();

                HQ_ProMaster = bu.GetHQ_Promotion_MasterData(txtSearch.Text); // new

                grdHQPromotionMaster.DataSource = HQ_ProMaster;
                lbl_qty_HQ_PromotionMaster.Text = HQ_ProMaster.Rows.Count.ToNumberFormat();

                int colNo = HQ_ProMaster.Rows.Count;
                SetButtonAfterBindGrid(colNo);

                Select_HQPromotion_Master(null);
            }
        }

        private void BindSaleType()
        {
            cbbSaleType.DataSource = null;
            var saleType = bu.GetSaleTypeData();
            cbbSaleType.BindDropdownList(saleType, "SaleTypeName", "SaleTypeID");
        }

        private void SetButtonAfterBindGrid(int colNo)
        {
            pnlEdit_HQPromotion.OpenControl(false, PanelEdit_HQPromotion_Controls.ToArray());

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            btnExcel.Enabled = true;

            if (colNo > 0)
            {
                btnEdit.Enabled = true;
                //btnRemove.Enabled = true;
            }

            if (grdHQ_Promotion.RowCount > 0
                    || grdHQPromotionMaster.RowCount > 0
                    || grdHQ_Reward.RowCount > 0
                    || grdSkuGroup.RowCount > 0
                    || grdSKUGroup_EXC.RowCount > 0)
            {
                btnExportExcel.Enabled = true;
            }
        }

        private void frmPromotionSetting_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
            InitialData();
        }

        private void BindPromotionType()
        {
            cbbPromotionType.Items.Add("mmc");
            cbbPromotionType.Items.Add("mmch");
            cbbPromotionType.SelectedIndex = 0;
        }

        private void BindPromotionPattern()
        {
            cbbPromotionPattern.Items.Add("prd");
            cbbPromotionPattern.Items.Add("txn");
            cbbPromotionPattern.SelectedIndex = 0;
        }

        private void BindStepCondition1()
        {
            cbbStepCondition1.Items.Add("qty");
            cbbStepCondition1.Items.Add("amt");
            cbbStepCondition1.SelectedIndex = 0;
        }

        private void BindDisCountPattern()
        {
            cbbDisCountPattern.Items.Add("free=free");
            cbbDisCountPattern.Items.Add("disc_perc_amt");
            cbbDisCountPattern.Items.Add("disc_baht_qty");
            cbbDisCountPattern.Items.Add("fix_baht_qty");
            cbbDisCountPattern.SelectedIndex = 0;
        }

        private void BindStepCondition2()
        {
            cbbStepCondition2.Items.Add("qty");
            cbbStepCondition2.Items.Add("amt");
            cbbStepCondition2.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            dtHQPromotion = new DataTable();
            this.Close();
        }

        private void grdHQPromotionMaster_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdHQPromotionMaster.SetRowPostPaint(sender, e, this.Font);
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            string tabName = tabControl1.SelectedTab.Text.ToString();

            if (tabName == "HQ_Promotion_Master")
            {
                BindHQ_Promotion_Master();
            }
            else if (tabName == "HQ-Reward")
            {
                BindHQ_Reward();
            }
            else if (tabName == "SKU-GROUP")
            {
                BindHQ_SKUGroup();
            }
            else if (tabName == "SKU-GROUP-EXC")
            {
                BindHQ_SKUGroup_Exc();
            }
            else if (tabName == "HQ_Promotion")
            {
                BindHQ_Promotion();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditFlag = 1;

            pnlEdit_HQPromotion.OpenControl(true, PanelEdit_HQPromotion_Controls.ToArray());

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            string tabName = tabControl1.SelectedTab.Text.ToString();

            string date = DateTime.Now.ToString("yyMMdd",cultures);

            if (tabName == "HQ_Promotion_Master")
            {
                pnlSearch_HQ_Promotion_Master.Enabled = false;
                grdHQPromotionMaster.Enabled = false;

                txtPromotionName.DisableTextBox(false);

                mtbPromotionID_Master.DisableTextBox(false);

                mtbPromotionID_Master.Text = date;
                mtbPromotionID_Master.Focus();

                txtPromotionName.Text = "";

            }
            else if (tabName == "HQ-Reward")
            {
                pnlSearch_HQ_Reward.Enabled = false;
                grdHQ_Reward.Enabled = false;

                mtb_RewardID.DisableTextBox(false);
                txtRewardName.DisableTextBox(false);

                mtb_RewardID.Text = date;
                txtRewardName.Text = "";

                mtb_RewardID.Focus();
            }
            else if (tabName == "SKU-GROUP")
            {
                pnlSearchHQ_SKUGroup.Enabled = false;
                grdSkuGroup.Enabled = false;

                btnAddSKU_ID.Enabled = true;
                btnRemoveSKU.Enabled = true;

                lsbSKU_ID.DisableTextBox(false);

                if (lsbSKU_ID.Items.Count > 0)
                {
                    lsbSKU_ID.Items.Clear();
                }

               
                mtbHQ_SKUGroupID.DisableTextBox(false);

                mtbHQ_SKUGroupID.Text = date;
                mtbHQ_SKUGroupID.Focus();
            }
            else if (tabName == "SKU-GROUP-EXC")
            {
                pnlSearchSKUGroupExc.Enabled = false;
                grdSKUGroup_EXC.Enabled = false;

                mtbSKU_ID_Exc.DisableTextBox(false);

                mtbSKU_ID_Exc.Text = "";

                mtbSKU_ID_Exc.Focus();
            }
            else if (tabName == "HQ_Promotion")
            {
                pnlSearch_HQPro.Enabled = false;
                grdHQ_Promotion.Enabled = false;

                pnlEdit_HQPromotion.OpenControl(true, PanelEdit_HQPromotion_Controls.ToArray());
                pnlEdit_HQPromotion.ClearControl();

                txtPromotionID_HQPro.DisableTextBox(true);
                txtPromotionID_HQPro.Focus();

                RunningPromotionID();

                SetDefaultPanelEdit_HQPromotion();
                SetOpenEditPromotionControl();

               
            }
        }

        private void SetDefaultPanelEdit_HQPromotion()
        {
            cbbPromotionType.SelectedIndex = 0;
            cbbPromotionPattern.SelectedIndex = 0;
            cbbStepCondition1.SelectedIndex = 0;
            cbbDisCountPattern.SelectedIndex = 0;
            dtpEffectiveDate.Value = DateTime.Now;
            dtpExpireDate.Value = DateTime.Now;

            chkboxsRecomputable.Checked = false;
            chkboxsIgnoreApplied.Checked = false;
            chkboxsIsUseCoupon.Checked = false;
        }

        private void RunningPromotionID()
        {
            string dateNow = DateTime.Now.ToString("yyMMdd",cultures);

            var Promotion = bu.GetSelectHQPromotion(dateNow);

            if (Promotion.Count > 0)
            {
                string PromotionIDmax = Promotion.Select(x => x.PromotionID).Max();
                string _PromotionIDmax = PromotionIDmax.Substring(6, 4);
                int MaxID = Convert.ToInt32(_PromotionIDmax) + 1;
                txtPromotionID_HQPro.Text = dateNow + MaxID.ToString("0000");
            }
            else
            {
                txtPromotionID_HQPro.Text = dateNow + "0001";
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditFlag = 2;

            pnlEdit_HQPromotion.OpenControl(true, PanelEdit_HQPromotion_Controls.ToArray());

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            string tabName = tabControl1.SelectedTab.Text.ToString();

            if (tabName == "HQ_Promotion_Master")
            {
                mtbPromotionID_Master.DisableTextBox(true);
                txtPromotionName.DisableTextBox(false);

                txtPromotionName.Focus();
            }
            else if (tabName == "HQ-Reward")
            {
                mtb_RewardID.DisableTextBox(true);
                txtRewardName.DisableTextBox(false);

                txtRewardName.Focus();
            }
            else if (tabName == "SKU-GROUP")
            {
                lsbSKU_ID.DisableTextBox(false);

                btnAddSKU_ID.Enabled = true;
                btnRemoveSKU.Enabled = true;

                if (lsbSKU_ID.Items.Count > 0)
                {
                    lsbSKU_ID.Items.Clear();
                }

                mtbHQ_SKUGroupID.DisableTextBox(true);

                btnAddSKU_ID.Focus();
            }
            else if (tabName == "SKU-GROUP-EXC")
            {
                mtbSKU_ID_Exc.DisableTextBox(false);
                mtbSKU_ID_Exc.Focus();
            }
            else if (tabName == "HQ_Promotion")
            {
                SetOpenEditPromotionControl();

                Select_HQPromotion(null);
            }
        }

        private void SetOpenEditPromotionControl()
        {
            cbbPromotionType.Enabled = true;
            cbbPromotionPattern.Enabled = true;
            cbbStepCondition1.Enabled = true;

            string date = DateTime.Now.ToString("yyMMdd",cultures);

            if (cbbPromotionPattern.SelectedIndex == 0) //prd
            {
                mtbSKUGroupID1.Enabled = true;
                mtbSKUGroupID1.ReadOnly = false;

                mtbSKUGroupID1.Text = date;

                chkboxsRecomputable.Enabled = true;
                chkboxsIsUseCoupon.Enabled = true;
                chkboxsIgnoreApplied.Enabled = true;
            }
            else //txn
            {
                mtbSKUGroupID1.Enabled = false;

                chkboxsRecomputable.Enabled = false;
                chkboxsIsUseCoupon.Enabled = false;
                chkboxsIgnoreApplied.Enabled = false;
            }

            txtConditionStart.DisableTextBox(false);

            if (!string.IsNullOrEmpty(txtConditionStart.Text) && !string.IsNullOrEmpty(txtConditionEnd.Text))
            {//มีค่าใน ConditionStart ถึงกรอก ConditionEnd ได้
                txtConditionEnd.DisableTextBox(false);
            }
            else
            {
                txtConditionEnd.DisableTextBox(true);
            }

            cbbDisCountPattern.Enabled = true;

            if (cbbDisCountPattern.SelectedIndex > 0) //disc_perc_amt, disc_baht_qty, fix_baht_qty 
            {
                txtDisCountAmt.DisableTextBox(false);

                mtbPruductGroupRewardID.Enabled = false;
                mtbPruductGroupRewardID2.Enabled = false;
            }
            else //free = free 
            {
                txtDisCountAmt.DisableTextBox(true);

                mtbPruductGroupRewardID.Enabled = true;
                mtbPruductGroupRewardID2.Enabled = true;//กรอกหรือไม่ก็ได้

                mtbPruductGroupRewardID.Text = date;
            }

            if (mtbPruductGroupRewardID.MaskCompleted == true && !string.IsNullOrEmpty(txtPruductGroupRewardAmt.Text))//มีข้อมูลใน PruductGroupRewardID จะต้องปิดให้กรอก
            {
                txtPruductGroupRewardAmt.DisableTextBox(false);
            }
            else
            {
                txtPruductGroupRewardAmt.DisableTextBox(true);
            }

            mtbRewardID.Enabled = true;
            mtbRewardID.Text = date;

            txtPromotionPriority.DisableTextBox(false);
            txtHitLimit.DisableTextBox(false);

            dtpEffectiveDate.Enabled = true;
            dtpExpireDate.Enabled = true;

            txtPlusSaleFrom.DisableTextBox(false);

            //ปิดไว้ก่อนยังไม่ใช้
            txtPruductGroupRewardAmt2.DisableTextBox(true);
            cbbStepCondition2.Enabled = false;
            txtSKUGroupID2.Enabled = false;

        }

        private bool ValidateSave(string tabName)
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (tabName == "HQ_Promotion_Master")
            {
                errList.SetErrMessage(Validate_HQ_PromotionMaster);
            }
            else if (tabName == "HQ-Reward")
            {
                errList.SetErrMessage(Validate_HQ_Reward);
            }
            else if (tabName == "SKU-GROUP")
            {
                errList.SetErrMessage(Validate_HQ_SKUGroup);
            }
            else if (tabName == "SKU-GROUP-EXC")
            {
                errList.SetErrMessage(Validate_HQ_SKUGroup_Exc);
            }
            else if (tabName == "HQ_Promotion")
            {
                ValidateHQ_PromotionList();
                errList.SetErrMessage(Validate_HQ_Promotion);
            }
            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }
            return ret;
        }

        private void SaveHQ_PromotionMaster()
        {
            try
            {
                int ret = 0;

                var HQ_PromotionMaster = bu.GetHQ_Promotion_Master(x => x.PromotionID == mtbPromotionID_Master.Text); //new

                if (EditFlag == 1)
                {
                    if (HQ_PromotionMaster.Count > 0)
                    {
                        string msg = "PromotionID ซ้ำกับในระบบ กรุณาเปลี่ยนใหม่ !!";
                        msg.ShowWarningMessage();
                        return;
                    }
                }

                var tbl_HQ_Promotion_Master = new tbl_HQ_Promotion_Master();

                if (HQ_PromotionMaster.Count > 0)
                {
                    tbl_HQ_Promotion_Master = HQ_PromotionMaster[0];
                    tbl_HQ_Promotion_Master.PromotionName = txtPromotionName.Text;
                }
                else
                {
                    tbl_HQ_Promotion_Master.PromotionID = mtbPromotionID_Master.Text;
                    tbl_HQ_Promotion_Master.PromotionName = txtPromotionName.Text;
                }

                ret = bu.UpdateHQ_Promotion_MasterData(tbl_HQ_Promotion_Master); //new

                if (ret == 1)
                {
                    mtbPromotionID_Master.Text = "";
                    txtPromotionName.Text = "";

                    mtbPromotionID_Master.DisableTextBox(true);
                    txtPromotionName.DisableTextBox(true);

                    grdHQPromotionMaster.Enabled = true;
                    pnlSearch_HQ_Promotion_Master.Enabled = true;

                    btnSearchHQ_Promotion_Master.PerformClick();

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void SaveHQ_Reward()
        {
            try
            {
                int ret = 0;

                var HQ_Reward = bu.GetHQ_Reward(x => x.RewardID == mtb_RewardID.Text); //new

                if (EditFlag == 1)
                {
                    if (HQ_Reward.Count > 0)
                    {
                        string msg = "RewardID ซ้ำกับในระบบ กรุณาเปลี่ยนใหม่ !!";
                        msg.ShowWarningMessage();
                        return;
                    }
                }

                var tbl_HQ_Reward = new tbl_HQ_Reward();

                if (HQ_Reward.Count > 0)
                {
                    tbl_HQ_Reward = HQ_Reward[0];
                    tbl_HQ_Reward.RewardName = txtRewardName.Text;
                }
                else
                {
                    tbl_HQ_Reward.RewardID = mtb_RewardID.Text;
                    tbl_HQ_Reward.RewardName = txtRewardName.Text;
                }

                ret = bu.UpdateHQ_RewardData(tbl_HQ_Reward);//

                if (ret == 1)
                {
                    mtb_RewardID.Text = "";
                    txtRewardName.Text = "";

                    mtb_RewardID.DisableTextBox(true);
                    txtRewardName.DisableTextBox(true);

                    grdHQ_Reward.Enabled = true;
                    pnlSearch_HQ_Reward.Enabled = true;

                    btnSearchReward.PerformClick();

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void SaveHQ_SKUGroup()
        {
            int ret = 0;

            string msg = "";

            try
            {
                var HQ_SKUGroup = bu.GetHQ_SKUGroup(x=>x.SKUGroupID == mtbHQ_SKUGroupID.Text);

                for (int i = 0; i < lsbSKU_ID.Items.Count; i++)
                {
                    string SKU = lsbSKU_ID.Items[i].ToString();

                    var SKUGroup = HQ_SKUGroup.FirstOrDefault(x => x.SKUGroupID == mtbHQ_SKUGroupID.Text && x.SKU_ID == SKU);

                    if (SKUGroup != null)
                    {
                        msg = "SKUGroupID และ SKU_ID มีแล้วในระบบ กรุณาเปลี่ยนใหม่ !!";
                        msg.ShowWarningMessage();
                        return;
                    }
                }

                if (EditFlag == 1) //Insert
                {
                    var SKUGroupList = new List<tbl_HQ_SKUGroup>();

                    for (int i = 0; i < lsbSKU_ID.Items.Count; i++)
                    {
                        string SKU = lsbSKU_ID.Items[i].ToString();

                        var SkuGroup = new tbl_HQ_SKUGroup();

                        SkuGroup.SKUGroupID = mtbHQ_SKUGroupID.Text;
                        SkuGroup.SKU_ID = SKU;

                        SKUGroupList.Add(SkuGroup);
                    }

                    foreach (var data in SKUGroupList)
                    {
                        ret = bu.UpdateHQ_SKUGroupData(data);
                    }
                }
                else //Update
                {
                    string oldSKU_ID = grdSkuGroup.CurrentRow.Cells["colSKU_ID"].Value.ToString();

                    var tbl_HQ_SKUGroups = new tbl_HQ_SKUGroup();
                    tbl_HQ_SKUGroups.SKUGroupID = mtbHQ_SKUGroupID.Text;
                    tbl_HQ_SKUGroups.SKU_ID = lsbSKU_ID.Items[0].ToString();

                    ret = bu.UpdateSKUGroup(tbl_HQ_SKUGroups, oldSKU_ID);
                }

                if (ret == 1)
                {
                    mtbHQ_SKUGroupID.DisableTextBox(true);
                    mtbHQ_SKUGroupID.BackColor = ColorTranslator.FromHtml("#DCDCDC");

                    lsbSKU_ID.DisableTextBox(true);
                    lsbSKU_ID.BackColor = ColorTranslator.FromHtml("#DCDCDC");

                    grdSkuGroup.Enabled = true;
                    pnlSearchHQ_SKUGroup.Enabled = true;

                    btnSearchSkuGroup.PerformClick();

                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void Save_SKUGroupExc()
        {
            int ret = 0;
            string msg = "";
            string oldSKU_ID = "";
            try
            {
                var SKUGroupExcList = bu.GetHQ_SKUGroupExc(x => x.SKU_ID == mtbSKU_ID_Exc.Text);

                if (EditFlag == 2) // update
                {
                    if (SKUGroupExcList.Count > 0)
                    {
                        msg = "SKU_ID ซ้ำกับในระบบ กรุณาเปลี่ยนใหม่ !!";
                        msg.ShowWarningMessage();
                        return;
                    }
                    else
                    {
                        if (grdSKUGroup_EXC.RowCount > 0)
                        {
                            oldSKU_ID = grdSKUGroup_EXC.CurrentRow.Cells["colSKU_ID_EXC"].Value.ToString();
                        }
                    }
                }

                if (EditFlag == 1) // Insert
                {
                    if (SKUGroupExcList.Count > 0)
                    {
                        msg = "SKU_ID ซ้ำกับในระบบ กรุณาเปลี่ยนใหม่ !!";
                        msg.ShowWarningMessage();
                        return;
                    }
                }

                var new_HQ_SKUGroupExc = new tbl_HQ_SKUGroup_EXC();

                new_HQ_SKUGroupExc.SKU_ID = mtbSKU_ID_Exc.Text;

                ret = bu.UpdateHQ_SKUGroup_ExcData(new_HQ_SKUGroupExc,oldSKU_ID);//

                if (ret == 1)
                {
                    mtbSKU_ID_Exc.Text = "";

                    mtbSKU_ID_Exc.DisableTextBox(true);

                    grdSKUGroup_EXC.Enabled = true;
                    pnlSearchSKUGroupExc.Enabled = true;

                    btnSearchSKUGroup_EXC.PerformClick();

                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void SaveHQ_Promotion()
        {
            try
            {
                string msg = "";

                if (mtbPruductGroupRewardID.Text == mtbPruductGroupRewardID2.Text && cbbDisCountPattern.SelectedIndex == 0) //free to free
                {
                    msg = "PruductGroupRewardID ห้ามเป็นรหัสเดียวกับ PruductGroupRewardID2\n";
                    msg.ShowErrorMessage();

                    mtbPruductGroupRewardID.ErrorMaskedTextBox();
                    mtbPruductGroupRewardID2.ErrorMaskedTextBox();

                    return;
                }

                int ret = 0;

                var HQPro = bu.GetSelectHQPromotion(txtPromotionID_HQPro.Text);

                var HQPro_Data = new tbl_HQ_Promotion();

                if (HQPro.Count > 0)
                {
                    HQPro_Data = HQPro[0];
                    PrePareSaveHQPromotion(HQPro_Data);

                    HQPro_Data.UpdateDate = DateTime.Now;
                    HQPro_Data.UpdateBy = Helper.tbl_Users.Username;
                }
                else
                {
                    HQPro_Data.PromotionID = txtPromotionID_HQPro.Text; //
                    PrePareSaveHQPromotion(HQPro_Data);

                    HQPro_Data.CreatedDate = DateTime.Now;
                    HQPro_Data.CreateBy = Helper.tbl_Users.Username;

                    HQPro_Data.UpdateDate = null;
                    HQPro_Data.UpdateBy = null;
                }

                ret = bu.UpdateHQPromotionData(HQPro_Data);

                if (ret == 1)
                {
                    EditFlag = 0;
                    grdHQ_Promotion.Enabled = true;
                    pnlSearch_HQPro.Enabled = true;

                    btnSearchHQ_Promotion.PerformClick();

                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void PrePareSaveHQPromotion(tbl_HQ_Promotion HQPro_Data)
        {
            HQPro_Data.PromotionType = cbbPromotionType.Text;
            HQPro_Data.PromotionPattern = cbbPromotionPattern.Text;
            HQPro_Data.StepCondition1 = cbbStepCondition1.Text;
       
            if (cbbPromotionPattern.SelectedIndex == 0) //prd
            {
                HQPro_Data.SKUGroupID1 = mtbSKUGroupID1.Text;

                HQPro_Data.Recomputable = chkboxsRecomputable.Checked ? true : false;
                HQPro_Data.IsUseCoupon = chkboxsIsUseCoupon.Checked ? true : false;
                HQPro_Data.IgnoreApplied = chkboxsIgnoreApplied.Checked ? true : false;
            }
            else //txn
            {
                HQPro_Data.SKUGroupID1 = null;

                HQPro_Data.Recomputable = null;
                HQPro_Data.IsUseCoupon = null;
                HQPro_Data.IgnoreApplied = null;
            }

            HQPro_Data.ConditionStart = Convert.ToInt32(txtConditionStart.Text);

            if (!string.IsNullOrEmpty(txtConditionStart.Text) && !string.IsNullOrEmpty(txtConditionEnd.Text))//มีค่าใน ConditionStart ถึงกรอก ConditionEnd ได้
            {
                HQPro_Data.ConditionEnd = Convert.ToInt32(txtConditionEnd.Text);
            }
            else
            {
                HQPro_Data.ConditionEnd = null;
            }

            HQPro_Data.DisCountPattern = cbbDisCountPattern.Text;

            if (cbbDisCountPattern.SelectedIndex > 0) //disc_perc_amt, disc_baht_qty, fix_baht_qty 
            {
                HQPro_Data.DisCountAmt = Convert.ToDecimal(txtDisCountAmt.Text).ToDecimalN2();

                HQPro_Data.PruductGroupRewardID = null;

                HQPro_Data.PruductGroupRewardID2 = null;
            }
            else //free = free 
            {
                HQPro_Data.DisCountAmt = null;

                HQPro_Data.PruductGroupRewardID = mtbPruductGroupRewardID.Text;

                if (mtbPruductGroupRewardID2.MaskCompleted == true)
                {
                    HQPro_Data.PruductGroupRewardID2 = mtbPruductGroupRewardID2.Text; //กรอกหรือไม่ก็ได้
                }
               
            }

            if (mtbPruductGroupRewardID.MaskCompleted == true && !string.IsNullOrEmpty(txtPruductGroupRewardAmt.Text))//มีข้อมูลใน PruductGroupRewardID จะต้องปิดให้กรอก
            {
                HQPro_Data.PruductGroupRewardAmt = Convert.ToInt32(txtPruductGroupRewardAmt.Text);
            }
            else
            {
                HQPro_Data.PruductGroupRewardAmt = null;
            }

            HQPro_Data.RewardID = mtbRewardID.Text;
            HQPro_Data.PromotionPriority = Convert.ToInt32(txtPromotionPriority.Text);

            if (!string.IsNullOrEmpty(txtHitLimit.Text))
            {
                HQPro_Data.HitLimit = Convert.ToInt32(txtHitLimit.Text);
            }
            else
            {
                HQPro_Data.HitLimit = null;
            }

            HQPro_Data.EffectiveDate = dtpEffectiveDate.Value;
            HQPro_Data.ExpireDate = dtpExpireDate.Value;

            if (!string.IsNullOrEmpty(txtPlusSaleFrom.Text))
            {
                HQPro_Data.PlusSaleFrom = Convert.ToInt32(txtPlusSaleFrom.Text);
            }
            else
            {
                HQPro_Data.PlusSaleFrom = null;
            }

            HQPro_Data.PruductGroupRewardAmt2 = null; //ปิดไว้ก่อนยังไม่ใช้
            HQPro_Data.StepCondition2 = null;//ปิดไว้ก่อนยังไม่ใช้
            HQPro_Data.SKUGroupID2 = null;//ปิดไว้ก่อนยังไม่ใช้

            HQPro_Data.SaleTypeID = Convert.ToInt32(cbbSaleType.SelectedValue);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string tabName = tabControl1.SelectedTab.Text.ToString();

            if (!ValidateSave(tabName))//new
            {
                return;
            }

            string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการบันทึก!!";

            if (!cfMsg.ConfirmMessageBox(title))
                return;

            if (tabName == "HQ_Promotion_Master")
            {
                SaveHQ_PromotionMaster();
            }
            else if (tabName == "HQ-Reward")
            {
                SaveHQ_Reward();
            }
            else if (tabName == "SKU-GROUP")
            {
                SaveHQ_SKUGroup();
            }
            else if (tabName == "SKU-GROUP-EXC")
            {
                Save_SKUGroupExc();
            }
            else if (tabName == "HQ_Promotion")
            {
                SaveHQ_Promotion();
            }
        }

        private void Select_HQPromotion_Master(DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow grdRows = null;

                if (e != null)
                {
                    if (e.RowIndex == -1)
                    {
                        return;
                    }
                    else
                    {
                        grdRows = grdHQPromotionMaster.Rows[e.RowIndex];
                    }
                }
                else
                {
                    grdRows = grdHQPromotionMaster.CurrentRow;
                }

                if (grdRows != null)
                {
                    mtbPromotionID_Master.Text = grdRows.Cells["colPromotionID"].Value.ToString();
                    txtPromotionName.Text = grdRows.Cells["colPromotionName"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void grdHQPromotionMaster_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Select_HQPromotion_Master(e);
        }

        private void btnSearchHQ_Promotion_Master_Click(object sender, EventArgs e)
        {
            BindHQ_Promotion_Master();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindHQ_Promotion_Master();
            }
        }

        private void BindHQ_Reward()
        {
            mtb_RewardID.Text = "";
            txtRewardName.Text = "";

            if (grdBranch.Rows.Count > 0)
            {
                DataTable HQ_Reward = new DataTable();

                HQ_Reward = bu.GetHQ_RewardData(txtSearchReward.Text);

                grdHQ_Reward.DataSource = HQ_Reward;
                lbl_Qty_Reward.Text = HQ_Reward.Rows.Count.ToNumberFormat();

                int colNo = HQ_Reward.Rows.Count;
                SetButtonAfterBindGrid(colNo);

                Select_HQ_Reward(null);
            }
        }

        private void btnSearchReward_Click(object sender, EventArgs e)
        {
            BindHQ_Reward();
        }

        private void txtSearchReward_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindHQ_Reward();
            }
        }

        private void Select_HQ_Reward(DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow grdRows = null;

                if (e != null)
                {
                    if (e.RowIndex == -1)
                    {
                        return;
                    }
                    else
                    {
                        grdRows = grdHQ_Reward.Rows[e.RowIndex];
                    }
                }
                else
                {
                    grdRows = grdHQ_Reward.CurrentRow;
                }

                if (grdRows != null)
                {
                    mtb_RewardID.Text = grdRows.Cells["colRewardID"].Value.ToString();
                    txtRewardName.Text = grdRows.Cells["colRewardName"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void grdHQ_Reward_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Select_HQ_Reward(e);
        }

        private void grdHQ_Reward_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdHQ_Reward.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string tabName = tabControl1.SelectedTab.Text.ToString();

            if (tabName == "HQ_Promotion_Master")
            {
                mtbPromotionID_Master.DisableTextBox(true);
                txtPromotionName.DisableTextBox(true);

                pnlSearch_HQ_Promotion_Master.Enabled = true;
                grdHQPromotionMaster.Enabled = true;
                BindHQ_Promotion_Master();
            }
            else if (tabName == "HQ-Reward")
            {
                mtb_RewardID.DisableTextBox(true);
                txtRewardName.DisableTextBox(true);

                pnlSearch_HQ_Reward.Enabled = true;
                grdHQ_Reward.Enabled = true;
                BindHQ_Reward();
            }
            else if (tabName == "SKU-GROUP")
            {
                mtbHQ_SKUGroupID.DisableTextBox(true);
                lsbSKU_ID.DisableTextBox(true);

                pnlSearchHQ_SKUGroup.Enabled = true;
                grdSkuGroup.Enabled = true;

                btnAddSKU_ID.Enabled = false;
                btnRemoveSKU.Enabled = false;

                BindHQ_SKUGroup();
            }
            else if (tabName == "SKU-GROUP-EXC")
            {
                mtbSKU_ID_Exc.DisableTextBox(true);

                pnlSearchSKUGroupExc.Enabled = true;
                grdSKUGroup_EXC.Enabled = true;
                BindHQ_SKUGroup_Exc();
            }
            else if (tabName == "HQ_Promotion")
            {
                pnlEdit_HQPromotion.OpenControl(false, PanelEdit_HQPromotion_Controls.ToArray());

                pnlSearch_HQPro.Enabled = true;
                grdHQ_Promotion.Enabled = true;
                btnSearchHQ_Promotion.PerformClick();
            }
        }

        private void BindHQ_SKUGroup()
        {
            btnAddSKU_ID.Enabled = false;
            btnRemoveSKU.Enabled = false;

            if (grdBranch.Rows.Count > 0)
            {
                mtbHQ_SKUGroupID.Text = "";

                if (lsbSKU_ID.Items.Count > 0)
                {
                    lsbSKU_ID.Items.Clear();
                }

                DataTable dt = new DataTable();

                dt = bu.GetHQ_SKUGroupData(txtSearch_SKUGroup.Text); // new

                grdSkuGroup.DataSource = dt;

                lbl_Qty_grdSkuGroup.Text = dt.Rows.Count.ToNumberFormat();

                int colNo = dt.Rows.Count;

                SetButtonAfterBindGrid(colNo);

                Select_SkuGroup(null);
            }
        }

        private void btnSearchSkuGroup_Click(object sender, EventArgs e)
        {
            BindHQ_SKUGroup();
        }

        private void txtSearch_SKUGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindHQ_SKUGroup();
            }
        }

        private void Select_SkuGroup(DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow grdRows = null;

                if (e != null)
                {
                    if (e.RowIndex == -1)
                        return;
                    else
                        grdRows = grdSkuGroup.Rows[e.RowIndex];
                }
                else
                    grdRows = grdSkuGroup.CurrentRow;

                if (grdRows != null)
                {
                    mtbHQ_SKUGroupID.Text = grdRows.Cells["colSKUGroupID"].Value.ToString();

                    if (lsbSKU_ID.Items.Count > 0)
                    {
                        lsbSKU_ID.Items.Clear();
                    }

                    string SKU_ID = grdRows.Cells["colSKU_ID"].Value.ToString();

                    lsbSKU_ID.Items.Add(SKU_ID);

                    //var ListSKU_ID = SKU_ID.ToArray().ToList();

                    //string FilterID = "";

                    //for (int i = 0; i < ListSKU_ID.Count; i++)
                    //{
                    //    string ID = ListSKU_ID[i].ToString();

                    //    if (ID != "," && ID != "\r" && ID != "\n")
                    //    {
                    //        FilterID += ID;

                    //        if (FilterID.Length == 8)
                    //        {
                    //            lsbSKU_ID.Items.Add(FilterID);
                    //            FilterID = "";
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void grdSkuGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Select_SkuGroup(e);
        }

        private void grdSkuGroup_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdSkuGroup.SetRowPostPaint(sender, e, this.Font);
        }

        private void BindHQ_SKUGroup_Exc()
        {
            mtbSKU_ID_Exc.Text = "";

            if (grdBranch.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = bu.GetHQ_SKUGroup_ExcData(txtSearchSKUGroup_EXC.Text); // new

                grdSKUGroup_EXC.DataSource = dt;
                lbl_Qty_SKUGroup_Exc.Text = dt.Rows.Count.ToNumberFormat();

                int colNo = dt.Rows.Count;
                SetButtonAfterBindGrid(colNo);

                Select_SKUGroupExc(null);
            }
        }

        private void btnSearchSKUGroup_EXC_Click(object sender, EventArgs e)
        {
            BindHQ_SKUGroup_Exc();
        }

        private void txtSearchSKUGroup_EXC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindHQ_SKUGroup_Exc();
            }
        }

        private void Select_SKUGroupExc(DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow grdRows = null;

                if (e != null)
                {
                    if (e.RowIndex == -1)
                        return;
                    else
                        grdRows = grdSKUGroup_EXC.Rows[e.RowIndex];
                }
                else
                    grdRows = grdSKUGroup_EXC.CurrentRow;

                if (grdRows != null)
                    mtbSKU_ID_Exc.Text = grdRows.Cells["colSKU_ID_EXC"].Value.ToString();

            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void grdSKUGroup_EXC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Select_SKUGroupExc(e);
        }

        private void grdSKUGroup_EXC_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdSKUGroup_EXC.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            frmExcel frm = new frmExcel();
            frm.ShowDialog();
        }

        private void BindHQ_Promotion()
        {
            if (grdBranch.Rows.Count > 0)
            {
                pnlEdit_HQPromotion.ClearControl();
                pnlEdit_HQPromotion.OpenControl(false, PanelEdit_HQPromotion_Controls.ToArray());

                dtHQPromotion = new DataTable();
                dtHQPromotion = bu.GetHQ_PromotionData(txtSearchHQ_Promotion.Text);

                grdHQ_Promotion.DataSource = dtHQPromotion;
                lbl_Qty_Promotion.Text = dtHQPromotion.Rows.Count.ToNumberFormat();

                int colNo = dtHQPromotion.Rows.Count;
                SetButtonAfterBindGrid(colNo);

                Select_HQPromotion(null);
            }
        }

        private void btnSearchHQ_Promotion_Click(object sender, EventArgs e)
        {
            BindHQ_Promotion();
        }

        private void grdHQ_Promotion_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdHQ_Promotion.SetRowPostPaint(sender, e, this.Font);
        }

        private void Select_HQPromotion(DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow grdRows = null;

                if (e != null)
                {
                    if (e.RowIndex == -1)
                    {
                        return;
                    }
                    else
                    {
                        grdRows = grdHQ_Promotion.Rows[e.RowIndex];
                    }
                }
                else
                {
                    grdRows = grdHQ_Promotion.CurrentRow;
                }

                if (grdRows != null)
                {
                    string colPromotionID = grdRows.Cells["colPromotionID_HQPro"].Value.ToString();

                    DataRow r = dtHQPromotion.AsEnumerable().FirstOrDefault(x => x.Field<string>("PromotionID") == colPromotionID);

                    if (r != null)
                    {
                        txtPromotionID_HQPro.Text = colPromotionID;

                        string PromotionType = r["PromotionType"].ToString();
                        if (PromotionType == "mmc")
                        {
                            cbbPromotionType.SelectedIndex = 0;
                        }
                        else //mmch
                        {
                            cbbPromotionType.SelectedIndex = 1;
                        }
                        
                        string PromotionPattern = r["PromotionPattern"].ToString();

                        if (PromotionPattern == "prd")
                        {
                            cbbPromotionPattern.SelectedIndex = 0;

                            if (EditFlag == 2)
                            {
                                chkboxsRecomputable.Enabled = true;
                                chkboxsIgnoreApplied.Enabled = true;
                                chkboxsIsUseCoupon.Enabled = true;
                            }
                        }
                        else
                        {
                            cbbPromotionPattern.SelectedIndex = 1;

                            chkboxsRecomputable.Enabled = false;
                            chkboxsIgnoreApplied.Enabled = false;
                            chkboxsIsUseCoupon.Enabled = false;
                        }
                        
                        cbbStepCondition1.Text = r["StepCondition1"].ToString();

                        mtbSKUGroupID1.Text = r["SKUGroupID1"].ToString();
                        txtConditionStart.Text = r["ConditionStart"].ToString();
                        txtConditionEnd.Text = r["ConditionEnd"].ToString();

                        string DisCountPattern = r["DisCountPattern"].ToString();

                        PrePareDiscountPattern(DisCountPattern);

                        txtDisCountAmt.Text = r["DisCountAmt"].ToString();

                        mtbPruductGroupRewardID.Text = r["PruductGroupRewardID"].ToString();
                        txtPruductGroupRewardAmt.Text = r["PruductGroupRewardAmt"].ToString();

                        mtbPruductGroupRewardID2.Text = r["PruductGroupRewardID2"].ToString();
                        txtPruductGroupRewardAmt2.Text = r["PruductGroupRewardAmt2"].ToString();

                        cbbStepCondition2.Text = r["StepCondition2"].ToString();

                        txtSKUGroupID2.Text = r["SKUGroupID2"].ToString();
                        mtbRewardID.Text = r["RewardID"].ToString();
                        txtPromotionPriority.Text = r["PromotionPriority"].ToString();
                        txtHitLimit.Text = r["HitLimit"].ToString();

                        CheckBoxSelect(r["Recomputable"].ToString(), chkboxsRecomputable);

                        CheckBoxSelect(r["IgnoreApplied"].ToString(), chkboxsIgnoreApplied);

                        CheckBoxSelect(r["IsUseCoupon"].ToString(), chkboxsIsUseCoupon);

                        dtpEffectiveDate.Value = Convert.ToDateTime(r["EffectiveDate"]);
                        dtpExpireDate.Value = Convert.ToDateTime(r["ExpireDate"]);
                        txtPlusSaleFrom.Text = r["PlusSaleFrom"].ToString();

                        string SaleTypeID = r["SaleTypeID"].ToString();
                        if (!string.IsNullOrEmpty(SaleTypeID))
                        {
                            cbbSaleType.SelectedValue = Convert.ToInt32(SaleTypeID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void PrePareDiscountPattern(string DisCountPattern)
        {
            if (DisCountPattern == "free=free")
            {
                cbbDisCountPattern.SelectedIndex = 0;
            }
            else if (DisCountPattern == "disc_perc_amt")
            {
                cbbDisCountPattern.SelectedIndex = 1;
            }
            else if (DisCountPattern == "disc_baht_qty")
            {
                cbbDisCountPattern.SelectedIndex = 2;
            }
            else if (DisCountPattern == "fix_baht_qty")
            {
                cbbDisCountPattern.SelectedIndex = 3;
            }
        }

        private void CheckBoxSelect(string text,CheckBox chkBox)
        {
            if (!string.IsNullOrEmpty(text))
            {
                bool Value = Convert.ToBoolean(text);

                if (Value == true)
                {
                    chkBox.Checked = true;
                }
            }
            else
            {
                chkBox.Checked = false;
            }
        }

        private void grdHQ_Promotion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Select_HQPromotion(e);
        }

        private void txtSearchHQ_Promotion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindHQ_Promotion();
            }
        }

        private void cbbPromotionPattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EditFlag > 0)
            {
                if (cbbPromotionPattern.SelectedIndex == 0) //prd
                {
                    string date = DateTime.Now.ToString("yyMMdd",cultures);

                    mtbSKUGroupID1.DisableTextBox(false);//
                    mtbSKUGroupID1.Enabled = true;
                    mtbSKUGroupID1.Text = date;

                    chkboxsRecomputable.Enabled = true;
                    chkboxsIgnoreApplied.Enabled = true;
                    chkboxsIsUseCoupon.Enabled = true;
                }
                else // txn
                {
                    mtbSKUGroupID1.Text = ""; //
                    mtbSKUGroupID1.DisableTextBox(true);
                    mtbSKUGroupID1.Enabled = false;

                    chkboxsRecomputable.Enabled = false;
                    chkboxsIgnoreApplied.Enabled = false;
                    chkboxsIsUseCoupon.Enabled = false;
                }
            }
            else
            {
                mtbSKUGroupID1.DisableTextBox(true);
            }
        }

        private void txtConditionStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtConditionStart.SetTextBoxNumberOnly(e);
        }

        private void txtConditionStart_TextChanged(object sender, EventArgs e)
        {
            if (EditFlag > 0)
            {
                if (!string.IsNullOrEmpty(txtConditionStart.Text))
                {
                    txtConditionEnd.DisableTextBox(false);
                }
                else
                {
                    txtConditionEnd.Text = "";
                    txtConditionEnd.DisableTextBox(true);
                }
            }
        }

        private void txtConditionEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtConditionEnd.SetTextBoxNumberOnly(e);
        }

        private void cbbDisCountPattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EditFlag > 0)
            {
                if (cbbDisCountPattern.SelectedIndex == 0) //free=free
                {
                    mtbPruductGroupRewardID.DisableTextBox(false);
                    mtbPruductGroupRewardID.Enabled = true;
                    txtDisCountAmt.Text = "";
                    txtDisCountAmt.DisableTextBox(true);

                    mtbPruductGroupRewardID2.DisableTextBox(false);
                    mtbPruductGroupRewardID2.Enabled = true;
                    if (!string.IsNullOrEmpty(mtbPruductGroupRewardID.Text))
                    {
                        txtPruductGroupRewardAmt.DisableTextBox(true);
                    }
                    else
                    {
                        txtPruductGroupRewardAmt.DisableTextBox(false);
                    }

                    if (!string.IsNullOrEmpty(mtbPruductGroupRewardID2.Text))
                    {
                        txtPruductGroupRewardAmt2.DisableTextBox(false);
                    }
                    else
                    {
                        txtPruductGroupRewardAmt2.DisableTextBox(true);
                    }
                }
                else
                {
                    mtbPruductGroupRewardID.Text = "";
                    mtbPruductGroupRewardID.DisableTextBox(true);

                    txtDisCountAmt.DisableTextBox(false);

                    mtbPruductGroupRewardID2.Text = "";
                    mtbPruductGroupRewardID2.DisableTextBox(true);
                }
            }
        }

        private void txtPruductGroupRewardAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtPruductGroupRewardAmt.SetTextBoxNumberOnly(e);
        }

        private void txtPruductGroupRewardAmt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtPruductGroupRewardAmt2.SetTextBoxNumberOnly(e);
        }

        private void txtPromotionPriority_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtPromotionPriority.SetTextBoxNumberOnly(e);
        }

        private void txtHitLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtHitLimit.SetTextBoxNumberOnly(e);
        }

        private void txtPlusSaleFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtPlusSaleFrom.SetTextBoxNumberOnly(e);
        }

        private void txtDisCountAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtDisCountAmt.SetTextBoxNumberOnly(e);
        }

        private void mtbPruductGroupRewardID_TextChanged(object sender, EventArgs e)
        {
            if (EditFlag > 0)
            {
                if (mtbPruductGroupRewardID.MaskCompleted == true)//มีข้อมูล เปิดให้กรอก
                {
                    txtPruductGroupRewardAmt.DisableTextBox(false);
                    
                }
                else
                {
                    txtPruductGroupRewardAmt.Text = "";
                    txtPruductGroupRewardAmt.DisableTextBox(true); //ปิด
                }

                if (mtbPruductGroupRewardID.Text.Length < 8)
                {
                    string date = DateTime.Now.ToString("yyMMdd",cultures);
                    mtbPruductGroupRewardID.Text = date;
                }
            }
        }

        private void mtbPruductGroupRewardID2_TextChanged(object sender, EventArgs e)
        {
            if (EditFlag > 0)
            {
                if (mtbPruductGroupRewardID2.MaskCompleted == true)
                {
                    txtPruductGroupRewardAmt2.Text = "";
                    txtPruductGroupRewardAmt2.DisableTextBox(false);
                }
                else
                {
                    txtPruductGroupRewardAmt2.DisableTextBox(true);
                }

                if (mtbPruductGroupRewardID2.Text.Length < 8)
                {
                    string date = DateTime.Now.ToString("yyMMdd",cultures);
                    mtbPruductGroupRewardID2.Text = date;
                }
            }
        }

        private void btnRefresh_Branch_Click(object sender, EventArgs e)
        {
            BindBranchData();
        }

        private void chkBoxSelectBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (grdBranch.Rows.Count > 0)
            {
                for (int i = 0; i < grdBranch.Rows.Count; i++)
                {
                    bool OnlineStatus = Convert.ToBoolean(grdBranch.Rows[i].Cells["colOnlineStatus"].Value);
                    if (Convert.ToBoolean(grdBranch.Rows[i].Cells["colChkBranch"].Value) == false && OnlineStatus == true)
                    {
                        grdBranch.Rows[i].Cells["colChkBranch"].Value = true;
                    }
                    else
                    {
                        grdBranch.Rows[i].Cells["colChkBranch"].Value = false;
                    }
                }
            }
            else
            {
                chkBoxSelectBranch.Checked = false;
            }
        }

        private void btnAddSKU_ID_Click(object sender, EventArgs e)
        {
            frmSKU frm = new frmSKU();
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(skuID))
            {
                bool dupplicate = false;

                if (lsbSKU_ID.Items.Count > 0)
                {
                    for (int i = 0; i < lsbSKU_ID.Items.Count; i++)
                    {
                        string skuIDlist = lsbSKU_ID.Items[i].ToString();
                        if (skuID == skuIDlist)
                        {
                            string msg = "SKU_ID มีอยู่แล้ว กรุณาเพิ่ม SKU_ID อื่น !!";
                            msg.ShowWarningMessage();
                            dupplicate = true;
                            break;
                        }
                    }

                }

                if (dupplicate == false)
                {
                    if (EditFlag == 2)//Update
                    {
                        lsbSKU_ID.Items.Clear();
                    }

                    lsbSKU_ID.Items.Add(skuID);
                }
            }
        }

        private void mtbSKUGroupID1_TextChanged(object sender, EventArgs e)
        {
            if (mtbSKUGroupID1.Text.Length < 7)
            {
                string date = DateTime.Now.ToString("yyMMdd",cultures);
                mtbSKUGroupID1.Text = date;
            }
        }

        private void btnRemoveSKU_Click(object sender, EventArgs e)
        {
            if (lsbSKU_ID.SelectedIndex != -1)
            {
                lsbSKU_ID.Items.RemoveAt(lsbSKU_ID.Items.IndexOf(lsbSKU_ID.SelectedItem));
            }
            else
            {
                string msg = "กรุณาเลือก SKU ที่ต้องการจะลบ !!";
                msg.ShowWarningMessage();
                return;
            }
        }

        private void grdBranch_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdBranch.SetRowPostPaint(sender, e, this.Font);
        }

        private void PrePareDataTable(DataTable dt)
        {
            if (dt.Columns.Count > 0)
            {
                dt.Columns.Clear();
            }
           
            dt.Columns.Add("PromotionID", typeof(string));
            dt.Columns.Add("PromotionType", typeof(string));
            dt.Columns.Add("PromotionPattern", typeof(string));
            dt.Columns.Add("StepCondition1", typeof(string));
            dt.Columns.Add("SKUGroupID1", typeof(string));
            dt.Columns.Add("ConditionStart", typeof(int));
            dt.Columns.Add("ConditionEnd", typeof(int));
            dt.Columns.Add("DisCountPattern", typeof(string));
            dt.Columns.Add("DisCountAmt", typeof(decimal));

            dt.Columns.Add("PruductGroupRewardID", typeof(string));
            dt.Columns.Add("PruductGroupRewardAmt", typeof(int));
            dt.Columns.Add("PruductGroupRewardID2", typeof(string));
            dt.Columns.Add("PruductGroupRewardAmt2", typeof(int));

            dt.Columns.Add("StepCondition2", typeof(string));
            dt.Columns.Add("SKUGroupID2", typeof(int));
            dt.Columns.Add("RewardID", typeof(string));
            dt.Columns.Add("PromotionPriority", typeof(int));
            dt.Columns.Add("HitLimit", typeof(int));

            dt.Columns.Add("Recomputable", typeof(int));
            dt.Columns.Add("IgnoreApplied", typeof(int));
            dt.Columns.Add("IsUseCoupon", typeof(int));

            dt.Columns.Add("EffectiveDate", typeof(string));
            dt.Columns.Add("ExpireDate", typeof(string));

            dt.Columns.Add("PlusSaleFrom", typeof(int));

            dt.Columns.Add("CreatedDate", typeof(string));
            dt.Columns.Add("UpdateDate", typeof(string));

            dt.Columns.Add("CreateBy", typeof(string));
            dt.Columns.Add("UpdateBy", typeof(string));

            dt.Columns.Add("SaleTypeID", typeof(int));
        }

        private DateTime PrePareNewDatetimeFormat2(DateTime TempDateTime)
        {
            int year = 0;

            if (TempDateTime.Year > 2000)
            {
                year = Convert.ToInt32(TempDateTime.Year - 543);
            }
            else
            {
                year = Convert.ToInt32(TempDateTime.Year + 543);
            }

            int months = Convert.ToInt32(TempDateTime.Month);
            int days = Convert.ToInt32(TempDateTime.Day);

            int hour = Convert.ToInt32(TempDateTime.Hour);
            int min = Convert.ToInt32(TempDateTime.Minute);
            int second = Convert.ToInt32(TempDateTime.Second);
            int Millisecond = Convert.ToInt32(TempDateTime.Millisecond);

            DateTime newDate = new DateTime(year, months, days, hour, min, second, Millisecond);

            return newDate;
        }

        private void PreParePromotionData(DataTable dt)
        {
            DataTable newTable = (DataTable)grdHQ_Promotion.DataSource;

            foreach (DataRow r in newTable.Rows)
            {
                DateTime _EffectiveDate = Convert.ToDateTime(r["EffectiveDate"]);
                _EffectiveDate = PrePareNewDatetimeFormat2(_EffectiveDate);

                DateTime _ExpireDate = Convert.ToDateTime(r["ExpireDate"]);
                _ExpireDate = PrePareNewDatetimeFormat2(_ExpireDate);

                string EffectiveDate = _EffectiveDate.ToString("dd/MM/yyyy h:mm:ss.fff");
                string ExpireDate = _ExpireDate.ToString("dd/MM/yyyy h:mm:ss.fff");

                DateTime _CreatedDate = Convert.ToDateTime(r["CreatedDate"]);
                _CreatedDate = PrePareNewDatetimeFormat2(_CreatedDate);

                DateTime _UpdateDate = Convert.ToDateTime(r["UpdateDate"]);
                _UpdateDate = PrePareNewDatetimeFormat2(_UpdateDate);

                Nullable<decimal> ConditionEnd = null;

                if (r["ConditionEnd"].ToString() != "NULL" && !string.IsNullOrEmpty(r["ConditionEnd"].ToString()))
                {
                    ConditionEnd = Convert.ToInt32(r["ConditionEnd"]);
                }

                Nullable<decimal> DisCountAmt = null;
                if (r["DisCountAmt"].ToString() != "NULL" && !string.IsNullOrEmpty(r["DisCountAmt"].ToString()))
                {
                    DisCountAmt = Convert.ToDecimal(r["DisCountAmt"]);
                }

                Nullable<int> PruductGroupRewardAmt = null;
                if (r["PruductGroupRewardAmt"].ToString() != "NULL" && !string.IsNullOrEmpty(r["PruductGroupRewardAmt"].ToString()))
                {
                    PruductGroupRewardAmt = Convert.ToInt32(r["PruductGroupRewardAmt"]);
                }

                Nullable<int> PruductGroupRewardAmt2 = null;
                if (r["PruductGroupRewardAmt2"].ToString() != "NULL" && !string.IsNullOrEmpty(r["PruductGroupRewardAmt2"].ToString()))
                {
                    PruductGroupRewardAmt2 = Convert.ToInt32(r["PruductGroupRewardAmt2"]);
                }

                Nullable<int> SKUGroupID2 = null;

                if (r["SKUGroupID2"].ToString() != "NULL" && !string.IsNullOrEmpty(r["SKUGroupID2"].ToString()))
                {
                    SKUGroupID2 = Convert.ToInt32(r["SKUGroupID2"]);
                }

                Nullable<int> PromotionPriority = null;

                if (r["PromotionPriority"].ToString() != "NULL" && !string.IsNullOrEmpty(r["PromotionPriority"].ToString()))
                {
                    PromotionPriority = Convert.ToInt32(r["PromotionPriority"]);
                }

                Nullable<int> HitLimit = null;

                if (r["HitLimit"].ToString() != "NULL" && !string.IsNullOrEmpty(r["HitLimit"].ToString()))
                {
                    HitLimit = Convert.ToInt32(r["HitLimit"]);
                }

                Nullable<int> Recomputable = null;

                if (r["Recomputable"].ToString() != "NULL" && !string.IsNullOrEmpty(r["Recomputable"].ToString()))
                {
                    Recomputable = Convert.ToInt32(r["Recomputable"]);
                }

                Nullable<int> IgnoreApplied = null;

                if (r["IgnoreApplied"].ToString() != "NULL" && !string.IsNullOrEmpty(r["IgnoreApplied"].ToString()))
                {
                    IgnoreApplied = Convert.ToInt32(r["IgnoreApplied"]);
                }

                Nullable<int> IsUseCoupon = null;

                if (r["IsUseCoupon"].ToString() != "NULL" && !string.IsNullOrEmpty(r["IsUseCoupon"].ToString()))
                {
                    IsUseCoupon = Convert.ToInt32(r["IsUseCoupon"]);
                }

                Nullable<int> PlusSaleFrom = null;

                if (r["PlusSaleFrom"].ToString() != "NULL" && !string.IsNullOrEmpty(r["PlusSaleFrom"].ToString()))
                {
                    PlusSaleFrom = Convert.ToInt32(r["PlusSaleFrom"]);
                }

                Nullable<int> SaleTypeID = null;
                if (r["SaleTypeID"].ToString() != "NULL" && !string.IsNullOrEmpty(r["SaleTypeID"].ToString()))
                {
                    SaleTypeID = Convert.ToInt32(r["SaleTypeID"]);
                }

                dt.Rows.Add(r["PromotionID"].ToString(), r["PromotionType"].ToString()
                    , r["PromotionPattern"].ToString(), r["StepCondition1"].ToString()
                , r["SKUGroupID1"].ToString() == "NULL" ? null : r["SKUGroupID1"].ToString()
                , Convert.ToInt32(r["ConditionStart"])
                , ConditionEnd == null ? null : ConditionEnd
                , r["DisCountPattern"].ToString()
                , DisCountAmt == null ? null : DisCountAmt
                , r["PruductGroupRewardID"].ToString() == "NULL" ? null : r["PruductGroupRewardID"].ToString()
                , PruductGroupRewardAmt == null ? null : PruductGroupRewardAmt
                , r["PruductGroupRewardID2"].ToString() == "NULL" ? null : r["PruductGroupRewardID2"].ToString()
                , PruductGroupRewardAmt2 == null ? null : PruductGroupRewardAmt2
                , r["StepCondition2"].ToString() == "NULL" ? null : r["StepCondition2"].ToString()
                , SKUGroupID2 == null ? null : SKUGroupID2
                , r["RewardID"].ToString()
                , PromotionPriority == null ? null : PromotionPriority
                , HitLimit == null ? null : HitLimit
                , Recomputable == null ? null : Recomputable
                , IgnoreApplied == null ? null : IgnoreApplied
                , IsUseCoupon == null ? null : IsUseCoupon
                , EffectiveDate, ExpireDate
                , PlusSaleFrom == null ? null : PlusSaleFrom
                , _CreatedDate, _UpdateDate
                , r["CreateBy"].ToString(), r["UpdateBy"].ToString()
                , SaleTypeID);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            int Promotion = grdHQ_Promotion.Rows.Count;
            int PromotionMaster = grdHQPromotionMaster.Rows.Count;
            int Reward = grdHQ_Reward.Rows.Count;
            int SKUGroup = grdSkuGroup.Rows.Count;
            int SKUGroupExc = grdSKUGroup_EXC.Rows.Count;

            if (Promotion == 0 && PromotionMaster == 0 && Reward == 0 
                && SKUGroup == 0 && SKUGroupExc == 0)
            {
                string msg = "ไม่พบข้อมูลที่ต้องการ Export กรุณาตรวจสอบข้อมูล !!";
                msg.ShowWarningMessage();
                return;
            }
            else
            {
                string dir = @"C:\AllCashExcels";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                List<DataTable> dtList = new List<DataTable>();

                DataTable dt = new DataTable();

                if (grdHQ_Promotion.Rows.Count > 0)
                {
                    PrePareDataTable(dt);//Set Column

                    PreParePromotionData(dt); 

                    dt.TableName = "tbl_HQ_Promotion";
                    dtList.Add(dt);
                }

                if (grdHQPromotionMaster.Rows.Count > 0)
                {
                    dt = new DataTable();
                    dt = (DataTable)grdHQPromotionMaster.DataSource;
                    dt.TableName = "tbl_HQ_Promotion_Master";
                    dtList.Add(dt);
                }

                if (grdHQ_Reward.Rows.Count > 0)
                {
                    dt = new DataTable();
                    dt = (DataTable)grdHQ_Reward.DataSource;
                    dt.TableName = "tbl_HQ_Reward";
                    dtList.Add(dt);
                }

                if (grdSkuGroup.Rows.Count > 0)
                {
                    dt = new DataTable();
                    dt = (DataTable)grdSkuGroup.DataSource;
                    dt.TableName = "tbl_HQ_SKUGroup";
                    dtList.Add(dt);
                }

                if (grdSKUGroup_EXC.Rows.Count > 0)
                {
                    dt = new DataTable();
                    dt = (DataTable)grdSKUGroup_EXC.DataSource;
                    dt.TableName = "tbl_HQ_SKUGroup_EXC";
                    dtList.Add(dt);
                }

                string ExcelPath = dir + @"\" + "PromotionExcel" + ".xlsx";
                My_DataTable_Extensions.ExportToExcelR2(dtList, ExcelPath, "PromotionExcel");
            }

           
        }

        public bool ValidateBranchCheck()
        {
            bool ValidateBranch = false;

            if (grdBranch.Rows.Count > 0)
            {
                var dtBranch = (DataTable)grdBranch.DataSource;

                DataRow dr = dtBranch.AsEnumerable().FirstOrDefault(x => x.Field<bool>("ChkBranch") == true && x.Field<bool>("OnlineStatus") == true);

                if (dr != null)
                {
                    ValidateBranch = true;
                }
                else
                {
                    for (int i = 0; i < grdBranch.Rows.Count; i++)
                    {
                        grdBranch.Rows[i].Cells["colChkBranch"].Value = false;
                    }
                }
            }

            return ValidateBranch;
        }

        private void SelectBranchList(List<string> selectList_Branch)
        {
            for (int i = 0; i < grdBranch.Rows.Count; i++)
            {
                bool colChkBranch = Convert.ToBoolean(grdBranch.Rows[i].Cells["colChkBranch"].Value);
                bool colOnlineStatus = Convert.ToBoolean(grdBranch.Rows[i].Cells["colOnlineStatus"].Value);

                if (colChkBranch == true && colOnlineStatus == true)
                {
                    selectList_Branch.Add(grdBranch.Rows[i].Cells["colBranchID"].Value.ToString());
                }
            }
        }

        private void SelectPromotionID_List(List<string> PromotionListID)
        {
            if (grdHQ_Promotion.RowCount > 0)
            {
                for (int i = 0; i < grdHQ_Promotion.RowCount; i++)
                {
                    string PromotionID = grdHQ_Promotion.Rows[i].Cells["colPromotionID_HQPro"].Value.ToString();
                    PromotionListID.Add(PromotionID);
                }
            }
        }

        private void SelectPromotion_Master_List(List<string> PromotionMasterList)
        {
            if (grdHQPromotionMaster.RowCount > 0)
            {
                for (int i = 0; i < grdHQPromotionMaster.RowCount; i++)
                {
                    string PromotionID = grdHQPromotionMaster.Rows[i].Cells["colPromotionID"].Value.ToString();
                    PromotionMasterList.Add(PromotionID);
                }
            }
        }

        private void SelectReward_List(List<string> RewardList)
        {
            if (grdHQ_Reward.RowCount > 0)
            {
                for (int i = 0; i < grdHQ_Reward.RowCount; i++)
                {
                    string RewardID = grdHQ_Reward.Rows[i].Cells["colRewardID"].Value.ToString();
                    RewardList.Add(RewardID);
                }
            }
        }

        private void SelectSKUGroup_List(List<string> SKUGroupID_List,List<string> SKU_ID_List)
        {
            if (grdSkuGroup.RowCount > 0)
            {
                for (int i = 0; i < grdSkuGroup.RowCount; i++)
                {
                    string SKUGroupID = grdSkuGroup.Rows[i].Cells["colSKUGroupID"].Value.ToString();
                    string SKU_ID = grdSkuGroup.Rows[i].Cells["colSKU_ID"].Value.ToString();
                    SKUGroupID_List.Add(SKUGroupID);
                    SKU_ID_List.Add(SKU_ID);
                }
            }
        }

        private void SelectSKUGroupEXC_List(List<string> SKUList)
        {
            if (grdSKUGroup_EXC.RowCount > 0)
            {
                for (int i = 0; i < grdSKUGroup_EXC.RowCount; i++)
                {
                    string SKU_ID = grdSKUGroup_EXC.Rows[i].Cells["colSKU_ID_EXC"].Value.ToString();
                    SKUList.Add(SKU_ID);
                }
            }
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            string msgCheckError = "";

            if (ValidateBranchCheck() == false)
            {
                msgCheckError += "เลือกศูนย์ที่ต้องการส่งข้อมูล !!\n";
            }

            if (!string.IsNullOrEmpty(msgCheckError))
            {
                msgCheckError.ShowWarningMessage();
                return;
            }

            string cfMsg = "ต้องการส่งข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการส่ง!!";

            if (!cfMsg.ConfirmMessageBox(title))
                return;

            bool ret = false;

            Cursor.Current = Cursors.WaitCursor;
            List<string> selectList_Branch = new List<string>();

            SelectBranchList(selectList_Branch);

            var allBranchID = string.Join(",", selectList_Branch);

            List<string> selectList_Promotion = new List<string>();
            SelectPromotionID_List(selectList_Promotion);
            var allPromotionID = string.Join(",", selectList_Promotion);

            List<string> selectList_PromotionMaster = new List<string>();
            SelectPromotion_Master_List(selectList_PromotionMaster);
            var allPromotionMasterID = string.Join(",", selectList_PromotionMaster);

            List<string> selectList_Reward = new List<string>();
            SelectReward_List(selectList_Reward);
            var allRewardID = string.Join(",", selectList_Reward);

            List<string> List_SKUGroupID = new List<string>();
            List<string> List_SKU_ID = new List<string>();
            SelectSKUGroup_List(List_SKUGroupID,List_SKU_ID);
            var allSKUGroupID = string.Join(",", List_SKUGroupID);
            var allSKU_ID = string.Join(",", List_SKU_ID);

            List<string> ListSKU = new List<string>();
            SelectSKUGroupEXC_List(ListSKU);
            var allSKU_EXC = string.Join(",", ListSKU);

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@BranchIDs", allBranchID);
            _params.Add("@PromotionIDs", allPromotionID);
            _params.Add("@PromotionID_Masters", allPromotionMasterID);
            _params.Add("@RewardIDs", allRewardID);
            _params.Add("@SKUGroupIDs", allSKUGroupID);
            _params.Add("@SKU_IDs", allSKU_ID);
            _params.Add("@SKU_ID_EXCs", allSKU_EXC);


            if (chkROP.Checked)
                _params.Add("@FlagRemove", 1);
            else
                _params.Add("@FlagRemove", 0);

            ret = bu.CallSendAllPromotionInfo(_params);

            if (ret == true)
            {
                Cursor.Current = Cursors.Default;
                string msg = "ส่งข้อมูลเรียบร้อยแล้ว!!";
                msg.ShowInfoMessage();

                //btnRefresh_Branch.PerformClick(); //edit by sailom.k 01/08/2022
                //btnSearch.PerformClick();
            }
            else
            {
                Cursor.Current = Cursors.Default;
                string msg = "ส่งข้อมูลล้มเหลว!!";
                msg.ShowErrorMessage();
                return;
            }
        }

        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            chkBoxSelectBranch.Checked = false;
        }

        private void frmPromotionSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        //private void Remove()
        //{
        //    string cfMsg = "คุณแน่ใจหรือไม่ที่จะลบข้อมูลรายการนี้?";
        //    string title = "ทำการยืนยัน!!";

        //    if (!cfMsg.ConfirmMessageBox(title))
        //        return;

        //    string tabName = tabControl1.SelectedTab.Text.ToString();

        //    if (tabName == "HQ_Promotion")
        //    {
                
        //    }
        //    if (tabName == "HQ_Promotion_Master")
        //    {
                
        //    }
        //    else if (tabName == "HQ-Reward")
        //    {
                
        //    }
        //    else if (tabName == "SKU-GROUP")
        //    {
                
        //    }
        //    else if (tabName == "SKU-GROUP-EXC")
        //    {
        //        //Remove_SKUGroupExc();
        //    }
        //}

        //private void Remove_SKUGroupExc()
        //{
        //    int ret = 0;

        //    try
        //    {
        //        var new_HQ_SKUGroupExc = new tbl_HQ_SKUGroup_EXC();

        //        new_HQ_SKUGroupExc.SKU_ID = mtbSKU_ID_Exc.Text;

        //        ret = bu.Remove_SKUGroupEXC(new_HQ_SKUGroupExc);

        //        if (ret == 1)
        //        {
        //            btnSearchSKUGroup_EXC.PerformClick();

        //            string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
        //            msg.ShowInfoMessage();
        //        }
        //        else
        //        {
        //            this.ShowProcessErr();
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ShowErrorMessage();
        //    }
        //}

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //Remove();
        }

        private void btnUpdatePromotion_Click(object sender, EventArgs e)
        {
            bool completeFlag = false;
            var dt = new DataTable();

            Cursor.Current = Cursors.WaitCursor;
            dt = bu.UpdatePromotionFromCenter(chkROP.Checked);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "1")
                {
                    completeFlag = true;
                }
            }

            if (completeFlag)
            {
                Cursor.Current = Cursors.Default;
                string message = "ดึงโปรโมชั่นจาก Center เรียบร้อยแล้ว!!!";
                message.ShowInfoMessage();
            }
            else
            {
                Cursor.Current = Cursors.Default;
                string message = "ไม่สามารถดึงโปรโมชั่นจาก Center ได้!!!";
                message.ShowWarningMessage();
            }
        }
    }
}
