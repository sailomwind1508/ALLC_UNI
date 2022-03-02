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

namespace AllCashUFormsApp.View.Page
{
    public partial class frmSendUserData : Form
    {
        MasterDataControl bu = new MasterDataControl();
        MenuBU menuBU = new MenuBU();
        public frmSendUserData()
        {
            InitializeComponent();
        }

        #region #Private_Method
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
            grdBranch.AutoGenerateColumns = false;
            grdBranch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            btnSendData.Enabled = false;
            btnCancelSend.Enabled = false;

            string msg = "สามารถใช้ได้เมื่อต่อ CENTER DB เท่านั้น !!";
            msg.ShowWarningMessage();
        }

        private void SetBranchTable(DataTable newTable)
        {
            newTable.Columns.Add("ChkBranch", typeof(bool));
            newTable.Columns.Add("BranchID", typeof(string));
            newTable.Columns.Add("BranchRefCode", typeof(string));
            newTable.Columns.Add("BranchName", typeof(string));
            newTable.Columns.Add("Pic", typeof(Bitmap));
            newTable.Columns.Add("OnlineStatus", typeof(bool));
            newTable.Columns.Add("db_server", typeof(string));
            newTable.Columns.Add("db_name", typeof(string));
        }

        private void BindBranchData()
        {
            DataTable dtBranch = new DataTable();

            bu.GetSendProductInfoPrepareData();

            dtBranch = bu.Get_proc_SendProductInfo_GetDataTable();

            DataTable newTable = new DataTable();
            SetBranchTable(newTable);

            Bitmap wifi_Img = new Bitmap(Properties.Resources.wifi); // new Resource Image
            Bitmap power_off_lmg = new Bitmap(Properties.Resources.closeBtn);

            foreach (DataRow r in dtBranch.Rows)
            {
                Bitmap colPic = Convert.ToBoolean(r["OnlineStatus"]) == true ? wifi_Img : power_off_lmg;
                newTable.Rows.Add(false
                    , r["BranchID"].ToString()
                    , r["BranchRefCode"].ToString()
                    , r["BranchName"].ToString()
                    , colPic
                    , r["OnlineStatus"]
                    , r["db_server"].ToString()
                    , r["db_name"].ToString()
                    );
            }

            grdBranch.DataSource = newTable;

            for (int i = 0; i < grdBranch.RowCount; i++)
            {
                bool _OnlineStatus = Convert.ToBoolean(grdBranch.Rows[i].Cells["colOnlineStatus"].Value);
                var currentCell = grdBranch.Rows[i].Cells["colChkBranch"];
                if (currentCell is DataGridViewCheckBoxCell)
                {
                    DataGridViewCheckBoxCell currentCellChk = currentCell as DataGridViewCheckBoxCell;
                    if (_OnlineStatus == true)
                        currentCellChk.ReadOnly = false;
                    else
                    {
                        currentCellChk.Value = false;
                        currentCellChk.ReadOnly = true;
                    }
                }
            }

            lblgrdQty.Text = newTable.Rows.Count.ToNumberFormat();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            if (newTable.Rows.Count > 0)
            {
                btnSendData.Enabled = true;
                btnCancelSend.Enabled = true;
            }
            else
            {
                btnSendData.Enabled = false;
                btnCancelSend.Enabled = false;
            }
        }

