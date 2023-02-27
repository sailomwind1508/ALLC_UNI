using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.Page;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmSearchCustomer : Form
    {
        Customer bu = new Customer();
        SaleArea buSaleArea = new SaleArea();
        public frmSearchCustomer()
        {
            InitializeComponent();
        }

        private void frmSearchCustomer_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;

            var allWH = new List<tbl_BranchWarehouse>();
            allWH.Add(new tbl_BranchWarehouse { WHID = "-1", WHCode = "==เลือก==" });
            allWH.AddRange(bu.GetAllBranchWarehouse(x => x.WHID.Length == 6));
            ddlWH.BindDropdownList(allWH, "WHCode", "WHID", 0);

            var allSalArea = new List<tbl_SalArea>();
            allSalArea.Add(new tbl_SalArea { SalAreaID = "-1", SalAreaName = "==เลือก==" });
            ddlSalArea.BindDropdownList(allSalArea, "SalAreaName", "SalAreaID", 0);

            var allShopType = new List<tbl_ShopType>();
            allShopType.Add(new tbl_ShopType { ShopTypeID = -1, ShopTypeName = "==เลือก==" });
            allShopType.AddRange(bu.GetAllShopType());
            ddlShopType.BindDropdownList(allShopType, "ShopTypeName", "ShopTypeID", 0);
        }

        private void SetGridView()
        {
            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                if (Convert.ToBoolean(grdList.Rows[i].Cells["colFlagMember"].Value) == true)
                    grdList.Rows[i].Cells["colFlagMember"].Value = true;

                var cells = grdList.Rows[i].Cells["colImageCustomer"];

                int _select = 0;
                bool flagColor = false;
                if (cells.Value != null)
                {
                    if (int.TryParse(cells.Value.ToString(), out _select))
                    {
                        if (_select == 1)
                            flagColor = true;
                    }
                }

                cells.Value = flagColor;
                cells.Style.BackColor = flagColor == true ? Color.LightGreen : Color.Red;

                var cellTelephone = grdList.Rows[i].Cells["colTelephone"];
                cellTelephone.Style.BackColor = cellTelephone.Value != null ? Color.LightGreen : Color.Red;

                var cellLatitude = grdList.Rows[i].Cells["colLatitude"];
                bool flagLatitude = false;
                if (cellLatitude.Value != null)
                {
                    if (cellLatitude.Value.ToString() == "0" || cellLatitude.Value.ToString() == "0.0" || cellLatitude.Value.ToString() == "")
                    { }
                    else
                        flagLatitude = true;
                }
                cellLatitude.Style.BackColor = flagLatitude == true ? Color.LightGreen : Color.Red;

                var cellLongitude = grdList.Rows[i].Cells["colLongitude"];
                bool flagLongitude = false;
                if (cellLongitude.Value != null)
                {
                    if (cellLongitude.Value.ToString() == "0" || cellLongitude.Value.ToString() == "0.0" || cellLongitude.Value.ToString() == "")
                    { }
                    else
                        flagLongitude = true;
                }
                cellLongitude.Style.BackColor = flagLongitude == true ? Color.LightGreen : Color.Red;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string WHID = ddlWH.SelectedIndex == 0 ? "" : ddlWH.SelectedValue.ToString();
                int shoptypeID = ddlShopType.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlShopType.SelectedValue);

                string SalAreaID = "";
                if (ddlSalArea.SelectedItem != null)
                    SalAreaID = ddlSalArea.SelectedValue.ToString() == "-1" ? "" : ddlSalArea.SelectedValue.ToString();

                Dictionary<string, object> _params = new Dictionary<string, object>();
                _params.Add("@FlagDel", rdoN.Checked ?  0 : 1);
                _params.Add("@WHID", WHID);
                _params.Add("@SalAreaID", SalAreaID);
                _params.Add("@ShopTypeID", shoptypeID);
                _params.Add("@Search", txtSearch.Text);

                var dtCustomer = new DataTable();
                dtCustomer = bu.GetCustomerData(_params);

                if (dtCustomer.Rows.Count > 0)
                    dtCustomer.Columns["Seq"].ReadOnly = false; //last edit by adisorn 02/02/2022

                DataTable newTable = new DataTable();
                newTable.Columns.Add("SelectCust", typeof(bool));
                newTable.Columns.Add("CustomerID", typeof(string));
                newTable.Columns.Add("CustName", typeof(string));
                newTable.Columns.Add("ImageCustomer", typeof(int));
                newTable.Columns.Add("ShopTypeName", typeof(string));
                newTable.Columns.Add("SalAreaID", typeof(string));
                newTable.Columns.Add("SalAreaName", typeof(string));
                newTable.Columns.Add("WHID", typeof(string));
                newTable.Columns.Add("Seq", typeof(int));
                newTable.Columns.Add("FlagMember", typeof(bool));
                newTable.Columns.Add("Latitude", typeof(string));
                newTable.Columns.Add("Longitude", typeof(string));
                newTable.Columns.Add("BillTo", typeof(string));
                newTable.Columns.Add("Telephone", typeof(string));

                foreach (DataRow r in dtCustomer.Rows)
                {
                    if (chkShelf.Checked)
                    {
                        if (!string.IsNullOrEmpty(r["ShelfID"].ToString()))
                        {
                            int _ImageCust = Convert.ToInt32(r["ImageCustomer"]);
                            newTable.Rows.Add(false, r["CustomerID"], r["CustName"], _ImageCust
                                , r["ShopTypeName"], r["SalAreaID"], r["SalAreaName"], r["WHID"], r["Seq"], r["FlagMember"]
                                , r["Latitude"], r["Longitude"], r["BillTo"], r["Telephone"]);
                        }
                    }
                    else
                    {
                        int _ImageCust = Convert.ToInt32(r["ImageCustomer"]);
                        newTable.Rows.Add(false, r["CustomerID"], r["CustName"], _ImageCust
                            , r["ShopTypeName"], r["SalAreaID"], r["SalAreaName"], r["WHID"], r["Seq"], r["FlagMember"]
                            , r["Latitude"], r["Longitude"], r["BillTo"], r["Telephone"]);
                    }
                }

                grdList.DataSource = newTable;
                lblrowqty.Text = newTable.Rows.Count.ToNumberFormat();
                grdList.CreateCheckBoxHeaderColumn("colSelectCust");
                SetGridView(); //SetFlagMember and ImageCustomer , set colour on row gridview
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch.PerformClick();
        }

        private void ddlWH_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string _whid = "";
                if (!string.IsNullOrEmpty(ddlWH.SelectedValue.ToString()) && ddlWH.SelectedValue.ToString() != "-1")
                    _whid = ddlWH.SelectedValue.ToString();

                Cursor.Current = Cursors.WaitCursor;

                var data = new List<tbl_SalArea>();
                data.Add(new tbl_SalArea {SalAreaID = "-1", SalAreaName = "==เลือก==" });

                if (!string.IsNullOrEmpty(_whid) && _whid.Length == 6)
                {
                    var listSalArea = buSaleArea.GetSalAreaByWHID(_whid);
                    if (listSalArea.Count == 24) //มีครบ 24 ตลาด
                    {
                        var listSaleArea = listSalArea.OrderBy(x => x.Seq).ToList();
                        data.AddRange(listSaleArea);
                    }
                    else
                    {
                        _whid = _whid.Substring(3, 3);
                        var SalAreaList = bu.GetSaleArea(x => x.SalAreaName.Contains(_whid) && x.Seq != 0);

                        for (int i = 0; i < listSalArea.Count; i++)  //20-06-2022 adisorn //ในกรณีที่ตลาด เปลี่ยนชื่อ 
                        {
                            var row = SalAreaList.Where(x => x.SalAreaID == listSalArea[i].SalAreaID).ToList();
                            if (row.Count == 0)
                                SalAreaList.Add(listSalArea[i]);
                        }

                        var _SalAreaID = SalAreaList.OrderBy(x => x.Seq).ToList();
                        data.AddRange(_SalAreaID);
                    }
                }

                ddlSalArea.BindDropdownList(data, "SalAreaName", "SalAreaID");

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ex.Message.ShowErrorMessage();
                Cursor.Current = Cursors.Default;
            }
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void frmSearchCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManagement.FlushMemory();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<string> selectList = new List<string>();
            
            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                if (Convert.ToBoolean(grdList.Rows[i].Cells[0].Value) == true)
                    selectList.Add(grdList.Rows[i].Cells[1].Value.ToString());
            }

            if (selectList.Count == 0)
            {
                frmReport._txtCustID = "";
                this.Close();
            }

            var joinString = string.Join(",", selectList);
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name.ToLower() == "frmReport".ToLower())
                    frmReport._txtCustID = joinString;
            }

            Cursor.Current = Cursors.Default;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (grdList.RowCount == 0)
            {
                frmReport._txtCustID = "";
            }
            this.Close();
        }
    }
}
