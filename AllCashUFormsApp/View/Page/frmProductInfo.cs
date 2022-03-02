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

        #region #Button_Event
        private void btnClose_Click(object sender, EventArgs e)
        {
            dtProduct = new DataTable();
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnSearch_Click";
            msg.WriteLog(this.GetType());

            BindProductData();

            msg = "end frmProductInfo=>btnSearch_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnAdd_Click";
            msg.WriteLog(this.GetType());

            flagNew = true;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            pnlGridView.Enabled = false;
            grdPro.Enabled = false;

            pnlEdit.ClearControl();

            OpenPanelEdit(true);

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
            //SaveSeq(); //old
            string msg = "start frmProductInfo=>btnSaveSeq_Click=>SaveSeq_New";
            msg.WriteLog(this.GetType());

            SaveSeq_New();

            msg = "end frmProductInfo=>btnSaveSeq_Click=>SaveSeq_New";
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

            msg = "end frmProductInfo=>btnEdit_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnCancel_Click";
            msg.WriteLog(this.GetType());

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

            msg = "end frmProductInfo=>btnCancel_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnSave_Click";
            msg.WriteLog(this.GetType());

            Save();

            msg = "end frmProductInfo=>btnSave_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnPrdCheck_Click(object sender, EventArgs e)
        {
            string _msg = "start frmProductInfo=>btnPrdCheck_Click";
            _msg.WriteLog(this.GetType());

            bool ProIsCorrect = false;
            string _ProductID = grdPro.CurrentRow.Cells["colProductID"].Value.ToString();

            var dt = bu.GetProductViewCheck(_ProductID);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.AsEnumerable().First();
                string ProductID = r["ProductID"].ToString();
                string ProductName = r["ProductName"].ToString();
                string ProductShortName = r["ProductShortName"].ToString();
                string Flavour = r["Flavour"].ToString();
                string VatType = r["VatType"].ToString();
                string ProductSubGroupID = r["ProductSubGroupID"].ToString();
                string ProductSubGroupName = r["ProductSubGroupName"].ToString();
                string ProductGroupID = r["ProductGroupID"].ToString();
                string ProductGroupName = r["ProductGroupName"].ToString();
                string UomSetID = r["UomSetID"].ToString();
                string UomSetName = r["UomSetName"].ToString();
                string BaseQty = r["BaseQty"].ToString();

                if (string.IsNullOrEmpty(ProductID) || string.IsNullOrEmpty(ProductName)
                    || string.IsNullOrEmpty(ProductShortName) || string.IsNullOrEmpty(Flavour)
                    || string.IsNullOrEmpty(VatType) || string.IsNullOrEmpty(ProductSubGroupID)
                    || string.IsNullOrEmpty(ProductSubGroupName) || string.IsNullOrEmpty(ProductGroupID)
                    || string.IsNullOrEmpty(ProductGroupName) || string.IsNullOrEmpty(UomSetID)
                    || string.IsNullOrEmpty(UomSetName) || string.IsNullOrEmpty(BaseQty))
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

            Remove();

            BindProductData();
            msg = "end frmProductInfo=>btnRemove_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductInfo=>btnPrint_Click";
            msg.WriteLog(this.GetType());

            FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

            ExportExcel();

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

        #region #Event_Page
        private void frmProductInfo_Load(object sender, EventArgs e)
        {
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
                BindProSubGroup(ddlPrdSubGroup);
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
                BindProductData();
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
                if (e.ColumnIndex == -1 || e.RowIndex == -1)
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
                        decimal colBaseQty = Convert.ToDecimal(grdPrdUom.CurrentRow.Cells["colBaseQty"].EditedFormattedValue).ToDecimalN2();
                        int _colUnit = Convert.ToInt32(grdPrdUom.CurrentRow.Cells["colUnit"].Value);
                        decimal _colBuyPrice = Convert.ToDecimal(grdPrdUom.CurrentRow.Cells["colBuyPrice"].EditedFormattedValue).ToDecimalN2();

                        string msg = "";

                        if (_colUnit == -1)
                            msg += "กรุณาเลือกหน่วย\n";

                        if (colBaseQty <= 0)
                            msg += "กรุณากรอกจำนวน\n";

                        if (_colBuyPrice <= 0)
                            msg += "กรุณากรอกราคาชื้อก่อนภาษี\n";

                        if (!string.IsNullOrEmpty(msg))
                        {
                            msg.ShowWarningMessage();
                            return;
                        }

                        if (colBaseQty > 0 && _colUnit != -1 && _colBuyPrice > 0)// && colSellPrice > 0 && colSellPriceVat > 0
                        {
                            int currentRowIndex = grd.CurrentCell.RowIndex;
                            int curentColIndex = grd.CurrentCell.ColumnIndex;

                            grd.ClearSelection();

                            int _newrowIndex = currentRowIndex + 1;

                            if (curentColIndex == 7)
                            {
                                grdPrdUom.AddNewRow(initDataGridList, 1, "", _newrowIndex, true, uoms, bu, this, 0); //
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

        #region #Private_Method
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
                    {

                    }
                    else
                    {
                        item.BackColor = ColorTranslator.FromHtml("#DCDCDC");
                    }
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

        private void BindProSubGroup(ComboBox ddl)
        {
            var prdSubGroup = bu.GetProductSubGroup(x => x.FlagDel == false);
            var _prdSubGroup = new List<tbl_ProductSubGroup>();
            _prdSubGroup.Add(new tbl_ProductSubGroup { ProductSubGroupID = -1, ProductSubGroupName = "==เลือก==" });
            _prdSubGroup.AddRange(prdSubGroup);
            ddl.BindDropdownList(_prdSubGroup, "ProductSubGroupName", "ProductSubGroupID");
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
        }

        private void BindProductData()
        {
            pnlEdit.ClearControl();

            OpenPanelEdit(false);

            SetDefaultNumber();

            dtProduct = new DataTable();

            int flagDel = rdoN.Checked ? 0 : 1;
            int ProductGroupID = ddlPrdGroup.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlPrdGroup.SelectedValue);
            int ProductSubGroupID = ddlPrdSubGroup.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlPrdSubGroup.SelectedValue);

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@flagDel", flagDel);
            _params.Add("@ProductGroupID", ProductGroupID);
            _params.Add("@ProductSubGroupID", ProductSubGroupID);
            _params.Add("@Search", txtSearch.Text);
            dtProduct = bu.proc_tbl_Product_Data(_params);
            grdPro.DataSource = dtProduct;
            lblgrdQty.Text = dtProduct.Rows.Count.ToNumberFormat();

            SetControl_PanelSearch();
            SelectProductDetails(null);
        }

        private void SelectProductDetails(DataGridViewCellEventArgs e)
        {
            try
            {
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

            Dictionary<string, object> _params = new Dictionary<string, object>();

            _params.Add("ProductID", _ProductID);

            var dtPrdGrp = bu.GetProductGroupPriceData(_params);

            if (dtPrdGrp.Rows.Count > 0)
            {
                for (int i = 0; i < dtPrdGrp.Rows.Count; i++)
                {
                    grdPrdUom.Rows.Add(1);

                    grdPrdUom.Rows[i].Cells["colProductID2"].Value = dtPrdGrp.Rows[i].Field<string>("ProductID");

                    grdPrdUom.BindComboBoxCell(grdPrdUom.Rows[i], i, true, 0, uoms, this, bu, 1);

                    grdPrdUom.Rows[i].Cells["colUnit"].Value = dtPrdGrp.Rows[i].Field<int>("UomSetID");

                    grdPrdUom.Rows[i].Cells["colBaseQty"].Value = dtPrdGrp.Rows[i].Field<int>("BaseQty").ToString();

                    grdPrdUom.Rows[i].Cells["colBuyPrice"].Value = dtPrdGrp.Rows[i].Field<decimal>("BuyPrice");

                    grdPrdUom.Rows[i].Cells["colSellPrice"].Value = dtPrdGrp.Rows[i].Field<decimal>("SellPrice");

                    grdPrdUom.Rows[i].Cells["colSellPriceVat"].Value = dtPrdGrp.Rows[i].Field<decimal>("SellPriceVat");

                    grdPrdUom.Rows[i].Cells["colComPrice"].Value = dtPrdGrp.Rows[i].Field<decimal>("ComPrice");

                    grdPrdUom.Rows[i].Cells["colWeight"].Value = dtPrdGrp.Rows[i].Field<decimal>("Weight");

                }
            }
            else
            {
                SetProductUomTable();
            }
        }

        private void SetProductUomTable()
        {
            grdPrdUom.Rows.Clear();

            var prdUoms = bu.GetProductUOM();

            var products = bu.SelectSingleProduct();
            //var allpro = bu.GetProduct(x=>x.ProductGroupID == 1);

            if (products.Count > 0)
            {
                //for (int i = 0; i < prdUoms.Count; i++)
                //{
                //    break;
                //}

                grdPrdUom.Rows.Add(1);

                grdPrdUom.Rows[0].Cells["colProductID2"].Value = products[0].ProductID;
                grdPrdUom.BindComboBoxCell(grdPrdUom.Rows[0], 0, true, 0, uoms, this, bu, 1);

                grdPrdUom.Rows[0].Cells["colUnit"].Value = prdUoms[0].ProductUomID;

                decimal prdUom = 0;

                grdPrdUom.Rows[0].Cells["colBaseQty"].Value = prdUom;
                grdPrdUom.Rows[0].Cells["colBuyPrice"].Value = prdUom.ToDecimalN2();
                grdPrdUom.Rows[0].Cells["colSellPrice"].Value = prdUom.ToDecimalN2();
                grdPrdUom.Rows[0].Cells["colSellPriceVat"].Value = prdUom.ToDecimalN2();
                grdPrdUom.Rows[0].Cells["colComPrice"].Value = prdUom.ToDecimalN2();
                grdPrdUom.Rows[0].Cells["colWeight"].Value = prdUom.ToDecimalN2();
            }
        } //Set Table โครงสร้างหน่วยสินค้า

        private void Remove()
        {
            try
            {
                string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
                string title = "ทำการยืนยัน!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;

                var Products = bu.SelectProductList(txtProductID.Text); //New

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
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

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

        private void Remove_ProductUomSet()
        {
            int ret = 0;

            var PrdUomSetList = bu.GetProductUOMSet(x => x.ProductID == txtProductID.Text);

            if (PrdUomSetList.Count > 0)
            {
                foreach (var data in PrdUomSetList)
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
            {
                msgErr += "จำนวน ในโครงสร้างหน่วยสินค้า ต้องไม่เป็นศูนย์ !!\n";
            }

            if (unit > 2)
            {
                msgErr += "เลือกหน่วยสินค้าได้สูงสุดแค่ 2 หน่วย !!";
            }

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
            PrdUomSet.UomCode = "";

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

        private void Save()
        {
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

                var PrdList = bu.SelectProductList(txtProductID.Text);

                if (flagNew == true)
                {
                    if (!ValidateData(PrdList.Count))
                        return;
                }

                if (PrdList != null && PrdList.Count > 0) //Remove OldData tbl_ProductUomSet , tbl_ProductPriceGroup
                {
                    Remove_ProductUomSet();
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

                        // ---ProductUomSet--- //
                        var PrdUomSet = new tbl_ProductUomSet();
                        PrdUomSet.UomSetID = _Unit;
                        PrdUomSet.UomSetName = PrdUom.FirstOrDefault(x => x.ProductUomID == _Unit).ProductUomName;
                        PrdUomSet.BaseQty = Convert.ToInt32(BaseQty);
                        PrdUomSet.Weight = Convert.ToDecimal(grdPrdUom.Rows[i].Cells["colWeight"].Value);
                        PrePareSave_ProductUomSet(PrdUomSet);
                        PrdUomSetList.Add(PrdUomSet);

                        // ---ProductPriceGroup--- //
                        var PrdPriceGroup = new tbl_ProductPriceGroup();
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
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    msg.ShowInfoMessage();

                    pnlGridView.Enabled = true;
                    OpenPanelEdit(false);
                    grdPro.Enabled = true;

                    BindProductData();
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

            prdData.ProductImg = null; //ไม่Upload Image

            prdData.ProductBrandID = 0;
            prdData.ProductLine = 0;

            decimal _sellPrice = 0;
            prdData.SellPrice = _sellPrice.ToDecimalN2();

            if (ddlSaleType.SelectedValue != null)
                prdData.SaleTypeID = Convert.ToInt32(ddlSaleType.SelectedValue);

            return prdData;
        }

        private void ExportExcel()
        {
            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@FlagDel", 0);
            this.OpenExcelReportsPopup("รายงานสินค้าทั้งหมด", "proc_GetProductDataToExcel.xslt", "proc_GetProductDataToExcel", _params, true);
        }

        private void SaveSeq_New()
        {
            int ret = 0;

            try
            {
                var ListProductID = new List<string>();

                for (int i = 0; i < grdPro.RowCount; i++)
                {
                    string PrdID = grdPro.Rows[i].Cells["colProductID"].Value.ToString();
                    ListProductID.Add(PrdID);
                }

                var _ProductID = string.Join(",", ListProductID);

                var ProductList = bu.SelectProductList(_ProductID); //

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
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        //private void SaveSeq()
        //{
        //    string msg = "start frmProductInfo=>btnSaveSeq_Click";
        //    msg.WriteLog(this.GetType());

        //    DataTable dt = new DataTable();
        //    dt = (DataTable)grdPro.DataSource;

        //    var PrdList = new List<tbl_Product>();

        //    int ret = 0;

        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            var Products = new tbl_Product();
        //            Products.ProductID = r["ProductID"].ToString();
        //            Products.Seq = Convert.ToInt16(r["Seq"]);
        //            Products.ProductCode = r["ProductID"].ToString();
        //            Products.ProductRefCode = r["ProductRefCode"].ToString();
        //            Products.Barcode = r["Barcode"].ToString();
        //            Products.ProductName = r["ProductName"].ToString();
        //            Products.ProductShortName = r["ProductShortName"].ToString();
        //            Products.ProductGroupID = Convert.ToInt32(r["ProductGroupID"]);
        //            Products.ProductSubGroupID = Convert.ToInt32(r["ProductSubGroupID"]);
        //            Products.Flavour = r["Flavour"].ToString();
        //            Products.Size = Convert.ToDecimal(r["Size"]);
        //            Products.SizeUOM = r["SizeUOM"].ToString();
        //            Products.Weight = Convert.ToDecimal(r["Weight"]);
        //            Products.WeightUOM = r["WeightUOM"].ToString();
        //            Products.ReorderPoint = Convert.ToInt16(r["ReorderPoint"]);
        //            Products.MinPoint = Convert.ToInt16(r["MinPoint"]);
        //            Products.PurchaseUomID = Convert.ToInt32(r["PurchaseUomID"]);
        //            Products.SaleUomID = Convert.ToInt32(r["SaleUomID"]);
        //            Products.VatType = Convert.ToBoolean(r["VatType"]);

        //            Products.StandardCost = Convert.ToDecimal(r["StandardCost"]);
        //            if (r["SellPrice"].ToString() != "")
        //            {
        //                Products.SellPrice = Convert.ToDecimal(r["SellPrice"]);
        //            }
        //            else
        //            {
        //                Products.SellPrice = null;
        //            }

        //            Products.IsFulfill = Convert.ToBoolean(r["IsFulfill"]);

        //            Products.Remark = r["Remark"].ToString();

        //            Products.CrDate = Convert.ToDateTime(r["CrDate"]);
        //            Products.CrUser = r["CrUser"].ToString();
        //            Products.EdDate = DateTime.Now;
        //            Products.EdUser = Helper.tbl_Users.Username;

        //            Products.FlagDel = Convert.ToBoolean(r["FlagDel"]);

        //            if (!string.IsNullOrEmpty(r["ProductImg"].ToString()))
        //            {
        //                Byte[] data = new Byte[0];
        //                data = (Byte[])r["ProductImg"];
        //                Products.ProductImg = data;
        //            }

        //            Products.FlagSend = Convert.ToBoolean(r["FlagSend"]);
        //            Products.FlagNew = Convert.ToBoolean(r["FlagNew"]);
        //            Products.FlagEdit = Convert.ToBoolean(r["FlagEdit"]);

        //            Products.ProductBrandID = Convert.ToInt32(r["ProductBrandID"]);
        //            Products.ProductLine = Convert.ToInt32(r["ProductLine"]);

        //            Products.SaleTypeID = Convert.ToInt32(r["SaleTypeID"]);

        //            PrdList.Add(Products);
        //        }
        //        foreach (var item in PrdList)
        //        {
        //            ret = bu.UpdateData(item);
        //        }
        //        if (ret == 1)
        //        {
        //            string message = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
        //            message.ShowInfoMessage();
        //            btnSearch.PerformClick();
        //        }
        //        else
        //        {
        //            this.ShowProcessErr();
        //            return;
        //        }
        //    }

        //    msg = "end frmProductInfo=>btnSaveSeq_Click";
        //    msg.WriteLog(this.GetType());
        //}

        #endregion
    }
}
