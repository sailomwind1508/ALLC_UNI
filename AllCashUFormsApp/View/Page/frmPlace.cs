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

namespace AllCashUFormsApp.View.Page
{
    public partial class frmPlace : Form
    {
        Province bu = new Province();
        MenuBU menuBU = new MenuBU();
        static DataTable dtProvince = new DataTable();
        static DataTable dtArea = new DataTable();
        static DataTable dtDistrict = new DataTable();
        Dictionary<Control, Label> validateProvince = new Dictionary<Control, Label>();
        Dictionary<Control, Label> validateArea = new Dictionary<Control, Label>();
        Dictionary<Control, Label> validateDistrict = new Dictionary<Control, Label>();
        private bool flagEdit; //Check ว่าสร้างใหม่ หรือ แก้ไข
        List<string> PnlProvinceControls = new List<string>();
        List<string> PnlAreaControls = new List<string>();
        List<string> PnlDistrictControls = new List<string>();
        public frmPlace()
        {
            InitializeComponent();
            validateProvince.Add(txtProvinceCode, lblCodeProvince);//
            validateProvince.Add(txtProvinceName, lblNameProvince);//

            validateArea.Add(txtAreaCode, lblCodeArea);//
            validateArea.Add(txtAreaName, lblNameArea);//

            validateDistrict.Add(txtDistrictCode, lblCodeDistrict);//
            validateDistrict.Add(txtDistrictName, lblNameDistrict);//
            PnlProvinceControls = new string[] { txtProvinceCode.Name }.ToList();
            PnlAreaControls = new string[] { txtAreaCode.Name }.ToList();
            PnlDistrictControls = new string[] { txtDistrictCode.Name }.ToList();
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

            SetDefaultGridViewEvent(grdDistrict);
            SetDefaultGridViewEvent(grdArea);
            SetDefaultGridViewEvent(grdProvince);
        }

        public void SetDefaultGridViewEvent(DataGridView grd)
        {
            grd.RowsDefaultCellStyle.BackColor = Color.White;
            grd.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
        }

        private void BindProvince()
        {
            string text = txtSearchProvince.Text;

            int flagDel = rdoProvinceN.Checked ? 0 : 1;
            dtProvince = new DataTable();
            dtProvince = bu.GetProvinceTable(flagDel,text); //new
            grdProvince.DataSource = dtProvince;
            lblgrdProvince_Qty.Text = dtProvince.Rows.Count.ToNumberFormat();

            if (dtProvince.Rows.Count == 0)
            {
                EnableButton(false);
                pnlProvinceEdit.ClearControl();
            }
            else
            {
                EnableButton(true);
            }

            grdProvince.RowsDefaultCellStyle.BackColor = Color.White;
            grdProvince.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            grdArea.RowsDefaultCellStyle.BackColor = Color.White;
            grdArea.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            grdDistrict.RowsDefaultCellStyle.BackColor = Color.White;
            grdDistrict.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            //pnlPrvStatus.Visible = rdoProvinceN.Checked ? false : true;
        }
        private void BindArea()
        {
            string text = txtSearchArea.Text;
            int provinceID = ddlProvince.SelectedIndex == 0  ? 0 : Convert.ToInt32(ddlProvince.SelectedValue);
            int flagDel = rdoAreaN.Checked ? 0 : 1;
            dtArea = new DataTable();
            dtArea = bu.GetAreaTable(flagDel, provinceID, text); //new
            grdArea.DataSource = dtArea;
            grdAreaQty.Text = dtArea.Rows.Count.ToNumberFormat();

            if (dtArea.Rows.Count == 0)
            {
                EnableButton(false);
                pnlAreaEdit.ClearControl();
                PnlAreaControlEdits(false);
            }
            else
            {
                EnableButton(true);
            }
            //pnlAreaStatus.Visible = rdoAreaN.Checked ? false : true;
        }
        private void BindDistrict()
        {
            string text = txtSearchDistrict.Text;

            int areaID = ddlArea.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlArea.SelectedValue);

            int flagDel = rdoDistrictN.Checked ? 0 : 1;

            dtDistrict = new DataTable();

            dtDistrict = bu.GetDistrictTable(flagDel, areaID, text); //new

            grdDistrict.DataSource = dtDistrict;

            lbl_grdDistrict_qty.Text = dtDistrict.Rows.Count.ToNumberFormat();

