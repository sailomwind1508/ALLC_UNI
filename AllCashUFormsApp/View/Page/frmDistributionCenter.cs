using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using System.Globalization;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmDistributionCenter : Form
    {
        SaleArea sa = new SaleArea();
        SaleAreaDistrict buSADistrict = new SaleAreaDistrict();
        MenuBU menuBU = new MenuBU();
        DistributionCenter bu = new DistributionCenter();

        DataTable dt = new DataTable();

        List<Control> saleEmpList = new List<Control>();
        List<Control> driverEmpList = new List<Control>();
        List<Control> helperEmpList = new List<Control>();

        List<tbl_SalArea> tbl_SalAreaList = new List<tbl_SalArea>();

        Dictionary<Control, Label> validateDepoCtrls = new Dictionary<Control, Label>();
        List<string> readOnlyDepoControls = new List<string>();

        Dictionary<Control, Label> validateEmpCtrls = new Dictionary<Control, Label>();
        List<string> readOnlyEmpControls = new List<string>();

        Dictionary<Control, Label> validateBWHCtrls = new Dictionary<Control, Label>();
        List<string> readOnlyBWHControls = new List<string>();

        Dictionary<Control, Label> validateVANCtrls = new Dictionary<Control, Label>();
        List<string> readOnlyVANDTControls = new List<string>();

        Dictionary<Control, Label> validateMKTCtrls = new Dictionary<Control, Label>();
        List<string> readOnlyMKTControls = new List<string>();

        static bool isEditList = false;

        public frmDistributionCenter()
        {
            InitializeComponent();

            #region Tab Depo

            readOnlyDepoControls = new string[] { txtPartID.Name }.ToList();

            validateDepoCtrls.Add(txtPartID, lblPartID);
            validateDepoCtrls.Add(txtPartSeq, lblPartSeq);
            validateDepoCtrls.Add(txtBranchID, lblBranchID);
            validateDepoCtrls.Add(txtBranchName, lblBranchName);
            validateDepoCtrls.Add(txtAddress, lblAddress);
            validateDepoCtrls.Add(txtAgentID, lblAgentID);
            validateDepoCtrls.Add(ddlPriceGroupID, lblPriceGroupID);
            validateDepoCtrls.Add(ddlBranchGroupID, lblBranchGroupID);
            validateDepoCtrls.Add(ddlProvinceID, lblProvince);
            validateDepoCtrls.Add(ddlAreaID, lblArea);
            validateDepoCtrls.Add(ddlDistrictID, lblDistrict);
            validateDepoCtrls.Add(txtTaxId, lblTaxId);

            validateDepoCtrls.Add(txtBranchRefCode, lblBranchRefCode);
            validateDepoCtrls.Add(txtSAPPlantID, lblSAPPlantID);

            validateDepoCtrls.Add(txtSystemType, lblSystemType);
            validateDepoCtrls.Add(txtBU, lblBU);
            validateDepoCtrls.Add(txtDC, lblDC);
            validateDepoCtrls.Add(txtPrefix1, lblPrefix1);
            validateDepoCtrls.Add(txtPrefix2, lblPrefix2);
            validateDepoCtrls.Add(txtState, lblState);
            validateDepoCtrls.Add(txtCountry, lblCountry);

            #endregion

            #region Tab Emp

            readOnlyEmpControls = new string[] { txtEmpID.Name }.ToList();

            validateEmpCtrls.Add(txtEmpID, lblEmpID);
            validateEmpCtrls.Add(ddlTitleName, lblTitleName);
            validateEmpCtrls.Add(txtFirstName, lblFirstName);
            validateEmpCtrls.Add(ddlDepartmentID, lblDepartmentID);
            validateEmpCtrls.Add(ddlPositionID, lblPositionID);
            validateEmpCtrls.Add(ddlDepo, lblDepo);
            validateEmpCtrls.Add(txtUsername, lblUsername);
            validateEmpCtrls.Add(txtPassword, lblPassword);
            validateEmpCtrls.Add(ddlRoleID, lblRoleID);

            #endregion

            #region Tab BHW

            readOnlyBWHControls = new List<string> { };

            validateBWHCtrls.Add(txtWHSeq, lblWHSeq);
            validateBWHCtrls.Add(txtWHCode, lblWHCode);
            validateBWHCtrls.Add(txtWHName, lblWHName);

            #endregion

            #region Tab VAN

            readOnlyVANDTControls = new List<string> { txtSaleEmpName.Name, txtDriverEmpName.Name, txtHelperEmpName.Name };

            validateVANCtrls.Add(txtWHCode_Van, lblWHCode_Van);
            validateVANCtrls.Add(txtWHName_Van, lblWHName_Van);
            validateVANCtrls.Add(ddlWHType_VanDT, lblWHType_VanDT);
            validateVANCtrls.Add(txtSaleEmpID, lblSaleEmpID);

            saleEmpList = new List<Control>() { txtSaleEmpID, txtSaleEmpName };
            driverEmpList = new List<Control>() { txtDriverEmpID, txtDriverEmpName };
            helperEmpList = new List<Control>() { txtHelperEmpID, txtHelperEmpName };

            #endregion

            #region Tab Market

            readOnlyMKTControls = new List<string> { txtSalAreaID.Name, txtSalAreaCode.Name, txtCountCustomer.Name };

            validateMKTCtrls.Add(txtSalAreaID, lblSalAreaID);
            validateMKTCtrls.Add(txtSalAreaCode, lblSalAreaCode);
            validateMKTCtrls.Add(txtSalAreaName, lblSalAreaName);
            validateMKTCtrls.Add(txtSeq, lblSeq);

            #endregion
        }

        #region Main

        private void InitPage()
        {
            var menu = bu.tbl_AdmFormList.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }

            btnAdd.Enabled = true;

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnCopy.Enabled = false;

            #region Initial Tab Depo

            grdDepo.DataSource = null;

            //panel36.ClearControl();

            //panel36.OpenControl(true, readOnlyDepoControls.ToArray());

            gbDepoAddOn.Visible = Helper.tbl_Users.RoleID == 5; //***********************

            var headerPic = bu.tbl_MstMenu.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            var partList = bu.GetAllMstPart();
            ddlPartID.BindDropdownList(partList, "PartName", "PartID", 0);
            InitPart();

            var priceGroupList = bu.GetAllPriceGroup();
            ddlPriceGroupID.BindDropdownList(priceGroupList, "PriceGroupName", "PriceGroupID");

            var branhGroupList = bu.GetAllBranchGroup();
            ddlBranchGroupID.BindDropdownList(branhGroupList, "BranchGroupName", "BranchGroupID");

            var proviceList = bu.GetMstProvince();
            ddlProvinceID.BindDropdownList(proviceList, "ProvinceName", "ProvinceID");

            Func<tbl_MstArea, bool> areaFunc = (x => x.ProvinceID.ToString() == ddlProvinceID.SelectedValue.ToString());
            var areaList = bu.GetMstArea(areaFunc);
            ddlAreaID.BindDropdownList(areaList, "AreaName", "AreaID", 0);

            Func<tbl_MstDistrict, bool> distFunc = (x => x.AreaID.ToString() == ddlAreaID.SelectedValue.ToString());
            var districtList = bu.GetMstDistrict(distFunc);
            ddlDistrictID.BindDropdownList(districtList, "DistrictName", "DistrictID", 0);

            //BindBranchData();

            grdDepo.SetDataGridViewStyle();

            pnlBranchDT.ClearControl();

            btnAdd.Enabled = true;
            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnCopy.Enabled = false;

            pnlBranchDT.OpenControl(false, readOnlyDepoControls.ToArray());

            #endregion

            MemoryManagement.FlushMemory();

            InitialTab_Employee();

            MemoryManagement.FlushMemory();

            InitialTab_BranchWarehouse();

            MemoryManagement.FlushMemory();

            InitialTab_Van();

            MemoryManagement.FlushMemory();

            InitialTab_MKT();

            MemoryManagement.FlushMemory();
        }

        private void InitPart()
        {
            GenBranchID(ddlPartID);
            rdoBranchC.Enabled = false;
            rdoBranchN.Enabled = false;
        }

        private void InitialTab_Employee()
        {
            var titleList = new Dictionary<string, string>();
            titleList = bu.GetAllTitleName();
            ddlTitleName.BindDropdownList(titleList, "key", "value");

            var departmentList = new List<tbl_Department>();
            var _departmentList = bu.GetAllDepartment();
            departmentList.Add(new tbl_Department { DepartmentID = -1, DepartmentName = "==เลือก==" });
            departmentList.AddRange(_departmentList);

            ddlDepartment.BindDropdownList(departmentList, "DepartmentName", "DepartmentID");

            departmentList = new List<tbl_Department>();
            departmentList.AddRange(_departmentList);
            ddlDepartmentID.BindDropdownList(departmentList, "DepartmentName", "DepartmentID");

            var positionList = new List<tbl_Position>();
            var _positionList = bu.GetAllPosition();
            positionList.Add(new tbl_Position { PositionID = -1, PositionName = "==เลือก==" });
            positionList.AddRange(_positionList);

            ddlPosition.BindDropdownList(positionList, "PositionName", "PositionID");

            positionList = new List<tbl_Position>();
            positionList.AddRange(_positionList);
            ddlPositionID.BindDropdownList(positionList, "PositionName", "PositionID");

            var rolesList = bu.GetAllRoles();
            ddlRoleID.BindDropdownList(rolesList, "RoleName", "RoleID");

            var branchList = bu.GetBranch();
            ddlDepo.BindDropdownList(branchList, "BranchName", "BranchID");

            grdEmpList.SetDataGridViewStyle();
            pnlEmpDT.ClearControl();
            pnlEmpDT.OpenControl(false, readOnlyDepoControls.ToArray());

            txtEmpID.Text = ddlDepo.SelectedValue + "Exxx";

            rdoEmpStatusN.Enabled = false;
            rdoEmpStatusC.Enabled = false;
        }

        private void InitialTab_BranchWarehouse()
        {
            grdBWHList.SetDataGridViewStyle();

            pnlBWH.ClearControl();
            pnlBWH.OpenControl(false, readOnlyDepoControls.ToArray());

            rdoBWHStatusN.Enabled = false;
            rdoBWHStatusC.Enabled = false;
        }

        private void InitialTab_Van()
        {
            PrepareCashVan();

            grdVanList.SetDataGridViewStyle();

            pnlVANDT.ClearControl();
            pnlVANDT.OpenControl(false, readOnlyVANDTControls.ToArray());

            rdoStatusVanN.Enabled = false;
            rdoStatusVanC.Enabled = false;
        }

        private void InitialTab_MKT()
        {
            var zoneList = new List<tbl_Zone>();
            var _zoneList = bu.GetZone();
            zoneList.Add(new tbl_Zone { ZoneID = -1, ZoneName = "==เลือก==" });
            zoneList.AddRange(_zoneList);
            ddlZone.BindDropdownList(zoneList, "ZoneName", "ZoneID", 0);

            zoneList = new List<tbl_Zone>();
            zoneList.Add(new tbl_Zone { ZoneID = -1, ZoneName = "==เลือก==" });
            zoneList.AddRange(_zoneList);
            ddlZoneID.BindDropdownList(zoneList, "ZoneName", "ZoneID", 0);

            var vanList = new List<tbl_BranchWarehouse>();
            Func<tbl_BranchWarehouse, bool> tbl_BranchWarehouseFunc = (x => x.VanType != 0);
            vanList.Add(new tbl_BranchWarehouse { WHID = "-1", WHName = "==เลือก==" });
            vanList.AddRange(bu.GetAllBranchWarehouse(tbl_BranchWarehouseFunc));
            ddlWHID.BindDropdownList(vanList, "WHName", "WHID");

            grdSaleAreaList.SetDataGridViewStyle();

            pnlMKT.ClearControl();

            pnlMKT.OpenControl(false, readOnlyMKTControls.ToArray());

            rdoMKTStatusN.Enabled = false;
            rdoMKTStatusC.Enabled = false;
        }

        private bool ValidateSave(string tabName)
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //validate header
            {
                if (tabName == tabDepo.Name)
                {
                    errList.SetErrMessage(validateDepoCtrls);
                }
                else if (tabName == tabEmp.Name)
                {
                    errList.SetErrMessage(validateEmpCtrls);

                    if (errList.Count == 0)
                    {
                        var fName = txtFirstName.Text;
                        var lName = txtLastName.Text;

                        if (!string.IsNullOrEmpty(fName))
                        {
                            Func<tbl_Employee, bool> tbl_EmployeeFunc = (x => x.FirstName == fName && x.LastName == lName);
                            var emps = bu.GetEmployee(tbl_EmployeeFunc);
                            if (emps != null && emps.Count > 0)
                            {
                                string message = "ชื่อ-นามสกุลซ้ำ กรุณาระบุใหม่ !!!";
                                message.ShowWarningMessage();
                                ret = false;
                                txtFirstName.ErrorTextBox();
                                txtFirstName.Focus();
                            }
                        }

                        if (ret)
                        {
                            Func<tbl_Users, bool> tbl_UsersFunc = (x => x.Username == txtUsername.Text);
                            var users = bu.GetUser(tbl_UsersFunc);
                            if (users != null && users.Count > 0)
                            {
                                string message = "ชื่อเข้าระบบซ้ำ กรุณาระบุใหม่ !!!";
                                message.ShowWarningMessage();
                                ret = false;
                                txtUsername.ErrorTextBox();
                                txtUsername.Focus();
                            }
                        }
                    }
                }
                else if (tabName == tabWarehouse.Name)
                {
                    errList.SetErrMessage(validateBWHCtrls);
                }
                else if (tabName == tabVan.Name)
                {
                    errList.SetErrMessage(validateVANCtrls);
                }
                else if (tabName == tabMarket.Name)
                {
                    errList.SetErrMessage(validateMKTCtrls);
                }

                if (errList.Count > 0)
                {
                    string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                    message.ShowWarningMessage();
                    ret = false;
                }
            }

            return ret;
        }

        private void frmDistributionCenter_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();

            MemoryManagement.FlushMemory();

            btnSearchBranch.PerformClick();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tctrlDistribution.SelectedTab == tabDepo)
            {
                pnlBranchDT.OpenControl(true, readOnlyDepoControls.ToArray());

                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnCopy.Enabled = false;
                pnlBranchDT.ClearControl();

                txtPartID.DisableTextBox(true);
                //txtBranchID.DisableTextBox(true);

                InitPart();

                rdoBranchN.Checked = true;
                rdoBranchC.Checked = false;
            }
            else if (tctrlDistribution.SelectedTab == tabEmp)
            {
                pnlEmpDT.OpenControl(true, readOnlyEmpControls.ToArray());

                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnCopy.Enabled = false;
                pnlEmpDT.ClearControl();

                PrepareEmpID();

                txtEmpID.DisableTextBox(true);

                rdoEmpStatusN.Checked = true;
                rdoEmpStatusC.Checked = false;

                rdoEmpStatusN.Enabled = false;
                rdoEmpStatusC.Enabled = false;
            }
            else if (tctrlDistribution.SelectedTab == tabWarehouse)
            {
                pnlBWH.OpenControl(true, readOnlyEmpControls.ToArray());

                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnCopy.Enabled = false;
                pnlBWH.ClearControl();

                txtWHCode.DisableTextBox(true);

                rdoBWHStatusN.Checked = true;
                rdoBWHStatusC.Checked = false;

                rdoBWHStatusN.Enabled = false;
                rdoBWHStatusC.Enabled = false;

                PrepareBWH();
            }
            else if (tctrlDistribution.SelectedTab == tabVan)
            {
                pnlVANDT.OpenControl(true, readOnlyVANDTControls.ToArray());

                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnCopy.Enabled = false;
                pnlVANDT.ClearControl();

                txtWHCode_Van.DisableTextBox(true);
                txtSaleEmpName.DisableTextBox(true);
                txtDriverEmpName.DisableTextBox(true);
                txtHelperEmpName.DisableTextBox(true);

                rdoStatusVanN.Checked = true;
                rdoStatusVanC.Checked = false;

                rdoStatusVanN.Enabled = false;
                rdoStatusVanC.Enabled = false;

                PrepareVan();
            }
            else if (tctrlDistribution.SelectedTab == tabMarket)
            {
                pnlMKT.OpenControl(true, readOnlyMKTControls.ToArray());

                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnCopy.Enabled = false;
                pnlMKT.ClearControl();

                txtSalAreaID.DisableTextBox(true);
                txtSalAreaCode.DisableTextBox(true);
                txtCountCustomer.DisableTextBox(true);

                rdoMKTStatusN.Checked = true;
                rdoMKTStatusC.Checked = false;

                rdoMKTStatusN.Enabled = false;
                rdoMKTStatusC.Enabled = false;

                PrepareMTK();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (tctrlDistribution.SelectedTab == tabDepo)
            {
                pnlBranchDT.OpenControl(true, readOnlyDepoControls.ToArray());
                btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
                btnCancel.Enabled = true;
                btnCopy.Enabled = false;
                //grdDepo.ClearSelection();
            }
            else if (tctrlDistribution.SelectedTab == tabEmp)
            {
                pnlEmpDT.OpenControl(true, readOnlyEmpControls.ToArray());
                btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
                btnCancel.Enabled = true;
                btnCopy.Enabled = false;
            }
            else if (tctrlDistribution.SelectedTab == tabWarehouse)
            {
                pnlBWH.OpenControl(true, readOnlyBWHControls.ToArray());
                btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
                btnCancel.Enabled = true;
                btnCopy.Enabled = false;

                txtWHCode.DisableTextBox(true);
            }
            else if (tctrlDistribution.SelectedTab == tabVan)
            {
                pnlVANDT.OpenControl(true, readOnlyVANDTControls.ToArray());
                btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
                btnCancel.Enabled = true;
                btnCopy.Enabled = false;

                txtWHCode_Van.DisableTextBox(true);
                txtSaleEmpName.DisableTextBox(true);
                txtDriverEmpName.DisableTextBox(true);
                txtHelperEmpName.DisableTextBox(true);
            }
            else if (tctrlDistribution.SelectedTab == tabMarket)
            {
                pnlMKT.OpenControl(true, readOnlyMKTControls.ToArray());
                btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
                btnCancel.Enabled = true;
                btnCopy.Enabled = false;

                txtSalAreaID.DisableTextBox(true);
                txtSalAreaCode.DisableTextBox(true);
                txtCountCustomer.DisableTextBox(true);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (tctrlDistribution.SelectedTab == tabDepo)
            {
                int rowIndex = grdDepo.CurrentCell.RowIndex;
                if (rowIndex != -1)
                {
                    var code = grdDepo.Rows[rowIndex].Cells[0].Value.ToString();
                    RemoveBranch(code);

                    pnlBranchDT.ClearControl();

                    txtPartID.DisableTextBox(true);
                }
            }
            else if (tctrlDistribution.SelectedTab == tabEmp)
            {
                int rowIndex = grdEmpList.CurrentCell.RowIndex;
                if (rowIndex != -1)
                {
                    var code = grdEmpList.Rows[rowIndex].Cells[0].Value.ToString();
                    RemoveEmployee(code);

                    pnlEmpDT.ClearControl();

                    txtEmpID.DisableTextBox(true);
                }
            }
            else if (tctrlDistribution.SelectedTab == tabWarehouse)
            {
                int rowIndex = grdBWHList.CurrentCell.RowIndex;
                if (rowIndex != -1)
                {
                    var code = grdBWHList.Rows[rowIndex].Cells[1].Value.ToString();
                    RemoveBranchWarehouse(code);

                    pnlBWH.ClearControl();

                    txtWHCode.DisableTextBox(true);
                }
            }
            else if (tctrlDistribution.SelectedTab == tabVan)
            {
                int rowIndex = grdVanList.CurrentCell.RowIndex;
                if (rowIndex != -1)
                {
                    var code = grdVanList.Rows[rowIndex].Cells[0].Value.ToString();
                    RemoveVan(code);

                    pnlVANDT.ClearControl();

                    txtWHCode_Van.DisableTextBox(true);
                    txtSaleEmpName.DisableTextBox(true);
                    txtDriverEmpName.DisableTextBox(true);
                    txtHelperEmpName.DisableTextBox(true);
                }
            }
            else if (tctrlDistribution.SelectedTab == tabMarket)
            {
                int rowIndex = grdSaleAreaList.CurrentCell.RowIndex;
                if (rowIndex != -1)
                {
                    var code = grdSaleAreaList.Rows[rowIndex].Cells[0].Value.ToString();
                    RemoveMKT(code);

                    pnlMKT.ClearControl();

                    txtSalAreaID.DisableTextBox(true);
                    txtSalAreaCode.DisableTextBox(true);
                    txtCountCustomer.DisableTextBox(true);
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tctrlDistribution.SelectedTab == tabDepo)
            {
                SaveBranch();
            }
            else if (tctrlDistribution.SelectedTab == tabEmp)
            {
                SaveEmployee();
            }
            else if (tctrlDistribution.SelectedTab == tabWarehouse)
            {
                SaveBranchWarehouse();
            }
            else if (tctrlDistribution.SelectedTab == tabVan)
            {
                SaveVan();
            }
            else if (tctrlDistribution.SelectedTab == tabMarket)
            {
                //SaveMKT();
                SaveTabMarketing(); //03-08-2022 adisorn update with store
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (tctrlDistribution.SelectedTab == tabDepo)
            {
                pnlBranchDT.ClearControl();

                btnAdd.Enabled = true;
                this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
                btnCopy.Enabled = false;

                pnlBranchDT.OpenControl(false, readOnlyDepoControls.ToArray());

                InitPart();
            }
            else if (tctrlDistribution.SelectedTab == tabEmp)
            {
                pnlEmpDT.ClearControl();

                btnAdd.Enabled = true;
                this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
                btnCopy.Enabled = false;

                pnlEmpDT.OpenControl(false, readOnlyEmpControls.ToArray());

                PrepareEmpID();
            }
            else if (tctrlDistribution.SelectedTab == tabWarehouse)
            {
                pnlBWH.ClearControl();

                btnAdd.Enabled = true;
                this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
                btnCopy.Enabled = false;

                pnlBWH.OpenControl(false, readOnlyBWHControls.ToArray());

                //PrepareEmpID();
            }
            else if (tctrlDistribution.SelectedTab == tabVan)
            {
                pnlVANDT.ClearControl();

                btnAdd.Enabled = true;
                this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
                btnCopy.Enabled = false;

                pnlVANDT.OpenControl(false, readOnlyVANDTControls.ToArray());

                //PrepareEmpID();
            }
            else if (tctrlDistribution.SelectedTab == tabMarket)
            {
                pnlMKT.ClearControl();

                btnAdd.Enabled = true;
                this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
                btnCopy.Enabled = false;

                pnlMKT.OpenControl(false, readOnlyMKTControls.ToArray());

                //PrepareEmpID();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tctrlDistribution_TabIndexChanged(object sender, EventArgs e)
        {
            //btnCancel.PerformClick();
        }

        private void tctrlDistribution_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            btnCancel.Enabled = false;

            if (tctrlDistribution.SelectedTab == tabDepo)
            {
                pnlBranchDT.OpenControl(false, readOnlyDepoControls.ToArray());
            }
            else if (tctrlDistribution.SelectedTab == tabEmp)
            {
                btnSearchEmp.PerformClick();
                pnlEmpDT.OpenControl(false, readOnlyEmpControls.ToArray());
            }
            else if (tctrlDistribution.SelectedTab == tabWarehouse)
            {
                btnSearchWarehouse.PerformClick();
                pnlBWH.OpenControl(false, readOnlyBWHControls.ToArray());
            }
            else if (tctrlDistribution.SelectedTab == tabVan)
            {
                btnSearchVan.PerformClick();
                pnlVANDT.OpenControl(false, readOnlyVANDTControls.ToArray());
            }
            else if (tctrlDistribution.SelectedTab == tabMarket)
            {
                btnSearchMKT.PerformClick();
                pnlMKT.OpenControl(false, readOnlyMKTControls.ToArray());
            }
        }

        #endregion

        #region Tab Depo

        private void RemoveBranch(string branchID)
        {
            try
            {
                int ret = 0;

                string cfMsg = "ต้องการลบข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการลบ!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Branch b = new Branch();
                Func<tbl_Branch, bool> tbl_BranchFunc = (x => x.BranchID == branchID);
                var tbl_BranchList = bu.GetBranch(tbl_BranchFunc);
                if (tbl_BranchList != null && tbl_BranchList.Count > 0)
                {
                    tbl_Branch bData = new tbl_Branch();
                    bData = tbl_BranchList[0];
                    //bData.FlagDel = true;

                    ret = b.RemoveData(bData);

                    if (ret == 1)
                    {
                        string msg = "ลบข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();

                        InitPage();
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
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void SaveBranch()
        {
            try
            {
                int ret = 0;

                if (!ValidateSave(tabDepo.Name))
                    return;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Branch b = new Branch();
                tbl_Branch bData = new tbl_Branch();

                pnlBranchDT.Controls.SetObjectFromControl(bData);
                bool isEditMode = bData.CheckExistsData(bData.BranchCode);

                bData.BranchCode = bData.BranchID;
                var currentDate = DateTime.Now;
                bData.CrDate = currentDate;
                bData.CrUser = Helper.tbl_Users.Username;
                bData.EdDate = null;
                bData.EdUser = null;

                if (isEditMode)
                {
                    bData.EdDate = DateTime.Now;
                    bData.EdUser = Helper.tbl_Users.Username;
                }

                bData.SalAreaID = 0;
                bData.CloseTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0).TimeOfDay;

                bData.FlagSend = false;
                if (rdoBranchN.Checked)
                    bData.FlagDel = false;
                else if (rdoBranchC.Checked)
                    bData.FlagDel = true;

                ret = b.UpdateData(bData);

                if (ret == 1)
                {
                    pnlBranchDT.OpenControl(false, readOnlyDepoControls.ToArray());

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, "");
                    btnAdd.Enabled = true;
                    btnCopy.Enabled = false;

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
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void BindBranchData()
        {
            string text = txtSearchBranch.Text;
            Func<tbl_Branch, bool> tbl_BranchFunc = (x => x.FlagDel == rdoCancel.Checked && (x.BranchCode.Contains(text) || x.BranchName.Contains(text)));

            var dt = bu.GetDepoTable(tbl_BranchFunc);

            if (dt != null && dt.Rows.Count > 0)
            {
                var grd = grdDepo;
                grd.DataSource = dt;

                DataGridViewColumn col0 = grd.Columns[0];
                DataGridViewColumn col1 = grd.Columns[1];
                DataGridViewColumn col2 = grd.Columns[2];
                DataGridViewColumn col3 = grd.Columns[3];
                DataGridViewColumn col4 = grd.Columns[4];
                DataGridViewColumn col5 = grd.Columns[5];
                DataGridViewColumn col6 = grd.Columns[6];
                DataGridViewColumn col7 = grd.Columns[7];

                col0.SetColumnStyle(80, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col1.SetColumnStyle(160, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col2.SetColumnStyle(80, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col3.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col4.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col5.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col6.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col7.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.Fill, DataGridViewContentAlignment.MiddleLeft, "", 100);
            }
            else
            {
                var grd = grdDepo;
                grd.DataSource = null;

                btnCancel.PerformClick();
            }
        }

        private void BindBranchDetail()
        {
            if (grdDepo != null && grdDepo.RowCount > 0 && grdDepo.CurrentCell != null)
            {
                int rowIndex = grdDepo.CurrentCell.RowIndex;
                if (rowIndex != -1)  //(e.RowIndex != -1)
                {
                    var branchCode = grdDepo.Rows[rowIndex].Cells[0].Value.ToString();

                    Func<tbl_SalArea, bool> tbl_SalAreaFunc = (x => x.BranchID == branchCode);
                    tbl_SalAreaList = bu.GetSaleArea(tbl_SalAreaFunc);

                    var branchData = bu.GetBranch().FirstOrDefault(x => x.BranchCode == branchCode);
                    if (branchData != null)
                    {
                        rdoBranchN.Checked = branchData.FlagDel == false;
                        rdoBranchC.Checked = branchData.FlagDel == true;

                        pnlBranchDT.Controls.SetTextBoxControlValue(branchData);

                        ddlPartID.SelectedValue = branchData.PartID;
                        ddlPriceGroupID.SelectedValue = branchData.PriceGroupID;
                        ddlBranchGroupID.SelectedValue = branchData.BranchGroupID;
                        ddlProvinceID.SelectedValue = branchData.ProvinceID;
                        ddlAreaID.SelectedValue = branchData.AreaID;
                        ddlDistrictID.SelectedValue = branchData.DistrictID;

                        pnlBranchDT.OpenControl(false, readOnlyDepoControls.ToArray());

                        btnSearchBranch.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
                        btnAdd.Enabled = true;
                        btnCopy.Enabled = false;
                        btnRemove.Enabled = true;

                        btnRemove.Enabled = !branchData.FlagDel;
                    }
                }
            }
        }

        private void btnSearchBranch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            BindBranchData();
            BindBranchDetail();

            Cursor.Current = Cursors.Default;
        }

        private void grdDepo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdDepo.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdDepo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BindBranchDetail();
        }

        private void grdDepo_SelectionChanged(object sender, EventArgs e)
        {
            //BindBranchDetail();
        }

        private void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvinceID.SelectedValue != null)
            {
                int _provinceID = 0;

                try
                {
                    _provinceID = ((tbl_MstProvince)ddlProvinceID.SelectedValue).ProvinceID;
                }
                catch
                {
                    _provinceID = Convert.ToInt32(ddlProvinceID.SelectedValue);
                }

                Func<tbl_MstArea, bool> areaFunc = (x => x.ProvinceID == _provinceID);
                var areaList = bu.GetMstArea(areaFunc);
                ddlAreaID.BindDropdownList(areaList, "AreaName", "AreaID", 0);
            }
        }

        private void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAreaID.SelectedValue != null)
            {
                int _areaID = 0;
                try
                {
                    _areaID = ((tbl_MstArea)ddlAreaID.SelectedValue).AreaID;
                }
                catch
                {
                    _areaID = Convert.ToInt32(ddlAreaID.SelectedValue);
                }

                Func<tbl_MstDistrict, bool> distFunc = (x => x.AreaID == _areaID);
                var districtList = bu.GetMstDistrict(distFunc);
                ddlDistrictID.BindDropdownList(districtList, "DistrictName", "DistrictID", 0);
            }
        }

        private void rdoCancel_CheckedChanged(object sender, EventArgs e)
        {
            //BindBranchData();
        }

        private void rdoNormal_CheckedChanged(object sender, EventArgs e)
        {
            //BindBranchData();
        }

        private void txtSearchBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindBranchData();
            }
        }

        private void txtPartID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.ToNumberOnly(sender);
        }

        private void txtPartSeq_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.ToNumberOnly(sender);
        }

        private void txtBranchID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.ToNumberOnly(sender);
        }

        private void txtBU_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.ToNumberOnly(sender);
        }

        private void txtDC_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.ToNumberOnly(sender);
        }

        private void txtRunInit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.ToNumberOnly(sender);
        }

        private void ddlPartID_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ddl = sender as ComboBox;
            GenBranchID(ddl);
        }

        private void txtPartSeq_KeyDown(object sender, KeyEventArgs e)
        {
            txtBranchID.Text = txtPartID.Text + txtPartSeq.Text;
        }

        private void GenBranchID(ComboBox ddl)
        {
            int seq = 0;
            var b = bu.GetBranch();
            if (b != null && b.Count > 0)
            {
                seq = b.Max(x => Convert.ToInt32(x.PartSeq));
                txtPartSeq.Text = seq.ToString().Length < 2 ? "0" + seq.ToString() : seq.ToString();
            }

            txtPartID.Text = ddl.SelectedValue.ToString();
            txtBranchID.Text = txtPartID.Text + txtPartSeq.Text;
        }

        #endregion

        #region Tab Emp

        private void PrepareEmpID()
        {
            string _empID = "";
            if (grdDepo.CurrentCell != null)
            {
                int rowIndex = grdDepo.CurrentCell.RowIndex;
                if (rowIndex != -1)
                {

                    var code = grdDepo.Rows[rowIndex].Cells[0].Value.ToString();

                    Func<tbl_Employee, bool> tbl_EmployeeFunc = null;
                    var tbl_EmployeeList = bu.GetEmployee(tbl_EmployeeFunc);
                    if (tbl_EmployeeList != null && tbl_EmployeeList.Count > 0)
                    {
                        string _no = tbl_EmployeeList.Max(x => x.EmpID);
                        var no = Convert.ToInt32(_no.Substring(4, _no.Length - 4)) + 1;
                        _empID = code + "E" + no.ToString("000");
                    }
                    else
                        _empID = code + "E001";
                }
            }
            else
            {
                _empID = ddlDepo.SelectedValue + "E001";
            }

            txtEmpID.Text = _empID;
        }

        private void SaveEmployee()
        {
            try
            {
                int ret = 0;
                tbl_Employee eData = new tbl_Employee();

                bool isEditMode = eData.CheckExistsData(txtEmpID.Text);

                if (!isEditMode && !ValidateSave(tabEmp.Name))
                    return;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                //add employee
                Employee e = new Employee();

                pnlEmpDT.Controls.SetObjectFromControl(eData);

                if (isEditMode)
                {
                    eData.EmpID = txtEmpID.Text;
                }
                else
                {
                    Func<tbl_Employee, bool> tbl_EmployeeFunc = null;
                    var allEmp = bu.GetEmployee(tbl_EmployeeFunc);
                    if (allEmp != null && allEmp.Count > 0)
                    {
                        string _maxEmpID = allEmp.Max(x => x.EmpID);
                        var maxEmpID = Convert.ToInt32(_maxEmpID.Substring(4, _maxEmpID.Length - 4)) + 1;

                        string formateRunNo = "";
                        for (int i = 0; i < 3; i++)
                        {
                            formateRunNo += "0";
                        }
                        string tempRunningNo = maxEmpID.ToString(formateRunNo);

                        eData.EmpID = ddlDepo.SelectedValue + "E" + tempRunningNo;
                    }
                    else
                    {
                        eData.EmpID = ddlDepo.SelectedValue + "E000";

                        //case new document and no have empid Last edit by sailom .k 01/07/2022---------------------------------------
                        var _user = bu.GetUser().FirstOrDefault(x => x.Username == Helper.tbl_Users.Username);
                        if (_user != null)
                        {
                            eData.EmpID = _user.EmpID;
                        }
                        //case new document and no have empid Last edit by sailom .k 01/07/2022---------------------------------------
                    }
                }

                eData.RoleID = Convert.ToInt32(ddlRoleID.SelectedValue); //Adisorn 22/12/2564

                eData.LastName = string.IsNullOrEmpty(txtLastName.Text) ? "" : txtLastName.Text;
                eData.NickName = "";
                eData.EmpCode = eData.EmpID;
                eData.EmpIDCard = txtEmp_ID_Card.Text;
                eData.IDCard = txtIDCard.Text;

                var currentDate = DateTime.Now;
                eData.CrDate = currentDate;
                eData.CrUser = Helper.tbl_Users.Username;
                eData.EdDate = null;
                eData.EdUser = null;

                if (isEditMode)
                {
                    eData.EdDate = DateTime.Now;
                    eData.EdUser = Helper.tbl_Users.Username;
                }

                eData.FlagSend = false;

                if (rdoEmpStatusN.Checked)
                    eData.FlagDel = false;
                else if (rdoEmpStatusC.Checked)
                    eData.FlagDel = true;

                ret = e.UpdateData(eData);

                if (ret == 1) //add user
                {
                    Users u = new Users();
                    tbl_Users uData = new tbl_Users();

                    isEditMode = uData.CheckExistsData(txtUsername.Text);

                    if (isEditMode)
                    {
                        Func<tbl_Users, bool> tbl_UsersFunc = (x => x.Username == txtUsername.Text);
                        var userList = bu.GetUser(tbl_UsersFunc);
                        if (userList != null && userList.Count > 0)
                        {
                            uData = userList[0];
                            uData.FlagDel = rdoEmpStatusC.Checked;
                            uData.EdDate = DateTime.Now;
                            uData.EdUser = Helper.tbl_Users.Username;
                        }
                    }
                    else
                    {
                        gbUsers.Controls.SetObjectFromControl(uData);

                        uData.FirstName = eData.FirstName;
                        uData.LastName = eData.LastName;
                        uData.Email = "";
                        uData.EmpID = eData.EmpID;

                        uData.CrDate = currentDate;
                        uData.CrUser = Helper.tbl_Users.Username;
                        uData.EdDate = null;
                        uData.EdUser = null;

                        uData.FlagSend = false;

                        if (rdoEmpStatusN.Checked)
                            uData.FlagDel = false;
                        else if (rdoEmpStatusC.Checked)
                            uData.FlagDel = true;
                    }

                    uData.RoleID = Convert.ToInt32(ddlRoleID.SelectedValue); //Edit By Adisorn 22/12/2564

                    uData.Username = txtUsername.Text; //Edit By Adisorn 20/12/2564 
                    uData.Password = txtPassword.Text; //เก็บ UserID และ Pass เมื่อเพิ่มหรือแก้ไข ข้อมูลพนักงาน

                    ret = u.UpdateData(uData);
                }

                if (ret == 1)
                {
                    pnlEmpDT.OpenControl(false, readOnlyEmpControls.ToArray());

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, "");
                    btnAdd.Enabled = true;
                    btnCopy.Enabled = false;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    btnSearchEmp.PerformClick();
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

        private void RemoveEmployee(string empID)
        {
            try
            {
                int ret = 0;

                string cfMsg = "ต้องการลบข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการลบ!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Employee b = new Employee();
                Func<tbl_Employee, bool> tbl_EmployeeFunc = (x => x.EmpID == empID);
                var tbl_EmployeeList = bu.GetEmployee(tbl_EmployeeFunc);
                if (tbl_EmployeeList != null && tbl_EmployeeList.Count > 0)
                {
                    tbl_Employee eData = new tbl_Employee();
                    eData = tbl_EmployeeList[0];
                    eData.FlagDel = true;

                    ret = b.UpdateData(eData);

                    if (ret == 1)
                    {
                        Users u = new Users();
                        Func<tbl_Users, bool> tbl_UsersFunc = (x => x.EmpID == empID);
                        var tbl_UsersList = bu.GetUser(tbl_UsersFunc);
                        if (tbl_UsersList != null && tbl_UsersList.Count > 0)
                        {
                            tbl_Users uData = new tbl_Users();
                            uData = tbl_UsersList[0];
                            uData.FlagDel = true;

                            ret = u.UpdateData(uData);
                        }
                    }

                    if (ret == 1)
                    {
                        string msg = "ลบข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();

                        btnSearchEmp.PerformClick();
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
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        //private void BindEmployeeData()
        //{
        //    string text = txtSearchEmp.Text;

        //    Func<tbl_Employee, bool> tbl_EmployeeFunc = null;
        //    if (ddlDepartment.SelectedValue != null && ddlDepartment.SelectedValue.ToString() == "-1")
        //    {
        //        tbl_EmployeeFunc = (x => x.FlagDel == rdoEmpC.Checked &&
        //        x.PositionID == (Convert.ToInt32(ddlPosition.SelectedValue) != -1 ? Convert.ToInt32(ddlPosition.SelectedValue) : x.PositionID) &&
        //        (x.EmpCode.Contains(text) || x.TitleName.Contains(text) || x.FirstName.Contains(text) || x.LastName.Contains(text)));
        //    }
        //    else if (ddlPosition.SelectedValue != null && ddlPosition.SelectedValue.ToString() == "-1")
        //    {
        //        tbl_EmployeeFunc = (x => x.FlagDel == rdoEmpC.Checked &&
        //        x.DepartmentID == (Convert.ToInt32(ddlDepartment.SelectedValue) != -1 ? Convert.ToInt32(ddlDepartment.SelectedValue) : x.DepartmentID) &&
        //        (x.EmpCode.Contains(text) || x.TitleName.Contains(text) || x.FirstName.Contains(text) || x.LastName.Contains(text)));
        //    }
        //    else
        //    {
        //        tbl_EmployeeFunc = (x => x.FlagDel == rdoEmpC.Checked &&
        //        x.DepartmentID == (Convert.ToInt32(ddlDepartment.SelectedValue) != -1 ? Convert.ToInt32(ddlDepartment.SelectedValue) : x.DepartmentID) &&
        //        x.PositionID == (Convert.ToInt32(ddlPosition.SelectedValue) != -1 ? Convert.ToInt32(ddlPosition.SelectedValue) : x.PositionID) &&
        //        (x.EmpCode.Contains(text) || x.TitleName.Contains(text) || x.FirstName.Contains(text) || x.LastName.Contains(text)));
        //    }

        //    var dt = bu.GetEmpTable(tbl_EmployeeFunc);

        //    if (dt != null)
        //    {
        //        grdEmpList.DataSource = null;

        //        var grd = grdEmpList;
        //        grd.DataSource = dt;

        //        DataGridViewColumn col0 = grd.Columns[0];
        //        DataGridViewColumn col1 = grd.Columns[1];
        //        DataGridViewColumn col2 = grd.Columns[2];
        //        DataGridViewColumn col3 = grd.Columns[3];
        //        DataGridViewColumn col4 = grd.Columns[4];
        //        DataGridViewColumn col5 = grd.Columns[5];
        //        DataGridViewColumn col6 = grd.Columns[6];
        //        DataGridViewColumn col7 = grd.Columns[7];
        //        DataGridViewColumn col8 = grd.Columns[8];
        //        DataGridViewColumn col9 = grd.Columns[9];
        //        DataGridViewColumn col10 = grd.Columns[10];

        //        col0.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
        //        col1.SetColumnStyle(140, DataGridViewAutoSizeColumnMode.Fill, DataGridViewContentAlignment.MiddleLeft, "", 140);
        //        col2.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
        //        col3.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
        //        col4.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
        //        col5.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
        //        col6.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0);
        //        col7.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
        //        col8.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0);
        //        col9.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
        //        col10.SetColumnStyle(60, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0, new DataGridViewCheckBoxColumn());

        //        lblEmpCount.Text = dt.Rows.Count.ToNumberFormat();
        //    }
        //}

        private void BindEmployeeData_V2()
        {
            int _DepartmentID = ddlDepartment.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlDepartment.SelectedValue);
            int _PositionID = ddlPosition.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlPosition.SelectedValue);
            int _flagDel = rdoEmpN.Checked ? 0 : 1;

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@flagDel", _flagDel);
            _params.Add("@DepartmentID", _DepartmentID);
            _params.Add("@PositionID", _PositionID);
            _params.Add("@Search", txtSearchEmp.Text);

            var dt = bu.proc_GetEmployee_Data(_params);

            if (dt != null)
            {
                grdEmpList.DataSource = null;

                var grd = grdEmpList;
                grd.DataSource = dt;

                DataGridViewColumn col0 = grd.Columns[0];
                DataGridViewColumn col1 = grd.Columns[1];
                DataGridViewColumn col2 = grd.Columns[2];
                DataGridViewColumn col3 = grd.Columns[3];
                DataGridViewColumn col4 = grd.Columns[4];
                DataGridViewColumn col5 = grd.Columns[5];
                DataGridViewColumn col6 = grd.Columns[6];
                DataGridViewColumn col7 = grd.Columns[7];
                DataGridViewColumn col8 = grd.Columns[8];
                DataGridViewColumn col9 = grd.Columns[9];
                DataGridViewColumn col10 = grd.Columns[10];

                col0.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col1.SetColumnStyle(140, DataGridViewAutoSizeColumnMode.Fill, DataGridViewContentAlignment.MiddleLeft, "", 140);
                col2.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col3.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col4.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col5.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col6.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0);
                col7.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col8.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0);
                col9.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col10.SetColumnStyle(60, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0, new DataGridViewCheckBoxColumn());

                lblEmpCount.Text = dt.Rows.Count.ToNumberFormat();
            }
            SelectEmployeeDetail(null);
        }

        private void BindEmployeeDetail()
        {
            //if (grdEmpList.RowCount > 0 && grdEmpList.CurrentCell != null)
            //{
            //    int rowIndex = grdEmpList.CurrentCell.RowIndex;

            //    if (rowIndex != -1)
            //    {
            //        var empCode = grdEmpList.Rows[rowIndex].Cells[0].Value.ToString();
            //        Func<tbl_Employee, bool> tbl_EmployeeFunc = (x => x.EmpCode == empCode);
            //        var empDT = bu.GetEmpDetails(tbl_EmployeeFunc);

            //        if (empDT != null && empDT.Count > 0)
            //        {
            //            rdoEmpStatusN.Checked = empDT[0].FlagDel == false;
            //            rdoEmpStatusC.Checked = empDT[0].FlagDel == true;

            //            pnlEmpDT.Controls.SetTextBoxControlValue(empDT[0]);

            //            ddlDepartmentID.SelectedValue = empDT[0].DepartmentID;
            //            ddlPositionID.SelectedValue = empDT[0].PositionID;

            //            if (ddlDepo.Items != null && ddlDepo.Items.Count > 0)
            //                ddlDepo.SelectedIndex = 0;

            //            ddlRoleID.SelectedValue = empDT[0].RoleID;
            //            ddlTitleName.SelectedValue = empDT[0].TitleName;

            //            pnlEmpDT.OpenControl(false, readOnlyEmpControls.ToArray());

            //            btnSearchBranch.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
            //            btnAdd.Enabled = true;
            //            btnCopy.Enabled = false;
            //            btnRemove.Enabled = true;

            //            btnRemove.Enabled = !empDT[0].FlagDel;
            //        }
            //    }
            //}
        }

        private void SelectEmployeeDetail(DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow grdRows = null;

                if (e != null)
                {
                    if (e.RowIndex == -1)
                        return;
                    else
                        grdRows = grdEmpList.Rows[e.RowIndex];
                }
                else
                {
                    grdRows = grdEmpList.CurrentRow;
                }

                if (grdRows != null)
                {
                    string _EmpCode = grdRows.Cells[0].Value.ToString();

                    int _DepartmentID = ddlDepartment.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlDepartment.SelectedValue);
                    int _PositionID = ddlPosition.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlPosition.SelectedValue);
                    int _flagDel = rdoEmpN.Checked ? 0 : 1;

                    Dictionary<string, object> _params = new Dictionary<string, object>();
                    _params.Add("@flagDel", _flagDel);
                    _params.Add("@DepartmentID", _DepartmentID);
                    _params.Add("@PositionID", _PositionID);
                    _params.Add("@Search", txtSearchEmp.Text);

                    var dtEmployee = bu.proc_GetEmployee_Data(_params, true);

                    DataRow r = dtEmployee.AsEnumerable().FirstOrDefault(x => x.Field<string>("EmpCode") == _EmpCode);

                    if (r != null)
                    {
                        bool flagDel = Convert.ToBoolean(r["FlagDel"]);
                        rdoEmpStatusN.Checked = flagDel == false;
                        rdoEmpStatusC.Checked = flagDel == true;

                        txtEmpID.Text = r["EmpCode"].ToString();
                        txtEmp_ID_Card.Text = r["EmpIDCard"].ToString(); //รหัสบัญชี
                        txtIDCard.Text = r["IDCard"].ToString();
                        ddlTitleName.SelectedValue = r["TitleName"].ToString();
                        txtFirstName.Text = r["FirstName"].ToString();
                        txtLastName.Text = r["LastName"].ToString();

                        ddlDepartmentID.SelectedValue = Convert.ToInt32(r["DepartmentID"]);
                        ddlPositionID.SelectedValue = Convert.ToInt32(r["PositionID"]);

                        if (ddlDepo.Items != null && ddlDepo.Items.Count > 0)
                            ddlDepo.SelectedIndex = 0;

                        txtUsername.Text = r["Username"].ToString();
                        txtPassword.Text = r["Password"].ToString();

                        ddlRoleID.SelectedValue = Convert.ToInt32(r["RoleID"]);

                        pnlEmpDT.OpenControl(false, readOnlyEmpControls.ToArray());

                        btnSearchBranch.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
                        btnAdd.Enabled = true;
                        btnCopy.Enabled = false;
                        btnRemove.Enabled = true;

                        btnRemove.Enabled = !flagDel;
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }

            
        }

        private void grdEmpList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectEmployeeDetail(e);
        }

        private void grdEmpList_SelectionChanged(object sender, EventArgs e)
        {
            //BindEmployeeDetail();
        }

        private void grdEmpList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdEmpList.SetRowPostPaint(sender, e, this.Font);
        }

        private void rdoEmpN_CheckedChanged(object sender, EventArgs e)
        {
            //BindEmployeeData();
        }

        private void rdoEmpC_CheckedChanged(object sender, EventArgs e)
        {
            //BindEmployeeData();
        }

        private void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindEmployeeData();
            BindEmployeeData_V2(); //Edit By Adisorn 20/12/2564
        }

        private void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindEmployeeData();
            BindEmployeeData_V2(); //Edit By Adisorn 20/12/2564
        }

        private void btnSearchEmp_Click(object sender, EventArgs e)
        {
            //BindEmployeeData();
            BindEmployeeData_V2(); //Edit By Adisorn 20/12/2564
           
        }

        private void txtSearchEmp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                //BindEmployeeData();
                BindEmployeeData_V2(); //Edit By Adisorn 20/12/2564
        }

        #endregion

        #region Tab Branch WareHouse

        private void PrepareBWH()
        {
            int rowIndex = grdDepo.CurrentCell.RowIndex;
            if (rowIndex != -1)
            {
                var code = grdDepo.Rows[rowIndex].Cells[0].Value.ToString();
                var name = grdDepo.Rows[rowIndex].Cells[1].Value.ToString();

                string _whCode = "";
                string _whSeq = "";
                string _whName = "";

                Func<tbl_BranchWarehouse, bool> tbl_BranchWarehouseFunc = (x => x.VanType == 0 && x.BranchID == code && x.WHCode.Contains(code + "D"));
                var tbl_BranchWarehouseList = bu.GetAllBranchWarehouse(tbl_BranchWarehouseFunc);
                if (tbl_BranchWarehouseList != null && tbl_BranchWarehouseList.Count > 0)
                {
                    string _no = tbl_BranchWarehouseList.Max(x => x.WHCode);
                    var no = Convert.ToInt32(_no.Substring(4, _no.Length - 4)) + 1;

                    string formateRunNo = "";
                    for (int i = 0; i < 2; i++)
                    {
                        formateRunNo += "0";
                    }
                    string tempRunningNo = no.ToString(formateRunNo);

                    _whSeq = "20" + tempRunningNo;
                    _whCode = code + "D" + tempRunningNo;
                    _whName = "จุด Drop " + no + " - " + name;
                }
                else
                {
                    _whSeq = "2001";
                    _whCode = code + "D01";
                    _whName = "จุด Drop " + 1 + " - " + name;
                }

                txtWHSeq.Text = _whSeq;
                txtWHCode.Text = _whCode;
                txtWHName.Text = _whName;
            }
        }

        private void SaveBranchWarehouse()
        {
            try
            {
                int ret = 0;
                tbl_BranchWarehouse bwData = new tbl_BranchWarehouse();

                bool isEditMode = bwData.CheckExistsData(txtWHCode.Text);

                if (!isEditMode && !ValidateSave(tabWarehouse.Name))
                    return;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                BranchWarehouse bw = new BranchWarehouse();
                if (isEditMode)
                {
                    Func<tbl_BranchWarehouse, bool> func = (x => x.WHCode == txtWHCode.Text);
                    bwData = new tbl_BranchWarehouse();
                    bwData = bu.GetBranchWarehouse(func);
                    pnlBWH.Controls.SetObjectFromControl(bwData);
                    bwData.EdDate = DateTime.Now;
                    bwData.EdUser = Helper.tbl_Users.Username;
                }
                else
                {
                    pnlBWH.Controls.SetObjectFromControl(bwData);

                    bwData.WHCode = txtWHCode.Text;
                    bwData.WHID = bwData.WHCode;
                    if (grdDepo.CurrentCell != null)
                    {
                        int rowIndex = grdDepo.CurrentCell.RowIndex;
                        if (rowIndex != -1)
                        {
                            var code = grdDepo.Rows[rowIndex].Cells[0].Value.ToString();
                            bwData.BranchID = code;
                        }
                    }

                    bwData.License = "";
                    bwData.WHType = 0;
                    bwData.SaleEmpID = "0";

                    var currentDate = DateTime.Now;
                    bwData.CrDate = currentDate;
                    bwData.CrUser = Helper.tbl_Users.Username;

                    bwData.FlagSend = false;
                    bwData.VanType = 0;
                    bwData.HelperEmpID = "0";
                    bwData.POSNo = "";
                }

                if (rdoBWHStatusN.Checked)
                    bwData.FlagDel = false;
                else if (rdoBWHStatusC.Checked)
                    bwData.FlagDel = true;

                ret = bw.UpdateData(bwData);

                if (ret == 1)
                {
                    pnlBWH.OpenControl(false, readOnlyBWHControls.ToArray());

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, "");
                    btnAdd.Enabled = true;
                    btnCopy.Enabled = false;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    btnSearchWarehouse.PerformClick();
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

        private void RemoveBranchWarehouse(string whCode)
        {
            try
            {
                int ret = 0;

                string cfMsg = "ต้องการลบข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการลบ!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                BranchWarehouse b = new BranchWarehouse();
                Func<tbl_BranchWarehouse, bool> tbl_BranchWarehouseFunc = (x => x.WHCode == whCode);
                var tbl_BranchWarehouse = bu.GetBranchWarehouse(tbl_BranchWarehouseFunc);
                if (tbl_BranchWarehouse != null)
                {
                    tbl_BranchWarehouse eData = new tbl_BranchWarehouse();
                    eData = tbl_BranchWarehouse;
                    eData.FlagDel = true;

                    ret = b.UpdateData(eData);

                    if (ret == 1)
                    {
                        string msg = "ลบข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();

                        btnSearchWarehouse.PerformClick();
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
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void BindBranchWarehouseData()
        {
            string text = txtSearchWarehouse.Text;

            Func<tbl_BranchWarehouse, bool> tbl_BranchWarehouseFunc = null;
            if (!string.IsNullOrEmpty(text))
                tbl_BranchWarehouseFunc = (x => x.FlagDel == rdoWHCancel.Checked && (x.WHCode.Contains(text)) || x.WHName.Contains(text));
            else
                tbl_BranchWarehouseFunc = (x => x.FlagDel == rdoWHCancel.Checked);

            var dt = bu.GetBWHTable(tbl_BranchWarehouseFunc);

            if (dt != null)
            {
                grdBWHList.DataSource = null;

                var grd = grdBWHList;
                grd.DataSource = dt;

                DataGridViewColumn col0 = grd.Columns[0];
                DataGridViewColumn col1 = grd.Columns[1];
                DataGridViewColumn col2 = grd.Columns[2];

                col0.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col1.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col2.SetColumnStyle(160, DataGridViewAutoSizeColumnMode.Fill, DataGridViewContentAlignment.MiddleLeft, "", 160);

                lblWHCount.Text = dt.Rows.Count.ToNumberFormat();
            }
        }

        private void BindBranchWarehouseDetail()
        {
            if (grdBWHList.CurrentCell != null)
            {
                int rowIndex = grdBWHList.CurrentCell.RowIndex;

                if (rowIndex != -1)
                {
                    var whCode = grdBWHList.Rows[rowIndex].Cells[1].Value.ToString();
                    Func<tbl_BranchWarehouse, bool> tbl_BranchWarehouseFunc = (x => x.WHCode == whCode);
                    var bwh = bu.GetBranchWarehouse(tbl_BranchWarehouseFunc);

                    if (bwh != null)
                    {
                        rdoBWHStatusN.Checked = bwh.FlagDel == false;
                        rdoBWHStatusC.Checked = bwh.FlagDel == true;
                        pnlBWH.Controls.SetTextBoxControlValue(bwh);

                        pnlBWH.OpenControl(false, readOnlyBWHControls.ToArray());

                        btnSearchBranch.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
                        btnAdd.Enabled = true;
                        btnCopy.Enabled = false;
                        btnRemove.Enabled = true;

                        btnRemove.Enabled = !bwh.FlagDel;
                    }
                }
            }
        }

        private void btnSearchWarehouse_Click(object sender, EventArgs e)
        {
            BindBranchWarehouseData();
            BindBranchWarehouseDetail();
        }

        private void txtSearchWarehouse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BindBranchWarehouseData();
        }

        private void rdoWHNormal_CheckedChanged(object sender, EventArgs e)
        {
            //BindBranchWarehouseData();
        }

        private void rdoWHCancel_CheckedChanged(object sender, EventArgs e)
        {
            //BindBranchWarehouseData();
        }

        private void grdBWHList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdBWHList.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdBWHList_SelectionChanged(object sender, EventArgs e)
        {
            //BindBranchWarehouseDetail();
        }

        private void grdBWHList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BindBranchWarehouseDetail();
        }

        #endregion

        #region Tab Van

        private void PrepareVan()
        {
            int rowIndex = grdDepo.CurrentCell.RowIndex;
            if (rowIndex != -1)
            {
                var code = grdDepo.Rows[rowIndex].Cells[0].Value.ToString();
                var name = grdDepo.Rows[rowIndex].Cells[1].Value.ToString();

                string _whCode = "";
                string _whSeq = "";
                //string _whName = "";

                Func<tbl_BranchWarehouse, bool> tbl_BranchWarehouseFunc = (x => x.BranchID == code && x.VanType == 1);
                var tbl_BranchWarehouseList = bu.GetAllBranchWarehouse(tbl_BranchWarehouseFunc);
                if (tbl_BranchWarehouseList != null && tbl_BranchWarehouseList.Count > 0)
                {
                    string _no = tbl_BranchWarehouseList.Max(x => x.WHSeq);
                    var no = Convert.ToInt32(_no) + 1;

                    string tempRunningNo = no.ToString("00");

                    _whSeq = no.ToString("000");
                    _whCode = code + "V" + tempRunningNo;
                    //_whName = txtWHName_Van.Text;
                }
                else
                {
                    _whSeq = "001";
                    _whCode = code + "V01";
                    //_whName = txtWHName_Van.Text;
                }

                txtWHSeq_Van.Text = _whSeq;
                txtWHCode_Van.Text = _whCode;
                //txtWHName_Van.Text = _whName;
            }
        }

        private void RemoveVan(string whCode)
        {
            try
            {
                int ret = 0;

                string cfMsg = "ต้องการลบข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการลบ!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                BranchWarehouse b = new BranchWarehouse();
                Func<tbl_BranchWarehouse, bool> tbl_BranchWarehouseFunc = (x => x.WHCode == whCode);
                var tbl_BranchWarehouse = bu.GetBranchWarehouse(tbl_BranchWarehouseFunc);
                if (tbl_BranchWarehouse != null)
                {
                    tbl_BranchWarehouse eData = new tbl_BranchWarehouse();
                    eData = tbl_BranchWarehouse;
                    eData.FlagDel = true;

                    ret = b.UpdateData(eData);

                    if (ret == 1)
                    {
                        string msg = "ลบข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();

                        btnSearchVan.PerformClick();
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
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void SaveVan()
        {
            try
            {
                int ret = 0;
                tbl_BranchWarehouse bwData = new tbl_BranchWarehouse();

                bool isEditMode = bwData.CheckExistsData(txtWHCode_Van.Text);

                if (!isEditMode && !ValidateSave(tabVan.Name))
                    return;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                BranchWarehouse bw = new BranchWarehouse();
                if (isEditMode)
                {
                    Func<tbl_BranchWarehouse, bool> func = (x => x.WHCode == txtWHCode_Van.Text);
                    bwData = new tbl_BranchWarehouse();
                    bwData = bu.GetBranchWarehouse(func);
                    
                    bwData.EdDate = DateTime.Now;
                    bwData.EdUser = Helper.tbl_Users.Username;
                }
                else
                {
                    var currentDate = DateTime.Now;
                    bwData.CrDate = currentDate;
                    bwData.CrUser = Helper.tbl_Users.Username;
                }

                bwData.WHName = txtWHName_Van.Text;
                bwData.WHSeq = txtWHSeq_Van.Text;
                bwData.WHCode = txtWHCode_Van.Text;
                bwData.WHID = bwData.WHCode;
                if (grdDepo.CurrentCell != null)
                {
                    int rowIndex = grdDepo.CurrentCell.RowIndex;
                    if (rowIndex != -1)
                    {
                        var code = grdDepo.Rows[rowIndex].Cells[0].Value.ToString();
                        bwData.BranchID = code;
                    }
                }

                bwData.License = txtLicense.Text;

                if (rdoPreOrderVan.Checked)
                    bwData.WHType = 2;
                else if (rdoCashVan.Checked)
                    bwData.WHType = 1;
                
                bwData.SaleEmpID = txtSaleEmpID.Text;

                bwData.FlagSend = false;

                int vanType = Convert.ToInt32(ddlWHType_VanDT.SelectedValue);
                bwData.VanType = vanType;
                bwData.HelperEmpID = txtHelperEmpID.Text;
                bwData.DriverEmpID = txtDriverEmpID.Text;
                bwData.POSNo = txtPOSNo.Text;
                
                if (rdoStatusVanN.Checked)
                    bwData.FlagDel = false;
                else if (rdoStatusVanC.Checked)
                    bwData.FlagDel = true;

                ret = bw.UpdateData(bwData);

                if (ret == 1)
                {
                    pnlVANDT.OpenControl(false, readOnlyVANDTControls.ToArray());

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, "");
                    btnAdd.Enabled = true;
                    btnCopy.Enabled = false;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    btnSearchVan.PerformClick();
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

        private void BindVaneData()
        {
            string text = txtSearchVan.Text;
            var selWhType = Convert.ToInt32(ddlWHType_SVan.SelectedValue);
            var rdoWHType = rdoCashVan.Checked ? 1 : (rdoPreOrderVan.Checked ? 2 : 0);

            Func<tbl_BranchWarehouse, bool> tbl_BranchWarehouseFunc = null;

            tbl_BranchWarehouseFunc = (x => x.WHType == rdoWHType && x.FlagDel == rdoVanC.Checked &&
           //(x.WHType == (selWhType != -1 ? selWhType : x.WHType)) &&
           (x.WHCode.Contains(text) || x.WHName.Contains(text)));

            var dt = bu.GetVanTable(tbl_BranchWarehouseFunc, selWhType);

            if (dt != null)
            {
                var grd = grdVanList;
                grd.DataSource = dt;

                DataGridViewColumn col0 = grd.Columns[0];
                DataGridViewColumn col1 = grd.Columns[1];
                DataGridViewColumn col2 = grd.Columns[2];
                DataGridViewColumn col3 = grd.Columns[3];
                DataGridViewColumn col4 = grd.Columns[4];
                DataGridViewColumn col5 = grd.Columns[5];
                DataGridViewColumn col6 = grd.Columns[6];
                DataGridViewColumn col7 = grd.Columns[7];
                DataGridViewColumn col8 = grd.Columns[8];
                DataGridViewColumn col9 = grd.Columns[9];
                DataGridViewColumn col10 = grd.Columns[10];

                col0.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col1.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.Fill, DataGridViewContentAlignment.MiddleLeft, "", 120);
                col2.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleCenter, "", 0);
                col3.SetColumnStyle(80, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col4.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col5.SetColumnStyle(80, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col6.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col7.SetColumnStyle(80, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col8.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col9.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                col10.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);

                lblVanCount.Text = dt.Rows.Count.ToNumberFormat();
            }
        }

        private void BindVanDetail()
        {
            if (grdVanList.CurrentCell != null)
            {
                int rowIndex = grdVanList.CurrentCell.RowIndex;

                if (rowIndex != -1)
                {
                    var cell0 = grdVanList.Rows[rowIndex].Cells[0];
                    var cell4 = grdVanList.Rows[rowIndex].Cells[4];
                    var cell6 = grdVanList.Rows[rowIndex].Cells[6];
                    var cell7 = grdVanList.Rows[rowIndex].Cells[8];

                    var whCode = cell0.Value.ToString();

                    Func<tbl_BranchWarehouse, bool> tbl_BranchWarehouseFunc = (x => x.WHCode == whCode);
                    var bwh = bu.GetBranchWarehouse(tbl_BranchWarehouseFunc);

                    if (bwh != null)
                    {
                        rdoStatusVanN.Checked = bwh.FlagDel == false;
                        rdoStatusVanC.Checked = bwh.FlagDel == true;

                        pnlVANDT.Controls.SetTextBoxControlValue(bwh);

                        ddlWHType_VanDT.SelectedValue = bwh.WHType;
                        txtSaleEmpName.Text = cell4.Value.ToString();
                        txtDriverEmpName.Text = cell6.Value.ToString();
                        txtHelperEmpName.Text = cell7.Value.ToString();

                        pnlVANDT.OpenControl(false, readOnlyVANDTControls.ToArray());

                        btnSearchBranch.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
                        btnAdd.Enabled = true;
                        btnCopy.Enabled = false;
                        btnRemove.Enabled = true;

                        btnRemove.Enabled = !bwh.FlagDel;
                    }
                }
            }
        }

        private void PrepareCashVan()
        {
            var whTypeList = new List<tbl_VanType>();
            var _whTypeList = bu.GetWHType().Where(x => x.WHType == 1).OrderBy(x => x.Seq).ToList();
            whTypeList.Add(new tbl_VanType { AutoID = -1, Name = "==เลือก==" });
            whTypeList.AddRange(_whTypeList);
            ddlWHType_SVan.BindDropdownList(whTypeList, "Name", "AutoID");

            whTypeList = new List<tbl_VanType>();
            whTypeList.AddRange(_whTypeList);
            ddlWHType_VanDT.BindDropdownList(whTypeList, "Name", "AutoID");
        }

        private void PreparePreOrder()
        {
            var whTypeList = new List<tbl_VanType>();
            var _whTypeList = bu.GetWHType().Where(x => x.WHType == 2).OrderBy(x => x.Seq).ToList();
            whTypeList.Add(new tbl_VanType { AutoID = -1, Name = "==เลือก==" });
            whTypeList.AddRange(_whTypeList);
            ddlWHType_SVan.BindDropdownList(whTypeList, "Name", "AutoID");

            whTypeList = new List<tbl_VanType>();
            whTypeList.AddRange(_whTypeList);
            ddlWHType_VanDT.BindDropdownList(whTypeList, "Name", "AutoID");
        }

        private void rdoCashVan_CheckedChanged(object sender, EventArgs e)
        {
            BindVaneData();

            PrepareCashVan();
        }

        private void btnPreOrderVan_CheckedChanged(object sender, EventArgs e)
        {
            BindVaneData();

            PreparePreOrder();
        }

        private void btnSearchVan_Click(object sender, EventArgs e)
        {
            BindVaneData();
            BindVanDetail();
        }

        private void txtSearchVan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BindVaneData();
        }

        private void grdVanList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BindVanDetail();
        }

        private void grdVanList_SelectionChanged(object sender, EventArgs e)
        {
            //BindVanDetail();
        }

        private void grdVanList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdVanList.SetRowPostPaint(sender, e, this.Font);
        }

        private void rdoVanN_CheckedChanged(object sender, EventArgs e)
        {
            //BindVaneData();
        }

        private void rdoVanC_CheckedChanged(object sender, EventArgs e)
        {
            //BindVaneData();
        }

        private void btnSaleEmpID_Click(object sender, EventArgs e)
        {
            Func<tbl_Employee, bool> empFunc = (x => x.PositionID == 4);
            this.OpenEmployeePopup(saleEmpList, "เลือกพนักงาน", empFunc);
        }

        private void btnDriverEmpID_Click(object sender, EventArgs e)
        {
            Func<tbl_Employee, bool> empFunc = (x => x.PositionID == 8);
            this.OpenEmployeePopup(driverEmpList, "เลือกพนักงาน", empFunc);
        }

        private void btnHelperEmpID_Click(object sender, EventArgs e)
        {
            Func<tbl_Employee, bool> empFunc = (x => x.PositionID == 9);
            this.OpenEmployeePopup(helperEmpList, "เลือกพนักงาน", empFunc);
        }

        private void txtSaleEmpID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("Employee", saleEmpList, txtSaleEmpID.Text);
            }
        }

        private void txtDriverEmpID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("Employee", driverEmpList, txtDriverEmpID.Text);
            }
        }

        private void txtHelperEmpID_KeyDown(object sender, KeyEventArgs e)
        {

        }

        #endregion

        #region Tab MKT

        private void PrepareMTK()
        {
            Cursor.Current = Cursors.WaitCursor;

            Func<tbl_SalArea, bool> tbl_SalAreaFunc = (x => x.BranchID == bu.tbl_Branchs[0].BranchID);
            tbl_SalAreaList = bu.GetSaleArea(tbl_SalAreaFunc);

            int rowIndex = grdDepo.CurrentCell.RowIndex;
            if (rowIndex != -1)
            {
                var code = grdDepo.Rows[rowIndex].Cells[0].Value.ToString();
                var name = grdDepo.Rows[rowIndex].Cells[1].Value.ToString();

                string _salAreaID = "";
                string _salAreaName = "";
                string _salAreaCode = "";
                string _seq = "1";

                if (tbl_SalAreaList != null && tbl_SalAreaList.Count > 0)
                {
                    string _no = tbl_SalAreaList.Max(x => x.SalAreaCode);
                    var no = Convert.ToInt32(_no) + 1;

                    string tempRunningNo = no.ToString("000");

                    _salAreaCode = no.ToString("000");
                    _salAreaID = code + "M" + _salAreaCode;

                    if (ddlWHID.SelectedValue.ToString() != "-1")
                    {
                        string vCode = ddlWHID.SelectedValue.ToString().Substring(3, 3);
                        string _name = tbl_SalAreaList.Where(x => x.SalAreaName.Contains(vCode)).Max(x => x.SalAreaName);
                        if (_name != null)
                        {
                            try
                            {
                                var _mktName = _name.Substring(_name.Length - 2, 2);
                                var mktName = Convert.ToInt32(_mktName) + 1;

                                _salAreaName = "ตลาด " + vCode + "-" + mktName;
                            }
                            catch
                            {
                                _salAreaName = "ตลาด xxx-xx";
                            }
                        }
                        else
                            _salAreaName = "ตลาด xxx-xx";

                        int? _s = tbl_SalAreaList.Where(x => x.SalAreaName.Contains(vCode)).Max(x => x.Seq);
                        if (_s != null)
                            _seq = (_s.Value + 1).ToString();
                        else
                            _seq = "1";
                    }
                    else
                        _salAreaName = "ตลาด xxx-xx";
                }
                else
                {
                    _salAreaCode = code + "M001";
                    _salAreaID = "001";
                    _salAreaName = "ตลาด xxx-xx";
                }

                txtSalAreaCode.Text = _salAreaCode;
                txtSalAreaID.Text = _salAreaID;
                txtSalAreaName.Text = _salAreaName;
                txtSeq.Text = _seq;
                txtCountCustomer.Text = "0";
            }

            Cursor.Current = Cursors.Default;
        }

        private int UpdateSalAreaData()
        {
            int ret = 0;

            var dtSalArea = sa.GetSalAreaData(0, txtSalAreaID.Text);

            var _SalArea = new tbl_SalArea();

            pnlMKT.Controls.SetObjectFromControl(_SalArea);

            _SalArea.BranchID = bu.tbl_Branchs[0].BranchID;

            if (dtSalArea != null && dtSalArea.Rows.Count > 0)
            {
                _SalArea.CrDate = dtSalArea.Rows[0].Field<DateTime>("CrDate");
                _SalArea.CrUser = dtSalArea.Rows[0].Field<string>("CrUser");

                _SalArea.EdDate = DateTime.Now;
                _SalArea.EdUser = Helper.tbl_Users.Username;
            }
            else
            {
                _SalArea.CrDate = DateTime.Now;
                _SalArea.CrUser = Helper.tbl_Users.Username;
            }

            _SalArea.ZoneID = Convert.ToInt32(ddlZoneID.SelectedValue) == -1 ? 0 : Convert.ToInt32(ddlZoneID.SelectedValue);
            _SalArea.FlagDel = rdoMKTStatusN.Checked ? false : true;

            ret = sa.UpdateData(_SalArea);
            return ret;
        }

        private void SaveTabMarketing()
        {
            try
            {
                if (!ValidateSave(tabMarket.Name))
                    return;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                List<int> ret = new List<int>();

                ret.Add(UpdateSalAreaData());

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@SalAreaID", txtSalAreaID.Text);
                _params.Add("@WHID", ddlWHID.SelectedValue.ToString());
                _params.Add("@ZoneID", Convert.ToInt32(ddlZoneID.SelectedValue) == -1 ? 0 : Convert.ToInt32(ddlZoneID.SelectedValue));
                ret.Add(buSADistrict.UpdateSalAreaDistrict(_params));

                if (ret.All(x=>x == 1))
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    btnSearchMKT.PerformClick();
                }
                else
                {
                    this.ShowProcessErr();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void SaveMKT()
        {
            try
            {
                int ret = 0;
                tbl_SalArea saData = new tbl_SalArea();

                bool isEditMode = saData.CheckExistsData(txtSalAreaID.Text);

                if (!isEditMode && !ValidateSave(tabMarket.Name))
                    return;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                if (isEditMode)
                {
                    List<tbl_SalAreaDistrict> sadList = new List<tbl_SalAreaDistrict>();

                    Func<tbl_SalArea, bool> tbl_SalAreaFunc = (x => x.SalAreaID == txtSalAreaID.Text);
                    //saData = bu.GetSaleArea(tbl_SalAreaFunc)[0];
                    saData = bu.GetSaleArea(txtSalAreaID.Text);
                    pnlMKT.Controls.SetObjectFromControl(saData);
                    saData.EdDate = DateTime.Now;
                    saData.EdUser = Helper.tbl_Users.Username;

                    SaleAreaDistrict sad = new SaleAreaDistrict();
                    //Func<tbl_SalAreaDistrict, bool> tbl_SalAreaDistrictFunc = (x => x.SalAreaID == txtSalAreaID.Text);
                    //sadList = bu.GetSaleAreaDistrict(tbl_SalAreaDistrictFunc);
                    sadList = bu.GetSaleAreaDistrict(txtSalAreaID.Text);

                    List<int> removeList = new List<int>();

                    foreach (var item in sadList)
                    {
                        removeList.Add(sad.RemoveData(item));
                    }
                }
                else
                {
                    pnlMKT.Controls.SetObjectFromControl(saData);
                    saData.CrDate = DateTime.Now;
                    saData.CrUser = Helper.tbl_Users.Username;
                }

                if (grdDepo.CurrentCell != null)
                {
                    int rowIndex = grdDepo.CurrentCell.RowIndex;
                    if (rowIndex != -1)
                    {
                        var code = grdDepo.Rows[rowIndex].Cells[0].Value.ToString();
                        saData.BranchID = code;
                    }
                }

                saData.ZoneID = saData.ZoneID == -1 ? 0 : saData.ZoneID;
                if (rdoMKTStatusN.Checked)
                    saData.FlagDel = false;
                else if (rdoMKTStatusC.Checked)
                    saData.FlagDel = true;

                ret = sa.UpdateData(saData);

                if (ret == 1)
                {
                    List<tbl_SalAreaDistrict> sadList = new List<tbl_SalAreaDistrict>();

                    sadList = PrepareSaleAreaDistrict(saData.SalAreaID);
                    SaleAreaDistrict sad = new SaleAreaDistrict();

                    List<int> retUpdate = new List<int>();
                    bu.SalAreaDistrictPerformUpdate(sadList);
                    //foreach (var item in sadList)
                    //{
                    //    retUpdate.Add(sad.UpdateData(item));
                    //}

                    if (retUpdate.All(x => x == 1))
                        ret = 1;
                }

                if (ret == 1)
                {
                    pnlMKT.OpenControl(false, readOnlyMKTControls.ToArray());

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, "");
                    btnAdd.Enabled = true;
                    btnCopy.Enabled = false;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    btnSearchMKT.PerformClick();
                }
                else
                {
                    this.ShowProcessErr();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private List<tbl_SalAreaDistrict> PrepareSaleAreaDistrict(string salAreaID)
        {
            var allDistrict = new tbl_MstDistrict().SelectAll();
            var allArea = new tbl_MstArea().SelectAll();

            List<tbl_SalAreaDistrict> tbl_SalAreaDistrictList = new List<tbl_SalAreaDistrict>();
            for (int i = 0; i < grdSaleDistrictList.RowCount; i++)
            {
                var cell0 = grdSaleDistrictList.Rows[i].Cells[0];
                var cell1 = grdSaleDistrictList.Rows[i].Cells[1];
                var cell2 = grdSaleDistrictList.Rows[i].Cells[2];
                var cell3 = grdSaleDistrictList.Rows[i].Cells[3];

                tbl_SalAreaDistrict _sad = new tbl_SalAreaDistrict();
                _sad.SalAreaID = salAreaID;

                _sad.WHID = ddlWHID.SelectedValue.ToString();
                _sad.DistrictCode = cell2.Value.ToString();

                //Func<tbl_MstDistrict, bool> tbl_MstDistrictFunc = (a => a.DistrictCode == _sad.DistrictCode);
                //var _tbl_MstDistrict = new tbl_MstDistrict().Select(tbl_MstDistrictFunc);

                var _tbl_MstDistrict = allDistrict.FirstOrDefault(x => x.DistrictCode == _sad.DistrictCode);

                //SelectSingle
                if (_tbl_MstDistrict != null)
                {
                    _sad.DistrictID = _tbl_MstDistrict.DistrictID;
                }
                _sad.DistrictName = cell3.Value.ToString();
                _sad.AreaName = cell1.Value.ToString();
                _sad.ProvinceName = cell0.Value.ToString();

                //Func<tbl_MstArea, bool> tbl_MstAreaFunc = (a => a.AreaName == _sad.AreaName);
                //var _tbl_MstAreaList = new tbl_MstArea().Select(tbl_MstAreaFunc);

                var _tbl_MstAreaList = allArea.FirstOrDefault(x => x.AreaName == _sad.AreaName);
                if (_tbl_MstAreaList != null)
                {
                    _sad.PostalCode = _tbl_MstAreaList.PostalCode;
                }

                tbl_SalAreaDistrictList.Add(_sad);
            }

            return tbl_SalAreaDistrictList;
        }

        private void RemoveMKT(string code)
        {
            try
            {
                int ret = 0;

                string cfMsg = "ต้องการลบข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการลบ!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                SaleAreaDistrict s = new SaleAreaDistrict();
                Func<tbl_SalAreaDistrict, bool> tbl_SalAreaDistrictFunc = (x => x.SalAreaID == code);
                var tbl_SalAreaDistrictList = bu.GetSaleAreaDistrict(tbl_SalAreaDistrictFunc);
                if (tbl_SalAreaDistrictList != null && tbl_SalAreaDistrictList.Count > 0)
                {
                    List<tbl_SalAreaDistrict> eData = new List<tbl_SalAreaDistrict>();
                    eData = tbl_SalAreaDistrictList;

                    //List<int> retList = new List<int>();
                    //foreach (var item in eData)
                    //{
                    //    retList.Add(s.RemoveData(item));
                    //}

                    //if (retList.All(x => x == 1))
                    //    ret = 1;

                    if (ret == 0)
                    {
                        SaleArea sa = new SaleArea();
                        Func<tbl_SalArea, bool> tbl_SalAreaFunc = (x => x.SalAreaID == code);
                        var tbl_SalAreaList = bu.GetSaleArea(tbl_SalAreaFunc);
                        if (tbl_SalAreaList != null && tbl_SalAreaList.Count > 0)
                        {
                            tbl_SalArea saData = new tbl_SalArea();
                            saData = tbl_SalAreaList[0];
                            saData.FlagDel = true;

                            ret = sa.UpdateData(saData);
                        }
                    }

                    if (ret == 1)
                    {
                        string msg = "ลบข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();

                        btnSearchMKT.PerformClick();
                    }
                    else
                    {
                        this.ShowProcessErr();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void BindMKTDetail()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (grdSaleAreaList.CurrentCell != null)
                {
                    int rowIndex = grdSaleAreaList.CurrentCell.RowIndex;

                    if (rowIndex != -1)
                    {
                        var cell0 = grdSaleAreaList.Rows[rowIndex].Cells[0];
                        var cell5 = grdSaleAreaList.Rows[rowIndex].Cells[5];

                        var salAreaID = cell0.Value.ToString();

                        Func<tbl_SalArea, bool> tbl_SalAreaFunc = (x => x.SalAreaID == salAreaID);
                        var sa = bu.GetSaleArea(tbl_SalAreaFunc);

                        if (sa != null && sa.Count > 0)
                        {
                            pnlMKT.Controls.SetTextBoxControlValue(sa[0]);
                            txtCountCustomer.Text = cell5.Value.ToString();

                            rdoMKTStatusN.Checked = sa[0].FlagDel == false;
                            rdoMKTStatusC.Checked = sa[0].FlagDel == true;

                            ddlZoneID.SelectedValue = sa[0].ZoneID == 0 ? -1 : sa[0].ZoneID;

                            var saleAreDistinct1 = bu.SelectSalAreaDistrict(salAreaID);

                            //var saleAreDistinct = bu.GetSaleAreaDistrict(x => x.SalAreaID == salAreaID);
                            if (saleAreDistinct1 != null && saleAreDistinct1.Count > 0)
                            {
                                ddlWHID.SelectedValue = saleAreDistinct1[0].WHID.ToString();
                            }
                            else
                                ddlWHID.SelectedValue = "-1";

                            Sub_BindSaleAreaDistrict(txtSalAreaID.Text);

                            pnlMKT.OpenControl(false, readOnlyVANDTControls.ToArray());

                            btnSearchBranch.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
                            btnAdd.Enabled = true;
                            btnCopy.Enabled = false;
                            btnRemove.Enabled = true;

                            btnRemove.Enabled = !sa[0].FlagDel;
                        }
                    }
                }
                else
                {
                    DataTable _dt = new DataTable();
                    SetSalAreaDistrict(_dt);
                    grdSaleDistrictList.DataSource = _dt;
                    lblDistrictCount.Text = _dt.Rows.Count.ToNumberFormat();

                    pnlMKT.ClearControl();
                    pnlMKT.OpenControl(false, readOnlyVANDTControls.ToArray());

                    btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }

            Cursor.Current = Cursors.Default;
        }

        private void BindMKTData()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                var selZone = Convert.ToInt32(ddlZone.SelectedValue);

                // Func<tbl_SalArea, bool> tbl_SalAreaFunc = null;
                // int _zoneID = (selZone != -1 ? selZone : 0);
                // tbl_SalAreaFunc = (x => x.FlagDel == rdoMKTC.Checked &&
                //(x.ZoneID == (selZone != -1 ? selZone : x.ZoneID)) &&
                //(x.SalAreaID.Contains(txtSearchMKT.Text) || x.SalAreaName.Contains(txtSearchMKT.Text)));

                int _flagDel = rdoMKTN.Checked ? 0 : 1;
                int _ZoneID = selZone != -1 ? selZone : 0;

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@Search", txtSearchMKT.Text);
                _params.Add("@FlagDel", _flagDel);
                _params.Add("@ZoneID", _ZoneID);

                //var dt = bu.GetMKTTable(tbl_SalAreaFunc);//657

                var dt2 = bu.proc_GetMKT_Data(_params);//366

                var grd = grdSaleAreaList;
                grd.DataSource = dt2;
                lblMKTCount.Text = dt2.Rows.Count.ToNumberFormat();

                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    DataGridViewColumn col0 = grd.Columns[0];
                    DataGridViewColumn col1 = grd.Columns[1];
                    DataGridViewColumn col2 = grd.Columns[2];
                    DataGridViewColumn col3 = grd.Columns[3];
                    DataGridViewColumn col4 = grd.Columns[4];
                    DataGridViewColumn col5 = grd.Columns[5];

                    col0.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                    col1.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                    col2.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.Fill, DataGridViewContentAlignment.MiddleLeft, "", 120);
                    col3.SetColumnStyle(80, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                    col4.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                    col5.SetColumnStyle(100, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }

            Cursor.Current = Cursors.Default;
        }

        public void AddSaleAreaDistrictRow(DataTable newRowDt)
        {
            var grd = grdSaleDistrictList;
            var dr = newRowDt;
            bool isDuplicate = false;
            for (int i = 0; i < grd.Rows.Count; i++)
            {
                var cell2 = grd.Rows[i].Cells[2];
                if (cell2.Value.ToString() == dr.Rows[0]["DistrictCode"].ToString())
                {
                    string message = "ห้ามเลือกตำบลซ้ำ !!!";
                    message.ShowWarningMessage();

                    isDuplicate = true;
                    break;
                }
            }

            if (isDuplicate)
                return;

            DataTable dataTable = (DataTable)grd.DataSource;
            DataRow drToAdd = dataTable.NewRow();

            drToAdd[0] = dr.Rows[0]["ProvinceName"].ToString();
            drToAdd[1] = dr.Rows[0]["AreaName"].ToString();
            drToAdd[2] = dr.Rows[0]["DistrictCode"].ToString();
            drToAdd[3] = dr.Rows[0]["DistrictName"].ToString();

            dataTable.Rows.Add(drToAdd);
            dataTable.AcceptChanges();

            lblDistrictCount.Text = dataTable.Rows.Count.ToNumberFormat();
        }

        private void SetSalAreaDistrict(DataTable dt)
        {
            dt.Columns.Add("จังหวัด", typeof(string));
            dt.Columns.Add("อำเภอ", typeof(string));
            dt.Columns.Add("รหัสตำบล", typeof(string));
            dt.Columns.Add("ชื่อตำบล", typeof(string));
        }

        private void Sub_BindSaleAreaDistrict(string _SalAreaID)
        {
            if (!string.IsNullOrEmpty(_SalAreaID))
            {
                //Func<tbl_SalAreaDistrict, bool> tbl_SalAreaDistrictFunc = (x => x.SalAreaID == _SalAreaID);
                //var dt = new SaleAreaDistrict().GetDataTable(tbl_SalAreaDistrictFunc);//3000
                var _dt = new SaleAreaDistrict().SelectSingleDT(_SalAreaID); //new

                DataTable districtDT = new DataTable();

                SetSalAreaDistrict(districtDT);

                foreach (DataRow r in _dt.Rows)
                {
                    districtDT.Rows.Add(r["ProvinceName"], r["AreaName"], r["DistrictCode"], r["DistrictName"]);
                }

                var grd = grdSaleDistrictList;
                grd.DataSource = districtDT;
                lblDistrictCount.Text = districtDT.Rows.Count.ToNumberFormat();

                if (districtDT != null)
                {
                    DataGridViewColumn col0 = grd.Columns[0];
                    DataGridViewColumn col1 = grd.Columns[1];
                    DataGridViewColumn col2 = grd.Columns[2];
                    DataGridViewColumn col3 = grd.Columns[3];

                    col0.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                    col1.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                    col2.SetColumnStyle(80, DataGridViewAutoSizeColumnMode.NotSet, DataGridViewContentAlignment.MiddleLeft, "", 0);
                    col3.SetColumnStyle(120, DataGridViewAutoSizeColumnMode.Fill, DataGridViewContentAlignment.MiddleLeft, "", 0);
                }
            }
        }

        private void btnSearchMKT_Click(object sender, EventArgs e)
        {
            BindMKTData();
            BindMKTDetail();
        }

        private void txtSearchMKT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindMKTData();
                BindMKTDetail();
            }
        }

        private void rdoMKTN_CheckedChanged(object sender, EventArgs e)
        {
            //BindMKTData();
        }

        private void rdoMKTC_CheckedChanged(object sender, EventArgs e)
        {
            //BindMKTData();
        }

        private void btnDistrict_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSalAreaID.Text))
            {
                this.OpenSaleAreaDistrictPopup(new List<Control>() { grdSaleDistrictList }, "เลือกตำบล");
            }
        }

        private void grdSaleAreaList_SelectionChanged(object sender, EventArgs e)
        {
            //BindMKTDetail();
        }

        private void grdSaleAreaList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BindMKTDetail();

            isEditList = true;
        }

        private void grdSaleAreaList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdSaleAreaList.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdSaleDistrictList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdSaleDistrictList.SetRowPostPaint(sender, e, this.Font);
        }

        private void ddlWHID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isEditList)
            {
                return;
            }

            string _salAreaName = "";
            string _seq = "1";
            if (ddlWHID.SelectedValue.ToString() != "-1" && tbl_SalAreaList != null && tbl_SalAreaList.Count > 0)
            {
                string vCode = ddlWHID.SelectedValue.ToString().Substring(3, 3);
                string _name = tbl_SalAreaList.Where(x => x.SalAreaName.Contains(vCode)).Max(x => x.SalAreaName);
                if (_name != null)
                {
                    try
                    {
                        var _mktName = _name.Length > 10 ? _name.Substring(_name.Length - 2, 2) : _name.Substring(_name.Length - 1, 1);
                        var mktName = Convert.ToInt32(_mktName) + 1;

                        _salAreaName = "ตลาด " + vCode + "-" + mktName;
                    }
                    catch
                    {

                        _salAreaName = "ตลาด xxx-xx";
                    }
                   
                }
                else
                    _salAreaName = "ตลาด xxx-xx";

                int? _s = tbl_SalAreaList.Where(x => x.SalAreaName.Contains(vCode)).Max(x => x.Seq);
                if (_s != null)
                    _seq = (_s.Value + 1).ToString();
                else
                    _seq = "1";
            }
            else
                _salAreaName = "ตลาด xxx-xx";

            txtSalAreaName.Text = _salAreaName;
            txtSeq.Text = _seq;
        }

        private void txtSalAreaCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.ToNumberOnly(sender);
        }

        private void txtSeq_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.ToNumberOnly(sender);
        }


        #endregion

        private void frmDistributionCenter_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void ddlWHID_MouseClick(object sender, MouseEventArgs e)
        {
            isEditList = false;
        }
    }
}
