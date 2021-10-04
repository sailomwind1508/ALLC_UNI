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

            initDataGridList = new Dictionary<int, string>() { { 0, "combobox" }, { 1, "0" }, { 2, "0" }, { 3, "0" }, { 4, "0" }, { 5, "0" }, { 6, "0" }, { 7, "0" } };
        }

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

            grdList2.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(grdList2_UserDeletingRow);
            grdList2.UserDeletingRow -= new System.Windows.Forms.DataGridViewRowCancelEventHandler(grdList2_UserDeletingRow);

        }
        private void SetDefaultNumber()
        {
            txtReorderPoint.Text = "0";
            txtMinPoint.Text = "0";
            textBox1.Text = "0.00";//ราคาต้นทุนเฉลี่ย = 0.00  --เกิดจากการ Sum ???
            textBox2.Text = "0.00";//มูลค่าสินค้า = 0.00  --เกิดจากการ Sum ???
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
            //ประเภทสินค้า -grd
            var PrdType = new List<tbl_ProductType>();
            PrdType.Add(new tbl_ProductType { ProductTypeID = -1, ProductTypeName = "==เลือก==" });
            var _PrdType = bu.GetProductType(x => x.FlagDel == false); //สร้างใหม่
            PrdType.AddRange(_PrdType);
            ddlPrdType.BindDropdownList(PrdType, "ProductTypeName", "ProductTypeID");

            //กลุ่มสินค้า -grd
            ddlProGroupNoData(ddlPrdGroup);

            //กลุ่มย่อยสินค้า -grd
            ddlProSubGroupNoData(ddlPrdSubGroup);

            SetDefaultDropDownEdit();

            PanelEdit(false);

            GrdList2ReadOnly(true);

            grdPro.AutoGenerateColumns = false;
            grdList2.AutoGenerateColumns = false;
            grdPro.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            chkTablet.Enabled = false;

            btnBrowse.Enabled = false; // รูป

            btnSaveSeq.Enabled = false; //ลำดับ
        }
        private void PanelEdit(bool flag)
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
            SetEnableControl();
        }
        private void SetEnableControl()
        {
            textBox1.ReadOnly = true; //ราคาต้นทุนเฉลี่ย
            textBox2.ReadOnly = true; //มูลค่าสินค้า = 0.00
            button1.Enabled = false; // ดึงข้อมูลจาก Center
            btnBrowse.Enabled = false; //รูปปิด
            btnProGroup.Enabled = false; //กลุ่มสินค้า
            ddlProUom.Enabled = false;
        }
        private void frmProductInfo_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }
        private void BindDDLProGroup(int prdtypeID, ComboBox ddl)
        {
            var prdgroupID = bu.GetProductGroup().Where(x => x.FlagDel == false && x.ProductTypeID == prdtypeID).ToList();
            var prdgroup = new List<tbl_ProductGroup>();
            prdgroup.Add(new tbl_ProductGroup { ProductGroupID = -1, ProductGroupName = "==เลือก==" });
            prdgroup.AddRange(prdgroupID);
            ddl.BindDropdownList(prdgroup, "ProductGroupName", "ProductGroupID");
        }
        private void BindDDLProSubGroup(ComboBox ddl)
        {
            var prdSubGroup = bu.GetProductSubGroup(x => x.FlagDel == false);
            var _prdSubGroup = new List<tbl_ProductSubGroup>();
            _prdSubGroup.Add(new tbl_ProductSubGroup { ProductSubGroupID = -1, ProductSubGroupName = "==เลือก==" });
            _prdSubGroup.AddRange(prdSubGroup);
            ddl.BindDropdownList(_prdSubGroup, "ProductSubGroupName", "ProductSubGroupID");
        }
        private void ddlPrdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPrdType.SelectedIndex > 0)
            {
                int prdtypeID = Convert.ToInt32(ddlPrdType.SelectedValue);
                BindDDLProGroup(prdtypeID, ddlPrdGroup); // ProductGroup 
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
                BindDDLProSubGroup(ddlPrdSubGroup);
            }
            else
            {
                ddlProSubGroupNoData(ddlPrdSubGroup);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void BindProductInfo()
        {
            pnlEdit.ClearControl();

            PanelEdit(false);

            SetDefaultNumber();

            string text = txtSearch.Text; //

            dtTempPrd = new DataTable();

            Func<tbl_Product, bool> tbl_ProductFunc = null;

            bool flag = rdoN.Checked ? false : true;

            tbl_ProductFunc = (x => x.FlagDel == flag
            && x.ProductGroupID == (Convert.ToInt32(ddlPrdGroup.SelectedValue) != -1 ? Convert.ToInt32(ddlPrdGroup.SelectedValue) : x.ProductGroupID)
            && x.ProductSubGroupID == (Convert.ToInt32(ddlPrdSubGroup.SelectedValue) != -1 ? Convert.ToInt32(ddlPrdSubGroup.SelectedValue) : x.ProductSubGroupID)
            && (x.ProductID.Contains(text) || x.ProductName.Contains(text)));

            dtTempPrd = bu.GetPrdTable(tbl_ProductFunc);

            if (dtTempPrd.Rows.Count > 0)
            {

                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;

                lblgrdQty.Text = dtTempPrd.Rows.Count.ToNumberFormat();
                grdPro.Columns["colSeq"].ReadOnly = false;

                GrdList2ReadOnly(true);

                btnSaveSeq.Enabled = true;
            }
            else
            {
                lblgrdQty.Text = "0";

                rdoVatN.Checked = true;
                rdoVatF.Checked = false;

                chkTablet.Checked = false;
                chkTablet.Enabled = false;

                SetDefaultDropDownEdit();
                SetDefaultGrdList2();

                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnAdd.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;

                btnSaveSeq.Enabled = false;
            }
            grdPro.DataSource = dtTempPrd;

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindProductInfo();
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
            BindProductInfo();
        }

        private void rdoC_CheckedChanged(object sender, EventArgs e)
        {
            BindProductInfo();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindProductInfo();
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
        private void SelectProductDetails(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e != null)
                {
                    if (e.RowIndex == -1)
                        return;
                }

                DataGridViewRow gridrow = null;
                if (e != null)
                    gridrow = grdPro.Rows[e.RowIndex];
                else
                    gridrow = grdPro.CurrentRow;

                string proID = grdPro.CurrentRow.Cells["colProductID"].Value.ToString();

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

                        grdList2.Rows.Clear();

                        DataTable dtPrdGrp = bu.GetPrdPriceGroupTable(x => x.ProductID == TempProID);
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
            catch (Exception ex)
            {
                picProductImg.Image = null;
                return;
            }
        }
        private void grdPro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit.Enabled = true;
            SelectProductDetails(e);
        }

        private void ddlProType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProType.SelectedIndex > 0)
            {
                int protypeID = Convert.ToInt32(ddlProType.SelectedValue);
                var progroup = bu.GetProductGroup().Where(x => x.FlagDel == false && x.ProductTypeID == protypeID).ToList();
                ddlProGroup.BindDropdownList(progroup, "ProductGroupName", "ProductGroupID", 0);
            }
        }

        private void ddlProGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            var prosubgroup = bu.GetProductSubGroup(x => x.FlagDel == false );
            ddlProSubGroup.BindDropdownList(prosubgroup, "ProductSubGroupName", "ProductSubGroupID");
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
                grdList2.Rows[i].Cells["colBuyPrice"].Value = prdUom;
                grdList2.Rows[i].Cells["colSellPrice"].Value = prdUom;
                grdList2.Rows[i].Cells["colSellPriceVat"].Value = prdUom;
                grdList2.Rows[i].Cells["colComPrice"].Value = prdUom;
                grdList2.Rows[i].Cells["colWeight"].Value = prdUom;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            flagEdit = false;
            pnlGridView.Enabled = false;
            grdPro.Enabled = false;

            pnlEdit.ClearControl();

            PanelEdit(true);

            GrdList2ReadOnly(false);

            txtProductID.BackColor = Color.White;
            txtProductID.ReadOnly = false;

            SetDefaultNumber();

            SetDefaultGrdList2();

            chkTablet.Enabled = true;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = false;

            grdPro.Enabled = false;

            ddlProUom.Enabled = true;
        }

        private void grdList2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList2.SetRowPostPaint(sender, e, this.Font);
        }

        private void txtKeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            else
            {
                return;
            }
        }
        private void ddlProGroup_DropDownStyleChanged(object sender, EventArgs e)
        {

        }

        private void txtReorderPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txtMinPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txtSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void grdPro_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void grdPro_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

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
            if (grdList2.CurrentRow.Cells[e.ColumnIndex].Value == null)
            {
                grdList2.CurrentRow.Cells[e.ColumnIndex].Value = 0;
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            flagEdit = true;
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            PanelEdit(true);

            GrdList2ReadOnly(false);

            btnProGroup.Enabled = false; // ปุ่มเพิ่มกลุ่มสินค้า

            chkTablet.Enabled = true;
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

            PanelEdit(false);

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
        private void RemoveProduct()
        {
            if (txtProductID.Text != "")
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
                    string msg = ex.Message.ToString();
                    msg.ShowErrorMessage();
                }
            }
            else
            {
                string msg = "ไม่พบสินค้าที่ต้องการลบ !!";
                msg.ShowWarningMessage();
                return;
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveProduct();
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
        private void Save()
        {

            try
            {
                var tbl_ProductList = new List<tbl_Product>();

                var tbl_ProductUomSetList = new List<tbl_ProductUomSet>();

                var tbl_ProductPriceGroupList = new List<tbl_ProductPriceGroup>();

                tbl_ProductList = bu.GetProductEntity(x => x.ProductID == txtProductID.Text);

                if (flagEdit == false) // ถ้าเป็นการ new Insert จะนำรหัสไปค้นหาใน DB ว่ามีหรือไม่ 
                {
                    if (tbl_ProductList.Count > 0)
                    {
                        string msg = "รหัสสินค้าไม่ถูกต้อง รหัสสินค้าซ้ำในระบบ !!";
                        msg.ShowErrorMessage();
                        return;
                    }
                    if (txtProductID.TextLength < 8 && txtProductID.TextLength > 8)
                    {
                        string _msg = "รหัสสินค้าไม่ควรน้อยหรือมากกว่า 8 หลัก ";
                        _msg.ShowErrorMessage();
                        return;
                    }
                }

                if (!ValidateSave())
                {
                    return;
                }

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;

                if (flagEdit == true)
                {
                    tbl_ProductUomSetList = bu.GetProductUOMSet(x => x.ProductID == txtProductID.Text); // FlagDel == false

                    if (tbl_ProductUomSetList.Count > 0)
                    {
                        foreach (var item in tbl_ProductUomSetList)
                        {
                            ret = bu.RemoveData(item);
                        }

                        if (ret == 1)
                        {
                            tbl_ProductPriceGroupList = bu.GetProductPriceGroup(x => x.ProductID == txtProductID.Text); // FlagDel == false

                            if (tbl_ProductPriceGroupList.Count > 0)
                            {
                                foreach (var item in tbl_ProductPriceGroupList)
                                {
                                    ret = bu.RemoveData(item);
                                }
                            }
                        }

                    }
                }

                // new Insert ---------------------////////

                tbl_ProductUomSetList = new List<tbl_ProductUomSet>(); //Save

                tbl_ProductPriceGroupList = new List<tbl_ProductPriceGroup>(); //Save

                var ProductUom = bu.GetProductUOM(); //หีบ Pack

                for (int i = 0; i < grdList2.Rows.Count; i++)
                {
                    string colBaseQty = grdList2.Rows[i].Cells["colBaseQty"].Value.ToString();
                    string colBuyPrice = grdList2.Rows[i].Cells["colBuyPrice"].Value.ToString();
                    string colSellPrice = grdList2.Rows[i].Cells["colSellPrice"].Value.ToString();
                    string colSellPriceVat = grdList2.Rows[i].Cells["colSellPriceVat"].Value.ToString();
                    string colComPrice = grdList2.Rows[i].Cells["colComPrice"].Value.ToString();

                    //จำนวน                      
                    if (colBaseQty != "0")
                    {
                        tbl_ProductUomSet tbl_ProductUomSets = new tbl_ProductUomSet();
                        tbl_ProductUomSets.ProductID = txtProductID.Text;
                        int colUnit = Convert.ToInt32(grdList2.Rows[i].Cells["colUnit"].Value);
                        tbl_ProductUomSets.UomSetID = colUnit;  //
                        tbl_ProductUomSets.UomSetName = ProductUom.FirstOrDefault(x => x.ProductUomID == colUnit).ProductUomName;

                        tbl_ProductUomSets.BaseUomID = ProductUom.FirstOrDefault(x => x.ProductUomID == 2).ProductUomID;
                        tbl_ProductUomSets.BaseUomName = ProductUom.FirstOrDefault(x => x.ProductUomID == 2).ProductUomName;

                        tbl_ProductUomSets.BaseQty = Convert.ToInt32(colBaseQty);

                        tbl_ProductUomSets.CrDate = DateTime.Now;
                        tbl_ProductUomSets.CrUser = Helper.tbl_Users.Username;

                        tbl_ProductUomSets.EdDate = null;
                        tbl_ProductUomSets.EdUser = null;

                        tbl_ProductUomSets.FlagDel = false;
                        tbl_ProductUomSets.FlagSend = false;
                        tbl_ProductUomSets.FlagNew = false;
                        tbl_ProductUomSets.FlagEdit = false;

                        tbl_ProductUomSets.Weight = Convert.ToDecimal(grdList2.Rows[i].Cells["colWeight"].Value);

                        tbl_ProductUomSets.StandardCost = null;
                        tbl_ProductUomSets.UomCode = "";

                        tbl_ProductUomSetList.Add(tbl_ProductUomSets);

                        ////////------------------------------------------------///////////////

                        var tbl_ProductPriceGroups = new tbl_ProductPriceGroup();

                        tbl_ProductPriceGroups.PriceGroupID = 1;//fix
                        tbl_ProductPriceGroups.ProductID = txtProductID.Text;
                        tbl_ProductPriceGroups.ProductUomID = colUnit;

                        tbl_ProductPriceGroups.SellPrice = Convert.ToDecimal(colSellPrice);

                        tbl_ProductPriceGroups.BuyPrice = Convert.ToDecimal(colBuyPrice);

                        tbl_ProductPriceGroups.SellPriceVat = Convert.ToDecimal(colSellPriceVat);

                        decimal buyprice = Convert.ToDecimal(colBuyPrice).ToDecimalN2();
                        decimal vat = (buyprice * 7) / 100;
                        decimal buypricevat = buyprice + vat.ToDecimalN2();   //warnning

                        tbl_ProductPriceGroups.BuyPriceVat = buypricevat;

                        tbl_ProductPriceGroups.ComPrice = Convert.ToDecimal(colComPrice);

                        tbl_ProductPriceGroups.CrDate = DateTime.Now;
                        tbl_ProductPriceGroups.CrUser = Helper.tbl_Users.Username;

                        tbl_ProductPriceGroups.EdDate = null;
                        tbl_ProductPriceGroups.EdUser = null;

                        tbl_ProductPriceGroups.FlagDel = false;
                        tbl_ProductPriceGroups.FlagSend = false;
                        tbl_ProductPriceGroups.FlagNew = false;
                        tbl_ProductPriceGroups.FlagEdit = false;

                        tbl_ProductPriceGroupList.Add(tbl_ProductPriceGroups);

                    } //finish check 

                } // finish loop
                if (tbl_ProductUomSetList.Count > 0)
                {
                    foreach (var item in tbl_ProductUomSetList) // insert tbl_ProductUomSet
                    {
                        ret = bu.UpdateData(item);
                    }
                }
                if (tbl_ProductPriceGroupList.Count > 0)
                {
                    foreach (var item in tbl_ProductPriceGroupList)   // insert tbl_ProductPriceGroup
                    {
                        ret = bu.UpdateData(item);
                    }
                }

                var tbl_Products = new tbl_Product();


                if (tbl_ProductList.Count > 0)
                {
                    tbl_Products = tbl_ProductList[0];

                    tbl_Products.EdDate = DateTime.Now;
                    tbl_Products.EdUser = Helper.tbl_Users.Username;
                }

                else if (tbl_ProductList.Count == 0)
                {
                    tbl_Products.ProductCode = txtProductID.Text;

                    tbl_Products.CrDate = DateTime.Now;
                    tbl_Products.CrUser = Helper.tbl_Users.Username;

                    tbl_Products.Seq = 0;
                }

                pnlEdit.Controls.SetObjectFromControl(tbl_Products);

                tbl_Products.ProductGroupID = Convert.ToInt32(ddlProGroup.SelectedValue);

                tbl_Products.ProductSubGroupID = Convert.ToInt32(ddlProSubGroup.SelectedValue);
                if (flagEdit == true)
                {
                    var prodUomSet = tbl_ProductUomSetList.FirstOrDefault(x => x.BaseQty == 1);
                    if (prodUomSet != null)
                    {
                        tbl_Products.SaleUomID = tbl_ProductUomSetList.FirstOrDefault(x => x.BaseQty == 1).UomSetID;//หน่วยขาย
                    }

                    var purchaseUom = tbl_ProductUomSetList.FirstOrDefault(x => x.BaseQty > 1);
                    if (purchaseUom != null)
                    {
                        tbl_Products.PurchaseUomID = purchaseUom.UomSetID;
                        tbl_Products.SizeUOM = purchaseUom.UomSetName;
                        tbl_Products.WeightUOM = purchaseUom.UomSetName;
                    }
                }
                else
                {
                    if (tbl_ProductUomSetList.Count > 0)
                    {
                        var saleUom = tbl_ProductUomSetList.FirstOrDefault(x => x.BaseQty == 1);

                        tbl_Products.SaleUomID = tbl_ProductUomSetList.First().UomSetID;//หน่วยขาย

                        var purchaseUom = tbl_ProductUomSetList.FirstOrDefault(x => x.BaseQty > 1);
                        if (purchaseUom != null)
                        {
                            tbl_Products.PurchaseUomID = tbl_ProductUomSetList.FirstOrDefault(x => x.BaseQty > 1).UomSetID;//ซื้อ
                            tbl_Products.SizeUOM = tbl_ProductUomSetList.FirstOrDefault(x => x.BaseQty > 1).UomSetName;
                            tbl_Products.WeightUOM = tbl_ProductUomSetList.FirstOrDefault(x => x.BaseQty > 1).UomSetName;
                        }
                        else
                        {
                            tbl_Products.PurchaseUomID = tbl_ProductUomSetList.First().UomSetID;//ซื้อ
                            tbl_Products.SizeUOM = tbl_ProductUomSetList.First().UomSetName;
                            tbl_Products.WeightUOM = tbl_ProductUomSetList.First().UomSetName;
                        }

                    }
                    else
                    {
                        tbl_Products.SaleUomID = Convert.ToInt32(ddlProUom.SelectedValue);

                        tbl_Products.PurchaseUomID = Convert.ToInt32(ddlProUom.SelectedValue);
                        tbl_Products.SizeUOM = ddlProUom.Text;
                        tbl_Products.WeightUOM = ddlProUom.Text;
                    }

                }

                tbl_Products.Barcode = txtBarCode.Text;

                tbl_Products.Flavour = ddlProFlavour.Text;

                tbl_Products.VatType = rdoVatN.Checked ? true : false;  //1 มีภาษี 0 ไม่มีภาษี

                tbl_Products.Remark = "";

                tbl_Products.StandardCost = 0;  // ???

                tbl_Products.IsFulfill = chkTablet.Checked ? true : false;

                tbl_Products.FlagDel = rdoN.Checked ? false : true;
                tbl_Products.FlagSend = false;
                tbl_Products.FlagNew = false;
                tbl_Products.FlagEdit = false;

                //string ProImg = tbl_Products.ProductImg.ToString();
                if (tbl_Products.ProductImg == null)
                {
                    tbl_Products.ProductImg = null;
                }
                
                tbl_Products.ProductBrandID = 0;
                tbl_Products.ProductLine = 0;

                ret = bu.UpdateData(tbl_Products);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    msg.ShowInfoMessage();
                    pnlGridView.Enabled = true;
                    PanelEdit(false);
                    grdPro.Enabled = true;
                    btnSearch.PerformClick();
                    chkTablet.Enabled = false;

                    ddlProUom.Enabled = false;
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void txtProductID_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtProductRefCode_KeyPress(object sender, KeyPressEventArgs e)
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
                    decimal colBuyPrice = Convert.ToDecimal(grdList2.CurrentRow.Cells["colBuyPrice"].Value).ToDecimalN2();
                    decimal colSellPrice = Convert.ToDecimal(grdList2.CurrentRow.Cells["colSellPrice"].Value).ToDecimalN2();
                    decimal colSellPriceVat = Convert.ToDecimal(grdList2.CurrentRow.Cells["colSellPriceVat"].Value).ToDecimalN2();
                    if (colBaseQty > 0 && colBuyPrice > 0 && colSellPrice > 0 && colSellPriceVat > 0)
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

        private void grdList2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            grdList2.SetUserDeletingRow(sender, e);
        }
    }
}
