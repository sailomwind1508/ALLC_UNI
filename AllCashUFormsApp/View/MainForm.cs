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

namespace AllCashUFormsApp.View
{
    public partial class MainForm : Form
    {
        MenuBU menuBU = new MenuBU();
        List<tbl_MstMenu> menuList = new List<tbl_MstMenu>();
        
        public MainForm()
        {
            InitializeComponent();
            this.Text = "หน้าหลัก" + " - " + ConfigurationManager.AppSettings["Version"];
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Helper.tbl_Users != null)
            {
                toolStripStatusLabel1.Text = "ฐานข้อมูล : " + Helper.BranchName + ", ผู้ใช้งานระบบ : " + Helper.tbl_Users.Username;
            }

            var menuImage = new Bitmap(AllCashUFormsApp.Properties.Resources.menu);

            menuList = menuBU.GetAllData().OrderBy(x => x.Seq).ToList();
            

            foreach (var topParent in menuList.Where(x => string.IsNullOrEmpty(x.MenuParent.ToString())).ToList())
            {
                var topItem = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Name = topParent.MenuName,
                    Text = topParent.MenuText,
                    Image = topParent.MenuImage.byteArrayToImage()
                };

                foreach (var menuParent in menuList.Where(x => x.MenuParent == topParent.MenuID))
                {
                    var parentItem = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = menuParent.MenuName,
                        Text = menuParent.MenuText,
                        Image = menuParent.MenuImage.byteArrayToImage()
                    };

                    parentItem.Click += ToolStripMenuItem_Click;

                    foreach (var menuParent2 in menuList.Where(x => x.MenuParent == menuParent.MenuID))
                    {
                        var parentItem2 = new System.Windows.Forms.ToolStripMenuItem()
                        {
                            Name = menuParent2.MenuName,
                            Text = menuParent2.MenuText,
                            Image = menuParent2.MenuImage.byteArrayToImage()
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

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name.ToLower() == name.ToLower()) //(f.Name == "frmOD")
                {
                    isOpen = true;
                    f.Focus();
                    break;
                }
            }

            if (!isOpen)
            {
                Form frm = name.GetFormByName();
                if (frm != null)
                {
                    frm.MdiParent = this;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.WindowState = FormWindowState.Minimized;
                    //frm.Dock = DockStyle.Fill;
                    frm.Text = ((ToolStripItem)sender).Text;
                    frm.Show();
                    frm.WindowState = FormWindowState.Maximized;
                }
               
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
