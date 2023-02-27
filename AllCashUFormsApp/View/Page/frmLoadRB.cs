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
    public partial class frmLoadRB : Form
    {
        RB bu = new RB();
        MasterDataControl buMaster = new MasterDataControl();
        SendToHQ buHQ = new SendToHQ();

        public static string _branchID = "";
        string docTypeCode = "";

        List<tbl_Product> allProduct = new List<tbl_Product>();

        List<tbl_ProductUomSet> allUomSet = new List<tbl_ProductUomSet>();

        List<tbl_ProductUom> uoms = new List<tbl_ProductUom>();
        List<tbl_ProductUom> allUOM = new List<tbl_ProductUom>();
        List<tbl_ProductUom> prodUOMs = new List<tbl_ProductUom>();

        List<tbl_PRDetail> allPRDetails = new List<tbl_PRDetail>();

        public frmLoadRB()
        {
            InitializeComponent();
        }

        #region private_method
        private void InitPage()
        {
            var documentType = bu.tbl_DocumentType.FirstOrDefault(x => x.DocTypeCode.Trim() == "RB");
            if (documentType != null)
            {
                docTypeCode = documentType.DocTypeCode;
                FormHeader.Text = documentType.DocHeader;
                FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");
            }
            var headerPic = bu.tbl_MstMenu.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            txdDocNo.BackColor = Color.Turquoise;
            grdList.SetDataGridViewStyle();
        }

        private void InitialData()
        {
            grdBranch.AutoGenerateColumns = false;
            grdBranch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            grdList.AutoGenerateColumns = false;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            grdList.Rows.Clear();

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

            allUOM = bu.tbl_ProductUom;
            uoms.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
            uoms.AddRange(allUOM);

            allProduct = bu.tbl_Product;
            allUomSet = bu.tbl_ProductUomSet;

            grdList.SetDataGridViewStyle();

            btnSearchAllBranch.PerformClick();
        }

        private void BindPRDetail(List<tbl_PRDetail> prDts)
        {
            allPRDetails = prDts;

            var listPrdID = allPRDetails.Select(x => x.ProductID).ToList();
            prodUOMs.AddRange(bu.GetProductUOM(listPrdID));

            for (int i = 0; i < prDts.Count; i++)
            {
                grdList.Rows.Add(1);

                grdList.Rows[i].Cells["colProductCode"].Value = prDts[i].ProductID;

                string productName = "";
                if (!string.IsNullOrEmpty(prDts[i].ProductName))
                    productName = prDts[i].ProductName;
                else
                {
                    var data = allProduct.FirstOrDefault(x => x.ProductID == prDts[i].ProductID);
                    if (data != null)
                        productName = data.ProductName;
                }

                grdList.Rows[i].Cells["colProductName"].Value = productName;

                var productUOMSet = allUomSet.Where(x => x.ProductID == prDts[i].ProductID && x.UomSetID == prDts[i].OrderUom).ToList();
                if (productUOMSet != null && productUOMSet.Count > 0)
                {
                    grdList.Rows[i].Cells[4].Value = "1*" + productUOMSet[0].BaseQty;
                    grdList.Rows[i].Cells["colUnit"].Value = productUOMSet[0].UomSetName;
                }

                grdList.Rows[i].Cells[5].Value = prDts[i].OrderQty;
                grdList.Rows[i].Cells["colDocNo"].Value = prDts[i].DocNo;
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

                var pr = bu.tbl_PRMaster;
                var prDts = bu.tbl_PRDetails;

                if (string.IsNullOrEmpty(pr.DocNo))
                {
                    msg = "ไม่พบข้อมูลเลขที่เอกสาร!!!";
                    msg.ShowWarningMessage();

                    btnCancel.PerformClick();
                    return;
                }

                if (prDts != null && prDts.Count > 0)
                {
                    BindPRDetail(prDts);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLoadRB_Load(object sender, EventArgs e)
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

                Cursor.Current = Cursors.Default;
                lblgrdQty.Text = newTable.Rows.Count.ToNumberFormat();
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
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
            this.OpenDocPopup("ใบโอนสินค้าระหว่างสาขา", "AllBranchRB");
            Cursor.Current = Cursors.Default;
        }

        private void grdBranch_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            string msg = "";

            if (grdList.RowCount == 0)
            {
                msg = "ไม่พบข้อมูลใบเบิกสินค้า RB !!";
                msg.ShowWarningMessage();
                return;
            }

            string cfMsg = "ต้องการส่งข้อมูลใช่หรือไม่?";
            string title = "ยืนยันการส่ง!!";
            if (!cfMsg.ConfirmMessageBox(title))
                return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string _BranchID = grdList.Rows[0].Cells["colDocNo"].Value.ToString().Substring(2, 3);
                Dictionary<string, object> Params = new Dictionary<string, object>();
                Params.Add("@BranchID", _BranchID);
                Params.Add("@DocNo", grdList.Rows[0].Cells["colDocNo"].Value.ToString());
                Params.Add("@DocTypeCode", docTypeCode);
                bool ret = buHQ.CallSendDataToCenter_RL(Params);

                Cursor.Current = Cursors.Default;
                if (ret == true)
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

        private void txdDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindData(txdDocNo.Text);
            }
        }

        private void frmLoadRB_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
