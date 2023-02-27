using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmDocumentStatus : Form
    {
        OD bu = new OD();
        tbl_PRMaster tbl_PRMaster = null;
        tbl_POMaster tbl_POMaster = null;
        public static string _branchID = "";

        public frmDocumentStatus()
        {
            InitializeComponent();
        }

        private void InitPage()
        {
            var menu = bu.tbl_AdmFormList.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (menu != null)
            {
                FormHeader.Text = menu.FormText;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }
            var headerPic = bu.tbl_MstMenu.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnCopy.Enabled = false;
        }

        private void InitialData()
        {
            dtpDocDate.SetDateTimePickerFormat();

            var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
            ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
        }

        public void BindData(string _DocNo)
        {
            try
            {
                if (string.IsNullOrEmpty(_DocNo))
                    return;

                Cursor.Current = Cursors.WaitCursor;

                txdDocNo.Text = _DocNo;

                string msg = "";
                tbl_POMaster = null;
                tbl_PRMaster = null;

                if (_DocNo.Contains("OD") || _DocNo.Contains("RE"))
                {
                    tbl_POMaster = bu.GetPOMaster(_DocNo);
                    if (tbl_POMaster == null)
                        msg = "ไม่พบข้อมูลเลขที่เอกสาร !!!";
                    else
                    {
                        dtpDocDate.Value = tbl_POMaster.DocDate;
                        ddlDocStatus.SelectedValue = tbl_POMaster.DocStatus;
                    }
                }
                else if (_DocNo.Contains("RL") || _DocNo.Contains("RB"))
                {
                    tbl_PRMaster = bu.GetPRMaster(_DocNo);
                    if (tbl_PRMaster == null)
                        msg = "ไม่พบข้อมูลเลขที่เอกสาร !!!";
                    else
                    {
                        dtpDocDate.Value = tbl_PRMaster.DocDate;
                        ddlDocStatus.SelectedValue = tbl_PRMaster.DocStatus;
                    }
                }

                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnCopy.Enabled = false;
                btnCancel.Enabled = false;
                if (tbl_POMaster == null && tbl_PRMaster == null)
                {
                    btnSave.Enabled = false;
                }

                if (!string.IsNullOrEmpty(msg))
                    msg.ShowWarningMessage();

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.Message.ShowErrorMessage();
            }
        }

        private void frmDocumentStatus_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
            InitialData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchDoc_Click(object sender, EventArgs e)
        {
            BindData(txdDocNo.Text.Trim());

            Cursor.Current = Cursors.WaitCursor;
            this.OpenDocPopup("ใบสั่งสินค้าทั้งหมด", "AllDocData");
            Cursor.Current = Cursors.Default;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";
                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                Cursor.Current = Cursors.WaitCursor;

                int ret = 0;
                string msg = "";
                string _DocTypeCode = txdDocNo.Text.Substring(0, 2);

                if (tbl_POMaster == null && tbl_PRMaster == null)
                {
                    msg = "ไม่พบข้อมูลเลขที่เอกสาร !!!";
                    return;
                }

                bu.tbl_POMaster = null;
                bu.tbl_PRMaster = null;
                if (txdDocNo.Text.Contains("OD") || txdDocNo.Text.Contains("RE"))
                {
                    bu.tbl_POMaster = tbl_POMaster;
                    bu.tbl_POMaster.DocStatus = ddlDocStatus.SelectedValue.ToString();
                    bu.tbl_POMaster.EdDate = DateTime.Now;
                    bu.tbl_POMaster.EdUser = Helper.tbl_Users.Username;
                }
                else if (txdDocNo.Text.Contains("RL") || txdDocNo.Text.Contains("RB"))
                {
                    bu.tbl_PRMaster = tbl_PRMaster;
                    bu.tbl_PRMaster.DocStatus = ddlDocStatus.SelectedValue.ToString();
                    bu.tbl_PRMaster.EdDate = DateTime.Now;
                    bu.tbl_PRMaster.EdUser = Helper.tbl_Users.Username;
                }

                ret = bu.PerformUpdateData();

                Cursor.Current = Cursors.Default;
                if (ret == 1)
                {
                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                }
                else
                {
                    this.ShowProcessErr();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void txdDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    BindData(txdDocNo.Text.Trim());
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void frmDocumentStatus_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
