using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
namespace AllCashUFormsApp.View.Page
{
    public partial class FrmPay : Form
    {
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            btnEdit.Enabled = false;
            btnRemove.Enabled = false;
            btnCopy.Enabled = false;
            btnPrint.Enabled = false;
            gridPayment.Enabled = true;

            EditGrd(gridPayment, false);

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

        private void FixTxt()
        {
            txtTotalSend.Text = "0.00";
            txtTotalCheque.Text = "0.00";
            txtTotalTransfer.Text = "0.00";
            txtTotalDeposit.Text = "0.00";
            txtSumTotalSend.Text = "0.00";
            txtSumPOMaster.Text = "0.00";
            txtSumDiv.Text = "0.00";
            txtTotalGetMoney.Text = "0.00";

            txtBranchName.DisableTextBox(true);

            txtTotalSend.DisableTextBox(true);
            txtTotalCheque.DisableTextBox(true);
            txtTotalTransfer.DisableTextBox(true);
            txtTotalDeposit.DisableTextBox(true);
            txtSumTotalSend.DisableTextBox(true);
            txtSumPOMaster.DisableTextBox(true);
            txtSumDiv.DisableTextBox(true);
            txtTotalGetMoney.DisableTextBox(false);
        }

        private void FrmPay_Load(object sender, EventArgs e)
        {
            InitPage();

            InitialData();
        }

        private void btnSearchBranchCode_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกสาขา/ซุ้ม");
        }

