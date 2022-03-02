using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using AllCashUFormsApp.View.Page;
namespace AllCashUFormsApp.View.UControl
{
    public partial class frmSearchProduct : Form
    {
        Product bu = new Product();
        public frmSearchProduct()
        {
            InitializeComponent();

            this.Load += frmSearchProduct_Load;

            btnAccept.Click += btnAccept_Click;
            btnCancel.Click += btnCancel_Click;

            grdList.RowPostPaint += grdList_RowPostPaint;
        }

        private void BindData()
        {
            DataTable dt = new DataTable();
            string txtSubGroup = "";
            txtSubGroup = frmReport._txtSubGroupPro2;

            List<string> listStr = txtSubGroup.Split(',').ToList();
            Dictionary<string, string> prdList = new Dictionary<string, string>();

            if (txtSubGroup != "")
            {
                foreach (var item in listStr)
                {
                    var _dt = bu.GetProductTable(x => x.FlagDel == false && x.ProductSubGroupID == Convert.ToInt32(item));
                    foreach (DataRow r in _dt.Rows)
                    {
                        prdList.Add(r["ProductCode"].ToString(), r["ProductName"].ToString());
                    }
                }
            }
            else
            {
                var _dt = bu.GetProductTable(x => x.FlagDel == false);
                foreach (DataRow r in _dt.Rows)
                {
                    prdList.Add(r["ProductCode"].ToString(), r["ProductName"].ToString());
                }
            }

            DataTable dtPrd = new DataTable();
            if(prdList.Count > 0)
            {
                dtPrd = prdList.ToDataTable();
            }

            grdList.DataSource = dtPrd;

            grdList.CreateCheckBoxHeaderColumn("colSelect");

            lblgridCount.Text = dtPrd.Rows.Count.ToNumberFormat();
        }

        private void frmSearchProduct_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;
            BindData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            List<string> selectList = new List<string>();
            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                    if (Convert.ToBoolean(grdList.Rows[i].Cells[0].Value) == true)
                    {
                        selectList.Add(grdList.Rows[i].Cells[1].Value.ToString());
                    }
            }
            var joinString = string.Join(",", selectList);
            frmReport._txtPro = joinString;
            this.Close();
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }
    }
}
