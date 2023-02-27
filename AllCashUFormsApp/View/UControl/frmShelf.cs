using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.View.Page;
using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;

namespace AllCashUFormsApp.View.UControl.A_UC
{
  
    public partial class frmShelf : Form
    {

        CustomerShelf bu = new CustomerShelf();
        public frmShelf()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveShelf_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtShelfNo.Text))
            //{
            //    frmCustomerInfo.shelfID = txtShelfNo.Text;
            //    frmCustomerInfo.Comment = txtRemark.Text;
            //    this.Close();
            //}
            //else
            //{
            //    string msg = "กรุณากรอกเลข Shelf !!";
            //    msg.ShowWarningMessage();
            //}

            try
            {
                if (frmCustomerInfo.page == "add" || frmCustomerInfo.page == "edit") //19-09-2022 adisorn
                {
                    string msg = "";
                    int ret = 0;

                    if (string.IsNullOrEmpty(txtShelfNo.Text))
                    {
                        msg = "กรุณากรอกเลข Shelf !!";
                        msg.ShowWarningMessage();
                        return;
                    }

                    Cursor.Current = Cursors.WaitCursor;

                    string custID = frmCustomerInfo._CustomerID;
                    var Shelf_List = bu.GetCustomerShelf(x => x.CustomerID == custID && x.ShelfID == txtShelfNo.Text);

                    var custShelf = new tbl_ArCustomerShelf();
                    if (Shelf_List.Count == 0)
                    {
                        var CustShelfList = bu.GetCustShelf();

                        if (CustShelfList.Count > 0)
                            custShelf.AutoID = CustShelfList.Max(x => x.AutoID) + 1;
                        else
                            custShelf.AutoID = 0;

                        custShelf.CustomerID = custID;
                        custShelf.ProductID = bu.GetProduct(x => x.ProductShortName.Contains("ชั้นวาง")).First().ProductID;
                        custShelf.ShelfID = txtShelfNo.Text.Trim();
                        custShelf.WHID = frmCustomerInfo.WarehouseID;

                        custShelf.CrDate = DateTime.Now;
                        custShelf.CrUser = Helper.tbl_Users.Username;

                        custShelf.FlagSend = false;
                        custShelf.FlagNew = true;
                        custShelf.FlagEdit = false;
                        custShelf.FlagDel = false;
                    }
                    else
                    {
                        custShelf = Shelf_List[0];
                        custShelf.EdDate = DateTime.Now;
                        custShelf.EdUser = Helper.tbl_Users.Username;
                    }

                    
                    custShelf.ImagePath = txtRemark.Text.Trim();

                    ret = bu.UpdateShelf(custShelf);

                    Cursor.Current = Cursors.Default;

                    if (ret == 1)
                    {
                        msg = "บันทึกข้อมูลเรียบร้อยแล้ว!!";
                        msg.ShowInfoMessage();
                        this.Close();
                    }
                    else
                    {
                        this.ShowProcessErr();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }
         
        public void frmShelf_Load(object sender, EventArgs e)
        {
            if (frmCustomerInfo.page == "add")
            {
                this.Text = "Add";
                this.Update();
                label1.Text = "เพิ่มเลข Shelf :";

                txtShelfNo.ClearTextBox();
                txtRemark.ClearTextBox();

                txtShelfNo.DisableTextBox(false);

                txtShelfNo.Focus();
            }
            else if (frmCustomerInfo.page == "edit")
            {
                this.Text = "Edit";
                this.Update();
                label1.Text = "แก้เลข Shelf :";

                txtShelfNo.Text = frmCustomerInfo.shelfID;
                txtShelfNo.DisableTextBox(true);

                Func<tbl_ArCustomerShelf,bool > func = x => x.CustomerID == frmCustomerInfo._CustomerID && x.ShelfID == frmCustomerInfo.shelfID;
                var listCustShelf = bu.GetCustomerShelf(func);
                if (listCustShelf.Count > 0)
                    txtRemark.Text = listCustShelf[0].ImagePath;
                else
                    txtRemark.Text = "";

                txtRemark.Focus();
            }
            //else if (frmCustomerInfo.page == "edit")
            //{
            //    this.Text = "Edit";
            //    this.Update();
            //    label1.Text = "แก้เลข Shelf :";
            //    txtShelfNo.Text = frmCustomerInfo.shelfID;
            //}
        }

        private void txtShelfNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
                e.Handled = true;
            else
                return;
        }
    }
}
