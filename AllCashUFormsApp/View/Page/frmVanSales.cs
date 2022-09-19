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
    public partial class frmVanSales : Form
    {
        MenuBU menuBU = new MenuBU();
        TabletSales bu = new TabletSales();
        CustomerShelf buShelf = new CustomerShelf();
        Promotion pro = new Promotion();
        static PromotionTemp proTmp = new PromotionTemp();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        List<tbl_ProductUom> uoms = new List<tbl_ProductUom>();
        List<tbl_SalArea> saleAreaList = new List<tbl_SalArea>();
        List<tbl_SalAreaDistrict> salAreaDistrictList = new List<tbl_SalAreaDistrict>();
        Dictionary<string, decimal> listDiscount = new Dictionary<string, decimal>();
        Dictionary<string, decimal> lineTotalDisc = new Dictionary<string, decimal>();

        bool validateNewRow = true;
        public string docTypeCode = "";
        int runDigit = 0;

        List<Control> searchCustControls = new List<Control>();
        List<Control> searchPromotionControls = new List<Control>();
        List<Control> searchBWHControls = new List<Control>();
        List<Control> searchEmpControls = new List<Control>();
        List<string> readOnlyControls = new List<string>();

        int[] cellEdit = new int[] { 0, 3, 4, 5 }; // new int[] { 0, 3, 4, 5, 7, 8 };
        int[] numberCell = new int[] { 4, 5, 8 };
        int[] uOnlineShopType = new int[] { 8, 9, 10 };

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

        List<tbl_PODetail> allPODetails = new List<tbl_PODetail>();
        List<tbl_ProductUom> prodUOMs = new List<tbl_ProductUom>();
        bool isAdd = false;

        Dictionary<string, string> allEmp = new Dictionary<string, string>();
        tbl_Employee emp = new tbl_Employee();
        tbl_ApSupplier supp = new tbl_ApSupplier();

        List<tbl_Employee> allEmp2 = new List<tbl_Employee>();
        List<tbl_BranchWarehouse> allBranchWarehouse = new List<tbl_BranchWarehouse>();


        public frmVanSales()
        {
            InitializeComponent();

            searchCustControls = new List<Control> { txtCustomerID, txtCustName, txtBillTo, txtContact, txtTelephone };
            searchBWHControls = new List<Control> { txtWHCode, txtWHName };
            searchEmpControls = new List<Control> { txtEmpCode };
            readOnlyControls = new List<string>() { txtCustName.Name, txtWHName.Name, txtEmpCode.Name, txtCrUser.Name };

            dataGridList = new Dictionary<int, string>() { { 0, "ProductID" }, { 2, "ProductName" }, { 3, "UOMSetID" }, { 5, "SellPriceVat" }, { 6, "VatType" }, { 11, "SellPrice" } };
            initDataGridList = new Dictionary<int, string>() { { 0, "" }, { 2, "" }, { 3, "combobox" }, { 4, "0" }, { 5, "0.00" }, { 6, "0" }, { 7, "N" }, { 8, "0.00" }, { 9, "0.00" }, { 10, "" }, { 11, "0.00" } };

            txdDocNo.KeyDown += TxdDocNo_KeyDown;
            txtWHCode.KeyDown += TxtWHCode_KeyDown;
            txtWHCode.TextChanged += TxtWHCode_TextChanged;
            txtCustomerID.KeyDown += TxtCustomerCode_KeyDown;

            dtpDocDate.ValueChanged += DtpDocDate_ValueChanged;
            nudCreditDay.ValueChanged += NudCreditDay_ValueChanged;
        }

        #region private methods

        private void InitPage()
        {
            var menu = bu.tbl_AdmFormList.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }

            var documentType = bu.tbl_DocumentType.FirstOrDefault(x => x.DocTypeCode.Trim() == "IM");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                runDigit = Convert.ToInt32(documentType.RunLength);

                this.ClearControl(bu, docTypeCode, runDigit);
                txtComment.Text = documentType.DocRemark;
            }

            validateNewRow = true;
            btnAdd.Enabled = true;

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnPrintCrys.Enabled = btnPrint.Enabled;

            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            var headerPic = bu.tbl_MstMenu.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
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
            //dtpDueDate.SetDateTimePickerFormat();

            allUOM = bu.tbl_ProductUom;

            uoms.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
            uoms.AddRange(allUOM);

            saleAreaList.AddRange(bu.tbl_SalArea);
            salAreaDistrictList.AddRange(bu.tbl_SalAreaDistrict);

            //data gridview
            grdList.SetDataGridViewStyle();
            SetDefaultGridViewEvent(grdList);

            allProduct = bu.tbl_Product;
            allUomSet = bu.tbl_ProductUomSet;

            allProductPrice = bu.tbl_ProductPriceGroup;
            allProdGroup = bu.tbl_ProductGroup;
            allProdSubGroup = bu.tbl_ProductSubGroup;

            ClearPromotionTemp();
        }

        private void ClearPromotionTemp()
        {
            pro.RemoveTempData();
        }

        public void BindVanSalesData(string ivDocNo)
        {
            bu.GetDocData(ivDocNo, docTypeCode);

            var po = bu.tbl_POMaster;
            var poDts = bu.tbl_PODetails;

            if (string.IsNullOrEmpty(po.DocNo))
            {
                string msg = "ไม่พบข้อมูลเลขที่เอกสาร!!!";
                msg.ShowWarningMessage();

                btnCancel.PerformClick();

                return;
            }

            if (po != null)
            {
                BindPOMaster(po);
            }
            if (poDts != null && poDts.Count > 0)
            {
                BindPODetail(poDts);
            }

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            btnSearchDoc.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
            btnPrintCrys.Enabled = btnPrint.Enabled;

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;
            btnReCalc.Enabled = false;
            btnShowPromotion.Enabled = false;

            grdList.CellContentClick -= grdList_CellContentClick;

            //bool checkEditMode = bu.CheckExistsPO(ivDocNo);
            //po.DocStatus.CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);

            btnUpdateAddress.Enabled = true;
            btnGenCustIVNo.Enabled = true;

            btnEdit.Enabled = ddlDocStatus.SelectedValue.ToString() == "4";
            btnGenCustIVNo.Enabled = ddlDocStatus.SelectedValue.ToString() == "4" && string.IsNullOrEmpty(txtCustInvNO.Text);
            btnShowVE.Enabled = !string.IsNullOrEmpty(txtCustInvNO.Text);
            btnCustInfo.Enabled = !string.IsNullOrEmpty(txtCustomerID.Text);

            CreateGridBtnList();
        }

        private void BindPOMaster(tbl_POMaster po)
        {
            txdDocNo.Text = po.DocNo;

            dtpDocDate.Value = po.DocDate.ToDateTimeFormat();

            txtCustomerID.Text = po.CustomerID;
            txtCustName.Text = po.CustName;
            txtContact.Text = po.ContactName;
            txtTelephone.Text = po.ContactTel;
            txtBillTo.Text = po.Address;
            txtCustPONo.Text = po.CustPONo;
            txtCustInvNO.Text = po.CustInvNO;

            if (allEmp2.Count == 0)
            {
                allEmp2 = bu.GetEmployee();
            }

            var employee = allEmp2.FirstOrDefault(x => x.EmpID == po.EmpID);// edit by sailom.k 13/12/2021 Helper.tbl_Users.EmpID); // bu.GetEmployee(Helper.tbl_Users.EmpID);
            if (employee != null)
                txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName, employee.LastName);

            var emp = allEmp2.FirstOrDefault(x => x.EmpID == po.SaleEmpID);
            if (emp != null)
                txtEmpCode.Text = emp.TitleName + " " + emp.FirstName + " " + emp.LastName;

            txtWHCode.Text = po.WHID;
            Func<tbl_BranchWarehouse, bool> func = (x => x.WHID == po.WHID);
            var wh = allBranchWarehouse.FirstOrDefault(func);// bu.GetBranchWarehouse(func);
            if (wh != null)
            {
                txtWHName.Text = wh.WHName;
            }

            //FilterSaleArea(po.CustomerID);
            FilterSaleArea(po.SalAreaID); //edit by sailom .k 13/12/2021
            ddlDocStatus.BindDropdownDocStatus(bu, po.DocStatus);

            txtComment.Text = po.Comment;
            txtRemark.Text = po.Remark;
            txnAmount.Text = po.Amount.Value.ToStringN2();
            //txnDiscountType.Text = "0";// po.DiscountType;
            txnDiscountAmt.Text = po.Discount.Value.ToStringN2();

            decimal amt = 0;
            decimal excVat = 0;
            decimal discount = 0;
            decimal vatRate = 0;
            if (po.Amount != null)
                amt = po.Amount.Value;
            if (po.ExcVat != null)
                excVat = po.ExcVat.Value;
            if (po.Discount != null)
                discount = po.Discount.Value;
            if (po.VatRate != null)
                vatRate = po.VatRate.Value;

            decimal incVat = ((amt - excVat - discount) * 100) / (100 + vatRate);

            txnBeforeVat.Text = incVat.ToStringN2();
            txnVatAmt.Text = po.VatAmt.Value.ToStringN2();
            lblVatType.Text = po.VatRate.Value.ToStringN0();
            txnExcVat.Text = po.ExcVat.Value.ToStringN2();
            txnTotalDue.Text = po.TotalDue.ToStringN2();
        }

        private void BindPODetail(List<tbl_PODetail> poDts)
        {
            grdList.Rows.Clear();

            //var allUOM = bu.tbl_ProductUom;
            var allProduct = bu.tbl_Product;
            //var allProductPrice = bu.tbl_ProductPriceGroup;

            //Last edit by sailom.k 14/09/2021 tunning performance-------------------------
            isAdd = false;
            allPODetails = poDts;

            List<tbl_DiscountType> disTypeList = new List<tbl_DiscountType>();
            disTypeList = bu.tbl_DiscountType;

            prodUOMs = new List<tbl_ProductUom>();
            var listPrdID = allPODetails.Select(x => x.ProductID).ToList();
            prodUOMs.AddRange(bu.GetProductUOM(listPrdID));
            //Last edit by sailom.k 14/09/2021 tunning performance--------------------

            for (int i = 0; i < poDts.Count; i++)
            {
                grdList.Rows.Add(1);

                grdList.Rows[i].Cells[0].Value = poDts[i].ProductID;

                string productName = string.Empty;
                if (!string.IsNullOrEmpty(poDts[i].ProductName))
                {
                    productName = poDts[i].ProductName;
                }
                else
                {
                    var data = allProduct.FirstOrDefault(x => x.ProductID == poDts[i].ProductID);
                    if (data != null)
                    {
                        productName = data.ProductName;
                    }
                }

                grdList.Rows[i].Cells[2].Value = productName;

                //grdList.BindComboBoxCell(allProduct, grdList.Rows[i], i, false, 3, uoms, this, bu, 0);
                grdList.BindComboBoxDiscountTypeCell(allProduct, grdList.Rows[i], i, false, 3, uoms, this, bu, 0, disTypeList, 7); //Last edit by sailom.k 14/09/2021 tunning performance

                grdList.Rows[i].Cells[3].Value = poDts[i].OrderUom;

                grdList.Rows[i].Cells[4].Value = poDts[i].OrderQty;
                grdList.Rows[i].Cells[5].Value = poDts[i].UnitPrice;
                grdList.Rows[i].Cells[6].Value = poDts[i].VatType;
                grdList.Rows[i].Cells[7].Value = poDts[i].LineDiscountType;
                grdList.Rows[i].Cells[8].Value = poDts[i].LineDiscount;
                grdList.Rows[i].Cells[9].Value = poDts[i].LineTotal;
                grdList.Rows[i].Cells[10].Value = poDts[i].OrderUom;

                if (poDts[i].OrderUom != -1)
                {
                    string prdCode = poDts[i].ProductID;
                    //Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == poDts[i].OrderUom && x.ProductID == prdCode);
                    var prdPriceList = allProductPrice.Where(x => x.ProductUomID == poDts[i].OrderUom && x.ProductID == prdCode).ToList(); //bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);

                    if (prdPriceList != null && prdPriceList.Count > 0)
                        grdList.Rows[i].Cells[11].Value = prdPriceList[0].SellPrice.Value;
                }
            }
        }

        public void BindSearchProduct(DataTable productDT, int rowIndex)
        {
            validateNewRow = true;
            if (Helper.tbl_Users.RoleID != 10) //allow super admin add duplicate item for support promotion 24022021
            {
                grdList.ValidateDuplicateSKU(productDT.Rows[0]["ProductID"].ToString(), 0, rowIndex, ref validateNewRow);
            }

            var allProduct = bu.tbl_Product;
            grdList.BindDataGrid(allProduct, dataGridList, initDataGridList, productDT, 0, rowIndex, validateNewRow, this, uoms, bu, 0);
        }

        private void CalcPromotion(bool isShowPopup = false)
        {
            try
            {
                pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                pro.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();
                var ret = pro.RemoveTempData();

                if (ret == 1)
                {
                    //var allProduct = bu.tbl_Product;
                    bool validateQty = true;
                    if (grdList.RowCount > 0)
                    {
                        for (int i = 0; i < grdList.RowCount; i++)
                        {
                            var prdCodeCell = grdList.Rows[i].Cells[0];
                            var qtyCell = grdList.Rows[i].Cells[4];
                            var discountType = grdList.Rows[i].Cells[6];
                            if (prdCodeCell.IsNotNullOrEmptyCell() && discountType.IsNotNullOrEmptyCell()
                                && discountType.EditedFormattedValue.ToString() != "F" && discountType.EditedFormattedValue.ToString() != "S")
                            {
                                if (qtyCell.IsNotNullOrEmptyCell() && Convert.ToDecimal(qtyCell.EditedFormattedValue.ToString()) <= 0) //check qty
                                {
                                    var message = "จำนวนสินค้าไม่ถูกต้อง กรุณาตรวจสอบอีกครั้ง !!!";
                                    message.ShowWarningMessage();
                                    validateQty = false;
                                    break;
                                }
                            }
                        }
                    }
                    //validateQty = grdList.ValiadteDataGridView(allProduct, 0, 3, 4, 5, null, "", false);

                    if (txtCustName.Text.Contains("ไม่ระบุ")) //validate except customer
                    {
                        return;
                    }
                    if (string.IsNullOrEmpty(txtCustName.Text))
                    {
                        var message = "กรุณาเลือกร้านค้า !!!";
                        message.ShowWarningMessage();
                        txtCustomerID.ErrorTextBox();
                        return;
                    }

                    decimal totalDiscount = 0.00m;

                    if (validateQty)
                    {
                        pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                        pro.tbl_HQ_Promotion_Hits = new List<tbl_HQ_Promotion_Hit>();

                        proTmp.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                        bu.tbl_POMaster = new tbl_POMaster();
                        bu.tbl_PODetails = new List<tbl_PODetail>();

                        PreparePOMaster(false);
                        PreparePODetail(0, false);

                        //var proList = pro.CalculatePromotion(bu.tbl_PODetails.Where(x => x.UnitPrice > 0).ToList(), txtCustomerCode.Text.Trim());
                        var proList = pro.CalculatePromotion(bu.tbl_PODetails, txtCustomerID.Text.Trim(), bu.tbl_POMaster.WHID); //for support split van last edit by sailom.k 19/10/2021
                        if (proList != null && proList.Count > 0)
                        {
                            var delFlag = pro.RemoveTempData();
                            if (delFlag == 1)
                            {
                                PreparePromotionTemp(proList);
                                var addFlag = pro.AddTempData();

                                var po = bu.tbl_POMaster;

                                proTmp.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();
                                if (proTmp.tbl_HQ_Promotion_Hit_Temps.Any(x => x.DisCountAmt.Value > 0))
                                    po.Discount = proTmp.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value);

                                //po.Discount = pro.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value);
                                totalDiscount = po.Discount.Value;

                                //decimal reCaclBeforeVat = Convert.ToDecimal(txnBeforeVat.Text) - po.Discount.Value;
                                //txnBeforeVat.Text = reCaclBeforeVat.ToStringN2();
                                if (isShowPopup && addFlag == 1)
                                {
                                    if (pro.tbl_HQ_Promotion_Hit_Temps != null && pro.tbl_HQ_Promotion_Hit_Temps.Count > 0)
                                    {
                                        ValidateInputShelf();

                                        this.OpenPromotionTempPopup(searchPromotionControls, "โปรโมชั่น");
                                    }
                                }
                            }
                        }
                    }

                    txnDiscountAmt.Text = totalDiscount.ToStringN2();

                    CalculateTotal();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void CalcPromotionBeforeSave()
        {
            CalcPromotion(true);

            PreparePODetail(2, false); //Calc Pro

            DistributeFreePromotion(bu.tbl_PODetails);

            BindPODetail(bu.tbl_PODetails);

            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                CalculateRow(grdList, i);
            }

            CalculateTotal();

            //decimal totalDiscount = 0.00m;
            //proTmp.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();

            //if (proTmp.tbl_HQ_Promotion_Hit_Temps.Any(x => x.DisCountAmt.Value > 0))
            //    totalDiscount = proTmp.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value);

            //txnDiscountAmt.Text = totalDiscount.ToStringN2();
        }

        private void CalculateRow(DataGridView grd, int rowIndex)
        {
            try
            {
                decimal orderQty = 0;
                decimal unitPrice = 0;
                decimal unitSellPrice = 0;
                decimal discount = 0;
                int orderUom = -1;
                decimal discountSellPrice = 0;

                var cell0 = grd.Rows[rowIndex].Cells[0];
                var cell3 = grd.Rows[rowIndex].Cells[3];
                var cell4 = grd.Rows[rowIndex].Cells[4];
                var cell5 = grd.Rows[rowIndex].Cells[5];
                var cell6 = grd.Rows[rowIndex].Cells[6];
                var cell7 = grd.Rows[rowIndex].Cells[7];
                var cell8 = grd.Rows[rowIndex].Cells[8];
                var cell9 = grd.Rows[rowIndex].Cells[9];
                var cell11 = grd.Rows[rowIndex].Cells[11];

                if (cell3.IsNotNullOrEmptyCell())
                {
                    //Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => uomList.Contains(x.ProductUomID));
                    //var allPrdUOM = bu.GetUOM(); //bu.GetUOM(tbl_ProductUomPre);

                    var prdUOM = allUOM.FirstOrDefault(x => x.ProductUomName == cell3.EditedFormattedValue.ToString());
                    if (prdUOM != null)
                    {
                        orderUom = prdUOM.ProductUomID;
                        if (orderUom != -1)
                        {
                            string prdCode = cell0.EditedFormattedValue.ToString();
                            Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == orderUom && x.ProductID == prdCode);
                            var prdPriceList = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList(); // bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);

                            if (prdPriceList != null && prdPriceList.Count > 0)
                            {
                                cell5.Value = prdPriceList[0].SellPriceVat.Value;
                                cell11.Value = prdPriceList[0].SellPrice.Value;
                            }
                        }
                        else
                        {
                            cell5.Value = 0;
                            cell11.Value = 0;
                        }
                    }
                    else
                    {
                        cell5.Value = 0;
                        cell11.Value = 0;
                    }
                }
                if (cell4.IsNotNullOrEmptyCell())
                {
                    orderQty = Convert.ToDecimal(cell4.EditedFormattedValue);
                }
                if (cell5.IsNotNullOrEmptyCell())
                {
                    unitPrice = Convert.ToDecimal(cell5.EditedFormattedValue);
                }
                if (cell11.IsNotNullOrEmptyCell())
                {
                    unitSellPrice = Convert.ToDecimal(cell11.EditedFormattedValue);
                }
                if (cell8.IsNotNullOrEmptyCell())
                {
                    if (cell7.IsNotNullOrEmptyCell())
                    {
                        discount = bu.CalDiscountType(cell7.EditedFormattedValue.ToString(), cell8.EditedFormattedValue.ToString(), orderQty, unitPrice);

                        //edit by sailom 21-06-2021
                        discountSellPrice = discount;
                        //if (cell6.FormattedValue.ToString() != "0")
                        //    discountSellPrice = bu.CalDiscountType(cell7.EditedFormattedValue.ToString(), cell8.EditedFormattedValue.ToString(), orderQty, unitSellPrice);
                    }
                    else
                    {
                        discount = 0;
                    }

                    if (discountSellPrice != 0)
                    {
                        listDiscount.Remove(cell0.EditedFormattedValue.ToString());
                        listDiscount.Add(cell0.EditedFormattedValue.ToString(), discountSellPrice);
                    }
                    else
                        listDiscount.Remove(cell0.EditedFormattedValue.ToString());

                    //edit by sailom 21-06-2021
                    cell9.Value = ((orderQty * unitPrice) - discountSellPrice).ToDecimalN2().ToStringN2();
                    //cell9.Value = (orderQty * unitPrice).ToDecimalN2().ToStringN2();
                }

                CalculateTotal();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }

        }

        private void CalculateTotal()
        {
            try
            {
                decimal amount = 0;
                decimal excVat = 0;
                decimal incVat = 0;
                decimal vatAmt = 0;
                decimal totalDue = 0;
                decimal vatRate = 0;
                string discountType = "";
                decimal totalDiscountAmt = 0;

                //for support U-Online
                bool isUOnline = false;
                //if (!string.IsNullOrEmpty(txtCustomerCode.Text))
                //{
                //    var cust = bu.GetCustomer(txtCustomerCode.Text);
                //    if (cust != null && cust.Count > 0)
                //    {
                //        if (cust.FirstOrDefault(x => uOnlineShopType.Contains(x.ShopTypeID)) != null)
                //            isUOnline = true; 
                //    }
                //}

                //var allPrdUOM = bu.GetUOM();
                //var allProductPrice = bu.tbl_ProductPriceGroup;

                //edit by sailom 21-06-2021
                decimal totalDiscount = 0;
                //if (!string.IsNullOrEmpty(txnDiscountAmt.Text) && Convert.ToDecimal(txnDiscountAmt.Text) > 0)
                //    totalDiscount = Convert.ToDecimal(txnDiscountAmt.Text).ToDecimalN2();

                pro.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();
                if (pro.tbl_HQ_Promotion_Hit_Temps.Any(x => x.DisCountAmt.Value > 0))
                    totalDiscount = pro.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value).ToDecimalN2();
                else
                    totalDiscount = 0;

                for (int i = 0; i < grdList.RowCount; i++)
                {
                    var prodIDCell = grdList.Rows[i].Cells[0];
                    var unitCell = grdList.Rows[i].Cells[3];
                    var qtyCell = grdList.Rows[i].Cells[4];
                    var priceCell = grdList.Rows[i].Cells[5];
                    var vatCell = grdList.Rows[i].Cells[6];
                    var lineTotalCell = grdList.Rows[i].Cells[9];
                    var discountTypeCell = grdList.Rows[i].Cells[7];
                    var discountAmtCell = grdList.Rows[i].Cells[8];
                    var sellPriceCell = grdList.Rows[i].Cells[11];
                    string prdCode = prodIDCell.EditedFormattedValue.ToString();
                    var _qty = Convert.ToDecimal(qtyCell.EditedFormattedValue);

                    if (!string.IsNullOrEmpty(prdCode))
                    {
                        //edit by sailom 21-06-2021
                        //if (totalDiscount > 0)
                        //{
                        //    decimal discountPerSku = totalDiscount / (_qty <= 0 ? 1 : _qty);
                        //    if (listDiscount.Count(x => x.Key == prdCode) == 0)
                        //        listDiscount.Add(prdCode, discountPerSku);
                        //}

                        if (lineTotalCell.IsNotNullOrEmptyCell())
                        {
                            //edit by sailom 21-06-2021
                            amount += (_qty * Convert.ToDecimal(priceCell.EditedFormattedValue));
                            //amount += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                        }
                        if (vatCell.IsNotNullOrEmptyCell())
                        {
                            decimal _vateRate = Convert.ToDecimal(vatCell.EditedFormattedValue);
                            if (_vateRate > 0) //have VAT
                            {
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
                            else
                            {
                                discountAmtCell.Value = 0;
                            }
                        }
                        if (discountAmtCell.IsNotNullOrEmptyCell())
                        {
                            //edit by sailom 21-06-2021
                            //totalDiscountAmt += Convert.ToDecimal(discountAmtCell.EditedFormattedValue);

                            if (isUOnline)
                            {
                                decimal unitPrice = 0;
                                unitPrice = Convert.ToDecimal(priceCell.EditedFormattedValue);

                                var _discount = bu.CalDiscountType(discountTypeCell.EditedFormattedValue.ToString(), discountAmtCell.EditedFormattedValue.ToString(), _qty, unitPrice);

                                totalDiscountAmt += _discount;
                            }

                        }
                    }
                }

                excVat = excVat.ToDecimalN2();

                //edit by sailom 21-06-2021
                totalDiscount += totalDiscountAmt;
                totalDue = amount - totalDiscount; //edit by sailom 21-06-2021

                incVat = ((amount - excVat - totalDiscount) * 100) / (100 + vatRate);

                vatAmt = incVat * (vatRate / 100).ToDecimalN2();

                lblVatType.Text = vatRate.ToDecimalN0().ToString();
                txnAmount.Text = amount.ToDecimalN2().ToStringN2();
                txnBeforeVat.Text = incVat.ToStringN2();
                txnVatAmt.Text = vatAmt.ToStringN2();
                txnExcVat.Text = excVat.ToStringN2();
                txnTotalDue.Text = totalDue.ToStringN2();
                txnCommission.Text = "0.00";

                //edit by sailom 21-06-2021
                txnDiscountAmt.Text = totalDiscount.ToStringN2();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
        }

        private void FilterSaleArea(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                //Func<tbl_ArCustomer, bool> func = (x => x.CustomerID == text.Trim());

                //24/11/2021 by sailom .k-----------------------------------------------------------------
                bool ret = FindSaleAreaByCust(text); //find sale area by customer id

                if (!ret)
                {
                    FindSaleAreaByWHID(text); //find sale area by whid
                }
                //24/11/2021 by sailom .k-----------------------------------------------------------------
            }
        }

        /// <summary>
        /// 24/11/2021 by sailom .k
        /// </summary>
        /// <param name="text"></param>
        private bool FindSaleAreaByCust(string text)
        {
            bool ret = false;
            var cust = bu.GetCustomer(text.Trim());
            if (cust != null && cust.Count > 0)
            {
                var _saleAreaList = salAreaDistrictList.Where(x => x.WHID == cust[0].WHID);
                var listOfSalAreaID = _saleAreaList.Select(a => a.SalAreaID).ToList();
                //saleAreaList = bu.GetAllSaleArea().Where(x => listOfSalAreaID.Contains(x.SalAreaID)).ToList();
                saleAreaList = bu.GetAllSaleArea(listOfSalAreaID); //Last edit by sailom.k 14/09/2021 tunning performance

                //edit by sailom 07-06-2021---------------------------------------
                saleAreaList = saleAreaList.Where(p => p.SalAreaID == cust[0].SalAreaID).ToList();

                if (saleAreaList.Count == 0)
                    saleAreaList = bu.GetSaleArea(x => x.SalAreaName.Contains(cust[0].WHID.Substring(3, 3)));

                if (saleAreaList.Count > 0)
                {
                    Predicate<tbl_SalArea> defaultSelect = delegate (tbl_SalArea p) { return p.SalAreaID == cust[0].SalAreaID; };
                    ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID", null, defaultSelect);
                    ret = saleAreaList.Count > 0;
                }

                //edit by sailom 07-06-2021---------------------------------------
                string empID = cust[0].EmpID;
                var emp = allEmp2.FirstOrDefault(x => x.EmpID == empID);// bu.GetEmployee(empID);
                if (emp != null)
                    txtEmpCode.Text = emp.TitleName + " " + emp.FirstName + " " + emp.LastName;

                Func<tbl_BranchWarehouse, bool> whFunc = (x => x.WHID == cust[0].WHID);
                var wh = bu.GetBranchWarehouse(whFunc);
                if (wh != null)
                {
                    if (string.IsNullOrEmpty(txtWHCode.Text))
                    {
                        txtWHCode.Text = wh.WHCode;
                        txtWHName.Text = wh.WHName;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// 24/11/2021 by sailom .k
        /// </summary>
        /// <param name="text"></param>
        private void FindSaleAreaByWHID(string text)
        {
            var _saleAreaList = salAreaDistrictList.Where(x => x.WHID == text.Trim());
            var listOfSalAreaID = _saleAreaList.Select(a => a.SalAreaID).ToList();
            //saleAreaList = bu.GetAllSaleArea().Where(x => listOfSalAreaID.Contains(x.SalAreaID)).ToList();
            saleAreaList = bu.GetAllSaleArea(listOfSalAreaID); //Last edit by sailom.k 14/09/2021 tunning performance

            if (saleAreaList.Count == 0)
            {
                if (!string.IsNullOrEmpty(txtCustomerID.Text))
                {
                    var cust = bu.GetCustomer(txtCustomerID.Text.Trim());
                    if (cust != null && cust.Count > 0)
                    {
                        _saleAreaList = salAreaDistrictList.Where(x => x.WHID == cust[0].WHID);
                        listOfSalAreaID = _saleAreaList.Select(a => a.SalAreaID).ToList();
                        saleAreaList = bu.GetAllSaleArea(listOfSalAreaID); //Last edit by sailom.k 14/09/2021 tunning performance
                        saleAreaList = saleAreaList.Where(p => p.SalAreaID == cust[0].SalAreaID).ToList();

                        if (saleAreaList.Count == 0)
                            saleAreaList = bu.GetSaleArea(x => x.SalAreaName.Contains(cust[0].WHID.Substring(3, 3)));
                    }
                }
            }

            ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID");

            string empFullName = "";
            var bwh = bu.GetBranchWarehouse(x => x.WHID == text);
            if (bwh != null)
            {
                var emp = allEmp2.FirstOrDefault(x => x.EmpID == bwh.SaleEmpID);//bu.GetEmployee(bwh.SaleEmpID);
                if (emp != null)
                    empFullName = emp.TitleName + " " + emp.FirstName + " " + emp.LastName;
            }

            if (!string.IsNullOrEmpty(empFullName))
            {
                txtEmpCode.Text = empFullName;
            }

        }

        /// <summary>
        /// Last edit by sailom.k 14/09/2021 tunning performance //for support U-Online
        /// </summary>
        private void ValidateUCustomer(string customerID, int currentRowIndex)
        {
            //try
            //{
            //    int i = currentRowIndex;
            //    var cust = bu.GetCustomer(customerID);
            //    if (cust != null && cust.Count > 0)
            //    {
            //        bool isReadOnly = true;
            //        if (cust.FirstOrDefault(x => uOnlineShopType.Contains(x.ShopTypeID)) != null)
            //            isReadOnly = false;

            //        CalculateRow(grdList, i);

            //        grdList.Rows[i].Cells["colLineDiscountType"].ReadOnly = isReadOnly;
            //        grdList.Rows[i].Cells["colLineDiscount"].ReadOnly = isReadOnly;

            //        if (isReadOnly) //normal
            //        {
            //            grdList.Rows[i].Cells["colLineDiscountType"].Style.BackColor = ColorTranslator.FromHtml("#DCDCDC");
            //            grdList.Rows[i].Cells["colLineDiscount"].Style.BackColor = ColorTranslator.FromHtml("#DCDCDC");


            //            grdList.Rows[i].Cells["colLineDiscount"].Value = 0;
            //            CalculateTotal();
            //        }
            //        else //u-online
            //        {
            //            grdList.Rows[i].Cells["colLineDiscountType"].Style.BackColor = Color.White;
            //            grdList.Rows[i].Cells["colLineDiscount"].Style.BackColor = Color.White;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.WriteLog(this.GetType());

            //    string msg = ex.Message;
            //    msg.ShowErrorMessage();
            //}
        }

        /// <summary>
        /// for support U-Online
        /// </summary>
        /// <param name="customerID"></param>
        private void ValidateUCustomer(string customerID)
        {
            //try
            //{
            //    var cust = bu.GetCustomer(customerID);
            //    if (cust != null && cust.Count > 0)
            //    {
            //        bool isReadOnly = true;
            //        if (cust.FirstOrDefault(x => uOnlineShopType.Contains(x.ShopTypeID)) != null)
            //            isReadOnly = false;

            //        for (int i = 0; i < grdList.RowCount; i++)
            //        {
            //            CalculateRow(grdList, i);

            //            grdList.Rows[i].Cells["colLineDiscountType"].ReadOnly = isReadOnly;
            //            grdList.Rows[i].Cells["colLineDiscount"].ReadOnly = isReadOnly;

            //            if (isReadOnly) //normal
            //            {
            //                grdList.Rows[i].Cells["colLineDiscountType"].Style.BackColor = ColorTranslator.FromHtml("#DCDCDC");
            //                grdList.Rows[i].Cells["colLineDiscount"].Style.BackColor = ColorTranslator.FromHtml("#DCDCDC");

            //                //if (grdList.Rows[i].Cells["colLineDiscountType"] is DataGridViewComboBoxCell)
            //                //{
            //                //    var cbo = (DataGridViewComboBoxCell)grdList.Rows[i].Cells["colLineDiscountType"];
            //                //    cbo.Value = "N";
            //                //}

            //                grdList.Rows[i].Cells["colLineDiscount"].Value = 0;
            //                CalculateTotal();
            //            }
            //            else //u-online
            //            {
            //                grdList.Rows[i].Cells["colLineDiscountType"].Style.BackColor = Color.White;
            //                grdList.Rows[i].Cells["colLineDiscount"].Style.BackColor = Color.White;
            //            }

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.WriteLog(this.GetType());

            //    string msg = ex.Message;
            //    msg.ShowErrorMessage();
            //}
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
            allProduct = bu.tbl_Product;

            this.ClearControl(bu, docTypeCode, runDigit);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnPrintCrys.Enabled = btnPrint.Enabled;

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            InitHeader();

            grdList.AutoGenerateColumns = false;
            validateNewRow = true;

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;

            grdList.Rows.Clear();
            //AddNewRow(grdList, 0);
            grdList.AddNewRow(allProduct, initDataGridList, 0, "", 0, validateNewRow, uoms, bu, this, 0);
            isAdd = true; //Last edit by sailom.k 14/09/2021 tunning performance

            allEmp2 = bu.GetEmployee();
            allBranchWarehouse = bu.GetAllBranchWarehouse();

            var employee = allEmp2.FirstOrDefault(x => x.EmpID == Helper.tbl_Users.EmpID); //bu.GetEmployee(Helper.tbl_Users.EmpID);
            txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName, employee.LastName);
            btnShowPromotion.Enabled = true;
            btnReCalc.Enabled = true;

            ClearPromotionTemp();

            emp = employee;// bu.GetEmployee(Helper.tbl_Users.EmpID);
            supp = bu.GetSupplier(txtCustomerID.Text);

            allEmp = new Dictionary<string, string>();
            allEmp2.ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty) + x.LastName.Replace(" ", string.Empty))));

            ddlDocStatus.BindDropdownDocStatus(bu, "4");
            //txtCustomerCode.Focus();
        }

        private void PreparePOMaster(bool editFlag = false)
        {
            bu.tbl_POMaster = new tbl_POMaster();

            if (emp == null || emp.EmpID == null)
                emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            if (supp == null || supp.SupplierID == null)
                supp = bu.GetSupplier(txtCustomerID.Text);

            if (allEmp.Count == 0)
                allEmp2.ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty) + x.LastName.Replace(" ", string.Empty))));

            var selEmp = allEmp.FirstOrDefault(x => x.Value == txtEmpCode.Text.Replace(" ", string.Empty));
            //KeyValuePair<string, string> selEmp = new KeyValuePair<string, string>();
            //bu.GetEmployee().ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty))));
            //selEmp = allEmp.FirstOrDefault(x => x.Value == txtEmpCode.Text.Replace(" ", string.Empty));
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

            //if (ddlSaleArea.SelectedValue == null)
            //{
            //    saleAreaList.AddRange(bu.tbl_SalArea);
            //    ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID", null, null);
            //}

            if (ddlSaleArea.SelectedValue != null)
            {
                po.SalAreaID = ddlSaleArea.SelectedValue.ToString();
            }

            po.Address = txtBillTo.Text;
            po.ContactName = txtContact.Text;
            po.ContactTel = txtTelephone.Text;
            po.Shipto = txtBillTo.Text;

            po.CreditDay = Convert.ToInt16(nudCreditDay.Value);
            //Func<tbl_ArCustomer, bool> func = (x => x.CustomerCode == txtCustomerCode.Text);
            //var cust = bu.GetCustomer(func);
            var cust = bu.GetCustomer(txtCustomerID.Text); //edit by sailom.k 10/12/2021
            if (cust != null && cust.Count > 0)
            {
                po.CreditDay = cust[0].CreditDay;
                po.CustType = cust[0].CustomerTypeID.Value.ToString();
            }
            po.DueDate = dtpDocDate.Value.AddDays(po.CreditDay.Value);
            po.CustomerID = txtCustomerID.Text;
            po.CustName = txtCustName.Text;
            po.CustInvNO = string.IsNullOrEmpty(txtCustInvNO.Text) ? null : txtCustInvNO.Text; //05022021
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
            po.DiscountType = "A";
            po.OldDiscount = null;

            //if (pro.tbl_HQ_Promotion_Hit_Temps != null && pro.tbl_HQ_Promotion_Hit_Temps.Count > 0)
            //po.Discount = pro.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt).Value.ToDecimalN2();

            proTmp.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();
            if (proTmp.tbl_HQ_Promotion_Hit_Temps.Any(x => x.DisCountAmt.Value > 0))
                po.Discount = proTmp.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value).ToDecimalN2();
            else
                po.Discount = 0;

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

        private void PreparePODetail(int isSave, bool editFlag = false)
        {
            bu.tbl_PODetails.Clear();

            var poDts = bu.tbl_PODetails;
            DateTime crDate = DateTime.Now;
            lineTotalDisc = new Dictionary<string, decimal>();

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
                var lineTotalDiscCell = grdList.Rows[i].Cells["colLineDiscount"];

                if (prdCodeCell.IsNotNullOrEmptyCell() && prdNameCell.IsNotNullOrEmptyCell() && priceCell.IsNotNullOrEmptyCell() && discountTypeCell.IsNotNullOrEmptyCell())
                {
                    decimal _unitPrice = 0;
                    if (decimal.TryParse(priceCell.Value.ToString(), out _unitPrice))
                    {
                        if (!string.IsNullOrEmpty(prdNameCell.EditedFormattedValue.ToString()) && (discountTypeCell.Value.ToString() == "N" || discountTypeCell.Value.ToString() == "A")) //(_unitPrice > 0) //05022021
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
                            poDt.ReceivedQty = Convert.ToDecimal(orderQtyCell.EditedFormattedValue);
                            poDt.RejectedQty = 0;
                            poDt.StockedQty = 0;

                            decimal unitCost = 0;
                            //Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == poDt.OrderUom && x.ProductID == poDt.ProductID);
                            //var prdPriceGroup = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList();// bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);
                            //if (prdPriceGroup != null && prdPriceGroup.Count > 0)
                            //    unitCost = prdPriceGroup[0].BuyPrice.Value;

                            //for support pre-order-------------------------
                            var _uom = allUomSet.FirstOrDefault(x => x.UomSetID == poDt.OrderUom && x.ProductID == poDt.ProductID);
                            if (_uom != null)
                            {
                                unitCost = _uom.BaseQty;
                            }

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

                            //Distribute discount prd promotion edit by sailom 30402021-------------------------------------------------------------------------------------------------------------------------------
                            bool isDistributeDiscountPromotion = false;
                            if (pro.tbl_HQ_Promotion_Hits != null && pro.tbl_HQ_Promotion_Hits.Count > 0)
                            {
                                foreach (var item in pro.tbl_HQ_Promotion_Hits)
                                {
                                    var promotions = pro.GetHQPromotion(x => x.PromotionID == item.PromotionID);

                                    if (promotions != null && promotions.Count > 0)
                                    {
                                        var prdProItems = promotions.Where(x => x.PromotionPattern == "prd").ToList();
                                        var txnProItems = promotions.Where(x => x.PromotionPattern == "txn").ToList();

                                        if (prdProItems.Count > 0 &&
                                            prdProItems.All(x => string.IsNullOrEmpty(x.PruductGroupRewardID)) &&
                                            prdProItems.Sum(x => x.DisCountAmt) > 0) //prd + discount
                                        {
                                            var skuG = pro.GetHQSKUGroup(x => prdProItems.Select(y => y.SKUGroupID1).ToList().Contains(x.SKUGroupID));
                                            foreach (var prdItem in skuG)
                                            {
                                                if (poDt.ProductID == prdItem.SKU_ID)
                                                {
                                                    isDistributeDiscountPromotion = true;
                                                }
                                            }
                                        }
                                        if (txnProItems.Count > 0 && txnProItems[0].DisCountAmt != null && txnProItems[0].DisCountAmt > 0) //txn + discount
                                        {
                                            var excList = pro.GetHQSKUGroup_EXC().Select(x => x.SKU_ID).ToList(); //No Distribute discount to except product 02032021 by sailom
                                            if (!excList.Contains(poDt.ProductID))
                                            {
                                                isDistributeDiscountPromotion = true;
                                            }
                                        }
                                    }
                                }
                            }

                            if (isDistributeDiscountPromotion)
                            {
                                if (isSave == 1)
                                    DistributeDiscountPromotion(poDt);
                                else if (isSave == 2)
                                    DistributeDiscountPromotionTemp(poDt);

                                lineTotalDisc.Add(poDt.ProductID, poDt.LineDiscount);
                            }
                            //Distribute discount prd promotion edit by sailom 30402021-------------------------------------------------------------------------------------------------------------------------------

                            poDts.Add(poDt);

                            //if (isSave == 1)
                            //    DistributeFreePromotion(poDts, poDt);
                        }
                    }
                }
            }


            //Distribute discount 02032021 by sailom
            decimal totalDiscount = 0.00m;
            proTmp.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();

            if (proTmp.tbl_HQ_Promotion_Hit_Temps.Any(x => x.DisCountAmt.Value > 0))
                totalDiscount = proTmp.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value);

            decimal totalDisc = totalDiscount;
            //if (decimal.TryParse(totalDiscount, out totalDisc))
            {
                if (totalDiscount > 0 && lineTotalDisc.Sum(x => x.Value) > 0)
                {
                    if (totalDisc != lineTotalDisc.Sum(x => x.Value))
                    {
                        decimal diff = 0.00m;
                        diff = lineTotalDisc.Sum(x => x.Value) - totalDisc;

                        if (diff < 0)
                            diff = diff * -1;

                        var updateDisc = poDts.FirstOrDefault(x => x.ProductID == lineTotalDisc.Last().Key);
                        if (updateDisc != null)
                        {
                            if (lineTotalDisc.Sum(x => x.Value) > totalDisc)
                            {
                                updateDisc.LineDiscount -= diff;
                            }
                            else if (totalDisc > lineTotalDisc.Sum(x => x.Value))
                            {
                                updateDisc.LineDiscount += diff;
                            }
                        }
                    }
                }
            }
        }

        private void SubDistributeFreePromotion(List<tbl_PODetail> poDts, tbl_HQ_Promotion_Hit_Temp item, string sKUGroupRewardID, int? sKUGroupRewardAmt, DateTime crDate)
        {
            var itemGroup = pro.GetHQSKUGroup(x => x.SKUGroupID == sKUGroupRewardID);
            if (itemGroup.Count > 0)
            {
                var _poDt = new tbl_PODetail();
                //poDt.CopyTo(_poDt);
                if (_poDt != null)
                {
                    int _lineNo = poDts.Count > 0 ? Convert.ToInt32(poDts.Max(x => x.Line)) + 1 : 0;
                    _poDt.DocNo = bu.tbl_POMaster.DocNo;
                    _poDt.ProductID = itemGroup[0].SKU_ID;

                    var allProduct = bu.tbl_Product;
                    var freePrd = allProduct.Where(x => x.ProductID == itemGroup[0].SKU_ID).ToList(); //bu.GetProduct(x => x.ProductID == itemGroup[0].SKU_ID);
                    if (freePrd != null && freePrd.Count > 0)
                    {
                        var prd = freePrd[0];
                        _poDt.ProductName = prd.ProductName;

                        if (prd.SaleUomID == 1 || prd.SaleUomID == 2)
                            _poDt.OrderUom = 2;
                        else
                            _poDt.OrderUom = prd.SaleUomID;

                        _poDt.OrderQty = sKUGroupRewardAmt;
                        _poDt.ReceivedQty = sKUGroupRewardAmt;
                        _poDt.VatType = 0;
                        _poDt.RejectedQty = 0;
                        _poDt.StockedQty = 0;

                        _poDt.Line = Convert.ToInt16(_lineNo);
                        _poDt.LineTotal = 0;

                        var prdPrices = allProductPrice.Where(x => x.ProductUomID == prd.SaleUomID && x.ProductID == itemGroup[0].SKU_ID).ToList(); // bu.GetProductPriceGroup(x => x.ProductUomID == prd.SaleUomID && x.ProductID == itemGroup[0].SKU_ID);
                        if (prdPrices != null && prdPrices.Count > 0)
                        {
                            _poDt.LineDiscount = prdPrices[0].SellPriceVat.Value;
                            _poDt.UnitPrice = prdPrices[0].SellPriceVat.Value;
                            //_poDt.UnitCost = prdPrices[0].BuyPrice.Value;
                            //for support pre-order-------------------------
                            var _uom = allUomSet.FirstOrDefault(x => x.UomSetID == _poDt.OrderUom && x.ProductID == _poDt.ProductID);
                            if (_uom != null)
                            {
                                _poDt.UnitCost = _uom.BaseQty;
                            }
                        }
                        else
                        {
                            _poDt.LineDiscount = 0.00m;
                            _poDt.UnitPrice = 0.00m;
                            _poDt.UnitPrice = 0.00m;
                        }

                        _poDt.LineDiscountRate = 0;
                        _poDt.LineDiscountType = "F";
                        _poDt.FlagDel = false;
                        _poDt.FlagSend = false;
                        _poDt.UnitComPrice = 0;
                        _poDt.LineComTotal = 0;
                        _poDt.LineRemark = "";
                        _poDt.FreeQty = 0;
                        _poDt.FreeUom = 0;
                        _poDt.FreeUnit = 0;

                        _poDt.CustType = "";
                        _poDt.CrDate = crDate;
                        _poDt.CrUser = Helper.tbl_Users.Username;

                        _poDt.LineRemark = pro.GetHQReward(x => x.RewardID == item.RewardID)[0].RewardName;

                        if (_poDt.ProductName.Contains("ชั้นวาง")) // edit by sailom .k 17/11/2021
                        {
                            if (!string.IsNullOrEmpty(item.ShelfID) && item.ShelfID != "000")
                                bu.tbl_PODetails.Add(_poDt);
                        }
                        else
                        {
                            if (bu.tbl_PODetails.Any(x => x.ProductID == _poDt.ProductID)) { }//No Add free item //last edit by asilom .k 26/08/2022
                            else
                                bu.tbl_PODetails.Add(_poDt);
                        }
                    }
                }
            }
        }

        private void SubDistributeFreeMMCHPromotion(List<tbl_PODetail> poDts, Dictionary<string, int> mmchProList, string rewardID, DateTime crDate)
        {

            if (mmchProList.Count > 0)
            {
                foreach (var item in mmchProList)
                {
                    var _poDt = new tbl_PODetail();
                    //poDt.CopyTo(_poDt);
                    if (_poDt != null)
                    {
                        int _lineNo = poDts.Count > 0 ? Convert.ToInt32(poDts.Max(x => x.Line)) + 1 : 0;
                        _poDt.DocNo = bu.tbl_POMaster.DocNo;
                        _poDt.ProductID = item.Key;

                        var allProduct = bu.tbl_Product;
                        var freePrd = allProduct.Where(x => x.ProductID == item.Key).ToList();
                        if (freePrd != null && freePrd.Count > 0)
                        {
                            var prd = freePrd[0];
                            _poDt.ProductName = prd.ProductName;

                            if (prd.SaleUomID == 1 || prd.SaleUomID == 2)
                                _poDt.OrderUom = 2;
                            else
                                _poDt.OrderUom = prd.SaleUomID;

                            _poDt.OrderQty = item.Value;
                            _poDt.ReceivedQty = item.Value;
                            _poDt.VatType = 0;
                            _poDt.RejectedQty = 0;
                            _poDt.StockedQty = 0;

                            _poDt.Line = Convert.ToInt16(_lineNo);
                            _poDt.LineTotal = 0;

                            var prdPrices = allProductPrice.Where(x => x.ProductUomID == prd.SaleUomID && x.ProductID == item.Key).ToList(); // bu.GetProductPriceGroup(x => x.ProductUomID == prd.SaleUomID && x.ProductID == itemGroup[0].SKU_ID);
                            if (prdPrices != null && prdPrices.Count > 0)
                            {
                                _poDt.LineDiscount = prdPrices[0].SellPriceVat.Value;
                                _poDt.UnitPrice = prdPrices[0].SellPriceVat.Value;
                                //_poDt.UnitCost = prdPrices[0].BuyPrice.Value;
                                //for support pre-order-------------------------
                                var _uom = allUomSet.FirstOrDefault(x => x.UomSetID == _poDt.OrderUom && x.ProductID == _poDt.ProductID);
                                if (_uom != null)
                                {
                                    _poDt.UnitCost = _uom.BaseQty;
                                }
                            }
                            else
                            {
                                _poDt.LineDiscount = 0.00m;
                                _poDt.UnitPrice = 0.00m;
                                _poDt.UnitPrice = 0.00m;
                            }

                            _poDt.LineDiscountRate = 0;
                            _poDt.LineDiscountType = "F";
                            _poDt.FlagDel = false;
                            _poDt.FlagSend = false;
                            _poDt.UnitComPrice = 0;
                            _poDt.LineComTotal = 0;
                            _poDt.LineRemark = "";
                            _poDt.FreeQty = 0;
                            _poDt.FreeUom = 0;
                            _poDt.FreeUnit = 0;

                            _poDt.CustType = "";
                            _poDt.CrDate = crDate;
                            _poDt.CrUser = Helper.tbl_Users.Username;

                            _poDt.LineRemark = pro.GetHQReward(x => x.RewardID == rewardID)[0].RewardName;

                            if (bu.tbl_PODetails.Any(x => x.ProductID == _poDt.ProductID)) { }//No Add free item //last edit by asilom .k 26/08/2022
                            else
                                bu.tbl_PODetails.Add(_poDt);
                        }
                    }
                }
            }
        }

        private void DistributeFreePromotion(List<tbl_PODetail> poDts)
        {
            DateTime crDate = DateTime.Now;

            pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
            pro.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();

            RemoveShelfItem(); //remove null shelf

            if (pro.tbl_HQ_Promotion_Hit_Temps != null && pro.tbl_HQ_Promotion_Hit_Temps.Count > 0)
            {
                List<tbl_HQ_Promotion_Hit_Temp> proHits = pro.tbl_HQ_Promotion_Hit_Temps.Where(x => !string.IsNullOrEmpty(x.SKUGroupRewardID)).ToList();

                if (proHits != null && proHits.Count > 0)
                {
                    foreach (tbl_HQ_Promotion_Hit_Temp item in proHits)
                    {
                        var proInfo = pro.GetHQPromotion(a => a.PromotionID == item.PromotionID);
                        if (proInfo.Count > 0)
                        {
                            if (proInfo[0].PromotionType == "mmch")
                            {
                                var frm = new frmPromotionProduct();
                                SubDistributeFreeMMCHPromotion(poDts, frm.GetMMCHList(), proInfo[0].RewardID, crDate);
                            }
                            else
                            {
                                SubDistributeFreePromotion(poDts, item, item.SKUGroupRewardID, item.SKUGroupRewardAmt, crDate);

                                if (!string.IsNullOrEmpty(item.SKUGroupRewardID2))
                                {
                                    SubDistributeFreePromotion(poDts, item, item.SKUGroupRewardID2, item.SKUGroupRewardAmt2, crDate);
                                }
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
                List<tbl_HQ_Promotion_Hit> proHits = new List<tbl_HQ_Promotion_Hit>();

                proHits = pro.tbl_HQ_Promotion_Hits.Where(x => x.DocNo == poDt.DocNo && string.IsNullOrEmpty(x.SKUGroupRewardID)).ToList();

                var newObj = proHits.Select(x => new PromotionHitTempModel { SKUGroupID = x.SKUGroupID, PromotionID = x.PromotionID, DisCountAmt = x.DisCountAmt }).ToList();
                SubDistributeDiscountPro(newObj, poDt);
            }
        }

        private void DistributeDiscountPromotionTemp(tbl_PODetail poDt)
        {
            pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
            pro.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();

            if (pro.tbl_HQ_Promotion_Hit_Temps != null && pro.tbl_HQ_Promotion_Hit_Temps.Count > 0)
            {
                List<tbl_HQ_Promotion_Hit_Temp> proHits = new List<tbl_HQ_Promotion_Hit_Temp>();

                proHits = pro.tbl_HQ_Promotion_Hit_Temps.Where(x => string.IsNullOrEmpty(x.SKUGroupRewardID)).ToList();

                var newObj = proHits.Select(x => new PromotionHitTempModel { SKUGroupID = x.SKUGroupID, PromotionID = x.PromotionID, DisCountAmt = x.DisCountAmt }).ToList();
                SubDistributeDiscountPro(newObj, poDt);
            }
        }

        private void SubDistributeDiscountPro(List<PromotionHitTempModel> proHits, tbl_PODetail poDt)
        {
            Dictionary<decimal, decimal> _allDiscount = new Dictionary<decimal, decimal>();
            List<string> _lineRemarkList = new List<string>();
            var skuExcept = pro.GetHQSKUGroup_EXC();

            if (proHits != null && proHits.Count > 0)
            {
                _allDiscount = CalcTotalDiscountPro(proHits, _lineRemarkList, poDt.ProductID);

                var allHQSKUGroup = pro.GetHQSKUGroup();

                decimal totalPrdQty = 0;
                decimal totalTxnQty = 0;
                //Dictionary<string, decimal> lineTotal = new Dictionary<string, decimal>();
                decimal lineTotal = 0;
                decimal exceptAmt = 0;

                for (int i = 0; i < grdList.RowCount; i++)
                {
                    string prdID = "";
                    string uomName = "";
                    decimal _orderQty = 0.00m;
                    decimal unitPrc = 0;
                    decimal _lineTotal = 0.00m;

                    var prdCodeCell = grdList.Rows[i].Cells[0];
                    var prdNameCell = grdList.Rows[i].Cells[2];
                    var uomTypeCell = grdList.Rows[i].Cells[3];
                    var orderQtyCell = grdList.Rows[i].Cells[4];
                    var unitPriceCell = grdList.Rows[i].Cells[5];
                    var lineTotalCell = grdList.Rows[i].Cells["colTotal"];

                    if (prdCodeCell.IsNotNullOrEmptyCell() && prdNameCell.IsNotNullOrEmptyCell())
                    {
                        prdID = prdCodeCell.EditedFormattedValue.ToString();
                        uomName = uomTypeCell.EditedFormattedValue.ToString();
                        _orderQty = Convert.ToDecimal(orderQtyCell.EditedFormattedValue);
                        unitPrc = Convert.ToDecimal(unitPriceCell.EditedFormattedValue);
                        _lineTotal = Convert.ToDecimal(lineTotalCell.EditedFormattedValue);

                        //02032021 by sailom
                        if (poDt.ProductID == prdID)
                        {
                            lineTotal = _lineTotal;
                            //lineTotal.Add(poDt.ProductID, _lineTotal);
                        }

                        var excList = skuExcept.Select(x => x.SKU_ID).ToList(); //No Distribute discount to except product 02032021 by sailom
                        if (excList.Contains(prdID))
                        {
                            exceptAmt += _lineTotal;
                        }

                        decimal unitQty = 0;
                        if (unitPrc > 0)
                        {
                            var prdUOMSets = bu.GetProductUOMSet(allUomSet, prdID);
                            int _orderUom = 0;

                            var uom = allUOM.FirstOrDefault(x => x.ProductUomName == uomName);
                            if (uom != null)
                                _orderUom = Convert.ToInt32(uom.ProductUomID);

                            if (_orderUom == 1 && prdUOMSets != null && prdUOMSets.Count > 0)
                                unitQty = (_orderQty * prdUOMSets.First().BaseQty);
                            else
                                unitQty = _orderQty;

                            totalTxnQty += unitQty;
                        }

                        foreach (var item in proHits)
                        {
                            var prdList = allHQSKUGroup.Where(x => x.SKUGroupID == item.SKUGroupID).ToList();
                            if (prdList.Select(x => x.SKU_ID).ToList().Contains(prdID))
                            {
                                totalPrdQty += unitQty;
                            }
                        }
                    }
                }

                if (_allDiscount.Count > 0 && _lineRemarkList.Count > 0)
                {
                    decimal _lineDisount = 0.00m;

                    string prdID = poDt.ProductID;
                    decimal _orderQty = poDt.OrderQty.Value;

                    decimal _unitQty = 0;
                    var _prdUOMSets = bu.GetProductUOMSet(allUomSet, prdID);

                    if (poDt.OrderUom == 1 && _prdUOMSets != null && _prdUOMSets.Count > 0)
                        _unitQty = (_orderQty * _prdUOMSets.First().BaseQty);
                    else
                        _unitQty = _orderQty;

                    if (_allDiscount.First().Key > 0) //prd
                    {
                        var prdDis = _allDiscount.Sum(x => x.Key);
                        var prdDiscount = (prdDis / totalPrdQty) * (_unitQty);/////////////////////////////////////////////////////////// cal prd promotion
                        _lineDisount += prdDiscount.ToDecimalN2();
                    }
                    if (_allDiscount.Last().Value > 0) //txn
                    {
                        bool isPlusDiscount = true;

                        var txtPro = pro.GetHQPromotion(x => proHits.Select(a => a.PromotionID).ToList().Contains(x.PromotionID));
                        if (txtPro != null && txtPro.Count > 0)
                        {
                            if (txtPro.Any(x => x.PromotionPattern.ToLower() == "txn"))
                            {
                                var exSku = pro.GetHQSKUGroup_EXC(x => x.SKU_ID == poDt.ProductID);
                                if (exSku != null && exSku.Count > 0)
                                {
                                    isPlusDiscount = false;
                                }
                            }
                        }

                        if (isPlusDiscount)
                        {
                            var txnDis = _allDiscount.Sum(x => x.Value);//////////////////////////////////////////////////////////////must except prd except put-------------------------------------
                                                                        //02032021 by sailom
                            decimal totalDue = 1;
                            if (decimal.TryParse(txnAmount.Text, out totalDue))
                            {
                                totalDue = totalDue - exceptAmt;
                                _lineDisount += txnDis * (lineTotal / totalDue);
                            }

                            //var txnDiscount = (txnDis / totalTxnQty) * (_unitQty);
                            //_lineDisount += txnDiscount.ToDecimalN2();
                        }
                    }

                    poDt.LineDiscount = _lineDisount.ToDecimalN2();
                    poDt.LineRemark = string.Join(",", _lineRemarkList.Distinct().ToList());
                    if (poDt.LineDiscount > 0)
                    {
                        poDt.LineDiscountType = "A";
                        poDt.LineDiscountRate = 0;
                    }
                }
            }
        }

        private Dictionary<decimal, decimal> CalcTotalDiscountPro(List<PromotionHitTempModel> proHits, List<string> _lineRemarkList, string productID)
        {
            Dictionary<decimal, decimal> ret = new Dictionary<decimal, decimal>();
            decimal _linePrdDiscount = 0;
            decimal _lineTxnDiscount = 0;
            try
            {
                foreach (var item in proHits)
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
                                var skuGroups = pro.GetHQSKUGroup(x => x.SKU_ID == productID);

                                if (skuGroups != null && skuGroups.Count > 0)
                                {
                                    var listGroup = skuGroups.Select(x => x.SKUGroupID).ToList();
                                    if (listGroup.Contains(prdItem.SKUGroupID1))
                                    {
                                        _linePrdDiscount += item.DisCountAmt.Value;

                                        if (_lineRemarkList.All(x => x != prdItem.RewardID))
                                            _lineRemarkList.Add(prdItem.RewardID);
                                    }
                                }

                            }
                        }
                        if (txnProItems.Count > 0)
                        {
                            _lineTxnDiscount += item.DisCountAmt.Value;

                            foreach (tbl_HQ_Promotion txnItem in txnProItems)
                            {
                                if (_lineRemarkList.All(x => x != txnItem.RewardID))
                                    _lineRemarkList.Add(txnItem.RewardID);
                            }
                        }
                    }
                }

                ret.Add(_linePrdDiscount, _lineTxnDiscount);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ret;
        }

        private void PrepareInvMovement(bool editFlag = false)
        {
            bu.tbl_InvMovements.Clear();

            var invMms = bu.tbl_InvMovements;
            var poDts = bu.tbl_PODetails; //05022021 remove stock like a tablet. //bu.tbl_PODetails.Where(x => x.LineTotal != 0 || x.LineDiscountType == "F").ToList();  //for free product from promotion
            var po = bu.tbl_POMaster;
            var allProduct = bu.tbl_Product;
            //var prodGroup = bu.GetProductGroup();
            //var prodSubGroup = bu.GetProductSubGroup();

            DateTime crDate = DateTime.Now;

            foreach (var poDt in poDts)
            {
                var invMm = new tbl_InvMovement();

                invMm.ProductID = poDt.ProductID;
                invMm.ProductName = poDt.ProductName;
                invMm.RefDocNo = poDt.DocNo;
                //invMm.TrnDate = Helper.tbl_Users.RoleID == 10 ? dtpDocDate.Value.ToDateTimeFormat() : crDate.ToDateTimeFormat(); //31012021
                invMm.TrnDate = dtpDocDate.Value.ToDateTimeFormat();//last edit by sailom .k 10/08/2022 
                invMm.TrnType = "S";
                invMm.DocTypeCode = po.DocTypeCode;
                invMm.WHID = po.WHID;
                invMm.FromWHID = "";
                invMm.ToWHID = "";

                decimal unitQty = 0;

                var prdUOMSets = bu.GetProductUOMSet(allUomSet, poDt.ProductID);
                if (prdUOMSets != null && prdUOMSets.Count > 0)
                {
                    //if (poDt.OrderUom != 2) //06042021 by sailom.k
                    var minUomID = 2;
                    minUomID = allUomSet.GetMinUOM(poDt);

                    if (poDt.OrderUom != minUomID)
                        unitQty = (poDt.ReceivedQty.Value * prdUOMSets[0].BaseQty);
                    else
                        unitQty = poDt.ReceivedQty.Value;
                }
                else
                {
                    unitQty = poDt.ReceivedQty.Value;
                }

                invMm.TrnQtyIn = 0;
                invMm.TrnQtyOut = unitQty;
                invMm.TrnQty = -invMm.TrnQtyOut;
                invMm.CrDate = crDate;

                if (editFlag)
                {
                    invMm.EdDate = crDate;
                    invMm.TrnType = ddlDocStatus.SelectedValue.ToString() == "5" ? "X" : "S";
                }

                var prodItem = allProduct.FirstOrDefault(x => x.ProductID == poDt.ProductID);
                var prodGroupItem = allProdGroup.FirstOrDefault(x => x.ProductGroupID == prodItem.ProductGroupID);
                var prodSubGroupItem = allProdSubGroup.FirstOrDefault(x => x.ProductSubGroupID == prodItem.ProductSubGroupID);

                invMm.ProductGroupCode = prodGroupItem.ProductGroupCode;
                invMm.ProductGroupName = prodGroupItem.ProductGroupName;
                invMm.ProductSubGroupCode = prodSubGroupItem.ProductSubGroupCode;
                invMm.ProductSubGroupName = prodSubGroupItem.ProductSubGroupName;
                invMm.FlagSend = false;

                invMms.Add(invMm);

            }
        }

        private void PrepareInvTransaction()
        {
            bu.tbl_InvTransactions.Clear();

            var invTrs = bu.tbl_InvTransactions;
            var poDts = bu.tbl_PODetails; //05022021 remove stock like a tablet. //bu.tbl_PODetails.Where(x => x.LineTotal != 0 || x.LineDiscountType == "F").ToList();  //for free product from promotion
            var po = bu.tbl_POMaster;
            //var prod = bu.GetProduct();
            //var prodGroup = bu.GetProductGroup();
            //var prodSubGroup = bu.GetProductSubGroup();
            var comp = bu.GetCompany();

            DateTime crDate = DateTime.Now;

            foreach (var poDt in poDts)
            {
                var invtr = new tbl_InvTransaction();
                invtr.ProductID = poDt.ProductID;
                invtr.RefDocNo = poDt.DocNo;
                invtr.RefLineID = poDt.Line;
                invtr.TrnDate = crDate.ToDateTimeFormat();
                invtr.BranchFrom = comp.CompanyCode;
                invtr.WHFrom = po.WHID;
                invtr.BranchTo = comp.CompanyCode;
                invtr.WHTo = comp.CompanyCode;
                invtr.TrnType = "S";
                invtr.DocTypeCode = po.DocTypeCode;
                invtr.TrnUomID = poDt.OrderUom;
                invtr.TrnUom = null;
                invtr.BringQty = 0;

                decimal unitQty = 0;

                var prdUOMSets = bu.GetProductUOMSet(allUomSet, poDt.ProductID);
                if (prdUOMSets != null && prdUOMSets.Count > 0)
                {
                    //if (poDt.OrderUom != 2) //06042021 by sailom.k
                    var minUomID = 2;
                    minUomID = allUomSet.GetMinUOM(poDt);

                    if (poDt.OrderUom != minUomID)
                        unitQty = (poDt.ReceivedQty.Value * prdUOMSets[0].BaseQty);
                    else
                        unitQty = poDt.ReceivedQty.Value;
                }
                else
                {
                    unitQty = poDt.ReceivedQty.Value;
                }

                decimal unitCost = 0;
                //Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == poDt.OrderUom && x.ProductID == poDt.ProductID);
                //var prdPriceGroup = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList(); // bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);
                //if (prdPriceGroup != null && prdPriceGroup.Count > 0)
                //    unitCost = prdPriceGroup[0].BuyPrice.Value;

                //for support pre-order-------------------------
                var _uom = allUomSet.FirstOrDefault(x => x.UomSetID == poDt.OrderUom && x.ProductID == poDt.ProductID);
                if (_uom != null)
                {
                    unitCost = _uom.BaseQty;
                }

                invtr.TrnQtyIn = 0;
                invtr.TrnQtyOut = poDt.ReceivedQty.Value;
                invtr.TrnQty = -unitQty;
                invtr.RemainQty = 0;
                invtr.UnitPrice = poDt.UnitPrice;
                invtr.UnitCost = unitCost.ToDecimalN5(); //(poDt.LineTotal.Value / unitQty).ToDecimalN5();
                invtr.LineDiscountType = poDt.LineDiscountType;
                invtr.LineDiscount = poDt.LineDiscount;
                invtr.TrnVat = poDt.VatType;
                invtr.TrnValue = -poDt.LineTotal.Value;
                invtr.TrnTotal = po.Amount.Value;
                invtr.CostValue = -unitCost;
                invtr.Supplier = Convert.ToInt32(po.SupplierID);
                invtr.Customer = po.CustomerID;
                invtr.RefSONo = null;
                invtr.CustPONo = po.CustPONo;
                invtr.CustInvoiceNo = po.CustInvNO;
                invtr.Salesperson = Convert.ToInt32(comp.CompanyCode);
                invtr.SalAreaID = po.SalAreaID;
                invtr.ModifiedDate = crDate;
                invtr.FlagDel = false;
                invtr.FlagSend = false;
                //invtr.LineDiscount = poDt.LineDiscount;
                //invtr.LineDiscountType = poDt.LineDiscountType;

                invTrs.Add(invtr);
            }
        }

        private void PrepareInvWarehouse(bool editFlag = false)
        {
            bu.tbl_InvWarehouses.Clear();

            var invWhs = bu.tbl_InvWarehouses;
            //var poDts = bu.tbl_PODetails; //05022021 remove stock like a tablet. //bu.tbl_PODetails.Where(x => x.LineTotal != 0 || x.LineDiscountType == "F").ToList();  //for free product from promotion
            //var po = bu.tbl_POMaster;

            DateTime crDate = DateTime.Now;
            //var allWHStock = bu.GetInvWarehouse(po.WHID); //edit by sailom 13/12/2021

            //edit by sailom .k 16/12/201----------------------------------------------------
            List<tbl_InvMovement> invWhItems = new List<tbl_InvMovement>();
            List<string> prdList = new List<string>();
            for (int i = 0; i < grdList.RowCount; i++)
            {
                var prdCodeCell = grdList.Rows[i].Cells[0];
                var qtyCell = grdList.Rows[i].Cells[4];
                if (prdCodeCell.IsNotNullOrEmptyCell() && qtyCell.IsNotNullOrEmptyCell()) //check over recieve
                {
                    var _productID = prdCodeCell.EditedFormattedValue.ToString();
                    prdList.Add(_productID);
                }
            }

            if (prdList.Count > 0)
                invWhItems = bu.GetTotalStockMovement(prdList, txtWHCode.Text); //  edit by sailom 13/12/2021

            //edit by sailom .k 16/12/201----------------------------------------------------

            foreach (var item in invWhItems)
            {
                var invWh = new tbl_InvWarehouse();

                invWh.ProductID = item.ProductID;
                invWh.WHID = item.WHID;
                invWh.QtyOnHand = item.TrnQty;

                //invWh.QtyOnHand = 0;

                //decimal unitQty = 0;

                //var prdUOMSets = bu.GetProductUOMSet(allUomSet, poDt.ProductID);
                //if (prdUOMSets != null && prdUOMSets.Count > 0)
                //{
                //    //if (poDt.OrderUom != 2) //06042021 by sailom.k
                //    var minUomID = 2;
                //    minUomID = allUomSet.GetMinUOM(poDt);

                //    if (poDt.OrderUom != minUomID)
                //        unitQty = (poDt.ReceivedQty.Value * prdUOMSets[0].BaseQty);
                //    else
                //        unitQty = poDt.ReceivedQty.Value;
                //}
                //else
                //{
                //    unitQty = poDt.ReceivedQty.Value;
                //}

                //SetQtyOnHand(invWh, allWHStock, unitQty, poDt.ProductID, po.WHID, editFlag);

                invWh.QtyOnOrder = 0;
                invWh.QtyOnBackOrder = 0;
                invWh.QtyInTransit = 0;
                invWh.QtyOutTransit = 0;
                invWh.QtyOnReject = 0;
                invWh.MinimumQty = 0;
                invWh.MaximumQty = 0;
                invWh.ReOrderQty = 0;
                invWh.CrDate = crDate;
                invWh.CrUser = Helper.tbl_Users.Username;

                if (editFlag)
                {
                    invWh.EdDate = crDate;
                    invWh.EdUser = Helper.tbl_Users.Username;
                }

                invWh.FlagDel = false;
                invWh.FlagSend = false;

                invWhs.Add(invWh);

                //if (invWhs.Any(x => x.ProductID == poDt.ProductID && x.WHID == po.WHID))
                //{
                //    var whItem = invWhs.FirstOrDefault(x => x.ProductID == poDt.ProductID && x.WHID == po.WHID);
                //    whItem.QtyOnHand += invWh.QtyOnHand;
                //}
                //else
                //    invWhs.Add(invWh);
            }
        }

        private void PreparePromotion()
        {
            if (pro.tbl_HQ_Promotion_Hit_Temps != null && pro.tbl_HQ_Promotion_Hit_Temps.Count > 0)
            {
                pro.tbl_HQ_Promotion_Hits = new List<tbl_HQ_Promotion_Hit>();
                var _pro = new List<tbl_HQ_Promotion_Hit>();

                DateTime crDate = DateTime.Now;

                foreach (var pItem in pro.tbl_HQ_Promotion_Hit_Temps)
                {
                    var p = new tbl_HQ_Promotion_Hit();

                    p.DocNo = bu.tbl_POMaster.DocNo;
                    p.PromotionID = pItem.PromotionID;
                    p.RoundHit = pItem.RoundHit;
                    p.SKUGroupID = pItem.SKUGroupID;
                    p.DisCountAmt = pItem.DisCountAmt;
                    //p.SKUGroupID = pItem.
                    p.ShelfID = pItem.ShelfID;
                    p.SKUGroupRewardID = pItem.SKUGroupRewardID;
                    p.SKUGroupRewardAmt = pItem.SKUGroupRewardAmt;
                    p.SKUGroupRewardID2 = pItem.SKUGroupRewardID2;
                    p.SKUGroupRewardAmt2 = pItem.SKUGroupRewardAmt2;
                    p.RewardID = pItem.RewardID;
                    p.CrDate = crDate;
                    p.CrUser = Helper.tbl_Users.Username;
                    p.FlagDel = false;
                    p.FlagSend = false;

                    _pro.Add(p);
                }

                pro.tbl_HQ_Promotion_Hits = _pro;
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
                p.SKUGroupRewardID2 = pItem.PruductGroupRewardID2;
                p.SKUGroupRewardAmt2 = pItem.PruductGroupRewardAmt2;
                p.RewardID = pItem.RewardID;
                p.CrDate = crDate;
                p.CrUser = Helper.tbl_Users.Username;
                p.FlagDel = false;
                p.FlagSend = false;

                _pro.Add(p);
            }

            pro.tbl_HQ_Promotion_Hit_Temps = _pro;
        }

        private void PrepareArCustomerShelf()
        {
            var cDate = DateTime.Now;
            var po = bu.tbl_POMaster;
            if (po != null)
            {
                bu.tbl_ArCustomerShelfs = new List<tbl_ArCustomerShelf>();
                var proShelf = pro.tbl_HQ_Promotion_Hits.Where(x => !string.IsNullOrEmpty(x.ShelfID)).ToList();

                foreach (var item in proShelf)
                {
                    var obj = new tbl_ArCustomerShelf();
                    obj.CustomerID = po.CustomerID;
                    obj.ShelfID = item.ShelfID;
                    obj.WHID = po.WHID;

                    var skuGroupRewardID = pro.GetHQSKUGroup(x => x.SKUGroupID == item.SKUGroupRewardID);
                    if (skuGroupRewardID != null && skuGroupRewardID.Count > 0)
                    {
                        obj.ProductID = skuGroupRewardID[0].SKU_ID;
                    }

                    obj.FlagNew = true;
                    obj.FlagEdit = false;
                    obj.CrDate = cDate;
                    obj.CrUser = item.CrUser;
                    obj.EdDate = null;
                    obj.EdUser = null;
                    obj.FlagDel = false;
                    obj.FlagSend = false;

                    bu.tbl_ArCustomerShelfs.Add(obj);
                }
            }
        }

        private void PrepareInvMaster(tbl_POMaster po)
        {
            bu.tbl_IVMaster = new tbl_IVMaster();

            var bwh = bu.GetAllBranchWarehouse(x => !x.FlagDel && x.WHType != 0); // edit by sailom .k 03/03/2022 for support pre-order

            var iv = bu.tbl_IVMaster;

            iv.DocNo = txtCustInvNO.Text;
            iv.RevisionNo = 0;
            iv.DocTypeCode = "V";
            iv.DocStatus = po.DocStatus;
            iv.DocDate = po.DocDate;
            iv.DocRef = po.DocNo;
            iv.StatusInOut = null;
            iv.SupplierID = "0";
            iv.SuppName = "";
            iv.WHID = po.WHID;

            if (bwh != null && bwh.Count > 0)
            {
                string saleEmpID = bwh.First(x => x.WHID == po.WHID).SaleEmpID;
                iv.EmpID = saleEmpID;
                iv.SaleEmpID = saleEmpID;
            }
            else
            {
                iv.EmpID = "";
                iv.SaleEmpID = "";
            }

            iv.SalAreaID = po.SalAreaID;
            iv.Address = po.Address;
            iv.ContactName = po.ContactName;
            iv.ContactTel = po.ContactTel;
            iv.Shipto = po.Shipto;
            iv.CreditDay = po.CreditDay;
            iv.CustType = po.CustType;
            iv.DueDate = po.DueDate;
            iv.CustomerID = po.CustomerID;
            iv.CustName = po.CustName;
            iv.CustInvNO = "";
            iv.CustInvDate = null;
            iv.CustPODate = po.CustPODate;
            iv.CustPONo = po.DocRef;
            iv.Remark = "สร้างใบกำกับจากการใบกำกับภาษีอย่างย่อ : " + po.DocNo;
            iv.Comment = po.Comment;
            iv.OldAmount = 0;
            iv.Amount = po.Amount;
            iv.OldExcVat = 0;
            iv.ExcVat = po.ExcVat;
            iv.OldIncVat = 0;
            iv.IncVat = po.IncVat;
            iv.VatRate = po.VatRate;
            iv.VatAmt = po.VatAmt;
            iv.Freight = 0.00m;
            iv.DiscountType = "";
            iv.OldDiscount = null;
            iv.Discount = bu.tbl_POMaster.Discount; //pro.GetAllData().Sum(x => x.DisCountAmt);
            iv.TotalDue = po.TotalDue;
            iv.ApprovedBy = null;
            iv.ApprovedDate = null;
            iv.PayType = 0;
            iv.CrDate = DateTime.Now;
            iv.CrUser = Helper.tbl_Users.Username;
            iv.EdDate = null;
            iv.EdUser = Helper.tbl_Users.Username;
            iv.FlagDel = false;
            iv.FlagSend = false;
            iv.OldTotalCom = 0.00m;
            iv.TotalCom = 0.00m;
            iv.CNType = 0;

            Func<tbl_Branch, bool> whFunc = (x => x.BranchCode == txtWHCode.Text.Split('V')[0]);
            var wh = bu.GetBranch(whFunc);
            if (wh != null && wh.Count > 0)
                iv.FromWHCode = wh[0].VanCode;

            iv.FromLocCode = "VC4STV" + iv.WHID.Split('V')[1].ToString();
            iv.ToWHCode = null;
            iv.ToLocCode = null;
        }

        private void PrepareInvDetail(List<tbl_PODetail> poDts)
        {
            bu.tbl_IVDetails = new List<tbl_IVDetail>();
            //var allUomSet = bu.tbl_ProductUomSet;
            var allProduct = bu.tbl_Product;
            //var allProductPrice = bu.tbl_ProductPriceGroup;

            var ivDts = bu.tbl_IVDetails;
            DateTime crDate = DateTime.Now;

            //Find line number from product edit by sailom .k 15/09/2022----------------
            var listProductSeq = new Dictionary<string, short>();
            listProductSeq = bu.FindProductLineNumber(poDts);
            //Find line number from product edit by sailom .k 15/09/2022----------------

            short index = 0;
            foreach (tbl_PODetail _podt in poDts)
            {
                var ivDt = new tbl_IVDetail();
                ivDt.DocNo = bu.tbl_IVMaster.DocNo;
                ivDt.Line = listProductSeq.Count > 0 ? listProductSeq.First(x => x.Key == _podt.ProductID).Value : index; // _podt.Line; //Find line number from product edit by sailom .k 15/09/2022
                ivDt.ProductID = _podt.ProductID;
                ivDt.ProductName = _podt.ProductName;

                //if (_podt.OrderUom == 1 || _podt.OrderUom == 2)
                //    ivDt.OrderUom = 2;// _podt.OrderUom;
                //else
                //    ivDt.OrderUom = _podt.OrderUom;

                //decimal unitQty = 0;

                //var prdUOMSets = bu.GetProductUOMSet(ivDt.ProductID);
                //if (_podt.OrderUom == 1 && prdUOMSets != null && prdUOMSets.Count > 0)
                //    unitQty = (_podt.OrderQty.Value * prdUOMSets.First().BaseQty);
                //else
                //    unitQty = _podt.OrderQty.Value;

                decimal unitQty = 0;

                var prdUOMSets = allUomSet.Where(x => x.ProductID == _podt.ProductID).ToList();
                if (_podt.OrderUom == 1 && prdUOMSets != null && prdUOMSets.Count > 0)
                    unitQty = (_podt.OrderQty.Value * prdUOMSets.First().BaseQty);
                else
                    unitQty = _podt.OrderQty.Value;

                int tmpOrderUom = _podt.OrderUom;

                var prd = allProduct.FirstOrDefault(x => x.ProductID == _podt.ProductID);
                if (prd != null)
                {
                    var saleUom = allUomSet.FirstOrDefault(x => x.ProductID == prd.ProductID && x.UomSetID == prd.SaleUomID);
                    if (saleUom != null)
                        tmpOrderUom = saleUom.UomSetID; //25012021
                }

                ivDt.OrderUom = tmpOrderUom; //25012021

                ivDt.OrderQty = unitQty;
                ivDt.ReceivedQty = unitQty;

                decimal unitPrice = 0;
                Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == tmpOrderUom && x.ProductID == _podt.ProductID);
                var prdPriceGroup = allProductPrice.FirstOrDefault(tbl_ProductPriceGroupPre); //29012021
                if (prdPriceGroup != null)
                    unitPrice = prdPriceGroup.SellPriceVat.Value;

                ivDt.UnitPrice = unitPrice;

                ivDt.RejectedQty = 0;
                ivDt.StockedQty = 0;
                ivDt.LineTotal = _podt.LineTotal;
                ivDt.UnitCost = 0;
                ivDt.VatType = _podt.VatType;
                ivDt.LineDiscountType = _podt.LineDiscountType;
                ivDt.LineDiscount = _podt.LineDiscount;
                ivDt.CustType = _podt.CustType;
                ivDt.CrDate = crDate;
                ivDt.CrUser = Helper.tbl_Users.Username;
                ivDt.EdDate = null;
                ivDt.EdUser = Helper.tbl_Users.Username;
                ivDt.FlagDel = false;
                ivDt.FlagSend = false;
                ivDt.UnitComPrice = 0;
                ivDt.LineComTotal = 0;

                ivDts.Add(ivDt);

                index++;
            }
        }

        private void SetQtyOnHand(tbl_InvWarehouse invWh, List<tbl_InvWarehouse> allWHStock, decimal unitQty, string productID, string whID, bool editFlag)
        {
            //var invWhItem = bu.GetInvWarehouse(productID, whID);
            var invWhItem = allWHStock.Where(x => x.ProductID == productID && x.WHID == whID).ToList(); //edit by sailom .k 13/12/2021
            if (editFlag)
            {
                if (invWhItem != null && invWhItem.Count > 0)
                {
                    if (ddlDocStatus.SelectedValue.ToString() == "5")
                        invWh.QtyOnHand = invWhItem[0].QtyOnHand + unitQty;
                }
            }
            else
            {
                if (invWhItem != null && invWhItem.Count > 0)
                {
                    invWh.QtyOnHand = invWhItem[0].QtyOnHand - unitQty;
                }
                else
                {
                    invWh.QtyOnHand = -unitQty;
                }
            }
        }

        private void Save()
        {
            try
            {
                if (!ValidateSave())
                    return;

                string docno = string.Empty;
                bool editFlag = true;
                int ret = 0;

                bool checkEditMode = bu.CheckExistsPO(txdDocNo.Text);
                if (checkEditMode)
                {
                    string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                    string title = "ยืนยันการบันทึก!!";
                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    bu = new TabletSales();

                    //CalcPromotion(true);
                    //var promotions = pro.GetAllData(x => x.DocNo == txdDocNo.Text);
                    //if (promotions != null && promotions.Count > 0)
                    //{
                    //    BindPOPromotion(promotions);
                    //}

                    //validate edit PO and not change status by sailom/k 07/10/2021
                    var chkPO = bu.GetPOMaster(txdDocNo.Text);
                    if (chkPO != null && chkPO.DocStatus == "4")
                    {
                        if (ddlDocStatus.SelectedValue.ToString() == chkPO.DocStatus)
                        {
                            string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                            msg.ShowInfoMessage();

                            return;
                        }
                    }


                    docno = txdDocNo.Text;
                    editFlag = true;
                    bu.tbl_DocRunning = new List<tbl_DocRunning>();

                    bu.tbl_POMaster = null;
                    bu.tbl_POMaster = bu.GetPOMaster(docno);

                    //validate docno by sailom.k 27-05-2021
                    if (bu.tbl_POMaster.DocNo.Length < 12)
                    {
                        this.ShowDocNoProcessErr();
                        return;
                    }

                    bu.tbl_POMaster.DocStatus = ddlDocStatus.SelectedValue.ToString();
                    bu.tbl_POMaster.Remark = txtRemark.Text;
                    bu.tbl_POMaster.Comment = txtComment.Text;
                    bu.tbl_POMaster.EdDate = DateTime.Now;
                    bu.tbl_POMaster.EdUser = Helper.tbl_Users.Username;

                    bu.tbl_InvMovements.Clear();
                    bu.tbl_InvMovements.AddRange(bu.GetInvMovement(docno));

                    if (ddlDocStatus.SelectedValue.ToString() == "5")
                        bu.tbl_InvMovements.ForEach(x => x.TrnType = "X");

                    bu.tbl_InvTransactions.Clear();
                    bu.tbl_InvTransactions.AddRange(bu.GetInvTransaction(docno));

                    if (ddlDocStatus.SelectedValue.ToString() == "5")
                        bu.tbl_InvTransactions.ForEach(x => x.TrnType = "X");

                    bu.tbl_InvWarehouses.Clear();

                    //string whID = bu.tbl_POMaster.WHID;
                    //var poDts = bu.GetPODetails(docno);

                    //var allWHStock = bu.GetInvWarehouse(whID); //edit by sailom 13/12/2021

                    //foreach (var poDt in poDts)
                    //{
                    //    var invWh = new tbl_InvWarehouse();
                    //    //var invWhs = bu.GetInvWarehouse(poDt.ProductID, whID);
                    //    var invWhs = allWHStock.Where(x => x.ProductID == poDt.ProductID && x.WHID == whID).ToList(); //bu.GetInvWarehouse(poDt.ProductID, whID);

                    //    invWh = invWhs[0];

                    //    decimal unitQty = 0;

                    //    var prdUOMSets = bu.GetProductUOMSet(allUomSet, poDt.ProductID);
                    //    if (prdUOMSets != null && prdUOMSets.Count > 0)
                    //    {
                    //        //if (poDt.OrderUom != 2) //06042021 by sailom.k
                    //        var minUomID = 2;
                    //        minUomID = allUomSet.GetMinUOM(poDt);

                    //        if (poDt.OrderUom != minUomID)
                    //            unitQty = (poDt.ReceivedQty.Value * prdUOMSets[0].BaseQty);
                    //        else
                    //            unitQty = poDt.ReceivedQty.Value;
                    //    }
                    //    else
                    //    {
                    //        unitQty = poDt.ReceivedQty.Value;
                    //    }

                    //    SetQtyOnHand(invWh, allWHStock, unitQty, poDt.ProductID, whID, editFlag);

                    //    bu.tbl_InvWarehouses.Add(invWh);
                    //}

                    //pro.tbl_HQ_Promotion_Hits = new List<tbl_HQ_Promotion_Hit>();
                    //pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                    ////pro.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();
                    ////pro.tbl_HQ_Promotion_Hits = pro.GetAllData(x => x.DocNo == docno);
                    //PreparePromotion();

                    //ret = bu.UpdateData(docTypeCode);
                    ret = bu.PerformUpdateData(docTypeCode); //edit by sailom .k 14/12/2021

                    BindVanSalesData(docno);
                    //edit by sailom .k 16/12/2021
                    bu.tbl_POMaster = new tbl_POMaster();
                    bu.tbl_PODetails.Clear();
                    bu.tbl_InvMovements.Clear();
                    bu.tbl_InvTransactions.Clear();
                    bu.tbl_DocRunning.Clear();
                    PrepareInvWarehouse();//edit by sailom .k 16/12/2021

                    ret = bu.PerformUpdateData(); //edit by sailom .k 16/12/2021

                    //ลบข้อมูลใน arCustomer Shlef เมื่อยกเลิกเอกสาร //by sailom 24/08/2021
                    //if (ret == 1)
                    //{
                    //    var shelfList = new List<tbl_ArCustomerShelf>();
                    //    foreach (var item in poDts)
                    //    {

                    //    }

                    //    shelfList = buShelf.GetCustomerShelf(x => x.ShelfID == shelf_id && x.CustomerID == txtCustomerID.Text && x.FlagDel == false);

                    //    if (shelfList.Count > 0)
                    //    {
                    //        shelfList[0].FlagDel = true;
                    //        shelfList[0].EdDate = DateTime.Now;
                    //        shelfList[0].EdUser = Helper.tbl_Users.Username;

                    //        foreach (var item in shelfList)
                    //        {
                    //            ret = buShelf.UpdateData(item);
                    //        }
                    //    }
                    //}

                    //if (ret == 1 && pro.tbl_HQ_Promotion_Hits != null && pro.tbl_HQ_Promotion_Hits.Count > 0)
                    //{
                    //    ret = pro.RemoveTempData();
                    //    if (ret == 1)
                    //    {
                    //        ret = pro.RemoveData();
                    //        if (ret == 1)
                    //            ret = pro.UpdateData();
                    //    }
                    //}
                }
                else
                {
                    string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                    string title = "ยืนยันการบันทึก!!";
                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    bu = new TabletSales();
                    CalcPromotion(true);

                    //ValidateInputShelf();

                    docno = bu.GenDocNo(docTypeCode, txtWHCode.Text);
                    editFlag = false;
                    bu.PrepareDocRunning(docTypeCode);

                    PreparePOMaster(editFlag);

                    //validate docno by sailom.k 27-05-2021
                    if (bu.tbl_POMaster.DocNo.Length < 12)
                    {
                        this.ShowDocNoProcessErr();
                        return;
                    }

                    PreparePromotion();
                    PreparePODetail(1, editFlag); //Calc Pro

                    DistributeFreePromotion(bu.tbl_PODetails);

                    PrepareInvMovement(editFlag);
                    PrepareInvTransaction(); //Calc Pro
                    //PrepareInvWarehouse();

                    //ret = bu.UpdateData(docTypeCode);
                    ret = bu.PerformUpdateData(docTypeCode); //edit by sailom .k 14/12/2021

                    if (ret == 1 && pro.tbl_HQ_Promotion_Hits != null && pro.tbl_HQ_Promotion_Hits.Count > 0)
                    {
                        pro.tbl_HQ_Promotion_Hits = new List<tbl_HQ_Promotion_Hit>();
                        pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                        pro.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();

                        RemoveShelfItem();

                        PreparePromotion();
                        PrepareArCustomerShelf();

                        ret = pro.RemoveTempData();
                        if (ret == 1)
                        {
                            ret = pro.RemoveData();
                            if (ret == 1)
                                ret = pro.UpdateData();
                            if (ret == 1)
                                ret = bu.UpdateCustomerShelf();
                        }
                    }

                    BindVanSalesData(docno);

                    //edit by sailom .k 16/12/2021
                    bu.tbl_POMaster = new tbl_POMaster();
                    bu.tbl_PODetails.Clear();
                    bu.tbl_InvMovements.Clear();
                    bu.tbl_InvTransactions.Clear();
                    bu.tbl_DocRunning.Clear();
                    PrepareInvWarehouse();//edit by sailom .k 16/12/2021

                    ret = bu.PerformUpdateData(); //edit by sailom .k 16/12/2021
                }

                if (ret == 1)
                {
                    this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, txdDocNo.Text);
                    btnPrintCrys.Enabled = btnPrint.Enabled;

                    txdDocNo.Text = docno;

                    grdList.CellContentClick -= grdList_CellContentClick;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    ddlDocStatus.SelectedValue.ToString().CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
                    //CheckCancelDoc(ddlDocStatus.SelectedValue.ToString());

                    btnUpdateAddress.Enabled = true;
                    btnGenCustIVNo.Enabled = true;
                    btnReCalc.Enabled = false;
                    btnShowPromotion.Enabled = false;

                    btnEdit.Enabled = ddlDocStatus.SelectedValue.ToString() == "4";
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

        private void ValidateInputShelf()
        {
            var allProduct = bu.tbl_Product;

            bool isValidateShelf = true;
            var proFreeList = pro.tbl_HQ_Promotion_Hit_Temps.Where(x => !string.IsNullOrEmpty(x.SKUGroupRewardID)).ToList();
            foreach (var proFree in proFreeList)
            {
                var skuGroups = pro.GetHQSKUGroup(x => x.SKUGroupID == proFree.SKUGroupRewardID).ToList();
                foreach (var g in skuGroups)
                {
                    var prd = allProduct.Where(x => x.ProductID == g.SKU_ID).ToList(); // bu.GetProduct(x => x.ProductID == g.SKU_ID).ToList();
                    if (prd != null && prd.Count > 0)
                    {
                        bool isShelf = prd.Any(x => x.Flavour == "ชั้นวาง" || x.ProductName.Contains("ชั้นวาง"));

                        if (string.IsNullOrEmpty(proFree.ShelfID) && isShelf)
                        {
                            isValidateShelf = false;
                            break;
                        }
                    }
                }
            }

            if (!isValidateShelf)
            {
                var message = "กรุณากรอกเลขที่ Shelf !!!";
                message.ShowWarningMessage();
            }
        }

        private void RemoveShelfItem()
        {
            var allProduct = bu.tbl_Product;

            var proFreeList = pro.tbl_HQ_Promotion_Hit_Temps.Where(x => !string.IsNullOrEmpty(x.SKUGroupRewardID)).ToList();
            foreach (var proFree in proFreeList)
            {
                var skuGroups = pro.GetHQSKUGroup(x => x.SKUGroupID == proFree.SKUGroupRewardID).ToList();
                foreach (var g in skuGroups)
                {
                    var prd = allProduct.Where(x => x.ProductID == g.SKU_ID).ToList(); // bu.GetProduct(x => x.ProductID == g.SKU_ID).ToList();
                    if (prd != null && prd.Count > 0)
                    {
                        Dictionary<tbl_HQ_Promotion_Hit_Temp, bool> validateShelf = new Dictionary<tbl_HQ_Promotion_Hit_Temp, bool>();
                        bool isRemove = false;

                        var _cust = new tbl_ArCustomerShelf().Select(x => x.ProductID == g.SKU_ID && x.FlagDel == false && x.CustomerID == txtCustomerID.Text).ToList();
                        if (_cust != null && _cust.Count > 0)
                        {
                            var _custGroup = _cust.GroupBy(x => new { x.CustomerID, x.ProductID }).Select(a => a.First()).ToList();

                            foreach (var item in _custGroup)
                            {
                                var cust = _cust.Where(x => x.ProductID == item.ProductID).ToList();
                                if (cust != null && cust.Count > 0)
                                {
                                    if (cust.Count >= 2)
                                    {
                                        validateShelf.Add(proFree, false);
                                        break;
                                    }
                                }
                            }

                            if (validateShelf.Any(x => !x.Value))
                                isRemove = true;
                        }

                        bool isShelf = prd.Any(x => x.Flavour == "ชั้นวาง" || x.ProductName.Contains("ชั้นวาง"));

                        if (isRemove && string.IsNullOrEmpty(proFree.ShelfID) && isShelf)
                            isRemove = true;

                        if ((isRemove && isShelf) || proFree.ShelfID == "000")
                            pro.tbl_HQ_Promotion_Hit_Temps.Remove(proFree);
                    }
                }
            }
        }

        private void SaveIV()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCustInvNO.Text) && !string.IsNullOrEmpty(txdDocNo.Text))
                {
                    string msg = "ใบกำกับภาษีถูกสร้างไปแล้ว!!!";
                    msg.ShowInfoMessage();
                    return;
                }
                if (ddlDocStatus.SelectedValue.ToString() == "5")
                {
                    string msg = "เอกสารถูกยกเลิกแล้ว ไม่สามารถออกใบกำกับภาษีได้!!!";
                    msg.ShowInfoMessage();
                    return;
                }

                bool checkEditMode = bu.CheckExistsPO(txdDocNo.Text);

                if (checkEditMode)
                {
                    string cfMsg = "ต้องการสร้างใบกำกับภาษีใช่หรือไม่?";
                    string title = "ยืนยันการสร้าง!!";
                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    Cursor.Current = Cursors.WaitCursor;

                    //PreparePOMaster();
                    //PreparePODetail(1);
                    bu.tbl_POMaster = bu.GetPOMaster(txdDocNo.Text);
                    bu.tbl_PODetails = bu.GetPODetails(txdDocNo.Text); //Last edit by sailom .k 07/01/2022

                    List<tbl_PODetail> temptbl_PODetails = new List<tbl_PODetail>();
                    temptbl_PODetails = SerializeHelper.CloneObject<List<tbl_PODetail>>(bu.tbl_PODetails);

                    tbl_POMaster temptbl_POMaster = new tbl_POMaster();
                    temptbl_POMaster = SerializeHelper.CloneObject<tbl_POMaster>(bu.tbl_POMaster);

                    bu = new TabletSales();

                    txtCustInvNO.Text = bu.GenDocNo("V", txtWHCode.Text);

                    if (!string.IsNullOrEmpty(txtCustInvNO.Text))
                    {
                        temptbl_POMaster.CustInvNO = txtCustInvNO.Text;
                        temptbl_POMaster.FlagSend = false;
                    }

                    bu.tbl_POMaster = temptbl_POMaster;
                    PrepareInvMaster(bu.tbl_POMaster);

                    PrepareInvDetail(temptbl_PODetails);

                    string docNo = bu.tbl_POMaster.DocNo;
                    string custInvNO = bu.tbl_POMaster.CustInvNO;
                    //bu.tbl_POMaster = new tbl_POMaster();
                    //bu.tbl_PODetails.Clear();

                    //int ret = bu.UpdateData();
                    int ret = bu.PerformUpdateData(); //edit by sailom .k 14/12/2021

                    if (ret == 1)
                    {
                        if (!string.IsNullOrEmpty(txtCustomerID.Text) && !string.IsNullOrEmpty(custInvNO) && !string.IsNullOrEmpty(docNo))
                        {
                            bool result = bu.UpdateCustomerSAPCode(txtCustomerID.Text, custInvNO, docNo); //update customer SAP code
                            if (result)
                                ret = 1;
                        }
                    }
                    else
                        ret = 0;

                    //revers iv master - details edit by sailom.k 09/09/2022
                    if (ret == 0)
                    {
                        if (bu.tbl_IVMaster != null && !string.IsNullOrEmpty(bu.tbl_IVMaster.DocNo))
                        {
                            bu.ReverseVE(bu.tbl_IVMaster.DocNo);
                        }
                    }
                    //revers iv master - details edit by sailom.k 09/09/2022

                    if (ret == 1)
                    {
                        Cursor.Current = Cursors.Default;

                        btnShowVE.Enabled = true;
                        string msg = "สร้างใบกำกับภาษีเรียบร้อยแล้ว";
                        msg.ShowInfoMessage();
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        btnShowVE.Enabled = false;
                        string msg = "สร้างใบกำกับภาษีไม่สำเร็จ";
                        msg.ShowInfoMessage();
                        txtCustInvNO.Text = "";
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    string msg = "กรุณาบันทึกเอกสารอย่างย่อ ก่อนสร้างใบกำกับภาษี!!!";
                    msg.ShowInfoMessage();
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
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
            var allProduct = bu.tbl_Product;

            var cDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Ticks;
            var docDate = new DateTime(dtpDocDate.Value.Year, dtpDocDate.Value.Month, dtpDocDate.Value.Day).Ticks;

            if (Helper.tbl_Users.RoleID != 10 && dtpDocDate.Value != null && docDate < cDate)
            {
                string message = "ห้ามเลือกวันที่ย้อนหลัง !!!";
                message.ShowWarningMessage();
                ret = false;
            }

            if (ret)
            {
                if (Helper.tbl_Users.RoleID != 10 && !dtpDocDate.ValidateEndDay(bu))
                {
                    string message = "ระบบปิดวันไปแล้ว ไม่สามารถเลือกวันที่นี้ได้ !!!";
                    message.ShowWarningMessage();
                    ret = false;
                }
            }


            if (ret) //validate customer shelf
            {
                var _cust = bu.GetCustomerShelfByCustID(txtCustomerID.Text); // new tbl_ArCustomerShelf().Select(x => x.FlagDel == false && x.CustomerID == txtCustomerID.Text).ToList();
                var _custGroup = _cust.GroupBy(x => new { x.CustomerID, x.ProductID }).Select(a => a.First()).ToList();
                bool validateShelf = true;

                foreach (var item in _custGroup)
                {
                    var cust = _cust.Where(x => x.ProductID == item.ProductID).ToList();
                    if (cust != null && cust.Count > 0)
                    {
                        if (cust.Count >= 2)
                        {
                            validateShelf = false;
                            break;
                        }
                    }
                }

                if (!validateShelf)
                {
                    string message = "ร้านค้านี้ได้ Shelf ครบตามจำนวนที่กำหนดแล้ว ห้ามแถม Shelf ให้ลูกค้าอีก !!!";
                    message.ShowWarningMessage();
                }
            }

            if (ret)
            {
                errList.SetErrMessageList(txtCustomerID, lblCustomerCode);
                errList.SetErrMessageList(txtBillTo, lblBillTo);
                errList.SetErrMessageList(txtContact, lblContact);
                errList.SetErrMessageList(txtEmpCode, lblEmpCode);
                errList.SetErrMessageList(txtWHCode, lblWHCode);

                if (errList.Count == 0)
                {
                    //Func<tbl_ArCustomer, bool> func = (x => x.CustomerCode == txtCustomerCode.Text);
                    //var sup = bu.GetCustomer(func);
                    var sup = bu.GetCustomer(txtCustomerID.Text); //edit by sailom.k 10/12/2021
                    if (sup == null)
                    {
                        string t = lblCustomerCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtCustomerID.ErrorTextBox();
                    }
                    if (sup.Count == 0) //edit by sailom 21-06-2021
                    {
                        string message = "ไม่พบข้อมูลร้านค้านี้ในระบบ!!!";
                        errList.Add(message);
                        txtCustomerID.ErrorTextBox();
                    }

                    //Func<tbl_BranchWarehouse, bool> whFunc = (x => x.WHID == txtWHCode.Text);
                    //var wh = bu.GetBranchWarehouse(whFunc);
                    var wh = bu.GetBranchWarehouse(txtWHCode.Text); //Last edit by sailom .k 07/02/2022
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
                    //var allProduct = bu.tbl_Product;
                    ret = grdList.ValiadteDataGridView(allProduct, 0, 3, 4, 5, bu, "", false);

                    if (ret)
                    {
                        List<bool> checkEmptyCell = new List<bool>();
                        List<bool> checkPrdCode = new List<bool>();
                        List<bool> checkQty = new List<bool>();
                        List<bool> checkPrice = new List<bool>();

                        var validateRLList = new List<ValidateRL>();

                        if (grdList.RowCount > 0)
                        {
                            var invWhItem = bu.ValidatetStockMovement();

                            for (int i = 0; i < grdList.RowCount; i++)
                            {
                                var prdCodeCell = grdList.Rows[i].Cells[0];
                                var qtyCell = grdList.Rows[i].Cells[4];
                                var uomCell = grdList.Rows[i].Cells[3];

                                if (prdCodeCell.IsNotNullOrEmptyCell() && qtyCell.IsNotNullOrEmptyCell()) //check over recieve
                                {
                                    decimal qtyValue = 0;
                                    if (decimal.TryParse(qtyCell.EditedFormattedValue.ToString(), out qtyValue))
                                    {
                                        if (qtyValue > 0)
                                        {
                                            var productID = prdCodeCell.EditedFormattedValue.ToString();
                                            var productUomName = uomCell.EditedFormattedValue.ToString();
                                            Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => x.ProductUomName == productUomName);
                                            var prdUOMs = allUOM.Where(tbl_ProductUomPre).ToList();

                                            decimal unitQty = 0;

                                            var prdUOMSets = bu.GetProductUOMSet(allUomSet, productID);
                                            if (prdUOMSets != null && prdUOMSets.Count > 0 && prdUOMs != null && prdUOMs.Count > 0)
                                            {
                                                if (prdUOMs[0].ProductUomID == prdUOMSets[0].UomSetID) //if (prdUOMs[0].ProductUomID != 2) //15022021
                                                    unitQty = (qtyValue * prdUOMSets[0].BaseQty);
                                                else
                                                    unitQty = qtyValue;
                                            }
                                            else
                                                unitQty = qtyValue;

                                            //var invWhItem = bu.GetInvWarehouse(productID, txtWHCode.Text);
                                            //decimal whQty = 0;

                                            //if (invWhItem != null && invWhItem.Count > 0)
                                            //    whQty = invWhItem[0].QtyOnHand;


                                            decimal whQty = 0;

                                            if (invWhItem != null && invWhItem.Count > 0)
                                                whQty = invWhItem.Where(x => x.WHID == txtWHCode.Text && x.ProductID == productID).Sum(x => x.TrnQty);

                                            if (unitQty > whQty)
                                            {
                                                validateRLList.Add(new ValidateRL
                                                {
                                                    ProductID = productID,
                                                    StockQty = whQty,
                                                    InputQty = unitQty
                                                });

                                            }
                                        }
                                    }
                                }
                            }

                            if (validateRLList.Count > 0)
                            {
                                var message = "";
                                var tempMsg = "";

                                tempMsg += string.Format("ไม่สามารถทำรายการสินค้านี้ได้ \n\n");
                                foreach (var item in validateRLList)
                                {
                                    tempMsg += string.Format("--> เนื่องจากสินค้า {0} ใน Stock มีแค่ {1} หีบ \n", item.ProductID, item.StockQty.ToStringN2());
                                }
                                message = tempMsg;

                                message.ShowWarningMessage();
                            }
                        }
                    }

                }
            }

            return ret;
        }

        #endregion

        #region event methods

        private void frmVanSales_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InitialData();

            ddlDocStatus.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (bu.tbl_POMaster != null)
            {
                if (Helper.tbl_Users.RoleID != 10 && bu.tbl_POMaster.FlagSend == true)
                {
                    string message = "ไม่สามารถแก้ไขเอกสารได้ เนื่องจากได้ส่งข้อมูลเข้า Data Center แล้ว";
                    message.ShowWarningMessage();
                    return;
                }
            }

            btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, txdDocNo.Text);
            btnPrintCrys.Enabled = btnPrint.Enabled;

            txdDocNo.DisableTextBox(false);
            txtCustomerID.DisableTextBox(false);
            txtCustPONo.DisableTextBox(false);
            btnSearchCust.Enabled = true;
            txtCustInvNO.DisableTextBox(false);

            txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");
            ddlDocStatus.Enabled = true;
            txtRemark.Enabled = true;
            txtComment.Enabled = true;
            btnCancel.Enabled = true;
            btnReCalc.Enabled = false;
            btnShowPromotion.Enabled = false;

            validateNewRow = true;

            if (!string.IsNullOrEmpty(txdDocNo.Text) && bu.CheckExistsPO(txdDocNo.Text))
                grdList.CellContentClick -= grdList_CellContentClick;
            else
            {
                grdList.CellContentClick -= grdList_CellContentClick;
                grdList.CellContentClick += grdList_CellContentClick;
            }

            dtpDocDate.Focus();
            isAdd = true; //Last edit by sailom.k 14/09/2021 tunning performance
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            //last edit by sailom .k 15/06/2022--------------------
            if (!string.IsNullOrEmpty(txdDocNo.Text))
                bu.GetDocData(txdDocNo.Text, docTypeCode);
            //last edit by sailom .k 15/06/2022--------------------

            btnCopy.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnPrint, txdDocNo.Text);
            btnPrintCrys.Enabled = btnPrint.Enabled;

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == "4"; };
            ddlDocStatus.SelectedValueDropdownList(condition);
            ddlDocStatus.Enabled = false;

            txdDocNo.Text = string.Empty;
            txtCustPONo.Text = string.Empty;
            txtCustInvNO.Text = string.Empty;

            validateNewRow = true;
            ddlSaleArea.Enabled = true;

            ClearPromotionTemp();

            allEmp2 = bu.GetEmployee();
            allBranchWarehouse = bu.GetAllBranchWarehouse();

            emp = allEmp2.FirstOrDefault(x => x.EmpID == Helper.tbl_Users.EmpID); //bu.GetEmployee(Helper.tbl_Users.EmpID);
            supp = bu.GetSupplier(txtCustomerID.Text);

            allEmp = new Dictionary<string, string>();
            allEmp2.ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty) + x.LastName.Replace(" ", string.Empty))));

            //Reset Discount edit by sailom.k 19/04/2022
            if (grdList.RowCount > 0)
            {
                for (int i = 0; i < grdList.RowCount; i++)
                {
                    grdList.Rows[i].Cells[8].Value = 0;
                }
            }

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //CalcPromotion(false);

            //PreparePODetail(2);

            //DistributeFreePromotion(bu.tbl_PODetails);

            //BindPODetail(bu.tbl_PODetails);

            //for (int i = 0; i < grdList.Rows.Count; i++)
            //{
            //    CalculateRow(grdList, i);
            //}

            //CalculateTotal();

            //decimal totalDiscount = 0.00m;
            //proTmp.tbl_HQ_Promotion_Hit_Temps = proTmp.GetAllData();

            //if (proTmp.tbl_HQ_Promotion_Hit_Temps.Any(x => x.DisCountAmt.Value > 0))
            //    totalDiscount = proTmp.tbl_HQ_Promotion_Hit_Temps.Sum(x => x.DisCountAmt.Value);

            //txnDiscountAmt.Text = totalDiscount.ToStringN2();

            Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ClearControl(bu, docTypeCode, runDigit);
            btnAdd.Enabled = true;

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnPrintCrys.Enabled = btnPrint.Enabled;

            validateNewRow = true;

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            btnCancel.EnableButton(btnSearchDoc);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;

            ClearPromotionTemp();

            ddlDocStatus.BindDropdownDocStatus(bu, "4");
            ddlDocStatus.Enabled = false;

            grdList.CellContentClick -= grdList_CellContentClick;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

            string cfMsg = "ต้องการพิมพ์โดยที่ไม่ดูรายงานใช่หรือไม่?";
            string title = "ยืนยันการพิมพ์!!";
            var confirmResult = FlexibleMessageBox.Show(cfMsg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (confirmResult == DialogResult.Yes)
            {
                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocNo", txdDocNo.Text);
                //this.OpenCrystalReportsPopup("ใบกำกับภาษีอย่างย่อ", "Form_IV.rpt", "Form_IV", _params);

                this.OpenReportingReportsNonPreViewPopup("ใบกำกับภาษีอย่างย่อ", "Form_IV.rdlc", "Form_IV", _params); //Reporting service by sailom 30/11/2021
            }
            else
            {
                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocNo", txdDocNo.Text);
                //this.OpenCrystalReportsPopup("ใบกำกับภาษีอย่างย่อ", "Form_IV.rpt", "Form_IV", _params);

                this.OpenReportingReportsPopup("ใบกำกับภาษีอย่างย่อ", "Form_IV.rdlc", "Form_IV", _params); //Reporting service by sailom 30/11/2021
            }
        }

        private void btnPrintCrys_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txdDocNo.Text))
            {
                FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocNo", txdDocNo.Text);
                this.OpenCrystalReportsPopup("ใบกำกับภาษีอย่างย่อ", "Form_IV.rpt", "Form_IV", _params);
            }
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
            this.OpenIVDocPopup("ใบกำกับสินค้า/กำกับภาษี", docTypeCode);
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
            this.OpenCustomerPopup(searchCustControls, "เลือกลูกค้า", x => x.FlagDel == false);

            FilterSaleArea(txtCustomerID.Text);

            //ValidateUCustomer(txtCustomerCode.Text);
        }

        private void btnCustInfo_Click(object sender, EventArgs e)
        {
            string _custID = txtCustomerID.Text;

            if (!string.IsNullOrEmpty(_custID))
            {
                MainForm mfrm = null;
                frmCustomerInfo frm = new frmCustomerInfo();
                bool isActive = false;

                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name.ToLower() == "mainform")
                    {
                        mfrm = (MainForm)f;
                    }

                    if (f.Name.ToLower() == frm.Name.ToLower())
                    {
                        f.Activate();
                        isActive = true;
                    }
                }

                if (!isActive)
                {
                    frm.MdiParent = mfrm;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                    frm.BindCustomerInfo(_custID);
                }
            }
        }

        private void btnGenCustIVNo_Click(object sender, EventArgs e)
        {
            SaveIV();
        }

        private void btnReCalc_Click(object sender, EventArgs e)
        {
            ClearPromotionTemp();
            CalcPromotionBeforeSave();
        }

        private void TxtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    this.BindData("Customer", searchCustControls, txt.Text);
                }

                FilterSaleArea(txt.Text);

                //ValidateUCustomer(txt.Text);
            }
        }

        private void TxtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("BranchWarehouse", searchBWHControls, txt.Text);

                FilterSaleArea(txt.Text);
            }
        }

        private void TxtWHCode_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (!string.IsNullOrEmpty(txt.Text))
            {
                var bwh = bu.GetBranchWarehouse(x => x.WHID == txt.Text);
                if (bwh != null)
                {
                    this.BindData("BranchWarehouse", searchBWHControls, txt.Text);

                    FilterSaleArea(txt.Text);
                }
            }
        }

        private void TxdDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BindVanSalesData(txdDocNo.Text);
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
            //Last edit by sailom.k 14/09/2021 tunning performance
            if (isAdd) //when click add button
            {
                if (e.ColumnIndex == 0)
                    grdList.ModifyComboBoxCell(allProduct, e.RowIndex, bu, 3, 0);
            }
            else //when search data
            {
                if (e.ColumnIndex == 0)
                {
                    //last edit by sailom .k 15/06/2022-----------------
                    if (allPODetails.Count == 0)
                        allPODetails = bu.tbl_PODetails;
                    //last edit by sailom .k 15/06/2022-----------------

                    if (allPODetails.Count > 0 && prodUOMs.Count > 0)
                    {
                        var _prodUOMs = new List<tbl_ProductUom>();
                        var prdID = grdList.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString();
                        if (!string.IsNullOrEmpty(prdID))
                        {
                            _prodUOMs.AddRange(bu.GetProductUOM(new string[] { prdID }.ToList()));

                            _prodUOMs = _prodUOMs.Count == 0 ? prodUOMs : _prodUOMs;

                            grdList.ModifyComboBoxCell_Tunning(allProduct, e.RowIndex, bu, 3, 0, _prodUOMs);
                        }

                    }
                }
            }
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
                var cell2 = grd.Rows[currentRowIndex].Cells[2];

                if (curentColIndex == 0)
                {
                    bool isNewRow = true;
                    if (cell0.IsNotNullOrEmptyCell())
                    {
                        validateNewRow = true;

                        if (Helper.tbl_Users.RoleID != 10) //aloow super admin add duplicate item for support promotion 24022021
                        {
                            var checkDup = grd.ValidateDuplicateSKU(cell0.EditedFormattedValue.ToString(), 0, currentRowIndex, ref validateNewRow);
                            if (!checkDup)
                            {
                                GridViewHelper.ShowDupSKUMessage();
                                cell0.Value = "";
                                grd.Rows.RemoveAt(currentRowIndex);
                                isNewRow = false;
                                validateNewRow = false;
                            }
                            //if (cell0.EditedFormattedValue != null && !string.IsNullOrEmpty(cell0.EditedFormattedValue.ToString()))
                            //{
                            //    //last edit by sailom.k 15/07/2022-----------------------------
                            //    var checkDup = grd.ValidateDuplicateSKU(cell0.EditedFormattedValue.ToString(), 0, currentRowIndex, ref validateNewRow);
                            //    if (!checkDup)
                            //    {
                            //        GridViewHelper.ShowDupSKUMessage();
                            //        cell0.Value = "";
                            //        grd.Rows.RemoveAt(currentRowIndex);
                            //        isNewRow = false;
                            //        validateNewRow = false;
                            //        return;
                            //    }

                            //    validateNewRow = grd.ValidateNewRowWhenCopy(cell0.EditedFormattedValue.ToString(), 0, currentRowIndex);
                            //    if (!validateNewRow)
                            //    {
                            //        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 3];
                            //        grd.BeginEdit(true);
                            //        return;
                            //    }
                            //    //last edit by sailom.k 15/07/2022-----------------------------
                            //}
                        }

                        if (isNewRow)
                        {
                            grd.BindDataGridViewRow(dataGridList, initDataGridList, dt, 0, cell0.EditedFormattedValue.ToString(), currentRowIndex, 0, ref validateNewRow, "IMProduct", false);
                            //if (!validateNewRow)
                            //{
                            //    isNewRow = false;
                            //}
                        }
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
                else if (curentColIndex == 5 || curentColIndex == 8)
                {
                    isAdd = true;

                    if (cell2.IsNotNullOrEmptyCell())
                    {
                        if ((grd.RowCount - 1) == currentRowIndex)
                        {
                            validateNewRow = true;

                            if (Helper.tbl_Users.RoleID != 10) //aloow super admin add duplicate item for support promotion 24022021
                            {
                                var checkDup = grd.ValidateDuplicateSKU(cell0.EditedFormattedValue.ToString(), 0, currentRowIndex, ref validateNewRow);
                                if (!checkDup)
                                {
                                    validateNewRow = false;
                                }
                            }

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

                        //ValidateUCustomer(txtCustomerCode.Text, currentRowIndex);
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

        private void btnShowPromotion_Click(object sender, EventArgs e)
        {
            CalcPromotionBeforeSave();
        }

        private void btnUpdateAddress_Click(object sender, EventArgs e)
        {
            bool checkEditMode = bu.CheckExistsPO(txdDocNo.Text);
            if (checkEditMode)
            {
                string cfMsg = "ต้องการปรับปรุงที่อยู่ใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                bool ret = bu.UpdateCustomerAddress(txdDocNo.Text);
                if (ret)
                {
                    //bu.tbl_POMaster = bu.GetPOMaster(txdDocNo.Text);
                    //if (bu.tbl_POMaster != null)
                    {
                        var cust = bu.GetCustomer(txtCustomerID.Text);
                        if (cust != null && cust.Count > 0)
                        {
                            txtBillTo.Text = cust[0].BillTo;
                            txtCustName.Text = cust[0].CustName;
                            txtTelephone.Text = cust[0].Telephone;
                            txtContact.Text = cust[0].Contact;

                            string message = "เปลี่ยนแปลงที่อยู่เรียบร้อยแล้ว!!!";
                            message.ShowInfoMessage();
                        }
                    }
                }
            }
            else //last edit by sailom 27/01/2022 
            {
                var cust = bu.GetCustomer(txtCustomerID.Text);
                if (cust != null && cust.Count > 0)
                {
                    txtBillTo.Text = cust[0].BillTo;
                    txtCustName.Text = cust[0].CustName;
                    txtTelephone.Text = cust[0].Telephone;
                    txtContact.Text = cust[0].Contact;
                }
            }
        }

        private void frmVanSales_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void btnShowVE_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustInvNO.Text))
            {
                MainForm mfrm = null;
                var frmList = Application.OpenForms;
                if (frmList != null && frmList.Count > 0)
                {
                    frmVE frm = new frmVE();
                    bool isActive = false;

                    foreach (Form f in frmList)
                    {
                        if (f.Name.ToLower() == "mainform") //(f.Name == "frmOD")
                        {
                            mfrm = (MainForm)f;
                        }

                        if (f.Name.ToLower() == frm.Name.ToLower())
                        {
                            f.Activate();
                            isActive = true;
                        }
                    }

                    if (!isActive)
                    {
                        frm.MdiParent = mfrm;
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.WindowState = FormWindowState.Minimized;

                        frm.Show();
                        frm.docTypeCode = "V";
                        frm.BindVEData(txtCustInvNO.Text);
                        frm.WindowState = FormWindowState.Maximized;
                    }
                }
            }
        }

        #endregion

        private void CreateGridBtnList()
        {
            contextMenuStrip1 = new ContextMenuStrip();

            contextMenuStrip1.Items.Clear();
            contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(grdMenu_Opening);

            grdList.ContextMenuStrip = contextMenuStrip1;
        }

        void grdMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Acquire references to the owning control and item.
            Control c = contextMenuStrip1.SourceControl as Control;
            ToolStripDropDownItem tsi = contextMenuStrip1.OwnerItem as ToolStripDropDownItem;

            // Clear the ContextMenuStrip control's Items collection.
            contextMenuStrip1.Items.Clear();

            // Populate the ContextMenuStrip control with its default items.
            var printImage = new Bitmap(AllCashUFormsApp.Properties.Resources.copyBtn).ImageToByte();

            List<tbl_MstMenu> menuList = new List<tbl_MstMenu>();
            tbl_MstMenu m = new tbl_MstMenu();
            m.MenuID = 300;
            m.MenuName = "prdDetails";
            m.MenuText = "รายละเอียดสินค้า";
            m.FormName = "prdDetails";
            m.MenuImage = printImage;
            menuList.Add(m);

            printImage = new Bitmap(AllCashUFormsApp.Properties.Resources.depo).ImageToByte();
            m = new tbl_MstMenu();
            m.MenuID = 301;
            m.MenuName = "prdStock";
            m.MenuText = "ตรวจสอบสินค้าเคลื่อนไหว";
            m.FormName = "prdStock";
            m.MenuImage = printImage;
            menuList.Add(m);

            printImage = new Bitmap(AllCashUFormsApp.Properties.Resources.invoiceFull).ImageToByte();
            m = new tbl_MstMenu();
            m.MenuID = 302;
            m.MenuName = "repStock";
            m.MenuText = "รายงานสินค้าคงเหลือ แยกตามคลัง";
            m.FormName = "repStock";
            m.MenuImage = printImage;
            menuList.Add(m);

            foreach (var item in menuList)
            {
                contextMenuStrip1.Items.Add(item.MenuText, item.MenuImage.byteArrayToImage(), ToolGrdStripMenuItem_Click);
            }

            // Set Cancel to false. 
            // It is optimized to true based on empty entry.
            e.Cancel = false;
        }

        private void ToolGrdStripMenuItem_Click(object sender, EventArgs e)
        {
            string menuStripTxt = ((System.Windows.Forms.ToolStripItem)sender).Text;
            if (grdList.CurrentCell.RowIndex != -1 && grdList.CurrentCell.ColumnIndex != -1)
            {
                int rowIndex = grdList.CurrentCell.RowIndex;
                int colIndex = grdList.CurrentCell.ColumnIndex;
                string productID = grdList.Rows[rowIndex].Cells[0].EditedFormattedValue.ToString();

                if (!string.IsNullOrEmpty(productID))
                {
                    switch (menuStripTxt)
                    {
                        case "รายละเอียดสินค้า":
                            {
                                MainForm mfrm = null;
                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Name.ToLower() == "mainform")
                                    {
                                        mfrm = (MainForm)f;
                                    }
                                }

                                frmProductInfo frm = new frmProductInfo();
                                frm.MdiParent = mfrm;
                                frm.StartPosition = FormStartPosition.CenterParent;
                                frm.WindowState = FormWindowState.Maximized;
                                frm.Show();
                                frm.BindProductInfo(productID);
                            }
                            break;
                        case "ตรวจสอบสินค้าเคลื่อนไหว":
                            {
                                MainForm mfrm = null;
                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Name.ToLower() == "mainform")
                                    {
                                        mfrm = (MainForm)f;
                                    }
                                }

                                frmProductMovement frm = new frmProductMovement();
                                frm.MdiParent = mfrm;
                                frm.StartPosition = FormStartPosition.CenterParent;
                                frm.WindowState = FormWindowState.Maximized;
                                frm.Show();
                                frm.BindProductMovement(txtWHCode.Text, txtWHCode.Text, productID, true);

                            }
                            break;
                        case "รายงานสินค้าคงเหลือ แยกตามคลัง":
                            {
                                var cDate = DateTime.Now.AddDays(1);
                                Dictionary<string, object> _params = new Dictionary<string, object>();

                                _params.Add("@DateFr", cDate);
                                _params.Add("@DateTo", cDate);
                                _params.Add("@YearFr", -1);
                                _params.Add("@MonthFr", -1);
                                _params.Add("@YearTo", -1);
                                _params.Add("@MonthTo", -1);
                                //Doc Status--------------------------------------
                                _params.Add("@DocStatus", "4");
                                _params.Add("@BranchID", bu.tbl_Branchs[0].BranchID);
                                _params.Add("@WHID", txtWHCode.Text);
                                //WHID--------------------------------------
                                //ProductSubGroupID--------------------------------------
                                _params.Add("@ProductSubGroupID", "");
                                //ProductSubGroupID--------------------------------------
                                //ProductID--------------------------------------
                                _params.Add("@ProductID", productID);
                                //ProductID--------------------------------------
                                _params.Add("@FromWH", txtWHCode.Text);
                                _params.Add("@ToWH", txtWHCode.Text);
                                //FromWH And ToWH--------------------------------------
                                //SalAreaID--------------------------------------
                                _params.Add("@SalAreaID", "");
                                //SalAreaID--------------------------------------
                                //ShopTypeID--------------------------------------
                                _params.Add("@ShopTypeID", 0);

                                this.OpenExcelReportsPopup(menuStripTxt, "proc_RPTStock.XSLT", "proc_RPTStock_XSLT", _params, true);
                            }
                            break;
                        default: break;
                    }
                }
            }

        }
    }
}
