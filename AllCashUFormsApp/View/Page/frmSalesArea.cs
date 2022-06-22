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
using AllCashUFormsApp.View.UControl;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmSalesArea : Form
    {
        SaleAreaDistrict bu = new SaleAreaDistrict();
        SaleArea buSale = new SaleArea();
        MenuBU menuBU = new MenuBU();

        List<string> TempCode = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public static string _ProvinceCode = "", _ProvinceName = "", _ProvinceID = "";

        List<tbl_MstArea> tempArea;
        List<tbl_MstDistrict> tempDistrict;
        List<tbl_Branch> branch;

        DataTable tempSalAreaDistrict; //SalAreaDistrictByProvince
        public frmSalesArea()
        {
            InitializeComponent();
            this.Load += frmSalesArea_Load;

            treeView1.AfterCheck += treeView1_AfterCheck;

            btnSave.Click += btnSave_Click;
            btnClose.Click += btnClose_Click;

            grdProvince.CellClick += grdProvince_CellClick;
            grdProvince.RowPostPaint += grdProvince_RowPostPaint;

            this.FormClosed += frmSalesArea_FormClosed;
        }

        #region Private_Method
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

            grdProvince.AutoGenerateColumns = false;
            grdProvince.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            treeView1.CheckBoxes = true;
        }

        private void BindProvinceData()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            var provinceDT = bu.GetProvinceFromSalAreaDistrict();
            provinceDT.Rows.Add("", "");
            grdProvince.DataSource = provinceDT;

            if (provinceDT.Rows.Count > 0)
                btnSave.Enabled = true;
        }

        private void SetProvinceData()
        {
            string _key = grdProvince.CurrentRow.Cells["colProvinceID"].Value.ToString();
            string _value = grdProvince.CurrentRow.Cells["colProvinceCode"].Value.ToString() + ":" + grdProvince.CurrentRow.Cells["colProvinceName"].Value.ToString();

            treeView1.Nodes.Add(_key, _value);

            treeView1.Nodes[0].Checked = tempSalAreaDistrict.Rows.Count > 0 ? true : false;
        }

        private void SetAreaData()
        {
            for (int i = 0; i < tempArea.Count; i++)
            {
                string _ProvinceID1 = tempArea[i].ProvinceID.ToString();
                int index = treeView1.Nodes.IndexOfKey(_ProvinceID1);

                string _key = tempArea[i].AreaID.ToString();
                string _value = tempArea[i].AreaCode + ":" + tempArea[i].AreaName;

                treeView1.Nodes[index].Nodes.Add(_key, _value);

                DataRow r = tempSalAreaDistrict.AsEnumerable().FirstOrDefault(x => x.Field<string>("AreaCode") == tempArea[i].AreaCode);
                treeView1.Nodes[index].Nodes[i == 0 ? 0 : i].Checked = r != null ? true : false;
            }
        }

        private void SetDistrictData()
        {
            for (int i = 0; i < tempDistrict.Count; i++)
            {
                string _DistrictID = tempDistrict[i].DistrictID.ToString();
                string _AreaID = tempDistrict[i].AreaID.ToString();
                string _FullName = tempDistrict[i].DistrictCode + ":" + tempDistrict[i].DistrictName;

                int AreaIndex = treeView1.Nodes[0].Nodes.IndexOfKey(_AreaID);

                DataRow r = tempSalAreaDistrict.AsEnumerable().FirstOrDefault(x => x.Field<string>("DistrictCode") == tempDistrict[i].DistrictCode);

                treeView1.Nodes[0].Nodes[AreaIndex].Nodes.Add(_DistrictID, _FullName);

                if (treeView1.Nodes[0].Nodes[AreaIndex].FirstNode == null)
                {
                    treeView1.Nodes[0].Nodes[AreaIndex].Nodes[0].Checked = r != null ? true : false;
                }
                else
                {
                    treeView1.Nodes[0].Nodes[AreaIndex].LastNode.Checked = r != null ? true : false;
                }
            }
        }

        private void SetTreeViewData()
        {
            SetProvinceData();
            SetAreaData();
            SetDistrictData();
        }

        private void SelectDetails(DataGridViewCellEventArgs e)
        {
            try
            {
                int _columnIndex = 0;
                int _rowIndex = 0;

                if (e != null)
                {
                    if (e.RowIndex == -1 || e.ColumnIndex == -1)
                        return;
                    else
                        _rowIndex = e.RowIndex;
                    _columnIndex = e.ColumnIndex;
                }
                else
                {
                    _columnIndex = grdProvince.CurrentCell.ColumnIndex;
                    _rowIndex = grdProvince.CurrentRow.Index;
                }

                if (grdProvince.Columns[_columnIndex].Name == "colSearch")
                {
                    frmSearchProvince frm = new frmSearchProvince();
                    frm.ShowDialog();

                    if (!string.IsNullOrEmpty(_ProvinceCode))
                    {
                        int currentrow = _rowIndex;
                        int lastrow = grdProvince.Rows.Count - 1;

                        bool Dupplicate = false; //เช็คว่ามีข้อมูลจังหวัดซ้ำหรือไม่ !!

                        for (int i = 0; i < grdProvince.Rows.Count; i++)
                        {
                            string _code = grdProvince.Rows[i].Cells["colProvinceCode"].Value.ToString();
                            if (_code == _ProvinceCode)
                            {
                                Dupplicate = true;
                                break;
                            }
                        }

                        if (Dupplicate == true)
                        {
                            string msg = "ข้อมูลจังหวัดซ้ำ !!";
                            msg.ShowWarningMessage();
                            return;
                        }

                        if (currentrow == lastrow)
                        {
                            var dt = (DataTable)grdProvince.DataSource;
                            dt.Rows[_rowIndex].Delete();
                            dt.Rows.Add(_ProvinceCode, _ProvinceName, Convert.ToInt32(_ProvinceID));
                            dt.Rows.Add("", "", null);
                            grdProvince.DataSource = dt;
                        }
                        else
                        {
                            grdProvince.Rows[_rowIndex].Cells["colProvinceCode"].Value = _ProvinceCode;
                            grdProvince.Rows[_rowIndex].Cells["colProvinceName"].Value = _ProvinceName;
                            grdProvince.Rows[_rowIndex].Cells["colProvinceID"].Value = Convert.ToInt32(_ProvinceID);
                        }
                    }
                }
                else
                {
                    tempArea = null;
                    tempDistrict = null;
                    tempSalAreaDistrict = null;
                    treeView1.Nodes.Clear();

                    Cursor.Current = Cursors.WaitCursor;

                    branch = bu.GetBranch();

                    if (branch.Count > 0)
                    {
                        string _WHID = branch[0].BranchID + "V01";

                        string colName = grdProvince.CurrentRow.Cells[_columnIndex].Value.ToString();
                        if (!string.IsNullOrEmpty(colName))
                        {
                            //ถ้าไม่มีข้อมูล ให้เช็ค ว่าจังหวัดนั้น WHID = '' 
                            int _ProvinceID = Convert.ToInt32(grdProvince.CurrentRow.Cells["colProvinceID"].Value);
                            tempSalAreaDistrict = bu.GetSalAreaDistrictByProvince(_ProvinceID, _WHID);

                            tempArea = bu.GetMstArea(x => x.FlagDel == false && x.ProvinceID == _ProvinceID);
                            List<int> filterAreaID = tempArea.Select(x => x.AreaID).ToList();
                            tempDistrict = bu.GetMstDistrict(x => filterAreaID.Contains(Convert.ToInt32(x.AreaID)));

                            SetTreeViewData();
                        }
                    }

                    Cursor.Current = Cursors.Default;
                }


            }
            catch (Exception ex)
            {
                ex.Message.ShowWarningMessage();
            }
        }

        private List<TreeNode> CheckedNodes()
        {
            List<TreeNode> checked_nodes = new List<TreeNode>();

            for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
            {
                if (treeView1.Nodes[0].Nodes[i].Checked == true)
                {
                    checked_nodes.Add(treeView1.Nodes[0].Nodes[i]);
                }
            }

            return checked_nodes;
        }

        private void Save()
        {
            List<int> ret = new List<int>();
            string msg = "";

            try
            {
                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                List<TreeNode> AreaTreeNode = CheckedNodes();

                if (AreaTreeNode.Count == 0)
                {
                    msg = "กรุณาเลือกเขตการขาย !!";
                    msg.ShowWarningMessage();
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                string _WHID = "";
                if (branch.Count > 0)
                {
                    _WHID = branch[0].BranchID + "V01";
                }

                int ret2 = bu.DeleteByWHID(_WHID); //ลบแค่ V01 ,ไม่ต้องสนว่าจะมีข้อมูล

                var list = new List<tbl_SalAreaDistrict>();

                for (int i = 0; i < AreaTreeNode.Count; i++)
                {
                    int indexArea = AreaTreeNode[i].Index;

                    for (int x = 0; x < treeView1.Nodes[0].Nodes[indexArea].Nodes.Count; x++)//District
                    {
                        if (!treeView1.Nodes[0].Nodes[indexArea].Nodes[x].Checked)
                            continue;

                        List<char> areaText = treeView1.Nodes[0].Nodes[indexArea].Nodes[x].Parent.Text.ToCharArray().ToList();
                        string _AreaName = "";

                        for (int y = 0; y < areaText.Count; y++)
                        {
                            string text = areaText[y].ToString();
                            if (!TempCode.Contains(text) && text != ":")
                            {
                                _AreaName += text;
                            }
                        }

                        List<char> districtText = treeView1.Nodes[0].Nodes[indexArea].Nodes[x].Text.ToCharArray().ToList();
                        string _DistrictCode = "";
                        string _DistrictName = "";

                        for (int z = 0; z < districtText.Count; z++)
                        {
                            string text = districtText[z].ToString();

                            if (!TempCode.Contains(text) && text != ":")
                            {
                                _DistrictName += text;
                            }
                            else if (TempCode.Contains(text))
                            {
                                _DistrictCode += text;
                            }
                        }

                        var data = new tbl_SalAreaDistrict();
                        data.ProvinceName = grdProvince.CurrentRow.Cells["colProvinceName"].Value.ToString();
                        data.SalAreaID = "";
                        data.WHID = _WHID;

                        data.AreaName = _AreaName;

                        data.DistrictCode = _DistrictCode;
                        data.DistrictID = Convert.ToInt32(treeView1.Nodes[0].Nodes[indexArea].Nodes[x].Name);
                        data.DistrictName = _DistrictName;

                        data.PostalCode = "";
                        data.FlagSend = false;

                        list.Add(data);
                    }
                }

                var dt = PrePareSave_SalAreaDistrict(list, _WHID);

                foreach (DataRow r in dt.Rows)
                {
                    ret.Add(bu.InsertSalAreaDistrict(r));
                }

                if (ret.All(x => x == 1))
                {
                    Cursor.Current = Cursors.Default;

                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    SelectDetails(null);
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    this.ShowProcessErr();
                }


            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.Message.ShowErrorMessage();
            }
        }

        private void SetSalAreaDistrict_Table(DataTable dt)
        {
            dt.Columns.Add("SalAreaID", typeof(string));
            dt.Columns.Add("WHID", typeof(string));
            dt.Columns.Add("DistrictID", typeof(int));
            dt.Columns.Add("DistrictCode", typeof(string));
            dt.Columns.Add("DistrictName", typeof(string));
            dt.Columns.Add("AreaName", typeof(string));
            dt.Columns.Add("ProvinceName", typeof(string));
            dt.Columns.Add("PostalCode", typeof(string));
            dt.Columns.Add("FlagSend", typeof(bool));
        }

        private DataTable PrePareSave_SalAreaDistrict(List<tbl_SalAreaDistrict> list, string _WHID)
        {
            var dt = new DataTable("SalAreaDistrict");
            SetSalAreaDistrict_Table(dt);

            string _SalAreaName = _WHID.Substring(3, 3);
            var _dtSalArea = buSale.GetSalAreaData(0, _SalAreaName);

            for (int x = 0; x < _dtSalArea.Rows.Count; x++)
            {
                string _SalAreaID = _dtSalArea.Rows[x].Field<string>("SalAreaID");

                for (int y = 0; y < list.Count; y++)
                {
                    dt.Rows.Add(_SalAreaID
                           , list[y].WHID
                           , list[y].DistrictID
                           , list[y].DistrictCode
                           , list[y].DistrictName
                           , list[y].AreaName
                           , list[y].ProvinceName
                           , list[y].PostalCode
                           , list[y].FlagSend);
                }
            }

            return dt;
        }

        #endregion

        #region Event_Method
        private void frmSalesArea_Load(object sender, EventArgs e)
        {
            InitPage();
            BindProvinceData();
            SelectDetails(null);
        }

        private void grdProvince_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectDetails(e);
        }

        private void grdProvince_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdProvince.SetRowPostPaint(sender, e, this.Font);
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                foreach (TreeNode tn in e.Node.Nodes)
                {
                    tn.Checked = e.Node.Checked;
                }

            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
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

        private void frmSalesArea_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        #endregion

    }
}
