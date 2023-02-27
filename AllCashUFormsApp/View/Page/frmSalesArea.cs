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

            var dtProvince = new DataTable("Province");
            dtProvince.Columns.Add("ProvinceCode", typeof(string));
            dtProvince.Columns.Add("ProvinceName", typeof(string));
            dtProvince.Columns.Add("ProvinceID", typeof(int));

            var provinceDT = bu.GetProvinceFromSalAreaDistrict();

            if (provinceDT.Rows.Count > 0)
            {
                foreach (DataRow r in provinceDT.Rows)
                {
                    dtProvince.Rows.Add(r["ProvinceCode"], r["ProvinceName"], r["ProvinceID"]);
                }
            }

            dtProvince.Rows.Add(null, null , null);

            grdProvince.DataSource = dtProvince;

            if (dtProvince.Rows.Count > 0)
                btnSave.Enabled = true;
        }

        private void SetProvinceData(int rowIndex)
        {
            //string _key = grdProvince.CurrentRow.Cells["colProvinceID"].Value.ToString();
            //string _value = grdProvince.CurrentRow.Cells["colProvinceCode"].Value.ToString() + ":" + grdProvince.CurrentRow.Cells["colProvinceName"].Value.ToString();
            string _key = grdProvince.Rows[rowIndex].Cells["colProvinceID"].Value.ToString();
            string _value = grdProvince.Rows[rowIndex].Cells["colProvinceCode"].Value.ToString() + ":" + grdProvince.Rows[rowIndex].Cells["colProvinceName"].Value.ToString();

            treeView1.Nodes.Add(_key, _value);
            if (treeView1.Nodes.Count == 0)
            {
                treeView1.Nodes[0].Checked = tempSalAreaDistrict.Rows.Count > 0 ? true : false;
            }
            else
            {
                var lastnode = treeView1.Nodes.Find(_key, true);
                treeView1.Nodes[lastnode[0].Index].Checked = tempSalAreaDistrict.Rows.Count > 0 ? true : false;
            }

        }

        private void SetDataToTreeView()
        {
            //Area
            int indexProvince = 0;
            string _areaID = "";

            for (int i = 0; i < tempArea.Count; i++)
            {
                string _ProvinceID1 = tempArea[i].ProvinceID.ToString();
                indexProvince = treeView1.Nodes.IndexOfKey(_ProvinceID1);

                _areaID = tempArea[i].AreaID.ToString();
                string _value = tempArea[i].AreaCode + ":" + tempArea[i].AreaName;

                treeView1.Nodes[indexProvince].Nodes.Add(_areaID, _value);

                DataRow r = tempSalAreaDistrict.AsEnumerable().FirstOrDefault(x => x.Field<string>("AreaCode") == tempArea[i].AreaCode);
                treeView1.Nodes[indexProvince].Nodes[i == 0 ? 0 : i].Checked = r != null ? true : false;
            }

            //District
            for (int i = 0; i < tempDistrict.Count; i++)
            {
                string _DistrictID = tempDistrict[i].DistrictID.ToString();
                string _AreaID = tempDistrict[i].AreaID.ToString();
                string _FullName = tempDistrict[i].DistrictCode + ":" + tempDistrict[i].DistrictName;

                int AreaIndex = treeView1.Nodes[indexProvince].Nodes.IndexOfKey(_AreaID);

                DataRow r = tempSalAreaDistrict.AsEnumerable().FirstOrDefault(x => x.Field<string>("DistrictCode") == tempDistrict[i].DistrictCode);

                treeView1.Nodes[indexProvince].Nodes[AreaIndex].Nodes.Add(_DistrictID, _FullName);

                if (treeView1.Nodes[indexProvince].Nodes[AreaIndex].FirstNode == null)
                {
                    treeView1.Nodes[indexProvince].Nodes[AreaIndex].Nodes[0].Checked = r != null ? true : false;
                }
                else
                {
                    treeView1.Nodes[indexProvince].Nodes[AreaIndex].LastNode.Checked = r != null ? true : false;
                }
            }
        }

        private void SetTreeViewData(int rowIndex = 0)
        {
            SetProvinceData(rowIndex);
            SetDataToTreeView();
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

                Cursor.Current = Cursors.WaitCursor;

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
                            dt.Rows.Add(null, null, null);
                            grdProvince.DataSource = dt;

                            if (grdProvince.RowCount > 1)
                            {
                                tempArea = null;
                                tempDistrict = null;
                                tempSalAreaDistrict = null;
                                treeView1.Nodes.Clear();

                                string _WHID = bu.tbl_Branchs[0].BranchID + "V01";
                                for (int i = 0; i < grdProvince.RowCount; i++)
                                {
                                    string _colName = grdProvince.Rows[i].Cells[0].Value.ToString();
                                    if (!string.IsNullOrEmpty(_colName))
                                    {
                                        int _ProvinceID = Convert.ToInt32(grdProvince.Rows[i].Cells["colProvinceID"].Value);
                                        tempSalAreaDistrict = bu.GetSalAreaDistrictByProvince(_ProvinceID, _WHID);

                                        tempArea = bu.GetMstArea(x => x.FlagDel == false && x.ProvinceID == _ProvinceID);
                                        List<int> filterAreaID = tempArea.Select(x => x.AreaID).ToList();
                                        tempDistrict = bu.GetMstDistrict(x => filterAreaID.Contains(Convert.ToInt32(x.AreaID)));

                                        SetTreeViewData(i);
                                    }
                                }
                            }
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
                    if (string.IsNullOrEmpty(grdProvince.CurrentRow.Cells[0].Value.ToString()))
                    {
                        return;
                    }

                    tempArea = null;
                    tempDistrict = null;
                    tempSalAreaDistrict = null;
                    treeView1.Nodes.Clear();

                    branch = bu.tbl_Branchs;

                    if (branch.Count > 0)
                    {
                        string _WHID = branch[0].BranchID + "V01";

                        if (grdProvince.RowCount > 1)
                        {
                            for (int i = 0; i < grdProvince.RowCount; i++)
                            {
                                string _colName = grdProvince.Rows[i].Cells[0].Value.ToString();
                                if (!string.IsNullOrEmpty(_colName))
                                {
                                    int _ProvinceID = Convert.ToInt32(grdProvince.Rows[i].Cells["colProvinceID"].Value);
                                    tempSalAreaDistrict = bu.GetSalAreaDistrictByProvince(_ProvinceID, _WHID);

                                    tempArea = bu.GetMstArea(x => x.FlagDel == false && x.ProvinceID == _ProvinceID);
                                    List<int> filterAreaID = tempArea.Select(x => x.AreaID).ToList();
                                    tempDistrict = bu.GetMstDistrict(x => filterAreaID.Contains(Convert.ToInt32(x.AreaID)));

                                    SetTreeViewData(i);
                                }
                            }

                        }
                        else
                        {
                            string colName = grdProvince.CurrentRow.Cells[_columnIndex].Value.ToString();
                            if (!string.IsNullOrEmpty(colName))
                            {
                                //ถ้าไม่มีข้อมูล ให้เช็ค ว่าจังหวัดนั้น WHID = '' 
                                int _ProvinceID = Convert.ToInt32(grdProvince.CurrentRow.Cells["colProvinceID"].Value);
                                tempSalAreaDistrict = bu.GetSalAreaDistrictByProvince(_ProvinceID, _WHID);

                                tempArea = bu.GetMstArea(x => x.FlagDel == false && x.ProvinceID == _ProvinceID);
                                List<int> filterAreaID = tempArea.Select(x => x.AreaID).ToList();
                                tempDistrict = bu.GetMstDistrict(x => filterAreaID.Contains(Convert.ToInt32(x.AreaID)));

                                SetTreeViewData(grdProvince.CurrentRow.Index);
                            }
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ex.Message.ShowWarningMessage();
            }
        }

        private List<TreeNode> CheckedNodes()
        {
            List<TreeNode> checked_nodes = new List<TreeNode>();
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                for (int x = 0; x < treeView1.Nodes[i].Nodes.Count; x++)
                {
                    if (treeView1.Nodes[i].Nodes[x].Checked == true)
                    {
                        checked_nodes.Add(treeView1.Nodes[i].Nodes[x]);
                    }
                }
            }
            

            return checked_nodes;
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
            Application.AddMessageFilter(new ButtonLogger()); //last edit by sailom.k 17/10/2022

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
            int ret = new int();
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

                if (bu.tbl_Branchs.Count > 0)
                {
                    _WHID = bu.tbl_Branchs[0].BranchID + "V01";
                }

                int ret2 = bu.DeleteByWHID(_WHID); //ลบแค่ V01 ,ไม่ต้องสนว่าจะมีข้อมูล

                var list = new List<tbl_SalAreaDistrict>();

                var cloneDt = (DataTable)grdProvince.DataSource;

                for (int i = 0; i < AreaTreeNode.Count; i++)
                {
                    string[] arrProvince = AreaTreeNode[i].Parent.Text.Split(':');

                    var dr = cloneDt.AsEnumerable().FirstOrDefault(x => x.Field<string>("ProvinceName") == arrProvince[1]);

                    var indexNode = treeView1.Nodes.IndexOfKey(dr[2].ToString());//ProvinceID
                    int indexArea = AreaTreeNode[i].Index;
                    for (int x = 0; x < treeView1.Nodes[indexNode].Nodes[indexArea].Nodes.Count; x++)//District
                    {
                        if (!treeView1.Nodes[indexNode].Nodes[indexArea].Nodes[x].Checked)
                            continue;

                        string[] arrArea = treeView1.Nodes[indexNode].Nodes[indexArea].Nodes[x].Parent.Text.Split(':');
                        string _AreaName = arrArea[1];

                        string[] arrDistrict = treeView1.Nodes[indexNode].Nodes[indexArea].Nodes[x].Text.Split(':');
                        string _DistrictCode = arrDistrict[0];
                        string _DistrictName = arrDistrict[1];

                        var data = new tbl_SalAreaDistrict();
                        data.ProvinceName = arrProvince[1];
                        data.SalAreaID = "";
                        data.WHID = _WHID;

                        data.AreaName = _AreaName;

                        data.DistrictCode = _DistrictCode;
                        data.DistrictID = Convert.ToInt32(treeView1.Nodes[indexNode].Nodes[indexArea].Nodes[x].Name);
                        data.DistrictName = _DistrictName;

                        data.PostalCode = "";
                        data.FlagSend = false;

                        list.Add(data);
                    }
                }

                var dt = PrePareSave_SalAreaDistrict(list, _WHID);

                ret = bu.InsertSalAreaDistrict(dt);

                if (ret > 0)
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

        private void grdProvince_DataSourceChanged(object sender, EventArgs e)
        {
            SelectDetails(null);
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
