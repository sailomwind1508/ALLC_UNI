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
using AllCashUFormsApp.Dal;
using System.Data.SqlClient;
using AllCashUFormsApp.View.UControl;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmReceiveTabletData : Form
    {
        MenuBU menuBU = new MenuBU();
        SendData bu = new SendData();
        List<Control> searchBranchWarehouseControls = new List<Control>();

        public frmReceiveTabletData()
        {
            InitializeComponent();
            searchBranchWarehouseControls = new List<Control>() { txtWHCode, txtWHName };

            txtWHCode.KeyDown += TxtWHCode_KeyDown;
            txtWHCode.TextChanged += TxtWHCode_TextChanged;
        }

        #region Private Methods

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
        }

        private void InitialData()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnRemove.Enabled = false;
            grdList.AutoGenerateColumns = false;
            grdCalendar.AutoGenerateColumns = false;

            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdCalendar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            txtWHName.DisableTextBox(true);

            dtpSendDate.SetDateTimePickerFormat();

            //var comp = bu.GetCompany();
            //btnPullSalesAmt.Enabled = (comp.CompanyCode == "104" && (Helper.tbl_Users.Username == "Admin" || Helper.tbl_Users.Username == "superadmin"));
        }

        private void BindBranchWarehouse()
        {
            var bwh = bu.GetAllBranchWarehouse(x => x.WHID == txtWHCode.Text);
            if (bwh.Count != 0)
            {
                txtWHName.Text = bwh[0].WHName;
            }
            else
            {
                txtWHCode.Clear();
                txtWHName.Clear();
                txtWHCode.Focus();

                return;
            }
        }

        private void BindSendData()
        {
            DataTable dt = new DataTable();
            dt = bu.GetSendDataTableView();

            DataTable dtClone = new DataTable();
            dtClone = dt.Clone();
            dtClone.Clear();

            var tempDataRow = new List<DataRow>();
            if (dtpSendDate.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
            {
                dt = bu.GetSendDataTableViewLastest();
                tempDataRow = dt.AsEnumerable().ToList();
            }
            else
                tempDataRow = dt.AsEnumerable().Where(x => x.Field<DateTime>("DateSend").ToShortDateString() == dtpSendDate.Value.ToShortDateString()).ToList();

            if (!string.IsNullOrEmpty(txtWHCode.Text))
                tempDataRow = tempDataRow.Where(x => x.Field<string>("WHID") == txtWHCode.Text).ToList();

            foreach (DataRow r in tempDataRow)
            {
                dtClone.Rows.Add(r["WHID"].ToString(), Convert.ToDateTime(r["DateSend"]), Convert.ToBoolean(r["FlagSend"]), Convert.ToBoolean(r["FlagReceive"]));
            }

            grdCalendar.DataSource = dtClone; //Bind Gridview

            for (int i = 0; i < grdCalendar.Rows.Count; i++)
            {
                foreach (DataRow r in dtClone.Rows)
                {
                    if (r["WHID"].ToString() == grdCalendar.Rows[i].Cells["colWHID"].Value.ToString())
                    {
                        grdCalendar.Rows[i].Cells["colSendFlag"].Value = Convert.ToBoolean(r["FlagSend"]);
                        grdCalendar.Rows[i].Cells["colPullFlag"].Value = Convert.ToBoolean(r["FlagReceive"]);
                    }
                }
            }
        }

        private void BindTLGridview()
        {
            var dt = bu.GetTLData(dtpSendDate.Value);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdList.DataSource = dt;

                grdList.CreateCheckBoxHeaderColumn("colSelect");
            }
            else
                grdList.DataSource = null;

            FormHelper.CheckOverStok1000(dtpSendDate.Value); //Check Over Stock in main stock(1000)
        }

        private void Save(DataGridViewCellEventArgs e, DataGridView grid, int _event, string whid)
        {
            try
            {
                int ret = 0;
                string col1 = "";
                string col2 = "";
                string col3 = "";

                if (grid.Name == "grdList")
                {
                    col1 = "colDateSend2";
                    col2 = "colFlagSend2";
                    col3 = "colFlagReceive2";
                }
                else
                {
                    col1 = "colDateSend";
                    col2 = "colSendFlag";
                    col3 = "colPullFlag";
                }

                var sendDate = grid.Rows[e.RowIndex].Cells[col1];
                var sendFlag = grid.Rows[e.RowIndex].Cells[col2];
                var pullFlag = grid.Rows[e.RowIndex].Cells[col3];

                DateTime _datesend = Convert.ToDateTime(sendDate.Value);

                List<tbl_SendData> sendDataList = new List<tbl_SendData>();
                sendDataList = new tbl_SendData().Select(x => x.WHID == whid && x.DateSend.ToShortDateString() == _datesend.ToShortDateString());

                tbl_SendData sdata = new tbl_SendData();
                sdata = sendDataList.First();
                sdata.WHID = whid;
                sdata.DateSend = _datesend;
                sdata.FlagSend = Convert.ToBoolean(sendFlag.Value);
                sdata.FlagReceive = Convert.ToBoolean(pullFlag.Value);

                ret = bu.UpdateData(sdata);

                if (ret != 0)
                {
                    string msg = "";
                    if (_event == 4)
                    {
                        msg = "ยกเลิกการส่งข้อมูล เรียบร้อยแล้ว!!";
                    }
                    else if (_event == 5)
                    {
                        msg = "ส่งยอดแล้ว เรียบร้อยแล้ว!!";
                    }
                    else if (_event == 6)
                    {
                        msg = "ดึงยอดแล้ว เรียบร้อยแล้ว!!";
                    }
                    msg.ShowInfoMessage();

                    BindSendData();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        #endregion

        #region Event Methods

        private void frmReceiveTabletData_Load(object sender, EventArgs e)
        {
            InitPage();

            InitialData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.Dispose();
            this.Close();
        }

        private void TxtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txt = (TextBox)sender;
                this.BindData("BranchWarehouse", searchBranchWarehouseControls, txt.Text);
            }
        }

        private void TxtWHCode_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (!string.IsNullOrEmpty(txt.Text))
            {
                var bwh = bu.GetBranchWarehouse(x => x.WHID == txt.Text);
                if (bwh != null)
                    this.BindData("BranchWarehouse", searchBranchWarehouseControls, txt.Text);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindSendData();

            if (grdCalendar.Rows.Count != 0)
                btnRemove.Enabled = true;

            BindTLGridview();
        }

        private void btnSearchVan_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchBranchWarehouseControls, "เลือกคลังสินค้า", (x => x.VanType != 0));
        }

        private void txtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtWHCode.Text))
                    BindBranchWarehouse();
            }
        }

        private void grdCalendar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var whid = grdCalendar.Rows[e.RowIndex].Cells["colWHID"];
            var sendFlag = grdCalendar.Rows[e.RowIndex].Cells["colSendFlag"];
            var pullFlag = grdCalendar.Rows[e.RowIndex].Cells["colPullFlag"];
            var sendDate = grdCalendar.Rows[e.RowIndex].Cells["colDateSend"];

            string _whid = whid.Value.ToString();

            if (e.ColumnIndex == 4) //ยังไม่ส่งยอด
            {
                sendFlag.Value = false;
                pullFlag.Value = false;

                int ret = bu.ClearTLdata(_whid);

                if (ret != 0)
                    Save(e, grdCalendar, 4, _whid);
            }
            else if (e.ColumnIndex == 5) // ส่งยอดแล้ว
            {
                sendFlag.Value = true;

                Save(e, grdCalendar, 5, _whid);
            }
            else if (e.ColumnIndex == 6) //ดึงยอดแล้ว
            {
                sendFlag.Value = true;
                pullFlag.Value = true;

                Save(e, grdCalendar, 6, _whid);
            }
            else if (e.ColumnIndex == 7) //ลบ
            {
                string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
                string title = "ทำการยืนยัน!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                bu = new SendData();

                int ret = 0;

                var datesend = Convert.ToDateTime(sendDate.Value).ToShortDateString();

                List<tbl_SendData> tbl_SendDatas = new List<tbl_SendData>();
                tbl_SendDatas = bu.GetSendData(x => x.WHID == _whid && x.DateSend.ToShortDateString() == datesend);

                ret = bu.RemoveData(tbl_SendDatas.First());

                if (ret == 1)
                {
                    ret = bu.ClearTLdata(_whid);

                    if (ret != 0)
                    {
                        string msg = "ลบข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();

                        BindSendData();
                    }
                }
            }
        }

        private void grdCalendar_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdCalendar.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void btnPullSalesAmt_Click(object sender, EventArgs e)
        {
            try
            {
                string whid = "";
                DateTime docDate = new DateTime();
                if (grdList.Rows.Count > 0)
                {
                    List<int> checkList = new List<int>();
                    foreach (DataGridViewRow row in grdList.Rows)
                    {
                        var sel = grdList.Rows[row.Index].Cells["colSelect"].EditedFormattedValue;
                        var docdate = grdList.Rows[row.Index].Cells["colDocDate2"].Value;

                        if (sel != null)
                        {
                            if (docdate != null && !string.IsNullOrEmpty(docdate.ToString()))
                                checkList.Add(row.Index);
                        }
                    }

                    if (checkList.Count > 0)
                    {
                        Dictionary<string, DateTime> whids = new Dictionary<string, DateTime>();
                        foreach (var rowIndex in checkList)
                        {
                            var sel = grdList.Rows[rowIndex].Cells["colSelect"].EditedFormattedValue;
                            whid = grdList.Rows[rowIndex].Cells["colWHID2"].Value.ToString();

                            var chk = (bool)sel;
                            if (chk)
                                whids.Add(whid, Convert.ToDateTime(grdList.Rows[rowIndex].Cells["colDocDate2"].Value));
                                //docDate = Convert.ToDateTime(grdList.Rows[rowIndex].Cells["colDocDate2"].Value); 
                        }

                        if (whids.Count > 0) //(!string.IsNullOrEmpty(whid))
                        {
                            frmConfirmSendData frm = new frmConfirmSendData();
                            frm.PreparePopupForm("ConfirmPreOrder", frm.Name, "ยืนยันการส่งยอดขาย", whids);
                            frm.ShowDialog();
                        }
                        else
                        {
                            string msg = "กรุณาเลือกรายการก่อนดึงยอดขาย!!!";
                            msg.ShowWarningMessage();

                            return;
                        }
                    }
                    else
                    {
                        FlexibleMessageBox.Show("ไม่พบข้อมูลการขาย!!!", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;

                FlexibleMessageBox.Show("ไม่พบข้อมูลการขาย!!!", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
           
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var whid = grdList.Rows[e.RowIndex].Cells["colWHID2"].Value.ToString();
            var datesend = grdList.Rows[e.RowIndex].Cells["colDateSend2"];
            var sendFlag = grdList.Rows[e.RowIndex].Cells["colFlagSend2"];
            var pullFlag = grdList.Rows[e.RowIndex].Cells["colFlagReceive2"];

            if (e.ColumnIndex == 9) //ยังไม่ส่งยอด
            {
                sendFlag.Value = false;
                pullFlag.Value = false;

                var ret = bu.ClearTLdata(whid);

                if (ret != 0)
                {
                    Save(e, grdList, 4, whid);

                    BindTLGridview();
                }
            }
            else if (e.ColumnIndex == 10) //ลบ
            {
                try
                {
                    string cfMsg = "คุณแน่ใจหรือไม่ ที่จะลบข้อมูลในรายการนี้?";
                    string title = "ข้อความ";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    int ret = 0;

                    List<tbl_SendData> tbl_SendDatas = new List<tbl_SendData>();
                    tbl_SendDatas = bu.GetSendData(x => x.WHID == whid && x.DateSend.ToShortDateString() == Convert.ToDateTime(datesend.Value).ToShortDateString());

                    if (tbl_SendDatas != null && tbl_SendDatas.Count > 0)
                    {
                        ret = bu.RemoveData(tbl_SendDatas.First());
                        if (ret == 1)
                        {
                            ret = bu.ClearTLdata(whid);

                            if (ret != 0)
                            {
                                string msg = "ลบข้อมูลเรียบร้อยแล้ว!!";
                                msg.ShowInfoMessage();

                                BindSendData();
                            }
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
        }

        #endregion

        private void frmReceiveTabletData_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
