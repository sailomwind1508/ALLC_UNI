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
    public partial class frmVE : Form
    {
        MenuBU menuBU = new MenuBU();
        VE bu = new VE();
        Promotion pro = new Promotion();
        PromotionTemp proTmp = new PromotionTemp();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        List<tbl_ProductUom> uoms = new List<tbl_ProductUom>();
        List<tbl_SalArea> saleAreaList = new List<tbl_SalArea>();
        List<tbl_SalAreaDistrict> salAreaDistrictList = new List<tbl_SalAreaDistrict>();
        Dictionary<string, decimal> listDiscount = new Dictionary<string, decimal>();

        bool validateNewRow = true;
        string docTypeCode = "";
        int runDigit = 0;

        List<Control> searchCustControls = new List<Control>();
        List<Control> searchPromotionControls = new List<Control>();
        List<Control> searchBWHControls = new List<Control>();
        List<Control> searchEmpControls = new List<Control>();
        List<string> readOnlyControls = new List<string>();

        int[] cellEdit = new int[] { 0, 3, 4 };
        int[] numberCell = new int[] { 4 };

        Dictionary<int, string> dataGridList = new Dictionary<int, string>();
        Dictionary<int, string> initDataGridList = new Dictionary<int, string>();

        Func<tbl_BranchWarehouse, bool> whFunc = (x => x.VanType != 0); // x.WHID.Contains("V"));
        Func<tbl_Employee, bool> empFunc = (x => x.PositionID == 4);

        List<tbl_Product> allProduct = new List<tbl_Product>();
        List<tbl_ProductUomSet> allUomSet = new List<tbl_ProductUomSet>();
        List<tbl_ProductUom> allUOM = new List<tbl_ProductUom>();
        List<tbl_ProductPriceGroup> allProductPrice = new List<tbl_ProductPriceGroup>();
        List<tbl_ProductGroup> allProdGroup = new List<tbl_ProductGroup>();
        List<tbl_ProductSubGroup> allProdSubGroup = new List<tbl_ProductSubGroup>();

        private ContextMenuStrip printContextMenuStrip;

        public frmVE()
        {
            InitializeComponent();

            searchCustControls = new List<Control> { txtCustomerCode, txtCustName, txtBillTo, txtContact, txtTelephone };
            searchBWHControls = new List<Control> { txtWHCode, txtWHName };
            searchEmpControls = new List<Control> { txtEmpCode };
            readOnlyControls = new List<string>() { txtCustName.Name, txtWHName.Name, txtEmpCode.Name, txtCrUser.Name };

            dataGridList = new Dictionary<int, string>() { { 0, "ProductID" }, { 2, "ProductName" }, { 3, "UOMSetID" }, { 5, "SellPriceVat" }, { 6, "VatType" }, { 11, "SellPrice" } };
            initDataGridList = new Dictionary<int, string>() { { 0, "" }, { 2, "" }, { 3, "combobox" }, { 4, "0" }, { 5, "0.00" }, { 6, "0" }, { 7, "N" }, { 8, "0.00" }, { 9, "0.00" }, { 10, "" }, { 11, "0.00" } };

            txdDocNo.KeyDown += TxdDocNo_KeyDown;
            txtWHCode.KeyDown += TxtWHCode_KeyDown;
            txtCustomerCode.KeyDown += TxtCustomerCode_KeyDown;

            dtpDocDate.ValueChanged += DtpDocDate_ValueChanged;
            nudCreditDay.ValueChanged += NudCreditDay_ValueChanged;
        }

        #region private methods

        private void InitPage()
        {
            var menu = bu.GetAllFromMenu().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }

            var documentType = bu.GetDocumentType().FirstOrDefault(x => x.DocTypeCode.Trim() == "V");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                runDigit = documentType.DocFormat.Length - documentType.RunLength.Value;

                this.ClearControl(bu, docTypeCode, runDigit);
                txtComment.Text = documentType.DocRemark;
            }

            validateNewRow = true;
            btnAdd.Enabled = true;

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnAdd.Enabled = false;

            var headerPic = menuBU.GetAllData().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            //header control
            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;
            btnSearchDoc.Enabled = true;

            dtpDocDate.SetDateTimePickerFormat();
            dtpDueDate.SetDateTimePickerFormat();

            allUOM = bu.GetUOM();

            uoms.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
            uoms.AddRange(allUOM);

            saleAreaList.AddRange(bu.GetAllSaleArea());
            salAreaDistrictList.AddRange(bu.GetAllSaleAreaDistrict());

            //data gridview
            grdList.SetDataGridViewStyle();
            SetDefaultGridViewEvent(grdList);

            CreatePrintBtnList();

            allProduct = bu.GetProduct();
            //allUomSet = bu.GetUOMSet();

            allProductPrice = bu.GetProductPriceGroup();
            //allProdGroup = bu.GetProductGroup();
            //allProdSubGroup = bu.GetProductSubGroup();
        }

        private void CreatePrintBtnList()
        {
            printContextMenuStrip = new ContextMenuStrip();

            printContextMenuStrip.Items.Clear();
            printContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(cms_Opening);

            btnPrint.ContextMenuStrip = printContextMenuStrip;
        }

        void cms_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Acquire references to the owning control and item.
            Control c = printContextMenuStrip.SourceControl as Control;
            ToolStripDropDownItem tsi = printContextMenuStrip.OwnerItem as ToolStripDropDownItem;

            // Clear the ContextMenuStrip control's Items collection.
            printContextMenuStrip.Items.Clear();

            // Populate the ContextMenuStrip control with its default items.
            var printImage = new Bitmap(AllCashUFormsApp.Properties.Resources.printBtn).ImageToByte();

            List<tbl_MstMenu> menuList = new List<tbl_MstMenu>();
            tbl_MstMenu m = new tbl_MstMenu();
            m.MenuID = 100;
            m.MenuName = "VAll";
            m.MenuText = "พิมพ์ทั้งหมด";
            m.FormName = "frmCrystalReport";
            m.MenuImage = printImage;
            menuList.Add(m);

            m = new tbl_MstMenu();
            m.MenuID = 101;
            m.MenuName = "VOriginal";
            m.MenuText = "พิมพ์ต้นฉบับ";
            m.FormName = "frmCrystalReport";
            m.MenuImage = printImage;
            menuList.Add(m);

            m = new tbl_MstMenu();
            m.MenuID = 102;
            m.MenuName = "VCopy";
            m.MenuText = "พิมพ์สำเนา";
            m.FormName = "frmCrystalReport";
            m.MenuImage = printImage;
            menuList.Add(m);

            foreach (var item in menuList)
            {
                printContextMenuStrip.Items.Add(item.MenuText, item.MenuImage.byteArrayToImage(), ToolStripMenuItem_Click);                
            }

            // Set Cancel to false. 
            // It is optimized to true based on empty entry.
            e.Cancel = false;
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@DocNo", txdDocNo.Text);

            string frmText = ((System.Windows.Forms.ToolStripItem)sender).Text;
            if (frmText == "พิมพ์ทั้งหมด")
            {
                this.OpenCrystalReportsPopup("ใบกำกับภาษีเต็มรูป/ใบเสร็จรับเงิน(ต้นฉบับ)", "Form_V.rpt", "Form_IV", _params);
                this.OpenCrystalReportsPopup("ใบกำกับภาษีเต็มรูป/ใบเสร็จรับเงิน(สำเนา)", "Form_V_Copy.rpt", "Form_IV", _params);
            }
            else if (frmText == "พิมพ์ต้นฉบับ")
            {
                this.OpenCrystalReportsPopup("ใบกำกับภาษีเต็มรูป/ใบเสร็จรับเงิน(ต้นฉบับ)", "Form_V.rpt", "Form_IV", _params);
            }
            else if (frmText == "พิมพ์สำเนา")
            {
                this.OpenCrystalReportsPopup("ใบกำกับภาษีเต็มรูป/ใบเสร็จรับเงิน(สำเนา)", "Form_V_Copy.rpt", "Form_IV", _params);
            }
            
        }

        public void BindVEData(string ivDocNo)
        {
            bu.GetDocData(ivDocNo, docTypeCode);

            var iv = bu.tbl_IVMaster;
            var ivDts = bu.tbl_IVDetails;

            if (string.IsNullOrEmpty(iv.DocNo))
            {
                string msg = "ไม่พบข้อมูลเลขที่เอกสาร!!!";
                msg.ShowWarningMessage();

                btnCancel.PerformClick();

                return;
            }

            if (iv != null)
            {
                BindIVMaster(iv);
            }
            if (ivDts != null && ivDts.Count > 0)
            {
                BindIVDetail(ivDts);
            }

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            btnSearchDoc.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
            //btnPrint.Enabled = true;

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;
            //btnReCalc.Enabled = true;

            grdList.CellContentClick -= grdList_CellContentClick;


            btnUpdateAddress.Enabled = true;
            btnCopy.Enabled = false;
            //btnGenCustIVNo.Enabled = true;
        }

        private void BindIVMaster(tbl_IVMaster obj)
        {
            var allEmp = bu.GetEmployee();
            var allBranchWH = bu.GetAllBranchWarehouse();

            txdDocNo.Text = obj.DocNo;
            txtCustPONo.Text = obj.DocRef;
            dtpDocDate.Value = obj.DocDate.ToDateTimeFormat();

            txtCustomerCode.Text = obj.CustomerID;
            txtCustName.Text = obj.CustName;
            txtContact.Text = obj.ContactName;
            txtTelephone.Text = obj.ContactTel;
            txtBillTo.Text = obj.Address;
            //txtCustobjNo.Text = obj.CustobjNo;
            txtCustInvNO.Text = obj.CustInvNO;
            txtRemark.Text = obj.Remark;

            var employee = allEmp.FirstOrDefault(x => x.EmpID == Helper.tbl_Users.EmpID);
            if (employee != null)
                txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName);

            var emp = allEmp.FirstOrDefault(x => x.EmpID == obj.SaleEmpID);
            if (emp != null)
                txtEmpCode.Text = emp.TitleName + " " + emp.FirstName;

            txtWHCode.Text = obj.WHID;
            Func<tbl_BranchWarehouse, bool> func = (x => x.WHID == obj.WHID);
            var wh = allBranchWH.FirstOrDefault(func);//bu.GetBranchWarehouse(func);
            if (wh != null)
            {
                txtWHName.Text = wh.WHName;
            }

            FilterSaleArea(obj.CustomerID);
            ddlDocStatus.BindDropdownDocStatus(bu, obj.DocStatus);

            txtComment.Text = obj.Comment;

            txnAmount.Text = obj.IncVat.Value.ToStringN2(); //obj.Amount.Value.ToStringN2();
            txnDiscountAmt.Text = obj.Discount.Value.ToStringN2();

            //decimal amt = 0;
            //decimal excVat = 0;
            //decimal discount = 0;
            //decimal vatRate = 0;
            //if (obj.Amount != null)
            //    amt = obj.Amount.Value;
            //if (obj.ExcVat != null)
            //    excVat = obj.ExcVat.Value;
            //if (obj.Discount != null)
            //    discount = obj.Discount.Value;
            //if (obj.VatRate != null)
            //    vatRate = obj.VatRate.Value;

            //decimal incVat = ((amt - excVat - discount) * 100) / (100 + vatRate);

            txnBeforeVat.Text = (obj.IncVat.Value - obj.Discount.Value - obj.VatAmt.Value - obj.ExcVat.Value).ToStringN2();//(obj.IncVat.Value - obj.VatAmt.Value).ToStringN2();
            txnVatAmt.Text = obj.VatAmt.Value.ToStringN2();
            lblVatType.Text = obj.VatRate != null ? obj.VatRate.Value.ToStringN0() : ""; //obj.VatRate.Value.ToStringN0();
            txnExcVat.Text = obj.ExcVat.Value.ToStringN2();
            txnTotalDue.Text = obj.TotalDue.ToStringN2();
        }

        private void BindIVDetail(List<tbl_IVDetail> obj)
        {
            grdList.Rows.Clear();

            //var allUOM = bu.GetUOM();
            //var allProduct = bu.GetProduct();
            //var allPrdPriceList = bu.GetProductPriceGroup();
            //var allUOMSet = bu.GetUOMSet();
            var allPOMaster = bu.GetPOMasterByCustInvNo(bu.tbl_IVMaster.DocNo);

            var allPODetails = new List<tbl_PODetail>();
            foreach (var item in allPOMaster)
            {
                allPODetails.AddRange(bu.GetPODetails(item.DocNo));
            }
 
            for (int i = 0; i < obj.Count; i++)
            {
                grdList.Rows.Add(1);

                grdList.Rows[i].Cells[0].Value = obj[i].ProductID;

                string productName = string.Empty;
                if (!string.IsNullOrEmpty(obj[i].ProductName))
                {
                    productName = obj[i].ProductName;
                }
                else
                {
                    var data = allProduct.FirstOrDefault(x => x.ProductID == obj[i].ProductID);
                    if (data != null)
                    {
                        productName = data.ProductName;
                    }
                }

                grdList.Rows[i].Cells[2].Value = productName;
                grdList.BindComboBoxCell(allProduct, grdList.Rows[i], i, false, 3, uoms, this, bu, 0);

                decimal? orderQty = obj[i].OrderQty;
                int orderUom = obj[i].OrderUom;
                decimal? unitPrice = obj[i].UnitPrice;

                var poMst = allPOMaster.FirstOrDefault(x => x.CustInvNO == txdDocNo.Text);
                if (poMst != null)
                {
                    var poDT = allPODetails.FirstOrDefault(x => x.DocNo == poMst.DocNo && x.ProductID == obj[i].ProductID);
                    if (poDT != null)
                    {
                        if (obj[i].OrderUom != poDT.OrderUom)
                        {
                            orderUom = poDT.OrderUom;
                            orderQty = poDT.OrderQty;
                        }
                    }
                }

                var _prdPriceList = allProductPrice.FirstOrDefault(x => x.ProductUomID == orderUom && x.ProductID == obj[i].ProductID);
                if (_prdPriceList != null)
                {
                    unitPrice = _prdPriceList.SellPriceVat;
                }

                grdList.Rows[i].Cells[3].Value = orderUom;
                grdList.Rows[i].Cells[4].Value = orderQty;
                grdList.Rows[i].Cells[5].Value = unitPrice; // obj[i].UnitPrice;
                grdList.Rows[i].Cells[6].Value = obj[i].UnitComPrice;
                grdList.Rows[i].Cells[7].Value = obj[i].VatType;
                grdList.Rows[i].Cells[8].Value = obj[i].LineDiscountType;
                grdList.Rows[i].Cells[9].Value = obj[i].LineDiscount;
                grdList.Rows[i].Cells[10].Value = obj[i].LineTotal;
                grdList.Rows[i].Cells[11].Value = obj[i].LineComTotal;
                grdList.Rows[i].Cells[12].Value = orderUom; // obj[i].OrderUom;
                grdList.Rows[i].Cells[11].Value = 0.00;

                //if (orderUom != -1)
                //{
                //    string prdCode = obj[i].ProductID;
                //    //Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == obj[i].OrderUom && x.ProductID == prdCode);
                //    var prdPriceList = allPrdPriceList.Where(x => x.ProductUomID == orderUom && x.ProductID == prdCode).ToList(); //bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);

                //    if (prdPriceList != null && prdPriceList.Count > 0)
                //        grdList.Rows[i].Cells[11].Value = 0.00; // prdPriceList[0].SellPrice.Value;
                //}
            }
        }

        public void BindSearchProduct(DataTable productDT, int rowIndex)
        {
            validateNewRow = true;
            grdList.ValidateDuplicateSKU(productDT.Rows[0]["ProductID"].ToString(), 0, rowIndex, ref validateNewRow);


            grdList.BindDataGrid(allProduct, dataGridList, initDataGridList, productDT, 0, rowIndex, validateNewRow, this, uoms, bu, 0);
        }

        private void ClearForm()
        {
            this.ClearControl(bu, docTypeCode, runDigit);
            btnAdd.Enabled = true;

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");

            validateNewRow = true;

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            btnCancel.EnableButton(btnSearchDoc);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;

            grdList.CellContentClick -= grdList_CellContentClick;
        }

        private void CalcPromotion(bool isShowPopup = false)
        {

            PreparePOMaster(false);
            PreparePODetail(false, false);

            var proList = pro.CalculatePromotion(bu.tbl_PODetails);
            if (proList != null && proList.Count > 0)
            {
                var delFlag = pro.RemoveTempData();
                if (delFlag == 1)
                {
                    PreparePromotionTemp(proList);
                    var addFlag = pro.AddTempData();

                    var po = bu.tbl_POMaster;
                    po.Discount = pro.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt);
                    txnDiscountAmt.Text = po.Discount.Value.ToStringN2();

                    //decimal reCaclBeforeVat = Convert.ToDecimal(txnBeforeVat.Text) - po.Discount.Value;
                    //txnBeforeVat.Text = reCaclBeforeVat.ToStringN2();
                    if (isShowPopup && addFlag == 1)
                    {
                        if (pro.tbl_HQ_Promotion_Hit_Temps != null && pro.tbl_HQ_Promotion_Hit_Temps.Count > 0)
                        {
                            this.OpenPromotionTempPopup(searchPromotionControls, "โปรโมชั่น");
                        }
                    }
                }
            }

            CalculateTotal();
        }

        private void CalculateRow(DataGridView grd, int rowIndex)
        {
            decimal orderQty = 0;
            decimal unitPrice = 0;
            decimal discount = 0;
            int orderUom = -1;
            string lineDiscountType = "";

            var cell0 = grd.Rows[rowIndex].Cells[0];
            var cell3 = grd.Rows[rowIndex].Cells[3];
            var cell4 = grd.Rows[rowIndex].Cells[4];
            var cell5 = grd.Rows[rowIndex].Cells[5];
            var cell8 = grd.Rows[rowIndex].Cells[8];
            var cell9 = grd.Rows[rowIndex].Cells[9];

            if (cell3.IsNotNullOrEmptyCell())
            {
                //var allPrdUOM = bu.GetUOM();
                var prdUOM = allUOM.FirstOrDefault(x => x.ProductUomName == cell3.EditedFormattedValue.ToString());
                if (prdUOM != null)
                {
                    orderUom = prdUOM.ProductUomID;
                    if (orderUom != -1)
                    {
                        string prdCode = cell0.EditedFormattedValue.ToString();
                        Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == orderUom && x.ProductID == prdCode);
                        var prdPriceList = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList(); //bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);

                        if (prdPriceList != null && prdPriceList.Count > 0)
                            cell5.Value = prdPriceList[0].SellPriceVat.Value;
                    }
                    else
                        cell5.Value = 0;
                }
                else
                    cell5.Value = 0;
            }
            if (cell4.IsNotNullOrEmptyCell())
                orderQty = Convert.ToDecimal(cell4.EditedFormattedValue);
            if (cell5.IsNotNullOrEmptyCell())
                unitPrice = Convert.ToDecimal(cell5.EditedFormattedValue);
            if (cell9.IsNotNullOrEmptyCell())
            {
                if (cell8.IsNotNullOrEmptyCell())
                {
                    discount = bu.CalDiscountType(cell8.EditedFormattedValue.ToString(), cell9.EditedFormattedValue.ToString(), orderQty, unitPrice);

                    //var allLineDiscountType = bu.GetDiscountType();
                    //var ldt = allLineDiscountType.FirstOrDefault(x => x.DiscountTypeName == cell8.EditedFormattedValue.ToString());
                    //if (ldt != null)
                    //{
                    //    lineDiscountType = ldt.DiscountTypeCode;
                    //    var cell9Value = Convert.ToDecimal(cell9.EditedFormattedValue);

                    //    switch (lineDiscountType)
                    //    {
                    //        case "N": { discount = 0; } break;
                    //        case "A": { discount = cell9Value; } break;
                    //        case "P":
                    //            {
                    //                decimal total = (orderQty * unitPrice).ToDecimalN2();
                    //                discount = cell9Value;
                    //                decimal discountAmt = (total * discount) / 100;

                    //                discount = discountAmt;
                    //            }
                    //            break;
                    //        case "Q": { discount = cell9Value * orderQty; } break;
                    //        case "F": { discount = (cell9Value * unitPrice).ToDecimalN2(); } break;
                    //        default:
                    //            break;
                    //    }
                    //}
                    //else
                    //    discount = 0;
                }
                else
                    discount = 0;

                grd.Rows[rowIndex].Cells[10].Value = ((orderQty * unitPrice) - discount).ToDecimalN2().ToStringN2();
            }

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            decimal amount = 0;
            decimal excVat = 0;
            decimal incVat = 0;
            decimal vatAmt = 0;
            decimal totalDue = 0;
            decimal vatRate = 0;
            string discountType = "";
            decimal totalDiscountAmt = 0;

            //var allPrdUOM = bu.GetUOM();
            //var allPrdPriceList = bu.GetProductPriceGroup();

            decimal totalDiscount = 0;
            if (!string.IsNullOrEmpty(txnDiscountAmt.Text))
                totalDiscount = Convert.ToDecimal(txnDiscountAmt.Text).ToDecimalN2();

            //Dictionary<string, decimal> listVAT0List = new Dictionary<string, decimal>();

            for (int i = 0; i < grdList.RowCount; i++)
            {
                var cell0 = grdList.Rows[i].Cells[0];
                var cell3 = grdList.Rows[i].Cells[3];
                var cell4 = grdList.Rows[i].Cells[4];
                var vatCell = grdList.Rows[i].Cells[7];
                var lineTotalCell = grdList.Rows[i].Cells[9];
                var discountTypeCell = grdList.Rows[i].Cells[8];
                var discountAmtCell = grdList.Rows[i].Cells[9];
                var sellPriceCell = grdList.Rows[i].Cells[13];
                var _qty = Convert.ToDecimal(cell4.EditedFormattedValue);
                string prdCode = cell0.EditedFormattedValue.ToString();

                if (!string.IsNullOrEmpty(prdCode))
                {
                    if (totalDiscount > 0)
                    {
                        decimal discountPerSku = totalDiscount / _qty;
                        if (listDiscount.Count(x => x.Key == prdCode) == 0)
                            listDiscount.Add(prdCode, discountPerSku);
                    }

                    if (lineTotalCell.IsNotNullOrEmptyCell())
                    {
                        amount += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                    }
                    if (vatCell.IsNotNullOrEmptyCell())
                    {
                        decimal _vateRate = Convert.ToDecimal(vatCell.EditedFormattedValue);
                        if (_vateRate > 0) //have VAT
                        {
                            //incVat += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                            vatRate = _vateRate;
                        }
                        else //No VAT
                        {
                            excVat += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                        }
                    }
                    if (discountTypeCell.IsNotNullOrEmptyCell())
                    {
                        if (discountTypeCell.EditedFormattedValue.ToString() != "ไม่มี")
                        {
                            discountType += discountTypeCell.EditedFormattedValue.ToString() + ", ";
                        }
                    }
                    if (discountAmtCell.IsNotNullOrEmptyCell())
                    {
                        totalDiscountAmt += Convert.ToDecimal(discountAmtCell.EditedFormattedValue);
                    }
                }
            }

            excVat = excVat.ToDecimalN2();
            totalDue = amount - totalDiscount;

            incVat = ((amount - excVat - totalDiscount) * 100) / (100 + vatRate);
            vatAmt = incVat * (vatRate / 100).ToDecimalN2();

            lblVatType.Text = vatRate.ToDecimalN0().ToString();
            txnAmount.Text = amount.ToDecimalN2().ToStringN2();
            txnBeforeVat.Text = incVat.ToStringN2();
            txnVatAmt.Text = vatAmt.ToStringN2();
            txnExcVat.Text = excVat.ToStringN2();
            txnTotalDue.Text = totalDue.ToStringN2();
        }

        private void FilterSaleArea(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Func<tbl_ArCustomer, bool> func = (x => x.CustomerID == text.Trim());
                var cust = bu.GetCustomer(func);
                if (cust != null && cust.Count > 0)
                {
                    var _saleAreaList = salAreaDistrictList.Where(x => x.WHID == cust[0].WHID);
                    var listOfSalAreaID = _saleAreaList.Select(a => a.SalAreaID).ToList();
                    saleAreaList = bu.GetAllSaleArea().Where(x => listOfSalAreaID.Contains(x.SalAreaID)).ToList();

                    Predicate<tbl_SalArea> predicate = delegate (tbl_SalArea p) { return p.SalAreaID == cust[0].SalAreaID; };
                    ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID", null, predicate);

                    var emp = bu.GetEmployee(cust[0].EmpID);
                    if (emp != null)
                        txtEmpCode.Text = emp.TitleName + " " + emp.FirstName;

                    Func<tbl_BranchWarehouse, bool> whFunc = (x => x.WHID == cust[0].WHID);
                    var wh = bu.GetBranchWarehouse(whFunc);
                    if (wh != null)
                    {
                        txtWHCode.Text = wh.WHCode;
                        txtWHName.Text = wh.WHName;
                    }
                }
            }
        }

        private void InitHeader()
        {
            dtpDocDate.SetDateTimePickerFormat();
            dtpDueDate.SetDateTimePickerFormat();

            ddlDocStatus.BindDropdownDocStatus(bu);

            ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID", null, null);
            ddlSaleArea.Enabled = true;
        }

        private void InitialData()
        {
            this.ClearControl(bu, docTypeCode, runDigit);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            InitHeader();

            grdList.AutoGenerateColumns = false;
            validateNewRow = true;

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;

            grdList.Rows.Clear();
            //AddNewRow(grdList, 0);
            grdList.AddNewRow(allProduct, initDataGridList, 0, "", 0, validateNewRow, uoms, bu, this, 0);

            var employee = bu.GetEmployee(Helper.tbl_Users.EmpID);
            txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName);

            //txtCustomerCode.Focus();
        }

        private void PreparePOMaster(bool editFlag = false)
        {
            bu.tbl_POMaster = new tbl_POMaster();

            var comp = bu.GetCompany();
            var emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            var supp = bu.GetSupplier(txtCustomerCode.Text);

            Dictionary<string, string> allEmp = new Dictionary<string, string>();
            KeyValuePair<string, string> selEmp = new KeyValuePair<string, string>();

            bu.GetEmployee().ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty))));
            selEmp = allEmp.FirstOrDefault(x => x.Value == txtEmpCode.Text.Replace(" ", string.Empty));
            var vanWH = bu.GetAllBranchWarehouse().FirstOrDefault(x => x.SaleEmpID == selEmp.Key);

            var po = bu.tbl_POMaster;
            bool checkEditMode = bu.CheckExistsPO(txdDocNo.Text);

            if (checkEditMode)
                po.DocNo = txdDocNo.Text;
            else
                po.DocNo = bu.GenDocNo(docTypeCode, txtWHCode.Text);

            po.RevisionNo = 0;
            po.DocTypeCode = "IV";
            po.DocStatus = ddlDocStatus.SelectedValue.ToString();
            po.DocDate = dtpDocDate.Value.ToDateTimeFormat();
            po.DocRef = docTypeCode;
            po.StatusInOut = null;
            po.SupplierID = "0";
            po.SuppName = "";

            if (vanWH != null)
                po.WHID = vanWH.WHID;

            if (emp != null)
                po.EmpID = emp.EmpID;

            if (!string.IsNullOrEmpty(selEmp.Key))
                po.SaleEmpID = selEmp.Key;

            po.SalAreaID = ddlSaleArea.SelectedValue.ToString();
            po.Address = txtBillTo.Text;
            po.ContactName = txtContact.Text;
            po.ContactTel = txtTelephone.Text;
            po.Shipto = txtBillTo.Text;

            po.CreditDay = Convert.ToInt16(nudCreditDay.Value);
            Func<tbl_ArCustomer, bool> func = (x => x.CustomerCode == txtCustomerCode.Text);
            var cust = bu.GetCustomer(func);
            if (cust != null && cust.Count > 0)
            {
                po.CreditDay = cust[0].CreditDay;
                po.CustType = cust[0].CustomerTypeID.Value.ToString();
            }
            po.DueDate = dtpDocDate.Value.AddDays(po.CreditDay.Value);
            po.CustomerID = txtCustomerCode.Text;
            po.CustName = txtCustName.Text;
            po.CustInvNO = txtCustInvNO.Text;
            po.CustInvDate = null;
            po.CustPODate = dtpDocDate.Value;
            po.CustPONo = txtCustPONo.Text;
            po.Remark = "";
            po.Comment = txtComment.Text;
            po.OldAmount = Convert.ToDecimal(txnAmount.Text);
            po.Amount = Convert.ToDecimal(txnAmount.Text);
            po.OldExcVat = Convert.ToDecimal(txnExcVat.Text);
            po.ExcVat = Convert.ToDecimal(txnExcVat.Text);

            var incVat = Convert.ToDecimal(txnBeforeVat.Text) + Convert.ToDecimal(txnVatAmt.Text);
            po.OldIncVat = incVat;
            po.IncVat = incVat;

            decimal _vatRate = 0;
            for (int i = 0; i < grdList.RowCount; i++)
            {
                if (Convert.ToDecimal(grdList.Rows[i].Cells[6].EditedFormattedValue.ToString()) > 0)
                {
                    _vatRate = Convert.ToDecimal(grdList.Rows[i].Cells[6].EditedFormattedValue);
                    break;
                }
            }

            po.VatRate = _vatRate;

            po.VatAmt = Convert.ToDecimal(txnVatAmt.Text);
            po.Freight = 0.00m;
            po.DiscountType = "";
            po.OldDiscount = null;
            po.Discount = 0.00m;
            po.TotalDue = Convert.ToDecimal(txnTotalDue.Text);
            po.ApprovedBy = null;
            po.ApprovedDate = null;
            po.PayType = 0;
            po.CrDate = DateTime.Now;
            po.CrUser = Helper.tbl_Users.Username;

            if (editFlag)
            {
                po.EdDate = DateTime.Now;
                po.EdUser = Helper.tbl_Users.Username;
            }

            po.FlagDel = false;
            po.FlagSend = false;
            po.OldTotalCom = 0.00m;
            po.TotalCom = 0.00m;
            po.CNType = 0;
            po.DiscountRate = 0.00m;
        }

        private void PreparePODetail(bool isSave, bool editFlag = false)
        {
            bu.tbl_PODetails.Clear();

            var poDts = bu.tbl_PODetails;
            DateTime crDate = DateTime.Now;

            for (int i = 0; i < grdList.RowCount; i++)
            {
                var poDt = new tbl_PODetail();

                var prdCodeCell = grdList.Rows[i].Cells[0];
                var prdNameCell = grdList.Rows[i].Cells[2];
                var uomTypeCell = grdList.Rows[i].Cells[3];
                var orderQtyCell = grdList.Rows[i].Cells[4];
                var priceCell = grdList.Rows[i].Cells[5];
                var vatCell = grdList.Rows[i].Cells[6];
                var discountTypeCell = grdList.Rows[i].Cells[7];
                var discountAmtCell = grdList.Rows[i].Cells[8];
                var lineAmt = grdList.Rows[i].Cells[9];

                if (prdCodeCell.IsNotNullOrEmptyCell() && prdNameCell.IsNotNullOrEmptyCell())
                {
                    int lineNo = poDts.Count > 0 ? Convert.ToInt32(poDts.Max(x => x.Line)) + 1 : 0;

                    poDt.DocNo = bu.tbl_POMaster.DocNo;
                    poDt.Line = Convert.ToInt16(lineNo);
                    poDt.ProductID = prdCodeCell.EditedFormattedValue.ToString();
                    poDt.ProductName = prdNameCell.EditedFormattedValue.ToString();

                    var uom = allUOM.FirstOrDefault(x => x.ProductUomName == uomTypeCell.EditedFormattedValue.ToString());
                    if (uom != null)
                    {
                        poDt.OrderUom = Convert.ToInt32(uom.ProductUomID);
                    }

                    poDt.OrderQty = Convert.ToDecimal(orderQtyCell.EditedFormattedValue);
                    poDt.ReceivedQty = Convert.ToDecimal(orderQtyCell.EditedFormattedValue); ;
                    poDt.RejectedQty = 0;
                    poDt.StockedQty = 0;

                    decimal unitCost = 0;
                    Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == poDt.OrderUom && x.ProductID == poDt.ProductID);
                    var prdPriceGroup = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList(); //bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);
                    if (prdPriceGroup != null && prdPriceGroup.Count > 0)
                        unitCost = prdPriceGroup[0].BuyPrice.Value;

                    poDt.LineTotal = Convert.ToDecimal(lineAmt.EditedFormattedValue);
                    poDt.UnitCost = unitCost.ToDecimalN5();
                    poDt.UnitPrice = Convert.ToDecimal(priceCell.EditedFormattedValue);
                    poDt.VatType = Convert.ToByte(vatCell.EditedFormattedValue);

                    //var allLineDiscountType = bu.GetDiscountType();
                    //var ldt = allLineDiscountType.FirstOrDefault(x => x.DiscountTypeName == discountTypeCell.EditedFormattedValue.ToString());
                    //if (ldt != null)
                    //{
                    //    poDt.LineDiscountType = ldt.DiscountTypeCode;
                    //    poDt.LineDiscountRate = 0;
                    //    poDt.LineDiscount = Convert.ToDecimal(discountAmtCell.EditedFormattedValue);
                    //}

                    poDt.CustType = "";
                    poDt.CrDate = crDate;
                    poDt.CrUser = Helper.tbl_Users.Username;

                    if (editFlag)
                    {
                        poDt.EdDate = crDate;
                        poDt.EdUser = Helper.tbl_Users.Username;
                    }

                    poDt.FlagDel = false;
                    poDt.FlagSend = false;
                    poDt.UnitComPrice = 0;
                    poDt.LineComTotal = 0;
                    poDt.LineRemark = "";
                    poDt.FreeQty = 0;
                    poDt.FreeUom = 0;
                    poDt.FreeUnit = 0;
                    poDt.LineDiscountType = "N";

                    if (isSave)
                        DistributeDiscountPromotion(poDt);

                    poDts.Add(poDt);

                    if (isSave)
                        DistributeFreePromotion(poDts, poDt);
                }
            }
        }

        private void DistributeFreePromotion(List<tbl_PODetail> poDts, tbl_PODetail poDt)
        {
            if (pro.tbl_HQ_Promotion_Hits != null && pro.tbl_HQ_Promotion_Hits.Count > 0)
            {
                List<tbl_HQ_Promotion_Hit> proHits = new List<tbl_HQ_Promotion_Hit>();
                proHits = pro.tbl_HQ_Promotion_Hits.Where(x => x.DocNo == poDt.DocNo && !string.IsNullOrEmpty(x.SKUGroupRewardID)).ToList();

                if (proHits != null && proHits.Count > 0)
                {
                    foreach (tbl_HQ_Promotion_Hit item in proHits)
                    {
                        var itemGroup = pro.GetHQSKUGroup(x => x.SKU_ID == poDt.ProductID);
                        if (itemGroup.Count > 0 && itemGroup.Any(x => x.SKUGroupID == item.SKUGroupRewardID))
                        {
                            var _poDt = new tbl_PODetail();
                            poDt.CopyTo(_poDt);
                            if (_poDt != null)
                            {
                                int _lineNo = poDts.Count > 0 ? Convert.ToInt32(poDts.Max(x => x.Line)) + 1 : 0;
                                _poDt.OrderUom = 2;
                                _poDt.OrderQty = item.SKUGroupRewardAmt;
                                _poDt.ReceivedQty = item.SKUGroupRewardAmt;
                                _poDt.VatType = 0;
                                _poDt.Line = Convert.ToInt16(_lineNo);
                                _poDt.LineTotal = 0;
                                _poDt.LineDiscount = _poDt.UnitComPrice;
                                _poDt.LineDiscountRate = 0;
                                _poDt.LineDiscountType = "F";
                                _poDt.LineRemark = pro.GetHQReward(x => x.RewardID == item.RewardID)[0].RewardName;
                                bu.tbl_PODetails.Add(_poDt);
                            }
                        }
                    }
                }
            }
        }

        private void DistributeDiscountPromotion(tbl_PODetail poDt)
        {
            if (pro.tbl_HQ_Promotion_Hits != null && pro.tbl_HQ_Promotion_Hits.Count > 0)
            {
                decimal _lineDiscount = 0;
                List<string> _lineRemarkList = new List<string>();
                List<tbl_HQ_Promotion_Hit> proHits = new List<tbl_HQ_Promotion_Hit>();
                proHits = pro.tbl_HQ_Promotion_Hits.Where(x => x.DocNo == poDt.DocNo && string.IsNullOrEmpty(x.SKUGroupRewardID)).ToList();

                if (proHits != null && proHits.Count > 0)
                {
                    foreach (tbl_HQ_Promotion_Hit item in proHits)
                    {
                        var promotions = pro.GetHQPromotion(x => x.PromotionID == item.PromotionID);

                        if (promotions != null && promotions.Count > 0)
                        {
                            var prdProItems = promotions.Where(x => x.PromotionPattern == "prd").ToList();
                            var txnProItems = promotions.Where(x => x.PromotionPattern == "txn").ToList();

                            if (prdProItems.Count > 0)
                            {
                                foreach (tbl_HQ_Promotion prdItem in prdProItems)
                                {
                                    var skuGroups = pro.GetHQSKUGroup(x => x.SKU_ID == poDt.ProductID);

                                    if (skuGroups != null && skuGroups.Count > 0)
                                    {
                                        var listGroup = skuGroups.Select(x => x.SKUGroupID).ToList();
                                        if (listGroup.Contains(prdItem.SKUGroupID1))
                                        {
                                            _lineDiscount += item.DisCountAmt.Value;

                                            if (_lineRemarkList.All(x => x != prdItem.RewardID))
                                                _lineRemarkList.Add(prdItem.RewardID);
                                        }
                                    }

                                }
                            }
                            if (txnProItems.Count > 0)
                            {
                                _lineDiscount += item.DisCountAmt.Value;

                                foreach (tbl_HQ_Promotion txnItem in txnProItems)
                                {
                                    if (_lineRemarkList.All(x => x != txnItem.RewardID))
                                        _lineRemarkList.Add(txnItem.RewardID);
                                }
                            }
                        }
                    }

                    if (_lineDiscount > 0 && _lineRemarkList.Count > 0)
                    {
                        poDt.LineDiscount = _lineDiscount / poDt.OrderQty.Value;
                        poDt.LineRemark = string.Join(",", _lineRemarkList.Distinct().ToList());
                        if (poDt.LineDiscount > 0)
                        {
                            poDt.LineDiscountType = "A";
                            poDt.LineDiscountRate = 0;
                        }
                    }
                }
            }
        }

        private void PreparePromotionTemp(List<PromotionRuleModel> proList)
        {
            pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();

            var _pro = new List<tbl_HQ_Promotion_Hit_Temp>();
            DateTime crDate = DateTime.Now;

            foreach (var pItem in proList)
            {
                var p = new tbl_HQ_Promotion_Hit_Temp();

                p.DocNo = bu.tbl_IVMaster.DocNo;
                p.PromotionID = pItem.PromotionID;
                p.RoundHit = pItem.RoundHit;
                p.SKUGroupID = pItem.ProductGroupID;
                p.DisCountAmt = pItem.DisCountAmt;
                p.SKUGroupRewardID = pItem.PruductGroupRewardID;
                p.SKUGroupRewardAmt = pItem.PruductGroupRewardAmt;
                p.RewardID = pItem.RewardID;
                p.CrDate = crDate;
                p.CrUser = Helper.tbl_Users.Username;
                p.FlagDel = false;
                p.FlagSend = false;

                _pro.Add(p);
            }

            pro.tbl_HQ_Promotion_Hit_Temps = _pro;
        }

        private void Save()
        {
            try
            {
                if (!ValidateSave())
                    return;

                string docno = string.Empty;
                int ret = 0;

                bool checkEditMode = bu.CheckExistsIV(txdDocNo.Text);
                if (checkEditMode)
                {
                    string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                    string title = "ยืนยันการบันทึก!!";
                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    bu = new VE();

                    docno = txdDocNo.Text;
                    if (ddlDocStatus.SelectedValue.ToString() == "5")
                    {
                        var veList = bu.GetInvVEMaster(docno);
                        if (veList != null && veList.Count > 0)
                            bu.tbl_IVMaster = veList[0];

                        bu.tbl_IVDetails = bu.GetInvVEDetails(docno);

                        bu.tbl_POMaster = null;
                        bu.tbl_POMaster = bu.GetPOMaster(txtCustPONo.Text);
                        bu.tbl_PODetails.Clear();
                        bu.tbl_InvMovements.Clear();
                        bu.tbl_InvTransactions.Clear();
                        bu.tbl_InvWarehouses.Clear();

                        //string whID = bu.tbl_POMaster.WHID;
                        //var poDts = bu.GetPODetails(docno);

                        ret = bu.RemoveIVDetails();
                        if (ret == 1)
                            ret = bu.RemoveIVMaster();

                        if (ret == 1)
                        {
                            bu.tbl_IVMaster = null;
                            bu.tbl_IVDetails.Clear();
                            bu.tbl_POMaster.CustInvNO = "";

                            ret = bu.UpdateData();
                        }
                    }
                    else
                        ret = 1;
                }
              
                if (ret == 1)
                {
                    this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, txdDocNo.Text);

                    txdDocNo.Text = docno;

                    grdList.CellContentClick -= grdList_CellContentClick;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    ddlDocStatus.SelectedValue.ToString().CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
                   
                    ClearForm();

                    btnUpdateAddress.Enabled = true;
                    btnAdd.Enabled = false;
                    //btnReCalc.Enabled = true;
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

        public void SetDefaultGridViewEvent(DataGridView grd)
        {
            grd.CellContentClick -= new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellContentClick);
            grd.CellEndEdit -= new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellEndEdit);
            grd.CellValidating -= new System.Windows.Forms.DataGridViewCellValidatingEventHandler(grdList_CellValidating);
            grd.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellValueChanged);
            grd.EditingControlShowing -= new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(grdList_EditingControlShowing);
            grd.RowPostPaint -= new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(grdList_RowPostPaint);
            grd.UserDeletingRow -= new System.Windows.Forms.DataGridViewRowCancelEventHandler(grdList_UserDeletingRow);
            grd.KeyDown -= new System.Windows.Forms.KeyEventHandler(grdList_KeyDown);
            grd.CellClick -= new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellClick);

            grd.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellContentClick);
            grd.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellEndEdit);
            grd.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(grdList_CellValidating);
            grd.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellValueChanged);
            grd.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(grdList_EditingControlShowing);
            grd.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(grdList_RowPostPaint);
            grd.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(grdList_UserDeletingRow);
            grd.KeyDown += new System.Windows.Forms.KeyEventHandler(grdList_KeyDown);
            grd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(grdList_CellClick);
        }

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (!dtpDocDate.ValidateEndDay(bu))
            {
                string message = "ระบบปิดวันไปแล้ว ไม่สามารถเลือกวันที่นี้ได้ !!!";
                message.ShowWarningMessage();
                ret = false;
            }
            
            if (ret)
            {
                errList.SetErrMessageList(txtCustomerCode, lblCustomerCode);
                errList.SetErrMessageList(txtBillTo, lblBillTo);
                errList.SetErrMessageList(txtContact, lblContact);
                errList.SetErrMessageList(txtEmpCode, lblEmpCode);
                errList.SetErrMessageList(txtWHCode, lblWHCode);

                if (errList.Count == 0)
                {
                    Func<tbl_ArCustomer, bool> func = (x => x.CustomerCode == txtCustomerCode.Text);
                    var sup = bu.GetCustomer(func);
                    if (sup == null)
                    {
                        string t = lblCustomerCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtCustomerCode.ErrorTextBox();
                    }

                    Func<tbl_BranchWarehouse, bool> whFunc = (x => x.WHID == txtWHCode.Text);
                    var wh = bu.GetBranchWarehouse(whFunc);
                    if (wh == null)
                    {
                        string t = lblWHCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtWHCode.ErrorTextBox();
                    }
                }

                if (errList.Count > 0) //validate header
                {
                    string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                    message.ShowWarningMessage();
                    ret = false;
                }
                else //validate data grid
                {
                    //var allProduct = bu.GetProduct();
                    ret = grdList.ValiadteDataGridView(allProduct, 0, 3, 4, 5, bu, "", false);
                }
            }

            return ret;
        }

        #endregion

        #region event methods

        private void frmVE_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InitialData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (bu.tbl_POMaster != null)
            {
                if (bu.tbl_POMaster.FlagSend == true)
                {
                    string message = "ไม่สามารถแก้ไขเอกสารได้ เนื่องจากได้ส่งข้อมูลเข้า Data Center แล้ว";
                    message.ShowWarningMessage();
                    return;
                }
            }

            btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, txdDocNo.Text);

            txdDocNo.DisableTextBox(false);
            txtCustomerCode.DisableTextBox(false);
            txtCustPONo.DisableTextBox(false);
            btnSearchCust.Enabled = true;
            txtCustInvNO.DisableTextBox(false);

            txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");
            ddlDocStatus.Enabled = true;
            btnCancel.Enabled = true;

            validateNewRow = true;

            if (!string.IsNullOrEmpty(txdDocNo.Text) && bu.CheckExistsPO(txdDocNo.Text))
                grdList.CellContentClick -= grdList_CellContentClick;
            else
            {
                grdList.CellContentClick -= grdList_CellContentClick;
                grdList.CellContentClick += grdList_CellContentClick;
            }

            dtpDocDate.Focus();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            btnCopy.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnPrint, txdDocNo.Text);

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == "4"; };
            ddlDocStatus.SelectedValueDropdownList(condition);
            ddlDocStatus.Enabled = false;

            txdDocNo.Text = string.Empty;
            txtCustPONo.Text = string.Empty;
            txtCustInvNO.Text = string.Empty;

            validateNewRow = true;

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var documentType = bu.GetDocumentType().FirstOrDefault(x => x.DocTypeCode.Trim() == "V");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                runDigit = documentType.DocFormat.Length - documentType.RunLength.Value;

                this.ClearControl(bu, docTypeCode, runDigit);
                txtComment.Text = documentType.DocRemark;
            }
            btnAdd.Enabled = false;

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnCopy.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnPrint.Enabled = false;

            validateNewRow = true;

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;

            grdList.CellContentClick -= grdList_CellContentClick;
            btnSearchDoc.Enabled = true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printContextMenuStrip.Show(btnPrint, new Point(0, btnPrint.Height));
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchDoc_Click(object sender, EventArgs e)
        {
            this.OpenIVDocPopup("ใบกำกับภาษีขาย", docTypeCode);
        }

        private void btnSearchWHCode_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchBWHControls, "เลือกคลังสินค้า", whFunc);
        }

        private void btnSearchEmpCode_Click(object sender, EventArgs e)
        {
            this.OpenEmployeeNamePopup(searchEmpControls, "เลือกพนักงาน", empFunc);
        }

        private void btnSearchCust_Click(object sender, EventArgs e)
        {
            this.OpenCustomerPopup(searchCustControls, "เลือกลูกค้า");

            FilterSaleArea(txtCustomerCode.Text);
        }

        private void btnCustInfo_Click(object sender, EventArgs e)
        {
        }

        private void btnReCalc_Click(object sender, EventArgs e)
        {
            //CalcPromotion(false);
        }

        private void TxtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("Customer", searchCustControls, txt.Text);

                FilterSaleArea(txt.Text);
            }
        }

        private void TxtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("BranchWarehouse", searchBWHControls, txt.Text);
            }
        }

        private void TxdDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BindVEData(txdDocNo.Text);
        }

        private void DtpDocDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDocDate.CalcDueDate(dtpDueDate, nudCreditDay);
        }

        private void NudCreditDay_ValueChanged(object sender, EventArgs e)
        {
            nudCreditDay.CalcDueDate(dtpDocDate, dtpDueDate);
        }

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grdList.SetCellContentClick(this, sender, e, "IMProduct", 4);
        }

        private void grdList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                grdList.ModifyComboBoxCell(allProduct, e.RowIndex, bu, 3, 0);
        }

        private void grdList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
                tb.PreviewKeyDown -= DataGridView_PreviewKeyDown;
                tb.PreviewKeyDown += DataGridView_PreviewKeyDown;

                tb.KeyPress -= DataGridView_KeyPress;
                tb.KeyPress += DataGridView_KeyPress;
            }
            else if (e.Control is DataGridViewComboBoxEditingControl)
            {
                e.CellStyle.BackColor = Color.White;

                DataGridViewComboBoxEditingControl tb = (DataGridViewComboBoxEditingControl)e.Control;
                tb.PreviewKeyDown -= DataGridView_PreviewKeyDown;
                tb.PreviewKeyDown += DataGridView_PreviewKeyDown;

                tb.KeyPress -= DataGridView_KeyPress;
                tb.KeyPress += DataGridView_KeyPress;

                ComboBox combo = e.Control as ComboBox;
                if (combo != null)
                {
                    combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                    combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                }
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.Items != null && cb.Items.Count > 0)
                CalculateRow(grdList, grdList.CurrentRow.Index);
        }

        private void DataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            grdList.SetCellNumberOnly(sender, e, numberCell.ToList());
        }

        private void DataGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

            if (e.KeyCode == Keys.Enter)
            {
                int currentRowIndex = grd.CurrentCell.RowIndex;
                int curentColIndex = grd.CurrentCell.ColumnIndex;

                grd.ClearSelection();

                var cell0 = grd.Rows[currentRowIndex].Cells[0];
                if (curentColIndex == 0)
                {
                    bool isNewRow = true;
                    if (cell0.IsNotNullOrEmptyCell())
                    {
                        validateNewRow = true;

                        var checkDup = grd.ValidateDuplicateSKU(cell0.EditedFormattedValue.ToString(), 0, currentRowIndex, ref validateNewRow);
                        if (!checkDup)
                        {
                            GridViewHelper.ShowDupSKUMessage();
                            cell0.Value = "";
                            grd.Rows.RemoveAt(currentRowIndex);
                            isNewRow = false;
                            validateNewRow = true;
                        }

                        if (isNewRow)
                            grd.BindDataGridViewRow(dataGridList, initDataGridList, dt, 0, cell0.EditedFormattedValue.ToString(), currentRowIndex, 0, ref validateNewRow, "IMProduct", false);
                    }

                    if (isNewRow)
                    {
                        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 3];
                        grd.BeginEdit(true);
                    }
                }
                else if (curentColIndex == 3 || curentColIndex == 4)
                {
                    grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 1];
                    grd.BeginEdit(true);
                }
                else if (curentColIndex == 5)
                {
                    if (cell0.IsNotNullOrEmptyCell())
                    {
                        if ((grd.RowCount - 1) == currentRowIndex)
                        {
                            grdList.AddNewRow(allProduct, initDataGridList, 0, "", currentRowIndex + 1, validateNewRow, uoms, bu, this, 0);

                            if (validateNewRow)
                            {
                                if (grd.RowCount > currentRowIndex + 1)
                                {
                                    grd.CurrentCell = grd.Rows[currentRowIndex + 1].Cells[0];
                                    grd.BeginEdit(true);
                                }
                            }
                        }
                        else
                        {
                            if (grd.RowCount > currentRowIndex + 1)
                            {
                                grd.CurrentCell = grd.Rows[currentRowIndex + 1].Cells[0];
                                grd.BeginEdit(true);
                            }
                        }

                    }
                }
            }
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
        }

        private void grdList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView grd = (DataGridView)sender;
                if (grd != null && grd.CurrentRow != null)
                {
                    int currentRowIndex = grd.CurrentCell.RowIndex;
                    int curentColIndex = grd.CurrentCell.ColumnIndex;

                    var numCals = new int[] { 0, 3, 4, 5, 7, 8 };
                    if (numCals.Contains(curentColIndex))
                    {
                        var cell4 = grd.Rows[currentRowIndex].Cells[4];
                        if (cell4.IsNotNullOrEmptyCell())
                            CalculateRow(grd, currentRowIndex);
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

        private void grdList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            grdList.SetUserDeletingRow(sender, e);
        }

        private void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataGridView _grd = (DataGridView)sender;
                if (_grd != null && _grd.CurrentRow != null)
                {
                    int rowIndex = _grd.CurrentRow.Index;
                    if (_grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "White" || _grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "0")
                        _grd.Rows.RemoveAt(rowIndex);
                    else
                        return;
                }
            }
        }

        private void grdList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            grdList.SetCellClick(sender, e, cellEdit, 0);
        }


        #endregion

        private void btnUpdateAddress_Click(object sender, EventArgs e)
        {
            string cfMsg = "ต้องการปรับปรุงที่อยู่ใช่หรือไม่?";
            string title = "ยืนยันการบันทึก!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            bool checkEditMode = bu.CheckExistsIV(txdDocNo.Text);
            if (checkEditMode)
            {
                bool ret = bu.UpdateCustomerAddress(txdDocNo.Text, true);
                if (ret)
                {
                    //bu.tbl_POMaster = bu.GetPOMaster(txdDocNo.Text);
                    //if (bu.tbl_POMaster != null)
                    {
                        var cust = bu.GetCustomer(txtCustomerCode.Text);
                        if (cust != null && cust.Count > 0)
                        {
                            txtBillTo.Text = cust[0].BillTo;
                            txtCustName.Text = cust[0].CustName;
                            txtTelephone.Text = cust[0].Telephone;
                            txtContact.Text = cust[0].Contact;
                        }

                    }

                }
            }
        }
    }
}
