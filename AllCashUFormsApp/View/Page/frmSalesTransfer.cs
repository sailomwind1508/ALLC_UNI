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
        MenuBU menuBU = new MenuBU();
        public frmSalesTransfer()
        {
            InitializeComponent();
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

        private void btnAddTransferTo_Click(object sender, EventArgs e)
        {
            if (lsbTransferFr.Items.Count > 0)
            {
                lsbTransferFr.SelectedIndex = 0;

                lsbSelected(lsbTransferFr, lsbTransferTo);
            }
        }

        private void btnAddTransferFr_Click(object sender, EventArgs e)
        {
            if (lsbTransferTo.Items.Count > 0)
            {
                lsbTransferTo.SelectedIndex = 0;

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
            SelectItemToTop();
        }

        private void btnMoveItemDown_Click(object sender, EventArgs e)
        {
            SelectMoveItemDown();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tabName = tabPage.SelectedTab.Text.ToString();

            if (tabName == "โอนย้ายตลาด")
            {

            }
            else if (tabName == "เปลี่ยนพนักงานขาย")
            {
                BindSaleEmp();
            }
        }

        private void grdSaleEmp_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdSaleEmp.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnEmpSwap_Click(object sender, EventArgs e)
        {
            SwapEmployee();
        }

        private void btnDeleteSalArea_Click(object sender, EventArgs e)
        {
            DeleteSalArea();
        }

        private void btnSaveSaleEmp_Click(object sender, EventArgs e)
        {
            SaveSaleEmp();
        }

        private void btnSaveSalAreaSeq_Click(object sender, EventArgs e)
        {
            //เปลี่ยนลำดับ SalArea
            try
            {
                if (lsbTransferFr.Items.Count == 0)
                {
                    string msg = "กรุณาเลือก Van ที่ต้องการโอนย้ายตลาด !!";
                    msg.ShowWarningMessage();
                    return;
                }
                else
                {
                    string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                    string title = "ยืนยันการบันทึก!!";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    int ret = 0;

                    var allSalArea = bu.GetSaleArea(x => ListSalAreaID.Contains(x.SalAreaID) && x.FlagDel == false);

                    var ListSalArea = new List<tbl_SalArea>();

                    short Seq = 1;

                    for (int i = 0; i < lsbTransferFr.Items.Count; i++)
                    {
                        var SaleArea = new tbl_SalArea();

                        SaleArea = allSalArea.FirstOrDefault(x => x.SalAreaName == lsbTransferFr.Items[i].ToString());

                        if (SaleArea != null)
                        {
                            SaleArea.Seq = Seq++;
                            ListSalArea.Add(SaleArea);
                        }
                    }

                    foreach (var data in ListSalArea)
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
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                return;
            }
        }

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
                return;
            }

        }

        #endregion

        #region Method
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

                EmployeeID = "";

                var dtSaleEmp = bu.GetSaleEmployee(PositionID);

                ListWHID.Clear();


                if (dtSaleEmp.Rows.Count > 0)
                {
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
            }
            catch (Exception ex)
            {
                ex.Message.ShowWarningMessage();
                return;
            }
        }

        private void BindBranchWareHouse()
        {
            var BranchWH = new List<tbl_BranchWarehouse>();
            BranchWH.Add(new tbl_BranchWarehouse { SaleEmpID = "", WHCode = "==เลือก==" });
            BranchWH.AddRange(bu.GetAllBranchWarehouse(x => x.WHType == 1 && ListWHID.Contains(x.WHID)));

            if (BranchWH.Count > 0)
            {
                cbbBranchWareHouse.BindDropdownList(BranchWH, "WHCode", "SaleEmpID", 0);
            }
            else
            {
                BindBranchWHDefault();
            }
        }

        private void BindSalArea()
        {
            var AllSalAreaID = bu.GetSaleAreaDistrict(x => x.WHID.Contains(cbbBranchWareHouse.Text));

            ListSalAreaID.Clear(); //

            if (AllSalAreaID.Count > 0)
            {
                ListSalAreaID = AllSalAreaID.Select(x => x.SalAreaID).Distinct().ToList();

                var allSalArea = bu.GetSaleArea(x => ListSalAreaID.Contains(x.SalAreaID) && x.FlagDel == false).OrderBy(x => x.Seq).ToList();

                for (int i = 0; i < allSalArea.Count; i++)
                {
                    lsbTransferFr.Items.Add(allSalArea[i].SalAreaName);
                }
            }
            else //ไม่มีข้อมูล SalAreaDistrict
            {
                if (!string.IsNullOrEmpty(cbbBranchWareHouse.Text))
                {
                    string WHID = cbbBranchWareHouse.Text.Substring(3, 3);

                    var allSalArea = bu.GetSaleArea(x => x.SalAreaName.Contains(WHID) && x.FlagDel == false).OrderBy(x => x.Seq).ToList();

                    for (int i = 0; i < allSalArea.Count; i++)
                    {
                        lsbTransferFr.Items.Add(allSalArea[i].SalAreaName);
                        ListSalAreaID.Add(allSalArea[i].SalAreaID);
                    }
                }
            }
        }

        private void PrePareBranchWH()
        {
            lsbTransferTo.ClearListBoxItem();
            lsbVan.ClearListBoxItem();
            lsbTransferFr.ClearListBoxItem();

            txtSaleEmpID.Clear();
            txtFirstName.Clear();

            if (cbbBranchWareHouse.SelectedIndex > 0)
            {
                string SaleEmpID = cbbBranchWareHouse.SelectedValue.ToString();
                string VanName = "";

                if (!string.IsNullOrEmpty(SaleEmpID))
                {
                    var Emp = bu.GetEmployee(x => x.EmpID == SaleEmpID && x.FlagDel == false);

                    VanName = Emp[0].TitleName + " " + Emp[0].FirstName;
                    lsbVan.Items.Add(VanName);
                }

                txtSaleEmpID.Text = SaleEmpID;
                txtFirstName.Text = VanName;

                BindSalArea();
            }
        }

        public void lsbSelected(ListBox TransferFr, ListBox TransferTo)
        {
            string SalAreaName = TransferFr.SelectedItem.ToString();

            TransferTo.Items.Add(SalAreaName);

            TransferFr.Items.RemoveAt(TransferFr.SelectedIndex);
        }

        public void lsbSelectedAll(ListBox TransferFr, ListBox TransferTo)
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

        private void SelectItemToTop()
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

        private void SelectMoveItemDown()
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

        private void BindSaleEmp()
        {
            grdSaleEmp.AutoGenerateColumns = false;

            DataTable newDT = new DataTable();
            newDT = bu.GetAllSaleEmployee();
            grdSaleEmp.DataSource = newDT;
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

        private void SaveSaleEmp()
        {
            try
            {
                if (string.IsNullOrEmpty(txtFullName.Text) && string.IsNullOrEmpty(EmployeeID))
                {
                    string msg = "กรุณาเลือกพนักงานขายที่ต้องการเปลี่ยน !!";
                    msg.ShowWarningMessage();
                    return;
                }
                else
                {
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
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                return;
            }
        }

        private void DeleteSalArea()
        {
            try //ลบตลาด ต้องไม่มีข้อมูลลูกค้า
            {
                if (lsbTransferTo.Items.Count == 0)
                {
                    string msg = "กรุณาเลือกตลาดที่ต้องการลบ !!";
                    msg.ShowWarningMessage();
                    return;
                }
                else
                {
                    string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
                    string title = "ทำการยืนยัน!!";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

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

                    if (SaleAreaID.Count > 0)
                    {
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
                            return;
                        }
                    }
                    else
                    {
                        string msg = "ไม่สามารถลบตลาดที่มีลูกค้าได้ !!";
                        msg.ShowErrorMessage();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                return;
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

                            var BranchWH = bu.GetBranchWarehouse(x => x.SaleEmpID == NewEmpID && x.WHType == 1);

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
                            return;
                        }
                    }
                }
                else
                {
                    string msg = "เลือกพนักงานขายที่ต้องการเปลี่ยน !!";
                    msg.ShowWarningMessage();
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                return;
            }
        }

        #endregion

        private void frmSalesTransfer_Load(object sender, EventArgs e)
        {
            InitPage();

            BindBranchWHDefault();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }
    }
}
