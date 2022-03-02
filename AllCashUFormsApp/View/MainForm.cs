using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.Page;
using AllCashUFormsApp.Controller;
using System.Globalization;

namespace AllCashUFormsApp.View
{
    public partial class MainForm : Form
    {
        CultureInfo cultures = System.Globalization.CultureInfo.GetCultureInfo("th-TH");

        MenuBU menuBU = new MenuBU();
        Permission permBU = new Permission();
        List<tbl_MstMenu> menuList = new List<tbl_MstMenu>();

        public MainForm()
        {
            InitializeComponent();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            this.Text = "หน้าหลัก" + " - " + ConfigurationManager.AppSettings["Version"] + " Version " + version;

            menuStrip1.Cursor = Cursors.Hand;
            StartTimer();
            //menuStrip1.MouseLeave += new EventHandler(SetCursorToHandOn_MouseLeave);
            //menuStrip1.MouseEnter += new EventHandler(SetCursorToArrowOn_MouseEnter);

            toolStripStatusLabel1.BackColor = Color.FromArgb(94, 186, 125);
            statusStrip1.BackColor = Color.FromArgb(205, 233, 254);
        }

        System.Windows.Forms.Timer tmr = null;
        private void StartTimer()
        {
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 1000;
            tmr.Tick += new EventHandler(timer1_Tick);
            tmr.Enabled = true;
        }

        //private void SetCursorToArrowOn_MouseHover(object sender, EventArgs e)
        //{
        //    this.Cursor = Cursors.Hand;
        //}

        //private void SetCursorToArrowOn_DragOver(object sender, EventArgs e)
        //{
        //    this.Cursor = Cursors.Hand;
        //}

        //private void SetCursorToHandOn_MouseLeave(object sender, EventArgs e)
        //{
        //    this.Cursor = Cursors.Arrow;
        //}

        private void MainForm_Load(object sender, EventArgs e)
        {
            //FlexibleMessageBox.MAX_WIDTH_FACTOR = Math.Round(100 * 0.1, 1);
            //FlexibleMessageBox.MAX_HEIGHT_FACTOR = Math.Round(100 * 0.1, 1);
            FlexibleMessageBox.FONT = new Font("Tahoma", 10, FontStyle.Regular);

            if (Helper.tbl_Users != null)
            {
                toolStripStatusLabel1.Text = "";
            }

            var menuImage = new Bitmap(AllCashUFormsApp.Properties.Resources.menu);

            menuList = menuBU.GetAllData().OrderBy(x => x.Seq).ToList();

            var permission = permBU.VerifyPermission(Helper.tbl_Users.RoleID.Value);

            foreach (var topParent in menuList.Where(x => string.IsNullOrEmpty(x.MenuParent.ToString())).ToList())
            {
                var pr_perm = permission.FirstOrDefault(x => x.ControlID == topParent.MenuID);
                var topItem = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Name = topParent.MenuName,
                    Text = topParent.MenuText,
                    Image = topParent.MenuImage.byteArrayToImage(),
                    Visible = pr_perm != null ? pr_perm.Visible.Value : false,
                    Enabled = pr_perm != null ? pr_perm.Enable.Value : false
                };

                //allow super admin only edit by sailom .k 27/07/2021
                if (topItem.Name == "frmDataMigration" || topItem.Name == "frmAddMenu")
                {
                    if (Helper.tbl_Users.RoleID != 10)
                    {
                        topItem.Visible = false;
                        topItem.Enabled = false;
                    }
                }

                foreach (var menuParent in menuList.Where(x => x.MenuParent == topParent.MenuID))
                {
                    var mpr_perm = permission.FirstOrDefault(x => x.ControlID == menuParent.MenuID);
                    var parentItem = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = menuParent.MenuName,
                        Text = menuParent.MenuText,
                        Image = menuParent.MenuImage.byteArrayToImage(),
                        Visible = mpr_perm != null ? mpr_perm.Visible.Value : false,
                        Enabled = mpr_perm != null ? mpr_perm.Enable.Value : false
                    };

                    parentItem.Click += ToolStripMenuItem_Click;

                    foreach (var menuParent2 in menuList.Where(x => x.MenuParent == menuParent.MenuID))
                    {
                        var mpr2_perm = permission.FirstOrDefault(x => x.ControlID == menuParent.MenuID);
                        var parentItem2 = new System.Windows.Forms.ToolStripMenuItem()
                        {
                            Name = menuParent2.MenuName,
                            Text = menuParent2.MenuText,
                            Image = menuParent2.MenuImage.byteArrayToImage(),
                            Visible = mpr2_perm != null ? mpr2_perm.Visible.Value : false,
                            Enabled = mpr2_perm != null ? mpr2_perm.Enable.Value : false,
                        };

                        parentItem2.Click += ToolStripMenuItem_Click;

                        parentItem.DropDownItems.Add(parentItem2);
                    }

                    topItem.DropDownItems.Add(parentItem);
                }

