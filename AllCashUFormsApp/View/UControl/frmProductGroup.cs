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

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmProductGroup : Form
    {
        ProductGroup bu = new ProductGroup();
        Dictionary<Control, Label> ValidateCtrls = new Dictionary<Control, Label>();
        public frmProductGroup()
        {
            InitializeComponent();
            ValidateCtrls.Add(txtProductGroupCode, label2);
            ValidateCtrls.Add(txtProductGroupName, label3);
        }

        private void BindData()
        {
            if (listBox1.Items.Count > 0)
                listBox1.Items.Clear();

            int flagDel = 0;
            var dt = bu.GetProductGroupTable(flagDel);

            int MaxPrdGroupID = 1;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string prdgroup = dt.Rows[i].Field<string>("ProductGroupCode") + " " + dt.Rows[i].Field<string>("ProductGroupName");
                    listBox1.Items.Add(prdgroup);
                }
            }

            MaxPrdGroupID = dt.AsEnumerable().Max(x => x.Field<int>("ProductGroupID")) + 1;

            txtProductGroupCode.Text = MaxPrdGroupID.ToNumberFormat();
            txtProductGroupName.Text = "";

            var PrdType = bu.GetProductType(x => x.FlagDel == false);
            cbbPrdType.BindDropdownList(PrdType, "ProductTypeName", "ProductTypeID");
        }

        private void frmProductGroup_Load(object sender, EventArgs e)
        {
            txtProductGroupCode.DisableTextBox(true);

            BindData();
        }

        private void frmProductGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductGroup=>btnClose_Click";
            msg.WriteLog(this.GetType());

            this.Close();

            msg = "end frmProductGroup=>btnClose_Click";
            msg.WriteLog(this.GetType());
        }

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            if (ret) //true
            {
                errList.SetErrMessage(ValidateCtrls);
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
            try
            {
                if (!ValidateSave())
                    return;

                string cfMsg = "ต้องการบันทึกข้อมูลใช่หรือไม่?";
                string title = "ยืนยันการบันทึก!!";

                if (!cfMsg.ConfirmMessageBox(title))
                    return;

                var PrdGroupList = bu.GetProductGroupNonFlag(x=>x.ProductGroupName == txtProductGroupName.Text);

                if (PrdGroupList.Count > 0)
                {
                    string msg = "ไม่สามารถเพิ่มรายการได้ \n";
                    msg += "กลุ่ม " + txtProductGroupName.Text + " มีในระบบแล้ว !!";
                    msg.ShowWarningMessage();
                    return;
                }

                var PrdGroup = new tbl_ProductGroup();
                PrdGroup.ProductGroupID = Convert.ToInt32(txtProductGroupCode.Text);
                PrdGroup.ProductGroupCode = txtProductGroupCode.Text;
                PrdGroup.ProductGroupName = txtProductGroupName.Text;

                PrdGroup.CrDate = DateTime.Now;
                PrdGroup.CrUser = Helper.tbl_Users.Username;

                PrdGroup.EdDate = null;
                PrdGroup.EdUser = null;

                PrdGroup.FlagDel = false;
                PrdGroup.FlagSend = false;

                PrdGroup.ProductGroupImg = null;

                var branch = bu.GetBranch();
                if (branch != null)
                    PrdGroup.BranchID = branch[0].BranchID;
                else
                    PrdGroup.BranchID = null;

                if (cbbPrdType.Items.Count > 0)
                    PrdGroup.ProductTypeID = Convert.ToInt32(cbbPrdType.SelectedValue);
                else
                    PrdGroup.ProductTypeID = null;

                int ret = bu.UpdateData(PrdGroup);

                if (ret == 1)
                {
                    string msg = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    msg.ShowInfoMessage();

                    BindData();

                    this.Close();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "start frmProductGroup=>btnSave_Click";
            msg.WriteLog(this.GetType());

            Save();

            msg = "end frmProductGroup=>btnSave_Click";
            msg.WriteLog(this.GetType());
        }

    }
}
