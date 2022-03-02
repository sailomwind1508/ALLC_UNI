using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
namespace AllCashUFormsApp.View.Page
{
    public partial class FrmPay : Form
    {
        CultureInfo cultures = System.Globalization.CultureInfo.GetCultureInfo("th-TH");

        static DataTable dtBankNote;
        MenuBU menuBU = new MenuBU();
        BankNote bu = new BankNote();
        List<Control> searchBranchControls = new List<Control>();
        int[] numberCell = new int[] { 1, 2, 3, 4 };

        public FrmPay()
        {
            InitializeComponent();
            searchBranchControls = new List<Control>() { txtBranchCode, txtBranchName };
        }

        private void EditGrd(DataGridView grd, bool mode)
        {
            grd.Columns["colWHID"].ReadOnly = mode;
            grd.Columns["colSend"].ReadOnly = mode;
            grd.Columns["colCheque"].ReadOnly = mode;
            grd.Columns["colTransfer"].ReadOnly = mode;

            //for (int i = 0; i < gridPayment.Rows.Count; i++)
            //{
            //    grd.Rows[i].Cells[1].ReadOnly = false;
            //    grd.Rows[i].Cells[2].ReadOnly = false;
            //    grd.Rows[i].Cells[3].ReadOnly = false;
            //    grd.Rows[i].Cells[4].ReadOnly = false;
            //}
        }

        private void LockPanel(bool Lock)
        {
            txtBranchCode.DisableTextBox(Lock);
            txtBranchName.DisableTextBox(Lock);
            dtpDocDate.Enabled = !Lock;
            btnSearch.Enabled = !Lock;
            btnCalculateSales.Enabled = !Lock;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            btnEdit.Enabled = false;
            btnRemove.Enabled = false;
            btnCopy.Enabled = false;
            btnPrint.Enabled = false;
            gridPayment.Enabled = true;

            EditGrd(gridPayment, false);

            LockPanel(true);

            gridPayment.Focus();
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

            dtpDocDate.Format = DateTimePickerFormat.Custom;
            dtpDocDate.CustomFormat = "dd/MM/yyyy";
        }

        private void InitialData()
        {
            gridPayment.AutoGenerateColumns = false;

            var b = bu.GetBranch();

            if (b != null)
            {
                this.BindData("FromBranchID", searchBranchControls, b[0].BranchID);
            }

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            btnEdit.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnPrint.Enabled = false;

            FixTxt();
        }

        private void SetZeroToTextBox()
        {
            txtTotalSend.Text = "0.00";
            txtTotalCheque.Text = "0.00";
            txtTotalTransfer.Text = "0.00";
            txtTotalDeposit.Text = "0.00";
            txtSumTotalSend.Text = "0.00";
            txtSumPOMaster.Text = "0.00";
            txtSumDiv.Text = "0.00";
            txtTotalGetMoney.Text = "0.00";
        }

        private void FixTxt()
        {
            SetZeroToTextBox();

            txtBranchName.DisableTextBox(true);

            txtTotalSend.DisableTextBox(true);
            txtTotalCheque.DisableTextBox(true);
            txtTotalTransfer.DisableTextBox(true);
            txtTotalDeposit.DisableTextBox(true);
            txtSumTotalSend.DisableTextBox(true);
            txtSumPOMaster.DisableTextBox(true);
            txtSumDiv.DisableTextBox(true);
            txtTotalGetMoney.DisableTextBox(true);
        }

        private void FrmPay_Load(object sender, EventArgs e)
        {
            InitPage();

            InitialData();
        }

        private void btnSearchBranchCode_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกเดโป้/สาขา");
        }