        private void BindEmployeeData(string db_name, string db_server, Label lblQty, DataGridView grdList, bool NoCheckBoxColumns = false)
        {
            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@flagDel", 0);
            _params.Add("@db_name", db_name);
            _params.Add("@db_server", db_server);
            _params.Add("@Search", txtSearch.Text);

            DataTable newEmp = new DataTable();

            var dt = bu.proc_Employee_Data(_params);

            if (NoCheckBoxColumns == false)
            {
                SetEmployeeTable(newEmp);

                foreach (DataRow r in dt.Rows)
                {
                    //string _FullName = r["TitleName"].ToString() + " " + r["FirstName"].ToString() + " " + r["LastName"].ToString();
                    newEmp.Rows.Add(false, r["EmpID"]
                        , r["FirstName"]
                        , r["LastName"]
                        , r["DepartmentName"]
                        , r["PositionName"]
                    );
                }
            }
            else
            {
                SetEmployeeTable(newEmp, false);

                foreach (DataRow r in dt.Rows)
                {
                    newEmp.Rows.Add(r["EmpID"], r["FirstName"], r["LastName"], r["DepartmentName"], r["PositionName"]);
                }
            }

            grdList.DataSource = newEmp;

            if (NoCheckBoxColumns == false)
            {
                //grdList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grdList.Columns[0].Width = 30;
                grdList.Columns[0].HeaderText = "";

                for (int i = 1; i < grdList.ColumnCount; i++)//ปิด Columns ยกเว้น ปุ่ม CheckBox
                {
                    grdList.Columns[i].ReadOnly = true;
                }
            }
            else
            {
                grdList.ReadOnly = true;
            }

            lblQty.Text = dt.Rows.Count.ToNumberFormat();
        }

        private void SetEmployeeTable(DataTable dt, bool flagCheckbox = true)
        {
            if (flagCheckbox == true)
            {
                dt.Columns.Add("", typeof(bool));
            }

            dt.Columns.Add("รหัสพนักงาน", typeof(string));
            dt.Columns.Add("ชื่อ", typeof(string));
            dt.Columns.Add("นามสกุล", typeof(string));
            dt.Columns.Add("แผนก", typeof(string));
            dt.Columns.Add("ตำแหน่ง", typeof(string));
        }

        public bool EmployeeCheck()
        {
            bool check = false;

            if (grdviewFrom.Rows.Count > 0)
            {
                var dt = (DataTable)grdviewFrom.DataSource;

                DataRow dr = dt.AsEnumerable().FirstOrDefault(x => x.Field<bool>("Column1") == true);

                if (dr != null)
                    check = true;
            }

            return check;
        }

        private string SelectEmployee()
        {
            List<string> EmpList = new List<string>();

            string db_server = grdBranch.CurrentRow.Cells["col_db_server"].Value.ToString();
            string db_name = grdBranch.CurrentRow.Cells["col_db_name"].Value.ToString();

            Dictionary<string, object> _parameters = new Dictionary<string, object>();
            _parameters.Add("@flagDel", 0);
            _parameters.Add("@db_name", db_name);
            _parameters.Add("@db_server", db_server);

            var dtTo = new DataTable();
            SetEmployeeTable(dtTo, false);

            foreach (DataGridViewRow r in grdviewFrom.Rows)
            {
                string firstName = r.Cells[2].Value.ToString();
                string LastName = r.Cells[3].Value.ToString();

                DataRow dr = dtTo.AsEnumerable().FirstOrDefault(x => x.Field<string>("ชื่อ") == firstName && x.Field<string>("นามสกุล") == LastName);

                if (Convert.ToBoolean(r.Cells[0].Value) == true && dr == null)
                {
                    EmpList.Add(r.Cells[1].Value.ToString());//EmpID
                }
            }

            string emp = string.Join(",", EmpList);
            return emp;
        }

