using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.Controller;
using System.IO;
using AllCashUFormsApp.View.UControl;
namespace AllCashUFormsApp.View.Page
{
    public partial class frmProductInfo : Form
    {
        Product bu = new Product();
        MenuBU menuBU = new MenuBU();
        List<string> readOnlyPnlEditControls = new List<string>();
        static DataTable dtProduct = new DataTable();
        List<tbl_ProductUom> uoms = new List<tbl_ProductUom>();
        public static string prdsubgroupID = "";
        Dictionary<Control, Label> validateCtrls = new Dictionary<Control, Label>(); // Validate Save
        Dictionary<int, string> initDataGridList = new Dictionary<int, string>();
        bool flagNew = true;
        DataTable tbl_ProductPriceGroupList = new DataTable();

        public frmProductInfo()
        {
            InitializeComponent();

            readOnlyPnlEditControls = new string[] { txtProductID.Name }.ToList(); //เปิด ปิด Panel Edit

            validateCtrls.Add(txtProductID, lblProID);
            validateCtrls.Add(txtProductRefCode, lblProRefCode);
            validateCtrls.Add(txtProductName, lblProName);
            validateCtrls.Add(txtProductShortName, lblProShortName);
            validateCtrls.Add(ddlProType, lblProType);
            validateCtrls.Add(ddlProGroup, lblGroupPro);

            initDataGridList = new Dictionary<int, string>() { { 0, "combobox" }, { 1, "0" }, { 2, "0" }, { 3, "0" }, { 4, "0" }, { 5, "0" }, { 6, "0" }, { 7, "0" } };
        }

        #region Button Event
        private void btnClose_Click(object sender, EventArgs e)
        {
            dtProduct = new DataTable();
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnSearch_Click";
            msg.WriteLog(this.GetType());

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                pnlEdit.ClearControl();

                OpenPanelEdit(false);

                SetDefaultNumber();

                dtProduct = new DataTable();

                int _flagDel = rdoN.Checked ? 0 : 1;
                int _ProductGroupID = ddlPrdGroup.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlPrdGroup.SelectedValue);
                int _ProductSubGroupID = ddlPrdSubGroup.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlPrdSubGroup.SelectedValue);

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@flagDel", _flagDel);
                _params.Add("@ProductGroupID", _ProductGroupID);
                _params.Add("@ProductSubGroupID", _ProductSubGroupID);
                _params.Add("@Search", txtSearch.Text);
                dtProduct = bu.proc_tbl_Product_Data(_params);
                grdPro.DataSource = dtProduct;
                lblgrdQty.Text = dtProduct.Rows.Count.ToNumberFormat();

