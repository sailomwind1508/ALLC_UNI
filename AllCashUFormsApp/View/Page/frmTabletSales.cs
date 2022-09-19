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
    public partial class frmTabletSales : Form, IFormEvent
    {
        MenuBU menuBU = new MenuBU();
        TabletSales bu = new TabletSales();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        List<tbl_ProductUom> uoms = new List<tbl_ProductUom>();
        List<tbl_SalArea> saleAreaList = new List<tbl_SalArea>();
        List<tbl_SalAreaDistrict> salAreaDistrictList = new List<tbl_SalAreaDistrict>();
        Dictionary<string, decimal> listDiscount = new Dictionary<string, decimal>();
        bool isAdd = false;

        bool validateNewRow = true;
        public string docTypeCode = ""; //last edit by adisorn 08/11/2021
        int runDigit = 0;

        List<Control> searchCustControls = new List<Control>();
        List<Control> searchBWHControls = new List<Control>();
        List<Control> searchEmpControls = new List<Control>();
        List<string> readOnlyControls = new List<string>();

        int[] cellEdit = new int[] { 0, 3, 4, 5, 7, 8 };
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
        //List<tbl_DiscountType> allLineDiscountType = new List<tbl_DiscountType>();

        List<tbl_PODetail> allPODetails = new List<tbl_PODetail>();
        List<tbl_ProductUom> prodUOMs = new List<tbl_ProductUom>();

        tbl_Employee emp = new tbl_Employee();
        tbl_ApSupplier supp = new tbl_ApSupplier();
        Dictionary<string, string> allEmp = new Dictionary<string, string>();

        public frmTabletSales()
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
            txtCustomerID.KeyDown += TxtCustomerCode_KeyDown;

            dtpDocDate.ValueChanged += DtpDocDate_ValueChanged;
            nudCreditDay.ValueChanged += NudCreditDay_ValueChanged;
        }

        #region private methods

        public void InitPage()
        {
            var menu = bu.tbl_AdmFormList.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }

            var documentType = bu.tbl_DocumentType.FirstOrDefault(x => x.DocTypeCode.Trim() == "IV");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                runDigit = documentType.DocFormat.Length;// - 2; edit by sailom 03/10/2021 for support tablet docno 14 digit
                this.ClearControl(docTypeCode, runDigit);
                txtComment.Text = documentType.DocRemark;
            }

            validateNewRow = true;
            btnAdd.Enabled = false;

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
            dtpDueDate.SetDateTimePickerFormat();

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

        }

        public void BindTabletSalesData(string ivDocNo)
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

                CalculateTotal(true); //for support pre-order by sailom.k 12/04/2022
            }

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            btnSearchDoc.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
            btnPrintCrys.Enabled = btnPrint.Enabled;

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;

            grdList.CellContentClick -= grdList_CellContentClick;

            //bool checkEditMode = bu.CheckExistsPO(ivDocNo);
            //po.DocStatus.CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);

            btnUpdateAddress.Enabled = true;
            btnGenCustIVNo.Enabled = true;
            btnCopy.Enabled = false;
            btnEdit.Enabled = ddlDocStatus.SelectedValue.ToString() == "4";
            btnGenCustIVNo.Enabled = ddlDocStatus.SelectedValue.ToString() == "4" && string.IsNullOrEmpty(txtCustInvNO.Text);
            btnShowVE.Enabled = !string.IsNullOrEmpty(txtCustInvNO.Text);
            btnCustInfo.Enabled = !string.IsNullOrEmpty(txtCustomerID.Text);

            CreateGridBtnList();

            if (Helper.tbl_Users.RoleID == 10)
            {
                btnRecovery.Visible = true;
                btnRecovery.Enabled = true;
            }
            else
                btnRecovery.Visible = false;
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

            //Last edit by sailom.k 14 / 09 / 2021 tunning performance
            var employee = bu.GetEmployee(po.SaleEmpID);
            if (employee != null)
            {
                txtEmpCode.Text = employee.TitleName + " " + employee.FirstName + " " + employee.LastName;
                txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName, employee.LastName);
            }

            //var emp = bu.GetEmployee().FirstOrDefault(x => x.EmpID == po.SaleEmpID);
            //if (emp != null)

            txtWHCode.Text = po.WHID;
            Func<tbl_BranchWarehouse, bool> func = (x => x.WHID == po.WHID);
            var wh = bu.GetBranchWarehouse(func);
            if (wh != null)
            {
                txtWHName.Text = wh.WHName;
            }

            //FilterSaleArea(po.CustomerID);
            FilterSaleArea(po.SalAreaID); //edit by sailom .k 13/12/2021
            ddlDocStatus.BindDropdownDocStatus(bu, po.DocStatus);
     
            txtComment.Text = po.Comment;

            txnAmount.Text = (po.Amount.Value < po.IncVat.Value ? po.IncVat.Value : po.Amount.Value).ToStringN2(); //edit by sailom 19/04/2022
            txnDiscountAmt.Text = po.Discount.Value.ToStringN2();

            decimal amt = 0;
            decimal excVat = 0;
            decimal discount = 0;
            decimal vatRate = 0;
            if (po.Amount != null)
                amt = (po.Amount.Value < po.IncVat.Value ? po.IncVat.Value : po.Amount.Value); //edit by sailom 19/04/2022
            if (po.ExcVat != null)
                excVat = po.ExcVat.Value;
            if (po.Discount != null)
                discount = po.Discount.Value;
            if (po.VatRate != null)
                vatRate = po.VatRate.Value;

            decimal incVat = ((amt - excVat - discount) * 100) / (100 + vatRate);

            txnBeforeVat.Text = incVat.ToStringN2();
            txnVatAmt.Text = po.VatAmt.Value.ToStringN2();
            lblVatType.Text = po.VatRate == null ? "0" : po.VatRate.Value.ToStringN0();
            txnExcVat.Text = po.ExcVat.Value.ToStringN2();
            txnTotalDue.Text = po.TotalDue.ToStringN2();

            //txnAmount.Text = (po.Amount.Value < po.IncVat.Value ? po.IncVat.Value : po.Amount.Value).ToStringN2(); //edit by sailom 19/04/2022
            //txnDiscountType.Text = "0";// po.DiscountType;
            //txnDiscountAmt.Text = po.Discount.Value.ToStringN2();
            //txnBeforeVat.Text = (po.IncVat.Value - po.Discount.Value - po.VatAmt.Value - po.ExcVat.Value).ToStringN2(); //(po.IncVat.Value - po.VatAmt.Value).ToStringN2();
            //txnVatAmt.Text = po.VatAmt.Value.ToStringN2();
            //lblVatType.Text = po.VatRate != null ? po.VatRate.Value.ToStringN0() : "";
            //txnExcVat.Text = po.ExcVat.Value.ToStringN2();
            //txnTotalDue.Text = po.TotalDue.ToStringN2();
        }

        private void BindPODetail(List<tbl_PODetail> poDts)
        {
            grdList.Rows.Clear();

            isAdd = false;
            //Last edit by sailom.k 14/09/2021 tunning performance-------------------------
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
                grdList.Rows[i].Cells[9].Value = poDts[i].OrderQty * poDts[i].UnitPrice;// poDts[i].LineTotal; //for support pre-order by sailm.k 12/04/2022
                grdList.Rows[i].Cells[10].Value = poDts[i].OrderUom;

                if (poDts[i].OrderUom != -1)
                {
                    string prdCode = poDts[i].ProductID;
                    var prdPriceList = allProductPrice.Where(x => x.ProductUomID == poDts[i].OrderUom && x.ProductID == prdCode).ToList();

                    if (prdPriceList != null && prdPriceList.Count > 0)
                        grdList.Rows[i].Cells[11].Value = prdPriceList[0].SellPrice.Value;
                }
            }
        }

        public void BindSearchProduct(DataTable productDT, int rowIndex)
        {
            validateNewRow = true;
            grdList.ValidateDuplicateSKU(productDT.Rows[0]["ProductID"].ToString(), 0, rowIndex, ref validateNewRow);


            grdList.BindDataGrid(allProduct, dataGridList, initDataGridList, productDT, 0, rowIndex, validateNewRow, this, uoms, bu, 0);
        }

        private decimal CalcReceiveQry(List<tbl_POMaster> poList, List<tbl_PODetail> poDts, string productID, decimal? orderQty, bool hasRefOD)
        {
            decimal ret = 0;

            if (poList != null && poList.Count > 0)
            {
                decimal totalQty = 0;
                foreach (var po in poList)
                {
                    if (poDts != null && poDts.Count > 0)
                    {
                        foreach (var poDt in poDts.Where(x => x.DocNo == po.DocNo && x.ProductID == productID).ToList())
                        {
                            totalQty += poDt.ReceivedQty.Value;
                        }
                    }
                }

                ret = hasRefOD ? (orderQty.Value - totalQty) : totalQty;
            }
            else
            {
                ret = orderQty.Value;
            }

            return ret;
        }

        private void CalculateRow(DataGridView grd, int rowIndex)
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
                var allPrdUOM = bu.GetUOM(); //bu.GetUOM(tbl_ProductUomPre);
                var prdUOM = allPrdUOM.FirstOrDefault(x => x.ProductUomName == cell3.EditedFormattedValue.ToString());
                if (prdUOM != null)
                {
                    orderUom = prdUOM.ProductUomID;
                    if (orderUom != -1)
                    {
                        string prdCode = cell0.EditedFormattedValue.ToString();
                        Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == orderUom && x.ProductID == prdCode);
                        var prdPriceList = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList(); //bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);

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

                    if (cell6.FormattedValue.ToString() != "0")
                        discountSellPrice = bu.CalDiscountType(cell7.EditedFormattedValue.ToString(), cell8.EditedFormattedValue.ToString(), orderQty, unitSellPrice);
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

                cell9.Value = ((orderQty * unitPrice) - discount).ToDecimalN2().ToStringN2();
            }

            CalculateTotal();
        }

        private void CalculateTotal(bool isEdit = false)
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
                var vatCell = grdList.Rows[i].Cells[6];
                var lineTotalCell = grdList.Rows[i].Cells[9];
                var discountTypeCell = grdList.Rows[i].Cells[7];
                var discountAmtCell = grdList.Rows[i].Cells[8];
                var sellPriceCell = grdList.Rows[i].Cells[11];

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
                        var _qty = Convert.ToDecimal(cell4.EditedFormattedValue);

                        //var prdUOM = allPrdUOM.FirstOrDefault(x => x.ProductUomName == cell3.EditedFormattedValue.ToString());
                        //if (prdUOM != null)
                        //{
                        //    var orderUom = prdUOM.ProductUomID;
                        //    if (orderUom != -1)
                        //    {
                        //        string prdCode = cell0.EditedFormattedValue.ToString();
                        //        Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == orderUom && x.ProductID == prdCode);
                        //        var prdPriceList = allPrdPriceList.Where(tbl_ProductPriceGroupPre).ToList();

                        //        if (prdPriceList != null && prdPriceList.Count > 0)
                        //        {
                        //            if (listDiscount.Count > 0)
                        //            {
                        //                var _discount = listDiscount.FirstOrDefault(x => x.Key == prdCode);
                        //                if (_discount.Key != null)
                        //                {
                        //                    var _sellPricetmp = ((prdPriceList[0].SellPrice.Value * _qty) - _discount.Value);

                        //                    incVat += _sellPricetmp;
                        //                    vatAmt += (_sellPricetmp * (vatRate / 100));
                        //                }
                        //            }
                        //            else
                        //            {
                        //                incVat += prdPriceList[0].SellPrice.Value * _qty; //Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                        //                vatAmt += (prdPriceList[0].SellPriceVat.Value - prdPriceList[0].SellPrice.Value) * _qty;
                        //            }
                        //        }
                        //    }
                        //}
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

            //edit by sailom 19/04/2022
            var _incVat = Convert.ToDecimal(txnAmount.Text);
            amount = (amount < _incVat ? _incVat : amount);

            excVat = excVat.ToDecimalN2();
            totalDue = amount - totalDiscount;
            incVat = ((amount - excVat - totalDiscount) * 100) / (100 + vatRate);
            vatAmt = incVat * (vatRate / 100).ToDecimalN2();

            lblVatType.Text = vatRate.ToDecimalN0().ToString();
            txnAmount.Text = amount.ToDecimalN2().ToStringN2();

            //edit by sailom 19/04/2022----------------------------
            if (isEdit)
            {
                var po = bu.tbl_POMaster;
                if (po != null)
                {
                    //last edit by sailom .k 04/08/2022
                    txnBeforeVat.Text = (po.DocRef == "IM" ? (po.Amount.Value - po.ExcVat.Value - po.Discount.Value - po.VatAmt.Value) : incVat).ToStringN2();//(incVat - po.ExcVat.Value - po.Discount.Value - po.VatAmt.Value)).ToStringN2();
                    txnVatAmt.Text = po.VatAmt.Value.ToStringN2();
                }
            }
            else
            {
                txnBeforeVat.Text = incVat.ToStringN2();
                txnVatAmt.Text = vatAmt.ToStringN2();
            }
            //edit by sailom 19/04/2022----------------------------

            txnExcVat.Text = excVat.ToStringN2();
            txnTotalDue.Text = totalDue.ToStringN2();
            txnCommission.Text = "0.00";

            //excVat = excVat.ToDecimalN2();
            //incVat = incVat.ToDecimalN2();

            ////vatAmt = listVAT0List.Sum(x => x.Value).ToDecimalN2(); //(incVat * (vatRate / 100.00m)).ToDecimalN2();
            //totalDue = incVat + vatAmt + excVat; // (amount - totalDiscountAmt).ToDecimalN2();
            //lblVatType.Text = vatRate.ToDecimalN0().ToString();

            //txnAmount.Text = amount.ToDecimalN2().ToStringN2();
            //txnDiscountType.Text = "0"; //discountType;
            //txnDiscountAmt.Text = "0.00";  //totalDiscountAmt.ToDecimalN2().ToStringN2();
            //txnBeforeVat.Text = incVat.ToStringN2(); // (incVat - vatAmt).ToDecimalN2().ToStringN2();
            //txnVatAmt.Text = vatAmt.ToStringN2();
            //txnExcVat.Text = excVat.ToStringN2();
            //txnTotalDue.Text = totalDue.ToStringN2();
            //txnCommission.Text = "0.00";
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
                var emp = bu.GetEmployee(empID);
                if (emp != null)
                    txtEmpCode.Text = emp.TitleName + " " + emp.FirstName + " " + emp.LastName;

                Func<tbl_BranchWarehouse, bool> whFunc = (x => x.WHID == cust[0].WHID);
                var wh = bu.GetBranchWarehouse(whFunc);
                if (wh != null)
                {
                    txtWHCode.Text = wh.WHCode;
                    txtWHName.Text = wh.WHName;
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
                var emp = bu.GetEmployee(bwh.SaleEmpID);
                if (emp != null)
                    empFullName = emp.TitleName + " " + emp.FirstName + " " + emp.LastName;
            }

            if (!string.IsNullOrEmpty(empFullName))
            {
                txtEmpCode.Text = empFullName;
            }
        }

        public void InitHeader()
        {
            dtpDocDate.SetDateTimePickerFormat();
            dtpDueDate.SetDateTimePickerFormat();

            ddlDocStatus.BindDropdownDocStatus(bu);

            ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID", null, null);
            ddlSaleArea.Enabled = true;
        }

        public void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

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
            isAdd = true;

            var employee = bu.GetEmployee(Helper.tbl_Users.EmpID);
            txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName, employee.LastName);

            emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            supp = bu.GetSupplier(txtCustomerID.Text);

            allEmp = new Dictionary<string, string>();
            bu.GetEmployee().ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty) + x.LastName.Replace(" ", string.Empty))));

            ddlDocStatus.BindDropdownDocStatus(bu, "4");
            //txtCustomerCode.Focus();
        }

        private string PreparePOMaster(bool editFlag = false)
        {
            bu.tbl_POMaster = new tbl_POMaster();

            var comp = bu.tbl_Companies;

            if (emp == null || emp.EmpID == null)
                emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            if (supp == null || supp.SupplierID == null)
                supp = bu.GetSupplier(txtCustomerID.Text);

            if (allEmp.Count == 0)
                bu.GetEmployee().ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty) + x.LastName.Replace(" ", string.Empty))));

            var selEmp = allEmp.FirstOrDefault(x => x.Value == txtEmpCode.Text.Replace(" ", string.Empty));
            var vanWH = bu.GetAllBranchWarehouse().FirstOrDefault(x => x.SaleEmpID == selEmp.Key);

            var po = bu.tbl_POMaster;
            bool checkEditMode = bu.CheckExistsPO(txdDocNo.Text);

            if (checkEditMode)
                po.DocNo = txdDocNo.Text;
            else
                po.DocNo = bu.GenDocNo(docTypeCode, txtWHCode.Text);

            //last edit by sailom.k 06/07/2022--------------------------------------------
            var tmpDocNo = "";
            var checkDocNo = bu.tbl_Branchs[0].BranchID + vanWH.WHID.Substring(4, 2);
            var oldDocNo = po.DocNo.Substring(0, 5).ToString();
            if (oldDocNo != checkDocNo) 
            {
                tmpDocNo = checkDocNo +  po.DocNo.Substring(5, po.DocNo.Length - 5);
            }

            if (!string.IsNullOrEmpty(tmpDocNo))
            {
                po.DocNo = tmpDocNo;
            }
            //last edit by sailom.k 06/07/2022--------------------------------------------

            po.RevisionNo = 0;
            po.DocTypeCode = docTypeCode;
            po.DocStatus = ddlDocStatus.SelectedValue.ToString();
            po.DocDate = dtpDocDate.Value.ToDateTimeFormat();
            po.DocRef = "";
            po.StatusInOut = null;
            po.SupplierID = "0";
            po.SuppName = "";
            po.WHID = vanWH.WHID;
            po.EmpID = emp.EmpID;
            po.SaleEmpID = selEmp.Key;
            po.SalAreaID = ddlSaleArea.SelectedValue.ToString();
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
            po.Discount = Convert.ToDecimal(txnDiscountAmt.Text); // 0.00m; //Last edit by sailom .k 07/01/2022
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

            return po.DocNo;
        }

        private void PreparePODetail(bool editFlag = false)
        {
            bu.tbl_PODetails.Clear();

            var poDts = bu.tbl_PODetails;
            DateTime crDate = DateTime.Now;
            var allLineDiscountType = bu.GetDiscountType();

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
                    poDt.DocNo = bu.tbl_POMaster.DocNo;
                    poDt.Line = Convert.ToInt16(i);
                    poDt.ProductID = prdCodeCell.EditedFormattedValue.ToString();
                    poDt.ProductName = prdNameCell.EditedFormattedValue.ToString();

                    var uom = allUOM.FirstOrDefault(x => x.ProductUomName == uomTypeCell.EditedFormattedValue.ToString()); //edti by sailom .k 14/12/2021 // bu.GetUOM().FirstOrDefault(x => x.ProductUomName == uomTypeCell.EditedFormattedValue.ToString());
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
                    //var prdPriceGroup = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList(); //bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);
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

                    var ldt = allLineDiscountType.FirstOrDefault(x => x.DiscountTypeName == discountTypeCell.EditedFormattedValue.ToString());
                    if (ldt != null)
                    {
                        poDt.LineDiscountType = ldt.DiscountTypeCode;
                        poDt.LineDiscountRate = 0;
                        poDt.LineDiscount = Convert.ToDecimal(discountAmtCell.EditedFormattedValue);
                    }

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

                    poDts.Add(poDt);
                }
            }
        }

        private void PrepareInvMovement(bool editFlag = false)
        {
            bu.tbl_InvMovements.Clear();

            var invMms = bu.tbl_InvMovements;
            var poDts = bu.tbl_PODetails;
            var po = bu.tbl_POMaster;
            //var prod = bu.GetProduct();
            //var prodGroup = bu.GetProductGroup();
            //var prodSubGroup = bu.GetProductSubGroup();

            DateTime crDate = DateTime.Now;

            foreach (var poDt in poDts)
            {
                var invMm = new tbl_InvMovement();

                invMm.ProductID = poDt.ProductID;
                invMm.ProductName = poDt.ProductName;
                invMm.RefDocNo = poDt.DocNo;
                //invMm.TrnDate = crDate.ToDateTimeFormat();
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
            var poDts = bu.tbl_PODetails;
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
                //var prdPriceGroup = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList(); //bu.GetProductPriceGroup(tbl_ProductPriceGroupPre);
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
                invtr.SalAreaID = "0";
                invtr.ModifiedDate = crDate;
                invtr.FlagDel = false;
                invtr.FlagSend = false;

                invTrs.Add(invtr);
            }
        }

        private void PrepareInvWarehouse(bool editFlag = false)
        {
            bu.tbl_InvWarehouses.Clear();

            var invWhs = bu.tbl_InvWarehouses;
            //var poDts = bu.tbl_PODetails;
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
            }
        }

        private void PrepareInvMaster(tbl_POMaster po)
        {
            bu.tbl_IVMaster = new tbl_IVMaster();

            var iv = bu.tbl_IVMaster;

            var bwh = bu.GetAllBranchWarehouse(x => !x.FlagDel && x.WHType != 0); // edit by sailom .k 03/03/2022 for support pre-order

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
            //iv.EmpID = po.EmpID;
            //iv.SaleEmpID = po.SaleEmpID;
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
            iv.Remark = "สร้างใบกำกับจากการใบกำกับภาษีอย่างย่อ : " + po.DocRef;
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
            iv.Discount = po.Discount; //0.00m; //edit by sailom 09032021
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
            if (wh == null && wh.Count > 0)
                iv.FromWHCode = wh[0].VanCode;

            iv.FromLocCode = "VC4STV" + iv.WHID.Split('V')[1].ToString();
            iv.ToWHCode = null;
            iv.ToLocCode = null;
        }

        private void PrepareInvDetail(List<tbl_PODetail> poDts)
        {
            bu.tbl_IVDetails = new List<tbl_IVDetail>();

            //var allUomSet = bu.tbl_ProductUomSet;
            //var allProduct = bu.tbl_Product;
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

                //ivDt.OrderUom = _podt.OrderUom;
                //ivDt.OrderQty = _podt.OrderQty;
                //ivDt.UnitPrice = _podt.UnitPrice;
                //ivDt.ReceivedQty = _podt.ReceivedQty;
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
                ivDt.LineDiscountType = "N";
                ivDt.LineDiscount = _podt.LineDiscount;
                ivDt.CustType = "";
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
                    invWh.QtyOnHand = unitQty;
                }
            }
        }

        public void Save()
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
                    bu.tbl_POMaster.DocStatus = ddlDocStatus.SelectedValue.ToString();
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

                    //ret = bu.UpdateData();
                    ret = bu.PerformUpdateData(); //edit by sailom .k 14/12/2021

                    //edit by sailom .k 16/12/2021
                    bu.tbl_POMaster = new tbl_POMaster();
                    bu.tbl_PODetails.Clear();
                    bu.tbl_InvMovements.Clear();
                    bu.tbl_InvTransactions.Clear();
                    bu.tbl_DocRunning.Clear();
                    PrepareInvWarehouse();//edit by sailom .k 16/12/2021

                    ret = bu.PerformUpdateData(); //edit by sailom .k 16/12/2021
                }
                else
                {
                    string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                    string title = "ยืนยันการบันทึก!!";
                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    bu = new TabletSales();

                    //docno = bu.GenDocNo(docTypeCode, txtWHCode.Text);
                    editFlag = false;
                    bu.PrepareDocRunning(docTypeCode);

                    docno = PreparePOMaster(editFlag);
                    PreparePODetail(editFlag);
                    PrepareInvMovement(editFlag);
                    PrepareInvTransaction();
                    //PrepareInvWarehouse();

                    //ret = bu.UpdateData();
                    ret = bu.PerformUpdateData(); //edit by sailom .k 14/12/2021

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
                    btnUpdateAddress.Enabled = true;
                    btnGenCustIVNo.Enabled = true;

                    grdList.CellContentClick -= grdList_CellContentClick;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    ddlDocStatus.SelectedValue.ToString().CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
                    //CheckCancelDoc(ddlDocStatus.SelectedValue.ToString());
                    btnAdd.Enabled = false;
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

                    bu = new TabletSales();

                    //last edit by sailom .k 18/06/2022--------------------------------------------------------------------------
                    //PreparePOMaster();
                    //PreparePODetail();
                    bu.tbl_POMaster = bu.GetPOMaster(txdDocNo.Text);
                    bu.tbl_PODetails = bu.GetPODetails(txdDocNo.Text); //Last edit by sailom .k 07/01/2022

                    //List<tbl_PODetail> temptbl_PODetails = new List<tbl_PODetail>();
                    //temptbl_PODetails = SerializeHelper.CloneObject<List<tbl_PODetail>>(bu.tbl_PODetails);

                    //tbl_POMaster temptbl_POMaster = new tbl_POMaster();
                    //temptbl_POMaster = SerializeHelper.CloneObject<tbl_POMaster>(bu.tbl_POMaster);

                    txtCustInvNO.Text = bu.GenDocNo("V", txtWHCode.Text);

                    if (!string.IsNullOrEmpty(txtCustInvNO.Text))
                    {
                        //temptbl_POMaster.CustInvNO = txtCustInvNO.Text;
                        //temptbl_POMaster.FlagSend = false;
                        bu.tbl_POMaster.CustInvNO = txtCustInvNO.Text;
                        bu.tbl_POMaster.FlagSend = false;
                    }

                    //bu.tbl_POMaster = temptbl_POMaster;
                    PrepareInvMaster(bu.tbl_POMaster);

                    PrepareInvDetail(bu.tbl_PODetails);
                    //PrepareInvDetail(temptbl_PODetails);
                    //last edit by sailom .k 18/06/2022--------------------------------------------------------------------------

                    //int ret = bu.UpdateData();
                    int ret = bu.PerformUpdateData(); //edit by sailom .k 14/12/2021

                    if (ret == 1)
                    {
                        ret = 0;
                        if (!string.IsNullOrEmpty(txtCustomerID.Text) && !string.IsNullOrEmpty(bu.tbl_POMaster.CustInvNO) && !string.IsNullOrEmpty(bu.tbl_POMaster.DocNo))
                        {
                            bool result = bu.UpdateCustomerSAPCode(txtCustomerID.Text, bu.tbl_POMaster.CustInvNO, bu.tbl_POMaster.DocNo); //update customer SAP code
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

        private void SaveIV_OLD()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCustInvNO.Text) && !string.IsNullOrEmpty(txdDocNo.Text))
                {
                    string msg = "ใบกำกับภาษีถูกสร้างไปแล้ว!!!";
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

                    List<tbl_PODetail> temptbl_PODetails = new List<tbl_PODetail>();
                    temptbl_PODetails = SerializeHelper.CloneObject<List<tbl_PODetail>>(bu.tbl_PODetails);

                    tbl_POMaster temptbl_POMaster = new tbl_POMaster();
                    temptbl_POMaster = SerializeHelper.CloneObject<tbl_POMaster>(bu.tbl_POMaster);

                    bu = new TabletSales();

                    txtCustInvNO.Text = bu.GenDocNo("V", txtWHCode.Text);

                    if (!string.IsNullOrEmpty(txtCustInvNO.Text))
                        temptbl_POMaster.CustInvNO = txtCustInvNO.Text;

                    bu.tbl_POMaster = temptbl_POMaster;
                    PrepareInvMaster(bu.tbl_POMaster);

                    PrepareInvDetail(temptbl_PODetails);

                    int ret = bu.UpdateData();

                    if (ret == 1)
                    {
                        string msg = "สร้างใบกำกับภาษีเรียบร้อยแล้ว";
                        msg.ShowInfoMessage();
                    }
                }
                else
                {
                    string msg = "กรุณาบันทึกเอกสารอย่างย่อ ก่อนสร้างใบกำกับภาษี!!!";
                    msg.ShowInfoMessage();
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

        public bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();
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
                    string whid = string.IsNullOrEmpty(bu.tbl_POMaster.WHID) ? txtWHCode.Text : bu.tbl_POMaster.WHID;
                    ret = grdList.ValiadteDataGridView(allProduct, 0, 3, 4, 5, bu, whid, false);
                }
            }

            return ret;
        }

        #endregion


        #region event methods

        private void frmTabletSales_Load(object sender, EventArgs e)
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
            isAdd = true;
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

            emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            supp = bu.GetSupplier(txtCustomerID.Text);

            allEmp = new Dictionary<string, string>();
            bu.GetEmployee().ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty) + x.LastName.Replace(" ", string.Empty))));

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
            Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ClearControl(docTypeCode, runDigit);
            btnAdd.Enabled = false;

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnCopy.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnPrintCrys.Enabled = btnPrint.Enabled;

            validateNewRow = true;

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            //btnCancel.EnableButton(btnSearchDoc);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;
            btnSearchDoc.Enabled = true;
            txdDocNo.Enabled = true;

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
            bool isShowAll = chkShowAll.Checked;
            this.OpenIVDocPopup("ใบกำกับสินค้า/กำกับภาษี", docTypeCode, isShowAll);
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

            FilterSaleArea(txtCustomerID.Text);
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
            CalculateRow(grdList, grdList.CurrentRow.Index);
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
                BindTabletSalesData(txdDocNo.Text);
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
            grdList.SetCellContentClick(this, sender, e, "IVProduct", 4);
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
                        }

                        if (isNewRow)
                            grd.BindDataGridViewRow(dataGridList, initDataGridList, dt, 0, cell0.EditedFormattedValue.ToString(), currentRowIndex, 0, ref validateNewRow, "IVProduct", false);
                    }

                    if (isNewRow)
                        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 3];
                }
                else if (curentColIndex == 3 || curentColIndex == 4)
                {
                    grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 1];
                }
                else if (curentColIndex == 5)
                {
                    isAdd = true;

                    if (cell0.IsNotNullOrEmptyCell())
                    {
                        if ((grd.RowCount - 1) == currentRowIndex)
                        {
                            grdList.AddNewRow(allProduct, initDataGridList, 0, "", currentRowIndex + 1, validateNewRow, uoms, bu, this, 0);

                            if (validateNewRow)
                            {
                                if (grd.RowCount > currentRowIndex + 1)
                                    grd.CurrentCell = grd.Rows[currentRowIndex + 1].Cells[0];
                            }
                        }
                        else
                        {
                            if (grd.RowCount > currentRowIndex + 1)
                                grd.CurrentCell = grd.Rows[currentRowIndex + 1].Cells[0];
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

        private void frmTabletSales_FormClosed(object sender, FormClosedEventArgs e)
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

        private void btnRecovery_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txdDocNo.Text))
            {
                string cfMsg = "ต้องการกู้คืนข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการกู้คืน!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                var ret = bu.RecoveryPO(txdDocNo.Text, Helper.tbl_Users.Username);
                if (ret == "1")
                {
                    BindTabletSalesData(txdDocNo.Text);

                    string message = "กู้ข้อมูลสำเร็จ!!!";
                    message.ShowInfoMessage();
                }
                else
                {
                    string message = "กู้ข้อมูลไม่สำเร็จ กรุณาลองใหม่อีกครั้ง!!!";
                    message.ShowErrorMessage();
                }
            }
            else
            {
                string message = "กรุณาเลือกเลขที่เอกสารก่อน!!!";
                txdDocNo.Focus();
                message.ShowWarningMessage();
            }
        }
    }
}
