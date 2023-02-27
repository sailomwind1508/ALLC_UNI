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
    public partial class frmPriceGroup : Form
    {
        PriceGroup bu = new PriceGroup();
        List<string> OpenPanelSearchControls = new List<string>();
        Dictionary<Control, Label> ValidateSaveCtrls = new Dictionary<Control, Label>();
        public frmPriceGroup()
        {
            InitializeComponent();
            OpenPanelSearchControls = new string[] { txtSearch.Name }.ToList();
            ValidateSaveCtrls.Add(txtPriceGroupCode, lblID);
            ValidateSaveCtrls.Add(txtPriceGroupName, lblName);
        }
        private void InitialData()
        {
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdList.AutoGenerateColumns = false;

            txtPriceGroupCode.DisableTextBox(true);
            txtPriceGroupName.DisableTextBox(true);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            pnlStatus.Visible = false;

            BindData();
        }
        private void BindData()
        {
            txtPriceGroupCode.Clear();
            txtPriceGroupName.Clear();

            DataTable dt = new DataTable();
            int flagDel = rdoN.Checked ? 0 : 1;
            dt = bu.GetPriceGroupGridData(txtSearch.Text, flagDel);

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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmPriceGroup_Load(object sender, EventArgs e)
        {
            InitialData();
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
                    txtPriceGroupCode.Text = grdRows.Cells["colPriceGroupCode"].Value.ToString();
                    txtPriceGroupName.Text = grdRows.Cells["colPriceGroupName"].Value.ToString();
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
        private void PrePareRunningNo()
        {
            var PriceGroup = bu.GetPriceGroup();

            if (PriceGroup.Count > 0)
            {
                int PriceGroupCode = Convert.ToInt32(PriceGroup.Select(x => x.PriceGroupCode).Max()) + 1;

                txtPriceGroupCode.Text = PriceGroupCode.ToString("00");
            }
            else
            {
                txtPriceGroupCode.Text = "00";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");

            txtPriceGroupCode.Clear();
            txtPriceGroupName.Clear();

            grdList.Enabled = false;

            pnlSearch.OpenControl(false, OpenPanelSearchControls.ToArray());

            
            txtPriceGroupCode.DisableTextBox(true);
            txtPriceGroupName.DisableTextBox(false);

            txtPriceGroupName.Focus();

            PrePareRunningNo();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            pnlSearch.OpenControl(false, OpenPanelSearchControls.ToArray());
            txtPriceGroupCode.DisableTextBox(true);
            txtPriceGroupName.DisableTextBox(false);
            txtPriceGroupName.Focus();
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
            try
            {
                if (!ValidateSave())
                {
                    return;
                }

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;

                var PriceGroup = new tbl_PriceGroup();
                var PriceGroupALL = bu.GetPriceGroup();
                var PriceGroupList = PriceGroupALL.FirstOrDefault(x => x.PriceGroupCode == txtPriceGroupCode.Text);

                if (PriceGroupList != null)
                {
                    PriceGroup = PriceGroupList;
                    PriceGroup.PriceGroupName = txtPriceGroupName.Text;
                    PriceGroup.EdDate = DateTime.Now;
                    PriceGroup.EdUser = Helper.tbl_Users.Username;
                }
                else
                {
                    PriceGroup.PriceGroupCode = txtPriceGroupCode.Text;
                    PriceGroup.PriceGroupName = txtPriceGroupName.Text;

                    PriceGroup.CrDate = DateTime.Now;
                    PriceGroup.CrUser = Helper.tbl_Users.Username;

                    PriceGroup.EdDate = null;
                    PriceGroup.EdUser = null;
                 
                    PriceGroup.FlagDel = false;
                    PriceGroup.FlagSend = false;

                    var branch = bu.GetBranch();
                    if (branch != null)
                    {
                        PriceGroup.BranchID = branch.First().BranchID;
                    }
                    else
                    {
                        PriceGroup.BranchID = null;
                    }

                    PriceGroup.StartDate = null;
                    PriceGroup.EndDate = null;
                }

                ret = bu.UpdateData(PriceGroup);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    pnlSearch.OpenControl(true, OpenPanelSearchControls.ToArray());

                    txtPriceGroupCode.Clear();
                    txtPriceGroupName.Clear();

                    txtPriceGroupCode.DisableTextBox(true);
                    txtPriceGroupName.DisableTextBox(true);

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

                var PriceGroupList = bu.GetPriceGroup(x => x.PriceGroupCode == txtPriceGroupCode.Text); 

                if (PriceGroupList.Count > 0)
                {
                    var PriceGroup = new tbl_PriceGroup();
                    PriceGroup = PriceGroupList[0];

                    PriceGroup.EdDate = DateTime.Now;
                    PriceGroup.EdUser = Helper.tbl_Users.Username;

                    if (flagRemove == true)
                    {
                        PriceGroup.FlagDel = true;
                    }
                    else
                    {
                        PriceGroup.FlagDel = false;
                    }

                    ret = bu.UpdateData(PriceGroup);

                    if (ret == 1)
                    {
                        string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();

                        txtPriceGroupCode.DisableTextBox(true);
                        txtPriceGroupName.DisableTextBox(true);

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

            txtPriceGroupCode.Clear();
            txtPriceGroupName.Clear();
            txtSearch.Focus();
            txtPriceGroupCode.DisableTextBox(true);
            txtPriceGroupName.DisableTextBox(true);

            grdList.Enabled = true;
            BindData();
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
                    string PriceGroupID = grdList.Rows[e.RowIndex].Cells["colPriceGroupID"].Value.ToString();

                    if (!string.IsNullOrEmpty(PriceGroupID))
                    {
                        frmCustomerInfo.pricegroupID = PriceGroupID;
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            SelectDetails(null);
        }
    }
}
