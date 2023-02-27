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
    public partial class frmEndDay : Form
    {
        MenuBU menuBU = new MenuBU();
        EndDay bu = new EndDay();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        List<tbl_ProductUom> uoms = new List<tbl_ProductUom>();
        string docTypeCode = "";
        int runDigit = 0;
        List<Control> searchBranchControls = new List<Control>();
        List<Control> searchBWHControls = new List<Control>();
        List<Control> searchEmpControls = new List<Control>();
        List<string> readOnlyControls = new List<string>();
        int[] cellEdit = new int[] { };
        int[] numberCell = new int[] { 6 };
        Dictionary<int, string> dataGridList = new Dictionary<int, string>();
        Dictionary<int, string> initDataGridList = new Dictionary<int, string>();

        Dictionary<string, List<string>> tempUpdateDocRef = new Dictionary<string, List<string>>();

        Func<tbl_BranchWarehouse, bool> fbiPredicate = (x => x.VanType != 0); //x.WHID.Contains("V"));

        List<tbl_ProductUomSet> allUomSet = new List<tbl_ProductUomSet>();
        List<tbl_Product> allProduct = new List<tbl_Product>();
        List<tbl_ProductPriceGroup> allProductPrice = new List<tbl_ProductPriceGroup>();
        List<tbl_BranchWarehouse> allBranchWH = new List<tbl_BranchWarehouse>();
        List<tbl_Branch> allBranch = new List<tbl_Branch>();

        List<tbl_POMaster> allPOMaster = new List<tbl_POMaster>();
        List<tbl_PODetail> allPODetails = new List<tbl_PODetail>();
        List<tbl_PRMaster> allPRMaster = new List<tbl_PRMaster>();

        public frmEndDay()
        {
            InitializeComponent();

            searchBranchControls = new List<Control>() { txtBranchCode, txtBranchName };
            readOnlyControls = new List<string>() { txtBranchName.Name, txnTotalDue.Name };
            txtBranchCode.KeyDown += TxtFromBranchID_KeyDown;
            //txdDocNo.KeyDown += TxdDocNo_KeyDown;
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

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "");
            btnAdd.Enabled = false;

            var headerPic = menuBU.GetAllData().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            InitialData();
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);

            //btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            //btnAdd.Enabled = false;

            this.OpenControl(true, readOnlyControls.ToArray(), cellEdit);

            InitHeader();

            txtBranchCode.Focus();

            btnEndDay.Enabled = false;
            btnCancelEndDay.Enabled = false;
        }

        private void InitHeader()
        {
            var b = bu.GetBranch();
            if (b != null)
            {
                this.BindData("FromBranchID", searchBranchControls, b[0].BranchID);
            }

            dtpDocDate.SetDateTimePickerFormat();
        }

        public void BindEndDayData(DateTime docDate, bool isSum = false)
        {
            var dt_po = new DataTable();
            dt_po = bu.GetEndDay_PO(docDate, "IV", isSum);

            var dt_od = bu.GetEndDay_PODetails(docDate, "OD");

            var dt_rl = bu.GetEndDay_PRDetails(docDate, "RL");

            var dt_rb = bu.GetEndDay_PRDetails(docDate, "RB");

            if (dt_po == null && dt_po.Rows.Count == 0 &&
                dt_od == null && dt_od.Rows.Count == 0 &&
                dt_rl == null && dt_rl.Rows.Count == 0 &&
                dt_rb == null && dt_rb.Rows.Count == 0)
            {
                string msg = "ไม่พบข้อมูลเลขที่เอกสาร!!!";
                msg.ShowWarningMessage();

                btnCancel.PerformClick();

                return;
            }

            BindPO(dt_po);
            BindTotalPO(dt_po);

            BindTF(grdOD, dt_od);
            BindTotalTF(grdODTotal, dt_od);

            BindTF(grdRL, dt_rl);
            BindTotalTF(grdRLTotal, dt_rl);

            BindTF(grdRB, dt_rb);
            BindTotalTF(grdRBTotal, dt_rb);

            decimal total = 0.00m;
            if (dt_po != null && dt_po.Rows.Count > 0)
                total = dt_po.AsEnumerable().ToList().Sum(x => x.Field<decimal>("รวมภาษี")).ToDecimalN2();

            txnTotalDue.Text = total.ToStringN2();
        }

        private void BindPO(DataTable dt)
        {
            BindGridView(grdDailySales, dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdDailySales.Columns[0].Visible = false;
                grdDailySales.Columns[2].Visible = false;

                grdDailySales.Columns[4].DefaultCellStyle.Format = "N2";
                grdDailySales.Columns[5].DefaultCellStyle.Format = "N2";
                grdDailySales.Columns[6].DefaultCellStyle.Format = "N2";
                grdDailySales.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grdDailySales.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grdDailySales.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void BindTotalPO(DataTable dt)
        {
            Dictionary<string, string> totalList = new Dictionary<string, string>();
            List<DataRow> rowList = dt.AsEnumerable().ToList();
            foreach (var row in rowList)
            {
                string whid = row.Field<string>("VAN");
                if (totalList.Count(x => x.Key == whid) == 0)
                {
                    decimal sum = 0.00m;
                    sum = rowList.Where(x => x.Field<string>("VAN") == whid).Sum(a => a.Field<decimal>("รวมภาษี"));

                    totalList.Add(whid, sum.ToStringN2());
                }
            }

            if (totalList.Count > 0)
            {
                DataTable newTable = new DataTable();
                newTable.Columns.Add("VAN", typeof(string));
                newTable.Columns.Add("รวมภาษี", typeof(decimal));
                foreach (var item in totalList)
                {
                    newTable.Rows.Add(item.Key, item.Value);
                }

                BindGridView(grdDailySalesTotal, newTable);
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    grdDailySalesTotal.Columns[1].DefaultCellStyle.Format = "N2";
                    grdDailySalesTotal.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        private void BindTF(DataGridView grd, DataTable dt)
        {
            BindGridView(grd, dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                grd.Columns[7].DefaultCellStyle.Format = "N2";
                grd.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grd.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grd.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void BindTotalTF(DataGridView grd, DataTable dt)
        {
            var newData = dt.AsEnumerable().Select(x => new { WHID = x.Field<string>("VAN"), DocNo = x.Field<string>("เลขที่อ้างอิง"), DocRef = x.Field<string>("เลขที่โอน") }).ToList();
            var grpData = newData.GroupBy(x => new { x.WHID, x.DocNo }).Select(x => x.First()).ToList();
            if (grpData != null && grpData.Count > 0)
            {
                DataTable newTable = new DataTable();
                newTable.Columns.Add("VAN", typeof(string));
                newTable.Columns.Add("เลขที่ใบสั่ง", typeof(string));
                newTable.Columns.Add("เลขที่ใบโอน(M)", typeof(string));

                foreach (var item in grpData)
                {
                    newTable.Rows.Add(item.WHID, item.DocNo, item.DocRef);
                }

                BindGridView(grd, newTable);
            }
        }

        private void BindGridView(DataGridView grd, DataTable dt)
        {
            grd.DataSource = null;
            grd.DataSource = dt;
            grd.ReadOnly = true;
        }

        private void ClearData()
        {
            bu.tbl_IVMaster = null;
            bu.tbl_IVDetails.Clear();
            bu.tbl_POMaster = null;
            bu.tbl_PODetails.Clear();
            bu.tbl_PRMaster = null;
            bu.tbl_PRDetails.Clear();
            bu.tbl_InvMovements.Clear();
            bu.tbl_InvTransactions.Clear();
            bu.tbl_InvWarehouses.Clear();
            bu.tbl_SaleBranchSummary = null;
        }

        private void CloseEndDay()
        {
            try
            {
                string docno = string.Empty;
                var cDate = DateTime.Now;

                string cfMsg = "คุณจะไม่สามารถแก้ไขเอกสารของวันที่ " + dtpDocDate.Value.ToDateTimeFormatString() + " ได้อีก?";
                string title = "ยืนยันการปิดวัน!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Cursor.Current = Cursors.WaitCursor;
                List<string> customerIDList = new List<string>();

                bu = new EndDay();
                int ret = 0;

                allUomSet = bu.GetUOMSet();
                allProduct = bu.GetProduct();
                allProductPrice = bu.GetProductPriceGroup();
                allPOMaster = bu.GetAllPOMaster(dtpDocDate.Value);
                allBranchWH = bu.GetAllBranchWarehouse(x => !x.FlagDel && x.WHType == 1);
                allPODetails = bu.GetAllPODetails(dtpDocDate.Value);
                allBranch = bu.GetBranch();
                allPRMaster = bu.GetAllPRMaster(dtpDocDate.Value);

                //PO Tab--------------------------------------------------------------------------------------
                List<int> retList = new List<int>();
                if (grdDailySales.Rows.Count > 0)
                {
                    var allPOEndDay = bu.GetEndDay_PO(dtpDocDate.Value, "IV", true, false);
                    List<string> whidList = new List<string>();

                    for (int i = 0; i < grdDailySales.Rows.Count; i++)
                    {
                        var _cell1 = grdDailySales.Rows[i].Cells[1];
                        var cell7 = grdDailySales.Rows[i].Cells[7];

                        if (_cell1.IsNotNullOrEmptyCell() && (cell7.Value == null || string.IsNullOrEmpty(cell7.Value.ToString())))
                        {
                            string _whid = _cell1.Value.ToString();
                            if (whidList.All(x => x != _whid))
                            {
                                whidList.Add(_whid);
                            }
                        }
                    }

                    foreach (var _whid in whidList)
                    {
                        var tbl_IVMaster = new tbl_IVMaster();
                        tbl_IVMaster = PreparePOInvMasters(_whid, allPOEndDay);

                        //if(!VerifyCustmerFlagBill(tbl_IVMaster))
                        //{
                        //    string msg = "กรู";
                        //    msg.ShowWarningMessage();
                        //    return;
                        //}

                        ClearData();
                        bu.tbl_IVMaster = tbl_IVMaster;

                        if (bu.tbl_ArCustomers.Any(x => x.CustomerID == tbl_IVMaster.CustomerID))
                        {
                            customerIDList.Add(tbl_IVMaster.CustomerID);
                        }

                        var docNos = tempUpdateDocRef.Distinct().FirstOrDefault(x => x.Key == tbl_IVMaster.DocNo).Value;
                        PreparePOInvDetails(docNos, tbl_IVMaster.DocNo);

                        List<tbl_IVDetail> ivDTList = new List<tbl_IVDetail>();
                        ivDTList = bu.tbl_IVDetails.GroupBy(a => new { a.ProductID }).Select(x => x.First()).ToList();

                        foreach (var ivDT in ivDTList)
                        {
                            var tmp = bu.tbl_IVDetails.Where(x => x.ProductID == ivDT.ProductID).ToList();

                            ivDT.OrderQty = tmp.Sum(x => x.OrderQty);
                            ivDT.ReceivedQty = tmp.Sum(x => x.ReceivedQty);
                            ivDT.RejectedQty = tmp.Sum(x => x.RejectedQty);
                            ivDT.StockedQty = tmp.Sum(x => x.StockedQty);
                            ivDT.LineDiscount = tmp.Sum(x => x.LineDiscount);
                            ivDT.LineTotal = tmp.Sum(x => x.LineTotal);
                        }

                        bu.tbl_IVDetails = ivDTList;

                        var _ret = bu.UpdateData();

                        if (_ret == 1)
                        {
                            foreach (string poDocNo in docNos)
                            {
                                bu.tbl_POMaster = allPOMaster.FirstOrDefault(x => x.DocNo == poDocNo);// bu.GetPOMaster(poDocNo);
                                bu.tbl_POMaster.CustInvNO = tbl_IVMaster.DocNo;
                                bu.tbl_POMaster.FlagSend = false;

                                _ret = bu.UpdatePOMaster(bu.tbl_POMaster);
                            }
                        }

                        retList.Add(_ret);

                        ret = retList.All(x => x == 1) ? 1 : 0;

                    }

                    customerIDList = customerIDList.Distinct().ToList();
                    //PO Tab--------------------------------------------------------------------------------------
                }

                //OD Tab-----------------------------------------------------

                retList = new List<int>();
                if (grdOD.Rows.Count > 0)
                {
                    List<string> docNoList = new List<string>();

                    for (int i = 0; i < grdOD.Rows.Count; i++)
                    {
                        var _cell0 = grdOD.Rows[i].Cells[0];
                        var _cell8 = grdOD.Rows[i].Cells[8];
                        if (_cell8.IsNotNullOrEmptyCell())
                        {
                            string _docNo = _cell8.Value.ToString();
                            if (docNoList.All(x => x != _docNo))
                                docNoList.Add(_docNo);
                        }
                    }

                    foreach (var docNo in docNoList)
                    {
                        var tbl_IVMaster = new tbl_IVMaster();
                        tbl_IVMaster = PrepareODInvMaster(docNo);

                        ClearData();
                        bu.tbl_IVMaster = tbl_IVMaster;

                        PreparePOInvDetails(new List<string> { docNo }, tbl_IVMaster.DocNo);

                        var _ret = bu.UpdateData();

                        if (_ret == 1)
                        {

                            bu.tbl_POMaster = allPOMaster.FirstOrDefault(x => x.DocNo == docNo);// bu.GetPOMaster(docNo);
                            bu.tbl_POMaster.DocRef = tbl_IVMaster.DocNo;
                            bu.tbl_POMaster.FlagSend = false;

                            _ret = bu.UpdatePOMaster(bu.tbl_POMaster);

                        }

                        retList.Add(_ret);
                    }

                    ret = retList.All(x => x == 1) ? 1 : 0;
                }

                //OD Tab-----------------------------------------------------

                //RL Tab----------------------------------------------------

                retList = new List<int>();
                if (grdRL.Rows.Count > 0)
                {
                    var dt_rl = bu.GetEndDay_PRDetails(dtpDocDate.Value, "RL");
                    var tmpRLs = dt_rl.AsEnumerable().ToList().Select(x => new { DocNo = x.Field<string>("เลขที่อ้างอิง"), WHID = x.Field<string>("VAN") }).ToList();
                    tmpRLs = tmpRLs.GroupBy(x => new { x.DocNo }).Select(a => a.First()).ToList();

                    foreach (var rlItem in tmpRLs)
                    {
                        var tbl_PRMaster = PreparePRInvMaster("RL", rlItem.DocNo, rlItem.WHID);
                        if (tbl_PRMaster != null)
                        {
                            bu.tbl_PRMaster = tbl_PRMaster;
                            var _ret = bu.UpdatePRMaster(bu.tbl_PRMaster);

                            retList.Add(_ret);
                        }
                    }

                    ret = retList.All(x => x == 1) ? 1 : 0;
                }

                //RL Tab----------------------------------------------------

                //RB Tab----------------------------------------------------

                retList = new List<int>();
                if (grdRB.Rows.Count > 0)
                {
                    var dt_rb = bu.GetEndDay_PRDetails(dtpDocDate.Value, "RB");

                    var tmpRBs = dt_rb.AsEnumerable().ToList().Select(x => new { DocNo = x.Field<string>("เลขที่อ้างอิง"), WHID = x.Field<string>("VAN") }).ToList();
                    tmpRBs = tmpRBs.GroupBy(x => new { x.DocNo }).Select(a => a.First()).ToList();

                    foreach (var rbItem in tmpRBs)
                    {
                        var tbl_PRMaster = PreparePRInvMaster("RB", rbItem.DocNo, rbItem.WHID);
                        if (tbl_PRMaster != null)
                        {
                            bu.tbl_PRMaster = tbl_PRMaster;
                            var _ret = bu.UpdatePRMaster(bu.tbl_PRMaster);
                            retList.Add(_ret);
                        }
                    }

                    ret = retList.All(x => x == 1) ? 1 : 0;
                }

                //RB Tab----------------------------------------------------

                if (ret == 1)
                {
                    ClearData();

                    PrepareSaleBranchSummary(cDate);

                    ret = bu.UpdateSaleBranchSummaryData(bu.tbl_SaleBranchSummary);

                    if (ret == 1)
                    {
                        Cursor.Current = Cursors.Default;

                        //update customer SAP code-----------------------
                        List<bool> retCustList = new List<bool>();
                        foreach (var customerID in customerIDList)
                        {
                            bool result = bu.UpdateCustomerSAPCode(customerID);
                            retCustList.Add(result);
                        }
                        //update customer SAP code-----------------------

                        if (retCustList.All(x => x == true))
                        {
                            //show popup close end day success.
                            string msg = "ปิดวันเรียบร้อยแล้ว";
                            msg.ShowInfoMessage();
                        }

                        BindEndDayData(dtpDocDate.Value, true);

                        var item = bu.GetSaleBranchSummary(x => !x.FlagDel && x.SaleDate.ToShortDateString() == dtpDocDate.Value.ToShortDateString());
                        if (item != null)
                            btnEndDay.Enabled = false;
                        else
                            btnEndDay.Enabled = true;

                        btnCancelEndDay.Enabled = !btnEndDay.Enabled;
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    this.ShowProcessErr();
                    return;
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

        private void CancelEndDay()
        {
            try
            {
                string cfMsg = "คุณกำลังยกเลิกปิดวัน ของวันที่ " + dtpDocDate.Value.ToDateTimeFormatString() + " ?";
                string title = "ยืนยันการยกเลิกปิดวัน!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Cursor.Current = Cursors.WaitCursor;

                var cDate = DateTime.Now;

                allPOMaster = bu.GetAllPOMaster(dtpDocDate.Value);
                var allIVDetails = bu.GetIVDetails(dtpDocDate.Value);
                var allIVMasters = bu.GetIVMaster(dtpDocDate.Value);
                allPRMaster = bu.GetAllPRMaster(dtpDocDate.Value);
                allBranch = bu.GetBranch();

                string user = Helper.tbl_Users.Username;
                string conditionDate = dtpDocDate.Value.ToShortDateString();

                //PO Tab--------------------------------------------------------------------------------------
                ClearData();

                int ret = 0;
                var ivMasters = new List<tbl_IVMaster>();
                ivMasters = allIVMasters.Where(x => x.DocTypeCode.Trim() == "V" && x.Remark == "สร้างใบกำกับจากการปิดวัน" && x.DocDate.ToShortDateString() == conditionDate).ToList(); //bu.GetIVMaster(x => x.DocTypeCode.Trim() == "V" && x.Remark == "สร้างใบกำกับจากการปิดวัน" && x.DocDate.ToShortDateString() == conditionDate);
                if (ivMasters.Count > 0)
                {
                    List<int> _letIVs = new List<int>();
                    foreach (var ivMstItem in ivMasters)
                    {
                        bu.tbl_IVMaster = ivMstItem;
                        string docNo = bu.tbl_IVMaster.DocNo;

                        ret = bu.RemovePOIVMaster(bu.tbl_IVMaster); //remove iv master

                        if (ret == 1)
                        {
                            bu.tbl_IVDetails = allIVDetails.Where(x => x.DocNo == docNo).ToList(); //bu.GetIVDetails(x => x.DocNo == docNo);
                            if (bu.tbl_IVDetails != null && bu.tbl_IVDetails.Count > 0)
                                ret = bu.RemovePOIVDetails(bu.tbl_IVDetails); //remove iv details
                        }

                        if (ret == 1)
                        {
                            var tbl_POMasters = new List<tbl_POMaster>();
                            tbl_POMasters = allPOMaster.Where(x => x.DocTypeCode.Trim() == "IV" && x.DocDate.ToShortDateString() == conditionDate && x.CustInvNO == docNo).ToList();// bu.GetPOMaster(x => x.DocTypeCode.Trim() == "IV" && x.DocDate.ToShortDateString() == conditionDate && x.CustInvNO == docNo);
                            if (tbl_POMasters.Count > 0)
                            {
                                List<int> _letPOs = new List<int>();
                                foreach (tbl_POMaster poItem in tbl_POMasters)
                                {
                                    poItem.CustInvNO = "";
                                    poItem.EdUser = user;
                                    poItem.EdDate = cDate;

                                    _letPOs.Add(bu.UpdatePOMaster(poItem)); //update po master
                                }

                                ret = _letPOs.All(x => x == 1) ? 1 : 0;
                            }

                            _letIVs.Add(ret);
                        }
                    }

                    ret = _letIVs.All(x => x == 1) ? 1 : 0;
                }

                //PO Tab--------------------------------------------------------------------------------------

                //OD Tab--------------------------------------------------------------------------------------
                ClearData();

                ivMasters = new List<tbl_IVMaster>();
                ivMasters = allIVMasters.Where(x => x.DocTypeCode.Trim() == "H" && x.DocDate.ToShortDateString() == conditionDate).ToList();// bu.GetIVMaster(x => x.DocTypeCode.Trim() == "H" && x.DocDate.ToShortDateString() == conditionDate);
                if (ivMasters.Count > 0)
                {
                    List<int> _letIVs = new List<int>();
                    foreach (var ivMstItem in ivMasters)
                    {
                        bu.tbl_IVMaster = ivMstItem;
                        string docNo = bu.tbl_IVMaster.DocNo;

                        ret = bu.RemovePOIVMaster(bu.tbl_IVMaster); //remove iv master

                        if (ret == 1)
                        {
                            bu.tbl_IVDetails = allIVDetails.Where(x => x.DocNo == docNo).ToList();// bu.GetIVDetails(x => x.DocNo == docNo);
                            if (bu.tbl_IVDetails != null && bu.tbl_IVDetails.Count > 0)
                                ret = bu.RemovePOIVDetails(bu.tbl_IVDetails); //remove iv details
                        }
                        if (ret == 1)
                        {
                            var tbl_POMasters = new List<tbl_POMaster>();
                            tbl_POMasters =  allPOMaster.Where(x => x.DocTypeCode.Trim() == "OD" && x.DocRef.Trim() == docNo.Trim()).ToList();// bu.GetPOMaster(x => x.DocTypeCode.Trim() == "OD" && x.DocRef.Trim() == docNo.Trim());
                            if (tbl_POMasters.Count > 0)
                            {
                                List<int> _letODs = new List<int>();
                                foreach (tbl_POMaster poItem in tbl_POMasters)
                                {
                                    poItem.DocRef = "";
                                    poItem.EdUser = user;
                                    poItem.EdDate = cDate;

                                    _letODs.Add(bu.UpdatePOMaster(poItem)); //update po master
                                }

                                ret = _letODs.All(x => x == 1) ? 1 : 0;
                            }

                            _letIVs.Add(ret);
                        }
                    }

                    ret = _letIVs.All(x => x == 1) ? 1 : 0;
                }
                //OD Tab--------------------------------------------------------------------------------------

                //RL Tab--------------------------------------------------------------------------------------
                if (grdRL.Rows.Count > 0)
                {
                    ClearData();
                    List<int> _letRLs = new List<int>();

                    for (int i = 0; i < grdRL.Rows.Count; i++)
                    {
                        var cell8 = grdRL.Rows[i].Cells[8];
                        if (cell8.IsNotNullOrEmptyCell())
                        {
                            string rlDocNo = cell8.Value.ToString();
                            bu.tbl_PRMaster = allPRMaster.FirstOrDefault(x => x.DocNo == rlDocNo); // bu.GetPRMaster(rlDocNo);
                            var rl = bu.tbl_PRMaster;
                            rl.EdDate = cDate;
                            rl.EdUser = user;
                            rl.DocRef = "";

                            _letRLs.Add(bu.UpdatePRMaster(rl));
                        }
                    }

                    ret = _letRLs.All(x => x == 1) ? 1 : 0;
                }
                //RL Tab--------------------------------------------------------------------------------------

                //RB Tab--------------------------------------------------------------------------------------
                if (grdRB.Rows.Count > 0)
                {
                    ClearData();
                    List<int> _letRBs = new List<int>();

                    for (int i = 0; i < grdRB.Rows.Count; i++)
                    {
                        var cell8 = grdRB.Rows[i].Cells[8];
                        if (cell8.IsNotNullOrEmptyCell())
                        {
                            string rlDocNo = cell8.Value.ToString();
                            bu.tbl_PRMaster = allPRMaster.FirstOrDefault(x => x.DocNo == rlDocNo); //bu.GetPRMaster(rlDocNo);
                            var rb = bu.tbl_PRMaster;
                            rb.EdDate = cDate;
                            rb.EdUser = user;
                            rb.DocRef = "";

                            _letRBs.Add(bu.UpdatePRMaster(rb));
                        }
                    }

                    ret = _letRBs.All(x => x == 1) ? 1 : 0;
                }
                //RB Tab--------------------------------------------------------------------------------------

                if (ret == 1)
                {
                    ClearData();

                    var branch = allBranch; //bu.GetBranch();
                    if (branch != null && branch.Count > 0)
                    {
                        bu.tbl_SaleBranchSummary = bu.GetSaleBranchSummary(x => x.BranchID == branch[0].BranchID && x.SaleDate.ToShortDateString() == conditionDate);
                        var sbs = bu.tbl_SaleBranchSummary;
                        sbs.FlagDel = true;
                        sbs.EdDate = cDate;
                        sbs.EdUser = user;

                        ret = bu.UpdateSaleBranchSummaryData(bu.tbl_SaleBranchSummary);

                        if (ret == 1)
                        {
                            //show popup close end day success.
                            Cursor.Current = Cursors.Default;
                            string msg = "ยกเลิกปิดวัน สำเร็จ!!!";
                            msg.ShowInfoMessage();

                            BindEndDayData(dtpDocDate.Value, true);

                            var item = bu.GetSaleBranchSummary(x => !x.FlagDel && x.SaleDate.ToShortDateString() == dtpDocDate.Value.ToShortDateString());
                            if (item != null)
                                btnEndDay.Enabled = false;
                            else
                                btnEndDay.Enabled = true;

                            btnCancelEndDay.Enabled = !btnEndDay.Enabled;
                        }
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    this.ShowProcessErr();
                    return;
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

        private tbl_IVMaster PreparePOInvMasters(string whid, DataTable dt = null)
        {
            tempUpdateDocRef = new Dictionary<string, List<string>>();

            var cDate = DateTime.Now;
            var grd = grdDailySales;

            decimal? oldAmount = 0.00m;
            decimal amount = 0.00m;
            decimal? oldExcVat = 0.00m;
            decimal excVat = 0.00m;
            decimal? oldIncVat = 0.00m;
            decimal? incVat = 0.00m;
            decimal? vatRate = 0.00m;
            decimal? vatAmt = 0.00m;
            decimal freight = 0.00m;
            //string discountType = "";
            decimal? oldDiscount = 0.00m;
            decimal discount = 0.00m;
            decimal totalDue = 0.00m;
            string custName = "";

            var po = new tbl_POMaster();

            bu.tbl_IVMaster = new tbl_IVMaster();
            List<string> custNameList = new List<string>();

            List<string> docNoList = new List<string>();
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    //}
                    //for (int i = 0; i < grd.Rows.Count; i++)
                    //{
                    var cell0 = item["DocNo"].ToString(); //grd.Rows[i].Cells[0];
                    var cell7 = item["เลขใบกำกับภาษี"].ToString();
                    //var cell2 = item["CustomerID"]; // grd.Rows[i].Cells[2];
                    if (!string.IsNullOrEmpty(cell0) && string.IsNullOrEmpty(cell7)) //cell0.IsNotNullOrEmptyCell())
                    {
                        string docNo = cell0; // cell0.Value.ToString();

                        foreach (var docItem in docNo.Split('|').ToList())
                        {
                            bu.tbl_POMaster = allPOMaster.FirstOrDefault(x => x.DocNo == docItem); //bu.GetPOMaster(docItem);
                            var _po = bu.tbl_POMaster;

                            if (_po != null)
                            {
                                po = _po;

                                if (whid == _po.WHID)
                                {
                                    docNoList.Add(docItem);
                                    custNameList.Add(_po.CustomerID);

                                    oldAmount += _po.OldAmount;
                                    amount += _po.Amount.Value;
                                    oldExcVat += _po.OldExcVat;
                                    excVat += _po.ExcVat.Value;
                                    oldIncVat += _po.OldIncVat;
                                    incVat += _po.IncVat;
                                    vatRate += _po.VatRate;
                                    vatAmt += _po.VatAmt;
                                    freight += _po.Freight;
                                    //discountType += _po.DiscountType;
                                    oldDiscount += _po.OldDiscount;
                                    discount += _po.Discount.Value;
                                    totalDue += _po.TotalDue;
                                }
                            }
                        }
                    }
                }
            }

            //custName = string.Join(",", custNameList);
            //custName = custName.Length > 200 ? custName.Substring(0, 200) : custName;

            var iv = bu.tbl_IVMaster;

            string custInvNO = "";
            custInvNO = bu.GenDocNo("V", whid);

            if (tempUpdateDocRef.Count(x => x.Key == custInvNO) == 0)
                tempUpdateDocRef.Add(custInvNO, docNoList.Distinct().ToList());

            iv.DocNo = custInvNO;
            iv.RevisionNo = 0;
            iv.DocTypeCode = "V";
            iv.DocStatus = po.DocStatus;
            iv.DocDate = dtpDocDate.Value.ToDateTimeFormat();
            iv.DocRef = "";
            iv.StatusInOut = null;
            iv.SupplierID = "0";
            iv.SuppName = "";
            iv.WHID = whid;

            custName = "";
            if (allBranchWH != null && allBranchWH.Count > 0)
            {
                string saleEmpID = allBranchWH.First(x => x.WHID == whid).SaleEmpID;
                iv.EmpID = saleEmpID;
                iv.SaleEmpID = saleEmpID;

                var emp = bu.GetEmployee(x => x.EmpID == saleEmpID);
                if (emp != null && emp.Count > 0)
                {
                    custName = string.Join(" ", emp.First().TitleName, emp.First().FirstName, emp.First().LastName);
                }
            }
            else
            {
                iv.EmpID = "";
                iv.SaleEmpID = "";
            }
            iv.SalAreaID = "";
            iv.Address = bu.GetBranch().First().Address;
            iv.ContactName = "";
            iv.ContactTel = "";
            iv.Shipto = "";
            iv.CreditDay = 0;
            iv.CustType = "";
            iv.DueDate = cDate;
            iv.CustomerID = whid;//po.WHID;
            iv.CustName = custName;

            iv.CustInvNO = "";
            iv.CustInvDate = cDate;
            iv.CustPODate = po.CustPODate;
            iv.CustPONo = po.DocRef;
            iv.Remark = "สร้างใบกำกับจากการปิดวัน";
            iv.Comment = "";
            iv.OldAmount = oldAmount;
            iv.Amount = amount;
            iv.OldExcVat = oldExcVat;
            iv.ExcVat = excVat;
            iv.OldIncVat = oldIncVat;
            iv.IncVat = incVat;
            iv.VatRate = vatRate;
            iv.VatAmt = vatAmt;
            iv.Freight = freight;
            iv.DiscountType = po.DiscountType;
            iv.OldDiscount = oldDiscount;
            iv.Discount = discount;
            iv.TotalDue = totalDue;
            iv.ApprovedBy = null;
            iv.ApprovedDate = null;
            iv.PayType = 0;
            iv.CrDate = cDate;
            iv.CrUser = Helper.tbl_Users.Username;

            //iv.EdDate = null;
            //iv.EdUser = Helper.tbl_Users.Username;
            iv.FlagDel = false;
            iv.FlagSend = false;
            iv.OldTotalCom = 0.00m;
            iv.TotalCom = 0.00m;
            iv.CNType = 0;

            Func<tbl_Branch, bool> whFunc = (x => x.BranchCode == po.WHID.Split('V')[0]);
            var wh = allBranch.Where(whFunc).ToList();   //bu.GetBranch(whFunc);
            if (wh != null && wh.Count > 0)
                iv.FromWHCode = wh[0].VanCode;

            iv.FromLocCode = "VC4STV" + po.WHID.Split('V')[1].ToString();
            iv.ToWHCode = null;
            iv.ToLocCode = null;

            //if (bu.tbl_IVMaster != null)
            //    tbl_IVMasters.Add(bu.tbl_IVMaster);
            //}

            return bu.tbl_IVMaster;
        }

        private void PreparePOInvDetails(List<string> docNos, string ivDocNo)
        {
            List<tbl_PODetail> poDTList = new List<tbl_PODetail>();
            
            foreach (var docNo in docNos)
            {
                var _tbl_PODetails = allPODetails.Where(x => x.DocNo == docNo).ToList(); // bu.GetPODetails(docNo);
                poDTList.AddRange(_tbl_PODetails);
            }

            DateTime crDate = DateTime.Now;

            foreach (tbl_PODetail _podt in poDTList)
            {
                var ivDt = new tbl_IVDetail();
                ivDt.DocNo = ivDocNo;
                ivDt.Line = _podt.Line;
                ivDt.ProductID = _podt.ProductID;
                ivDt.ProductName = _podt.ProductName;

                decimal UnitComPrice = 0.00m;
                decimal LineComTotal = 0.00m;

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

                //25012021
                //if (_podt.OrderUom == 1 || _podt.OrderUom == 2)
                //    ivDt.OrderUom = 2;// _podt.OrderUom;
                //else
                //    ivDt.OrderUom = _podt.OrderUom;

                ivDt.OrderQty = unitQty;
                ivDt.ReceivedQty = unitQty;

                //ivDt.UnitPrice = _podt.UnitPrice.Value;
                decimal unitPrice = 0;
                Func<tbl_ProductPriceGroup, bool> tbl_ProductPriceGroupPre = (x => x.ProductUomID == tmpOrderUom && x.ProductID == _podt.ProductID);
                var prdPriceGroup = allProductPrice.FirstOrDefault(tbl_ProductPriceGroupPre); //29012021
                if (prdPriceGroup != null)
                    unitPrice = prdPriceGroup.SellPriceVat.Value;

                ivDt.UnitPrice = unitPrice;

                ivDt.RejectedQty = _podt.RejectedQty != null ? _podt.RejectedQty.Value : 0;
                ivDt.StockedQty = _podt.StockedQty;
                ivDt.LineTotal = _podt.LineTotal.Value;
                ivDt.UnitCost = _podt.UnitCost.Value;
                ivDt.VatType = _podt.VatType.Value;
                ivDt.LineDiscountType = _podt.LineDiscountType;
                ivDt.LineDiscount = _podt.LineDiscount;
                ivDt.UnitComPrice = UnitComPrice;
                ivDt.LineComTotal = LineComTotal;

                ivDt.CustType = _podt.CustType;
                ivDt.CrDate = crDate;
                ivDt.CrUser = Helper.tbl_Users.Username;
                //ivDt.EdDate = null;
                //ivDt.EdUser = Helper.tbl_Users.Username;
                ivDt.FlagDel = false;
                ivDt.FlagSend = false;

                bu.tbl_IVDetails.Add(ivDt);
            }
        }

        private tbl_IVMaster PrepareODInvMaster(string docNo)
        {
            tempUpdateDocRef = new Dictionary<string, List<string>>();
            var cDate = DateTime.Now;
            var grd = grdOD;

            var po = new tbl_POMaster();
            po = allPOMaster.FirstOrDefault(x => x.DocNo == docNo); //bu.GetPOMaster(docNo);

            bu.tbl_IVMaster = new tbl_IVMaster();

            var iv = bu.tbl_IVMaster;

            string ivDocNo = bu.GenDocNo("H", po.WHID);

            iv.DocNo = ivDocNo;
            iv.RevisionNo = 0;
            iv.DocTypeCode = "H";
            iv.DocStatus = po.DocStatus;
            iv.DocDate = dtpDocDate.Value.ToDateTimeFormat();
            iv.DocRef = docNo;
            iv.StatusInOut = null;
            iv.SupplierID = "0";
            iv.SuppName = "";
            iv.WHID = po.WHID;
            iv.EmpID = po.EmpID;
            iv.SaleEmpID = "0";
            iv.SalAreaID = "0";
            iv.Address = "";
            iv.ContactName = "";
            iv.ContactTel = "";
            iv.Shipto = "";
            iv.CreditDay = 0;
            iv.CustType = "";
            iv.DueDate = new DateTime(1900, 1, 1);
            iv.CustomerID = "0";
            iv.CustName = "";
            iv.CustInvNO = "";
            iv.CustInvDate = new DateTime(1900, 1, 1);
            iv.CustPODate = new DateTime(1900, 1, 1);
            iv.CustPONo = "";
            iv.Remark = "สร้างใบโอนสินค้าจากการปิดวัน : " + docNo;
            iv.Comment = "";
            iv.OldAmount = 0.00m;
            iv.Amount = 0.00m;
            iv.OldExcVat = 0.00m;
            iv.ExcVat = 0.00m;
            iv.OldIncVat = 0.00m;
            iv.IncVat = 0.00m;
            iv.VatRate = 0.00m;
            iv.VatAmt = 0.00m;
            iv.Freight = 0.00m;
            iv.DiscountType = "";
            iv.OldDiscount = 0.00m;
            iv.Discount = 0.00m;
            iv.TotalDue = 0.00m;
            iv.ApprovedBy = null;
            iv.ApprovedDate = null;
            iv.PayType = 0;
            iv.CrDate = cDate;
            iv.CrUser = Helper.tbl_Users.Username;

            //iv.EdDate = null;
            //iv.EdUser = Helper.tbl_Users.Username;
            iv.FlagDel = false;
            iv.FlagSend = false;
            iv.OldTotalCom = 0.00m;
            iv.TotalCom = 0.00m;
            iv.CNType = 0;

            var wh = allBranch.Where(x => x.BranchCode == po.WHID.Split('V')[0]).ToList(); //bu.GetBranch(x => x.BranchCode == po.WHID.Split('V')[0]);
            if (wh != null && wh.Count > 0)
            {
                iv.FromWHCode = wh[0].FactoryCode;
                iv.FromLocCode = wh[0].FactoryLocation;
                iv.ToWHCode = wh[0].BranchRefCode;
                iv.ToLocCode = wh[0].SAPPlantID;
            }

            return bu.tbl_IVMaster;
        }

        private tbl_PRMaster PreparePRInvMaster(string docType, string docNo, string whid)
        {
            var cDate = DateTime.Now;

            tbl_PRMaster pr = new tbl_PRMaster();
            pr = allPRMaster.FirstOrDefault(x => x.DocNo == docNo); // bu.GetPRMaster(docNo);
            if (pr != null)
            {
                string code = docType == "RL" ? "C" : "M";
                string docRef = bu.GenDocNo(code, whid);
                pr.DocRef = docRef;
                pr.EdDate = cDate;
                pr.FlagSend = false;
                pr.EdUser = Helper.tbl_Users.Username;
            }

            return pr;
        }

        private void PrepareSaleBranchSummary(DateTime startTime)
        {
            var cDate = DateTime.Now;
            bu.tbl_SaleBranchSummary = new tbl_SaleBranchSummary();
            var obj = bu.tbl_SaleBranchSummary;
            var branch = allBranch; //; bu.GetBranch();
            if (branch != null && branch.Count > 0)
            {
                var b = branch.First();

                obj.BranchID = b.BranchID;
                obj.EmpID = Helper.tbl_Users.EmpID;
                obj.SaleDate = dtpDocDate.Value.ToDateTimeFormat();
                obj.ShiftNo = 1;

                TimeSpan ts = new TimeSpan(startTime.Hour, startTime.Minute, startTime.Second);
                obj.StartTime = ts;

                ts = new TimeSpan(cDate.Hour, cDate.Minute, cDate.Second);
                obj.EndTime = ts;

                obj.TotalSale = Convert.ToDecimal(txnTotalDue.Text);
                obj.TotalExpense = 0.00m;
                obj.NetTotal = 0.00m;
                obj.IsSend = true;
                obj.TotalSend = 0.00m;
                obj.IBDocNo = "";
                obj.INDocNo = "";
                obj.CrDate = cDate;
                obj.CrUser = Helper.tbl_Users.Username;
                obj.FlagDel = false;
                obj.FlagSend = false;
            }
        }

        private bool VerifyCustmerFlagBill(tbl_IVMaster tbl_IVMaster)
        {
            bool ret = true;
            try
            {
                if (tbl_IVMaster != null)
                {
                    string customerID = tbl_IVMaster.CustomerID;
                    if (!string.IsNullOrEmpty(customerID))
                    {
                        var custInfo = bu.GetCustomer(x => x.CustomerID == customerID);
                        if (custInfo != null && custInfo.Count > 0)
                        {
                            if (custInfo[0].FlagBill)
                                ret = false;
                        }
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();

                return ret;
            }
        }

        #endregion

        #region event methods

        private void frmEndDay_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private void TxtFromBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("FromBranchID", searchBranchControls, txt.Text);
            }
        }

        private void btnSearchBranchCode_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกสาขา/ซุ้ม");
        }

        private void btnSearchEndDay_Click(object sender, EventArgs e)
        {
            BindEndDayData(dtpDocDate.Value, false);

            var item = bu.GetSaleBranchSummary(x => !x.FlagDel && x.SaleDate.ToShortDateString() == dtpDocDate.Value.ToShortDateString());
            if (item != null)
                btnEndDay.Enabled = false;
            else
                btnEndDay.Enabled = true;

            btnCancelEndDay.Enabled = !btnEndDay.Enabled;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InitialData();
        }

        private void btnEndDay_Click(object sender, EventArgs e)
        {
            CloseEndDay();
        }

        private void btnCancelEndDay_Click(object sender, EventArgs e)
        {
            CancelEndDay();
        }

        #endregion
    }
}
