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
    public partial class frmSearchSupp : Form
    {
        //Supplier bu = new Supplier();
        DataTable dt = new DataTable();
        OD odBU = new OD();
        ObjectFactory objectFactory = new ObjectFactory();
        private ObjectType _objType;
        private string formName = "";
        private List<DataGridColumn> _gridColumn = new List<DataGridColumn>();
        private string formText = "";
        private int rowindex = -1;
        private static List<Control> controlList = new List<Control>();
        private static string[] conditionString = null;
        private static Func<tbl_BranchWarehouse, bool> predicate = null;
        private static Func<tbl_MstDistrict, bool> predicateDistrict = null;  //ดูให้ดี
        private static Func<tbl_Employee, bool> predicateEmp = null;
        private static Func<tbl_ArCustomer, bool> predicateCust = null;
        private static string DocTypeCode = "";

        List<Control> searchCustControls = new List<Control>();
        List<Control> searchBWHControls = new List<Control>();
        //private static Func<tbl_SalAreaDistrict, bool> predicateSAR = null;

        public frmSearchSupp()
        {
            InitializeComponent();

            predicate = null;
            predicateEmp = null;
            predicateDistrict = null;
            predicateCust = null;

            //searchCustControls = new List<Control> { txtCustomerCode, txtCustName };
            //searchBWHControls = new List<Control> { txtWHCode, txtWHName };
            //txtCustomerCode.KeyDown += TxtCustomerCode_KeyDown;
            //txtWHCode.KeyDown += TxtWHCode_KeyDown;

            //predicateSAR = null;
        }

        private void TxtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchCustControls = new List<Control> { txtCustomerCode, txtCustName };
                TextBox txt = (TextBox)sender;
                this.BindData("Customer", searchCustControls, txt.Text);
            }
        }

        private void TxtWHCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchBWHControls = new List<Control> { txtWHCode, txtWHName };
                TextBox txt = (TextBox)sender;
                this.BindData("BranchWarehouse", searchBWHControls, txt.Text);
            }
        }

        public void PreparePopupForm(string type, string frmName, string popUPText, List<DataGridColumn> gridColumn, int? _rowIndex = null, List<Control> _controls = null, string[] _conditionString = null, bool isHideExSearch = false)
        {
            DocTypeCode = type;
            PreparePopupFactory(type, frmName, popUPText, gridColumn, _rowIndex, _controls);
            conditionString = _conditionString;
            //predicate = null;
            //predicateEmp = null;
            //predicateSAR = null;

        }

        public void PreparePopupFormWithPredicate(string type, string frmName, string popUPText, List<DataGridColumn> gridColumn, int? _rowIndex = null, List<Control> _controls = null, Func<tbl_BranchWarehouse, bool> _predicate = null)
        {

            PreparePopupFactory(type, frmName, popUPText, gridColumn, _rowIndex, _controls);
            predicate = _predicate;
            //predicateEmp = null;
            //predicateSAR = null;
        }
        public void PreparePopupFormWithPredicate(string type, string frmName, string popUPText, List<DataGridColumn> gridColumn, int? _rowIndex = null, List<Control> _controls = null, Func<tbl_MstDistrict, bool> _predicate = null)
        {

            PreparePopupFactory(type, frmName, popUPText, gridColumn, _rowIndex, _controls);
            predicateDistrict = _predicate; // 

        }
        public void PreparePopupFormWithPredicate(string type, string frmName, string popUPText, List<DataGridColumn> gridColumn, int? _rowIndex = null, List<Control> _controls = null, Func<tbl_Employee, bool> _predicate = null)
        {

            PreparePopupFactory(type, frmName, popUPText, gridColumn, _rowIndex, _controls);
            predicateEmp = _predicate;
            //predicate = null;
            //predicateSAR = null;
        }
        public void PreparePopupFormWithPredicate(string type, string frmName, string popUPText, List<DataGridColumn> gridColumn, int? _rowIndex = null, List<Control> _controls = null, Func<tbl_ArCustomer, bool> _predicate = null, bool isHideExSearch = false)
        {
            PreparePopupFactory(type, frmName, popUPText, gridColumn, _rowIndex, _controls);
            predicateCust = _predicate;
            //predicate = null;
            //predicateSAR = null;
        }

        public void PreparePopupFormWithPredicate(string type, string frmName, string popUPText, List<DataGridColumn> gridColumn, int? _rowIndex = null, List<Control> _controls = null, Func<tbl_SalAreaDistrict, bool> _predicate = null)
        {

            PreparePopupFactory(type, frmName, popUPText, gridColumn, _rowIndex, _controls);
            //predicateSAR = _predicate;
            //predicate = null;
            //predicateEmp = null;
        }

        private void PreparePopupFactory(string type, string frmName, string popUPText, List<DataGridColumn> gridColumn, int? _rowIndex = null, List<Control> _controls = null)
        {
            switch (type)
            {
                case "BranchWarehouseID": { _objType = ObjectType.BranchWarehouseID; } break;
                case "Supplier": { _objType = ObjectType.Supplier; } break;
                case "SubDistict": { _objType = ObjectType.SubDistict; } break;
                case "Promotion": { _objType = ObjectType.Promotion; } break;
                case "PromotionTemp": { _objType = ObjectType.PromotionTemp; } break;
                case "ODProduct": { _objType = ObjectType.ODProduct; rowindex = _rowIndex.Value; } break;
                case "REProduct": { _objType = ObjectType.REProduct; rowindex = _rowIndex.Value; } break;
                case "RLProduct": { _objType = ObjectType.RLProduct; rowindex = _rowIndex.Value; } break;
                case "RBProduct": { _objType = ObjectType.RBProduct; rowindex = _rowIndex.Value; } break;
                case "RJProduct": { _objType = ObjectType.RJProduct; rowindex = _rowIndex.Value; } break;
                case "RTProduct": { _objType = ObjectType.RTProduct; rowindex = _rowIndex.Value; } break;
                case "TRProduct": { _objType = ObjectType.TRProduct; } break;
                case "CCProduct": { _objType = ObjectType.CCProduct; } break;
                case "IVProduct": { _objType = ObjectType.IVProduct; rowindex = _rowIndex.Value; } break;
                case "IMProduct": { _objType = ObjectType.IMProduct; rowindex = _rowIndex.Value; } break;
                case "VEProduct": { _objType = ObjectType.VEProduct; rowindex = _rowIndex.Value; } break;
                case "IVPreProduct": { _objType = ObjectType.IVPreProduct; rowindex = _rowIndex.Value; } break;
                case "IMPreProduct": { _objType = ObjectType.IMPreProduct; rowindex = _rowIndex.Value; } break;
                case "PreOrderProduct": { _objType = ObjectType.PreOrderProduct; rowindex = _rowIndex.Value; } break;
                case "OD": { _objType = ObjectType.OD; } break;
                case "RE": { _objType = ObjectType.RE; } break;
                case "RL": { _objType = ObjectType.RL; } break;
                case "REOD": { _objType = ObjectType.REOD; } break;
                case "RB": { _objType = ObjectType.RB; } break;
                case "RJ": { _objType = ObjectType.RJ; } break;
                case "RT": { _objType = ObjectType.RT; } break;
                case "TR": { _objType = ObjectType.TR; } break;
                case "RJRB": { _objType = ObjectType.RJRB; } break;
                case "BranchWarehouse": { _objType = ObjectType.BranchWarehouse; } break;
                case "FromBranchID": { _objType = ObjectType.FromBranchID; } break;
                case "Employee": { _objType = ObjectType.Employee; } break;
                case "EmployeeName": { _objType = ObjectType.EmployeeName; } break;
                case "Customer": { _objType = ObjectType.Customer; } break;
                case "CustomerPre": { _objType = ObjectType.CustomerPre; } break;
                case "SaleAreaDistrict": { _objType = ObjectType.SaleAreaDistrict; } break;
                case "IM": { _objType = ObjectType.IM; } break;
                case "IV": { _objType = ObjectType.IV; } break;
                case "CCIV": { _objType = ObjectType.CCIV; } break;
                case "IVPre": { _objType = ObjectType.IVPre; } break;
                case "IVPreRB": { _objType = ObjectType.IVPreRB; } break;
                case "IMPre": { _objType = ObjectType.IMPre; } break;
                case "PreOrder": { _objType = ObjectType.PreOrder; } break;
                case "IVPrePO": { _objType = ObjectType.IVPrePO; } break;
                case "V": { _objType = ObjectType.V; } break;
                default:
                    break;
            }

            formName = frmName;
            formText = popUPText;
            _gridColumn = gridColumn;

            controlList = _controls;
        }

        private void frmSearchSupp_Load(object sender, EventArgs e)
        {
            this.Text = formText;
            var obj = objectFactory.Get(_objType, null);

            if (predicate != null)
            {
                BranchWarehouse bwh = obj as BranchWarehouse;
                dt = bwh.GetDataTableByCondition(predicate);
            }
            else if (predicateEmp != null)
            {
                Employee emp = obj as Employee;
                dt = emp.GetDataTableByCondition(predicateEmp);
            }
            else if (predicateCust != null)
            {
                Customer c = obj as Customer;
                dt = c.GetDataTableByCondition(predicateCust);
            }
            else if (predicateDistrict != null)
            {
                SubDistict pro = obj as SubDistict;
                dt = pro.GetDataTableByCondition(predicateDistrict);
            }
            else if (_objType == ObjectType.CustomerPre)
            {
                Customer c = obj as Customer;
                dt = c.GetDataTableByCondition(predicateCust);
            }
            else if (_objType == ObjectType.IVPrePO)
            {
                IVPre c = obj as IVPre;
                c = new IVPre();
                dt = c.GetDataTableByCondition(null);
            }
            //else if (predicateSAR != null)
            //{
            //    SaleAreaDistrict s = obj as SaleAreaDistrict;
            //    dt = s.GetDataTableByCondition(predicateSAR);
            //}
            else
            {
                //edit by sailom .k 14/12/2021-----------------------------------
                if (_objType == ObjectType.IV || _objType == ObjectType.IVPre || _objType == ObjectType.IVPreRB)
                {
                    if (conditionString != null)
                    {
                        conditionString = new string[] { "19000101" };
                        dt = obj.GetDataTableByCondition(conditionString);
                    }
                    else
                        dt = obj.GetDataTableByCondition(null);
                }//edit by sailom .k 14/12/2021-----------------------------------
                else
                {
                    dt = obj.GetDataTableByCondition(conditionString);
                }

                searchCustControls = new List<Control> { txtCustomerCode, txtCustName };
                searchBWHControls = new List<Control> { txtWHCode, txtWHName };

                if (txtWHCode != null)
                    txtWHCode.KeyDown += TxtWHCode_KeyDown;

                if (txtCustomerCode != null)
                    txtCustomerCode.KeyDown += TxtCustomerCode_KeyDown;
            }

            foreach (DataGridColumn item in _gridColumn)
            {
                grdList.Columns.Add(item.SetSearchDataGridViewProperties());
            }

            if (_gridColumn[0].AddNumberInFirstRow)
            {
                //grdList.RowPostPaint += gridView_RowPostPaint;
                grdList.RowHeadersVisible = true;

                dtpDocDate.SetDateTimePickerFormat();

                var allDocStatus = new List<tbl_DocumentStatus>();
                allDocStatus.Add(new tbl_DocumentStatus { DocStatusCode = "-1", DocStatusName = "==เลือก==" });

                if (_objType == ObjectType.RL || _objType == ObjectType.PreOrder)
                    allDocStatus.AddRange(odBU.GetDocStatus().Where(x => x.DocStatusCode == "3" || x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList());
                else
                    allDocStatus.AddRange(odBU.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList());

                Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == "4"; };
                ddlDocStatus.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);

                if (_objType == ObjectType.PreOrder)
                    condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == "3"; };// last edit by sailom 08-06-2021

                ddlDocStatus.SelectedValueDropdownList(condition);

                pnlAdcSearch.Visible = true;
                pnlAdcSearch.Show();

                //if (_gridColumn[0].AddSearchAddOn)
                //{
                //    lnkSearchAddOn.Visible = true;
                //    lnkSearchAddOn.Show();
                //}
            }
            else
            {
                //grdList.RowPostPaint -= gridView_RowPostPaint;
                grdList.RowHeadersVisible = false;
            }

            if (_gridColumn[0].AddSearchAddOn)
            {

                pnlAdcSearch.Hide();
                pnlAdcSearch.Visible = true;

                lnkSearchAddOn.Hide();
                lnkSearchAddOn.Visible = true;
            }
            else
            {
                pnlAdcSearch.Hide();
                pnlAdcSearch.Visible = false;

                lnkSearchAddOn.Hide();
                lnkSearchAddOn.Visible = false;
            }

            //BindDataGrid(dt);
            //if (_objType != ObjectType.IV)
            //btnSearchSupp.PerformClick();
            Search();
            this.ActiveControl = txtSSuppCode;
        }

        private void gridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //var grid = sender as DataGridView;
            //var rowIdx = (e.RowIndex + 1).ToString();

            //var centerFormat = new StringFormat()
            //{
            //    // right alignment might actually make more sense for numbers
            //    Alignment = StringAlignment.Center,
            //    LineAlignment = StringAlignment.Center
            //};

            //var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            //e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);

            //grid.RowsDefaultCellStyle.BackColor = Color.White;
            //grid.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            //grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(69, 171, 213);
            //grid.EnableHeadersVisualStyles = false;

            grdList.SetRowPostPaint(sender, e, this.Font);

            //for support pre-order---------------------------------------
            if (DocTypeCode == "PreOrder")
            {
                try
                {
                    var row = grdList.Rows[e.RowIndex];

                    var _edDate = row.Cells["EdDate"].Value.ToString();
                    var _edUser = row.Cells["EdUser"].Value.ToString();

                    if (!string.IsNullOrEmpty(_edDate) && !string.IsNullOrEmpty(_edUser))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                }
                catch { }   
            }
        }

        private void BindDataGrid(DataTable _dt)
        {
            grdList.AutoGenerateColumns = false;
            grdList.DataSource = _dt;

            for (int i = 0; i < grdList.Columns.Count; i++)
            {
                grdList.Columns[i].DefaultCellStyle.Font = new Font("Tahoma", 9.5F);
                grdList.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            //grdList.DataBindingComplete += (o, e) =>
            // {
            //     foreach (DataGridViewRow row in grdList.Rows)
            //         row.HeaderCell.Value = (row.Index + 1).ToString();
            // };

            lblCountList.Text = _dt.Rows.Count.ToNumberFormat();
        }

        private void Search()
        {
            DataTable _dt = new DataTable();
            _dt = dt.Clone();
            DataRow[] filteredRows = null;

            try
            {
                string searchText = txtSSuppCode.Text.ToUpper();
                SelectItem(searchText, ref _dt, ref filteredRows);

                //if (_objType == ObjectType.IV) //for IV
                //{
                //    if (filteredRows == null || filteredRows.Count() == 0)
                //    {
                //        var obj = objectFactory.Get(_objType, null);
                //        dt = obj.GetDataTableByCondition(conditionString);
                //        BindDataGrid(dt);
                //    }
                //    else
                //        BindDataGrid(_dt);
                //}
                //else
                {
                    if (filteredRows == null)
                        BindDataGrid(dt);
                    else
                        BindDataGrid(_dt);
                }

                txtSSuppCode.Focus();
            }
            catch (Exception ex)
            {
                BindDataGrid(_dt);
            }
        }

        private void btnSearchSupp_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void SelectItem(string text, ref DataTable _dt, ref DataRow[] filteredRows)
        {
            switch (_objType)
            {
                case ObjectType.BranchWarehouseID: SubSelectProductItem(text, ref _dt, ref filteredRows, "WHID", "WHName"); break;
                case ObjectType.Supplier: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.SubDistict: SubSelectProductItem(text, ref _dt, ref filteredRows, "DistrictCode", "DistrictName", "DistrictID"); break;
                case ObjectType.Promotion: SubSelectProductItem(text, ref _dt, ref filteredRows, "PromotionID", "PromotionName"); break;
                case ObjectType.PromotionTemp: SubSelectProductItem(text, ref _dt, ref filteredRows, "PromotionID", "PromotionName"); break;
                case ObjectType.ODProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.REProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.RLProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.RBProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.RJProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.RTProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.TRProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.CCProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.IVProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.IMProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.VEProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.IVPreProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.IMPreProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.PreOrderProduct: SubSelectProductItem(text, ref _dt, ref filteredRows, "ProductID", "ProductName"); break;
                case ObjectType.OD: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.RE: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.REOD: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.RL: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.RB: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.RJ: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.RT: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.TR: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.RJRB: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.BranchWarehouse: SubSelectItem(text, ref _dt, ref filteredRows, "WHCode", "WHName", null); break;
                case ObjectType.FromBranchID: SubSelectItem(text, ref _dt, ref filteredRows, "BranchCode", "BranchName", null); break;
                case ObjectType.Employee: SubSelectItem(text, ref _dt, ref filteredRows, "EmpCode", "EmpName", null); break;
                case ObjectType.EmployeeName: SubSelectItem(text, ref _dt, ref filteredRows, "EmpCode", "EmpName", null); break;
                case ObjectType.Customer: SubSelectItem(text, ref _dt, ref filteredRows, "CustomerID", "CustName", "SalAreaName", "WHID"); break;
                case ObjectType.CustomerPre: SubSelectItem(text, ref _dt, ref filteredRows, "CustomerID", "CustName", "SalAreaName", "WHID"); break;
                case ObjectType.SaleAreaDistrict: SubSelectProductItem(text, ref _dt, ref filteredRows, "DistrictCode", "DistrictName", "DistrictID"); break;
                case ObjectType.IM: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.IV: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.CCIV: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.IVPre: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.IVPreRB: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.IMPre: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.PreOrder: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.IVPrePO: FilterItem(text, ref _dt, ref filteredRows); break;
                case ObjectType.V: FilterItem(text, ref _dt, ref filteredRows); break;
                default:
                    break;
            }
        }

        private void SubSelectProductItem(string text, ref DataTable _dt, ref DataRow[] filteredRows, string id = null, string name = null, string name2 = null)
        {
            SubSelectItem(text, ref _dt, ref filteredRows, id, name, name2);
        }

        private void SubSelectItem(string text, ref DataTable _dt, ref DataRow[] filteredRows, string code, string name, string name2)
        {
            if (!string.IsNullOrEmpty(text))
            {
                filteredRows = dt.Select(string.Format("{0} LIKE '%{1}%' OR {2} LIKE '%{3}%'", code, text, name, text));
                if (filteredRows != null)
                {
                    _dt.AddDataTableRow(ref filteredRows);
                }
            }
        }

        private void SubSelectItem(string text, ref DataTable _dt, ref DataRow[] filteredRows, string code, string name, string saleArea, string whid)
        {
            List<DataRow> filteredRowsTemp = new List<DataRow>();
            if (!string.IsNullOrEmpty(text))
            {
                var tmp = string.Format("({0} LIKE '%{1}%' OR {2} LIKE '%{3}%' OR {4} LIKE '%{5}%' OR {6} LIKE '%{7}%') AND " + "{8} = " + (string.IsNullOrEmpty(txtWHCode.Text) ? "{8}" : " '{9}' ") + " AND {10} = " + (string.IsNullOrEmpty(txtCustomerCode.Text) ? "{10}" : " '{11}' ")
                    , code, text, name, text, saleArea, text, whid, text, whid, txtWHCode.Text, code, txtCustomerCode.Text);
                filteredRows = dt.Select(tmp);
            }
            else
            {
                var tmp = string.Format("{0} = " + (string.IsNullOrEmpty(txtWHCode.Text) ? "{0}" : " '{1}' ") + " AND {2} = " + (string.IsNullOrEmpty(txtCustomerCode.Text) ? "{2}" : " '{3}' "), whid, txtWHCode.Text, code, txtCustomerCode.Text);
                filteredRows = dt.Select(tmp);
            }

            if (filteredRows != null)
            {
                _dt.AddDataTableRow(ref filteredRows);
            }
        }

        private void FilterItem(string text, ref DataTable _dt, ref DataRow[] filteredRows)
        {
            DateTime docDate = Convert.ToDateTime(dtpDocDate.Value.ToShortDateString());
            string docStatus = ((tbl_DocumentStatus)ddlDocStatus.SelectedItem).DocStatusName;

            if (!string.IsNullOrEmpty(text))
            {
                if (dtpDocDate.Enabled && ddlDocStatus.SelectedIndex != 0)
                {
                    filteredRows = dt.AsEnumerable().Where(x => x.Field<string>("DocNo").Contains(text) && x.Field<DateTime>("DocDate").ToShortDateString() == docDate.ToShortDateString() && x.Field<string>("DocStatus") == docStatus).ToArray();
                }
                else if (dtpDocDate.Enabled)
                {
                    filteredRows = dt.AsEnumerable().Where(x => x.Field<string>("DocNo").Contains(text) && x.Field<DateTime>("DocDate").ToShortDateString() == docDate.ToShortDateString()).ToArray();
                }
                else if (ddlDocStatus.SelectedIndex != 0)
                {
                    //last edit by sailom 11-06-2021---------------------------------------------------------
                    filteredRows = dt.AsEnumerable().Where(x => (
                    x.Field<string>("DocNo").Contains(text) &&
                    x.Field<string>("DocStatus") == docStatus)).ToArray();

                    if (filteredRows.Count() == 0)
                    {
                        filteredRows = dt.AsEnumerable().Where(x => (
                        (x.Field<string>("CustomerID") != null && x.Field<string>("CustomerID").Contains(text)) &&
                        x.Field<string>("DocStatus") == docStatus)).ToArray();
                    }
                    if (filteredRows.Count() == 0)
                    {
                        filteredRows = dt.AsEnumerable().Where(x => (
                        (x.Field<string>("CustomerName") != null && x.Field<string>("CustomerName").Contains(text)) &&
                        x.Field<string>("DocStatus") == docStatus)).ToArray();
                    }
                    if (filteredRows.Count() == 0)
                    {
                        filteredRows = dt.AsEnumerable().Where(x => (
                        (x.Field<string>("WHID") != null && x.Field<string>("WHID").Contains(text)) &&
                        x.Field<string>("DocStatus") == docStatus)).ToArray();
                    }
                    //last edit by sailom 11-06-2021---------------------------------------------------------
                }
            }
            else
            {
                if (dtpDocDate.Enabled && ddlDocStatus.SelectedIndex != 0)
                {
                    filteredRows = dt.AsEnumerable().Where(x => x.Field<DateTime>("DocDate").ToShortDateString() == docDate.ToShortDateString() && x.Field<string>("DocStatus") == docStatus).ToArray();
                }
                else if (dtpDocDate.Enabled)
                {
                    filteredRows = dt.AsEnumerable().Where(x => x.Field<DateTime>("DocDate").ToShortDateString() == docDate.ToShortDateString()).ToArray();
                }
                else if (ddlDocStatus.SelectedIndex != 0)
                {
                    filteredRows = dt.AsEnumerable().Where(x => x.Field<string>("DocStatus") == docStatus).ToArray();
                }
            }

            if (filteredRows != null)
            {
                if ((txtWHCode.Visible && !string.IsNullOrEmpty(txtWHCode.Text)) ||
                    (txtCustomerCode.Visible && !string.IsNullOrEmpty(txtCustomerCode.Text)))
                {
                    _dt.AddDataTableRow(ref filteredRows);

                    var tempDT = new DataTable();
                    tempDT = SubFilterItem(_dt);
                    if (tempDT.Rows.Count > 0)
                    {
                        List<DataRow> tempRowList = new List<DataRow>();
                        foreach (DataRow r in tempDT.Rows)
                        {
                            tempRowList.Add(r);
                        }

                        DataRow[] rowList = tempRowList.ToArray();

                        _dt = new DataTable();
                        _dt = tempDT.Clone();
                        _dt.AddDataTableRow(ref rowList);
                    }
                    else
                    {
                        _dt = _dt.Clone();
                    }
                }
                else
                    _dt.AddDataTableRow(ref filteredRows);
            }
        }

        private DataTable SubFilterItem(DataTable _dt)
        {
            DataTable ret_dt = new DataTable();
            ret_dt = _dt.Clone();

            DataRow[] filteredRows = null;

            DateTime docDate = Convert.ToDateTime(dtpDocDate.Value.ToShortDateString());
            string docStatus = ((tbl_DocumentStatus)ddlDocStatus.SelectedItem).DocStatusName;
            bool verifyCondition = false;

            if (txtWHCode != null && !string.IsNullOrEmpty(txtWHCode.Text))
            {
                filteredRows = _dt.AsEnumerable().Where(x => x.Field<string>("WHID") == txtWHCode.Text).ToArray();

                verifyCondition = true;
            }

            if (txtCustomerCode != null && !string.IsNullOrEmpty(txtCustomerCode.Text))
            {
                filteredRows = _dt.AsEnumerable().Where(x => x.Field<string>("CustomerID") == txtCustomerCode.Text).ToArray();

                verifyCondition = true;
            }

            if (txtWHCode != null && txtCustomerCode != null && !string.IsNullOrEmpty(txtWHCode.Text) && !string.IsNullOrEmpty(txtCustomerCode.Text)) ////////////////////////////////////
            {
                filteredRows = _dt.AsEnumerable().Where(x => x.Field<string>("WHID") == txtWHCode.Text && x.Field<string>("CustomerID") == txtCustomerCode.Text).ToArray();

                verifyCondition = true;
            }

            if (verifyCondition && filteredRows != null)
                ret_dt.AddDataTableRow(ref filteredRows);
            else
                ret_dt = _dt;

            return ret_dt;
        }

        private void txtSSuppCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchSupp.PerformClick();
            }
        }

        private void grdList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grd = (DataGridView)sender;
            if (grd != null & grd.Rows.Count > 0)
            {
                if (e.RowIndex >= 0)
                {
                    var selectRow = grd.Rows[e.RowIndex];

                    string selectCode = "";
                    selectCode = selectRow.Cells[0].Value.ToString();

                    if (!string.IsNullOrEmpty(selectCode))
                    {
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f.Name.ToLower() == formName.ToLower())
                            {
                                Form frm = (Form)f;

                                switch (_objType)
                                {
                                    case ObjectType.BranchWarehouseID: frm.BindData("BranchWarehouseID", controlList, selectCode); break;
                                    case ObjectType.Supplier: frm.BindData("Supplier", controlList, selectCode); break;
                                    case ObjectType.SubDistict: frm.BindData("SubDistict", controlList, selectCode); break;
                                    case ObjectType.Promotion: frm.BindData("Promotion", controlList, selectCode); break;
                                    case ObjectType.PromotionTemp: frm.BindData("PromotionTemp", controlList, selectRow.Cells["PromotionID"].Value.ToString()); break;
                                    case ObjectType.ODProduct: BindProduct(frm, _objType, selectCode); break;
                                    case ObjectType.REProduct: BindProduct(frm, _objType, selectCode); break;
                                    case ObjectType.RLProduct: BindProduct(frm, _objType, selectCode); break;
                                    case ObjectType.RBProduct: BindProduct(frm, _objType, selectCode); break;
                                    case ObjectType.RJProduct: BindProduct(frm, _objType, selectCode); break;
                                    case ObjectType.RTProduct: BindProduct(frm, _objType, selectCode); break;
                                    case ObjectType.TRProduct: frm.BindData("TRProduct", controlList, selectCode); break;
                                    case ObjectType.CCProduct: frm.BindData("CCProduct", controlList, selectCode); break;
                                    case ObjectType.IVProduct: BindProduct(frm, _objType, selectCode); break;
                                    case ObjectType.IMProduct: BindProduct(frm, _objType, selectCode); break;
                                    case ObjectType.VEProduct: BindProduct(frm, _objType, selectCode); break;
                                    case ObjectType.IVPreProduct: BindProduct(frm, _objType, selectCode); break;
                                    case ObjectType.IMPreProduct: BindProduct(frm, _objType, selectCode); break;
                                    case ObjectType.PreOrderProduct: BindProduct(frm, _objType, selectCode); break;
                                    case ObjectType.OD: ((frmOD)frm).BindODData(selectCode); break;
                                    case ObjectType.RE: ((frmRE)frm).BindREData(selectCode); break;
                                    case ObjectType.REOD: ((frmRE)frm).BindODData(selectCode); break;
                                    case ObjectType.RL: ((frmRL)frm).BindRLData(selectCode); break;
                                    case ObjectType.RB: ((frmRB)frm).BindRBData(selectCode); break;
                                    case ObjectType.RJ: ((frmRJ)frm).BindRJData(selectCode); break;
                                    case ObjectType.RT: ((frmRT)frm).BindRTData(selectCode); break;
                                    case ObjectType.TR: ((frmTR)frm).BindTRData(selectCode); break;
                                    case ObjectType.RJRB: ((frmRJ)frm).BindRBData(selectCode); break;
                                    case ObjectType.IM: ((frmVanSales)frm).BindVanSalesData(selectCode); break;
                                    case ObjectType.IV: ((frmTabletSales)frm).BindTabletSalesData(selectCode); break;
                                    case ObjectType.CCIV: ((frmCancelPOItem)frm).BindTabletSalesData(selectCode); break;
                                    case ObjectType.IVPre: ((frmTabletSalesPre)frm).BindTabletSalesData(selectCode); break;
                                    case ObjectType.IVPreRB: ((frmRB)frm).BindRBFromPOData(selectCode, "IV"); break;
                                    case ObjectType.IMPre: ((frm1000SalesPre)frm).BindVanSalesData(selectCode); break;
                                    case ObjectType.PreOrder: ((frmPreOrder)frm).BindVanSalesData(selectCode, "IV2"); break;
                                    case ObjectType.IVPrePO: ((frmPreOrder)frm).BindVanSalesPOData(selectCode); break;
                                    case ObjectType.V: ((frmVE)frm).BindVEData(selectCode); break;
                                    case ObjectType.BranchWarehouse: frm.BindData("BranchWarehouse", controlList, selectCode); break;
                                    case ObjectType.FromBranchID: frm.BindData("FromBranchID", controlList, selectCode); break;
                                    case ObjectType.Employee: frm.BindData("Employee", controlList, selectCode); break;
                                    case ObjectType.EmployeeName: frm.BindData("EmployeeName", controlList, selectCode); break;
                                    case ObjectType.Customer: frm.BindData("Customer", controlList, selectCode); break;
                                    case ObjectType.CustomerPre: frm.BindData("Customer", controlList, selectCode); break;
                                    case ObjectType.SaleAreaDistrict: BindProduct(frm, _objType, selectRow.Cells[2].Value.ToString()); break;
                                    default:
                                        break;
                                }

                                this.Close();
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void BindProduct(Form frm, ObjectType type, string selectCode)
        {
            DataTable _dt = new DataTable();
            _dt = dt.Clone();
            DataRow[] filteredRows = null;
            SelectItem(selectCode, ref _dt, ref filteredRows);

            if (type == ObjectType.ODProduct)
            {
                ((frmOD)frm).BindSearchProduct(_dt, rowindex);
            }
            else if (type == ObjectType.REProduct)
            {
                ((frmRE)frm).BindSearchProduct(_dt, rowindex);
            }
            else if (type == ObjectType.RLProduct)
            {
                ((frmRL)frm).BindSearchProduct(_dt, rowindex);
            }
            else if (type == ObjectType.RBProduct)
            {
                ((frmRB)frm).BindSearchProduct(_dt, rowindex);
            }
            else if (type == ObjectType.RJProduct)
            {
                ((frmRJ)frm).BindSearchProduct(_dt, rowindex);
            }
            else if (type == ObjectType.RTProduct)
            {
                ((frmRT)frm).BindSearchProduct(_dt, rowindex);
            }
            else if (type == ObjectType.SaleAreaDistrict)
            {
                ((frmDistributionCenter)frm).AddSaleAreaDistrictRow(_dt);
            }
            else if (type == ObjectType.IVProduct)
            {
                ((frmTabletSales)frm).BindSearchProduct(_dt, rowindex);
            }
            else if (type == ObjectType.IVPreProduct)
            {
                ((frmTabletSalesPre)frm).BindSearchProduct(_dt, rowindex);
            }
            else if (type == ObjectType.IMProduct)
            {
                ((frmVanSales)frm).BindSearchProduct(_dt, rowindex);
            }
            else if (type == ObjectType.IMPreProduct)
            {
                ((frm1000SalesPre)frm).BindSearchProduct(_dt, rowindex);
            }
            else if (type == ObjectType.PreOrderProduct)
            {
                ((frmPreOrder)frm).BindSearchProduct(_dt, rowindex);
            }
            else if (type == ObjectType.VEProduct)
            {
                ((frmVE)frm).BindSearchProduct(_dt, rowindex);
            }
        }

        private void rdoY_CheckedChanged(object sender, EventArgs e)
        {
            dtpDocDate.Enabled = false;
        }

        private void rdoN_CheckedChanged(object sender, EventArgs e)
        {
            dtpDocDate.Enabled = true;
        }

        private void lnkSearchAddOn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!pnlSearchAddOn.Visible)
            {
                pnlSearchAddOn.Visible = true;
                pnlSearchAddOn.Show();
            }
            else
            {
                pnlSearchAddOn.Hide();
                pnlSearchAddOn.Visible = false;
            }
        }

        private void btnSearchWHCode_Click(object sender, EventArgs e)
        {
            searchBWHControls = new List<Control> { txtWHCode, txtWHName };
            this.OpenBranchWarehousePopup(searchBWHControls, "เลือกคลังสินค้า");
        }

        private void btnSearchCust_Click(object sender, EventArgs e)
        {
            searchCustControls = new List<Control> { txtCustomerCode, txtCustName };
            this.OpenCustomerPopup(searchCustControls, "เลือกลูกค้า", null);
        }

        private void grdList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (grdList.Columns[e.ColumnIndex].Name == "DocStatusImg")
            {
                var rowIdx = e.RowIndex;
                var docStatus = grdList.Rows[rowIdx].Cells["DocStatus"].Value;

                if (grdList.Rows[e.RowIndex].Cells["DocStatus"].Value != null && !string.IsNullOrEmpty(docStatus.ToString()))
                {
                    if (_objType == ObjectType.RL)
                    {
                        Bitmap closeImg = new Bitmap(Properties.Resources.power_off);
                        Bitmap cancelmg = new Bitmap(Properties.Resources.closeBtn);
                        Bitmap inProcessmg = new Bitmap(Properties.Resources.timeBtn);
                        Bitmap statusImg = null;
                        if (docStatus.ToString() == "Closed")
                        {
                            statusImg = closeImg;
                        }
                        else if (docStatus.ToString() == "Cancelled")
                        {
                            statusImg = cancelmg;
                        }
                        else if (docStatus.ToString() == "In Process")
                        {
                            statusImg = inProcessmg;
                        }
                        if (statusImg != null)
                        {
                            // Your code would go here - below is just the code I used to test 
                            e.Value = statusImg;
                        }
                    }
                    else
                    {
                        Bitmap closeImg = new Bitmap(Properties.Resources.power_off);
                        Bitmap cancelmg = new Bitmap(Properties.Resources.closeBtn);
                        Bitmap statusImg = docStatus.ToString() == "Closed" ? closeImg : cancelmg;
                        if (statusImg != null)
                        {
                            // Your code would go here - below is just the code I used to test 
                            e.Value = statusImg;
                        }
                    }
                }
            }
        }
    }
}