                if (menuList.Count(x => x.MenuParent == topParent.MenuID) == 0)
                {
                    topItem.Click += ToolStripMenuItem_Click;
                }

                menuStrip1.Items.Add(topItem);
            }


            menuStrip1.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpen = false;
            string name = ((ToolStripItem)sender).Name;
            List<Form> openForms = new List<Form>();

            foreach (Form f in Application.OpenForms)
            {
                openForms.Add(f);
            }

            foreach (Form f in openForms)
            {
                if (f.Name.ToLower() == name.ToLower()) //(f.Name == "frmOD")
                {
                    isOpen = true;
                    if (f.Visible == false)
                    {
                        f.Visible = true;

                        f.MdiParent = this;
                        f.StartPosition = FormStartPosition.CenterParent;
                        f.WindowState = FormWindowState.Minimized;
                        //frm.Dock = DockStyle.Fill;
                        f.Text = ((ToolStripItem)sender).Text;

                        MemoryManagement.FlushMemory();

                        f.Show();
                        f.WindowState = FormWindowState.Maximized;

                        f.Focus();
                    }
                    else
                    {
                        f.Focus();
                    }
                    break;
                }
            }

            if (name == "frmMinimize")
            {
                foreach (Form f in openForms)
                {
                    foreach (Form childForm in f.MdiChildren)
                    {
                        if (f.WindowState == FormWindowState.Maximized)
                        {
                            childForm.WindowState = FormWindowState.Minimized;
                            //childForm.WindowState = FormWindowState.Normal;
                        }
                        else
                        {
                            childForm.WindowState = FormWindowState.Normal;
                        }
                    }
                }
            }
            else if (name == "frmMaximize")
            {
                foreach (Form f in openForms)
                {
                    foreach (Form childForm in f.MdiChildren)
                    {
                        childForm.WindowState = FormWindowState.Maximized;
                    }
                }
            }
            else if (name == "frmCascade")
            {
                this.LayoutMdi(MdiLayout.Cascade);
            }
            else if (name == "frmTileHorizontal")
            {
                this.LayoutMdi(MdiLayout.TileHorizontal);
            }
            else if (name == "frmTileVertical")
            {
                this.LayoutMdi(MdiLayout.TileVertical);
            }

            if (!isOpen)
            {
                if (name == "frmLogOff")
                {
                    frmLogin login = new frmLogin();

                    //Last edit by sailom .k 28/02/2022-----------------------------------------------------
                    //foreach (Form f in openForms)
                    //{
                    //    if (f.Name != login.Name)
                    //    {
                    //        f.Hide();
                    //        f.Dispose();
                    //    }
                    //}
                    //Last edit by sailom .k 28/02/2022-----------------------------------------------------

                    if (login != null)
                    {
                        if (Helper.tbl_Users != null)
                            login.PrepareDefaultLogin(Helper.tbl_Users.Username, Helper.tbl_Users.Password);

                        login.Show();
                        return;
                    }
                }

                Form frm = name.GetFormByName();
                if (frm != null)
                {
                    frm.MdiParent = this;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.WindowState = FormWindowState.Minimized;
                    //frm.Dock = DockStyle.Fill;
                    frm.Text = ((ToolStripItem)sender).Text;

                    MemoryManagement.FlushMemory();

                    frm.Show();
                    frm.WindowState = FormWindowState.Maximized;
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string cTime = DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss", cultures);
            toolStripStatusLabel1.Text = "ฐานข้อมูล : " + Helper.BranchName + ", ผู้ใช้งานระบบ : " + Helper.tbl_Users.Username + ", วันที่ : " + cTime;
        }
    }
}
