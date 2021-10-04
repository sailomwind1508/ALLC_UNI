using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    public partial class frmTLData : Form
    {
        MasterDataControl bu = new MasterDataControl();
        MenuBU menuBU = new MenuBU();
        List<Control> searchBranchControls = new List<Control>();
        public frmTLData()
        {
            InitializeComponent();
            searchBranchControls = new List<Control>() { txtBranchCode, txtBranchName };
        }
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
            btnAdd.EnableButton(btnEdit, btnRemove, btnSave, btnCancel, btnCopy, btnPrint, "");
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            txtBranchName.DisableTextBox(true);

            var b = bu.GetBranch();

            if (b != null)
            {
                txtBranchCode.Text = b[0].BranchCode;
                txtBranchName.Text = b[0].BranchName;
            }

            var allWH = bu.GetAllBranchWarehouse(x=>x.WHType == 1);
            ddlWH.BindDropdownList(allWH,"WHCode","WHID",0);
        }
        private void frmTLData_Load(object sender, EventArgs e)
        {
            InitPage();
            InitialData();
        }
        private void txtBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var b = bu.GetBranch(x => x.BranchCode == txtBranchCode.Text);
                if (b.Count > 0)
                {
                    txtBranchName.Text = b[0].BranchName;
                }
                else
                {
                    txtBranchCode.Clear();
                    txtBranchName.Clear();
                }
            }
        }
        private void btnSearchBranch_Click(object sender, EventArgs e)
        {
            this.OpenFromBranchIDPopup(searchBranchControls, "เลือกสาขา/ซุ้ม");
        }
        private void NotSortColumnsGridView(List<DataGridView> grdList)
        {
            foreach (var grd in grdList)
            {
                foreach (DataGridViewColumn column in grd.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
           
        }
        private void BindData()
        {
            string WHID = ddlWH.SelectedValue.ToString();

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("@WHID",WHID);

            DataTable TL_POMaster1 = new DataTable();
            TL_POMaster1 = bu.GetTLData_TL_POMaster1(_params);
            grdTL_POMaster1.DataSource = TL_POMaster1;
            lblQtyCount_POMaster1.Text = TL_POMaster1.Rows.Count.ToNumberFormat();

            DataTable TL_POMaster2 = new DataTable();
            TL_POMaster2 = bu.GetTLData_TL_POMaster2(_params);
            grdTL_POMaster2.DataSource = TL_POMaster2;
            lblQtyCount_TL_POMaster2.Text = TL_POMaster2.Rows.Count.ToNumberFormat();

            DataTable TL_PODetail = new DataTable();
            TL_PODetail = bu.GetTL_data_tbl_TL_PODetail(_params);
            grdTL_PODetail.DataSource = TL_PODetail;
            lblQtyCount_TL_PODetail.Text = TL_PODetail.Rows.Count.ToNumberFormat();

            DataTable TL_ArCustomerShelf = new DataTable();
            TL_ArCustomerShelf = bu.GetTL_data_tbl_TL_ArCustomerShelf(_params);
            grdTL_ArCustomerShelf.DataSource = TL_ArCustomerShelf;
            lblQtyCount_TL_ArCustomerShelf.Text = TL_ArCustomerShelf.Rows.Count.ToNumberFormat();

            DataTable TL_CustomerCode = new DataTable();
            TL_CustomerCode = bu.GetTL_data_tbl_TL_CustomerCode(_params);
            grdTL_CustomerCode.DataSource = TL_CustomerCode;
            lblQtyCount_TL_CustomerCode.Text = TL_CustomerCode.Rows.Count.ToNumberFormat();

            DataTable TL_Visit = new DataTable();
            TL_Visit = bu.GetTL_data_tbl_TL_Visit(_params);
            grdTL_Visit.DataSource = TL_Visit;
            lblQtyCount_TL_Visit.Text = TL_Visit.Rows.Count.ToNumberFormat();

            DataTable TL_VisitStock = new DataTable();
            TL_VisitStock = bu.GetTL_data_tbl_TL_VisitStock(_params);
            grdTL_VisitStock.DataSource = TL_VisitStock;
            lblQtyCount_TL_VisitStock.Text = TL_VisitStock.Rows.Count.ToNumberFormat();

            DataTable TL_ArCustomer = new DataTable();
            TL_ArCustomer = bu.GetTL_data_tbl_TL_ArCustomer(_params);
            grdTL_ArCustomer.DataSource = TL_ArCustomer;
            lblQtyCount_TL_ArCustomer.Text = TL_ArCustomer.Rows.Count.ToNumberFormat();

            var grdList = new List<DataGridView>();
            grdList.Add(grdTL_POMaster1);
            grdList.Add(grdTL_POMaster2);
            grdList.Add(grdTL_PODetail);
            grdList.Add(grdTL_ArCustomerShelf);
            grdList.Add(grdTL_CustomerCode);
            grdList.Add(grdTL_Visit);
            grdList.Add(grdTL_VisitStock);
            grdList.Add(grdTL_ArCustomer);

            NotSortColumnsGridView(grdList);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void grdTL_POMaster1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdTL_POMaster1.SetRowPostPaint(sender, e, this.Font);
        }
        private void grdTL_POMaster2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdTL_POMaster2.SetRowPostPaint(sender, e, this.Font);
        }
        private void grdTL_PODetail_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdTL_PODetail.SetRowPostPaint(sender, e, this.Font);
        }
        private void grdTL_ArCustomerShelf_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdTL_ArCustomerShelf.SetRowPostPaint(sender, e, this.Font);
        }
        private void grdTL_CustomerCode_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdTL_CustomerCode.SetRowPostPaint(sender, e, this.Font);
        }
        private void grdTL_Visit_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdTL_Visit.SetRowPostPaint(sender, e, this.Font);
        }
        private void grdTL_VisitStock_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdTL_VisitStock.SetRowPostPaint(sender, e, this.Font);
        }
        private void grdTL_ArCustomer_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdTL_ArCustomer.SetRowPostPaint(sender, e, this.Font);
        }

        private void frmTLData_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }
    }
}