                SetControl_PanelSearch();
                SelectProductDetails(null);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                Cursor.Current = Cursors.Default;
            }

            msg = "end frmProductInfo=>btnSearch_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnAdd_Click";
            msg.WriteLog(this.GetType());

            flagNew = true;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnRemove.Enabled = false;

            pnlGridView.Enabled = false;
            grdPro.Enabled = false;

            pnlEdit.ClearControl();

            OpenPanelEdit(true);
            rdoCancel.Enabled = false;

            SetProductUomTable();

            SetPrdUomGridView(false);
            btnProGroup.Enabled = true; // ปุ่มเพิ่มกลุ่มสินค้า

            txtProductID.DisableTextBox(false);
            txtProductID.Focus(); 

            SetDefaultNumber();

            chkTablet.Enabled = true;

            grdPro.Enabled = false;

            rdoNormal.Checked = true;
            rdoVatN.Checked = true;

            msg = "end frmProductInfo=>btnAdd_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Image";
            openFileDialog1.Filter = "bitmap files (*.bit)|*.bit|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(openFileDialog1.FileName);
                //picProductImg.Image = image.ImageToByte().byteArrayToImage();
                picProductImg.Image = image.ImageToByte().byteArrayToImage(228, 228);

                //MenuPic.Image = bitmap.ImageToByte(228, 228).byteArrayToImage(228, 228);
            }
        }

        private void btnSaveSeq_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnSaveSeq_Click";
            msg.WriteLog(this.GetType());

            int ret = 0;

            try
            {
                var ListProID = new List<string>();
                for (int i = 0; i < grdPro.RowCount; i++)
                {
                    string PrdID = grdPro.Rows[i].Cells["colProductID"].Value.ToString();
                    ListProID.Add(PrdID);
                }
                var _ProductID = string.Join(",", ListProID);
                var ProductList = bu.SelectProductList(_ProductID);
                if (ProductList != null && ProductList.Count > 0)
                {
                    var dtPro = (DataTable)grdPro.DataSource;

                    for (int i = 0; i < ProductList.Count; i++)
                    {
                        string proID = ProductList[i].ProductID;

                        DataRow r = dtPro.AsEnumerable().FirstOrDefault(x => x.Field<string>("ProductID") == proID);
                        if (r != null)
                        {
                            ProductList[i].Seq = Convert.ToInt16(r["Seq"]);
                            ProductList[i].EdDate = DateTime.Now;
                            ProductList[i].EdUser = Helper.tbl_Users.Username;
                        }
                    }
                }

                foreach (var Products in ProductList)
                {
                    ret = bu.UpdateData(Products);
                }

                if (ret == 1)
                {
                    string message = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    message.ShowInfoMessage();
                    btnSearch.PerformClick();
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

            msg = "end frmProductInfo=>btnSaveSeq_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnProSubGroup_Click(object sender, EventArgs e)
        {
            frmProductSubGroup frm = new frmProductSubGroup();
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(prdsubgroupID))
            {
                ddlProSubGroup.SelectedValue = Convert.ToInt32(prdsubgroupID);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnCopy_Click";
            msg.WriteLog(this.GetType());

            flagNew = true;

            pnlGridView.Enabled = false;
            grdPro.Enabled = false;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            OpenPanelEdit(true);

            SetPrdUomGridView(false);

            btnProGroup.Enabled = true; // ปุ่มเพิ่มกลุ่มสินค้า

            txtProductID.Text = "";
            txtProductID.DisableTextBox(false);

            txtProductID.Focus();

            ddlProUom.Enabled = true;

            rdoNormal.Checked = true;
            rdoCancel.Enabled = false;

            msg = "end frmProductInfo=>btnCopy_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnEdit_Click";
            msg.WriteLog(this.GetType());

            flagNew = false;

            pnlGridView.Enabled = false;
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            OpenPanelEdit(true);

            SetPrdUomGridView(false);

            //btnProGroup.Enabled = false; // ปุ่มเพิ่มกลุ่มสินค้า
            btnProGroup.Enabled = true; // ปุ่มเพิ่มกลุ่มสินค้า

            txtProductRefCode.Focus();

            if (Helper.tbl_Users.RoleID == 5 || Helper.tbl_Users.RoleID == 5 || Helper.tbl_Users.RoleID == 10)
                rdoCancel.Enabled = true;
            else
                rdoCancel.Enabled = false;

            msg = "end frmProductInfo=>btnEdit_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnCancel_Click";
            msg.WriteLog(this.GetType());

            Cursor.Current = Cursors.WaitCursor;

            flagNew = true;

            pnlGridView.Enabled = true;

            pnlEdit.ClearControl();

            grdPrdUom.Rows.Clear();
            SetPrdUomGridView(true);

            ddlProType.SelectedIndex = 0;//ประเภทสินค้า -edit
            ddlProGroup.SelectedIndex = 0;//กลุ่มสินค้า -edit
            ddlProSubGroup.SelectedIndex = 0;//กลุ่มย่อยสินค้า -edit
            ddlProUom.SelectedIndex = 0;//หน่วยเล็กที่สุด
            ddlSaleType.SelectedIndex = 0;//รูปแบบการขาย

            OpenPanelEdit(false);

            grdPro.Enabled = true;

            SelectProductDetails(null);

            SetControl_PanelSearch();

            Cursor.Current = Cursors.Default;

            msg = "end frmProductInfo=>btnCancel_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnSave_Click";
            msg.WriteLog(this.GetType());

            try
            {
                if (!ValidateSave())
                    return;
                if (!ValidateBaseQty())
                    return;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                if (ValidatePrdUom()) //เช็คหน่วยสินค้าซ้ำ 2022/04/26 adisorn
                {
                    msg = "โครงสร้างหน่วยสินค้า -> หน่วยสินค้าซ้ำ !!";
                    if (!string.IsNullOrEmpty(msg))
                    {
                        msg.ShowWarningMessage();
                        return;
                    }
                }

                Cursor.Current = Cursors.WaitCursor;

                var PrdList = bu.SelectProductList(txtProductID.Text);

                if (flagNew == true)
                {
                    if (!ValidateData(PrdList.Count))
                        return;
                }

                var ProductUomSet = bu.GetProductUomSet_Single(txtProductID.Text); //adisorn 10/08/2022

                if (PrdList != null && PrdList.Count > 0) //Remove OldData tbl_ProductUomSet , tbl_ProductPriceGroup
                {
                    Remove_ProductUomSet(ProductUomSet);
                    Remove_ProductPriceGroup();
                }

                List<int> ret = new List<int>();

                var PrdUomSetList = new List<tbl_ProductUomSet>();
                var PrdPriceGroupList = new List<tbl_ProductPriceGroup>();
                var PrdUom = bu.GetProductUOM();
                for (int i = 0; i < grdPrdUom.Rows.Count; i++)
                {
                    string BaseQty = grdPrdUom.Rows[i].Cells["colBaseQty"].Value.ToString();
                    //int _colBuyPrice = Convert.ToInt32(grdList2.Rows[i].Cells["colBuyPrice"].Value);

                    if (BaseQty != "0")//&& _colUnit != -1 && _colBuyPrice > 0
                    {
                        int _Unit = Convert.ToInt32(grdPrdUom.Rows[i].Cells["colUnit"].Value);

                        var PrdUomSet = new tbl_ProductUomSet();//
                        PrdUomSet.UomSetID = _Unit;
                        PrdUomSet.UomSetName = PrdUom.FirstOrDefault(x => x.ProductUomID == _Unit).ProductUomName;
                        PrdUomSet.BaseQty = Convert.ToInt32(BaseQty);
                        PrdUomSet.Weight = Convert.ToDecimal(grdPrdUom.Rows[i].Cells["colWeight"].Value);


                        if (ProductUomSet.Count > 0) //26-07-2022 Adisorn ดึงUomCode จากหน่วยเก่า มาใช้
                        {
                            var uomCode = ProductUomSet.FirstOrDefault(x => x.UomSetID == _Unit);
                            if (uomCode != null)
                                PrdUomSet.UomCode = uomCode.UomCode;
                            else
                                PrdUomSet.UomCode = "";
                        }
                        else
                        {
                            PrdUomSet.UomCode = "";
                        }
                        
                        PrePareSave_ProductUomSet(PrdUomSet);
                        PrdUomSetList.Add(PrdUomSet);

                        var PrdPriceGroup = new tbl_ProductPriceGroup();//
                        PrdPriceGroup.ProductUomID = _Unit;
                        PrdPriceGroup.SellPrice = Convert.ToDecimal(grdPrdUom.Rows[i].Cells["colSellPrice"].Value);

                        decimal _BuyPrice = Convert.ToDecimal(grdPrdUom.Rows[i].Cells["colBuyPrice"].Value);
                        PrdPriceGroup.BuyPrice = _BuyPrice;

                        PrdPriceGroup.SellPriceVat = Convert.ToDecimal(grdPrdUom.Rows[i].Cells["colSellPriceVat"].Value);

                        decimal vat = (_BuyPrice * 7) / 100;
                        decimal BuyPriceVat = _BuyPrice + vat.ToDecimalN2();
                        PrdPriceGroup.BuyPriceVat = BuyPriceVat;

                        PrdPriceGroup.ComPrice = Convert.ToDecimal(grdPrdUom.Rows[i].Cells["colComPrice"].Value);

                        PrePareSave_ProductPriceGroup(PrdPriceGroup);

                        PrdPriceGroupList.Add(PrdPriceGroup);
                    }
                }

                if (PrdUomSetList.Count > 0)//SaveProductUomSet
                {
                    foreach (var data in PrdUomSetList)     
                    {
                        ret.Add(bu.UpdateData(data));
                    }
                }

                if (PrdPriceGroupList.Count > 0) //SaveProductPriceGroup
                {
                    foreach (var data in PrdPriceGroupList)
                    {
                        ret.Add(bu.UpdateData(data));
                    }
                }

                var prdData = PrePareSave_Product(PrdList, PrdUomSetList);

                ret.Add(bu.UpdateData(prdData)); //SaveProduct

                if (ret.All(x => x == 1))
                {
                    UpdateMasterData(); //last edit by sailom .k 04/10/2022

                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!!"; // \nกรณีที่มีการ เพิ่ม แก้ไข ลบ สินค้า ให้กดปุ่มอัพเดตสินค้า หรือปิด-เปิด ระบบใหม่เพื่ออัพเดตสินค้าทุกครั้ง!!!";
                    msg.ShowInfoMessage();

                    pnlGridView.Enabled = true;
                    OpenPanelEdit(false);
                    grdPro.Enabled = true;

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
                Cursor.Current = Cursors.Default;
                ex.Message.ShowErrorMessage();
            }

            msg = "end frmProductInfo=>btnSave_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnPrdCheck_Click(object sender, EventArgs e)
        {
            string _msg = "start frmProductInfo=>btnPrdCheck_Click";
            _msg.WriteLog(this.GetType());

            bool ProIsCorrect = false;

            var dt = bu.GetProductViewCheck(grdPro.CurrentRow.Cells["colProductID"].Value.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.AsEnumerable().First();

                if (string.IsNullOrEmpty(r["ProductID"].ToString()) || string.IsNullOrEmpty(r["ProductName"].ToString())
                    || string.IsNullOrEmpty(r["ProductShortName"].ToString()) || string.IsNullOrEmpty(r["Flavour"].ToString())
                    || string.IsNullOrEmpty(r["VatType"].ToString()) || string.IsNullOrEmpty(r["ProductSubGroupID"].ToString())
                    || string.IsNullOrEmpty(r["ProductSubGroupName"].ToString()) || string.IsNullOrEmpty(r["ProductGroupID"].ToString())
                    || string.IsNullOrEmpty(r["ProductGroupName"].ToString()) || string.IsNullOrEmpty(r["UomSetID"].ToString())
                    || string.IsNullOrEmpty(r["UomSetName"].ToString()) || string.IsNullOrEmpty(r["BaseQty"].ToString()))
                {
                    ProIsCorrect = true;
                }
            }
            else
            {
                ProIsCorrect = true;
            }

            if (ProIsCorrect == true)
            {
                string msg = "สินค้าไม่สามารถส่งข้อมูลเข้าTabletได้ !!\n";
                msg += "กรุณาตรวจสอบสินค้าใหม่อีกครั้ง\n";
                msg.ShowWarningMessage();
                return;
            }

            _msg = "end frmProductInfo=>btnPrdCheck_Click";
            _msg.WriteLog(this.GetType());
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnRemove_Click";
            msg.WriteLog(this.GetType());

            try
            {
                string cfMsg = "คุณแน่ใจหรือไม่ที่จะลบข้อมูลรายการนี้?";
                string title = "ทำการยืนยัน!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;

                var Products = bu.SelectProductList(txtProductID.Text);
                if (Products.Count > 0 && Products != null)
                {
                    Products[0].FlagDel = true;
                    Products[0].IsFulfill = true; //edit by sailom .k 08/12/2021
                    Products[0].EdDate = DateTime.Now;
                    Products[0].EdUser = Helper.tbl_Users.Username;
                    ret = bu.UpdateData(Products[0]);
                }

                var PrdUomSet = bu.GetProductUOMSet(x => x.ProductID == txtProductID.Text);
                if (PrdUomSet.Count > 0 && PrdUomSet != null)
                {
                    for (int i = 0; i < PrdUomSet.Count; i++)
                    {
                        PrdUomSet[i].FlagDel = true;
                        PrdUomSet[i].EdDate = DateTime.Now;
                        PrdUomSet[i].EdUser = Helper.tbl_Users.Username;
                    }
                    foreach (var data in PrdUomSet)
                    {
                        ret = bu.UpdateData(data);
                    }
                }

                var PrdPriceGroup = bu.GetProductPriceGroup(x => x.ProductID == txtProductID.Text);
                if (PrdPriceGroup.Count > 0 && PrdPriceGroup != null)
                {
                    for (int i = 0; i < PrdPriceGroup.Count; i++)
                    {
                        PrdPriceGroup[i].FlagDel = true;
                        PrdPriceGroup[i].EdDate = DateTime.Now;
                        PrdPriceGroup[i].EdUser = Helper.tbl_Users.Username;
                    }
                    foreach (var data in PrdPriceGroup)
                    {
                        ret = bu.UpdateData(data);
                    }
                }

                btnSearch.PerformClick();
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }

            msg = "end frmProductInfo=>btnRemove_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnPrint_Click";
            msg.WriteLog(this.GetType());

            FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@FlagDel", 0);
            this.OpenExcelReportsPopup("รายงานสินค้าทั้งหมด", "proc_GetProductDataToExcel.xslt", "proc_GetProductDataToExcel", _params, true);

            msg = "end frmProductInfo=>btnPrint_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnProGroup_Click(object sender, EventArgs e)
        {
            frmProductGroup frm = new frmProductGroup();
            frm.ShowDialog();

            var PrdGroupList = bu.GetProductGroupNonFlag(x => x.FlagDel == false);
            int index = 0;
            if (PrdGroupList.Count > 1)
                index = PrdGroupList.Count - 1;

            ddlProGroup.BindDropdownList(PrdGroupList, "ProductGroupName", "ProductGroupID", index);
        }

        #endregion

        #region Event Method
        private void frmProductInfo_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
            InitialData();
        }

        private void ddlPrdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPrdType.SelectedIndex > 0)
            {
                int prdtypeID = Convert.ToInt32(ddlPrdType.SelectedValue);
                SetProductGroup(prdtypeID, ddlPrdGroup); // ProductGroup 
            }
            else
            {
                ddlProGroupNoData(ddlPrdGroup);
                ddlProSubGroupNoData(ddlPrdSubGroup);
            }
        }

        private void ddlPrdGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPrdGroup.SelectedIndex > 0)
            {
                var prdSubGroup = bu.GetProductSubGroup(x => x.FlagDel == false);
                var _prdSubGroup = new List<tbl_ProductSubGroup>();
                _prdSubGroup.Add(new tbl_ProductSubGroup { ProductSubGroupID = -1, ProductSubGroupName = "==เลือก==" });
                _prdSubGroup.AddRange(prdSubGroup);
                ddlPrdSubGroup.BindDropdownList(_prdSubGroup, "ProductSubGroupName", "ProductSubGroupID");
            }
            else
            {
                ddlProSubGroupNoData(ddlPrdSubGroup);
            }
        }

        private void grdPro_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPro.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            int[] numberCell = new int[] { 6 };
            grdPro.SetCellNumberOnly(sender, e, numberCell.ToList());
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void grdPro_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string ProSeq_cell = grdPro.CurrentRow.Cells["colSeq"].Value.ToString();  //ลำดับ

            if (ProSeq_cell == "")
            {
                grdPro.CurrentRow.Cells["colSeq"].Value = 0;
            }
        }

        private void grdPro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectProductDetails(e);
        }

        private void ddlProType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProType.SelectedIndex != 0)
            {
                int protypeID = Convert.ToInt32(ddlProType.SelectedValue);
                var progroup = bu.GetProductGroup().Where(x => x.FlagDel == false && x.ProductTypeID == protypeID).ToList();

                if (progroup != null && progroup.Count > 0)
                {
                    ddlProGroup.BindDropdownList(progroup, "ProductGroupName", "ProductGroupID", 0);
                }
                else
                {
                    ddlProGroup.DataSource = null;

                    string msg = "ไม่พบข้อมูล กรุณาเลือก --> ประเภทสินค้าใหม่ !! ";
                    msg.ShowWarningMessage();
                }
            }
            else
            {
                var progroup = bu.GetProductGroup().Where(x => x.FlagDel == false).ToList();
                ddlProGroup.BindDropdownList(progroup, "ProductGroupName", "ProductGroupID", 0);
            }
        }

        private void ddlProGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            var prosubgroup = bu.GetProductSubGroup(x => x.FlagDel == false);
            ddlProSubGroup.BindDropdownList(prosubgroup, "ProductSubGroupName", "ProductSubGroupID");
        }

        private void grdList2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdPrdUom.SetRowPostPaint(sender, e, this.Font);
        }

        private void txtReorderPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtReorderPoint.SetTextBoxNumberWithDot(e);
        }

        private void txtMinPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtMinPoint.SetTextBoxNumberWithDot(e);
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtWeight.SetTextBoxNumberWithDot(e);
        }

        private void txtSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtSize.SetTextBoxNumberWithDot(e);
        }

        private void grdPro_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
            tb.KeyPress -= grdPro_KeyPress;
            tb.KeyPress += grdPro_KeyPress;
        }

        private void grdList2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
                    tb.PreviewKeyDown -= grdList2_PreviewKeyDown;
                    tb.PreviewKeyDown += grdList2_PreviewKeyDown;

                    tb.KeyPress -= grdList2_KeyPress;
                    tb.KeyPress += grdList2_KeyPress;
                }
            }
            catch (Exception)
            {

            }
        }

        private void grdList2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int[] numberCell = new int[] { 2, 3, 4, 5, 6, 7 };
            grdPrdUom.SetCellNumberOnly(sender, e, numberCell.ToList());
        }

        private void grdList2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grdPrdUom.CurrentRow == null )
                    return;
                if (e.ColumnIndex == -1)
                    return;
                if (e.RowIndex == -1)
                    return;

                if (grdPrdUom.Columns[e.ColumnIndex].Name != "colUnit")
                {
                    if (grdPrdUom.CurrentRow.Cells[e.ColumnIndex].Value == null)
                    {
                        grdPrdUom.CurrentRow.Cells[e.ColumnIndex].Value = 0.00;
                    }
                    else
                    {
                        if (grdPrdUom.Columns[e.ColumnIndex].Name != "colBaseQty")//จำนวน
                        {
                            decimal colNo = Convert.ToDecimal(grdPrdUom.CurrentRow.Cells[e.ColumnIndex].Value);

                            grdPrdUom.CurrentRow.Cells[e.ColumnIndex].Value = colNo.ToDecimalN2();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowWarningMessage();
            }
        }

        private void grdList2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                DataGridView grd = null;

                if (sender is DataGridViewTextBoxEditingControl)
                {
                    DataGridViewTextBoxEditingControl _grd = (DataGridViewTextBoxEditingControl)sender;
                    grd = _grd.EditingControlDataGridView;
                }
                else if (sender is DataGridViewComboBoxEditingControl)
                {
                    DataGridViewComboBoxEditingControl _grd = (DataGridViewComboBoxEditingControl)sender;
                    grd = _grd.EditingControlDataGridView;
                }
                if (grd != null)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        string _Unit = "";
                        string _BaseQty = "";
                        string _BuyPrice = "";
                        decimal _colBaseQty = 0;
                        decimal _colBuyPrice = 0;
                        int _colUnit = 0;
                        for (int i = 0; i < grdPrdUom.RowCount; i++)
                        {
                            _colBaseQty = Convert.ToDecimal(grdPrdUom.Rows[i].Cells["colBaseQty"].EditedFormattedValue).ToDecimalN2();
                            _colUnit = Convert.ToInt32(grdPrdUom.Rows[i].Cells["colUnit"].Value);
                            _colBuyPrice = Convert.ToDecimal(grdPrdUom.Rows[i].Cells["colBuyPrice"].EditedFormattedValue).ToDecimalN2();

                            if (_colUnit == -1)
                                _Unit = "กรุณาเลือกหน่วย\n";
                            if (_colBaseQty <= 0)
                                _BaseQty = "กรุณากรอกจำนวน\n";
                            if (_colBuyPrice <= 0)
                                _BuyPrice = "กรุณากรอกราคาชื้อก่อนภาษี\n";
                        }
                        
                        string msg = "";

                        if (_Unit != "")
                            msg += "กรุณาเลือกหน่วย\n";
                        if (_BaseQty != "")
                            msg += "กรุณากรอกจำนวน\n";
                        if (_BuyPrice != "")
                            msg += "กรุณากรอกราคาชื้อก่อนภาษี\n";
                        if (!string.IsNullOrEmpty(msg))
                        {
                            
                            msg.ShowWarningMessage();
                            return;
                        }

                        if (_colBaseQty > 0 && _colUnit != -1 && _colBuyPrice > 0)// && colSellPrice > 0 && colSellPriceVat > 0
                        {
                            grd.ClearSelection();

                            if (grd.CurrentCell.ColumnIndex == 7)
                            {
                                grdPrdUom.AddNewRow(initDataGridList, 1, "", grd.CurrentCell.RowIndex + 1, true, uoms, bu, this, 0); //
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowWarningMessage();
            }
        }

        private void grdList2_KeyDown(object sender, KeyEventArgs e)
        {
            if (grdPrdUom.CurrentRow.Index != 0 && e.KeyCode == Keys.Delete)
            {
                grdPrdUom.SetDeleteKeyDown(sender, e);
            }
        }

        private void grdPrdUom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1)
            {
                grdPrdUom.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                grdPrdUom.EndEdit();
            }
            else if (grdPrdUom.EditMode != DataGridViewEditMode.EditOnEnter)
            {
                grdPrdUom.EditMode = DataGridViewEditMode.EditOnEnter;
                grdPrdUom.BeginEdit(false);
            }
        }

        private void frmProductInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        #endregion

        #region Private Method
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

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnAdd.Enabled = true;
            btnPrint.Enabled = true;

            uoms.AddRange(bu.GetUOM());

            grdPro.RowsDefaultCellStyle.BackColor = Color.White;
            grdPro.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
        }

        private void SetDefaultNumber()
        {
            txtReorderPoint.Text = "0";
            txtMinPoint.Text = "0";
            txttextBox1.Text = "0.00";//ราคาต้นทุนเฉลี่ย = 0.00  --เกิดจากการ Sum ???
            txttextBox2.Text = "0.00";//มูลค่าสินค้า = 0.00  --เกิดจากการ Sum ???
            txtWeight.Text = "0";
            txtSize.Text = "0";

            chkTablet.Checked = false;
            button1.Enabled = false; // ดึงข้อมูลจาก Center
        }

        public void BindProductInfo(string prodcutID)
        {
            txtSearch.Text = prodcutID;

            btnSearch.PerformClick();
        }

        private void ddlProGroupNoData(ComboBox ddl)
        {
            var ProGroup = new List<tbl_ProductGroup>();
            ProGroup.Add(new tbl_ProductGroup { ProductGroupID = -1, ProductGroupName = "==เลือก==" });
            ddl.BindDropdownList(ProGroup, "ProductGroupName", "ProductGroupID");
        }

        private void ddlProSubGroupNoData(ComboBox ddl)
        {
            var ProSubGroup = new List<tbl_ProductSubGroup>();
            ProSubGroup.Add(new tbl_ProductSubGroup { ProductSubGroupID = -1, ProductSubGroupName = "==เลือก==" });
            ddl.BindDropdownList(ProSubGroup, "ProductSubGroupName", "ProductSubGroupID");
        }

        private void SetDefaultDropDownEdit()
        {
            //ประเภทสินค้า -edit
            var PrdType = bu.GetProductType(x => x.FlagDel == false); //สร้างใหม่
            ddlProType.BindDropdownList(PrdType, "ProductTypeName", "ProductTypeID");

            //กลุ่มสินค้า -edit
            int protypeID = Convert.ToInt32(ddlProType.SelectedValue);
            var progroup = bu.GetProductGroup().Where(x => x.FlagDel == false && x.ProductTypeID == protypeID).ToList();
            ddlProGroup.BindDropdownList(progroup, "ProductGroupName", "ProductGroupID");

            //กลุ่มย่อยสินค้า -edit
            int progroupID = Convert.ToInt32(ddlProGroup.SelectedValue);
            var prosubgroup = bu.GetProductSubGroup(x => x.FlagDel == false && x.ProductGroupID == progroupID);
            ddlProSubGroup.BindDropdownList(prosubgroup, "ProductSubGroupName", "ProductSubGroupID");

            //หน่วยเล็กที่สุด
            var prouom = bu.GetProductUOM();//สร้างใหม่
            ddlProUom.BindDropdownList(prouom, "ProductUomName", "ProductUomID");

            //รสชาติ
            //var flavor = bu.GetProductFlavour();
            //ddlProFlavour.BindDropdownList(flavor, "ProductFlavourName", "ProductFlavourID");

            //รูปแบบการขาย
            var _saleType = bu.GetSaleType();
            ddlSaleType.BindDropdownList(_saleType, "SaleTypeName", "SaleTypeID", 0);
        }

        private void InitialData()
        {
            //ประเภทสินค้า -grd
            var PrdType = new List<tbl_ProductType>();
            PrdType.Add(new tbl_ProductType { ProductTypeID = -1, ProductTypeName = "==เลือก==" });
            PrdType.AddRange(bu.GetProductType(x => x.FlagDel == false));
            ddlPrdType.BindDropdownList(PrdType, "ProductTypeName", "ProductTypeID");

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("ProductID", "");
            tbl_ProductPriceGroupList = bu.GetProductGroupPriceData(_params);

            //กลุ่มสินค้า -grd
            ddlProGroupNoData(ddlPrdGroup);

            //กลุ่มย่อยสินค้า -grd
            ddlProSubGroupNoData(ddlPrdSubGroup);

            SetDefaultDropDownEdit();

            OpenPanelEdit(false);

            SetPrdUomGridView(true);

            grdPro.AutoGenerateColumns = false;
            grdPrdUom.AutoGenerateColumns = false;
            grdPro.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            chkTablet.Enabled = false;

            btnBrowse.Enabled = false; // รูป
            btnSaveSeq.Enabled = false; //ลำดับ
            btnPrdCheck.Enabled = false;
            button1.Enabled = false; // ดึงข้อมูลจาก Center
        }

        private void OpenPanelEdit(bool flag)
        {
            pnlEdit.OpenControl(flag, readOnlyPnlEditControls.ToArray());

            foreach (Control item in pnlEdit.Controls)
            {
                if (flag == false) // สั่งปิด   //ใส่สีเทาใน Control Panel
                {
                    if (item is Label || item is Panel || item is PictureBox || item is CheckBox || item is Button
                        || item is GroupBox || item is ComboBox || item is ListBox || item is RadioButton)
                    { }    
                    else
                        item.BackColor = ColorTranslator.FromHtml("#DCDCDC");
                }
            }

            ControlNotUse();
        }

        private void ControlNotUse()
        {
            btnBrowse.Enabled = false; //รูปปิด
            btnProGroup.Enabled = false; //กลุ่มสินค้า
        }

        private void SetProductGroup(int prdtypeID, ComboBox ddl)
        {
            var prdgroupID = bu.GetProductGroup().Where(x => x.FlagDel == false && x.ProductTypeID == prdtypeID).ToList();
            var prdgroup = new List<tbl_ProductGroup>();
            prdgroup.Add(new tbl_ProductGroup { ProductGroupID = -1, ProductGroupName = "==เลือก==" });
            prdgroup.AddRange(prdgroupID);
            ddl.BindDropdownList(prdgroup, "ProductGroupName", "ProductGroupID");
        }

        private void SetPrdUomGridView(bool flagReadOnly)
        {
            grdPrdUom.Columns["colUnit"].ReadOnly = flagReadOnly;
            grdPrdUom.Columns["colProductID2"].ReadOnly = flagReadOnly;
            grdPrdUom.Columns["colBaseQty"].ReadOnly = flagReadOnly;
            grdPrdUom.Columns["colBuyPrice"].ReadOnly = flagReadOnly;
            grdPrdUom.Columns["colSellPrice"].ReadOnly = flagReadOnly;
            grdPrdUom.Columns["colSellPriceVat"].ReadOnly = flagReadOnly;
            grdPrdUom.Columns["colComPrice"].ReadOnly = flagReadOnly;
            grdPrdUom.Columns["colWeight"].ReadOnly = flagReadOnly;
        }

        private void SetControl_PanelSearch()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            btnPrint.Enabled = true;
            btnSaveSeq.Enabled = false;
            btnPrdCheck.Enabled = false;

            if (grdPro.Rows.Count > 0)
            {
                btnPrdCheck.Enabled = true;
                btnEdit.Enabled = true;
                btnCopy.Enabled = true;
                btnSaveSeq.Enabled = true;
                grdPro.Columns["colSeq"].ReadOnly = false;
                SetPrdUomGridView(true);
            }

            if (grdPro.Rows.Count > 0 && rdoN.Checked == true)
            {
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
            }

            if (rdoC.Checked == true)
                btnAdd.Enabled = false;

            //09/09/2022 adisorn -> admin and superadmin will delete customer data only 
            if (Helper.tbl_Users.RoleID == 5 || Helper.tbl_Users.RoleID == 5 || Helper.tbl_Users.RoleID == 10)
                btnRemove.Enabled = true;
            else
                btnRemove.Enabled = false;
        }

        private void SelectProductDetails(DataGridViewCellEventArgs e)
        {
            try
            {
                //09/09/2022 adisorn -> admin and superadmin will delete customer data only 
                if (Helper.tbl_Users.RoleID == 5 || Helper.tbl_Users.RoleID == 5 || Helper.tbl_Users.RoleID == 10)
                    btnRemove.Enabled = true;
                else
                    btnRemove.Enabled = false;

                DataGridViewRow gridViewRow = null;

                if (e != null)
                {
                    if (e.RowIndex == -1)
                        return;
                    else
                        gridViewRow = grdPro.Rows[e.RowIndex];
                }
                else
                    gridViewRow = grdPro.CurrentRow;

                if (gridViewRow != null)
                {
                    string _ProductID = gridViewRow.Cells["colProductID"].Value.ToString();

                    DataRow r = dtProduct.AsEnumerable().FirstOrDefault(x => x.Field<string>("ProductID") == _ProductID);

                    if (r != null)
                    {
                        txtProductID.Text = r["ProductID"].ToString();
                        txtProductRefCode.Text = r["ProductRefCode"].ToString();

                        ddlProType.SelectedValue = Convert.ToInt32(r["ProductTypeID"]);
                        ddlProGroup.SelectedValue = Convert.ToInt32(r["ProductGroupID"]);
                        ddlProSubGroup.SelectedValue = Convert.ToInt32(r["ProductSubGroupID"]);

                        string _SaleTypeID = r["SaleTypeID"].ToString();
                        if (!string.IsNullOrEmpty(_SaleTypeID))
                        {
                            ddlSaleType.SelectedValue = Convert.ToInt32(_SaleTypeID);
                        }

                        txtBarCode.Text = r["BarCode"].ToString();
                        txtProductName.Text = r["ProductName"].ToString();
                        txtProductShortName.Text = r["ProductShortName"].ToString();

                        ddlProUom.SelectedValue = Convert.ToInt32(r["SaleUomID"]); //หน่วยเล็กที่สุด
                        txtProFlavour.Text = r["Flavour"].ToString(); // รสชาติ

                        //chkBoxFlagDel.Checked = false; //ยกเลิก
                        //if (Convert.ToBoolean(r["FlagDel"]) == false) // ปกติ
                        //    chkBoxFlagDel.Checked = true;

                        rdoNormal.Checked = true;
                        if (Convert.ToBoolean(r["FlagDel"]) == true) //ยกเลิก //ADISORN 29/12/2564
                            rdoCancel.Checked = true;

                        SetProductUomTable(_ProductID); //Set Data To grdPrdUom  //ADISORN 29/12/2564

                        rdoVatF.Checked = true; //ไม่มีภาษี

                        if (!string.IsNullOrEmpty(r["VatType"].ToString()))
                        {
                            bool GetVat = Convert.ToBoolean(r["VatType"]);
                            if (GetVat == true)
                                rdoVatN.Checked = true; //มีภาษี
                        }

                        txtReorderPoint.Text = r["ReorderPoint"].ToString(); //จุุดสั่งซื้อ
                        txtMinPoint.Text = r["MinPoint"].ToString(); //จุดต่ำสุด

                        decimal weight = Convert.ToDecimal(r["Weight"]);
                        txtWeight.Text = weight.ToStringN0(); //น้ำหนักบรรจุ

                        decimal size = Convert.ToDecimal(r["Size"]);
                        txtSize.Text = size.ToStringN0(); //ปริมาณบรรจุหน้าซอง

                        chkTablet.Checked = false; //ไม่เช็ค
                        if (!string.IsNullOrEmpty(r["IsFulfill"].ToString()))
                        {
                            bool isFulfill = Convert.ToBoolean(r["IsFulfill"]);
                            if (isFulfill == true)
                                chkTablet.Checked = true; //เช็ค
                        }

                        if (Convert.ToBoolean(r["FlagDel"]) == true) // ปกติ
                            chkTablet.Checked = true;

                        picProductImg.Image = null;
                        if (!string.IsNullOrEmpty(r["ProductImg"].ToString()))
                        {
                            Byte[] data = new Byte[0];
                            data = (Byte[])r["ProductImg"];
                            MemoryStream mem = new MemoryStream(data);
                            picProductImg.Image = Image.FromStream(mem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void SetProductUomTable(string _ProductID)
        {
            grdPrdUom.Rows.Clear();

            //Dictionary<string, object> _params = new Dictionary<string, object>();
            //_params.Add("ProductID", _ProductID);
            //var dt = bu.GetProductGroupPriceData(_params);
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        grdPrdUom.Rows.Add(1);
            //        grdPrdUom.Rows[i].Cells["colProductID2"].Value = dt.Rows[i].Field<string>("ProductID");
            //        grdPrdUom.BindComboBoxCell(grdPrdUom.Rows[i], i, true, 0, uoms, this, bu, 1);
            //        grdPrdUom.Rows[i].Cells["colUnit"].Value = dt.Rows[i].Field<int>("UomSetID");
            //        grdPrdUom.Rows[i].Cells["colBaseQty"].Value = dt.Rows[i].Field<int>("BaseQty").ToString();
            //        grdPrdUom.Rows[i].Cells["colBuyPrice"].Value = dt.Rows[i].Field<decimal>("BuyPrice");
            //        grdPrdUom.Rows[i].Cells["colSellPrice"].Value = dt.Rows[i].Field<decimal>("SellPrice");
            //        grdPrdUom.Rows[i].Cells["colSellPriceVat"].Value = dt.Rows[i].Field<decimal>("SellPriceVat");
            //        grdPrdUom.Rows[i].Cells["colComPrice"].Value = dt.Rows[i].Field<decimal>("ComPrice");
            //        grdPrdUom.Rows[i].Cells["colWeight"].Value = dt.Rows[i].Field<decimal>("Weight").ToDecimalN2();
            //    }
            //}
            //else
            //{
            //    SetProductUomTable();
            //}

            //last edit by sialom .k 14/10/2022---------------------
            var dt = tbl_ProductPriceGroupList.Select("ProductID = '"+_ProductID + "'");
            if (dt != null && dt.Count() > 0)
            {
                for (int i = 0; i < dt.Count(); i++)
                {
                    grdPrdUom.Rows.Add(1);
                    grdPrdUom.Rows[i].Cells["colProductID2"].Value = dt[i].Field<string>("ProductID");
                    grdPrdUom.BindComboBoxCell(grdPrdUom.Rows[i], i, true, 0, uoms, this, bu, 1);
                    grdPrdUom.Rows[i].Cells["colUnit"].Value = dt[i].Field<int>("UomSetID");
                    grdPrdUom.Rows[i].Cells["colBaseQty"].Value = dt[i].Field<int>("BaseQty").ToString();
                    grdPrdUom.Rows[i].Cells["colBuyPrice"].Value = dt[i].Field<decimal>("BuyPrice");
                    grdPrdUom.Rows[i].Cells["colSellPrice"].Value = dt[i].Field<decimal>("SellPrice");
                    grdPrdUom.Rows[i].Cells["colSellPriceVat"].Value = dt[i].Field<decimal>("SellPriceVat");
                    grdPrdUom.Rows[i].Cells["colComPrice"].Value = dt[i].Field<decimal>("ComPrice");
                    grdPrdUom.Rows[i].Cells["colWeight"].Value = dt[i].Field<decimal>("Weight").ToDecimalN2();
                }
            }
            else
            {
                SetProductUomTable();
            }
            //last edit by sialom .k 14/10/2022---------------------
        }

        private void SetProductUomTable()
        {
            grdPrdUom.Rows.Clear();

            var prdUoms = bu.GetProductUOM();

            var products = bu.SelectSingleProduct();
            //var allpro = bu.GetProduct(x=>x.ProductGroupID == 1);

            if (products.Count > 0)
            {
                grdPrdUom.Rows.Add(1);
                grdPrdUom.Rows[0].Cells["colProductID2"].Value = products[0].ProductID;

                grdPrdUom.BindComboBoxCell(grdPrdUom.Rows[0], 0, true, 0, uoms, this, bu, 1);
                grdPrdUom.Rows[0].Cells["colUnit"].Value = prdUoms[0].ProductUomID;

                grdPrdUom.Rows[0].Cells["colBaseQty"].Value = 0.00;
                grdPrdUom.Rows[0].Cells["colBuyPrice"].Value = 0.00;
                grdPrdUom.Rows[0].Cells["colSellPrice"].Value = 0.00;
                grdPrdUom.Rows[0].Cells["colSellPriceVat"].Value = 0.00;
                grdPrdUom.Rows[0].Cells["colComPrice"].Value = 0.00;
                grdPrdUom.Rows[0].Cells["colWeight"].Value = 0.00;
            }
        } //Table โครงสร้างหน่วยสินค้า

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //true
            {
                errList.SetErrMessage(validateCtrls);
            }
            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }
            return ret;
        }

        private bool ValidateData(int PrdRows)
        {
            bool ret = true;// ถ้าเป็นการ new Insert จะนำรหัสไปค้นหาใน DB ว่ามีหรือไม่ 

            string msg = "";

            if (PrdRows > 0)
            {
                msg += "รหัสสินค้าไม่ถูกต้อง รหัสสินค้าซ้ำในระบบ !!\n";
                ret = false;
            }
            if (txtProductID.TextLength != 8)
            {
                msg += "รหัสสินค้าไม่ควรน้อยหรือมากกว่า 8 หลัก \n";
                ret = false;
            }

            if (!string.IsNullOrEmpty(msg))
            {
                msg.ShowErrorMessage();
            }

            return ret;
        }

        private bool ValidatePrdUom()
        {
            bool ret = false;

            List<int> unitList = new List<int>();
            for (int i = 0; i < grdPrdUom.RowCount; i++)
            {
                int _unit = Convert.ToInt32(grdPrdUom.Rows[i].Cells["colUnit"].Value);
                if (unitList.Count == 0)
                {
                    unitList.Add(_unit);
                }
                else
                {
                    bool checkPrdUom = unitList.Contains(_unit);
                    if (checkPrdUom)
                    {
                        ret = true;
                        break;
                    }
                }
            }

            return ret;
        }

        private void Remove_ProductUomSet(List<tbl_ProductUomSet> ProductUomSets)
        {
            int ret = 0;
            if (ProductUomSets.Count > 0)
            {
                foreach (var data in ProductUomSets)
                {
                    ret = bu.RemoveData(data);
                }
            }
        }

        private void Remove_ProductPriceGroup()
        {
            int ret = 0;
            var PrdPriceGroup = bu.GetProductPriceGroup(x => x.ProductID == txtProductID.Text);
            foreach (var data in PrdPriceGroup)
            {
                ret = bu.RemoveData(data);
            }
        }

        private void PrePareSave_ProductPriceGroup(tbl_ProductPriceGroup PrdPriceGroup)
        {
            PrdPriceGroup.PriceGroupID = 1;//fix
            PrdPriceGroup.ProductID = txtProductID.Text;

            PrdPriceGroup.CrDate = DateTime.Now;
            PrdPriceGroup.CrUser = Helper.tbl_Users.Username;

            PrdPriceGroup.EdDate = null;
            PrdPriceGroup.EdUser = null;

            PrdPriceGroup.FlagDel = false;
            PrdPriceGroup.FlagSend = false;
            PrdPriceGroup.FlagNew = false;
            PrdPriceGroup.FlagEdit = false;
        }

        private bool ValidateBaseQty()
        {
            bool ret = true;
            string msgErr = "";
            int unit = 0;//เลือกได้ 2 หน่วยเท่านั้น 
            int _BaseQty2 = 0;

            for (int i = 0; i < grdPrdUom.Rows.Count; i++)
            {
                int _BaseQty = Convert.ToInt32(grdPrdUom.Rows[i].Cells["colBaseQty"].Value);
                if (_BaseQty > 0)
                {
                    _BaseQty2++;
                    unit++;
                }
            }

            if (_BaseQty2 == 0)
                msgErr += "จำนวน ในโครงสร้างหน่วยสินค้า ต้องไม่เป็นศูนย์ !!\n";
            if (unit > 2)
                msgErr += "เลือกหน่วยสินค้าได้สูงสุดแค่ 2 หน่วย !!";

            if (!string.IsNullOrEmpty(msgErr))
            {
                msgErr.ShowWarningMessage();
                ret = false;
            }

            return ret;
        }

        private void PrePareSave_ProductUomSet(tbl_ProductUomSet PrdUomSet)
        {
            PrdUomSet.ProductID = txtProductID.Text;

            PrdUomSet.StandardCost = null;

            //PrdUomSet.UomCode = "";//Fix

            PrdUomSet.BaseUomID = Convert.ToInt32(ddlProUom.SelectedValue);
            PrdUomSet.BaseUomName = ddlProUom.Text;

            PrdUomSet.CrDate = DateTime.Now;
            PrdUomSet.CrUser = Helper.tbl_Users.Username;

            PrdUomSet.EdDate = null;
            PrdUomSet.EdUser = null;

            PrdUomSet.FlagDel = false;
            PrdUomSet.FlagSend = false;
            PrdUomSet.FlagNew = false;
            PrdUomSet.FlagEdit = false;
        }

        private tbl_Product PrePareSave_Product(List<tbl_Product> PrdList, List<tbl_ProductUomSet> PrdUomSetList)
        {
            var prdData = new tbl_Product();

            if (PrdList.Count > 0)
            {
                prdData = PrdList[0];
                prdData.EdDate = DateTime.Now;
                prdData.EdUser = Helper.tbl_Users.Username;
            }
            else
            {
                prdData.ProductCode = txtProductID.Text;
                prdData.Seq = 0;

                prdData.CrDate = DateTime.Now;
                prdData.CrUser = Helper.tbl_Users.Username;

                prdData.EdDate = null;
                prdData.EdUser = null;

                prdData.ProductLine = 0;
                prdData.ProductBrandID = 0;

                prdData.ProductImg = null; //ไม่Upload Image
            }

            if (PrdUomSetList.Count > 0)
            {
                int maxBaseQty = PrdUomSetList.Max(x => x.BaseQty);
                prdData.PurchaseUomID = PrdUomSetList.FirstOrDefault(x => x.BaseQty == maxBaseQty).UomSetID;//ซื้อ //last edit by A 02/12/2021
            }
            else
            {
                prdData.PurchaseUomID = Convert.ToInt32(ddlProUom.SelectedValue);
            }

            pnlEdit.Controls.SetObjectFromControl(prdData);

            prdData.SaleUomID = Convert.ToInt32(ddlProUom.SelectedValue); //หน่วยขาย เป็นหน่วยที่เล็กที่สุด

            prdData.WeightUOM = ddlProUom.Text;
            prdData.SizeUOM = ddlProUom.Text;

            prdData.Flavour = txtProFlavour.Text;

            if (string.IsNullOrEmpty(prdData.Flavour))
                prdData.Flavour = "ไม่ระบุ";

            prdData.ProductGroupID = Convert.ToInt32(ddlProGroup.SelectedValue);

            prdData.ProductSubGroupID = Convert.ToInt32(ddlProSubGroup.SelectedValue);

            prdData.Barcode = txtBarCode.Text;

            prdData.VatType = rdoVatN.Checked ? true : false;  //1 มีภาษี 0 ไม่มีภาษี

            prdData.Remark = "";

            prdData.StandardCost = 0;

            prdData.IsFulfill = chkTablet.Checked ? true : false;

            prdData.FlagDel = rdoNormal.Checked ? false : true;

            prdData.FlagSend = false;
            prdData.FlagNew = false;
            prdData.FlagEdit = false;

            decimal _sellPrice = 0;
            prdData.SellPrice = _sellPrice.ToDecimalN2();

            if (ddlSaleType.SelectedValue != null)
                prdData.SaleTypeID = Convert.ToInt32(ddlSaleType.SelectedValue);

            return prdData;
        }

        #endregion

        private void UpdateMasterData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (Connection.ConnectionString.Contains("DB_SDSS_UNI_CENTER"))
                {
                    //print no load customer data
                }
                else
                    bu.GetAllData();

                bu.tbl_AdmFormList = bu.GetAllFromMenu();
                bu.tbl_DocumentType = bu.GetDocumentType();
                bu.tbl_MstMenu = bu.GetAllMenuData();

                //prod.GetAllData();
                bu.tbl_ProductUom = bu.GetUOM();
                bu.tbl_ProductUomSet = bu.GetUOMSet();
                bu.tbl_DiscountType = bu.GetDiscountType();

                //edit by sailom .k 14/12/2021------------------------------------------
                bu.tbl_Branchs = bu.GetBranch();
                bu.tbl_Companies = bu.GetAllCompany();
                bu.tbl_ProductPriceGroup = bu.GetProductPriceGroup();
                //edit by sailom .k 14/12/2021------------------------------------------

                bu.tbl_SalArea = bu.GetAllSaleArea();
                bu.tbl_SalAreaDistrict = bu.GetAllSaleAreaDistrict();
                bu.tbl_Product = bu.GetProductNonFlag(); //for support when user open old document have a cancel product!! last edit by sailom.k 05/05/2022
                bu.tbl_ProductGroup = bu.GetProductGroup();
                bu.tbl_ProductSubGroup = bu.GetProductSubGroup();


                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("ProductID", "");
                tbl_ProductPriceGroupList = bu.GetProductGroupPriceData(_params);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            UpdateMasterData();
        }

        private void ValidateCloseProduct(Control ctrl)
        {
            //edit by sailom .k 17/02/2023----------------------------------------------
            if (Helper.BranchName != "CENTER")
            {
                bool isChecked = false;
                if (ctrl is CheckBox)
                {
                    isChecked = ((CheckBox)ctrl).Checked;
                }
                else if (ctrl is RadioButton)
                {
                    isChecked = ((RadioButton)ctrl).Checked;
                }

                if (isChecked)
                {
                    bool validateClose = false;
                    decimal qty = 0;
                    string validateMsg = "";
                    var stockVan = bu.GetInvWarehouse().Where(x => x.ProductID == txtProductID.Text && x.WHID.Contains("V")).ToList();
                    if (stockVan != null && stockVan.Count > 0)
                    {
                        foreach (tbl_InvWarehouse item in (List<tbl_InvWarehouse>)stockVan)
                        {
                            if (item.QtyOnHand != 0)
                                validateMsg += "\n แวน : " + item.WHID +  " => " + Convert.ToInt32(item.QtyOnHand).ToNumberFormat() + " (หน่วยเล็ก)";
                        }

                        qty = stockVan.Sum(x => x.QtyOnHand);
                        if (qty != 0)
                        {
                            validateClose = true;
                        }
                    }

                    if (validateClose)
                    {
                        if (ctrl is CheckBox)
                        {
                            ((CheckBox)ctrl).Checked = false;
                        }
                        else if (ctrl is RadioButton)
                        {
                            ((RadioButton)ctrl).Checked = false;
                            rdoNormal.Checked = true;
                        }

                        string msg = "ไม่สามารถปิดโค้ดสินค้า  '" + txtProductID.Text + " : " + txtProductShortName.Text + " ' !!! \n เนื่องจากยังมีสินค้าคงเหลือที่ \n " 
                            //+ Convert.ToInt32(qty).ToNumberFormat()
                            //+ " (หน่วยเล็ก) ที่คลังแวน " +
                            + validateMsg +
                            "\n\n กรุณาทำ RB ลงจากแวนให้เรียบร้อยก่อน!!!";
                        msg.ShowWarningMessage();
                        return;
                    }
                }
            }

            //edit by sailom .k 17/02/2023----------------------------------------------
        }

        private void chkTablet_MouseClick(object sender, MouseEventArgs e)
        {
            ValidateCloseProduct(chkTablet);
        }

        private void rdoCancel_MouseClick(object sender, MouseEventArgs e)
        {
            ValidateCloseProduct(rdoCancel);
        }
    }
}
