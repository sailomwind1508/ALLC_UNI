using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmSendUserData : Form
    {
        MasterDataControl bu = new MasterDataControl();
        public frmSendUserData()
        {
            InitializeComponent();
        }

        #region Method
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

            if (!Connection.ConnectionString.Contains("DB_SDSS_UNI_CENTER"))
            {
                string msg = "สามารถใช้ได้เมื่อต่อ CENTER DB เท่านั้น !!";
                msg.ShowWarningMessage();
                return;
            }

            btnSearchBranch.PerformClick();
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
            Cursor.Current = Cursors.WaitCursor;

            bu.proc_GetDNS_Data(); //ดึงข้อมูล DNS ที่ออนไลน์ เฉพาะ ศูนย์ U-Force Edit By Adisorn 31/05/2022

            DataTable branchDT = bu.Get_proc_SendProductInfo_GetDataTable();

            DataTable newDT = new DataTable();
            SetBranchTable(newDT);

            Bitmap wifi_Img = new Bitmap(Properties.Resources.wifi); // new Resource Image
            Bitmap power_off_lmg = new Bitmap(Properties.Resources.closeBtn);

            foreach (DataRow r in branchDT.Rows)
            {
                //Bitmap colPic = null; //adisorn 31/05/2022
                //if (Convert.ToBoolean(r["OnlineStatus"]) == true && r["db_name"].ToString().Contains("DB_ALL_CASH_UNI"))
                //    colPic = wifi_Img;
                //else
                //    colPic = power_off_lmg;

                Bitmap colPic = Convert.ToBoolean(r["OnlineStatus"]) == true ? wifi_Img : power_off_lmg;
                newDT.Rows.Add(false
                        , r["BranchID"].ToString()
                        , r["BranchRefCode"].ToString()
                        , r["BranchName"].ToString()
                        , colPic
                        , r["OnlineStatus"]
                        , r["db_server"].ToString()
                        , r["db_name"].ToString()
                    );
            }

            grdBranch.DataSource = newDT;

            for (int i = 0; i < grdBranch.RowCount; i++)
            {
                bool _OnlineStatus = Convert.ToBoolean(grdBranch.Rows[i].Cells["colOnlineStatus"].Value);
                var currentCell = grdBranch.Rows[i].Cells["colBranchCheck"];
                if (currentCell is DataGridViewCheckBoxCell)
                {
                    DataGridViewCheckBoxCell cellCheck = currentCell as DataGridViewCheckBoxCell;
                    if (_OnlineStatus == true)
                        cellCheck.ReadOnly = false;
                    else
                    {
                        cellCheck.Value = false;
                        cellCheck.ReadOnly = true;
                    }
                }
            }

            lblgrdQty.Text = newDT.Rows.Count.ToNumberFormat();

            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            if (newDT.Rows.Count > 0)
            {
                btnSendData.Enabled = true;
                btnCancelSend.Enabled = true;
            }
            else
            {
                btnSendData.Enabled = false;
                btnCancelSend.Enabled = false;
            }

            Cursor.Current = Cursors.Default;
        }

        private void BindEmployeeData(string db_name, string db_server)
        {
            Cursor.Current = Cursors.WaitCursor;

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@flagDel", "0");
            _params.Add("@db_name", db_name);
            _params.Add("@db_server", db_server);
            _params.Add("@Search", txtSearch.Text);
            var dt = bu.proc_Employee_Data(_params);

            DataTable newEmp = new DataTable();
            SetEmployeeTable(newEmp);

            foreach (DataRow r in dt.Rows)
            {
                newEmp.Rows.Add(false, r["EmpID"], r["FirstName"], r["LastName"], r["DepartmentName"], r["PositionName"] );
            }

            grdviewFrom.DataSource = newEmp;

            grdviewFrom.Columns[0].HeaderText = "";
            grdviewFrom.Columns[0].Width = 30;
            grdviewFrom.Columns[1].Width = 120;
            grdviewFrom.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdviewFrom.Columns[3].Width = 220;
            grdviewFrom.Columns[4].Width = 150;
            grdviewFrom.Columns[5].Width = 150;

            for (int i = 1; i < grdviewFrom.ColumnCount; i++)//ปิด Columns ยกเว้น ปุ่ม CheckBox
            {
                grdviewFrom.Columns[i].ReadOnly = true;
            }

            label1.Text = dt.Rows.Count.ToNumberFormat();

            Cursor.Current = Cursors.Default;
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

        private string FilterEmp()
        {
            //List<string> EmpList = new List<string>();

            //foreach (DataGridViewRow r in grdviewFrom.Rows)
            //{
            //    if (Convert.ToBoolean(r.Cells[0].Value) == true)
            //    {
            //        EmpList.Add(r.Cells[1].Value.ToString().Trim());//EmpID
            //    }
            //}

            //Dictionary<string, object> _params = new Dictionary<string, object>();
            //_params.Add("@flagDel", "0");
            //_params.Add("@db_name", grdBranch.CurrentRow.Cells["col_db_name"].Value.ToString());
            //_params.Add("@db_server", grdBranch.CurrentRow.Cells["col_db_server"].Value.ToString());
            //_params.Add("@Search", txtSearch.Text);
            //var dt = bu.proc_Employee_Data(_params);

            //foreach (DataGridViewRow r in grdviewFrom.Rows)
            //{
            //    string firstName = r.Cells[2].Value.ToString().Trim();
            //    string LastName = r.Cells[3].Value.ToString().Trim();

            //    DataRow dr = dt.AsEnumerable().FirstOrDefault(x => x.Field<string>("FirstName").Trim() == firstName && x.Field<string>("LastName").Trim() == LastName);

            //    if (Convert.ToBoolean(r.Cells[0].Value) == true && dr == null)
            //    {
            //        EmpList.Add(r.Cells[1].Value.ToString());//EmpID
            //    }
            //}

            //string emp = string.Join(",", EmpList);
            return "";
        }

        private void SendData()
        {
            //try
            //{
            //    string msg = "";

            //    List<int> ret = new List<int>();

            //    bool _OnlineStatus = Convert.ToBoolean(grdBranch.CurrentRow.Cells["colOnlineStatus"].Value);

            //    if (_OnlineStatus == false) // เลือก ศูนย์ที่ออนไลน์ เท่านั้น
            //        msg += "เลือกศูนย์ที่ Online เท่านั้น !!\n";

            //    if (EmployeeCheck() == false)
            //        msg += "เลือกพนักงานที่ต้องการส่งข้อมูล !!\n";

            //    if (!string.IsNullOrEmpty(msg))
            //    {
            //        msg.ShowWarningMessage();
            //        return;
            //    }

            //    string listEmpID = FilterEmp();

            //    if (listEmpID == "")
            //    {
            //        for (int i = 0; i < grdviewFrom.RowCount; i++)
            //        {
            //            grdviewFrom.Rows[i].Cells[0].Value = false;
            //        }

            //        msg = "ข้อมูลพนักงานซ้ำในระบบ !!";
            //        msg.ShowWarningMessage();
            //        return;
            //    }

            //    Cursor.Current = Cursors.WaitCursor;

            //    var empList = bu.SelectEmpList(listEmpID);//ข้อมูล Employee จาก Center
            //    var userList = bu.SelectUserList(listEmpID);//ข้อมูล User จาก Center

            //    string branchID = grdBranch.CurrentRow.Cells["colBranchID"].Value.ToString();
            //    string db_server = grdBranch.CurrentRow.Cells["col_db_server"].Value.ToString();
            //    string db_name = grdBranch.CurrentRow.Cells["col_db_name"].Value.ToString();

            //    Dictionary<string, object> _params = new Dictionary<string, object>();
            //    _params.Add("@flagDel", "0");
            //    _params.Add("@db_name", db_name);
            //    _params.Add("@db_server", db_server);

            //    DataTable dtEmp = bu.proc_Employee_Data(_params); //ค้นหา Employee จาก Server ที่เลือก

            //    var eData = new List<tbl_Employee>();
            //    var uData = new List<tbl_Users>();

            //    for (int i = 0; i < empList.Count; i++)
            //    {
            //        string formatEmpID = branchID + "E";

            //        if (dtEmp.Rows.Count == 0)
            //        {
            //            formatEmpID += "001";
            //        }
            //        else
            //        {
            //            string maxID = "";

            //            if (eData.Count == 0)
            //                maxID = dtEmp.AsEnumerable().Max(x => x.Field<string>("EmpID"));
            //            else
            //                maxID = eData.Max(x => x.EmpID);

            //            int maxEmpID = Convert.ToInt32(maxID.Substring(4, 3)) + 1;
            //            formatEmpID += maxEmpID.ToString();
            //        }

            //        empList[i].EmpID = formatEmpID;
            //        empList[i].EmpCode = formatEmpID;
            //        eData.Add(empList[i]);
            //    }

            //    for (int i = 0; i < userList.Count; i++)
            //    {
            //        uData = new List<tbl_Users>();

            //        var filter = eData.FirstOrDefault(x => x.FirstName == userList[i].FirstName && x.LastName == userList[i].LastName);
            //        if (filter != null)
            //        {
            //            userList[i].EmpID = filter.EmpID;
            //            uData.Add(userList[i]);
            //        }
            //    }

            //    //db_name = "DB_ALL_CASH_UNI_NMA_TEST";

            //    for (int i = 0; i < eData.Count; i++)
            //    {
            //        ret.Add(bu.InsertEmployee(eData[i], db_name, db_server));
            //    }

            //    for (int i = 0; i < uData.Count; i++)
            //    {
            //        ret.Add(bu.InsertUser(uData[i], db_name, db_server));
            //    }

            //    if (ret.All(x => x == 1))
            //    {
            //        msg = "ส่งข้อมูลเรียบร้อยแล้ว !!";
            //        msg.ShowInfoMessage();

            //        for (int i = 0; i < grdviewFrom.RowCount; i++)
            //        {
            //            grdviewFrom.Rows[i].Cells[0].Value = false;
            //        }
            //    }
            //    else
            //    {
            //        msg = "ส่งข้อมูลล้มเหลว!!";
            //        msg.ShowErrorMessage();
            //    }

            //    Cursor.Current = Cursors.Default;
            //}
            //catch (Exception ex)
            //{
            //    ex.Message.ShowErrorMessage();
            //    Cursor.Current = Cursors.Default;
            //}
        }

        #endregion

        #region Method
        private void frmSendUserData_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }

        private void btnSearchBranch_Click(object sender, EventArgs e)
        {
            BindBranchData();
            btnSearch.PerformClick();
        }

        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            BindBranchData();
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            //SendData(); 
            try
            {
                string msg = "";

                if (Convert.ToBoolean(grdBranch.CurrentRow.Cells["colOnlineStatus"].Value) == false) // เลือก ศูนย์ Online
                    msg += "เลือกศูนย์ที่ Online เท่านั้น !!\n";

                if (EmployeeCheck() == false)
                    msg += "เลือกพนักงานที่ต้องการส่งข้อมูล !!\n";

                if (!string.IsNullOrEmpty(msg))
                {
                    msg.ShowWarningMessage();
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                List<string> EmpList = new List<string>();

                foreach (DataGridViewRow r in grdviewFrom.Rows)
                {
                    if (Convert.ToBoolean(r.Cells[0].Value) == true)
                    {
                        EmpList.Add(r.Cells[1].Value.ToString().Trim());
                    }
                }

                string allEmpID = string.Join(",", EmpList);
                string _branchID = grdBranch.CurrentRow.Cells["colBranchID"].Value.ToString();
                string _dbName = grdBranch.CurrentRow.Cells["col_db_name"].Value.ToString();
                string _dbServer = grdBranch.CurrentRow.Cells["col_db_server"].Value.ToString();

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@EmpID", allEmpID);
                _params.Add("@db_server", _dbServer);
                _params.Add("@db_name", _dbName);
                _params.Add("@branch_ID", _branchID);
                _params.Add("@UserName",Helper.tbl_Users.Username);

                bool flagSend = bu.proc_SendUserData_ToBranch(_params);

                if (flagSend)
                {
                    msg = "ส่งข้อมูลเรียบร้อยแล้ว !!";
                    msg.ShowInfoMessage();

                    for (int i = 0; i < grdviewFrom.RowCount; i++)
                    {
                        grdviewFrom.Rows[i].Cells[0].Value = false;
                    }
                }
                else
                {
                    msg = "ส่งข้อมูลล้มเหลว!!";
                    msg.ShowErrorMessage();
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                Cursor.Current = Cursors.Default;
            }
        }

        private void chkboxBranchCheck_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (grdBranch.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < grdBranch.Rows.Count; i++)
            //        {
            //            bool OnlineStatus = Convert.ToBoolean(grdBranch.Rows[i].Cells["colOnlineStatus"].Value);

            //            if (Convert.ToBoolean(grdBranch.Rows[i].Cells["colBranchCheck"].Value) == false && OnlineStatus == true)
            //                grdBranch.Rows[i].Cells["colBranchCheck"].Value = true;
            //            else
            //                grdBranch.Rows[i].Cells["colBranchCheck"].Value = false;
            //        }
            //    }
            //    else
            //        chkboxBranchCheck.Checked = false;
            //}
            //catch (Exception ex)
            //{
            //    ex.Message.ShowErrorMessage();
            //}
        }

        private void tabPage_Click(object sender, EventArgs e)
        {
            if (tabPage.SelectedIndex == 0) //ส่งข้อมูลพนักงาน
            {
                BindEmployeeData("", "");
            }
        }

        private void chkSelectAllUser_Click(object sender, EventArgs e)
        {
            SetCheckBox(grdviewFrom, chkSelectAllUser);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindEmployeeData("", "");
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindEmployeeData("", "");
            }
        }

        private void grdviewFrom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                    return;
                if (e.ColumnIndex == -1)
                    return;

                Cursor.Current = Cursors.WaitCursor;

                string _EmpID = grdviewFrom.Rows[e.RowIndex].Cells["รหัสพนักงาน"].Value.ToString();

                if (!string.IsNullOrEmpty(_EmpID))
                {
                    frmEmployeeInfo frm = new frmEmployeeInfo();
                    frm.empID = _EmpID;

                    MainForm mfrm = null;
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f.Name.ToLower() == "mainform")
                        {
                            mfrm = (MainForm)f;
                        }
                    }

                    frm.MdiParent = mfrm;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();

                    btnSearch.PerformClick();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        #endregion

        private void frmSendUserData_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdviewFrom_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdviewFrom.SetRowPostPaint(sender, e, this.Font);
        }

        private void grdBranch_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdBranch.SetRowPostPaint(sender, e, this.Font);
        }
    }
}
 