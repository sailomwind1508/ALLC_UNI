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
        static DataTable dtTempPrd = new DataTable();
        List<tbl_ProductUom> uoms = new List<tbl_ProductUom>();
        public static string prdsubgroupID = "";
        Dictionary<Control, Label> validateCtrls = new Dictionary<Control, Label>(); // Validate Save
        Dictionary<int, string> initDataGridList = new Dictionary<int, string>();
        bool flagEdit = false;
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

        #region EventButtonClick
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            flagEdit = false;

            pnlGridView.Enabled = false;
            grdPro.Enabled = false;

            pnlEdit.ClearControl();

            OpenPanelEdit(true);

            GrdList2ReadOnly(false);

            txtProductID.DisableTextBox(false);
            txtProductID.Focus();

            SetDefaultNumber();

            SetDefaultGrdList2();

            chkTablet.Enabled = true;

            grdPro.Enabled = false;

            chkBoxFlagDel.Checked = true;
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
            DataTable dt = new DataTable();
            dt = (DataTable)grdPro.DataSource;

            List<tbl_Product> proList = new List<tbl_Product>();

            int ret = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    tbl_Product tbl_Products = new tbl_Product();
                    tbl_Products.ProductID = r["ProductID"].ToString();
                    tbl_Products.Seq = Convert.ToInt16(r["Seq"]);
                    tbl_Products.ProductCode = r["ProductID"].ToString();
                    tbl_Products.ProductRefCode = r["ProductRefCode"].ToString();
                    tbl_Products.Barcode = r["Barcode"].ToString();
                    tbl_Products.ProductName = r["ProductName"].ToString();
                    tbl_Products.ProductShortName = r["ProductShortName"].ToString();
                    tbl_Products.ProductGroupID = Convert.ToInt32(r["ProductGroupID"]);
                    tbl_Products.ProductSubGroupID = Convert.ToInt32(r["ProductSubGroupID"]);
                    tbl_Products.Flavour = r["Flavour"].ToString();
                    tbl_Products.Size = Convert.ToDecimal(r["Size"]);
                    tbl_Products.SizeUOM = r["SizeUOM"].ToString();
                    tbl_Products.Weight = Convert.ToDecimal(r["Weight"]);
                    tbl_Products.WeightUOM = r["WeightUOM"].ToString();
                    tbl_Products.ReorderPoint = Convert.ToInt16(r["ReorderPoint"]);
                    tbl_Products.MinPoint = Convert.ToInt16(r["MinPoint"]);
                    tbl_Products.PurchaseUomID = Convert.ToInt32(r["PurchaseUomID"]);
                    tbl_Products.SaleUomID = Convert.ToInt32(r["SaleUomID"]);
                    tbl_Products.VatType = Convert.ToBoolean(r["VatType"]);

                    tbl_Products.StandardCost = Convert.ToDecimal(r["StandardCost"]);
                    tbl_Products.SellPrice = Convert.ToDecimal(r["SellPrice"]);
                    tbl_Products.IsFulfill = Convert.ToBoolean(r["IsFulfill"]);

                    tbl_Products.Remark = r["Remark"].ToString();

                    tbl_Products.CrDate = Convert.ToDateTime(r["CrDate"]);
                    tbl_Products.CrUser = r["CrUser"].ToString();
                    tbl_Products.EdDate = DateTime.Now;
                    tbl_Products.EdUser = Helper.tbl_Users.Username;

                    tbl_Products.FlagDel = Convert.ToBoolean(r["FlagDel"]);

                    if (!string.IsNullOrEmpty(r["ProductImg"].ToString()))
                    {
                        Byte[] data = new Byte[0];
                        data = (Byte[])r["ProductImg"];
                        tbl_Products.ProductImg = data;
                    }

                    tbl_Products.FlagSend = Convert.ToBoolean(r["FlagSend"]);
                    tbl_Products.FlagNew = Convert.ToBoolean(r["FlagNew"]);
                    tbl_Products.FlagEdit = Convert.ToBoolean(r["FlagEdit"]);

                    tbl_Products.ProductBrandID = Convert.ToInt32(r["ProductBrandID"]);
                    tbl_Products.ProductLine = Convert.ToInt32(r["ProductLine"]);

                    proList.Add(tbl_Products);
                }
                foreach (var item in proList)
                {
                    ret = bu.UpdateData(item);
                }
                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                    btnSearch.PerformClick();
                }
            }
        }

        private void btnProSubGroup_Click(object sender, EventArgs e)
        {
            frmProductSubGroup frm = new frmProductSubGroup();
            frm.ShowDialog();
            if (prdsubgroupID != "")
            {
                ddlProSubGroup.SelectedValue = Convert.ToInt32(prdsubgroupID);
            }
        }


        #endregion

        #region EventPage
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
                BindProGroup(prdtypeID, ddlPrdGroup); // ProductGroup 
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

        private void rdoN_CheckedChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void rdoC_CheckedChanged(object sender, EventArgs e)
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
            btnEdit.Enabled = true;
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
            grdList2.SetRowPostPaint(sender, e, this.Font);
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

        private void grdPro_SelectionChanged(object sender, EventArgs e)
        {
            SelectProductDetails(null);
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
            grdList2.SetCellNumberOnly(sender, e, numberCell.ToList());
        }

        private void grdList2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (grdList2.Columns[e.ColumnIndex].Name != "colUnit")
            {
                if (grdList2.CurrentRow.Cells[e.ColumnIndex].Value == null)
                {
                    grdList2.CurrentRow.Cells[e.ColumnIndex].Value = 0.00;
                }
                else
                {
                    if (grdList2.Columns[e.ColumnIndex].Name != "colBaseQty")//จำนวน
                    {
                        decimal colNo = Convert.ToDecimal(grdList2.CurrentRow.Cells[e.ColumnIndex].Value);

                        grdList2.CurrentRow.Cells[e.ColumnIndex].Value = colNo.ToDecimalN2();
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            flagEdit = true;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            OpenPanelEdit(true);

            GrdList2ReadOnly(false);

            btnProGroup.Enabled = false; // ปุ่มเพิ่มกลุ่มสินค้า

            txtProductRefCode.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlGridView.Enabled = true;

            pnlEdit.ClearControl();

            SetDefaultNumber();

            rdoVatN.Checked = true;
            rdoVatF.Checked = false;

            chkTablet.Checked = false;
            chkTablet.Enabled = false;

            SetDefaultDropDownEdit();

            SetDefaultGrdList2();

            OpenPanelEdit(false);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnRemove.Enabled = true;

            ddlProUom.Enabled = false;

            grdPro.Enabled = true;

            GrdList2ReadOnly(false);

            btnSearch.PerformClick();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void txtProductID_KeyPress(object sender, KeyPressEventArgs e)
        {
            //txtProductID.SetTextBoxNumberOnly(e);
        }

        private void txtProductRefCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //txtProductRefCode.SetTextBoxNumberOnly(e);
        }

        private void grdList2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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
                    decimal colBaseQty = Convert.ToDecimal(grdList2.CurrentRow.Cells["colBaseQty"].Value).ToDecimalN2();
                    int _colUnit = Convert.ToInt32(grdList2.CurrentRow.Cells["colUnit"].Value);
                    decimal _colBuyPrice = Convert.ToDecimal(grdList2.CurrentRow.Cells["colBuyPrice"].Value).ToDecimalN2();

                    if (colBaseQty > 0 && _colUnit != -1 && _colBuyPrice > 0)// && colSellPrice > 0 && colSellPriceVat > 0
                    {
                        int currentRowIndex = grd.CurrentCell.RowIndex;
                        int curentColIndex = grd.CurrentCell.ColumnIndex;

                        grd.ClearSelection();

                        if (curentColIndex == 7)
                        {
                            grdList2.AddNewRow(initDataGridList, 1, "", currentRowIndex + 1, true, uoms, bu, this, 0); //
                        }

                    }
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            flagEdit = false;

            pnlGridView.Enabled = false;
            grdPro.Enabled = false;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            OpenPanelEdit(true);

            GrdList2ReadOnly(false);

            btnProGroup.Enabled = false; // ปุ่มเพิ่มกลุ่มสินค้า

            txtProductID.Text = "";
            txtProductID.DisableTextBox(false);

            txtProductID.Focus();

            ddlProUom.Enabled = true;
        }

        private void btnPrdCheck_Click(object sender, EventArgs e)
        {
            bool ProIsCorrect = false;
            string colProductID = grdPro.CurrentRow.Cells["colProductID"].Value.ToString();

            DataTable dtPrdCheck = new DataTable();

            dtPrdCheck = bu.GetProductViewCheck(colProductID);

            if (dtPrdCheck.Rows.Count > 0)
            {
                var PrdCheck = dtPrdCheck.AsEnumerable().ToList();

                foreach (DataRow r in dtPrdCheck.Rows)
                {
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
                        break;
                    }
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
        }

        private void grdList2_KeyDown(object sender, KeyEventArgs e)
        {
            if (grdList2.RowCount > 1)
            {
                grdList2.SetDeleteKeyDown(sender, e);
            }
        }

        private void frmProductInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveProduct();
        }

        #endregion

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
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            uoms.AddRange(bu.GetUOM());

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
            ddlProType.BindDropdownList(PrdType, "ProductTypeName", "ProductTypeID", 0);

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
            var flavor = bu.GetProductFlavour();
            ddlProFlavour.BindDropdownList(flavor, "ProductFlavourName", "ProductFlavourID");
        }

        private void InitialData()
        {
            chkBoxFlagDel.Checked = false; //นำกลับมาใช้ใหม่  FlagDel = True Check False Uncheck
            chkBoxFlagDel.Visible = false; 

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

            GrdList2ReadOnly(true);

            grdPro.AutoGenerateColumns = false;
            grdList2.AutoGenerateColumns = false;
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
                    if (item is Label || item is Panel || item is PictureBox || item is CheckBox || item is Button || item is GroupBox || item is ComboBox || item is ListBox)
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
            //ddlProUom.Enabled = false;
            chkBoxFlagDel.Enabled = false; //นำกลับมาใช้ใหม่
        }

        private void BindProGroup(int prdtypeID, ComboBox ddl)
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

        private void GrdList2ReadOnly(bool flag)
        {
            grdList2.Columns["colUnit"].ReadOnly = flag;

            grdList2.Columns["colProductID2"].ReadOnly = flag;

            grdList2.Columns["colBaseQty"].ReadOnly = flag;

            grdList2.Columns["colBuyPrice"].ReadOnly = flag;
            grdList2.Columns["colSellPrice"].ReadOnly = flag;
            grdList2.Columns["colSellPriceVat"].ReadOnly = flag;
            grdList2.Columns["colComPrice"].ReadOnly = flag;

            grdList2.Columns["colWeight"].ReadOnly = flag;
        }

        private void PrePareButton_Search()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            chkBoxFlagDel.Visible = false;
            btnEdit.Enabled = false;
            btnRemove.Enabled = false;

            btnSaveSeq.Enabled = false;

            btnPrdCheck.Enabled = false;

            if (grdPro.Rows.Count > 0)
            {
                btnPrdCheck.Enabled = true;
                btnEdit.Enabled = true;
                btnCopy.Enabled = true;
                btnSaveSeq.Enabled = true;

                grdPro.Columns["colSeq"].ReadOnly = false;
                GrdList2ReadOnly(true);
            }
            if (grdPro.Rows.Count > 0 && rdoN.Checked == true)
            {
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
            }
        }

        private void BindData()
        {
            pnlEdit.ClearControl();

            OpenPanelEdit(false);

            SetDefaultNumber();

            dtTempPrd = new DataTable();

            int flagDel = rdoN.Checked ? 0 : 1;

            int ProductGroupID = ddlPrdGroup.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlPrdGroup.SelectedValue);
            int ProductSubGroupID = ddlPrdSubGroup.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlPrdSubGroup.SelectedValue);

            dtTempPrd = bu.GetProductView(flagDel,ProductGroupID,ProductSubGroupID,txtSearch.Text);

            grdPro.DataSource = dtTempPrd;
            lblgrdQty.Text = dtTempPrd.Rows.Count.ToNumberFormat();

            PrePareButton_Search();
        }

        private void SelectProductDetails(DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow gridViewRow = null;

                if (e != null)
                {
                    if (e.RowIndex == -1)
                    {
                        return;
                    }
                    else
                    {
                        gridViewRow = grdPro.Rows[e.RowIndex];
                    } 
                }
                else
                {
                    gridViewRow = grdPro.CurrentRow;
                }

                if (gridViewRow != null)
                {
                    string proID = gridViewRow.Cells["colProductID"].Value.ToString();

                    //List<tbl_ProductUom> prodUOMs = new List<tbl_ProductUom>();
                    //prodUOMs.AddRange(bu.GetProductUOM());

                    foreach (DataRow r in dtTempPrd.Rows)
                    {
                        string TempProID = r["ProductID"].ToString();
                        if (TempProID == proID)
                        {
                            txtProductID.Text = TempProID;
                            txtProductRefCode.Text = r["ProductRefCode"].ToString();

                            ddlProType.SelectedValue = Convert.ToInt32(r["ProductTypeID"]);

                            ddlProGroup.SelectedValue = Convert.ToInt32(r["ProductGroupID"]);

                            ddlProSubGroup.SelectedValue = Convert.ToInt32(r["ProductSubGroupID"]);

                            txtBarCode.Text = r["BarCode"].ToString();
                            txtProductName.Text = r["ProductName"].ToString();
                            txtProductShortName.Text = r["ProductShortName"].ToString();

                            ddlProUom.SelectedValue = Convert.ToInt32(r["SaleUomID"]); //หน่วยเล็กที่สุด
                            ddlProFlavour.Text = r["Flavour"].ToString(); // รสชาติ

                            if (Convert.ToBoolean(r["FlagDel"]) == false) // ปกติ
                            {
                                chkBoxFlagDel.Checked = true;
                            }
                            else //ยกเลิก
                            {
                                chkBoxFlagDel.Checked = false;
                            }

                            grdList2.Rows.Clear();

                            Dictionary<string, object> _params = new Dictionary<string, object>();

                            _params.Add("ProductID",TempProID);

                            DataTable dtPrdGrp = bu.GetProductGroupPriceData(_params);

                            if (dtPrdGrp.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtPrdGrp.Rows.Count; i++)
                                {
                                    grdList2.Rows.Add(1);

                                    grdList2.Rows[i].Cells["colProductID2"].Value = dtPrdGrp.Rows[i].Field<string>("ProductID");

                                    grdList2.BindComboBoxCell(grdList2.Rows[i], i, true, 0, uoms, this, bu, 1);

                                    grdList2.Rows[i].Cells["colUnit"].Value = dtPrdGrp.Rows[i].Field<int>("UomSetID");

                                    grdList2.Rows[i].Cells["colBaseQty"].Value = dtPrdGrp.Rows[i].Field<int>("BaseQty").ToString();

                                    grdList2.Rows[i].Cells["colBuyPrice"].Value = dtPrdGrp.Rows[i].Field<decimal>("BuyPrice");

                                    grdList2.Rows[i].Cells["colSellPrice"].Value = dtPrdGrp.Rows[i].Field<decimal>("SellPrice");

                                    grdList2.Rows[i].Cells["colSellPriceVat"].Value = dtPrdGrp.Rows[i].Field<decimal>("SellPriceVat");

                                    grdList2.Rows[i].Cells["colComPrice"].Value = dtPrdGrp.Rows[i].Field<decimal>("ComPrice");

                                    grdList2.Rows[i].Cells["colWeight"].Value = dtPrdGrp.Rows[i].Field<decimal>("Weight");

                                }
                            }
                            else
                            {
                                SetDefaultGrdList2();
                            }

                            if (!string.IsNullOrEmpty(r["VatType"].ToString()))
                            {
                                bool GetVat = Convert.ToBoolean(r["VatType"]);
                                if (GetVat == true)
                                {
                                    rdoVatN.Checked = true; //มีภาษี
                                }
                                else if (GetVat == false)
                                {
                                    rdoVatF.Checked = true; //ไม่มีภาษี
                                }
                            }

                            txtReorderPoint.Text = r["ReorderPoint"].ToString(); //จุุดสั่งซื้อ
                            txtMinPoint.Text = r["MinPoint"].ToString(); //จุดต่ำสุด

                            decimal weight = Convert.ToDecimal(r["Weight"]);
                            txtWeight.Text = weight.ToStringN0(); //น้ำหนักบรรจุ

                            decimal size = Convert.ToDecimal(r["Size"]);
                            txtSize.Text = size.ToStringN0(); //ปริมาณบรรจุหน้าซอง

                            if (!string.IsNullOrEmpty(r["IsFulfill"].ToString()))
                            {
                                bool isFulfill = Convert.ToBoolean(r["IsFulfill"]);
                                if (isFulfill == true)
                                {
                                    chkTablet.Checked = true; //เช็ค
                                }
                                else if (isFulfill == false)
                                {
                                    chkTablet.Checked = false; //ไม่เช็ค
                                }
                            }

                            if (!string.IsNullOrEmpty(r["ProductImg"].ToString()))
                            {
                                Byte[] data = new Byte[0];
                                data = (Byte[])r["ProductImg"];
                                MemoryStream mem = new MemoryStream(data);
                                picProductImg.Image = Image.FromStream(mem);
                            }
                            else
                            {
                                picProductImg.Image = null;
                            }

                            break;
                        }
                    }
                 }   
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void SetDefaultGrdList2()
        {
            grdList2.Rows.Clear();

            var prdUoms = bu.GetProductUOM();

            var allpro = bu.GetProduct();

            for (int i = 0; i < prdUoms.Count; i++)
            {
                grdList2.Rows.Add(1);
                grdList2.Rows[i].Cells["colProductID2"].Value = allpro.First().ProductID;
                grdList2.BindComboBoxCell(grdList2.Rows[i], i, true, 0, uoms, this, bu, 1);


                grdList2.Rows[i].Cells["colUnit"].Value = prdUoms[i].ProductUomID;

                decimal prdUom = 0;

                grdList2.Rows[i].Cells["colBaseQty"].Value = prdUom;
                grdList2.Rows[i].Cells["colBuyPrice"].Value = prdUom.ToDecimalN2();
                grdList2.Rows[i].Cells["colSellPrice"].Value = prdUom.ToDecimalN2();
                grdList2.Rows[i].Cells["colSellPriceVat"].Value = prdUom.ToDecimalN2();
                grdList2.Rows[i].Cells["colComPrice"].Value = prdUom.ToDecimalN2();
                grdList2.Rows[i].Cells["colWeight"].Value = prdUom.ToDecimalN2();
            }
        }

        private void RemoveProduct()
        {
            try
            {
                string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
                string title = "ทำการยืนยัน!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;
                var proList = bu.GetProductEntity(x => x.ProductID == txtProductID.Text); //สร้างใหม่

                if (proList.Count > 0)
                {
                    proList[0].FlagDel = true;
                    proList[0].EdDate = DateTime.Now;
                    proList[0].EdUser = Helper.tbl_Users.Username;

                    foreach (var item in proList)
                    {
                        ret = bu.UpdateData(item);
                    }

                    if (ret == 1)
                    {
                        var prouomsetList = bu.GetProductUOMSet(x => x.ProductID == txtProductID.Text);

                        for (int i = 0; i < prouomsetList.Count; i++)
                        {
                            prouomsetList[i].FlagDel = true;
                            prouomsetList[i].EdDate = DateTime.Now;
                            prouomsetList[i].EdUser = Helper.tbl_Users.Username;
                        }

                        foreach (var item in prouomsetList)
                        {
                            ret = bu.UpdateData(item); // New Method on Product Controller
                        }

                        if (ret == 1)
                        {
                            var proPriceGroupList = bu.GetProductPriceGroup(x => x.ProductID == txtProductID.Text);

                            for (int i = 0; i < proPriceGroupList.Count; i++)
                            {
                                proPriceGroupList[i].FlagDel = true;
                                proPriceGroupList[i].EdDate = DateTime.Now;
                                proPriceGroupList[i].EdUser = Helper.tbl_Users.Username;
                            }

                            foreach (var item in proPriceGroupList)
                            {
                                ret = bu.UpdateData(item); // New Method on Product Controller
                            }

                            if (ret == 1)
                            {
                                string msg = "ลบข้อมูลเรียบร้อยแล้ว";
                                msg.ShowInfoMessage();
                                btnSearch.PerformClick();
                            }
                        }
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

        private bool ValidateData(List<tbl_Product> PrdList)
        {
            bool ret = true;// ถ้าเป็นการ new Insert จะนำรหัสไปค้นหาใน DB ว่ามีหรือไม่ 

            string msg = "";

            if (PrdList.Count > 0)
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

        private void PrePareRemove_ProductUomSet()
        {
            int ret = 0;

            var PrdUomSetList = bu.GetProductUOMSet(x => x.ProductID == txtProductID.Text);

            if (PrdUomSetList.Count > 0)
            {
                foreach (var item in PrdUomSetList)
                {
                    ret = bu.RemoveData(item);
                }
            }
        }

        private void PrePareRemove_ProductPriceGroup()
        {
            int ret = 0;

            var PrdPriceGroupList = bu.GetProductPriceGroup(x => x.ProductID == txtProductID.Text);

            foreach (var item in PrdPriceGroupList)
            {
                ret = bu.RemoveData(item);
            }

        }

        private void PrePareSave_ProductUomSet(tbl_ProductUomSet ProUomSet)
        {
            ProUomSet.ProductID = txtProductID.Text;

            ProUomSet.StandardCost = null;
            ProUomSet.UomCode = "";

            ProUomSet.BaseUomID = Convert.ToInt32(ddlProUom.SelectedValue);
            ProUomSet.BaseUomName = ddlProUom.Text;

            ProUomSet.CrDate = DateTime.Now;
            ProUomSet.CrUser = Helper.tbl_Users.Username;

            ProUomSet.EdDate = null;
            ProUomSet.EdUser = null;

            ProUomSet.FlagDel = false;
            ProUomSet.FlagSend = false;
            ProUomSet.FlagNew = false;
            ProUomSet.FlagEdit = false;
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

            for (int i = 0; i < grdList2.Rows.Count; i++)
            {
                int _BaseQty = Convert.ToInt32(grdList2.Rows[i].Cells["colBaseQty"].Value);
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

        private void Save()
        {
            try
            {
                if (!ValidateSave())
                {
                    return;
                }

                if (!ValidateBaseQty())
                {
                    return;
                }
               

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

              
                var PrdList = new List<tbl_Product>();

                if (flagEdit == false)
                {
                    PrdList = bu.GetProductEntity(x => x.ProductID == txtProductID.Text);

                    if (!ValidateData(PrdList))
                    {
                        return;
                    }
                }

                if (flagEdit == true) //Remove OldData tbl_ProductUomSet , tbl_ProductPriceGroup
                {
                    PrePareRemove_ProductUomSet();
                    PrePareRemove_ProductPriceGroup();
                }

                int ret = 0;

                var PrdUomSetList = new List<tbl_ProductUomSet>();
                var PrdPriceGroupList = new List<tbl_ProductPriceGroup>();

                var PrdUomSet = new tbl_ProductUomSet();
                var PrdPriceGroup = new tbl_ProductPriceGroup();

                var PrdUom = bu.GetProductUOM();

                for (int i = 0; i < grdList2.Rows.Count; i++)
                {
                    string BaseQty = grdList2.Rows[i].Cells["colBaseQty"].Value.ToString();
                    int _colUnit = Convert.ToInt32(grdList2.Rows[i].Cells["colUnit"].Value);
                    int _colBuyPrice = Convert.ToInt32(grdList2.Rows[i].Cells["colBuyPrice"].Value);

                    if (BaseQty != "0")//&& _colUnit != -1 && _colBuyPrice > 0
                    {
                        PrdUomSet = new tbl_ProductUomSet();
                        PrdUomSet.UomSetID = _colUnit;
                        PrdUomSet.UomSetName = PrdUom.FirstOrDefault(x => x.ProductUomID == _colUnit).ProductUomName;
                        PrdUomSet.BaseQty = Convert.ToInt32(BaseQty);
                        PrdUomSet.Weight = Convert.ToDecimal(grdList2.Rows[i].Cells["colWeight"].Value);

                        PrePareSave_ProductUomSet(PrdUomSet);

                        PrdUomSetList.Add(PrdUomSet);

                        //-----------//

                        PrdPriceGroup = new tbl_ProductPriceGroup();

                        PrdPriceGroup.ProductUomID = _colUnit;

                        PrdPriceGroup.SellPrice = Convert.ToDecimal(grdList2.Rows[i].Cells["colSellPrice"].Value);

                        decimal BuyPrice = Convert.ToDecimal(grdList2.Rows[i].Cells["colBuyPrice"].Value);

                        PrdPriceGroup.BuyPrice = BuyPrice;

                        PrdPriceGroup.SellPriceVat = Convert.ToDecimal(grdList2.Rows[i].Cells["colSellPriceVat"].Value);

                        decimal vat = (BuyPrice * 7) / 100;
                        decimal BuyPriceVat = BuyPrice + vat.ToDecimalN2();

                        PrdPriceGroup.BuyPriceVat = BuyPriceVat;

                        PrdPriceGroup.ComPrice = Convert.ToDecimal(grdList2.Rows[i].Cells["colComPrice"].Value);

                        PrePareSave_ProductPriceGroup(PrdPriceGroup);

                        PrdPriceGroupList.Add(PrdPriceGroup);
                    }
                }

                //--------------//

                if (PrdUomSetList.Count > 0)
                {
                    foreach (var item in PrdUomSetList) // INSERT
                    {
                        ret = bu.UpdateData(item);
                    }
                }

                if (PrdPriceGroupList.Count > 0)
                {
                    foreach (var item in PrdPriceGroupList)   // INSERT
                    {
                        ret = bu.UpdateData(item);
                    }
                }

                //-----------//
                if (PrdUomSetList.Count > 0)
                {
                    ret = PrePareSave_Product(PrdList, PrdUomSetList);
                }
                

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    msg.ShowInfoMessage();

                    pnlGridView.Enabled = true;

                    OpenPanelEdit(false);

                    grdPro.Enabled = true;

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
                return;
            }
        }

        private int PrePareSave_Product(List<tbl_Product> PrdList, List<tbl_ProductUomSet> PrdUomList)
        {
            int ret = 0;

            var Prd = new tbl_Product();

            var ProUomSet = new tbl_ProductUomSet();

            if (PrdUomList.Count > 0)
            {
                ProUomSet = PrdUomList.FirstOrDefault(x => x.BaseQty > 1);
            }

            if (PrdList.Count > 0)
            {
                Prd = PrdList[0];

                Prd.EdDate = DateTime.Now;
                Prd.EdUser = Helper.tbl_Users.Username;
            }
            else
            {
                Prd.ProductCode = txtProductID.Text;

                Prd.CrDate = DateTime.Now;
                Prd.CrUser = Helper.tbl_Users.Username;

                Prd.EdDate = null;
                Prd.EdUser = null;

                Prd.Seq = 0;
            }

            if (ProUomSet != null && PrdUomList.Count > 0)
            {
                Prd.PurchaseUomID = PrdUomList.FirstOrDefault(x => x.BaseQty > 1).UomSetID;//ซื้อ
            }
            else
            {
                Prd.PurchaseUomID = Convert.ToInt32(ddlProUom.SelectedValue);
            }

            pnlEdit.Controls.SetObjectFromControl(Prd);

            Prd.SaleUomID = Convert.ToInt32(ddlProUom.SelectedValue); //หน่วยขาย เป็นหน่วยที่เล็กที่สุด

            Prd.WeightUOM = ddlProUom.Text;
            Prd.SizeUOM = ddlProUom.Text;

            Prd.ProductGroupID = Convert.ToInt32(ddlProGroup.SelectedValue);

            Prd.ProductSubGroupID = Convert.ToInt32(ddlProSubGroup.SelectedValue);

            Prd.Barcode = txtBarCode.Text;

            Prd.Flavour = "ไม่ระบุ"; //Fix

            Prd.VatType = rdoVatN.Checked ? true : false;  //1 มีภาษี 0 ไม่มีภาษี

            Prd.Remark = "";

            Prd.StandardCost = 0;  // ???

            Prd.IsFulfill = chkTablet.Checked ? true : false;

            Prd.FlagDel = chkBoxFlagDel.Checked ? false : true;

            Prd.FlagSend = false;
            Prd.FlagNew = false;
            Prd.FlagEdit = false;

            Prd.ProductImg = null; //ไม่Upload Image

            Prd.ProductBrandID = 0;
            Prd.ProductLine = 0;

            decimal _sellPrice = 0;
            Prd.SellPrice = _sellPrice.ToDecimalN2();

            ret = bu.UpdateData(Prd);

            return ret;
        }

    }
}
