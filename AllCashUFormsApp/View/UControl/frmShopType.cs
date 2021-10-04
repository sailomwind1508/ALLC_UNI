using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.Page;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmShopType : Form
    {
        ShopType bu = new ShopType();
        List<string> OpenPanelSearchControls = new List<string>();
        Dictionary<Control, Label> ValidateSaveCtrls = new Dictionary<Control, Label>(); // Validate Save
        public frmShopType()
        {
            InitializeComponent();
            OpenPanelSearchControls = new string[] { txtSearch.Name }.ToList();
            ValidateSaveCtrls.Add(txtShopTypeCode, lblID);
            ValidateSaveCtrls.Add(txtShopTypeName, lblName);
        }
        private void PrePare_cbbShopTypeGroup()
        {
            var ShopTypeGroup = bu.GetShopTypeGroup(x => x.FlagDel == false);
            cbbShopTypeGroup.BindDropdownList(ShopTypeGroup, "ShopTypeGroupName", "ShopTypeGroupID");
        }
        private void DisablePanelEdit(bool flagDisable)
        {
            txtShopTypeCode.DisableTextBox(flagDisable);
            txtShopTypeName.DisableTextBox(flagDisable);
            cbbShopTypeGroup.Enabled = !flagDisable;
        }
        private void InitialData()
        {
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdList.AutoGenerateColumns = false;

            DisablePanelEdit(true);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            pnlStatus.Visible = false;

            PrePare_cbbShopTypeGroup();

            BindData();
        }
        private void BindData()
        {
            txtShopTypeCode.Clear();
            txtShopTypeName.Clear();
            
            int flagDel = rdoN.Checked ? 0 : 1;

            DataTable dt = new DataTable();

            dt = bu.GetShopTypeGridData(flagDel,txtSearch.Text);

            grdList.DataSource = dt;

            lbl_Qty.Text = dt.Rows.Count.ToNumberFormat();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            pnlStatus.Visible = false;
            btnEdit.Enabled = false;
            btnRemove.Enabled = false;

            if (rdoN.Checked && dt.Rows.Count > 0)
            {
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
            }
            else if (rdoC.Checked && dt.Rows.Count > 0)
            {
                btnAdd.Enabled = false;
                pnlStatus.Visible = true;
            }
            else if (rdoC.Checked && dt.Rows.Count == 0)
            {
                btnAdd.Enabled = false;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindData();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmShopType_Load(object sender, EventArgs e)
        {
            InitialData();
        }
        private void rdoN_CheckedChanged(object sender, EventArgs e)
        {
            BindData();
        }
        private void rdoC_CheckedChanged(object sender, EventArgs e)
        {
            BindData();
        }
        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }
        private void SelectDetails(DataGridViewCellEventArgs e)
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
                        grdRows = grdList.Rows[e.RowIndex];
                    }
                }
                else
                {
                    grdRows = grdList.CurrentRow;
                }

                if (grdRows != null)
                {
                    txtShopTypeCode.Text = grdRows.Cells["colShopTypeID"].Value.ToString();
                    txtShopTypeName.Text = grdRows.Cells["colShopTypeName"].Value.ToString();
                    cbbShopTypeGroup.SelectedValue = Convert.ToInt32(grdRows.Cells["colShopTypeGroupID"].Value);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
        private void grdList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectDetails(e);
        }
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            SelectDetails(null);
        }
        private void grdList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                else
                {
                    string colShopTypeID = grdList.Rows[e.RowIndex].Cells["colShopTypeID"].Value.ToString();

                    if (!string.IsNullOrEmpty(colShopTypeID))
                    {
                        frmCustomerInfo.shoptypeID = colShopTypeID;
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
        private void PrePareRunningNo()
        {
            var ShopType = bu.GetShopTypeALL();

            if (ShopType.Count > 0)
            {
                int ShopTypeCode = Convert.ToInt32(ShopType.Select(x => x.ShopTypeCode).Max()) + 1;

                txtShopTypeCode.Text = ShopTypeCode.ToString();
            }
            else
            {
                txtShopTypeCode.Text = "1";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");

            txtShopTypeCode.Clear();
            txtShopTypeName.Clear();

            grdList.Enabled = false;

            pnlSearch.OpenControl(false, OpenPanelSearchControls.ToArray());

            DisablePanelEdit(false);
            txtShopTypeCode.DisableTextBox(true);
            txtShopTypeCode.Focus();
            PrePareRunningNo();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            pnlSearch.OpenControl(false, OpenPanelSearchControls.ToArray());
            txtShopTypeCode.DisableTextBox(true);
            txtShopTypeName.DisableTextBox(false);
            txtShopTypeName.Focus();
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
        }
        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //true
            {
                errList.SetErrMessage(ValidateSaveCtrls);
            }
            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }
            return ret;
        }
        private void Save()
        {
            if (!ValidateSave())
            {
                return;
            }

            try
            {
                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;

                var ShopType = new tbl_ShopType();
                var ShopTypeALL = bu.GetShopTypeALL();
                var ShopTypeList = ShopTypeALL.FirstOrDefault(x => x.ShopTypeID == Convert.ToInt32(txtShopTypeCode.Text));

                if (ShopTypeList != null)
                {
                    ShopType = ShopTypeList;
                    ShopType.ShopTypeName = txtShopTypeName.Text;
                    ShopType.EdDate = DateTime.Now;
                    ShopType.EdUser = Helper.tbl_Users.Username;
                }
                else
                {
                    if(ShopTypeALL.Count > 0)
                    {
                        ShopType.ShopTypeID = ShopTypeALL.Select(x=>x.ShopTypeID).Max() + 1;
                    }
                    else
                    {
                        ShopType.ShopTypeID = 1;
                    }

                    ShopType.ShopTypeCode = txtShopTypeCode.Text;
                    ShopType.ShopTypeName = txtShopTypeName.Text;
                    ShopType.ShopTypeGroupID = Convert.ToInt32(cbbShopTypeGroup.SelectedValue);

                    ShopType.CrDate = DateTime.Now;
                    ShopType.CrUser = Helper.tbl_Users.Username;

                    ShopType.EdDate = null;
                    ShopType.EdUser = null;

                    ShopType.FlagSend = false;
                    ShopType.FlagDel = false;
                }

                ret = bu.UpdateData(ShopType);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    pnlSearch.OpenControl(true, OpenPanelSearchControls.ToArray());

                    txtShopTypeCode.Clear();
                    txtShopTypeName.Clear();

                    DisablePanelEdit(true);

                    txtSearch.DisableTextBox(false);
                    grdList.Enabled = true;

                    BindData();
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Remove(bool flagRemove = true)
        {
            try
            {
                if (flagRemove == true)
                {
                    string cfMsg = "ต้องการลบข้อมูลใช่หรือไม่?";
                    string title = "ยืนยันการลบ!!";
                    if (!cfMsg.ConfirmMessageBox(title))
                        return;
                }
            
                int ret = 0;

                var ShopTypeList = bu.GetShopTypeALL(x=>x.ShopTypeID == Convert.ToInt32(txtShopTypeCode.Text)); // new

                if (ShopTypeList.Count > 0)
                {
                    var ShopType = new tbl_ShopType();
                    ShopType = ShopTypeList[0];

                    ShopType.EdDate = DateTime.Now;
                    ShopType.EdUser = Helper.tbl_Users.Username;

                    if (flagRemove == true)
                    {
                        ShopType.FlagDel = true;
                    }
                    else
                    {
                        ShopType.FlagDel = false;
                    }

                    ret = bu.UpdateData(ShopType);

                    if (ret == 1)
                    {
                        string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();

                        txtShopTypeCode.DisableTextBox(true);
                        txtShopTypeName.DisableTextBox(true);

                        pnlSearch.OpenControl(true, OpenPanelSearchControls.ToArray());
                        txtSearch.DisableTextBox(false);

                        if (flagRemove == true)
                        {
                            btnSearch.PerformClick();
                        }
                        else
                        {
                            rdoC.Checked = false; //
                            rdoC.Checked = true; //
                        }
                    }
                    else
                    {
                        this.ShowProcessErr();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            Remove();
        }
        private void btnNormal_Click(object sender, EventArgs e)
        {
            Remove(false);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlSearch.OpenControl(true, OpenPanelSearchControls.ToArray());
            txtSearch.DisableTextBox(false);

            txtShopTypeCode.Clear();
            txtShopTypeName.Clear();

            txtSearch.Focus();

            DisablePanelEdit(true);

            grdList.Enabled = true;
            BindData();
        }
    }
}