        private void BindBankNote()
        {
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

            }
            else
            {
                MessageBox.Show("ไม่พบยอดขายของวันที่ " + dtpDocDate.Value.ToShortDateString(), "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

        private string IDpayMaster()
        {
            string dateF = dtpDocDate.Value.ToString("yyyyMMdd");
            string IDPM = txtBranchCode.Text + dateF;
            return IDPM;
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

        private void SaveBankNote()
        {
            try
            {
                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;
                BankNote bu = new BankNote();

                string DocNumber = gridPayment.Rows[0].Cells[8].Value.ToString();
                List<tbl_PayMaster> payMastersList = bu.GetPayMaster(x => x.DocNo == DocNumber && x.FlagDel == false);
                string DocNoFormat = IDpayMaster();
                if (payMastersList.Count == 0) // ไม่มีข้อมูลใน PayMaster 
                {   //ดึงวันที่ใน POMASTER 
                    var datePO = bu.GetAllPOMaster("IV", x => x.DocTypeCode == "IV" && x.DocStatus == "4" && x.DocDate.ToShortDateString() == dtpDocDate.Value.ToShortDateString());
                    bu = new BankNote();
                    tbl_PayMaster pdataN = new tbl_PayMaster();
                    pdataN.AutoID = ForMatAutoIDPayMaster();

                    pdataN.DocNo = DocNoFormat;
                    var branch = bu.GetBranch();
                    pdataN.BranchID = branch[0].BranchCode;
                    pdataN.Docdate = datePO[0].DocDate;
                    pdataN.CrDate = DateTime.Now;
                    pdataN.CrUser = Helper.tbl_Users.Username;
                    pdataN.EdDate = null;
                    pdataN.EdUser = null;
                    pdataN.FlagDel = false;
                    pdataN.FlagSend = false;
                    pdataN.TotalSend = Convert.ToDecimal(txtTotalSend.Text);//

                    //BankNote buDetail = new BankNote();
                    List<tbl_PayDetail> payDetailList = new List<tbl_PayDetail>();
                    int PayDetailID = 0;
                    PayDetailID = ForMatautoIDPayDetail();
                    for (int i = 0; i < gridPayment.Rows.Count; i++)
                    {
                        tbl_PayDetail PD = new tbl_PayDetail();
                        PD.DocNo = DocNoFormat;
                        PD.AutoID = PayDetailID;
                        PD.WHID = gridPayment.Rows[i].Cells["colWHID"].Value.ToString();
                        PD.Send = Convert.ToDecimal(gridPayment.Rows[i].Cells["colSend"].Value);
                        PD.Cheque = Convert.ToDecimal(gridPayment.Rows[i].Cells["colCheque"].Value);
                        PD.Transfer = Convert.ToDecimal(gridPayment.Rows[i].Cells["colTransfer"].Value);
                        PD.Deposit = Convert.ToDecimal(gridPayment.Rows[i].Cells["colDeposit"].Value);
                        PD.TotalSale = Convert.ToDecimal(gridPayment.Rows[i].Cells["colTotal"].Value);
                        PD.CrDate = DateTime.Now;
                        PD.CrUser = Helper.tbl_Users.Username;
                        PD.FlagDel = false;
                        PD.FlagSend = false;
                        PD.EdDate = null;
                        PD.EdUser = null;
                        payDetailList.Add(PD);
                    }

                    ret = bu.UpdateDataPayDetail(payDetailList);

                    ret = bu.UpdateDataPayMaster(pdataN);
                }
                else
                {  //มีข้อมูล ใน PayMaster แล้ว
                   //ลบอันเก่า
                    tbl_PayMaster PayMasterM = new tbl_PayMaster();
                    PayMasterM = payMastersList[0];
                    PayMasterM.FlagDel = true;
                    PayMasterM.EdDate = DateTime.Now;
                    PayMasterM.EdUser = Helper.tbl_Users.Username;
                    ret = bu.UpdateDataPayMaster(PayMasterM);

                    // เพิ่มอันใหม่ โดยอ้างอิงจาก DocNo เก่า
                    PayMasterM = new tbl_PayMaster();
                    PayMasterM = payMastersList[0];
                    PayMasterM.AutoID = ForMatAutoIDPayMaster();
                    PayMasterM.CrDate = DateTime.Now;
                    PayMasterM.CrUser = Helper.tbl_Users.Username;
                    PayMasterM.EdDate = null;
                    PayMasterM.EdUser = null;
                    PayMasterM.FlagDel = false;
                    PayMasterM.FlagSend = false;
                    PayMasterM.TotalSend = Convert.ToDecimal(txtTotalSend.Text);
                    ret = bu.UpdateDataPayMaster(PayMasterM);

                    //ลบอันเก่า
                    //PayDetail buOld = new PayDetail();
                    bu.tbl_PayDetails.Clear();
                    List<tbl_PayDetail> payDetailsOld = bu.GetPayDetail(x => x.DocNo == DocNumber && x.FlagDel == false);
                    for (int i = 0; i < payDetailsOld.Count; i++)
                    {
                        payDetailsOld[i].EdDate = DateTime.Now;
                        payDetailsOld[i].EdUser = Helper.tbl_Users.Username;
                        payDetailsOld[i].FlagDel = true;
                    }

                    //เพิ่มอันใหม่
                    //PayDetail buNew = new PayDetail();

                    List<tbl_PayDetail> payDetailsNew = new List<tbl_PayDetail>(); //bu.GetPayDetail(x => x.DocNo == DocNumber && x.FlagDel == false);
                    int autoIDpayDetail = 0;
                    autoIDpayDetail = ForMatautoIDPayDetail();
                    for (int i = 0; i < gridPayment.Rows.Count; i++)
                    {
                        tbl_PayDetail p = new tbl_PayDetail();
                        p.AutoID = autoIDpayDetail;
                        p.DocNo = DocNumber;

                        p.Send = Convert.ToDecimal(gridPayment.Rows[i].Cells["colSend"].Value);
                        p.Cheque = Convert.ToDecimal(gridPayment.Rows[i].Cells["colCheque"].Value);
                        p.Transfer = Convert.ToDecimal(gridPayment.Rows[i].Cells["colTransfer"].Value);
                        p.Deposit = Convert.ToDecimal(gridPayment.Rows[i].Cells["colDeposit"].Value);
                        p.TotalSale = Convert.ToDecimal(gridPayment.Rows[i].Cells["colTotal"].Value);
                        p.WHID = gridPayment.Rows[i].Cells["colWHID"].Value.ToString();
                        p.CrDate = DateTime.Now;
                        p.CrUser = Helper.tbl_Users.Username;
                        payDetailsNew.Add(p);
                        //payDetailsNew[i].AutoID = autoIDpayDetail;
                        //payDetailsNew[i].Send = Convert.ToDecimal(gridPayment.Rows[i].Cells[1].Value);
                        //payDetailsNew[i].Cheque = Convert.ToDecimal(gridPayment.Rows[i].Cells[2].Value);
                        //payDetailsNew[i].Transfer = Convert.ToDecimal(gridPayment.Rows[i].Cells[3].Value);
                        //payDetailsNew[i].Deposit = Convert.ToDecimal(gridPayment.Rows[i].Cells[4].Value);
                        //payDetailsNew[i].TotalSale = Convert.ToDecimal(gridPayment.Rows[i].Cells[5].Value);

                        //payDetailsNew[i].CrDate = DateTime.Now;
                        //payDetailsNew[i].CrUser = Helper.tbl_Users.Username;
                    }

                    bu.tbl_PayDetails.Clear();
                    //bu.tbl_PayDetails.AddRange(payDetailsOld);
                    ret = bu.UpdateDataPayDetail(payDetailsOld);

                    bu.tbl_PayDetails.Clear();
                    //bu.tbl_PayDetails.AddRange(payDetailsNew);
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
                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

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
                    if (txtBranchCode.Text == "")
                    {
                        MessageBox.Show("ไม่พบข้อมูล --> เดโป้/สาขา", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    List<tbl_Branch> branchList = new List<tbl_Branch>();
                    branchList = bu.GetBranch(x => x.BranchCode == txtBranchCode.Text);
                    if (branchList.Count != 0)
                    {
                        SaveBankNote();

                        BindBankNote();

                        CalDiv(gridPayment, true);

                        btnSave.Enabled = false;
                        btnCancel.Enabled = false;
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
                //PayMaster bu = new PayMaster();

                //ลบอันเก่า
                string DocNumber = gridPayment.Rows[0].Cells[8].Value.ToString();

                List<tbl_PayMaster> payMastersListOld = bu.GetPayMaster(x => x.DocNo == DocNumber && x.FlagDel == false);

                //PayMaster buOld = new PayMaster(); //อันเก่า
                payMastersListOld[0].FlagDel = true;
                payMastersListOld[0].EdDate = DateTime.Now;
                payMastersListOld[0].EdUser = Helper.tbl_Users.Username;

                List<tbl_PayMaster> payMastersListNew = bu.GetPayMaster(x => x.DocNo == DocNumber && x.FlagDel == false);
                //PayMaster buNew = new PayMaster();
                payMastersListNew[0].AutoID = ForMatAutoIDPayMaster();
                payMastersListNew[0].CrDate = DateTime.Now;
                payMastersListNew[0].FlagDel = false;
                payMastersListNew[0].CrUser = Helper.tbl_Users.Username;
                decimal totalS = 0;
                payMastersListNew[0].TotalSend = totalS;

                bu.tbl_PayMasters.Clear();
                //bu.tbl_PayMasters.AddRange(payMastersListOld);
                ret = bu.UpdateDataPayMaster(payMastersListOld);

                bu.tbl_PayMasters.Clear();
                //bu.tbl_PayMasters.AddRange(payMastersListNew);
                ret = bu.UpdateDataPayMaster(payMastersListNew);

                //Reset ยอดส่งเงินสด,ชำระบิลเครดิต,ยอดโอน,ค่าธรรมเนียม,รวมส่งเงิน,เกินขาด = 0
                //PayDetail buDetailOld = new PayDetail();

                //เก่า
                List<tbl_PayDetail> payDetailsOld = bu.GetPayDetail(x => x.DocNo == DocNumber && x.FlagDel == false);
                for (int i = 0; i < gridPayment.Rows.Count; i++)
                {
                    payDetailsOld[i].FlagDel = true;
                    payDetailsOld[i].EdDate = DateTime.Now;
                    payDetailsOld[i].EdUser = Helper.tbl_Users.Username;
                }

                //เพิ่มแถวใหม่ 
                //PayDetail buDetailNew = new PayDetail();
                List<tbl_PayDetail> payDetailsNew = bu.GetPayDetail(x => x.DocNo == DocNumber && x.FlagDel == false);
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
                //เก่า
                bu.tbl_PayDetails.Clear();
                //bu.tbl_PayDetails.AddRange(payDetailsOld);
                ret = bu.RemoveDataPayDetail(payDetailsOld);

                //ใหม่
                bu.tbl_PayDetails.Clear();
                //bu.tbl_PayDetails.AddRange(payDetailsNew);
                ret = bu.UpdateDataPayDetail(payDetailsNew);

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
                string msg = ex.Message;
                msg.ShowErrorMessage();
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
            btnCancel.Enabled = false;
            btnEdit.Enabled = true;
            btnPrint.Enabled = true;
            btnRemove.Enabled = true;
            btnSave.Enabled = false;
            BindBankNote();
            CalDiv(gridPayment, true);
            gridPayment.Enabled = false;
        }

        private void gridPayment_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
        }

        private void gridPayment_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                gridPayment.SetRowPostPaint(sender, e, this.Font);
            }
            catch
            {

            }

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

        private void gridPayment_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

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
                //tb.PreviewKeyDown -= gridPayment_PreviewKeyDown;
                //tb.PreviewKeyDown += gridPayment_PreviewKeyDown;

                tb.KeyPress -= gridPayment_KeyPress;
                tb.KeyPress += gridPayment_KeyPress;

                //tb.KeyDown += gridPayment_KeyDown;
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
                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@DocDate", dtpDocDate.Value);
                this.OpenCrystalReportsPopup("รายงานส่งเงินประจำวัน", "V_Bank_Note.rpt", "Rep_Bakn_Note", _params);
            }
            else
            {
                MessageBox.Show("ไม่พบยอดขายของวันที่ " + dtpDocDate.Value.ToString("dd/MM/yyyy"), "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

        }

        private void txtTotalGetMoney_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
