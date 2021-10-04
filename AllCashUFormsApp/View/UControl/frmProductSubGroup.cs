using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using System.IO;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmProductSubGroup : Form
    {
        ProductGroup bu = new ProductGroup();
        static DataTable dt = new DataTable();
        Dictionary<Control, Label> validateCtrls = new Dictionary<Control, Label>(); // Validate Save
        public frmProductSubGroup()
        {
            InitializeComponent();
            validateCtrls.Add(txtProductGroupCode, lblGroup_ProCode);
            validateCtrls.Add(txtProductGroupName, lbl_GroupPrdName);
            validateCtrls.Add(txtProductSubGroupCode, lbl_PrdSubGroupCode);
            validateCtrls.Add(txtProductSubGroupName, lbl_PrdSubGroupName);
        }
        private void BindProductSubGroup()
        {
           
            dt = bu.GetPrdGroupTable(); // NewMethod

            treeView1.Nodes.Clear();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    string prdGrpName = r["ProductGroupCode"].ToString() + " : " + r["ProductGroupName"].ToString();
                    string prdSubGrpName = r["ProductSubGroupCode"].ToString() + " : " + r["ProductSubGroupName"].ToString();

                    if (treeView1.Nodes.Count == 0)
                    {
                        treeView1.Nodes.Add(prdGrpName);
                        treeView1.Nodes[0].Nodes.Add(prdSubGrpName);

                    }

                    else
                    {
                        bool ret = false;

                        for (int i = 0; i < treeView1.Nodes.Count; i++)
                        {
                            if (treeView1.Nodes[i].Text == prdGrpName)
                            {
                                treeView1.Nodes[i].Nodes.Add(prdSubGrpName);
                                ret = true;
                                break;
                            }
                        }
                        if (ret == false)
                        {
                            treeView1.Nodes.Add(prdGrpName);
                            int maxTV = treeView1.Nodes.Count - 1;
                            treeView1.Nodes[maxTV].Nodes.Add(prdSubGrpName);
                        }
                    }

                }
            }
            
        }
        private void ReadOnlyPanel(bool flagEdit = false)
        {
            pnlEdit.Enabled = flagEdit;

            picProSubGroup.Enabled = flagEdit;

            chkFlagProGroup.Enabled = flagEdit;
           
            chkIsFulfill.Enabled = flagEdit;
         
            chkFlagProSubGroup.Enabled = flagEdit;
        }
        private void InitialData()
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            btnBrowse.Enabled = false; //Picture

            BindProductSubGroup();
            ReadOnlyPanel();
        }
        private void frmProductSubGroup_Load(object sender, EventArgs e)
        {
            InitialData();
            BindProductSubGroup();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindProductSubGroup();
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            btnEdit.Enabled = true;
            btnRemove.Enabled = true;

            var prd = dt.AsEnumerable().ToList();
            
            List<string> TempCode = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            string prosubgroupcode = "";

            var chkList = e.Node.Text.ToCharArray().ToList();

            for (int i = 0; i < chkList.Count; i++)
            {
                if (TempCode.Contains(chkList[i].ToString()))
                {
                    prosubgroupcode += chkList[i].ToString();
                    if (prosubgroupcode.Length == 4)
                    {
                        break;
                    }
                }
            }

          

            DataRow item = prd.FirstOrDefault(x => x.Field<string>("ProductSubGroupCode") == prosubgroupcode);
            if (item != null)
            {
                txtProductGroupCode.Text = item["ProductGroupCode"].ToString();
                txtProductGroupName.Text = item["ProductGroupName"].ToString();
                txtProductSubGroupCode.Text = item["ProductSubGroupCode"].ToString();
                txtProductSubGroupName.Text = item["ProductSubGroupName"].ToString();

                bool flag = Convert.ToBoolean(item["FlagDel"]);
                chkFlagProGroup.Checked = flag ? true : false;

                bool flagProSubGroup = Convert.ToBoolean(item["FlagDel2"]);
                chkFlagProSubGroup.Checked = flagProSubGroup ? true : false;

                bool flagIsFulfill = Convert.ToBoolean(item["IsFulfill"]);
                chkIsFulfill.Checked = flagIsFulfill ? true : false;
                

                //if (!string.IsNullOrEmpty(item["ProductSubGroupImg"].ToString()))
                //{
                //    var bArr = ((Byte[])item["ProductSubGroupImg"]);
                //    picProSubGroup.Image = bArr.byteArrayToImage();
                //    picProSubGroup.SizeMode = PictureBoxSizeMode.StretchImage;
                //}
                //else
                //{
                //    picProSubGroup.Image = null;
                //}

            }
            else if (item == null)
            {
                string progroup = prosubgroupcode.Substring(0, 2);

                DataRow item2 = prd.FirstOrDefault(x => x.Field<string>("ProductGroupCode") == progroup);
                if (item2 != null)
                {
                    txtProductGroupCode.Text = item2["ProductGroupCode"].ToString();
                    txtProductGroupName.Text = item2["ProductGroupName"].ToString();
                    txtProductSubGroupCode.Text = item2["ProductSubGroupCode"].ToString();
                    txtProductSubGroupName.Text = item2["ProductSubGroupName"].ToString();

                    bool flag = Convert.ToBoolean(item2["FlagDel"]);
                    chkFlagProGroup.Checked = flag ? true : false;

                    bool flagProSubGroup = Convert.ToBoolean(item2["FlagDel2"]);
                    chkFlagProSubGroup.Checked = flagProSubGroup ? true : false;

                    bool flagIsFulfill = Convert.ToBoolean(item2["IsFulfill"]);
                    chkIsFulfill.Checked = flagIsFulfill ? true : false;
                }
                else
                {
                    panel8.ClearControl();
                    panel11.ClearControl();
                }
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
        private void SetCheckBox(bool flagEdit = false)
        {
            chkFlagProGroup.Checked = flagEdit;
            chkIsFulfill.Checked = flagEdit;
            chkFlagProSubGroup.Checked = flagEdit;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = false;

            pnlEdit.ClearControl();
            ReadOnlyPanel(true);
            txtProductGroupCode.DisableTextBox(false);
            txtProductSubGroupCode.DisableTextBox(false);
            treeView1.Enabled = false;
            SetCheckBox();
        }
        private void Remove()
        {
            if (txtProductGroupCode.Text != "")
            {
                try
                {
                    string cfMsg = "คุณแน่ใจมั้ยที่จะลบข้อมูลรายการนี้?";
                    string title = "ทำการยืนยัน!!";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    int ret = 0;
                    tbl_ProductGroup tbl_ProductGroups = new tbl_ProductGroup();
                    var dtTemp = dt.AsEnumerable().ToList();
                    DataRow item = dtTemp.FirstOrDefault(x => x.Field<string>("ProductGroupCode") == txtProductGroupCode.Text);
                    if (item != null)
                    {
                        tbl_ProductGroups.ProductGroupID = item.Field<int>("ProductGroupID");
                        tbl_ProductGroups.ProductGroupCode = item.Field<string>("ProductGroupCode");
                        tbl_ProductGroups.ProductGroupName = item.Field<string>("ProductGroupName");

                        tbl_ProductGroups.CrDate = item.Field<DateTime>("CrDate");
                        tbl_ProductGroups.CrUser = item.Field<string>("CrUser");

                        tbl_ProductGroups.EdDate = DateTime.Now;
                        tbl_ProductGroups.EdUser = Helper.tbl_Users.Username;

                        tbl_ProductGroups.FlagDel = true;
                        tbl_ProductGroups.FlagSend = item.Field<bool>("FlagSend");//

                        tbl_ProductGroups.BranchID = item.Field<string>("BranchID");//
                        tbl_ProductGroups.ProductTypeID = item.Field<int>("ProductTypeID");//

                        tbl_ProductGroups.ProductGroupImg = item.Field<byte[]>("ProductGroupImg");


                        ret = bu.UpdateData(tbl_ProductGroups);
                        if (ret == 1)
                        {
                            string msg = "ลบข้อมูลเรียบร้อยแล้ว";
                            msg.ShowInfoMessage();
                            btnSearch.PerformClick();
                        }
                        else
                        {
                            this.ShowProcessErr();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                    msg.ShowErrorMessage();
                }
            }
            else
            {
                string msg = "ไม่พบข้อมูลกลุ่มสินค้า !!";
                msg.ShowWarningMessage();
                return;
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            Remove();
        }
        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //true
            {
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
        private void Save()
        {
            string msgError = "";
            if (txtProductGroupCode.TextLength < 2 || txtProductGroupCode.TextLength > 2)
            {
                msgError += "--> กรอก รหัสกลุ่มสินค้า 2 ตัวเลขเท่านั้น \n";
            }
            if (txtProductSubGroupCode.TextLength < 4 || txtProductSubGroupCode.TextLength > 4)
            {
                msgError += "--> กรอก รหัสกลุ่มย่อยสินค้า 4 ตัวเลขเท่านั้น \n";
            }
            if (msgError != "")
            {
                FlexibleMessageBox.Show(msgError, "คำเตือน",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
            if (!ValidateSave())
            {
                return;
            }
           
            else
            {
                try
                {
                    string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                    string title = "ยืนยันการบันทึก!!";

                    if (!cfMsg.ConfirmMessageBox(title))
                        return;

                    int ret = 0;
                    var protype = bu.GetProductType().First();
                    var branch = bu.GetBranch().First();

                    var allprdgroup = bu.GetProductGroupNonFlag(x=>x.ProductGroupCode == txtProductGroupCode.Text); //new dal selectnonflag ใน basecontrol
                    var allprdsubgroup = bu.GetProductSubGroup(x=>x.ProductSubGroupCode == txtProductSubGroupCode.Text);//new dal selectnonflag ใน basecontrol

                    var MaxProductGroupID = bu.GetProductGroupNonFlag().OrderByDescending(x => x.ProductGroupID).First();

                    int _maxprdGroupID = MaxProductGroupID.ProductGroupID + 1;

                    tbl_ProductGroup tbl_ProductGroups = new tbl_ProductGroup();  // save

                    if (allprdgroup.Count > 0)
                    {
                        tbl_ProductGroups = allprdgroup[0];

                        tbl_ProductGroups.EdDate = DateTime.Now;
                        tbl_ProductGroups.EdUser = Helper.tbl_Users.Username;
                    }
                    else if (allprdgroup.Count == 0)
                    {
                        tbl_ProductGroups.ProductGroupID = _maxprdGroupID;

                        tbl_ProductGroups.CrDate = DateTime.Now;
                        tbl_ProductGroups.CrUser = Helper.tbl_Users.Username;
                        tbl_ProductGroups.FlagSend = false;
                        tbl_ProductGroups.ProductGroupImg = null;

                        tbl_ProductGroups.BranchID = branch.BranchID;
                        tbl_ProductGroups.ProductTypeID = protype.ProductTypeID;
                    }

                    tbl_ProductGroups.ProductGroupCode = txtProductGroupCode.Text;
                    tbl_ProductGroups.ProductGroupName = txtProductGroupName.Text;
                   
                    tbl_ProductGroups.FlagDel = chkFlagProGroup.Checked ? true : false;
                    
                    ret = bu.UpdateData(tbl_ProductGroups);  // save

                    if (ret == 1)
                    {
                        var maxProductSubGroupID = bu.GetProductSubGroup().OrderByDescending(x => x.ProductSubGroupID).First();

                        int prdsubgroupID = maxProductSubGroupID.ProductSubGroupID + 1;

                        tbl_ProductSubGroup tbl_ProductSubGroup = new tbl_ProductSubGroup(); // save

                        if (allprdsubgroup.Count > 0)
                        {
                            tbl_ProductSubGroup = allprdsubgroup[0];
                            tbl_ProductSubGroup.EdDate = DateTime.Now;
                            tbl_ProductSubGroup.EdUser = Helper.tbl_Users.Username;
                        }
                        else if (allprdsubgroup.Count == 0)
                        {

                            tbl_ProductSubGroup.ProductSubGroupID = prdsubgroupID;

                            if (allprdgroup.Count == 0)
                            {
                                tbl_ProductSubGroup.ProductGroupID = _maxprdGroupID;
                            }
                            else if (allprdgroup.Count > 0)
                            {
                                tbl_ProductSubGroup.ProductGroupID = allprdgroup[0].ProductGroupID;
                            }

                            tbl_ProductSubGroup.ProductSubGroupImg = null;

                            tbl_ProductSubGroup.CrDate = DateTime.Now;
                            tbl_ProductSubGroup.CrUser = Helper.tbl_Users.Username;
                            tbl_ProductSubGroup.FlagSend = false;
                        }
                       
                        tbl_ProductSubGroup.ProductSubGroupCode = txtProductSubGroupCode.Text;
                        tbl_ProductSubGroup.ProductSubGroupName = txtProductSubGroupName.Text;
                        tbl_ProductSubGroup.IsFulfill = chkIsFulfill.Checked ? true : false;
                     
                        tbl_ProductSubGroup.FlagDel = chkFlagProSubGroup.Checked ? true : false;
                      
                        ret = bu.UpdateData(tbl_ProductSubGroup); //save

                        if (ret == 1)
                        {
                            string msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                            msg.ShowInfoMessage();
                       
                            treeView1.Enabled = true;

                            pnlEdit.ClearControl();

                            SetCheckBox();

                            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, "");
                            btnAdd.Enabled = true;
                            btnSave.Enabled = false;
                            btnCancel.Enabled = false;

                            pnlEdit.Enabled = false;

                            btnSearch.PerformClick();
                        }
                        else
                        {
                            this.ShowProcessErr();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    msg.ShowErrorMessage();
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtProductGroupCode.Text != "" && txtProductSubGroupCode.Text != "")
            {
                ReadOnlyPanel(true);
                btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
                btnSave.Enabled = true;
                btnEdit.Enabled = false;
                txtProductGroupCode.DisableTextBox(true);
                txtProductSubGroupCode.DisableTextBox(true);
            }
        }

        private void keyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
            else
            {
                return;
            }
        }
        private void txtProductGroupCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPress(e);
        }

        private void txtProductSubGroupCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPress(e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            pnlEdit.ClearControl();

            treeView1.Enabled = true;
            SetCheckBox();
            pnlEdit.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

