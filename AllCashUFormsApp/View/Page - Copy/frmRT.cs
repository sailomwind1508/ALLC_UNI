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
    public partial class frmRT : Form
    {
        MenuBU menuBU = new MenuBU();
        RT bu = new RT();
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
        //List<tbl_ProductUomSet> allUomSet = new List<tbl_ProductUomSet>();
        List<tbl_ProductUom> allUOM = new List<tbl_ProductUom>();
        List<tbl_ProductPriceGroup> allProductPrice = new List<tbl_ProductPriceGroup>();
        //List<tbl_ProductGroup> allProdGroup = new List<tbl_ProductGroup>();
        //List<tbl_ProductSubGroup> allProdSubGroup = new List<tbl_ProductSubGroup>();

        public frmRT()
        {
            InitializeComponent();

            searchCustControls = new List<Control> { txtCustomerCode, txtCustName, txtBillTo, txtContact, txtTelephone };
            searchBWHControls = new List<Control> { txtWHCode, txtWHName };
            searchEmpControls = new List<Control> { txtEmpCode };
            readOnlyControls = new List<string>() { txtCustName.Name, txtWHName.Name, txtEmpCode.Name };

            dataGridList = new Dictionary<int, string>() { { 0, "ProductID" }, { 2, "ProductName" }, { 3, "UOMSetID" }, { 5, "SellPriceVat" }, { 7, "VatType" } };
            initDataGridList = new Dictionary<int, string>() { { 0, "" }, { 2, "" }, { 3, "combobox" }, { 4, "0" }, { 5, "0.00" }, { 6, "0.00" }, { 7, "0" }, { 8, "N" }, { 9, "0" }, { 10, "0.00" }, { 11, "0.00" } };

            txdDocNo.KeyDown += TxdDocNo_KeyDown;
            txtWHCode.KeyDown += TxtWHCode_KeyDown;
            txtCustomerCode.KeyDown += TxtCustomerCode_KeyDown;
        }

        #region private methods

        private void InitPage()
        {
            var documentType = bu.GetDocumentType().FirstOrDefault(x => x.DocTypeCode.Trim() == "RT");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                runDigit = documentType.DocFormat.Length - 2;
                FormHeader.Text = documentType.DocHeader;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");

                this.ClearControl(docTypeCode, runDigit);
            }

            validateNewRow = true;
            btnAdd.Enabled = true;

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");

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

            allUOM = bu.GetUOM();

            uoms.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
            uoms.AddRange(allUOM);

            saleAreaList.AddRange(bu.GetAllSaleArea());
            salAreaDistrictList.AddRange(bu.GetAllSaleAreaDistrict());

            //data gridview
            grdList.SetDataGridViewStyle();
            SetDefaultGridViewEvent(grdList);

            allProduct = bu.GetProduct();
            //allUomSet = bu.GetUOMSet();

            allProductPrice = bu.GetProductPriceGroup();
            //allProdGroup = bu.GetProductGroup();
            //allProdSubGroup = bu.GetProductSubGroup();
        }

        public void BindRTData(string odDocNo)
        {
            bu.GetDocData(odDocNo, docTypeCode);

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

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;

            grdList.CellContentClick -= grdList_CellContentClick;

            bool checkEditMode = bu.CheckExistsPO(odDocNo);
            po.DocStatus.CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
        }

        private void BindPOMaster(tbl_POMaster po)
        {
            txdDocNo.Text = po.DocNo;

            dtpDocDate.Value = po.DocDate.ToDateTimeFormat();

            txtCustomerCode.Text = po.CustomerID;
            txtCustName.Text = po.CustName;
            txtContact.Text = po.ContactName;
            txtTelephone.Text = po.ContactTel;
            txtBillTo.Text = po.Address;

            var emp = bu.GetEmployee().FirstOrDefault(x => x.EmpID == po.EmpID);
            if (emp != null)
            {
                txtEmpCode.Text = emp.TitleName + " " + emp.FirstName;
            }

            txtWHCode.Text = po.WHID;
            Func<tbl_BranchWarehouse, bool> func = (x => x.WHID == po.WHID);
            var wh = bu.GetBranchWarehouse(func);
            if (wh != null)
            {
                txtWHName.Text = wh.WHName;
            }

            FilterSaleArea(po.SalAreaID);
            ddlDocStatus.BindDropdownDocStatus(bu, po.DocStatus);

            txtComment.Text = po.Comment;
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

            //txnAmount.Text = po.Amount.Value.ToStringN2();
            //txnDiscountType.Text = po.DiscountType;
            //txnDiscountAmt.Text = po.Discount.Value.ToStringN2();
            //txnBeforeVat.Text = (po.IncVat.Value - po.VatAmt.Value).ToStringN2();
            //txnVatAmt.Text = po.VatAmt.Value.ToStringN2();
            //lblVatType.Text = po.VatRate.Value.ToStringN0();
            //txnExcVat.Text = po.ExcVat.Value.ToStringN2();
            //txnTotalDue.Text = po.TotalDue.ToStringN2();
        }

        private void BindPODetail(List<tbl_PODetail> poDts)
        {
            grdList.Rows.Clear();

            var allUOM = bu.GetUOM();


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

                grdList.BindComboBoxCell(allProduct, grdList.Rows[i], i, false, 3, uoms, this, bu, 0);
                grdList.Rows[i].Cells[3].Value = poDts[i].OrderUom;

                grdList.Rows[i].Cells[4].Value = poDts[i].OrderQty;
                grdList.Rows[i].Cells[5].Value = poDts[i].UnitPrice;
                grdList.Rows[i].Cells[6].Value = 0;
                grdList.Rows[i].Cells[7].Value = poDts[i].VatType;
                grdList.Rows[i].Cells[8].Value = poDts[i].LineDiscountType;
                grdList.Rows[i].Cells[9].Value = poDts[i].LineDiscount;
                grdList.Rows[i].Cells[10].Value = poDts[i].LineTotal;
                grdList.Rows[i].Cells[11].Value = poDts[i].OrderUom;
            }
        }

        public void BindSearchProduct(DataTable productDT, int rowIndex)
        {
            validateNewRow = true;
            grdList.ValidateDuplicateSKU(productDT.Rows[0]["ProductID"].ToString(), 0, rowIndex, ref validateNewRow);

            grdList.BindDataGrid(allProduct, dataGridList, initDataGridList, productDT, 0, rowIndex, validateNewRow, this, uoms, bu, 0);
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
                var allPrdUOM = bu.GetUOM();
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

            for (int i = 0; i < grdList.RowCount; i++)
            {
                var cell0 = grdList.Rows[i].Cells[0];
                var cell3 = grdList.Rows[i].Cells[3];
                var cell4 = grdList.Rows[i].Cells[4];
                var vatCell = grdList.Rows[i].Cells[7];
                var lineTotalCell = grdList.Rows[i].Cells[10];
                var discountTypeCell = grdList.Rows[i].Cells[8];
                var discountAmtCell = grdList.Rows[i].Cells[9];
                string prdCode = cell0.EditedFormattedValue.ToString();
                var _qty = Convert.ToDecimal(cell4.EditedFormattedValue);

                if (totalDiscount > 0)
                {
                    decimal discountPerSku = totalDiscount / (_qty <= 0 ? 1 : _qty);
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
                        var prdUOM = allUOM.FirstOrDefault(x => x.ProductUomName == cell3.EditedFormattedValue.ToString());
                        if (prdUOM != null)
                        {
                            var orderUom = prdUOM.ProductUomID;
                            if (orderUom != -1)
                            {
                                //string prdCode = cell0.EditedFormattedValue.ToString();
                                Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == orderUom && x.ProductID == prdCode);
                                var prdPriceList = allProductPrice.Where(tbl_ProductPriceGroupPre).ToList();

                                if (prdPriceList != null && prdPriceList.Count > 0)
                                {
                                    //if (listDiscount.Count > 0)
                                    //{
                                    //    var _discount = listDiscount.FirstOrDefault(x => x.Key == prdCode);
                                    //    if (_discount.Key != null)
                                    //    {
                                    //        var _sellPricetmp = (prdPriceList[0].SellPrice.Value * Convert.ToDecimal(cell4.EditedFormattedValue)) - _discount.Value;

                                    //        incVat += _sellPricetmp;
                                    //        vatAmt += _sellPricetmp * (vatRate / 100);
                                    //    }
                                    //}
                                    //else
                                    {
                                        //var _qty = Convert.ToDecimal(cell4.EditedFormattedValue);
                                        incVat += (prdPriceList[0].SellPrice.Value * _qty); //Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                                        vatAmt += (prdPriceList[0].SellPriceVat.Value - prdPriceList[0].SellPrice.Value) * _qty;
                                    }
                                }
                            }
                        }
                    }
                    else //No VAT
                    {
                        excVat += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                    }
                }
                //if (vatCell.IsNotNullOrEmptyCell())
                //{
                //    decimal _vateRate = Convert.ToDecimal(vatCell.EditedFormattedValue);
                //    if (_vateRate > 0) //have VAT
                //    {
                //        var prdUOM = allPrdUOM.FirstOrDefault(x => x.ProductUomName == cell3.EditedFormattedValue.ToString());
                //        if (prdUOM != null)
                //        {
                //            var orderUom = prdUOM.ProductUomID;
                //            if (orderUom != -1)
                //            {
                //                string prdCode = cell0.EditedFormattedValue.ToString();
                //                Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == orderUom && x.ProductID == prdCode);
                //                var prdPriceList = allPrdPriceList.Where(tbl_ProductPriceGroupPre).ToList();

                //                if (prdPriceList != null && prdPriceList.Count > 0)
                //                {
                //                    incVat += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                //                    vatRate = _vateRate;
                //                }
                //            }
                //    }
                //    else //No VAT
                //    {
                //        excVat += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                //    }
                //}
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
           

            //excVat = excVat.ToDecimalN2();
            //incVat = incVat.ToDecimalN2();
            //vatAmt = (incVat * (vatRate / 100.00m)).ToDecimalN2();
            //totalDue = (amount - totalDiscountAmt).ToDecimalN2();
            //lblVatType.Text = vatRate.ToDecimalN0().ToString();

            //txnAmount.Text = amount.ToDecimalN2().ToStringN2();
            //txnDiscountType.Text = discountType;
            //txnDiscountAmt.Text = totalDiscountAmt.ToDecimalN2().ToStringN2();
            //txnBeforeVat.Text = (incVat - vatAmt).ToDecimalN2().ToStringN2();
            //txnVatAmt.Text = vatAmt.ToStringN2();
            //txnExcVat.Text = excVat.ToStringN2();
            //txnTotalDue.Text = totalDue.ToStringN2();
        }

        private void FilterSaleArea(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Func<tbl_ArCustomer, bool> func = (x => x.CustomerCode == text);
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

            ddlDocStatus.BindDropdownDocStatus(bu);

            ddlSaleArea.BindDropdownList(saleAreaList, "SalAreaName", "SalAreaID", null, null);
            ddlSaleArea.Enabled = true;
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            InitHeader();

            grdList.AutoGenerateColumns = false;
            validateNewRow = true;

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;

            grdList.Rows.Clear();

            grdList.AddNewRow(allProduct, initDataGridList, 0, "", 0, validateNewRow, uoms, bu, this, 0);

            txtCustomerCode.Focus();
        }

        private void PreparePOMaster(bool editFlag = false)
        {
            bu.tbl_POMaster = new tbl_POMaster();

            var comp = bu.GetCompany();
            var emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            var supp = bu.GetSupplier(txtCustomerCode.Text);

            Dictionary<string, string> allEmp = new Dictionary<string, string>();

            bu.GetEmployee().ForEach(x => allEmp.Add(x.EmpID, (x.TitleName.Replace(" ", string.Empty) + x.FirstName.Replace(" ", string.Empty))));
            var selEmp = allEmp.FirstOrDefault(x => x.Value == txtEmpCode.Text.Replace(" ", string.Empty));
            var vanWH = bu.GetAllBranchWarehouse().FirstOrDefault(x => x.SaleEmpID == selEmp.Key);

            var po = bu.tbl_POMaster;
            bool checkEditMode = bu.CheckExistsPO(txdDocNo.Text);

            if (checkEditMode)
                po.DocNo = txdDocNo.Text;
            else
                po.DocNo = bu.GenDocNo(docTypeCode);

            po.RevisionNo = 0;
            po.DocTypeCode = docTypeCode;
            po.DocStatus = ddlDocStatus.SelectedValue.ToString();
            po.DocDate = dtpDocDate.Value.ToDateTimeFormat();
            po.DocRef = "";
            po.StatusInOut = null;
            po.SupplierID = "0";
            po.SuppName = "";
            po.WHID = vanWH.WHID;
            po.EmpID = selEmp.Key;
            po.SaleEmpID = selEmp.Key;
            po.SalAreaID = ddlSaleArea.SelectedValue.ToString();
            po.Address = txtBillTo.Text;
            po.ContactName = txtContact.Text;
            po.ContactTel = txtTelephone.Text;
            po.Shipto = txtBillTo.Text;

            po.CreditDay = 0;
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
            po.CustInvNO = "";
            po.CustInvDate = null;
            po.CustPODate = dtpDocDate.Value;
            po.CustPONo = "";
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
                if (Convert.ToDecimal(grdList.Rows[i].Cells[7].EditedFormattedValue.ToString()) > 0)
                {
                    _vatRate = Convert.ToDecimal(grdList.Rows[i].Cells[7].EditedFormattedValue);
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

        private void PreparePODetail(bool editFlag = false)
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
                var comTypeCell = grdList.Rows[i].Cells[6];
                var vatCell = grdList.Rows[i].Cells[7];
                var discountTypeCell = grdList.Rows[i].Cells[8];
                var discountAmtCell = grdList.Rows[i].Cells[9];
                var lineAmt = grdList.Rows[i].Cells[10];
                var comAmtCell = grdList.Rows[i].Cells[11];
                var orderUomCell = grdList.Rows[i].Cells[12];

                if (prdCodeCell.IsNotNullOrEmptyCell() && prdNameCell.IsNotNullOrEmptyCell())
                {
                    poDt.DocNo = bu.tbl_POMaster.DocNo;
                    poDt.Line = Convert.ToInt16(i);
                    poDt.ProductID = prdCodeCell.EditedFormattedValue.ToString();
                    poDt.ProductName = prdNameCell.EditedFormattedValue.ToString();

                    var uom = bu.GetUOM().FirstOrDefault(x => x.ProductUomName == uomTypeCell.EditedFormattedValue.ToString());
                    if (uom != null)
                    {
                        poDt.OrderUom = Convert.ToInt32(uom.ProductUomID);
                    }

                    poDt.OrderQty = Convert.ToDecimal(orderQtyCell.EditedFormattedValue);
                    poDt.ReceivedQty = 0;
                    poDt.RejectedQty = 0;
                    poDt.StockedQty = 0;
                    poDt.UnitCost = 0;
                    poDt.UnitPrice = Convert.ToDecimal(priceCell.EditedFormattedValue);
                    poDt.VatType = Convert.ToByte(vatCell.EditedFormattedValue);

                    var allLineDiscountType = bu.GetDiscountType();
                    var ldt = allLineDiscountType.FirstOrDefault(x => x.DiscountTypeName == discountTypeCell.EditedFormattedValue.ToString());
                    if (ldt != null)
                    {
                        poDt.LineDiscountType = ldt.DiscountTypeCode;
                        poDt.LineDiscountRate = 0;
                        poDt.LineDiscount = Convert.ToDecimal(discountAmtCell.EditedFormattedValue);
                    }

                    poDt.LineTotal = Convert.ToDecimal(lineAmt.EditedFormattedValue);
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

        private void Save()
        {
            try
            {
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

                    bu = new RT();

                    docno = txdDocNo.Text;
                    editFlag = true;
                    bu.tbl_DocRunning = new List<tbl_DocRunning>();

                    bu.tbl_POMaster = null;
                    bu.tbl_POMaster = bu.GetPOMaster(docno);
                    bu.tbl_POMaster.DocStatus = ddlDocStatus.SelectedValue.ToString();

                    ret = bu.UpdateData();
                }
                else
                {
                    if (!ValidateSave())
                        return;

                    string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                    string title = "ยืนยันการบันทึก!!";
                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    //docno = bu.GenDocNo(docTypeCode);
                    editFlag = false;
                    bu.PrepareDocRunning(docTypeCode);

                    PreparePOMaster(editFlag);
                    PreparePODetail(editFlag);

                    ret = bu.RemovePODetails(bu.tbl_POMaster.DocNo);
                    if (ret == 0)
                    {
                        this.ShowProcessErr();
                        return;
                    }

                    ret = bu.UpdateData();
                }

                if (ret == 1)
                {
                    this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, txdDocNo.Text);

                    txdDocNo.Text = bu.tbl_POMaster.DocNo; ;

                    grdList.CellContentClick -= grdList_CellContentClick;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    ddlDocStatus.SelectedValue.ToString().CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
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

            var cDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Ticks;
            var docDate = new DateTime(dtpDocDate.Value.Year, dtpDocDate.Value.Month, dtpDocDate.Value.Day).Ticks;

            if (dtpDocDate.Value != null && docDate < cDate)
            {
                string message = "ห้ามเลือกวันที่ย้อนหลัง !!!";
                message.ShowWarningMessage();
                ret = false;
            }

            if (ret)
            {
                if (!dtpDocDate.ValidateEndDay(bu))
                {
                    string message = "ระบบปิดวันไปแล้ว ไม่สามารถเลือกวันที่นี้ได้ !!!";
                    message.ShowWarningMessage();
                    ret = false;
                }
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
                BindRTData(txdDocNo.Text);
        }

        private void frmRT_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InitialData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, txdDocNo.Text);

            txdDocNo.DisableTextBox(false);
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
            this.ClearControl(docTypeCode, runDigit);
            btnAdd.Enabled = true;

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");

            validateNewRow = true;

            this.OpenControl(false, readOnlyControls.ToArray(), cellEdit);

            btnCancel.EnableButton(btnSearchDoc);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;

            grdList.CellContentClick -= grdList_CellContentClick;
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

        private void btnSearchDoc_Click(object sender, EventArgs e)
        {
            this.OpenDocPopup("สินค้าคืนจากลูกค้า", docTypeCode);
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

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grdList.SetCellContentClick(this, sender, e, "RTProduct", 4);
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
                var cell2 = grd.Rows[currentRowIndex].Cells[2];

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
                            validateNewRow = false;
                            grd.Rows.RemoveAt(currentRowIndex);
                            isNewRow = false;
                        }

                        if (isNewRow)
                            grd.BindDataGridViewRow(dataGridList, initDataGridList, dt, 0, cell0.EditedFormattedValue.ToString(), currentRowIndex, 0, ref validateNewRow, "RTProduct", false);
                    }

                    if (isNewRow)
                        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 3];
                }
                else if (curentColIndex == 3)
                {
                    grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 1];
                }
                else if (curentColIndex == 4)
                {
                    if (cell2.IsNotNullOrEmptyCell())
                    {
                        if ((grd.RowCount - 1) == currentRowIndex)
                        {
                            validateNewRow = true;

                            var checkDup = grd.ValidateDuplicateSKU(cell0.EditedFormattedValue.ToString(), 0, currentRowIndex, ref validateNewRow);
                            if (!checkDup)
                            {
                                validateNewRow = false;
                            }

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

                    var numCals = new int[] { 0, 3, 4 };
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
    }
}
