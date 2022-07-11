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
using AllCashUFormsApp.View.Page;
namespace AllCashUFormsApp.View.UControl
{
    public partial class frmSearchBranchWareHouseList : Form
    {
        BranchWarehouse bu = new BranchWarehouse();
        public frmSearchBranchWareHouseList()
        {
            InitializeComponent();
        }

        public void SetTableBranchWH(DataTable dtBranchWH)
        {
            dtBranchWH.Columns.Add("WHID", typeof(string));
            dtBranchWH.Columns.Add("WHName", typeof(string));
            dtBranchWH.Columns.Add("SaleType", typeof(string));
            dtBranchWH.Columns.Add("BranchID", typeof(string));
        }

        private void BindBranchWareHouse()
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dtBranchWH = new DataTable();
                SetTableBranchWH(dtBranchWH);

                if (frmReport._RptStock == "ALL")
                {
                    if (!string.IsNullOrEmpty(txtSearch.Text))
                        dt = bu.GetBranchWareHouseTable(x => x.WHID != "0" && (x.WHID.Contains(txtSearch.Text) || x.WHName.Contains(txtSearch.Text)));
                    else
                        dt = bu.GetBranchWareHouseTable(x => x.WHID != "0"); // edit by sailom .k 03/03/2022 for support pre-order
                                                                             // && (x.WHType == 0 || x.WHType == 1)); //For remove whid = 0 last edit by sailom 11/10/2021
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtSearch.Text))
                    {
                        dt = bu.GetBranchWareHouseTable(x => x.WHID != "0" && x.WHType != 0 && (x.WHID.Contains(txtSearch.Text) || x.WHName.Contains(txtSearch.Text)));
                    }
                    else
                    {
                        dt = bu.GetBranchWareHouseTable(x => x.WHID != "0" && x.WHType != 0); // edit by sailom .k 03/03/2022 for support pre-order // && x.WHType == 1); //For remove whid = 0 last edit by sailom 11/10/2021
                    }
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        string _saletypeName = "";
                        string _saletypeID = r["SaleTypeID"].ToString();

                        switch (_saletypeID)
                        {
                            case "2":
                                _saletypeName = "A";
                                break;
                            case "3":
                                _saletypeName = "B";
                                break;
                            default:
                                _saletypeName = "ปกติ";
                                break;
                        }

                        dtBranchWH.Rows.Add(r["WHID"], r["WHName"], _saletypeName, r["BranchID"]);
                    }
                }

                grdList.DataSource = dtBranchWH;
                grdList.CreateCheckBoxHeaderColumn("colSelect");
                lblgridCount.Text = grdList.Rows.Count.ToNumberFormat();
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void frmSearchBrachWareHouseList_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;
           
            BindBranchWareHouse();
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

            frmProductSlip.allBranchWH = joinString;
            frmReport._txtBW = joinString;

            this.Close();
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void frmSearchBranchWareHouseList_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindBranchWareHouse();
            }
        }
    }
}
