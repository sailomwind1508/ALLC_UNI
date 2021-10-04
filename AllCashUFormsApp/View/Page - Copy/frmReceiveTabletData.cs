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
            grdList2.AutoGenerateColumns = false;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdList2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            txtWHName.DisableTextBox(true);

            dateTimePicker1.SetDateTimePickerFormat();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BindSendData()
        {
            DataTable dt = new DataTable();
            dt = bu.GetSendDataTableView();

            DataTable dt2 = new DataTable();
            dt2 = dt.Clone();
            dt2.Clear();

            var temp = new List<DataRow>();
            if (dateTimePicker1.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
            {
                dt = bu.GetSendDataTableViewLastest();
                temp = dt.AsEnumerable().ToList();
            }
            else
            {
                temp = dt.AsEnumerable().Where(x => x.Field<DateTime>("DateSend").ToShortDateString() == dateTimePicker1.Value.ToShortDateString()).ToList();
            }

            if (!string.IsNullOrEmpty(txtWHCode.Text))
            {
                temp = temp.Where(x => x.Field<string>("WHID") == txtWHCode.Text).ToList();

            }

            foreach (DataRow r in temp)
            {
                dt2.Rows.Add(r["WHID"].ToString(), Convert.ToDateTime(r["DateSend"])
                    , Convert.ToBoolean(r["FlagSend"]), Convert.ToBoolean(r["FlagReceive"]));
            }
            grdList2.DataSource = dt2;

            for (int i = 0; i < grdList2.Rows.Count; i++)
            {
                foreach (DataRow r in dt2.Rows)
                {
                    if (r["WHID"].ToString() == grdList2.Rows[i].Cells[0].Value.ToString())
                    {
                        grdList2.Rows[i].Cells[2].Value = Convert.ToBoolean(r["FlagSend"]);
                        grdList2.Rows[i].Cells[3].Value = Convert.ToBoolean(r["FlagReceive"]);
                    }

                }

            }
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
                {
                    this.BindData("BranchWarehouse", searchBranchWarehouseControls, txt.Text);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindSendData();
            if (grdList2.Rows.Count != 0)
            {
                btnRemove.Enabled = true;
            }
        }

        private void frmReceiveTabletData_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();

        }

        private void BindBranchWarehouse()
        {
            var WH2 = bu.GetAllBranchWarehouse(x => x.WHID == txtWHCode.Text);
            if (WH2.Count != 0)
            {
                txtWHName.Text = WH2[0].WHName;
            }
            else
            {
                txtWHCode.Clear();
                txtWHName.Clear();
                txtWHCode.Focus();
                return;
            }

        }

        private void btnSearchVan_Click(object sender, EventArgs e)
        {
            this.OpenBranchWarehousePopup(searchBranchWarehouseControls, "เลือกคลังสินค้า", (x => x.VanType != 0));
        }

        private void txtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtWHCode.Text != "")
                {
                    BindBranchWarehouse();
                }
            }
        }
        private void Save(DataGridViewCellEventArgs e)
        {
            string _WHID = grdList2.Rows[e.RowIndex].Cells[0].Value.ToString();
            DateTime _datesend = Convert.ToDateTime(grdList2.Rows[e.RowIndex].Cells[1].Value);
            List<tbl_SendData> tbl_SendDataList = new List<tbl_SendData>();
            tbl_SendDataList = new tbl_SendData().Select(x => x.WHID == _WHID && x.DateSend == _datesend);
            int ret = 0;
            tbl_SendData sdata = new tbl_SendData();
            sdata = tbl_SendDataList[0];
            sdata.WHID = _WHID;
            sdata.DateSend = _datesend;
            sdata.FlagSend = Convert.ToBoolean(grdList2.Rows[e.RowIndex].Cells[2].Value);
            sdata.FlagReceive = Convert.ToBoolean(grdList2.Rows[e.RowIndex].Cells[3].Value);
            ret = bu.UpdateData(sdata);
        }
        private void grdList2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 4) //ยังไม่ส่งยอด
            {
                grdList2.Rows[e.RowIndex].Cells[2].Value = false;
                grdList2.Rows[e.RowIndex].Cells[3].Value = false;
                string cell0 = grdList2.Rows[e.RowIndex].Cells[0].Value.ToString();
                var conStr = Helper.ConnectionString;
                using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("proc_clear_TL_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add(new SqlParameter("@WHID", cell0));
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                Save(e);
            }
            else if (e.ColumnIndex == 5) // ส่งยอดแล้ว
            {
                grdList2.Rows[e.RowIndex].Cells[2].Value = true;
                Save(e);
            }
            else if (e.ColumnIndex == 6) //ดึงยอดแล้ว
            {
                grdList2.Rows[e.RowIndex].Cells[2].Value = true;
                grdList2.Rows[e.RowIndex].Cells[3].Value = true;
                Save(e);
            }
            else if (e.ColumnIndex == 7) //ลบ
            {
                string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
                string title = "ทำการยืนยัน!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                bu = new SendData();
                int ret = 0;
                var WHID = grdList2.Rows[e.RowIndex].Cells[0].Value.ToString();
                var datesend = grdList2.Rows[e.RowIndex].Cells[1].Value.ToString();
                List<tbl_SendData> tbl_SendDatas = new List<tbl_SendData>();
                tbl_SendDatas = bu.GetSendData(x => x.WHID == WHID && x.DateSend.ToString() == datesend);
                tbl_SendData tbl_SendData2 = new tbl_SendData();
                tbl_SendData2 = tbl_SendDatas[0];
                ret = bu.RemoveData(tbl_SendData2);
                if (ret == 1)
                {
                    string cell0 = grdList2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    var conStr = Helper.ConnectionString;
                    using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("proc_clear_TL_data", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add(new SqlParameter("@WHID", cell0));
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();
                    BindSendData();
                }
            }
        }

        private void grdList2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList2.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }
    }
}
