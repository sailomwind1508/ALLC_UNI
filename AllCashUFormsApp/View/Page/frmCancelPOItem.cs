using AllCashUFormsApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmCancelPOItem : Form
    {
        CultureInfo cultures = System.Globalization.CultureInfo.GetCultureInfo("th-TH");

        MenuBU menuBU = new MenuBU();
        CancelPOItem bu = new CancelPOItem();
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        List<Control> searchFromPrdControls = new List<Control>();
        bool isComplete = false;
        bool isCancel = false;
        string docTypeCode = "";
        int runDigit = 0;

        public frmCancelPOItem()
        {
            InitializeComponent();

            searchFromPrdControls = new List<Control> { txtFromProductCode, txtFromProductName };
            txdDocNo.KeyDown += TxdDocNo_KeyDown;
            txtFromProductCode.KeyDown += TxtFromProductCode_KeyDown;
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

            this.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, "1");
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnCopy.Enabled = false;
            btnRemove.Enabled = false;
            btnCancel.Enabled = false;

            var headerPic = menuBU.GetAllData().FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            InitialData();

            progressBar1.Value = 0;
            txtComment.Text = string.Empty;
        }

        private void InitialData()
        {
            this.ClearControl(docTypeCode, runDigit);
        }

        public void BindTabletSalesData(string ivDocNo)
        {
            bu.GetDocData(ivDocNo, "IV");

            var po = bu.tbl_POMaster;

            if (string.IsNullOrEmpty(po.DocNo))
            {
                string msg = "ไม่พบข้อมูลเลขที่เอกสาร!!!";
                msg.ShowWarningMessage();

                btnCancel.PerformClick();

                return;
            }

            if (po != null)
            {
                txdDocNo.Text = po.DocNo;
                txtIVDocNo.Text = po.CustInvNO;
                txtCustomerID.Text = po.CustomerID;
                txtCustName.Text = po.CustName;
                txtBillTo.Text = po.Address;

                Dictionary<string, string> shList = new Dictionary<string, string>();
                bu.GetCustomerShelf(po.CustomerID).AsEnumerable().ToList().ForEach(x => shList.Add(x.Field<string>("ShelfID"), x.Field<string>("ShelfIDName")));
                ddlShelf.BindDropdownList(shList, "key", "value");
                //ddlShelf.DisplayMember = "Text";
                //ddlShelf.ValueMember = "Value";
                //foreach (var item in shList)
                //{
                //    ddlShelf.Items.Add(new { Text = item.Value, Value = item.Key });
                //}

                txnAmount.Text = po.IncVat.Value.ToStringN2(); //po.Amount.Value.ToStringN2();
                txnDiscountType.Text = "0";// po.DiscountType;
                txnDiscountAmt.Text = po.Discount.Value.ToStringN2();
                txnBeforeVat.Text = (po.IncVat.Value - po.Discount.Value - po.VatAmt.Value - po.ExcVat.Value).ToStringN2(); //(po.IncVat.Value - po.VatAmt.Value).ToStringN2();
                txnVatAmt.Text = po.VatAmt.Value.ToStringN2();
                lblVatType.Text = po.VatRate != null ? po.VatRate.Value.ToStringN0() : "";
                txnExcVat.Text = po.ExcVat.Value.ToStringN2();
                txnTotalDue.Text = po.TotalDue.ToStringN2();
            }

            btnSearchDoc.EnableButton(btnAdd, btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, btnExcel);

            txdDocNo.DisableTextBox(false);
            txdDocNo.BackColor = Color.Turquoise;

            btnCopy.Enabled = false;
            btnEdit.Enabled = false;
        }

        private bool ValidateInput()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //validate header
            {
                errList.SetErrMessageList(txdDocNo, lblDocNo);
                //errList.SetErrMessageList(txtShelfNo, lblShelfNo);
                errList.SetErrMessageList(txtFromProductCode, lblFromProductCode);
            }

            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }

            return ret;
        }

        #endregion

        #region event methods

        private void TxtFromProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("CCProduct", searchFromPrdControls, txt.Text);
            }
        }

        private void TxdDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BindTabletSalesData(txdDocNo.Text);
        }


        private void frmCancelPOItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void btnSearchDoc_Click(object sender, EventArgs e)
        {
            this.OpenIVDocPopup("ใบกำกับสินค้า/กำกับภาษี", "CCIV", false);
        }

        private void btnSearchFromProductCode_Click(object sender, EventArgs e)
        {
            this.OpenProductPopup(searchFromPrdControls, "เลือกสินค้า");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                string cfMsg = "ต้องการแก้ไขข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการแก้ไข!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Cursor.Current = Cursors.WaitCursor;

                txtComment.AppendText("--------------------------Begin Precess-----------------------------");
                txtComment.AppendText(Environment.NewLine);

                progressBar1.Minimum = 0;
                progressBar1.Maximum = 100;
                progressBar1.Step = 10;

                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void frmCancelPOItem_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txdDocNo.Text) &&
                    //!string.IsNullOrEmpty(txtShelfNo.Text) &&
                    !string.IsNullOrEmpty(txtFromProductCode.Text))
                {
                    Cursor.Current = Cursors.WaitCursor;

                    var self = (BackgroundWorker)sender;

                    string shelfID = "";
                    if (ddlShelf != null)
                    {
                        string text = "";
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            text = ddlShelf.Text;
                        });

                        shelfID = text;
                    }

                    bu.CallCancelPOItem(txdDocNo.Text, shelfID, txtFromProductCode.Text, self);

                    if (!isCancel)
                        isComplete = true;
                }
            }
            catch (Exception ex)
            {
                isComplete = false;
                ex.WriteLog(this.GetType());
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor.Current = Cursors.Default;

            txtComment.AppendText("--------------------------Complete Precess-----------------------------");
            txtComment.AppendText(Environment.NewLine);

            string msg = "";
            if (isComplete)
            {
                msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";

                //Send mail to HQ //edit by sailom .k 07/01/2022---------------------------------------

                var cdate = DateTime.Now.ToString("dd/MM/yyyy", cultures);

                //FormHelper.CreateAndSendMail("พบการ บิลขาย", bu.tbl_Branchs[0].BranchName, cdate);

                //Send mail to HQ //edit by sailom .k 07/01/2022---------------------------------------
            }
            else
            {
                msg = "เกิดข้อผิดพลาดในการ แก้ไขข้อมูล กรุณาลองใหม่อีกครั้งหรือติดต่อ IT Support!!!";
                if (isCancel)
                {
                    msg = "ยกเลิก การทำรายการแล้ว!!!";
                }
            }

            msg.ShowInfoMessage();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
            Cursor.Current = Cursors.WaitCursor;

            var message = Convert.ToString(e.UserState);
            //Debug.WriteLine(message);
            //statusLabel.Text = message;

            txtComment.AppendText(message);
            txtComment.AppendText(Environment.NewLine);

            if (message.EndsWith(" PERCENT COMPLETE"))
            {
                int percent;
                if (int.TryParse(message.Split(' ')[0], out percent)) {
                    if (percent > 100)
                        percent = 100;

                    progressBar1.Value = percent;
                }
            }
            if (message.EndsWith("row(s)."))
            {
                txtComment.AppendText(message);
                txtComment.AppendText(Environment.NewLine);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion
    }
}
