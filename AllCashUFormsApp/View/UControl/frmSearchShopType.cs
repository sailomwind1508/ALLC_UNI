using AllCashUFormsApp.Controller;
using AllCashUFormsApp.View.Page;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Model;
namespace AllCashUFormsApp.View.UControl
{
    public partial class frmSearchShopType : Form
    {
        ShopType bu = new ShopType();
        Dictionary<Control, Label> validateShopTypeCtrls = new Dictionary<Control, Label>();
        List<string> readOnlyPnlGridControls = new List<string>();
        List<string> readOnlyPnlEditControls = new List<string>();
        public frmSearchShopType()
        {
            InitializeComponent();
            validateShopTypeCtrls.Add(txtShopTypeName, lblName);
            readOnlyPnlEditControls = new string[] { txtShopTypeName.Name }.ToList();
            readOnlyPnlGridControls = new string[] { txtSearch.Name }.ToList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DataSet ds = new DataSet();
        private void BindShopTypeData()
        {
            int flagDel = rdoN.Checked ? 0 : 1;

            string searchtext="";

          

            if (txtSearch.Text != "")
            {
                searchtext = txtSearch.Text;
            }
           

            DataTable _dt = new DataTable();
           
             _dt = bu.GetShopTypeGridData(flagDel,searchtext);

            DataTable dt = new DataTable();
            dt.Columns.Add("ShopTypeID", typeof(int));
            dt.Columns.Add("ShopTypeName", typeof(string));
            dt.Columns.Add("CrDate", typeof(string));
            dt.Columns.Add("CrUser", typeof(string));
            dt.Columns.Add("EdDate", typeof(string));
            dt.Columns.Add("EdUser", typeof(string)); 
            dt.Columns.Add("FlagDel", typeof(bool));

            foreach (DataRow r in _dt.Rows)
            {
                dt.Rows.Add(r["ShopTypeID"].ToString(), r["ShopTypeName"].ToString(), r["CrDate"].ToString(), r["CrUser"].ToString(), r["EdDate"].ToString(), r["EdUser"].ToString(), r["FlagDel"].ToString());
            }
            gridShopType.DataSource = dt;
            gridShopType.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnSearchShopType_Click(object sender, EventArgs e)
        {
            BindShopTypeData();
        }

        private void gridShopType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
                if (e.RowIndex == -1)
                    return;
                DataGridViewRow gridrow = gridShopType.Rows[e.RowIndex];
                txtShopTypeID.Text = gridrow.Cells["colShopTypeID"].Value.ToString();
                txtShopTypeName.Text = gridrow.Cells["colShopTypeName"].Value.ToString();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void gridShopType_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                    return;
                DataGridViewRow gridrow = gridShopType.Rows[e.RowIndex];
                string cells = gridrow.Cells["colShopTypeID"].Value.ToString();
                if(cells != null)
                {
                    frmCustomerInfo.shoptypeID = cells;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void rdoFlagDel0_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rdoFlagDel1_CheckedChanged(object sender, EventArgs e)
        {
            lblStatus.Visible = false;

            chkNormal.Visible = false;
            chkNormal.Checked = true;
            chkNormal.Enabled = false;
           
            btnNormal.Visible = false;
            BindShopTypeData();
        }

        private void frmSearchShopType_Load(object sender, EventArgs e)
        {
            BindShopTypeData();
            btnSave.Enabled = false;
            lblStatus.Visible = false;
            chkNormal.Visible = false;
            btnNormal.Visible = false;
            pnlEdit.OpenControl(false, readOnlyPnlEditControls.ToArray());
            btnEdit.Enabled = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            pnlGrid.OpenControl(false, readOnlyPnlGridControls.ToArray());
            pnlEdit.Enabled = true;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            gridShopType.Enabled = false;
            txtShopTypeName.DisableTextBox(false);
            txtShopTypeName.Clear();
            txtShopTypeName.Focus();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            var ShopType = bu.GetShopTypeALLFlag();
            if(ShopType.Count != 0)
            {
                int _ShopTypeID = ShopType.Max(x => x.ShopTypeID);
                var shoptypeID = _ShopTypeID + 1;
                txtShopTypeID.Text = shoptypeID.ToString();
            }
            else
            {
                txtShopTypeID.Text = "1";
            }
        }
        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //true
            {
                errList.SetErrMessage(validateShopTypeCtrls);
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
                bu = new ShopType();
                if(!ValidateSave())
                    return;
                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;
                int shoptypeID = Convert.ToInt32(txtShopTypeID.Text);
                bool isEditMode = bu.CheckExistsShopType(shoptypeID);
                if(isEditMode)
                {
                    List<tbl_ShopType> shopTypeList = bu.GetShopType(shoptypeID);
                    shopTypeList.ForEach(x =>
                    {
                        x.ShopTypeName = txtShopTypeName.Text;
                        x.EdDate = DateTime.Now;
                        x.EdUser = Helper.tbl_Users.Username;
                    });
                    bu.tbl_ShopTypes.AddRange(shopTypeList);
                    ret = bu.UpdateData();
                }
                else
                {
                    tbl_ShopType shoptype = new tbl_ShopType();
                    pnlEdit.Controls.SetObjectFromControl(shoptype);
                    shoptype.CrDate = DateTime.Now;
                    shoptype.CrUser = Helper.tbl_Users.Username;
                    shoptype.ShopTypeCode = txtShopTypeID.Text;
                    shoptype.EdDate = null;
                    shoptype.EdUser = null;
                    shoptype.FlagSend = false;

                    if(rdoN.Checked == true)
                    {
                        shoptype.FlagDel = false;
                    }
                    else if(rdoC.Checked == true)
                    {
                        shoptype.FlagDel = true;
                    }
                    ret = bu.UpdateData(shoptype);
                }
                if(ret == 1)
                {
                    pnlEdit.OpenControl(false, readOnlyPnlEditControls.ToArray());
                    pnlGrid.OpenControl(true, readOnlyPnlGridControls.ToArray());
                    pnlEdit.ClearControl();
                    gridShopType.Enabled = true;
                    btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                    BindShopTypeData();

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
            Save();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint,"");
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            gridShopType.Enabled = false;
            pnlGrid.OpenControl(false, readOnlyPnlGridControls.ToArray());
            pnlEdit.Enabled = true;
            txtShopTypeName.DisableTextBox(false);
            txtShopTypeName.Focus();
        }
        private void RemoveShopType()
        {
            try
            {
                string cfMsg = "ต้องการลบข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการลบ!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;
                if (txtShopTypeID.Text != "")
                {
                    int ret = 0;
                    bu = new ShopType();
                    Func<tbl_ShopType, bool> shoptypeFunc = (x => x.ShopTypeID == Convert.ToInt32(txtShopTypeID.Text));
                    List<tbl_ShopType> shoptypeList = bu.GetShopType(shoptypeFunc);
                    tbl_ShopType stData = new tbl_ShopType();
                    stData = shoptypeList[0];
                    stData.EdDate = DateTime.Now;
                    stData.EdUser = Helper.tbl_Users.Username;
                    stData.FlagDel = true;
                    ret = bu.UpdateData(stData);
                    if(ret == 1)
                    {
                        string msg = "ลบข้อมูลเรียบร้อย!!";
                        msg.ShowInfoMessage();
                        BindShopTypeData();
                        pnlEdit.ClearControl();

                        btnSave.Enabled = false;
                        btnCancel.Enabled = false;
                        btnAdd.Enabled = true;
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
        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveShopType();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlGrid.OpenControl(true, readOnlyPnlGridControls.ToArray());
            pnlEdit.OpenControl(false, readOnlyPnlEditControls.ToArray());
            gridShopType.Enabled = true;

            txtShopTypeName.DisableTextBox(true);

            pnlEdit.ClearControl();

            DisableButton();
            btnAdd.Enabled = true;

            btnSearchShopType.Enabled = true;

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
        private void rdoN_CheckedChanged(object sender, EventArgs e)
        {
            pnlEdit.ClearControl();
            lblStatus.Visible = false;
            chkNormal.Visible = false;
            btnNormal.Visible = false;
            BindShopTypeData();
        }

        private void rdoC_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            pnlEdit.ClearControl();
            lblStatus.Visible = true;
            chkNormal.Visible = true;
            btnNormal.Visible = true;
            chkNormal.Checked = true;
            btnNormal.Enabled = true;
            BindShopTypeData();
        }
      
        private void editFlag()
        {
            try
            {
                string cfMsg = "ต้องการลบข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการลบ!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                if (txtShopTypeID.Text != "")
                {
                    bu = new ShopType();
                    int ret = 0;
                    tbl_ShopType stData = new tbl_ShopType();
                    bool isEditMode = stData.CheckExistsData(txtShopTypeID.Text);
                    if(isEditMode)
                    {
                        Func<tbl_ShopType, bool> shoptypeFunc = (x => x.ShopTypeID == Convert.ToInt32(txtShopTypeID.Text));
                        List<tbl_ShopType> shoptypeList = bu.GetShopType(shoptypeFunc);
                        stData = shoptypeList[0];
                        stData.EdDate = DateTime.Now;
                        stData.EdUser = Helper.tbl_Users.Username;
                        stData.FlagDel = false;
                        ret = bu.UpdateData(stData);
                    }
                    else
                    {
                        return;
                    }
                    if(ret == 1)
                    {
                        string msg = "ปรับสถานะรายการ เป็น ปกติ เรียบร้อยแล้ว";
                        msg.ShowInfoMessage();
                        BindShopTypeData();
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
            editFlag();
        }

        private void gridShopType_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            gridShopType.SetRowPostPaint(sender, e, this.Font);
        }
    }
}
