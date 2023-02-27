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
    public partial class frmRE : Form
    {
        MenuBU menuBU = new MenuBU();
        RE bu = new RE();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();

        List<tbl_ProductUom> uoms = new List<tbl_ProductUom>();
        bool validateNewRow = true;
        string docTypeCode = "";
        int runDigit = 0;
        List<Control> searchSuppControls = new List<Control>();
        List<Control> searchBWHControls = new List<Control>();
        int[] cellEdit = new int[] { 0, 3, 4, 5, 7, 8, 9 }; //{ 0, 3, 4, 5, 9 }; 
        //List<int> uomList = new List<int>();
        int[] numberCell = new int[] { 4, 5, 6 };
        Dictionary<int, string> dataGridList = new Dictionary<int, string>();
        Dictionary<int, string> initDataGridList = new Dictionary<int, string>();
        List<tbl_Product> allProduct = new List<tbl_Product>();
        List<tbl_ProductUomSet> allUomSet = new List<tbl_ProductUomSet>();
        List<tbl_ProductUom> allPrdUOM = new List<tbl_ProductUom>();
        List<tbl_ProductPriceGroup> allprdPriceList = new List<tbl_ProductPriceGroup>();
        List<tbl_DiscountType> allLineDiscountType = new List<tbl_DiscountType>();

        List<tbl_PODetail> allPODetails = new List<tbl_PODetail>();
        List<tbl_ProductUom> prodUOMs = new List<tbl_ProductUom>();
        tbl_Employee emp = new tbl_Employee();
        tbl_ApSupplier supp = new tbl_ApSupplier();

        bool isAdd = false;

        public frmRE()
        {
            InitializeComponent();

            searchSuppControls = new List<Control> { txtSupplierCode, txtSuppName, txtBillTo, txtContact, txtTelephone, nudCreditDay, dtpDocDate, dtpDueDate };
            searchBWHControls = new List<Control> { txtWHCode, txtWHName };

            dataGridList = new Dictionary<int, string>() { { 0, "ProductID" }, { 2, "ProductName" }, { 3, "UOMSetID" }, { 5, "UnitPrice" }, { 6, "VatType" } };
            initDataGridList = new Dictionary<int, string>() { { 0, "" }, { 2, "" }, { 3, "combobox" }, { 4, "0" }, { 5, "0" }, { 6, "0" }, { 7, "N" }, { 8, "0.00" }, { 9, "0" }, { 10, "" } };

            txdDocNo.KeyDown += TxdDocNo_KeyDown;
            txtODDoc.KeyDown += TxtODDoc_KeyDown;
            dtpDocDate.ValueChanged += DtpDocDate_ValueChanged;
            nudCreditDay.ValueChanged += NudCreditDay_ValueChanged;
            txtSupplierCode.KeyDown += TxtSupplierCode_KeyDown;
            txtWHCode.KeyDown += TxtWHCode_KeyDown;

            //txtSupplierCode.SetSearchControl("Supplier", searchSuppControls);
            //txtWHCode.SetSearchControl("BranchWarehouse", searchBWHControls);
        }

        #region private methods

        private void InitPage()
        {
            var documentType = bu.tbl_DocumentType.FirstOrDefault(x => x.DocTypeCode.Trim() == "RE");
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

            this.OpenControl(false, new string[] { txtSuppName.Name, txtCrUser.Name }, cellEdit);

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, btnPrintCrys, "");

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
            dtpCustInvDate.SetDateTimePickerFormat();

            //uomList = bu.GetUOM().Select(x => x.ProductUomID).ToList();

            uoms.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
            //Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => uomList.Contains(x.ProductUomID));
            uoms.AddRange(bu.GetUOM());//bu.GetUOM(tbl_ProductUomPre));

            //data gridview
            grdList.SetDataGridViewStyle();
            SetDefaultGridViewEvent(grdList);

            allProduct = bu.tbl_Product;
            allUomSet = bu.tbl_ProductUomSet;
            allPrdUOM = bu.GetUOM(); //bu.GetUOM(tbl_ProductUomPre);
            allprdPriceList = bu.GetProductPriceGroup();
            allLineDiscountType = bu.GetDiscountType();
        }

        public void BindREData(string reDocNo)
        {
            bu.GetDocData(reDocNo, docTypeCode);

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
                dtpDocDate.Value = po.DocDate.ToDateTimeFormat(); //06052021
            }

            if (poDts != null && poDts.Count > 0)
            {
                BindPODetail(poDts);
            }
            else
            {
                grdList.Rows.Clear(); //last edit by sailom .k 28/09/2022
            }

            this.OpenControl(false, new string[] { txtSuppName.Name, txtCrUser.Name }, cellEdit);

            btnSearchDoc.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);
            btnPrintCrys.Enabled = btnPrint.Enabled;

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");

            grdList.CellContentClick -= grdList_CellContentClick;

            bool checkEditMode = bu.CheckExistsPO(reDocNo);
            po.DocStatus.CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
            //CheckCancelDoc(po.DocStatus);

            CreateGridBtnList();
        }

        public void BindODData(string odDocNo)
        {
            string _docTypeCode = "OD";

            bu.GetDocData(odDocNo, _docTypeCode);

            var po = bu.tbl_POMaster;
            var poDts = bu.tbl_PODetails;

            bool checkEditMode = bu.CheckExistsPO(odDocNo);
            if (!checkEditMode)
            {
                string msg = "ไม่พบข้อมูลเลขที่เอกสาร!!!";
                msg.ShowWarningMessage();

                InitialData();

                return;
            }

            if (po != null)
            {
                BindPOMaster(po);
                txtComment.Text = "สั่งสินค้า : " + po.DocNo;
                supp = bu.GetSupplier(txtSupplierCode.Text);
            }
            if (poDts != null && poDts.Count > 0)
            {
                Func<tbl_POMaster, bool> poMaster = (x => x.DocNo == odDocNo && x.DocTypeCode.Trim() == _docTypeCode);
                var checkCancelOD = bu.GetPOMaster(_docTypeCode, poMaster);

                if (checkCancelOD.Count > 0 && checkCancelOD[0].DocStatus == "5")
                {
                    string msg = "ใบสั่งซื้อเลขที่ : " + po.DocRef + " ถูกยกเลิกแล้ว ไม่สามารถอ้างอิงได้!";
                    msg.ShowWarningMessage();
                    txtODDoc.Text = "";

                    return;
                }

                BindPODetail(poDts, true);
            }

            //btnSearchDoc.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);

            //btnSave.Enabled = true;
            //btnEdit.Enabled = false;

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");

            for (int i = 0; i < grdList.RowCount; i++)
            {
                CalculateRow(grdList, i);
            }

            CreateGridBtnList();
            //grdList.CellContentClick -= grdList_CellContentClick;
        }

        private void BindPOMaster(tbl_POMaster po)
        {
            if (po.DocNo.Contains("OD"))
            {
                txtODDoc.Text = po.DocNo;
            }
            if (po.DocNo.Contains("RE"))
            {
                txdDocNo.Text = po.DocNo;
                txtODDoc.Text = po.DocRef;
            }

            dtpDocDate.Value = DateTime.Now.ToDateTimeFormat(); // po.DocDate.ToDateTimeFormat(); //12022021

            txtSupplierCode.Text = po.SupplierID;
            txtSuppName.Text = po.SuppName;

            txtCustInvNO.Text = po.CustInvNO;
            dtpCustInvDate.Value = po.CustInvDate == null ? DateTime.Now.ToDateTimeFormat() : po.CustInvDate.Value.ToDateTimeFormat();

            Func<tbl_BranchWarehouse, bool> tbl_BranchWarehousePre = (x => x.WHID == po.WHID);
            var whCode = bu.GetBranchWarehouse(tbl_BranchWarehousePre);
            if (whCode != null)
            {
                txtWHCode.Text = whCode.WHCode;
                txtWHName.Text = whCode.WHName;
            }

            txtContact.Text = po.ContactName;
            txtTelephone.Text = po.ContactTel;
            txtBillTo.Text = po.Shipto;
            nudCreditDay.Value = po.CreditDay.Value;

            int creditDayAmt = 0;
            if (po.CreditDay != null)
            {
                creditDayAmt = po.CreditDay.Value;
            }
            dtpDueDate.Value = DateTime.Now.AddDays(creditDayAmt).ToDateTimeFormat(); // po.DueDate.Value.ToDateTimeFormat(); //12022021

            if (ddlDocStatus.DataSource == null)
            {
                var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
                ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
            }
            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == po.DocStatus; };
            ddlDocStatus.SelectedValueDropdownList(condition);

            var emp = bu.GetEmployeeByUserName(po.CrUser); //edit by sailom 22/03/2022 //bu.GetEmployeeByUserName(po.EmpID);
            if (emp != null)
                txtCrUser.Text = string.Join(" ", emp.TitleName, emp.FirstName, emp.LastName);
            else
                txtCrUser.Text = po.CrUser;

            txtRemark.Text = po.Remark;
            txtComment.Text = po.Comment;

            txnAmount.Text = po.Amount.Value.ToStringN2();
            txnExcVat.Text = po.ExcVat.Value.ToStringN2();
            txnIncVat.Text = po.IncVat.Value.ToStringN2();
            txnVatAmt.Text = po.VatAmt.Value.ToStringN2();
            txnTotalDue.Text = po.TotalDue.ToStringN2();
        }

        private void BindPODetail(List<tbl_PODetail> poDts, bool isOD = false)
        {
            grdList.Rows.Clear();

            var allPOMaster = new List<tbl_POMaster>();
            var allPODetail = new List<tbl_PODetail>();

            //edit by sailom .k 14/12/2021---------------------------
            if (isOD)
            {
                allPOMaster.AddRange(bu.GetPOMaster("OD", x => !x.FlagDel));
                allPODetail.AddRange(bu.GetPODetails("OD", x => !x.FlagDel));

                allPOMaster.AddRange(bu.GetPOMaster("RE", x => !x.FlagDel));
                allPODetail.AddRange(bu.GetPODetails("RE", x => !x.FlagDel));
            }
            else
            {
                allPOMaster.AddRange(bu.GetPOMaster("RE", x => !x.FlagDel));
                allPODetail.AddRange(bu.GetPODetails("RE", x => !x.FlagDel));
            }
            //edit by sailom .k 14/12/2021---------------------------

            bool hasRefDoc = false;
            bool editREFlag = false;
            Func<tbl_POMaster, bool> tbl_POMasterPre = null;
            if (!string.IsNullOrEmpty(txtODDoc.Text) && bu.CheckExistsPO(txtODDoc.Text))
            {
                //tbl_POMasterPre = (x => x.DocRef.Trim() == txtODDoc.Text && x.DocTypeCode.Trim() == "RE" && x.DocStatus != "5");
                tbl_POMasterPre = (x => x.DocRef.Trim() == txtODDoc.Text && x.DocTypeCode.Trim() == "RE"); //edit by sailom.k 07/12/2022
                hasRefDoc = true;

                if (!string.IsNullOrEmpty(txdDocNo.Text) && bu.CheckExistsPO(txdDocNo.Text))
                {
                    editREFlag = true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txdDocNo.Text))
                {
                    tbl_POMasterPre = (x => x.DocNo == txdDocNo.Text && x.DocTypeCode.Trim() == "RE");
                    hasRefDoc = false;
                }
            }

            var poList = allPOMaster.Where(tbl_POMasterPre).ToList();// bu.GetPOMaster(tbl_POMasterPre);
            if (hasRefDoc && poList.Count == 0)
            {
                tbl_POMasterPre = (x => x.DocNo == txtODDoc.Text && x.DocTypeCode.Trim() == "OD");
                poList = allPOMaster.Where(tbl_POMasterPre).ToList();//bu.GetPOMaster(tbl_POMasterPre);

                hasRefDoc = false;
            }

            if (poList != null && poList.Count > 0)
            {
                Func<tbl_PODetail, bool> tbl_PODetailPre = (x => poList.Select(a => a.DocNo).Contains(x.DocNo));
                var poDtList = allPODetail.Where(tbl_PODetailPre).ToList();//bu.GetPODetails(tbl_PODetailPre);

                //Last edit by sailom.k 14/09/2021 tunning performance-------------------------
                isAdd = false;
                allPODetails = poDts;

                List<tbl_DiscountType> disTypeList = new List<tbl_DiscountType>();
                disTypeList = bu.tbl_DiscountType;

                var listPrdID = allPODetails.Select(x => x.ProductID).ToList();
                prodUOMs.AddRange(bu.GetProductUOM(listPrdID));
                //Last edit by sailom.k 14/09/2021 tunning performance--------------------

                for (int i = 0; i < poDts.Count; i++)
                {
                    decimal? receivedQty = 0;
                    if (editREFlag)
                    {
                        receivedQty = poDts[i].ReceivedQty;
                        //grdList.Rows[i].Cells[4].Value = poDts[i].ReceivedQty;
                    }
                    else
                    {
                        receivedQty = CalcReceiveQry(poList, poDtList, poDts[i].ProductID, poDts[i].OrderQty, hasRefDoc);
                        //grdList.Rows[i].Cells[4].Value = odQty; //15022021
                    }

                    if (receivedQty > 0) //15022021 visible ReceivedQty > 0
                    {
                        grdList.Rows.Add(1);

                        int cRowIndex = grdList.Rows.Count - 1;

                        string _prodID = poDts[i].ProductID;
                        grdList.Rows[cRowIndex].Cells[0].Value = _prodID;

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

                        //BindComboBoxCell(this DataGridView grd, DataGridViewRow row, int rowIndex, bool flagNewRow, int comboBoxIndex, List<tbl_ProductUom> prodUOMs, Form frm, BaseControl bu)
                        //BindComboBoxCell(grdList.Rows[i], i, false);
                        //grdList.BindComboBoxCell(allProduct, grdList.Rows[cRowIndex], cRowIndex, false, 3, uoms, this, bu, 0);
                        grdList.BindComboBoxDiscountTypeCell(allProduct, grdList.Rows[cRowIndex], cRowIndex, false, 3, uoms, this, bu, 0, disTypeList, 7); //Last edit by sailom.k 14/09/2021 tunning performance

                        grdList.Rows[cRowIndex].Cells[2].Value = productName;
                        grdList.Rows[cRowIndex].Cells[3].Value = poDts[i].OrderUom;

                        grdList.Rows[cRowIndex].Cells[4].Value = receivedQty;
                        grdList.Rows[cRowIndex].Cells[5].Value = poDts[i].UnitPrice;
                        grdList.Rows[cRowIndex].Cells[6].Value = poDts[i].VatType;

                        grdList.Rows[cRowIndex].Cells[7].Value = poDts[i].LineDiscountType;

                        grdList.Rows[cRowIndex].Cells[8].Value = poDts[i].LineDiscount;
                        grdList.Rows[cRowIndex].Cells[9].Value = poDts[i].LineTotal;
                        grdList.Rows[cRowIndex].Cells[10].Value = poDts[i].OrderUom;
                    }
                }
            }
        }

        public void BindSearchProduct(DataTable productDT, int rowIndex)
        {
            //if (!string.IsNullOrEmpty(productDT.Rows[0]["ProductID"].ToString()))
            //{
            //    if (!string.IsNullOrEmpty(grdList.Rows[rowIndex].Cells[0].EditedFormattedValue.ToString()))
            //    {
            //        var cell0 = grdList.Rows[rowIndex].Cells[0];
            //        int currentRowIndex = grdList.CurrentCell.RowIndex;
            //        if (cell0.IsNotNullOrEmptyCell())
            //        {
            //            grdList.BindComboBoxCell(grdList.Rows[currentRowIndex], currentRowIndex, false, 3, uoms, this, bu, 0);
            //        }

            //        var cell4 = grdList.Rows[rowIndex].Cells[4];
            //        if (cell4.IsNotNullOrEmptyCell())
            //        {
            //            CalculateRow(grdList, rowIndex);
            //        }
            //    }
            //}

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
            decimal discount = 0;
            int orderUom = -1;
            string lineDiscountType = "";



            var cell0 = grd.Rows[rowIndex].Cells[0];
            var cell3 = grd.Rows[rowIndex].Cells[3];
            var cell4 = grd.Rows[rowIndex].Cells[4];
            var cell5 = grd.Rows[rowIndex].Cells[5];
            var cell7 = grd.Rows[rowIndex].Cells[7];
            var cell8 = grd.Rows[rowIndex].Cells[8];

            if (cell3.IsNotNullOrEmptyCell())
            {
                //Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => uomList.Contains(x.ProductUomID));

                var prdUOM = allPrdUOM.FirstOrDefault(x => x.ProductUomName == cell3.EditedFormattedValue.ToString());
                if (prdUOM != null)
                {
                    orderUom = prdUOM.ProductUomID;
                    if (orderUom != -1)
                    {
                        string prdCode = cell0.EditedFormattedValue.ToString();
                        Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == orderUom && x.ProductID == prdCode);
                        var prdPriceList = allprdPriceList.Where(tbl_ProductPriceGroupPre).ToList();

                        if (prdPriceList != null && prdPriceList.Count > 0)
                        {
                            cell5.Value = prdPriceList[0].BuyPrice.Value;
                        }
                    }
                    else
                    {
                        cell5.Value = 0;
                    }
                }
                else
                {
                    cell5.Value = 0;
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
            if (cell8.IsNotNullOrEmptyCell())
            {
                if (cell7.IsNotNullOrEmptyCell())
                {
                    var ldt = allLineDiscountType.FirstOrDefault(x => x.DiscountTypeName == cell7.EditedFormattedValue.ToString());
                    if (ldt != null)
                    {
                        lineDiscountType = ldt.DiscountTypeCode;
                        var cell8Value = Convert.ToDecimal(cell8.EditedFormattedValue);

                        switch (lineDiscountType)
                        {
                            case "N": { discount = 0; } break;
                            case "A": { discount = cell8Value; } break;
                            case "P":
                                {
                                    decimal total = (orderQty * unitPrice).ToDecimalN2();
                                    discount = cell8Value;
                                    decimal discountAmt = (total * discount) / 100;

                                    discount = discountAmt;
                                }
                                break;
                            case "Q": { discount = cell8Value * orderQty; } break;
                            case "F": { discount = (cell8Value * unitPrice).ToDecimalN2(); } break;
                            default:
                                break;
                        }
                    }
                    else
                        discount = 0;
                }
                else
                {
                    discount = 0;
                }

                grd.Rows[rowIndex].Cells[9].Value = ((orderQty * unitPrice) - discount).ToDecimalN2().ToStringN2();
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

            for (int i = 0; i < grdList.RowCount; i++)
            {
                var vatCell = grdList.Rows[i].Cells[6];
                var lineTotalCell = grdList.Rows[i].Cells[9];

                if (lineTotalCell.IsNotNullOrEmptyCell())
                {
                    amount += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                }
                if (vatCell.IsNotNullOrEmptyCell())
                {
                    decimal _vateRate = Convert.ToDecimal(vatCell.EditedFormattedValue);
                    if (_vateRate > 0) //have VAT
                    {
                        incVat += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                        vatRate = _vateRate;
                    }
                    else //No VAT
                    {
                        excVat += Convert.ToDecimal(lineTotalCell.EditedFormattedValue);
                    }
                }
            }

            excVat = excVat.ToDecimalN2();

            incVat = incVat.ToDecimalN2(); //(amount - excVat).ToDecimalN2();

            vatAmt = (incVat * (vatRate / 100.00m)).ToDecimalN2();

            totalDue = (amount + vatAmt).ToDecimalN2();

            txnAmount.Text = amount.ToDecimalN2().ToStringN2();
            txnExcVat.Text = excVat.ToDecimalN2().ToStringN2();
            txnIncVat.Text = incVat.ToStringN2();
            txnVatAmt.Text = vatAmt.ToStringN2();
            txnTotalDue.Text = totalDue.ToStringN2();
        }

        private void InitHeader()
        {
            this.BindData("BranchWarehouse", searchBWHControls, "1000");

            var employee = bu.GetEmployee(Helper.tbl_Users.EmpID);
            txtCrUser.Text = string.Join(" ", employee.TitleName, employee.FirstName, employee.LastName);

            dtpDocDate.SetDateTimePickerFormat();
            dtpDueDate.SetDateTimePickerFormat();
            dtpCustInvDate.SetDateTimePickerFormat();

            var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
            ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnPrintCrys.Enabled = btnPrint.Enabled;

            this.OpenControl(true, new string[] { txtSuppName.Name, txtCrUser.Name }, cellEdit);

            InitHeader();

            grdList.AutoGenerateColumns = false;
            validateNewRow = true;

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;

            grdList.Rows.Clear();
            //AddNewRow(grdList, 0);
            grdList.AddNewRow(allProduct, initDataGridList, 0, "", 0, validateNewRow, uoms, bu, this, 0);
            isAdd = true; //Last edit by sailom.k 14/09/2021 tunning performance

            txtComment.Text = "สั่งสินค้า : ";

            txtSupplierCode.Focus();

            emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            supp = bu.GetSupplier(txtSupplierCode.Text); 
        }

        private string PreparePOMaster(bool editFlag = false)
        {
            string ret = string.Empty;

            var odPO = bu.GetPOMaster(txtODDoc.Text);
            var comp = bu.tbl_Companies;

            if (emp == null || emp.EmpID == null)
                emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            if (supp == null || supp.SupplierID == null)
                supp = bu.GetSupplier(txtSupplierCode.Text);

            var po = bu.tbl_POMaster;
            bool checkEditMode = bu.CheckExistsPO(txdDocNo.Text);
            if (checkEditMode)
            {
                po.DocNo = txdDocNo.Text;
                if (po.DocStatus == "5")
                {
                    btnAdd.Enabled = true;
                    btnCopy.Enabled = true;

                    btnEdit.Enabled = false;
                }
                else
                {
                    btnEdit.Enabled = true;
                }
            }
            else
            {
                po.DocNo = bu.GenDocNo(docTypeCode);
            }

            ret = po.DocNo;

            po.RevisionNo = 0;
            po.DocTypeCode = docTypeCode;
            po.DocStatus = ddlDocStatus.SelectedValue.ToString();
            po.DocDate = dtpDocDate.Value.ToDateTimeFormat();

            var checkExistsPO = bu.CheckExistsPO(txtODDoc.Text);
            if (checkExistsPO)
            {
                po.DocRef = txtODDoc.Text;
            }
            else
            {
                po.DocRef = string.Empty;
            }

            po.StatusInOut = null;
            po.SupplierID = txtSupplierCode.Text;
            po.SuppName = txtSuppName.Text;
            po.WHID = comp[0].WHID;
            po.EmpID = emp.EmpID;
            po.SaleEmpID = "0";
            po.SalAreaID = "0";
            po.Address = supp.BillTo;
            po.ContactName = txtContact.Text;
            po.ContactTel = txtTelephone.Text;
            po.Shipto = txtBillTo.Text;
            po.CreditDay = Convert.ToInt16(nudCreditDay.Value);
            po.DueDate = dtpDueDate.Value.ToDateTimeFormat();
            po.CustomerID = "0";
            po.CustType = "";
            po.CustName = "";
            po.CustInvNO = txtCustInvNO.Text;
            po.CustInvDate = dtpCustInvDate.Value.ToDateTimeFormat();
            po.CustPODate = odPO != null ? odPO.DocDate.ToDateTimeFormat() : DateTime.Now.ToDateTimeFormat();
            po.CustPONo = txtODDoc.Text;
            po.Remark = txtRemark.Text;
            po.Comment = txtComment.Text;
            po.OldAmount = 0.00m;
            po.Amount = Convert.ToDecimal(txnAmount.Text);
            po.OldExcVat = 0.00m;
            po.ExcVat = Convert.ToDecimal(txnExcVat.Text);
            po.OldIncVat = 0.00m;
            po.IncVat = Convert.ToDecimal(txnIncVat.Text);

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

            return ret;
        }

        private void PreparePODetail(bool editFlag = false)
        {
            bu.tbl_PODetails.Clear();

            var poDts = bu.tbl_PODetails;
            DateTime crDate = DateTime.Now;
            var odPdts = bu.GetPODetails(txtODDoc.Text);

            for (int i = 0; i < grdList.RowCount; i++)
            {
                var poDt = new tbl_PODetail();

                var cell0 = grdList.Rows[i].Cells[0];
                var cell2 = grdList.Rows[i].Cells[2];
                var cell3 = grdList.Rows[i].Cells[3];
                var cell4 = grdList.Rows[i].Cells[4];
                var cell5 = grdList.Rows[i].Cells[5];
                var cell6 = grdList.Rows[i].Cells[6];
                var cell7 = grdList.Rows[i].Cells[7];
                var cell8 = grdList.Rows[i].Cells[8];
                var cell9 = grdList.Rows[i].Cells[9];

                if (cell0.IsNotNullOrEmptyCell() && cell2.IsNotNullOrEmptyCell())
                {
                    poDt.DocNo = bu.tbl_POMaster.DocNo;
                    poDt.Line = Convert.ToInt16(i);
                    poDt.ProductID = cell0.EditedFormattedValue.ToString();
                    poDt.ProductName = cell2.EditedFormattedValue.ToString();

                    //Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => uomList.Contains(x.ProductUomID));
                    //var allPrdUOM = bu.GetUOM();
                    var prdUOM = allPrdUOM.FirstOrDefault(x => x.ProductUomName == cell3.EditedFormattedValue.ToString());
                    if (prdUOM != null)
                    {
                        poDt.OrderUom = prdUOM.ProductUomID;
                    }

                    poDt.OrderQty = 0;
                    if (odPdts != null && odPdts.Count > 0)
                    {
                        var odPdt = odPdts.FirstOrDefault(x => x.ProductID == cell0.EditedFormattedValue.ToString());
                        if (odPdt != null)
                        {
                            poDt.OrderQty = odPdt.OrderQty;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtODDoc.Text)) //when no ref OD
                            {
                                poDt.OrderQty = Convert.ToDecimal(cell4.EditedFormattedValue);
                            }
                        }
                    }

                    poDt.ReceivedQty = Convert.ToDecimal(cell4.EditedFormattedValue);
                    poDt.RejectedQty = 0;
                    poDt.StockedQty = 0;
                    poDt.UnitCost = 0;
                    poDt.UnitPrice = Convert.ToDecimal(cell5.EditedFormattedValue);
                    poDt.VatType = Convert.ToByte(Convert.ToDecimal(cell6.EditedFormattedValue));

                    //var allLineDiscountType = bu.GetDiscountType();
                    var ldt = allLineDiscountType.FirstOrDefault(x => x.DiscountTypeName == cell7.EditedFormattedValue.ToString());
                    if (ldt != null)
                    {
                        poDt.LineDiscountType = ldt.DiscountTypeCode;
                        poDt.LineDiscountRate = 0;
                        poDt.LineDiscount = Convert.ToDecimal(cell8.EditedFormattedValue);
                    }

                    poDt.LineTotal = Convert.ToDecimal(cell9.EditedFormattedValue);
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
            var prod = bu.GetProduct();
            var prodGroup = bu.GetProductGroup();
            var prodSubGroup = bu.GetProductSubGroup();

            DateTime crDate = DateTime.Now;

            foreach (var poDt in poDts)
            {
                var invMm = new tbl_InvMovement();

                invMm.ProductID = poDt.ProductID;
                invMm.ProductName = poDt.ProductName;
                invMm.RefDocNo = poDt.DocNo;
                //invMm.TrnDate = crDate.ToDateTimeFormat();
                invMm.TrnDate = dtpDocDate.Value.ToDateTimeFormat();//last edit by sailom .k 10/08/2022 
                invMm.TrnType = "R";
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

                invMm.TrnQtyIn = unitQty;
                invMm.TrnQtyOut = 0;
                invMm.TrnQty = invMm.TrnQtyIn;
                invMm.CrDate = crDate;

                if (editFlag)
                {
                    invMm.EdDate = crDate;
                    invMm.TrnType = ddlDocStatus.SelectedValue.ToString() == "5" ? "X" : "R";
                }

                var prodItem = prod.FirstOrDefault(x => x.ProductID == poDt.ProductID);
                var prodGroupItem = prodGroup.FirstOrDefault(x => x.ProductGroupID == prodItem.ProductGroupID);
                var prodSubGroupItem = prodSubGroup.FirstOrDefault(x => x.ProductSubGroupID == prodItem.ProductSubGroupID);

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
            var prod = bu.GetProduct();
            var prodGroup = bu.GetProductGroup();
            var prodSubGroup = bu.GetProductSubGroup();
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
                invtr.TrnType = "R";
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

                invtr.TrnQtyIn = unitQty;
                invtr.TrnQtyOut = 0;
                invtr.TrnQty = invtr.TrnQtyIn;
                invtr.RemainQty = 0;
                invtr.UnitPrice = poDt.UnitPrice;
                invtr.UnitCost = (poDt.LineTotal.Value / invtr.TrnQtyIn).ToDecimalN5();
                invtr.LineDiscountType = poDt.LineDiscountType;
                invtr.LineDiscount = poDt.LineDiscount;
                invtr.TrnVat = poDt.VatType;
                invtr.TrnValue = poDt.LineTotal.Value;
                invtr.TrnTotal = po.Amount.Value;
                invtr.CostValue = poDt.LineTotal.Value;
                invtr.Supplier = Convert.ToInt32(po.SupplierID);
                invtr.Customer = "0";
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
                invWhItems = bu.GetTotalStockMovement(prdList, bu.tbl_Companies.First().WHID); //  edit by sailom 13/12/2021

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

        private void SetQtyOnHand(tbl_InvWarehouse invWh, List<tbl_InvWarehouse> allWHStock, decimal unitQty, string productID, string whID, bool editFlag)
        {
            //var invWhItem = bu.GetInvWarehouse(productID, whID);
            var invWhItem = allWHStock.Where(x => x.ProductID == productID && x.WHID == whID).ToList(); //edit by sailom .k 13/12/2021
            if (editFlag)
            {
                if (invWhItem != null && invWhItem.Count > 0)
                {
                    if (ddlDocStatus.SelectedValue.ToString() == "5")
                        invWh.QtyOnHand = invWhItem[0].QtyOnHand - unitQty;
                }
            }
            else
            {
                if (invWhItem != null && invWhItem.Count > 0)
                {
                    invWh.QtyOnHand = invWhItem[0].QtyOnHand + unitQty;
                }
                else
                {
                    invWh.QtyOnHand = unitQty;
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

                    bu = new RE();

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

                    DateTime crDate = DateTime.Now;

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
                    bu.tbl_POMaster.EdDate = crDate;
                    bu.tbl_POMaster.EdUser = Helper.tbl_Users.Username;

                    bu.tbl_InvMovements.Clear();
                    List<tbl_InvMovement> tbl_InvMovements = bu.GetInvMovement(docno);
                    tbl_InvMovements.ForEach(x =>
                    {
                        x.EdDate = crDate;

                        if (ddlDocStatus.SelectedValue.ToString() == "5")
                            x.TrnType = "X";
                    });
                    bu.tbl_InvMovements.AddRange(tbl_InvMovements);

                    bu.tbl_InvTransactions.Clear();
                    List<tbl_InvTransaction> tbl_InvTransactions = bu.GetInvTransaction(docno);
                    tbl_InvTransactions.ForEach(x =>
                    {
                        x.ModifiedDate = crDate;

                        if (ddlDocStatus.SelectedValue.ToString() == "5")
                            x.TrnType = "X";
                    });
                    bu.tbl_InvTransactions.AddRange(tbl_InvTransactions);

                    bu.tbl_InvWarehouses.Clear();

                    //string whID = bu.tbl_POMaster.WHID;
                    //var poDts = bu.GetPODetails(docno);

                    //var allInvWhs = bu.GetInvWarehouse();

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
                    if (!ValidateSave())
                        return;

                    bu = new RE();

                    string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                    string title = "ยืนยันการบันทึก!!";
                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    //docno = bu.GenDocNo(docTypeCode);
                    editFlag = false;
                    bu.PrepareDocRunning(docTypeCode);

                    docno = PreparePOMaster(editFlag);

                    //validate docno by sailom.k 27-05-2021
                    if (bu.tbl_POMaster.DocNo.Length < 12)
                    {
                        this.ShowDocNoProcessErr();
                        return;
                    }

                    PreparePODetail(editFlag);
                    PrepareInvMovement(editFlag);
                    PrepareInvTransaction();
                    //PrepareInvWarehouse();

                    //ret = bu.RemovePODetails();
                    //if (ret == 0)
                    //{
                    //    this.ShowProcessErr();
                    //    return;
                    //}
                    //ret = bu.RemoveInvMovements();
                    //if (ret == 0)
                    //{
                    //    this.ShowProcessErr();
                    //    return;
                    //}
                    //ret = bu.RemoveInvTransaction();
                    //if (ret == 0)
                    //{
                    //    this.ShowProcessErr();
                    //    return;
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

                if (ret == 1)
                {
                    this.OpenControl(false, new string[] { txtSuppName.Name, txtCrUser.Name }, cellEdit);

                    btnSave.EnableButton(btnEdit, btnRemove, btnCancel, btnAdd, btnCopy, btnPrint, btnExcel, txdDocNo.Text);
                    btnPrintCrys.Enabled = btnPrint.Enabled;

                    txdDocNo.Text = docno;

                    grdList.CellContentClick -= grdList_CellContentClick;

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    ddlDocStatus.SelectedValue.ToString().CheckCancelDoc(checkEditMode, btnAdd, btnCopy, btnEdit);
                    //CheckCancelDoc(ddlDocStatus.SelectedValue.ToString());
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

            if (!(new List<int> { 5, 10 }).Contains(Helper.tbl_Users.RoleID.Value) && dtpDocDate.Value != null && docDate < cDate)
            {
                string message = "ห้ามเลือกวันที่ย้อนหลัง !!!";
                message.ShowWarningMessage();
                ret = false;
            }

            if (ret)
            {
                if (!(new List<int> { 5, 10 }).Contains(Helper.tbl_Users.RoleID.Value) && !dtpDocDate.ValidateEndDay(bu))
                {
                    string message = "ระบบปิดวันไปแล้ว ไม่สามารถเลือกวันที่นี้ได้ !!! \n ***หากต้องการทำรายการนี้ต้องแจ้งทาง IT เท่านั้น***";
                    message.ShowWarningMessage();
                    ret = false;
                }
            }

            if (ret)
            {
                errList.SetErrMessageList(txtSupplierCode, lblSupplierCode);
                errList.SetErrMessageList(txtBillTo, lblAddress);
                errList.SetErrMessageList(txtContact, lblContact);
                errList.SetErrMessageList(txtTelephone, lblTelephone);
                errList.SetErrMessageList(txtCustInvNO, lblCustInvNO);
                errList.SetErrMessageList(txtWHCode, lblWHCode);

                if (errList.Count == 0)
                {
                    var sup = bu.GetSupplier(txtSupplierCode.Text);
                    if (sup == null)
                    {
                        string t = lblSupplierCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtSupplierCode.ErrorTextBox();
                    }

                    //Func<tbl_BranchWarehouse, bool> branchWarehousePre = (x => x.WHCode.ToLower() == txtWHCode.Text.ToLower());
                    //var wh = bu.GetBranchWarehouse(branchWarehousePre);
                    var wh = bu.GetBranchWarehouse(txtWHCode.Text); //Last edit by sailom .k 07/02/2022
                    if (wh == null)
                    {
                        string t = lblWHCode.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txtWHCode.ErrorTextBox();
                    }
                }

                if (errList.Count > 0)
                {
                    string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                    message.ShowWarningMessage();
                    ret = false;
                }
            }

            if (ret)
            {
                ret = grdList.ValiadteDataGridView(allProduct, 0, 3, 4, 5, bu, "", false);
            }

            return ret;
        }

        #endregion

        #region event methods

        private void frmRE_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string msg = "start frmRE=>btnAdd_Click";
            msg.WriteLog(this.GetType());

            InitialData();

            msg = "end frmRE=>btnAdd_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string msg = "start frmRE=>btnEdit_Click";
            msg.WriteLog(this.GetType());

            btnEdit.EnableButton(btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, txdDocNo.Text);
            btnPrintCrys.Enabled = btnPrint.Enabled;

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise; // Translator.FromHtml("#7FFFD4");
            ddlDocStatus.Enabled = true;
            btnCancel.Enabled = true;

            validateNewRow = true;

            if (!string.IsNullOrEmpty(txdDocNo.Text) && bu.CheckExistsPO(txdDocNo.Text))
            {
                grdList.CellContentClick -= grdList_CellContentClick;
            }
            else
            {
                grdList.CellContentClick -= grdList_CellContentClick;
                grdList.CellContentClick += grdList_CellContentClick;
            }

            dtpDocDate.Focus();
            isAdd = true; //Last edit by sailom.k 14/09/2021 tunning performance

            msg = "end frmRE=>btnEdit_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string msg = "start frmRE=>btnCopy_Click";
            msg.WriteLog(this.GetType());

            btnCopy.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnPrint, txdDocNo.Text);
            btnPrintCrys.Enabled = btnPrint.Enabled;

            this.OpenControl(true, new string[] { txtSuppName.Name, txtCrUser.Name }, cellEdit);

            txdDocNo.Text = string.Empty;
            txtODDoc.Text = string.Empty;

            Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == "4"; };
            ddlDocStatus.SelectedValueDropdownList(condition);
            ddlDocStatus.Enabled = false;

            validateNewRow = true;

            emp = bu.GetEmployee(Helper.tbl_Users.EmpID);
            supp = bu.GetSupplier(txtSupplierCode.Text);

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;


            msg = "end frmRE=>btnCopy_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string msg  = "start frmRE=>btnSave_Click";
            msg.WriteLog(this.GetType());

            Save();

            msg = "end frmRE=>btnSave_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string msg = "start frmRE=>btnCancel_Click";
            msg.WriteLog(this.GetType());

            this.ClearControl(docTypeCode, runDigit);

            btnAdd.Enabled = true;
            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnPrintCrys.Enabled = btnPrint.Enabled;

            validateNewRow = true;

            this.OpenControl(false, new string[] { txtSuppName.Name, txtCrUser.Name }, cellEdit);

            btnCancel.EnableButton(btnSearchDoc);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;

            grdList.CellContentClick -= grdList_CellContentClick;

            msg = "end frmRE=>btnCancel_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string msg = "start frmRE=>btnPrint_Click";
            msg.WriteLog(this.GetType());

            FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

            string cfMsg = "ต้องการพิมพ์โดยที่ไม่ดูรายงานใช่หรือไม่?";
            string title = "ยืนยันการพิมพ์!!";
            var confirmResult = FlexibleMessageBox.Show(cfMsg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (confirmResult == DialogResult.Yes)
            {

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocNo", txdDocNo.Text);
                //this.OpenCrystalReportsPopup("ใบรับสินค้า", "From_RE.rpt", "Form_RE", _params);

                this.OpenReportingReportsNonPreViewPopup("ใบรับสินค้า", "Form_RE.rdlc", "Form_RE", _params); //Reporting service by sailom 30/11/2021
            }
            else
            {
                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocNo", txdDocNo.Text);
                //this.OpenCrystalReportsPopup("ใบรับสินค้า", "From_RE.rpt", "Form_RE", _params);

                this.OpenReportingReportsPopup("ใบรับสินค้า", "Form_RE.rdlc", "Form_RE", _params); //Reporting service by sailom 30/11/2021
            }

            msg = "end frmRE=>btnPrint_Click";
            msg.WriteLog(this.GetType());
        }

        private void btnPrintCrys_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txdDocNo.Text))
            {
                FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocNo", txdDocNo.Text);
                this.OpenCrystalReportsPopup("ใบรับสินค้า", "From_RE.rpt", "Form_RE", _params);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchSupp_Click(object sender, EventArgs e)
        {
            this.OpenSupplierPopup(searchSuppControls, "เลือกผู้จำหน่าย");
        }

        private void btnWHID_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchBWHControls, "เลือกคลังสินค้า", new string[] { "1000" });
        }

        private void btnSearchDoc_Click(object sender, EventArgs e)
        {
            this.OpenDocPopup("ใบรับสินค้า", docTypeCode);
        }

        private void btnOD_Click(object sender, EventArgs e)
        {
            this.OpenDocPopup("ใบสั่งสินค้า", "REOD");

            grdList.CellContentClick -= grdList_CellContentClick;
            grdList.CellContentClick += grdList_CellContentClick;
        }

        private void TxtODDoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtODDoc.Text))
                {
                    BindODData(txtODDoc.Text);
                }
            }
        }

        private void TxdDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txdDocNo.Text))
                {
                    BindREData(txdDocNo.Text);
                }
            }
        }

        private void TxtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                //txtWHCode.SetSearchControl("BranchWarehouse", searchBWHControls);
                this.BindData("BranchWarehouse", searchBWHControls, txt.Text);
            }
        }

        private void TxtSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                //txtSupplierCode.SetSearchControl("Supplier", searchSuppControls);
                this.BindData("Supplier", searchSuppControls, txt.Text);
            }
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
            int qtyCell = 4;
            grdList.SetCellContentClick(this, sender, e, "REProduct", qtyCell);

            //var grd = sender as DataGridView;
            //var cell0 = grd.Rows[e.RowIndex].Cells[0];
            //int currentRowIndex = e.RowIndex;
            //if (cell0.IsNotNullOrEmptyCell())
            //{
            //    grd.BindComboBoxCell(grd.Rows[currentRowIndex], currentRowIndex, false, 3, uoms, this, bu, 0);
            //}

            //var cell4 = grdList.Rows[currentRowIndex].Cells[qtyCell];
            //if (cell4.IsNotNullOrEmptyCell())
            //{
            //    CalculateRow(grdList, currentRowIndex);
            //}
        }

        private void grdList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            grdList.SetCellClick(sender, e, cellEdit, 0);
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
                        {
                            CalculateRow(grd, currentRowIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
        }

        private void grdList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void grdList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0)
            //{
            //    grdList.ModifyComboBoxCell(allProduct, e.RowIndex, bu, 3, 0);
            //}

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
                    if (allPODetails.Count > 0 && prodUOMs.Count > 0)
                    {
                        grdList.ModifyComboBoxCell_Tunning(allProduct, e.RowIndex, bu, 3, 0, prodUOMs);
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
            {
                CalculateRow(grdList, grdList.CurrentRow.Index);
            }
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

                var editCols = new int[] { 3, 4, 7, 8 };

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
                            grd.BindDataGridViewRow(dataGridList, initDataGridList, dt, 0, cell0.EditedFormattedValue.ToString(), currentRowIndex, 0, ref validateNewRow, "REProduct", false);
                    }

                    if (isNewRow)
                        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 3];
                }
                else if (editCols.Contains(curentColIndex))
                {
                    grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 1];
                }
                else if (curentColIndex == 5)
                {
                    grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 2];
                }
                else if (curentColIndex == 9)
                {
                    isAdd = true;

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

        private void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            //grdList.SetDeleteKeyDown(sender, e);
            if (e.KeyCode == Keys.Delete)
            {
                DataGridView _grd = (DataGridView)sender;
                if (_grd != null && _grd.CurrentRow != null)
                {
                    //edit by sailom 10-06-2021
                    foreach (DataGridViewRow rowselected in _grd.SelectedRows)
                    {
                        int rowIndex = rowselected.Index;
                        if (_grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "White" || _grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "0")
                        {
                            _grd.Rows.RemoveAt(rowIndex);

                            if (rowIndex <= _grd.RowCount - 1)
                            {
                                CalculateRow(_grd, rowIndex);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                    //int rowIndex = _grd.CurrentRow.Index;
                    //if (_grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "White" || _grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "0")
                    //{
                    //    _grd.Rows.RemoveAt(rowIndex);

                    //    if (rowIndex <= _grd.RowCount - 1)
                    //    {
                    //        CalculateRow(_grd, rowIndex);
                    //    }
                    //}
                    //else
                    //{
                    //    return;
                    //}
                }
            }
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            grdList.SetUserDeletingRow(sender, e);
        }

        #endregion

        private void frmRE_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            bool flagExExcel = false;
            if (!string.IsNullOrEmpty(txdDocNo.Text))
            {
                var chkPO = bu.GetPOMaster(txdDocNo.Text);
                if (chkPO != null)
                    flagExExcel = true;
            }

            if (flagExExcel)
            {
                FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocNo", txdDocNo.Text);
                this.OpenExcelReportsPopup("ใบรับสินค้า", "Form_RE_XSLT.xslt", "Form_RE", _params, true);
            }
            else
            {
                FlexibleMessageBox.Show("ไม่พบช้อมูลใบรับสินค้า", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }

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

                                string whid = bu.tbl_Branchs[0].BranchID + txtWHCode.Text;
                                _params.Add("@WHID", whid);
                                //WHID--------------------------------------
                                //ProductSubGroupID--------------------------------------
                                _params.Add("@ProductSubGroupID", "");
                                //ProductSubGroupID--------------------------------------
                                //ProductID--------------------------------------
                                _params.Add("@ProductID", productID);
                                //ProductID--------------------------------------
                                _params.Add("@FromWH", whid);
                                _params.Add("@ToWH", whid);
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
