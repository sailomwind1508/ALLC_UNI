using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace AllCashUFormsApp
{
    public static class GridViewHelper
    {
        static CheckBox headerCheckBox = new CheckBox();
        static DataGridView grdList = null;
        static string CheckBoxColName = "";

        //public static void CreateCheckBoxHeaderColumn(this DataGridView grd, string colName)
        //{
        //    try
        //    {
        //        grdList = grd;
        //        CheckBoxColName = colName;

        //        //Add a CheckBox Column to the DataGridView Header Cell.

        //        //Find the Location of Header Cell.
        //        Point headerCellLocation = grdList.GetCellDisplayRectangle(0, -1, true).Location;

        //        //Place the Header CheckBox in the Location of the Header Cell.
        //        headerCheckBox.Location = new Point(headerCellLocation.X + 8, headerCellLocation.Y + 2);
        //        headerCheckBox.BackColor = Color.White;
        //        headerCheckBox.Size = new Size(18, 18);

        //        //Assign Click event to the Header CheckBox.
        //        headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);
        //        grdList.Controls.Add(headerCheckBox);
        //        //grdList.CurrentCell = grdList.Rows[0].Cells[0];

        //        //Assign Click event to the DataGridView Cell.
        //        grdList.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellClick);
        //    }
        //    catch
        //    {


        //    }

        //}

        public static void CreateCheckBoxHeaderColumn(this DataGridView grd, string colName)
        {
            try
            {
                grdList = null;
                grdList = grd;

                CheckBoxColName = colName;

                //Add a CheckBox Column to the DataGridView Header Cell.

                //Find the Location of Header Cell.
                Point headerCellLocation = grd.GetCellDisplayRectangle(0, -1, true).Location;
                //headerCheckBox = new CheckBox();
                //Place the Header CheckBox in the Location of the Header Cell.
                headerCheckBox.Location = new Point(headerCellLocation.X + 8, headerCellLocation.Y + 2);
                headerCheckBox.BackColor = Color.White;
                headerCheckBox.Size = new Size(18, 18);

                headerCheckBox.Checked = false; // uncheck 

                //Assign Click event to the Header CheckBox.
                headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);
                grd.Controls.Add(headerCheckBox);
                //grdList.CurrentCell = grdList.Rows[0].Cells[0];

                //Assign Click event to the DataGridView Cell.
                grd.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellClick);
            }
            catch
            {
           
            }
        }

        private static void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            ////Necessary to end the edit mode of the Cell.
            ////var grd = (DataGridView)sender;
            //if (grdList.Name == "grdPO")
            //{
            //    CheckBoxColName = "colSelectRowPO";
            //}
            //else if (grdList.Name == "grdPOMst")
            //{
            //    CheckBoxColName = "colSelectRow";
            //}
            //else
            //{
            //    CheckBoxColName = "colSelect";
            //}

            grdList.EndEdit();

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grdList.Rows)
            {
                //try
                //{
                //    DataGridViewCheckBoxCell checkBox = (row.Cells[CheckBoxColName] as DataGridViewCheckBoxCell);
                //    checkBox.Value = headerCheckBox.Checked;
                //}
                //catch
                {
                    DataGridViewCheckBoxCell checkBox = (row.Cells[CheckBoxColName] as DataGridViewCheckBoxCell);
                    checkBox.Value = headerCheckBox.Checked;
                }
            }

            grdList.CurrentCell = null;
            grdList.RefreshEdit();
            grdList.Refresh();
        }

        private static void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var grd = (DataGridView)sender;
            //if (grd.Name == "grdPO")
            //{
            //    CheckBoxColName = "colSelectRowPO";
            //}
            //else if (grd.Name == "grdPOMst")
            //{
            //    CheckBoxColName = "colSelectRow";
            //}

            //Check to ensure that the row CheckBox is clicked.
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                bool isChecked = true;
                foreach (DataGridViewRow row in grd.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[CheckBoxColName].EditedFormattedValue) == false)
                    {
                        isChecked = false;
                        break;
                    }
                }
                headerCheckBox.Checked = isChecked;
            }
        }


        public static DataGridViewColumn SetSearchDataGridViewProperties(this DataGridColumn item)
        {
            DataGridViewColumn col = null;
            col = item.ColoumnType != null ? item.ColoumnType : new DataGridViewTextBoxColumn();
            col.DataPropertyName = item.DataPropertyName;
            col.Name = item.Name;
            col.HeaderText = item.HeaderText;
            col.Width = item.Width;
            col.AutoSizeMode = item.AutoSizeColumnMode;
            if (item.Visibility != null && item.Visibility.Value != true)
            {
                col.Visible = false;
            }
            else
            {
                col.Visible = true;
            }

            if (item.AutoSizeColumnMode == DataGridViewAutoSizeColumnMode.Fill)
            {
                col.MinimumWidth = item.Width;
            }
            col.ReadOnly = true;
            col.DefaultCellStyle.Alignment = item.Alignment;// DataGridViewContentAlignment.MiddleRight;
            col.DefaultCellStyle.Format = item.Format;

            return col;
        }

        public static void SetDataGridViewStyle(this DataGridView grd)
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            grd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            grd.DefaultCellStyle = dataGridViewCellStyle2;

            grd.RowsDefaultCellStyle.BackColor = Color.White;
            grd.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
        }

        public static void AddDataTableRow(this DataTable _dt, ref DataRow[] filteredRows)
        {
            foreach (var row in filteredRows.Distinct())
            {
                List<object> data = new List<object>();
                for (int i = 0; i < row.ItemArray.Count(); i++)
                {
                    data.Add(row[i]);
                }
                _dt.Rows.Add(data.ToArray());
            }
        }

        public static bool IsNotNullOrEmptyCell(this DataGridViewCell cell)
        {
            try
            {
                return (cell != null && cell.EditedFormattedValue != null && !string.IsNullOrEmpty(cell.EditedFormattedValue.ToString()));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidateCode(this DataGridView grd, DataTable dt, int rowIndex, int validateCellIndex, string objType, bool isLike = true)
        {
            bool ret = true;
            if (grd != null && grd.CurrentRow != null)
            {
                ObjectFactory objectFactory = new ObjectFactory();
                ObjectType _objType = new ObjectType();
                switch (objType)
                {
                    case "ODProduct": { _objType = ObjectType.ODProduct; } break;
                    case "REProduct": { _objType = ObjectType.REProduct; } break;
                    case "RLProduct": { _objType = ObjectType.RLProduct; } break;
                    case "RBProduct": { _objType = ObjectType.RBProduct; } break;
                    case "RJProduct": { _objType = ObjectType.RJProduct; } break;
                    case "RTProduct": { _objType = ObjectType.RTProduct; } break;
                    case "IMProduct": { _objType = ObjectType.IMProduct; } break;
                    case "IVProduct": { _objType = ObjectType.IVProduct; } break;
                    case "VEProduct": { _objType = ObjectType.VEProduct; } break;
                    case "IVPreProduct": { _objType = ObjectType.IVPreProduct; } break;
                    case "IMPreProduct": { _objType = ObjectType.IMPreProduct; } break;
                    case "PreOrderProduct": { _objType = ObjectType.PreOrderProduct; } break;
                    default:
                        break;
                }

                var prod = objectFactory.Get(_objType, null);
                dt = prod.GetDataTable();

                DataTable _dt = new DataTable();
                _dt = dt.Clone();
                DataRow[] filteredRows = null;

                if (grd.Rows[rowIndex].Cells[validateCellIndex].IsNotNullOrEmptyCell())
                {
                    string text = "";
                    text = grd.Rows[rowIndex].Cells[validateCellIndex].EditedFormattedValue.ToString();
                    if (!string.IsNullOrEmpty(text))
                    {
                        SelectItem(text, dt, _dt, ref filteredRows, isLike);

                        if (filteredRows == null || _dt.Rows.Count == 0)
                        {
                            string msg = "ไม่พบข้อมูลที่เลือก";
                            msg.ShowWarningMessage();

                            grd.Rows[rowIndex].Cells[validateCellIndex + 2].Value = "";

                            ret = false;
                            //validateNewRow = false;
                        }
                    }
                }
            }

            return ret;
        }

        public static void SelectItem(this string text, DataTable dt, DataTable _dt, ref DataRow[] filteredRows, bool isLike = true)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (!isLike)
                    filteredRows = dt.Select(string.Format("{0} = '{1}' OR {2} = '{3}'", "ProductID", text, "ProductName", text));
                else
                    filteredRows = dt.Select(string.Format("{0} LIKE '%{1}%' OR {2} LIKE '%{3}%'", "ProductID", text, "ProductName", text));

                foreach (var row in filteredRows)
                {
                    List<object> data = new List<object>();
                    for (int i = 0; i < row.ItemArray.Count(); i++)
                    {
                        data.Add(row[i]);
                    }
                    _dt.Rows.Add(data.ToArray());
                }
            }
        }

        public static void BindDataGridViewRow(this DataGridView grd, Dictionary<int, string> dataGridList, Dictionary<int, string> initDataGridList, DataTable dt, int idIndex, string id, int rowIndex, int colIndex, ref bool validateNewRow, string objType, bool isLike = true)
        {
            if (rowIndex != -1)
            {
                //ValidateCode(this DataGridView grd, DataTable dt, int rowIndex, int validateCellIndex)
                bool ret = grd.ValidateCode(dt, rowIndex, colIndex, objType, isLike);
                if (!ret)
                {
                    validateNewRow = false;
                    return;
                }

                if (rowIndex != 0)
                {
                    if (Helper.tbl_Users.RoleID != 10) //aloow super admin add duplicate item for support promotion 24022021
                    {
                        grd.ValidateDuplicateSKU(id, colIndex, rowIndex, ref validateNewRow);
                    }
                }
            }

            ObjectFactory objectFactory = new ObjectFactory();
            ObjectType _objType = new ObjectType();
            switch (objType)
            {
                case "ODProduct": { _objType = ObjectType.ODProduct; } break;
                case "REProduct": { _objType = ObjectType.REProduct; } break;
                case "RLProduct": { _objType = ObjectType.RLProduct; } break;
                case "RBProduct": { _objType = ObjectType.RBProduct; } break;
                case "RJProduct": { _objType = ObjectType.RJProduct; } break;
                case "RTProduct": { _objType = ObjectType.RTProduct; } break;
                case "IMProduct": { _objType = ObjectType.IMProduct; } break;
                case "IVProduct": { _objType = ObjectType.IVProduct; } break;
                case "VEProduct": { _objType = ObjectType.VEProduct; } break;
                case "IVPreProduct": { _objType = ObjectType.IVPreProduct; } break;
                case "IMPreProduct": { _objType = ObjectType.IMPreProduct; } break;
                case "PreOrderProduct": { _objType = ObjectType.PreOrderProduct; } break;
                default:
                    break;
            }

            var prod = objectFactory.Get(_objType, null);
            dt = prod.GetDataTable();

            DataTable _dt = new DataTable();
            _dt = dt.Clone();
            DataRow[] filteredRows = null;

            id.SelectItem(dt, _dt, ref filteredRows);

            grd.UpdateRow(dataGridList, initDataGridList, _dt, idIndex, grd.CurrentRow.Index, validateNewRow);
        }

        public static void BindDataGridViewRow_New(this DataGridView grdList, List<tbl_Product> allProducts, Dictionary<int, string> dataGridList, Dictionary<int, string> initDataGridList, string productID, string objType, int idIndex, int rowIndex, bool validateNewRow, Form frm, List<tbl_ProductUom> prodUOMs, BaseControl bu, int prdCellIndex, bool isCheckAll = false, int? minUOM = null)
        {
            ObjectFactory objectFactory = new ObjectFactory();
            ObjectType _objType = new ObjectType();
            switch (objType)
            {
                case "ODProduct": { _objType = ObjectType.ODProduct; } break;
                case "REProduct": { _objType = ObjectType.REProduct; } break;
                case "RLProduct": { _objType = ObjectType.RLProduct; } break;
                case "RBProduct": { _objType = ObjectType.RBProduct; } break;
                case "RJProduct": { _objType = ObjectType.RJProduct; } break;
                case "RTProduct": { _objType = ObjectType.RTProduct; } break;
                case "IMProduct": { _objType = ObjectType.IMProduct; } break;
                case "IVProduct": { _objType = ObjectType.IVProduct; } break;
                case "VEProduct": { _objType = ObjectType.VEProduct; } break;
                case "IVPreProduct": { _objType = ObjectType.IVPreProduct; } break;
                case "IMPreProduct": { _objType = ObjectType.IMPreProduct; } break;
                case "PreOrderProduct": { _objType = ObjectType.PreOrderProduct; } break;
                default:
                    break;
            }

            var prod = objectFactory.Get(_objType, null);
            var _dt1 = prod.GetDataTable();

            DataTable dt2 = new DataTable();
            dt2 = _dt1.Clone();
            DataRow[] filteredRows = null;

            productID.SelectItem(_dt1, dt2, ref filteredRows);

            if (_dt1 != null && _dt1.Rows.Count > 0)
            {
                grdList.AutoGenerateColumns = false;

                grdList.UpdateRow(allProducts, frm, dataGridList, initDataGridList, _dt1, idIndex, rowIndex, validateNewRow, prodUOMs, bu, prdCellIndex, isCheckAll, minUOM);

                for (int i = 0; i < grdList.Columns.Count; i++)
                {
                    grdList.Columns[i].DefaultCellStyle.Font = new Font("Tahoma", 9.5F);
                    grdList.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        public static void BindDataGrid(this DataGridView grdList, Dictionary<int, string> dataGridList, Dictionary<int, string> initDataGridList, DataTable _dt, int idIndex, int rowIndex, bool validateNewRow)
        {
            if (_dt != null && _dt.Rows.Count > 0)
            {
                grdList.AutoGenerateColumns = false;

                grdList.UpdateRow(dataGridList, initDataGridList, _dt, idIndex, grdList.CurrentRow.Index, validateNewRow);

                for (int i = 0; i < grdList.Columns.Count; i++)
                {
                    grdList.Columns[i].DefaultCellStyle.Font = new Font("Tahoma", 9.5F);
                    grdList.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        public static void BindDataGrid(this DataGridView grdList, List<tbl_Product> allProducts, Dictionary<int, string> dataGridList, Dictionary<int, string> initDataGridList, DataTable _dt, int idIndex, int rowIndex, bool validateNewRow, Form frm, List<tbl_ProductUom> prodUOMs, BaseControl bu, int prdCellIndex, bool isCheckAll = false, int? minUOM = null)
        {
            if (_dt != null && _dt.Rows.Count > 0)
            {
                grdList.AutoGenerateColumns = false;

                grdList.UpdateRow(allProducts, frm, dataGridList, initDataGridList, _dt, idIndex, rowIndex, validateNewRow, prodUOMs, bu, prdCellIndex, isCheckAll, minUOM);

                for (int i = 0; i < grdList.Columns.Count; i++)
                {
                    grdList.Columns[i].DefaultCellStyle.Font = new Font("Tahoma", 9.5F);
                    grdList.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        public static void BindDataGrid(this DataGridView grdList, List<tbl_Product> allProducts, Dictionary<int, string> dataGridList, Dictionary<int, string> initDataGridList, DataTable _dt, int idIndex, int rowIndex, bool validateNewRow, Form frm, List<tbl_ProductUom> prodUOMs, List<tbl_Cause> causeList, BaseControl bu, int prdCellIndex, bool isCheckAll = false)
        {
            if (_dt != null && _dt.Rows.Count > 0)
            {
                grdList.AutoGenerateColumns = false;

                grdList.UpdateRow(allProducts, frm, dataGridList, initDataGridList, _dt, idIndex, rowIndex, validateNewRow, prodUOMs, causeList, bu, prdCellIndex, isCheckAll);

                for (int i = 0; i < grdList.Columns.Count; i++)
                {
                    grdList.Columns[i].DefaultCellStyle.Font = new Font("Tahoma", 9.5F);
                    grdList.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        public static void UpdateRow(this DataGridView grd, Dictionary<int, string> dataGridList, Dictionary<int, string> initDataGridList, DataTable _dt, int idIndex, int rowIndex, bool validateNewRow)
        {
            try
            {
                if (_dt.Rows.Count > 0)
                {
                    if (!validateNewRow)
                    {
                        ShowDupSKUMessage();
                        grd.InitRowData(initDataGridList, idIndex, _dt.Rows[0]["ProductID"].ToString(), rowIndex);
                    }
                    else
                    {
                        foreach (var item in dataGridList)
                        {
                            grd.Rows[rowIndex].Cells[item.Key].Value = _dt.Rows[0][item.Value];
                        }
                    }
                }
            }
            catch { }
        }

        public static void UpdateRow(this DataGridView grd, List<tbl_Product> allProducts, Form frm, Dictionary<int, string> dataGridList, Dictionary<int, string> initDataGridList, DataTable _dt, int idIndex, int rowIndex, bool validateNewRow, List<tbl_ProductUom> prodUOMs, BaseControl bu, int prdCellIndex, bool isCheckAll = false, int? minUOM = null)
        {
            try
            {
                if (_dt.Rows.Count > 0)
                {
                    if (!validateNewRow)
                    {
                        ShowDupSKUMessage();
                        if (isCheckAll)
                        {
                            grd.InitRowData(frm, initDataGridList, idIndex, _dt.Rows[rowIndex]["ProductID"].ToString(), rowIndex, allProducts, prodUOMs, bu, prdCellIndex);
                        }
                        else
                        {
                            if (minUOM != null)
                            {
                                grd.SetMinUOM(dataGridList, _dt, rowIndex, bu);
                            }
                            else
                                grd.InitRowData(frm, initDataGridList, idIndex, _dt.Rows[0]["ProductID"].ToString(), rowIndex, allProducts, prodUOMs, bu, prdCellIndex);
                        }
                    }
                    else
                    {
                        if (isCheckAll)
                        {
                            foreach (var item in dataGridList)
                            {
                                grd.Rows[rowIndex].Cells[item.Key].Value = _dt.Rows[rowIndex][item.Value];
                            }
                        }
                        else
                        {
                            if (minUOM != null)
                            {
                                grd.SetMinUOM(dataGridList, _dt, rowIndex, bu);
                            }
                            else
                            {
                                foreach (var item in dataGridList)
                                {
                                    grd.Rows[rowIndex].Cells[item.Key].Value = _dt.Rows[0][item.Value];
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private static void SetMinUOM(this DataGridView grd, Dictionary<int, string> dataGridList, DataTable _dt, int rowIndex, BaseControl bu)
        {
            string prodID = _dt.Rows[0]["ProductID"].ToString();
            if (!string.IsNullOrEmpty(prodID))
            {
                Func<tbl_Product, bool> predicate = (x => x.ProductID == prodID);
                int _minUOM = bu.GetMinProductUOM(predicate);

                foreach (var item in dataGridList)
                {
                    if (item.Value.ToLower().Contains("uom"))
                    {
                        grd.Rows[rowIndex].Cells[item.Key].Value = _minUOM;
                    }
                    else
                    {
                        grd.Rows[rowIndex].Cells[item.Key].Value = _dt.Rows[0][item.Value];
                    }
                }
            }
        }

        public static void UpdateRow(this DataGridView grd, List<tbl_Product> allProducts, Form frm, Dictionary<int, string> dataGridList, Dictionary<int, string> initDataGridList, DataTable _dt, int idIndex, int rowIndex, bool validateNewRow, List<tbl_ProductUom> prodUOMs, List<tbl_Cause> causeList, BaseControl bu, int prdCellIndex, bool isCheckAll = false)
        {
            if (_dt.Rows.Count > 0)
            {
                int _rowIndex = 0;
                if (isCheckAll)
                    _rowIndex = rowIndex;
                else
                    _rowIndex = 0;

                if (!validateNewRow)
                {
                    ShowDupSKUMessage();
                    grd.InitRowData(frm, initDataGridList, idIndex, _dt.Rows[_rowIndex]["ProductID"].ToString(), rowIndex, allProducts, prodUOMs, causeList, bu, prdCellIndex);
                }
                else
                {
                    foreach (var item in dataGridList)
                    {
                        grd.Rows[_rowIndex].Cells[item.Key].Value = _dt.Rows[_rowIndex][item.Value];
                    }
                }
            }
        }

        public static void BindComboBoxCell<T>(this DataGridView grd, DataGridViewRow row, int rowIndex, int comboBoxIndex, List<T> objs, string displayMember, string valueMember)
        {
            DataGridViewComboBoxCell cbo = (DataGridViewComboBoxCell)(row.Cells[comboBoxIndex]);
            if (objs != null && objs.Count > 0)
            {
                cbo.DataSource = objs;
                cbo.DisplayMember = displayMember;
                cbo.ValueMember = valueMember;
            }
        }

        public static void BindComboBoxCell(this DataGridView grd, List<tbl_Product> allProducts, DataGridViewRow row, int rowIndex, bool flagNewRow, int comboBoxIndex, List<tbl_ProductUom> prodUOMs, Form frm, BaseControl bu, int prdCellIndex, bool isMinUOM = false)
        {
            if (flagNewRow)
                SubBindComboBoxCell(row.Cells[comboBoxIndex], allProducts, prodUOMs, row, bu, prdCellIndex, isMinUOM);
            else
                grd.ModifyComboBoxCell(rowIndex, bu, comboBoxIndex, prdCellIndex, isMinUOM);

            if (bu is RE || bu is TabletSales)
                BindComboDiscount(bu, row.Cells[7]);
            else if (bu is RT || bu is VE)
                BindComboDiscount(bu, row.Cells[8]);

        }

        public static void BindComboBoxDiscountTypeCell(this DataGridView grd, List<tbl_Product> allProducts, DataGridViewRow row
            , int rowIndex, bool flagNewRow, int comboBoxIndex, List<tbl_ProductUom> prodUOMs, Form frm, BaseControl bu, int prdCellIndex, List<tbl_DiscountType> disTypeList, int disColIndex)
        {
            //if (flagNewRow)
            //    SubBindComboBoxCell(row.Cells[comboBoxIndex], allProducts, prodUOMs, row, bu, prdCellIndex, false);
            //else
            //    grd.ModifyComboBoxCell(rowIndex, bu, comboBoxIndex, prdCellIndex, false);

            BindComboDiscountType(bu, row.Cells[disColIndex], disTypeList);

        }

        private static void BindComboDiscount(BaseControl bu, DataGridViewCell discountCbCell)
        {
            DataGridViewComboBoxCell cboLineDiscountType = (DataGridViewComboBoxCell)(discountCbCell);
            cboLineDiscountType.DataSource = bu.GetDiscountType();
            cboLineDiscountType.DisplayMember = "DiscountTypeName";
            cboLineDiscountType.ValueMember = "DiscountTypeCode";
            cboLineDiscountType.Value = "N";
        }

        private static void BindComboDiscountType(BaseControl bu, DataGridViewCell discountCbCell, List<tbl_DiscountType> disTypeList)
        {
            DataGridViewComboBoxCell cboLineDiscountType = (DataGridViewComboBoxCell)(discountCbCell);
            cboLineDiscountType.DataSource = disTypeList;
            cboLineDiscountType.DisplayMember = "DiscountTypeName";
            cboLineDiscountType.ValueMember = "DiscountTypeCode";
            cboLineDiscountType.Value = "N";
        }

        public static void BindComboBoxCell(this DataGridView grd, DataGridViewRow row, int rowIndex, bool flagNewRow, int comboBoxIndex, List<tbl_Cause> causeList, Form frm, BaseControl bu, int prdCellIndex)
        {
            SubBindComboBoxCell(row.Cells[comboBoxIndex], causeList, row, bu, prdCellIndex);
        }

        public static void SubBindComboBoxCell(DataGridViewCell cboCell, List<tbl_Product> allProducts, List<tbl_ProductUom> prodUOMs, DataGridViewRow row, BaseControl bu, int prdCellIndex, bool isMinUOM = false)
        {
            DataGridViewComboBoxCell cboUOM = (DataGridViewComboBoxCell)(cboCell);
            if (prodUOMs != null && prodUOMs.Count > 0)
            {
                string prodID = row.Cells[prdCellIndex].EditedFormattedValue.ToString();

                if (prodUOMs.Count > 2)
                {
                    var prdUOMs = bu.GetProductUOM(prodID);
                    if (prdUOMs.Count > 0)
                    {
                        cboUOM.DataSource = prdUOMs;
                        cboUOM.DisplayMember = "ProductUomName";
                        cboUOM.ValueMember = "ProductUomID";
                    }
                }
                else
                {
                    cboUOM.DataSource = prodUOMs;
                    cboUOM.DisplayMember = "ProductUomName";
                    cboUOM.ValueMember = "ProductUomID";
                }

                if (isMinUOM)
                {
                    Func<tbl_Product, bool> predicate = (x => x.ProductID == prodID);
                    int minUOM = bu.GetMinProductUOM(predicate);
                    cboUOM.Value = minUOM;
                }
                else
                {
                    Func<tbl_Product, bool> predicate = (x => x.ProductID == prodID);
                    var purchaseUom = allProducts.Where(predicate).ToList();// bu.GetProduct(predicate);

                    if (purchaseUom != null && purchaseUom.Count > 0)
                    {
                        if (prodUOMs.Any(x => x.ProductUomID == purchaseUom[0].PurchaseUomID))
                        {
                            cboUOM.Value = purchaseUom[0].PurchaseUomID;
                        }
                    }
                    //else if (prodUOMs.Any(x => x.ProductUomID == 4))
                    //{
                    //    cboUOM.Value = 4;
                    //}
                    //else if (prodUOMs.Any(x => x.ProductUomID == 3))
                    //{
                    //    cboUOM.Value = 3;
                    //}
                    else
                    {
                        var tmpUoms = new List<tbl_ProductUom>();
                        tmpUoms.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
                        tmpUoms.AddRange(bu.GetUOM());

                        cboUOM.DataSource = tmpUoms;
                        cboUOM.DisplayMember = "ProductUomName";
                        cboUOM.ValueMember = "ProductUomID";

                        cboUOM.Value = -1;
                    }
                }
            }
        }

        public static void SubBindComboBoxCell(DataGridViewCell cboCell, List<tbl_Cause> causeList, DataGridViewRow row, BaseControl bu, int prdCellIndex)
        {
            DataGridViewComboBoxCell dataCboCell = (DataGridViewComboBoxCell)(cboCell);
            if (causeList != null && causeList.Count > 0)
            {
                dataCboCell.DataSource = causeList;
                dataCboCell.DisplayMember = "CauseName";
                dataCboCell.ValueMember = "CauseID";

                dataCboCell.Value = -1;
            }
        }

        //public static void ModifyComboBoxCell(this DataGridView grid, int rowindex, BaseControl bu, int cboIndex)
        //{
        //    int[] uomList = new int[] { 1, 2, 3, 4 };

        //    var cell0 = grid.Rows[rowindex].Cells[0];
        //    if (cell0.IsNotNullOrEmptyCell())
        //    {
        //        var prdID = cell0.EditedFormattedValue.ToString();

        //        var cboCell = grid.Rows[rowindex].Cells[cboIndex];

        //        List<tbl_ProductUom> prodUOMs = new List<tbl_ProductUom>();

        //        prodUOMs.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
        //        Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => uomList.Contains(x.ProductUomID));
        //        prodUOMs.AddRange(bu.GetProductUOM(prdID));

        //        if (prodUOMs != null && prodUOMs.Count > 0)
        //        {
        //            SubBindComboBoxCell(cboCell, prodUOMs);
        //        }
        //    }
        //}

        public static void ModifyComboBoxCell_Tunning(this DataGridView grid, List<tbl_Product> allProducts, int checkRowindex, BaseControl bu, int cboIndex, int prdCellindex, List<tbl_ProductUom> prodUOMs)
        {
            var prdCell = grid.Rows[checkRowindex].Cells[prdCellindex];
            if (prdCell.IsNotNullOrEmptyCell())
            {
                var cboCell = grid.Rows[checkRowindex].Cells[cboIndex];

                if (prodUOMs != null && prodUOMs.Count > 0)
                {
                    SubBindComboBoxCell(cboCell, allProducts, prodUOMs, grid.Rows[checkRowindex], bu, prdCellindex, false);
                }
            }
        }

        public static void ModifyComboBoxCell(this DataGridView grid, List<tbl_Product> allProducts, int checkRowindex, BaseControl bu, int cboIndex, int prdCellindex, bool isMinUOM = false)
        {
            //int[] uomList = new int[] { 1, 2, 3, 4 };

            var prdCell = grid.Rows[checkRowindex].Cells[prdCellindex];
            if (prdCell.IsNotNullOrEmptyCell())
            {
                var prdID = prdCell.EditedFormattedValue.ToString();

                var cboCell = grid.Rows[checkRowindex].Cells[cboIndex];

                List<tbl_ProductUom> prodUOMs = new List<tbl_ProductUom>();

                //prodUOMs.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
                //Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => uomList.Contains(x.ProductUomID));
                prodUOMs.AddRange(bu.GetProductUOM(prdID));

                if (prodUOMs != null && prodUOMs.Count > 0)
                {
                    SubBindComboBoxCell(cboCell, allProducts, prodUOMs, grid.Rows[checkRowindex], bu, prdCellindex, isMinUOM);
                }
            }
        }

        public static void ModifyComboBoxCell(this DataGridView grid, int checkRowindex, BaseControl bu, int cboIndex, int prdCellindex, bool isCauseCbo, bool isMinUOM = false)
        {
            var prdCell = grid.Rows[checkRowindex].Cells[prdCellindex];
            if (prdCell.IsNotNullOrEmptyCell())
            {
                var prdID = prdCell.EditedFormattedValue.ToString();

                var cboCell = grid.Rows[checkRowindex].Cells[cboIndex];

                //List<tbl_Cause> causeList = new List<tbl_Cause>();
                //causeList.AddRange(bu.GetCause());

                //if (causeList != null && causeList.Count > 0)
                //{
                //    SubBindComboBoxCell(cboCell, causeList, grid.Rows[checkRowindex], bu, prdCellindex);
                //}
            }
        }

        public static void InitRowData(this DataGridView grd, Dictionary<int, string> dataGridList, int idIndex, string id, int rowIndex)
        {
            foreach (var item in dataGridList)
            {
                if (item.Key == idIndex)
                {
                    grd.Rows[rowIndex].Cells[item.Key].Value = id;
                }
                else
                {
                    grd.Rows[rowIndex].Cells[item.Key].Value = item.Value;
                }
            }
        }

        public static void InitRowData(this DataGridView grd, Form frm, Dictionary<int, string> dataGridList, int idIndex, string id, int rowIndex, List<tbl_Product> allProducts, List<tbl_ProductUom> prodUOMs, BaseControl bu, int prdCellIndex)
        {
            try
            {
                foreach (var item in dataGridList)
                {
                    if (item.Key == idIndex)
                    {
                        grd.Rows[rowIndex].Cells[item.Key].Value = id;
                    }
                    else
                    {
                        if (item.Value.ToString() == "combobox")
                            grd.BindComboBoxCell(allProducts, grd.Rows[rowIndex], rowIndex, true, item.Key, prodUOMs, frm, bu, prdCellIndex);
                        else
                            grd.Rows[rowIndex].Cells[item.Key].Value = item.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(null);
            }

        }

        public static void InitRowData(this DataGridView grd, Form frm, Dictionary<int, string> dataGridList, int idIndex, string id, int rowIndex, List<tbl_Product> allProducts, List<tbl_ProductUom> prodUOMs, List<tbl_Cause> causeList, BaseControl bu, int prdCellIndex)
        {
            foreach (var item in dataGridList)
            {
                if (item.Key == idIndex)
                {
                    grd.Rows[rowIndex].Cells[item.Key].Value = id;
                }
                else
                {
                    if (item.Value.ToString() == "combobox1")
                    {
                        grd.BindComboBoxCell(allProducts, grd.Rows[rowIndex], rowIndex, true, item.Key, prodUOMs, frm, bu, prdCellIndex);
                    }
                    else if (item.Value.ToString() == "combobox2")
                    {
                        grd.BindComboBoxCell(grd.Rows[rowIndex], rowIndex, true, item.Key, causeList, frm, bu, prdCellIndex);
                    }
                    else
                        grd.Rows[rowIndex].Cells[item.Key].Value = item.Value;
                }
            }
        }


        public static bool ValidateDuplicateSKU(this DataGridView grdList, string _prodcutID, int colIndex, int rowIndex, ref bool validateNewRow)
        {
            bool ret = true;
            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                if (grdList[colIndex, i].IsNotNullOrEmptyCell())
                {
                    string productID = grdList[colIndex, i].EditedFormattedValue.ToString();
                    if (productID == _prodcutID && i != rowIndex)
                    {
                        ret = false;
                        validateNewRow = false;

                        break;
                    }
                }
            }

            return ret;
        }

        public static void AddNewRow(this DataGridView grd, Dictionary<int, string> initDataGridList, int idIndex, string id, int rowIndex, bool validateNewRow)
        {
            if (!validateNewRow)
                return;

            grd.Rows.Add(1);
            grd.InitRowData(initDataGridList, idIndex, id, rowIndex);

            //grd.Rows[rowIndex].Cells[7].ReadOnly = true;
        }

        public static void AddNewRow(this DataGridView grd, List<tbl_Product> allProducts, Dictionary<int, string> initDataGridList, int idIndex, string id, int rowIndex, bool validateNewRow, List<tbl_ProductUom> prodUOMs, BaseControl bu, Form frm, int prdCellIndex)
        {
            if (!validateNewRow)
                return;

            grd.Rows.Add(1);
            grd.InitRowData(frm, initDataGridList, idIndex, id, rowIndex, allProducts, prodUOMs, bu, prdCellIndex);

            //grd.Rows[rowIndex].Cells[7].ReadOnly = true;
        }

        public static void AddNewRow(this DataGridView grd, List<tbl_Product> allProducts, Dictionary<int, string> initDataGridList, int idIndex, string id, int rowIndex, bool validateNewRow, List<tbl_ProductUom> prodUOMs, List<tbl_Cause> causeList, BaseControl bu, Form frm, int prdCellIndex)
        {
            if (!validateNewRow)
                return;

            grd.Rows.Add(1);
            grd.InitRowData(frm, initDataGridList, idIndex, id, rowIndex, allProducts, prodUOMs, causeList, bu, prdCellIndex);
        }

        public static void SetRowPostPaint(this DataGridView grd, object sender, DataGridViewRowPostPaintEventArgs e, Font font)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, font, SystemBrushes.ControlText, headerBounds, centerFormat);

            SetDefaultGridViewEvent(grid);
        }

        public static void SetRowPostPaintUControl(this DataGridView grd, object sender, DataGridViewRowPostPaintEventArgs e, Font font)
        {
            var grid = sender as DataGridView;
            SetDefaultGridViewEvent(grid);
        }

        private static void SetDefaultGridViewEvent(DataGridView grd)
        {
            grd.RowsDefaultCellStyle.BackColor = Color.White;
            grd.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            grd.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(69, 171, 213);
            grd.EnableHeadersVisualStyles = false;
        }

        public static void SetRowPostPaintImage(this DataGridView grd, object sender, DataGridViewRowPostPaintEventArgs e, Font font)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1);
            var docStatus = grid.Rows[rowIdx].Cells["DocStatus"].Value;

            if (grid.Rows[e.RowIndex].Cells["DocStatus"].Value != null && !string.IsNullOrEmpty(docStatus.ToString()))
            {
                var centerFormat = new StringFormat()
                {
                    // right alignment might actually make more sense for numbers
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
                Bitmap closeImg = new Bitmap(Properties.Resources.power_off);
                Bitmap cancelmg = new Bitmap(Properties.Resources.closeBtn);
                Bitmap statusImg = docStatus.ToString() == "Closed" ? closeImg : cancelmg;

                e.Graphics.DrawImage(statusImg, headerBounds); // .DrawString(rowIdx, font, SystemBrushes.ControlText, headerBounds, centerFormat);
            }
        }

        public static void SetCellNumberOnly(this DataGridView grd, object sender, KeyPressEventArgs e, List<int> numberCell)
        {
            DataGridView tempgrd = null;
            if (sender is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl _grd = (DataGridViewTextBoxEditingControl)sender;

                tempgrd = _grd.EditingControlDataGridView;
            }
            else if (sender is DataGridViewComboBoxEditingControl)
            {
                DataGridViewComboBoxEditingControl _grd = (DataGridViewComboBoxEditingControl)sender;

                tempgrd = _grd.EditingControlDataGridView;
            }

            if (tempgrd != null)
            {
                int currentRowIndex = tempgrd.CurrentCell.RowIndex;
                int curentColIndex = tempgrd.CurrentCell.ColumnIndex;
                {
                    if (numberCell.Contains(curentColIndex))
                    {
                        e.ToNumberOnly(sender);
                    }
                }
            }
        }

        public static void SetUserDeletingRow(this DataGridView grd, object sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridView _grd = (DataGridView)sender;
            if (_grd != null && grd.CurrentRow != null)
            {
                if (_grd.Rows[e.Row.Index].Cells[0].ReadOnly)
                {
                    e.Cancel = true;
                }
            }
        }

        public static void SetDeleteKeyDown(this DataGridView grd, object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataGridView _grd = (DataGridView)sender;
                if (_grd != null && _grd.CurrentRow != null)
                {
                    int rowIndex = _grd.CurrentRow.Index;
                    if (_grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "White" || _grd.Rows[rowIndex].Cells[0].Style.BackColor.Name == "0")
                    {
                        _grd.Rows.RemoveAt(rowIndex);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        public static void SetCellClick(this DataGridView grd, object sender, DataGridViewCellEventArgs e, int[] cellEdit, int prdCellIndex)
        {
            try
            {
                DataGridView _grd = (DataGridView)sender;
                if (_grd != null && _grd.CurrentRow != null)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex == -1)
                        {
                            for (int i = 0; i < cellEdit.Length; i++)
                            {
                                _grd.Rows[e.RowIndex].Cells[cellEdit[i]].ReadOnly = true;
                            }
                        }
                        else
                        {
                            if (_grd.Rows[e.RowIndex].Cells[prdCellIndex].Style.BackColor == ColorTranslator.FromHtml("#E3E3E3"))
                            {
                                for (int i = 0; i < cellEdit.Length; i++)
                                {
                                    _grd.Rows[e.RowIndex].Cells[cellEdit[i]].ReadOnly = true;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cellEdit.Length; i++)
                                {
                                    _grd.Rows[e.RowIndex].Cells[cellEdit[i]].ReadOnly = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public static void SetCellContentClick(this DataGridView grd, Form frm, object sender, DataGridViewCellEventArgs e, string docTypeCode, int qtyCellIndex)
        {
            if (grd.Columns[e.ColumnIndex].Name == "colSearchProduct")
            {
                frm.OpenCellProductPopup("เลือกสินค้า", docTypeCode, e.RowIndex);

                if (grd.Columns[0].Visible)
                {
                    grd.CurrentCell = grd.Rows[e.RowIndex].Cells[qtyCellIndex];
                }
                else
                {
                    grd.CurrentCell = grd.Rows[e.RowIndex].Cells[qtyCellIndex - 1];
                }

                grd.CurrentCell.Selected = true;

                //grdList.CellContentClick -= grdList_CellContentClick;
            }
        }

        public static void CheckCancelDoc(this string docStatus, bool checkEditMode, AddButton btnAdd, CopyButton btnCopy, EditButton btnEdit)
        {
            if (checkEditMode && docStatus == "5")
            {
                btnAdd.Enabled = true;
                btnCopy.Enabled = true;

                btnEdit.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = true;
            }
        }

        public static void SetCellPainting(this DataGridView grd, object sender, DataGridViewCellPaintingEventArgs e, int colMergeIndex)
        {
            if (e.ColumnIndex == colMergeIndex)
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                    return;
                if (IsTheSameCellValue(grd, e.ColumnIndex, e.RowIndex))
                {
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    e.AdvancedBorderStyle.Top = grd.AdvancedCellBorderStyle.Top;
                }
            }
        }

        private static bool IsTheSameCellValue(DataGridView grd, int column, int row)
        {
            DataGridViewCell cell1 = grd[column, row];
            DataGridViewCell cell2 = grd[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }

        public static void SetCellFormatting(this DataGridView grd, object sender, DataGridViewCellFormattingEventArgs e, int colMergeIndex)
        {
            if (e.ColumnIndex == colMergeIndex)
            {
                if (e.RowIndex == 0)
                    return;
                if (IsTheSameCellValue(grd, e.ColumnIndex, e.RowIndex))
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                }
            }
        }

        public static void ShowDupSKUMessage()
        {
            string msg = "รหัสสินค้าซ้ำ มีข้อมูลนี้อยู่ในรายการแล้ว";
            msg.ShowWarningMessage();
        }

        public static bool ValidateEndDay(this DateTimePicker docDate, BaseControl bu)
        {
            bool ret = false;
            var comp = bu.tbl_Companies.First(); // bu.GetCompany(); //Last edit by sailom .k 07/02/2022
            DateTime cDate = docDate.Value.ToDateTimeFormat();

            //Func<tbl_SaleBranchSummary, bool> predicate = (x => x.BranchID == comp.CompanyCode && x.SaleDate == cDate && x.FlagDel == false);
            //var tbl_SaleBranchSummary = bu.GetSaleBranchSummary(predicate);
            var tbl_SaleBranchSummary = bu.ValidateCheckEndDay(comp.CompanyCode, cDate); //Last edit by sailom .k 07/02/2022
            if (tbl_SaleBranchSummary != null) // is end day
            {
                ret = false;
            }
            else //not end say
            {
                ret = true;
            }

            return ret;
        }



        public static void BindComboBoxCell(this DataGridView grd, DataGridViewRow row, int rowIndex, bool flagNewRow, int comboBoxIndex, List<tbl_ProductUom> prodUOMs, Form frm, BaseControl bu, int prdCellIndex, bool isMinUOM = false)
        {
            if (flagNewRow)
                SubBindComboBoxCell(row.Cells[comboBoxIndex], prodUOMs, row, bu, prdCellIndex, isMinUOM);
            else
                grd.ModifyComboBoxCell(prodUOMs, rowIndex, bu, comboBoxIndex, prdCellIndex, isMinUOM);//

            if (bu is RE || bu is TabletSales)
                BindComboDiscount(bu, row.Cells[7]);
            else if (bu is RT || bu is VE)
                BindComboDiscount(bu, row.Cells[8]);

        }


        public static void SubBindComboBoxCell(DataGridViewCell cboCell, List<tbl_ProductUom> prodUOMs, DataGridViewRow row, BaseControl bu, int prdCellIndex, bool isMinUOM = false)
        {
            DataGridViewComboBoxCell cboUOM = (DataGridViewComboBoxCell)(cboCell);
            if (prodUOMs != null && prodUOMs.Count > 0)
            {
                cboUOM.DataSource = prodUOMs;
                cboUOM.DisplayMember = "ProductUomName";
                cboUOM.ValueMember = "ProductUomID";

                string prodID = row.Cells[prdCellIndex].EditedFormattedValue.ToString();

                if (isMinUOM)
                {
                    Func<tbl_Product, bool> predicate = (x => x.ProductID == prodID);
                    int minUOM = bu.GetMinProductUOM(predicate);
                    cboUOM.Value = minUOM;
                }
                else
                {
                    Func<tbl_Product, bool> predicate = (x => x.ProductID == prodID);
                    var purchaseUom = bu.GetProduct(predicate);

                    if (purchaseUom != null && purchaseUom.Count > 0)
                    {
                        if (prodUOMs.Any(x => x.ProductUomID == purchaseUom[0].PurchaseUomID))
                        {
                            cboUOM.Value = purchaseUom[0].PurchaseUomID;
                        }
                    }
                    //else if (prodUOMs.Any(x => x.ProductUomID == 4))
                    //{
                    //    cboUOM.Value = 4;
                    //}
                    //else if (prodUOMs.Any(x => x.ProductUomID == 3))
                    //{
                    //    cboUOM.Value = 3;
                    //}
                    else
                    {
                        var tmpUoms = new List<tbl_ProductUom>();
                        tmpUoms.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
                        tmpUoms.AddRange(bu.GetUOM());

                        cboUOM.DataSource = tmpUoms;
                        cboUOM.DisplayMember = "ProductUomName";
                        cboUOM.ValueMember = "ProductUomID";

                        cboUOM.Value = -1;
                    }
                }
            }
        }



        public static void ModifyComboBoxCell(this DataGridView grid, List<tbl_ProductUom> prodUOMs, int checkRowindex, BaseControl bu, int cboIndex, int prdCellindex, bool isMinUOM = false)
        {
            //int[] uomList = new int[] { 1, 2, 3, 4 };

            var prdCell = grid.Rows[checkRowindex].Cells[prdCellindex];//
            if (prdCell.IsNotNullOrEmptyCell())
            {
                var prdID = prdCell.EditedFormattedValue.ToString();

                var cboCell = grid.Rows[checkRowindex].Cells[cboIndex];

                //List<tbl_ProductUom> prodUOMs = new List<tbl_ProductUom>();

                //prodUOMs.Add(new tbl_ProductUom { ProductUomID = -1, ProductUomName = "เลือก" });
                //Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => uomList.Contains(x.ProductUomID));
                //prodUOMs.AddRange(bu.GetProductUOM(prdID));


                if (prodUOMs != null && prodUOMs.Count > 0)
                {
                    SubBindComboBoxCell(cboCell, prodUOMs, grid.Rows[checkRowindex], bu, prdCellindex, isMinUOM);
                }
            }
        }



        public static void AddNewRow(this DataGridView grd, Dictionary<int, string> initDataGridList, int idIndex, string id, int rowIndex, bool validateNewRow, List<tbl_ProductUom> prodUOMs, BaseControl bu, Form frm, int prdCellIndex)
        {
            if (!validateNewRow)//Method
                return;

            grd.Rows.Add(1);
            grd.InitRowData(frm, initDataGridList, idIndex, id, rowIndex, prodUOMs, bu, prdCellIndex);

            //grd.Rows[rowIndex].Cells[7].ReadOnly = true;
        }

        public static void InitRowData(this DataGridView grd, Form frm, Dictionary<int, string> dataGridList, int idIndex, string id, int rowIndex, List<tbl_ProductUom> prodUOMs, BaseControl bu, int prdCellIndex)
        {
            foreach (var item in dataGridList) //new Method
            {
                if (item.Key == idIndex)
                {
                    grd.Rows[rowIndex].Cells[item.Key].Value = id;//
                }
                else
                {
                    if (item.Value.ToString() == "combobox")
                        grd.BindComboBoxCell(grd.Rows[rowIndex], rowIndex, true, item.Key, prodUOMs, frm, bu, prdCellIndex);
                    else
                        grd.Rows[rowIndex].Cells[item.Key].Value = item.Value;
                }
            }
        }

        //public static void AddNewRow(this DataGridView grd, Dictionary<int, string> initDataGridList, int idIndex, string id, int rowIndex, bool validateNewRow, List<tbl_ProductUom> prodUOMs, BaseControl bu, Form frm, int prdCellIndex)
        //{
        //    if (!validateNewRow)
        //        return;

        //    grd.Rows.Add(1);
        //    grd.InitRowData(frm, initDataGridList, idIndex, id, rowIndex, prodUOMs, bu, prdCellIndex);

        //    //grd.Rows[rowIndex].Cells[7].ReadOnly = true;
        //}


        //public static void BindComboBoxCell(this DataGridView grd, DataGridViewRow row, int rowIndex, bool flagNewRow, int comboBoxIndex, List<tbl_ProductUom> prodUOMs, Form frm, BaseControl bu, int prdCellIndex, bool isMinUOM = false)
        //{
        //    if (flagNewRow)
        //        SubBindComboBoxCell(row.Cells[comboBoxIndex], prodUOMs, row, bu, prdCellIndex, isMinUOM);
        //    else
        //        grd.ModifyComboBoxCell(rowIndex, bu, comboBoxIndex, prdCellIndex, isMinUOM);

        //    if (bu is RE || bu is TabletSales)
        //        BindComboDiscount(bu, row.Cells[7]);
        //    else if (bu is RT || bu is VE)
        //        BindComboDiscount(bu, row.Cells[8]);

        //}

        public static void SetTextBoxNumberOnly(this TextBox txt, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }
        public static void SetTextBoxNumberWithDot(this TextBox txt, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
