using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmProductSubGroup : Form
    {
        ProductGroup bu = new ProductGroup();
        static DataTable dtPrdGroup = new DataTable();
        Dictionary<Control, Label> validateCtrls = new Dictionary<Control, Label>(); // Validate Save
        List<string> pnlPrdGroup_Controls = new List<string>();
        List<string> pnlPrdSubGroup_Controls = new List<string>();
        public frmProductSubGroup()
        {
            InitializeComponent();


            pnlPrdGroup_Controls = new string[] { txtProductGroupCode.Name }.ToList();
            pnlPrdSubGroup_Controls = new string[] { txtProductSubGroupCode.Name }.ToList();
        }

        #region #Event_Method
        private void frmProductSubGroup_Load(object sender, EventArgs e)
        {
            BindProductSubGroup();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindProductSubGroup();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                txtProductGroupCode.Text = "";
                txtProductGroupName.Text = "";
                txtProductSubGroupCode.Text = "";
                txtProductSubGroupName.Text = "";

                List<string> TempCode = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                string _NodeName = "";

                var nodeList = e.Node.Text.ToCharArray().ToList();
                for (int i = 0; i < nodeList.Count; i++)
                {
                    string _name = nodeList[i].ToString();

                    if (_name == ":")
                        break;
                    else if (TempCode.Contains(_name))
                        _NodeName += _name;
                }

                string FieldName = "";

                if (_NodeName.Length == 2)
                    FieldName = "ProductGroupCode";
                else if (_NodeName.Length == 4)
                    FieldName = "ProductSubGroupCode";

                DataRow dr = dtPrdGroup.AsEnumerable().FirstOrDefault(x => x.Field<string>(FieldName) == _NodeName);
                if (dr != null)
                {
                    txtProductGroupCode.Text = dr["ProductGroupCode"].ToString();
                    txtProductGroupName.Text = dr["ProductGroupName"].ToString();
                    txtProductSubGroupCode.Text = dr["ProductSubGroupCode"].ToString();
                    txtProductSubGroupName.Text = dr["ProductSubGroupName"].ToString();

                    rdoN.Checked = true;
                    if (Convert.ToBoolean(dr["FlagDel"]) == true)
                        rdoC.Checked = true;

                    rdoN_2.Checked = true;
                    if (!string.IsNullOrEmpty(dr["FlagDel2"].ToString()))
                    {
                        if (Convert.ToBoolean(dr["FlagDel2"]) == true)
                            rdoC_2.Checked = true;
                    }

                    chkIsFulfill.Checked = false;
                    if (!string.IsNullOrEmpty(dr["IsFulfill"].ToString()))
                        chkIsFulfill.Checked = Convert.ToBoolean(dr["IsFulfill"]) ? true : false;
                }

                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnAdd.Enabled = true;

                if (!string.IsNullOrEmpty(txtProductSubGroupCode.Text))
                    btnEdit.Enabled = true;
                else
                    btnEdit.Enabled = false;

                btnSave.Enabled = false;
                btnCancel.Enabled = false;

            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void btnDefaultSize_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");

            pnlProductGroup.OpenControl(false, pnlPrdGroup_Controls.ToArray());
            pnlProductSubGroup.OpenControl(true, pnlPrdSubGroup_Controls.ToArray());

            txtProductSubGroupCode.DisableTextBox(false);
            txtProductSubGroupName.DisableTextBox(false);

            txtProductSubGroupCode.Text = "";
            txtProductSubGroupName.Text = "";

            txtProductSubGroupCode.Focus();
            chkIsFulfill.Checked = false;

            pnlSearch.Enabled = false;
            treeView1.Enabled = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //Remove();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save_ProductSubGroup();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = true;
            btnEdit.Enabled = false;

            pnlProductGroup.OpenControl(false, pnlPrdGroup_Controls.ToArray());
            pnlProductSubGroup.OpenControl(true, pnlPrdSubGroup_Controls.ToArray());
            txtProductSubGroupCode.DisableTextBox(true);
            txtProductSubGroupName.DisableTextBox(false);

            txtProductSubGroupName.Focus();

            pnlSearch.Enabled = false;
            treeView1.Enabled = false;
        }

        private void txtProductGroupCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetKeyPress(e);
        }

        private void txtProductSubGroupCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetKeyPress(e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            treeView1.Enabled = true;
            pnlSearch.Enabled = true;

            txtProductGroupCode.Text = "";
            txtProductGroupName.Text = "";
            txtProductSubGroupCode.Text = "";
            txtProductSubGroupName.Text = "";

            pnlProductGroup.OpenControl(false, pnlPrdGroup_Controls.ToArray());
            pnlProductSubGroup.OpenControl(false, pnlPrdSubGroup_Controls.ToArray());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region #Private_Method
        private void BindProductSubGroup()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            pnlProductGroup.OpenControl(false, pnlPrdGroup_Controls.ToArray());
            pnlProductSubGroup.OpenControl(false, pnlPrdSubGroup_Controls.ToArray());

            dtPrdGroup = bu.GetPrdGroupTable();
            treeView1.Nodes.Clear();

            if (dtPrdGroup.Rows.Count > 0)
            {
                bool ret = false;

                foreach (DataRow r in dtPrdGroup.Rows)
                {
                    string prdGrpName = r["ProductGroupCode"].ToString() + " : " + r["ProductGroupName"].ToString();
                    string prdSubGrpName = r["ProductSubGroupCode"].ToString() + " : " + r["ProductSubGroupName"].ToString();

                    if (!string.IsNullOrEmpty(prdGrpName)) //เช็คใน ProductGroup มี ProductSubGroup หรือไม่ ??
                    {
                        if (treeView1.Nodes.Count == 0)
                        {
                            treeView1.Nodes.Add(prdGrpName);
                            if (prdSubGrpName != " : ")
                                treeView1.Nodes[0].Nodes.Add(prdSubGrpName);
                        }

                        else
                        {
                            ret = false;

                            for (int i = 0; i < treeView1.Nodes.Count; i++)
                            {
                                if (treeView1.Nodes[i].Text == prdGrpName)//ถ้าสินค้าในกลุ่มเดียวกัน 
                                {
                                    if (prdSubGrpName != " : ")
                                    {
                                        treeView1.Nodes[i].Nodes.Add(prdSubGrpName);
                                        ret = true;
                                        break;
                                    }
                                }
                            }

                            if (ret == false)
                            {
                                treeView1.Nodes.Add(prdGrpName);
                                int maxTV = treeView1.Nodes.Count - 1;
                                if (prdSubGrpName != " : ")
                                    treeView1.Nodes[maxTV].Nodes.Add(prdSubGrpName);
                            }
                        }
                    }
                }
            }
        }

        //private void Remove()
        //{
        //    if (!string.IsNullOrEmpty(txtProductGroupCode.Text))
        //    {
        //        try
        //        {
        //            string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
        //            string title = "ทำการยืนยัน!!";

        //            if (!cfMsg.ConfirmMessageBox(title))
        //                return;

        //            int ret = 0;

        //            var _PrdGroups = new tbl_ProductGroup();

        //            DataRow r = dtPrdGroup.AsEnumerable().FirstOrDefault(x => x.Field<string>("ProductGroupCode") == txtProductGroupCode.Text);

        //            if (r != null)
        //            {
        //                _PrdGroups.ProductGroupID = r.Field<int>("ProductGroupID");
        //                _PrdGroups.ProductGroupCode = r.Field<string>("ProductGroupCode");
        //                _PrdGroups.ProductGroupName = r.Field<string>("ProductGroupName");

        //                _PrdGroups.CrDate = r.Field<DateTime>("CrDate");
        //                _PrdGroups.CrUser = r.Field<string>("CrUser");

        //                _PrdGroups.EdDate = DateTime.Now;
        //                _PrdGroups.EdUser = Helper.tbl_Users.Username;

        //                _PrdGroups.FlagDel = true;
        //                _PrdGroups.FlagSend = r.Field<bool>("FlagSend");

        //                _PrdGroups.BranchID = r.Field<string>("BranchID");
        //                _PrdGroups.ProductTypeID = r.Field<int>("ProductTypeID");
        //                _PrdGroups.ProductGroupImg = r.Field<byte[]>("ProductGroupImg");

        //                ret = bu.UpdateData(_PrdGroups);

        //                if (ret == 1)
        //                {
        //                    string msg = "ลบข้อมูลเรียบร้อยแล้ว";
        //                    msg.ShowInfoMessage();
        //                    btnSearch.PerformClick();
        //                }
        //                else
        //                {
        //                    this.ShowProcessErr();
        //                    return;
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            string msg = ex.Message.ToString();
        //            msg.ShowErrorMessage();
        //        }
        //    }
        //    else
        //    {
        //        string msg = "ไม่พบข้อมูลกลุ่มสินค้า !!";
        //        msg.ShowWarningMessage();
        //        return;
        //    }
        //}

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //true
            {
                if (txtProductGroupCode.ReadOnly == false)
                {
                    validateCtrls.Add(txtProductGroupCode, lblGroup_ProCode);
                    validateCtrls.Add(txtProductGroupName, lbl_GroupPrdName);
                }
                if (txtProductSubGroupCode.ReadOnly == false)
                {
                    validateCtrls.Add(txtProductSubGroupCode, lbl_PrdSubGroupCode);
                    validateCtrls.Add(txtProductSubGroupName, lbl_PrdSubGroupName);
                }

                errList.SetErrMessage(validateCtrls);
            }
            if (errList.Count > 0)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n\n" + string.Join("\n", errList);
                message.ShowWarningMessage();
                ret = false;
            }
            return ret;
        }

        private void Save_ProductSubGroup()
        {
            if (!ValidateSave())
                return;

            string msg = "";

            if (txtProductSubGroupCode.TextLength < 4 || txtProductSubGroupCode.TextLength > 4)
            {
                txtProductSubGroupCode.ErrorTextBox();
                msg += "รหัสกลุ่มย่อยสินค้า 4ตัวเลขเท่านั้น\n";
            }


            if (!string.IsNullOrEmpty(msg))
            {
                msg.ShowWarningMessage();
                return;
            }

            try
            {
                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                int ret = 0;

                var PrdSubGroup = bu.GetProductSubGroup(x => x.ProductSubGroupCode == txtProductSubGroupCode.Text);
                var MaxID = bu.GetProductSubGroup().OrderByDescending(x => x.ProductSubGroupID);

                int _PrdSubGroupID = 1;

                if (MaxID != null)
                    _PrdSubGroupID = MaxID.First().ProductSubGroupID + 1;

                var _PrdSubGroup = new tbl_ProductSubGroup();

                if (PrdSubGroup.Count > 0)
                {
                    _PrdSubGroup = PrdSubGroup[0];
                    _PrdSubGroup.EdDate = DateTime.Now;
                    _PrdSubGroup.EdUser = Helper.tbl_Users.Username;
                }
                else if (PrdSubGroup.Count == 0) //Insert
                {
                    _PrdSubGroup.ProductSubGroupID = _PrdSubGroupID;

                    var prdGroup = bu.GetProductGroupNonFlag(x => x.ProductGroupCode == txtProductGroupCode.Text);
                    _PrdSubGroup.ProductGroupID = prdGroup[0].ProductGroupID;

                    _PrdSubGroup.ProductSubGroupImg = null;
                    _PrdSubGroup.CrDate = DateTime.Now;
                    _PrdSubGroup.CrUser = Helper.tbl_Users.Username;
                    _PrdSubGroup.FlagSend = false;
                }

                _PrdSubGroup.ProductSubGroupCode = txtProductSubGroupCode.Text;
                _PrdSubGroup.ProductSubGroupName = txtProductSubGroupName.Text;
                _PrdSubGroup.IsFulfill = chkIsFulfill.Checked ? true : false;
                _PrdSubGroup.FlagDel = rdoN_2.Checked ? false : true;

                ret = bu.UpdateData(_PrdSubGroup);

                if (ret == 1)
                {
                    msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                    msg.ShowInfoMessage();

                    chkIsFulfill.Checked = false;

                    txtProductGroupCode.Text = "";
                    txtProductGroupName.Text = "";

                    txtProductSubGroupCode.Text = "";
                    txtProductSubGroupName.Text = "";

                    pnlSearch.Enabled = true;
                    treeView1.Enabled = true;

                    BindProductSubGroup();
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

        //private void Save()
        //{
        //    if (!ValidateSave())
        //        return;

        //    string msg = "";
        //    var PrdGroup = new List<tbl_ProductGroup>();
        //    PrdGroup = bu.GetProductGroupNonFlag(x => x.ProductGroupCode == txtProductGroupCode.Text);
           

        //    if (txtProductSubGroupCode.ReadOnly == false)
        //    {
        //        if (txtProductSubGroupCode.TextLength < 4 || txtProductSubGroupCode.TextLength > 4)
        //        {
        //            txtProductSubGroupCode.ErrorTextBox();
        //            msg += "รหัสกลุ่มย่อยสินค้า 4ตัวเลขเท่านั้น\n";
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(msg))
        //    {
        //        msg.ShowWarningMessage();
        //        return;
        //    }

        //    try
        //    {
        //        string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
        //        string title = "ยืนยันการบันทึก!!";

        //        if (!cfMsg.ConfirmMessageBox(title))
        //            return;

        //        List<int> ret = new List<int>();
                
        //        var _PrdGroup = new tbl_ProductGroup();

        //        if (PrdGroup.Count > 0)
        //        {
        //            _PrdGroup = PrdGroup[0];

        //            _PrdGroup.EdDate = DateTime.Now;
        //            _PrdGroup.EdUser = Helper.tbl_Users.Username;
        //        }
        //        else if (PrdGroup.Count == 0)
        //        {
        //            var MaxProductGroupID = bu.GetProductGroupNonFlag().OrderByDescending(x => x.ProductGroupID).First();
        //            _PrdGroup.ProductGroupID = MaxProductGroupID.ProductGroupID + 1;

        //            _PrdGroup.CrDate = DateTime.Now;
        //            _PrdGroup.CrUser = Helper.tbl_Users.Username;
        //            _PrdGroup.FlagSend = false;
        //            _PrdGroup.ProductGroupImg = null;

        //            var _ProductType = bu.GetProductType();
        //            _PrdGroup.ProductTypeID = _ProductType[0].ProductTypeID;
        //        }

        //        var branch = bu.GetBranch();
        //        _PrdGroup.BranchID = branch[0].BranchID;

        //        _PrdGroup.ProductGroupCode = txtProductGroupCode.Text;
        //        _PrdGroup.ProductGroupName = txtProductGroupName.Text;

        //        _PrdGroup.FlagDel = rdoN.Checked ? false : true;

        //        ret.Add(bu.UpdateData(_PrdGroup));

        //        if (txtProductSubGroupName.ReadOnly == false)
        //            ret.Add(Save_ProductSubGroup(_PrdGroup));

        //        int ret1 = ret.All(x => x == 1) ? 1 : 0;

        //        if (ret1 == 1)
        //        {
        //            msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
        //            msg.ShowInfoMessage();

        //            treeView1.Enabled = true;
        //            BindProductSubGroup();
        //        }
        //        else
        //        {
        //            this.ShowProcessErr();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ShowErrorMessage();
        //    }
        //}

        //private int Save_ProductSubGroup(tbl_ProductGroup _PrdGroup)
        //{
        //    var PrdSubGroup = bu.GetProductSubGroup(x => x.ProductSubGroupCode == txtProductSubGroupCode.Text);

        //    var MaxID = bu.GetProductSubGroup().OrderByDescending(x => x.ProductSubGroupID).First();

        //    int _PrdSubGroupID = MaxID.ProductSubGroupID + 1;

        //    var _PrdSubGroup = new tbl_ProductSubGroup();

        //    if (PrdSubGroup.Count > 0)
        //    {
        //        _PrdSubGroup = PrdSubGroup[0];
        //        _PrdSubGroup.EdDate = DateTime.Now;
        //        _PrdSubGroup.EdUser = Helper.tbl_Users.Username;
        //    }
        //    else if (PrdSubGroup.Count == 0) //Insert
        //    {
        //        _PrdSubGroup.ProductSubGroupID = _PrdSubGroupID;
        //        _PrdSubGroup.ProductGroupID = _PrdGroup.ProductGroupID;
        //        _PrdSubGroup.ProductSubGroupImg = null;
        //        _PrdSubGroup.CrDate = DateTime.Now;
        //        _PrdSubGroup.CrUser = Helper.tbl_Users.Username;
        //        _PrdSubGroup.FlagSend = false;
        //    }

        //    _PrdSubGroup.ProductSubGroupCode = txtProductSubGroupCode.Text;
        //    _PrdSubGroup.ProductSubGroupName = txtProductSubGroupName.Text;
        //    _PrdSubGroup.IsFulfill = chkIsFulfill.Checked ? true : false;

        //    _PrdSubGroup.FlagDel = rdoN_2.Checked ? false : true;

        //    int ret = 0;
        //    ret = bu.UpdateData(_PrdSubGroup);

        //    return ret; 
        //}

        private void SetKeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
                e.Handled = true;
            else
                return;
        }

        #endregion
    }
}

