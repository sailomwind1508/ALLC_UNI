using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;

namespace AllCashUFormsApp.View.Page
{
    public partial class FrmAddMenu : Form
    {
        MenuBU bu = new MenuBU();
        List<tbl_MstMenu> tbl_MstMenuList = new List<tbl_MstMenu>();

        public FrmAddMenu()
        {
            InitializeComponent();

            //int w = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Size.Width);
            ////int w1 = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Size.Width) / 2;
            ////int w2 = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Size.Width) / 3;
            //int h = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Size.Height);
        }

        #region Private Methods

        private void InitPage()
        {
            FormHeader.Text = this.Text;
            FormHeader.BackColor = ColorTranslator.FromHtml("#7AD1F9");

            ClearForm();

            btnAdd.Enabled = true;

            OpenText(false);

            EnableButton(txtMenuID.Text);


            tbl_MstMenuList = bu.GetAllData();

            BindListView();

            var headerPic = tbl_MstMenuList.FirstOrDefault(x => x.FormName.ToLower() == this.Name.ToLower());
            if (headerPic != null)
            {
                FormPic.Image = headerPic.MenuImage.byteArrayToImage();
                FormPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        private void BindListView()
        {
            tbl_MstMenuList = bu.GetAllData();

            listViewMenu.Items.Clear();
            listViewPicture.Items.Clear();

            foreach (var item in tbl_MstMenuList)
            {
                var row = new string[] { item.MenuID.ToString(), item.MenuName, item.MenuText, item.MenuParent.ToString(), item.FormName, item.Seq.ToString() };
                ListViewItem listItem = new ListViewItem(row);
                listItem.Tag = item;

                listViewMenu.Items.Add(listItem);
            }

            listViewPicture.View = System.Windows.Forms.View.Details;
            listViewPicture.Columns.Add("Menu Image", 150);
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(30, 30);
            foreach (var item in tbl_MstMenuList)
            {
                if (item.MenuImage != null)
                    imageList.Images.Add(item.MenuImage.byteArrayToImage());
            }

            listViewPicture.SmallImageList = imageList;
            int index = 0;
            foreach (var item in tbl_MstMenuList)
            {
                listViewPicture.Items.Add(item.MenuName, index);
                index++;
            }


            //var addImg = new Bitmap(AllCashUFormsApp.Properties.Resources.addBtn);
        }

        private void EnableButton(string conditionText)
        {
            btnEdit.Enabled = !string.IsNullOrEmpty(conditionText);
            btnRemove.Enabled = !string.IsNullOrEmpty(conditionText);
            btnSave.Enabled = !btnEdit.Enabled && !btnAdd.Enabled;
            btnCancel.Enabled = !btnAdd.Enabled;
            btnAdd.Enabled = !btnSave.Enabled && !btnEdit.Enabled;

            btnCopy.Enabled = false;
            btnPrint.Enabled = false;
        }

        public tbl_MstMenu PrepareData()
        {
            tbl_MstMenu tbl_MstMenu = new tbl_MstMenu();

            try
            {
                tbl_MstMenu.UserID = Convert.ToInt32(txtUserID.Text);
                tbl_MstMenu.MenuName = txtMenuName.Text;
                tbl_MstMenu.MenuText = txtMenuText.Text;
                tbl_MstMenu.FormName = txtFormName.Text;
                tbl_MstMenu.Seq = Convert.ToInt32(txtSeq.Text);

                if (!string.IsNullOrEmpty(txtMenuID.Text))
                {
                    int menuID = Convert.ToInt32(txtMenuID.Text);
                    tbl_MstMenu.MenuID = menuID;
                }

                if (!string.IsNullOrEmpty(txtParent.Text))
                {
                    tbl_MstMenu.MenuParent = Convert.ToInt32(txtParent.Text);
                }
                else
                {
                    tbl_MstMenu.MenuParent = null;
                }

                tbl_MstMenu.MenuImage = MenuPic.Image.ImageToByte();
                txtPicture.Text = openFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                throw;
            }

            return tbl_MstMenu;

        }

        private void ClearForm()
        {
            txtUserID.ClearTextBox();
            txtMenuName.ClearTextBox();
            txtMenuText.ClearTextBox();
            txtFormName.ClearTextBox();
            txtMenuID.ClearTextBox();
            txtParent.ClearTextBox();
            txtPicture.ClearTextBox();
            txtSeq.ClearTextBox();

            MenuPic.Image = null;
            MenuPicShow.Image = null;

            btnAdd.Enabled = true;
            EnableButton(txtMenuID.Text);
        }

        private void OpenText(bool mode)
        {
            txtUserID.Enabled = mode;
            txtMenuName.Enabled = mode;
            txtMenuText.Enabled = mode;
            txtFormName.Enabled = mode;
            txtMenuID.Enabled = false;
            txtParent.Enabled = mode;
            txtPicture.Enabled = false;
            txtSeq.Enabled = mode;
            btnBrowse.Enabled = mode;
        }

        private bool ValidateSave()
        {
            bool ret = true;
            List<string> errList = new List<string>();

            errList.SetErrMessageList(txtUserID);
            errList.SetErrMessageList(txtMenuName);
            errList.SetErrMessageList(txtMenuText);
            errList.SetErrMessageList(txtSeq);
            errList.SetErrMessageList(txtPicture);

            if (errList.Count > 0)
            {
                ret = false;
            }

            if (!ret)
            {
                string message = "กรุณากรอกข้อมูลที่จำเป็น \n" + string.Join("\n", errList);
                FlexibleMessageBox.Show(message, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ret;
        }

        private void BindData(int menuID, string menuName = "")
        {
            tbl_MstMenu menu = null;
            menu = tbl_MstMenuList.FirstOrDefault(x => x.MenuID == menuID);
            if (menu != null)
            {
                SetObject(menu);
            }
            else
            {
                menu = tbl_MstMenuList.FirstOrDefault(x => x.MenuName == menuName);
                if (menu != null)
                {
                    SetObject(menu);
                }
            }
        }

        private void SetObject(tbl_MstMenu menu)
        {
            txtMenuID.Text = menu.MenuID.ToString();
            txtUserID.Text = menu.UserID.ToString();
            txtMenuText.Text = menu.MenuText;
            txtFormName.Text = menu.FormName;
            txtParent.Text = menu.MenuParent.ToString();
            txtMenuName.Text = menu.MenuName;
            txtSeq.Text = menu.Seq.ToString();

            var image = menu.MenuImage.byteArrayToImage();

            MenuPic.Image = image;
            MenuPicShow.Image = image.ImageToByte().byteArrayToImage(243, 172);
        }

        private void CalcPanalSize()
        {
            var newSize = new Size(Convert.ToInt32(panel3.Size.Width / 1.5), panel3.Size.Height);
            panel6.Size = newSize;
            panel7.Size = new Size(newSize.Width / 2, newSize.Height);

            if (listViewMenu.Size.Width != 0)
            {
                var menu_w = Convert.ToInt32(panel6.Size.Width / 1.5);
                listViewMenu.Size = new Size(menu_w, listViewMenu.Size.Height);

                var pic_w = menu_w / 2;
                listViewPicture.Size = new Size(pic_w, listViewPicture.Size.Height);
            }
        }

        #endregion

        #region Event Methods

        private void frmAddMenu_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Image";
            openFileDialog1.Filter = "PNG files (*.PNG)|*.PNG";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(openFileDialog1.FileName);
                MenuPic.Image = image.ImageToByte().byteArrayToImage();
                MenuPicShow.Image = image.ImageToByte().byteArrayToImage(228, 228);

                //MenuPic.Image = bitmap.ImageToByte(228, 228).byteArrayToImage(228, 228);
                txtPicture.Text = openFileDialog1.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateSave())
                    return;

                tbl_MstMenu tbl_MstMenu = PrepareData();
                int ret = 0;

                ret = bu.AddData(tbl_MstMenu);

                if (ret != 0)
                {
                    DialogResult dialogResult = FlexibleMessageBox.Show("Save successful!", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.OK)
                    {
                        InitPage();
                    }
                }
                else
                {
                    FlexibleMessageBox.Show("Save fail!", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                FlexibleMessageBox.Show(ex.Message, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateSave())
                    return;

                tbl_MstMenu tbl_MstMenu = PrepareData();
                int ret = 0;

                var menuList = bu.GetAllData();
                if (menuList.Count > 0 && menuList.FirstOrDefault(x => x.MenuID == tbl_MstMenu.MenuID) != null)
                {
                    ret = bu.UpdateData(tbl_MstMenu);
                }

                if (ret != 0)
                {
                    DialogResult dialogResult = FlexibleMessageBox.Show("Edit successful!", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.OK)
                    {
                        InitPage();
                    }
                }
                else
                {
                    FlexibleMessageBox.Show("Edit fail!", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                FlexibleMessageBox.Show(ex.Message, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewMenu.SelectedItems.Count > 0)
            {
                tbl_MstMenu selectedItem = (tbl_MstMenu)listViewMenu.SelectedItems[0].Tag;
                if (selectedItem != null)
                {
                    //for (int i = 0; i < listView1.Items.Count; i++)
                    //{
                    //    if (listView1.Items[i].Text == selectedItem.MenuName)
                    //    {
                    //        listView1.Items[i].Focused = true;
                    //    }

                    //}

                    ClearForm();

                    BindData(selectedItem.MenuID);

                    btnAdd.Enabled = false;
                    btnEdit.Enabled = true;
                    btnRemove.Enabled = true;
                    btnCancel.Enabled = true;

                    EnableButton(txtMenuID.Text);

                    OpenText(true);
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMenuID.Text))
            {
                FlexibleMessageBox.Show("Please select remove item!", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ret = 0;
            var menuList = bu.GetAllData();

            if (menuList.Count > 0)
            {
                tbl_MstMenu tbl_MstMenu = menuList.FirstOrDefault(x => x.MenuID == Convert.ToInt32(txtMenuID.Text));
                if (tbl_MstMenu != null)
                {
                    ret = bu.RemoveData(tbl_MstMenu);
                }
            }

            if (ret != 0)
            {
                DialogResult dialogResult = FlexibleMessageBox.Show("Remove successful!", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.OK)
                {
                    InitPage();
                }
            }
            else
            {
                FlexibleMessageBox.Show("Edit fail!", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            OpenText(false);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;

            EnableButton(txtMenuID.Text);

            OpenText(true);
        }

        private void listViewPicture_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewPicture.SelectedItems.Count > 0)
            {
                string MenuName = listViewPicture.SelectedItems[0].Text;
                if (!string.IsNullOrEmpty(MenuName))
                {
                    ClearForm();

                    BindData(-1, MenuName);

                    btnAdd.Enabled = false;
                    btnEdit.Enabled = true;
                    btnRemove.Enabled = true;
                    btnCancel.Enabled = true;

                    EnableButton(txtMenuID.Text);

                    OpenText(true);
                }
            }
        }

        private void txtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.ToNumberOnly(sender);
        }

        private void txtParent_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.ToNumberOnly(sender);
        }

        private void txtSeq_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.ToNumberOnly(sender);
        }

        private void FrmAddMenu_Resize(object sender, EventArgs e)
        {
            CalcPanalSize();
        }

        #endregion

        private void FrmAddMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