        private void BindBankNote()
        {
            dtBankNote = new DataTable();

            dtBankNote = bu.GetBankNoteTable(dtpDocDate.Value);

            if (dtBankNote.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("DocNo", typeof(string));
                dt.Columns.Add("WHID", typeof(string));
                dt.Columns.Add("Send", typeof(decimal));
                dt.Columns.Add("Cheque", typeof(decimal));
                dt.Columns.Add("Transfer", typeof(decimal));
                dt.Columns.Add("Deposit", typeof(decimal));
                dt.Columns.Add("TotalSend", typeof(decimal));
                dt.Columns.Add("Total", typeof(decimal));

                foreach (DataRow r in dtBankNote.Rows)
                {
                    dt.Rows.Add(r["DocNo"].ToString(), r["WHID"].ToString(), Convert.ToDecimal(r["Send"]).ToDecimalN2(), Convert.ToDecimal(r["Cheque"]).ToDecimalN2()
                        , Convert.ToDecimal(r["Transfer"]).ToDecimalN2(), Convert.ToDecimal(r["Deposit"]).ToDecimalN2()
                        , Convert.ToDecimal(r["TotalSend"]).ToDecimalN2(), Convert.ToDecimal(r["Total"]).ToDecimalN2());
                }

                gridPayment.DataSource = dt;
                CalDiv(gridPayment, true);

                txtTotalGetMoney.DisableTextBox(false);
            }
            else
            {
                txtTotalGetMoney.DisableTextBox(true);

                gridPayment.DataSource = dtBankNote;

                SetZeroToTextBox();

                FlexibleMessageBox.Show("ไม่พบยอดขายของวันที่ " + dtpDocDate.Value.ToShortDateString(), "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindBankNote();

            if (gridPayment.Rows.Count > 0)
            {
                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnEdit.Enabled = true;
                btnAdd.Enabled = false;
                btnRemove.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnCopy.Enabled = false;
                btnPrint.Enabled = true;

                //EditGrd(gridPayment, false);
            }
        }

        private void CalDiv(DataGridView grd, bool flag = false)
        {
            decimal totalSend = 0;
            decimal totalCheque = 0;
            decimal totalTransfer = 0;
            decimal TotalDeposit = 0;

            decimal sumtotalsend = 0; // cell 5 รวมส่งเงินทั้งหมด
            decimal sumPoMaster = 0; // cell5 รวมยอดขาย
            decimal sumDiv = 0; // cell6 เกินขาด

            for (int i = 0; i < gridPayment.Rows.Count; i++)
            {
                totalSend += Convert.ToDecimal(grd.Rows[i].Cells["colSend"].Value);
                totalCheque += Convert.ToDecimal(grd.Rows[i].Cells["colCheque"].Value);
                totalTransfer += Convert.ToDecimal(grd.Rows[i].Cells["colTransfer"].Value);
                TotalDeposit += Convert.ToDecimal(grd.Rows[i].Cells["colDeposit"].Value);

                decimal divTotal = 0;
                if (Convert.ToDecimal(grd.Rows[i].Cells["colTotalSend"].Value).ToDecimalN2() > 0)
                {
                    divTotal = Convert.ToDecimal(grd.Rows[i].Cells["colTotalSend"].Value).ToDecimalN2() - Convert.ToDecimal(grd.Rows[i].Cells["colTotal"].Value).ToDecimalN2();
                }
                else
                {
                    divTotal = Convert.ToDecimal(grd.Rows[i].Cells["colTotalSend"].Value).ToDecimalN2();
                }
                grd.Rows[i].Cells["colMoneyDiff"].Value = divTotal.ToDecimalN2(); //เกินขาด

                sumtotalsend += Convert.ToDecimal(grd.Rows[i].Cells["colTotalSend"].Value); // รวมส่งเงินทั้งหมด
                sumPoMaster += Convert.ToDecimal(grd.Rows[i].Cells["colTotal"].Value); // รวมยอดขายทั้งหมด
                sumDiv += Convert.ToDecimal(grd.Rows[i].Cells["colMoneyDiff"].Value);// รวมส่งเกินขาดทั้งหมด
            }

            txtTotalSend.Text = totalSend.ToStringN2();
            txtTotalCheque.Text = totalCheque.ToStringN2();
            txtTotalTransfer.Text = totalTransfer.ToStringN2();
            txtTotalDeposit.Text = TotalDeposit.ToStringN2();
            txtSumTotalSend.Text = sumtotalsend.ToStringN2(); //รวมส่งเงินทั้งหมด
            txtSumPOMaster.Text = sumPoMaster.ToStringN2(); //รวมยอดขายทั้งหมด
            txtSumDiv.Text = sumDiv.ToStringN2();

            if (flag == true) // ส่งเงินจริงทั้งหมด = false จะคิดแค่ตอน กดค้นหาเท่านั้น
            {
                //decimal totalDue = sumPoMaster + sumDiv;
                txtTotalGetMoney.Text = txtSumTotalSend.Text; // totalDue.ToStringN2();
            }

        }

        private void CalculateRow(DataGridView grd, int rowIndex)
        {
            try
            {
                decimal Send = 0;
                decimal Transfer = 0;
                decimal Deposit = 0;

                var cellSend = grd.Rows[rowIndex].Cells["colSend"];
                var cellTransfer = grd.Rows[rowIndex].Cells["colTransfer"];
                var cellDeposit = grd.Rows[rowIndex].Cells["colDeposit"];
                var cellTotalSend = grd.Rows[rowIndex].Cells["colTotalSend"];
                var cellTotal = grd.Rows[rowIndex].Cells["colTotal"];

                Send = Convert.ToDecimal(cellSend.EditedFormattedValue);
                Transfer = Convert.ToDecimal(cellTransfer.EditedFormattedValue);
                Deposit = Convert.ToDecimal(cellDeposit.EditedFormattedValue);

                decimal totalsends = Send + Transfer + Deposit;

                grd.Rows[rowIndex].Cells["colTotalSend"].Value = totalsends.ToDecimalN2();

                decimal totalSend = 0;
                totalSend = Convert.ToDecimal(cellTotalSend.EditedFormattedValue) - Convert.ToDecimal(cellTotal.EditedFormattedValue);

                grd.Rows[rowIndex].Cells["colMoneyDiff"].Value = totalSend.ToDecimalN2();
            }
            catch (Exception ex)
            {


            }
        }

        private void btnCalculateSales_Click(object sender, EventArgs e)
        {
            BindBankNote();
            CalDiv(gridPayment, true);
        }

        private string NewDocNoPayMaster()
        {
            //string dateF = DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US"));
            string _DocDate = dtpDocDate.Value.ToString("yyyyMMdd", new CultureInfo("en-US")); //แก้DocNo Run by DocDate //Adisorn 22/12/2564 
            string DocNo = txtBranchCode.Text + _DocDate;
            return DocNo;
        }

        private int ForMatAutoIDPayMaster()
        {
            int autoID = 0;
            var allPayMaster = bu.GetPayMaster();
            if (allPayMaster != null && allPayMaster.Count > 0)
            {
                int payMasterMaxID = allPayMaster.Max(x => x.AutoID);
                autoID = payMasterMaxID + 1;
            }
            else //ไม่เจอ
            {
                autoID = 1;
            }
            return autoID;
        }

        private int ForMatautoIDPayDetail()
        {
            int autoID = 0;
            var allPayDetail = bu.GetPayDetail();
            if (allPayDetail != null && allPayDetail.Count > 0)
            {
                int payDetailMaxID = allPayDetail.Max(x => x.AutoID);
                autoID = payDetailMaxID + 1;
            }
            else //ไม่เจอ
            {
                autoID = 1;
            }
            return autoID;
        }

        private void PrePareSave_PayMaster(tbl_PayMaster payMaster)
        {
            var poMaster = bu.GetAllPOMaster("IV", x => x.DocTypeCode == "IV" && x.DocStatus == "4" && x.DocDate.ToShortDateString() == dtpDocDate.Value.ToShortDateString());

            payMaster.AutoID = ForMatAutoIDPayMaster();

            string DocNoFormat = NewDocNoPayMaster();
            payMaster.DocNo = DocNoFormat;

            var branch = bu.GetBranch();
            payMaster.BranchID = branch[0].BranchCode;
            payMaster.Docdate = poMaster[0].DocDate;

            payMaster.CrDate = DateTime.Now;
            payMaster.CrUser = Helper.tbl_Users.Username;

            payMaster.EdDate = null;
            payMaster.EdUser = null;

            payMaster.FlagDel = false;
            payMaster.FlagSend = false;

            payMaster.TotalSend = Convert.ToDecimal(txtTotalSend.Text);
        }

        private void PrePareSave_PayDetail(List<tbl_PayDetail> payDetailList, string DocNo)
        {
            int _AutoID = ForMatautoIDPayDetail();

            for (int i = 0; i < gridPayment.Rows.Count; i++)
            {
                var paydetail = new tbl_PayDetail();
                paydetail.DocNo = DocNo;
                paydetail.AutoID = _AutoID;
                paydetail.WHID = gridPayment.Rows[i].Cells["colWHID"].Value.ToString();
                paydetail.Send = Convert.ToDecimal(gridPayment.Rows[i].Cells["colSend"].Value);
                paydetail.Cheque = Convert.ToDecimal(gridPayment.Rows[i].Cells["colCheque"].Value);
                paydetail.Transfer = Convert.ToDecimal(gridPayment.Rows[i].Cells["colTransfer"].Value);
                paydetail.Deposit = Convert.ToDecimal(gridPayment.Rows[i].Cells["colDeposit"].Value);
                paydetail.TotalSale = Convert.ToDecimal(gridPayment.Rows[i].Cells["colTotal"].Value);
                paydetail.CrDate = DateTime.Now;
                paydetail.CrUser = Helper.tbl_Users.Username;

                paydetail.FlagDel = false;
                paydetail.FlagSend = false;

                paydetail.EdDate = null;
                paydetail.EdUser = null;

                payDetailList.Add(paydetail);
            }
        }

        private void SaveBankNote_New()
        {
            try
            {
                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;

                List<int> result = new List<int>();

                string DocNumber = gridPayment.Rows[0].Cells["colDocNo"].Value.ToString();

                var payMastersList = bu.GetPayMaster(x => x.DocNo == DocNumber && x.FlagDel == false);

                if (payMastersList.Count == 0) // New Insert
                {
                    var poMaster = bu.GetAllPOMaster("IV", x => x.DocTypeCode == "IV" && x.DocStatus == "4" && x.DocDate.ToShortDateString() == dtpDocDate.Value.ToShortDateString());

                    var payMaster = new tbl_PayMaster();

                    PrePareSave_PayMaster(payMaster);

                    var payDetailList = new List<tbl_PayDetail>();

                    PrePareSave_PayDetail(payDetailList, payMaster.DocNo);

                    ret = bu.UpdateDataPayMaster(payMaster);

                    if (ret == 1)
                    {
                        bu.tbl_PayMasters = new List<tbl_PayMaster>();
                        bu.tbl_PayDetails = payDetailList;
                        ret = bu.PerformUpdateData(); //edit by sailom .k 10/01/2022
                        //ret = bu.UpdateDataPayDetail(payDetailList);
                    }
                }
                else
                {  //Update
                    var paymaster_old = new tbl_PayMaster();
                    paymaster_old = payMastersList[0];
                    paymaster_old.FlagDel = true;
                    paymaster_old.EdDate = DateTime.Now;
                    paymaster_old.EdUser = Helper.tbl_Users.Username;

                    ret = bu.UpdateDataPayMaster(paymaster_old); //Remove PayMaster Old Data

                    if (ret == 1)
                    {
                        var PayMaster = new tbl_PayMaster();
                        PayMaster = payMastersList[0];
                        PayMaster.AutoID = ForMatAutoIDPayMaster();
                        PayMaster.CrDate = DateTime.Now;
                        PayMaster.CrUser = Helper.tbl_Users.Username;
                        PayMaster.EdDate = null;
                        PayMaster.EdUser = null;
                        PayMaster.FlagDel = false;
                        PayMaster.FlagSend = false;
                        PayMaster.TotalSend = Convert.ToDecimal(txtTotalSend.Text);

                        ret = bu.UpdateDataPayMaster(PayMaster); //Save New PayMaster Data
                    }

                    var payDetailsOld = bu.GetPayDetail(x => x.DocNo == DocNumber && x.FlagDel == false);//ข้อมูลเก่า

                    for (int i = 0; i < payDetailsOld.Count; i++)
                    {
                        payDetailsOld[i].EdDate = DateTime.Now;
                        payDetailsOld[i].EdUser = Helper.tbl_Users.Username;
                        payDetailsOld[i].FlagDel = true;
                    }

                    bu.tbl_PayMasters = new List<tbl_PayMaster>();
                    bu.tbl_PayDetails = payDetailsOld;
                    ret = bu.PerformUpdateData(); //edit by sailom .k 10/01/2022
                    //ret = bu.UpdateDataPayDetail(payDetailsOld); //Remove PayDetail Old Data

                    if (ret == 1)
                    {
                        var payDetailsNew = new List<tbl_PayDetail>(); //ข้อมูลใหม่

                        PrePareSave_PayDetail(payDetailsNew, DocNumber);

                        bu.tbl_PayMasters = new List<tbl_PayMaster>();
                        bu.tbl_PayDetails = payDetailsNew;
                        ret = bu.PerformUpdateData(); //edit by sailom .k 10/01/2022
                        //ret = bu.UpdateDataPayDetail(payDetailsNew);
                    }
                }

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    //Send mail to HQ //edit by sailom .k 07/01/2022---------------------------------------
                    if (dtBankNote.Rows.Count > 0)
                    {
                        FormHelper.PrintingReportName = new List<string>();

                        FormHelper.ShowPrintingReportName = false;
                        Dictionary<string, object> _params = new Dictionary<string, object>();
                        _params.Add("@DocDate", dtpDocDate.Value);
                        this.OpenExcelReportsPopup("รายงานส่งเงินประจำวัน", "Rep_Bank_Note.xslt", "Rep_Bank_Note", _params, true);
                        this.OpenExcelReportsPopup("รายงานสรุปส่งเงินประจำวัน", "Rep_BankNote_TotalSalePerMonth.xslt", "Rep_BankNote_TotalSalePerMonth", _params, true);

                        var cdate = dtpDocDate.Value.ToString("dd/MM/yyyy", cultures);

                        FormHelper.CreateAndSendMailWithAttachFile("พบการบันทึกข้อมูลในหน้า Bank Note", bu.tbl_Branchs[0].BranchName, cdate);
                    }
                    //Send mail to HQ //edit by sailom .k 07/01/2022---------------------------------------
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
                ex.Message.ShowErrorMessage();
            }
        }

        //private void SaveBankNote()
        //{
        //    try
        //    {
        //        string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
        //        string title = "ยืนยันการบันทึก!!";

        //        if (!cfMsg.ConfirmMessageBox(title))
        //            return;

        //        int ret = 0;

        //        string DocNumber = gridPayment.Rows[0].Cells[8].Value.ToString();

        //        var payMastersList = bu.GetPayMaster(x => x.DocNo == DocNumber && x.FlagDel == false);

        //        if (payMastersList.Count == 0) // ไม่มีข้อมูลใน PayMaster 
        //        {   
        //            var poMaster = bu.GetAllPOMaster("IV", x => x.DocTypeCode == "IV" 
        //            && x.DocStatus == "4"
        //            && x.DocDate.ToShortDateString() == dtpDocDate.Value.ToShortDateString());

        //            var payMaster = new tbl_PayMaster();
        //            payMaster.AutoID = ForMatAutoIDPayMaster();

        //            string DocNoFormat = NewDocNoPayMaster();
        //            payMaster.DocNo = DocNoFormat;

        //            var branch = bu.GetBranch();
        //            payMaster.BranchID = branch[0].BranchCode;
        //            payMaster.Docdate = poMaster[0].DocDate;

        //            payMaster.CrDate = DateTime.Now;
        //            payMaster.CrUser = Helper.tbl_Users.Username;

        //            payMaster.EdDate = null;
        //            payMaster.EdUser = null;

        //            payMaster.FlagDel = false;
        //            payMaster.FlagSend = false;

        //            payMaster.TotalSend = Convert.ToDecimal(txtTotalSend.Text);


        //            var payDetailList = new List<tbl_PayDetail>();

        //            PrePareSave_PayDetail(payDetailList,DocNoFormat);

        //            ret = bu.UpdateDataPayDetail(payDetailList);

        //            ret = bu.UpdateDataPayMaster(payMastersList);
        //        }
        //        else
        //        {  //มีข้อมูล ใน PayMaster แล้ว
        //           //ลบอันเก่า
        //            var PayMasterM = new tbl_PayMaster();
        //            PayMasterM = payMastersList[0];
        //            PayMasterM.FlagDel = true;
        //            PayMasterM.EdDate = DateTime.Now;
        //            PayMasterM.EdUser = Helper.tbl_Users.Username;
        //            ret = bu.UpdateDataPayMaster(PayMasterM);

        //            // เพิ่มอันใหม่ โดยอ้างอิงจาก DocNo เก่า
        //            PayMasterM = new tbl_PayMaster();
        //            PayMasterM = payMastersList[0];
        //            PayMasterM.AutoID = ForMatAutoIDPayMaster();
        //            PayMasterM.CrDate = DateTime.Now;
        //            PayMasterM.CrUser = Helper.tbl_Users.Username;
        //            PayMasterM.EdDate = null;
        //            PayMasterM.EdUser = null;
        //            PayMasterM.FlagDel = false;
        //            PayMasterM.FlagSend = false;
        //            PayMasterM.TotalSend = Convert.ToDecimal(txtTotalSend.Text);
        //            ret = bu.UpdateDataPayMaster(PayMasterM);

        //            //ลบอันเก่า
        //            //PayDetail buOld = new PayDetail();
        //            //bu.tbl_PayDetails.Clear();

        //            var payDetailsOld = bu.GetPayDetail(x => x.DocNo == DocNumber && x.FlagDel == false);

        //            for (int i = 0; i < payDetailsOld.Count; i++)
        //            {
        //                payDetailsOld[i].EdDate = DateTime.Now;
        //                payDetailsOld[i].EdUser = Helper.tbl_Users.Username;
        //                payDetailsOld[i].FlagDel = true;
        //            }

        //            //เพิ่มอันใหม่
        //            //PayDetail buNew = new PayDetail();

        //            var payDetailsNew = new List<tbl_PayDetail>(); //bu.GetPayDetail(x => x.DocNo == DocNumber && x.FlagDel == false);

        //            int autoIDpayDetail = 0;
        //            autoIDpayDetail = ForMatautoIDPayDetail();
        //            for (int i = 0; i < gridPayment.Rows.Count; i++)
        //            {
        //                var p = new tbl_PayDetail();
        //                p.AutoID = autoIDpayDetail;
        //                p.DocNo = DocNumber;

        //                p.Send = Convert.ToDecimal(gridPayment.Rows[i].Cells["colSend"].Value);
        //                p.Cheque = Convert.ToDecimal(gridPayment.Rows[i].Cells["colCheque"].Value);
        //                p.Transfer = Convert.ToDecimal(gridPayment.Rows[i].Cells["colTransfer"].Value);
        //                p.Deposit = Convert.ToDecimal(gridPayment.Rows[i].Cells["colDeposit"].Value);
        //                p.TotalSale = Convert.ToDecimal(gridPayment.Rows[i].Cells["colTotal"].Value);
        //                p.WHID = gridPayment.Rows[i].Cells["colWHID"].Value.ToString();
        //                p.CrDate = DateTime.Now;
        //                p.CrUser = Helper.tbl_Users.Username;
        //                payDetailsNew.Add(p);
        //                //payDetailsNew[i].AutoID = autoIDpayDetail;
        //                //payDetailsNew[i].Send = Convert.ToDecimal(gridPayment.Rows[i].Cells[1].Value);
        //                //payDetailsNew[i].Cheque = Convert.ToDecimal(gridPayment.Rows[i].Cells[2].Value);
        //                //payDetailsNew[i].Transfer = Convert.ToDecimal(gridPayment.Rows[i].Cells[3].Value);
        //                //payDetailsNew[i].Deposit = Convert.ToDecimal(gridPayment.Rows[i].Cells[4].Value);
        //                //payDetailsNew[i].TotalSale = Convert.ToDecimal(gridPayment.Rows[i].Cells[5].Value);

        //                //payDetailsNew[i].CrDate = DateTime.Now;
        //                //payDetailsNew[i].CrUser = Helper.tbl_Users.Username;
        //            }

        //            //bu.tbl_PayDetails.Clear();
        //            //bu.tbl_PayDetails.AddRange(payDetailsOld);
        //            ret = bu.UpdateDataPayDetail(payDetailsOld);

        //            //bu.tbl_PayDetails.Clear();
        //            //bu.tbl_PayDetails.AddRange(payDetailsNew);
        //            ret = bu.UpdateDataPayDetail(payDetailsNew);
        //        }
        //        if (ret == 1)
        //        {
        //            string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
        //            msg.ShowInfoMessage();
        //        }
        //        else
        //        {
        //            this.ShowProcessErr();
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.WriteLog(this.GetType());
        //        string msg = ex.Message;
        //        msg.ShowErrorMessage();
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save()
        {
            try
            {
                if (gridPayment.Rows.Count != 0)
                {
                    if (string.IsNullOrEmpty(txtBranchCode.Text))
                    {
                        MessageBox.Show("ไม่พบข้อมูล --> เดโป้/สาขา", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    var branchList = bu.GetBranch(x => x.BranchCode == txtBranchCode.Text);

                    if (branchList.Count != 0)
                    {
                        SaveBankNote_New();//11.11.2564 A

                        //SaveBankNote();

                        BindBankNote();

                        CalDiv(gridPayment, true);

                        btnSave.Enabled = false;
                        btnCancel.Enabled = true;
                        btnEdit.Enabled = true;
                        btnPrint.Enabled = true;
                        btnRemove.Enabled = true;
                        gridPayment.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("รหัสคลังไม่ถูกต้อง --> เดโป้/สาขา", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void Remove()
        {
            string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
            string title = "ยืนยันการลบ!!";

            if (!cfMsg.ConfirmMessageBox(title))
                return;

            try
            {
                int ret = 0;

                //ลบอันเก่า
                string DocNumber = gridPayment.Rows[0].Cells[8].Value.ToString();

                var payMastersListOld = bu.GetPayMaster(x => x.DocNo == DocNumber && x.FlagDel == false);

                if (payMastersListOld.Count > 0)
                {
                    //PayMaster buOld = new PayMaster(); //อันเก่า
                    payMastersListOld[0].FlagDel = true;
                    payMastersListOld[0].EdDate = DateTime.Now;
                    payMastersListOld[0].EdUser = Helper.tbl_Users.Username;

                    ret = bu.UpdateDataPayMaster(payMastersListOld);
                }

                var payMastersListNew = bu.GetPayMaster(x => x.DocNo == DocNumber && x.FlagDel == false);
                if (payMastersListNew.Count > 0)
                {
                    //PayMaster buNew = new PayMaster();
                    payMastersListNew[0].AutoID = ForMatAutoIDPayMaster();
                    payMastersListNew[0].CrDate = DateTime.Now;
                    payMastersListNew[0].FlagDel = false;
                    payMastersListNew[0].CrUser = Helper.tbl_Users.Username;
                    decimal totalS = 0;
                    payMastersListNew[0].TotalSend = totalS;
                    ret = bu.UpdateDataPayMaster(payMastersListNew);
                }

                //bu.tbl_PayMasters.Clear();
                //bu.tbl_PayMasters.AddRange(payMastersListOld);

                //bu.tbl_PayMasters.Clear();
                //bu.tbl_PayMasters.AddRange(payMastersListNew);

                //Reset ยอดส่งเงินสด,ชำระบิลเครดิต,ยอดโอน,ค่าธรรมเนียม,รวมส่งเงิน,เกินขาด = 0
                //PayDetail buDetailOld = new PayDetail();

                //เก่า
                var payDetailsOld = bu.GetPayDetail(x => x.DocNo == DocNumber && x.FlagDel == false);
                if (payDetailsOld.Count > 0)
                {
                    for (int i = 0; i < gridPayment.Rows.Count; i++)
                    {
                        payDetailsOld[i].FlagDel = true;
                        payDetailsOld[i].EdDate = DateTime.Now;
                        payDetailsOld[i].EdUser = Helper.tbl_Users.Username;
                    }

                    ret = bu.RemoveDataPayDetail(payDetailsOld);
                }


                //เพิ่มแถวใหม่ 
                var payDetailsNew = bu.GetPayDetail(x => x.DocNo == DocNumber && x.FlagDel == false);
                if (payDetailsNew.Count > 0)
                {
                    decimal num = 0;
                    for (int i = 0; i < gridPayment.Rows.Count; i++)
                    {
                        payDetailsNew[i].AutoID = ForMatautoIDPayDetail();
                        payDetailsNew[i].Send = num.ToDecimalN2();
                        payDetailsNew[i].Cheque = num.ToDecimalN2();
                        payDetailsNew[i].Transfer = num.ToDecimalN2();
                        payDetailsNew[i].Deposit = num.ToDecimalN2();

                        payDetailsNew[i].FlagDel = false;
                        payDetailsNew[i].CrDate = DateTime.Now;
                        payDetailsNew[i].CrUser = Helper.tbl_Users.Username;
                    }

                    ret = bu.UpdateDataPayDetail(payDetailsNew);
                }

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
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
                ex.Message.ShowErrorMessage();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (gridPayment.Rows.Count != 0)
            {
                Remove();
                BindBankNote();
                CalDiv(gridPayment, true);
                txtTotalGetMoney.Text = "0.00";
                gridPayment.Enabled = false;

                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnEdit.Enabled = true;
                btnPrint.Enabled = true;

                btnAdd.Enabled = false;
                btnRemove.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnCopy.Enabled = false;
            }

        }

        private void txtBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBranchCode.Text != "")
                {
                    List<tbl_Branch> BranchWareHouseList = bu.GetBranch(x => x.BranchCode == txtBranchCode.Text);
                    if (BranchWareHouseList.Count != 0)
                    {
                        txtBranchName.Text = BranchWareHouseList[0].BranchName;
                    }
                    else
                    {
                        txtBranchCode.Clear();
                        txtBranchName.Clear();
                    }
                }
                else
                {
                    txtBranchCode.Clear();
                    txtBranchName.Clear();
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LockPanel(false);

            btnCancel.Enabled = false;
            btnEdit.Enabled = true;
            btnPrint.Enabled = true;
            btnRemove.Enabled = true;
            btnSave.Enabled = false;
            BindBankNote();
            CalDiv(gridPayment, true);
            gridPayment.Enabled = false;
        }

        private void gridPayment_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            gridPayment.SetRowPostPaint(sender, e, this.Font);
        }

        private void gridPayment_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                DataGridViewTextBoxEditingControl _grd = (DataGridViewTextBoxEditingControl)sender;
                DataGridView grd = _grd.EditingControlDataGridView;
                if (grd.CurrentCell.ColumnIndex > 0)
                {


                    if (e.KeyCode == Keys.Enter)
                    {
                        int currentRowIndex = grd.CurrentCell.RowIndex;
                        int curentColIndex = grd.CurrentCell.ColumnIndex;
                        grd.ClearSelection();
                        grd.CurrentCell = grd.Rows[currentRowIndex].Cells[curentColIndex + 1];
                    }
                }
                //if (e.KeyCode == Keys.Enter)
                //{
                //    int _columnIndex = gridPayment.CurrentCell.ColumnIndex;
                //    gridPayment.CurrentCell = gridPayment.CurrentRow.Cells[_columnIndex + 1];//
                //}
            }
            catch
            {


            }


        }

        private void gridPayment_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                    //int _columnIndex = gridPayment.CurrentCell.ColumnIndex;
                    //gridPayment.CurrentCell = gridPayment.CurrentRow.Cells[_columnIndex + 1];
                }
            }
            catch
            {

            }

        }

        private void gridPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (gridPayment.CurrentCell.ColumnIndex <= 4)
                {
                    gridPayment.SetCellNumberOnly(sender, e, numberCell.ToList());
                }
            }
            catch
            {

            }

        }

        private void gridPayment_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;

                tb.KeyPress -= gridPayment_KeyPress;
                tb.KeyPress += gridPayment_KeyPress;

            }
            catch
            {


            }

        }

        private void gridPayment_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView grd = sender as DataGridView;
                if (grd != null && grd.CurrentRow != null)
                {

                    if (e.ColumnIndex == 1 ||
                        e.ColumnIndex == 3 ||
                        e.ColumnIndex == 4)
                    {
                        int currentRowIndex = grd.CurrentCell.RowIndex;

                        CalculateRow(gridPayment, currentRowIndex);

                        CalDiv(gridPayment);
                    }
                }
            }
            catch
            {
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dtBankNote.Rows.Count > 0)
            {
                FormHelper.ShowPrintingReportName = true; //edit by sailom .k 07/01/2022

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocDate", dtpDocDate.Value);
                this.OpenExcelReportsPopup("รายงานส่งเงินประจำวัน", "Rep_Bank_Note.xslt", "Rep_Bank_Note", _params, true);
                //this.OpenCrystalReportsPopup("รายงานส่งเงินประจำวัน", "V_Bank_Note.rpt", "Rep_Bakn_Note", _params);
            }
            else
            {
                FlexibleMessageBox.Show("ไม่พบยอดขายของวันที่ " + dtpDocDate.Value.ToString("dd/MM/yyyy"), "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

        }

        private void txtTotalGetMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void FrmPay_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void gridPayment_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string _value = gridPayment.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString();
            if (string.IsNullOrEmpty(_value))
            {
                e.Cancel = true;
            }
        }

        private void gridPayment_CellErrorTextNeeded(object sender, DataGridViewCellErrorTextNeededEventArgs e)
        {

        }
    }
}
