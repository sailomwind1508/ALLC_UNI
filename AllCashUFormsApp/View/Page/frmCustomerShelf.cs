using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
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
    public partial class frmCustomerShelf : Form
    {
        CustomerShelf bu = new CustomerShelf();
        MenuBU menuBU = new MenuBU();
        List<string> PanelEditControls = new List<string>();
        List<string> PanelSearchControls = new List<string>();
        DataTable tempShelf = new DataTable("Shelf");
        public frmCustomerShelf()
        {
            InitializeComponent();
            PanelSearchControls = new string[] { txtSearch.Name }.ToList();
            PanelEditControls = new string[] { txtWHID.Name }.ToList();
        }

        #region Private Event
        private void frmCustomerShelf_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                pnlEdit.ClearControl();
                pnlEdit.OpenControl(false, PanelEditControls.ToArray());

                string WHID = ddlVan.SelectedIndex == 0 ? "" : ddlVan.SelectedValue.ToString();
                int FlagDel = rdoN.Checked ? 0 : 1;
                tempShelf = bu.GetCustomerShelfData(txtSearch.Text, FlagDel, WHID);
                grdCustomerShelf.DataSource = tempShelf;
                lblrowQty.Text = tempShelf.Rows.Count.ToNumberFormat();

                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnSave.Enabled = false;
                btnCancel.Enabled = false;

                if (tempShelf.Rows.Count > 0)
                {
                    btnEdit.Enabled = true;
                }

                SelectDetail(null);
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void grdCustomerShelf_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectDetail(e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            pnlSearch.OpenControl(false, PanelSearchControls.ToArray());
            txtShelfID.DisableTextBox(false);
            txtShelfID.Select();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "start frmCustomerShelf=>btnSave_Click";
            msg.WriteLog(this.GetType());

            try
            {
                if (string.IsNullOrEmpty(txtShelfID.Text))
                {
                    msg = "กรุณากรอกรหัส Shelf !!";
                    msg.ShowWarningMessage();
                    txtShelfID.ErrorTextBox();
                    return;
                }

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Cursor.Current = Cursors.WaitCursor;

                int ret = 0;

                var Shelf = new tbl_ArCustomerShelf();
                Shelf.AutoID = Convert.ToInt32(txtAutoID.Text.Trim());
                Shelf.ShelfID = txtShelfID.Text.Trim();
                ret = bu.UpdateCustomerShelf(Shelf);

                if (ret == 1)
                {
                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                    pnlSearch.OpenControl(true, PanelSearchControls.ToArray());
                    txtSearch.DisableTextBox(false);
                    pnlEdit.OpenControl(false, PanelEditControls.ToArray());
                    btnSearch.PerformClick();
                }
                else
                {
                    this.ShowProcessErr();
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                Cursor.Current = Cursors.Default;
            }

            msg = "end frmCustomerShelf=>btnSave_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlEdit.ClearControl();
            pnlEdit.OpenControl(false, PanelEditControls.ToArray());
            pnlSearch.OpenControl(true, PanelSearchControls.ToArray());
            txtSearch.DisableTextBox(false);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            if (grdCustomerShelf.Rows.Count > 0)
            {
                btnEdit.Enabled = true;
            }

            SelectDetail(null);
        }

        #endregion

        #region Private Method
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

            grdCustomerShelf.RowsDefaultCellStyle.BackColor = Color.White;
            grdCustomerShelf.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            grdCustomerShelf.AutoGenerateColumns = false;
            grdCustomerShelf.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            pnlEdit.OpenControl(false, PanelEditControls.ToArray());
        }

        private void InitialData()
        {
            var branchWarehouse = new List<tbl_BranchWarehouse>();
            branchWarehouse.Add(new tbl_BranchWarehouse { WHID = "-1", WHCode = "==เลือก==" });
            branchWarehouse.AddRange(bu.GetAllBranchWarehouse(x => x.WHID.Length == 6));
            ddlVan.BindDropdownList(branchWarehouse, "WHCode", "WHID", 0);
        }

        private void SelectDetail(DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow grdRows = null;

                if (e != null)
                {
                    if (e.RowIndex == -1)
                        return;

                    grdRows = grdCustomerShelf.Rows[e.RowIndex];
                }
                else
                {
                    grdRows = grdCustomerShelf.CurrentRow;
                }

                if (grdRows != null)
                {
                    int _AutoID = Convert.ToInt32(grdRows.Cells["colAutoID"].Value);
                    DataRow r = tempShelf.AsEnumerable().FirstOrDefault(x => x.Field<int>("AutoID") == _AutoID);

                    bool _FlagDel = Convert.ToBoolean(r["FlagDel"]);
                    if (_FlagDel)
                        rdoCancel.Checked = true;
                    else
                        rdoNormal.Checked = true;

                    txtAutoID.Text = r["AutoID"].ToString().Trim();
                    txtWHID.Text = r["WHID"].ToString().Trim();
                    txtWHName.Text = r["WHName"].ToString().Trim();
                    txtCustomerID.Text = r["CustomerID"].ToString().Trim();
                    txtCustName.Text = r["CustName"].ToString().Trim();
                    txtProductID.Text = r["ProductID"].ToString().Trim();
                    txtProductName.Text = r["ProductName"].ToString().Trim();
                    txtShelfID.Text = r["ShelfID"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCustomerShelf_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void grdCustomerShelf_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdCustomerShelf.SetRowPostPaint(sender, e, this.Font);
        }

        private void txtShelfID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
                e.Handled = true;
            else
                return;
        }

    }
}
