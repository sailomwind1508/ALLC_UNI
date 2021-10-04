using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;//
using AllCashUFormsApp.Model;//
using AllCashUFormsApp.View.Page;//
namespace AllCashUFormsApp.View.UControl.A_UC
{
    public partial class frmSearchPriceGroup : Form
    {
        List<Control> searchBranchControls = new List<Control>();
        PriceGroup bu = new PriceGroup();
        Dictionary<Control, Label> validatePriceGroupCtrls = new Dictionary<Control, Label>(); // Validate Save
        List<string> readOnlyPnlGridControls = new List<string>();
        List<string> readOnlyPnlEditControls = new List<string>();
        public frmSearchPriceGroup()
        {
            InitializeComponent();
            searchBranchControls = new List<Control>() { txtBranchCode};
            validatePriceGroupCtrls.Add(txtPriceGroupName, lblName);//
            readOnlyPnlGridControls = new string[] { txtSearch.Name }.ToList();
            readOnlyPnlEditControls = new string[] { txtPriceGroupCode.Name,txtPriceGroupName.Name }.ToList();
        }
        private void BindData()
        {
            DataTable dt = new DataTable();
            int flagDel = rdoN.Checked ? 0 : 1;
            
            string searchtext = "";
            if(txtSearch.Text != "")
            {
                searchtext = txtSearch.Text;
            }

            dt = bu.GetPriceGroupGridData(searchtext,flagDel);
             

            DataTable _dt = new DataTable(); //คัดเฉพาะที่โชว์ใน GridView
            _dt.Columns.Add("PriceGroupID", typeof(int));
            _dt.Columns.Add("PriceGroupCode",typeof(string));
            _dt.Columns.Add("PriceGroupName", typeof(string));
            _dt.Columns.Add("CrDate", typeof(string));
            _dt.Columns.Add("CrUser", typeof(string));
            _dt.Columns.Add("EdDate", typeof(string));
            _dt.Columns.Add("EdUser", typeof(string));
            _dt.Columns.Add("FlagSend", typeof(bool));
            _dt.Columns.Add("BranchID", typeof(string));
            foreach (DataRow item in dt.Rows)
            {
                _dt.Rows.Add(item["PriceGroupID"].ToString(),
                item["PriceGroupCode"].ToString(),
                item["PriceGroupName"].ToString(),
                item["CrDate"].ToString(),
                item["CrUser"].ToString(),
                item["EdDate"].ToString(),
                item["EdUser"].ToString(),
                item["FlagSend"].ToString(),
                item["BranchID"].ToString());
            }
            gridPriceGroup.DataSource = _dt;
            gridPriceGroup.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lblPriceGroupCount.Text = _dt.Rows.Count.ToNumberFormat();
        }
        public void hideTextbox()
        {
            txtPriceGroupCode.DisableTextBox(true);
            txtPriceGroupName.DisableTextBox(true);
        }
        private void btnSearchPriceGroup_Click(object sender, EventArgs e)
        {
            BindData();
        }
        private void gridPriceGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(rdoC.Checked == true)
            {
                btnEdit.Enabled = false;
            }
            else if (rdoN.Checked == true)
            {
                btnEdit.Enabled = true;
            }
            btnRemove.Enabled = true;
            try
               {
                    if (e.RowIndex == -1) return;
                    else
                    {
                    DataGridViewRow gridViewRow = gridPriceGroup.Rows[e.RowIndex];
                    txtPriceGroupCode.Text = gridViewRow.Cells["colPriceGroupCode"].Value.ToString();
                    txtPriceGroupName.Text = gridViewRow.Cells["colPriceGroupName"].Value.ToString();
                    txtPriceGroupID.Text = gridViewRow.Cells["colPriceGroupID"].Value.ToString();
                    txtBranchCode.Text = gridViewRow.Cells["colBranchID"].Value.ToString();
                    }
               }
            catch (Exception ex)
               {
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void gridPriceGroup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1) return;
                else
                {
                    DataGridViewRow gridViewRow = gridPriceGroup.Rows[e.RowIndex];
                    frmCustomerInfo.pricegroupID = gridViewRow.Cells["colPriceGroupID"].Value.ToString();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

       
        private void BindBranch()
        {
            var b = bu.GetBranch();
            if (b != null)
            {
                this.BindData("FromBranchID", searchBranchControls, b[0].BranchID);
            }
        }
        private void frmSearchPriceGroup_Load(object sender, EventArgs e)
        {
            BindData();
            hideTextbox();
            
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnEdit.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BindBranch();
            pnlGridView.OpenControl(false, readOnlyPnlGridControls.ToArray());
            pnlEdit.Enabled = true;
            gridPriceGroup.Enabled = false;
            txtPriceGroupCode.DisableTextBox(true);
            txtPriceGroupName.Clear();
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            txtPriceGroupName.DisableTextBox(false);
            txtPriceGroupName.Focus();

            var allPriceGroup = bu.GetPriceGroup();
            
            if (allPriceGroup != null && allPriceGroup.Count > 0)
            {
                string autoID = "";
                string _pricegroupCode = allPriceGroup.Max(x => x.PriceGroupCode);
                var pricegroupCode = Convert.ToInt32(_pricegroupCode) + 1;
                for (int i = 0; i < 2; i++)
                {
                    autoID += "0";
                }
                txtPriceGroupCode.Text = pricegroupCode.ToString(autoID);
                int _pricegroupID = allPriceGroup.Max(x => x.PriceGroupID);
                var pricegroupID = _pricegroupID + 1;
                txtPriceGroupID.Text = pricegroupID.ToString();
            }
            else
            {
                txtPriceGroupID.Text = "1";
                txtPriceGroupCode.Text = "00";
            }
           
        }
        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //true
            {
                errList.SetErrMessage(validatePriceGroupCtrls);
            }
            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }
            return ret;
        }
        private void SavePriceGroup()
        {
            try
            {
                bu = new PriceGroup();

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;
                int pricegroupID = Convert.ToInt32(txtPriceGroupID.Text);
                bool isEditMode = bu.CheckExistsPriceGroup(pricegroupID);

                if (isEditMode)
                {
                    List<tbl_PriceGroup> priceGroupList = bu.GetPriceGroup(pricegroupID);
                    priceGroupList.ForEach(x =>
                    {
                        x.PriceGroupName = txtPriceGroupName.Text;
                        x.EdDate = DateTime.Now;
                        x.EdUser = Helper.tbl_Users.Username;
                    });
                    bu.tbl_PriceGroups.AddRange(priceGroupList);
                    ret = bu.UpdateData();
                }
                else
                {
                    tbl_PriceGroup pgData = new tbl_PriceGroup(); //Model
                    pnlEdit.Controls.SetObjectFromControl(pgData);

                    pgData.EdDate = null;
                    pgData.EdUser = null;
                    pgData.CrUser = Helper.tbl_Users.Username;
                    pgData.CrDate = DateTime.Now;

                    pgData.BranchID = txtBranchCode.Text;

                    pgData.StartDate = DateTime.Now;
                    pgData.EndDate = DateTime.Now;

                    pgData.FlagSend = false;
                    if(rdoN.Checked == true)
                    {
                        pgData.FlagDel = false;
                    }
                    else if(rdoC.Checked == true)
                    {
                        pgData.FlagDel = true;
                    }
                    PriceGroup p = new PriceGroup();
                    ret = p.UpdateData(pgData);
                }
                if(ret == 1)
                {
                    pnlEdit.OpenControl(false, readOnlyPnlEditControls.ToArray());
                    pnlGridView.OpenControl(true, readOnlyPnlGridControls.ToArray());
                    gridPriceGroup.Enabled = true;
                    pnlEdit.Enabled = false;
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                    BindData();
                    pnlEdit.ClearControl();

                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnAdd.Enabled = true;
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }

            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ValidateSave();
            SavePriceGroup();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlGridView.OpenControl(true, readOnlyPnlGridControls.ToArray());
            pnlEdit.OpenControl(false, readOnlyPnlEditControls.ToArray());
            gridPriceGroup.Enabled = true;

            txtPriceGroupName.DisableTextBox(true);

            pnlEdit.ClearControl();
            
            DisableButton();
            btnAdd.Enabled = true;

            btnSearchPriceGroup.Enabled = true;

            txtSearch.DisableTextBox(false);
            rdoC.Enabled = true;
            rdoN.Enabled = true;
            txtSearch.Focus();
        }
        private void DisableButton()
        {
            btnAdd.Enabled = false;
            btnSave.Enabled = false;
            btnEdit.Enabled = false;
            btnCancel.Enabled = false;
            btnCopy.Enabled = false;
            btnPrint.Enabled = false;
            btnRemove.Enabled = false;
        }
       
        private void btnEdit_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            gridPriceGroup.Enabled = false;
            pnlGridView.OpenControl(false, readOnlyPnlGridControls.ToArray());

            pnlEdit.Enabled = true;
            txtPriceGroupName.DisableTextBox(false);
            txtPriceGroupName.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdoC_CheckedChanged(object sender, EventArgs e)
        {
            pnlEdit.ClearControl();
            lblStatus.Visible = true;
            chkNormal.Visible = true;
            chkNormal.Enabled = false;
            chkNormal.Checked = true;
            btnNormal.Visible = true;
            BindData();
        }
       private void EditMode()
        {
            try
            {
                if (txtPriceGroupID.Text != "")
                {
                    int ret = 0;

                    bool isEditMode = bu.CheckExistsPriceGroup(Convert.ToInt32(txtPriceGroupID.Text));


                    if (isEditMode)
                    {
                        int pricegroupID = Convert.ToInt32(txtPriceGroupID.Text);
                        List<tbl_PriceGroup> pricegroupList = bu.GetPriceGroup(pricegroupID);
                        pricegroupList.ForEach(x =>
                        {
                            x.FlagDel = false;
                            x.EdDate = DateTime.Now;
                            x.EdUser = Helper.tbl_Users.Username;
                        });
                        bu.tbl_PriceGroups.AddRange(pricegroupList);
                        ret = bu.UpdateData();
                    }
                    else
                    {
                        return;
                    }
                    if (ret == 1)
                    {
                        pnlEdit.OpenControl(false, readOnlyPnlEditControls.ToArray());
                        pnlGridView.OpenControl(true, readOnlyPnlGridControls.ToArray());
                        gridPriceGroup.Enabled = true;
                        btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                        btnNormal.Enabled = true;
                        BindData();
                        pnlEdit.ClearControl();
                    }
                    else
                    {
                        this.ShowProcessErr();
                        return;
                    }

                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }
        private void btnNormal_Click(object sender, EventArgs e)
        {
            EditMode();
        }

        private void rdoN_CheckedChanged(object sender, EventArgs e)
        {
            pnlEdit.ClearControl();
            lblStatus.Visible = false;
            btnNormal.Visible = false;
            chkNormal.Visible = false;
            BindData();
        }
        private void RemovePriceGroup(string pricegroupID)
        {
            try
            {
                int ret = 0;

                string cfMsg = "ต้องการลบข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการลบ!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                bu = new PriceGroup();
                var PriceGroupList = bu.GetPriceGroup(x => x.PriceGroupID.ToString() == pricegroupID);
                if(PriceGroupList.Count != 0)
                {
                    tbl_PriceGroup pgData = new tbl_PriceGroup();
                    pgData = PriceGroupList[0];
                    pgData.FlagDel = true;
                    pgData.EdDate = DateTime.Now;
                    pgData.EdUser = Helper.tbl_Users.Username;
                    ret = bu.UpdateData(pgData);
                }
                if (ret == 1)
                {
                    string msg = "ลบข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

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
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            int rowIndex = gridPriceGroup.CurrentCell.RowIndex;
            if (rowIndex != -1)
            {
                var ID = gridPriceGroup.Rows[rowIndex].Cells["colPriceGroupID"].Value.ToString();
                RemovePriceGroup(ID);
                pnlEdit.ClearControl();
            }

        }

        private void gridPriceGroup_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            gridPriceGroup.SetRowPostPaint(sender, e, this.Font);
        }
    }
}