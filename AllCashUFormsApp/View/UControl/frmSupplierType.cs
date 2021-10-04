using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.Page;
namespace AllCashUFormsApp.View.UControl
{
    public partial class frmSupplierType : Form
    {
        MasterDataControl bu = new MasterDataControl();
        List<string> OpenPanelSearchControls = new List<string>();
        List<string> OpenPanelEditControls = new List<string>();
        Dictionary<Control, Label> ValidateSaveCtrls = new Dictionary<Control, Label>(); // Validate Save
        public frmSupplierType()
        {
            InitializeComponent();
            OpenPanelSearchControls = new string[] { txtSearch.Name }.ToList();
            OpenPanelEditControls = new string[] { txtApSupplierTypeCode.Name }.ToList();
            ValidateSaveCtrls.Add(txtApSupplierTypeCode, lbl_Code);
            ValidateSaveCtrls.Add(txtApSupplierTypeName, lbl_Name);
        }
        private void InitialData()
        {
            grdApSupplierType.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdApSupplierType.AutoGenerateColumns = false;

            pnlEdit.OpenControl(false, OpenPanelEditControls.ToArray());
        }
        private void BindData()
        {
            ClearPnlEdit();

            DataTable dt = new DataTable();
            int flagDel = rdoN.Checked ? 0 : 1;
            dt = bu.GetApSupplierTypeData(flagDel, txtSearch.Text);
            grdApSupplierType.DataSource = dt;
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
        private void frmSearchSupplierType_Load(object sender, EventArgs e)
        {
            InitialData();
            BindData();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void grdApSupplierType_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdApSupplierType.SetRowPostPaint(sender, e, this.Font);
        }
        private void SelectSupplierTypeDetails(DataGridViewCellEventArgs e)
        {
            DataGridViewRow gridViewRow = null;

            if (e != null)
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                else
                {
                    gridViewRow = grdApSupplierType.Rows[e.RowIndex];
                }
            }
            else
            {
                gridViewRow = grdApSupplierType.CurrentRow;
            }

            if (gridViewRow != null)
            {
                txtApSupplierTypeCode.Text = gridViewRow.Cells["colApSupplierTypeCode"].Value.ToString();
                txtApSupplierTypeName.Text = gridViewRow.Cells["colApSupplierTypeName"].Value.ToString();
            }
        }
        private void grdApSupplierType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectSupplierTypeDetails(e);
        }
        private void grdApSupplierType_SelectionChanged(object sender, EventArgs e)
        {
            SelectSupplierTypeDetails(null);
        }
        private void PrePareRunningNo()
        {
            var ApSupplierType = bu.GetSupplierType();

            if (ApSupplierType.Count > 0)
            {
                int Max_SupplierCode = Convert.ToInt32(ApSupplierType.Select(x => x.ApSupplierTypeCode).Max()) + 1;

                txtApSupplierTypeCode.Text = Max_SupplierCode.ToString("00");
            }
            else
            {
                txtApSupplierTypeCode.Text = "01";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
            ClearPnlEdit();
            grdApSupplierType.Enabled = false;
            pnlSearch.OpenControl(false,OpenPanelSearchControls.ToArray());
            pnlEdit.OpenControl(true, OpenPanelEditControls.ToArray());

            PrePareRunningNo();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            pnlSearch.OpenControl(false, OpenPanelSearchControls.ToArray());
            pnlEdit.OpenControl(true, OpenPanelEditControls.ToArray());
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
        }
        private void ClearPnlEdit()
        {
            txtApSupplierTypeCode.Clear();
            txtApSupplierTypeName.Clear();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPnlEdit();
            pnlSearch.OpenControl(true, OpenPanelSearchControls.ToArray());
            txtSearch.DisableTextBox(false);
            pnlEdit.OpenControl(false, OpenPanelEditControls.ToArray());
            grdApSupplierType.Enabled = true;
            BindData();
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

                var ApSupplierTypeAll = bu.GetSupplierType();
                var ApSupplierType = ApSupplierTypeAll.FirstOrDefault(x => x.ApSupplierTypeCode == txtApSupplierTypeCode.Text);
                var SupplierType = new tbl_ApSupplierType();

                if (ApSupplierType != null)
                {
                    SupplierType = ApSupplierType;
                    SupplierType.ApSupplierTypeCode = txtApSupplierTypeCode.Text;
                    SupplierType.ApSupplierTypeName = txtApSupplierTypeName.Text;
                    SupplierType.EdDate = DateTime.Now;
                    SupplierType.EdUser = Helper.tbl_Users.Username;
                }
                else
                {
                    if (ApSupplierTypeAll.Count > 0)
                    {
                        SupplierType.APSupplierTypeID = ApSupplierTypeAll.Select(x => x.APSupplierTypeID).Max() + 1;
                    }
                    else
                    {
                        SupplierType.APSupplierTypeID = 1;
                    }

                    SupplierType.ApSupplierTypeCode = txtApSupplierTypeCode.Text;
                    SupplierType.ApSupplierTypeName = txtApSupplierTypeName.Text;

                    SupplierType.CrDate = DateTime.Now;
                    SupplierType.CrUser = Helper.tbl_Users.Username;

                    SupplierType.EdDate = null;
                    SupplierType.EdUser = null;

                    SupplierType.FlagDel = false;
                    SupplierType.FlagSend = false;
                }

                ret = bu.UpdateApSupplierTypeData(SupplierType);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    pnlSearch.OpenControl(true, OpenPanelSearchControls.ToArray());
                    pnlEdit.ClearControl();
                    pnlEdit.OpenControl(false, OpenPanelEditControls.ToArray());

                    txtSearch.DisableTextBox(false);
                    grdApSupplierType.Enabled = true;

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
                    string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
                    string title = "ทำการยืนยัน!!";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;
                }
              
                int ret = 0;

                var ApSupplierType = bu.GetSupplierType(x=>x.ApSupplierTypeCode == txtApSupplierTypeCode.Text);
                var SupplierType = new tbl_ApSupplierType();

                if (ApSupplierType.Count > 0)
                {
                    SupplierType = ApSupplierType[0];

                    SupplierType.EdDate = DateTime.Now;
                    SupplierType.EdUser = Helper.tbl_Users.Username;

                    if (flagRemove == true)
                    {
                        SupplierType.FlagDel = true;
                    }
                    else
                    {
                        SupplierType.FlagDel = false;
                    }

                    ret = bu.UpdateApSupplierTypeData(SupplierType);
                }
                
                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    if (flagRemove == true)
                    {
                        pnlEdit.OpenControl(false, OpenPanelEditControls.ToArray());

                        pnlSearch.OpenControl(true, OpenPanelSearchControls.ToArray());
                        txtSearch.DisableTextBox(false);

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
        private void grdApSupplierType_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            else
            {
                if (rdoN.Checked == true)
                {
                    string SupplierTypeID = grdApSupplierType.Rows[e.RowIndex].Cells["colAPSupplierTypeID"].Value.ToString();
                    frmSupplierInfo.SupplierTypeID = SupplierTypeID;
                    this.Close();
                }
            }
        }
    }
}
