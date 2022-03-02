using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmPromotion : Form
    {
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        private ObjectType _objType;
        private string formName = "";
        private List<DataGridColumn> _gridColumn = new List<DataGridColumn>();
        private string formText = "";
        private static List<Control> controlList = new List<Control>();
        private static string[] conditionString = null;

        PromotionTemp bu = new PromotionTemp();
        Promotion pro = new Promotion();

        public frmPromotion()
        {
            InitializeComponent();
        }

        public void PreparePopupForm(string type, string frmName, string popUPText, List<DataGridColumn> gridColumn, int? _rowIndex = null, List<Control> _controls = null, string[] _conditionString = null)
        {
            PreparePopupFactory(type, frmName, popUPText, gridColumn, _rowIndex, _controls);
            conditionString = _conditionString;
        }

        private void PreparePopupFactory(string type, string frmName, string popUPText, List<DataGridColumn> gridColumn, int? _rowIndex = null, List<Control> _controls = null)
        {
            switch (type)
            {

                case "Promotion": { _objType = ObjectType.Promotion; } break;
                case "PromotionTemp": { _objType = ObjectType.PromotionTemp; } break;
                case "PromotionProduct": { _objType = ObjectType.PromotionProduct; } break;

                default:
                    break;
            }

            formName = frmName;
            formText = popUPText;
            _gridColumn = gridColumn;

            controlList = _controls;
        }

        private void frmPromotion_Load(object sender, EventArgs e)
        {
            this.Text = formText;
            var obj = objectFactory.Get(_objType, null);
            grdList.AutoGenerateColumns = false;

            dt = obj.GetDataTableByCondition(conditionString);

            if (_gridColumn[0].AddNumberInFirstRow)
            {
                grdList.RowPostPaint += gridView_RowPostPaint;
                grdList.RowHeadersVisible = true;
            }
            else
            {
                grdList.RowPostPaint -= gridView_RowPostPaint;
                grdList.RowHeadersVisible = false;
            }

            Search();
        }

        private void gridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void BindDataGrid(DataTable _dt)
        {
            grdList.DataSource = _dt;

            lblCountList.Text = _dt.Rows.Count.ToNumberFormat();

            bu.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
            bu.tbl_HQ_Promotion_Hit_Temps = bu.GetAllData();

            for (int i = 0; i < grdList.RowCount; i++)
            {
                var cell0 = grdList.Rows[i].Cells[0];
                var cell2 = grdList.Rows[i].Cells[2];

                if (cell2.IsNotNullOrEmptyCell())
                {
                    string promotionID = cell2.Value.ToString();
                    var proInfo = pro.GetHQPromotion(a => a.PromotionID == promotionID);

                    if (proInfo != null && proInfo.Count > 0 && proInfo[0].PromotionType == "mmc")
                    {
                        if (cell0 is DataGridViewCheckBoxCell)
                        {
                            grdList.Rows[i].Cells[0].Value = false;
                            grdList.Rows[i].Cells[0] = new DataGridViewTextBoxCell();
                            grdList.Rows[i].Cells[0].Style.Padding = new Padding(grdList.Rows[i].Cells[2].OwningColumn.Width, 0, 0, 0);
                            grdList.Rows[i].Cells[0].ReadOnly = true;
                            //break;
                        }
                    }
                }
            }
        }

        private void Search()
        {
            try
            {
                BindDataGrid(dt);
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
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
                    if (selectRow.Cells[2].IsNotNullOrEmptyCell())
                        selectCode = selectRow.Cells[2].Value.ToString();

                    if (!string.IsNullOrEmpty(selectCode))
                    {
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f.Name.ToLower() == formName.ToLower())
                            {
                                Form frm = (Form)f;

                                switch (_objType)
                                {
                                    case ObjectType.Promotion: frm.BindData("Promotion", controlList, selectCode); break;
                                    case ObjectType.PromotionTemp: frm.BindData("PromotionTemp", controlList, selectCode); break;

                                    default:
                                        break;
                                }

                                //this.Close();
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void frmPromotion_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                List<tbl_HQ_Promotion_Hit_Temp> proTmpList = new List<tbl_HQ_Promotion_Hit_Temp>();
                List<tbl_HQ_Promotion_Hit_Temp> mmchPro = new List<tbl_HQ_Promotion_Hit_Temp>();
                List<tbl_HQ_Promotion_Hit_Temp> mmchProUnCheck = new List<tbl_HQ_Promotion_Hit_Temp>();
                List<tbl_HQ_Promotion_Hit_Temp> mmcPro = new List<tbl_HQ_Promotion_Hit_Temp>();

                //foreach (tbl_HQ_Promotion_Hit_Temp p in bu.tbl_HQ_Promotion_Hit_Temps)
                //{
                //    if (bu.tbl_HQ_Promotion_Hit_Temps != null && bu.tbl_HQ_Promotion_Hit_Temps.Count > 0)
                //    {
                //        var item = bu.tbl_HQ_Promotion_Hit_Temps.FirstOrDefault(x => x.PromotionID == p.PromotionID);
                //        if (item != null)
                //        {
                //            var skuList = bu.GetHQSKUGroup(x => x.SKUGroupID == item.SKUGroupRewardID);
                //            if (skuList != null && skuList.Count > 0)
                //            {
                //                var prd = bu.GetProduct(x => skuList.Select(a => a.SKU_ID).Contains(x.ProductID)).ToList();
                //                if (prd != null && prd.Count > 0)
                //                {
                //                    if (prd.Any(x => x.Flavour == "ชั้นวาง" || x.ProductName.Contains("ชั้นวาง")))
                //                    {
                //                        tmpList.Add(p);
                //                        break;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}

                for (int i = 0; i < grdList.RowCount; i++)
                {
                    var cell2 = grdList.Rows[i].Cells[2];

                    if (cell2.IsNotNullOrEmptyCell())
                    {
                        string promotionID = cell2.Value.ToString();
                        var proInfo = pro.GetHQPromotion(a => a.PromotionID == promotionID);

                        if (proInfo != null && proInfo.Count > 0)
                        {
                            if (proInfo[0].PromotionType == "mmc")
                            {
                                bu.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                                bu.tbl_HQ_Promotion_Hit_Temps = bu.GetAllData();

                                var mmcPros = bu.tbl_HQ_Promotion_Hit_Temps.Where(x => x.PromotionID == promotionID).ToList();
                                mmcPro.AddRange(mmcPros);
                            }
                            else if(proInfo[0].PromotionType == "mmch")
                            {
                                var cell0 = grdList.Rows[i].Cells[0];
                                if (cell0 is DataGridViewCheckBoxCell)
                                {
                                    DataGridViewCheckBoxCell chkchecking = cell0 as DataGridViewCheckBoxCell;
                                    //string promotionID = cell2.Value.ToString();

                                    var item = bu.tbl_HQ_Promotion_Hit_Temps.FirstOrDefault(x => x.PromotionID == promotionID);
                                    if (item != null)
                                    {
                                        if (Convert.ToBoolean(chkchecking.EditedFormattedValue) == true)
                                            mmchPro.Add(item);
                                        else
                                            mmchProUnCheck.Add(item);
                                    }
                                }
                            }
                        }
                    }
                }

                //Remove promotion uncheck
                foreach (var _proItem in mmchProUnCheck)
                {
                    int ret = pro.RemoveTempData(_proItem);
                }

                //add promotion mmc and checked mmch
                proTmpList.AddRange(mmcPro);
                proTmpList.AddRange(mmchPro);

                //for support u-online last edit by sailom .k 18-06-2021
                foreach (var _proItem in proTmpList)
                {
                    int ret = pro.RemoveTempData(_proItem);

                    if (ret == 1 && proTmpList.Count > 0)
                    {
                        pro.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
                        pro.tbl_HQ_Promotion_Hit_Temps.Add(_proItem);

                        ret = pro.AddTempData();
                    }
                }

                if (mmchPro.Count > 0) //have mmch choose //16/11/2021 by sailom .k
                {
                    frmPromotionProduct _frm = new frmPromotionProduct();

                    List<DataGridColumn> colList = new List<DataGridColumn>();
                    AddPromotionPopupCols(_frm, colList);

                    _frm.PreparePopupForm("PromotionProduct", _frm.Name, "เลือกของแถม", colList, null, null, null);
                    _frm.ShowDialog();
                }

            }
            catch (Exception ex)
            {

                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        public static void AddPromotionPopupCols(frmPromotionProduct _frm, List<DataGridColumn> colList)
        {
            colList.Add(new DataGridColumn() { DataPropertyName = "Choose", HeaderText = "เลือก", Name = "Choose", Width = 50, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter, ColoumnType = new DataGridViewCheckBoxColumn() });
            colList.Add(new DataGridColumn() { DataPropertyName = "ProductName", HeaderText = "ชื่อโปรโมชั่น", Name = "ProductName", Width = 150, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });
            colList.Add(new DataGridColumn() { DataPropertyName = "ProductQty", HeaderText = "รหัสโปรโมชั่น", Name = "ProductQty", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "ProductID", HeaderText = "ProductID", Name = "PromotionID", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Visibility = false });
        }

        private void frmPromotion_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdList.Columns[e.ColumnIndex].Name == "colChoose")
            {
                var currentCell = grdList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (currentCell is DataGridViewCheckBoxCell)
                {
                    DataGridViewCheckBoxCell currentCellChk = currentCell as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(currentCellChk.EditedFormattedValue) == true)
                    {
                        for (int i = 0; i < grdList.RowCount; i++)
                        {
                            if (e.RowIndex != i)
                            {
                                var cell0 = grdList.Rows[i].Cells[0];
     
                                if (cell0 is DataGridViewCheckBoxCell)
                                {
                                    DataGridViewCheckBoxCell chk = cell0 as DataGridViewCheckBoxCell;
                                    chk.Value = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
