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
using AllCashUFormsApp.View.UControl;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmSalesTransfer : Form
    {
        Employee bu = new Employee();
        SaleArea buSaleArea = new SaleArea();
        BranchWarehouse bbu = new BranchWarehouse();
        MenuBU menuBU = new MenuBU();
        List<Control> FromBranchWHControls = new List<Control>();
        List<Control> ToBranchWHControls = new List<Control>();
        Dictionary<Control, Label> validateSaveCtrls = new Dictionary<Control, Label>();
        public frmSalesTransfer()
        {
            InitializeComponent();

            FromBranchWHControls = new List<Control> { txtFromWHCode, txtFromWHName };
            ToBranchWHControls = new List<Control> { txtToWHCode, txtToWHName };

            validateSaveCtrls.Add(txtFromWHCode, lblFromWHCode);//
            validateSaveCtrls.Add(txtToWHCode, lblToWHCode);//
        }

        #region #--# Variable
        string EmployeeID = "";
        List<string> ListSalAreaID = new List<string>();
        List<string> ListWHID = new List<string>();
        public static string EmpID = "", EmpName = "";
        #endregion

        #region Event
        private void cbbBranchWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrePareBranchWH();
        }

        private void rdoCashVan_Click(object sender, EventArgs e)
        {
            BindEmpData(4);
            BindBranchWareHouse();
        }

        private void tabPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tabName = tabPage.SelectedTab.Text.ToString();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            if (tabName == "โอนย้ายตลาด")
            {

            }
            else if (tabName == "เปลี่ยนพนักงานขาย")
            {
                BindSaleEmp();
            }
            else if (tabName == "ผ่าแวน")
            {
                BindVan();

                txtFromWHCode.DisableTextBox(true);
                txtToWHCode.DisableTextBox(true);

                btnSearchFromWHCode.Enabled = false;
                btnSearchToWHCode.Enabled = false;
            }
        }

        private void rdoPreOrder_Click(object sender, EventArgs e)
        {
            BindEmpData(7); //7 - PreSale
            BindBranchWareHouse();
        }

        private void grdSaleEmp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var grd = (DataGridView)sender;
                if (grd.Columns[e.ColumnIndex] is DataGridViewButtonColumn && grd.Columns[e.ColumnIndex].Name == "colSearch")
                {
                    frmSearchEmployee frm = new frmSearchEmployee();
                    frm.ShowDialog();

                    if (!string.IsNullOrEmpty(EmpID))
                    {
                        grd.Rows[e.RowIndex].Cells["colEmployeeID"].Value = EmpID;
                        grd.Rows[e.RowIndex].Cells["colEmployeeName"].Value = EmpName;
                    }
                }
                else if (grd.Columns[e.ColumnIndex] is DataGridViewButtonColumn && grd.Columns[e.ColumnIndex].Name == "colClearData")
                {
                    grd.Rows[e.RowIndex].Cells["colEmployeeID"].Value = null;
                    grd.Rows[e.RowIndex].Cells["colEmployeeName"].Value = null;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        #endregion

        #region Method
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
        }

        private void BindBranchWHDefault()
        {
            var BranchWH = new List<tbl_BranchWarehouse>();
            BranchWH.Add(new tbl_BranchWarehouse { SaleEmpID = "", WHCode = "==เลือก==" });
            cbbBranchWareHouse.BindDropdownList(BranchWH, "WHCode", "SaleEmpID", 0);
        }

        private void BindEmpData(int PositionID)
        {
            try
            {
                lsbSale.ClearListBoxItem(); //new method
                ListWHID.Clear();
                EmployeeID = "";

                var dtSaleEmp = bu.GetSaleEmployee(PositionID);

                if (dtSaleEmp.Rows.Count == 0)
                    return;

                foreach (DataRow r in dtSaleEmp.Rows)
                {
                    string WHID = r["WHID"].ToString();
                    if (!string.IsNullOrEmpty(WHID))
                    {
                        ListWHID.Add(WHID);
                    }
                }

                var SaleEmp = dtSaleEmp.AsEnumerable().FirstOrDefault(x => x.Field<string>("WHID") is null);
                if (SaleEmp != null)
                {
                    lsbSale.Items.Add(SaleEmp.Field<string>("EmpName").ToString());
                    EmployeeID = SaleEmp.Field<string>("EmpID").ToString();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowWarningMessage();
            }
        }

        private void BindBranchWareHouse()
        {
            var BranchWH = new List<tbl_BranchWarehouse>();
            BranchWH.Add(new tbl_BranchWarehouse { SaleEmpID = "", WHCode = "==เลือก==" });
            BranchWH.AddRange(bu.GetAllBranchWarehouse(x => x.WHType != 0 && ListWHID.Contains(x.WHID))); // edit by sailom .k 03/03/2022 for support pre-order

            if (BranchWH.Count > 0)
            {
                cbbBranchWareHouse.BindDropdownList(BranchWH, "WHCode", "SaleEmpID", 0);
            }
            else
            {
                BindBranchWHDefault();
            }
        }

        private void SetSalAreaData()
        {
            ListSalAreaID.Clear();

            var data = new List<tbl_SalArea>();

            if (!string.IsNullOrEmpty(cbbBranchWareHouse.Text) && cbbBranchWareHouse.Text.Length == 6)
            {
                var listSalArea = buSaleArea.GetSalAreaByWHID(cbbBranchWareHouse.Text);

                if (listSalArea.Count == 24) //มีครบ 24 ตลาด
                {
                    var listSaleArea = listSalArea.OrderBy(x => x.Seq).ToList();
                    data.AddRange(listSaleArea);
                }
                else
                {
                    string _WHID = cbbBranchWareHouse.Text.Substring(3, 3);
                    var SalAreaList = bu.GetSaleArea(x => x.SalAreaName.Contains(_WHID) && x.Seq != 0);

                    for (int i = 0; i < listSalArea.Count; i++) //ในกรณีที่ตลาด เปลี่ยนชื่อ 
                    {
                        var row = SalAreaList.Where(x => x.SalAreaID == listSalArea[i].SalAreaID).ToList();
                        if (row.Count == 0)
                        {
                            SalAreaList.Add(listSalArea[i]);
                        }
                    }

                    var _SalAreaID = SalAreaList.OrderBy(x => x.Seq).ToList();
                    data.AddRange(_SalAreaID);
                }

            }

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Seq == 0)
                {
                    lsbTransferTo.Items.Add(data[i].SalAreaName);
                }
                else
                {
                    lsbTransferFr.Items.Add(data[i].SalAreaName);
                }
                ListSalAreaID.Add(data[i].SalAreaID);
            }
        }

        private void BindSalArea()
        {
            //var AllSalAreaID = bu.GetSaleAreaDistrict(x => x.WHID.Contains(cbbBranchWareHouse.Text));

            //ListSalAreaID.Clear(); //

            //var SalAreaList = new List<tbl_SalArea>();

            //if (AllSalAreaID.Count > 0)
            //{
            //    ListSalAreaID = AllSalAreaID.Select(x => x.SalAreaID).Distinct().ToList();

            //    SalAreaList = bu.GetSaleArea(x => ListSalAreaID.Contains(x.SalAreaID) && x.FlagDel == false).OrderBy(x => x.Seq).ToList();
            //}
            //else //ไม่มีข้อมูล SalAreaDistrict
            //{
            //    if (!string.IsNullOrEmpty(cbbBranchWareHouse.Text))
            //    {
            //        string WHID = cbbBranchWareHouse.Text.Substring(3, 3);
            //        SalAreaList = bu.GetSaleArea(x => x.SalAreaName.Contains(WHID) && x.FlagDel == false).OrderBy(x => x.Seq).ToList();
            //    }
            //}

            //for (int i = 0; i < SalAreaList.Count; i++)
            //{
            //    if (SalAreaList[i].Seq == 0)
            //    {
            //        lsbTransferTo.Items.Add(SalAreaList[i].SalAreaName);
            //    }
            //    else
            //    {
            //        lsbTransferFr.Items.Add(SalAreaList[i].SalAreaName);
            //    }
            //    ListSalAreaID.Add(SalAreaList[i].SalAreaID);
            //}
        }

        private void PrePareBranchWH()
        {
            lsbTransferTo.ClearListBoxItem();
            lsbVan.ClearListBoxItem();
            lsbTransferFr.ClearListBoxItem();

            txtSaleEmpID.Clear();
            txtFirstName.Clear();

            string SaleEmpID = cbbBranchWareHouse.SelectedValue.ToString();
           
            if (!string.IsNullOrEmpty(SaleEmpID))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    var Emp = bu.GetEmployee(x => x.EmpID == SaleEmpID && x.FlagDel == false);
                    if (Emp != null && Emp.Count > 0)
                    {
                        string VanName = "";
                        VanName = Emp[0].TitleName + " " + Emp[0].FirstName;
                        lsbVan.Items.Add(VanName);
                        txtSaleEmpID.Text = SaleEmpID;
                        txtFirstName.Text = VanName;
                        //BindSalArea();
                        SetSalAreaData(); //20-06-2022 adisorn
                    }
                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    ex.Message.ShowErrorMessage();
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        public void lsbSelected(ListBox TransferFr, ListBox TransferTo)
        {
            try
            {
                string SalAreaName = TransferFr.SelectedItem.ToString();

                TransferTo.Items.Add(SalAreaName);

                TransferFr.Items.RemoveAt(TransferFr.SelectedIndex);

                if (TransferTo.Items.Count > 0)
                {
                    TransferTo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        public void lsbSelectedAll(ListBox TransferFr, ListBox TransferTo)
        {
            try
            {
                if (TransferFr.Items.Count > 0 && TransferFr != null)
                {
                    for (int i = 0; i < TransferFr.Items.Count; i++)
                    {
                        string SalAreaName = TransferFr.Items[i].ToString();
                        TransferTo.Items.Add(SalAreaName);
                    }
                    TransferFr.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void BindSaleEmp()
        {
            grdSaleEmp.AutoGenerateColumns = false;

            DataTable newDT = new DataTable();
            newDT = bu.GetAllSaleEmployee();
            grdSaleEmp.DataSource = newDT;
        }

        private void BindVan()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            var dt = bu.GetBranchWarehouseMappingDT();
            grdSpecialVan.DataSource = dt;
            lblgrdQty.Text = dt.Rows.Count.ToNumberFormat();

            if (dt.Rows.Count > 0)
            {
                btnRemove.Enabled = true;
            }
            else
            {
                txtFromWHCode.Text = "";
                txtToWHCode.Text = "";
                txtFromWHName.Text = "";
                txtToWHName.Text = "";
            }
        }

        private void SwapEmployee()
        {
            if (!string.IsNullOrEmpty(EmployeeID) && lsbSale.Items.Count > 0)
            {
                if (btnEmpSwap.Text == "<")
                {
                    btnEmpSwap.Text = ">";
                    txtFullName.Clear();
                }
                else
                {
                    btnEmpSwap.Text = "<";

                    var EmpList = bu.GetEmployee(x => x.EmpID == EmployeeID);

                    string FullName = EmpList[0].TitleName + " " + EmpList[0].FirstName + " " + EmpList[0].LastName + " (" + EmpList[0].EmpID + ")";
                    txtFullName.Text = FullName;
                    EmployeeID = EmpList[0].EmpID;
                }
            }
        }

        private void SaveBranchWH()
        {
            try
            {
                bool flagSave = false;

                for (int i = 0; i < grdSaleEmp.RowCount; i++)
                {
                    if (grdSaleEmp.Rows[i].Cells["colEmployeeID"].Value != null)
                    {
                        flagSave = true;
                        break;
                    }
                }

                if (flagSave == true)
                {
                    string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                    string title = "ยืนยันการบันทึก!!";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    int ret = 0;

                    string NewEmpID = "";
                    string NewEmpName = "";

                    string OldEmpID = "";

                    var ListBranchWH = new List<tbl_BranchWarehouse>();

                    for (int i = 0; i < grdSaleEmp.RowCount; i++)
                    {
                        var _NewEmpID = grdSaleEmp.Rows[i].Cells["colEmployeeID"].Value;

                        if (_NewEmpID != null)
                        {
                            NewEmpID = _NewEmpID.ToString();
                            NewEmpName = grdSaleEmp.Rows[i].Cells["colEmployeeName"].Value.ToString();

                            OldEmpID = grdSaleEmp.Rows[i].Cells["colSaleEmpID"].Value.ToString();

                            var BranchWH = bu.GetBranchWarehouse(x => x.SaleEmpID == NewEmpID && x.WHType != 0); // edit by sailom .k 03/03/2022 for support pre-order

                            if (BranchWH != null)
                            {
                                string msg = "พนักงาน " + NewEmpName + " (" + NewEmpID + ") " + "อยู่เกินกว่า 1 Van ไม่ได้!!";
                                msg.ShowWarningMessage();

                                break;
                            }
                            else
                            {
                                var BranchWarehouse = bu.GetBranchWarehouse(x => x.SaleEmpID == OldEmpID);
                                if (BranchWarehouse != null)
                                {
                                    BranchWarehouse.SaleEmpID = NewEmpID;
                                    BranchWarehouse.EdDate = DateTime.Now;
                                    BranchWarehouse.EdUser = Helper.tbl_Users.Username;

                                    ListBranchWH.Add(BranchWarehouse);
                                }
                            }
                        }
                    }

                    if (ListBranchWH.Count > 0)
                    {
                        foreach (var data in ListBranchWH)
                        {
                            ret = bu.UpdateBranchWareHouseData(data);
                        }

                        if (ret == 1)
                        {
                            string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                            msg.ShowInfoMessage();

                            BindSaleEmp();
                        }
                        else
                        {
                            this.ShowProcessErr();
                        }
                    }
                }
                else
                {
                    string msg = "เลือกพนักงานขายที่ต้องการเปลี่ยน !!";
                    msg.ShowWarningMessage();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        #endregion

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret)
            {
                errList.SetErrMessage(validateSaveCtrls);
            }
            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }
            return ret;
        }

        private void btnAddVan_Click(object sender, EventArgs e)
        {
            if (!ValidateSave())
            {
                return;
            }

            string msg = "";

            if (txtFromWHCode.Text == txtToWHCode.Text)
            {
                msg += "Van (ต้นทาง) และ (ปลายทาง) : ห้ามซ้ำ !!\n";
            }

            var dt = new DataTable();
            dt = ((DataTable)grdSpecialVan.DataSource).Clone();

            dt.Rows.Clear();

            for (int i = 0; i < grdSpecialVan.Rows.Count; i++)
            {
                string _WHIDFrom = grdSpecialVan.Rows[i].Cells["colWHIDFrom"].Value.ToString();
                string _WHIDTo = grdSpecialVan.Rows[i].Cells["colWHIDTo"].Value.ToString();

                if (!string.IsNullOrEmpty(_WHIDFrom) && !string.IsNullOrEmpty(_WHIDTo))
                {
                    dt.Rows.Add(_WHIDFrom, _WHIDTo);
                }
            }

            if (grdSpecialVan.Rows.Count > 0)
            {
                var _WHIDFr = dt.AsEnumerable().FirstOrDefault(x => x.Field<string>("WHIDFrom") == txtFromWHCode.Text || x.Field<string>("WHIDFrom") == txtToWHCode.Text);
                var _WHIDTo = dt.AsEnumerable().FirstOrDefault(x => x.Field<string>("WHIDTo") == txtFromWHCode.Text || x.Field<string>("WHIDTo") == txtToWHCode.Text);

                if (_WHIDFr != null || _WHIDTo != null)
                {
                    msg += "Van (ต้นทาง) หรือ (ปลายทาง) : ซ้ำ !!\n";
                }
            }

            if (!string.IsNullOrEmpty(msg))
            {
                msg.ShowWarningMessage();
                return;
            }
            else
            {
                dt.Rows.Add(txtFromWHCode.Text, txtToWHCode.Text);
                grdSpecialVan.DataSource = dt;
            }
        }

        private void Save()
        {
            string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการบันทึก!!";

            if (!cfMsg.ConfirmMessageBox(title))
                return;

            if (!ValidateSave())
                return;

            List<int> ret = new List<int>();

            var AllBranchWH = bu.GetAllBranchWarehouse(x => x.WHType != 0); // edit by sailom .k 03/03/2022 for support pre-order

            if (!ValidateDupplicate(AllBranchWH))
                return;

            try
            {
                var branchWHMapList = new List<tbl_BranchWarehouseMapping>();
                for (int i = 0; i < grdSpecialVan.RowCount; i++)
                {
                    var branchWHMap = new tbl_BranchWarehouseMapping();
                    branchWHMap.WHIDFrom = grdSpecialVan.Rows[i].Cells["colWHIDFrom"].Value.ToString();
                    branchWHMap.WHIDTo = grdSpecialVan.Rows[i].Cells["colWHIDTo"].Value.ToString();
                    branchWHMapList.Add(branchWHMap);
                }

                var BranchWH = new List<tbl_BranchWarehouse>();

                for (int i = 0; i < branchWHMapList.Count; i++)
                {
                    string WHIDFr = branchWHMapList[i].WHIDFrom;
                    string WHIDTo = branchWHMapList[i].WHIDTo;

                    var BranchWHFr = AllBranchWH.FirstOrDefault(x => x.WHID == WHIDFr);
                    if (BranchWHFr != null)
                    {
                        BranchWHFr.SaleTypeID = 2;
                        BranchWH.Add(BranchWHFr);
                    }

                    var BranchWHTo = AllBranchWH.FirstOrDefault(x => x.WHID == WHIDTo);
                    if (BranchWHTo != null)
                    {
                        BranchWHTo.SaleTypeID = 3;
                        BranchWH.Add(BranchWHTo);
                    }

                }

                if (branchWHMapList.Count > 0)
                {
                    ret.Add(bu.SaveWithStore(branchWHMapList));
                }

                if (BranchWH.Count > 0)
                {
                    ret.Add(bbu.SaveWithStore(BranchWH));
                }

                if (ret.All(x=>x == 1))
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    msg.ShowInfoMessage();

                    BindVan();

                    txtFromWHCode.DisableTextBox(true);
                    txtToWHCode.DisableTextBox(true);

                    btnSearchFromWHCode.Enabled = false;
                    btnSearchToWHCode.Enabled = false;
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

        private bool ValidateDupplicate(List<tbl_BranchWarehouse> AllBranchWH)
        {

            bool ret = true;

            string msg = "";

            var chkBranchFr = AllBranchWH.FirstOrDefault(x => x.WHID == txtFromWHCode.Text);
            var chkBranchTo = AllBranchWH.FirstOrDefault(x => x.WHID == txtToWHCode.Text);

            if (chkBranchFr == null || chkBranchTo == null)
            {
                msg += "คลังต้นทาง หรือ คลังปลายทาง ไม่ถูกต้อง !!\n";
            }

            else if (txtFromWHCode.Text == txtToWHCode.Text)
            {
                msg += "คลังต้นทาง และ คลังปลายทาง : ห้ามซ้ำ !!\n";
            }
            else
            {
                var dt = new DataTable();
                dt = ((DataTable)grdSpecialVan.DataSource).Clone();

                dt.Rows.Clear();

                if (grdSpecialVan.Rows.Count > 0)
                {
                    for (int i = 0; i < grdSpecialVan.Rows.Count; i++)
                    {
                        string WHIDFrom = grdSpecialVan.Rows[i].Cells["colWHIDFrom"].Value.ToString();
                        string WHIDTo = grdSpecialVan.Rows[i].Cells["colWHIDTo"].Value.ToString();

                        if (!string.IsNullOrEmpty(WHIDFrom) && !string.IsNullOrEmpty(WHIDTo))
                        {
                            dt.Rows.Add(WHIDFrom, WHIDTo);
                        }
                    }

                    var _WHIDFr = dt.AsEnumerable().FirstOrDefault(x => x.Field<string>("WHIDFrom") == txtFromWHCode.Text || x.Field<string>("WHIDFrom") == txtToWHCode.Text);
                    var _WHIDTo = dt.AsEnumerable().FirstOrDefault(x => x.Field<string>("WHIDTo") == txtFromWHCode.Text || x.Field<string>("WHIDTo") == txtToWHCode.Text);

                    if (_WHIDFr != null || _WHIDTo != null)
                    {
                        msg += "คลังต้นทาง หรือ คลังปลายทาง : ซ้ำ !!\n";
                    }
                    else
                    {
                        dt.Rows.Add(txtFromWHCode.Text, txtToWHCode.Text);
                        grdSpecialVan.DataSource = dt;
                    }
                }
                else
                {
                    dt.Rows.Add(txtFromWHCode.Text, txtToWHCode.Text);
                    grdSpecialVan.DataSource = dt;
                }
            }
            
            if (!string.IsNullOrEmpty(msg))
            {
                msg.ShowWarningMessage();
                ret = false;
            }

            return ret;
        }

        private void txtFromWHCode_TextChanged(object sender, EventArgs e)
        {
            if (txtFromWHCode.TextLength == 6)
            {
                var branchWH = bu.GetBranchWarehouse(x=>x.WHID == txtFromWHCode.Text);
                if (branchWH != null)
                {
                    txtFromWHName.Text = branchWH.WHName;
                }
            }
        }

        private void txtToWHCode_TextChanged(object sender, EventArgs e)
        {
            if (txtToWHCode.TextLength == 6)
            {
                var branchWH = bu.GetBranchWarehouse(x => x.WHID == txtToWHCode.Text);
                if (branchWH != null)
                {
                    txtToWHName.Text = branchWH.WHName;
                }
            }
        }

        private void SelectDetails(DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow grdRows = null;

                if (e != null)
                {
                    if (e.RowIndex == -1)
                        return;
                    else
                        grdRows = grdSpecialVan.Rows[e.RowIndex];
                }
                else
                {
                    grdRows = grdSpecialVan.CurrentRow;
                }

                if (grdRows != null)
                {
                    txtFromWHCode.Text = grdRows.Cells["colWHIDFrom"].Value.ToString();
                    txtToWHCode.Text = grdRows.Cells["colWHIDTo"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void grdSpecialVan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectDetails(e);
        }

        private void grdSpecialVan_SelectionChanged(object sender, EventArgs e)
        {
            SelectDetails(null);
        }

        private void frmSalesTransfer_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();

            BindBranchWHDefault();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        #region Button_Event
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string tabName = tabPage.SelectedTab.Text.ToString();

            if (tabName == "ผ่าแวน")
            {
                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

                txtFromWHCode.DisableTextBox(false);
                txtToWHCode.DisableTextBox(false);

                btnSearchFromWHCode.Enabled = true;
                btnSearchToWHCode.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string tabName = tabPage.SelectedTab.Text.ToString();

            if (tabName == "ผ่าแวน")
            {
                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnAdd.Enabled = true;

                txtFromWHCode.DisableTextBox(true);
                txtToWHCode.DisableTextBox(true);

                btnSearchFromWHCode.Enabled = false;
                btnSearchToWHCode.Enabled = false;

                BindVan();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string tabName = tabPage.SelectedTab.Text.ToString();

            if (tabName == "ผ่าแวน")
            {
                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

                txtFromWHCode.DisableTextBox(false);
                txtToWHCode.DisableTextBox(false);

                txtFromWHCode.Text = "";
                txtFromWHName.Text = "";

                txtToWHCode.Text = "";
                txtToWHName.Text = "";

                btnSearchFromWHCode.Enabled = true;
                btnSearchToWHCode.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string tabName = tabPage.SelectedTab.Text.ToString();

            if (tabName == "ผ่าแวน")
            {
                Save();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEmpSwap_Click(object sender, EventArgs e)
        {
            SwapEmployee();
        }

        private void btnDeleteSalArea_Click(object sender, EventArgs e)
        {
            try //ลบตลาด ต้องไม่มีข้อมูลลูกค้า
            {
                if (lsbTransferTo.Items.Count == 0)
                {
                    string msg = "กรุณาเลือกตลาดที่ต้องการลบ !!";
                    msg.ShowWarningMessage();
                    return;
                }

                string cfMsg = "คุณแน่ใจหรือไม่ที่จะลบข้อมูลรายการนี้?";
                string title = "ทำการยืนยัน!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Cursor.Current = Cursors.WaitCursor;

                var SalAreaData = bu.GetSaleArea(x => ListSalAreaID.Contains(x.SalAreaID));

                List<string> SaleAreaID = new List<string>();

                for (int i = 0; i < lsbTransferTo.Items.Count; i++)
                {
                    var SaleArea = new tbl_SalArea();
                    SaleArea = SalAreaData.FirstOrDefault(x => x.SalAreaName == lsbTransferTo.Items[i].ToString());

                    var cData = bu.GetCustomerSalArea(SaleArea.SalAreaID);
                    if (cData.Count == 0)
                    {
                        SaleAreaID.Add(SaleArea.SalAreaID);
                    }
                }

                if (SaleAreaID.Count == 0)
                {
                    string msg = "ไม่สามารถลบตลาดที่มีลูกค้าได้ !!";
                    msg.ShowErrorMessage();
                    return;
                }

                int ret = 0;

                var SalArea = bu.GetSaleArea(x => SaleAreaID.Contains(x.SalAreaID));

                for (int i = 0; i < SalArea.Count; i++)
                {
                    SalArea[i].FlagDel = true;
                    SalArea[i].EdDate = DateTime.Now;
                    SalArea[i].EdUser = Helper.tbl_Users.Username;
                }

                foreach (var data in SalArea)
                {
                    ret = bu.UpdateSalAreaData(data);
                }

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    PrePareBranchWH();
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
            }
        }

        private void btnSaveSaleEmp_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (string.IsNullOrEmpty(txtFullName.Text) && string.IsNullOrEmpty(EmployeeID))
                {
                    string msg = "กรุณาเลือกพนักงานขายที่ต้องการเปลี่ยน !!";
                    msg.ShowWarningMessage();
                    return;
                }

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;

                var branchWH = bu.GetBranchWarehouse(x => x.WHID == cbbBranchWareHouse.Text);
                if (branchWH != null)
                {
                    branchWH.SaleEmpID = EmployeeID;
                    ret = bu.UpdateBranchWareHouseData(branchWH);
                }

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    BindEmpData(4);
                    BindBranchWareHouse();
                    SwapEmployee();
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

            Cursor.Current = Cursors.Default;
        }

        private void btnSaveSalAreaSeq_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string msg = "";

                if (lsbTransferFr.Items.Count == 0)
                {
                    msg = "กรุณาเลือก Van ที่ต้องการโอนย้ายตลาด !!";
                    msg.ShowWarningMessage();
                    return;
                }

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;

                var allSalArea = bu.GetSaleArea(x => ListSalAreaID.Contains(x.SalAreaID) && x.FlagDel == false);
                var ListSalArea = new List<tbl_SalArea>();

                short _Seq = 1;

                for (int i = 0; i < lsbTransferFr.Items.Count; i++)
                {
                    string saleName = lsbTransferFr.Items[i].ToString();
                    var data = allSalArea.FirstOrDefault(x => x.SalAreaName == lsbTransferFr.Items[i].ToString());

                    if (data != null)
                    {
                        data.Seq = _Seq;
                        data.EdDate = DateTime.Now;
                        data.EdUser = Helper.tbl_Users.Username;
                        ListSalArea.Add(data);
                        _Seq++;
                    }
                }

                for (int i = 0; i < lsbTransferTo.Items.Count; i++)
                {
                    var data = allSalArea.FirstOrDefault(x => x.SalAreaName == lsbTransferTo.Items[i].ToString());

                    if (data != null)
                    {
                        data.Seq = 0;
                        data.EdDate = DateTime.Now;
                        data.EdUser = Helper.tbl_Users.Username;
                        ListSalArea.Add(data);
                    }
                }

                foreach (var data in ListSalArea)
                {
                    ret = bu.UpdateSalAreaData(data);
                }

                if (ret == 1)
                {
                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    PrePareBranchWH();
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
            Cursor.Current = Cursors.Default;
        }//เปลี่ยนลำดับ SalArea

        private void btnClearGrdData_Click(object sender, EventArgs e)
        {
            if (grdSaleEmp.RowCount > 0)
            {
                for (int i = 0; i < grdSaleEmp.RowCount; i++)
                {   //Clear ข้อมูลในช่อง รหัส และช่อง เป็นพนักงาน ทั้งหมด
                    grdSaleEmp.Rows[i].Cells["colEmployeeID"].Value = null;
                    grdSaleEmp.Rows[i].Cells["colEmployeeName"].Value = null;
                }
            }
        }

        private void btnSaveBranchWH_Click(object sender, EventArgs e)
        {
            SaveBranchWH();
        }

        private void btnSearchFromWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(FromBranchWHControls, "เลือกคลังสินค้า", x => x.WHType != 0); // edit by sailom .k 03/03/2022 for support pre-order
        }

        private void btnSearchToWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(ToBranchWHControls, "เลือกคลังสินค้า", x => x.WHType != 0); // edit by sailom .k 03/03/2022 for support pre-order
        }

        private void btnAddTransferTo_Click(object sender, EventArgs e)
        {
            
            if (lsbTransferFr.Items.Count > 0)
            {
                lsbSelected(lsbTransferFr, lsbTransferTo);
            }
        }

        private void btnAddTransferFr_Click(object sender, EventArgs e)
        {
            if (lsbTransferTo.Items.Count > 0)
            {
                lsbSelected(lsbTransferTo, lsbTransferFr);
            }
        }

        private void btnAllTransferTo_Click(object sender, EventArgs e)
        {
            lsbSelectedAll(lsbTransferTo, lsbTransferFr);
        }

        private void btnAllTransferFr_Click(object sender, EventArgs e)
        {
            lsbSelectedAll(lsbTransferFr, lsbTransferTo);
        }

        private void btnTrasferFrToTop_Click(object sender, EventArgs e)
        {
            try
            {
                if (lsbTransferFr.SelectedItem != null)
                {
                    int OldIndex = lsbTransferFr.SelectedIndex;

                    if (lsbTransferFr.Items.Count > 0 && OldIndex != 0)
                    {
                        lsbTransferFr.Items.Insert(OldIndex - 1, lsbTransferFr.SelectedItem); // Add
                        lsbTransferFr.SelectedIndex = OldIndex - 1;
                        lsbTransferFr.Items.RemoveAt(OldIndex + 1);
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
            try
            {
                string tabName = tabPage.SelectedTab.Text;
                if (tabName == "ผ่าแวน")
                {
                    string cfMsg = "คุณแน่ใจหรือไม่ที่จะลบข้อมูลรายการนี้?";
                    string title = "ทำการยืนยัน!!";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    var data = new tbl_BranchWarehouseMapping();
                    data.WHIDFrom = grdSpecialVan.CurrentRow.Cells["colWHIDFrom"].Value.ToString();
                    data.WHIDTo = grdSpecialVan.CurrentRow.Cells["colWHIDTo"].Value.ToString();

                    var bwh = bu.GetAllBranchWarehouse(x => x.WHType != 0); // edit by sailom .k 03/03/2022 for support pre-order

                    var bwhList = new List<tbl_BranchWarehouse>();

                    var bwhFr = bwh.FirstOrDefault(x => x.WHID == data.WHIDFrom);
                    if (bwhFr != null)
                    {
                        bwhFr.SaleTypeID = 1;
                        bwhList.Add(bwhFr);
                    }

                    var bwhTo = bwh.FirstOrDefault(x => x.WHID == data.WHIDTo);
                    if (bwhTo != null)
                    {
                        bwhTo.SaleTypeID = 1;
                        bwhList.Add(bwhTo);
                    }

                    int ret = 0;
                    int retBranchWH = 0;

                    if (bwhList.Count > 0)
                    {
                        ret = bu.DeleteWithStore(data);
                        retBranchWH = bbu.SaveWithStore(bwhList);
                    }

                    if (ret > 0 && retBranchWH > 0)
                    {
                        string msg = "ลบข้อมูลเรียบร้อยแล้ว";
                        msg.ShowInfoMessage();

                        btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                        btnAdd.Enabled = true;
                        btnEdit.Enabled = true;
                        btnSave.Enabled = false;
                        btnCancel.Enabled = false;

                        BindVan();

                        txtFromWHCode.DisableTextBox(true);
                        txtToWHCode.DisableTextBox(true);

                        btnSearchFromWHCode.Enabled = false;
                        btnSearchToWHCode.Enabled = false;
                    }
                    else
                    {
                        this.ShowProcessErr();
                    }

                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
            

            
        }

        private void btnMoveItemDown_Click(object sender, EventArgs e)
        {
            if (lsbTransferFr.SelectedItem != null)
            {
                int OldIndex = lsbTransferFr.SelectedIndex;

                if (lsbTransferFr.Items.Count > 0)
                {
                    lsbTransferFr.Items.Insert(OldIndex + 2, lsbTransferFr.SelectedItem); // Add
                    lsbTransferFr.SelectedIndex = OldIndex + 2;
                    lsbTransferFr.Items.RemoveAt(OldIndex);
                }
            }
        }

        #endregion


        private void grdSaleEmp_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdSaleEmp.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdSpecialVan_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdSpecialVan.SetRowPostPaint(sender, e, this.Font);
        }
    }
}