        private void SelectBranchDetails(DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow grdRows = null;

                if (e != null)
                {
                    if (e.RowIndex == -1)
                        return;
                    else
                        grdRows = grdBranch.Rows[e.RowIndex];
                }
                else
                    grdRows = grdBranch.CurrentRow;

                if (grdRows != null)
                {
                    if (tabPage.SelectedIndex == 0) //ส่งข้อมูล
                    {
                        chkSelectAllUser.Checked = false;

                        bool _colOnlineStatus = Convert.ToBoolean(grdBranch.Rows[e.RowIndex].Cells["colOnlineStatus"].Value);
                        if (_colOnlineStatus == false)
                        {
                            DataTable dt = new DataTable();
                            SetEmployeeTable(dt);
                            grdviewFrom.DataSource = dt;

                            dt = new DataTable();
                            SetEmployeeTable(dt, false);
                            grdviewTo.DataSource = dt;
                            return;
                        }
                        else
                        {
                            txtSearch.Text = "";

                            string db_server = "";
                            string db_name = "";

                            BindEmployeeData(db_name, db_server, label1, grdviewFrom); //ศูนย์ Center

                            db_server = grdBranch.Rows[e.RowIndex].Cells["col_db_server"].Value.ToString();
                            db_name = grdBranch.Rows[e.RowIndex].Cells["col_db_name"].Value.ToString();
                            BindEmployeeData(db_name, db_server, label10, grdviewTo, true); //ศูนย์ ปลายทาง
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowWarningMessage();
            }
        }

        private void SetCheckBox(DataGridView grd, CheckBox checkbox)
        {
            if (grd.Rows.Count > 0)
            {
                for (int i = 0; i < grd.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(grd.Rows[i].Cells[0].Value) == false)
                        grd.Rows[i].Cells[0].Value = true;
                    else
                        grd.Rows[i].Cells[0].Value = false;
                }
            }
            else
                checkbox.Checked = false;
        }

        #endregion

        #region #Event_Method

        private void frmSendUserData_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();

            BindBranchData();

            if (grdBranch.RowCount > 0)
            {
                string db_server = "";
                string db_name = "";

                if (tabPage.SelectedIndex == 0) //ส่งข้อมูล
                {
                    BindEmployeeData("", "", label1, grdviewFrom); //ศูนย์ Center

                    db_server = grdBranch.Rows[0].Cells["col_db_server"].Value.ToString();
                    db_name = grdBranch.Rows[0].Cells["col_db_name"].Value.ToString();
                    BindEmployeeData(db_name, db_server, label10, grdviewTo, true); //ศูนย์ ปลายทาง
                }
            }
        }

        private void btnSearchBranch_Click(object sender, EventArgs e)
        {
            BindBranchData();
        }

        private void chkBoxSelectAllBranch_Click(object sender, EventArgs e)
        {
            if (grdBranch.Rows.Count > 0)
            {
                for (int i = 0; i < grdBranch.Rows.Count; i++)
                {
                    bool OnlineStatus = Convert.ToBoolean(grdBranch.Rows[i].Cells["colOnlineStatus"].Value);

                    if (Convert.ToBoolean(grdBranch.Rows[i].Cells["colChkBranch"].Value) == false && OnlineStatus == true)
                        grdBranch.Rows[i].Cells["colChkBranch"].Value = true;
                    else
                        grdBranch.Rows[i].Cells["colChkBranch"].Value = false;
                }
            }
            else
                chkBoxSelectAllBranch.Checked = false;
        }

        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            BindBranchData();
            //BindEmployeeData();
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            int ret = 0;

            string msg = "";

            bool _OnlineStatus = Convert.ToBoolean(grdBranch.CurrentRow.Cells["colOnlineStatus"].Value);

            if ( _OnlineStatus == false) // ต้องเลือก Branch แล้วก็ Branch ต้อง Online เท่านั้น
                msg = "เลือกศูนย์ที่ Online เท่านั้น !!\n";

            if (EmployeeCheck() == false)
                msg += "เลือกพนักงานที่ต้องการส่งข้อมูล !!\n";

            if (!string.IsNullOrEmpty(msg))
            {
                msg.ShowWarningMessage();
                return;
            }

            string db_server = grdBranch.CurrentRow.Cells["col_db_server"].Value.ToString();
            string branchID = grdBranch.CurrentRow.Cells["colBranchID"].Value.ToString();
            string db_name = grdBranch.CurrentRow.Cells["col_db_name"].Value.ToString();

            string _allBranchID = "";
            //_allBranchID = SelectBranch();
            _allBranchID = branchID;

            string _allEmpID = "";
            _allEmpID = SelectEmployee();

            if (_allEmpID.Length < 7)
            {
                msg = "ข้อมูลพนักงานที่เลือก มีซ้ำในระบบแล้ว !!";
                msg.ShowWarningMessage();
                return;
            }

            var empList = bu.SelectEmpList(_allEmpID);//ข้อมูล Employee จาก Center
            var userList = bu.SelectUserList(_allEmpID);//ข้อมูล User จาก Center

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@flagDel", 0);
            _params.Add("@db_name", db_name);
            _params.Add("@db_server", db_server);

            DataTable dtEmp = bu.proc_Employee_Data(_params);

