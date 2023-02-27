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
    public partial class frmLoadOD : Form
    {
        string docTypeCode = "";
        public static string _branchID = "";
        OD bu = new OD();
        SendToHQ buHQ = new SendToHQ();
        MasterDataControl buMaster = new MasterDataControl();
        List<tbl_Product> allProduct = new List<tbl_Product>();
        public frmLoadOD()
        {
            InitializeComponent();
        }

        #region private_method
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

            var documentType = bu.tbl_DocumentType.FirstOrDefault(x => x.DocTypeCode.Trim() == "OD");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                FormHeader.Text = documentType.DocHeader;
            }
        }

        private void InitialData()
        {
            grdBranch.AutoGenerateColumns = false;
            grdBranch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            grdList.AutoGenerateColumns = false;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnCopy.Enabled = false;

            btnSendData.Enabled = false;
            btnCancelSend.Enabled = false;
            if (!Connection.ConnectionString.Contains("DB_SDSS_UNI_CENTER"))
            {
                string msg = "สามารถใช้ได้เมื่อต่อ CENTER DB เท่านั้น !!";
                msg.ShowWarningMessage();
                return;
            }

            allProduct = bu.tbl_Product;
            btnSearchAllBranch.PerformClick();
        }

        private void BindPODetail(List<tbl_PODetail> poDts)
        {
            var allUOM = bu.tbl_ProductUom;

            for (int i = 0; i < poDts.Count; i++)
            {
                grdList.Rows.Add(1);

                grdList.Rows[i].Cells[0].Value = poDts[i].ProductID;

                string productName = "";
                if (!string.IsNullOrEmpty(poDts[i].ProductName))
                {
                    productName = poDts[i].ProductName;
                }
                else
                {
                    var data = allProduct.FirstOrDefault(x => x.ProductID == poDts[i].ProductID);
                    if (data != null)
                        productName = data.ProductName;
                }

                grdList.Rows[i].Cells[2].Value = productName;
                grdList.Rows[i].Cells[3].Value = allUOM.First(x => x.ProductUomID == poDts[i].OrderUom).ProductUomName;
                grdList.Rows[i].Cells[4].Value = poDts[i].VatType;
                grdList.Rows[i].Cells[5].Value = poDts[i].OrderQty;
                grdList.Rows[i].Cells[6].Value = poDts[i].UnitPrice;
                grdList.Rows[i].Cells[7].Value = poDts[i].LineTotal;
                grdList.Rows[i].Cells[8].Value = poDts[i].OrderUom;
                grdList.Rows[i].Cells[9].Value = poDts[i].DocNo;
            }
        }

        public void BindData(string _DocNo)
        {
            try
            {
                grdList.Rows.Clear();

                string msg = "";
                bool flagOnline = Convert.ToBoolean(grdBranch.CurrentRow.Cells["colOnlineStatus"].Value);
                if (!flagOnline)
                {
                    msg = "The selected branch is offline !! \n Please check your DNS !!";
                    msg.ShowWarningMessage();
                    return;
                }

                if (string.IsNullOrEmpty(_DocNo))
                    return;

                txdDocNo.Text = _DocNo; //popup

                Cursor.Current = Cursors.WaitCursor;
                grdList.Rows.Clear();

                Dictionary<string, object> Params = new Dictionary<string, object>();
                Params.Add("@BranchID", grdBranch.CurrentRow.Cells["colBranchID"].Value.ToString());
                Params.Add("@DocNo", _DocNo);
                Params.Add("@DocTypeCode", docTypeCode);
                bu.GetDocData_AllBranch(Params, docTypeCode);

                var po = bu.tbl_POMaster;
                var poDts = bu.tbl_PODetails;

                if (string.IsNullOrEmpty(po.DocNo))
                {
                    msg = "ไม่พบข้อมูลเลขที่เอกสาร!!!";
                    msg.ShowWarningMessage();

                    btnCancel.PerformClick();
                    return;
                }

                if (poDts != null && poDts.Count > 0)
                {
                    BindPODetail(poDts);
                }

                if (grdList.Rows.Count > 0)
                {
                    btnSendData.Enabled = true;
                    btnCancelSend.Enabled = true;
                }
                else
                {
                    btnSendData.Enabled = false;
                    btnCancelSend.Enabled = false;
                }

                lbl_rowqty.Text = grdList.RowCount.ToNumberFormat();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.Message.ShowErrorMessage();
            }
        }

        #endregion

        private void btnSearchDoc_Click(object sender, EventArgs e)
        {
            grdList.Rows.Clear();
            _branchID = "";

            bool flagOnline = Convert.ToBoolean(grdBranch.CurrentRow.Cells["colOnlineStatus"].Value);
            if (!flagOnline)
            {
                string msg = "The selected branch is offline !! \n Please check your DNS !!";
                msg.ShowWarningMessage();
                return;
            }

            if (grdBranch.CurrentRow != null)
            {
                _branchID = grdBranch.CurrentRow.Cells["colBranchID"].Value.ToString();
            }

            Cursor.Current = Cursors.WaitCursor;
            this.OpenDocPopup("ใบสั่งสินค้า", "AllBranchOD");
            Cursor.Current = Cursors.Default;
        }

        private void frmLoadOD_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

            InitPage();
            InitialData();
        }

        private void btnSearchAllBranch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DataTable newTable = new DataTable();
                newTable.Columns.Add("ChkBranch", typeof(bool));
                newTable.Columns.Add("BranchID", typeof(string));
                newTable.Columns.Add("BranchRefCode", typeof(string));
                newTable.Columns.Add("BranchName", typeof(string));
                newTable.Columns.Add("Pic", typeof(Bitmap));
                newTable.Columns.Add("OnlineStatus", typeof(bool));

                buMaster.proc_GetDNS_Data(); //ดึงข้อมูล DNS ที่ออนไลน์ เฉพาะ ศูนย์ U-Force

                var dtBranch = buMaster.Get_proc_SendProductInfo_GetDataTable();

                Bitmap wifi_Img = new Bitmap(Properties.Resources.wifi);
                Bitmap power_off_lmg = new Bitmap(Properties.Resources.closeBtn);

                foreach (DataRow r in dtBranch.Rows)
                {
                    Bitmap colPic = Convert.ToBoolean(r["OnlineStatus"]) == true ? wifi_Img : power_off_lmg;
                    newTable.Rows.Add(false
                        , r["BranchID"].ToString()
                        , r["BranchRefCode"].ToString()
                        , r["BranchName"].ToString()
                        , colPic
                        , r["OnlineStatus"]);
                }

                grdBranch.DataSource = newTable;
                lblgrdQty.Text = newTable.Rows.Count.ToNumberFormat();

                if (!string.IsNullOrEmpty(txdDocNo.Text))
                {
                    BindData(txdDocNo.Text);
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.Message.ShowErrorMessage();
            }
        }

        private void txdDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindData(txdDocNo.Text);
            }
        }

        private void grdBranch_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdBranch.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLoadOD_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            string msg = "";

            if (grdList.RowCount == 0)
            {
                msg = "ไม่พบข้อมูลใบเบิกสินค้า OD !!";
                msg.ShowWarningMessage();
                return;
            }

            string cfMsg = "ต้องการส่งข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการส่ง!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                string _BranchID = grdList.Rows[0].Cells["colDocNo"].Value.ToString().Substring(2,3);
                Dictionary<string, object> Params = new Dictionary<string, object>();
                Params.Add("@BranchID", _BranchID);
                Params.Add("@DocNo", grdList.Rows[0].Cells["colDocNo"].Value.ToString());
                Params.Add("@DocTypeCode", docTypeCode);
                bool ret = buHQ.CallSendDataToCenter_OD(Params);

                Cursor.Current = Cursors.Default;
                if (ret)
                {
                    msg = "ส่งข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                }
                else
                {
                    msg = "ส่งข้อมูลล้มเหลว!!";
                    msg.ShowErrorMessage();
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.Message.ShowErrorMessage();
            }
        }

        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            btnSearchAllBranch.PerformClick();
        }

        private void grdBranch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