            if (dtDistrict.Rows.Count == 0)
            {
                EnableButton(false);
                pnlDistrictEdit.ClearControl();
            }
            else
            {
                EnableButton(true);
            }
            //pnlDistrictStatus.Visible = rdoDistrictN.Checked ? false : false;
        }
        private void TabPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tabb = TabPlace.SelectedTab.Text.ToString();
            if (tabb == "อำเภอ/เขต")
            {
                pnlProvinceEdit.Visible = false;
                pnlAreaEdit.Visible = true;
                pnlDistrictEdit.Visible = false;

                rdoAreaN.Checked = true;
                
                BindArea();
                PnlAreaControlEdits(false); //
                
                dtProvince = new DataTable();
                dtDistrict = new DataTable();
            }
            else if (tabb == "จังหวัด")
            {
                pnlProvinceEdit.Visible = true;
                pnlAreaEdit.Visible = false;
                pnlDistrictEdit.Visible = false;

                rdoProvinceN.Checked = true;

                BindProvince();
                PnlProvinceControlEdits(false);//

                dtArea = new DataTable();
                dtDistrict = new DataTable();
            }
            else if (tabb == "แขวง/ตำบล")
            {
                pnlProvinceEdit.Visible = false;
                pnlAreaEdit.Visible = false;
                pnlDistrictEdit.Visible = true;

                rdoDistrictN.Checked = true;

                BindDistrict();
                PnlDistrictControlEdits(false); //

                dtProvince = new DataTable();
                dtArea = new DataTable();
            }
        }
        private void SetDefaultGridView()
        {
            grdProvince.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdArea.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdDistrict.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            grdProvince.AutoGenerateColumns = false;
            grdArea.AutoGenerateColumns = false;
            grdDistrict.AutoGenerateColumns = false;
        }
        private void EnableButton(bool flagEnable)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;

            if (flagEnable == true)
            {
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
            }
            
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }
        private void InitialData()
        {
            EnableButton(true);

            SetDefaultGridView();

            rdoProvinceN.Checked = true;
            pnlProvinceEdit.Visible = true;
            pnlAreaEdit.Visible = false;
            pnlDistrictEdit.Visible = false;

            var ProvinceList = new List<tbl_MstProvince>();
            ProvinceList.Add(new tbl_MstProvince { ProvinceID = -1 , ProvinceName = "==เลือก=="} );
            ProvinceList.AddRange(bu.GetMstProvince());
            ddlProvince.BindDropdownList(ProvinceList, "ProvinceName", "ProvinceID");

            var AreaList = new List<tbl_MstArea>();
            AreaList.Add(new tbl_MstArea { AreaID = -1, AreaName = "==เลือก==" });
            AreaList.AddRange(bu.GetMstArea());
            ddlArea.BindDropdownList(AreaList, "AreaName", "AreaID");

            BindProvince();

            PnlProvinceControlEdits(false);

            //pnlPrvStatus.Visible = false;
            //pnlAreaStatus.Visible = false;
            //pnlDistrictStatus.Visible = false;
        }
        private void PnlProvinceControlEdits(bool flagEnable)
        {
            pnlProvinceEdit.OpenControl(flagEnable, PnlProvinceControls.ToArray());
            foreach (Control item in pnlProvinceEdit.Controls)
            {
                if (item is Label || item is Panel || item is PictureBox || item is Button || item is GroupBox || item is CheckBox || item is ComboBox || item is ListBox)
                {

                }
                else
                {
                    if (flagEnable == true)
                    {
                        item.BackColor = Color.White;
                    }
                    else
                    {
                        item.BackColor = ColorTranslator.FromHtml("#DCDCDC");
                    }
                }
            }

            chkFlagProvince.Enabled = flagEnable;
            btnFlagProvince.Enabled = flagEnable;
        }
        private void PnlAreaControlEdits(bool flagEnable)
        {
            pnlAreaEdit.OpenControl(flagEnable, PnlAreaControls.ToArray());
            foreach (Control item in pnlProvinceEdit.Controls)
            {
                if (item is Label || item is Panel || item is PictureBox || item is Button || item is GroupBox || item is CheckBox || item is ComboBox || item is ListBox)
                {

                }
                else
                {
                    if (flagEnable == true)
                    {
                        item.BackColor = Color.White;
                    }
                    else
                    {
                        item.BackColor = ColorTranslator.FromHtml("#DCDCDC");
                    }
                }
            }
            chkFlagArea.Enabled = flagEnable;
            btnFlagArea.Enabled = flagEnable;
        }
        private void PnlDistrictControlEdits(bool flagEnable)
        {
            pnlDistrictEdit.OpenControl(flagEnable, PnlDistrictControls.ToArray());
            foreach (Control item in pnlDistrictEdit.Controls)
            {
                if (item is Label || item is Panel || item is PictureBox || item is Button || item is GroupBox || item is CheckBox || item is ComboBox || item is ListBox)
                {

                }
                else
                {
                    if (flagEnable == true)
                    {
                        item.BackColor = Color.White;
                    }
                    else
                    {
                        item.BackColor = ColorTranslator.FromHtml("#DCDCDC");
                    }
                }
            }
            chkFlagDistrict.Enabled = flagEnable;
            btnFlagDistrict.Enabled = flagEnable;
        }
        private void frmPlace_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }

        private void btnSearchProvince_Click(object sender, EventArgs e)
        {
            BindProvince();
           
        }

        private void txtSearchProvince_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindProvince();
            }
        }

        private void rdoProvinceN_CheckedChanged(object sender, EventArgs e)
        {
            BindProvince();
        }

        private void rdoProvinceC_CheckedChanged(object sender, EventArgs e)
        {
            BindProvince();

            btnRemove.Enabled = false;

        }
        private void SelectProvinceDetails(DataGridViewCellEventArgs e)
        {
            if (e != null)
            {
                if (e.RowIndex == -1)
                    return;
            }

            DataGridViewRow gridrow = null;

            if (e != null)
                gridrow = grdProvince.Rows[e.RowIndex];
            else
                gridrow = grdProvince.CurrentRow;
            if (gridrow != null)
            {
                string ProvinceID = gridrow.Cells["colProvinceID"].Value.ToString();

                foreach (DataRow r in dtProvince.Rows)
                {
                    if (r["ProvinceID"].ToString() == ProvinceID)
                    {
                        txtProvinceCode.Text = r["ProvinceCode"].ToString();
                        txtProvinceName.Text = r["ProvinceName"].ToString();
                        chkFlagProvince.Checked = Convert.ToBoolean(r["FlagDel"]);
                        break;
                    }
                }
            }
            
            
        }
        private void grdProvince_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectProvinceDetails(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            dtProvince = new DataTable();
            dtArea = new DataTable();
            dtDistrict = new DataTable();

            this.Close();
        }
        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();
            string tabName = TabPlace.SelectedTab.Text.ToString();

            if (tabName == "จังหวัด") //true
            {
                errList.SetErrMessage(validateProvince);
            }
            else if (tabName == "อำเภอ/เขต")
            {
                errList.SetErrMessage(validateArea);
            }
            else if (tabName == "แขวง/ตำบล")
            {
                errList.SetErrMessage(validateDistrict);
            }
            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }
            return ret;
        }
        private void grdProvince_SelectionChanged(object sender, EventArgs e)
        {
            SelectProvinceDetails(null);
        }
        private void txtKeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
            else
            {
                return;
            }
        }
        private void PrePareButtonAdd()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = false;
        }
        private void PrePareControlToAdd(CheckBox chkbox,TextBox txtCode, TextBox txtName,Panel PanelEdit,CheckBox chkFlag,Button btnFlag)
        {
            flagEdit = false;
            TabPlace.Enabled = false;

            PrePareButtonAdd();

            chkbox.Checked = false;
            
            txtCode.DisableTextBox(false);
            txtCode.Focus();

            txtName.DisableTextBox(false);

            PanelEdit.ClearControl();

            chkFlag.Enabled = true;
            btnFlag.Enabled = true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string tabName = TabPlace.SelectedTab.Text.ToString();

            if (tabName == "จังหวัด")
            {
                PrePareControlToAdd(chkFlagProvince,txtProvinceCode,txtProvinceName,pnlProvinceEdit,chkFlagProvince,btnFlagProvince);
            }

            else if (tabName == "อำเภอ/เขต")
            {
                if (ddlProvince.SelectedIndex == 0)
                {
                    string msg = "กรุณาเลือก จังหวัด !!";
                    msg.ShowWarningMessage();
                    return;
                }
                else
                {
                    PrePareControlToAdd(chkFlagArea, txtAreaCode, txtAreaName, pnlAreaEdit,chkFlagArea,btnFlagArea);
                    txtPostalCode.DisableTextBox(false);
                }
            }

            else if (tabName == "แขวง/ตำบล")
            {
                if (ddlArea.SelectedIndex == 0)
                {
                    string msg = "กรุณาเลือก เขต/อำเภอ !!";
                    msg.ShowWarningMessage();
                    return;
                }
                else
                {
                    PrePareControlToAdd(chkFlagDistrict, txtDistrictCode, txtDistrictName, pnlDistrictEdit, chkFlagDistrict, btnFlagDistrict);
                }
            }
            
        }
        private tbl_MstArea PrePareMstArea(bool flagRemove,string AreaID)
        {
            tbl_MstArea tbl_MstAreas = new tbl_MstArea();

            foreach (DataRow r in dtArea.Rows)
            {
                if (r["AreaID"].ToString() == AreaID)
                {
                    tbl_MstAreas.AreaID = Convert.ToInt32(r["AreaID"]);
                    tbl_MstAreas.AreaCode = txtAreaCode.Text;
                    tbl_MstAreas.AreaName = txtAreaName.Text;
                    tbl_MstAreas.ProvinceID = Convert.ToInt32(r["ProvinceID"]);
                    tbl_MstAreas.PostalCode = txtPostalCode.Text;

                    if (string.IsNullOrEmpty(r["CrDate"].ToString()))
                    {
                        tbl_MstAreas.CrDate = null;
                    }
                    else
                    {
                        tbl_MstAreas.CrDate = Convert.ToDateTime(r["CrDate"]);
                    }

                    tbl_MstAreas.CrUser = r["CrUser"].ToString();

                    tbl_MstAreas.EdDate = DateTime.Now;
                    tbl_MstAreas.EdUser = Helper.tbl_Users.Username;

                    if (flagRemove == true)
                    {
                        tbl_MstAreas.FlagDel = true;
                    }
                    else
                    {
                        tbl_MstAreas.FlagDel = chkFlagArea.Checked ? true : false;
                    }

                    tbl_MstAreas.FlagSend = Convert.ToBoolean(r["FlagSend"]);

                    break;
                }
            }
            return tbl_MstAreas;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            flagEdit = true;
         
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;

            TabPlace.Enabled = false;

            string tabName = TabPlace.SelectedTab.Text.ToString();

            if (tabName == "จังหวัด")
            {
                //PnlProvinceGridViewControlEdit(true);
                PnlProvinceControlEdits(true);
                txtProvinceCode.DisableTextBox(true);
                txtProvinceName.DisableTextBox(false);
            }

            else if (tabName == "อำเภอ/เขต")
            {
                //PnlAreaGridViewControlEdit(true);
                PnlAreaControlEdits(true);
                txtAreaCode.DisableTextBox(true);
                txtAreaName.DisableTextBox(false);
            }

            else if (tabName == "แขวง/ตำบล")
            {
                //PnlDistrictGridViewControlEdit(true);
                PnlDistrictControlEdits(true);
                txtDistrictCode.DisableTextBox(true);
                txtDistrictName.DisableTextBox(false);
            }

        }
        private void SaveArea()
        {
            if (txtAreaCode.TextLength < 6 && txtAreaCode.TextLength > 6)
            {
                string msg = "--> รหัสเขต : ควรไม่น้อยกว่า 6 ตัว และไม่มากกว่า 6 ตัว ";
                msg.ShowWarningMessage();
                return;
            }

            var tbl_MstAreas = new tbl_MstArea();
            int ret = 0;
            if (flagEdit == true)
            {
                string AreaID = grdArea.CurrentRow.Cells["colAreaID"].Value.ToString();
                tbl_MstAreas = PrePareMstArea(false, AreaID);
            }
            else
            {
                var areaList = bu.GetMstArea().OrderByDescending(x=>x.AreaID).ToList();
                tbl_MstAreas.AreaID = areaList.Count > 0 ? areaList.First().AreaID + 1 : 1;

                tbl_MstAreas.AreaCode = txtAreaCode.Text;
                tbl_MstAreas.AreaName = txtAreaName.Text;

                tbl_MstAreas.ProvinceID = Convert.ToInt32(ddlProvince.SelectedValue);

                tbl_MstAreas.PostalCode = txtPostalCode.Text;

                tbl_MstAreas.CrDate = DateTime.Now;
                tbl_MstAreas.CrUser = Helper.tbl_Users.Username;

                tbl_MstAreas.EdDate = null;
                tbl_MstAreas.EdUser = null;

                tbl_MstAreas.FlagDel = chkFlagArea.Checked ? true : false;
                tbl_MstAreas.FlagSend = false;
            }
            ret = bu.UpdateData(tbl_MstAreas);
            if (ret == 1)
            {
                string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                msg.ShowInfoMessage();
             
                TabPlace.Enabled = true;
                txtSearchArea.DisableTextBox(false);
                grdArea.Enabled = true;

                PnlAreaControlEdits(false);

                EnableButton(true);

                btnSearchArea.PerformClick();
            }
            else
            {
                this.ShowProcessErr();
                return;
            }
        }
        private tbl_MstDistrict PrePareMstDistrict(bool flagRemove,string DistrictID)
        {
            var tbl_MstDistricts = new tbl_MstDistrict();
            foreach (DataRow r in dtDistrict.Rows)
            {
                if (r["DistrictID"].ToString() == DistrictID)
                {
                    tbl_MstDistricts.DistrictID = Convert.ToInt32(r["DistrictID"]);
                    tbl_MstDistricts.DistrictCode = txtDistrictCode.Text;
                    tbl_MstDistricts.DistrictName = txtDistrictName.Text;
                    tbl_MstDistricts.AreaID = Convert.ToInt32(r["AreaID"]);

                    if (string.IsNullOrEmpty(r["CrDate"].ToString()))
                    {
                        tbl_MstDistricts.CrDate = null;
                    }
                    else
                    {
                        tbl_MstDistricts.CrDate = Convert.ToDateTime(r["CrDate"]);
                    }
                    tbl_MstDistricts.CrUser = r["CrUser"].ToString();

                    tbl_MstDistricts.EdDate = DateTime.Now;
                    tbl_MstDistricts.EdUser = Helper.tbl_Users.Username;

                    if (flagRemove == false)
                    {
                        tbl_MstDistricts.FlagDel = chkFlagDistrict.Checked ? true : false;
                    }
                    else
                    {
                        tbl_MstDistricts.FlagDel = true;
                    }

                    tbl_MstDistricts.FlagSend = Convert.ToBoolean(r["FlagSend"]);

                    tbl_MstDistricts.CountDis = Convert.ToInt32(r["CountDis"]);

                    break;
                }
            }
            return tbl_MstDistricts;
        }
        private tbl_MstProvince PrePareMstProvince(bool flagRemove,string ProvinceID)
        {
            tbl_MstProvince tbl_MstProvinces = new tbl_MstProvince();

                foreach (DataRow r in dtProvince.Rows)
                {
                    if (r["ProvinceID"].ToString() == ProvinceID)
                    {
                        tbl_MstProvinces.ProvinceID = Convert.ToInt32(r["ProvinceID"]);
                        tbl_MstProvinces.ProvinceCode = txtProvinceCode.Text;

                        tbl_MstProvinces.ProvinceName = txtProvinceName.Text;

                        if (string.IsNullOrEmpty(r["CrDate"].ToString()))
                        {
                            tbl_MstProvinces.CrDate = null;
                        }
                        else
                        {
                            tbl_MstProvinces.CrDate = Convert.ToDateTime(r["CrDate"]);
                        }

                        tbl_MstProvinces.CrUser = r["CrUser"].ToString();

                        tbl_MstProvinces.EdDate = DateTime.Now;
                        
                        tbl_MstProvinces.EdUser = Helper.tbl_Users.Username;

                        if (flagRemove == true)
                        {
                            tbl_MstProvinces.FlagDel = true;
                        }
                        else
                        {
                            tbl_MstProvinces.FlagDel = chkFlagProvince.Checked ? true : false;
                        }

                        tbl_MstProvinces.FlagSend = Convert.ToBoolean(r["FlagSend"]);
                        break;
                    }
                }
            return tbl_MstProvinces;
        }
        private void SaveProvince()
        {
            try
            {
                if (txtProvinceCode.TextLength < 6 && txtProvinceCode.TextLength > 6)
                {
                    string msg = "--> รหัส : ควรไม่น้อยกว่า 6 ตัว และไม่มากกว่า 6 ตัว ";
                    msg.ShowWarningMessage();
                    return;
                }

                var tbl_MstProvinces = new tbl_MstProvince();

                int ret = 0;

                if (flagEdit == true)
                {
                    string ProvinceID = grdProvince.CurrentRow.Cells["colProvinceID"].Value.ToString();

                    tbl_MstProvinces = PrePareMstProvince(false,ProvinceID); 
                }
                else
                {
                    var prvList = bu.GetAllProvince().OrderByDescending(x=>x.ProvinceID).ToList();
                    tbl_MstProvinces.ProvinceID = prvList.Count > 0 ? prvList.First().ProvinceID + 1 : 1;

                    tbl_MstProvinces.ProvinceCode = txtProvinceCode.Text;
                    tbl_MstProvinces.ProvinceName = txtProvinceName.Text;

                    tbl_MstProvinces.CrDate = DateTime.Now;
                    tbl_MstProvinces.CrUser = Helper.tbl_Users.Username;

                    tbl_MstProvinces.EdDate = null;
                    tbl_MstProvinces.EdUser = null;

                    tbl_MstProvinces.FlagDel = chkFlagProvince.Checked ? true : false;
                    tbl_MstProvinces.FlagSend = false;
                }

                ret = bu.UpdateData(tbl_MstProvinces); //MstProvince - Update - New Insert

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    TabPlace.Enabled = true;

                    txtSearchProvince.DisableTextBox(false);
                    grdProvince.Enabled = true;

                    PnlProvinceControlEdits(false);

                    EnableButton(true);

                    btnSearchProvince.PerformClick();
                }
                else
                {
                    this.ShowProcessErr();
                    return;
                }
                
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                msg.ShowErrorMessage();
            }
        }
        private void SaveDistrict()
        {
            if (txtDistrictCode.TextLength < 6 && txtDistrictCode.TextLength > 6)
            {
                string msg = "--> รหัส : ควรไม่น้อยกว่า 6 ตัว และไม่มากกว่า 6 ตัว ";
                msg.ShowWarningMessage();
                return;
            }
            var tbl_MstDistricts = new tbl_MstDistrict();

            int ret = 0;

            if (flagEdit == true)
            {
                string DistrictID = grdDistrict.CurrentRow.Cells["colDistrictID"].Value.ToString();

                tbl_MstDistricts = PrePareMstDistrict(false, DistrictID);
            }
            else
            {
                var districtList = bu.GetMstDistrict().OrderByDescending(x=>x.DistrictID).ToList();
                tbl_MstDistricts.DistrictID = districtList.Count > 0 ? districtList.First().DistrictID + 1 : 1;

                tbl_MstDistricts.DistrictCode = txtDistrictCode.Text;
                tbl_MstDistricts.DistrictName = txtDistrictName.Text;
                tbl_MstDistricts.AreaID = Convert.ToInt32(ddlArea.SelectedValue);

                tbl_MstDistricts.CrDate = DateTime.Now;
                tbl_MstDistricts.CrUser = Helper.tbl_Users.Username;

                tbl_MstDistricts.EdDate = null;
                tbl_MstDistricts.EdUser = null;

                tbl_MstDistricts.FlagDel = chkFlagDistrict.Checked ? true : false;
                
                tbl_MstDistricts.FlagSend = false;

                tbl_MstDistricts.CountDis = 0;
            }
            ret = bu.UpdateData(tbl_MstDistricts); //new method UPDATE INSERT

            if (ret == 1)
            {
                string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                msg.ShowInfoMessage();
                
                TabPlace.Enabled = true;
                txtSearchDistrict.DisableTextBox(false);
                grdDistrict.Enabled = true;

                PnlDistrictControlEdits(false);

                EnableButton(true);

                btnSearchDistrict.PerformClick();
            }
            else
            {
                this.ShowProcessErr();
                return;
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateSave())
            {
                return;
            }

            string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการบันทึก!!";

            if (!cfMsg.ConfirmMessageBox(title))
                return;

            string tabName = TabPlace.SelectedTab.Text.ToString();

            if (tabName == "จังหวัด")
            {
                SaveProvince();
            }
            else if (tabName == "อำเภอ/เขต")
            {
                SaveArea();
            }
            else if (tabName == "แขวง/ตำบล")
            {
                SaveDistrict();
            }
        }

        private void grdProvince_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdProvince.SetRowPostPaint(sender, e, this.Font);
        }
        private void PrePareTabPanel(Panel pnlTop,Panel pnlButtom,DataGridView grd,Button btnSearch)
        {
            pnlTop.Enabled = true;
            pnlButtom.Enabled = true;
            grd.Enabled = true;
            btnSearch.PerformClick();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            EnableButton(true);

            TabPlace.Enabled = true;

            string tabName = TabPlace.SelectedTab.Text.ToString();

            if (tabName == "จังหวัด")
            {
                //PnlProvinceGridViewControlEdit(false);
                PnlProvinceControlEdits(false);//

                PrePareTabPanel(pnlTopProvince, pnlBottomProvince, grdProvince, btnSearchProvince);
            }

            else if (tabName == "อำเภอ/เขต")
            {
                PnlAreaControlEdits(false);//

                PrePareTabPanel(pnlTopPageArea, pnlButtomPageArea, grdArea, btnSearchArea);
            }

            else if (tabName == "แขวง/ตำบล")
            {
                //txtDistrictCode.DisableTextBox(true);
                //txtDistrictName.DisableTextBox(true);

                PnlDistrictControlEdits(false);//


                PrePareTabPanel(pnlDistrictTopPage, pnlDistrictButtomPage, grdDistrict, btnSearchDistrict);
            }

        }

        private void btnFlagProvince_Click(object sender, EventArgs e)
        {
            chkFlagProvince.Checked = false;
        }

        private void txtProvinceCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txtAreaCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txtDistrictCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }
        
        private void btnRemove_Click(object sender, EventArgs e)
        {
            string cfMsg = "คุณแน่ใจหรือไม่ที่จะลบข้อมูลรายการนี้?";
            string title = "ทำการยืนยัน!!";

            if (!cfMsg.ConfirmMessageBox(title))
                return;

            int ret = 0;

            string tabName = TabPlace.SelectedTab.Text.ToString();

            if (tabName == "จังหวัด")
            {
                var tbl_MstProvinces = new tbl_MstProvince();

                string ProvinceID = grdProvince.CurrentRow.Cells["colProvinceID"].Value.ToString();

                if (!string.IsNullOrEmpty(ProvinceID))
                {
                    tbl_MstProvinces = PrePareMstProvince(true, ProvinceID);

                    ret = bu.UpdateData(tbl_MstProvinces);

                    if (ret == 1)
                    {
                        string msg = "บันทึกข้อมูลเรียบร้อยแล้ว !!";
                        msg.ShowInfoMessage();
                        btnSearchProvince.PerformClick();
                    }
                    else
                    {
                        this.ShowProcessErr();
                        return;
                    }
                }
                else
                {
                    string msg = "ไม่พบข้อมูลจังหวัด กรุณาเลือกจังหวัดที่ต้องการลบ !!";
                    msg.ShowWarningMessage();
                    return;
                }
            }
            else if (tabName == "อำเภอ/เขต")
            {
                tbl_MstArea tbl_MstAreas = new tbl_MstArea();

                string AreaID = grdArea.CurrentRow.Cells["colAreaID"].Value.ToString();

                if (!string.IsNullOrEmpty(AreaID))
                {
                    tbl_MstAreas = PrePareMstArea(true, AreaID);

                    ret = bu.UpdateData(tbl_MstAreas);

                    if (ret == 1)
                    {
                        string msg = "บันทึกข้อมูลเรียบร้อยแล้ว !!";
                        msg.ShowInfoMessage();
                        btnSearchArea.PerformClick();
                    }
                    else
                    {
                        this.ShowProcessErr();
                        return;
                    }
                }
                else
                {
                    string msg = "ไม่พบข้อมูลเขต กรุณาเลือกเขตที่ต้องการลบ !!";
                    msg.ShowWarningMessage();
                    return;
                }
            }
            else if (tabName == "แขวง/ตำบล")
            {
                var tbl_MstDistricts = new tbl_MstDistrict();

                string DistrictID = grdDistrict.CurrentRow.Cells["colDistrictID"].Value.ToString();

                if (!string.IsNullOrEmpty(DistrictID))
                {
                    tbl_MstDistricts = PrePareMstDistrict(true, DistrictID);

                    ret = bu.UpdateData(tbl_MstDistricts);

                    if (ret == 1)
                    {
                        string msg = "บันทึกข้อมูลเรียบร้อยแล้ว !!";
                        msg.ShowInfoMessage();
                        btnSearchDistrict.PerformClick();
                    }
                    else
                    {
                        this.ShowProcessErr();
                        return;
                    }
                }
                else
                {
                    string msg = "ไม่พบข้อมูลตำบล กรุณาเลือกตำบลที่ต้องการลบ !!";
                    msg.ShowWarningMessage();
                    return;
                }
            }
        }

        private void btnSearchArea_Click(object sender, EventArgs e)
        {
            BindArea();
        }

        private void txtSearchArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindArea();
            }

        }

        private void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindArea();
        }

        private void rdoAreaN_CheckedChanged(object sender, EventArgs e)
        {
            BindArea();
        }

        private void rdoAreaC_CheckedChanged(object sender, EventArgs e)
        {
            BindArea();
            btnRemove.Enabled = false;
        }
        private void SelectAreaDetails(DataGridViewCellEventArgs e)
        {
            if (e != null)
            {
                if (e.RowIndex == -1)
                    return;
            }

            DataGridViewRow gridrow = null;

            if (e != null)
                gridrow = grdArea.Rows[e.RowIndex];
            else
                gridrow = grdArea.CurrentRow;

            string AreaID = gridrow.Cells["colAreaID"].Value.ToString();

            foreach (DataRow r in dtArea.Rows)
            {
                if (r["AreaID"].ToString() == AreaID)
                {
                    txtAreaCode.Text = r["AreaCode"].ToString();
                    txtAreaName.Text = r["AreaName"].ToString();
                    txtPostalCode.Text = r["PostalCode"].ToString();
                    chkFlagArea.Checked = Convert.ToBoolean(r["FlagDel"]);
                    break;
                }
            }

        }
        private void grdArea_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectAreaDetails(e);
        }

        private void grdArea_SelectionChanged(object sender, EventArgs e)
        {
            SelectAreaDetails(null);
        }

        private void btnFlagArea_Click(object sender, EventArgs e)
        {
            chkFlagArea.Checked = false;
        }

        private void grdArea_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdArea.SetRowPostPaint(sender, e, this.Font);
        }

        private void txtPostalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void btnFlagDistrict_Click(object sender, EventArgs e)
        {
            chkFlagDistrict.Checked = false;
        }

        private void btnSearchDistrict_Click(object sender, EventArgs e)
        {
            BindDistrict();
        }

        private void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDistrict();
        }

        private void txtSearchDistrict_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindDistrict();
            }
        }

        private void rdoDistrictN_CheckedChanged(object sender, EventArgs e)
        {
            BindDistrict();
        }

        private void rdoDistrictC_CheckedChanged(object sender, EventArgs e)
        {
            BindDistrict();
            btnRemove.Enabled = false;
        }

        private void SelectDistrictDetails(DataGridViewCellEventArgs e)
        {
            if (e != null)
            {
                if (e.RowIndex == -1)
                    return;
            }

            DataGridViewRow gridrow = null;

            if (e != null)
                gridrow = grdDistrict.Rows[e.RowIndex];
            else
                gridrow = grdDistrict.CurrentRow;

            if (gridrow != null)
            {
                string DistrictID = gridrow.Cells["colDistrictID"].Value.ToString();

                foreach (DataRow r in dtDistrict.Rows)
                {
                    if (r["DistrictID"].ToString() == DistrictID)
                    {
                        txtDistrictCode.Text = r["DistrictCode"].ToString();
                        txtDistrictName.Text = r["DistrictName"].ToString();
                        chkFlagDistrict.Checked = Convert.ToBoolean(r["FlagDel"]);
                        break;
                    }
                }
            }
            

        }
        private void grdDistrict_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectDistrictDetails(e);
        }

        private void grdDistrict_SelectionChanged(object sender, EventArgs e)
        {
            SelectDistrictDetails(null);
        }

        private void grdDistrict_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdDistrict.SetRowPostPaint(sender, e, this.Font);
        }

        private void frmPlace_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
