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
    public partial class frmProductUnit : Form
    {
        MasterDataControl bu = new MasterDataControl();
        MenuBU menuBU = new MenuBU();
        List<string> PanelEditControls = new List<string>();
        Dictionary<Control, Label> ValidateSaveCtrls = new Dictionary<Control, Label>();
        bool flagEdit = false;
        public frmProductUnit()
        {
            InitializeComponent();
            PanelEditControls = new string[] { txtProductUomCode.Name }.ToList();
            ValidateSaveCtrls.Add(txtProductUomCode, lbl_Code);
            ValidateSaveCtrls.Add(txtProductUomName, lbl_Name);
            ValidateSaveCtrls.Add(txtProductUomNameTH, lbl_Name_Th);
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

            grdProductUom.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            SetDefaultGridViewEvent(grdProductUom);
        }

        public void SetDefaultGridViewEvent(DataGridView grd)
        {
            grd.RowsDefaultCellStyle.BackColor = Color.White;
            grd.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
        }

        private void OpenPanelEdit(bool flagEnable)
        {
            pnlEdit.OpenControl(flagEnable, PanelEditControls.ToArray());

            foreach (Control item in pnlEdit.Controls)
            {
                if (flagEnable == false) // สั่งปิด   //ใส่สีเทาใน Control Panel
                {
                    if (item is Label || item is Panel || item is PictureBox || item is CheckBox || item is Button || item is GroupBox || item is ComboBox || item is ListBox || item is NumericUpDown)
                    {

                    }
                    else
                    {
                        item.BackColor = ColorTranslator.FromHtml("#DCDCDC");
                    }
                }
            }
        }
        private void SetButton()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }
        private void InitialData()
        {
            OpenPanelEdit(false);
            SetButton();
            pnlStatus.Visible = false;
        }
        private void frmProductUnit_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
            InitialData();
        }
        private void ClearTextBoxEdit()
        {
            txtProductUomCode.Clear();
            txtProductUomName.Clear();
            txtProductUomNameTH.Clear();
        }
        private void BindData()
        {
            ClearTextBoxEdit();

            int flagDel = rdoN.Checked ? 0 : 1;

            DataTable dt = new DataTable();

            dt = bu.GetProductUomData(flagDel,txtSearch.Text);

            btnEdit.Enabled = true;

            pnlStatus.Visible = false;

            btnAdd.Enabled = true;

            btnRemove.Enabled = true;

            if (rdoN.Checked == true)
            {
                if (dt.Rows.Count == 0)
                {
                    btnRemove.Enabled = false;
                    btnEdit.Enabled = false;
                }
            }
            else
            {
                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnSave.Enabled = false;
                btnCancel.Enabled = false;

                if (dt.Rows.Count > 0)
                {
                    pnlStatus.Visible = true;
                }
            }

            grdProductUom.DataSource = dt;
            lblgrdQty.Text = dt.Rows.Count.ToNumberFormat();
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
        private void grdProductUom_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdProductUom.SetRowPostPaint(sender, e, this.Font);
        }
        private void SelectProductUomDetails(DataGridViewCellEventArgs e)
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
                    grdRows = grdProductUom.Rows[e.RowIndex];
                }
            }
            else
            {
                grdRows = grdProductUom.CurrentRow;
            }

            if (grdRows != null)
            {
                txtProductUomCode.Text = grdRows.Cells["colProductUomCode"].Value.ToString();
                txtProductUomName.Text = grdRows.Cells["colProductUomName"].Value.ToString();
                txtProductUomNameTH.Text = grdRows.Cells["colProductUomNameTH"].Value.ToString();
            }
        }
        private void grdProductUom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectProductUomDetails(e);
        }
        private void grdProductUom_SelectionChanged(object sender, EventArgs e)
        {
            SelectProductUomDetails(null);
        }
        private void OpenPanelGridView(bool flagEnable)
        {
            txtSearch.DisableTextBox(!flagEnable);
            btnSearch.Enabled = flagEnable;
            rdoN.Enabled = flagEnable;
            rdoC.Enabled = flagEnable;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            flagEdit = false;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            OpenPanelGridView(false);
            OpenPanelEdit(true);

            ClearTextBoxEdit();
            grdProductUom.Enabled = false;

            txtProductUomCode.DisableTextBox(false);
            txtProductUomCode.Focus();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            flagEdit = true;
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            OpenPanelGridView(false);

            OpenPanelEdit(true);

            txtProductUomCode.DisableTextBox(true);
            txtProductUomName.Focus();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearTextBoxEdit();

            OpenPanelGridView(true);

            SetButton();

            btnSearch.PerformClick();

            OpenPanelEdit(false);

            grdProductUom.Enabled = true;
        }
        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) 
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateSave())
            {
                return;
            }
            try
            {
                int ret = 0;

               
                var ProUomList = bu.GetProductUom(x => x.ProductUomCode == txtProductUomCode.Text);

                if (flagEdit == false && ProUomList.Count > 0)
                {
                    string msg = "รหัสซ้ำกับในระบบ กรุณาเปลี่ยนรหัสใหม่ !!";
                    msg.ShowWarningMessage();
                    return;
                }

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;


                var ProUom = new tbl_ProductUom();

                if (ProUomList.Count > 0)
                {
                    ProUom = ProUomList[0];
                    PrePare_ProductUom(ProUom);

                    ProUom.EdDate = DateTime.Now;
                    ProUom.EdUser = Helper.tbl_Users.Username;
                }
                else //New ข้อมูลหน่วยสินค้า
                {
                    var proUomList = bu.GetProductUom();

                    if (proUomList.Count > 0)
                    {
                        ProUom.ProductUomID = proUomList.Select(x => x.ProductUomID).Max() + 1;
                    }
                    else
                    {
                        ProUom.ProductUomID = 1;
                    }

                    PrePare_ProductUom(ProUom);

                    ProUom.CrDate = DateTime.Now;//
                    ProUom.CrUser = Helper.tbl_Users.Username;//

                    ProUom.EdDate = null;
                    ProUom.EdUser = null;

                    ProUom.FlagDel = false;
                    ProUom.FlagSend = false;
                }

                ret = bu.UpdateProductUomData(ProUom);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    msg.ShowInfoMessage();

                    OpenPanelEdit(false);//
                    OpenPanelGridView(true);

                    grdProductUom.Enabled = true;

                    SetButton();

                    btnSearch.PerformClick();
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
        private void PrePare_ProductUom(tbl_ProductUom ProUom,bool EditData = false)
        {
            ProUom.ProductUomCode = txtProductUomCode.Text;
            ProUom.ProductUomName = txtProductUomName.Text;
            ProUom.ProductUomNameTH = txtProductUomNameTH.Text;

            if (EditData == true)
            {
                if (grdProductUom.CurrentRow.Cells["colCrDate"].Value != null)
                {
                    ProUom.CrDate = Convert.ToDateTime(grdProductUom.CurrentRow.Cells["colCrDate"].Value);
                }
                else
                {
                    ProUom.CrDate = null;
                }

                ProUom.CrUser = grdProductUom.CurrentRow.Cells["colCrUser"].Value.ToString();

                ProUom.EdDate = DateTime.Now;
                ProUom.EdUser = Helper.tbl_Users.Username;
            }
        }
        private void Remove(bool flagRemove = true)
        {
            try
            {
                if (flagRemove == true)
                {
                    string cfMsg = "คุณแน่ใจหรือไม่ที่จะลบข้อมูลรายการนี้?";
                    string title = "ทำการยืนยัน!!";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;
                }
               
                int ret = 0;

                int ProductUomID = Convert.ToInt32(grdProductUom.CurrentRow.Cells["colProductUomID"].Value);

                var ProUom = bu.GetProductUom(x=>x.ProductUomID == ProductUomID);

                if (ProUom.Count > 0)
                {
                    if (flagRemove == true)
                    {
                        ProUom[0].FlagDel = true;
                    }

                    else
                    {
                        ProUom[0].FlagDel = false;
                    }

                    ProUom[0].EdDate = DateTime.Now;
                    ProUom[0].EdUser = Helper.tbl_Users.Username;

                    ret = bu.UpdateProductUomData(ProUom[0]);
                }
                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    msg.ShowInfoMessage();

                    if (flagRemove == false)
                    {
                        rdoC.Checked = false;
                        rdoC.Checked = true;
                    }
                    else
                    {
                        btnSearch.PerformClick();
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
        private void btnN_Click(object sender, EventArgs e)
        {
            Remove(false);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProductUnit_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
