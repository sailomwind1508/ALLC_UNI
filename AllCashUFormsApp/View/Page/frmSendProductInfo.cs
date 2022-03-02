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
    public partial class frmSendProductInfo : Form
    {
        MasterDataControl bu = new MasterDataControl();
        MenuBU menuBU = new MenuBU();
        public frmSendProductInfo()
        {
            InitializeComponent();
        }

        #region #private_method
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
        }

        private void InitialData()
        {
            grdBranch.AutoGenerateColumns = false;
            grdBranch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            grdPro.AutoGenerateColumns = false;
            grdPro.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            btnSendData.Enabled = false;
            btnCancelSend.Enabled = false;

            BindBranchData();
            BindProductData();

            string msg = "สามารถใช้ได้เมื่อต่อ CENTER DB เท่านั้น !!";
            msg.ShowWarningMessage();
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

            Bitmap wifi_Img = new Bitmap(Properties.Resources.wifi); // new Resource Image
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

        private void PrePareDataTable_Product(DataTable newTable)
        {
            newTable.Columns.Add("ChkPro", typeof(bool));
            newTable.Columns.Add("ProductID", typeof(string));
            newTable.Columns.Add("ProductName", typeof(string));
            newTable.Columns.Add("ProductRefCode", typeof(string));
            newTable.Columns.Add("ProductShortName", typeof(string));

            newTable.Columns.Add("ProductGroupID", typeof(int));
            newTable.Columns.Add("ProductGroupName", typeof(string));

            newTable.Columns.Add("ProductSubGroupID", typeof(int));
            newTable.Columns.Add("ProductSubGroupName", typeof(string));

            newTable.Columns.Add("SaleUomID", typeof(int));
            newTable.Columns.Add("ProductUomName", typeof(string));
            newTable.Columns.Add("Seq", typeof(Int16));
            newTable.Columns.Add("FlagDel", typeof(bool));
            newTable.Columns.Add("ProductTypeName", typeof(string));
        }

        private void BindProductData()
        {
            int flagDel = rdoN.Checked ? 0 : 1;

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@flagDel", flagDel);
            _params.Add("@Search", txtSearch.Text);

            DataTable dt = new DataTable();
            dt = bu.GetSendData_Product(_params);

            DataTable newTable = new DataTable();

            PrePareDataTable_Product(newTable);

            foreach (DataRow r in dt.Rows)
            {
                newTable.Rows.Add(false, r["ProductID"], r["ProductName"], r["ProductRefCode"], r["ProductShortName"]
                  , r["ProductGroupID"], r["ProductGroupName"], r["ProductSubGroupID"], r["ProductSubGroupName"]
                  , r["SaleUomID"], r["ProductUomName"], r["Seq"], r["FlagDel"], r["ProductTypeName"]);
            }

            grdPro.DataSource = newTable;
            lbl_qty_Product.Text = newTable.Rows.Count.ToNumberFormat();

            btnSendData.Enabled = false;
            btnCancelSend.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                btnSendData.Enabled = true;
                btnCancelSend.Enabled = true;
            }
        }

        private void PrePare_BranchID(List<string> selectList_Branch)
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

        private void PrePare_Product(List<string> selectList_Product)
        {
            for (int i = 0; i < grdPro.Rows.Count; i++)
            {
                if (Convert.ToBoolean(grdPro.Rows[i].Cells["colChkPro"].Value) == true)
                {
                    selectList_Product.Add(grdPro.Rows[i].Cells["colProductID"].Value.ToString());
                }
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

        public bool ValidatePro_Check()
        {
            bool ValidatePro = false;

            if (grdPro.Rows.Count > 0)
            {
                var dtPro = (DataTable)grdPro.DataSource;

                DataRow dr = dtPro.AsEnumerable().FirstOrDefault(x => x.Field<bool>("ChkPro") == true);

                if (dr != null)
                {
                    ValidatePro = true;
                }
            }

            return ValidatePro;
        }

        #endregion

        #region #event_method

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            string msg = "";

            if (ValidateBranchCheck() == false)
                msg += "เลือกศูนย์ที่ต้องการส่งข้อมูล !!\n";

            if (ValidatePro_Check() == false)
                msg += "เลือกสินค้าที่ต้องการส่งข้อมูล !!\n";

            if (!string.IsNullOrEmpty(msg))
            {
                msg.ShowWarningMessage();
                return;
            }

            string cfMsg = "ต้องการส่งข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการส่ง!!";

            if (!cfMsg.ConfirmMessageBox(title))
                return;

            bool ret = false;

            List<string> selectList_Branch = new List<string>();

            PrePare_BranchID(selectList_Branch);

            var joinString_Branch = string.Join(",", selectList_Branch);

            List<string> selectList_Product = new List<string>();

            PrePare_Product(selectList_Product);

            string joinString_Product = string.Join(",", selectList_Product);

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@BranchIDs", joinString_Branch);
            _params.Add("@ProductIDs", joinString_Product);

            ret = bu.CallSendDataProduct(_params);

            if (ret == true)
            {
                msg = "ส่งข้อมูลเรียบร้อยแล้ว!!";
                msg.ShowInfoMessage();

                BindBranchData();
                BindProductData();
            }
            else
            {
                msg = "ส่งข้อมูลล้มเหลว!!";
                msg.ShowErrorMessage();
            }
        }

        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            BindBranchData();
            BindProductData();

            chkBoxSelectBranch.Checked = false;
            chkBoxSelectPro.Checked = false;
        }

        private void frmSendProductInfo_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindProductData();
        }

        private void chkBoxSelectPro_Click(object sender, EventArgs e)
        {
            if (grdPro.Rows.Count > 0)
            {
                for (int i = 0; i < grdPro.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(grdPro.Rows[i].Cells["colChkPro"].Value) == false)
                        grdPro.Rows[i].Cells["colChkPro"].Value = true;
                    else
                        grdPro.Rows[i].Cells["colChkPro"].Value = false;
                }
            }
            else
            {
                chkBoxSelectPro.Checked = false;
            }
        }

        private void chkBoxSelectBranch_Click(object sender, EventArgs e)
        {
            if (grdBranch.Rows.Count > 0)
            {
                for (int i = 0; i < grdBranch.Rows.Count; i++)
                {
                    bool OnlineStatus = Convert.ToBoolean(grdBranch.Rows[i].Cells["colOnlineStatus"].Value);
                    if (Convert.ToBoolean(grdBranch.Rows[i].Cells["colChkBranch"].Value) == false && OnlineStatus == true)
                        grdBranch.Rows[i].Cells["colChkBranch"].Value = true;
                    else
                        grdBranch.Rows[i].Cells["colChkBranch"].Value = false;
                }
            }
            else
            {
                chkBoxSelectBranch.Checked = false;
            }
        }

        private void grdBranch_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdBranch.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdPro_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPro.SetRowPostPaint(sender, e, this.Font);
        }

        private void frmSendProductInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindProductData();
            }
        }

        private void btnSearchBranch_Click(object sender, EventArgs e)
        {
            BindBranchData();
        }

        #endregion
    }
}
