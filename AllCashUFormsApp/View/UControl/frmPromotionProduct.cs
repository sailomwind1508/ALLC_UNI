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
    public partial class frmPromotionProduct : Form
    {
        DataTable dt = new DataTable();
        ObjectFactory objectFactory = new ObjectFactory();
        private ObjectType _objType;
        private string formName = "";
        private List<DataGridColumn> _gridColumn = new List<DataGridColumn>();
        private string formText = "";
        private static List<Control> controlList = new List<Control>();
        private static string[] conditionString = null;
        public static Dictionary<string, int> mmchProList = new Dictionary<string, int>();

        PromotionTemp bu = new PromotionTemp();
        Promotion pro = new Promotion();

        public frmPromotionProduct()
        {
            InitializeComponent();
        }

        #region private medthods

        private void BindDataGrid(DataTable _dt)
        {
            grdList.ClearControl();
            grdList.ClearSelection();
            grdList.MultiSelect = false;
            string expression;
            expression = "ProductSubGroupID = 23";

            DataTable _dtTemp = new DataTable();
            _dtTemp = _dt.Clone();
            _dtTemp.Clear();
            DataRow[] foundRows = _dt.Select(expression);
            foreach (DataRow r in foundRows)
            {
                _dtTemp.Rows.Add(r[0], r[1], r[2], r[3], r[4], r[5]);
            }
            grdList.DataSource = _dtTemp;
            bu.tbl_HQ_Promotion_Hit_Temps = new List<tbl_HQ_Promotion_Hit_Temp>();
            bu.tbl_HQ_Promotion_Hit_Temps = bu.GetAllData();

            for (int i = 0; i < _dtTemp.Rows.Count; i++)
            {
                bool isReadOnly = false;
                var cell0 = grdList.Rows[i].Cells[0];
                var cellDT0 = Convert.ToBoolean(_dt.Rows[i][0]);
                isReadOnly = cellDT0;
                //if (cell0.IsNotNullOrEmptyCell())
                //{
                //    if (cell0.Value is bool)
                //    {
                //        isReadOnly = (bool)cell0.Value;
                //    }
                //}

                DataGridViewCheckBoxCell chkchecking = cell0 as DataGridViewCheckBoxCell;
                chkchecking.Value = isReadOnly;
                grdList.Rows[i].ReadOnly = isReadOnly;
            }

            InitData(_dt);
        }

        private void InitData(DataTable _dt)
        {

            var proInfo = pro.GetHQPromotion(a => bu.tbl_HQ_Promotion_Hit_Temps.Select(x => x.PromotionID).Contains(a.PromotionID));
            if (proInfo.Count > 0)
            {
                var item = proInfo.FirstOrDefault(x => x.PromotionType == "mmch");
                if (item != null)
                {

                    var pro = bu.tbl_HQ_Promotion_Hit_Temps.FirstOrDefault(x => x.PromotionID == item.PromotionID);
                    if (pro != null)
                    {
                        int totalAmt = pro.SKUGroupRewardAmt2.Value > 0 ? pro.SKUGroupRewardAmt2.Value : pro.SKUGroupRewardAmt.Value;
                        lblCountList.Text = "/0" + totalAmt.ToNumberFormat(); //_dt.Rows.Count.ToNumberFormat();

                        //if (pro.SKUGroupRewardAmt != null && pro.SKUGroupRewardAmt2 != null)
                        //{
                        //    for (int i = 0; i < _dt.Rows.Count; i++)
                        //    {
                        //        var cell0 = grdList.Rows[i].Cells[0];
                        //        var collID = _dt.Rows[i]["ProductID"].ToString();
                        //        var prod = bu.GetHQ_SKUGroup(x => x.SKUGroupID == pro.SKUGroupRewardID);
                        //        if (collID == prod.First().SKU_ID)
                        //        {
                        //            grdList.Rows[i].Cells[2].Value = pro.SKUGroupRewardAmt.Value;

                        //            DataGridViewCheckBoxCell chkchecking = cell0 as DataGridViewCheckBoxCell;
                        //            chkchecking.Value = true;
                        //            //grdList.Rows[i].ReadOnly = true;

                        //            chkchecking.ReadOnly = true;
                        //            grdList.Rows[i].Cells[2].ReadOnly = true;

                        //        }
                        //    }
                        //}

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

        public Dictionary<string, int> GetMMCHList()
        {
            return mmchProList;
        }

        private void SetMMCHProList(int rowIndex)
        {
            try
            {
                var cell4 = grdList.Rows[rowIndex].Cells[4];

                if (cell4.IsNotNullOrEmptyCell())
                {
                    string promotionID = cell4.Value.ToString();
                    var proInfo = pro.GetHQPromotion(a => bu.tbl_HQ_Promotion_Hit_Temps.Select(x => x.PromotionID).Contains(a.PromotionID));
                    if (proInfo.Count > 0)
                    {
                        var item = proInfo.FirstOrDefault(x => x.PromotionType == "mmch");
                        if (item != null)
                        {
                            var cell0 = grdList.Rows[rowIndex].Cells[0];
                            if (cell0 is DataGridViewCheckBoxCell)
                            {
                                DataGridViewCheckBoxCell chkchecking = cell0 as DataGridViewCheckBoxCell;

                                if (Convert.ToBoolean(chkchecking.EditedFormattedValue) == true)
                                {
                                    var cell2 = grdList.Rows[rowIndex].Cells[2];
                                    if (cell2.IsNotNullOrEmptyCell())
                                    {
                                        mmchProList.Remove(grdList.Rows[rowIndex].Cells[3].Value.ToString());

                                        if (Convert.ToInt32(cell2.EditedFormattedValue) > 0)
                                            mmchProList.Add(grdList.Rows[rowIndex].Cells[3].Value.ToString(), Convert.ToInt32(cell2.EditedFormattedValue));

                                        if (rowIndex == grdList.RowCount - 1)
                                        {
                                            if (dt != null && dt.Rows.Count > 0)
                                            {
                                                string expression;
                                                expression = "ProductSubGroupID <> 23";

                                                DataRow[] foundRows = dt.Select(expression);
                                                foreach (DataRow r in foundRows)
                                                {
                                                    string prdID = r["ProductID"].ToString();
                                                    mmchProList.Remove(prdID);
                                                    mmchProList.Add(prdID, Convert.ToInt32(r["ProductQty"]));
                                                }
                                            }
                                        }
                                            
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
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
                case "PromotionProduct": { _objType = ObjectType.PromotionProduct; } break;
                default:
                    break;
            }

            formName = frmName;
            formText = popUPText;
            _gridColumn = gridColumn;

            controlList = _controls;
        }

        #endregion

        #region event methods

        private void frmPromotionProduct_Load(object sender, EventArgs e)
        {
            this.Text = formText;
            mmchProList = new Dictionary<string, int>();
            var obj = objectFactory.Get(_objType, null);
            grdList.AutoGenerateColumns = false;

            dt = obj.GetDataTableByCondition(conditionString);
            if (dt != null && dt.Rows.Count > 0)
            {
                Search();
            }
            else
            {
                this.Close();
            }
            
        }

        private void frmPromotionProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            mmchProList = new Dictionary<string, int>();
            if (grdList != null && grdList.RowCount > 0)
            {
                for (int i = 0; i < grdList.RowCount; i++)
                {
                    SetMMCHProList(i);
                }

                var _ret = ValidateQty(grdList, 0, 2);
                e.Cancel = !_ret ? true : false;
            }
        }

        private void frmPromotionProduct_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void grdList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grdList != null && grdList.RowCount > 0)
                {
                    if (grdList.Columns[e.ColumnIndex].Name == "colChoose")
                    {
                        var currentCell = grdList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        if (currentCell is DataGridViewCheckBoxCell)
                        {
                            var cell2 = grdList.Rows[e.RowIndex].Cells[2];
                            DataGridViewTextBoxCell txt = cell2 as DataGridViewTextBoxCell;

                            DataGridViewCheckBoxCell currentCellChk = currentCell as DataGridViewCheckBoxCell;
                            txt.ReadOnly = !Convert.ToBoolean(currentCellChk.EditedFormattedValue);
                        }
                    }
                }
            }
            catch
            {
            }

        }

        private void grdList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;

                    tb.KeyPress -= DataGridView_KeyPress;
                    tb.KeyPress += DataGridView_KeyPress;
                }
            }
            catch
            {


            }

        }

        private void DataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                grdList.SetCellNumberOnly(sender, e, new List<int>() { 2 });
            }
            catch
            {

            }
        }

        delegate void SetColumnIndex(int i);

        private bool ValidateQty(DataGridView grd, int rowIndex, int colIndex, bool isEndEdit = false)
        {
            bool ret = true;
            try
            {
                if (grdList.DataSource != null)
                {
                    //var grd = grd as DataGridView;
                    if (grd != null && grd.RowCount > 0)
                    {
                        int totalAmt = 0;
                        for (int i = 0; i < grd.Rows.Count; i++)
                        {
                            var cell0 = grdList.Rows[i].Cells[0];
                            var cell3 = grdList.Rows[i].Cells[3];
                            if (cell0 is DataGridViewCheckBoxCell)
                            {
                                DataGridViewCheckBoxCell chkchecking = cell0 as DataGridViewCheckBoxCell;
                                if (Convert.ToBoolean(chkchecking.EditedFormattedValue) == true)
                                {
                                    var cell2 = grdList.Rows[i].Cells[2];
                                    if (cell2.IsNotNullOrEmptyCell() && !cell0.ReadOnly)
                                    {
                                        int amt = Convert.ToInt32(cell2.EditedFormattedValue);
                                        totalAmt += amt;

                                    }
                                }
                            }
                        }

                        var proInfo = pro.GetHQPromotion(a => bu.tbl_HQ_Promotion_Hit_Temps.Select(x => x.PromotionID).Contains(a.PromotionID));
                        if (proInfo.Count > 0)
                        {
                            var item = proInfo.FirstOrDefault(x => x.PromotionType == "mmch");
                            if (item != null)
                            {

                                var pro = bu.tbl_HQ_Promotion_Hit_Temps.FirstOrDefault(x => x.PromotionID == item.PromotionID);
                                if (pro != null)
                                {
                                    int _totalAmt = pro.SKUGroupRewardAmt2.Value > 0 ? pro.SKUGroupRewardAmt2.Value : pro.SKUGroupRewardAmt.Value;
                                    if (totalAmt > _totalAmt)
                                    {
                                        string msg = "จำนวนของแถมเกินกว่าที่กำหนดไว้!!!";
                                        msg.ShowWarningMessage();
                                        ret = false;

                                        if (isEndEdit)
                                        {
                                            grdList.CurrentCell = grd.Rows[rowIndex].Cells[colIndex - 1];
                                            grdList.BeginEdit(true);
                                        }
                                        
                                    }
                                    else
                                    {
                                        lblCountList.Text = totalAmt.ToNumberFormat() + "/" + _totalAmt.ToNumberFormat();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                ret = false;
                for (int i = 0; i < grdList.Rows.Count; i++)
                {
                    grdList.Rows[i].Cells[2].Value = 0;
                }
            }

            return ret;
        }

        private void grdList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var grd = sender as DataGridView;
            ValidateQty(grd, e.RowIndex, e.ColumnIndex, true);
        }

        private void Mymethod(int columnIndex)
        {
            grdList.CurrentCell = grdList.CurrentRow.Cells[columnIndex];
            grdList.BeginEdit(true);
        }

        #endregion

    }
}