            var eData = new List<tbl_Employee>();
            var uData = new List<tbl_Users>();
            
            for (int i = 0; i < empList.Count; i++)
            {
                string maxID = "";
                string formatEmpID = "";
                if (dtEmp.Rows.Count == 0)
                {
                    formatEmpID = branchID + "E" + "001";
                }
                else
                {
                    if (eData.Count == 0)
                        maxID = dtEmp.AsEnumerable().Max(x => x.Field<string>("EmpID"));
                    else
                        maxID = eData.Max(x => x.EmpID);

                    int maxEmpID = Convert.ToInt32(maxID.Substring(4, 3)) + 1;
                    formatEmpID = branchID + "E" + maxEmpID.ToString();
                }
                
                empList[i].EmpID = formatEmpID;
                empList[i].EmpCode = formatEmpID;
                eData.Add(empList[i]);
            }

            for (int i = 0; i < userList.Count; i++)
            {
                var filter = eData.FirstOrDefault(x => x.FirstName == userList[i].FirstName && x.LastName == userList[i].LastName);
                if (filter != null)
                {
                    userList[i].EmpID = filter.EmpID;
                    uData.Add(userList[i]);
                }
            }

            List<int> retList = new List<int>();

            for (int i = 0; i < eData.Count; i++)
            {
                retList.Add(bu.InsertEmployee(eData[i], db_name, db_server));
            }

            for (int i = 0; i < uData.Count; i++)
            {
                retList.Add(bu.InsertUser(uData[i], db_name, db_server));
            }

            if (retList.All(x => x == 1))
            {
                ret = 1;
            }

            if (ret == 1)
            {
                msg = "ส่งข้อมูลเรียบร้อยแล้ว!!";
                msg.ShowInfoMessage();

                if (tabPage.SelectedIndex == 0) //ส่งข้อมูล
                {
                    BindEmployeeData("", "", label1, grdviewFrom); //ศูนย์ Center

                    db_server = grdBranch.Rows[0].Cells["col_db_server"].Value.ToString();
                    db_name = grdBranch.Rows[0].Cells["col_db_name"].Value.ToString();
                    BindEmployeeData(db_name, db_server, label10, grdviewTo, true); //ศูนย์ ปลายทาง
                }
            }
            else
            {
                msg = "ส่งข้อมูลล้มเหลว!!";
                msg.ShowErrorMessage();
            }
        }

        private void grdBranch_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdBranch.SetRowPostPaint(sender,e,this.Font);
        }

        private void grdBranch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectBranchDetails(e);
        }

        private void grdBranchList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //grdBranchList.SetRowPostPaint(sender, e, this.Font);
        }

        private void tabPage_Click(object sender, EventArgs e)
        {
            string db_server = "";
            string db_name = "";

            if (tabPage.SelectedIndex == 0)  //ส่งข้อมูล
            {
                BindEmployeeData("", "", label1, grdviewFrom); //ศูนย์ Center

                db_server = grdBranch.CurrentRow.Cells["col_db_server"].Value.ToString();
                db_name = grdBranch.CurrentRow.Cells["col_db_name"].Value.ToString();
                BindEmployeeData(db_name, db_server, label10, grdviewTo, true); //ศูนย์ ปลายทาง

                label8.Text = grdBranch.CurrentRow.Cells["colBranchName"].Value.ToString();
            }
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            SetCheckBox(grdviewFrom, chkSelectAllUser);
        }

        private void grdviewFrom_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdviewFrom.SetRowPostPaint(sender,e, this.Font);
        }

        private void grdviewTo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdviewTo.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string db_server = "";
            string db_name = "";

            if (tabPage.SelectedIndex == 0) //ส่งข้อมูล
            {
                chkSelectAllUser.Checked = false;
                BindEmployeeData(db_name, db_server, label1, grdviewFrom); //ศูนย์ Center
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string db_server = "";
                string db_name = "";

                if (tabPage.SelectedIndex == 0) //ส่งข้อมูล
                {
                    BindEmployeeData(db_name, db_server, label1, grdviewFrom); //ศูนย์ Center
                }
            }
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSendUserData_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
